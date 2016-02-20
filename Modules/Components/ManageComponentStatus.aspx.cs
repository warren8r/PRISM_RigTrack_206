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
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            grdJobList.DataSource =  jobOrders(combo_job.SelectedValue,combo_assets.SelectedValue);
            grdJobList.DataBind();
        }
    }
    protected void combo_job_SelectedIndexChanged(object o, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs e)
    {
        string query = "select P.Id,PA.AssetName+' ('+SerialNumber+')' as  [AssetName] from Prism_Assets P,PrismAssetName PA  where P.AssetName=PA.ID and  P.id  in" +
           " (select AssetId from PrismJobAssignedAssets,manageJobOrders where jid=PrismJobAssignedAssets.JobId and status='Approved' and PrismJobAssignedAssets.JobId=" + combo_job.SelectedValue + ")";
        combo_assets.DataSource = SqlHelper.ExecuteDataset(GlobalConnetionString.ClientConnection(), CommandType.Text, query).Tables[0];
        combo_assets.DataBind();
    }    
    protected void btn_save_Click(object sender, EventArgs e)
    {
        //SqlConnection db = new SqlConnection(GlobalConnetionString.ClientConnection());
        //SqlTransaction transaction;
        //db.Open();
        //transaction = db.BeginTransaction();
        //string queryjobassetInsert = "", queryjobpersonalInsert = "";
        //try
        //{

        //    //Jobs with Assets and Components insertion
        //    for (int asset = 0; asset < combo_Components.Items.Count; asset++)
        //    {
        //        if (combo_Components.Items[asset].Checked)
        //        {
        //            try
        //            {
        //                string fromlocationtype = "", fromlocationid = "", tolocationtype = "", tolocationid = "", currentlocationtype = "", currentlocationid = "", assetstatus = "", assetid = "";
        //                queryjobassetInsert = "Insert into PrismJobAssetAssignedComponents(JobId,AssetId,CompID,ModifiedBy,ModifiedDate)" +
        //                   " values('" + combo_job.SelectedValue + "','" + combo_assets.SelectedValue + "','" + combo_Components.Items[asset].Value + "','" + Session["userId"].ToString() + "','" + DatesbetweenDatatable.getdatetimeformat(DateTime.Now.ToString()) + "')";
        //                SqlHelper.ExecuteNonQuery(transaction, CommandType.Text, queryjobassetInsert);
        //                string query_existassetloc = "SELECT top(1) * FROM    PrismComponentCurrentLocation WHERE   AssetID=" + combo_assets.Items[asset].Value + " order by ComponentCurrentLocationID desc";
        //                DataTable dt_existassetlocation = SqlHelper.ExecuteDataset(transaction, CommandType.Text, query_existassetloc).Tables[0];
        //                DataTable dt_assetlocation = SqlHelper.ExecuteDataset(transaction, CommandType.Text, "select * from Prism_Assets where ID=" + combo_assets.Items[asset].Value + "").Tables[0];
        //                if (dt_existassetlocation.Rows.Count == 0)
        //                {
        //                    fromlocationtype = "WareHouse";
        //                    fromlocationid = dt_assetlocation.Rows[0]["WarehouseId"].ToString();
        //                    currentlocationtype = "WareHouse";
        //                    currentlocationid = dt_assetlocation.Rows[0]["WarehouseId"].ToString();
        //                }
        //                else
        //                {
        //                    fromlocationtype = dt_existassetlocation.Rows[0]["ToLocationType"].ToString();
        //                    fromlocationid = dt_existassetlocation.Rows[0]["ToLocationID"].ToString();

        //                    currentlocationtype = dt_existassetlocation.Rows[0]["ToLocationType"].ToString();
        //                    currentlocationid = dt_existassetlocation.Rows[0]["ToLocationID"].ToString();

        //                }
        //                tolocationtype = "Job";
        //                tolocationid = combo_job.SelectedValue.ToString();
        //                assetstatus = "Available";

        //                string insert_locationtype = "insert into PrismComponentCurrentLocation(AssetID,FromLocationType,FromLocationID,ToLocationType,ToLocationID,AssetStatus,CurrentLocationType,CurrentLocationID)values(" +
        //                    "" + combo_assets.Items[asset].Value + ",'" + fromlocationtype + "'," + fromlocationid + ",'" + tolocationtype + "'," + tolocationid + ",'" + assetstatus + "','" + currentlocationtype + "','" + currentlocationid + "')";
        //                SqlHelper.ExecuteNonQuery(transaction, CommandType.Text, insert_locationtype);

        //            }
        //            catch (Exception ex) { transaction.Rollback(); }
        //            finally
        //            {
        //            }

        //        }
        //    }

        //    SqlHelper.ExecuteNonQuery(transaction, CommandType.Text, "Update manageJobOrders set JobAssignedDate='" + DatesbetweenDatatable.getdatetimeformat(DateTime.Now.ToString()) + "' where jid='" + combo_job.SelectedValue + "'");
        //    transaction.Commit();
        //    lbl_message.ForeColor = Color.Green;
        //    lbl_message.Text = "Job Assignment Successfully Completed with Asset(s) and Personal(s)";
        //    grdJobList.DataSource =  jobOrders(combo_job.SelectedValue,combo_assets.SelectedValue);
        //    grdJobList.DataBind();

        //}
        //catch (Exception ex)
        //{
        //    transaction.Rollback();
        //    lbl_message.ForeColor = Color.Red;
        //    lbl_message.Text = "Job assignment Failed";
        //}
    }
    public DataTable jobOrders(string jobid, string assetid)
    {
        string query = "select  JO.jid,JO.jobid,jo.jobname,JT.jobtype,JO.startdate,JO.enddate,JO.JobAssignedDate, (opuser.firstName+' '+opuser.lastName) as operationsManager,(puser.firstName+' '+puser.lastName) as projectManager," +
            " JO.approveddatetime as JoborderDate,cost from manageJobOrders JO,jobTypes JT,Users opuser, Users puser where puser.userID = JO.programManagerId " +
            " and opuser.userID = JO.opManagerId and JO.jobtype=JT.jobtypeid and jid in (select distinct JobId from PrismJobAssetAssignedComponents";
        if (jobid != "" && assetid!="")
        {
            query += " where JobId=" + jobid + " and AssetId=" + assetid + "";
        }
        else if (assetid != "" && jobid=="")
        {
            query += " where AssetId=" + assetid + "";
        }
        else if (assetid == "" && jobid != "")
        {
            query += " where JobId=" + jobid + "";
        }
        query += ")";
        return SqlHelper.ExecuteDataset(GlobalConnetionString.ClientConnection(), CommandType.Text, query).Tables[0];
    }
    //protected void grdJobList_ItemCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
    //{
    //    //if IsertAnother button is clicked 
    //    if (e.CommandName == "Cancel" || e.CommandName == "Edit")
    //    {
    //        grdJobList.DataSource =  jobOrders(combo_job.SelectedValue,combo_assets.SelectedValue);
    //        grdJobList.DataBind();
    //    }
    //}
    protected void grdJobList_PageIndexChanged(object source, GridPageChangedEventArgs e)
    {
        grdJobList.CurrentPageIndex = e.NewPageIndex;
        grdJobList.DataSource =  jobOrders(combo_job.SelectedValue,combo_assets.SelectedValue);
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
            "select PC.CompID,ComponentName,PCN.Description,WA.Name as Warehousename,comp_categoryname, Serialno,PC.Type as componenttype,Make,"+
            " PC.Cost as compcost,PJAC.Componentstatus,PJAC.AssetId,PJAC.JobId from PrsimWarehouses WA ," +
            " Prism_Components PC,Prism_ComponentNames PCN,PrismJobAssetAssignedComponents PJAC,manageJobOrders MJ,Prism_ComponentCategory PCC" +
            " where PC.Componentid=PCN.componet_id and PJAC.CompID=PC.CompID and PJAC.JobId=MJ.jid and PCC.comp_categoryid=PC.Comp_Categoryid" +
            " and WA.ID=PC.Warehouseid and PJAC.JobId=" + dataKeyValuejid + " and PJAC.AssetId=" + dataKeyValueassetid + "").Tables[0];

                gridJobComponents.DataBind();
            }
        }
    }
    
    protected void btn_save_OnClick(object sender, EventArgs e)
    {
        //gridJobAssets
        foreach (GridNestedViewItem item1 in grdJobList.MasterTableView.GetItems(GridItemType.NestedView)) // loop through the nested items of a NestedView Template 
        {
            RadGrid gridasset = (RadGrid)item1.FindControl("gridJobAssets");
            
            foreach (GridNestedViewItem assetitem in gridasset.MasterTableView.GetItems(GridItemType.NestedView)) // loop through the nested items of a NestedView Template 
            {
                
                RadGrid grid = (RadGrid)assetitem.FindControl("gridJobComponents");
            string dataKeyValue = Convert.ToString(((GridDataItem)(assetitem.ParentItem)).GetDataKeyValue("jid"));
            string dataKeyValueasset = Convert.ToString(((GridDataItem)(assetitem.ParentItem)).GetDataKeyValue("Assetinfoid"));
                
            RadComboBox radcombo_type = (RadComboBox)assetitem.FindControl("radcombo_type");
            if (radcombo_type.SelectedValue != "")
            {
                foreach (GridDataItem i in grid.Items)
                {
                    CheckBox chk_select = (CheckBox)i.FindControl("chk_select");
                    Label AssetAssignid = (Label)i.FindControl("lbl_assetassignedid");
                    Label lbl_assetid = (Label)i.FindControl("lbl_assetid");
                    Label lbl_CompID = (Label)i.FindControl("lbl_CompID");
                    Label lbl_AssetName = (Label)i.FindControl("lbl_AssetName");
                    if (chk_select.Checked)
                    {
                        string query_insert = "Update PrismJobAssetAssignedComponents set" +
                    " ModifiedDate='" + DatesbetweenDatatable.getdatetimeformat(DateTime.Now.ToString()) + "',ModifiedBy='" + Session["userId"].ToString() + "',Componentstatus=" + radcombo_type.SelectedValue + " where CompID=" + lbl_CompID.Text + "";
                        SqlHelper.ExecuteNonQuery(GlobalConnetionString.ClientConnection(), CommandType.Text, query_insert);


                        string fromlocationtype = "", fromlocationid = "", currentlocationtype = "", currentlocationid = "", assetstatus = "", assetcurrentlocid = "";

                        string query_existassetloc = "SELECT top(1) * FROM    PrismComponentCurrentLocation WHERE  ComponentID=" + lbl_CompID.Text + " order by ComponentCurrentLocationID desc";
                        DataTable dt_existassetlocation = SqlHelper.ExecuteDataset(GlobalConnetionString.ClientConnection(), CommandType.Text, query_existassetloc).Tables[0];
                        //DataTable dt_assetlocation = SqlHelper.ExecuteDataset(GlobalConnetionString.ClientConnection(), CommandType.Text, "select * from Prism_Assets where ID=" + lbl_assetid.Text + "").Tables[0];
                        if (dt_existassetlocation.Rows.Count > 0)
                        {
                            if (radcombo_type.SelectedValue == "3")
                            {
                                assetcurrentlocid = dt_existassetlocation.Rows[0]["ComponentCurrentLocationID"].ToString();
                                currentlocationtype = dt_existassetlocation.Rows[0]["ToLocationType"].ToString();
                                currentlocationid = dt_existassetlocation.Rows[0]["ToLocationID"].ToString();

                                assetstatus = "In Use";

                                string update_locationtype = "update PrismComponentCurrentLocation set AssetStatus='" + assetstatus + "'," +
                                    " CurrentLocationType='" + currentlocationtype + "',CurrentLocationID='" + currentlocationid + "' , ComponentMovedDate='" + DatesbetweenDatatable.getdatetimeformat(DateTime.Now.ToString()) + "' where ComponentCurrentLocationID=" + assetcurrentlocid + "";

                                SqlHelper.ExecuteNonQuery(GlobalConnetionString.ClientConnection(), CommandType.Text, update_locationtype);
                                string notificationsendtowhome = eventNotification.sendEventNotification("CSSNI01");
                                if (notificationsendtowhome != "")
                                {
                                    bool status = eventNotification.sendEventNotificationWithEmail(notificationsendtowhome, "CSSNI01", "JOB", combo_job.SelectedValue, combo_job.SelectedItem.Text,
                                           "", "", "ComponentStatusNotInUse", "", lbl_AssetName.Text);

                                }
                            }
                            else
                            {
                                assetcurrentlocid = dt_existassetlocation.Rows[0]["ComponentCurrentLocationID"].ToString();
                                currentlocationtype = dt_existassetlocation.Rows[0]["FromLocationType"].ToString();
                                currentlocationid = dt_existassetlocation.Rows[0]["FromLocationID"].ToString();

                                assetstatus = "Available";

                                string update_locationtype = "update PrismComponentCurrentLocation set AssetStatus='" + assetstatus + "'," +
                                    " CurrentLocationType='" + currentlocationtype + "',CurrentLocationID='" + currentlocationid + "' ,ComponentMovedDate='' where ComponentCurrentLocationID=" + assetcurrentlocid + "";

                                SqlHelper.ExecuteNonQuery(GlobalConnetionString.ClientConnection(), CommandType.Text, update_locationtype);
                                string notificationsendtowhome = eventNotification.sendEventNotification("CSSA01");
                                if (notificationsendtowhome != "")
                                {
                                    bool status = eventNotification.sendEventNotificationWithEmail(notificationsendtowhome, "CSSA01", "JOB", combo_job.SelectedValue, combo_job.SelectedItem.Text,
                                           "", "", "ComponentStatusAvailable", "", lbl_AssetName.Text);

                                }
                            }

                        }



                    }
                }
            }
            //grid.Rebind();
            grid.DataSource = SqlHelper.ExecuteDataset(GlobalConnetionString.ClientConnection(), CommandType.Text,
             "select PC.CompID,ComponentName,PCN.Description,WA.Name as Warehousename,comp_categoryname, Serialno,PC.Type as componenttype," +
             " Make,PC.Cost as compcost,PJAC.Componentstatus,PJAC.AssetId,PJAC.JobId from PrsimWarehouses WA ," +
             " Prism_Components PC,Prism_ComponentNames PCN,PrismJobAssetAssignedComponents PJAC,manageJobOrders MJ,Prism_ComponentCategory PCC" +
             " where PC.Componentid=PCN.componet_id and PJAC.CompID=PC.CompID and PJAC.JobId=MJ.jid and PCC.comp_categoryid=PC.Comp_Categoryid" +
             " and WA.ID=PC.Warehouseid and PJAC.JobId=" + dataKeyValue + " and PJAC.AssetId=" + dataKeyValueasset + "").Tables[0];

            grid.DataBind();
            //radcombo_type.SelectedIndex=-1;
        }
        }

        //GridNestedViewItem nesteditem = (GridNestedViewItem)grdJobList.MasterTableView.GetItems(GridItemType.NestedView)[0];
        //CheckBox chk_select = (CheckBox)nesteditem.FindControl("chk_select");

    }
    protected void gridJobComponents_ItemDataBound(object sender, GridItemEventArgs e)
    {
        RadGrid grid = (RadGrid)sender;
        if (e.Item is GridEditableItem && e.Item.IsInEditMode)
        {
            GridEditableItem item = e.Item as GridEditableItem;
            DropDownList cmb = (DropDownList)item["StatusText"].Controls[0];
            string ss = cmb.SelectedValue;
        }
        //else if (e.Item is GridDataItem)
        //{
        //    GridDataItem item1 = (GridDataItem)e.Item;
        //    Label lbl_compstatus = (Label)item1.FindControl("lbl_compstatus");
        //    if (lbl_compstatus.Text == "")
        //    {
        //        DropDownList cmb = (DropDownList)item1["StatusText"].Controls[0];
        //        cmb.SelectedValue = "1";
        //        //
        //        //string ss = cmb.SelectedValue;
        //    }
        //    else
        //    {

        //    }
        //}
        
        //else if(e.Item is GridEditableItem)
    }
    protected void gridJobComponents_ItemCommand(object source, GridCommandEventArgs e)
    {
        //Code for During Add,Edit can no happen and vice-versa 
        bool editMode = e.Item.OwnerTableView.OwnerGrid.EditIndexes.Count > 0;
        bool insertMode = e.Item.OwnerTableView.IsItemInserted;
        RadGrid gridJobComponents = (source as RadGrid);
        GridEditableItem item = e.Item as GridEditableItem;
        string dataKeyValue = Convert.ToString(((GridDataItem)(item)).GetDataKeyValue("CompID"));

        Label lbl_CompID = (Label)item.FindControl("lbl_CompID");
        Label lbl_jid = (Label)item.FindControl("lbl_jid");
        Label lbl_assetid = (Label)item.FindControl("lbl_assetid");

        if (e.CommandName == "Update")
        {

            if (e.Item is GridEditableItem && e.Item.IsInEditMode)
            {
                DropDownList cmb = (DropDownList)item["StatusText"].Controls[0];

                string query_insert = "Update PrismJobAssetAssignedComponents set" +
                 " ModifiedDate='" + DatesbetweenDatatable.getdatetimeformat(DateTime.Now.ToString()) + "',ModifiedBy='" + Session["userId"].ToString() + "',Componentstatus=" + cmb.SelectedValue + " where CompID=" + lbl_CompID.Text + "";
                SqlHelper.ExecuteNonQuery(GlobalConnetionString.ClientConnection(), CommandType.Text, query_insert);

                //SqlHelper.ExecuteNonQuery(GlobalConnetionString.ClientConnection(), CommandType.Text, "Update PrismJobAssetAssignedComponents set" +
                //    " ModifiedDate='" + DatesbetweenDatatable.getdatetimeformat(DateTime.Now.ToString()) + "',ModifiedBy='" + Session["userId"].ToString() + "',Componentstatus=" + cmb.SelectedValue + " where Componet_Assign_ID=" + dataKeyValue + "");


                string fromlocationtype = "", fromlocationid = "", currentlocationtype = "", currentlocationid = "", assetstatus = "", assetcurrentlocid = "";

                string query_existassetloc = "SELECT top(1) * FROM    PrismComponentCurrentLocation WHERE  ComponentID=" + lbl_CompID.Text + " order by ComponentCurrentLocationID desc";
                DataTable dt_existassetlocation = SqlHelper.ExecuteDataset(GlobalConnetionString.ClientConnection(), CommandType.Text, query_existassetloc).Tables[0];
                //DataTable dt_assetlocation = SqlHelper.ExecuteDataset(GlobalConnetionString.ClientConnection(), CommandType.Text, "select * from Prism_Assets where ID=" + lbl_assetid.Text + "").Tables[0];
                if (dt_existassetlocation.Rows.Count > 0)
                {
                    if (cmb.SelectedValue == "3")
                    {
                        assetcurrentlocid = dt_existassetlocation.Rows[0]["ComponentCurrentLocationID"].ToString();
                        currentlocationtype = dt_existassetlocation.Rows[0]["ToLocationType"].ToString();
                        currentlocationid = dt_existassetlocation.Rows[0]["ToLocationID"].ToString();

                        assetstatus = "In Use";

                        string update_locationtype = "update PrismComponentCurrentLocation set AssetStatus='" + assetstatus + "'," +
                            " CurrentLocationType='" + currentlocationtype + "',CurrentLocationID='" + currentlocationid + "' , ComponentMovedDate='" + DatesbetweenDatatable.getdatetimeformat(DateTime.Now.ToString()) + "' where ComponentCurrentLocationID=" + assetcurrentlocid + "";

                        SqlHelper.ExecuteNonQuery(GlobalConnetionString.ClientConnection(), CommandType.Text, update_locationtype);
                    }
                    else
                    {
                        assetcurrentlocid = dt_existassetlocation.Rows[0]["ComponentCurrentLocationID"].ToString();
                        currentlocationtype = dt_existassetlocation.Rows[0]["FromLocationType"].ToString();
                        currentlocationid = dt_existassetlocation.Rows[0]["FromLocationID"].ToString();

                        assetstatus = "Available";

                        string update_locationtype = "update PrismComponentCurrentLocation set AssetStatus='" + assetstatus + "'," +
                            " CurrentLocationType='" + currentlocationtype + "',CurrentLocationID='" + currentlocationid + "' ,ComponentMovedDate='' where ComponentCurrentLocationID=" + assetcurrentlocid + "";

                        SqlHelper.ExecuteNonQuery(GlobalConnetionString.ClientConnection(), CommandType.Text, update_locationtype);
                    }

                }
            }

        }
        gridJobComponents.DataSource = SqlHelper.ExecuteDataset(GlobalConnetionString.ClientConnection(), CommandType.Text,
           "select PC.CompID,ComponentName,PCN.Description,WA.Name as Warehousename,comp_categoryname, Serialno,PC.Type as componenttype,"+
           " Make,PC.Cost as compcost,PJAC.Componentstatus,PJAC.AssetId,PJAC.JobId from PrsimWarehouses WA ," +
           " Prism_Components PC,Prism_ComponentNames PCN,PrismJobAssetAssignedComponents PJAC,manageJobOrders MJ,Prism_ComponentCategory PCC" +
           " where PC.Componentid=PCN.componet_id and PJAC.CompID=PC.CompID and PJAC.JobId=MJ.jid and PCC.comp_categoryid=PC.Comp_Categoryid" +
           " and WA.ID=PC.Warehouseid and PJAC.JobId=" + lbl_jid.Text + " and PJAC.AssetId=" + lbl_assetid.Text + "").Tables[0];

        gridJobComponents.DataBind();


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
            " JA.ModifiedBy=u.userID and A.AssetName=PA.ID And JA.JobId=" + dataKeyValue + " and A.Id in"+
            " (select AssetId from PrismJobAssetAssignedComponents where JobId=" + dataKeyValue + "";
                if (combo_assets.SelectedValue != "")
                {
                    selectq += " and AssetId=" + combo_assets.SelectedValue + "";
                }
                selectq += ")";
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
            grdJobList.DataSource =  jobOrders(combo_job.SelectedValue,combo_assets.SelectedValue);
            grdJobList.DataBind();
        }
        else
        {
            GridSortExpression sortExpr = new GridSortExpression();
            sortExpr.FieldName = e.SortExpression;
            sortExpr.SortOrder = GridSortOrder.Descending;

            e.Item.OwnerTableView.SortExpressions.AddSortExpression(sortExpr);
            grdJobList.DataSource =  jobOrders(combo_job.SelectedValue,combo_assets.SelectedValue);
            grdJobList.DataBind();
        }
    }
    protected void btn_view_Click(object sender, EventArgs e)
    {
        grdJobList.DataSource = grdJobList.DataSource = jobOrders(combo_job.SelectedValue,combo_assets.SelectedValue);
        grdJobList.DataBind();
    }
}