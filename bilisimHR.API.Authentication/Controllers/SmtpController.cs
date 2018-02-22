using bilisimHR.API.Common.Filters;
using bilisimHR.Business.Model.Auth;
//using bilisimHR.Business.Model.Email;
using bilisimHR.Common.Core;
using bilisimHR.Services.Auth.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace bilisimHR.API.Smtp.Controllers
{
    /// <summary>
    /// SmtpController
    /// </summary>
    [RoutePrefix("api/Smtp")]
    public class SmtpController : ApiController
    {
        #region -Initiating-
        private ICoreLogger _logger;
        private ISmtpService _smtpService;

        /// <summary>
        /// SmtpController Constructor
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="smtpService"></param>
        public SmtpController(ICoreLogger logger, ISmtpService smtpService)
        {
            _logger = logger;
            _smtpService = smtpService;
        }
        #endregion

        // GET: api/Smtp
        /// <summary>
        /// Get All Smtp
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("")]
        [Description("Get All Smtp")]
        public IHttpActionResult GetAll()
        {
            try
            {
                return Ok(_smtpService.GetAllAsync().Result);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        /// <summary>
        /// Get Smtp By ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("{id:int}")]
        [Description("Get Smtp By ID")]
        public IHttpActionResult Get(int id)
        {
            try
            {
                return Ok(_smtpService.GetAsync(id).Result);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        // POST: api/Smtp
        /// <summary>
        /// Create New Smtp
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        [Route("")]
        [Description("Create New Smtp")]
        public IHttpActionResult Post(SmtpModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                _smtpService.InsertAsync(model);

                return Ok();
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }

        }

        // PUT: api/Smtp/5
        /// <summary>
        /// Update Smtp
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("")]
        [Description("Update Smtp")]
        public IHttpActionResult Patch(SmtpModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                _smtpService.UpdateAsync(model);
                return Ok();
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        // DELETE: api/Smtp/5
        /// <summary>
        /// Delete Smtp By ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("{id:int}")]
        [Description("Delete Smtp By ID")]
        public IHttpActionResult Delete(int id)
        {
            try
            {
                _smtpService.DeleteAsync(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        // Send Email: api/Smtp/5
        /// <summary>
        ///send email
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        [Route("Post/sendmailtest")]
        [Description("Create New Smtp")]
        public IHttpActionResult SendEmailTest(SmtpModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                _smtpService.SendEmailTest(model);

                return Ok();
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }

        }

    }
}
