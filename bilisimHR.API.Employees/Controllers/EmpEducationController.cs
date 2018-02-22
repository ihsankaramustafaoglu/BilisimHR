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
    /// EmpEducationController
    /// </summary>
    [RoutePrefix("api/EmpEducation")]
    //[SessionAuthorize]
    [WebApiActionLogger]
    // [GlobalException]
    public class EmpEducationController : ApiController
    {
        #region -Initiating-
        private ICoreLogger _logger;
        private IEmpEducationService _empEducationService;

        /// <summary>
        /// EmpEducationController Constructor
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="empEducationService"></param>
        public EmpEducationController(ICoreLogger logger, IEmpEducationService empEducationService)
        {
            _logger = logger;
            _empEducationService = empEducationService;
        }
        #endregion

        // GET: api/EmpEducation
        /// <summary>
        /// Get All EmpEducation
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("")]
        [Description("Get All EmpEducation")]
        public IHttpActionResult GetAll()
        {
            try
            {
                return Ok(_empEducationService.GetAllAsync().Result);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        /// <summary>
        /// Get EmpEducation By ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("{id:int}")]
        [Description("Get EmpEducation By ID")]
        public IHttpActionResult Get(int id)
        {
            try
            {
                return Ok(_empEducationService.GetAsync(id).Result);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        // POST: api/EmpEducation
        /// <summary>
        /// Create New EmpEducation
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        [Route("Post")]
        [Description("Create New EmpEducation")]
        public IHttpActionResult Post(EmpEducationModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                _empEducationService.InsertAsync(model);

                return Ok();
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        // PUT: api/EmpEducation/5
        /// <summary>
        /// Update EmpEducation
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("Put")]
        [Description("Update EmpEducation")]
        public IHttpActionResult Put(EmpEducationModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                _empEducationService.UpdateAsync(model);
                return Ok();
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        // DELETE: api/EmpEducation/5
        /// <summary>
        /// Delete EmpEducation By ID
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
                _empEducationService.DeleteAsync(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
    }
}
