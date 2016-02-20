using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Reflection;


public partial class Modules_Configuration_Manager_CreateShippingTicket_ : System.Web.UI.Page
{
    DataTable dt_JobAssetbottom, dtJobAssetsTop;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //combo_job.Enabled = false;
            //radcombo_warehouse.Enabled = false;
            btn_update.Visible = false;
            img_updatetotop.Visible = false;
            radtxt_start.SelectedDate = DateTime.Now;
            //grdJobList.DataSource = null;
            //grdJobList.DataBind();
            //combo_job.DataSource = RigTrack.DatabaseObjects.RigTrackDO.GetAllCurveGroupNames();
            //combo_job.DataTextField = "CurveGroupName";
            //combo_job.DataValueField = "ID";
            //combo_job.DataBind();

            ddlCompany.DataSource = RigTrack.DatabaseObjects.RigTrackDO.GetCompany();
            ddlCompany.DataTextField = "Name";
            ddlCompany.DataValueField = "ID";
            ddlCompany.DataBind();
        }

    }
    protected void ddlCompany_SelectedIndexChanged(object sender, DropDownListEventArgs e)
    {
        if (ddlCompany.SelectedValue != "0")
        {

            combo_job.Items.Clear();
            combo_job.Items.Add(new RadComboBoxItem("-Select-", "0"));
            DataTable dtJobDetails = RigTrack.DatabaseObjects.RigTrackDO.GetCurveGroupForCompany(Int32.Parse(ddlCompany.SelectedValue));
            combo_job.DataSource = dtJobDetails;
            combo_job.DataTextField = "CurveGroupName";
            combo_job.DataValueField = "ID";
            combo_job.DataBind();
            //string ids = "";
            //for (int i = 0; i < dtJobDetails.Rows.Count; i++)
            //{
            //    ids += dtJobDetails.Rows[i]["ID"].ToString() + ",";
            //}
            //if (ids != "")
            //{
            //    ids = ids.Remove(ids.Length - 1, 1);
            //    string query = "select  JO.ID,JO.JobNumber,jo.CurveGroupName as JOB,JO.JobStartDate,JO.JobLocation," +
            //" RigName,LeaseWell from [RigTrack].tblCurveGroup JO" +
            //" where ID in (select distinct JobId from PrismJobAssignedAssets where AssignMentStatus='Active') and JO.ID in(" + ids + ") order by JO.CreateDate desc";
            //    DataTable dtJobs = SqlHelper.ExecuteDataset(GlobalConnetionString.ClientConnection(), CommandType.Text, query).Tables[0];
            //    grdJobList.DataSource = dtJobs;
            //    grdJobList.DataBind();
            //}
        }
        else
        {


        }

    }
    protected void radcombo_warehouse_DataBound(object sender, EventArgs e)
    {
        var combo = (RadComboBox)sender;
        combo.Items.Insert(0, new RadComboBoxItem("Select WareHouse", string.Empty));
    }
    protected void radcombo_bottomwarehouse_DataBound(object sender, EventArgs e)
    {
        var combo = (RadComboBox)sender;
        combo.Items.Insert(0, new RadComboBoxItem("Select WareHouse", string.Empty));
    }
    public void bindtopgridfrombottomselected()
    {
        td_bottom.Visible = true;
        //RadgridForRemove.Visible = true;

        hid_selectedbottom.Value = "";
        string query_job = "", query_warehouse = "";
        if (combo_job.SelectedValue != "0")
        {
            query_warehouse = " select m.JobLocation as Address,m.CurveGroupName as jobname,n.AssetName as AssetName,jas.StatusText as StatusText,pa.ID as AssetMainID,* from Prism_Assets pa,PrismAssetName n,PrismJobAssignedAssets pj,[RigTrack].tblCurveGroup m,PrsimJobAssetStatus jas where " +
        " pa.AssetName=n.Id and pj.JobId=m.ID and pa.Id=pj.AssetId and pj.AssetStatus=jas.Id ";
            query_warehouse += " and m.ID=" + combo_job.SelectedValue + " and pj.AssignmentStatus='Active' ";
            //dtJobAssetsTop = SqlHelper.ExecuteDataset(GlobalConnetionString.ClientConnection(), CommandType.Text, query_job).Tables[0];
        }
        else
        {
            query_warehouse = "select pan.AssetName,pa.SerialNumber, (m.primaryAddress1+', '+m.primaryAddress2+', '+m.primaryCity+', '+m.primaryState+', '+m.primaryCountry+'.') as Address,'Available' as StatusText,pa.ID as AssetMainID,'" + radcombo_warehouse.SelectedItem.Text + "' as jobname,* from Prism_assets pa, PrismAssetName pan,PrsimWarehouses m where pa.AssetName=pan.ID and m.id=pa.warehouseid and pa.repairstatus='Ok' and pa.WarehouseId=" + radcombo_warehouse.SelectedValue + " and " +
            " pa.ID not in (select AssetID from PrismJobAssignedAssets where AssignmentStatus='Active') ";
            //query_top += " and a.warehouseId=" + radcombo_warehouse.SelectedValue + "";

        }
        //dtJobAssetsTop = SqlHelper.ExecuteDataset(GlobalConnetionString.ClientConnection(), CommandType.Text, query_warehouse).Tables[0];
        //string query = " select (m.primaryAddress1+', '+m.primaryAddress2+', '+m.primaryCity+', '+m.primaryState+', '+m.primaryCountry+'.') as Address,n.AssetName as AssetName,jas.StatusText as StatusText,a.ID as AssetMainID,* from Prism_Assets a,PrismAssetName n,PrismJobAssignedAssets pj,manageJobOrders m,PrsimJobAssetStatus jas where " +
        //" a.AssetName=n.Id and pj.JobId=m.jid and a.Id=pj.AssetId and pj.AssetStatus=jas.Id ";

        //if (combo_job.SelectedValue != "")
        //{
        //    query += " and m.jid=" + combo_job.SelectedValue + "";

        //}
        //else
        //{
        //    query += " and a.warehouseId=" + radcombo_warehouse.SelectedValue + "";

        //}
        foreach (GridDataItem item in RadgridForRemove.Items)
        {
            CheckBox chkSelect1 = (CheckBox)item.FindControl("chkSelect1");
            //Label lbl_AssetStatus = (Label)item.FindControl("lbl_AssetStatus");
            Label lbl_assetId1 = (Label)item.FindControl("lbl_assetId1");
            if (!chkSelect1.Checked)
            {
                hid_selectedbottom.Value += lbl_assetId1.Text + ",";
            }

        }
        string valbottomassets = "", bottomgrid = "";
        if (hid_selectedbottom.Value != "")
        {
            valbottomassets = hid_selectedbottom.Value.Remove(hid_selectedbottom.Value.Length - 1, 1);
        }
        if (valbottomassets != "")
        {
            bottomgrid = query_warehouse + "and pa.Id in (" + valbottomassets + ")";
            query_warehouse += " and pa.Id not in (" + valbottomassets + ")";
        }
        if (bottomgrid != "")
        {
            
            dt_JobAssetbottom = SqlHelper.ExecuteDataset(GlobalConnetionString.ClientConnection(), CommandType.Text, bottomgrid).Tables[0];
            RadgridForRemove.DataSource = dt_JobAssetbottom;
            RadgridForRemove.DataBind();
        }
        else
        {
            RadgridForRemove.DataSource = new string[] { };
            RadgridForRemove.DataBind();
            
        }
        dtJobAssetsTop = SqlHelper.ExecuteDataset(GlobalConnetionString.ClientConnection(), CommandType.Text, query_warehouse).Tables[0];
        btn_update.Visible = true;
        img_updatetotop.Visible = true;
        if (dtJobAssetsTop.Rows.Count > 0)
        {
            grdJobList.DataSource = dtJobAssetsTop;
            grdJobList.DataBind();
        }
    }
    protected void img_updatetotop_OnClick(object sender, EventArgs e)
    {
        bindtopgridfrombottomselected();
    }
    public void bindDetailsbottom()
    {

        td_bottom.Visible = true;
        //RadgridForRemove.Visible = true;
        hid_selectedids.Value = "";
        hid_selectedbottom.Value = "";
        string query_job = "", query_warehouse = "";
        if (combo_job.SelectedValue != "0")
        {
            query_warehouse = " select m.JobLocation as Address,m.CurveGroupName as jobname,n.AssetName as AssetName,jas.StatusText as StatusText,pa.ID as AssetMainID,* from Prism_Assets pa,PrismAssetName n,PrismJobAssignedAssets pj,[RigTrack].tblCurveGroup m,PrsimJobAssetStatus jas where " +
        " pa.AssetName=n.Id and pj.JobId=m.ID and pa.Id=pj.AssetId and pj.AssetStatus=jas.Id ";
            query_warehouse += " and m.ID=" + combo_job.SelectedValue + " and pj.AssignmentStatus='Active' ";

        }
        else
        {
            query_warehouse = "select pan.AssetName as AssetName,pa.SerialNumber,pa.ID as AssetMainID, (m.primaryAddress1+', '+m.primaryAddress2+', '+m.primaryCity+', '+m.primaryState+', '+m.primaryCountry+'.') as Address,'Available' as StatusText,'" + radcombo_warehouse.SelectedItem.Text + "' as jobname,* from Prism_assets pa, PrismAssetName pan,PrsimWarehouses m where pa.AssetName=pan.ID and m.id=pa.warehouseid and pa.repairstatus='Ok' and pa.WarehouseId=" + radcombo_warehouse.SelectedValue + " and " +
            " pa.ID not in (select AssetID from PrismJobAssignedAssets where AssignmentStatus='Active') ";
            //query_top += " and a.warehouseId=" + radcombo_warehouse.SelectedValue + "";
            //dtJobAssetsTop = SqlHelper.ExecuteDataset(GlobalConnetionString.ClientConnection(), CommandType.Text, query_warehouse).Tables[0];
        }
        //dtJobAssetsTop = SqlHelper.ExecuteDataset(GlobalConnetionString.ClientConnection(), CommandType.Text, query_warehouse).Tables[0];
        string existingingridone = "";
        foreach (GridDataItem item in grdJobList.Items)
        {
            CheckBox chkSelect = (CheckBox)item.FindControl("chkSelect");
            //Label lbl_AssetStatus = (Label)item.FindControl("lbl_AssetStatus");
            Label lbl_assetId = (Label)item.FindControl("lbl_assetId");
            if (chkSelect.Checked)
            {
                hid_selectedids.Value += lbl_assetId.Text + ",";
            }


        }
        foreach (GridDataItem item in RadgridForRemove.Items)
        {
            CheckBox chkSelect1 = (CheckBox)item.FindControl("chkSelect1");

            Label lbl_assetId1 = (Label)item.FindControl("lbl_assetId1");

            existingingridone += lbl_assetId1.Text + ",";


        }
        string valbottomassets = "", valAsset = "";
        if (hid_selectedids.Value != "")
        {
            valbottomassets = hid_selectedids.Value.Remove(hid_selectedids.Value.Length - 1, 1);
        }


        string bottomgrid = "";
        if (valbottomassets != "" && existingingridone != "")
        {
            string existingingridone1 = existingingridone.Remove(existingingridone.Length - 1, 1);
            bottomgrid = query_warehouse + "and pa.Id in (" + valbottomassets + "," + existingingridone1 + ")";
            query_warehouse += " and pa.Id not in (" + valbottomassets + "," + existingingridone1 + ")";
        }
        else if (valbottomassets != "")
        {
            bottomgrid = query_warehouse + "and pa.Id in (" + valbottomassets + ")";
            query_warehouse += " and pa.Id not in (" + valbottomassets + ")";
        }
        dtJobAssetsTop = SqlHelper.ExecuteDataset(GlobalConnetionString.ClientConnection(), CommandType.Text, query_warehouse).Tables[0];
        dt_JobAssetbottom = SqlHelper.ExecuteDataset(GlobalConnetionString.ClientConnection(), CommandType.Text, bottomgrid).Tables[0];
        if (dt_JobAssetbottom.Rows.Count == 0)
        {
            btn_update.Visible = false;
            img_updatetotop.Visible = false;
            //imgup.Visible = false;
        }
        else
        {
            btn_createticket.Enabled = true;
            btn_update.Visible = true;
            img_updatetotop.Visible = true;
            //imgup.Visible = true;
            RadgridForRemove.DataSource = dt_JobAssetbottom;
            RadgridForRemove.DataBind();

        }
        if (dtJobAssetsTop.Rows.Count > 0)
        {
            grdJobList.DataSource = dtJobAssetsTop;
            grdJobList.DataBind();
        }



    }
    protected void btn_viewinventory_Click(object sender, EventArgs e)
    {
        bindDetails();
        try
        {
            grdJobList.Visible = true;
            btn_createticket.Enabled = false;
            if (combo_job.SelectedValue != "0")
            {

                combo_job.Enabled = true;
                radcombo_warehouse.Enabled = false;
                SqlGetbottomJobs.SelectCommand = "select 0 as [jid],'Select JobName' as Jobname union select [ID],CurveGroupName + ' ('+JobNumber+')' as Jobname from [RigTrack].tblCurveGroup where isActive!='0' and " +
                                                    "(ID in(select jobid from PrismJobKits) or ID in (select jobid from PrismJobConsumables)) and ID not in(" + combo_job.SelectedValue + ")";
                radcombo_bottomjob.DataBind();
            }
            else
            {
                SqlGetbottomWarehouse.SelectCommand = "select Distinct wa.Name,wa.ID from PrsimWarehouses wa,Prism_Assets A where wa.ID=A.WarehouseId and wa.ID not in(" + radcombo_warehouse.SelectedValue + ")";
                combo_job.Enabled = false;
                radcombo_warehouse.Enabled = true;
                radcombo_bottomwarehouse.DataBind();
            }

            td_bottom.Visible = true;
            RadgridForRemove.Visible = true;
            RadgridForRemove.DataSource = new string[] { };
            RadgridForRemove.DataBind();
            RadgridForRemove.MasterTableView.ShowHeadersWhenNoRecords = true;
        }
        catch (Exception ex)
        {
        }
        //viewbutton.Attributes.Add("style", "disabled:false");
    }
    public void bindDetails()
    {
        try
        {
            string query_job = "", query_warehouse = "";// select (m.primaryAddress1+', '+m.primaryAddress2+', '+m.primaryCity+', '+m.primaryState+', '+m.primaryCountry+'.') as Address,n.AssetName as AssetName,jas.StatusText as StatusText,a.ID as AssetMainID,* from Prism_Assets a,PrismAssetName n,PrismJobAssignedAssets pj,manageJobOrders m,PrsimJobAssetStatus jas where " +
            //" a.AssetName=n.Id and pj.JobId=m.jid and a.Id=pj.AssetId and pj.AssetStatus=jas.Id ";

            if (combo_job.SelectedValue != "0" && combo_job.SelectedValue!="")
            {
                query_warehouse = " select m.JobLocation as Address,m.CurveGroupName as jobname,n.AssetName as AssetName,jas.StatusText as StatusText,pa.ID as AssetMainID,* from Prism_Assets pa,PrismAssetName n,PrismJobAssignedAssets pj,[RigTrack].tblCurveGroup m,PrsimJobAssetStatus jas where " +
            " pa.AssetName=n.Id and pj.JobId=m.ID and pa.Id=pj.AssetId and pj.AssetStatus=jas.Id ";
                query_warehouse += " and m.ID=" + combo_job.SelectedValue + " and pj.AssignmentStatus='Active'";
                //dtJobAssetsTop = SqlHelper.ExecuteDataset(GlobalConnetionString.ClientConnection(), CommandType.Text, query_job).Tables[0];
            }
            else
            {
                query_warehouse = "select pan.AssetName as AssetName,pa.SerialNumber,pa.ID as AssetMainID, (m.primaryAddress1+', '+m.primaryAddress2+', '+m.primaryCity+', '+m.primaryState+', '+m.primaryCountry+'.') as Address,'Available' as StatusText,'" + radcombo_warehouse.SelectedItem.Text + "' as jobname,* from Prism_assets pa, PrismAssetName pan,PrsimWarehouses m where pa.AssetName=pan.ID and m.id=pa.warehouseid and pa.repairstatus='Ok' and pa.WarehouseId=" + radcombo_warehouse.SelectedValue + " and " +
                " pa.ID not in (select AssetID from PrismJobAssignedAssets where AssignmentStatus='Active')";
                //query_top += " and a.warehouseId=" + radcombo_warehouse.SelectedValue + "";

            }
            dtJobAssetsTop = SqlHelper.ExecuteDataset(GlobalConnetionString.ClientConnection(), CommandType.Text, query_warehouse).Tables[0];
            if (dtJobAssetsTop.Rows.Count == 0)
            {
                btn_update.Visible = false;
                img_updatetotop.Visible = false;
            }
            else
            {
                RadgridForRemove.Visible = true;
                btn_update.Visible = true;
                img_updatetotop.Visible = true;
                grdJobList.DataSource = dtJobAssetsTop;
                grdJobList.DataBind();
                RadgridForRemove.DataSource = new string[] { };
                RadgridForRemove.DataBind();

            }
        }
        catch (Exception ex)
        {

        }
    }
    protected void btn_update_Click(object sender, EventArgs e)
    {
        bindDetailsbottom();
    }
    protected void btn_print_OnClick(object sender, EventArgs e)
    {
        if (hid_ticketid.Value != "0")
        {
            DataTable dt_gettickets = SqlHelper.ExecuteDataset(GlobalConnetionString.ClientConnection(), CommandType.Text, "select * from PrismShippingTicket where TicketID=" + hid_ticketid.Value + "").Tables[0];


            string selectque = "select n.AssetName+'('+pa.SerialNumber+')' as AssetNameID,ptd.FromLocation,ptd.FromLocationID, " +
                        " ptd.ToLocation,ptd.ToLocationID,ptd.AssetId as AssetID,ptd.Status as AssetMainStatus,* from PrismShippingTicket pt,PrismShippingTicketDetails ptd, " +
                        " Prism_Assets pa,PrismAssetName n where  pt.ID=ptd.TicketId and pa.AssetName=n.Id and pa.Id=ptd.AssetId and pt.TicketID=" + hid_ticketid.Value + "";
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
            excelWorksheet.Cells[6, 4] = dt_gettickets.Rows[0]["TicketID"].ToString();
            //KIt/AssetName
            excelWorksheet.Cells[8, 4] = dt_gettickets.Rows[0]["CreatedDate"].ToString();
            //Location
            excelWorksheet.Cells[6, 7] = dt_gettickets.Rows[0]["ShippingDate"].ToString();
            //Operator
            excelWorksheet.Cells[8, 7] = dt_gettickets.Rows[0]["Status"].ToString();


            //DataTable dt = SqlHelper.ExecuteDataset(GlobalConnetionString.ClientConnection(), CommandType.Text, "").Tables[0];
            int row = 13;

            for (int r = 0; r < dt_ticketdetails.Rows.Count; r++)
            {

                string lbl_FromLocationAddress = "", lbl_JobName = "", lbl_ToLocationAddress = "", lbl_JobWarehouseName = "";
                if (dt_ticketdetails.Rows[r]["FromLocation"].ToString() == "Job")
                {
                    DataTable dtgetaddfromjob = SqlHelper.ExecuteDataset(GlobalConnetionString.ClientConnection(), CommandType.Text, "select JobLocation as Address,CurveGroupName as JobName from [RigTrack].tblCurveGroup where ID=" + dt_ticketdetails.Rows[r]["FromLocationID"].ToString() + "").Tables[0];
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
                    DataTable dtgetaddfromjob = SqlHelper.ExecuteDataset(GlobalConnetionString.ClientConnection(), CommandType.Text, "select JobLocation as Address,CurveGroupName as JobName from [RigTrack].tblCurveGroup where ID=" + dt_ticketdetails.Rows[r]["ToLocationID"].ToString() + "").Tables[0];
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
                int col1 = 3;
                int col2 = 4;
                int col3 = 5;
                int col4 = 6;
                int col5 = 7;
                int col6 = 8;
                excelWorksheet.Cells[rownumber, col1] = dt_ticketdetails.Rows[r]["AssetNameID"].ToString();
                excelWorksheet.Cells[rownumber, col2] = lbl_JobName;
                excelWorksheet.Cells[rownumber, col3] = lbl_FromLocationAddress;
                excelWorksheet.Cells[rownumber, col4] = lbl_JobWarehouseName;
                excelWorksheet.Cells[rownumber, col5] = lbl_ToLocationAddress;
                excelWorksheet.Cells[rownumber, col6] = dt_ticketdetails.Rows[r]["AssetMainStatus"].ToString();
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
    }
    protected void grdJobList_ItemDataBound(object sender, GridItemEventArgs e)
    {

        if (e.Item is GridDataItem)
        {
            GridDataItem item = (GridDataItem)e.Item;
            //CheckBox chkSelect = (CheckBox)item.FindControl("chkSelect");
            //Label lbl_AssetStatus = (Label)item.FindControl("lbl_AssetStatus");
            //Label lbl_assetId = (Label)item.FindControl("lbl_assetId");
            //if (lbl_AssetStatus.Text == "Delivered")
            //{
            //    chkSelect.Enabled = false;
            //}
            //else
            //{
            //    chkSelect.Enabled = true;
            //}
            //if (chkSelect.Checked)
            //{
            //    hid_selectedids.Value += lbl_assetId.Text + ",";
            //}
            //GridDataItem item = (GridDataItem)e.Item;
            //TableCell cell = (TableCell)item["UniqueName"];
            //cell.BackColor = System.Drawing.Color.Red;
        }
    }
    protected void btn_reset_Click(object sender, EventArgs e)
    {
        combo_job.Items.Clear();
        combo_job.Items.Add(new RadComboBoxItem("-Select-", "0"));
        ddlCompany.SelectedValue = "0";
        radcombo_warehouse.Enabled = true;
        combo_job.Enabled = true;
        radcombo_warehouse.SelectedValue = String.Empty;
        grdJobList.DataSource = new string[] { };
        grdJobList.DataBind();
        RadgridForRemove.DataSource = new string[] { };
        RadgridForRemove.DataBind();
        grdJobList.Visible = false;
        RadgridForRemove.Visible = false;
        radcombo_bottomjob.DataBind();
        radcombo_bottomwarehouse.DataBind();
        radcombo_bottomjob.SelectedValue = "0";
        radcombo_bottomwarehouse.SelectedValue = String.Empty;

        btn_update.Visible = false;
        img_updatetotop.Visible = false;
        td_bottom.Visible = false;

    }
    public string GenerateNewAccountID(Int32 StartingKey)
    {
        //following SRP Example under a billion...
        Random rnd = new Random();
        return (string)rnd.Next(10000000, 21474836).ToString();
    }
    protected void btn_createticket_OnClick(object sender, EventArgs e)
    {
        string ticketid = GenerateNewAccountID(0);
        string message = "";
        if (combo_job.SelectedValue != "0")
        {
            if (radcombo_bottomjob.SelectedValue != "0")
            {//JOB TO JOB
                SqlConnection db = new SqlConnection(GlobalConnetionString.ClientConnection());
                SqlTransaction transaction;
                db.Open();
                transaction = db.BeginTransaction();
                string queryjobassetUpdate = "", queryjobpersonalUpdate = "", queryjobserviceUpdate = "";

                string inserttickets = "insert into PrismShippingTicket(ShippingDate,Status,TicketId,CreatedDate,ModifiedDate)values('" + radtxt_start.SelectedDate + "','Open','" + ticketid + "','" + DateTime.Now + "','" + DateTime.Now + "')";
                SqlHelper.ExecuteNonQuery(transaction, CommandType.Text, inserttickets);

                DataTable dt_TicketId = SqlHelper.ExecuteDataset(transaction, CommandType.Text, "SELECT * FROM    PrismShippingTicket WHERE  ID = IDENT_CURRENT('PrismShippingTicket')").Tables[0];
                foreach (GridDataItem item in RadgridForRemove.Items)
                {
                    CheckBox chkSelect = (CheckBox)item.FindControl("chkSelect1");
                    //Label lbl_AssetStatus = (Label)item.FindControl("lbl_AssetStatus");
                    Label lbl_assetId = (Label)item.FindControl("lbl_assetId1");
                    Label lbl_AssetStatus = (Label)item.FindControl("lbl_AssetStatus1");
                    try
                    {
                        queryjobassetUpdate = "Update PrismJobAssignedAssets set AssignmentStatus='InActive' where JobId=" + combo_job.SelectedValue + " and AssetId=" + lbl_assetId.Text;
                        SqlHelper.ExecuteNonQuery(transaction, CommandType.Text, queryjobassetUpdate);

                        string queryjobassetInsert = "Insert into PrismJobAssignedAssets(JobId,AssetId,AssetStatus,ModifiedBy,ModifiedDate,AssignmentStatus)" +
                           " values('" + radcombo_bottomjob.SelectedValue + "','" + lbl_assetId.Text + "','1','" + Session["userId"].ToString() + "','" + DateTime.Now + "','Active')";
                        SqlHelper.ExecuteNonQuery(transaction, CommandType.Text, queryjobassetInsert);

                        string fromlocationtype = "", fromlocationid = "", tolocationtype = "", tolocationid = "", currentlocationtype = "", currentlocationid = "", assetstatus = "", assetid = "";

                        string query_existassetloc = "SELECT top(1) * FROM    PrismAssetCurrentLocation WHERE AssetID=" + lbl_assetId.Text + " order by AssetCurrentLocationID desc";
                        DataTable dt_existassetlocation = SqlHelper.ExecuteDataset(GlobalConnetionString.ClientConnection(), CommandType.Text, query_existassetloc).Tables[0];
                        DataTable dt_assetlocation = SqlHelper.ExecuteDataset(GlobalConnetionString.ClientConnection(), CommandType.Text, "select * from Prism_Assets where ID=" + lbl_assetId.Text + "").Tables[0];
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
                        tolocationid = radcombo_bottomjob.SelectedValue.ToString();
                        if (tolocationid == "")
                            tolocationid = "0";
                        assetstatus = "Available";

                        string insert_locationtype = "insert into PrismAssetCurrentLocation(AssetID,FromLocationType,FromLocationID,ToLocationType,ToLocationID,AssetStatus,CurrentLocationType,CurrentLocationID)values(" +
                            "" + lbl_assetId.Text + ",'" + fromlocationtype + "'," + fromlocationid + ",'" + tolocationtype + "'," + tolocationid + ",'" + assetstatus + "','" + currentlocationtype + "','" + currentlocationid + "')";
                        SqlHelper.ExecuteNonQuery(transaction, CommandType.Text, insert_locationtype);

                        //INSERT SHIPPING TICKET DETAILS

                        string insertticketdetails = "insert into PrismShippingTicketDetails(TicketId,AssetId,FromLocation,FromLocationID,ToLocation,ToLocationID,Status)" +
                            "values(" + dt_TicketId.Rows[0]["ID"].ToString() + "," + lbl_assetId.Text + ",'" + fromlocationtype + "'," + fromlocationid + ",'" + tolocationtype + "'," +
                            "" + tolocationid + ",'" + lbl_AssetStatus.Text + "')";
                        SqlHelper.ExecuteNonQuery(transaction, CommandType.Text, insertticketdetails);


                        //string notificationsendtowhome = eventNotification.sendEventNotification("AC02");
                        //if (notificationsendtowhome != "")
                        //{
                        //    bool status = eventNotification.sendEventNotificationWithEmail(notificationsendtowhome, "AC02", "JOB", radcombo_bottomjob.SelectedValue, radcombo_bottomjob.SelectedItem.Text,
                        //           "", "", "AssetReassigned", "", lbl_assetId.Text);

                        //}
                        transaction.Commit();
                        message = "Success";
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        message = "Error";
                    }
                    finally
                    {
                    }


                }
            }
            else
            {//JOB TO WAREHOUSE
                SqlConnection db = new SqlConnection(GlobalConnetionString.ClientConnection());
                SqlTransaction transaction;
                db.Open();
                transaction = db.BeginTransaction();
                string queryjobassetUpdate = "", insertquery = "";
                try
                {
                    string inserttickets = "insert into PrismShippingTicket(ShippingDate,Status,TicketId,CreatedDate,ModifiedDate)values('" + radtxt_start.SelectedDate + "','Open','" + ticketid + "','" + DateTime.Now + "','" + DateTime.Now + "')";
                    SqlHelper.ExecuteNonQuery(transaction, CommandType.Text, inserttickets);

                    DataTable dt_TicketId = SqlHelper.ExecuteDataset(transaction, CommandType.Text, "SELECT * FROM    PrismShippingTicket WHERE  ID = IDENT_CURRENT('PrismShippingTicket')").Tables[0];
                    //Jobs with Assets insertion
                    foreach (GridDataItem item in RadgridForRemove.Items)
                    {
                        CheckBox chkSelect = (CheckBox)item.FindControl("chkSelect1");
                        //Label lbl_AssetStatus = (Label)item.FindControl("lbl_AssetStatus");
                        Label lbl_assetId = (Label)item.FindControl("lbl_assetId1");
                        Label lbl_AssetStatus = (Label)item.FindControl("lbl_AssetStatus1");


                        queryjobassetUpdate = "update PrismJobAssignedAssets set AssignmentStatus='InActive'  where JobId=" + combo_job.SelectedValue + " and AssetId=" + lbl_assetId.Text;
                        SqlHelper.ExecuteNonQuery(transaction, CommandType.Text, queryjobassetUpdate);

                        string queryprismassetsUpdate = "update Prism_Assets set repairstatus='Ok'  where Id=" + lbl_assetId.Text;
                        SqlHelper.ExecuteNonQuery(transaction, CommandType.Text, queryprismassetsUpdate);

                        string fromlocationtype = "", fromlocationid = "", tolocationtype = "", tolocationid = "", assetstatus = "", assetcurrentlocid = "", currentlocationtype = "", currentlocationid = "";

                        string query_existassetloc = "SELECT top(1) * FROM    PrismAssetCurrentLocation WHERE    AssetID=" + lbl_assetId.Text + " order by AssetCurrentLocationID desc";
                        DataTable dt_existassetlocation = SqlHelper.ExecuteDataset(transaction, CommandType.Text, query_existassetloc).Tables[0];
                        DataTable dt_assetlocation = SqlHelper.ExecuteDataset(transaction, CommandType.Text, "select * from Prism_Assets where ID=" + lbl_assetId.Text + "").Tables[0];

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
                            "" + lbl_assetId.Text + ",'" + fromlocationtype + "'," + fromlocationid + ",'" + tolocationtype + "'," + tolocationid + ",'" + assetstatus + "','" + currentlocationtype + "','" + currentlocationid + "')";
                            SqlHelper.ExecuteNonQuery(transaction, CommandType.Text, insert_locationtype);

                        }


                        string insertticketdetails = "insert into PrismShippingTicketDetails(TicketId,AssetId,FromLocation,FromLocationID,ToLocation,ToLocationID,Status)" +
                            "values(" + dt_TicketId.Rows[0]["ID"].ToString() + "," + lbl_assetId.Text + ",'" + fromlocationtype + "'," + fromlocationid + ",'" + tolocationtype + "'," +
                            "" + tolocationid + ",'" + lbl_AssetStatus.Text + "')";
                        SqlHelper.ExecuteNonQuery(transaction, CommandType.Text, insertticketdetails);


                        //string notificationsendtowhome = eventNotification.sendEventNotification("JAMWH01");
                        //if (notificationsendtowhome != "")
                        //{
                        //    bool status = eventNotification.sendEventNotificationWithEmail(notificationsendtowhome, "JAMWH01", "JOB", radcombo_bottomjob.SelectedValue, combo_job.SelectedItem.Text,
                        //           "", "", "MoveToWarehouse", "", lbl_assetId.Text);

                        //}



                    }
                    transaction.Commit();
                    message = "Success";

                }
                catch (Exception ex) { transaction.Rollback(); message = "Error"; }
                finally
                {
                }
            }
        }
        else if (radcombo_warehouse.SelectedValue != "")
        {//WAREHOUSE TO JOB
            SqlConnection db = new SqlConnection(GlobalConnetionString.ClientConnection());
            SqlTransaction transaction;
            db.Open();
            transaction = db.BeginTransaction();
            string notavailassets = "", assignedassetnames = "";
            string queryjobassetInsert = "", queryjobpersonalInsert = "";
            string inserttickets = "insert into PrismShippingTicket(ShippingDate,Status,TicketId,CreatedDate,ModifiedDate)values('" + radtxt_start.SelectedDate + "','Open','" + ticketid + "','" + DateTime.Now + "','" + DateTime.Now + "')";
            SqlHelper.ExecuteNonQuery(transaction, CommandType.Text, inserttickets);

            DataTable dt_TicketId = SqlHelper.ExecuteDataset(transaction, CommandType.Text, "SELECT * FROM    PrismShippingTicket WHERE  ID = IDENT_CURRENT('PrismShippingTicket')").Tables[0];
            foreach (GridDataItem item in RadgridForRemove.Items)
            {
                CheckBox chkSelect = (CheckBox)item.FindControl("chkSelect1");
                //Label lbl_AssetStatus = (Label)item.FindControl("lbl_AssetStatus");
                Label lbl_assetId = (Label)item.FindControl("lbl_assetId1");
                Label lbl_AssetStatus = (Label)item.FindControl("lbl_AssetStatus1");
                try
                {
                    string fromlocationtype = "", fromlocationid = "", tolocationtype = "", tolocationid = "", currentlocationtype = "", currentlocationid = "", assetstatus = "", assetid = "";
                    queryjobassetInsert = "Insert into PrismJobAssignedAssets(JobId,AssetId,AssetStatus,ModifiedBy,ModifiedDate,AssignmentStatus)" +
                       " values('" + radcombo_bottomjob.SelectedValue + "','" + lbl_assetId.Text + "','1','" + Session["userId"].ToString() + "','" + DatesbetweenDatatable.getdatetimeformat(DateTime.Now.ToString()) + "','Active')";
                    SqlHelper.ExecuteNonQuery(transaction, CommandType.Text, queryjobassetInsert);
                    string query_existassetloc = "SELECT top(1) * FROM    PrismAssetCurrentLocation WHERE   AssetID=" + lbl_assetId.Text + " order by AssetCurrentLocationID desc";
                    DataTable dt_existassetlocation = SqlHelper.ExecuteDataset(transaction, CommandType.Text, query_existassetloc).Tables[0];
                    DataTable dt_assetlocation = SqlHelper.ExecuteDataset(transaction, CommandType.Text, "select * from Prism_Assets where ID=" + lbl_assetId.Text + "").Tables[0];
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
                    tolocationid = radcombo_bottomjob.SelectedValue.ToString();
                    assetstatus = "Available";

                    string insert_locationtype = "insert into PrismAssetCurrentLocation(AssetID,FromLocationType,FromLocationID,ToLocationType,ToLocationID,AssetStatus,CurrentLocationType,CurrentLocationID)values(" +
                        "" + lbl_assetId.Text + ",'" + fromlocationtype + "'," + fromlocationid + ",'" + tolocationtype + "'," + tolocationid + ",'" + assetstatus + "','" + currentlocationtype + "','" + currentlocationid + "')";
                    SqlHelper.ExecuteNonQuery(transaction, CommandType.Text, insert_locationtype);


                    string insertticketdetails = "insert into PrismShippingTicketDetails(TicketId,AssetId,FromLocation,FromLocationID,ToLocation,ToLocationID,Status)" +
                            "values(" + dt_TicketId.Rows[0]["ID"].ToString() + "," + lbl_assetId.Text + ",'" + fromlocationtype + "'," + fromlocationid + ",'" + tolocationtype + "'," +
                            "" + tolocationid + ",'" + lbl_AssetStatus.Text + "')";
                    SqlHelper.ExecuteNonQuery(transaction, CommandType.Text, insertticketdetails);


                    //      string info = "Following " + combo_assets.Items[asset].Text + " is Assigned to Job:" + combo_job.SelectedItem.Text;
                    //      string str_insert_q = "insert into PrismEvent(eventCode,eventInfo,eventTime,userAssignedID,userAssignedTimeStamp,Source)values(" +
                    //    "'PA01','" + info + "','" + DatesbetweenDatatable.getdatetimeformat(DateTime.Now.ToString()) + "'," + combo_assets.Items[asset].Value + "" +
                    //",'" + DatesbetweenDatatable.getdatetimeformat(DateTime.Now.ToString()) + "','SALE')";
                    //      SqlHelper.ExecuteNonQuery(GlobalConnetionString.ClientConnection(), CommandType.Text, str_insert_q);
                    //string notificationsendtowhome = eventNotification.sendEventNotification("AC02");
                    //if (notificationsendtowhome != "")
                    //{
                    //    bool status = eventNotification.sendEventNotificationWithEmail(notificationsendtowhome, "AC02", "JOB", radcombo_bottomjob.SelectedValue, radcombo_bottomjob.SelectedItem.Text,
                    //           "", "", "Assetassigned", "", lbl_assetId.Text);

                    //}
                    transaction.Commit();
                    message = "Success";
                }
                catch (Exception ex) { 
                    transaction.Rollback(); message = "Error"; }
                finally
                {
                }


            }
        }
        if (message == "Success")
        {
            lbl_message.Text = "Ticket <b>" + ticketid + "</b> Created Successfully";
            lbl_message.ForeColor = Color.Green;
            hid_ticketid.Value = ticketid;
            btn_print.Visible = false;
            btn_createticket.Enabled = false;
        }
        else
        {
            hid_ticketid.Value = "0";
            lbl_message.Text = "Error";
            lbl_message.ForeColor = Color.Red;
            btn_print.Visible = false;
            btn_createticket.Enabled = true;
        }
    }
}