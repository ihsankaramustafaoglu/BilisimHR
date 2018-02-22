using bilisimHR.API.Common.Filters;
using bilisimHR.Business.Model.Employees;
using bilisimHR.Common.Core;
using bilisimHR.Services.Employees.Interfaces;
using System;
using System.ComponentModel;
using System.Web.Http;

namespace bilisimHR.API.Employees.Controllers
{
    /// <summary>
    /// EmpIdentityController
    /// </summary>
    [RoutePrefix("api/EmpIdentity")]
    //[SessionAuthorize]
    [WebApiActionLogger]
    // [GlobalException]
    public class EmpIdentityController : ApiController
    {
        #region -Initiating-
        private ICoreLogger _logger;
        private IEmpIdentityService _empIdentityService;

        /// <summary>
        /// EmpIdentityController Constructor
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="empIdentityService"></param>
        public EmpIdentityController(ICoreLogger logger, IEmpIdentityService empIdentityService)
        {
            _logger = logger;
            _empIdentityService = empIdentityService;
        }
        #endregion

        // GET: api/EmpIdentity
        /// <summary>
        /// Get All EmpIdentity
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("")]
        [Description("Get All EmpIdentity")]
        public IHttpActionResult GetAll()
        {
            try
            {
                return Ok(_empIdentityService.GetAllAsync().Result);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        /// <summary>
        /// Get EmpIdentity By ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("{id:int}")]
        [Description("Get EmpIdentity By ID")]
        public IHttpActionResult Get(int id)
        {
            try
            {
                return Ok(_empIdentityService.GetAsync(id).Result);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        // POST: api/EmpIdentity
        /// <summary>
        /// Create New EmpIdentity
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        [Route("Post")]
        [Description("Create New EmpIdentity")]
        public IHttpActionResult Post(EmpIdentityModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                _empIdentityService.InsertAsync(model);

                return Ok();
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }

        }

        // PUT: api/EmpIdentity/5
        /// <summary>
        /// Update EmpIdentity
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("Put")]
        [Description("Update EmpIdentity")]
        public IHttpActionResult Put(EmpIdentityModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                _empIdentityService.UpdateAsync(model);
                return Ok();
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        // DELETE: api/EmpIdentity/5
        /// <summary>
        /// Delete EmpIdentity By ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("Delete/{id}")]
        [Description("Delete Employee By ID")]
        public IHttpActionResult Delete(int id)
        {
            try
            {
                _empIdentityService.DeleteAsync(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
    }
}
