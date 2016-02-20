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


public partial class Modules_RigTrack_ViewReports : System.Web.UI.Page
{
  
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindDropDowns();
          
          
        }
    }
   
 
  
    //protected RigTrack.DatabaseTransferObjects.ReportSearchDTO AssignValues()
    //{
        
    //    return ReportSearchDTO;
    //}
  
   
    protected void BindDropDowns()
    {

        ddlCurveGroupID.DataSource = RigTrack.DatabaseObjects.RigTrackDO.GetAllCurveGroupNames();
        ddlCurveGroupID.DataTextField = "CurveGroupName";
        ddlCurveGroupID.DataValueField = "ID";
        ddlCurveGroupID.DataBind();

    }

    protected void ddlCurveGroupID_SelectedIndexChanged(object sender, DropDownListEventArgs e)
    {


        if (ddlCurveGroupID.SelectedValue == "0")
        {
            //ddlTarget.Enabled = false;

        }
        else
        {
            ddlTarget.Items.Clear();
            ddlTarget.Items.Add(new DropDownListItem("-Select-", "0"));
            ddlTarget.SelectedValue = "0";

            ddlTarget.DataSource = RigTrack.DatabaseObjects.RigTrackDO.GetTargetsForCurveGroup(Int32.Parse(ddlCurveGroupID.SelectedValue));
            ddlTarget.DataTextField = "TargetName";
            ddlTarget.DataValueField = "TargetID";
            ddlTarget.DataBind();
           
        }

    }

    protected void ddlTarget_SelectedIndexChanged(object sender, DropDownListEventArgs e)
    {
       
    }
}