using bilisimHR.Business.Model.Auth;
using bilisimHR.Common.Core;
using bilisimHR.Services.Auth.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace bilisimHR.API.Authentication.Controllers
{
    [RoutePrefix("api/Pages")]
    public class PagesController : ApiController
    {
        private ICoreLogger _logger;
        private IPagesService _pagesService;

        public PagesController(ICoreLogger logger, IPagesService pagesService)
        {
            _logger = logger;
            _pagesService = pagesService;
        }

        /// <summary>
        /// Get All Pages
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("")]
        [Description("Get All Pages")]
        public IHttpActionResult GetAll()
        {
            try
            {
                
                IList<PagesModel> pages = _pagesService.GetAllAsync().Result;
                return Ok(pages);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        /// <summary>
        /// Get All Pages Lite
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("GetAllLite")]
        [Description("Get All Pages")]
        public IHttpActionResult GetAllLite()
        {
            try
            {
                IList<PagesModelLite> pages = _pagesService.GetAllLiteAsync().Result;
                return Ok(pages);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        // GET: api/Page/5
        /// <summary>
        /// Get Page By ID
        /// </summary>
        /// <param name="id">Page Id</param>
        /// <returns></returns>
        [HttpGet]
        [Route("{id:int}")]
        [Description("Get Page By ID")]
        public IHttpActionResult Get(int id)
        {
            try
            {
                PagesModel page = _pagesService.GetAsync(id).Result;
                return Ok(page);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        // POST: api/Pages
        /// <summary>
        /// Create New Page
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("")]
        [Description("Create New Page")]
        public IHttpActionResult Post(PagesModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                _pagesService.InsertAsync(model);

                return Ok();
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        /// <summary>
        /// Update Page
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPatch]
        [Route("")]
        [Description("Update Page")]
        public IHttpActionResult Patch(PagesModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                _pagesService.UpdateAsync(model);

                return Ok();
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        /// <summary>
        /// Delete Page By ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("{id:int}")]
        [Description("Delete Page By ID")]
        public IHttpActionResult Delete(int id)
        {
            try
            {
                _pagesService.DeleteAsync(id);

                return Ok();
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        /// <summary>
        /// Insert ControllerAction(s) To Page
        /// </summary>
        /// <param name="id">Page Id</param>
        /// <param name="controllerActions">ControllerAction Id Array</param>
        /// <returns></returns>
        [HttpPatch]
        [Route("InsertControllerActions/{id}/ControllerActions")]
        [Description("Insert ControllerAction(s) To Page")]
        public IHttpActionResult InsertControllerAction(int id, int[] controllerActions)
        {
            try
            {
                _pagesService.InsertControllerActionsAsync(id, controllerActions.ToList());
                return Ok();
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        /// <summary>
        /// Delete ControllerAction(s) From Page
        /// </summary>
        /// <param name="id">User Id</param>
        /// <param name="controllerActions">ControllerAction Id Array</param>
        /// <returns></returns>
        [HttpPatch]
        [Route("DeleteControllerActions/{id}/ControllerActions")]
        [Description("Delete ControllerAction(s) From Page")]
        public IHttpActionResult DeleteControllerAction(int id, int[] controllerActions)
        {
            try
            {
                _pagesService.DeleteControllerActionsAsync(id, controllerActions.ToList());
                return Ok();
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
    }
}
