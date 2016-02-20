using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

public partial class Modules_Configuration_Manager_ViewEditShippingDetails : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            radtxt_start.SelectedDate = DateTime.Now.AddDays(-10);
            radtxt_stop.SelectedDate = DateTime.Now.AddDays(2);
            DataTable dt = SqlHelper.ExecuteDataset(GlobalConnetionString.ClientConnection(), CommandType.Text, "select * from PrismShippingTicket").Tables[0];
            radgridShippingDetails.DataSource = dt;
            radgridShippingDetails.DataBind();
        }
    }
    protected void grdJobList_ItemCommand(object source, GridCommandEventArgs e)
    {

        if (e.CommandName == RadGrid.ExpandCollapseCommandName)
        {

            GridDataItem item = e.Item as GridDataItem;
            LinkButton lnk_printticket = (LinkButton)item.FindControl("lnk_printticket");
            ScriptManager.GetCurrent(Page).RegisterPostBackControl(lnk_printticket);
            if (!item.Expanded)
            {

                GridNestedViewItem nestedItem = (GridNestedViewItem)item.ChildItem;

                string dataKeyValue = Convert.ToString(((GridDataItem)(nestedItem.ParentItem)).GetDataKeyValue("ID"));
                RadGrid radgridTicketDetails = (RadGrid)nestedItem.FindControl("radgridTicketDetails");
                string selectque = "select n.AssetName+'('+pa.SerialNumber+')' as AssetNameID,ptd.FromLocation,ptd.FromLocationID, "+
                    " ptd.ToLocation,ptd.ToLocationID,ptd.AssetId as AssetID,ptd.Status as AssetMainStatus,* from PrismShippingTicket pt,PrismShippingTicketDetails ptd, " +
                    " Prism_Assets pa,PrismAssetName n where  pt.ID=ptd.TicketId and pa.AssetName=n.Id and pa.Id=ptd.AssetId and pt.ID=" + dataKeyValue + "";
                DataTable dt = SqlHelper.ExecuteDataset(GlobalConnetionString.ClientConnection(), CommandType.Text, selectque).Tables[0];
                radgridTicketDetails.DataSource = dt;
                radgridTicketDetails.DataBind();
            }
        }
    }
    //protected void radgridShippingDetails_PreRender(object sender, EventArgs e)
    //{
    //    if (!Page.IsPostBack)
    //    {
    //        radgridShippingDetails.MasterTableView.Items[0].Expanded = true;
    //        //radgridShippingDetails.MasterTableView.Items[0].ChildItem.FindControl("InnerContainer").Visible = true;
    //    }
    //}
    protected void btn_view_OnClick(object sender, EventArgs e)
    {
        bind();
    }
    //radgridTicketDetails_ItemDataBound
    protected void radgridTicketDetails_OnItemCommand(object source, GridCommandEventArgs e)
    {
        bool editMode = e.Item.OwnerTableView.OwnerGrid.EditIndexes.Count > 0;
        bool insertMode = e.Item.OwnerTableView.IsItemInserted;
        RadGrid radgridTicketDetails = (source as RadGrid);
        GridEditableItem item = e.Item as GridEditableItem;
        string dataKeyValue = Convert.ToString(((GridDataItem)(item)).GetDataKeyValue("ID"));
        
        if (e.CommandName == "Update")
        {

            if (e.Item is GridEditableItem && e.Item.IsInEditMode)
            {
                GridDataItem item1 = e.Item as GridDataItem;
                Label lbl_assetid = (Label)item1.FindControl("lbl_assetid");
                RadComboBox radcombo_type = (RadComboBox)item1.FindControl("radcombo_type");
                SqlHelper.ExecuteNonQuery(GlobalConnetionString.ClientConnection(), CommandType.Text, "Update PrismJobAssignedAssets set" +
                        " ModifiedDate='" + DatesbetweenDatatable.getdatetimeformat(DateTime.Now.ToString()) + "',ModifiedBy='" + Session["userId"].ToString() + "',AssetStatus=" + radcombo_type.SelectedValue + " where Id=" + lbl_assetid.Text + "");
                SqlHelper.ExecuteNonQuery(GlobalConnetionString.ClientConnection(), CommandType.Text, "update PrismShippingTicketDetails set Status='" + radcombo_type.SelectedItem.Text + "' where AssetID=" + lbl_assetid.Text + "");
                if (radcombo_type.SelectedValue == "3")
                {
                    string updateeprismAssets = "update Prism_Assets set repairstatus='job' where ID=" + lbl_assetid.Text + "";
                    SqlHelper.ExecuteNonQuery(GlobalConnetionString.ClientConnection(), CommandType.Text, updateeprismAssets);

                }
                else
                {
                    string updateeprismAssets = "update Prism_Assets set repairstatus='Ok' where ID=" + lbl_assetid.Text + "";
                    SqlHelper.ExecuteNonQuery(GlobalConnetionString.ClientConnection(), CommandType.Text, updateeprismAssets);
                }


                string fromlocationtype = "", fromlocationid = "", currentlocationtype = "", currentlocationid = "", assetstatus = "", assetcurrentlocid = "";

                string query_existassetloc = "SELECT top(1) * FROM    PrismAssetCurrentLocation WHERE  AssetID=" + lbl_assetid.Text + " order by AssetCurrentLocationID desc";
                DataTable dt_existassetlocation = SqlHelper.ExecuteDataset(GlobalConnetionString.ClientConnection(), CommandType.Text, query_existassetloc).Tables[0];
                DataTable dt_assetlocation = SqlHelper.ExecuteDataset(GlobalConnetionString.ClientConnection(), CommandType.Text, "select * from Prism_Assets where ID=" + lbl_assetid.Text + "").Tables[0];
                if (dt_existassetlocation.Rows.Count > 0)
                {
                    if (radcombo_type.SelectedValue == "3")
                    {
                        assetcurrentlocid = dt_existassetlocation.Rows[0]["AssetCurrentLocationID"].ToString();
                        currentlocationtype = dt_existassetlocation.Rows[0]["ToLocationType"].ToString();
                        currentlocationid = dt_existassetlocation.Rows[0]["ToLocationID"].ToString();

                        assetstatus = "In Use";

                        string update_locationtype = "update PrismAssetCurrentLocation set AssetStatus='" + assetstatus + "'," +
                            " CurrentLocationType='" + currentlocationtype + "',CurrentLocationID='" + currentlocationid + "' , AssetMovedDate='" + DatesbetweenDatatable.getdatetimeformat(DateTime.Now.ToString()) + "' where AssetCurrentLocationID=" + assetcurrentlocid + "";

                        SqlHelper.ExecuteNonQuery(GlobalConnetionString.ClientConnection(), CommandType.Text, update_locationtype);
                    }
                    else
                    {
                        assetcurrentlocid = dt_existassetlocation.Rows[0]["AssetCurrentLocationID"].ToString();
                        currentlocationtype = dt_existassetlocation.Rows[0]["FromLocationType"].ToString();
                        currentlocationid = dt_existassetlocation.Rows[0]["FromLocationID"].ToString();

                        assetstatus = "Available";

                        string update_locationtype = "update PrismAssetCurrentLocation set AssetStatus='" + assetstatus + "'," +
                            " CurrentLocationType='" + currentlocationtype + "',CurrentLocationID='" + currentlocationid + "' ,AssetMovedDate='' where AssetCurrentLocationID=" + assetcurrentlocid + "";

                        SqlHelper.ExecuteNonQuery(GlobalConnetionString.ClientConnection(), CommandType.Text, update_locationtype);
                    }

                }
            }
            
        }
        DataTable dtgetexistingassetstatus = SqlHelper.ExecuteDataset(GlobalConnetionString.ClientConnection(), CommandType.Text, "select Status from PrismShippingTicketDetails where TicketID=" + dataKeyValue + "").Tables[0];
        int delivercnt = 0;
        for (int i = 0; i < dtgetexistingassetstatus.Rows.Count; i++)
        {
            if (dtgetexistingassetstatus.Rows[i]["Status"].ToString() == "Delivered")
            {
                delivercnt++;
            }
        }
        if (delivercnt == dtgetexistingassetstatus.Rows.Count)
        {
            SqlHelper.ExecuteNonQuery(GlobalConnetionString.ClientConnection(), CommandType.Text, "update PrismShippingTicket set Status='Closed' where ID=" + dataKeyValue + "");
        }

        string selectque = "select n.AssetName+'('+pa.SerialNumber+')' as AssetNameID,ptd.FromLocation,ptd.FromLocationID, " +
                    " ptd.ToLocation,ptd.ToLocationID,ptd.AssetId as AssetID,ptd.Status as AssetMainStatus,* from PrismShippingTicket pt,PrismShippingTicketDetails ptd, " +
                    " Prism_Assets pa,PrismAssetName n where  pt.ID=ptd.TicketId and pa.AssetName=n.Id and pa.Id=ptd.AssetId and pt.ID=" + dataKeyValue + "";
        DataTable dt = SqlHelper.ExecuteDataset(GlobalConnetionString.ClientConnection(), CommandType.Text, selectque).Tables[0];
        radgridTicketDetails.DataSource = dt;
        radgridTicketDetails.DataBind();
    }
    protected void lnk_printticket_Click(object sender, EventArgs e)
    {
        GridDataItem row1 = (GridDataItem)(((LinkButton)sender).NamingContainer);
        
        string dataKeyValue = Convert.ToString(((GridDataItem)(row1)).GetDataKeyValue("ID"));
        
        DataTable dt_gettickets = SqlHelper.ExecuteDataset(GlobalConnetionString.ClientConnection(), CommandType.Text, "select * from PrismShippingTicket where ID=" + dataKeyValue + "").Tables[0];


        string selectque = "select n.AssetName+'('+pa.SerialNumber+')' as AssetNameID,ptd.FromLocation,ptd.FromLocationID, " +
                    " ptd.ToLocation,ptd.ToLocationID,ptd.AssetId as AssetID,ptd.Status as AssetMainStatus,* from PrismShippingTicket pt,PrismShippingTicketDetails ptd, " +
                    " Prism_Assets pa,PrismAssetName n where  pt.ID=ptd.TicketId and pa.AssetName=n.Id and pa.Id=ptd.AssetId and pt.ID=" + dataKeyValue + "";
        DataTable dt_ticketdetails = SqlHelper.ExecuteDataset(GlobalConnetionString.ClientConnection(), CommandType.Text, selectque).Tables[0];
        Microsoft.Office.Interop.Excel.Application excelApp = new Microsoft.Office.Interop.Excel.ApplicationClass();
        excelApp.ScreenUpdating = false;
        excelApp.DisplayAlerts = false;
        string unique = DateTime.Now.ToString("MMddyyyyHHMMss");
        object misValue = System.Reflection.Missing.Value;
        if (File.Exists(Server.MapPath("ShippingTicketDocSheet_copy.xlsx")))
        {

            File.Delete(Server.MapPath("ShippingTicketDocSheet_copy.xlsx"));
        }
        File.Copy(Server.MapPath("ShippingTicketDocSheet.xlsx"), Server.MapPath("ShippingTicketDocSheet_copy.xlsx"));
        //Opening Excel file(myData.xlsx)
        Microsoft.Office.Interop.Excel.Workbook workbook = excelApp.Workbooks.Open(Server.MapPath("ShippingTicketDocSheet_copy.xlsx"), Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value);

        Microsoft.Office.Interop.Excel.Sheets excelSheets = workbook.Worksheets;
        string currentSheet = "Sheet1";
        Microsoft.Office.Interop.Excel.Worksheet excelWorksheet = (Microsoft.Office.Interop.Excel.Worksheet)excelSheets.get_Item(currentSheet);
        Microsoft.Office.Interop.Excel.Range range = (Microsoft.Office.Interop.Excel.Range)excelWorksheet.UsedRange;
        string strOperators = "", strOperators1 = "";
        int operatcol = 6;
        
        //Job Id
        excelWorksheet.Cells[4,2] = dt_gettickets.Rows[0]["TicketID"].ToString();
        //KIt/AssetName
        excelWorksheet.Cells[6,2] = dt_gettickets.Rows[0]["CreatedDate"].ToString();
        //Location
        excelWorksheet.Cells[8,2] = dt_gettickets.Rows[0]["ShippingDate"].ToString();
        //Operator
        excelWorksheet.Cells[10,2] = dt_gettickets.Rows[0]["Status"].ToString();


        //DataTable dt = SqlHelper.ExecuteDataset(GlobalConnetionString.ClientConnection(), CommandType.Text, "").Tables[0];
        int row = 17;
        
        for (int r = 0; r < dt_ticketdetails.Rows.Count; r++)
        {

            string lbl_FromLocationAddress = "", lbl_JobName = "", lbl_ToLocationAddress = "", lbl_JobWarehouseName = "";
            if (dt_ticketdetails.Rows[r]["FromLocation"].ToString() == "Job")
            {
                DataTable dtgetaddfromjob = SqlHelper.ExecuteDataset(GlobalConnetionString.ClientConnection(), CommandType.Text, "select (primaryAddress1+', '+primaryAddress2+', '+primaryCity+', '+primaryState+', '+primaryCountry+'.') as Address,jobname as JobName from manageJobOrders where jid=" + dt_ticketdetails.Rows[r]["FromLocationID"].ToString() + "").Tables[0];
                if (dtgetaddfromjob.Rows.Count > 0)
                {
                    lbl_FromLocationAddress = dtgetaddfromjob.Rows[0]["Address"].ToString();
                    lbl_JobName = dtgetaddfromjob.Rows[0]["JobName"].ToString();
                }
            }
            else if (dt_ticketdetails.Rows[r]["FromLocation"].ToString() == "WareHouse")
            {
                DataTable dtgetaddfromwarehouse = SqlHelper.ExecuteDataset(GlobalConnetionString.ClientConnection(), CommandType.Text, "select (primaryAddress1+', '+primaryAddress2+', '+primaryCity+', '+primaryState+', '+primaryCountry+'.') as Address,Name from PrsimWarehouses where ID=" + dt_ticketdetails.Rows[r]["FromLocationID"].ToString() + "").Tables[0];
                if (dtgetaddfromwarehouse.Rows.Count > 0)
                {
                    lbl_FromLocationAddress = dtgetaddfromwarehouse.Rows[0]["Address"].ToString();
                    lbl_JobName = dtgetaddfromwarehouse.Rows[0]["Name"].ToString();
                }
            }

            if (dt_ticketdetails.Rows[r]["ToLocation"].ToString() == "Job")
            {
                DataTable dtgetaddfromjob = SqlHelper.ExecuteDataset(GlobalConnetionString.ClientConnection(), CommandType.Text, "select (primaryAddress1+', '+primaryAddress2+', '+primaryCity+', '+primaryState+', '+primaryCountry+'.') as Address,jobname as JobName from manageJobOrders where jid=" + dt_ticketdetails.Rows[r]["ToLocationID"].ToString() + "").Tables[0];
                if (dtgetaddfromjob.Rows.Count > 0)
                {
                    lbl_ToLocationAddress = dtgetaddfromjob.Rows[0]["Address"].ToString();
                    lbl_JobWarehouseName = dtgetaddfromjob.Rows[0]["JobName"].ToString();
                }
            }
            else if (dt_ticketdetails.Rows[r]["ToLocation"].ToString() == "WareHouse")
            {
                DataTable dtgetaddfromwarehouse = SqlHelper.ExecuteDataset(GlobalConnetionString.ClientConnection(), CommandType.Text, "select (primaryAddress1+', '+primaryAddress2+', '+primaryCity+', '+primaryState+', '+primaryCountry+'.') as Address,Name from PrsimWarehouses where ID=" + dt_ticketdetails.Rows[r]["ToLocationID"].ToString() + "").Tables[0];
                if (dtgetaddfromwarehouse.Rows.Count > 0)
                {
                    lbl_ToLocationAddress = dtgetaddfromwarehouse.Rows[0]["Address"].ToString();
                    lbl_JobWarehouseName = dtgetaddfromwarehouse.Rows[0]["Name"].ToString();
                }
            }
            
            int rownumber = row + r;
            int col1 = 1;
            int col2 = 2;
            int col3 = 3;
            int col4 = 4;
            int col5 = 5;
            int col6 = 6;
            excelWorksheet.Cells[rownumber, col1] = dt_ticketdetails.Rows[r]["AssetNameID"].ToString();
            excelWorksheet.Cells[rownumber, col2] = lbl_JobName;
            excelWorksheet.Cells[rownumber, col3] = lbl_FromLocationAddress;
            excelWorksheet.Cells[rownumber, col4] = lbl_JobWarehouseName;
            excelWorksheet.Cells[rownumber, col5] = lbl_ToLocationAddress;
            //excelWorksheet.Cells[rownumber, col6] = dt_ticketdetails.Rows[r]["AssetMainStatus"].ToString();
        }
        


        workbook.Save();
        workbook.Close(true, misValue, misValue);
        excelApp.Quit();
        //Process[] pros = Process.GetProcesses();
        //for (int i = 0; i < pros.Count(); i++)
        //{
        //    if (pros[i].ProcessName.ToLower().Contains("excel"))
        //    {
        //        pros[i].Kill();
        //    }
        //}

        string FilePath = Server.MapPath("ShippingTicketDocSheet_copy.xlsx");
        FileInfo fileInfo = new FileInfo(FilePath);
        Response.Clear();
        Response.AddHeader("Content-Disposition", "attachment;filename=" + fileInfo.Name);
        Response.AddHeader("Content-Length", fileInfo.Length.ToString());
        Response.ContentType = "application/octet-stream";
        Response.Flush();
        Response.WriteFile(fileInfo.FullName);
        Response.End();
    }
    public void bind()
    {
        string query = "select * from PrismShippingTicket where CreatedDate between '" + radtxt_start.SelectedDate + "' and '" + radtxt_stop.SelectedDate + "'";
        if (txt_ticketid.Text != "")
        {
            query += " and TicketID like '%"+txt_ticketid.Text+"%'";
        }
        if (radcombo_status.SelectedValue != "0")
        {
            query += " and Status='" + radcombo_status.SelectedValue + "'";
        }

        DataTable dt = SqlHelper.ExecuteDataset(GlobalConnetionString.ClientConnection(), CommandType.Text, query).Tables[0];
        radgridShippingDetails.DataSource = dt;
        radgridShippingDetails.DataBind();
    }
    protected void radgridShippingDetails_ItemDataBound(object sender, GridItemEventArgs e)
    {
        RadGrid grid = (RadGrid)sender;
        if (e.Item is GridDataItem)
        {
            GridEditableItem item = e.Item as GridEditableItem;
            LinkButton lnk_printticket = (LinkButton)item.FindControl("lnk_printticket");
            ScriptManager.GetCurrent(Page).RegisterPostBackControl(lnk_printticket);
        }
    }
    protected void radgridTicketDetails_ItemDataBound(object sender, GridItemEventArgs e)
    {
        RadGrid grid = (RadGrid)sender;
        if (e.Item is GridDataItem)
        {
            GridEditableItem item = e.Item as GridEditableItem;
            Label lbl_FromLocationType = (Label)item.FindControl("lbl_FromLocation");
            Label lbl_assetid = (Label)item.FindControl("lbl_assetid");
            Label lbl_FromLocationAddress = (Label)item.FindControl("lbl_FromLocationAddress");
            Label lbl_JobName = (Label)item.FindControl("lbl_JobName");
            Label lbl_fromlocationid = (Label)item.FindControl("lbl_fromlocationid");

            Label lbl_ToLocation = (Label)item.FindControl("lbl_ToLocation");
            Label lbl_ToLocationAddress = (Label)item.FindControl("lbl_ToLocationAddress");
            Label lbl_JobWarehouseName = (Label)item.FindControl("lbl_JobWarehouseName");
            Label lbl_Tolocationid = (Label)item.FindControl("lbl_Tolocationid");
            
            if (lbl_FromLocationType.Text == "Job")
            {
                DataTable dtgetaddfromjob = SqlHelper.ExecuteDataset(GlobalConnetionString.ClientConnection(), CommandType.Text, "select (primaryAddress1+', '+primaryAddress2+', '+primaryCity+', '+primaryState+', '+primaryCountry+'.') as Address,jobname as JobName from manageJobOrders where jid=" + lbl_fromlocationid.Text + "").Tables[0];
                if (dtgetaddfromjob.Rows.Count > 0)
                {
                    lbl_FromLocationAddress.Text = dtgetaddfromjob.Rows[0]["Address"].ToString();
                    lbl_JobName.Text = dtgetaddfromjob.Rows[0]["JobName"].ToString();
                }
            }
            else if (lbl_FromLocationType.Text == "WareHouse")
            {
                DataTable dtgetaddfromwarehouse = SqlHelper.ExecuteDataset(GlobalConnetionString.ClientConnection(), CommandType.Text, "select (primaryAddress1+', '+primaryAddress2+', '+primaryCity+', '+primaryState+', '+primaryCountry+'.') as Address,Name from PrsimWarehouses where ID=" + lbl_fromlocationid.Text + "").Tables[0];
                if (dtgetaddfromwarehouse.Rows.Count > 0)
                {
                    lbl_FromLocationAddress.Text = dtgetaddfromwarehouse.Rows[0]["Address"].ToString();
                    lbl_JobName.Text = dtgetaddfromwarehouse.Rows[0]["Name"].ToString();
                }
            }

            if (lbl_ToLocation.Text == "Job")
            {
                DataTable dtgetaddfromjob = SqlHelper.ExecuteDataset(GlobalConnetionString.ClientConnection(), CommandType.Text, "select (primaryAddress1+', '+primaryAddress2+', '+primaryCity+', '+primaryState+', '+primaryCountry+'.') as Address,jobname as JobName from manageJobOrders where jid=" + lbl_Tolocationid.Text + "").Tables[0];
                if (dtgetaddfromjob.Rows.Count > 0)
                {
                    lbl_ToLocationAddress.Text = dtgetaddfromjob.Rows[0]["Address"].ToString();
                    lbl_JobWarehouseName.Text = dtgetaddfromjob.Rows[0]["JobName"].ToString();
                }
            }
            else if (lbl_ToLocation.Text == "WareHouse")
            {
                DataTable dtgetaddfromwarehouse = SqlHelper.ExecuteDataset(GlobalConnetionString.ClientConnection(), CommandType.Text, "select (primaryAddress1+', '+primaryAddress2+', '+primaryCity+', '+primaryState+', '+primaryCountry+'.') as Address,Name from PrsimWarehouses where ID=" + lbl_Tolocationid.Text + "").Tables[0];
                if (dtgetaddfromwarehouse.Rows.Count > 0)
                {
                    lbl_ToLocationAddress.Text = dtgetaddfromwarehouse.Rows[0]["Address"].ToString();
                    lbl_JobWarehouseName.Text = dtgetaddfromwarehouse.Rows[0]["Name"].ToString();
                }
            }
            
            
        }
    }
}