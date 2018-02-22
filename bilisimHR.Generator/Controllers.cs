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
    public class Controllers
    {
        private static string Path { get { return HttpContext.Current.Server.MapPath("~\\Files"); } }
        private static string ParentFolder { get { return "Controllers"; } }
        private static string Namespace { get { return "bilisimHR.API."; } }


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

                string ServiceName = TableName.Substring(0, 1).ToLower() + TableName.Substring(1, TableName.Length - 1);

                Content = @"using bilisimHR.Business.Model." + FolderName + @";
using bilisimHR.Common.Core;
using bilisimHR.Services." + FolderName + @".Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace " + Namespace + FolderName + @".Controllers
{
    [RoutePrefix(""api/"+ TableName + @""")]
    public class " + TableName + @"Controller : ApiController
    {
        #region -Initiating-
        private ICoreLogger _logger;
        private I"+ TableName + @"Service _"+ ServiceName + @"Service;

        /// <summary>
        /// " + TableName + @"Controller Constructor
        /// </summary>
        /// <param name=""logger""></param>
        /// <param name=""" + ServiceName + @"Service""></param>
        public " + TableName + @"Controller(ICoreLogger logger, I" + TableName + @"Service " + ServiceName + @"Service)
        {
            _logger = logger;
            _" + ServiceName + @"Service = " + ServiceName + @"Service;
        }
        #endregion

        // GET: api/" + TableName + @"
        /// <summary>
        /// Get All " + TableName + @"
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("""")]
        [Description(""Get All " + TableName + @""")]
        public IHttpActionResult GetAll()
        {
            try
            {
                return Ok(_" + ServiceName + @"Service.GetAllAsync().Result);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
            return Ok();
        }

        /// <summary>
        /// Get " + TableName + @" By ID
        /// </summary>
        /// <param name=""id""></param>
        /// <returns></returns>
        [HttpGet]
        [Route(""{id:int}"")]
        [Description(""Get " + TableName + @" By ID"")]
        public IHttpActionResult Get(int id)
        {
            try
            {
                return Ok(_" + ServiceName + @"Service.GetAsync(id).Result);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
            return Ok();
        }

        // POST: api/" + TableName + @"
        /// <summary>
        /// Create New " + TableName + @"
        /// </summary>
        /// <param name=""model""></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        [Route(""Post"")]
        [Description(""Create New " + TableName + @""")]
        public IHttpActionResult Post(" + TableName + @"Model model)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                _" + ServiceName + @"Service.InsertAsync(model);

                return Ok();
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }

        }

        // PUT: api/" + TableName + @"/5
        /// <summary>
        /// Update " + TableName + @"
        /// </summary>
        /// <param name=""model""></param>
        /// <returns></returns>
        [HttpPut]
        [Route(""Put"")]
        [Description(""Update " + TableName + @""")]
        public IHttpActionResult Put(" + TableName + @"Model model)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                _" + ServiceName + @"Service.UpdateAsync(model);
                return Ok();
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        // DELETE: api/" + TableName + @"/5
        /// <summary>
        /// Delete " + TableName + @" By ID
        /// </summary>
        /// <param name=""id""></param>
        /// <returns></returns>
        [HttpDelete]
        [Route(""Delete/{id}"")]
        [Description(""Delete Employee By ID"")]
        public IHttpActionResult Delete(int id)
        {
            try
            {
                _" + ServiceName + @"Service.DeleteAsync(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
    }
}
";

                CreateFiles(TableName + @"Controller", FolderName, Content);
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