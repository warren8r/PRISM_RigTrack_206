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




using System.Text;
using System.Web.Services;

public partial class Modules_RigTrack_Create3DGraph : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindDropDowns();


            Button btn = (Button)Page.Master.FindControl("btnClose");
            if (Request.QueryString["CurveGroupID"] != null)
            {

                string companyID = RigTrack.DatabaseObjects.RigTrackDO.GetCurveGroupsCompany(Int32.Parse(Request.QueryString["CurveGroupID"].ToString()));
                ddlCompany.SelectedValue = companyID;

                ddlCurveGroup.SelectedValue = Request.QueryString["CurveGroupID"].ToString();
                ViewState["CurveGroupID"] = Request.QueryString["CurveGroupID"].ToString();
                int curveGroupID = Int32.Parse(Request.QueryString["CurveGroupID"].ToString());

                if (Request.QueryString["CurveID"] != "" && Request.QueryString["CurveID"] != null)
                {
                    hiddenFieldCurves.Value = Request.QueryString["CurveID"].ToString();
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "ScriptManager1", "PopulateCurveDropdown(" + curveGroupID + ");", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "ScriptManager1", "OnClientItemSelected2(" + curveGroupID + ");", true);
                    DataTable dtTarget = RigTrack.DatabaseObjects.RigTrackDO.GetTargetsForCurveGroup(curveGroupID);
                    ddlTargets.DataSource = dtTarget;
                    ddlTargets.DataTextField = "TargetName";
                    ddlTargets.DataValueField = "TargetID";
                    ddlTargets.DataBind();
                }

                if (Request.QueryString["Modal"] != null && Request.QueryString["Modal"].ToString() == "True")
                {
                    ViewState["Modal"] = "True";
                    Control tblMenu = (Control)Page.Master.FindControl("tblMenu");
                    tblMenu.Visible = false;

                    RadMenu radMenu = Page.Master.FindControl("RadMenu1") as RadMenu;
                    radMenu.Visible = false;

                    btn.Visible = true;
                }

                try
                {
                    DataTable reportDT = RigTrack.DatabaseObjects.RigTrackDO.GetAllReports(Convert.ToInt32(Request.QueryString["CurveGroupID"]), 0);



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
                }
                catch (Exception ex)
                {

                }
            }
            else
            {
                btn.Visible = false;
            }
            
        }
    }

    protected void BindDropDowns()
    {

        ddlCurveGroup.DataSource = RigTrack.DatabaseObjects.RigTrackDO.GetAllCurveGroupNamesHaveTargets();
        ddlCurveGroup.DataTextField = "CurveGroupName";
        ddlCurveGroup.DataValueField = "ID";
        ddlCurveGroup.DataBind();

        ddlCompany.DataSource = RigTrack.DatabaseObjects.RigTrackDO.GetCompany();
        ddlCompany.DataTextField = "Name";
        ddlCompany.DataValueField = "ID";
        ddlCompany.DataBind();
    }

    [WebMethod]
    public static List<RigTrack.DatabaseTransferObjects.CurveGroupDTO> BindAllCurveGroups()
    {
        DataTable curveGroups = RigTrack.DatabaseObjects.RigTrackDO.GetAllCurveGroupNamesHaveTargets();
        List<RigTrack.DatabaseTransferObjects.CurveGroupDTO> returnData = new List<RigTrack.DatabaseTransferObjects.CurveGroupDTO>();
        for (int i = 0; i < curveGroups.Rows.Count; i++)
        {
            RigTrack.DatabaseTransferObjects.CurveGroupDTO dto = new RigTrack.DatabaseTransferObjects.CurveGroupDTO();
            dto.CurveGroupName = curveGroups.Rows[i]["CurveGroupName"].ToString();
            dto.ID = Int32.Parse(curveGroups.Rows[i]["ID"].ToString());
            returnData.Add(dto);
        }
        return returnData;
    }

    

    [WebMethod]
    public static List<RigTrack.DatabaseTransferObjects.CurveGroupDTO> BindCurveGroupsForCompany(string CompanyID)
    {
        int companyID_int = Int32.Parse(CompanyID);
        DataTable curveGroups = RigTrack.DatabaseObjects.RigTrackDO.GetCurveGroupsForPlotByCompany(companyID_int);
        List<RigTrack.DatabaseTransferObjects.CurveGroupDTO> returnData = new List<RigTrack.DatabaseTransferObjects.CurveGroupDTO>();
        for (int i = 0; i < curveGroups.Rows.Count; i++)
        {
            RigTrack.DatabaseTransferObjects.CurveGroupDTO dto = new RigTrack.DatabaseTransferObjects.CurveGroupDTO();
            dto.CurveGroupName = curveGroups.Rows[i]["CurveGroupName"].ToString();
            dto.ID = Int32.Parse(curveGroups.Rows[i]["ID"].ToString());
            returnData.Add(dto);
        }

        return returnData;
    }



    [WebMethod]
    public static List<RigTrack.DatabaseTransferObjects.SurveyDTO> GetSurveyDetails(string curvegrpID)
    {
        DataTable surveys = RigTrack.DatabaseObjects.RigTrackDO.GetSurveysForCurveGroup(Int32.Parse(curvegrpID));
        
        List<RigTrack.DatabaseTransferObjects.SurveyDTO> returnData = new List<RigTrack.DatabaseTransferObjects.SurveyDTO>();

        for (int i = 0; i < surveys.Rows.Count; i++)
        {
            RigTrack.DatabaseTransferObjects.SurveyDTO dto = new RigTrack.DatabaseTransferObjects.SurveyDTO();
            dto.MD = double.Parse(surveys.Rows[i]["MD"].ToString());
            dto.INC = double.Parse(surveys.Rows[i]["Inclination"].ToString());
            dto.Azimuth = double.Parse(surveys.Rows[i]["Azimuth"].ToString());
            dto.TVD = double.Parse(surveys.Rows[i]["TVD"].ToString());
            dto.NS = double.Parse(surveys.Rows[i]["NS"].ToString());
            dto.EW = double.Parse(surveys.Rows[i]["EW"].ToString());
            dto.VerticalSection = double.Parse(surveys.Rows[i]["VerticalSection"].ToString());
            dto.CL = double.Parse(surveys.Rows[i]["CL"].ToString());
            dto.ClosureDistance = double.Parse(surveys.Rows[i]["ClosureDistance"].ToString());
            dto.ClosureDirection = double.Parse(surveys.Rows[i]["ClosureDirection"].ToString());
            dto.DLS = double.Parse(surveys.Rows[i]["DLS"].ToString());
            dto.DLA = double.Parse(surveys.Rows[i]["DLA"].ToString());
            dto.BR = double.Parse(surveys.Rows[i]["BR"].ToString());
            dto.WR = double.Parse(surveys.Rows[i]["WR"].ToString());
            dto.TFO = double.Parse(surveys.Rows[i]["TFO"].ToString());
            dto.SurveyComment = surveys.Rows[i]["SurveyComment"].ToString();
            dto.RowNumber = int.Parse(surveys.Rows[i]["RowNumber"].ToString());
            dto.CurveColor = surveys.Rows[i]["Color"].ToString();
            returnData.Add(dto);
        }
        
        return returnData;
    }

    [WebMethod]
    public static List<RigTrack.DatabaseTransferObjects.SurveyDTO> GetSurveyDetailsForTarget(string targetID, string curvegrpID)
    {
        DataTable surveys = RigTrack.DatabaseObjects.RigTrackDO.GetSurveysForTarget(Int32.Parse(targetID));
        List<RigTrack.DatabaseTransferObjects.SurveyDTO> returnData = new List<RigTrack.DatabaseTransferObjects.SurveyDTO>();
        for (int i = 0; i < surveys.Rows.Count; i++)
        {
            RigTrack.DatabaseTransferObjects.SurveyDTO dto = new RigTrack.DatabaseTransferObjects.SurveyDTO();
            dto.MD = double.Parse(surveys.Rows[i]["MD"].ToString());
            dto.INC = double.Parse(surveys.Rows[i]["Inclination"].ToString());
            dto.Azimuth = double.Parse(surveys.Rows[i]["Azimuth"].ToString());
            dto.TVD = double.Parse(surveys.Rows[i]["TVD"].ToString());
            dto.NS = double.Parse(surveys.Rows[i]["NS"].ToString());
            dto.EW = double.Parse(surveys.Rows[i]["EW"].ToString());
            dto.VerticalSection = double.Parse(surveys.Rows[i]["VerticalSection"].ToString());
            dto.CL = double.Parse(surveys.Rows[i]["CL"].ToString());
            dto.ClosureDistance = double.Parse(surveys.Rows[i]["ClosureDistance"].ToString());
            dto.ClosureDirection = double.Parse(surveys.Rows[i]["ClosureDirection"].ToString());
            dto.DLS = double.Parse(surveys.Rows[i]["DLS"].ToString());
            dto.DLA = double.Parse(surveys.Rows[i]["DLA"].ToString());
            dto.BR = double.Parse(surveys.Rows[i]["BR"].ToString());
            dto.WR = double.Parse(surveys.Rows[i]["WR"].ToString());
            dto.TFO = double.Parse(surveys.Rows[i]["TFO"].ToString());
            returnData.Add(dto);
        }
        return returnData;
    }

    [WebMethod]
    public static List<GetTargetShapes> GetGraphDetails(string curvegrpID)
    {
        
        DataTable DtTargetDetails = RigTrack.DatabaseObjects.RigTrackDO.GetTargetsForCurveGroupID(Convert.ToInt32(curvegrpID));
        //int emptyval = 0;
       // DataRow[] dr = DtTargetDetails.Select("TargetShapeID >" + emptyval);
        List<GetTargetShapes> listshapedet = new List<GetTargetShapes>();
        for (int i = 0; i < DtTargetDetails.Rows.Count; i++)
        {
            GetTargetShapes shapedet = new GetTargetShapes();
            shapedet.shape = DtTargetDetails.Rows[i]["TargetShapeID"].ToString();
            shapedet.targetName = DtTargetDetails.Rows[i]["TargetName"].ToString();
            shapedet.xcoordinate = Convert.ToDouble(DtTargetDetails.Rows[i]["EWCoordinate"].ToString());
            shapedet.zcoordinate = Convert.ToDouble(DtTargetDetails.Rows[i]["NSCoordinate"].ToString());
            shapedet.numberOfVertices = Convert.ToDouble(DtTargetDetails.Rows[i]["NumberVertices"].ToString());
            shapedet.tvd = Convert.ToDouble(DtTargetDetails.Rows[i]["TVD"].ToString());
            shapedet.DiameterOfCircleXoffset = Convert.ToDouble(DtTargetDetails.Rows[i]["DiameterOfCircleXoffset"].ToString());
            shapedet.DiameterOfCircleYoffset = Convert.ToDouble(DtTargetDetails.Rows[i]["DiameterOfCircleYoffset"].ToString());
            shapedet.TargetOffsetXoffset = Convert.ToDouble(DtTargetDetails.Rows[i]["TargetOffsetXoffset"].ToString());
            shapedet.TargetOffsetYoffset = Convert.ToDouble(DtTargetDetails.Rows[i]["TargetOffsetYoffset"].ToString());
            shapedet.Rotation = Convert.ToDouble(DtTargetDetails.Rows[i]["Rotation"].ToString());


            shapedet.NumberVertices = Convert.ToDouble(DtTargetDetails.Rows[i]["NumberVertices"].ToString());

            shapedet.Corner1Xofffset = Convert.ToDouble(DtTargetDetails.Rows[i]["Corner1Xofffset"].ToString());
            shapedet.Corner1Yoffset = Convert.ToDouble(DtTargetDetails.Rows[i]["Corner1Yoffset"].ToString());

            shapedet.Corner2Xoffset = Convert.ToDouble(DtTargetDetails.Rows[i]["Corner2Xoffset"].ToString());
            shapedet.Corner2Yoffset = Convert.ToDouble(DtTargetDetails.Rows[i]["Corner2Yoffset"].ToString());

            shapedet.Corner3Xoffset = Convert.ToDouble(DtTargetDetails.Rows[i]["Corner3Xoffset"].ToString());
            shapedet.Corner3Yoffset = Convert.ToDouble(DtTargetDetails.Rows[i]["Corner3Yoffset"].ToString());

            shapedet.Corner4Xoffset = Convert.ToDouble(DtTargetDetails.Rows[i]["Corner4Xoffset"].ToString());
            shapedet.Corner4Yoffset = Convert.ToDouble(DtTargetDetails.Rows[i]["Corner4Yoffset"].ToString());

            shapedet.Corner5Xoffset = Convert.ToDouble(DtTargetDetails.Rows[i]["Corner5Xoffset"].ToString());
            shapedet.Corner5Yoffset = Convert.ToDouble(DtTargetDetails.Rows[i]["Corner5Yoffset"].ToString());

            shapedet.Corner6Xoffset = Convert.ToDouble(DtTargetDetails.Rows[i]["Corner6Xoffset"].ToString());
            shapedet.Corner6Yoffset = Convert.ToDouble(DtTargetDetails.Rows[i]["Corner6Yoffset"].ToString());

            shapedet.Corner7Xoffset = Convert.ToDouble(DtTargetDetails.Rows[i]["Corner7Xoffset"].ToString());
            shapedet.Corner7Yoffset = Convert.ToDouble(DtTargetDetails.Rows[i]["Corner7Yoffset"].ToString());

            shapedet.Corner8Xoffset = Convert.ToDouble(DtTargetDetails.Rows[i]["Corner8Xoffset"].ToString());
            shapedet.Corner8Yoffset = Convert.ToDouble(DtTargetDetails.Rows[i]["Corner8Yoffset"].ToString());

            listshapedet.Add(shapedet);
        }
        return listshapedet;
        //return Json(listshapedet, JsonRequestBehavior.AllowGet);
    }
    [WebMethod]
    public static List<GetTargetShapes> GetGraphDetailsOnTargetID(string targetID, string curveID)
    {
        DataTable DtTargetDetails = RigTrack.DatabaseObjects.RigTrackDO.GetTargetsForCurveGroupID(Convert.ToInt32(curveID));
        List<GetTargetShapes> listshapedet = new List<GetTargetShapes>();
        if (DtTargetDetails.Rows.Count > 0)
        {
            DataRow[] dr = DtTargetDetails.Select("ID =" + targetID);
            if (dr.Length > 0)
            {

                for (int i = 0; i < dr.Length; i++)
                {
                    GetTargetShapes shapedet = new GetTargetShapes();
                    shapedet.shape = dr[i]["TargetShapeID"].ToString();
                    shapedet.xcoordinate = Convert.ToDouble(dr[i]["EWCoordinate"].ToString());
                    shapedet.zcoordinate = Convert.ToDouble(dr[i]["NSCoordinate"].ToString());
                    shapedet.numberOfVertices = Convert.ToDouble(dr[i]["NumberVertices"].ToString());
                    shapedet.tvd = Convert.ToDouble(dr[i]["TVD"].ToString());
                    listshapedet.Add(shapedet);
                }
            }
        }
        return listshapedet;
        //return Json(listshapedet, JsonRequestBehavior.AllowGet);
    }

    [WebMethod]
    public static List<ComboInfo> GetTargetDetails(string curvegrpID)
    {
        DataTable DtTargetDetails = RigTrack.DatabaseObjects.RigTrackDO.GetTargetsForCurveGroup(Convert.ToInt32(curvegrpID));
        List<ComboInfo> result = new List<ComboInfo>();
        if (DtTargetDetails.Rows.Count > 0)
        {
            //DataTable dtTargets = DtTargetDetails.Select("TargetShapeID >0").CopyToDataTable();
            
            for (int i = 0; i < DtTargetDetails.Rows.Count; i++)
            {
                ComboInfo itemData = new ComboInfo { Text = DtTargetDetails.Rows[i]["TargetName"].ToString(), Value = DtTargetDetails.Rows[i]["TargetID"].ToString() };
                result.Add(itemData);
            }
        }
        return result;
    }

    [WebMethod]
    public static List<ComboInfo> GetTargetDetailsForAllCurves(string curveID)
    {

        string[] curveArray = curveID.Split(',');
        List<ComboInfo> result = new List<ComboInfo>();
     
       
        for (int i = 0; i < curveArray.Length; i++)
        {
            DataTable dtTargetDetails = RigTrack.DatabaseObjects.RigTrackDO.GetTargetsForAllCurves(Convert.ToInt32(curveArray[i]));
            foreach (DataRow row in dtTargetDetails.Rows)
            {
                bool duplicateData = false;
                //ComboInfo itemData = new ComboInfo { Text = dtTargetDetails.Rows[i]["TargetName"].ToString(), Value = dtTargetDetails.Rows[i]["TargetID"].ToString() };
                ComboInfo itemData = new ComboInfo { Text = row["TargetName"].ToString(), Value = row["TargetID"].ToString() };
                foreach (ComboInfo combo in result)
                {
                    if (itemData.Value == combo.Value)
                    {
                        duplicateData = true;
                    }
                }
                if (!duplicateData)
                {
                    result.Add(itemData);
                }
            }
        }
    
        return result;

    }
    public class ComboInfo
    {
        public string Text { get; set; }

        public string Value { get; set; }
    }


    protected void btnView2D_Click(object sender, EventArgs e)
    {
        //ViewState["CurveGroupID"] = ddlCurveGroup.SelectedValue;
        string curves = hiddenField2.Value;

        if (ddlCurveGroup.SelectedValue.ToString() == "0")
        {
            Response.Redirect("PlotGraph.aspx");
        }

        else
        {

            if (ViewState["Modal"] != null && ViewState["Modal"].ToString() == "True")
            {
                if (curves != "")
                {
                    curves = curves.Remove(curves.Length - 1);
                }
                Response.Redirect("PlotGraph.aspx?CurveGroupID=" + ddlCurveGroup.SelectedValue + "&CurveID=" + curves + "&Modal=True");
            }
            else
            {
                if (curves != "")
                {
                    curves = curves.Remove(curves.Length - 1);
                }
                Response.Redirect("PlotGraph.aspx?CurveGroupID=" + ddlCurveGroup.SelectedValue + "&CurveID=" + curves);
            }

        }

    }

    [WebMethod]
    public static List<RigTrack.DatabaseTransferObjects.ReportDTO> GetReport(string curveGroupID)
    {
        DataTable dt = RigTrack.DatabaseObjects.RigTrackDO.GetAllReports(Int32.Parse(curveGroupID), 0);
        List<RigTrack.DatabaseTransferObjects.ReportDTO> reportList = new List<RigTrack.DatabaseTransferObjects.ReportDTO>();
        //for (int i = 0; i < dt.Rows.Count; i++)
        //{
        RigTrack.DatabaseTransferObjects.ReportDTO reportDTO = new RigTrack.DatabaseTransferObjects.ReportDTO();
        reportDTO.JobNumber = dt.Rows[0]["JobNumber"].ToString();
        reportDTO.StateCountry = dt.Rows[0]["StateCountry"].ToString();
        reportDTO.Company = dt.Rows[0]["CompanyName"].ToString();
        reportDTO.Declination = dt.Rows[0]["Declination"].ToString();
        reportDTO.LeaseWell = dt.Rows[0]["LeaseWell"].ToString();
        reportDTO.Grid = dt.Rows[0]["Grid"].ToString();
        reportDTO.Location = dt.Rows[0]["JobLocation"].ToString();
        reportDTO.JobName = dt.Rows[0]["CurveGroupName"].ToString();
        reportDTO.RigName = dt.Rows[0]["RigName"].ToString();
        reportDTO.CurveName = dt.Rows[0]["CurveName"].ToString();
        reportDTO.RKB = dt.Rows[0]["RKB"].ToString();
        reportDTO.DateTime = dt.Rows[0]["CurrentDateTime"].ToString();
        reportDTO.GLorMSL = dt.Rows[0]["GLorMSL"].ToString();
        reportList.Add(reportDTO);
        //}
        return reportList;
    }

    [WebMethod]
    public static List<RigTrack.DatabaseTransferObjects.CurveDTO> GetCurves(string curveGroupID)
    {
        DataTable dt = RigTrack.DatabaseObjects.RigTrackDO.GetCurvesForPlot(Int32.Parse(curveGroupID));
        List<RigTrack.DatabaseTransferObjects.CurveDTO> curveList = new List<RigTrack.DatabaseTransferObjects.CurveDTO>();
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            RigTrack.DatabaseTransferObjects.CurveDTO curveDTO = new RigTrack.DatabaseTransferObjects.CurveDTO();
            curveDTO.Name = dt.Rows[i]["Name"].ToString();
            curveDTO.ID = Int32.Parse(dt.Rows[i]["ID"].ToString());
            curveList.Add(curveDTO);
        }
        return curveList;
    }

    [WebMethod]
    public static List<GetTargetShapes> GetTargetGraphDetails(string curveID)
    {
        string[] curveArray = curveID.Split(',');
        List<GetTargetShapes> listshapedet = new List<GetTargetShapes>();
        for (int i = 0; i < curveArray.Length; i++)
        {
            DataTable dtTargetDetails = RigTrack.DatabaseObjects.RigTrackDO.GetTargetsForCurveID(Convert.ToInt32(curveArray[i]));
            foreach (DataRow row in dtTargetDetails.Rows)
            {
                bool duplicateData = false;
                GetTargetShapes shapedet = new GetTargetShapes();
                shapedet.ID = Convert.ToInt32(row["ID"].ToString());
                shapedet.shape = row["TargetShapeID"].ToString();
                shapedet.targetName = row["Name"].ToString();
                shapedet.xcoordinate = Convert.ToDouble(row["EWCoordinate"].ToString());
                shapedet.zcoordinate = Convert.ToDouble(row["NSCoordinate"].ToString());
                shapedet.numberOfVertices = Convert.ToDouble(row["NumberVertices"].ToString());
                shapedet.tvd = Convert.ToDouble(row["TVD"].ToString());
                shapedet.DiameterOfCircleXoffset = Convert.ToDouble(row["DiameterOfCircleXoffset"].ToString());
                shapedet.DiameterOfCircleYoffset = Convert.ToDouble(row["DiameterOfCircleYoffset"].ToString());
                shapedet.TargetOffsetXoffset = Convert.ToDouble(row["TargetOffsetXoffset"].ToString());
                shapedet.TargetOffsetYoffset = Convert.ToDouble(row["TargetOffsetYoffset"].ToString());
                shapedet.Rotation = Convert.ToDouble(row["Rotation"].ToString());

                shapedet.NumberVertices = Convert.ToDouble(row["NumberVertices"].ToString());

                shapedet.Corner1Xofffset = Convert.ToDouble(row["Corner1Xofffset"].ToString());
                shapedet.Corner1Yoffset = Convert.ToDouble(row["Corner1Yoffset"].ToString());

                shapedet.Corner2Xoffset = Convert.ToDouble(row["Corner2Xoffset"].ToString());
                shapedet.Corner2Yoffset = Convert.ToDouble(row["Corner2Yoffset"].ToString());

                shapedet.Corner3Xoffset = Convert.ToDouble(row["Corner3Xoffset"].ToString());
                shapedet.Corner3Yoffset = Convert.ToDouble(row["Corner3Yoffset"].ToString());

                shapedet.Corner4Xoffset = Convert.ToDouble(row["Corner4Xoffset"].ToString());
                shapedet.Corner4Yoffset = Convert.ToDouble(row["Corner4Yoffset"].ToString());

                shapedet.Corner5Xoffset = Convert.ToDouble(row["Corner5Xoffset"].ToString());
                shapedet.Corner5Yoffset = Convert.ToDouble(row["Corner5Yoffset"].ToString());

                shapedet.Corner6Xoffset = Convert.ToDouble(row["Corner6Xoffset"].ToString());
                shapedet.Corner6Yoffset = Convert.ToDouble(row["Corner6Yoffset"].ToString());

                shapedet.Corner7Xoffset = Convert.ToDouble(row["Corner7Xoffset"].ToString());
                shapedet.Corner7Yoffset = Convert.ToDouble(row["Corner7Yoffset"].ToString());

                shapedet.Corner8Xoffset = Convert.ToDouble(row["Corner8Xoffset"].ToString());
                shapedet.Corner8Yoffset = Convert.ToDouble(row["Corner8Yoffset"].ToString());

                foreach(GetTargetShapes targetShape in listshapedet)
                {
                    if (targetShape.ID == shapedet.ID)
                    {
                        duplicateData = true;
                    }
                }
                if (!duplicateData)
                {
                    listshapedet.Add(shapedet);
                }
            }
        }
        return listshapedet;
    }

    [WebMethod]
    public static List<RigTrack.DatabaseTransferObjects.SurveyDTO> GetGraphDetailsPerCurves(string curves)
    {
        string[] curveID = curves.Split(',');
        List<RigTrack.DatabaseTransferObjects.SurveyDTO> surveyList = new List<RigTrack.DatabaseTransferObjects.SurveyDTO>();
        for (int i = 0; i < curveID.Length; i++)
        {
            DataTable dt = RigTrack.DatabaseObjects.RigTrackDO.GetSurveysForPlotFromCurve(Int32.Parse(curveID[i]));
            foreach (DataRow row in dt.Rows)
            {
                RigTrack.DatabaseTransferObjects.SurveyDTO surveyDTO = new RigTrack.DatabaseTransferObjects.SurveyDTO();
                surveyDTO.CurveID = int.Parse(row["CurveID"].ToString());
                surveyDTO.TVD = double.Parse(row["TVD"].ToString());
                surveyDTO.VerticalSection = double.Parse(row["VerticalSection"].ToString());
                surveyDTO.ClosureDistance = double.Parse(row["ClosureDistance"].ToString());
                surveyDTO.SurveyComment = row["SurveyComment"].ToString();
                surveyDTO.NS = Double.Parse(row["NS"].ToString());
                surveyDTO.EW = Double.Parse(row["EW"].ToString());
                surveyDTO.RowNumber = int.Parse(row["RowNumber"].ToString());
                surveyDTO.Name = row["Name"].ToString();
                surveyDTO.CurveColor = row["Color"].ToString();
                surveyList.Add(surveyDTO);
            }
        }

        return surveyList;

    }

    [WebMethod]
    public static void UpdateCurveColor(int curveID, string curveColor)
    {
        RigTrack.DatabaseObjects.RigTrackDO.UpdateCurveColor(curveID, curveColor);
    }


  
}