using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using Telerik.Web.UI;
public partial class Modules_Reports_CommandCentralDashboard : System.Web.UI.Page
{
    public static DataTable dt_jobpersonals = new DataTable();
    public static DataTable dt_jobequipment = new DataTable();
    public static DataTable dt_jobactivity = new DataTable();
    public static DataTable dt_jobassignAssets = new DataTable();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            bindgrid();
            //dt_jobassignAssets = SqlHelper.ExecuteDataset(GlobalConnetionString.ClientConnection(), CommandType.Text,
            //   " select *,ppa.AssetName as JobAssignedAsset from PrismJobAssignedAssets pja,Prism_Assets pa,PrismAssetName ppa where pja.Assetid=pa.Id and pa.Assetname=ppa.ID").Tables[0];
            //dt_jobpersonals = SqlHelper.ExecuteDataset(GlobalConnetionString.ClientConnection(), CommandType.Text,
            //    " select * from manageJobOrders mj,PrismJobAssignedPersonals pa,Users JP where mj.jid=pa.JobId and JP.UserId=pa.UserId").Tables[0];

            //dt_jobequipment = SqlHelper.ExecuteDataset(GlobalConnetionString.ClientConnection(), CommandType.Text,
            //   " select * from DataApprovalNotes").Tables[0];

