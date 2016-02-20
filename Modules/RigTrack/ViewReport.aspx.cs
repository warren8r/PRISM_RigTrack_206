using System;
using System.Data;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
public partial class Modules_RigTrack_ViewReport : System.Web.UI.Page
{
    #region Page Behavior
    protected void Page_Load(object sender, EventArgs e)
    { 
        if (!IsPostBack)
        {
            if (Request.QueryString["ReportID"] != null)
            {
                if (Request.QueryString["ReportID"].ToString() != "undefined")
                {
                    Session["ReportID"] = Request.QueryString["ReportID"];
                }
                ViewState["ReportID"] = Session["ReportID"];
                BindHeader();
                BindGrid();
                //string test = ViewState["ReportID"].ToString();
            }
        }
    }
    protected void RadGrid1_PageIndexChanged(object source, Telerik.Web.UI.GridPageChangedEventArgs e)
    {
        this.RadGrid1.CurrentPageIndex = e.NewPageIndex;
        BindGrid();
    }
    protected void RadGrid1_PageSizeChanged(object sender, GridPageSizeChangedEventArgs e)
    {
        BindGrid();
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
    #endregion
    #region Buttons
    #endregion
    #region Utility Methods
    protected RigTrack.DatabaseTransferObjects.ReportSearchDTO AssignValues()
    {
        RigTrack.DatabaseTransferObjects.ReportSearchDTO ReportSearchDTO = new RigTrack.DatabaseTransferObjects.ReportSearchDTO();

        int ReportID = 0;
        try
        {
            ReportID = Convert.ToInt32(Request.QueryString["ReportID"]);
        }
        catch (Exception) { }
        return ReportSearchDTO;
    }
    protected void BindHeader()
    {
        int ReportID = 0;
        try
        {
            //ReportID = Convert.ToInt32(Request.QueryString["ReportID"]);
            ReportID = Convert.ToInt32(ViewState["ReportID"]);
            DataTable reportDT = RigTrack.DatabaseObjects.RigTrackDO.GetReportFromID(ReportID);

            string imgName = "No Image Available";
            string imgType = "No Image Available";
            byte[] imgBytes;
            string base64String = "No Image Available";
            Image imgLogo = new Image();
            imgLogo = (Image)(Page.Master.FindControl("imgLogo"));

            if (!DBNull.Value.Equals(reportDT.Rows[0]["Attachment"]))
            {
                imgName = reportDT.Rows[0]["Name"].ToString();
                imgType = reportDT.Rows[0]["Type"].ToString();
                imgBytes = (byte[])reportDT.Rows[0]["Attachment"];
                base64String = Convert.ToBase64String(imgBytes, 0, imgBytes.Length);
            }
            else
                imgLogo.Visible = false;
            imgLogo.ImageUrl = "data:" + imgType + ";base64," + base64String;
            imgLogo.Width = 87;
            imgLogo.Height = 69;

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
            lblCompany.Text += " " + reportDT.Rows[0]["CompanyName"].ToString();
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

            lbl_Header1.Text = reportDT.Rows[0]["Company"].ToString();
            if (!DBNull.Value.Equals(reportDT.Rows[0]["HeaderComments"]))
            {
                Header1.Text = reportDT.Rows[0]["HeaderComments"].ToString();
                Header1.Visible = true;
            }
            if (!DBNull.Value.Equals(reportDT.Rows[0]["ExtraHeaderComments"]))
            {
                lbl_Header2.Text = "Additional Comments: ";
                lbl_Header2.Attributes.Add("style", "display:inline");
                Header2.Text = reportDT.Rows[0]["ExtraHeaderComments"].ToString();
                Header2.Visible = true;
            }
        }
        catch (Exception ex)
        {
            int i = 0;
        }
    }
    protected void BindGrid()
    {
        string Column1Value = "";
        int ReportID = 0;
        try
        {
            //ReportID = Convert.ToInt32(Request.QueryString["ReportID"]);
            ReportID = Convert.ToInt32(ViewState["ReportID"]);
        }
        catch (Exception ex)
        {
            int i = 0;
        }
        DataTable dt = RigTrack.DatabaseObjects.RigTrackDO.GetReport(ReportID);
        if(dt.Rows.Count == 1)
        { 
            foreach (DataRow row in dt.Rows)
            {
                try
                {
                    if(row["Column1"] != null)
                        Column1Value = row["Column1"].ToString();
                }
                catch (Exception ex)
                {
                    Column1Value = "";
                }
            }
        }
        if (Column1Value == "")
        { 
            RadGrid1.DataSource = dt;
            RadGrid1.DataBind();
        }
        else
        {
            gridlabel.Visible = true;
            RadGrid1.Visible = false;
        }
    }
    #endregion
}