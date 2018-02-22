using bilisimHR.Common.Helper;
using bilisimHR.DataLayer.Core.Entities.Auth;
using bilisimHR.DataLayer.NHibernate.Helper;
using Moq;
using NHibernate;
using NUnit.Framework;

namespace bilisimHR.DataLayer.NHibernate.Tests
{
    public class NHibernate
    {
        private static ISessionFactory _sessionFactory;
        private static UnitOfWork _uow;

        [SetUp]
        public void Initialize()
        {
            _sessionFactory = NHibernateSessionFactory.CreateSessionFactory(DBTypes.Oracle);
            //_sessionFactory = NHibernateSessionFactory.CreateSessionFactoryFromXML(@"D:\Bilisim\Git\bilisimHR\bilisimHR.DataLayer.NHibernate.Tests\bin\x64\Debug");
            var uowMock = new Mock<UnitOfWork>(_sessionFactory);
            _uow = uowMock.Object;
        }

        [Test]
        public void insert_role_and_check_id_type()
        {
            _uow.BeginTransaction();
            int id = (int)_uow.Session.Save(new Roles { Name = "Çalışan" });
            _uow.Commit();

            Assert.IsNotNull(id, "Insert işlemi sırasında hata oluştu. ID null");
            Assert.AreEqual(typeof(int), id.GetType(), "Insert işlemi sırasında hata oluştu. ID değeri int türünde değildir.");
        }

        [Test]
        public void update_role_and_check_is_updated()
        {
            _uow.BeginTransaction();
            Roles firstRole = _uow.Session.Get<Roles>(1);
            firstRole.Name = "IK";
            _uow.Session.Update(firstRole);
            _uow.Commit();

            _uow.BeginTransaction();
            Roles updatedRole = _uow.Session.Get<Roles>(1);
            _uow.Commit();

            Assert.That(firstRole.Name, Is.EqualTo(updatedRole.Name));
        }

        [Test]
        public void delete_role_and_check_is_deleted()
        {
            _uow.BeginTransaction();
            Roles role = _uow.Session.Get<Roles>(1);
            _uow.Session.Delete(role);
            _uow.Commit();

            _uow.BeginTransaction();
            Roles deletedRole = _uow.Session.Get<Roles>(1);
            _uow.Commit();

            Assert.IsNull(deletedRole, "Delete işlemi sırasında hata meydana geldi.");
        }

        [Test]
        public void insert_user_and_check_id_type()
        {
            string saltedPassword = string.Empty;
            string salt = string.Empty;

            Utility.SaltPassword("test", out salt, out saltedPassword);

            _uow.BeginTransaction();
            int id = (int)_uow.Session.Save(new Users
            {
                UserName = "bispiroglu",
                PasswordHash = saltedPassword,
                Salt = salt,
                EmailConfirmed = false,
                PhoneNumberConfirmed = false,
                LockoutEnabled = false,
                AccessFailedCount = 0
            });
            _uow.Commit();

            Assert.IsNotNull(id, "Insert işlemi sırasında hata oluştu. ID null");
            Assert.AreEqual(typeof(int), id.GetType(), "Insert işlemi sırasında hata oluştu. ID değeri int türünde değildir.");
        }
        
        [Test]
        public void deleting_role_should_not_delete_user()
        {
            _uow.BeginTransaction();
            int roleId = (int)_uow.Session.Save(new Roles { Name = "Çalışan" });

            string saltedPassword = string.Empty;
            string salt = string.Empty;

            Utility.SaltPassword("test", out salt, out saltedPassword);
            int userId = (int)_uow.Session.Save(new Users
            {
                UserName = "bispiroglu",
                PasswordHash = saltedPassword,
                Salt = salt,
                EmailConfirmed = false,
                PhoneNumberConfirmed = false,
                LockoutEnabled = false,
                AccessFailedCount = 0
            });
            _uow.Commit();


            _uow.BeginTransaction();
            Roles role = _uow.Session.Get<Roles>(roleId);
            Users user = _uow.Session.Get<Users>(userId);
            user. Roles.Add(role);
            _uow.Commit();
            
            _uow.BeginTransaction();
            _uow.Session.Delete(role);
            _uow.Commit();
        }

        [Test]
        public void deleting_user_should_not_delete_role()
        {
            _uow.BeginTransaction();
            int roleId = (int)_uow.Session.Save(new Roles { Name = "Çalışan" });

            string saltedPassword = string.Empty;
            string salt = string.Empty;

            Utility.SaltPassword("test", out salt, out saltedPassword);
            int userId = (int)_uow.Session.Save(new Users
            {
                UserName = "bispiroglu",
                PasswordHash = saltedPassword,
                Salt = salt,
                EmailConfirmed = false,
                PhoneNumberConfirmed = false,
                LockoutEnabled = false,
                AccessFailedCount = 0
            });
            _uow.Commit();


            _uow.BeginTransaction();
            Roles role = _uow.Session.Get<Roles>(roleId);
            Users user = _uow.Session.Get<Users>(userId);
            user.Roles.Add(role);
            _uow.Commit();

            _uow.BeginTransaction();
            _uow.Session.Delete(user);
            _uow.Commit();
        }

