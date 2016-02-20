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
public partial class Modules_Configuration_Manager_JobAssignments : System.Web.UI.Page
{
    DataTable dt_CompCurrLoc = SqlHelper.ExecuteDataset(GlobalConnetionString.ClientConnection(), CommandType.Text, "select * from PrismJobAssetAssignedComponents").Tables[0];
    protected void Page_Load(object sender, EventArgs e)
    {
        lbl_message.Text = "";
        if (!IsPostBack)
        {
            grdJobList.DataSource = jobOrders("", "");
            grdJobList.DataBind();
        }
    }
    protected void combo_job_SelectedIndexChanged(object o, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs e)
    {
        string query="select P.Id,PA.AssetName+' ('+SerialNumber+')' as  [AssetName] from Prism_Assets P,PrismAssetName PA  where P.AssetName=PA.ID and  P.id  in" +
           " (select AssetId from PrismJobAssignedAssets,manageJobOrders where jid=PrismJobAssignedAssets.JobId and status='Approved' and PrismJobAssignedAssets.JobId=" + combo_job.SelectedValue + ")";
        combo_assets.DataSource = SqlHelper.ExecuteDataset(GlobalConnetionString.ClientConnection(), CommandType.Text, query).Tables[0];
        combo_assets.DataBind();
    }
    protected void combo_newjob_SelectedIndexChanged(object o, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs e)
    {
        string query = "select P.Id,PA.AssetName+' ('+SerialNumber+')' as  [AssetName] from Prism_Assets P,PrismAssetName PA  where P.AssetName=PA.ID and  P.id  in" +
           " (select AssetId from PrismJobAssignedAssets,manageJobOrders where jid=PrismJobAssignedAssets.JobId and status='Approved'"+
           " and PrismJobAssignedAssets.JobId=" + combo_newjob.SelectedValue + ") and P.Id not in(select AssetId from PrismJobAssetAssignedComponents where JobId=" + combo_newjob.SelectedValue + " )";
        combo_newasset.DataSource = SqlHelper.ExecuteDataset(GlobalConnetionString.ClientConnection(), CommandType.Text, query).Tables[0];
        combo_newasset.DataBind();
    }    
    protected void combo_oldjob_SelectedIndexChanged(object o, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs e)
    {
        string query="select P.Id,PA.AssetName+' ('+SerialNumber+')' as  [AssetName] from Prism_Assets P,PrismAssetName PA  where P.AssetName=PA.ID and  P.id  in" +
           " (select AssetId from PrismJobAssignedAssets,manageJobOrders where jid=PrismJobAssignedAssets.JobId and status='Approved' and PrismJobAssignedAssets.JobId=" + combo_oldjob.SelectedValue + ")";
        combo_oldasset.DataSource = SqlHelper.ExecuteDataset(GlobalConnetionString.ClientConnection(), CommandType.Text, query).Tables[0];
        combo_oldasset.DataBind();
    }
    protected void combo_oldasset_SelectedIndexChanged(object o, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs e)
    {
        string query = "select PC.CompID,ComponentName+'('+Serialno+')'  as ComponentName from Prism_Components PC,Prism_ComponentNames PCN where PC.Componentid=PCN.componet_id" +
                    " and PC.CompID  in(select CompID from PrismJobAssetAssignedComponents where JobId=" + combo_oldjob.SelectedValue + " and AssetId="+combo_oldasset.SelectedValue+")";
        combo_ex_components.DataSource = SqlHelper.ExecuteDataset(GlobalConnetionString.ClientConnection(), CommandType.Text, query).Tables[0];
        combo_ex_components.DataBind();
    }    
    protected void combo_assets_SelectedIndexChanged(object o, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs e)
    {
        string query = " select PC.CompID,ComponentName+'('+Serialno+')'  as ComponentName from Prism_Components PC,Prism_ComponentNames PCN,manageJobOrders MJ,PrismJobAssetAssignedComponents PJAC where" +
                    " PC.Componentid=PCN.componet_id  and PJAC.CompID=PC.CompID and PJAC.JobId=MJ.jid "+
                    " and PC.CompID not in(select CompID from PrismJobAssetAssignedComponents) and PJAC.JobId=" + combo_job.SelectedValue + "";
        combo_Components.DataSource = SqlHelper.ExecuteDataset(GlobalConnetionString.ClientConnection(), CommandType.Text,query).Tables[0];
        combo_Components.DataBind();
    }
    protected void btn_movewarehouse_Click(object sender, EventArgs e)
    {
         SqlConnection db = new SqlConnection(GlobalConnetionString.ClientConnection());
        SqlTransaction transaction;
        db.Open();
        transaction = db.BeginTransaction();
        int maxassetID = 0;
        try
        {

            //Jobs with Assets and Components deletion
            for (int asset = 0; asset < combo_ex_components.Items.Count; asset++)
            {

                //maxassetID = Convert.ToInt32(dt_CompCurrLoc.Compute("Max ( Componet_Assign_ID ) ", "CompID = " + combo_ex_components.Items[asset] + "").ToString());
                //DataRow[] row_location = dt_CompCurrLoc.Select("Componet_Assign_ID=" + maxassetID);
                //lbl_locationame.Text = row_location[0]["CurrentLocationType"].ToString();
                if (combo_ex_components.Items[asset].Checked)
                {
                    try
                    {


                        string fromLocType = "", fromLocId = "";
                        string ToLocType = "", ToLocId = "";
                        string currLocType = "", currLocId = "";
                        string status = "";
                        string query_existassetloc = "SELECT top(1) * FROM    PrismComponentCurrentLocation WHERE   ComponentID=" + combo_ex_components.Items[asset].Value + " order by ComponentCurrentLocationID desc";
                        DataTable dt_existassetlocation = SqlHelper.ExecuteDataset(transaction, CommandType.Text, query_existassetloc).Tables[0];
                        DataTable dt_assetlocation = SqlHelper.ExecuteDataset(transaction, CommandType.Text, "select * from Prism_Components where CompID=" + combo_ex_components.Items[asset].Value + "").Tables[0];
                        if (dt_existassetlocation.Rows.Count == 0)
                        {
                            fromLocType = "WareHouse";
                            fromLocId = dt_assetlocation.Rows[0]["WarehouseId"].ToString();
                            currLocType = "WareHouse";
                            currLocId = dt_assetlocation.Rows[0]["WarehouseId"].ToString();
                        }
                        else
                        {
                            fromLocType = dt_existassetlocation.Rows[0]["CurrentLocationType"].ToString();
                            fromLocId = dt_existassetlocation.Rows[0]["CurrentLocationID"].ToString();

                            currLocType = "WareHouse";
                            currLocId = dt_assetlocation.Rows[0]["WarehouseId"].ToString();

                        }
                        ToLocType = "WareHouse";
                        ToLocId = dt_assetlocation.Rows[0]["WarehouseId"].ToString();
                        status = "Available";

                        string insert_locationtype = "insert into PrismComponentCurrentLocation(ComponentID,FromLocationType,FromLocationID,ToLocationType,ToLocationID,AssetStatus,CurrentLocationType,CurrentLocationID)values(" +
                            "" + combo_ex_components.Items[asset].Value + ",'" + fromLocType + "'," + fromLocId + ",'" + ToLocType + "'," + ToLocId + ",'" + status + "','" + currLocType + "','" + currLocId + "')";
                        SqlHelper.ExecuteNonQuery(transaction, CommandType.Text, insert_locationtype);


                        string query_remove = "";
                        query_remove="Delete PrismJobAssetAssignedComponents where JobId="+combo_oldjob.SelectedValue+" and"+
                            " AssetId=" + combo_oldasset.SelectedValue + " and CompID=" + combo_ex_components.Items[asset].Value+ "";
                        SqlHelper.ExecuteNonQuery(transaction, CommandType.Text, query_remove);
                        transaction.Commit();
                        string notificationsendtowhome = eventNotification.sendEventNotification("CMWH01");
                        if (notificationsendtowhome != "")
                        {
                            bool statusmove = eventNotification.sendEventNotificationWithEmail(notificationsendtowhome, "CMWH01", "JOB", combo_oldjob.SelectedValue, combo_oldjob.SelectedItem.Text,
                                   "", "", "ComponentMoveToWarehouse", "", combo_ex_components.Items[asset].Text);

                        }
                        lbl_message.ForeColor = Color.Green;
                        lbl_message.Text = "Selected Component(s) Moved to Warehouse Successfully";
                    }
                    catch (Exception ex) { transaction.Rollback(); }
                }




            }
        }
        catch (Exception ex) { transaction.Rollback(); }

    }    
    protected void btn_reassign_Click(object sender, EventArgs e)
    {
        SqlConnection db = new SqlConnection(GlobalConnetionString.ClientConnection());
        SqlTransaction transaction;
        db.Open();
        transaction = db.BeginTransaction();
        string queryjobassetUpdate = "", queryjobpersonalInsert = "";
        try
        {

            //Jobs with Assets and Components insertion
            for (int asset = 0; asset < combo_ex_components.Items.Count; asset++)
            {
                if (combo_ex_components.Items[asset].Checked)
                {
                    try
                    {
                        string fromlocationtype = "", fromlocationid = "", tolocationtype = "", tolocationid = "", currentlocationtype = "", currentlocationid = "", assetstatus = "", assetid = "";
                        queryjobassetUpdate = "Update PrismJobAssetAssignedComponents set JobId=" + combo_newjob.SelectedValue + ",AssetId=" + combo_newasset.SelectedValue + "," +
                            " ModifiedBy=" + Session["userId"].ToString() + "," +
                            " ModifiedDate='" + DatesbetweenDatatable.getdatetimeformat(DateTime.Now.ToString()) + "' where"+
                            " JobId=" + combo_oldjob.SelectedValue + " and AssetId=" + combo_oldasset.SelectedValue + " and CompID=" + combo_ex_components.Items[asset].Value + "";

                        SqlHelper.ExecuteNonQuery(transaction, CommandType.Text, queryjobassetUpdate);
                        string query_existassetloc = "SELECT top(1) * FROM    PrismComponentCurrentLocation WHERE   ComponentID=" + combo_ex_components.Items[asset].Value + " order by ComponentCurrentLocationID desc";
                        DataTable dt_existassetlocation = SqlHelper.ExecuteDataset(transaction, CommandType.Text, query_existassetloc).Tables[0];
                        DataTable dt_assetlocation = SqlHelper.ExecuteDataset(transaction, CommandType.Text, "select * from Prism_Components where CompID=" + combo_oldasset.SelectedValue + "").Tables[0];
                        if (dt_existassetlocation.Rows.Count == 0)
                        {
                            fromlocationtype = "WareHouse";
                            fromlocationid = dt_assetlocation.Rows[0]["WarehouseId"].ToString();
                            currentlocationtype = "WareHouse";
                            currentlocationid = dt_assetlocation.Rows[0]["WarehouseId"].ToString();
                        }
                        else
                        {
                            fromlocationtype = dt_existassetlocation.Rows[0]["ToLocationType"].ToString();
                            fromlocationid = dt_existassetlocation.Rows[0]["ToLocationID"].ToString();

                            currentlocationtype = dt_existassetlocation.Rows[0]["ToLocationType"].ToString();
                            currentlocationid = dt_existassetlocation.Rows[0]["ToLocationID"].ToString();

                        }
                        tolocationtype = "Job";
                        tolocationid = combo_job.SelectedValue.ToString();
                        assetstatus = "Available";

                        string insert_locationtype = "insert into PrismComponentCurrentLocation(ComponentID,FromLocationType,FromLocationID,ToLocationType,ToLocationID,AssetStatus,CurrentLocationType,CurrentLocationID)values(" +
                            "" + combo_ex_components.Items[asset].Value + ",'" + fromlocationtype + "'," + fromlocationid + ",'" + tolocationtype + "'," + tolocationid + ",'" + assetstatus + "','" + currentlocationtype + "','" + currentlocationid + "')";
                        SqlHelper.ExecuteNonQuery(transaction, CommandType.Text, insert_locationtype);
                        string notificationsendtowhome = eventNotification.sendEventNotification("CRA01");                       
                        if (notificationsendtowhome != "")
                        {
                            bool statusmove = eventNotification.sendEventNotificationWithEmail(notificationsendtowhome, "CRA01", "JOB", combo_oldjob.SelectedValue, combo_oldjob.SelectedItem.Text,
                                   "", "", "ComponentReassigned", "", combo_ex_components.Items[asset].Text);

                        }
                    }
                    catch (Exception ex) { transaction.Rollback(); }
                    finally
                    {
                    }

                }
            }

            SqlHelper.ExecuteNonQuery(transaction, CommandType.Text, "Update manageJobOrders set JobAssignedDate='" + DatesbetweenDatatable.getdatetimeformat(DateTime.Now.ToString()) + "' where jid='" + combo_newjob.SelectedValue + "'");
            transaction.Commit();
            lbl_message.ForeColor = Color.Green;
            lbl_message.Text = "Job Assignment Successfully Completed with Asset(s) and Components(s)";
            grdJobList.DataSource = jobOrders("", "");
            grdJobList.DataBind();

        }
        catch (Exception ex)
        {
            transaction.Rollback();
            lbl_message.ForeColor = Color.Red;
            lbl_message.Text = "Job assignment Failed";
        }
    }    
    protected void btn_save_Click(object sender, EventArgs e)
    {
        SqlConnection db = new SqlConnection(GlobalConnetionString.ClientConnection());
        SqlTransaction transaction;
        db.Open();
        transaction = db.BeginTransaction();
        string queryjobassetInsert = "", queryjobpersonalInsert = "";
        try
        {

            //Jobs with Assets and Components insertion
            for (int asset = 0; asset < combo_Components.Items.Count; asset++)
            {
                if (combo_Components.Items[asset].Checked)
                {
                    try
                    {
                        string fromlocationtype = "", fromlocationid = "", tolocationtype = "", tolocationid = "", currentlocationtype = "", currentlocationid = "", assetstatus = "", assetid = "";
                        queryjobassetInsert = "Insert into PrismJobAssetAssignedComponents(JobId,AssetId,CompID,ModifiedBy,ModifiedDate)" +
                           " values('" + combo_job.SelectedValue + "','"+combo_assets.SelectedValue+"','" + combo_Components.Items[asset].Value + "','" + Session["userId"].ToString() + "','" + DatesbetweenDatatable.getdatetimeformat(DateTime.Now.ToString()) + "')";
                        SqlHelper.ExecuteNonQuery(transaction, CommandType.Text, queryjobassetInsert);
                        string query_existassetloc = "SELECT top(1) * FROM    PrismComponentCurrentLocation WHERE   ComponentID=" + combo_Components.Items[asset].Value + " order by ComponentCurrentLocationID desc";
                        DataTable dt_existassetlocation = SqlHelper.ExecuteDataset(transaction, CommandType.Text, query_existassetloc).Tables[0];
                        DataTable dt_assetlocation = SqlHelper.ExecuteDataset(transaction, CommandType.Text, "select * from Prism_Components where CompID=" + combo_Components.Items[asset].Value + "").Tables[0];
                        if (dt_existassetlocation.Rows.Count == 0)
                        {
                            fromlocationtype = "WareHouse";
                            fromlocationid = dt_assetlocation.Rows[0]["WarehouseId"].ToString();
                            currentlocationtype = "WareHouse";
                            currentlocationid = dt_assetlocation.Rows[0]["WarehouseId"].ToString();
                        }
                        else
                        {
                            fromlocationtype = dt_existassetlocation.Rows[0]["ToLocationType"].ToString();
                            fromlocationid = dt_existassetlocation.Rows[0]["ToLocationID"].ToString();

                            currentlocationtype = dt_existassetlocation.Rows[0]["ToLocationType"].ToString();
                            currentlocationid = dt_existassetlocation.Rows[0]["ToLocationID"].ToString();
                            
                        }
                        tolocationtype = "Job";
                        tolocationid = combo_job.SelectedValue.ToString();
                        assetstatus = "Available";

                        string insert_locationtype = "insert into PrismComponentCurrentLocation(ComponentID,FromLocationType,FromLocationID,ToLocationType,ToLocationID," +
                            "AssetStatus,CurrentLocationType,CurrentLocationID)values(" +
                            "" + combo_assets.Items[asset].Value + ",'" + fromlocationtype + "'," + fromlocationid + ",'" + tolocationtype + "'," + tolocationid + ",'" + assetstatus + "','"+currentlocationtype+"','"+currentlocationid+"')";
                        SqlHelper.ExecuteNonQuery(transaction, CommandType.Text, insert_locationtype);
                        string notificationsendtowhome = eventNotification.sendEventNotification("CAA01");
                        if (notificationsendtowhome != "")
                        {
                            bool statusmove = eventNotification.sendEventNotificationWithEmail(notificationsendtowhome, "CAA01", "JOB", combo_job.SelectedValue, combo_job.SelectedItem.Text,
                                   "", "", "Componentassigned", "", combo_Components.Items[asset].Text);

                        }
                                               
                    }
                    catch (Exception ex) { transaction.Rollback(); }
                    finally
                    {
                    }

                }
            }
                   
            SqlHelper.ExecuteNonQuery(transaction, CommandType.Text, "Update manageJobOrders set JobAssignedDate='" + DatesbetweenDatatable.getdatetimeformat(DateTime.Now.ToString()) + "' where jid='" + combo_job.SelectedValue + "'");
            transaction.Commit();
            lbl_message.ForeColor = Color.Green;
            lbl_message.Text = "Job Assignment Successfully Completed with Asset(s) and Personal(s)";
            grdJobList.DataSource = jobOrders("", "");
            grdJobList.DataBind();
            
        }
        catch (Exception ex)
        {
            transaction.Rollback();
            lbl_message.ForeColor = Color.Red;
            lbl_message.Text = "Job assignment Failed";
        }
    }    
    public DataTable jobOrders(string startdate, string stopdate)
    {
        string query = "select  JO.jid,JO.jobid,jo.jobname,JT.jobtype,JO.startdate,JO.enddate,JO.JobAssignedDate, (opuser.firstName+' '+opuser.lastName) as operationsManager,(puser.firstName+' '+puser.lastName) as projectManager,"+
            " JO.approveddatetime as JoborderDate,cost from manageJobOrders JO,jobTypes JT,Users opuser, Users puser where puser.userID = JO.programManagerId " +
            " and opuser.userID = JO.opManagerId and JO.jobtype=JT.jobtypeid and jid in (select distinct JobId from PrismJobAssetAssignedComponents)";
        return SqlHelper.ExecuteDataset(GlobalConnetionString.ClientConnection(), CommandType.Text, query).Tables[0];
    }
    //protected void grdJobList_ItemCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
    //{
    //    //if IsertAnother button is clicked 
    //    if (e.CommandName == "Cancel" || e.CommandName == "Edit")
    //    {
    //        grdJobList.DataSource = jobOrders("", "");
    //        grdJobList.DataBind();
    //    }
    //}
    protected void grdJobList_PageIndexChanged(object source, GridPageChangedEventArgs e)
    {
        grdJobList.CurrentPageIndex = e.NewPageIndex;
        grdJobList.DataSource = jobOrders("", "");
        grdJobList.DataBind();
    }
    //protected void gridJobAssets_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
    //{
        
