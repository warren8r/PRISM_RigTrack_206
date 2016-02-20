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

public partial class Modules_RigTrack_PlotGraph : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        
        lblComments.Visible = true;
        if (!IsPostBack)
        {
            

            ddlCompany.DataSource = RigTrack.DatabaseObjects.RigTrackDO.GetCompany();
            ddlCompany.DataTextField = "Name";
            ddlCompany.DataValueField = "ID";
            ddlCompany.DataBind();

            //// Used for the samller graph
            //ddlCurveGroup2.DataSource = RigTrack.DatabaseObjects.RigTrackDO.GetAllCurveGroupNamesHaveTargets();
            //ddlCurveGroup2.DataTextField = "CurveGroupName";
            //ddlCurveGroup2.DataValueField = "ID";
            //ddlCurveGroup2.DataBind();

            Button btn = (Button)Page.Master.FindControl("btnClose");
           

            string curveGroupID = "";
            if (Request.QueryString["CurveGroupID"] != null)
            {
                curveGroupID = Request.QueryString["CurveGroupID"].ToString();
                string companyID = RigTrack.DatabaseObjects.RigTrackDO.GetCurveGroupsCompany(Int32.Parse(curveGroupID));
                DataTable dt = RigTrack.DatabaseObjects.RigTrackDO.GetAllCurveGroupsForCompany(Int32.Parse(companyID));
                ddlCurveGroup.DataSource = dt;
                ddlCurveGroup.DataTextField = "Name";
                ddlCurveGroup.DataValueField = "ID";
                ddlCurveGroup.DataBind();
                //ddlCurveGroup.Enabled = false;
                //ddlCompany.Enabled = false;

                ddlCompany.SelectedValue = companyID;
                ViewState["CurveGroupID"] = curveGroupID;
                ddlCurveGroup.SelectedValue = curveGroupID;
                ddlCurveGroup_SelectedIndexChanged(null, null);
                hiddenField.Value = curveGroupID;
                // used for the smaller graph
                //ddlCurveGroup2.SelectedValue = curveGroupID;
                //ddlCurveGroup2_SelectedIndexChanged(null, null);

                string plotComments = RigTrack.DatabaseObjects.RigTrackDO.GetPlotComments(Int32.Parse(curveGroupID));
                txtPlotComments.Text = plotComments;
                if (Request.QueryString["Modal"] != null && Request.QueryString["Modal"].ToString() == "True")
                {
                    ViewState["Modal"] = "True";
                    Control tblMenu = (Control)Page.Master.FindControl("tblMenu");
                    tblMenu.Visible = false;

                    RadMenu radMenu = Page.Master.FindControl("RadMenu1") as RadMenu;
                    radMenu.Visible = false;

                    btn.Visible = true;
                }
            }
            else
            {
                btn.Visible = false;
                ddlCurveGroup.DataSource = RigTrack.DatabaseObjects.RigTrackDO.GetAllCurveGroupNamesForSurvey();
                ddlCurveGroup.DataTextField = "CurveGroupName";
                ddlCurveGroup.DataValueField = "ID";
                ddlCurveGroup.DataBind();
            }
               
             
            

            int CurveGroupID = 0;
            int TargetID = 0;
            if (Request.QueryString["CurveGroupID"] != null)
            {

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
        }

    }

    protected void ddlCurveGroup_SelectedIndexChanged(object sender, DropDownListEventArgs e)
    {
        if (ddlCurveGroup.SelectedValue != "0")
        {
            ViewState["CurveGroupID"] = ddlCurveGroup.SelectedValue;
            ddlCurve.Enabled = true;
            //DataTable dtCurves = RigTrack.DatabaseObjects.RigTrackDO.GetCurvesForPlot(Int32.Parse(ddlCurveGroup.SelectedValue));
            //ddlCurve.DataSource = dtCurves;
            //ddlCurve.DataValueField = "ID";
            //ddlCurve.DataTextField = "Name";
            //ddlCurve.DataBind();

            string plotComments = RigTrack.DatabaseObjects.RigTrackDO.GetPlotComments(Int32.Parse(ddlCurveGroup.SelectedValue));
            txtPlotComments.Text = plotComments;

            if (Request.QueryString["CurveID"] != "" && Request.QueryString["CurveID"] != null)
            {
                hiddenFieldCurves.Value = Request.QueryString["CurveID"].ToString();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "ScriptManager1", "PopulateCurveDropdown(" + ViewState["CurveGroupID"].ToString() + ");", true);
            }
            else
            {
                DataTable dt = RigTrack.DatabaseObjects.RigTrackDO.GetCurveGroupFromID(Int32.Parse(ViewState["CurveGroupID"].ToString()));

                ScriptManager.RegisterStartupScript(this, this.GetType(), "ScriptManager1", "ItemSelectedFromServer(" + Int32.Parse(ViewState["CurveGroupID"].ToString()) + ");", true);
            }
        }
        else
        {
            ddlCurve.Enabled = false;
        }
    }

    [WebMethod]
    public static string GetPlotComments(string curveGroupID)
    {
        string returnValue = string.Empty;
        int curveGroupID_int = Int32.Parse(curveGroupID);
        if (curveGroupID_int > 0)
        {
            returnValue = RigTrack.DatabaseObjects.RigTrackDO.GetPlotComments(curveGroupID_int);
        }
        else
        {
            returnValue = "";
        }
        

        return returnValue;
    }


    [WebMethod]
    public static List<GetTargetShapes> GetGraphDetails(string curveGroupID)
    {
        DataTable DtTargetDetails = RigTrack.DatabaseObjects.RigTrackDO.GetTargetsForCurveGroupID(Convert.ToInt32(curveGroupID));
        //int emptyval = 0;
        //DataRow[] dr = DtTargetDetails.Select("TargetShapeID >" + emptyval);
        List<GetTargetShapes> listshapedet = new List<GetTargetShapes>();
        for (int i = 0; i < DtTargetDetails.Rows.Count; i++)
        {
            GetTargetShapes shapedet = new GetTargetShapes();
            shapedet.shape = DtTargetDetails.Rows[i]["TargetShapeID"].ToString();
            shapedet.xcoordinate = Convert.ToDouble(DtTargetDetails.Rows[i]["EWCoordinate"].ToString());
            shapedet.ycoordinate = Convert.ToDouble(DtTargetDetails.Rows[i]["NSCoordinate"].ToString());
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
    public static List<GetTargetShapes> GetTargetGraphDetails(string curveID)
    {
        string[] curveArray = curveID.Split(',');
        List<GetTargetShapes> listshapedet = new List<GetTargetShapes>();
        for (int i = 0; i < curveArray.Length; i++)
        {
            DataTable dtTargetDetails = RigTrack.DatabaseObjects.RigTrackDO.GetTargetsForCurveID(Convert.ToInt32(curveArray[i]));
            foreach(DataRow row in dtTargetDetails.Rows)
            {
                GetTargetShapes shapedet = new GetTargetShapes();
                shapedet.shape = row["TargetShapeID"].ToString();
                shapedet.xcoordinate = Convert.ToDouble(row["EWCoordinate"].ToString());
                shapedet.ycoordinate = Convert.ToDouble(row["NSCoordinate"].ToString());
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



                listshapedet.Add(shapedet);
            }
        }
        return listshapedet;
    }

    [WebMethod]
    public static List<RigTrack.DatabaseTransferObjects.SurveyDTO> GetSurveyDetails(string curveGroupID)
    {
        DataTable dt = RigTrack.DatabaseObjects.RigTrackDO.GetSurveysForPlot(Int32.Parse(curveGroupID));
        List<RigTrack.DatabaseTransferObjects.SurveyDTO> surveyList = new List<RigTrack.DatabaseTransferObjects.SurveyDTO>();
        if (dt.Rows.Count > 0)
        {
            foreach (DataRow row in dt.Rows)
            {
                RigTrack.DatabaseTransferObjects.SurveyDTO surveyDTO = new RigTrack.DatabaseTransferObjects.SurveyDTO();
                surveyDTO.TVD = Double.Parse(row["TVD"].ToString());
                surveyDTO.VerticalSection = Double.Parse(row["VerticalSection"].ToString());
                surveyDTO.ClosureDistance = Double.Parse(row["ClosureDistance"].ToString());
                surveyDTO.SurveyComment = row["SurveyComment"].ToString();
                surveyDTO.NS = Double.Parse(row["NS"].ToString());
                surveyDTO.EW = Double.Parse(row["EW"].ToString());
                surveyDTO.RowNumber = int.Parse(row["RowNumber"].ToString());
                surveyList.Add(surveyDTO);
            }
        }

        return surveyList;

   
    }

    [WebMethod]
    public static void btnSaveComments_Click(string curveGroupID, string comments)
    {

        int curveGroupID_int = Int32.Parse(curveGroupID);
        

        RigTrack.DatabaseObjects.RigTrackDO.SavePlotComments(curveGroupID_int, comments);
        
        //string script = "ErrorDialog(\" Please enter a valid number \");";
        //ScriptManager.RegisterStartupScript(this, this.GetType(), "NoRecords", script, true);

        //ConfirmDialog(this,event,'Confirm submitting timesheets for payment',true);"

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
                surveyDTO.TVD = double.Parse(row["TVD"].ToString());
                surveyDTO.VerticalSection = double.Parse(row["VerticalSection"].ToString());
                surveyDTO.ClosureDistance = double.Parse(row["ClosureDistance"].ToString());
                surveyDTO.SurveyComment = row["SurveyComment"].ToString();
                surveyDTO.NS = Double.Parse(row["NS"].ToString());
                surveyDTO.EW = Double.Parse(row["EW"].ToString());
                surveyDTO.RowNumber = int.Parse(row["RowNumber"].ToString());
                surveyDTO.Name = row["Name"].ToString();
                
                surveyList.Add(surveyDTO);
            }
        }

        return surveyList;

    }
    protected void btnView3D_Click(object sender, EventArgs e)
    {
        //ViewState["CurveGroupID"] = hiddenField.Value;
        string curves = hiddenField2.Value;

        if (ddlCurveGroup.SelectedValue.ToString() == "0")
        {
            Response.Redirect("Create3DGraph.aspx");
        }
    
       
        else
        {
            if (ViewState["Modal"] != null && ViewState["Modal"].ToString() == "True")
            {
                if (curves != "")
                {
                    curves = curves.Remove(curves.Length - 1);
                }
                Response.Redirect("Create3DGraph.aspx?CurveGroupID=" + ddlCurveGroup.SelectedValue + "&CurveID=" + curves + "&Modal=True");
            }
            else
            {
                if (curves != "")
                {
                    curves = curves.Remove(curves.Length - 1);
                }
                Response.Redirect("Create3DGraph.aspx?CurveGroupID=" + ddlCurveGroup.SelectedValue + "&CurveID=" + curves);
                //Response.Redirect("Create3DGraph.aspx?CurveGroupID=" + ddlCurveGroup.SelectedValue + "&CurveID=" + curves);
            }
        }
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
    public static List<RigTrack.DatabaseTransferObjects.ReportDTO> GetReport(string curveGroupID)
    {
        DataTable dt = RigTrack.DatabaseObjects.RigTrackDO.GetAllReports(Int32.Parse(curveGroupID), 0);
        List<RigTrack.DatabaseTransferObjects.ReportDTO> reportList = new List<RigTrack.DatabaseTransferObjects.ReportDTO>();
        //for (int i = 0; i < dt.Rows.Count; i++)
        //{
        if (dt.Rows.Count > 0)
        {

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
        }
        else
        {

            RigTrack.DatabaseTransferObjects.ReportDTO reportDTO = new RigTrack.DatabaseTransferObjects.ReportDTO();
            reportDTO.JobNumber = "";
            reportDTO.StateCountry = "";
            reportDTO.Company = "";
            reportDTO.Declination = "";
            reportDTO.LeaseWell = "";
            reportDTO.Grid = "";
            reportDTO.Location = "";
            reportDTO.JobName = "";
            reportDTO.RigName = "";
            reportDTO.CurveName = "";
            reportDTO.RKB = "";
            reportDTO.DateTime = "";
            reportDTO.GLorMSL = "";
            reportList.Add(reportDTO);
        }
        return reportList;
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


}