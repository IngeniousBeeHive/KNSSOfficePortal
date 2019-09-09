using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace office.hmvtrust.com.Reports.MasterReports
{
    public partial class PettyCash : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                //ReportViewer1.Visible = true;
                //var dt = repo.GetData("SELECT Branchname,BranchCode,Location,PinCode FROM Branches  ORDER BY BranchName", null);
                //ReportDataSource ds = new ReportDataSource("Branches", dt);


                //string imagePath = "http://52.77.88.220:5055/assets/images/HMVECST_Logo.png";

                //ReportParameter p1 = new ReportParameter("ClientImageUrl", imagePath);
                //ReportParameter p2 = new ReportParameter("ClientName", "Orchids The International School Unit of HMVECS Trust");
                //ReportParameter p3 = new ReportParameter("ClientAddress", "CA Site No.1, Rajajinagar HBCS Layout, Annaporneshwari Nagar, Sy.No.38,39, Srigandadakaval, Nagarbhavi, Bangalore – 560072");
                //ReportParameter p4 = new ReportParameter("ClientContact", "*");



                //ReportViewer1.LocalReport.SetParameters(p1);
                //ReportViewer1.LocalReport.SetParameters(p2);
                //ReportViewer1.LocalReport.SetParameters(p3);
                //ReportViewer1.LocalReport.SetParameters(p4);

                //ReportViewer1.LocalReport.DataSources.Clear();
                //ReportViewer1.LocalReport.DataSources.Add(ds);
                //ReportViewer1.LocalReport.Refresh();
            }
        }
    }
}