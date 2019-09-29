using hmvtrust.core.Entities;
using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NHibernate;
using NHibernate.Cfg;

namespace office.hmvtrust.com.Reports.MasterReports
{
    public partial class PettyCash : System.Web.UI.Page
    {
       

        public DataTable GetData(string query, Dictionary<string, object> parameters)
        {
            Configuration cfg = new Configuration();
            string conString = cfg.GetProperty(NHibernate.Cfg.Environment.ConnectionString);
            DataTable table = new DataTable();
            using (SqlConnection con = new SqlConnection(conString))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand(query, con);
                if (parameters != null)
                {
                    foreach (KeyValuePair<string, object> key in parameters)
                    {
                        cmd.Parameters.AddWithValue(key.Key, key.Value);
                    }
                }
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(table);
                con.Dispose();
            }
            return table;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                
                ReportViewer1.Visible = true;
                var dt = GetData("SELECT * FROM PettyCash ", null);
                ReportDataSource ds = new ReportDataSource("PettyCash", dt);


                string imagePath = "http://52.77.88.220:5055/assets/images/HMVECST_Logo.png";

                ReportParameter p1 = new ReportParameter("ClientImageUrl", imagePath);
                ReportParameter p2 = new ReportParameter("ClientName", "KNSS Horamav Karayogam");
                ReportParameter p3 = new ReportParameter("ClientAddress", "BanjaraLayout, Bangalore – 560016");
                ReportParameter p4 = new ReportParameter("ClientContact", "*");



                ReportViewer1.LocalReport.SetParameters(p1);
                ReportViewer1.LocalReport.SetParameters(p2);
                ReportViewer1.LocalReport.SetParameters(p3);
                ReportViewer1.LocalReport.SetParameters(p4);

                ReportViewer1.LocalReport.DataSources.Clear();
                ReportViewer1.LocalReport.DataSources.Add(ds);
                ReportViewer1.LocalReport.Refresh();
            }
        }
    }
}