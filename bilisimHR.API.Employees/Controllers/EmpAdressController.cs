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
    /// EmpAdressController
    /// </summary>
    [RoutePrefix("api/EmpAdress")]
    //[SessionAuthorize]
    [WebApiActionLogger]
    //[GlobalException]
    public class EmpAdressController : ApiController
    {
        #region -Initiating-
        private ICoreLogger _logger;
        private IEmpAdressService _empAdressService;

        /// <summary>
        /// EmpAdressController Constructor
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="empAdressService"></param>
        public EmpAdressController(ICoreLogger logger, IEmpAdressService empAdressService)
        {
            _logger = logger;
            _empAdressService = empAdressService;
        }
        #endregion

        // GET: api/EmpAdress
        /// <summary>
        /// Get All EmpAdress
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("")]
        [Description("Get All EmpAdress")]
        public IHttpActionResult GetAll()
        {
            try
            {
                return Ok(_empAdressService.GetAllAsync().Result);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        /// <summary>
        /// Get EmpAdress By ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("{id:int}")]
        [Description("Get EmpAdress By ID")]
        public IHttpActionResult Get(int id)
        {
            try
            {
                return Ok(_empAdressService.GetAsync(id).Result);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        // POST: api/EmpAdress
        /// <summary>
        /// Create New EmpAdress
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        [Route("Post")]
        [Description("Create New EmpAdress")]
        public IHttpActionResult Post(EmpAdressModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                _empAdressService.InsertAsync(model);

                return Ok();
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }

        }

        // PUT: api/EmpAdress/5
        /// <summary>
        /// Update EmpAdress
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("Put")]
        [Description("Update EmpAdress")]
        public IHttpActionResult Put(EmpAdressModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                _empAdressService.UpdateAsync(model);
                return Ok();
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        // DELETE: api/EmpAdress/5
        /// <summary>
        /// Delete EmpAdress By ID
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
                _empAdressService.DeleteAsync(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
    }
}
