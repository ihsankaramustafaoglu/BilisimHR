using bilisimHR.Business.Model.Auth;
using bilisimHR.Common.Core;
using bilisimHR.Common.Helper;
using bilisimHR.Services.Auth.Interfaces;
using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace bilisimHR.API.Authentication.Controllers
{
    /// <summary>
    /// Kullanıcı İşlemleri
    /// </summary>
    [RoutePrefix("api/Users")]
    //[SessionAuthorize]
    public class UsersController : ApiController
    {
        #region -Initiating-
        private ICoreLogger _logger;
        private IUsersService _usersService;

        /// <summary>
        /// UsersController Constructor
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="usersService"></param>
        public UsersController(ICoreLogger logger, IUsersService usersService)
        {
            _logger = logger;
            _usersService = usersService;
        }
        #endregion

        // GET: api/Users
        /// <summary>
        /// Get All Users
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("")]
        [Description("Get All Users")]
        public IHttpActionResult GetAll()
        {
            try
            {
                return Ok(_usersService.GetAllAsync().Result);
            }
            catch (Exception ex)
            {
                throw ex;
                // return InternalServerError(ex);
            }

        }

        /// <summary>
        /// Get User By ID
        /// </summary>
        /// <param name="id">Kullanıcı Id</param>
        /// <returns></returns>
        [HttpGet]
        [Route("{id:int}")]
        [Description("Get User By ID")]
        public IHttpActionResult GetById(int id)
        {
            try
            {
                return Ok(_usersService.GetAsync(id).Result);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }

        }

        /// <summary>
        /// Get User By Email
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("Email/{email}")]
        [Description("Get User By Email")]
        public IHttpActionResult GetByEmail(string email)
        {
            try
            {
                return Ok(_usersService.GetByEmailAsync(email).Result);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        /// <summary>
        /// Get User By UserName
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("UserName/{username}")]
        [Description("Get User By UserName")]
        public IHttpActionResult GetByUserName(string userName)
        {
            try
            {
                return Ok(_usersService.GetByUserNameAsync(userName).Result);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        /// <summary>
        /// Create User
        /// </summary>
        /// <remarks>Should use for creating new user.</remarks>
        /// <response code="200">Success</response>
        /// <response code="400">Bad Request</response>
        /// <response code="500">Internal Server Error</response>
        /// <param name="client"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("")]
        [AllowAnonymous]
        [Description("Create User")]
        public IHttpActionResult Patch(NewUserModel user)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                string saltedPassword = String.Empty;
                string salt = String.Empty;

                Utility.SaltPassword(user.Password, out salt, out saltedPassword);

                var id = _usersService.InsertAsync(new UsersModel
                {
                    UserName = user.UserName,
                    PasswordHash = saltedPassword,
                    Salt = salt,
                    EmailConfirmed = false,
                    PhoneNumberConfirmed = false,
                    LockoutEnabled = false,
                    AccessFailedCount = 0,
                }).Result;
                
                return Ok(id);
            }
            catch (Exception ex)
            {
                //Log error
                return InternalServerError(ex);
            }
        }

        /// <summary>
        /// Update User
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [HttpPatch]
        [Route("")]
        [Description("Update User")]
        public IHttpActionResult Put(UsersModel user)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                _usersService.UpdateAsync(user);
                return Ok();
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        // DELETE: api/Users/Delete/5
        /// <summary>
        /// Delete User By ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("{id:int}")]
        [Description("Delete User By ID")]
        public IHttpActionResult Delete(int id)
        {
            try
            {
                _usersService.DeleteAsync(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        /// <summary>
        /// Insert Role(s) To User
        /// </summary>
        /// <param name="id">User Id</param>
        /// <param name="roles">Role Id Array</param>
        /// <returns></returns>
        [HttpPatch]
        [Route("InsertRoles/{id}/Roles")]
        [Description("Insert Role(s) To User")]
        public IHttpActionResult InsertRoles(int id, int[] roles)
        {
            try
            {
                _usersService.InsertRolesAsync(id, roles.ToList());
                return Ok();
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        /// <summary>
        /// Delete Role(s) From User
        /// </summary>
        /// <param name="id">User Id</param>
        /// <param name="roles">Role Id Array</param>
        /// <returns></returns>
        [HttpPatch]
        [Route("DeleteRoles/{id}/Roles")]
        [Description("Delete Role(s) From User")]
        public IHttpActionResult DeleteRoles(int id, int[] roles)
        {
            try
            {
                _usersService.DeleteRolesAsync(id, roles.ToList());
                return Ok();
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        private IAuthenticationManager _authentication
        {
            get { return Request.GetOwinContext().Authentication; }
        }
    }
}
