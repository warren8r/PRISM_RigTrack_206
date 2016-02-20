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

public partial class Modules_RigTrack_CreateGraph : System.Web.UI.Page
{

    
    
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

            ddlCurveGroup.DataSource = RigTrack.DatabaseObjects.RigTrackDO.GetAllCurveGroupNamesHaveTargets();
            ddlCurveGroup.DataTextField = "CurveGroupName";
            ddlCurveGroup.DataValueField = "ID";
            ddlCurveGroup.DataBind();

            string CurveGroupID = "";
            if (Request.QueryString["CurveGroupID"] != null)
            {
                CurveGroupID = Request.QueryString["CurveGroupID"].ToString();
                ddlCurveGroup.SelectedValue = CurveGroupID;
                

                //DropDownListEventArgs curveGroup_e = new DropDownListEventArgs(ddlCurveGroup.SelectedIndex, ddlCurveGroup.SelectedText, ddlCurveGroup.SelectedValue);
                ddlCurveGroup_SelectedIndexChanged(null, null);
                ddlTargetID.Enabled = false;
                BtnDLSUp.Enabled = false;
                btnDLSDown.Enabled = false;
                txtDLSUpAndDown.Enabled = false;
                ddlProjectionPT.Enabled = false;
                btnClose.Visible = true;
                ddlCurveGroup.Enabled = false;
                
                
                updPnl1.Update();


               
            }

            txtDLSUpAndDown.Text = "1.0";
            txtDLS.Text = "1.0";
            
            //ScriptManager.RegisterStartupScript(this, this.GetType(), "ScriptManager1", "SimpleAlert();", true);
        }
    }
    protected void ddlCurveGroup_SelectedIndexChanged(object sender, DropDownListEventArgs e)
    {
        if (ddlCurveGroup.SelectedValue != "0")
        {
            if (Request.QueryString["CurveGroupID"] != null)
            {
                int curveGroupID = Int32.Parse(Request.QueryString["CurveGroupID"].ToString());
                
                ScriptManager.RegisterStartupScript(this, this.GetType(), "ScriptManager1", "ItemSelectedFromServer("+ curveGroupID + ");", true);
                
            }
            string ddlval = ddlCurveGroup.SelectedValue;
            DataTable DtTargetDetails = RigTrack.DatabaseObjects.RigTrackDO.GetTargetsForCurveGroupID(Convert.ToInt32(ddlval));
        }
    }
    [WebMethod]
    public static List<GetTargetShapes> GetGraphDetails(string curvegrpID)
    {
        DataTable DtTargetDetails = RigTrack.DatabaseObjects.RigTrackDO.GetTargetsForCurveGroupID(Convert.ToInt32(curvegrpID));
        int emptyval = 0;
        DataRow[] dr = DtTargetDetails.Select("TargetShapeID >" + emptyval);
        List<GetTargetShapes> listshapedet = new List<GetTargetShapes>();
        for (int i = 0; i < dr.Length; i++)
        {
            GetTargetShapes shapedet = new GetTargetShapes();
            shapedet.shape = dr[i]["TargetShapeID"].ToString();
            shapedet.xcoordinate = Convert.ToDouble(dr[i]["EWCoordinate"].ToString());
            shapedet.ycoordinate = Convert.ToDouble(dr[i]["NSCoordinate"].ToString());
            shapedet.numberOfVertices = Convert.ToDouble(dr[i]["NumberVertices"].ToString());
            shapedet.tvd = Convert.ToDouble(dr[i]["TVD"].ToString());
            listshapedet.Add(shapedet);
        }
        return listshapedet;
        //return Json(listshapedet, JsonRequestBehavior.AllowGet);
    }
    [WebMethod]
    public static List<GetTargetShapes> GetGraphDetailsOnTargetID(string targetID,string curveID)
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
                    shapedet.ycoordinate = Convert.ToDouble(dr[i]["NSCoordinate"].ToString());
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
        List<ComboInfo> result = new List<ComboInfo>();
        DataTable DtTargetDetails = RigTrack.DatabaseObjects.RigTrackDO.GetTargetsForCurveGroup(Int32.Parse(curvegrpID)); //RigTrack.DatabaseObjects.RigTrackDO.GetTargetsForCurveGroupID(Convert.ToInt32(curvegrpID));
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
    public class ComboInfo
    {
        public string Text { get; set; }

        public string Value { get; set; }
    }
    protected void btnEditTarget_Click(object sender, EventArgs e)
    {
        Response.Redirect("CreateTargets.aspx", true);
    }


    protected void BtnDrawgraph_Click(object sender, EventArgs e)
    {

        DataTable dt = RigTrack.DatabaseObjects.RigTrackDO.GetSurveysForCurve(12191);
        double[] x = new double[dt.Rows.Count];
        double[] y = new double[dt.Rows.Count];

        
        int count = 0;
        foreach (DataRow row in dt.Rows)
        {
            y[count] = double.Parse(row["TVD"].ToString());
            x[count] = double.Parse(row["EW"].ToString());
            count++;
        }
        string xStr = GetArrayString(x);
        string yStr = GetArrayString(y);
        string script = String.Format("Draw({0},{1});", xStr, yStr);
        ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "ScriptManager1",script, true);

        //int[] x = new int[] { 1, 2, 3, 4 };
        //int[] y = new int[] { 10, 20, 30, 40 };

        //string xStr = GetArrayString(x);
        //string yStr = GetArrayString(y);

        //StringBuilder sb = new StringBuilder();
        //sb.Append("<script language='javascript'>function Draw(){");
        //sb.Append("myGraph.context.beginPath();");
        //sb.Append("myGraph.context.moveTo(myGraph.centerX, myGraph.centerY);");
        //sb.Append("myGraph.context.lineTo(MyGraph.centerX + 50, myGraph.centerY - 50)");
        //sb.Append("myGraph.context.stroke();");
        //sb.Append("myGraph.context.closePath();");
        //sb.Append("}</script>");

        //if (!ClientScript.IsClientScriptBlockRegistered("ScriptManager1"))
        //{
        //    ClientScript.RegisterClientScriptBlock(this.GetType(), "ScriptManager1", sb.ToString());

        //}

        //string script = "<script language='javascript'>Draw(); </script";

        //if (!ClientScript.IsStartupScriptRegistered("ScriptManager1"))
        //{
        //    ClientScript.RegisterStartupScript(this.GetType(), "ScriptManager1", script);
        //}

        //ScriptManager.RegisterStartupScript(this, GetType(), "ScriptManager1", "DrawCurves();", true);
    }

    private string GetArrayString(double[] array)
    {
        StringBuilder sb = new StringBuilder();
        for (int i = 0; i < array.Length; i++)
        {
            sb.Append(array[i] + ",");
        }
        string arrayStr = string.Format("[{0}]", sb.ToString().TrimEnd(','));
        return arrayStr;
    }
}