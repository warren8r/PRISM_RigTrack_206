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
using System.Configuration;
using System.Collections;
using System.Web.Security;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Diagnostics;
using System.Reflection;

public partial class Modules_RigTrack_CreateCurves : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string color = ColorTranslator.ToHtml(Color.FromArgb(RadColorPicker1.SelectedColor.ToArgb()));
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

            ddlCurveType.DataSource = RigTrack.DatabaseObjects.RigTrackDO.GetAllCurveTypes();
            ddlCurveType.DataTextField = "Name";
            ddlCurveType.DataValueField = "ID";
            ddlCurveType.DataBind();

            ddlCurveGroup.Enabled = false;
        }
    }

    protected void ddlCurveGroup_SelectedIndexChanged(object sender, Telerik.Web.UI.DropDownListEventArgs e)
    {
        if (ddlCurveGroup.SelectedValue != "0")
        {
            var firstItem = ddlTarget.Items[0];
            ddlTarget.Items.Clear();
            ddlTarget.Items.Add(firstItem);
            ddlTarget.DataSource = RigTrack.DatabaseObjects.RigTrackDO.GetTargetsForCurveGroup(Convert.ToInt32(ddlCurveGroup.SelectedValue));
            ddlTarget.DataTextField = "TargetName";
            ddlTarget.DataValueField = "TargetID";
            ddlTarget.DataBind();
            ddlTarget.Enabled = true;
        }
        else
        {
            ddlTarget.SelectedValue = "0";
            ddlTarget.Enabled = false;
            btnCreate.Enabled = false;
        }
    }

    protected void ddlTarget_SelectedIndexChanged(object sender, DropDownListEventArgs e)
    {
        if (ddlTarget.SelectedValue != "0")
        {
            btnCreate.Enabled = true;
            ViewState["CurveGroupID"] = Convert.ToInt32(ddlCurveGroup.SelectedValue);
            ViewState["TargetID"] = Convert.ToInt32(ddlTarget.SelectedValue);
            ViewState["CurveGroupName"] = ddlCurveGroup.SelectedText;
            ViewState["TargetName"] = ddlTarget.SelectedText;
            DataTable dt = RigTrack.DatabaseObjects.RigTrackDO.GetCurvesForTarget(Convert.ToInt32(ddlTarget.SelectedValue));
            RadGridCurves.DataSource = dt;
            RadGridCurves.DataBind();
        }
        else
        {
            btnCreate.Enabled = false;
        }
    }

    protected void RadGridCurves_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
    {
        if (ViewState["TargetID"] != null)
        {
            DataTable dt = RigTrack.DatabaseObjects.RigTrackDO.GetCurvesForTarget(Convert.ToInt32(ViewState["TargetID"].ToString()));
            RadGridCurves.DataSource = dt;
        }
        else
        {
            DataTable dt = new DataTable();
            RadGridCurves.DataSource = dt;
        }
    }
    protected void RadGridCurves_UpdateCommand(object sender, GridCommandEventArgs e)
    {
        RigTrack.DatabaseTransferObjects.CurveDTO curveDTO = new RigTrack.DatabaseTransferObjects.CurveDTO();

        if (e.Item is GridEditableItem && e.Item.IsInEditMode)
        {
            GridEditableItem item = e.Item as GridEditableItem;

            int curveID, curveGroupID, number, curveTypeID;
            string curveName;
            double northOffset, eastOffset, VSDirection, RKBElevation;

            RadColorPicker gridColorPicker = item.FindControl("gridColorPicker") as RadColorPicker;
            Int32.TryParse((item["ID"].Controls[0] as TextBox).Text, out curveID);
            Int32.TryParse((item["CurveGroupID"].Controls[0] as TextBox).Text, out curveGroupID);
            Int32.TryParse((item["Number"].Controls[0] as TextBox).Text, out number);
            RadDropDownList ddl = item.FindControl("ddlCurveType") as RadDropDownList;
            Int32.TryParse(ddl.SelectedValue, out curveTypeID);
            RadDropDownList ddl2 = item.FindControl("ddlTargetName") as RadDropDownList;
            ViewState["TargetID"] = ddl2.SelectedValue;
            curveName = (item["CurveName"].Controls[0] as TextBox).Text;
            if (double.TryParse((item["NorthOffset"].Controls[0] as TextBox).Text, out northOffset) && double.TryParse((item["EastOffset"].Controls[0] as TextBox).Text, out eastOffset)
                && double.TryParse((item["VSDirection"].Controls[0] as TextBox).Text, out VSDirection) && double.TryParse((item["RKBElevation"].Controls[0] as TextBox).Text, out RKBElevation))
            {
                curveDTO.ID = curveID;
                curveDTO.CurveGroupID = curveGroupID;
                //curveDTO.TargetID = Convert.ToInt32(ViewState["TargetID"].ToString());
                curveDTO.TargetID = Convert.ToInt32(ddl2.SelectedValue);
                curveDTO.Number = number;
                curveDTO.CurveTypeID = curveTypeID;
                curveDTO.Name = curveName;
                curveDTO.NorthOffset = northOffset;
                curveDTO.EastOffset = eastOffset;
                curveDTO.VSDirection = VSDirection;
                curveDTO.RKBElevation = RKBElevation;
                curveDTO.hexColor = ColorTranslator.ToHtml(Color.FromArgb(gridColorPicker.SelectedColor.ToArgb()));
                curveDTO.isActive = true;

                ViewState["VSDirection"] = VSDirection;

                #region Updating Surveys on Curve Info Change 
                int count = 0;
                DataTable surveys = RigTrack.DatabaseObjects.RigTrackDO.GetSurveysForCurve(curveID);
                string methodOfCal = RigTrack.DatabaseObjects.RigTrackDO.GetCurveGroupMethodOfCalculation(curveGroupID);
                methodOfCal = methodOfCal[0].ToString();
                SurveyBuilder previousSurvey = null;
                if (surveys.Rows.Count > 0)
                {
                    foreach (DataRow row in surveys.Rows)
                    {
                        RigTrack.DatabaseTransferObjects.SurveyDTO surveyDTO;
                        if (Int32.Parse(row["RowNumber"].ToString()) == 0)
                        {
                            //This is Tie in, add survey Info to it. 
                            surveyDTO = new RigTrack.DatabaseTransferObjects.SurveyDTO();
                            double subsea = double.Parse(row["TieInSubseaTVD"].ToString());
                            double ns = double.Parse(row["TieInNS"].ToString());
                            double ew = double.Parse(row["TieInEW"].ToString());
                            double vs = double.Parse(row["TieInVerticalSection"].ToString());
                            SurveyBuilder tieIn = new SurveyBuilder();
                            surveyDTO.MD = double.Parse(row["MD"].ToString());
                            tieIn.Depth = surveyDTO.MD;
                            surveyDTO.INC = double.Parse(row["Inclination"].ToString());
                            tieIn.Inclination = surveyDTO.INC;
                            surveyDTO.Azimuth = double.Parse(row["Azimuth"].ToString());
                            tieIn.Azimuth = surveyDTO.Azimuth;
                            surveyDTO.TVD = double.Parse(row["TVD"].ToString());
                            tieIn.TVD = surveyDTO.TVD;
                            surveyDTO.SubseaTVD = subsea - RKBElevation;
                            tieIn.SubseasTVD = surveyDTO.SubseaTVD;
                            surveyDTO.NS = ns + northOffset;
                            tieIn.North = surveyDTO.NS;
                            surveyDTO.EW = ew + eastOffset;
                            tieIn.East = surveyDTO.EW;
                            surveyDTO.VerticalSection = vs + VSDirection;
                            tieIn.VerticalSection = surveyDTO.VerticalSection;
                            surveyDTO.ID = Int32.Parse(row["ID"].ToString());
                            surveyDTO.Name = row["Name"].ToString();
                            surveyDTO.SurveyComment = row["SurveyComment"].ToString();
                            surveyDTO.isActive = true;
                            surveyDTO.RowNumber = count;
                            surveyDTO.CurveGroupID = curveGroupID;
                            surveyDTO.CurveID = curveID;
                            RigTrack.DatabaseObjects.RigTrackDO.InsertUpdateSurvey(surveyDTO);
                            previousSurvey = tieIn;
                            count++;
                        }
                        else
                        {
                            //update other surveys. 
                            surveyDTO = new RigTrack.DatabaseTransferObjects.SurveyDTO();
                            SurveyBuilder currentSurvey = new SurveyBuilder();
                            currentSurvey.Depth = double.Parse(row["MD"].ToString());
                            currentSurvey.Inclination = double.Parse(row["Inclination"].ToString());
                            currentSurvey.Azimuth = double.Parse(row["Azimuth"].ToString());

                            switch (methodOfCal)
                            {
                                case "M":
                                    surveyDTO.MD = currentSurvey.Depth;
                                    surveyDTO.INC = currentSurvey.Inclination;
                                    surveyDTO.Azimuth = currentSurvey.Azimuth;
                                    currentSurvey.dogLegServerity = CurvatureCalculations.DogLegServerity(previousSurvey, currentSurvey);
                                    surveyDTO.DLS = currentSurvey.dogLegServerity;
                                    currentSurvey.dogLegAngle = CurvatureCalculations.DogLegAngle(previousSurvey, currentSurvey);
                                    surveyDTO.DLA = currentSurvey.dogLegAngle;
                                    currentSurvey.North = CurvatureCalculations.MinCurve_North(previousSurvey, currentSurvey);
                                    surveyDTO.NS = currentSurvey.North;
                                    currentSurvey.East = CurvatureCalculations.MinCurve_East(previousSurvey, currentSurvey);
                                    surveyDTO.EW = currentSurvey.East;
                                    currentSurvey.TVD = CurvatureCalculations.MinCurve_TVD(previousSurvey, currentSurvey);
                                    surveyDTO.TVD = currentSurvey.TVD;
                                    currentSurvey.SubseasTVD = currentSurvey.TVD - RKBElevation;
                                    surveyDTO.SubseaTVD = currentSurvey.SubseasTVD;
                                    currentSurvey.CL = CurvatureCalculations.FindCL(previousSurvey, currentSurvey);
                                    surveyDTO.CL = currentSurvey.CL;
                                    currentSurvey.ClosureDistance = CurvatureCalculations.FindClosureDistance(currentSurvey.North, currentSurvey.East);
                                    surveyDTO.ClosureDistance = currentSurvey.ClosureDistance;
                                    currentSurvey.ClosureDirection = CurvatureCalculations.FindClosureDirection(currentSurvey.North, currentSurvey.East);
                                    surveyDTO.ClosureDirection = currentSurvey.ClosureDirection;
                                    currentSurvey.VerticalSection = CurvatureCalculations.FindVerticalSection(currentSurvey, double.Parse(ViewState["VSDirection"].ToString()));
                                    surveyDTO.VerticalSection = currentSurvey.VerticalSection;
                                    currentSurvey.BR = CurvatureCalculations.FindBR(previousSurvey, currentSurvey);
                                    surveyDTO.BR = currentSurvey.BR;
                                    currentSurvey.WR = CurvatureCalculations.FindWR(previousSurvey, currentSurvey);
                                    surveyDTO.WR = currentSurvey.WR;
                                    currentSurvey.TFO = 0.00;
                                    surveyDTO.TFO = currentSurvey.TFO;
                                    surveyDTO.ID = Int32.Parse(row["ID"].ToString());
                                    surveyDTO.Name = row["Name"].ToString();
                                    surveyDTO.SurveyComment = row["SurveyComment"].ToString();
                                    surveyDTO.isActive = true;
                                    surveyDTO.RowNumber = count;
                                    surveyDTO.CurveGroupID = curveGroupID;
                                    surveyDTO.CurveID = curveID;
                                    RigTrack.DatabaseObjects.RigTrackDO.InsertUpdateSurvey(surveyDTO);
                                    previousSurvey = currentSurvey;
                                    count++;
                                    break;
                                case "A":
                                    surveyDTO.MD = currentSurvey.Depth;
                                    surveyDTO.INC = currentSurvey.Inclination;
                                    surveyDTO.Azimuth = currentSurvey.Azimuth;
                                    currentSurvey.dogLegServerity = CurvatureCalculations.DogLegServerity(previousSurvey, currentSurvey);
                                    surveyDTO.DLS = currentSurvey.dogLegServerity;
                                    currentSurvey.dogLegAngle = CurvatureCalculations.DogLegAngle(previousSurvey, currentSurvey);
                                    surveyDTO.DLA = currentSurvey.dogLegAngle;
                                    currentSurvey.North = CurvatureCalculations.AngleAvg_North(previousSurvey, currentSurvey);
                                    surveyDTO.NS = currentSurvey.North;
                                    currentSurvey.East = CurvatureCalculations.AngleAvg_East(previousSurvey, currentSurvey);
                                    surveyDTO.EW = currentSurvey.East;
                                    currentSurvey.TVD = CurvatureCalculations.AngleAvg_TVD(previousSurvey, currentSurvey);
                                    surveyDTO.TVD = currentSurvey.TVD;
                                    currentSurvey.SubseasTVD = currentSurvey.TVD -RKBElevation;
                                    surveyDTO.SubseaTVD = currentSurvey.SubseasTVD;
                                    currentSurvey.CL = CurvatureCalculations.FindCL(previousSurvey, currentSurvey);
                                    surveyDTO.CL = currentSurvey.CL;
                                    currentSurvey.ClosureDistance = CurvatureCalculations.FindClosureDistance(currentSurvey.North, currentSurvey.East);
                                    surveyDTO.ClosureDistance = currentSurvey.ClosureDistance;
                                    currentSurvey.ClosureDirection = CurvatureCalculations.FindClosureDirection(currentSurvey.North, currentSurvey.East);
                                    surveyDTO.ClosureDirection = currentSurvey.ClosureDirection;
                                    currentSurvey.VerticalSection = CurvatureCalculations.FindVerticalSection(currentSurvey, double.Parse(ViewState["VSDirection"].ToString()));
                                    surveyDTO.VerticalSection = currentSurvey.VerticalSection;
                                    currentSurvey.BR = CurvatureCalculations.FindBR(previousSurvey, currentSurvey);
                                    surveyDTO.BR = currentSurvey.BR;
                                    currentSurvey.WR = CurvatureCalculations.FindWR(previousSurvey, currentSurvey);
                                    surveyDTO.WR = currentSurvey.WR;
                                    currentSurvey.TFO = 0.00;
                                    surveyDTO.TFO = currentSurvey.TFO;
                                    surveyDTO.ID = Int32.Parse(row["ID"].ToString());
                                    surveyDTO.Name = row["Name"].ToString();
                                    surveyDTO.SurveyComment = row["SurveyComment"].ToString();
                                    surveyDTO.isActive = true;
                                    surveyDTO.RowNumber = count;
                                    surveyDTO.CurveGroupID = curveGroupID;
                                    surveyDTO.CurveID = curveID;
                                    RigTrack.DatabaseObjects.RigTrackDO.InsertUpdateSurvey(surveyDTO);
                                    previousSurvey = currentSurvey;
                                    count++;
                                    break;
                                case "R":
                                    surveyDTO.MD = currentSurvey.Depth;
                                    surveyDTO.INC = currentSurvey.Inclination;
                                    surveyDTO.Azimuth = currentSurvey.Azimuth;
                                    currentSurvey.dogLegServerity = CurvatureCalculations.DogLegServerity(previousSurvey, currentSurvey);
                                    surveyDTO.DLS = currentSurvey.dogLegServerity;
                                    currentSurvey.dogLegAngle = CurvatureCalculations.DogLegAngle(previousSurvey, currentSurvey);
                                    surveyDTO.DLA = currentSurvey.dogLegAngle;
                                    currentSurvey.North = CurvatureCalculations.Radius_North(previousSurvey, currentSurvey);
                                    surveyDTO.NS = currentSurvey.North;
                                    currentSurvey.East = CurvatureCalculations.Radius_East(previousSurvey, currentSurvey);
                                    surveyDTO.EW = currentSurvey.East;
                                    currentSurvey.TVD = CurvatureCalculations.RadiusTVD(previousSurvey, currentSurvey);
                                    surveyDTO.TVD = currentSurvey.TVD;
                                    currentSurvey.SubseasTVD = currentSurvey.TVD -RKBElevation;
                                    surveyDTO.SubseaTVD = currentSurvey.SubseasTVD;
                                    currentSurvey.CL = CurvatureCalculations.FindCL(previousSurvey, currentSurvey);
                                    surveyDTO.CL = currentSurvey.CL;
                                    currentSurvey.ClosureDistance = CurvatureCalculations.FindClosureDistance(currentSurvey.North, currentSurvey.East);
                                    surveyDTO.ClosureDistance = currentSurvey.ClosureDistance;
                                    currentSurvey.ClosureDirection = CurvatureCalculations.FindClosureDirection(currentSurvey.North, currentSurvey.East);
                                    surveyDTO.ClosureDirection = currentSurvey.ClosureDirection;
                                    currentSurvey.VerticalSection = CurvatureCalculations.FindVerticalSection(currentSurvey, double.Parse(ViewState["VSDirection"].ToString()));
                                    surveyDTO.VerticalSection = currentSurvey.VerticalSection;
                                    currentSurvey.BR = CurvatureCalculations.FindBR(previousSurvey, currentSurvey);
                                    surveyDTO.BR = currentSurvey.BR;
                                    currentSurvey.WR = CurvatureCalculations.FindWR(previousSurvey, currentSurvey);
                                    surveyDTO.WR = currentSurvey.WR;
                                    currentSurvey.TFO = 0.00;
                                    surveyDTO.TFO = currentSurvey.TFO;
                                    surveyDTO.ID = Int32.Parse(row["ID"].ToString());
                                    surveyDTO.Name = row["Name"].ToString();
                                    surveyDTO.SurveyComment = row["SurveyComment"].ToString();
                                    surveyDTO.isActive = true;
                                    surveyDTO.RowNumber = count;
                                    surveyDTO.CurveGroupID = curveGroupID;
                                    surveyDTO.CurveID = curveID;
                                    RigTrack.DatabaseObjects.RigTrackDO.InsertUpdateSurvey(surveyDTO);
                                    previousSurvey = currentSurvey;
                                    count++;
                                    break;
                                case "T":
                                    surveyDTO.MD = currentSurvey.Depth;
                                    surveyDTO.INC = currentSurvey.Inclination;
                                    surveyDTO.Azimuth = currentSurvey.Azimuth;
                                    currentSurvey.dogLegServerity = CurvatureCalculations.DogLegServerity(previousSurvey, currentSurvey);
                                    surveyDTO.DLS = currentSurvey.dogLegServerity;
                                    currentSurvey.dogLegAngle = CurvatureCalculations.DogLegAngle(previousSurvey, currentSurvey);
                                    surveyDTO.DLA = currentSurvey.dogLegAngle;
                                    currentSurvey.North = CurvatureCalculations.Tangential_North(previousSurvey, currentSurvey);
                                    surveyDTO.NS = currentSurvey.North;
                                    currentSurvey.East = CurvatureCalculations.Tangential_East(previousSurvey, currentSurvey);
                                    surveyDTO.EW = currentSurvey.East;
                                    currentSurvey.TVD = CurvatureCalculations.Tangential_TVD(previousSurvey, currentSurvey);
                                    surveyDTO.TVD = currentSurvey.TVD;
                                    currentSurvey.SubseasTVD = currentSurvey.TVD -RKBElevation;
                                    surveyDTO.SubseaTVD = currentSurvey.SubseasTVD;
                                    currentSurvey.CL = CurvatureCalculations.FindCL(previousSurvey, currentSurvey);
                                    surveyDTO.CL = currentSurvey.CL;
                                    currentSurvey.ClosureDistance = CurvatureCalculations.FindClosureDistance(currentSurvey.North, currentSurvey.East);
                                    surveyDTO.ClosureDistance = currentSurvey.ClosureDistance;
                                    currentSurvey.ClosureDirection = CurvatureCalculations.FindClosureDirection(currentSurvey.North, currentSurvey.East);
                                    surveyDTO.ClosureDirection = currentSurvey.ClosureDirection;
                                    currentSurvey.VerticalSection = CurvatureCalculations.FindVerticalSection(currentSurvey, double.Parse(ViewState["VSDirection"].ToString()));
                                    surveyDTO.VerticalSection = currentSurvey.VerticalSection;
                                    currentSurvey.BR = CurvatureCalculations.FindBR(previousSurvey, currentSurvey);
                                    surveyDTO.BR = currentSurvey.BR;
                                    currentSurvey.WR = CurvatureCalculations.FindWR(previousSurvey, currentSurvey);
                                    surveyDTO.WR = currentSurvey.WR;
                                    currentSurvey.TFO = 0.00;
                                    surveyDTO.TFO = currentSurvey.TFO;
                                    surveyDTO.ID = Int32.Parse(row["ID"].ToString());
                                    surveyDTO.Name = row["Name"].ToString();
                                    surveyDTO.SurveyComment = row["SurveyComment"].ToString();
                                    surveyDTO.isActive = true;
                                    surveyDTO.RowNumber = count;
                                    surveyDTO.CurveGroupID = curveGroupID;
                                    surveyDTO.CurveID = curveID;
                                    RigTrack.DatabaseObjects.RigTrackDO.InsertUpdateSurvey(surveyDTO);
                                    previousSurvey = currentSurvey;
                                    count++;
                                    break;
                                case "B":
                                    surveyDTO.MD = currentSurvey.Depth;
                                    surveyDTO.INC = currentSurvey.Inclination;
                                    surveyDTO.Azimuth = currentSurvey.Azimuth;
                                    currentSurvey.dogLegServerity = CurvatureCalculations.DogLegServerity(previousSurvey, currentSurvey);
                                    surveyDTO.DLS = currentSurvey.dogLegServerity;
                                    currentSurvey.dogLegAngle = CurvatureCalculations.DogLegAngle(previousSurvey, currentSurvey);
                                    surveyDTO.DLA = currentSurvey.dogLegAngle;
                                    currentSurvey.North = CurvatureCalculations.BalancedTangential_North(previousSurvey, currentSurvey);
                                    surveyDTO.NS = currentSurvey.North;
                                    currentSurvey.East = CurvatureCalculations.BalancedTangential_East(previousSurvey, currentSurvey);
                                    surveyDTO.EW = currentSurvey.East;
                                    currentSurvey.TVD = CurvatureCalculations.BalancedTangential_TVD(previousSurvey, currentSurvey);
                                    surveyDTO.TVD = currentSurvey.TVD;
                                    currentSurvey.SubseasTVD = currentSurvey.TVD -RKBElevation;
                                    surveyDTO.SubseaTVD = currentSurvey.SubseasTVD;
                                    currentSurvey.CL = CurvatureCalculations.FindCL(previousSurvey, currentSurvey);
                                    surveyDTO.CL = currentSurvey.CL;
                                    currentSurvey.ClosureDistance = CurvatureCalculations.FindClosureDistance(currentSurvey.North, currentSurvey.East);
                                    surveyDTO.ClosureDistance = currentSurvey.ClosureDistance;
                                    currentSurvey.ClosureDirection = CurvatureCalculations.FindClosureDirection(currentSurvey.North, currentSurvey.East);
                                    surveyDTO.ClosureDirection = currentSurvey.ClosureDirection;
                                    currentSurvey.VerticalSection = CurvatureCalculations.FindVerticalSection(currentSurvey, double.Parse(ViewState["VSDirection"].ToString()));
                                    surveyDTO.VerticalSection = currentSurvey.VerticalSection;
                                    currentSurvey.BR = CurvatureCalculations.FindBR(previousSurvey, currentSurvey);
                                    surveyDTO.BR = currentSurvey.BR;
                                    currentSurvey.WR = CurvatureCalculations.FindWR(previousSurvey, currentSurvey);
                                    surveyDTO.WR = currentSurvey.WR;
                                    currentSurvey.TFO = 0.00;
                                    surveyDTO.TFO = currentSurvey.TFO;
                                    surveyDTO.ID = Int32.Parse(row["ID"].ToString());
                                    surveyDTO.Name = row["Name"].ToString();
                                    surveyDTO.SurveyComment = row["SurveyComment"].ToString();
                                    surveyDTO.isActive = true;
                                    surveyDTO.RowNumber = count;
                                    surveyDTO.CurveGroupID = curveGroupID;
                                    surveyDTO.CurveID = curveID;
                                    RigTrack.DatabaseObjects.RigTrackDO.InsertUpdateSurvey(surveyDTO);
                                    previousSurvey = currentSurvey;
                                    count++;
                                    break;
                            }
                            
                        }
                    }
                }

                #endregion




                RigTrack.DatabaseObjects.RigTrackDO.InsertUpdateCurve(curveDTO);
                ddlTarget.SelectedValue = ViewState["TargetID"].ToString();
                DataTable dt = RigTrack.DatabaseObjects.RigTrackDO.GetCurvesForTarget(Convert.ToInt32(ViewState["TargetID"].ToString()));
                RadGridCurves.DataSource = dt;
                RadGridCurves.DataBind();
            }
            else
            {
                string script = "ErrorDialog(\" Please enter a valid number \");";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "NoRecords", script, true);
            }

            
        }
    }
    protected void RadGridCurves_ItemDataBound(object sender, GridItemEventArgs e)
    {
        if (e.Item is GridEditableItem && e.Item.IsInEditMode)
        {
            GridEditableItem item = e.Item as GridEditableItem;
            RadDropDownList ddl = item.FindControl("ddlCurveType") as RadDropDownList;
            DataTable dt = RigTrack.DatabaseObjects.RigTrackDO.GetAllCurveTypes();
            DataView dv = dt.DefaultView;
            dv.Sort = "ID desc";
            DataTable sortedDT = dv.ToTable();
            ddl.DataSource = sortedDT;
            ddl.DataTextField = "Name";
            ddl.DataValueField = "ID";
            ddl.DataBind();
            ddl.SelectedText = (item["CurveTypeName"].Controls[0] as TextBox).Text;

            RadDropDownList ddlTarget = item.FindControl("ddlTargetName") as RadDropDownList;
            DataTable dtTarget = RigTrack.DatabaseObjects.RigTrackDO.GetTargetsForCurveGroup(Convert.ToInt32(ViewState["CurveGroupID"].ToString()));
            ddlTarget.DataSource = dtTarget;
            ddlTarget.DataTextField = "TargetName";
            ddlTarget.DataValueField = "TargetID";
            ddlTarget.DataBind();
            ddlTarget.SelectedText = (item["TargetName"].Controls[0] as TextBox).Text;

            RadColorPicker gridColorPicker = item.FindControl("gridColorPicker") as RadColorPicker;
            gridColorPicker.SelectedColor = Color.FromName((item["Color"].Controls[0] as TextBox).Text);
        }

        if (e.Item is GridDataItem && !e.Item.IsInEditMode)
        {
            GridDataItem item = (GridDataItem)e.Item;

            Label lbl = (Label)item.FindControl("lblCurveType");

            if (lbl.Text == "-Select-")
            {
                lbl.Text = "";
            }

            Label lblColor = (Label)item.FindControl("lblColor");
            RadColorPicker gridColorPickerDisabled = (RadColorPicker)item.FindControl("gridColorPickerDisabled");
            gridColorPickerDisabled.SelectedColor = ColorTranslator.FromHtml(lblColor.Text);
        }
    }

    protected void btnCreate_Click(object sender, EventArgs e)
    {
        RigTrack.DatabaseTransferObjects.CurveDTO curveDTO = new RigTrack.DatabaseTransferObjects.CurveDTO();

        curveDTO.ID = 0;
        curveDTO.CurveGroupID = Convert.ToInt32(ViewState["CurveGroupID"].ToString());
        curveDTO.TargetID = Convert.ToInt32(ViewState["TargetID"].ToString());
        curveDTO.Number = RadGridCurves.Items.Count;
        curveDTO.Name = txtCurveName.Text;
        curveDTO.CurveTypeID = Convert.ToInt32(ddlCurveType.SelectedValue);
        curveDTO.NorthOffset = Convert.ToDouble(txtNorthOffset.Text);
        curveDTO.EastOffset = Convert.ToDouble(txtEastOffset.Text);
        curveDTO.VSDirection = Convert.ToDouble(txtVSDirection.Text);
        curveDTO.RKBElevation = Convert.ToDouble(txtRKBElevation.Text);
        curveDTO.hexColor = ColorTranslator.ToHtml(Color.FromArgb(RadColorPicker1.SelectedColor.ToArgb()));

        RigTrack.DatabaseObjects.RigTrackDO.InsertUpdateCurve(curveDTO);

        DataTable dt = RigTrack.DatabaseObjects.RigTrackDO.GetCurvesForTarget(Convert.ToInt32(ViewState["TargetID"].ToString()));
        RadGridCurves.DataSource = dt;
        RadGridCurves.DataBind();

        ddlCurveGroup.SelectedValue = "0";
        ddlCurveGroup_SelectedIndexChanged(null, null);

        ClearText();
    }

    protected void btnClear_Click(object sender, EventArgs e)
    {
        ClearText();
    }

    protected void ClearText()
    {
        txtCurveName.Text = "";
        ddlCurveType.SelectedValue = "0";
        ddlCurveType.DefaultMessage = "-Select-";
        ddlCurveType.SelectedIndex = -1;
        txtNorthOffset.Text = "0.00";
        txtEastOffset.Text = "0.00";
        txtVSDirection.Text = "0.00";
        txtRKBElevation.Text = "0.00";
        RadColorPicker1.SelectedColor = Color.Black;
    }
    protected void RadGridCurves_ItemCommand(object sender, GridCommandEventArgs e)
    {
        if (e.CommandName == RadGrid.ExportToExcelCommandName)
        {
            string curveGroupName = ViewState["CurveGroupName"].ToString();
            curveGroupName = curveGroupName.Substring(curveGroupName.IndexOf('-') + 2);
            string targetName = ViewState["TargetName"].ToString();
            targetName = targetName.Substring(targetName.IndexOf('-') + 2);

            RadGridCurves.ExportSettings.FileName = curveGroupName + " " + targetName + " Curves " + DateTime.Now.Date.ToString("MM/dd/yyyy");
        }
    }
    protected void RadGridCurves_ItemCreated(object sender, GridItemEventArgs e)
    {
        if (e.Item is GridCommandItem)
        {
            ScriptManager ScriptManager1 = (ScriptManager)(Page.Master.FindControl("ScriptManager1"));
            Button btnExcel = (e.Item as GridCommandItem).FindControl("ExportToExcelButton") as Button;
            Button btnPDF = (e.Item as GridCommandItem).FindControl("ExportToPdfButton") as Button;
            Button btnCSV = (e.Item as GridCommandItem).FindControl("ExportToCsvButton") as Button;
            Button btnWord = (e.Item as GridCommandItem).FindControl("ExportToWordButton") as Button;
            ScriptManager1.RegisterPostBackControl(btnExcel);
            ScriptManager1.RegisterPostBackControl(btnPDF);
            ScriptManager1.RegisterPostBackControl(btnCSV);
            ScriptManager1.RegisterPostBackControl(btnWord);
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

            ddlCurveGroup.SelectedValue = "0";

            ddlTarget.Items.Clear();
            ddlTarget.Items.Add(new DropDownListItem("-Select-", "0"));
            ddlTarget.SelectedValue = "0";
            ddlTarget.Enabled = false;
            ddlCurveGroup.Enabled = true;
            
        }
        else
        {
            ddlCurveGroup.Items.Clear();
            ddlCurveGroup.Items.Add(new DropDownListItem("-Select-", "0"));
            ddlCurveGroup.SelectedValue = "0";

            ddlTarget.SelectedValue = "0";
            ddlTarget.Enabled = false;
            ddlCurveGroup.Enabled = false;
            btnCreate.Enabled = false;
        }
    }
}