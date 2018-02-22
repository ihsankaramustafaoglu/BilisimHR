using bilisimHR.Generator.DbOperations;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;

namespace bilisimHR.Generator
{
    public class NHibernateRepositories
    {
        private static string Path { get { return HttpContext.Current.Server.MapPath("~\\Files"); } }
        private static string ParentFolder { get { return "NHibernateRepositories"; } }
        private static string Namespace { get { return "bilisimHR.DataLayer.NHibernate.Repositories."; } }


        public static void Generate()
        {
            CreateFolders();
            GenerateModel();
        }

        private static void GenerateModel()
        {
            TextInfo textInfo = new CultureInfo("en-US", false).TextInfo;

            DataTable tables = Islemler.GetTables();

            foreach (DataRow item in tables.Rows)
            {
                string Content = string.Empty;
                string TableName = item["TABLE_NAME"].ToString();
                string FolderName = item["FOLDER_NAME"].ToString();
                string Columns = string.Empty;

                TableName = textInfo.ToTitleCase(textInfo.ToLower(TableName)).Replace("_", "");

                Content = @"using bilisimHR.DataLayer.Core.Entities." + FolderName + @";
using bilisimHR.DataLayer.Core.Repositories." + FolderName + @";
using NHibernate;

namespace " + Namespace + FolderName + @"
{
    public class " + TableName + @"Repository : RepositoryBase<" + TableName + @", int>, I" + TableName + @"Repository
    {
        public " + TableName + @"Repository(ISessionFactory sessionFactory) : base(sessionFactory)
        {
            //...
        }
    }
}
";

                CreateFiles(TableName + @"Repository", FolderName, Content);
            }
        }

        private static void CreateFolders()
        {
            DataTable modules = Islemler.GetModules();

            foreach (DataRow item in modules.Rows)
            {
                string FolderPath = Path + "\\" + ParentFolder + "\\" + item["FOLDER_NAME"];
                bool exists = System.IO.Directory.Exists(FolderPath);

                if (!exists)
                    System.IO.Directory.CreateDirectory(FolderPath);
            }
        }

        private static void CreateFiles(string TableName, string FolderName, string Content)
        {
            string FileName = Path + "\\" + ParentFolder + "\\" + FolderName + "\\" + TableName + ".cs";
            File.Create(FileName).Close();
            File.AppendAllText(FileName, Content);
        }
        /*
        private static DataTable GetModules()
        {
            CmdBLSM cmdBLSM = new CmdBLSM();
            DataTable dt = cmdBLSM.getDataTable("SELECT * FROM " + ModuleOwner + ".G_MODULES", hata, CommandType.Text);

            return dt;
        }

        private static DataTable GetTables()
        {
            CmdBLSM cmdBLSM = new CmdBLSM();
            cmdBLSM.AddINParameter("P_OWNER", Owner, OracleDbType.Varchar2);
            DataTable dt = cmdBLSM.getDataTable(@"SELECT * 
FROM ALL_TABLES T, " + ModuleOwner + @".G_TABLES I, " + ModuleOwner + @".G_MODULES M
WHERE T.TABLE_NAME = I.TABLE_NAME
AND M.ID = I.G_MODULES_ID
AND OWNER = :P_OWNER", hata, CommandType.Text);

            return dt;
        }*/
    }
}