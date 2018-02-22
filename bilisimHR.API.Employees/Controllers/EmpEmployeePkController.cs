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
    /// EmpEmployeePkController
    /// </summary>
    [RoutePrefix("api/EmpEmployeePk")]
    //[SessionAuthorize]
    [WebApiActionLogger]
    // [GlobalException]
    public class EmpEmployeePkController : ApiController
    {
        #region -Initiating-
        private ICoreLogger _logger;
        private IEmpEmployeePkService _empEmployeePkService;

        /// <summary>
        /// EmpEmployeePkController Constructor
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="empEmployeePkService"></param>
        public EmpEmployeePkController(ICoreLogger logger, IEmpEmployeePkService empEmployeePkService)
        {
            _logger = logger;
            _empEmployeePkService = empEmployeePkService;
        }
        #endregion

        // GET: api/EmpEmployeePk
        /// <summary>
        /// Get All EmpEmployeePk
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("")]
        [Description("Get All EmpEmployeePk")]
        public IHttpActionResult GetAll()
        {
            try
            {
                return Ok(_empEmployeePkService.GetAllAsync().Result);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        /// <summary>
        /// Get EmpEmployeePk By ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("{id:int}")]
        [Description("Get EmpEmployeePk By ID")]
        public IHttpActionResult Get(int id)
        {
            try
            {
                return Ok(_empEmployeePkService.GetAsync(id).Result);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        // POST: api/EmpEmployeePk
        /// <summary>
        /// Create New EmpEmployeePk
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        [Route("Post")]
        [Description("Create New EmpEmployeePk")]
        public IHttpActionResult Post(EmpEmployeePkModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                _empEmployeePkService.InsertAsync(model);

                return Ok();
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }

        }

        // PUT: api/EmpEmployeePk/5
        /// <summary>
        /// Update EmpEmployeePk
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("Put")]
        [Description("Update EmpEmployeePk")]
        public IHttpActionResult Put(EmpEmployeePkModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                _empEmployeePkService.UpdateAsync(model);
                return Ok();
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        // DELETE: api/EmpEmployeePk/5
        /// <summary>
        /// Delete EmpEmployeePk By ID
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
                _empEmployeePkService.DeleteAsync(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
    }
}
