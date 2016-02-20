using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Telerik.Web.UI;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Diagnostics;


public partial class Modules_Configuration_Manager_AssetRepairStatus : System.Web.UI.Page
{
    public static DataTable dt_job = new DataTable();
    public static DataTable dt_warehouse = new DataTable();
    public static DataTable dt_AssetCurrLoc = new DataTable();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            radcombo_rstatus.SelectedValue = "0";
            dt_job = SqlHelper.ExecuteDataset(GlobalConnetionString.ClientConnection(), CommandType.Text, "Select * from manageJobOrders").Tables[0];
            dt_warehouse = SqlHelper.ExecuteDataset(GlobalConnetionString.ClientConnection(), CommandType.Text, "Select * from PrsimWarehouses").Tables[0];
            dt_AssetCurrLoc = SqlHelper.ExecuteDataset(GlobalConnetionString.ClientConnection(), CommandType.Text, "select * from PrismAssetCurrentLocation").Tables[0];            
            bind();

        }
    }
    protected void CheckChanged(object sender, EventArgs e)
    {
        if (hidd_asset.Value == "1")
        {
            string updatequery = "", insertquery = "", updatequery_Repair = "", jobAssetUpdateQuery = "";
            GridDataItem row = (GridDataItem)(((CheckBox)sender).NamingContainer);
            Label lbl_assetid = (Label)row.FindControl("lbl_assetid");
            Label lbl_statuscheck = (Label)row.FindControl("lbl_statuscheck");

            if (lbl_statuscheck.Text != "Ok")
            {
                updatequery = "update Prism_Assets set repairstatus='Ok' where Id=" + lbl_assetid.Text + "";
                DataTable dt_exist = SqlHelper.ExecuteDataset(GlobalConnetionString.ClientConnection(), CommandType.Text, "select max(AssetRid) as AssetRid from PrismAssetRepairStatus where assetid=" + lbl_assetid.Text + "").Tables[0];
                updatequery_Repair = "update PrismAssetRepairStatus set repairfixdate='" + DatesbetweenDatatable.getdatetimeformat(DateTime.Now.ToString()) + "' where AssetRid=" + dt_exist.Rows[0]["AssetRid"].ToString() + "";
                SqlHelper.ExecuteNonQuery(GlobalConnetionString.ClientConnection(), CommandType.Text, updatequery_Repair);
            }
            else
            {
                updatequery = "update Prism_Assets set repairstatus='Maintenance' where Id=" + lbl_assetid.Text + "";
                insertquery = "insert into PrismAssetRepairStatus(assetid,repairdate,status)values(" + lbl_assetid.Text + "," +
                        "'" + DatesbetweenDatatable.getdatetimeformat(DateTime.Now.ToString()) + "','Pending')";
                SqlHelper.ExecuteNonQuery(GlobalConnetionString.ClientConnection(), CommandType.Text, insertquery);
                DataTable dt_existprismjobassignedassets = SqlHelper.ExecuteDataset(GlobalConnetionString.ClientConnection(), CommandType.Text, "select * from PrismJobAssignedAssets where assignmentstatus='Active' and assetid='" + lbl_assetid.Text + "'").Tables[0];
                if (dt_existprismjobassignedassets.Rows.Count > 0)
                {
                    jobAssetUpdateQuery = "update PrismJobAssignedAssets set AssignmentStatus='InActive' where assetid=" + lbl_assetid.Text + " and AssignmentStatus='Active'";
                    SqlHelper.ExecuteNonQuery(GlobalConnetionString.ClientConnection(), CommandType.Text, jobAssetUpdateQuery);


                    string fromlocationtype = "", fromlocationid = "", tolocationtype = "", tolocationid = "", assetstatus = "", assetcurrentlocid = "", currentlocationtype = "", currentlocationid = "";

                    string query_existassetloc = "SELECT top(1) * FROM    PrismAssetCurrentLocation WHERE    AssetID=" + lbl_assetid.Text + " order by AssetCurrentLocationID desc";
                    DataTable dt_existassetlocation = SqlHelper.ExecuteDataset(GlobalConnetionString.ClientConnection(), CommandType.Text, query_existassetloc).Tables[0];
                    DataTable dt_assetlocation = SqlHelper.ExecuteDataset(GlobalConnetionString.ClientConnection(), CommandType.Text, "select * from Prism_Assets where ID=" + lbl_assetid.Text + "").Tables[0];

                    if (dt_existassetlocation.Rows.Count > 0)
                    {

                        fromlocationtype = dt_existassetlocation.Rows[0]["ToLocationType"].ToString();
                        fromlocationid = dt_existassetlocation.Rows[0]["ToLocationID"].ToString();


                        assetcurrentlocid = dt_existassetlocation.Rows[0]["AssetCurrentLocationID"].ToString();
                        tolocationtype = "WareHouse";
                        tolocationid = dt_assetlocation.Rows[0]["WarehouseId"].ToString();
                        currentlocationtype = "WareHouse";
                        currentlocationid = dt_assetlocation.Rows[0]["WarehouseId"].ToString();
                        assetstatus = "Available";

                        string insert_locationtype = "insert into PrismAssetCurrentLocation(AssetID,FromLocationType,FromLocationID,ToLocationType,ToLocationID,AssetStatus,CurrentLocationType,CurrentLocationID)values(" +
                        "" + lbl_assetid.Text + ",'" + fromlocationtype + "'," + fromlocationid + ",'" + tolocationtype + "'," + tolocationid + ",'" + assetstatus + "','" + currentlocationtype + "','" + currentlocationid + "')";
                        SqlHelper.ExecuteNonQuery(GlobalConnetionString.ClientConnection(), CommandType.Text, insert_locationtype);

                    }
                }

            }
            int updatecnt = SqlHelper.ExecuteNonQuery(GlobalConnetionString.ClientConnection(), CommandType.Text, updatequery);
            if (updatecnt > 0)
            {
                string notificationsendtowhome = eventNotification.sendEventNotification("ASR01");
                if (notificationsendtowhome != "")
                {
                    bool status = eventNotification.sendEventNotificationWithEmail(notificationsendtowhome, "ASR01", "ASSET", "", row["clientAssetName"].Text,
                        "", "", "Repair", lbl_statuscheck.Text, "");
                }
            }

           
        }
        bind();
    }
    protected void radgrid_repairstatus_ItemCommand(object source, GridCommandEventArgs e)
    {
        bind();
    }
    protected void btn_view_Click(object sender, EventArgs e)
    {
        bind();

    }
    protected void btn_viewassetcat_OnClick(object sender, EventArgs e)
    {
        binddataforassetcat();

    }
    public void  binddataforassetcat()
    {

        
        string assetids = "", str_assecatall = "", query_select = "";
        for (int assetcat = 0; assetcat < radcombo_assetcat.Items.Count; assetcat++)
        {
            if (radcombo_assetcat.Items[assetcat].Checked)
            {
                assetids += radcombo_assetcat.Items[assetcat].Value + ",";
            }
        }
        if (assetids != "")
        {
            str_assecatall = assetids.Remove(assetids.Length - 1, 1);
            query_select = "select  P.ID as MainAssetID,PA.AssetName,P.Cost as DailyCharge,P.runhrmaintenance as RunHrsToMaintenance,P.maintenancepercentage,P.previoususedhrs,* from Prism_Assets P,PrismAssetName PA,clientAssets AC " +
            "where  P.AssetName=PA.ID and P.AssetCategoryId=AC.clientAssetID and AC.clientAssetID in (" + str_assecatall + ") order by P.Id";
            DataTable dt_view = SqlHelper.ExecuteDataset(GlobalConnetionString.ClientConnection(), CommandType.Text, query_select).Tables[0];
            //radgrid_repairstatus.DataSourceID = String.Empty;
            radgrid_repairstatus.DataSource = dt_view;

            radgrid_repairstatus.DataBind();
        }
    }
    public void bind()
    {
        string query_select = "";
        if (radcombo_rstatus.SelectedValue == "0")
        {
            query_select = "select  PA.AssetName,P.Cost as DailyCharge,P.runhrmaintenance as RunHrsToMaintenance,P.maintenancepercentage,P.previoususedhrs,P.ID as MainAssetID,* from Prism_Assets P,PrismAssetName PA,clientAssets AC " +
            "where  P.AssetName=PA.ID and P.AssetCategoryId=AC.clientAssetID and SerialNumber like'%" + txt_assetsno.Text + "%'  order by P.Id";
        }
        //else if (radcombo_rstatus.SelectedValue == "Maintenance")
        //{
        //    query_select = "select  PA.AssetName,PA.ID as MainAssetID,* from Prism_Assets P,PrismAssetName PA,clientAssets AC " +
        //        "where  P.AssetName=PA.ID and P.AssetCategoryId=AC.clientAssetID and  SerialNumber like'%" + txt_assetsno.Text + "%' and repairstatus='" + radcombo_rstatus.SelectedValue + "'  order by P.Id";
        //}
        //else if (radcombo_rstatus.SelectedValue == "Ok")
        //{
        //    query_select = "select  PA.AssetName,PA.ID as MainAssetID,* from Prism_Assets P,PrismAssetName PA,clientAssets AC " +
        //        "where  P.AssetName=PA.ID and P.AssetCategoryId=AC.clientAssetID and SerialNumber like'%" + txt_assetsno.Text + "%' and repairstatus='" + radcombo_rstatus.SelectedValue + "'  order by P.Id";
        //}
        else
        {
            query_select = "select  PA.AssetName,P.Cost as DailyCharge,P.runhrmaintenance as RunHrsToMaintenance,P.maintenancepercentage,P.previoususedhrs,P.ID as MainAssetID,* from Prism_Assets P,PrismAssetName PA,clientAssets AC " +
                "where  P.AssetName=PA.ID and P.AssetCategoryId=AC.clientAssetID and SerialNumber like'%" + txt_assetsno.Text + "%' and repairstatus='" + radcombo_rstatus.SelectedValue + "' order by P.Id";
        }
        DataTable dt_view = SqlHelper.ExecuteDataset(GlobalConnetionString.ClientConnection(), CommandType.Text, query_select).Tables[0];
        //radgrid_repairstatus.DataSourceID = String.Empty;
        radgrid_repairstatus.DataSource = dt_view;

        radgrid_repairstatus.DataBind();
    }
    protected void radgrid_clientdetails_PageIndexChanged(object sender, Telerik.Web.UI.GridPageChangedEventArgs e)
    {
        radgrid_repairstatus.CurrentPageIndex = e.NewPageIndex;
        bind();
    }
    protected void lnk_downloaddoc_OnClick(object sender, EventArgs e)
    {
        GridDataItem row = (GridDataItem)(((LinkButton)sender).NamingContainer);
        Label lbl_assetname = (Label)row.FindControl("lbl_assetname");
        Label lbl_sno = (Label)row.FindControl("lbl_sno");
        Label lbl_assetcat = (Label)row.FindControl("lbl_assetcat");
        Label lbl_RunHrsToMaintenance = (Label)row.FindControl("lbl_RunHrsToMaintenance");
        Label lbl_totalrunhrs = (Label)row.FindControl("lbl_totalrunhrs");
        Label lbl_totalCumulativerunhrs = (Label)row.FindControl("lbl_totalCumulativerunhrs");
        Label lbl_locationame = (Label)row.FindControl("lbl_locationame");
        Microsoft.Office.Interop.Excel.Application excelApp = new Microsoft.Office.Interop.Excel.ApplicationClass();
        excelApp.ScreenUpdating = false;
        excelApp.DisplayAlerts = false;

        object misValue = System.Reflection.Missing.Value;
        if (File.Exists(Server.MapPath("AssetMaintenanceWorkOrderSheet_copy.xlsx")))
        {

            File.Delete(Server.MapPath("AssetMaintenanceWorkOrderSheet_copy.xlsx"));
        }
        File.Copy(Server.MapPath("AssetMaintenanceWorkOrderSheet.xlsx"), Server.MapPath("AssetMaintenanceWorkOrderSheet_copy.xlsx"));
        Microsoft.Office.Interop.Excel.Workbook workbook = excelApp.Workbooks.Open(Server.MapPath("AssetMaintenanceWorkOrderSheet.xlsx"), Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value);

        Microsoft.Office.Interop.Excel.Sheets excelSheets = workbook.Worksheets;
        string currentSheet = "Sheet1";
        Microsoft.Office.Interop.Excel.Worksheet excelWorksheet = (Microsoft.Office.Interop.Excel.Worksheet)excelSheets.get_Item(currentSheet);
        Microsoft.Office.Interop.Excel.Range range = (Microsoft.Office.Interop.Excel.Range)excelWorksheet.UsedRange;
        excelWorksheet.Cells[3, 2] = lbl_assetname.Text;
        
        excelWorksheet.Cells[4, 2] = lbl_sno.Text;
        excelWorksheet.Cells[5, 2] = lbl_assetcat.Text;
        excelWorksheet.Cells[6, 2] = lbl_locationame.Text;
        excelWorksheet.Cells[7, 2] = lbl_RunHrsToMaintenance.Text;
        excelWorksheet.Cells[8, 2] = lbl_totalrunhrs.Text;
        excelWorksheet.Cells[9, 2] = lbl_totalCumulativerunhrs.Text;
        workbook.Save();

        //workbook.SaveAs(Server.MapPath("RunReport.xlsx"), Excel.XlFileFormat.xlWorkbookNormal, misValue, misValue, misValue, misValue, Excel.XlSaveAsAccessMode.xlExclusive, misValue, misValue, misValue, misValue, misValue);
        workbook.Close(true, misValue, misValue);

        excelApp.Quit();
        Process[] pros = Process.GetProcesses();
        for (int i = 0; i < pros.Count(); i++)
        {
            if (pros[i].ProcessName.ToLower().Contains("excel"))
            {
                pros[i].Kill();
            }
        }

        string FilePath = Server.MapPath("AssetMaintenanceWorkOrderSheet.xlsx");
        FileInfo fileInfo = new FileInfo(FilePath);
        Response.Clear();
        Response.AddHeader("Content-Disposition", "attachment;filename=" + fileInfo.Name);
        Response.AddHeader("Content-Length", fileInfo.Length.ToString());
        Response.ContentType = "application/octet-stream";
        Response.Flush();
        Response.WriteFile(fileInfo.FullName);
        Response.End();

    }
    protected void btn_reset_OnClick(object sender, EventArgs e)
    {
        txt_assetsno.Text = "";
        radcombo_rstatus.SelectedValue = "0";
        radcombo_assetcat.DataBind();
        bind();
    }
    protected void radgrid_repairstatus_ItemDataBound(object sender, GridItemEventArgs e)
    {
        if (e.Item is GridDataItem)
        {
            GridDataItem item = (GridDataItem)e.Item;
            Label lbl_location = (Label)item.FindControl("lbl_location");
            Label lbl_statuscheck = (Label)item.FindControl("lbl_statuscheck");
            Label lbl_assetid = (Label)item.FindControl("lbl_assetid");
            Label lbl_locationame = (Label)item.FindControl("lbl_locationame");
            CheckBox isChecked = (CheckBox)item.FindControl("isChecked");
            Label lnk_map = (Label)item.FindControl("lnk_map");
            LinkButton lnk_downloaddoc = (LinkButton)item.FindControl("lnk_downloaddoc");
            //RadButton btn_maintain = (RadButton)item.FindControl("btn_maintain");
            //Label lbl_currentloctype = (Label)item.FindControl("lbl_currentloctype");
            int maxassetID = 0;
            string mapurl = "";
            string[] arrmaplatlang = null;
            try
            {
                maxassetID = Convert.ToInt32(dt_AssetCurrLoc.Compute("Max ( AssetCurrentLocationID ) ", "AssetID = " + lbl_assetid.Text + "").ToString());
                DataRow[] row_location = dt_AssetCurrLoc.Select("AssetCurrentLocationID=" + maxassetID);
                //lbl_locationame.Text = row_location[0]["CurrentLocationType"].ToString();
                switch (row_location[0]["CurrentLocationType"].ToString())
                {
                    case "WareHouse":
                        {
                            DataRow[] row_warehouse = dt_warehouse.Select("ID=" + row_location[0]["CurrentLocationID"].ToString());
                            if (row_warehouse.Length > 0)
                            {
                                arrmaplatlang = row_warehouse[0]["primaryLatLong"].ToString().Split(',');
                                lbl_locationame.Text = row_warehouse[0]["Name"].ToString();
                                mapurl = "http://maps.google.com/?q=" + row_warehouse[0]["primaryLatLong"].ToString();
                                //lnk_map.PostBackUrl =mapurl;

                            }

                            break;
                        }
                    case "Job":
                        {
                            DataRow[] row_job = dt_job.Select("jid=" + row_location[0]["CurrentLocationID"].ToString());
                            if (row_job.Length > 0)
                            {
                                arrmaplatlang = row_job[0]["primaryLatLong"].ToString().Split(',');
                                lbl_locationame.Text = row_job[0]["jobname"].ToString();
                                mapurl = "http://maps.google.com/?q=" + row_job[0]["primaryLatLong"].ToString();
                                //lnk_map.PostBackUrl = mapurl;
                            }
                            break;
                        }
                }




            }
            catch (Exception ex)
            {
                lbl_locationame.Text = "";
            }
            

            //lbl_statuscheck.Text = "Ok";
            if (lbl_statuscheck.Text == "Maintenance")
            {
                lbl_location.Text = "Under Maintenance";
                lbl_location.ForeColor = Color.Red;
                lbl_locationame.Text = "-NA-";
            }
            else if (lbl_statuscheck.Text.ToLower() == "job")
            {
                lbl_location.Text = "On Job";
                lbl_location.ForeColor = Color.Purple;
                lnk_map.Text = "<u style='cursor:hand'>View&#160;Map</u>";
                lnk_map.Attributes.Add("onclick", "javascript:return viewMap('" + mapurl + "'); ");
            }
            else if (lbl_statuscheck.Text == "Ok")
            {
                lbl_location.Text = "Available";
                lbl_location.ForeColor = Color.Green;
                lnk_map.Text = "<u style='cursor:hand'>View&#160;Map</u>";
                lnk_map.Attributes.Add("onclick", "javascript:return viewMap('" + mapurl + "'); ");
            }
            else 
            {
                lbl_location.Text = "Available";
                lbl_location.ForeColor = Color.Green;
            }
            //else
            //{
            //    lbl_statuscheck.Text = "Ok";
            //    //lbl_statuscheck.Text = dt_exist.Rows[0]["repairstatus"].ToString();
            //    lbl_statuscheck.ForeColor = Color.Green;
            //    isChecked.Checked = false;
            //}
            string selectq = "select * from PrismJobRunDetails Run, PrismJobRunHourdetails RunHours" +
                " where Run.runid=RunHours.runid";
            DataTable dt_getassetrunhrs = SqlHelper.ExecuteDataset(GlobalConnetionString.ClientConnection(), CommandType.Text, selectq).Tables[0];
            DataTable dt_prism_assets = SqlHelper.ExecuteDataset(GlobalConnetionString.ClientConnection(), CommandType.Text, "select * from prism_assets").Tables[0];
            Label lbl_totalrunhrs = (Label)item.FindControl("lbl_totalrunhrs");
            Label lbl_totalCumulativerunhrs = (Label)item.FindControl("lbl_totalCumulativerunhrs");
            Label lbl_RunHrsToMaintenance = (Label)item.FindControl("lbl_RunHrsToMaintenance");
            Label lbl_maintenancepercentage = (Label)item.FindControl("lbl_maintenancepercentage");

            DataRow[] dr_runhrval = dt_getassetrunhrs.Select("AssetId=" + lbl_assetid.Text + "");
            string obj_sum = dt_getassetrunhrs.Compute("sum(dailyrunhrs)", "AssetId=" + lbl_assetid.Text + "").ToString();
            DataRow[] dr_prismupdate = dt_prism_assets.Select("ID=" + lbl_assetid.Text + "");
            if (obj_sum != "" && dr_prismupdate.Length > 0)
            {
                lbl_totalCumulativerunhrs.Text = (WebUtility.NullToZero(obj_sum) + WebUtility.NullToZero(dr_prismupdate[0]["previoususedhrs"].ToString())).ToString();
            }
            else if(dr_prismupdate.Length>0)
            {
                lbl_totalCumulativerunhrs.Text = dr_prismupdate[0]["previoususedhrs"].ToString();
            }
            if (dr_runhrval.Length > 0)
            {
                lbl_totalrunhrs.Text = dr_runhrval[0]["dailyrunhrs"].ToString();
            }
            else
            {
                lbl_totalrunhrs.Text = "0";
            }
            if (dr_prismupdate.Length > 0)
            {
                if (dr_prismupdate[0]["LastMaintanenceDate"].ToString() == "" || dr_prismupdate[0]["LastMaintanenceDate"].ToString() == "1/1/1900 12:00:00 AM")
                {
                    if (dr_prismupdate[0]["previoususedhrs"].ToString() != "")
                    {
                        if (obj_sum != "")
                        {
                            lbl_totalrunhrs.Text = (Convert.ToDecimal(obj_sum) + Convert.ToDecimal(dr_prismupdate[0]["previoususedhrs"])).ToString();
                        }
                        else
                        {
                            lbl_totalrunhrs.Text = Convert.ToDecimal(dr_prismupdate[0]["previoususedhrs"]).ToString();
                        }
                    }
                }
                else
                {
                    string obj_sumofdate = dt_getassetrunhrs.Compute("sum(dailyrunhrs)", "Date>'" + Convert.ToDateTime(dr_prismupdate[0]["LastMaintanenceDate"].ToString()) + "' and AssetId=" + lbl_assetid.Text + "").ToString();
                    if (obj_sumofdate != "")
                    {
                        //DataRow[] dr_prismupdatedate = dt_prism_assets.Select("Date>" + Convert.ToDateTime(dr_prismupdate[0]["LastMaintanenceDate"].ToString()) + "");
                        lbl_totalrunhrs.Text = obj_sumofdate;
                    }
                }
            }
            Decimal runhrm = 0, maintper = 0;
            if (lbl_RunHrsToMaintenance.Text == "")
            {
                runhrm = 0;
            }
            else
            {
                runhrm = Convert.ToDecimal(lbl_RunHrsToMaintenance.Text);
            }
            if (lbl_maintenancepercentage.Text == "")
            {
                maintper = 0;
            }
            else
            {
                maintper = Convert.ToDecimal(lbl_maintenancepercentage.Text);
            }
            Decimal cutoff = runhrm - (maintper * runhrm / 100);
            if (lbl_totalrunhrs.Text != "")
            {
                if (Convert.ToDecimal(lbl_totalrunhrs.Text) >= cutoff)
                {
                    e.Item.BackColor = Color.Red;
                    e.Item.ForeColor = Color.White;
                }
                //lbl_totalCumulativerunhrs.Text = cum.ToString();
                //PrismAssetComponents
                // Get the Active checkbox in the item
            }
            

        }
    }
    //protected void btn_maintain_Click(object sender, EventArgs e)
    //{
    //    string updatequery = "", insertquery = "", updatequery_Repair = "", jobAssetUpdateQuery = "";
    //    GridDataItem row = (GridDataItem)(((RadButton)sender).NamingContainer);
        
            
    //        Label lbl_assetid = (Label)row.FindControl("lbl_assetid");
    //        Label lbl_statuscheck = (Label)row.FindControl("lbl_statuscheck");

    //        if (lbl_statuscheck.Text != "Ok")
    //        {
    //            updatequery = "update Prism_Assets set repairstatus='Ok' where Id=" + lbl_assetid.Text + "";
    //            DataTable dt_exist = SqlHelper.ExecuteDataset(GlobalConnetionString.ClientConnection(), CommandType.Text, "select max(AssetRid) as AssetRid from PrismAssetRepairStatus where assetid=" + lbl_assetid.Text + "").Tables[0];
    //            updatequery_Repair = "update PrismAssetRepairStatus set repairfixdate='" + DatesbetweenDatatable.getdatetimeformat(DateTime.Now.ToString()) + "' where AssetRid=" + dt_exist.Rows[0]["AssetRid"].ToString() + "";
    //            SqlHelper.ExecuteNonQuery(GlobalConnetionString.ClientConnection(), CommandType.Text, updatequery_Repair);
    //        }
    //        else
    //        {
    //            updatequery = "update Prism_Assets set repairstatus='Maintenance' where Id=" + lbl_assetid.Text + "";
    //            insertquery = "insert into PrismAssetRepairStatus(assetid,repairdate,status)values(" + lbl_assetid.Text + "," +
    //                    "'" + DatesbetweenDatatable.getdatetimeformat(DateTime.Now.ToString()) + "','Pending')";
    //            SqlHelper.ExecuteNonQuery(GlobalConnetionString.ClientConnection(), CommandType.Text, insertquery);
    //            DataTable dt_existprismjobassignedassets = SqlHelper.ExecuteDataset(GlobalConnetionString.ClientConnection(), CommandType.Text, "select * from PrismJobAssignedAssets where assignmentstatus='Active' and assetid='" + lbl_assetid.Text + "'").Tables[0];
    //            if (dt_existprismjobassignedassets.Rows.Count > 0)
    //            {
    //                jobAssetUpdateQuery = "update PrismJobAssignedAssets set AssignmentStatus='InActive' where assetid=" + lbl_assetid.Text + " and AssignmentStatus='Active'";
    //                SqlHelper.ExecuteNonQuery(GlobalConnetionString.ClientConnection(), CommandType.Text, jobAssetUpdateQuery);


    //                string fromlocationtype = "", fromlocationid = "", tolocationtype = "", tolocationid = "", assetstatus = "", assetcurrentlocid = "", currentlocationtype = "", currentlocationid = "";

    //                string query_existassetloc = "SELECT top(1) * FROM    PrismAssetCurrentLocation WHERE    AssetID=" + lbl_assetid.Text + " order by AssetCurrentLocationID desc";
    //                DataTable dt_existassetlocation = SqlHelper.ExecuteDataset(GlobalConnetionString.ClientConnection(), CommandType.Text, query_existassetloc).Tables[0];
    //                DataTable dt_assetlocation = SqlHelper.ExecuteDataset(GlobalConnetionString.ClientConnection(), CommandType.Text, "select * from Prism_Assets where ID=" + lbl_assetid.Text + "").Tables[0];

    //                if (dt_existassetlocation.Rows.Count > 0)
    //                {

    //                    fromlocationtype = dt_existassetlocation.Rows[0]["ToLocationType"].ToString();
    //                    fromlocationid = dt_existassetlocation.Rows[0]["ToLocationID"].ToString();


    //                    assetcurrentlocid = dt_existassetlocation.Rows[0]["AssetCurrentLocationID"].ToString();
    //                    tolocationtype = "WareHouse";
    //                    tolocationid = dt_assetlocation.Rows[0]["WarehouseId"].ToString();
    //                    currentlocationtype = "WareHouse";
    //                    currentlocationid = dt_assetlocation.Rows[0]["WarehouseId"].ToString();
    //                    assetstatus = "Available";

    //                    string insert_locationtype = "insert into PrismAssetCurrentLocation(AssetID,FromLocationType,FromLocationID,ToLocationType,ToLocationID,AssetStatus,CurrentLocationType,CurrentLocationID)values(" +
    //                    "" + lbl_assetid.Text + ",'" + fromlocationtype + "'," + fromlocationid + ",'" + tolocationtype + "'," + tolocationid + ",'" + assetstatus + "','" + currentlocationtype + "','" + currentlocationid + "')";
    //                    SqlHelper.ExecuteNonQuery(GlobalConnetionString.ClientConnection(), CommandType.Text, insert_locationtype);

    //                }
    //            }

    //        }
    //        int updatecnt = SqlHelper.ExecuteNonQuery(GlobalConnetionString.ClientConnection(), CommandType.Text, updatequery);
    //        if (updatecnt > 0)
    //        {
    //            string notificationsendtowhome = eventNotification.sendEventNotification("ASR01");
    //            if (notificationsendtowhome != "")
    //            {
    //                bool status = eventNotification.sendEventNotificationWithEmail(notificationsendtowhome, "ASR01", "ASSET", "", row["clientAssetName"].Text,
    //                    "", "", "Repair", lbl_statuscheck.Text, "");
    //            }
    //        }

    //    bind();
    //}
}