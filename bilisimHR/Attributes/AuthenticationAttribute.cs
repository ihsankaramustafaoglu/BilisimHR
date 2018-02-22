using System.Web.Mvc;

namespace bilisimHR.Attributes
{
    public class AuthenticationAttribute : ActionFilterAttribute
    {
        //private readonly ITokenContainer _tokenContainer;

        //public AuthenticationAttribute()
        //{
        //    _tokenContainer = new TokenContainer();
        //}

        //public override void OnActionExecuting(ActionExecutingContext filterContext)
        //{
        //    if (_tokenContainer.ApiToken == null)
        //        filterContext.HttpContext.Response.RedirectToRoute(RouteConfig.LoginRouteName);
        //    else
        //    {
        //        Token token = _tokenContainer.ApiToken as Token;
        //        if (DateTime.UtcNow > token.Expires)
        //        {
        //            if (!string.IsNullOrEmpty(token.RefreshToken))
        //            {
        //                RestfulHelper.Instance.BaseUri = Settings.Default.AuthAPI;
        //                Task<Token> sCode = Task.Run(async () =>
        //                {
        //                    Token newToken = await RestfulHelper.Instance.RefreshTokenAsync<Token>(token.RefreshToken, token.ClientId, Settings.Default.TokenPath);
        //                    return newToken;
        //                });

        //                _tokenContainer.ApiToken = sCode.Result;
        //            }
        //            else
        //            {
        //                _tokenContainer.ApiToken = null;
        //                filterContext.HttpContext.Response.RedirectToRoute(RouteConfig.LoginRouteName);
        //            }
        //        }
        //    }
        //}
    }
}