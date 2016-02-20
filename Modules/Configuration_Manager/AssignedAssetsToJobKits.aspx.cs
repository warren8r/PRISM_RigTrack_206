using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using System.Drawing;
using System.Data.SqlClient;

public partial class Modules_Configuration_Manager_AssignedAssetsToJobKits : System.Web.UI.Page
{
    int totalcnt = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        btn_save.Visible = false;
        btn_reset.Visible = false;
        btn_saving.Visible = false;
        string val = combo_job.SelectedValue;
        if (val != "")
        {
            binddata();
            //bindconsumables();
            DataTable dt_getkitdetfromjobid = SqlHelper.ExecuteDataset(GlobalConnetionString.ClientConnection(), CommandType.Text, "select * from PrismJobKits where jobid=" + combo_job.SelectedValue + " and Finalizedstatus='Finalized'").Tables[0];
            if (dt_getkitdetfromjobid.Rows.Count > 0)
            {
                btn_save.Enabled = false;
                btn_reset.Visible = true;
                btn_saving.Enabled = false;
            }
            else
            {
                btn_save.Enabled = true;
                btn_reset.Visible = true;
                btn_saving.Enabled = true;
            }
        }
    }
    protected void btn_view_OnClick(object sender, EventArgs e)
    {
        btn_save.Visible = true;
        btn_reset.Visible = true;
        btn_saving.Visible = true;
        lbl_message.Text = "";
        binddata();
        //bindconsumables();
        
        if (totalcnt == 0)
        {
            btn_save.Visible = false;
            btn_reset.Visible = false;
            btn_saving.Visible = false;
            lbl_message.Text = "No Data Found";
            lbl_message.ForeColor = Color.Red;
        }
        
    }
    public void binddata()
    {
        pnl_adddet.Controls.Clear();

        pnl_adddet.Controls.Add(new LiteralControl("<table border='0' cellpadding='0' cellspacing='0' width='100%'><tr><td align='center'>"));
        DataTable dt_getkitdetfromjobid = SqlHelper.ExecuteDataset(GlobalConnetionString.ClientConnection(), CommandType.Text, "select * from PrismJobKits where jobid=" + combo_job.SelectedValue + "").Tables[0];
        if (dt_getkitdetfromjobid.Rows.Count > 0)
        {
            for (int i = 0; i < dt_getkitdetfromjobid.Rows.Count; i++)
            {
                //PrismAssetKitDetails
                pnl_adddet.Controls.Add(new LiteralControl("<table border='1'>"));
                DataTable dt_kitname = SqlHelper.ExecuteDataset(GlobalConnetionString.ClientConnection(), CommandType.Text, "select * from PrismAssetKitDetails where assetkitid=" + dt_getkitdetfromjobid.Rows[i]["kitid"].ToString() + "").Tables[0];
                pnl_adddet.Controls.Add(new LiteralControl("<tr>"));
                pnl_adddet.Controls.Add(new LiteralControl("<td colspan='3' align='left'><b>Kit Name:" + dt_kitname.Rows[0]["KitName"].ToString() + "</b></td>"));
                pnl_adddet.Controls.Add(new LiteralControl("</tr>"));
                //PrismKitAssetFromKitName//
                DataTable dtgetexistdetails = SqlHelper.ExecuteDataset(GlobalConnetionString.ClientConnection(), CommandType.Text, "select * from PrismJobAssignedKitAssetDetails where jobid=" + combo_job.SelectedValue + "").Tables[0];


                DataTable dt_getassetids = SqlHelper.ExecuteDataset(GlobalConnetionString.ClientConnection(), CommandType.Text, "select * from PrismKitAssetFromKitName where assetkitid=" + dt_getkitdetfromjobid.Rows[i]["kitid"].ToString() + "").Tables[0];
                for (int j = 0; j < dt_getassetids.Rows.Count; j++)
                {

                    DataTable dt_getassetnames = SqlHelper.ExecuteDataset(GlobalConnetionString.ClientConnection(), CommandType.Text, "select * from PrismAssetName where ID=" + dt_getassetids.Rows[j]["assetids"].ToString() + "").Tables[0];
                    DataRow[] dr_getdet = dtgetexistdetails.Select("kitid=" + dt_kitname.Rows[0]["assetkitid"].ToString() + " and assetnameid=" + dt_getassetids.Rows[j]["assetids"].ToString() + "");
                    int qtyofasset = Convert.ToInt32(dt_getassetids.Rows[j]["qty"]);
                    string querygetserialno = "select P.Id as ID,PA.AssetName+' ('+SerialNumber+')' as  AssetName from Prism_Assets P,PrismAssetName PA" +
                        " where  P.AssetName=PA.ID and  (P.id not in (select AssetId from PrismJobAssignedAssets,manageJobOrders " +
                        " where jid=PrismJobAssignedAssets.JobId and status='Approved') or P.Id in (select AssetId from PrismJobAssignedKitAssetDetails " +
                        " where jobid=" + combo_job.SelectedValue + " and kitid=" + dt_kitname.Rows[0]["assetkitid"].ToString() + " and assetnameid=" + dt_getassetids.Rows[j]["assetids"].ToString() + "))" +
                        " and PA.ID=" + dt_getassetids.Rows[j]["assetids"].ToString() + " and P.repairstatus<>'Maintenance'";
                    DataTable dt_getserialnumbers = SqlHelper.ExecuteDataset(GlobalConnetionString.ClientConnection(), CommandType.Text, querygetserialno).Tables[0];
                    //PrismAssetName bringing assetname
                    for (int k = 0; k < qtyofasset; k++)
                    {

                        pnl_adddet.Controls.Add(new LiteralControl("<tr>"));
                        pnl_adddet.Controls.Add(new LiteralControl("<td align='right'>" + dt_getassetnames.Rows[0]["AssetName"].ToString() + "</td>"));
                        RadComboBox combo_serialnumberforasset = new RadComboBox();
                        combo_serialnumberforasset.ID = "combo_" + k + "_" + j + "_" + i;

                        combo_serialnumberforasset.DataTextField = "AssetName";
                        combo_serialnumberforasset.DataValueField = "ID";
                        combo_serialnumberforasset.OnClientSelectedIndexChanged = "OnClientSelectedIndexChanged";
                        combo_serialnumberforasset.DataSource = dt_getserialnumbers;
                        combo_serialnumberforasset.DataBind();
                        combo_serialnumberforasset.Items.Insert(0, new RadComboBoxItem("Select", "Select"));
                        pnl_adddet.Controls.Add(new LiteralControl("<td>"));
                        pnl_adddet.Controls.Add(combo_serialnumberforasset);
                        pnl_adddet.Controls.Add(new LiteralControl("</td>"));
                        RadComboBox combo_assignment = new RadComboBox();
                        combo_assignment.ID = "combo_assignments" + k + "_" + j + "_" + i;
                        combo_assignment.Items.Insert(0, new RadComboBoxItem("Assign Now", "Assign Now"));
                        combo_assignment.Items.Insert(1, new RadComboBoxItem("Assign Later", "Assign Later"));
                        pnl_adddet.Controls.Add(new LiteralControl("<td>"));
                        pnl_adddet.Controls.Add(combo_assignment);
                        pnl_adddet.Controls.Add(new LiteralControl("</td>"));
                        pnl_adddet.Controls.Add(new LiteralControl("</tr>"));
                        if (dr_getdet.Length > 0)
                        {
                            //    if (dtgetexistdetails.Rows[k]["number"].ToString() == k.ToString())
                            //    {
                            combo_assignment.SelectedValue = dr_getdet[k]["assigned"].ToString();
                            if (dr_getdet[k]["assetid"].ToString() != "")
                            {
                                combo_serialnumberforasset.SelectedValue = dr_getdet[k]["assetid"].ToString();
                            }
                            //    }
                        }
                    }


                }
                pnl_adddet.Controls.Add(new LiteralControl("</table>"));
            }
            pnl_adddet.Controls.Add(new LiteralControl("</td></tr></table>"));
            totalcnt++;
        }
        else
        {

        }
    }
    public void bindconsumables()
    {
        //pnl_consumables.Controls.Clear();
        //pnl_consumables.Controls.Add(new LiteralControl("<table border='0' cellpadding='0' cellspacing='0' width='100%'><tr><td align='center'>"));
        //DataTable dt_getconsumables = SqlHelper.ExecuteDataset(GlobalConnetionString.ClientConnection(), CommandType.Text, "select * from PrismJobConsumables where jobid=" + combo_job.SelectedValue + "").Tables[0];
        //if (dt_getconsumables.Rows.Count > 0)
        //{
        //    pnl_consumables.Controls.Add(new LiteralControl("<table border='1'>"));
        //    pnl_consumables.Controls.Add(new LiteralControl("<tr>"));
        //    pnl_consumables.Controls.Add(new LiteralControl("<td align='center'><b>Consumable Name</b></td>"));
        //    pnl_consumables.Controls.Add(new LiteralControl("<td align='center'><b>Qty</b></td>"));
        //    pnl_consumables.Controls.Add(new LiteralControl("</tr>"));
        //    for (int i = 0; i < dt_getconsumables.Rows.Count; i++)
        //    {

        //        DataTable dt_consumablename = SqlHelper.ExecuteDataset(GlobalConnetionString.ClientConnection(), CommandType.Text, "select * from Consumables where ConId=" + dt_getconsumables.Rows[i]["consumableid"].ToString() + "").Tables[0];
        //        pnl_consumables.Controls.Add(new LiteralControl("<tr>"));
        //        pnl_consumables.Controls.Add(new LiteralControl("<td align='left'>" + dt_consumablename.Rows[0]["ConName"].ToString() + "</td>"));
        //        TextBox txt_qty = new TextBox();
        //        txt_qty.ID = "txt_consumable" + i;
        //        txt_qty.Text = dt_getconsumables.Rows[i]["qty"].ToString();
        //        pnl_consumables.Controls.Add(new LiteralControl("<td>"));
        //        pnl_consumables.Controls.Add(txt_qty);
        //        pnl_consumables.Controls.Add(new LiteralControl("</td>"));
        //        pnl_consumables.Controls.Add(new LiteralControl("</tr>"));
        //    }
        //    pnl_consumables.Controls.Add(new LiteralControl("</table></td></tr></table>"));
        //    totalcnt++;
        //}
        //else
        //{

        //}
    }
    protected void btn_save_OnClick(object sender, EventArgs e)
    {
        SqlConnection db = new SqlConnection(GlobalConnetionString.ClientConnection());
        SqlTransaction transaction;
        db.Open();
        transaction = db.BeginTransaction();
        try
        {
            string querydel = "delete from PrismJobAssignedKitAssetDetails where jobid=" + combo_job.SelectedValue + "";
            SqlHelper.ExecuteNonQuery(transaction, CommandType.Text, querydel);
            string queryjobassetInsert = "", assignedassetnames = "";
            DataTable dt_getkitdetfromjobid = SqlHelper.ExecuteDataset(transaction, CommandType.Text, "select * from PrismJobKits where jobid=" + combo_job.SelectedValue + "").Tables[0];
            DataTable dtgetexistdetails = SqlHelper.ExecuteDataset(transaction, CommandType.Text, "select * from PrismJobAssignedKitAssetDetails where jobid=" + combo_job.SelectedValue + "").Tables[0];
            if (dt_getkitdetfromjobid.Rows.Count > 0)
            {
                for (int i = 0; i < dt_getkitdetfromjobid.Rows.Count; i++)
                {
                    DataTable dt_kitname = SqlHelper.ExecuteDataset(transaction, CommandType.Text, "select * from PrismAssetKitDetails where assetkitid=" + dt_getkitdetfromjobid.Rows[i]["kitid"].ToString() + "").Tables[0];
                    DataTable dt_getassetids = SqlHelper.ExecuteDataset(transaction, CommandType.Text, "select * from PrismKitAssetFromKitName where assetkitid=" + dt_getkitdetfromjobid.Rows[i]["kitid"].ToString() + "").Tables[0];
                    for (int j = 0; j < dt_getassetids.Rows.Count; j++)
                    {
                        DataTable dt_getassetnames = SqlHelper.ExecuteDataset(transaction, CommandType.Text, "select * from PrismAssetName where ID=" + dt_getassetids.Rows[j]["assetids"].ToString() + "").Tables[0];

                        int qtyofasset = Convert.ToInt32(dt_getassetids.Rows[j]["qty"]);
                        for (int k = 0; k < qtyofasset; k++)
                        {
                            string combo_serialnumberforassetid = "combo_" + k + "_" + j + "_" + i;
                            RadComboBox combo_serialnumberforasset = pnl_adddet.FindControl(combo_serialnumberforassetid) as RadComboBox;
                            string assignnowornotid = "combo_assignments" + k + "_" + j + "_" + i;
                            RadComboBox combo_assignment = pnl_adddet.FindControl(assignnowornotid) as RadComboBox;
                            //DataRow[] dr_getexistrow = dtgetexistdetails.Select("assetid=" + combo_serialnumberforasset.SelectedValue + " and assigned='" + combo_assignment.SelectedValue + "'");
                            int cnt = k + 1;
                            //if (dr_getexistrow.Length == 0)
                            //{
                                if (combo_assignment.SelectedValue == "Assign Now")
                                {
                                    if (combo_serialnumberforasset.SelectedValue != "Select")
                                    {
                                        string fromlocationtype = "", fromlocationid = "", tolocationtype = "", tolocationid = "", currentlocationtype = "", currentlocationid = "", assetstatus = "", assetid = "";
                                        queryjobassetInsert = "Insert into PrismJobAssignedAssets(JobId,AssetId,AssetStatus,ModifiedBy,ModifiedDate,KitName,AssignmentStatus)" +
                                            " values('" + combo_job.SelectedValue + "','" + combo_serialnumberforasset.SelectedValue + "','1','" + Session["userId"].ToString() + "','" + DatesbetweenDatatable.getdatetimeformat(DateTime.Now.ToString()) + "','" + dt_kitname.Rows[0]["KitName"].ToString() + "','Active')";
                                        SqlHelper.ExecuteNonQuery(transaction, CommandType.Text, queryjobassetInsert);

                                        string insertq = "insert into PrismJobAssignedKitAssetDetails(jobid,kitid,assetnameid,number,assigned,assetid)values(" +
                                            "" + combo_job.SelectedValue + "," + dt_getkitdetfromjobid.Rows[i]["kitid"].ToString() + "," +
                                        "" + dt_getassetnames.Rows[0]["Id"].ToString() + "," + cnt + ",'" + combo_assignment.SelectedValue + "'," +
                                        "" + combo_serialnumberforasset.SelectedValue + ")";
                                        SqlHelper.ExecuteNonQuery(transaction, CommandType.Text, insertq);
                                        string query_existassetloc = "SELECT top(1) * FROM    PrismAssetCurrentLocation WHERE   AssetID=" + combo_serialnumberforasset.SelectedValue + " order by AssetCurrentLocationID desc";
                                        DataTable dt_existassetlocation = SqlHelper.ExecuteDataset(transaction, CommandType.Text, query_existassetloc).Tables[0];
                                        DataTable dt_assetlocation = SqlHelper.ExecuteDataset(transaction, CommandType.Text, "select * from Prism_Assets where ID=" + combo_serialnumberforasset.SelectedValue + "").Tables[0];
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

                                        string insert_locationtype = "insert into PrismAssetCurrentLocation(AssetID,FromLocationType,FromLocationID,ToLocationType,ToLocationID,AssetStatus,CurrentLocationType,CurrentLocationID)values(" +
                                            "" + combo_serialnumberforasset.SelectedValue + ",'" + fromlocationtype + "'," + fromlocationid + ",'" + tolocationtype + "'," + tolocationid + ",'" + assetstatus + "','" + currentlocationtype + "','" + currentlocationid + "')";
                                        SqlHelper.ExecuteNonQuery(transaction, CommandType.Text, insert_locationtype);

                                        string info = "Following " + combo_serialnumberforasset.SelectedValue + " is Assigned to Job:" + combo_job.SelectedItem.Text;
                                        string str_insert_q = "insert into PrismEvent(eventCode,eventInfo,eventTime,userAssignedID,userAssignedTimeStamp,Source)values(" +
                                        "'PA01','" + info + "','" + DatesbetweenDatatable.getdatetimeformat(DateTime.Now.ToString()) + "'," + combo_serialnumberforasset.SelectedValue + "" +
                                    ",'" + DatesbetweenDatatable.getdatetimeformat(DateTime.Now.ToString()) + "','SALE')";
                                        SqlHelper.ExecuteNonQuery(transaction, CommandType.Text, str_insert_q);
                                        assignedassetnames += combo_serialnumberforasset.SelectedValue;
                                    }
                                }
                                else
                                {
                                    string insertq_assignlater = "";
                                    if (combo_serialnumberforasset.SelectedValue != "Select")
                                    {
                                        insertq_assignlater = "insert into PrismJobAssignedKitAssetDetails(jobid,kitid,assetnameid,number,assigned,assetid)values(" +
                                                "" + combo_job.SelectedValue + "," + dt_getkitdetfromjobid.Rows[i]["kitid"].ToString() + "," +
                                            "" + dt_getassetnames.Rows[0]["Id"].ToString() + "," + cnt + ",'" + combo_assignment.SelectedValue + "'," +
                                            "" + combo_serialnumberforasset.SelectedValue + ")";
                                    }
                                    else
                                    {
                                        insertq_assignlater = "insert into PrismJobAssignedKitAssetDetails(jobid,kitid,assetnameid,number,assigned)values(" +
                                                "" + combo_job.SelectedValue + "," + dt_getkitdetfromjobid.Rows[i]["kitid"].ToString() + "," +
                                            "" + dt_getassetnames.Rows[0]["Id"].ToString() + "," + cnt + ",'" + combo_assignment.SelectedValue + "'" +
                                            ")";
                                    }
                                    SqlHelper.ExecuteNonQuery(transaction, CommandType.Text, insertq_assignlater);
                                }
                            //}
                            //else
                            //{
                            //    //if (combo_serialnumberforasset.SelectedValue != "Select")
                            //    //{
                            //    //    string update = "update PrismJobAssignedKitAssetDetails set assigned='" + combo_assignment.SelectedValue + "',assetid=" + combo_serialnumberforasset.SelectedValue + "" +
                            //    //    " where jobid=" + combo_job.SelectedValue + " and kitid=" + dt_getkitdetfromjobid.Rows[i]["kitid"].ToString() + " and assetnameid=" + dt_getassetnames.Rows[0]["Id"].ToString() + "";
                            //    //    SqlHelper.ExecuteNonQuery(transaction, CommandType.Text, update);
                            //    //}
                            //}

                        }
                    }
                }
                string updateoralstatus = "update PrismJobKits set Finalizedstatus='Finalized' where jobid=" + combo_job.SelectedValue + "";
                SqlHelper.ExecuteNonQuery(transaction, CommandType.Text, updateoralstatus);
            }

            //CONSUMABLE SAVING CODE
            //DataTable dt_getconsumables = SqlHelper.ExecuteDataset(transaction, CommandType.Text, "select * from PrismJobConsumables where jobid=" + combo_job.SelectedValue + "").Tables[0];
            //if (dt_getconsumables.Rows.Count > 0)
            //{
            //    for (int i = 0; i < dt_getconsumables.Rows.Count; i++)
            //    {
            //        string consumableid = "txt_consumable" + i;
            //        TextBox txt_qty = pnl_adddet.FindControl(consumableid) as TextBox;
            //        if (txt_qty.Text != "")
            //        {
            //            string updateq = "update PrismJobConsumables set qty=" + txt_qty.Text + " where jobconsumableid=" + dt_getconsumables.Rows[i]["jobconsumableid"].ToString() + "";
            //            SqlHelper.ExecuteNonQuery(transaction, CommandType.Text, updateq);
            //        }

            //    }
            //}
            transaction.Commit();
            lbl_message.Text = "Assets Finalized to Selected Kits Successfully";
            lbl_message.ForeColor = Color.Green;
        }
        catch (Exception ex)
        {
            lbl_message.Text = ex.Message;
            transaction.Rollback();
        }
        btn_save.Visible = true;
        btn_reset.Visible = true;
        btn_saving.Visible = true;
    }
    protected void btn_reset_OnClick(object sender, EventArgs e)
    {
        pnl_adddet.Controls.Clear();
        //pnl_consumables.Controls.Clear();
        combo_job.SelectedIndex = 0;
        lbl_message.Text = "";

    }
    protected void btn_saving_OnClick(object sender, EventArgs e)
    {
        SqlConnection db = new SqlConnection(GlobalConnetionString.ClientConnection());
        SqlTransaction transaction;
        db.Open();
        transaction = db.BeginTransaction();
        try
        {
            string querydel = "delete from PrismJobAssignedKitAssetDetails where jobid=" + combo_job.SelectedValue + "";
            SqlHelper.ExecuteNonQuery(transaction, CommandType.Text, querydel);
            string queryjobassetInsert = "", assignedassetnames = "";
            DataTable dt_getkitdetfromjobid = SqlHelper.ExecuteDataset(transaction, CommandType.Text, "select * from PrismJobKits where jobid=" + combo_job.SelectedValue + "").Tables[0];
            DataTable dtgetexistdetails = SqlHelper.ExecuteDataset(transaction, CommandType.Text, "select * from PrismJobAssignedKitAssetDetails where jobid=" + combo_job.SelectedValue + "").Tables[0];
            if (dt_getkitdetfromjobid.Rows.Count > 0)
            {
                for (int i = 0; i < dt_getkitdetfromjobid.Rows.Count; i++)
                {
                    DataTable dt_kitname = SqlHelper.ExecuteDataset(transaction, CommandType.Text, "select * from PrismAssetKitDetails where assetkitid=" + dt_getkitdetfromjobid.Rows[i]["kitid"].ToString() + "").Tables[0];
                    DataTable dt_getassetids = SqlHelper.ExecuteDataset(transaction, CommandType.Text, "select * from PrismKitAssetFromKitName where assetkitid=" + dt_getkitdetfromjobid.Rows[i]["kitid"].ToString() + "").Tables[0];
                    for (int j = 0; j < dt_getassetids.Rows.Count; j++)
                    {
                        DataTable dt_getassetnames = SqlHelper.ExecuteDataset(transaction, CommandType.Text, "select * from PrismAssetName where ID=" + dt_getassetids.Rows[j]["assetids"].ToString() + "").Tables[0];

                        int qtyofasset = Convert.ToInt32(dt_getassetids.Rows[j]["qty"]);
                        for (int k = 0; k < qtyofasset; k++)
                        {
                            string combo_serialnumberforassetid = "combo_" + k + "_" + j + "_" + i;
                            RadComboBox combo_serialnumberforasset = pnl_adddet.FindControl(combo_serialnumberforassetid) as RadComboBox;
                            string assignnowornotid = "combo_assignments" + k + "_" + j + "_" + i;
                            RadComboBox combo_assignment = pnl_adddet.FindControl(assignnowornotid) as RadComboBox;
                            //DataRow[] dr_getexistrow = dtgetexistdetails.Select("assetid=" + combo_serialnumberforasset.SelectedValue + " and assigned='" + combo_assignment.SelectedValue + "'");
                            int cnt = k + 1;
                            //if (dr_getexistrow.Length == 0)
                            //{
                                if (combo_assignment.SelectedValue == "Assign Now")
                                {
                                    if (combo_serialnumberforasset.SelectedValue != "Select")
                                    {
                                        
                                        string insertq = "insert into PrismJobAssignedKitAssetDetails(jobid,kitid,assetnameid,number,assigned,assetid)values(" +
                                            "" + combo_job.SelectedValue + "," + dt_getkitdetfromjobid.Rows[i]["kitid"].ToString() + "," +
                                        "" + dt_getassetnames.Rows[0]["Id"].ToString() + "," + cnt + ",'" + combo_assignment.SelectedValue + "'," +
                                        "" + combo_serialnumberforasset.SelectedValue + ")";
                                        SqlHelper.ExecuteNonQuery(transaction, CommandType.Text, insertq);
                                    }
                                }
                                else
                                {
                                    string insertq_assignlater = "";
                                    if (combo_serialnumberforasset.SelectedValue != "Select")
                                    {
                                        insertq_assignlater = "insert into PrismJobAssignedKitAssetDetails(jobid,kitid,assetnameid,number,assigned,assetid)values(" +
                                                "" + combo_job.SelectedValue + "," + dt_getkitdetfromjobid.Rows[i]["kitid"].ToString() + "," +
                                            "" + dt_getassetnames.Rows[0]["Id"].ToString() + "," + cnt + ",'" + combo_assignment.SelectedValue + "'," +
                                            "" + combo_serialnumberforasset.SelectedValue + ")";
                                    }
                                    else
                                    {
                                        insertq_assignlater = "insert into PrismJobAssignedKitAssetDetails(jobid,kitid,assetnameid,number,assigned)values(" +
                                                "" + combo_job.SelectedValue + "," + dt_getkitdetfromjobid.Rows[i]["kitid"].ToString() + "," +
                                            "" + dt_getassetnames.Rows[0]["Id"].ToString() + "," + cnt + ",'" + combo_assignment.SelectedValue + "'" +
                                            ")";
                                    }
                                    SqlHelper.ExecuteNonQuery(transaction, CommandType.Text, insertq_assignlater);
                                }
                            //}
                            //else
                            //{
                            //    //if (combo_serialnumberforasset.SelectedValue != "Select")
                            //    //{
                            //    //    string update = "update PrismJobAssignedKitAssetDetails set assigned='" + combo_assignment.SelectedValue + "',assetid=" + combo_serialnumberforasset.SelectedValue +""+
                            //    //    " where jobid=" + combo_job.SelectedValue + " and kitid=" + dt_getkitdetfromjobid.Rows[i]["kitid"].ToString() + " and assetnameid="+dt_getassetnames.Rows[0]["Id"].ToString()+"";
                            //    //    SqlHelper.ExecuteNonQuery(transaction, CommandType.Text, update);
                            //    //}
                            //}
                        }
                    }
                }
                
            }
            //DataTable dt_getconsumables = SqlHelper.ExecuteDataset(transaction, CommandType.Text, "select * from PrismJobConsumables where jobid=" + combo_job.SelectedValue + "").Tables[0];
            //if (dt_getconsumables.Rows.Count > 0)
            //{
            //    for (int i = 0; i < dt_getconsumables.Rows.Count; i++)
            //    {
            //        string consumableid = "txt_consumable" + i;
            //        TextBox txt_qty = pnl_adddet.FindControl(consumableid) as TextBox;
            //        if (txt_qty.Text != "")
            //        {
            //            string updateq = "update PrismJobConsumables set qty=" + txt_qty.Text + " where jobconsumableid=" + dt_getconsumables.Rows[i]["jobconsumableid"].ToString() + "";
            //            SqlHelper.ExecuteNonQuery(transaction, CommandType.Text, updateq);
            //        }

            //    }
            //}
            transaction.Commit();
            lbl_message.Text = "Assets Saved to Selected Kits";
            lbl_message.ForeColor = Color.Green;
        }
        catch (Exception ex)
        {
            lbl_message.Text = ex.Message;
            transaction.Rollback();
        }
        btn_save.Visible = true;
        btn_reset.Visible = true;
        btn_saving.Visible = true;
    }
}