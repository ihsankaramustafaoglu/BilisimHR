using Oracle.DataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace bilisimHR.Generator
{
    public partial class Default : System.Web.UI.Page
    {
        private static string Path { get { return HttpContext.Current.Server.MapPath("~\\Files"); } }
        protected void Page_Load(object sender, EventArgs e)
        {
            FilesDelete();
            Models.Generate();
            Entities.Generate();
            Maps.Generate();
            Repositories.Generate();
            NHibernateRepositories.Generate();
            Controllers.Generate();
            Interfaces.Generate();
            Services.Generate();
            AngularModels.Generate();
        }
        private void FilesDelete()
        {
            DirectoryInfo di = new DirectoryInfo(Path);

            foreach (FileInfo file in di.GetFiles())
            {
                file.Delete();
            }

            foreach (DirectoryInfo directory in di.GetDirectories())
            {
                directory.Delete(true);
            }
        }

    }
}