    //    GridDataItem parentItem = ((source as RadGrid).NamingContainer as GridNestedViewItem).ParentItem as GridDataItem;
    //    (source as RadGrid).DataSource = SqlHelper.ExecuteDataset(GlobalConnetionString.ClientConnection(), CommandType.Text,
    //        "select A.AssetId,AssetName,SerialNumber,clientAssetName,WA.Name as Warehouse,(u.firstName+' '+u.lastName) as username,JA.ModifiedDate,JA.AssetStatus "+
    //        " from Prism_Assets A,PrismJobAssignedAssets JA,PrsimWarehouses WA,clientAssets AT,Users u,manageJobOrders MJ where AT.clientAssetID=A.AssetCategoryId and"+
    //        " WA.ID=A.WarehouseId  and MJ.jid=JA.JobId and JA.AssetId=A.Id and" +
    //        " JA.ModifiedBy=u.userID and JA.JobId=" + parentItem.GetDataKeyValue("jid").ToString() + "").Tables[0];
    //    lbl_message.Text = parentItem.GetDataKeyValue("jid").ToString();
        
    //}
    //protected void gridJobPersonals_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
    //{
    //    GridDataItem parentItem = ((source as RadGrid).NamingContainer as GridNestedViewItem).ParentItem as GridDataItem;
    //    (source as RadGrid).DataSource = SqlHelper.ExecuteDataset(GlobalConnetionString.ClientConnection(), CommandType.Text,
    //        "select (u.firstName+' '+u.lastName) as username from PrismJobAssignedPersonals PA,Users u where PA.UserId=u.UserId  and PA.JobId=" + parentItem.GetDataKeyValue("jid").ToString() + "").Tables[0];
    //    lbl_message.Text = parentItem.GetDataKeyValue("jid").ToString();
    //}
    protected void gridJobAssets_ItemCommand(object source, GridCommandEventArgs e)
    {

        if (e.CommandName == RadGrid.ExpandCollapseCommandName)
        {

            GridDataItem item = e.Item as GridDataItem;

            if (!item.Expanded)
            {

                GridNestedViewItem nestedItem = (GridNestedViewItem)item.ChildItem;
                string dataKeyValueassetid = Convert.ToString(((GridDataItem)(nestedItem.ParentItem)).GetDataKeyValue("Assetinfoid"));
                string dataKeyValuejid = Convert.ToString(((GridDataItem)(nestedItem.ParentItem)).GetDataKeyValue("jid"));

                RadGrid gridJobComponents = (RadGrid)nestedItem.FindControl("gridJobComponents");
                gridJobComponents.DataSource = SqlHelper.ExecuteDataset(GlobalConnetionString.ClientConnection(), CommandType.Text,
            "select PC.CompID,ComponentName,PCN.Description,WA.Name as Warehousename,comp_categoryname, Serialno,PC.Type as componenttype,Make,PC.Cost as compcost from PrsimWarehouses WA ," +
            " Prism_Components PC,Prism_ComponentNames PCN,PrismJobAssetAssignedComponents PJAC,manageJobOrders MJ,Prism_ComponentCategory PCC"+
            " where PC.Componentid=PCN.componet_id and PJAC.CompID=PC.CompID and PJAC.JobId=MJ.jid and PCC.comp_categoryid=PC.Comp_Categoryid"+
            " and WA.ID=PC.Warehouseid and PJAC.JobId=" + dataKeyValuejid + " and PJAC.AssetId="+dataKeyValueassetid+"").Tables[0];

                gridJobComponents.DataBind();
            }
        }
    }
    
