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
    public class Services
    {
        private static string Path { get { return HttpContext.Current.Server.MapPath("~\\Files"); } }
        private static string ParentFolder { get { return "Services"; } }
        private static string Namespace { get { return "bilisimHR.Services."; } }


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
                DataTable ForeingKeys = Islemler.GetFK(TableName);

                TableName = textInfo.ToTitleCase(textInfo.ToLower(TableName)).Replace("_", "");
                string ServiceName = TableName.Substring(0, 1).ToLower() + TableName.Substring(1, TableName.Length - 1);


                string x = "private readonly I" + TableName + @"Repository _" + ServiceName + @"Repository;";
                string y = "I" + TableName + @"Repository " + ServiceName + @"Repository";
                string z = "_" + ServiceName + @"Repository = " + ServiceName + @"Repository;";
                string a = string.Empty;

                int row = 0;
                foreach (DataRow fk in ForeingKeys.Rows)
                {
                    row++;
                    string FkTableName = fk["R_TABLE_NAME"].ToString();
                    FkTableName = textInfo.ToTitleCase(textInfo.ToLower(FkTableName)).Replace("_", "");
                    string FkServiceName = FkTableName.Substring(0, 1).ToLower() + FkTableName.Substring(1, FkTableName.Length - 1);
                    string FkColumnName = textInfo.ToTitleCase(textInfo.ToLower(fk["COLUMN_NAME"].ToString())).Replace("_", "");

                    x += @"
        private readonly I" + FkTableName + @"Repository _" + FkServiceName + @"Repository;";

                    y += ", I" + FkTableName + @"Repository " + FkServiceName + @"Repository";

                    z += @"
            _" + FkServiceName + @"Repository = " + FkServiceName + @"Repository;";

                    a += FkTableName + @" pk"+ row + @" = _" + FkServiceName + @"Repository.Get((int)model." + FkColumnName + @");
            dto." + FkTableName + @" = pk" + row + @";
            if (pk" + row + @" == null)
                throw new ArgumentNullException("""+ FkColumnName + @" ArgumentNullException Insert Async"");
";
                }


                Content = @"using bilisimHR.DataLayer.Core.Repositories." + FolderName + @";
using bilisimHR.Services." + FolderName + @".Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using bilisimHR.Business.Model." + FolderName + @";
using bilisimHR.DataLayer.Core.Entities." + FolderName + @";
using bilisimHR.Common.Core.AutoMapping;

namespace bilisimHR.Services." + FolderName + @".Classes
{
    public class " + TableName + @"Service : I" + TableName + @"Service
    {
        " + x + @"

        public " + TableName + @"Service(" + y + @")
        {
            " + z + @"
        }

        public Task DeleteAsync(int id)
        {
            _" + ServiceName + @"Repository.Delete(id);
            return Task.FromResult<object>(null);
        }

        public Task<IList<" + TableName + @"Model>> GetAllAsync()
        {
            var dal = _" + ServiceName + @"Repository.GetAll();

            if (dal == null)
                return Task.FromResult<IList<" + TableName + @"Model>>(null);
            else
            {
                IList<" + TableName + @"Model> modelList = new List<" + TableName + @"Model>();

                foreach (var user in dal)
                    modelList.Add(AutoMapperGenericHelper<" + TableName + @", " + TableName + @"Model>.Convert(user));

                return Task.FromResult<IList<" + TableName + @"Model>>(modelList);
                //IQueryable<" + TableName + @"Model> modelList = AutoMapperGenericHelper<" + TableName + @", " + TableName + @"Model>.ConvertAsQueryable(dal);
                //return Task.FromResult<IList<" + TableName + @"Model>>(modelList.ToList());
            }
        }

        public Task<" + TableName + @"Model> GetAsync(int id)
        {
            var dal = _" + ServiceName + @"Repository.Get(id);

            if (dal == null)
                return Task.FromResult<" + TableName + @"Model>(null);
            else
            {
                " + TableName + @"Model model = AutoMapperGenericHelper<" + TableName + @", " + TableName + @"Model>.Convert(dal);
                return Task.FromResult(model);
            }
        }

        public Task<object> InsertAsync(" + TableName + @"Model model)
        {
            if (model == null)
                throw new ArgumentNullException(""" + TableName + @"Model ArgumentNullException Insert Async"");

            " + TableName + @" dto = AutoMapperGenericHelper<" + TableName + @"Model, " + TableName + @">.Convert(model);

            "+ a + @"

            var id = _" + ServiceName + @"Repository.Insert(dto);
            return Task.FromResult<object>(id);
        }

        public Task UpdateAsync(" + TableName + @"Model model)
        {
            if (model == null)
                throw new ArgumentNullException(""" + TableName + @"Model ArgumentNullException Insert Async"");

            " + TableName + @" dto = AutoMapperGenericHelper<" + TableName + @"Model, " + TableName + @">.Convert(model);

            " + a + @"

            _" + ServiceName + @"Repository.Update(dto);

            return Task.FromResult<object>(null);
        }
    }
}
";

                CreateFiles(TableName + @"Service", FolderName, Content);
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
        }

        private static DataTable GetFK(string TableName)
        {
            CmdBLSM cmdBLSM = new CmdBLSM();
            cmdBLSM.AddINParameter("P_OWNER", Owner, OracleDbType.Varchar2);
            cmdBLSM.AddINParameter("P_TABLE", TableName, OracleDbType.Varchar2);
            DataTable dt = cmdBLSM.getDataTable(@"SELECT c.owner, c.r_owner, a.constraint_name, c_pk.constraint_name r_pk,
       a.table_name, a.column_name,
       c_pk.table_name r_table_name,
       c_pkc.column_name r_column_Name
  FROM all_cons_columns a
  JOIN all_constraints c ON a.owner = c.owner
                        AND a.constraint_name = c.constraint_name
  JOIN all_constraints c_pk ON c.r_owner = c_pk.owner
                           AND c.r_constraint_name = c_pk.constraint_name
  JOIN all_cons_columns c_pkc ON c_pkc.constraint_name = c_pk.constraint_name
 WHERE c.constraint_type = 'R'
   AND a.owner = :P_OWNER
   AND a.table_name = :P_TABLE",
        hata, CommandType.Text);

            return dt;
        }*/
    }
}