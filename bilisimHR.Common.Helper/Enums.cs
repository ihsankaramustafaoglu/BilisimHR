
namespace bilisimHR.Common.Helper
{
    public enum ApplicationTypes : int
    {
        WebDevelopment = 0,
        WebTest,
        WebProduction,
        MobileDevelopment,
        MobileTest,
        MobileProduction
    };

    public enum DBTypes : int
    {
        Oracle = 0,
        MSSQL,
        MySQL,
        SQLite
    }

    public enum OperationType : int
    {
        Create = 0,
        Read,
        Update,
        Delete,
        Operation
    }
}
