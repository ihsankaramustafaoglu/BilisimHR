using bilisimHR.Business.Model;
using bilisimHR.Business.Model.Auth;
using bilisimHR.Common.Core;
using bilisimHR.Common.Helper;
using bilisimHR.Infrastructure.Dependency.CastleWindsor;
using bilisimHR.Services.Auth.Interfaces;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.OAuth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;
using System.Web.Http;

namespace bilisimHR.API.Authentication.Providers
{
    /// <summary>
    /// Authorization server provider which controls the lifecycle of Authorization Server
    /// </summary>
    public class ApplicationOAuthProvider : OAuthAuthorizationServerProvider
    {
        //private KullanicilarModel _kullanici;
        //private IKullanicilarService _kullanicilarService;
        private UsersModel _user;
        private ClientsModel _client;
        private IUsersService _userService;
        private IClientsService _clientService;
        private ICoreLogger _logger;

        /// <summary>
        /// ApplicationOAuthProvider
        /// </summary>
        public ApplicationOAuthProvider()
        {
            //_kullanicilarService = WebApiInstaller.Resolve<IKullanicilarService>();
            _userService = WebApiInstaller.Resolve<IUsersService>();
            _clientService = WebApiInstaller.Resolve<IClientsService>();
            _logger = WebApiInstaller.Resolve<ICoreLogger>();
        }

        #region v.1.0.0
        // OAuthAuthorizationServerProvider sınıfının client erişimine izin verebilmek için ilgili ValidateClientAuthentication metotunu override ediyoruz.
        //public override Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        //{
        //    context.Validated();
        //    return Task.FromResult<object>(null);
        //}

        // OAuthAuthorizationServerProvider sınıfının kaynak erişimine izin verebilmek için ilgili GrantResourceOwnerCredentials metotunu override ediyoruz.
        //public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        //{
        //    // CORS ayarlarını set ediyoruz.
        //    context.OwinContext.Response.Headers.Add("Access-Control-Allow-Origin", new[] { "*" });

        //    _kullanici = await _kullanicilarService.GetKullanicilarByKullaniciAdiAsync(context.UserName);
        //    if (_kullanici == null)
        //    {
        //        context.SetError("invalid_grant", "Sistemde kayıtlı böyle bir kullanıcı bulunmamaktadır.");
        //        return;
        //    }

        //    if (_kullanici != null)
        //    {
        //        byte[] salt = Convert.FromBase64String(_kullanici.Salt);

        //        byte[] passwordSaltedHash = Utility.Hash(context.Password, salt);

        //        if (Convert.ToBase64String(passwordSaltedHash).Equals(_kullanici.SifreHash, StringComparison.InvariantCulture))
        //        {
        //            var identity = new ClaimsIdentity(context.Options.AuthenticationType);
        //            identity.AddClaim(new Claim("userName", _kullanici.KullaniciAdi));
        //            identity.AddClaim(new Claim("email", _kullanici.Email));
        //            identity.AddClaim(new Claim("userID", _kullanici.Id.ToString()));

        //            context.Validated(identity);
        //            context.Request.Context.Authentication.SignIn(identity);
        //        }
        //        else
        //            context.SetError("invalid_grant", "Kullanıcı adı veya şifre yanlış.");
        //    }
        //}

        //public override Task TokenEndpointResponse(OAuthTokenEndpointResponseContext context)
        //{
        //    var accessToken = context.AccessToken;
        //    _kullanicilarService.InsertKullaniciOturumAsync(new KullaniciOturumModel
        //    {
        //        AuthToken = context.AccessToken,
        //        KullaniciId = _kullanici.Id,
        //        SonlanmaTarihi = DateTime.Now.AddMinutes(2)
        //    });

        //    return Task.FromResult<object>(null);
        //}
        #endregion

        #region v.2.0.0
        //public override async Task ValidateClientRedirectUri(OAuthValidateClientRedirectUriContext context)
        //{
        //    //if (context.ClientId == Clients.Client1.Id)
        //    //{
        //    //    context.Validated(Clients.Client1.RedirectUrl);
        //    //}
        //    //else if (context.ClientId == Clients.Client2.Id)
        //    //{
        //    //    context.Validated(Clients.Client2.RedirectUrl);
        //    //}
        //    //return Task.FromResult(0);
        //}

        /// <summary>
        /// ValidateClientAuthentication
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override async Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            //context.Validated();
            string clientId;
            string clientSecret;

            _logger.Debug(GetType(), null, "Checking clientId & clientSecret");
            if (!context.TryGetBasicCredentials(out clientId, out clientSecret))
            {
                context.TryGetFormCredentials(out clientId, out clientSecret);
            }

            _logger.Debug(GetType(), null, "Is ClientId null");
            if (context.ClientId == null)
            {
                context.SetError("invalid_clientId", "ClientId parametre olarak gönderilmelidir.");
                return;
            }

            _logger.Debug(GetType(), null, "GetByClientIdAsync({0})", context.ClientId);
            _client = await _clientService.GetByClientIdAsync(context.ClientId);

            if (_client == null)
            {
                context.SetError("invalid_clientId", string.Format("Sistemde kayıtlı böyle bir client bulunmamaktadır.", context.ClientId));
                return;
            }

