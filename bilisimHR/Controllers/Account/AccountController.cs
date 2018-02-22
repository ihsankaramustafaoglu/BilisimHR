using bilisimHR.Business.Model.Auth;
using bilisimHR.Common.Helper;
using bilisimHR.Properties;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.DataHandler.Encoder;
using Microsoft.Owin.Security.DataHandler.Serializer;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace bilisimHR.Controllers.Account
{
    public class AccountController : BaseController
    {
        // GET: Login
        public ActionResult Login()
        {
            return View();
        }

        //
        // POST: /Account/Login
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginViewModel model, string returnUrl)
        {
            try
            {
                if (!ModelState.IsValid)
                    return View(model);

                //RestfulHelper.Instance.BaseUri = Settings.Default.AuthAPI;
                //Token token = await RestfulHelper.Instance.AuthenticateAsync<Token>(model.UserName, model.Password,
                //    Settings.Default.ClientID, Settings.Default.TokenPath);
                
                //_tokenContainer.ApiToken = token;

                return RedirectToAction("Index", "Home");
            }
            catch (Exception ex)
            {
                Dictionary<string, string> dictResult = JsonConvert.DeserializeObject<Dictionary<string, string>>(ex.Message);
                ModelState.Clear();
                ModelState.AddModelError("UserName", dictResult["error_description"].ToString());
                return View(model);
                //throw ex;
            }
            //return View(returnUrl ?? "/");
        }

        private IAuthenticationManager _authentication
        {
            get { return HttpContext.GetOwinContext().Authentication; }
        }
    }
}