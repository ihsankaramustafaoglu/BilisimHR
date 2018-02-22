using bilisimHR.DataLayer.Core.Entities.Auth;
using System;
using System.Collections.Generic;
using System.Linq;

namespace bilisimHR.Common.Helper.Tests.DataInitializer.Auth
{
    public class ClientsInitializer
    {
        /// <summary>
        /// Dummy Clients
        /// </summary>
        /// <returns></returns>
        public static IQueryable<Clients> GetAllClients()
        {
            var clients = new List<Clients>
            {
                new Clients() { Id = 1, InsertedBy = 1, InsertedDate = DateTime.Now,
                    UpdatedBy = 1, UpdatedDate = DateTime.Now, ClientId = "bilisimHR.WebAppTest",
                    Secret = "bilisimHR@WebAppTest", Name = "bilisimHR Web Portal Test",
                    ApplicationType = ApplicationTypes.WebTest, Active = true,
                    RefreshTokenLifeTime = 7200, AllowedOrigin = "*"
                },
                new Clients() { Id = 2, InsertedBy = 1, InsertedDate = DateTime.Now,
                    UpdatedBy = 1, UpdatedDate = DateTime.Now, ClientId = "bilisimHR.WebDevelopmentTest",
                    Secret = "bilisimHR@WebDevelopmentTest", Name = "bilisimHR Web Portal Development",
                    ApplicationType = ApplicationTypes.WebDevelopment, Active = true,
                    RefreshTokenLifeTime = 7200, AllowedOrigin = "*"
                },
                new Clients() { Id = 3, InsertedBy = 1, InsertedDate = DateTime.Now,
                    UpdatedBy = 1, UpdatedDate = DateTime.Now, ClientId = "bilisimHR.WebProductionTest",
                    Secret = "bilisimHR@WebProductionTest", Name = "bilisimHR Web Portal Production",
                    ApplicationType = ApplicationTypes.WebProduction, Active = true,
                    RefreshTokenLifeTime = 7200, AllowedOrigin = "*"
                },
                new Clients() { Id = 4, InsertedBy = 1, InsertedDate = DateTime.Now,
                    UpdatedBy = 1, UpdatedDate = DateTime.Now, ClientId = "bilisimHR.MobileTest",
                    Secret = "bilisimHR@MobileTest", Name = "bilisimHR Mobile Test",
                    ApplicationType = ApplicationTypes.MobileTest, Active = true,
                    RefreshTokenLifeTime = 7200, AllowedOrigin = "*"
                },
                new Clients() { Id = 5, InsertedBy = 1, InsertedDate = DateTime.Now,
                    UpdatedBy = 1, UpdatedDate = DateTime.Now, ClientId = "bilisimHR.MobileDevelopment",
                    Secret = "bilisimHR@MobileDevelopment", Name = "bilisimHR Mobile Development",
                    ApplicationType = ApplicationTypes.MobileDevelopment, Active = true,
                    RefreshTokenLifeTime = 7200, AllowedOrigin = "*"
                },
                new Clients() { Id = 5, InsertedBy = 1, InsertedDate = DateTime.Now,
                    UpdatedBy = 1, UpdatedDate = DateTime.Now, ClientId = "bilisimHR.MobileProduction",
                    Secret = "bilisimHR@MobileProduction", Name = "bilisimHR Mobile Production",
                    ApplicationType = ApplicationTypes.MobileProduction, Active = true,
                    RefreshTokenLifeTime = 7200, AllowedOrigin = "*"
                },
            };
            return clients.AsQueryable();
        }
    }
}