    protected void grdJobList_ItemCommand(object source, GridCommandEventArgs e)
    {

        if (e.CommandName == RadGrid.ExpandCollapseCommandName)
        {

            GridDataItem item = e.Item as GridDataItem;

            if (!item.Expanded)
            {

                GridNestedViewItem nestedItem = (GridNestedViewItem)item.ChildItem;

                string dataKeyValue = Convert.ToString(((GridDataItem)(nestedItem.ParentItem)).GetDataKeyValue("jid"));

                //RadGrid gridJobComponents = (RadGrid)nestedItem.FindControl("gridJobComponents");
                RadGrid gridJobAssets = (RadGrid)nestedItem.FindControl("gridJobAssets");


                string selectq = "select A.Id as Assetinfoid, MJ.jid,A.AssetId,PA.AssetName,SerialNumber,clientAssetName,WA.Name as Warehouse,(u.firstName+' '+u.lastName) as username,JA.ModifiedDate,JAS.StatusText as AssetStatus   " +
            " from Prism_Assets A,PrismJobAssignedAssets JA,PrsimWarehouses WA,clientAssets AT,Users u,manageJobOrders MJ,PrsimJobAssetStatus JAS,PrismAssetName PA where AT.clientAssetID=A.AssetCategoryId and" +
            " WA.ID=A.WarehouseId  and MJ.jid=JA.JobId and JA.AssetId=A.Id  and JAS.Id=JA.AssetStatus and" +
            " JA.ModifiedBy=u.userID and A.AssetName=PA.ID And JA.JobId=" + dataKeyValue + " and A.Id in (select AssetId from PrismJobAssetAssignedComponents where JobId=" + dataKeyValue + ")";
                gridJobAssets.DataSource = SqlHelper.ExecuteDataset(GlobalConnetionString.ClientConnection(), CommandType.Text, selectq).Tables[0];
               gridJobAssets.DataBind();


            }

        }

    }
    protected void grdJobList_SortCommand(object source, GridSortCommandEventArgs e)
    {
        //Default sort order Descending

        
        if (!e.Item.OwnerTableView.SortExpressions.ContainsExpression(e.SortExpression))
        {
            GridSortExpression sortExpr = new GridSortExpression();
            sortExpr.FieldName = e.SortExpression;
            sortExpr.SortOrder = GridSortOrder.Ascending;

            e.Item.OwnerTableView.SortExpressions.AddSortExpression(sortExpr);
            grdJobList.DataSource = jobOrders("", "");
            grdJobList.DataBind();
        }
        else
        {
            GridSortExpression sortExpr = new GridSortExpression();
            sortExpr.FieldName = e.SortExpression;
            sortExpr.SortOrder = GridSortOrder.Descending;

            e.Item.OwnerTableView.SortExpressions.AddSortExpression(sortExpr);
            grdJobList.DataSource = jobOrders("", "");
            grdJobList.DataBind();
        }
    }
}