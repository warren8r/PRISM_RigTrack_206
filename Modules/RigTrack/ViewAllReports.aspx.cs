using System;
using System.Data;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

public partial class Modules_RigTrack_ViewAllReports : System.Web.UI.Page
{
   
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {


            Button btn = (Button)Page.Master.FindControl("btnClose");

            if (Request.QueryString["CurveGroupID"] != null)
            {
                btn.Visible = true;
            }
            else
            {
                btn.Visible = false;
            }


            int CurveGroupID = 0;
            int TargetID = 0;
            try
            {
                CurveGroupID = Convert.ToInt32(Request.QueryString["CurveGroupID"]);
                TargetID = Convert.ToInt32(Request.QueryString["TargetID"]);
                DataTable reportDT = RigTrack.DatabaseObjects.RigTrackDO.GetAllReports(Convert.ToInt32(Request.QueryString["CurveGroupID"]), Convert.ToInt32(Request.QueryString["TargetID"]));

              

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
            DataTable dt = RigTrack.DatabaseObjects.RigTrackDO.GetAllReports(CurveGroupID, TargetID);
            RadGrid1.DataSource = dt;
            RadGrid1.DataBind();
        }
    }
    protected void RadGrid1_PageIndexChanged(object source, Telerik.Web.UI.GridPageChangedEventArgs e)
    {
        this.RadGrid1.CurrentPageIndex = e.NewPageIndex;

        DataTable dt = RigTrack.DatabaseObjects.RigTrackDO.GetAllReports(Convert.ToInt32(Request.QueryString["CurveGroupID"]), Convert.ToInt32(Request.QueryString["TargetID"]));

        RadGrid1.DataSource = dt;
        RadGrid1.DataBind();
    }
    protected void RadGrid1_PageSizeChanged(object sender, GridPageSizeChangedEventArgs e)
    {

        DataTable dt = RigTrack.DatabaseObjects.RigTrackDO.GetAllReports(Convert.ToInt32(Request.QueryString["CurveGroupID"]), Convert.ToInt32(Request.QueryString["TargetID"]));

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