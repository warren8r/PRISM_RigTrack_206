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

public partial class Modules_Configuration_Manager_AssignAssetsToJobKitsLater : System.Web.UI.Page
{
    int totalcnt = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        
        btn_reset.Visible = false;
        btn_saving.Visible = false;
        string val = combo_job.SelectedValue;
        if (val != "")
        {
            binddata();
        }
    }
    protected void btn_view_OnClick(object sender, EventArgs e)
    {
        btn_reset.Visible = true;
        btn_saving.Visible = true;
        lbl_message.Text = "";
        binddata();
    }
    public void binddata()
    {
        pnl_adddet.Controls.Clear();

        pnl_adddet.Controls.Add(new LiteralControl("<table border='0' cellpadding='0' cellspacing='0' width='100%'><tr><td align='center'>"));
        //DataTable dt_getkitdetfromjobid = SqlHelper.ExecuteDataset(GlobalConnetionString.ClientConnection(), CommandType.Text, "select * from PrismJobAssignedKitAssetDetails where jobid=" + combo_job.SelectedValue + " and assigned='Assign Later'").Tables[0];
        DataTable dt_getkitnames = SqlHelper.ExecuteDataset(GlobalConnetionString.ClientConnection(), CommandType.Text, "select distinct kitid,kitname from PrismJobAssignedKitAssetDetails,PrismAssetKitDetails where assetkitid=kitid and jobid=" + combo_job.SelectedValue + " and assigned='Assign Later'").Tables[0];
        if (dt_getkitnames.Rows.Count > 0)
        {
            for (int i = 0; i < dt_getkitnames.Rows.Count; i++)
            {
                
        
                //PrismAssetKitDetails
                pnl_adddet.Controls.Add(new LiteralControl("<table border='1'>"));
                //DataTable dt_kitname = SqlHelper.ExecuteDataset(GlobalConnetionString.ClientConnection(), CommandType.Text, "select * from PrismAssetKitDetails where assetkitid=" + dt_getkitnames.Rows[i]["kitid"].ToString() + "").Tables[0];
                pnl_adddet.Controls.Add(new LiteralControl("<tr>"));
                pnl_adddet.Controls.Add(new LiteralControl("<td colspan='3' align='left'><b>Kit Name:" + dt_getkitnames.Rows[i]["KitName"].ToString() + "</b></td>"));
                pnl_adddet.Controls.Add(new LiteralControl("</tr>"));
                DataTable dt_getkitdetfromjobid = SqlHelper.ExecuteDataset(GlobalConnetionString.ClientConnection(), CommandType.Text, "select * from PrismJobAssignedKitAssetDetails where jobid=" + combo_job.SelectedValue + " and assigned='Assign Later' and kitid=" + dt_getkitnames.Rows[i]["kitid"].ToString() + "").Tables[0];
                //PrismKitAssetFromKitName//
                //DataTable dtgetexistdetails = SqlHelper.ExecuteDataset(GlobalConnetionString.ClientConnection(), CommandType.Text, "select * from PrismJobAssignedKitAssetDetails where jobid=" + combo_job.SelectedValue + "").Tables[0];
                for (int j = 0; j < dt_getkitdetfromjobid.Rows.Count; j++)
                {

                    DataTable dt_getassetnames = SqlHelper.ExecuteDataset(GlobalConnetionString.ClientConnection(), CommandType.Text, "select * from PrismAssetName where ID=" + dt_getkitdetfromjobid.Rows[j]["assetnameid"].ToString() + "").Tables[0];
                    DataTable dt_getassetids = SqlHelper.ExecuteDataset(GlobalConnetionString.ClientConnection(), CommandType.Text, "select * from PrismKitAssetFromKitName where assetkitid=" + dt_getkitdetfromjobid.Rows[j]["kitid"].ToString() + "").Tables[0];
                    //int qtyofasset = Convert.ToInt32(dt_getassetids.Rows[j]["qty"]);
                    string querygetserialno = "select P.Id as ID,PA.AssetName+' ('+SerialNumber+')' as  AssetName from Prism_Assets P,PrismAssetName PA" +
                        " where  P.AssetName=PA.ID and  (P.id not in (select AssetId from PrismJobAssignedAssets,manageJobOrders " +
                        " where jid=PrismJobAssignedAssets.JobId and status='Approved' and AssignMentStatus='InActive') or P.Id in (select AssetId from PrismJobAssignedKitAssetDetails " +
                        " where jobid=" + combo_job.SelectedValue + " and kitid=" + dt_getkitnames.Rows[i]["kitid"].ToString() + " and assetnameid=" + dt_getkitdetfromjobid.Rows[j]["assetnameid"].ToString() + "))" +
                        " and PA.ID=" + dt_getkitdetfromjobid.Rows[j]["assetnameid"].ToString() + " and P.repairstatus<>'Maintenance'" +
                        " and P.ID not in (select assetid from PrismJobAssignedKitAssetDetails where jobid=" + combo_job.SelectedValue + "" +
                        "  and assetnameid=" + dt_getkitdetfromjobid.Rows[j]["assetnameid"].ToString() + " and assetid is not null)";
                    DataTable dt_getserialnumbers = SqlHelper.ExecuteDataset(GlobalConnetionString.ClientConnection(), CommandType.Text, querygetserialno).Tables[0];

                    pnl_adddet.Controls.Add(new LiteralControl("<tr>"));
                    pnl_adddet.Controls.Add(new LiteralControl("<td align='right'>" + dt_getassetnames.Rows[0]["AssetName"].ToString() + "</td>"));
                    RadComboBox combo_serialnumberforasset = new RadComboBox();
                    combo_serialnumberforasset.ID = "combo_" + i + "_" + j;

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
                    combo_assignment.ID = "combo_assignments_" + i + "_" + j;
                    combo_assignment.Items.Insert(0, new RadComboBoxItem("Assign Now", "Assign Now"));
                    combo_assignment.Items.Insert(1, new RadComboBoxItem("Assign Later", "Assign Later"));
                    pnl_adddet.Controls.Add(new LiteralControl("<td>"));
                    pnl_adddet.Controls.Add(combo_assignment);
                    pnl_adddet.Controls.Add(new LiteralControl("</td>"));
                    pnl_adddet.Controls.Add(new LiteralControl("</tr>"));
                    //combo_serialnumberforasset.SelectedValue = dt_getkitdetfromjobid.Rows[i]["assetnameid"].ToString();
                    combo_assignment.SelectedValue = dt_getkitdetfromjobid.Rows[j]["assigned"].ToString();
                }
                        
                   
                pnl_adddet.Controls.Add(new LiteralControl("</table>"));
            }
            pnl_adddet.Controls.Add(new LiteralControl("</td></tr></table>"));
            totalcnt++;
            btn_reset.Visible = true;
            btn_saving.Visible = true;
            lbl_message.Text = "";
        }
        else
        {
            btn_reset.Visible = false;
            btn_saving.Visible = false;
            lbl_message.Text = "No Data Found";
            lbl_message.ForeColor = Color.Red;
        }
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
            string queryjobassetInsert = "", assignedassetnames = "";
            DataTable dt_getkitnames = SqlHelper.ExecuteDataset(transaction, CommandType.Text, "select distinct kitid,kitname from PrismJobAssignedKitAssetDetails,PrismAssetKitDetails where assetkitid=kitid and jobid=" + combo_job.SelectedValue + " and assigned='Assign Later'").Tables[0];
            if (dt_getkitnames.Rows.Count > 0)
            {
                for (int i = 0; i < dt_getkitnames.Rows.Count; i++)
                {
                    DataTable dt_getkitdetfromjobid = SqlHelper.ExecuteDataset(transaction, CommandType.Text, "select * from PrismJobAssignedKitAssetDetails where jobid=" + combo_job.SelectedValue + " and assigned='Assign Later' and kitid=" + dt_getkitnames.Rows[i]["kitid"].ToString() + "").Tables[0];
                //PrismKitAssetFromKitName//
                //DataTable dtgetexistdetails = SqlHelper.ExecuteDataset(GlobalConnetionString.ClientConnection(), CommandType.Text, "select * from PrismJobAssignedKitAssetDetails where jobid=" + combo_job.SelectedValue + "").Tables[0];
                    for (int j = 0; j < dt_getkitdetfromjobid.Rows.Count; j++)
                    {

                        DataTable dt_getassetnames = SqlHelper.ExecuteDataset(transaction, CommandType.Text, "select * from PrismAssetName where ID=" + dt_getkitdetfromjobid.Rows[j]["assetnameid"].ToString() + "").Tables[0];
                        DataTable dt_getassetids = SqlHelper.ExecuteDataset(transaction, CommandType.Text, "select * from PrismKitAssetFromKitName where assetkitid=" + dt_getkitdetfromjobid.Rows[j]["kitid"].ToString() + "").Tables[0];

                        //int qtyofasset = Convert.ToInt32(dt_getassetids.Rows[j]["qty"]);

                        string combo_serialnumberforassetid = "combo_" + i + "_" + j;
                        RadComboBox combo_serialnumberforasset = pnl_adddet.FindControl(combo_serialnumberforassetid) as RadComboBox;
                        string assignnowornotid = "combo_assignments_" + i + "_" + j;
                        RadComboBox combo_assignment = pnl_adddet.FindControl(assignnowornotid) as RadComboBox;

                        if (combo_assignment.SelectedValue == "Assign Now")
                        {
                            if (combo_serialnumberforasset.SelectedValue != "Select")
                            {
                                string fromlocationtype = "", fromlocationid = "", tolocationtype = "", tolocationid = "", currentlocationtype = "", currentlocationid = "", assetstatus = "", assetid = "";
                                queryjobassetInsert = "Insert into PrismJobAssignedAssets(JobId,AssetId,AssetStatus,ModifiedBy,ModifiedDate,KitName,AssignmentStatus)" +
                                    " values('" + combo_job.SelectedValue + "','" + combo_serialnumberforasset.SelectedValue + "','1','" + Session["userId"].ToString() + "','" + DatesbetweenDatatable.getdatetimeformat(DateTime.Now.ToString()) + "','" + dt_getkitnames.Rows[i]["KitName"].ToString() + "','Active')";
                                SqlHelper.ExecuteNonQuery(transaction, CommandType.Text, queryjobassetInsert);

                                string insertq = "update PrismJobAssignedKitAssetDetails set assigned='" + combo_assignment.SelectedValue + "',assetid=" + combo_serialnumberforasset.SelectedValue + " where jobkitassignedid=" + dt_getkitdetfromjobid.Rows[j]["jobkitassignedid"] + "";

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
                            //string insertq_assignlater = "";
                            //if (combo_serialnumberforasset.SelectedValue != "Select")
                            //{
                            //    insertq_assignlater = "insert into PrismJobAssignedKitAssetDetails(jobid,kitid,assetnameid,number,assigned,assetid)values(" +
                            //            "" + combo_job.SelectedValue + "," + dt_getkitdetfromjobid.Rows[i]["kitid"].ToString() + "," +
                            //        "" + dt_getassetnames.Rows[0]["Id"].ToString() + "," + i + ",'" + combo_assignment.SelectedValue + "'," +
                            //        "" + combo_serialnumberforasset.SelectedValue + ")";
                            //}
                            //else
                            //{
                            //    insertq_assignlater = "insert into PrismJobAssignedKitAssetDetails(jobid,kitid,assetnameid,number,assigned)values(" +
                            //            "" + combo_job.SelectedValue + "," + dt_getkitdetfromjobid.Rows[i]["kitid"].ToString() + "," +
                            //        "" + dt_getassetnames.Rows[0]["Id"].ToString() + "," + i + ",'" + combo_assignment.SelectedValue + "'" +
                            //        ")";
                            //}
                            //SqlHelper.ExecuteNonQuery(transaction, CommandType.Text, insertq_assignlater);
                        }

                    }

                       
                }
                //string updateoralstatus = "update PrismJobKits set Finalizedstatus='Finalized' where jobid=" + combo_job.SelectedValue + "";
                //SqlHelper.ExecuteNonQuery(transaction, CommandType.Text, updateoralstatus);
            }

            //CONSUMABLE SAVING CODE
            //DataTable dt_getconsumables = SqlHelper.ExecuteDataset(GlobalConnetionString.ClientConnection(), CommandType.Text, "select * from PrismJobConsumables where jobid=" + combo_job.SelectedValue + "").Tables[0];
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
            lbl_message.Text = "Record Saved Successfully";
            lbl_message.ForeColor = Color.Green;
        }
        catch (Exception ex)
        {
            lbl_message.Text = ex.Message;
            transaction.Rollback();
        }
        //btn_save.Visible = true;
        btn_reset.Visible = true;
        btn_saving.Visible = true;
    }
}