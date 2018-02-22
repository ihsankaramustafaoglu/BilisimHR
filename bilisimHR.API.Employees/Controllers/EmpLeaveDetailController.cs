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
    /// EmpLeaveDetailController
    /// </summary>
    [RoutePrefix("api/EmpLeaveDetail")]
    //[SessionAuthorize]
    [WebApiActionLogger]
    // [GlobalException]
    public class EmpLeaveDetailController : ApiController
    {
        #region -Initiating-
        private ICoreLogger _logger;
        private IEmpLeaveDetailService _empLeaveDetailService;

        /// <summary>
        /// EmpLeaveDetailController Constructor
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="empLeaveDetailService"></param>
        public EmpLeaveDetailController(ICoreLogger logger, IEmpLeaveDetailService empLeaveDetailService)
        {
            _logger = logger;
            _empLeaveDetailService = empLeaveDetailService;
        }
        #endregion

        // GET: api/EmpLeaveDetail
        /// <summary>
        /// Get All EmpLeaveDetail
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("")]
        [Description("Get All EmpLeaveDetail")]
        public IHttpActionResult GetAll()
        {
            try
            {
                return Ok(_empLeaveDetailService.GetAllAsync().Result);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        /// <summary>
        /// Get EmpLeaveDetail By ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("{id:int}")]
        [Description("Get EmpLeaveDetail By ID")]
        public IHttpActionResult Get(int id)
        {
            try
            {
                return Ok(_empLeaveDetailService.GetAsync(id).Result);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        // POST: api/EmpLeaveDetail
        /// <summary>
        /// Create New EmpLeaveDetail
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        [Route("Post")]
        [Description("Create New EmpLeaveDetail")]
        public IHttpActionResult Post(EmpLeaveDetailModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                _empLeaveDetailService.InsertAsync(model);

                return Ok();
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }

        }

        // PUT: api/EmpLeaveDetail/5
        /// <summary>
        /// Update EmpLeaveDetail
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("Put")]
        [Description("Update EmpLeaveDetail")]
        public IHttpActionResult Put(EmpLeaveDetailModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                _empLeaveDetailService.UpdateAsync(model);
                return Ok();
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        // DELETE: api/EmpLeaveDetail/5
        /// <summary>
        /// Delete EmpLeaveDetail By ID
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
                _empLeaveDetailService.DeleteAsync(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
    }
}
