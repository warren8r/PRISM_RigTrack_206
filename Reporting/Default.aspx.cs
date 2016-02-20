using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Reporting_Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
    //    ReportViewer1.ReportSource = new ReportLibrary.MDM_Staging_Client_1DataSet().;
        Telerik.Reporting.UriReportSource uriReportSource = new Telerik.Reporting.UriReportSource();

        // Specifying an URL or a file path
        uriReportSource.Uri = "SampleReport.trdx";
    }
}