        [Test]
        public void deleting_user_without_assiociation_to_user_roles()
        {
            _uow.BeginTransaction();
            
            string saltedPassword = string.Empty;
            string salt = string.Empty;

            Utility.SaltPassword("test", out salt, out saltedPassword);
            int userId = (int)_uow.Session.Save(new Users
            {
                UserName = "bispiroglu",
                PasswordHash = saltedPassword,
                Salt = salt,
                EmailConfirmed = false,
                PhoneNumberConfirmed = false,
                LockoutEnabled = false,
                AccessFailedCount = 0
            });
            _uow.Commit();

            _uow.BeginTransaction();
            Users user = _uow.Session.Get<Users>(userId);
            _uow.Commit();

            _uow.BeginTransaction();
            _uow.Session.Delete(user);
            _uow.Commit();
        }

        [Test]
        public void deleting_role_without_assiociation_to_user_roles()
        {
            _uow.BeginTransaction();
            int roleId = (int)_uow.Session.Save(new Roles { Name = "Çalışan" });
            _uow.Commit();

            _uow.BeginTransaction();
            Roles role = _uow.Session.Get<Roles>(roleId);
            _uow.Commit();

            _uow.BeginTransaction();
            _uow.Session.Delete(role);
            _uow.Commit();
        }

        [Test]
        public void add_role_over_the_user_entity()
        {
            _uow.BeginTransaction();

            string saltedPassword = string.Empty;
            string salt = string.Empty;

            Utility.SaltPassword("test", out salt, out saltedPassword);
            int userId = (int)_uow.Session.Save(new Users
            {
                UserName = "bispiroglu",
                PasswordHash = saltedPassword,
                Salt = salt,
                EmailConfirmed = false,
                PhoneNumberConfirmed = false,
                LockoutEnabled = false,
                AccessFailedCount = 0
            });
            _uow.Commit();

            _uow.BeginTransaction();
            Users user = _uow.Session.Get<Users>(userId);
            user.Roles.Add(new Roles { Name = "Test 2" });
            _uow.Session.Save(user);
            _uow.Commit();
        }

