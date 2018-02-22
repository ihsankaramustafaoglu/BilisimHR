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
    /// EmpEmployeeController
    /// </summary>
    [RoutePrefix("api/EmpEmployee")]
    //[SessionAuthorize]
    [WebApiActionLogger]
    // [GlobalException]
    public class EmpEmployeeController : ApiController
    {
        #region -Initiating-
        private ICoreLogger _logger;
        private IEmpEmployeeService _empEmployeeService;

        /// <summary>
        /// EmpEmployeeController Constructor
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="empEmployeeService"></param>
        public EmpEmployeeController(ICoreLogger logger, IEmpEmployeeService empEmployeeService)
        {
            _logger = logger;
            _empEmployeeService = empEmployeeService;
        }
        #endregion

        // GET: api/EmpEmployee
        /// <summary>
        /// Get All EmpEmployee
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("")]
        [Description("Get All EmpEmployee")]
        public IHttpActionResult GetAll()
        {
            try
            {
                return Ok(_empEmployeeService.GetAllAsync().Result);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        /// <summary>
        /// Get EmpEmployee By ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("{id:int}")]
        [Description("Get EmpEmployee By ID")]
        public IHttpActionResult Get(int id)
        {
            try
            {
                return Ok(_empEmployeeService.GetAsync(id).Result);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        // POST: api/EmpEmployee
        /// <summary>
        /// Create New EmpEmployee
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        [Route("Post")]
        [Description("Create New EmpEmployee")]
        public IHttpActionResult Post(EmpEmployeeModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                _empEmployeeService.InsertAsync(model);

                return Ok();
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }

        }

        // PUT: api/EmpEmployee/5
        /// <summary>
        /// Update EmpEmployee
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("Put")]
        [Description("Update EmpEmployee")]
        public IHttpActionResult Put(EmpEmployeeModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                _empEmployeeService.UpdateAsync(model);
                return Ok();
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        // DELETE: api/EmpEmployee/5
        /// <summary>
        /// Delete EmpEmployee By ID
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
                _empEmployeeService.DeleteAsync(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
    }
}
