using bilisimHR.Business.Model.Auth;
using bilisimHR.Common.Core;
using bilisimHR.Common.Helper;
using bilisimHR.Services.Auth.Interfaces;
using System;
using System.ComponentModel;
using System.Web.Http;

namespace bilisimHR.API.Authentication.Controllers
{
    /// <summary>
    /// İstemci İşlemleri
    /// </summary>
    [RoutePrefix("api/Clients")]
    public class ClientsController : ApiController
    {
        //public ClientsController() : base()
        //{

        //}
        #region -Initiating-
        private ICoreLogger _logger;
        private IClientsService _clientService;

        /// <summary>
        /// ClientsController Constructor
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="clientService"></param>
        public ClientsController(ICoreLogger logger, IClientsService clientService)
        {
            _logger = logger;
            _clientService = clientService;
        }
        #endregion

        // GET: api/Clients
        /// <summary>
        /// Get All Clients
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("")]
        [Description("Get All Clients")]
        public IHttpActionResult GetAll()
        {
            try
            {
                return Ok(_clientService.GetAllAsync().Result);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        /// <summary>
        /// Get Client By ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("{id:int}")]
        [Description("Get Client By ID")]
        public IHttpActionResult Get(int id)
        {
            try
            {   
                return Ok(_clientService.GetAsync(id).Result);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
        
        /// <summary>
        /// Get Client By ClientID
        /// </summary>
        /// <param name="clientId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("ClientId/{clientId}")]
        [Description("Get Client By ClientId")]
        public IHttpActionResult GetByClientId(string clientId)
        {
            try
            {
                return Ok(_clientService.GetByClientIdAsync(clientId).Result);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        // POST: api/Clients
        /// <summary>
        /// Create New Client
        /// </summary>
        /// <param name="client"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("")]
        [AllowAnonymous]
        [Description("Create New Client")]
        public IHttpActionResult Post(ClientsModel client)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                client.Secret = Utility.GetHash(client.Secret);
                _clientService.InsertAsync(client);

                return Ok();
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        /// <summary>
        /// Update Client
        /// </summary>
        /// <param name="client"></param>
        /// <returns></returns>
        [HttpPatch]
        [Route("")]
        [Description("Update Client")]
        public IHttpActionResult Patch(ClientsModel client)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                _clientService.UpdateAsync(client);
                return Ok();
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        /// <summary>
        /// Delete Client By ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("{id:int}")]
        [Description("Delete Client By ID")]
        public IHttpActionResult Delete(int id)
        {
            try
            {
                _clientService.DeleteAsync(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
    }
}
