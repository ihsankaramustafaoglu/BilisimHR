using bilisimHR.Business.Model.Auth;
using bilisimHR.Common.Helper;
using bilisimHR.Infrastructure.Dependency.CastleWindsor;
using bilisimHR.Services.Auth.Interfaces;
using Microsoft.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;

namespace bilisimHR.API.Common.Filters
{
    /// <summary>
    /// SessionAuthorizeAttribute
    /// </summary>
    public class SessionAuthorizeAttribute : AuthorizeAttribute
    {
        /// <summary>
        /// OnAuthorization
        /// </summary>
        /// <param name="actionContext"></param>
        public override void OnAuthorization(HttpActionContext actionContext)
        {
            try
            {
                if (skipAuthorization(actionContext))
                    return;

                if (!hasAuthorizationHeader(actionContext))
                {
                    actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.BadRequest, "Missing 'Authorization' header. Access denied.");
                    return;
                }

                if (!verifyToken(actionContext))
                {
                    actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Unauthorized, "Your token is not valid.");
                    return;
                }

                if (!hasPermission(actionContext))
                {
                    actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Forbidden, "You don't have permission for this operation. Access denied.");
                    return;
                }

                base.OnAuthorization(actionContext);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private static bool skipAuthorization(HttpActionContext actionContext)
        {
            return actionContext.ActionDescriptor.GetCustomAttributes<AllowAnonymousAttribute>().Any()
                   || actionContext.ControllerContext.ControllerDescriptor.GetCustomAttributes<AllowAnonymousAttribute>().Any();
        }

        private HttpRequestMessage _currentRequest
        {
            get
            {
                return (HttpRequestMessage)HttpContext.Current.Items["MS_HttpRequestMessage"];
            }
        }

        private bool hasAuthorizationHeader(HttpActionContext actionContext)
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

        private bool verifyToken(HttpActionContext actionContext)
        {
            try
            {
                if (actionContext.Request.Properties.ContainsKey("MS_OwinContext"))
                {
                    var context = (OwinContext)actionContext.Request.Properties["MS_OwinContext"];
                    var claimsIdentity = context.Authentication.User.Identity as ClaimsIdentity;
                    string userId = string.Empty;
                    string userName = string.Empty;
                    
                    if (claimsIdentity.Claims.ToList().Count > 0)
                    {
                        Claim claimsUserId = claimsIdentity.Claims.Where(s => s.Type == ClaimTypes.PrimarySid).FirstOrDefault();
                        Claim claimsUserName = claimsIdentity.Claims.Where(s => s.Type == ClaimTypes.Name).FirstOrDefault();
                        userId = claimsUserId != null ? claimsUserId.Value : string.Empty;
                        userName = claimsUserName != null ? claimsUserName.Value : string.Empty;
                    }

                    if (string.IsNullOrEmpty(userId) || string.IsNullOrEmpty(userName))
                        return false;

                    return true;
                }

                return false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private bool hasPermission(HttpActionContext actionContext)
        {
            try
            {
                string controllerName = actionContext.ControllerContext.ControllerDescriptor.ControllerType.Name;
                string actionName = actionContext.ActionDescriptor.ActionName;
                string userId = string.Empty;
                string userName = string.Empty;

                if (actionContext.Request.Properties.ContainsKey("MS_OwinContext"))
                {
                    var context = (OwinContext)actionContext.Request.Properties["MS_OwinContext"];
                    var claimsIdentity = context.Authentication.User.Identity as ClaimsIdentity;

                    if (claimsIdentity.Claims.ToList().Count > 0)
                    {
                        Claim claimsUserId = claimsIdentity.Claims.Where(s => s.Type == ClaimTypes.PrimarySid).FirstOrDefault();
                        Claim claimsUserName = claimsIdentity.Claims.Where(s => s.Type == ClaimTypes.Name).FirstOrDefault();
                        userId = claimsUserId != null ? claimsUserId.Value : string.Empty;
                        userName = claimsUserName != null ? claimsUserName.Value : string.Empty;
                    }

                    var usersService = WebApiInstaller.Resolve<IUsersService>();
                    var roleInPagesService = WebApiInstaller.Resolve<IRoleInPagesService>();
                    var controllerInActionService = WebApiInstaller.Resolve<IControllerActionsService>();

                    if (string.IsNullOrEmpty(userId))
                        return false;
                    
                    UsersModelLite user = usersService.GetAsync(int.Parse(userId)).Result;

                    if (user == null)
                        return false;

                    if (user.Roles.Count < 1)
                        return false;

                    var controllerInActions = controllerInActionService.GetControllerActionsByControllerAndActionAsync(controllerName, actionName).Result;

                    if (controllerInActions == null)
                        return false;

                    List<RoleInPagesModel> roleInPageList = new List<RoleInPagesModel>();
                    foreach (int roleId in user.Roles)
                    {
                        var result = roleInPagesService.GetByRoleIdAsync(roleId).Result;

                        if (result != null)
                            roleInPageList.AddRange(result);
                    }

                    foreach (RoleInPagesModel model in roleInPageList.Distinct())
                    {
                        switch (controllerInActions.OperationType)
                        {
                            case OperationType.Create:
                                if (model.Create)
                                    return true;
                                break;
                            case OperationType.Read:
                                if (model.Read)
                                    return true;
                                break;
                            case OperationType.Update:
                                if (model.Update)
                                    return true;
                                break;
                            case OperationType.Delete:
                                if (model.Delete)
                                    return true;
                                break;
                            case OperationType.Operation:
                                if (model.Create && model.Read && model.Update && model.Delete)
                                    return true;
                                break;
                            default:
                                break;
                        }
                    }

                    // var user = usersService.GetAsync(int.Parse(userId)).Result;

                    //var roleList = user.Roles.Where(
                    //    u => u.Pages.Where(
                    //            p => p.ControllerActions.Where(
                    //                c => c.Controller == controllerName && c.Action == actionName).Count() > 0).Count() > 0).ToList();

                    //if (roleList.Count > 0)
                    //    return true;
                    //else
                    //    return false;
                }

                return false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
