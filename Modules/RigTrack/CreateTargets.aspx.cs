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
public partial class Modules_RigTrack_CreateTargets : System.Web.UI.Page
{
    #region Page Behavior
    MDM.Collector col = new MDM.Collector();
    private string gridMessage = null;
    private static List<TargetShapeDetails> Listvalues = new List<TargetShapeDetails>();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Random rnd = new Random();
            int month = rnd.Next(10);
            ddlCurveGroup.DataSource = RigTrack.DatabaseObjects.RigTrackDO.GetAllCurveGroupNames();
            ddlCurveGroup.DataTextField = "CurveGroupName";
            ddlCurveGroup.DataValueField = "ID";
            ddlCurveGroup.DataBind();
            //btnShowCurve.Enabled = true;
            btnAddRow.Visible = false;
            btntopSave.Visible = false;
            btnBottomAddRow.Visible = false;
            btnSave.Visible = false;
            btnClear.Visible = false;
            btntopClear.Visible = false;
            
        }
    }
    public void DisableEntryFields()
    {
        foreach (GridItem item in RadGrid1.MasterTableView.Items)
        {
            CheckBox chkSelect = item.FindControl("checkColumn") as CheckBox;
            RadDropDownList ddlTargetShape = (RadDropDownList)item.FindControl("ddlTargetShape");
            RadTextBox radtxtTargetName = item.FindControl("radtxtTargetName") as RadTextBox;
            RadTextBox radtxtTVD = item.FindControl("radtxtTVD") as RadTextBox;
            RadTextBox radtxtESC = item.FindControl("radtxtESC") as RadTextBox;
            RadTextBox radtxtNSC = item.FindControl("radtxtNSC") as RadTextBox;
            RadTextBox radtxtPolardirection = item.FindControl("radtxtPolardirection") as RadTextBox;
            RadTextBox radtxtPolardistance = item.FindControl("radtxtPolardistance") as RadTextBox;
            RadTextBox INCLAST = item.FindControl("INCLAST") as RadTextBox;
            RadTextBox AZMFromLastTarget = item.FindControl("AZMFromLastTarget") as RadTextBox;
            RadTextBox INCAtTarget = item.FindControl("INCAtTarget") as RadTextBox;
            RadTextBox AZMTAtTarget = item.FindControl("AZMTAtTarget") as RadTextBox;
            RadDropDownList NumberofVerticles = item.FindControl("NumberofVerticles") as RadDropDownList;
            RadTextBox Rotation = item.FindControl("Rotation") as RadTextBox;
            RadTextBox Thickness = item.FindControl("Thickness") as RadTextBox;
            RadDropDownList ddlPattern = (RadDropDownList)item.FindControl("ddlPattern");
            RadTextBox Comments = item.FindControl("Comments") as RadTextBox;
            Button btnShowPattern = (Button)item.FindControl("btnShowPattern");
            ddlTargetShape.Enabled = false;
            radtxtTargetName.Enabled = false;
            radtxtTVD.Enabled = false;
            radtxtESC.Enabled = false;
            radtxtNSC.Enabled = false;
            radtxtPolardirection.Enabled = false;
            radtxtPolardistance.Enabled = false;
            INCLAST.Enabled = false;
            AZMFromLastTarget.Enabled = false;
            AZMFromLastTarget.Enabled = false;
            INCAtTarget.Enabled = false;
            AZMTAtTarget.Enabled = false;
            NumberofVerticles.Enabled = false;
            Rotation.Enabled = false;
            Thickness.Enabled = false;
            ddlPattern.Enabled = false;
            Comments.Enabled = false;
            btnShowPattern.Enabled = false;
        }
    }
    protected void ddlCurveGroup_SelectedIndexChanged(object sender, DropDownListEventArgs e)
    {
        string ddlval = e.Value;
        btnAddRow.Visible = true;
        btntopSave.Visible = true;
        btnBottomAddRow.Visible = true;
        btnSave.Visible = true;
        btnClear.Visible = true;
        btntopClear.Visible = true;
        if (ddlCurveGroup.SelectedText != "-Select-")
        { 
            RadGrid1.Enabled = true;
            btnShowCurve.Enabled = true;
            btnAddRow.Enabled = true;
            btntopSave.Enabled = true;
            btnBottomAddRow.Enabled = true;
            btnSave.Enabled = true;
        }
        
        DataTable DtTargetDetails = RigTrack.DatabaseObjects.RigTrackDO.GetTargetsForCurveGroupID(Convert.ToInt32(ddlval));
        DataTable dtJobDetails = RigTrack.DatabaseObjects.RigTrackDO.GetAllCurveGroupsNotClosed();
        DataRow[] dr = dtJobDetails.Select("ID=" + ddlval + "");
        txtTargetXOffset.Text = "";
        txtTargetYOffset.Text = "";
        txtXDiameter.Text = "";
        txtYDiameter.Text = "";
        txtXCorner1.Text = "";
        txtYCorner1.Text = "";
        txtXCorner2.Text = "";
        txtYCorner2.Text = "";
        txtXCorner3.Text = "";
        txtYCorner3.Text = "";
        txtXCorner4.Text = "";
        txtYCorner4.Text = "";
        lblMessage.Text = "";
        RadGrid1.DataSource = BindGrid(DtTargetDetails);
        RadGrid1.Rebind();
        DisableEntryFields();
    }
    protected void CheckBox1_CheckedChanged(object sender, EventArgs e)
    {
        CheckBox chk = sender as CheckBox;
        GridDataItem item1 = (GridDataItem)chk.NamingContainer;
        if (chk.Checked)
        {
            
            Label lblDisplayID = item1.FindControl("lblDisplayID") as Label;
            lblTargetSelectedID.Text = lblDisplayID.Text;
            DataTable DtTargetDetails = RigTrack.DatabaseObjects.RigTrackDO.GetTargetsForCurveGroupID(0);
            DataRow[] drRow = DtTargetDetails.Select("ID=" + Convert.ToInt32(lblDisplayID.Text) + "");
            if (drRow.Length > 0)
            {
                
                RadDropDownList ddlTargetShape = (RadDropDownList)item1.FindControl("ddlTargetShape");
                hidShapeType.Value = ddlTargetShape.SelectedText;
                lblTargetShapeName.Text= ddlTargetShape.SelectedText;
                txtTargetXOffset.Text = drRow[0]["TargetOffsetXoffset"].ToString();
                txtTargetYOffset.Text = drRow[0]["TargetOffsetYoffset"].ToString();
                txtXDiameter.Text = drRow[0]["DiameterOfCircleXoffset"].ToString();
                txtYDiameter.Text = drRow[0]["DiameterOfCircleYoffset"].ToString();
                txtXCorner1.Text = drRow[0]["Corner1Xofffset"].ToString();
                txtYCorner1.Text = drRow[0]["Corner1Yoffset"].ToString();
                txtXCorner2.Text = drRow[0]["Corner2Xoffset"].ToString();
                txtYCorner2.Text = drRow[0]["Corner2Yoffset"].ToString();
                txtXCorner3.Text = drRow[0]["Corner3Xoffset"].ToString();
                txtYCorner3.Text = drRow[0]["Corner3Yoffset"].ToString();
                txtXCorner4.Text = drRow[0]["Corner4Xoffset"].ToString();
                txtYCorner4.Text = drRow[0]["Corner4Yoffset"].ToString();
                //string script = "function f(){$find(\"" + RadWindow1.ClientID + "\").show(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
                //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, true);
            }
        }
        //GridDataItem dataItem = e.Item as GridDataItem;
        foreach (GridItem item in RadGrid1.MasterTableView.Items)
        {
            //item1.Enabled = false;
            //item1.Cells[3].Enabled = true;
            //item1.Cells[3].BackColor = Color.Red;
            //item1.Cells[4].Enabled = fals;
            CheckBox chkSelect = item.FindControl("checkColumn") as CheckBox;
            RadDropDownList ddlTargetShape = (RadDropDownList)item.FindControl("ddlTargetShape");
            RadTextBox radtxtTargetName = item.FindControl("radtxtTargetName") as RadTextBox;
            RadTextBox radtxtTVD = item.FindControl("radtxtTVD") as RadTextBox;
            RadTextBox radtxtESC = item.FindControl("radtxtESC") as RadTextBox;
            RadTextBox radtxtNSC = item.FindControl("radtxtNSC") as RadTextBox;
            RadTextBox radtxtPolardirection = item.FindControl("radtxtPolardirection") as RadTextBox;
            RadTextBox radtxtPolardistance = item.FindControl("radtxtPolardistance") as RadTextBox;
            RadTextBox INCLAST = item.FindControl("INCLAST") as RadTextBox;
            RadTextBox AZMFromLastTarget = item.FindControl("AZMFromLastTarget") as RadTextBox;
            RadTextBox INCAtTarget = item.FindControl("INCAtTarget") as RadTextBox;
            RadTextBox AZMTAtTarget = item.FindControl("AZMTAtTarget") as RadTextBox;
            RadDropDownList NumberofVerticles = item.FindControl("NumberofVerticles") as RadDropDownList;
            RadTextBox Rotation = item.FindControl("Rotation") as RadTextBox;
            RadTextBox Thickness = item.FindControl("Thickness") as RadTextBox;
            RadDropDownList ddlPattern = (RadDropDownList)item.FindControl("ddlPattern");
            RadTextBox Comments = item.FindControl("Comments") as RadTextBox;
            Button btnShowPattern = (Button)item.FindControl("btnShowPattern");
            //chkSelect.Enabled = true;
            //chkSelect.Checked = true;
            if (!chkSelect.Checked)
            {
                //chkSelect.Checked = false;

                ddlTargetShape.Enabled = false;
                radtxtTargetName.Enabled = false;
                radtxtTVD.Enabled = false;
                radtxtESC.Enabled = false;
                radtxtNSC.Enabled = false;
                radtxtPolardirection.Enabled = false;
                radtxtPolardistance.Enabled = false;
                INCLAST.Enabled = false;
                AZMFromLastTarget.Enabled = false;
                AZMFromLastTarget.Enabled = false;
                INCAtTarget.Enabled = false;
                AZMTAtTarget.Enabled = false;
                NumberofVerticles.Enabled = false;
                Rotation.Enabled = false;
                Thickness.Enabled = false;
                ddlPattern.Enabled = false;
                Comments.Enabled = false;
                btnShowPattern.Enabled = false;
            }
            else
            {
                ddlTargetShape.Enabled = true;
                radtxtTargetName.Enabled = true;
                radtxtTVD.Enabled = true;
                radtxtESC.Enabled = true;
                radtxtNSC.Enabled = true;
                radtxtPolardirection.Enabled = true;
                radtxtPolardistance.Enabled = true;
                INCLAST.Enabled = true;
                AZMFromLastTarget.Enabled = true;
                AZMFromLastTarget.Enabled = true;
                INCAtTarget.Enabled = true;
                AZMTAtTarget.Enabled = true;
                NumberofVerticles.Enabled = true;
                Rotation.Enabled = true;
                Thickness.Enabled = true;
                ddlPattern.Enabled = true;
                Comments.Enabled = true;
                btnShowPattern.Enabled = true;
            }

        }
        
        if (chk.Checked)
        {
            item1.Enabled = true;
            //chk.Checked = false;
        }
        //btnAddRow.Enabled = true;
        //btnBottomAddRow.Enabled = true;

    }
    protected void RadGrid1_ItemDataBound(object sender, GridItemEventArgs e)
    {
        if (e.Item is GridDataItem)
        {
            GridDataItem item = (GridDataItem)e.Item;
            RadDropDownList list = (RadDropDownList)item.FindControl("ddlTargetShape");
            list.OnClientItemSelected = "OnClientItemSelected";
            
            CheckBox checkColumn = item.FindControl("checkColumn") as CheckBox;
            //if (list.SelectedValue != "0")
                //checkColumn.Checked = true;

            //checkColumn.Attributes.Add("onclick", "uncheckOther(this," + item.ItemIndex + ");");
            RadTextBox radtxtTVD = item.FindControl("radtxtTVD") as RadTextBox;
            RadTextBox radtxtNSC = item.FindControl("radtxtNSC") as RadTextBox;

            RadTextBox INCLAST = item.FindControl("INCLAST") as RadTextBox;
            RadTextBox AZMFromLastTarget = item.FindControl("AZMFromLastTarget") as RadTextBox;
            RadTextBox radtxtESC = item.FindControl("radtxtESC") as RadTextBox;
            RadTextBox radtxtPolardirection = item.FindControl("radtxtPolardistance") as RadTextBox;
            RadTextBox radtxtPolardistance = item.FindControl("radtxtPolardirection") as RadTextBox;
            Label lblTargetID = item.FindControl("lblTargetID") as Label;
            checkColumn.Attributes.Add("onclick", "uncheckOther(this," + item.ItemIndex + ","+ lblTargetID.Text + ");");
            radtxtESC.Attributes.Add("onChange", "OnChangeCaliculations('" + radtxtTVD.ClientID + "','" + radtxtNSC.ClientID + "','" + INCLAST.ClientID + "','" + AZMFromLastTarget.ClientID + "','" + radtxtESC.ClientID + "','" + radtxtPolardirection.ClientID + "','" + radtxtPolardistance.ClientID + "');");
            RadDropDownList ddlPattern = (RadDropDownList)item.FindControl("ddlPattern");
            Button btnShowPattern = (Button)item.FindControl("btnShowPattern");
            btnShowPattern.Attributes.Add("onclick", "return OnPatternChanged('"+ list.ClientID+ "'," + item.ItemIndex + ",'" + ddlPattern.ClientID + "');");
            //btnShowPattern.OnClientClicked = "OnPatternChanged";
        }
    }
    #endregion
    #region Buttons
    protected void btnSave_Click(object sender, EventArgs e)
    {
        RigTrack.DatabaseTransferObjects.TargetDTO targetDTO = new RigTrack.DatabaseTransferObjects.TargetDTO();
        
        targetDTO.ID = 0;
        targetDTO.CurveGroupID = Convert.ToInt32(ddlCurveGroup.SelectedValue);
        targetDTO.ReferenceOptionID = 1000;
        targetDTO.CreateDate = DateTime.Now;
        targetDTO.LastModifyDate = DateTime.Now;
        targetDTO.isActive = true;
        if (ddlCurveGroup.SelectedValue != "0")
        {
            foreach (GridItem item in RadGrid1.MasterTableView.Items)
            {
                RigTrack.DatabaseTransferObjects.TargetDTODetails targetDTODetails = new RigTrack.DatabaseTransferObjects.TargetDTODetails();

                RadDropDownList ddlTargetShape = (RadDropDownList)item.FindControl("ddlTargetShape");
                RadTextBox radtxtTargetName = item.FindControl("radtxtTargetName") as RadTextBox;
                RadTextBox radtxtTVD = item.FindControl("radtxtTVD") as RadTextBox;
                RadTextBox radtxtESC = item.FindControl("radtxtESC") as RadTextBox;
                RadTextBox radtxtNSC = item.FindControl("radtxtNSC") as RadTextBox;
                RadTextBox radtxtPolardirection = item.FindControl("radtxtPolardirection") as RadTextBox;
                RadTextBox radtxtPolardistance = item.FindControl("radtxtPolardistance") as RadTextBox;
                RadTextBox INCLAST = item.FindControl("INCLAST") as RadTextBox;
                RadTextBox AZMFromLastTarget = item.FindControl("AZMFromLastTarget") as RadTextBox;
                RadTextBox INCAtTarget = item.FindControl("INCAtTarget") as RadTextBox;
                RadTextBox AZMTAtTarget = item.FindControl("AZMTAtTarget") as RadTextBox;
                RadDropDownList NumberofVerticles = item.FindControl("NumberofVerticles") as RadDropDownList;
                RadTextBox Rotation = item.FindControl("Rotation") as RadTextBox;
                RadTextBox Thickness = item.FindControl("Thickness") as RadTextBox;
                RadDropDownList ddlPattern = (RadDropDownList)item.FindControl("ddlPattern");
                RadTextBox Comments = item.FindControl("Comments") as RadTextBox;
                Label lblTargetID= item.FindControl("lblTargetID") as Label;
                DataTable DtTargetDetailsExist = RigTrack.DatabaseObjects.RigTrackDO.GetTargetsForCurveGroupID(0);
                DataRow[] drRow = DtTargetDetailsExist.Select("ID=" + Convert.ToInt32(lblTargetID.Text) + "");
                if (!DBNull.Value.Equals(lblTargetID))
                {
                    if (drRow.Length == 0)
                    {
                        targetDTO.ID = 0;
                    }
                    else
                    {
                        if (lblTargetID.Text != "")
                            targetDTO.ID = Int32.Parse(lblTargetID.Text);
                        else
                            targetDTO.ID = 0;
                    }
                    
                }
                targetDTODetails.ID = targetDTO.ID;
                targetDTODetails.CurveGroupID = Convert.ToInt32(ddlCurveGroup.SelectedValue);
                targetDTODetails.TargetName = radtxtTargetName.Text;
                targetDTODetails.TargetShapeID = col.IntUtilParse(ddlTargetShape.SelectedValue.ToString());
                targetDTODetails.TVD = col.DecimalUtilParse(radtxtTVD.Text);
                targetDTODetails.NSCoordinate = col.DecimalUtilParse(radtxtNSC.Text);
                targetDTODetails.EWCoordinate = col.DecimalUtilParse(radtxtESC.Text);
                targetDTODetails.PolarDirection = col.DecimalUtilParse(radtxtPolardirection.Text);
                targetDTODetails.PolarDistance = col.DecimalUtilParse(radtxtPolardistance.Text);
                targetDTODetails.INCFromLastTarget = col.DecimalUtilParse(INCLAST.Text);
                targetDTODetails.AZMFromLastTarget = col.DecimalUtilParse(AZMFromLastTarget.Text);
                targetDTODetails.InclinationAtTarget = col.DecimalUtilParse(INCAtTarget.Text);
                targetDTODetails.AzimuthAtTarget = col.DecimalUtilParse(AZMTAtTarget.Text);
                targetDTODetails.NumberVertices = col.DecimalUtilParse(NumberofVerticles.SelectedValue);
                targetDTODetails.Rotation = col.DecimalUtilParse(Rotation.Text);
                targetDTODetails.TargetThickness = col.DecimalUtilParse(Thickness.Text);
                targetDTODetails.DrawingPattern = col.DecimalUtilParse(ddlPattern.SelectedValue);
                targetDTODetails.TargetComment = Comments.Text.ToString();
                targetDTODetails.isActive = true;
                if (ddlTargetShape.SelectedValue != "0")
                {
                    List<TargetShapeDetails> Listvalues_Insert = new List<TargetShapeDetails>();
                    Listvalues_Insert = Listvalues.Where(i => i.targetID == int.Parse(lblTargetID.Text)).ToList();
                    if (Listvalues_Insert.Count > 0)
                    {
                         targetDTODetails.TargetOffsetXoffset = col.DecimalUtilParse(Listvalues_Insert[0].TargetXOffset.ToString());
                         targetDTODetails.TargetOffsetYoffset = col.DecimalUtilParse(Listvalues_Insert[0].TargetYOffset.ToString());
                         targetDTODetails.DiameterOfCircleXoffset = col.DecimalUtilParse(Listvalues_Insert[0].XDiameter.ToString());
                         targetDTODetails.DiameterOfCircleYoffset = col.DecimalUtilParse(Listvalues_Insert[0].YDiameter.ToString());
                         targetDTODetails.Corner1Xofffset = col.DecimalUtilParse(Listvalues_Insert[0].XCorner1.ToString());
                         targetDTODetails.Corner1Yoffset = col.DecimalUtilParse(Listvalues_Insert[0].YCorner1.ToString());
                         targetDTODetails.Corner2Xoffset = col.DecimalUtilParse(Listvalues_Insert[0].XCorner2.ToString());
                         targetDTODetails.Corner2Yoffset = col.DecimalUtilParse(Listvalues_Insert[0].YCorner2.ToString());
                         targetDTODetails.Corner3Xoffset = col.DecimalUtilParse(Listvalues_Insert[0].XCorner3.ToString());
                         targetDTODetails.Corner3Yoffset = col.DecimalUtilParse(Listvalues_Insert[0].YCorner3.ToString());
                         targetDTODetails.Corner4Xoffset = col.DecimalUtilParse(Listvalues_Insert[0].XCorner4.ToString());
                         targetDTODetails.Corner4Yoffset = col.DecimalUtilParse(Listvalues_Insert[0].YCorner4.ToString());
                         targetDTODetails.ReferenceOptionID = 1000;
                         targetDTODetails.isActive = true;
                      }
                 }
                 int insertTargetDetailsID = RigTrack.DatabaseObjects.RigTrackDO.InsertUpdateTargetDetails(targetDTODetails);
            }
            DataTable DtTargetDetails = RigTrack.DatabaseObjects.RigTrackDO.GetTargetsForCurveGroupID(Convert.ToInt32(ddlCurveGroup.SelectedValue));
            lblMessage.Text = "Record Inserted Successfully";
            lblMessage.ForeColor = Color.Green;
            RadGrid1.DataSource = BindGrid(DtTargetDetails);
            RadGrid1.Rebind();
            btnAddRow.Enabled = true;
            btnBottomAddRow.Enabled = true;
            DisableEntryFields();
        }
    }
    protected void btnClear_Click(object sender, EventArgs e)
    {
        clear();
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        clear();
    }
    protected void btnAddRow_Click(object sender, EventArgs e)
    {
        btnBottomAddRow.Enabled = false;
        btnAddRow.Enabled = false;
        RadGrid1.DataSource = AddRow();
        RadGrid1.Rebind();
        DisableEntryFields();
        //int i = RadGrid1.PageCount - 1;
        foreach (GridItem item in RadGrid1.MasterTableView.Items)
        {
            if (item.ItemIndex == RadGrid1.MasterTableView.Items.Count - 1)
            {
                CheckBox chkSelect = item.FindControl("checkColumn") as CheckBox;
                RadDropDownList ddlTargetShape = (RadDropDownList)item.FindControl("ddlTargetShape");
                RadTextBox radtxtTargetName = item.FindControl("radtxtTargetName") as RadTextBox;
                RadTextBox radtxtTVD = item.FindControl("radtxtTVD") as RadTextBox;
                RadTextBox radtxtESC = item.FindControl("radtxtESC") as RadTextBox;
                RadTextBox radtxtNSC = item.FindControl("radtxtNSC") as RadTextBox;
                RadTextBox radtxtPolardirection = item.FindControl("radtxtPolardirection") as RadTextBox;
                RadTextBox radtxtPolardistance = item.FindControl("radtxtPolardistance") as RadTextBox;
                RadTextBox INCLAST = item.FindControl("INCLAST") as RadTextBox;
                RadTextBox AZMFromLastTarget = item.FindControl("AZMFromLastTarget") as RadTextBox;
                RadTextBox INCAtTarget = item.FindControl("INCAtTarget") as RadTextBox;
                RadTextBox AZMTAtTarget = item.FindControl("AZMTAtTarget") as RadTextBox;
                RadDropDownList NumberofVerticles = item.FindControl("NumberofVerticles") as RadDropDownList;
                RadTextBox Rotation = item.FindControl("Rotation") as RadTextBox;
                RadTextBox Thickness = item.FindControl("Thickness") as RadTextBox;
                RadDropDownList ddlPattern = (RadDropDownList)item.FindControl("ddlPattern");
                RadTextBox Comments = item.FindControl("Comments") as RadTextBox;
                Button btnShowPattern = (Button)item.FindControl("btnShowPattern");
                chkSelect.Checked = true;
                ddlTargetShape.Enabled = true;
                radtxtTargetName.Enabled = true;
                radtxtTVD.Enabled = true;
                radtxtESC.Enabled = true;
                radtxtNSC.Enabled = true;
                radtxtPolardirection.Enabled = true;
                radtxtPolardistance.Enabled = true;
                INCLAST.Enabled = true;
                AZMFromLastTarget.Enabled = true;
                AZMFromLastTarget.Enabled = true;
                INCAtTarget.Enabled = true;
                AZMTAtTarget.Enabled = true;
                NumberofVerticles.Enabled = true;
                Rotation.Enabled = true;
                Thickness.Enabled = true;
                ddlPattern.Enabled = true;
                Comments.Enabled = true;
                btnShowPattern.Enabled = true;
            }
        }
    }
    #endregion
    #region Utility Methods
    [WebMethod]
    public static string CaliculatePloarDirection(string north, string east)
    {
        double valuedirection = CurvatureCalculations.FindClosureDirection(Convert.ToDouble(north), Convert.ToDouble(east));
        double valuedistance = CurvatureCalculations.FindClosureDistance(Convert.ToDouble(north), Convert.ToDouble(east));
        return valuedirection.ToString() + "," + valuedistance.ToString();
        //return "Hello " + north + Environment.NewLine + "The Current Time is: "
        //    + DateTime.Now.ToString();
    }
    [WebMethod]
    public static string AddTargetShapeDetails(TargetShapeDetails TargetDetails)
    {
        //TargetShapeDetails Det = new TargetShapeDetails();
        List<TargetShapeDetails> Listvalues_Insert = new List<TargetShapeDetails>();
        Listvalues_Insert = Listvalues.Where(i => i.targetID == int.Parse(TargetDetails.targetID.ToString())).ToList();
        if(Listvalues_Insert.Count==0)
            Listvalues.Add(TargetDetails);

        return "";
    }
    public DataTable BindGrid(DataTable dt)
    {
        DataTable table = new DataTable();

        // Declare DataColumn and DataRow variables.
        DataColumn column;

        // Create new DataColumn, set DataType, ColumnName and add to DataTable.    
        column = new DataColumn();
        column.DataType = System.Type.GetType("System.Int32");
        column.ColumnName = "ID";
        table.Columns.Add(column);

        column = new DataColumn();
        column.DataType = Type.GetType("System.String");
        column.ColumnName = "TargetName";
        table.Columns.Add(column);

        // Create second column.
        column = new DataColumn();
        column.DataType = Type.GetType("System.Int32");
        column.ColumnName = "TargetShapeID";
        table.Columns.Add(column);

        column = new DataColumn();
        column.DataType = Type.GetType("System.Double");
        column.ColumnName = "TVD";
        table.Columns.Add(column);

        column = new DataColumn();
        column.DataType = Type.GetType("System.Double");
        column.ColumnName = "NSCoordinate";
        table.Columns.Add(column);

        column = new DataColumn();
        column.DataType = Type.GetType("System.Double");
        column.ColumnName = "EWCoordinate";
        table.Columns.Add(column);

        column = new DataColumn();
        column.DataType = Type.GetType("System.Double");
        column.ColumnName = "PolarDirection";
        table.Columns.Add(column);

        column = new DataColumn();
        column.DataType = Type.GetType("System.Double");
        column.ColumnName = "PolarDistance";
        table.Columns.Add(column);

        column = new DataColumn();
        column.DataType = Type.GetType("System.Double");
        column.ColumnName = "INCFromLastTarget";
        table.Columns.Add(column);

        column = new DataColumn();
        column.DataType = Type.GetType("System.Double");
        column.ColumnName = "AZMFromLastTarget";
        table.Columns.Add(column);

        column = new DataColumn();
        column.DataType = Type.GetType("System.Double");
        column.ColumnName = "InclinationAtTarget";
        table.Columns.Add(column);

        column = new DataColumn();
        column.DataType = Type.GetType("System.Double");
        column.ColumnName = "AzimuthAtTarget";
        table.Columns.Add(column);

        column = new DataColumn();
        column.DataType = Type.GetType("System.Double");
        column.ColumnName = "NumberVertices";
        table.Columns.Add(column);

        column = new DataColumn();
        column.DataType = Type.GetType("System.Double");
        column.ColumnName = "Rotation";
        table.Columns.Add(column);

        column = new DataColumn();
        column.DataType = Type.GetType("System.Double");
        column.ColumnName = "TargetThickness";
        table.Columns.Add(column);

        column = new DataColumn();
        column.DataType = Type.GetType("System.Double");
        column.ColumnName = "DrawingPattern";
        table.Columns.Add(column);

        column = new DataColumn();
        column.DataType = Type.GetType("System.String");
        column.ColumnName = "TargetComment";
        table.Columns.Add(column);

        if (dt.Rows.Count > 0)
        {
            foreach (DataRow row in dt.Rows)
            {
                try
                {
                    table.ImportRow(row);
                }
                catch (Exception ex)
                {
                    table = null;
                }
            }
        }
        return table;
    }
    private void DisplayMessage(string text)
    {
        RadGrid1.Controls.Add(new LiteralControl(string.Format("<span style='color:red'>{0}</span>", text)));
    }
    private void SetMessage(string message)
    {
        gridMessage = message;
    }
    public void clear()
    {
        ddlCurveGroup.SelectedIndex = 0;
        txtTargetXOffset.Text = "";
        txtTargetYOffset.Text = "";
        txtXDiameter.Text = "";
        txtYDiameter.Text = "";
        txtXCorner1.Text = "";
        txtYCorner1.Text = "";
        txtXCorner2.Text = "";
        txtYCorner2.Text = "";
        txtXCorner3.Text = "";
        txtYCorner3.Text = "";
        txtXCorner4.Text = "";
        txtYCorner4.Text = "";

        RadGrid1.Enabled = false;
        btnShowCurve.Enabled = false;
        btnAddRow.Enabled = false;
        btntopSave.Enabled = false;
        btnBottomAddRow.Enabled = false;
        btnSave.Enabled = false;
        RadGrid1.DataSource = new string[] { };
        RadGrid1.DataBind();
        lblMessage.Text = "";
    }
    public void clearonInsert()
    {
        //radTargetName.Text = "";
        ddlCurveGroup.SelectedValue = "0";
        txtTargetXOffset.Text = "";
        txtTargetYOffset.Text = "";
        txtXDiameter.Text = "";
        txtYDiameter.Text = "";
        txtXCorner1.Text = "";
        txtYCorner1.Text = "";
        txtXCorner2.Text = "";
        txtYCorner2.Text = "";
        txtXCorner3.Text = "";
        txtYCorner3.Text = "";
        txtXCorner4.Text = "";
        txtYCorner4.Text = "";
        //RadGrid1.Enabled = true;
        //RadGrid1.DataSource = new string[] { };
        //RadGrid1.DataBind();
        //DataTable DtTargetDetails_Empty = RigTrack.DatabaseObjects.RigTrackDO.GetTargetsForCurveGroupID(0);
        //RadGrid1.DataSource = BindGrid(DtTargetDetails_Empty);
        //RadGrid1.DataBind();
        RadGrid1.DataSource = new string[] { };
        RadGrid1.DataBind();
        btnAddRow.Visible = false;
        btntopSave.Visible = false;
        btnBottomAddRow.Visible = false;
        btnSave.Visible = false;
        btnClear.Visible = false;
        btntopClear.Visible = false;
        //lblMessage.Text = "";
    }
    private DataTable AddRow()
    {
        DataTable DtTargetDetails = RigTrack.DatabaseObjects.RigTrackDO.GetTargetsForCurveGroupID(Convert.ToInt32(ddlCurveGroup.SelectedValue));
        // method to create row 
        DataTable DtTargetDetails_Empty = RigTrack.DatabaseObjects.RigTrackDO.GetTargetsForCurveGroupID(0);
        DataTable dtCount = DtTargetDetails_Empty.AsEnumerable().Take(1).CopyToDataTable();
        int max = Convert.ToInt32(DtTargetDetails_Empty.AsEnumerable().Max(r => r["ID"]));

        DataRow dr = DtTargetDetails.NewRow();
        dr["ID"] = max+1;
        dr["TargetName"] = "";
        dr["TargetShapeID"] = 0;
        dr["TVD"] = 0.00;
        dr["NSCoordinate"] = 0.00;
        dr["EWCoordinate"] = 0.00;
        dr["PolarDirection"] = 0.00;
        dr["PolarDistance"] = 0.00;
        dr["INCFromLastTarget"] = 0.00;
        dr["AZMFromLastTarget"] = 0.00;
        dr["InclinationAtTarget"] = 0.00;
        dr["AzimuthAtTarget"] = 0.00;
        dr["NumberVertices"] = 0.00;
        dr["Rotation"] = 0.00;
        dr["TargetThickness"] = 0.00;
        dr["DrawingPattern"] = 0.00;
        dr["TargetComment"] = "";
        DtTargetDetails.Rows.Add(dr);
        return DtTargetDetails;
    }
    #endregion
}