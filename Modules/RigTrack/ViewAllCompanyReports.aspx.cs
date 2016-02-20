using System;
using System.Data;
using System.Web.UI.WebControls;
using System.Globalization;
using Telerik.Web.UI;

public partial class Modules_RigTrack_ViewAllCompanyReports : System.Web.UI.Page
{
   
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            int CompanyID = Convert.ToInt32(Request.QueryString["CompanyID"]);
            int StateID = Convert.ToInt32(Request.QueryString["StateID"]);
            string Starter = Request.QueryString["StartDate"];
            string Ender = Request.QueryString["EndDate"];
            if (Starter == null)
                Starter = "Thu, 14 Jul 1825 07:00:00 GMT";
            if (Ender == null)
                Ender = "Thu, 14 Jul 2225 07:00:00 GMT";
            DateTime StartDate = DateTime.Parse(Starter);
            DateTime EndDate = DateTime.Parse(Ender);
            bool Status = Convert.ToBoolean(Request.QueryString["Status"]);

            try
            {
                DataTable reportDT = RigTrack.DatabaseObjects.RigTrackDO.GetAllCompanyReports(CompanyID, StateID, StartDate, EndDate, Status);

                Label lblJobNumber = (Label)(Page.Master.FindControl("lblJobNumber"));
                Label lblStateCountry = (Label)(Page.Master.FindControl("lblStateCountry"));
                Label lblCompany = (Label)(Page.Master.FindControl("lblCompany"));
                Label lblDeclination = (Label)(Page.Master.FindControl("lblDeclination"));
                Label lblLeaseWell = (Label)(Page.Master.FindControl("lblLeaseWell"));
                Label lblGrid = (Label)(Page.Master.FindControl("lblGrid"));
                Label lblLocation = (Label)(Page.Master.FindControl("lblLocation"));
                Label lblJobName = (Label)(Page.Master.FindControl("lblJobName"));
                Label lblRigName = (Label)(Page.Master.FindControl("lblRigName"));
                Label lblCurveName = (Label)(Page.Master.FindControl("lblCurveName"));
                Label lblRKB = (Label)(Page.Master.FindControl("lblRKB"));
                Label lblDateTime = (Label)(Page.Master.FindControl("lblDateTime"));
                Label lblGLorMSL = (Label)(Page.Master.FindControl("lblGLorMSL"));

                lblJobNumber.Text += " " + reportDT.Rows[0]["JobNumber"].ToString();
                lblStateCountry.Text += " " + reportDT.Rows[0]["StateCountry"].ToString();
                lblCompany.Text += " " + reportDT.Rows[0]["Company"].ToString();
                lblDeclination.Text += " " + reportDT.Rows[0]["Declination"].ToString();
                lblLeaseWell.Text += " " + reportDT.Rows[0]["LeaseWell"].ToString();
                lblGrid.Text += " " + reportDT.Rows[0]["Grid"].ToString();
                lblLocation.Text += " " + reportDT.Rows[0]["JobLocation"].ToString();
                lblJobName.Text += " " + reportDT.Rows[0]["CurveGroupName"].ToString();
                lblRigName.Text += " " + reportDT.Rows[0]["RigName"].ToString();
                lblCurveName.Text += " " + reportDT.Rows[0]["CurveName"].ToString();
                lblRKB.Text += " " + reportDT.Rows[0]["RKB"].ToString();
                lblDateTime.Text += " " + reportDT.Rows[0]["CurrentDateTime"].ToString();
                lblGLorMSL.Text += " " + reportDT.Rows[0]["GLorMSL"].ToString();
            }
            catch (Exception ex)
            {
                int i = 0;
            }
            DataTable dt = RigTrack.DatabaseObjects.RigTrackDO.GetAllCompanyReports(CompanyID, StateID, StartDate, EndDate, Status);
            RadGrid1.DataSource = dt;
            RadGrid1.DataBind();
        }


    }
    protected void RadGrid1_PageIndexChanged(object source, Telerik.Web.UI.GridPageChangedEventArgs e)
    {
        this.RadGrid1.CurrentPageIndex = e.NewPageIndex;

        DataTable dt = RigTrack.DatabaseObjects.RigTrackDO.GetAllCompanyReports(Convert.ToInt32(Request.QueryString["CompanyID"]), Convert.ToInt32(Request.QueryString["StateID"]), Convert.ToDateTime(Request.QueryString["StartDate"]), Convert.ToDateTime(Request.QueryString["EndDate"]), Convert.ToBoolean(Request.QueryString["Status"]));

        RadGrid1.DataSource = dt;
        RadGrid1.DataBind();
    }
    protected void RadGrid1_PageSizeChanged(object sender, GridPageSizeChangedEventArgs e)
    {

        DataTable dt = RigTrack.DatabaseObjects.RigTrackDO.GetAllCompanyReports(Convert.ToInt32(Request.QueryString["CompanyID"]), Convert.ToInt32(Request.QueryString["StateID"]), Convert.ToDateTime(Request.QueryString["StartDate"]), Convert.ToDateTime(Request.QueryString["EndDate"]), Convert.ToBoolean(Request.QueryString["Status"]));

        RadGrid1.DataSource = dt;
        RadGrid1.DataBind();
    }
    protected void RadGrid1_ItemCreated(object sender, GridItemEventArgs e)
    {
        if (e.Item is GridCommandItem)
        {
            RadScriptManager RadScriptManager1 = (RadScriptManager)(Page.Master.FindControl("RadScriptManager1"));
            Button btnExcel = (e.Item as GridCommandItem).FindControl("ExportToExcelButton") as Button;
            Button btnPDF = (e.Item as GridCommandItem).FindControl("ExportToPdfButton") as Button;
            Button btnCSV = (e.Item as GridCommandItem).FindControl("ExportToCsvButton") as Button;
            Button btnWord = (e.Item as GridCommandItem).FindControl("ExportToWordButton") as Button;
            RadScriptManager1.RegisterPostBackControl(btnExcel);
            RadScriptManager1.RegisterPostBackControl(btnPDF);
            RadScriptManager1.RegisterPostBackControl(btnCSV);
            RadScriptManager1.RegisterPostBackControl(btnWord);
        }
    }
}