        [Test]
        public void init_auth_data()
        {
            #region Init Client
            _uow.BeginTransaction();
            Clients client = new Clients() {
                ClientId = "bilisimHR.WebApp",
                Secret = Utility.GetHash("bilisimHR@WebApp"),
                Name = "bilisimHR Web Portali",
                ApplicationType = ApplicationTypes.WebDevelopment,
                Active = true,
                RefreshTokenLifeTime = 7200,
                AllowedOrigin = "*"
            };
            int clientId = (int)_uow.Session.Save(client);
            _uow.Commit();
            #endregion

            #region Init User
            _uow.BeginTransaction();
            string saltedPassword = string.Empty;
            string salt = string.Empty;

            Utility.SaltPassword("123456", out salt, out saltedPassword);
            int userId = (int)_uow.Session.Save(new Users
            {
                UserName = "cispiroglu",
                PasswordHash = saltedPassword,
                Salt = salt,
                EmailConfirmed = false,
                PhoneNumberConfirmed = false,
                LockoutEnabled = false,
                AccessFailedCount = 0
            });
            _uow.Commit();
            #endregion

            #region Init Role
            _uow.BeginTransaction();
            int roleId = (int)_uow.Session.Save(new Roles { Name = "Yetkilendirme(Tam)" });
            _uow.Commit();
            #endregion

            #region Init Page
            _uow.BeginTransaction();
            int pageId = (int)_uow.Session.Save(new Pages { Name = "Administration" });
            _uow.Commit();
            #endregion

            #region InitControllerActions
            _uow.BeginTransaction();
            _uow.Session.Save(new ControllerActions { Controller = "ClientsController", Action = "GetAll", Description = "Get All Clients", OperationType = (OperationType)1 });
            _uow.Session.Save(new ControllerActions { Controller = "ClientsController", Action = "Get", Description = "Get Client By ID", OperationType = (OperationType)1 });
            _uow.Session.Save(new ControllerActions { Controller = "ClientsController", Action = "GetByClientId", Description = "Get Client By ClientId", OperationType = (OperationType)1 });
            _uow.Session.Save(new ControllerActions { Controller = "ClientsController", Action = "Post", Description = "Create New Client", OperationType = (OperationType)0 });
            _uow.Session.Save(new ControllerActions { Controller = "ClientsController", Action = "Patch", Description = "Update Client", OperationType = (OperationType)2 });
            _uow.Session.Save(new ControllerActions { Controller = "ClientsController", Action = "Delete", Description = "Delete Client By ID", OperationType = (OperationType)3 });
            _uow.Session.Save(new ControllerActions { Controller = "PagesController", Action = "GetAll", Description = "Get All Pages", OperationType = (OperationType)1 });
            _uow.Session.Save(new ControllerActions { Controller = "PagesController", Action = "Get", Description = "Get Page By ID", OperationType = (OperationType)1 });
            _uow.Session.Save(new ControllerActions { Controller = "PagesController", Action = "Post", Description = "Create New Page", OperationType = (OperationType)0 });
            _uow.Session.Save(new ControllerActions { Controller = "PagesController", Action = "Patch", Description = "Update Page", OperationType = (OperationType)2 });
            _uow.Session.Save(new ControllerActions { Controller = "PagesController", Action = "Delete", Description = "Delete Page By ID", OperationType = (OperationType)3 });
            _uow.Session.Save(new ControllerActions { Controller = "PagesController", Action = "InsertControllerAction", Description = "Insert ControllerAction(s) To Page", OperationType = (OperationType)2 });
            _uow.Session.Save(new ControllerActions { Controller = "PagesController", Action = "DeleteControllerAction", Description = "Delete ControllerAction(s) From Page", OperationType = (OperationType)2 });
            _uow.Session.Save(new ControllerActions { Controller = "RoleInPagesController", Action = "GetAll", Description = "Get All RoleInPages", OperationType = (OperationType)1 });
            _uow.Session.Save(new ControllerActions { Controller = "RoleInPagesController", Action = "Get", Description = "Get RoleInPages By ID", OperationType = (OperationType)1 });
            _uow.Session.Save(new ControllerActions { Controller = "RoleInPagesController", Action = "Post", Description = "Create New RoleInPages", OperationType = (OperationType)0 });
            _uow.Session.Save(new ControllerActions { Controller = "RoleInPagesController", Action = "Patch", Description = "Update RoleInPages", OperationType = (OperationType)2 });
            _uow.Session.Save(new ControllerActions { Controller = "RoleInPagesController", Action = "Delete", Description = "Delete RoleInPages By ID", OperationType = (OperationType)3 });
            _uow.Session.Save(new ControllerActions { Controller = "RolesController", Action = "GetAll", Description = "Get All Roles", OperationType = (OperationType)1 });
            _uow.Session.Save(new ControllerActions { Controller = "RolesController", Action = "Get", Description = "Get Role By ID", OperationType = (OperationType)1 });
            _uow.Session.Save(new ControllerActions { Controller = "RolesController", Action = "Post", Description = "Create New Role", OperationType = (OperationType)0 });
            _uow.Session.Save(new ControllerActions { Controller = "RolesController", Action = "Patch", Description = "Update Role", OperationType = (OperationType)2 });
            _uow.Session.Save(new ControllerActions { Controller = "RolesController", Action = "Delete", Description = "Delete Role By ID", OperationType = (OperationType)3 });
            _uow.Session.Save(new ControllerActions { Controller = "RolesController", Action = "InsertUsers", Description = "Insert User(s) To Role", OperationType = (OperationType)2 });
            _uow.Session.Save(new ControllerActions { Controller = "RolesController", Action = "DeleteRoles", Description = "Delete User(s) From Role", OperationType = (OperationType)2 });
            _uow.Session.Save(new ControllerActions { Controller = "RolesController", Action = "InsertPages", Description = "Insert Pages(s) To Role", OperationType = (OperationType)2 });
            _uow.Session.Save(new ControllerActions { Controller = "RolesController", Action = "DeletePages", Description = "Delete Pages(s) From Role", OperationType = (OperationType)2 });
            _uow.Session.Save(new ControllerActions { Controller = "UsersController", Action = "GetAll", Description = "Get All Users", OperationType = (OperationType)1 });
            _uow.Session.Save(new ControllerActions { Controller = "UsersController", Action = "GetById", Description = "Get User By ID", OperationType = (OperationType)1 });
            _uow.Session.Save(new ControllerActions { Controller = "UsersController", Action = "GetByEmail", Description = "Get User By Email", OperationType = (OperationType)1 });
            _uow.Session.Save(new ControllerActions { Controller = "UsersController", Action = "GetByUserName", Description = "Get User By UserName", OperationType = (OperationType)1 });
            _uow.Session.Save(new ControllerActions { Controller = "UsersController", Action = "Post", Description = "Create User", OperationType = (OperationType)0 });
            _uow.Session.Save(new ControllerActions { Controller = "UsersController", Action = "Patch", Description = "Update User", OperationType = (OperationType)2 });
            _uow.Session.Save(new ControllerActions { Controller = "UsersController", Action = "Delete", Description = "Delete User By ID", OperationType = (OperationType)3 });
            _uow.Session.Save(new ControllerActions { Controller = "UsersController", Action = "InsertRoles", Description = "Insert Role(s) To User", OperationType = (OperationType)2 });
            _uow.Session.Save(new ControllerActions { Controller = "UsersController", Action = "DeleteRoles", Description = "Delete Role(s) From User", OperationType = (OperationType)2 });
            _uow.Commit();
            #endregion

            #region Set Page Actions
            _uow.BeginTransaction();
            Pages page = _uow.Session.Get<Pages>(pageId);
            page.ControllerActions.Add(_uow.Session.Get<ControllerActions>(1));
            page.ControllerActions.Add(_uow.Session.Get<ControllerActions>(2));
            page.ControllerActions.Add(_uow.Session.Get<ControllerActions>(3));
            page.ControllerActions.Add(_uow.Session.Get<ControllerActions>(4));
            page.ControllerActions.Add(_uow.Session.Get<ControllerActions>(5));
            page.ControllerActions.Add(_uow.Session.Get<ControllerActions>(6));
            page.ControllerActions.Add(_uow.Session.Get<ControllerActions>(7));
            page.ControllerActions.Add(_uow.Session.Get<ControllerActions>(8));
            page.ControllerActions.Add(_uow.Session.Get<ControllerActions>(9));
            page.ControllerActions.Add(_uow.Session.Get<ControllerActions>(10));
            page.ControllerActions.Add(_uow.Session.Get<ControllerActions>(11));
            page.ControllerActions.Add(_uow.Session.Get<ControllerActions>(12));
            page.ControllerActions.Add(_uow.Session.Get<ControllerActions>(13));
            page.ControllerActions.Add(_uow.Session.Get<ControllerActions>(14));
            page.ControllerActions.Add(_uow.Session.Get<ControllerActions>(15));
            page.ControllerActions.Add(_uow.Session.Get<ControllerActions>(16));
            page.ControllerActions.Add(_uow.Session.Get<ControllerActions>(17));
            page.ControllerActions.Add(_uow.Session.Get<ControllerActions>(18));
            page.ControllerActions.Add(_uow.Session.Get<ControllerActions>(19));
            page.ControllerActions.Add(_uow.Session.Get<ControllerActions>(20));
            page.ControllerActions.Add(_uow.Session.Get<ControllerActions>(21));
            page.ControllerActions.Add(_uow.Session.Get<ControllerActions>(22));
            page.ControllerActions.Add(_uow.Session.Get<ControllerActions>(23));
            page.ControllerActions.Add(_uow.Session.Get<ControllerActions>(24));
            page.ControllerActions.Add(_uow.Session.Get<ControllerActions>(25));
            page.ControllerActions.Add(_uow.Session.Get<ControllerActions>(26));
            page.ControllerActions.Add(_uow.Session.Get<ControllerActions>(27));
            page.ControllerActions.Add(_uow.Session.Get<ControllerActions>(28));
            page.ControllerActions.Add(_uow.Session.Get<ControllerActions>(29));
            page.ControllerActions.Add(_uow.Session.Get<ControllerActions>(30));
            page.ControllerActions.Add(_uow.Session.Get<ControllerActions>(31));
            page.ControllerActions.Add(_uow.Session.Get<ControllerActions>(32));
            page.ControllerActions.Add(_uow.Session.Get<ControllerActions>(33));
            page.ControllerActions.Add(_uow.Session.Get<ControllerActions>(34));
            page.ControllerActions.Add(_uow.Session.Get<ControllerActions>(35));
            page.ControllerActions.Add(_uow.Session.Get<ControllerActions>(36));
            _uow.Session.Save(page);
            _uow.Commit();
            #endregion

            #region Set Role Pages
            _uow.BeginTransaction();
            RoleInPages roleInPages = new RoleInPages()
            {
                Role = _uow.Session.Get<Roles>(roleId),
                Page = _uow.Session.Get<Pages>(pageId),
                Create = false,
                Read = false,
                Update = true,
                Delete = true
            };
            _uow.Session.Save(roleInPages);
            _uow.Commit();
            #endregion

            #region Set User Roles
            _uow.BeginTransaction();
            Users user = _uow.Session.Get<Users>(userId);
            user.Roles.Add(_uow.Session.Get<Roles>(roleId));
            _uow.Session.Save(user);
            _uow.Commit();
            #endregion
        }

        [Test]
        public void delete_role_id_bigger_than_one()
        {
            
            for (int i = 2; i < 29000; ++i)
            {
                _uow.BeginTransaction();
                Roles role = _uow.Session.Get<Roles>(i);
                _uow.Session.Delete(role);
                _uow.Commit();
            }
        }
    }
}
