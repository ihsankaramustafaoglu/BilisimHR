using bilisimHR.Business.Model.Auth;
using bilisimHR.Common.Core;
using bilisimHR.Services.Auth.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace bilisimHR.API.Authentication.Controllers
{
    [RoutePrefix("api/RoleInPages")]
    public class RoleInPagesController : ApiController
    {
        private ICoreLogger _logger;
        private IRoleInPagesService _roleInPagesService;

        public RoleInPagesController(ICoreLogger logger, IRoleInPagesService roleInPagesService)
        {
            _logger = logger;
            _roleInPagesService = roleInPagesService;
        }

        /// <summary>
        /// Get All RoleInPages
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("")]
        [Description("Get All RoleInPages")]
        public IHttpActionResult GetAll()
        {
            try
            {
                IList<RoleInPagesModel> roles = _roleInPagesService.GetAllAsync().Result;
                return Ok(roles);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        /// <summary>
        /// Get RoleInPages By ID
        /// </summary>
        /// <param name="id">Id</param>
        /// <returns></returns>
        [HttpGet]
        [Route("{id:int}")]
        [Description("Get RoleInPages By ID")]
        public IHttpActionResult Get(int id)
        {
            try
            {
                RoleInPagesModel role = _roleInPagesService.GetAsync(id).Result;
                return Ok(role);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        /// <summary>
        /// Get RoleInPages By RoleID
        /// </summary>
        /// <param name="id">Role Id</param>
        /// <returns></returns>
        [HttpGet]
        [Route("GetByRoleID/{id:int}")]
        [Description("Get RoleInPages By RoleID")]
        public IHttpActionResult GetByRoleID(int id)
        {
            try
            {
                IList<RoleInPagesModel> roleList = _roleInPagesService.GetByRoleIdAsync(id).Result;
                return Ok(roleList);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        /// <summary>
        /// Create New RoleInPages
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("")]
        [Description("Create New RoleInPages")]
        public IHttpActionResult Post(RoleInPagesModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                _roleInPagesService.InsertAsync(model);

                return Ok();
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        /// <summary>
        /// Update RoleInPages
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPatch]
        [Route("")]
        [Description("Update RoleInPages")]
        public IHttpActionResult Patch(RoleInPagesModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                _roleInPagesService.UpdateAsync(model);

                return Ok();
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        // DELETE: api/Role/5
        /// <summary>
        /// Delete RoleInPages By ID
        /// </summary>
        /// <param name="id">RoleInPages Id</param>
        /// <returns></returns>
        [HttpDelete]
        [Route("{id:int}")]
        [Description("Delete RoleInPages By ID")]
        public IHttpActionResult Delete(int id)
        {
            try
            {
                _roleInPagesService.DeleteAsync(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
    }
}
