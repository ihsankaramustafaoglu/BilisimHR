using bilisimHR.Business.Model.Auth;
using bilisimHR.Common.Core;
using bilisimHR.Common.Helper;
using bilisimHR.Infrastructure.Dependency.CastleWindsor;
using bilisimHR.Services.Auth.Interfaces;
using Microsoft.Owin.Security.Infrastructure;
using System;
using System.Threading.Tasks;

namespace bilisimHR.API.Employees.Providers
{
    /// <summary>
    /// Refresh token provider which creates and receives referesh token
    /// </summary>
    public class RefreshTokenProvider : AuthenticationTokenProvider
    {
        private IRefreshTokenService _refreshTokenService;
        private ICoreLogger _logger;

        /// <summary>
        /// RefreshTokenProvider
        /// </summary>
        public RefreshTokenProvider()
        {
            _refreshTokenService = WebApiInstaller.Resolve<IRefreshTokenService>();
            _logger = WebApiInstaller.Resolve<ICoreLogger>();
        }

        /// <summary>
        /// CreateRefreshToken async
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public async override Task CreateAsync(AuthenticationTokenCreateContext context)
        {
            try
            {
                //Create(context);
                var clientid = context.Ticket.Properties.Dictionary["as:client_id"];

                if (string.IsNullOrEmpty(clientid))
                {
                    return;
                }

                var refreshTokenId = Guid.NewGuid().ToString("n");

                var refreshTokenLifeTime = context.OwinContext.Get<string>("as:clientRefreshTokenLifeTime");

                var token = new RefreshTokenModel()
                {
                    RefToken = Utility.GetHash(refreshTokenId),
                    ClientId = clientid,
                    UserName = context.Ticket.Identity.Name,
                    IssuedUtc = DateTime.UtcNow,
                    ExpiresUtc = DateTime.UtcNow.AddMinutes(Convert.ToDouble(refreshTokenLifeTime))
                };

                context.Ticket.Properties.IssuedUtc = token.IssuedUtc;
                context.Ticket.Properties.ExpiresUtc = token.ExpiresUtc;

                token.ProtectedTicket = context.SerializeTicket();

                var existingToken = await _refreshTokenService.GetByClientIdAndUserNameAsync(token.ClientId, token.UserName);

                if (existingToken != null)
                    await _refreshTokenService.DeleteAsync(existingToken.Id);

                await _refreshTokenService.InsertAsync(token);

                context.SetToken(refreshTokenId);
            }
            catch (Exception ex)
            {
                _logger.Error(GetType(), ex, "RefreshTokenProvider/CreateAsync");
                throw ex;
            }
        }

        /// <summary>
        /// ReceiveRefreshToken async
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public async override Task ReceiveAsync(AuthenticationTokenReceiveContext context)
        {
            var allowedOrigin = context.OwinContext.Get<string>("as:clientAllowedOrigin");
            context.OwinContext.Response.Headers.Add("Access-Control-Allow-Origin", new[] { allowedOrigin });

            string hashedTokenId = Utility.GetHash(context.Token);

            var refreshToken = await _refreshTokenService.GetByRefTokenAsync(hashedTokenId);

            if (refreshToken != null)
            {
                //Get protectedTicket from refreshToken class
                context.DeserializeTicket(refreshToken.ProtectedTicket);
                await _refreshTokenService.DeleteAsync(refreshToken.Id);
            }

            //Receive(context);
        }

        /// <summary>
        /// CreateRefreshToken
        /// </summary>
        /// <param name="context"></param>
        public override void Create(AuthenticationTokenCreateContext context)
        {
            throw new NotImplementedException();
            //context.SetToken(context.SerializeTicket());
        }
        
        /// <summary>
        /// ReceiveRefreshToken
        /// </summary>
        /// <param name="context"></param>
        public override void Receive(AuthenticationTokenReceiveContext context)
        {
            throw new NotImplementedException();
            //context.DeserializeTicket(context.Token);
            
            //if (context.Ticket == null)
            //{
            //    context.Response.StatusCode = 400;
            //    context.Response.ContentType = context.Request.ContentType;
            //    context.Response.ReasonPhrase = "invalid token";
            //    return;
            //}

            //context.SetTicket(context.Ticket);
        }
    }
}