            //Client Secret ile işlem yapılmak istenirse açılmalıdır.
            //if (_client.ApplicationType == ApplicationTypes.WebDevelopment)
            //{
            //    if (string.IsNullOrWhiteSpace(clientSecret))
            //    {
            //        context.SetError("invalid_clientId", "Client secret should be sent.");
            //        return Task.FromResult<object>(null);
            //    }
            //    else
            //    {
            //        if (client.Secret != Helper.GetHash(clientSecret))
            //        {
            //            context.SetError("invalid_clientId", "Client secret is invalid.");
            //            return Task.FromResult<object>(null);
            //        }
            //    }
            //}

            if (!_client.Active)
            {
                context.SetError("invalid_clientId", "Client aktif değildir.");
                return;
            }

            context.OwinContext.Set<string>("as:clientAllowedOrigin", _client.AllowedOrigin);
            context.OwinContext.Set<string>("as:clientRefreshTokenLifeTime", _client.RefreshTokenLifeTime.ToString());

            context.Validated();
        }

        /// <summary>
        /// GrantResourceOwnerCredentials
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            var allowedOrigin = context.OwinContext.Get<string>("as:clientAllowedOrigin");

            if (allowedOrigin == null)
                allowedOrigin = "*";

            context.OwinContext.Response.Headers.Add("Access-Control-Allow-Origin", new[] { allowedOrigin });

            if (hasAuthorizationHeader(context))
            {
                context.SetError("invalid_grant", "Token alınırken header içerisinde Authorization gönderilmemelidir.");
                return;
            }

            _user = await _userService.GetByUserNameAsync(context.UserName);
            if (_user == null)
            {
                context.SetError("invalid_grant", "Sistemde kayıtlı böyle bir kullanıcı bulunmamaktadır.");
                return;
            }

            if (_user != null)
            {
                byte[] salt = Convert.FromBase64String(_user.Salt);

                byte[] passwordSaltedHash = Utility.Hash(context.Password, salt);

                if (Convert.ToBase64String(passwordSaltedHash).Equals(_user.PasswordHash, StringComparison.InvariantCulture))
                {
                    var oAuthIdentity = new ClaimsIdentity(context.Options.AuthenticationType);
                    //identity.AddClaim(new Claim("userName", _user.UserName));
                    oAuthIdentity.AddClaim(new Claim(ClaimTypes.Name, _user.UserName));
                    oAuthIdentity.AddClaim(new Claim(ClaimTypes.Email, String.IsNullOrEmpty(_user.Email) ? string.Empty : _user.Email));
                    oAuthIdentity.AddClaim(new Claim(ClaimTypes.PrimarySid, _user.Id.ToString()));

                    var cookieIdentity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationType);
                    cookieIdentity.AddClaim(new Claim(ClaimTypes.Name, _user.UserName));
                    cookieIdentity.AddClaim(new Claim(ClaimTypes.Email, String.IsNullOrEmpty(_user.Email) ? string.Empty : _user.Email));
                    cookieIdentity.AddClaim(new Claim(ClaimTypes.PrimarySid, _user.Id.ToString()));

                    var props = new AuthenticationProperties(new Dictionary<string, string>
                    {
                        { "client_id", (context.ClientId == null) ? string.Empty : context.ClientId },
                        { "userName", context.UserName }
                    });
                    
                    var ticket = new AuthenticationTicket(oAuthIdentity, props);

                    //Add a response cookie...
                    //context.Response.Cookies.Append("Token", context.Options.AccessTokenFormat.Protect(ticket));

                    context.Validated(ticket);
                    context.Request.Context.Authentication.SignIn(cookieIdentity);
                }
                else
                    context.SetError("invalid_grant", "Kullanıcı adı veya şifre yanlış.");
            }
        }

        /// <summary>
        /// GrantRefreshToken
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override Task GrantRefreshToken(OAuthGrantRefreshTokenContext context)
        {
            var originalClient = context.Ticket.Properties.Dictionary["client_id"];
            var currentClient = context.ClientId;

            if (originalClient != currentClient)
            {
                context.SetError("invalid_clientId", "Refresh token is issued to a different clientId.");
                return Task.FromResult<object>(null);
            }

            // Change auth ticket for refresh token requests
            var newIdentity = new ClaimsIdentity(context.Ticket.Identity);

            var newClaim = newIdentity.Claims.Where(c => c.Type == "newClaim").FirstOrDefault();

            if (newClaim != null)
                newIdentity.RemoveClaim(newClaim);

            newIdentity.AddClaim(new Claim("newClaim", "newValue"));

            var newTicket = new AuthenticationTicket(newIdentity, context.Ticket.Properties);
            context.Validated(newTicket);

            return Task.FromResult<object>(null);
        }

        /// <summary>
        /// TokenEndpoint
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override Task TokenEndpoint(OAuthTokenEndpointContext context)
        {
            foreach (KeyValuePair<string, string> property in context.Properties.Dictionary)
            {
                context.AdditionalResponseParameters.Add(property.Key, property.Value);
            }

            return Task.FromResult<object>(null);
        }
        #endregion

        private bool hasAuthorizationHeader(OAuthGrantResourceOwnerCredentialsContext actionContext)
        {
            try
            {
                string token = string.Empty;
                token = (actionContext.Request.Headers.Any(x => x.Key == "Authorization")) ? actionContext.Request.Headers.Where(x => x.Key == "Authorization").FirstOrDefault().Value.SingleOrDefault().Replace("Bearer ", "") : "";

                if (string.IsNullOrEmpty(token))
                    return false;

                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}