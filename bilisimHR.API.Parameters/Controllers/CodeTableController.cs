using bilisimHR.Business.Model.Parameters;
using bilisimHR.Common.Core;
using bilisimHR.Services.Parameters.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Web.Http;

namespace bilisimHR.API.Parameters.Controllers
{
    [RoutePrefix("api/CodeTable")]
    public class CodeTableController : ApiController
    {
        #region -Initiating-
        private ICoreLogger _logger;
        private ICodesTableService _codeTableService;

        /// <summary>
        /// CodeBaseController Constructor
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="codeBaseService"></param>
        public CodeTableController(ICoreLogger logger, ICodesTableService codeTableService)
        {
            _logger = logger;
            _codeTableService = codeTableService;
        }
        #endregion

        [HttpGet]
        [Route("")]
        [Description("Get All Code Tables")]
        public IEnumerable<CodeTableModel> GetAll()
        {
            IEnumerable<CodeTableModel> list = _codeTableService.GetAll();
            return list;
        }

        //// GET: api/CodeBase/5
        [HttpGet]
        [Route("{id:int}")]
        [Description("Get Code Table By Id")]
        public CodeTableModel Get(int id)
        {
            return _codeTableService.Get(id);
        }

        //// POST: api/CodeBase
        [HttpPost]
        [Route("")]
        [Description("Create New Code Table")]
        public IHttpActionResult Post(CodeTableModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);
                
                _codeTableService.Insert(model);

                return Ok();
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        //// PATCH: api/CodeBase/5
        [HttpPatch]
        [Route("")]
        [Description("Update Code Table")]
        public IHttpActionResult Patch(CodeTableModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);


                _codeTableService.Update(model);

                return Ok();
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        //// DELETE: api/CodeBase/5
        [HttpDelete]
        [Route("{id:int}")]
        [Description("Delete Code Table By Id")]
        public IHttpActionResult Delete(int id)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);


                _codeTableService.Delete(id);

                return Ok();
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
    }
}
