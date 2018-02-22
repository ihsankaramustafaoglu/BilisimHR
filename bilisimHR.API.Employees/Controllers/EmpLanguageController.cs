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
    /// EmpLanguageController
    /// </summary>
    [RoutePrefix("api/EmpLanguage")]
    //[SessionAuthorize]
    [WebApiActionLogger]
    // [GlobalException]
    public class EmpLanguageController : ApiController
    {
        #region -Initiating-
        private ICoreLogger _logger;
        private IEmpLanguageService _empLanguageService;

        /// <summary>
        /// EmpLanguageController Constructor
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="empLanguageService"></param>
        public EmpLanguageController(ICoreLogger logger, IEmpLanguageService empLanguageService)
        {
            _logger = logger;
            _empLanguageService = empLanguageService;
        }
        #endregion

        // GET: api/EmpLanguage
        /// <summary>
        /// Get All EmpLanguage
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("")]
        [Description("Get All EmpLanguage")]
        public IHttpActionResult GetAll()
        {
            try
            {
                return Ok(_empLanguageService.GetAllAsync().Result);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        /// <summary>
        /// Get EmpLanguage By ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("{id:int}")]
        [Description("Get EmpLanguage By ID")]
        public IHttpActionResult Get(int id)
        {
            try
            {
                return Ok(_empLanguageService.GetAsync(id).Result);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        // POST: api/EmpLanguage
        /// <summary>
        /// Create New EmpLanguage
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        [Route("Post")]
        [Description("Create New EmpLanguage")]
        public IHttpActionResult Post(EmpLanguageModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                _empLanguageService.InsertAsync(model);

                return Ok();
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }

        }

        // PUT: api/EmpLanguage/5
        /// <summary>
        /// Update EmpLanguage
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("Put")]
        [Description("Update EmpLanguage")]
        public IHttpActionResult Put(EmpLanguageModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                _empLanguageService.UpdateAsync(model);
                return Ok();
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        // DELETE: api/EmpLanguage/5
        /// <summary>
        /// Delete EmpLanguage By ID
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
                _empLanguageService.DeleteAsync(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
    }
}
