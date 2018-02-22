using bilisimHR.Common.Core;
using bilisimHR.Services.Auth.Interfaces;
using bilisimHR.Business.Model.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.ComponentModel;

namespace bilisimHR.API.Authentication.Controllers
{
    [RoutePrefix("api/Roles")]
    public class RolesController : ApiController
    {
        private ICoreLogger _logger;
        private IRolesService _roleService;

        public RolesController(ICoreLogger logger, IRolesService roleService)
        {
            _logger = logger;
            _roleService = roleService;
        }

        // GET: api/Role
        /// <summary>
        /// Get All Roles
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("")]
        [Description("Get All Roles")]
        public IHttpActionResult GetAll()
        {
            try
            {
                IList<RolesModel> roles = _roleService.GetAllAsync().Result;
                return Ok(roles);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        /// <summary>
        /// Get All Roles Lite
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("GetAllLite")]
        [Description("Get All Roles")]
        public IHttpActionResult GetAllLite()
        {
            try
            {
                IList<RolesModelLite> roles = _roleService.GetAllLiteAsync().Result;
                return Ok(roles);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        // GET: api/Role/5
        /// <summary>
        /// Get Role By ID
        /// </summary>
        /// <param name="id">Role Id</param>
        /// <returns></returns>
        [HttpGet]
        [Route("{id:int}")]
        [Description("Get Role By Id")]
        public IHttpActionResult Get(int id)
        {
            try
            {
                RolesModel role = _roleService.GetAsync(id).Result;
                return Ok(role);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        /// <summary>
        /// Create New Role
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("")]
        [Description("Create New Role")]
        public IHttpActionResult Post(RolesModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                //for (int i = 0; i < 100000000; ++i)
                //    _roleService.InsertAsync(new RolesModel() { Name = "DummyRole: " + i });

                _roleService.InsertAsync(model);

                return Ok();
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        // PUT: api/Role/5
        /// <summary>
        /// Update Role
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPatch]
        [Route("")]
        [Description("Update Role")]
        public IHttpActionResult Patch(RolesModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                _roleService.UpdateAsync(model);

                return Ok();
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        /// <summary>
        /// Delete Role By ID
        /// </summary>
        /// <param name="id">Role Id</param>
        /// <returns></returns>
        [HttpDelete]
        [Route("{id:int}")]
        [Description("Delete Role By ID")]
        public IHttpActionResult Delete(int id)
        {
            try
            {
                _roleService.DeleteAsync(id);

                return Ok();
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        /// <summary>
        /// Insert User(s) To Role
        /// </summary>
        /// <param name="id">Role Id</param>
        /// <param name="users">User Id Array</param>
        /// <returns></returns>
        [HttpPatch]
        [Route("InsertUsers/{id}/Users")]
        [Description("Insert User(s) To Role")]
        public IHttpActionResult InsertUsers(int id, int[] users)
        {
            try
            {
                _roleService.InsertUsersAsync(id, users.ToList());
                return Ok();
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        /// <summary>
        /// Delete User(s) From Role
        /// </summary>
        /// <param name="id">Role Id</param>
        /// <param name="users">User Id Array</param>
        /// <returns></returns>
        [HttpPatch]
        [Route("DeleteUsers/{id}/Users")]
        [Description("Delete User(s) From Role")]
        public IHttpActionResult DeleteRoles(int id, int[] users)
        {
            try
            {
                _roleService.DeleteUsersAsync(id, users.ToList());
                return Ok();
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        /// <summary>
        /// Insert Pages(s) To Role
        /// </summary>
        /// <param name="id">Role Id</param>
        /// <param name="pages">Page Id Array</param>
        /// <returns></returns>
        [HttpPatch]
        [Route("InsertPages/{id}/Pages")]
        [Description("Insert Pages(s) To Role")]
        public IHttpActionResult InsertPages(int id, NewPagesModel[] pages)
        {
            try
            {
                _roleService.InsertPagesAsync(id, pages.ToList());
                return Ok();
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        /// <summary>
        /// Delete Pages(s) From Role
        /// </summary>
        /// <param name="id">Role Id</param>
        /// <param name="pages">Page Id Array</param>
        /// <returns></returns>
        [HttpPatch]
        [Route("DeletePages/{id}/Pages")]
        [Description("Delete Pages(s) From Role")]
        public IHttpActionResult DeletePages(int id, int[] pages)
        {
            try
            {
                _roleService.DeletePagesAsync(id, pages.ToList());
                return Ok();
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
    }
}
