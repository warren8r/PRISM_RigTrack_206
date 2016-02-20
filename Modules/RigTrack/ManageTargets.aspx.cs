using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using Telerik.Web.UI;
using System.Web.Services;
using System.Text;

public partial class Modules_RigTrack_ManageTargets : System.Web.UI.Page
{
    bool disableTargetTable = true;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ddlCompany.DataSource = RigTrack.DatabaseObjects.RigTrackDO.GetCompanyWithJob();
            ddlCompany.DataTextField = "Name";
            ddlCompany.DataValueField = "ID";
            ddlCompany.DataBind();

            //ddlCurveGroup.DataSource = RigTrack.DatabaseObjects.RigTrackDO.GetAllCurveGroupNames();
            //ddlCurveGroup.DataTextField = "CurveGroupName";
            //ddlCurveGroup.DataValueField = "ID";
            //ddlCurveGroup.DataBind();
            //RadGridTargets.DataSource = RigTrack.DatabaseObjects.RigTrackDO.GetTargetsForCurveGroupID(0);
            //RadGridTargets.DataBind();

        }
    }

    protected void ddlCompany_SelectedIndexChanged(object sender, DropDownListEventArgs e)
    {
        if (ddlCompany.SelectedValue != "0")
        {
            ddlCurveGroup.Items.Clear();
            ddlCurveGroup.Items.Add(new DropDownListItem("-Select-", "0"));

            ddlCurveGroup.DataSource = RigTrack.DatabaseObjects.RigTrackDO.GetCurveGroupForCompany(Int32.Parse(ddlCompany.SelectedValue));
            ddlCurveGroup.DataTextField = "CurveGroupName";
            ddlCurveGroup.DataValueField = "ID";
            ddlCurveGroup.DataBind();
        }
        else
        {
            ddlCurveGroup.Items.Clear();
            ddlCurveGroup.Items.Add(new DropDownListItem("-Select-", "0"));
            ddlCurveGroup.SelectedValue = "0";

            btnShowGraph.Enabled = false;
            DisableTargetShapeTextboxes();
            DataTable dt = new DataTable();
            RadGridTargets.DataSource = dt;
            RadGridTargets.DataBind();

        }

    }
    protected void ddlCurveGroup_SelectedIndexChanged(object sender, Telerik.Web.UI.DropDownListEventArgs e)
    {
        if (ddlCurveGroup.SelectedValue != "0")
        {
            ViewState["CurveGroupID"] = ddlCurveGroup.SelectedValue;
            DataTable dt = RigTrack.DatabaseObjects.RigTrackDO.GetTargetsForCurveGroupID(Convert.ToInt32(ddlCurveGroup.SelectedValue));
            RadGridTargets.DataSource = dt;
            RadGridTargets.DataBind();
            btnShowGraph.Enabled = true;
            //btnAddRow.Enabled = true;
            //btnSave.Enabled = true;
            //btnClear.Enabled = true;
        }
        else
        {
            btnShowGraph.Enabled = false;
            DisableTargetShapeTextboxes();
            DataTable dt = new DataTable();
            RadGridTargets.DataSource = dt;
            RadGridTargets.DataBind();
            //btnAddRow.Enabled = true;
            //btnSave.Enabled = true;
            //btnClear.Enabled = true;
        }
    }
    protected void RadGridTargets_ItemCreated(object sender, Telerik.Web.UI.GridItemEventArgs e)
    {

    }
    protected void RadGridTargets_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
    {
        if (ViewState["CurveGroupID"] != null)
        {
            DataTable dt = RigTrack.DatabaseObjects.RigTrackDO.GetTargetsForCurveGroupID(Convert.ToInt32(ViewState["CurveGroupID"].ToString()));
            RadGridTargets.DataSource = dt;
        }
        else
        {
            DataTable dt = new DataTable();
            RadGridTargets.DataSource = dt;
        }
    }
    protected void RadGridTargets_UpdateCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
    {
        RigTrack.DatabaseTransferObjects.TargetDTO targetDTO = new RigTrack.DatabaseTransferObjects.TargetDTO();

        if (e.Item is GridEditableItem && e.Item.IsInEditMode)
        {

            GridEditableItem item = e.Item as GridEditableItem;

            int targetID;
            string targetName, targetComments;
            double targetShapeID, TVD, nsCoordinate, ewCoordinate, polarDistance, polarDirection, incLastTarget, azmLastTarget, incAtTarget, azmAtTarget, numberVertices, rotation, targetThickness, drawingPattern;

            Int32.TryParse((item["ID"].Controls[0] as TextBox).Text, out targetID);
            targetName = (item["TargetName"].Controls[0] as TextBox).Text;
            //RadDropDownList ddlTargetShape = item.FindControl("ddlTargetShape") as RadDropDownList;
            double.TryParse(ddlTargetShape.SelectedValue, out targetShapeID);
            bool boolTVD = double.TryParse((item["TVD"].Controls[0] as TextBox).Text, out TVD);
            bool boolNS = double.TryParse((item["NSCoordinate"].Controls[0] as TextBox).Text, out nsCoordinate);
            bool boolEW = double.TryParse((item["EWCoordinate"].Controls[0] as TextBox).Text, out ewCoordinate);
            double.TryParse((item["PolarDistance"].Controls[0] as TextBox).Text, out polarDistance);
            double.TryParse((item["PolarDirection"].Controls[0] as TextBox).Text, out polarDirection);
            double.TryParse((item["INCFromLastTarget"].Controls[0] as TextBox).Text, out incLastTarget);
            double.TryParse((item["AZMFromLastTarget"].Controls[0] as TextBox).Text, out azmLastTarget);
            double.TryParse((item["InclinationAtTarget"].Controls[0] as TextBox).Text, out incAtTarget);
            double.TryParse((item["AzimuthAtTarget"].Controls[0] as TextBox).Text, out azmAtTarget);
            //double.TryParse((item["NumberVertices"].Controls[0] as TextBox).Text, out numberVertices);
            //double.TryParse((item["Rotation"].Controls[0] as TextBox).Text, out rotation);
            double.TryParse((item["TargetThickness"].Controls[0] as TextBox).Text, out targetThickness);
            RadDropDownList ddlDrawingPattern = item.FindControl("ddlDrawingPattern") as RadDropDownList;
            double.TryParse(ddlDrawingPattern.SelectedValue, out drawingPattern);
            targetComments = (item["TargetComment"].Controls[0] as TextBox).Text;

            if (ddlTargetShape.SelectedValue != "0")
            {
                targetDTO.TargetOffsetXoffset = Convert.ToDouble(txtTargetXOffset.Text);
                targetDTO.TargetOffsetYoffset = Convert.ToDouble(txtTargetYOffset.Text);
                targetDTO.DiameterOfCircleXoffset = Convert.ToDouble(txtXDiameter.Text);
                targetDTO.DiameterOfCircleYoffset = Convert.ToDouble(txtYDiameter.Text);
                targetDTO.Corner1Xoffset = Convert.ToDouble(txtXCorner1.Text);
                targetDTO.Corner1Yoffset = Convert.ToDouble(txtYCorner1.Text);
                targetDTO.Corner2Xoffset = Convert.ToDouble(txtXCorner2.Text);
                targetDTO.Corner2Yoffset = Convert.ToDouble(txtYCorner2.Text);
                targetDTO.Corner3Xoffset = Convert.ToDouble(txtXCorner3.Text);
                targetDTO.Corner3Yoffset = Convert.ToDouble(txtYCorner3.Text);
                targetDTO.Corner4Xoffset = Convert.ToDouble(txtXCorner4.Text);
                targetDTO.Corner4Yoffset = Convert.ToDouble(txtYCorner4.Text);
                targetDTO.Corner5Xoffset = Convert.ToDouble(txtXCorner5.Text);
                targetDTO.Corner5Yoffset = Convert.ToDouble(txtYCorner5.Text);
                targetDTO.Corner6Xoffset = Convert.ToDouble(txtXCorner6.Text);
                targetDTO.Corner6Yoffset = Convert.ToDouble(txtYCorner6.Text);
                targetDTO.Corner7Xoffset = Convert.ToDouble(txtXCorner7.Text);
                targetDTO.Corner7Yoffset = Convert.ToDouble(txtYCorner7.Text);
                targetDTO.Corner8Xoffset = Convert.ToDouble(txtXCorner8.Text);
                targetDTO.Corner8Yoffset = Convert.ToDouble(txtYCorner8.Text);
                targetDTO.NumberVertices = Convert.ToDouble(ddlVertices.SelectedValue);
                targetDTO.Rotation = Convert.ToDouble(txtRotation.Text);
                if (btnToTarget.Checked)
                {
                    targetDTO.ReferenceOptionID = 1000;
                }
                else if (btnToWellhead.Checked)
                {
                    targetDTO.ReferenceOptionID = 1001;
                }
            }

            targetDTO.ID = targetID;
            targetDTO.Name = targetName;
            targetDTO.TargetShapeID = targetShapeID;
            targetDTO.TVD = TVD;
            targetDTO.NSCoordinate = nsCoordinate;
            targetDTO.EWCoordinate = ewCoordinate;
            targetDTO.PolarDistance = polarDistance;
            targetDTO.PolarDirection = polarDirection;
            targetDTO.INCFromLastTarget = incLastTarget;
            targetDTO.AZMFromLastTarget = azmLastTarget;
            targetDTO.InclinationAtTarget = incAtTarget;
            targetDTO.AzimuthAtTarget = azmAtTarget;
            //targetDTO.NumberVertices = numberVertices;
            //targetDTO.Rotation = rotation;
            targetDTO.TargetThickness = targetThickness;
            targetDTO.DrawingPattern = drawingPattern;
            targetDTO.TargetComment = targetComments;

            if (targetDTO.Name == "" || boolTVD == false || boolNS == false || boolEW == false)
            {
                lblValidation.Visible = true;
               
            }

            else if ((double.Parse(txtXDiameter.Text) == 0) || (double.Parse(txtYDiameter.Text) == 0))
            {
                
                lblValidationXYDiameter.Visible = true;
            }

            else
            {
                lblValidation.Visible = false;
                lblValidationXYDiameter.Visible = false;
                int returnID = RigTrack.DatabaseObjects.RigTrackDO.InsertUpdateManageTarget(targetDTO);
            }
        }
    }

    protected void RadGridTargets_InsertCommand(object sender, GridCommandEventArgs e)
    {
        RigTrack.DatabaseTransferObjects.TargetDTO targetDTO = new RigTrack.DatabaseTransferObjects.TargetDTO();

        if (e.Item is GridEditableItem && e.Item.IsInEditMode)
        {
            GridEditableItem item = e.Item as GridEditableItem;

            int targetID;
            string targetName, targetComments;
            double targetShapeID, TVD, nsCoordinate, ewCoordinate, polarDistance, polarDirection, incLastTarget, azmLastTarget, incAtTarget, azmAtTarget, numberVertices, rotation, targetThickness, drawingPattern;

            targetName = (item["TargetName"].Controls[0] as TextBox).Text;
            //RadDropDownList ddlTargetShape = item.FindControl("ddlTargetShape") as RadDropDownList;
            double.TryParse(ddlTargetShape.SelectedValue, out targetShapeID);
            bool boolTVD = double.TryParse((item["TVD"].Controls[0] as TextBox).Text, out TVD);
            bool boolNS = double.TryParse((item["NSCoordinate"].Controls[0] as TextBox).Text, out nsCoordinate);
            bool boolEW = double.TryParse((item["EWCoordinate"].Controls[0] as TextBox).Text, out ewCoordinate);
            double.TryParse((item["PolarDistance"].Controls[0] as TextBox).Text, out polarDistance);
            double.TryParse((item["PolarDirection"].Controls[0] as TextBox).Text, out polarDirection);
            double.TryParse((item["INCFromLastTarget"].Controls[0] as TextBox).Text, out incLastTarget);
            double.TryParse((item["AZMFromLastTarget"].Controls[0] as TextBox).Text, out azmLastTarget);
            double.TryParse((item["InclinationAtTarget"].Controls[0] as TextBox).Text, out incAtTarget);
            double.TryParse((item["AzimuthAtTarget"].Controls[0] as TextBox).Text, out azmAtTarget);
            //double.TryParse((item["NumberVertices"].Controls[0] as TextBox).Text, out numberVertices);
            //double.TryParse((item["Rotation"].Controls[0] as TextBox).Text, out rotation);
            double.TryParse((item["TargetThickness"].Controls[0] as TextBox).Text, out targetThickness);
            RadDropDownList ddlDrawingPattern = item.FindControl("ddlDrawingPattern") as RadDropDownList;
            double.TryParse(ddlDrawingPattern.SelectedValue, out drawingPattern);
            targetComments = (item["TargetComment"].Controls[0] as TextBox).Text;

            if (ddlTargetShape.SelectedValue != "0")
            {
                targetDTO.TargetOffsetXoffset = Convert.ToDouble(txtTargetXOffset.Text);
                targetDTO.TargetOffsetYoffset = Convert.ToDouble(txtTargetYOffset.Text);
                targetDTO.DiameterOfCircleXoffset = Convert.ToDouble(txtXDiameter.Text);
                targetDTO.DiameterOfCircleYoffset = Convert.ToDouble(txtYDiameter.Text);
                targetDTO.Corner1Xoffset = Convert.ToDouble(txtXCorner1.Text);
                targetDTO.Corner1Yoffset = Convert.ToDouble(txtYCorner1.Text);
                targetDTO.Corner2Xoffset = Convert.ToDouble(txtXCorner2.Text);
                targetDTO.Corner2Yoffset = Convert.ToDouble(txtYCorner2.Text);
                targetDTO.Corner3Xoffset = Convert.ToDouble(txtXCorner3.Text);
                targetDTO.Corner3Yoffset = Convert.ToDouble(txtYCorner3.Text);
                targetDTO.Corner4Xoffset = Convert.ToDouble(txtXCorner4.Text);
                targetDTO.Corner4Yoffset = Convert.ToDouble(txtYCorner4.Text);
                targetDTO.Corner5Xoffset = Convert.ToDouble(txtXCorner5.Text);
                targetDTO.Corner5Yoffset = Convert.ToDouble(txtYCorner5.Text);
                targetDTO.Corner6Xoffset = Convert.ToDouble(txtXCorner6.Text);
                targetDTO.Corner6Yoffset = Convert.ToDouble(txtYCorner6.Text);
                targetDTO.Corner7Xoffset = Convert.ToDouble(txtXCorner7.Text);
                targetDTO.Corner7Yoffset = Convert.ToDouble(txtYCorner7.Text);
                targetDTO.Corner8Xoffset = Convert.ToDouble(txtXCorner8.Text);
                targetDTO.Corner8Yoffset = Convert.ToDouble(txtYCorner8.Text);
                targetDTO.NumberVertices = Convert.ToDouble(ddlVertices.SelectedValue);
                targetDTO.Rotation = Convert.ToDouble(txtRotation.Text);
                if (btnToTarget.Checked)
                {
                    targetDTO.ReferenceOptionID = 1000;
                }
                else if (btnToWellhead.Checked)
                {
                    targetDTO.ReferenceOptionID = 1001;
                }
            }

            targetDTO.ID = 0;
            targetDTO.CurveGroupID = Convert.ToInt32(ViewState["CurveGroupID"].ToString());
            targetDTO.Name = targetName;
            targetDTO.TargetShapeID = targetShapeID;
            targetDTO.TVD = TVD;
            targetDTO.NSCoordinate = nsCoordinate;
            targetDTO.EWCoordinate = ewCoordinate;
            targetDTO.PolarDistance = polarDistance;
            targetDTO.PolarDirection = polarDirection;
            targetDTO.INCFromLastTarget = incLastTarget;
            targetDTO.AZMFromLastTarget = azmLastTarget;
            targetDTO.InclinationAtTarget = incAtTarget;
            targetDTO.AzimuthAtTarget = azmAtTarget;
            //targetDTO.NumberVertices = numberVertices;
            //targetDTO.Rotation = rotation;
            targetDTO.TargetThickness = targetThickness;
            targetDTO.DrawingPattern = drawingPattern;
            targetDTO.TargetComment = targetComments;

            if (targetDTO.Name == "" || boolTVD == false || boolNS == false || boolEW == false)
            {
                lblValidation.Visible = true;
            }
            else if ((double.Parse(txtXDiameter.Text) == 0) || (double.Parse(txtYDiameter.Text) == 0))
            {

                lblValidationXYDiameter.Visible = true;
            }
            else
            {
                lblValidation.Visible = false;
                lblValidationXYDiameter.Visible = false;
                int returnID = RigTrack.DatabaseObjects.RigTrackDO.InsertUpdateManageTarget(targetDTO);
            }
        }
    }

    protected void RadGridTargets_ItemDataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
    {
        if (e.Item is GridEditableItem && e.Item.IsInEditMode)
        {
            disableTargetTable = false;
            EnableTargetShapeTextboxes();

            lblValidation.Visible = false;
            lblValidationXYDiameter.Visible = false;

            GridEditableItem item = e.Item as GridEditableItem;


            //RadDropDownList ddlTargetShape = item.FindControl("ddlTargetShape") as RadDropDownList;
            ddlTargetShape.SelectedText = (item["TargetShapeName"].Controls[0] as TextBox).Text;
            ddlTargetShape.Width = Unit.Pixel(70);
            RadDropDownList ddlDrawingPattern = item.FindControl("ddlDrawingPattern") as RadDropDownList;
            ddlDrawingPattern.SelectedText = (item["DrawingPatternName"].Controls[0] as TextBox).Text;
            ddlDrawingPattern.Width = Unit.Pixel(80);
            TextBox txtTargetName = (TextBox)item["TargetName"].Controls[0];
            TextBox txtTVD = (TextBox)item["TVD"].Controls[0];
            TextBox txtNSCoord = (TextBox)item["NSCoordinate"].Controls[0];
            TextBox txtEWCoord = (TextBox)item["EWCoordinate"].Controls[0];
            TextBox txtPolarDistance = (TextBox)item["PolarDistance"].Controls[0];
            TextBox txtPolarDirection = (TextBox)item["PolarDirection"].Controls[0];
            TextBox txtINCLastTarget = (TextBox)item["INCFromLastTarget"].Controls[0];
            TextBox txtAZMLastTarget = (TextBox)item["AZMFromLastTarget"].Controls[0];
            TextBox txtIncl = (TextBox)item["InclinationAtTarget"].Controls[0];
            TextBox txtAzimuth = (TextBox)item["AzimuthAtTarget"].Controls[0];
            //TextBox txtNumberVertices = (TextBox)item["NumberVertices"].Controls[0];
            //TextBox txtRotation = (TextBox)item["Rotation"].Controls[0];
            TextBox txtTargetThickness = (TextBox)item["TargetThickness"].Controls[0];
            TextBox txtTargetComments = (TextBox)item["TargetComment"].Controls[0];
            txtTargetName.Width = Unit.Pixel(70);
            txtTVD.Width = Unit.Pixel(70);
            txtNSCoord.Width = Unit.Pixel(70);
            txtEWCoord.Width = Unit.Pixel(70);
            txtPolarDistance.Width = Unit.Pixel(70);
            txtPolarDirection.Width = Unit.Pixel(70);
            txtINCLastTarget.Width = Unit.Pixel(70);
            txtAZMLastTarget.Width = Unit.Pixel(70);
            txtIncl.Width = Unit.Pixel(70);
            txtAzimuth.Width = Unit.Pixel(70);
            //txtNumberVertices.Width = Unit.Pixel(70);
            //txtRotation.Width = Unit.Pixel(70);
            txtTargetThickness.Width = Unit.Pixel(70);
            txtTargetComments.Width = Unit.Pixel(70);

            txtEWCoord.Attributes.Add("onChange", "OnChangeCalculations('" + txtTVD.ClientID + "','" + txtNSCoord.ClientID + "','" + txtINCLastTarget.ClientID + "','" + txtAZMLastTarget.ClientID + "','" + txtEWCoord.ClientID + "','" + txtPolarDirection.ClientID + "','" + txtPolarDistance.ClientID + "');");

            ddlTargetShape_SelectedIndexChanged(ddlTargetShape, null);

            int targetID;
            Int32.TryParse((item["ID"].Controls[0] as TextBox).Text, out targetID);

            if (targetID != 0)
            {
                DataTable dt = RigTrack.DatabaseObjects.RigTrackDO.GetTargetShapeDetails(targetID);
                double dblVertices = Convert.ToDouble(dt.Rows[0]["NumberVertices"].ToString());
                int numVertices = Convert.ToInt32(dblVertices);

                ddlVertices.SelectedValue = numVertices.ToString();
                if (numVertices != 4)
                {
                    ddlVertices_SelectedIndexChanged(null, null);
                }

                txtTargetXOffset.Text = dt.Rows[0]["TargetOffsetXoffset"].ToString();
                txtTargetYOffset.Text = dt.Rows[0]["TargetOffsetYoffset"].ToString();
                txtXDiameter.Text = dt.Rows[0]["DiameterOfCircleXoffset"].ToString();
                txtYDiameter.Text = dt.Rows[0]["DiameterOfCircleYoffset"].ToString();
                txtXCorner1.Text = dt.Rows[0]["Corner1Xofffset"].ToString();
                txtYCorner1.Text = dt.Rows[0]["Corner1Yoffset"].ToString();
                txtXCorner2.Text = dt.Rows[0]["Corner2Xoffset"].ToString();
                txtYCorner2.Text = dt.Rows[0]["Corner2Yoffset"].ToString();
                txtXCorner3.Text = dt.Rows[0]["Corner3Xoffset"].ToString();
                txtYCorner3.Text = dt.Rows[0]["Corner3Yoffset"].ToString();
                txtXCorner4.Text = dt.Rows[0]["Corner4Xoffset"].ToString();
                txtYCorner4.Text = dt.Rows[0]["Corner4Yoffset"].ToString();
                txtXCorner5.Text = dt.Rows[0]["Corner5Xoffset"].ToString();
                txtYCorner5.Text = dt.Rows[0]["Corner5Yoffset"].ToString();
                txtXCorner6.Text = dt.Rows[0]["Corner6Xoffset"].ToString();
                txtYCorner6.Text = dt.Rows[0]["Corner6Yoffset"].ToString();
                txtXCorner7.Text = dt.Rows[0]["Corner7Xoffset"].ToString();
                txtYCorner7.Text = dt.Rows[0]["Corner7Yoffset"].ToString();
                txtXCorner8.Text = dt.Rows[0]["Corner8Xoffset"].ToString();
                txtYCorner8.Text = dt.Rows[0]["Corner8Xoffset"].ToString();
                txtRotation.Text = dt.Rows[0]["Rotation"].ToString();

                if (dt.Rows[0]["ReferenceOptionID"].ToString() == "1000")
                {
                    btnToTarget.Checked = true;
                    btnToWellhead.Checked = false;
                }
                else if (dt.Rows[0]["ReferenceOptionID"].ToString() == "1001")
                {
                    btnToTarget.Checked = false;
                    btnToWellhead.Checked = true;
                }
                else
                {
                    btnToTarget.Checked = true;
                    btnToWellhead.Checked = false;
                }

                //lblTargetSelectedID.Text = targetID.ToString();
                lblTargetSelectedID.Text = (item["TargetName"].Controls[0] as TextBox).Text;

                // jd this calls the drawshapes javascript funtion when user clicks the edit button on the radgrid
                hdnTargetID.Value = ddlTargetShape.SelectedValue;
                hdnVertices.Value = ddlVertices.SelectedValue;


                ScriptManager.RegisterStartupScript(this, this.GetType(), "DrawShapesFromEdit", "DrawShapesEditButton(" + hdnTargetID.Value + "," + hdnVertices.Value + ");", true);


            }
        }
        else if (disableTargetTable)
        {
            DisableTargetShapeTextboxes();
        }
    }
    protected void RadGridTargets_ItemCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
    {

    }

    [WebMethod]
    public static string CalculatePolarDirection(string north, string east)
    {
        double dblNorth = 0;
        double dblEast = 0;
        double.TryParse(north, out dblNorth);
        double.TryParse(east, out dblEast);
        double valuedirection = CurvatureCalculations.FindClosureDirection(dblNorth, dblEast);
        double valuedistance = CurvatureCalculations.FindClosureDistance(dblNorth, dblEast);
        return valuedirection.ToString() + "," + valuedistance.ToString();
        //return "Hello " + north + Environment.NewLine + "The Current Time is: "
        //    + DateTime.Now.ToString();
    }

    protected void ddlTargetShape_SelectedIndexChanged(object sender, DropDownListEventArgs e)
    {
        RadDropDownList ddlTargetShape = (RadDropDownList)sender;
        if (ddlTargetShape.SelectedValue == "0")
        {
        }
        else if (ddlTargetShape.SelectedValue == "1000")
        {
            lblTargetShapeName.Text = "Circle";
            lblTargetOffset.Visible = true;
            lblTargetOffset.Text = "Diameter of Circle";
            txtXDiameter.Visible = true;
            txtYDiameter.Visible = false;
            lblCorner1.Visible = true;
            lblCorner2.Visible = true;
            lblCorner3.Visible = true;
            lblCorner4.Visible = true;
            txtXCorner1.Visible = true;
            txtYCorner1.Visible = true;
            txtXCorner2.Visible = true;
            txtYCorner2.Visible = true;
            txtXCorner3.Visible = true;
            txtYCorner3.Visible = true;
            txtXCorner4.Visible = true;
            txtYCorner4.Visible = true;
            lblCorner1.Text = "Corner 1";
            lblCorner2.Text = "Corner 2";
            lblCorner3.Text = "Corner 3";
            lblCorner4.Text = "Corner 4";

            lblCorner5.Visible = false;
            lblCorner6.Visible = false;
            lblCorner7.Visible = false;
            lblCorner8.Visible = false;
            txtXCorner5.Visible = false;
            txtYCorner5.Visible = false;
            txtXCorner6.Visible = false;
            txtYCorner6.Visible = false;
            txtXCorner7.Visible = false;
            txtYCorner7.Visible = false;
            txtXCorner8.Visible = false;
            txtYCorner8.Visible = false;
            ddlVertices.SelectedValue = "4";
            ddlVertices.Enabled = false;

        }
        else if (ddlTargetShape.SelectedValue == "1001")
        {
            lblTargetShapeName.Text = "Square";
            lblTargetOffset.Visible = true;
            lblTargetOffset.Text = "Length of Side";
            txtXDiameter.Visible = true;
            txtYDiameter.Visible = false;
            lblCorner1.Visible = true;
            lblCorner2.Visible = true;
            lblCorner3.Visible = true;
            lblCorner4.Visible = true;
            txtXCorner1.Visible = true;
            txtYCorner1.Visible = true;
            txtXCorner2.Visible = true;
            txtYCorner2.Visible = true;
            txtXCorner3.Visible = true;
            txtYCorner3.Visible = true;
            txtXCorner4.Visible = true;
            txtYCorner4.Visible = true;
            lblCorner1.Text = "Corner 1";
            lblCorner2.Text = "Corner 2";
            lblCorner3.Text = "Corner 3";
            lblCorner4.Text = "Corner 4";

            lblCorner5.Visible = false;
            lblCorner6.Visible = false;
            lblCorner7.Visible = false;
            lblCorner8.Visible = false;
            txtXCorner5.Visible = false;
            txtYCorner5.Visible = false;
            txtXCorner6.Visible = false;
            txtYCorner6.Visible = false;
            txtXCorner7.Visible = false;
            txtYCorner7.Visible = false;
            txtXCorner8.Visible = false;
            txtYCorner8.Visible = false;
            ddlVertices.SelectedValue = "4";
            ddlVertices.Enabled = false;
        }
        else if (ddlTargetShape.SelectedValue == "1002")
        {
            lblTargetShapeName.Text = "Rectangle";
            lblTargetOffset.Visible = true;
            lblTargetOffset.Text = "X and Y Lengths";
            txtXDiameter.Visible = true;
            txtYDiameter.Visible = true;
            lblCorner1.Visible = true;
            lblCorner2.Visible = true;
            lblCorner3.Visible = true;
            lblCorner4.Visible = true;
            txtXCorner1.Visible = true;
            txtYCorner1.Visible = true;
            txtXCorner2.Visible = true;
            txtYCorner2.Visible = true;
            txtXCorner3.Visible = true;
            txtYCorner3.Visible = true;
            txtXCorner4.Visible = true;
            txtYCorner4.Visible = true;
            lblCorner1.Text = "Corner 1";
            lblCorner2.Text = "Corner 2";
            lblCorner3.Text = "Corner 3";
            lblCorner4.Text = "Corner 4";
            ddlVertices.Enabled = false;

            lblCorner5.Visible = false;
            lblCorner6.Visible = false;
            lblCorner7.Visible = false;
            lblCorner8.Visible = false;
            txtXCorner5.Visible = false;
            txtYCorner5.Visible = false;
            txtXCorner6.Visible = false;
            txtYCorner6.Visible = false;
            txtXCorner7.Visible = false;
            txtYCorner7.Visible = false;
            txtXCorner8.Visible = false;
            txtYCorner8.Visible = false;
            ddlVertices.SelectedValue = "4";
        }
        else if (ddlTargetShape.SelectedValue == "1003")
        {
            lblTargetShapeName.Text = "Polygon";
            lblTargetOffset.Visible = false;
            txtXDiameter.Visible = false;
            txtYDiameter.Visible = false;
            lblCorner1.Visible = true;
            lblCorner2.Visible = true;
            lblCorner3.Visible = true;
            lblCorner4.Visible = true;
            txtXCorner1.Visible = true;
            txtYCorner1.Visible = true;
            txtXCorner2.Visible = true;
            txtYCorner2.Visible = true;
            txtXCorner3.Visible = true;
            txtYCorner3.Visible = true;
            txtXCorner4.Visible = true;
            txtYCorner4.Visible = true;
            lblCorner1.Text = "Vertex 1";
            lblCorner2.Text = "Vertex 2";
            lblCorner3.Text = "Vertex 3";
            lblCorner4.Text = "Vertex 4";

            ddlVertices.Enabled = true;
        }
        else if (ddlTargetShape.SelectedValue == "1004")
        {
            lblTargetShapeName.Text = "Ellipse";
            lblTargetOffset.Visible = true;
            lblTargetOffset.Text = "X and Y Lengths";
            txtXDiameter.Visible = true;
            txtYDiameter.Visible = true;
            lblCorner1.Visible = true;
            lblCorner2.Visible = true;
            lblCorner3.Visible = true;
            lblCorner4.Visible = true;
            txtXCorner1.Visible = true;
            txtYCorner1.Visible = true;
            txtXCorner2.Visible = true;
            txtYCorner2.Visible = true;
            txtXCorner3.Visible = true;
            txtYCorner3.Visible = true;
            txtXCorner4.Visible = true;
            txtYCorner4.Visible = true;
            lblCorner1.Text = "Corner 1";
            lblCorner2.Text = "Corner 2";
            lblCorner3.Text = "Corner 3";
            lblCorner4.Text = "Corner 4";

            lblCorner5.Visible = false;
            lblCorner6.Visible = false;
            lblCorner7.Visible = false;
            lblCorner8.Visible = false;
            txtXCorner5.Visible = false;
            txtYCorner5.Visible = false;
            txtXCorner6.Visible = false;
            txtYCorner6.Visible = false;
            txtXCorner7.Visible = false;
            txtYCorner7.Visible = false;
            txtXCorner8.Visible = false;
            txtYCorner8.Visible = false;
            ddlVertices.SelectedValue = "4";
            ddlVertices.Enabled = false;
        }
        else if (ddlTargetShape.SelectedValue == "1005")
        {
            lblTargetShapeName.Text = "Point";
            lblTargetOffset.Visible = false;
            txtXDiameter.Visible = false;
            txtYDiameter.Visible = false;
            lblCorner1.Visible = false;
            lblCorner2.Visible = false;
            lblCorner3.Visible = false;
            lblCorner4.Visible = false;
            txtXCorner1.Visible = false;
            txtYCorner1.Visible = false;
            txtXCorner2.Visible = false;
            txtYCorner2.Visible = false;
            txtXCorner3.Visible = false;
            txtYCorner3.Visible = false;
            txtXCorner4.Visible = false;
            txtYCorner4.Visible = false;

            lblCorner5.Visible = false;
            lblCorner6.Visible = false;
            lblCorner7.Visible = false;
            lblCorner8.Visible = false;
            txtXCorner5.Visible = false;
            txtYCorner5.Visible = false;
            txtXCorner6.Visible = false;
            txtYCorner6.Visible = false;
            txtXCorner7.Visible = false;
            txtYCorner7.Visible = false;
            txtXCorner8.Visible = false;
            txtYCorner8.Visible = false;
            ddlVertices.SelectedValue = "4";
            ddlVertices.Enabled = false;
        }
    }

    protected void EnableTargetShapeTextboxes()
    {
        txtTargetXOffset.Enabled = true;
        txtTargetYOffset.Enabled = true;
        txtXDiameter.Enabled = true;
        txtYDiameter.Enabled = true;
        txtXCorner1.Enabled = true;
        txtYCorner1.Enabled = true;
        txtXCorner2.Enabled = true;
        txtYCorner2.Enabled = true;
        txtXCorner3.Enabled = true;
        txtYCorner3.Enabled = true;
        txtXCorner4.Enabled = true;
        txtYCorner4.Enabled = true;
        txtRotation.Enabled = true;
        btnDraw.Enabled = true;
        btnToTarget.Enabled = true;
        btnToWellhead.Enabled = true;
        ddlTargetShape.Enabled = true;
    }

    protected void DisableTargetShapeTextboxes()
    {
        txtTargetXOffset.Enabled = false;
        txtTargetYOffset.Enabled = false;
        txtXDiameter.Enabled = false;
        txtYDiameter.Enabled = false;
        txtXCorner1.Enabled = false;
        txtYCorner1.Enabled = false;
        txtXCorner2.Enabled = false;
        txtYCorner2.Enabled = false;
        txtXCorner3.Enabled = false;
        txtYCorner3.Enabled = false;
        txtXCorner4.Enabled = false;
        txtYCorner4.Enabled = false;

        lblCorner5.Visible = false;
        txtXCorner5.Visible = false;
        txtYCorner5.Visible = false;
        lblCorner6.Visible = false;
        txtXCorner6.Visible = false;
        txtYCorner6.Visible = false;
        lblCorner7.Visible = false;
        txtXCorner7.Visible = false;
        txtYCorner7.Visible = false;
        lblCorner8.Visible = false;
        txtXCorner8.Visible = false;
        txtYCorner8.Visible = false;

        ddlVertices.Enabled = false;
        ddlTargetShape.Enabled = false;

        txtRotation.Enabled = false;
        btnDraw.Enabled = false;
        btnToTarget.Enabled = false;
        btnToWellhead.Enabled = false;

        txtTargetXOffset.Text = "0.00";
        txtTargetYOffset.Text = "0.00";
        txtXDiameter.Text = "0.00";
        txtYDiameter.Text = "0.00";
        txtXCorner1.Text = "0.00";
        txtYCorner1.Text = "0.00";
        txtXCorner2.Text = "0.00";
        txtYCorner2.Text = "0.00";
        txtXCorner3.Text = "0.00";
        txtYCorner3.Text = "0.00";
        txtXCorner4.Text = "0.00";
        txtYCorner4.Text = "0.00";
        txtXCorner5.Text = "0.00";
        txtYCorner5.Text = "0.00";
        txtXCorner6.Text = "0.00";
        txtYCorner6.Text = "0.00";
        txtXCorner7.Text = "0.00";
        txtYCorner7.Text = "0.00";
        txtXCorner8.Text = "0.00";
        txtYCorner8.Text = "0.00";
        txtRotation.Text = "0.00";

    }

    protected void ddlVertices_SelectedIndexChanged(object sender, DropDownListEventArgs e)
    {
        int selectedValue = Int32.Parse(ddlVertices.SelectedValue);

        switch (selectedValue)
        {
            case 3:
                lblCorner4.Visible = false;
                txtXCorner4.Visible = false;
                txtYCorner4.Visible = false;
                lblCorner5.Visible = false;
                txtXCorner5.Visible = false;
                txtYCorner5.Visible = false;
                lblCorner6.Visible = false;
                txtXCorner6.Visible = false;
                txtYCorner6.Visible = false;
                lblCorner7.Visible = false;
                txtXCorner7.Visible = false;
                txtYCorner7.Visible = false;
                lblCorner8.Visible = false;
                txtXCorner8.Visible = false;
                txtYCorner8.Visible = false;
                break;
            case 4:
                lblCorner4.Visible = true;
                txtXCorner4.Visible = true;
                txtYCorner4.Visible = true;
                lblCorner5.Visible = false;
                txtXCorner5.Visible = false;
                txtYCorner5.Visible = false;
                lblCorner6.Visible = false;
                txtXCorner6.Visible = false;
                txtYCorner6.Visible = false;
                lblCorner7.Visible = false;
                txtXCorner7.Visible = false;
                txtYCorner7.Visible = false;
                lblCorner8.Visible = false;
                txtXCorner8.Visible = false;
                txtYCorner8.Visible = false;
                break;
            case 5:
                lblCorner4.Visible = true;
                txtXCorner4.Visible = true;
                txtYCorner4.Visible = true;
                lblCorner5.Visible = true;
                txtXCorner5.Visible = true;
                txtYCorner5.Visible = true;
                lblCorner6.Visible = false;
                txtXCorner6.Visible = false;
                txtYCorner6.Visible = false;
                lblCorner7.Visible = false;
                txtXCorner7.Visible = false;
                txtYCorner7.Visible = false;
                lblCorner8.Visible = false;
                txtXCorner8.Visible = false;
                txtYCorner8.Visible = false;
                break;
            case 6:
                lblCorner4.Visible = true;
                txtXCorner4.Visible = true;
                txtYCorner4.Visible = true;
                lblCorner5.Visible = true;
                txtXCorner5.Visible = true;
                txtYCorner5.Visible = true;
                lblCorner6.Visible = true;
                txtXCorner6.Visible = true;
                txtYCorner6.Visible = true;
                lblCorner7.Visible = false;
                txtXCorner7.Visible = false;
                txtYCorner7.Visible = false;
                lblCorner8.Visible = false;
                txtXCorner8.Visible = false;
                txtYCorner8.Visible = false;
                break;
            case 7:
                lblCorner4.Visible = true;
                txtXCorner4.Visible = true;
                txtYCorner4.Visible = true;
                lblCorner5.Visible = true;
                txtXCorner5.Visible = true;
                txtYCorner5.Visible = true;
                lblCorner6.Visible = true;
                txtXCorner6.Visible = true;
                txtYCorner6.Visible = true;
                lblCorner7.Visible = true;
                txtXCorner7.Visible = true;
                txtYCorner7.Visible = true;
                lblCorner8.Visible = false;
                txtXCorner8.Visible = false;
                txtYCorner8.Visible = false;
                break;
            case 8:
                lblCorner4.Visible = true;
                txtXCorner4.Visible = true;
                txtYCorner4.Visible = true;
                lblCorner5.Visible = true;
                txtXCorner5.Visible = true;
                txtYCorner5.Visible = true;
                lblCorner6.Visible = true;
                txtXCorner6.Visible = true;
                txtYCorner6.Visible = true;
                lblCorner7.Visible = true;
                txtXCorner7.Visible = true;
                txtYCorner7.Visible = true;
                lblCorner8.Visible = true;
                txtXCorner8.Visible = true;
                txtYCorner8.Visible = true;
                break;
        }

    }


    protected void btnDraw_Click(object sender, EventArgs e)
    {
        ScriptManager.RegisterStartupScript(this, this.GetType(), "DrawShapesFromEdit", "DrawShapesEditButton(" + ddlTargetShape.SelectedValue + "," + ddlVertices.SelectedValue + ");", true);
    }
}