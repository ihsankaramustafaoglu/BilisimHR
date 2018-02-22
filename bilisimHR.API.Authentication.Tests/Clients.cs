using bilisimHR.API.Authentication.Controllers;
using bilisimHR.Business.Model.Auth;
using bilisimHR.Common.Core;
using bilisimHR.Services.Auth.Interfaces;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Web.Http;
using System.Web.Http.Results;

namespace bilisimHR.API.Authentication.Tests
{
    public class Clients
    {
        private Mock<ICoreLogger> _coreLoggerMock;
        private Mock<IClientsService> _clientsServiceMock;

        [SetUp]
        public void TestSetup()
        {
            _coreLoggerMock = new Mock<ICoreLogger> { CallBase = true };
            _clientsServiceMock = new Mock<IClientsService> { CallBase = true };
        }

        [Test]
        public void post_client()
        {

            IList<ClientsModel> expected = new List<ClientsModel>();
            _clientsServiceMock.Setup(c => c.GetAllAsync().Result).Returns(expected);

            var controller = new ClientsController(_coreLoggerMock.Object, _clientsServiceMock.Object);

            IHttpActionResult actionResult = controller.GetAll();
            Assert.IsInstanceOf<OkResult>(actionResult);

            //var actionResult = clientsController.Post(new ClientsModel() {
            //    ClientId = "clientIdTest",
            //    Secret = "clientSecret",
            //    Name = "clientNameTest",
            //    ApplicationType = ApplicationTypes.WebTest,
            //    Active = true,
            //    RefreshTokenLifeTime = 7200,
            //    AllowedOrigin = "*"
            //});

        }
    }
}
