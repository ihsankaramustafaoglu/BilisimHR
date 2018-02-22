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
    /// EmpPositionDetailController
    /// </summary>
    [RoutePrefix("api/EmpPositionDetail")]
    //[SessionAuthorize]
    [WebApiActionLogger]
    // [GlobalException]
    public class EmpPositionDetailController : ApiController
    {
        #region -Initiating-
        private ICoreLogger _logger;
        private IEmpPositionDetailService _empPositionDetailService;

        /// <summary>
        /// EmpPositionDetailController Constructor
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="empPositionDetailService"></param>
        public EmpPositionDetailController(ICoreLogger logger, IEmpPositionDetailService empPositionDetailService)
        {
            _logger = logger;
            _empPositionDetailService = empPositionDetailService;
        }
        #endregion

        // GET: api/EmpPositionDetail
        /// <summary>
        /// Get All EmpPositionDetail
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("")]
        [Description("Get All EmpPositionDetail")]
        public IHttpActionResult GetAll()
        {
            try
            {
                return Ok(_empPositionDetailService.GetAllAsync().Result);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        /// <summary>
        /// Get EmpPositionDetail By ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("{id:int}")]
        [Description("Get EmpPositionDetail By ID")]
        public IHttpActionResult Get(int id)
        {
            try
            {
                return Ok(_empPositionDetailService.GetAsync(id).Result);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        // POST: api/EmpPositionDetail
        /// <summary>
        /// Create New EmpPositionDetail
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        [Route("Post")]
        [Description("Create New EmpPositionDetail")]
        public IHttpActionResult Post(EmpPositionDetailModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                _empPositionDetailService.InsertAsync(model);

                return Ok();
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }

        }

        // PUT: api/EmpPositionDetail/5
        /// <summary>
        /// Update EmpPositionDetail
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("Put")]
        [Description("Update EmpPositionDetail")]
        public IHttpActionResult Put(EmpPositionDetailModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                _empPositionDetailService.UpdateAsync(model);
                return Ok();
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        // DELETE: api/EmpPositionDetail/5
        /// <summary>
        /// Delete EmpPositionDetail By ID
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
                _empPositionDetailService.DeleteAsync(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
    }
}