            //dt_jobactivity = SqlHelper.ExecuteDataset(GlobalConnetionString.ClientConnection(), CommandType.Text,
            //   "select * from JobDataApproval jd,RigStatusDet rd where jd.rigStatusid=rd.sid").Tables[0];
           
           
        }
    }
    protected void grid_rig_ItemCommand(object obj, GridCommandEventArgs e)
    {
        if (e.CommandName == Telerik.Web.UI.RadGrid.ExportToExcelCommandName ||
                   e.CommandName == Telerik.Web.UI.RadGrid.ExportToWordCommandName ||
                   e.CommandName == Telerik.Web.UI.RadGrid.ExportToCsvCommandName ||
                   e.CommandName == Telerik.Web.UI.RadGrid.ExportToPdfCommandName)
        {
            grid_rig.ExportSettings.ExportOnlyData = true;
            grid_rig.ExportSettings.OpenInNewWindow = true;
            grid_rig.ExportSettings.IgnorePaging = true;
            grid_rig.ExportSettings.FileName = "Export";
        }
    }
    protected void grid_rig_ItemDataBound(object sender, GridItemEventArgs e)
    {
        // RadGrid grid = (RadGrid)sender;
        if ((e.Item.ItemType == GridItemType.Item) || (e.Item.ItemType == GridItemType.AlternatingItem))
        { 
            GridDataItem dataItem = e.Item as GridDataItem;
            Label lbl_personals = (Label)dataItem.FindControl("lbl_personals");
            Label lbl_Equipment = (Label)dataItem.FindControl("lbl_Equipment");
            Label lbl_Activity = (Label)dataItem.FindControl("lbl_Activity");
            Label lbl_Assets = (Label)dataItem.FindControl("lbl_Assets");
            Label lbl_jid = (Label)dataItem.FindControl("lbl_jid");
            dt_jobassignAssets = SqlHelper.ExecuteDataset(GlobalConnetionString.ClientConnection(), CommandType.Text,
               " select *,ppa.AssetName as JobAssignedAsset from PrismJobAssignedAssets pja,Prism_Assets pa,PrismAssetName ppa where pja.Assetid=pa.Id and pa.Assetname=ppa.ID").Tables[0];
            dt_jobpersonals = SqlHelper.ExecuteDataset(GlobalConnetionString.ClientConnection(), CommandType.Text,
                " select * from manageJobOrders mj,PrismJobAssignedPersonals pa,Users JP where mj.jid=pa.JobId and JP.UserId=pa.UserId").Tables[0];

            dt_jobequipment = SqlHelper.ExecuteDataset(GlobalConnetionString.ClientConnection(), CommandType.Text,
               " select * from DataApprovalNotes").Tables[0];

            dt_jobactivity = SqlHelper.ExecuteDataset(GlobalConnetionString.ClientConnection(), CommandType.Text,
               "select * from JobDataApproval jd,RigStatusDet rd where jd.rigStatusid=rd.sid").Tables[0];
            //dataItem["CurrentLocationID"].Text;
            string dataKeyValue = lbl_jid.Text;
            DataRow[] row_JOB = dt_jobpersonals.Select("jid=" + dataKeyValue);
            DataRow[] row_JOBAssets = dt_jobassignAssets.Select("jobid=" + dataKeyValue);
            string personals = "",Assets="";
            for (int person = 0; person < row_JOB.Length; person++)
            {
                personals += Environment.NewLine + row_JOB[person]["firstName"].ToString() + " " + row_JOB[person]["lastName"].ToString() + ",";
            }
            if (personals != "")
            {
                lbl_personals.Text = personals.Remove(personals.Length - 1);
            }
            // JobAssignedAsset SerialNumber
            for (int asset = 0; asset < row_JOBAssets.Length; asset++)
            {
                Assets += Environment.NewLine + row_JOBAssets[asset]["JobAssignedAsset"].ToString() + " (<span style='color:blue;'>" + row_JOBAssets[asset]["SerialNumber"].ToString() + "</span>) ,";
            }
            if (Assets != "")
            {
                lbl_Assets.Text = Assets.Remove(Assets.Length - 1);
            }
            //try
            //{
            //    DateTime maxDate = DateTime.Parse(dt_jobequipment.Compute("Max ( Date ) ", "jid = " + dataKeyValue + "").ToString());
            //    if (maxDate != null)
            //    {
            //        DataRow[] rowEquipment = dt_jobequipment.Select("Date='" + maxDate + "' and jid = " + dataKeyValue + "");
            //        if (rowEquipment.Length > 0)
            //        {
            //            lbl_Equipment.Text = rowEquipment[0]["Notes"].ToString();
            //        }
            //    }
            //}
            //catch(Exception ex)
            //    {
            //        lbl_Equipment.Text = "";
            //    }
            //DataRow[] rowActivity = dt_jobactivity.Select("jid = " + dataKeyValue + "");
            //if (rowActivity.Length > 0)
            //{
            //    lbl_Activity.Text = rowActivity[0]["rigstatuses"].ToString();
            //}
            string getassetneed = "select runid,Date,runnumber from PrismJobRunDetails where jid=" + dataKeyValue + " and Date in (select max(Date) from PrismJobRunDetails where jid=" + dataKeyValue + ")";
            DataTable dt_assetneeded = SqlHelper.ExecuteDataset(GlobalConnetionString.ClientConnection(), CommandType.Text, getassetneed).Tables[0];
            if (dt_assetneeded.Rows.Count > 0)
            {
                Label lbl_runhr = (Label)dataItem.FindControl("lbl_runhr");
                Label lbl_runnumber = (Label)dataItem.FindControl("lbl_runnumber");
                lbl_runnumber.Text = dt_assetneeded.Rows[0]["runnumber"].ToString();
                lbl_runhr.Text = Convert.ToDateTime(dt_assetneeded.Rows[0]["Date"].ToString()).ToShortDateString();
                string query = "select pa.[24HourActivity] as Activity,ph.Time as Timedet from PrismJobRun24HourActivityLog p,Prism24HourActivity pa,Prism24Hours ph where p.Time=ph.TimeId and p.[24HourActivity]=pa.HourActivityId and p.RunId=" + dt_assetneeded.Rows[0]["runid"].ToString() + "  order by p.Time Desc";
                DataTable lastActivity = SqlHelper.ExecuteDataset(GlobalConnetionString.ClientConnection(), CommandType.Text, query).Tables[0];
                if (lastActivity.Rows.Count > 0)
                {
                    lbl_Activity.Text = lastActivity.Rows[0]["Activity"].ToString() + "(" + lastActivity.Rows[0]["Timedet"].ToString() + "),";
                }
                else
                {
                    lbl_Activity.Text = "No Activity Selected";
                }
                //lbl_Equipment.Text = "";
                DataTable dt_getsupplyneeded = SqlHelper.ExecuteDataset(GlobalConnetionString.ClientConnection(), CommandType.Text, "select * from PrismAssetName a, PrismJobRunAssetsRequired b where a.id=b.assetid and b.RunID=" + dt_assetneeded.Rows[0]["runid"].ToString() + "").Tables[0];
                if (dt_getsupplyneeded.Rows.Count > 0)
                {
                    for (int eqip = 0; eqip < dt_getsupplyneeded.Rows.Count; eqip++)
                    {
                        lbl_Equipment.Text += dt_getsupplyneeded.Rows[eqip]["AssetName"].ToString() + "(" + dt_getsupplyneeded.Rows[eqip]["Aqntty"].ToString() + "),";
                    }
                }
                else
                {
                    lbl_Equipment.Text = "Additional Assets Not Required";
                }
                //if (dt_getsupplyneeded.Rows.Count > 0)
                //{
                //    if (dt_getsupplyneeded.Rows[0]["suppliesneeded"].ToString() != "")
                //    {
                //        lbl_Equipment.Text += dt_getsupplyneeded.Rows[0]["suppliesneeded"].ToString() + ",";
                //    }
                //    if (dt_getsupplyneeded.Rows[1]["suppliesneeded"].ToString() != "")
                //    {
                //        lbl_Equipment.Text += dt_getsupplyneeded.Rows[1]["suppliesneeded"].ToString() + ",";
                //    }
                //    if (dt_getsupplyneeded.Rows[2]["suppliesneeded"].ToString() != "")
                //    {
                //        lbl_Equipment.Text += dt_getsupplyneeded.Rows[2]["suppliesneeded"].ToString() + ",";
                //    }
                //    if (dt_getsupplyneeded.Rows[3]["suppliesneeded"].ToString() != "")
                //    {
                //        lbl_Equipment.Text += dt_getsupplyneeded.Rows[3]["suppliesneeded"].ToString() + ",";
                //    }
                //    if (dt_getsupplyneeded.Rows[4]["suppliesneeded"].ToString() != "")
                //    {
                //        lbl_Equipment.Text += dt_getsupplyneeded.Rows[4]["suppliesneeded"].ToString() + "";
                //    }
                //}

            }
            
        }

    }
    protected void btn_view_Click(object sender, EventArgs e)
    {
        //string jobName = "";

        //foreach (RadComboBoxItem radcbiSource in radcombo_job.CheckedItems)
        //{
        //    jobName += radcbiSource.Value + ",";
        //}
        //string query = " select distinct mj.jid,mj.jobname,(um.firstName+' '+um.lastName) as Projectmanager, programManagerId ,mj.startdate ,mj.enddate ,RT.rigtypename  as Rigname," +
        //       " (mj.primaryAddress1+', '+mj.primaryAddress2+' ,'+mj.primaryCity+' ,'+mj.primaryState+', '+mj.primaryCountry+'.') as Address   from" +
        //       " Users um,manageJobOrders mj,RigTypes RT where um.userId=mj.programManagerId and mj.bitActive='True'  and mj.rigtypeid=RT.rigtypeid and mj.status='Approved' " +
        //    "and (mj.jid in(select jobid from PrismJobAssignedAssets) or jid in(select jobid from PrismJobAssignedPersonals))";
        //if (jobName != "")
        //{
        //    jobName = jobName.Remove(jobName.LastIndexOf(","), 1);
        //    query += " and  mj.jid in(" + jobName + ")";
        //}
        //grid_rig.DataSource = SqlHelper.ExecuteDataset(GlobalConnetionString.ClientConnection(), CommandType.Text,  query).Tables[0];
        
        //grid_rig.DataBind();
        bindgrid();
    }
    public void bindgrid()
    {
        string jobName = "";

        foreach (RadComboBoxItem radcbiSource in radcombo_job.CheckedItems)
        {
            jobName += radcbiSource.Value + ",";
        }
        string query = " select distinct mj.jid,mj.jobname,(um.firstName+' '+um.lastName) as Projectmanager, programManagerId ,mj.startdate ,mj.enddate ,RT.rigtypename  as Rigname," +
               " (mj.primaryAddress1+', '+mj.primaryAddress2+' ,'+mj.primaryCity+' ,'+mj.primaryState+', '+mj.primaryCountry+'.') as Address   from" +
               " Users um,manageJobOrders mj,RigTypes RT where um.userId=mj.programManagerId and mj.bitActive='True'  and mj.rigtypeid=RT.rigtypeid and mj.status='Approved' " +
            "and (mj.jid in(select jobid from PrismJobAssignedAssets) or jid in(select jobid from PrismJobAssignedPersonals))";
        if (jobName != "")
        {
            jobName = jobName.Remove(jobName.LastIndexOf(","), 1);
            query += " and  mj.jid in(" + jobName + ")";
        }
        //string str_querystring = Request.QueryString["jobid"].ToString();
        //string strjname = str_querystring.Substring(0, str_querystring.LastIndexOf('-') - 1);
        //string query = "select distinct mj.jid,mj.jobname,mj.primaryLatLong,(um.firstName+' '+um.lastName) as Projectmanager, programManagerId ,mj.startdate ,mj.enddate ,RT.rigtypename  as Rigname," +
        //                                                   "(mj.primaryAddress1+', '+mj.primaryAddress2+' ,'+mj.primaryCity+' ,'+mj.primaryState+', '+mj.primaryCountry+'.') as Address   from " +
        //                                                   "Users um,manageJobOrders mj,RigTypes RT where um.userId=mj.programManagerId and mj.bitActive='True'  and mj.rigtypeid=RT.rigtypeid and mj.status='Approved'" +
        //                                                " and (mj.jid in(select jobid from PrismJobAssignedAssets) or mj.jid in(select jobid from PrismJobAssignedPersonals)) and mj.jobname='" + strjname + "'";
        DataTable dt_getresult = SqlHelper.ExecuteDataset(GlobalConnetionString.ClientConnection(), CommandType.Text, query).Tables[0];
        grid_rig.DataSource = dt_getresult;
        grid_rig.DataBind();


    }
}