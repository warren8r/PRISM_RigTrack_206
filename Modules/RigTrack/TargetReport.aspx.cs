using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Modules_RigTrack_TargetReport : System.Web.UI.Page
{
    #region Page Behavior
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindHeader();
            //BindGrid();
            BindFields();
        }
    }

    private void BindFields()
    {
        int targetID = Convert.ToInt32(Request.QueryString["TargetID"]);
        DataTable dt = RigTrack.DatabaseObjects.RigTrackDO.GetTargetInfoFromTargetID(targetID);

        lblTargetTVD.Text = dt.Rows[0]["TVD"].ToString();
        lblTargetEW.Text = dt.Rows[0]["EWCoordinate"].ToString();
        lblTargetNS.Text = dt.Rows[0]["NSCoordinate"].ToString();
        lblTargetDirection.Text = dt.Rows[0]["PolarDirection"].ToString();
        lblTargetDisp.Text = dt.Rows[0]["PolarDistance"].ToString();

        int curveID = Convert.ToInt32(Request.QueryString["CurveID"]);
        DataTable dt2 = RigTrack.DatabaseObjects.RigTrackDO.GetSurveysForCurve(curveID);
        DataTable dt3 = RigTrack.DatabaseObjects.RigTrackDO.GetCurveInfo(curveID);

        if (dt2.Rows.Count > 1)
        {
            DataRow row2 = dt2.Rows[dt2.Rows.Count - 1];
            DataRow row1 = dt2.Rows[dt2.Rows.Count - 2];

            double row2MD = double.Parse(row2["MD"].ToString());
            double row2Inc = double.Parse(row2["Inclination"].ToString());
            double row2AZ = double.Parse(row2["Azimuth"].ToString());
            double row2TVD = double.Parse(row2["TVD"].ToString());
            double row2NS = double.Parse(row2["NS"].ToString());
            double row2EW = double.Parse(row2["EW"].ToString());
            double row2VS = double.Parse(row2["VerticalSection"].ToString());
            double row2WR = double.Parse(row2["WR"].ToString());
            double row2BR = double.Parse(row2["BR"].ToString());
            double row2DLS = double.Parse(row2["DLS"].ToString());
            double row2TFO = double.Parse(row2["TFO"].ToString());
            double row2Distance = double.Parse(row2["ClosureDistance"].ToString());
            double row2Direction = double.Parse(row2["ClosureDirection"].ToString());

            double row1MD = double.Parse(row1["MD"].ToString());
            double row1Inc = double.Parse(row1["Inclination"].ToString());
            double row1AZ = double.Parse(row1["Azimuth"].ToString());
            double row1TVD = double.Parse(row1["TVD"].ToString());
            double row1NS = double.Parse(row1["NS"].ToString());
            double row1EW = double.Parse(row1["EW"].ToString());
            double row1VS = double.Parse(row1["VerticalSection"].ToString());
            double row1WR = double.Parse(row1["WR"].ToString());
            double row1BR = double.Parse(row1["BR"].ToString());
            double row1DLS = double.Parse(row1["DLS"].ToString());
            double row1TFO = double.Parse(row1["TFO"].ToString());
            double row1Distance = double.Parse(row1["ClosureDistance"].ToString());
            double row1Direction = double.Parse(row1["ClosureDirection"].ToString());

            lblMDSensor.Text = row2MD.ToString();
            lblINCSensor.Text = row2Inc.ToString();
            lblAZMSensor.Text = row2AZ.ToString();
            lblTVDSensor.Text = row2TVD.ToString();
            lblEWSensor.Text = row2NS.ToString();
            lblNSSensor.Text = row2EW.ToString();
            lblVSSensor.Text = row2VS.ToString();
            lblDLSSensor.Text = row2DLS.ToString();
            lblBRSensor.Text = row2BR.ToString();
            lblWRSensor.Text = row2WR.ToString();

            double bitToSensor;
            if (double.TryParse(dt3.Rows[0]["BitToSensor"].ToString(), out bitToSensor))
            {
                lblMDBit.Text = (Math.Round((row2MD + bitToSensor), 4)).ToString();
                lblINCBit.Text = (Math.Round((row2Inc - row1Inc) + row2Inc, 4)).ToString();
                lblAZMBit.Text = (Math.Round((row2AZ - row1AZ) + row2AZ, 4)).ToString();
                lblTVDBit.Text = (Math.Round((row2TVD - row1TVD) + row2TVD, 4)).ToString();
                lblEWBit.Text = (Math.Round((row2EW - row1EW) + row2EW, 4)).ToString();
                lblNSBit.Text = (Math.Round((row2NS - row1NS) + row2NS, 4)).ToString();
                lblVSBit.Text = (Math.Round((row2VS - row1VS) + row2VS, 4)).ToString();
                lblDLSBit.Text = (Math.Round((row2DLS - row1DLS) + row2DLS, 4)).ToString();
                lblBRBit.Text = (Math.Round((row2BR - row1BR) + row2BR, 4)).ToString();
                lblWRBit.Text = (Math.Round((row2WR - row1WR) + row2WR, 4)).ToString();
            }
        }
    }
    #endregion
    #region Buttons
    protected void btnClear_Click(object sender, EventArgs e)
    {
        //Response.Redirect("CreateCurveGroup.aspx");
    }
    #endregion
    #region Utility Methods
    protected void BindHeader()
    {
        int TargetID = 0;
        try
        {
            TargetID = Convert.ToInt32(Request.QueryString["TargetID"]);
            DataTable TargetDT = RigTrack.DatabaseObjects.RigTrackDO.GetTargetReportFromTargetID(TargetID);

            string imgName = "No Image Available";
            string imgType = "No Image Available";
            byte[] imgBytes;
            string base64String = "No Image Available";
            Image imgLogo = new Image();
            imgLogo = (Image)(Page.Master.FindControl("imgLogo"));

            if (!DBNull.Value.Equals(TargetDT.Rows[0]["Attachment"]))
            {
                imgName = TargetDT.Rows[0]["Name"].ToString();
                imgType = TargetDT.Rows[0]["Type"].ToString();
                imgBytes = (byte[])TargetDT.Rows[0]["Attachment"];
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

            lblJobNumber.Text += " " + TargetDT.Rows[0]["JobNumber"].ToString();
            lblStateCountry.Text += " " + TargetDT.Rows[0]["StateCountry"].ToString();
            lblCompany.Text += " " + TargetDT.Rows[0]["Company"].ToString();
            lblDeclination.Text += " " + TargetDT.Rows[0]["Declination"].ToString();
            lblLeaseWell.Text += " " + TargetDT.Rows[0]["LeaseWell"].ToString();
            lblGrid.Text += " " + TargetDT.Rows[0]["Grid"].ToString();
            lblLocation.Text += " " + TargetDT.Rows[0]["JobLocation"].ToString();
            lblJobName.Text += " " + TargetDT.Rows[0]["CurveGroupName"].ToString();
            lblRigName.Text += " " + TargetDT.Rows[0]["RigName"].ToString();
            lblCurveName.Text += " " + TargetDT.Rows[0]["CurveName"].ToString();
            lblRKB.Text += " " + TargetDT.Rows[0]["RKB"].ToString();
            lblDateTime.Text += " " + TargetDT.Rows[0]["CurrentDateTime"].ToString();
            lblGLorMSL.Text += " " + TargetDT.Rows[0]["GLorMSL"].ToString();

            lbl_Header1.Text = TargetDT.Rows[0]["Company"].ToString();
            if (!DBNull.Value.Equals(TargetDT.Rows[0]["HeaderComments"]))
            {
                Header1.Text = TargetDT.Rows[0]["HeaderComments"].ToString();
                Header1.Visible = true;
            }
            if (!DBNull.Value.Equals(TargetDT.Rows[0]["ExtraHeaderComments"]))
            {
                lbl_Header2.Text = "Additional Comments: ";
                lbl_Header2.Attributes.Add("style", "display:inline");
                Header2.Text = TargetDT.Rows[0]["ExtraHeaderComments"].ToString();
                Header2.Visible = true;
            }
        }
        catch (Exception ex)
        {
            int i = 0;
        }
    }
    #endregion
    protected void RadGrid1_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
    {
        DataTable dt = new DataTable();
        RadGrid1.DataSource = dt;
    }
    protected void RadGrid2_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
    {
        DataTable dt = new DataTable();
        RadGrid2.DataSource = dt;
    }
    protected void RadGrid3_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
    {
        DataTable dt = new DataTable();
        RadGrid3.DataSource = dt;
    }
}