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
    /// EmpPhoneController
    /// </summary>
    [RoutePrefix("api/EmpPhone")]
    //[SessionAuthorize]
    [WebApiActionLogger]
    // [GlobalException]
    public class EmpPhoneController : ApiController
    {
        #region -Initiating-
        private ICoreLogger _logger;
        private IEmpPhoneService _empPhoneService;

        /// <summary>
        /// EmpPhoneController Constructor
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="empPhoneService"></param>
        public EmpPhoneController(ICoreLogger logger, IEmpPhoneService empPhoneService)
        {
            _logger = logger;
            _empPhoneService = empPhoneService;
        }
        #endregion

        // GET: api/EmpPhone
        /// <summary>
        /// Get All EmpPhone
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("")]
        [Description("Get All EmpPhone")]
        public IHttpActionResult GetAll()
        {
            try
            {
                return Ok(_empPhoneService.GetAllAsync().Result);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        /// <summary>
        /// Get EmpPhone By ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("{id:int}")]
        [Description("Get EmpPhone By ID")]
        public IHttpActionResult Get(int id)
        {
            try
            {
                return Ok(_empPhoneService.GetAsync(id).Result);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        // POST: api/EmpPhone
        /// <summary>
        /// Create New EmpPhone
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        [Route("Post")]
        [Description("Create New EmpPhone")]
        public IHttpActionResult Post(EmpPhoneModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                _empPhoneService.InsertAsync(model);

                return Ok();
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }

        }

        // PUT: api/EmpPhone/5
        /// <summary>
        /// Update EmpPhone
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("Put")]
        [Description("Update EmpPhone")]
        public IHttpActionResult Put(EmpPhoneModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                _empPhoneService.UpdateAsync(model);
                return Ok();
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        // DELETE: api/EmpPhone/5
        /// <summary>
        /// Delete EmpPhone By ID
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
                _empPhoneService.DeleteAsync(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
    }
}
