using bilisimHR.Common.Core;
using bilisimHR.Common.Helper;
using bilisimHR.Common.Helper.Tests.DataInitializer.Auth;
using bilisimHR.DataLayer.Core.Entities.Auth;
using bilisimHR.DataLayer.Core.Repositories.Auth;
using bilisimHR.DataLayer.NHibernate.Helper;
using bilisimHR.Services.Auth.Classes;
using bilisimHR.Services.Auth.Interfaces;
using Moq;
using NHibernate;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace bilisimHR.Services.Auth.Tests
{
    public class ClientsServiceTests
    {
        #region Variables
        private IQueryable<Clients> _clients;
        private ISessionFactory _sessionFactory;
        private IUnitOfWork _unitOfWork;
        private IClientsRepository _clientsMockRepository;
        private IClientsService _clientsService;
        
        #endregion
        
        /// <summary>
        /// Initial setup for tests
        /// </summary>
        [OneTimeSetUp]
        public void Setup()
        {
            _clients = ClientsInitializer.GetAllClients();
        }

        #region Setup
        /// <summary>
        /// Re-initializes test.
        /// </summary>
        [SetUp]
        public void ReInitializeTest()
        {
            _clients = ClientsInitializer.GetAllClients();
            _sessionFactory = NHibernateSessionFactory.CreateSessionFactory(DBTypes.SQLite);
            _unitOfWork = new Mock<IUnitOfWork>(MockBehavior.Default).Object;
            _clientsMockRepository = SetUpClientsRepository();
            _clientsService = new ClientsService(_clientsMockRepository);
        }
        #endregion

        #region Tests

        /// <summary>
        /// Service should return all the clients
        /// </summary>
        [Test]
        public void GetAllClientsTest()
        {
            var clients = _clientsService.GetAllAsync().Result;
            Assert.IsNotNull(clients);
            Assert.AreEqual(clients.Count, _clients.ToList().Count);
        }

        /// <summary>
        /// Service should return null
        /// </summary>
        [Test]
        public void GetAllClientsTestForNull()
        {
            _clients.ToList().Clear();
            var clients = _clientsService.GetAllAsync().Result;
            Assert.IsNotNull(clients);
            Assert.Null(clients);
            _clients = ClientsInitializer.GetAllClients();
        }
        #endregion

        #region Private member methods
        private IClientsRepository SetUpClientsRepository()
        {
            // Initialise repository
            // var mockRepo = new Mock<IClientsRepository>(MockBehavior.Default, _sessionFactory);
            var mockRepo = new Mock<IClientsRepository>(MockBehavior.Default);

            mockRepo.Setup(m => m.GetAll()).Returns(_clients);

            mockRepo.Setup(p => p.GetByClientIdAsync(It.IsAny<string>()))
                .Returns(new Func<string, Clients>(
                             id => _clients.ToList().Find(p => p.ClientId.Equals(id))));

            mockRepo.Setup(p => p.Insert((It.IsAny<Clients>())))
                .Callback(new Action<Clients>(newObject =>
                {
                    dynamic maxID = _clients.Last().Id;
                    dynamic nextID = maxID + 1;
                    newObject.Id = nextID;
                    _clients.ToList().Add(newObject);
                }));

            mockRepo.Setup(p => p.Update(It.IsAny<Clients>()))
                .Callback(new Action<Clients>(obj =>
                {
                    var oldObject = _clients.ToList().Find(a => a.Id == obj.Id);
                    oldObject = obj;
                }));

            mockRepo.Setup(p => p.Delete(It.IsAny<int>()))
                .Callback(new Action<int>(objId =>
                {
                    var objectToRemove = _clients.ToList().Find(a => a.Id == objId);

                    if (objectToRemove != null)
                        _clients.ToList().Remove(objectToRemove);
                }));

            return mockRepo.Object;
        }
        #endregion
    }
}
