using bilisimHR.Generator.DbOperations;
using System;
using System.Data;
using System.Globalization;
using System.IO;
using System.Web;

namespace bilisimHR.Generator
{
    public static class AngularModels
    {
        private static string Path { get { return HttpContext.Current.Server.MapPath("~\\Files"); } }
        private static string ParentFolder { get { return "AngularModels"; } }
        //private static string Namespace { get { return "bilisimHR.Business.Model."; } }


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
                string FolderName = item["FOLDER_NAME"].ToString().ToLower();
                string Columns = string.Empty;
                DataTable columns = Islemler.GetColumnsFull(TableName);
                //DataTable ForeingKeys = Islemler.GetFK(TableName);
                //DataTable Referances = Islemler.GetRef(TableName);

                TableName = textInfo.ToTitleCase(textInfo.ToLower(TableName)).Replace("_", "");

                foreach (DataRow Col in columns.Rows)
                {
                    string ColumnName = textInfo.ToTitleCase(textInfo.ToLower(Col["COLUMN_NAME"].ToString())).Replace("_", "");
                    string DbType = Col["DATA_TYPE"].ToString();
                    string DataType = string.Empty;

                    switch (DbType)
                    {
                        case "NUMBER":
                            DataType = "number";
                            break;
                        case "CHAR":
                            DataType = "string";
                            break;
                        case "VARCHAR2":
                            DataType = "string";
                            break;
                        case "DATE":
                            DataType = "Date";
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
    " + ColumnName + ": " + DataType + ";";
                }


                //string FKs = string.Empty;
                //foreach (DataRow fk in ForeingKeys.Rows)
                //{
                //    FKs += Environment.NewLine + Environment.NewLine + "\t\tpublic " + textInfo.ToTitleCase(textInfo.ToLower(fk["R_TABLE_NAME"].ToString())).Replace("_", "") + "Model " + textInfo.ToTitleCase(textInfo.ToLower(fk["R_TABLE_NAME"].ToString())).Replace("_", "") + " { get; }";
                //}

                //string Refs = string.Empty;
                //foreach (DataRow r in Referances.Rows)
                //{
                //    Refs += Environment.NewLine + Environment.NewLine + "\t\tpublic virtual IList<" + textInfo.ToTitleCase(textInfo.ToLower(r["TABLE_NAME"].ToString())).Replace("_", "") + "Model> " + textInfo.ToTitleCase(textInfo.ToLower(r["TABLE_NAME"].ToString())).Replace("_", "") + " { get; set; }";
                //}

                Content = @"export class " + TableName + @" {" + Columns + @"
}";

                CreateFiles(TableName, FolderName, Content);
            }
        }

        private static void CreateFolders()
        {
            DataTable modules = Islemler.GetModules();

            foreach (DataRow item in modules.Rows)
            {
                string FolderPath = Path + "\\" + ParentFolder + "\\" + item["FOLDER_NAME"].ToString().ToLower();
                bool exists = System.IO.Directory.Exists(FolderPath);

                if (!exists)
                    System.IO.Directory.CreateDirectory(FolderPath);
            }
        }

        private static void CreateFiles(string TableName, string FolderName, string Content)
        {
            string FileName = Path + "\\" + ParentFolder + "\\" + FolderName + "\\" + TableName + ".models.ts";
            File.Create(FileName).Close();
            File.AppendAllText(FileName, Content);
        }
    }
}