using bilisimHR.Generator.DbOperations;
using Oracle.DataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;

namespace bilisimHR.Generator
{
    public static class Maps
    {
        private static string Path { get { return HttpContext.Current.Server.MapPath("~\\Files"); } }
        private static string ParentFolder { get { return "Maps"; } }
        private static string Namespace { get { return "bilisimHR.DataLayer.NHibernate.Mappings."; } }


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
                DataTable columns = Islemler.GetColumnsNotInPK(TableName);
                DataTable ForeingKeys = Islemler.GetFK(TableName);
                DataTable Referances = Islemler.GetRef(TableName);

                TableName = textInfo.ToTitleCase(textInfo.ToLower(TableName)).Replace("_", "");

                foreach (DataRow Col in columns.Rows)
                {
                    string ColumnName = textInfo.ToTitleCase(textInfo.ToLower(Col["COLUMN_NAME"].ToString())).Replace("_", "");
                    string DbType = Col["DATA_TYPE"].ToString();
                    string DataType = string.Empty;

                    switch (DbType)
                    {
                        case "NUMBER":
                            DataType = "decimal";
                            break;
                        case "CHAR":
                            DataType = "string";
                            break;
                        case "VARCHAR2":
                            DataType = "string";
                            break;
                        case "DATE":
                            DataType = "DateTime";
                            break;
                        case "LONG":
                            DataType = "string";
                            break;
                        case "LONG RAW":
                            DataType = "string";
                            break;
                        case "BLOB":
                            DataType = "string";
                            break;
                        case "CLOB":
                            DataType = "string";
                            break;
                        default:
                            DataType = "string";
                            break;
                    }

                    Columns += @"
            Map(x => x." + ColumnName + @").Column(""" + Col["COLUMN_NAME"].ToString() + @""")" + (Col["NULLABLE"].ToString() == "N" ? ".Not" : string.Empty) + @".Nullable();";
                }


                string FKs = string.Empty;
                foreach (DataRow fk in ForeingKeys.Rows)
                {
                    FKs += Environment.NewLine + "\t\t\tReferences(x => x." + textInfo.ToTitleCase(textInfo.ToLower(fk["R_TABLE_NAME"].ToString())).Replace("_", "") + @").Column(""" + fk["COLUMN_NAME"].ToString() + @""");";
                }

                string Refs = string.Empty;
                foreach (DataRow r in Referances.Rows)
                {
                    Refs += Environment.NewLine + Environment.NewLine + @"\t\t\tHasMany(x => x." + textInfo.ToTitleCase(textInfo.ToLower(r["TABLE_NAME"].ToString())).Replace("_", "") + @").KeyColumn(""" + r["COLUMN_NAME"].ToString() + @""").Inverse().Cascade.None().ForeignKeyConstraintName(""" + r["CONSTRAINT_NAME"].ToString() + @""");";
                }

                Content = @"using bilisimHR.Common.Helper;
using bilisimHR.DataLayer.Core.Entities."+ FolderName+@";

namespace " + Namespace + FolderName + @"
{
    public class " + TableName + @"Map: EntityBaseMap<" + TableName + @">
    {
        public " + TableName + @"Map()
        {
            Table(""" + item["TABLE_NAME"].ToString() + @""");
            LazyLoad();
            " + Columns + FKs + Refs + @"
        }
    }
}
";

                CreateFiles(TableName + "Map", FolderName, Content);
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

        /*private static DataTable GetModules()
        {
            DbOperations.CmdBLSM cmdBLSM = new CmdBLSM();
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

        private static DataTable GetColumns(string TableName)
        {
            CmdBLSM cmdBLSM = new CmdBLSM();
            cmdBLSM.AddINParameter("P_OWNER", Owner, OracleDbType.Varchar2);
            cmdBLSM.AddINParameter("P_TABLE", TableName, OracleDbType.Varchar2);
            DataTable dt = cmdBLSM.getDataTable(@"SELECT * 
                    FROM ALL_TAB_COLUMNS 
                    WHERE OWNER = :P_OWNER 
                    AND TABLE_NAME = :P_TABLE 
                    AND COLUMN_NAME NOT IN ('ID', 'INSERTED_BY', 'INSERTED_DATE', 'UPDATED_BY', 'UPDATED_DATE')
                    AND COLUMN_NAME NOT IN (SELECT a.column_name
                      FROM all_cons_columns a
                      JOIN all_constraints c ON a.owner = c.owner
                                            AND a.constraint_name = c.constraint_name
                      JOIN all_constraints c_pk ON c.r_owner = c_pk.owner
                                               AND c.r_constraint_name = c_pk.constraint_name
                      JOIN all_cons_columns c_pkc ON c_pkc.constraint_name = c_pk.constraint_name
                     WHERE c.constraint_type = 'R'
                       AND a.owner = :P_OWNER
                       AND a.table_name = :P_TABLE)", hata, CommandType.Text);

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
        }

        private static DataTable GetRef(string TableName)
        {
            CmdBLSM cmdBLSM = new CmdBLSM();
            cmdBLSM.AddINParameter("P_OWNER", Owner, OracleDbType.Varchar2);
            cmdBLSM.AddINParameter("P_TABLE", TableName, OracleDbType.Varchar2);
            DataTable dt = cmdBLSM.getDataTable(@"SELECT C.OWNER, C.R_OWNER, A.CONSTRAINT_NAME, C_PK.CONSTRAINT_NAME R_PK,
       A.TABLE_NAME, A.COLUMN_NAME,
       C_PK.TABLE_NAME R_TABLE_NAME,
       C_PKC.COLUMN_NAME R_COLUMN_NAME
  FROM ALL_CONS_COLUMNS A
  JOIN ALL_CONSTRAINTS C ON A.OWNER = C.OWNER
                        AND A.CONSTRAINT_NAME = C.CONSTRAINT_NAME
  JOIN ALL_CONSTRAINTS C_PK ON C.R_OWNER = C_PK.OWNER
                           AND C.R_CONSTRAINT_NAME = C_PK.CONSTRAINT_NAME
  JOIN ALL_CONS_COLUMNS C_PKC ON C_PKC.CONSTRAINT_NAME = C_PK.CONSTRAINT_NAME
 WHERE C.CONSTRAINT_TYPE = 'R'
   AND C_PK.OWNER = :P_OWNER
   AND C_PK.TABLE_NAME = :P_TABLE",
        hata, CommandType.Text);

            return dt;
        }*/
    }
}