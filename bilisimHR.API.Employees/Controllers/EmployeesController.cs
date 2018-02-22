using bilisimHR.Business.Model.Employees;
using bilisimHR.Common.Core;
using bilisimHR.Services.Employees.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace bilisimHR.API.Employees.Controllers
{
    [RoutePrefix("api/Employees")]
    public class EmployeesController : ApiController
    {
        #region -Initiating-
        private ICoreLogger _logger;
        private IEmpEmployeePkService _empEmployeePkService;

        /// <summary>
        /// ClientsController Constructor
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="clientService"></param>
        public EmployeesController(ICoreLogger logger, IEmpEmployeePkService empEmployeePkService)
        {
            _logger = logger;
            _empEmployeePkService = empEmployeePkService;
        }
        #endregion

        // GET: api/Clients
        /// <summary>
        /// Get All Clients
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("")]
        [Description("Get All Employees")]
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
            return Ok();
        }

        /// <summary>
        /// Get Client By ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("{id:int}")]
        [Description("Get Employee By ID")]
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
            return Ok();
        }
        
        // POST: api/Clients
        /// <summary>
        /// Create New Client
        /// </summary>
        /// <param name="client"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        [Route("Post")]
        [Description("Create New Employee")]
        public IHttpActionResult Post(EmpEmployeePkModel employee)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);
                
                _empEmployeePkService.InsertAsync(employee);

                return Ok();
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }

        }

        // PUT: api/Clients/5
        /// <summary>
        /// Update Client
        /// </summary>
        /// <param name="client"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("Put")]
        [Description("Update Employee")]
        public IHttpActionResult Put(EmpEmployeePkModel employee)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                _empEmployeePkService.UpdateAsync(employee);
                return Ok();
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        // DELETE: api/Clients/5
        /// <summary>
        /// Delete Client By ID
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
