using bilisimHR.DataLayer.Core.Entities.Auth;

namespace bilisimHR.DataLayer.NHibernate.Mappings.Auth
{
    public class UsersMap : EntityBaseMap<Users>
    {
        //Constructor
        public UsersMap()
        {
            Table("USERS");
            LazyLoad();
            Map(x => x.Email).Column("EMAIL").Nullable();
            Map(x => x.EmailConfirmed).Column("EMAIL_CONFIRMED").Not.Nullable();
            Map(x => x.PasswordHash).Column("PASSWORD_HASH").Nullable();
            Map(x => x.Salt).Column("SALT").Nullable();
            Map(x => x.SecurityStamp).Column("SECURITY_STAMP").Nullable(); //.Default(Guid.NewGuid().ToString("D"));
            Map(x => x.PhoneNumber).Column("PHONE_NUMBER").Nullable();
            Map(x => x.PhoneNumberConfirmed).Column("PHONE_NUMBER_CONFIRMED").Not.Nullable();
            Map(x => x.TwoFactorEnabled).Column("TWO_FACTOR_ENABLED").Not.Nullable();
            Map(x => x.LockoutEndDate).Column("LOCKOUT_END_DATE").Nullable();
            Map(x => x.LockoutEnabled).Column("LOCKOUT_ENABLED").Not.Nullable();
            Map(x => x.AccessFailedCount).Column("ACCESS_FAILED_COUNT").Not.Nullable();
            Map(x => x.UserName).Column("USER_NAME").Not.Nullable();
            HasManyToMany(x => x.Roles).Table("USER_IN_ROLES").Cascade.None();
            /*HasManyToMany(x => x.Roles)
                    .Cascade.All()
                    .Table("UserInRole").ForeignKeyConstraintNames("Users_Id_Fk", "Roles_Id_Fk");*/
        }
    }
}
