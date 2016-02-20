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

public partial class Modules_Configuration_Manager_ManageConsumableQty : System.Web.UI.Page
{
    int totalcnt = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        btn_saving.Visible = false;
        btn_reset.Visible = false;
        radcombo_type.Visible = false;
        //string val = combo_job.SelectedValue;
        //if (val != "")
        //{
           
        //    //bindconsumables();
        //    DataTable dt_getkitdetfromjobid = SqlHelper.ExecuteDataset(GlobalConnetionString.ClientConnection(), CommandType.Text, "select * from PrismJobConsumables where jobid=" + combo_job.SelectedValue + "").Tables[0];
        //    if (dt_getkitdetfromjobid.Rows.Count > 0)
        //    {
        //        radcombo_type.Visible = true;
        //        btn_reset.Visible = true;
        //        btn_saving.Visible = true;
        //    }
        //    else
        //    {

        //        btn_reset.Visible = false;
        //        btn_saving.Visible = false;
        //        radcombo_type.Visible = false;
        //    }
        //}
    }
    protected void btn_view_OnClick(object sender, EventArgs e)
    {
        
        btn_reset.Visible = true;
        btn_saving.Visible = true;
        radcombo_type.Visible = true;
        radcombo_type.Visible = true;
        lbl_message.Text = "";
        
        bindconsumables();
        

    }
    protected void radgrid_manageassetkits_ItemDataBound(object sender, GridItemEventArgs e)
    {

        if (e.Item is GridDataItem)
        {
            GridDataItem item = (GridDataItem)e.Item;
            Label lbl_AssignedDate = (Label)item.FindControl("lbl_AssignedDate");
            Label lbl_Date = (Label)item.FindControl("lbl_Date");
            TextBox txt_qty = (TextBox)item.FindControl("txt_qty");
            Label lbl_qty = (Label)item.FindControl("lbl_qty");
            txt_qty.Text = lbl_qty.Text;
            if (lbl_Date.Text == "")
            {
                lbl_AssignedDate.Text = "-NA-";
            }
            else
            {
                lbl_AssignedDate.Text = Convert.ToDateTime(lbl_Date.Text).ToShortDateString();
            }
        }
    }
    public void bindconsumables()
    {
        string selectq = "select * from PrismJobConsumables c,PrsimJobAssetStatus s,Consumables cn where jobid=" + combo_job.SelectedValue + " and " +
           " c.ConsumableStatus=s.Id and c.consumableid=cn.ConID order by c.AssignedDate desc";
        DataTable dt_getconsumables = SqlHelper.ExecuteDataset(GlobalConnetionString.ClientConnection(), CommandType.Text, selectq).Tables[0];
        radgrid_manageassetkits.DataSource = dt_getconsumables;
        radgrid_manageassetkits.DataBind();
        //pnl_consumables.Controls.Clear();
        //pnl_consumables.Controls.Add(new LiteralControl("<table border='0' cellpadding='0' cellspacing='0' width='100%'><tr><td align='center'>"));
        //DataTable dt_getconsumables = SqlHelper.ExecuteDataset(GlobalConnetionString.ClientConnection(), CommandType.Text, "select * from PrismJobConsumables c,PrsimJobAssetStatus s where jobid=" + combo_job.SelectedValue + " and c.ConsumableStatus=s.Id order by c.AssignedDate desc").Tables[0];
        //if (dt_getconsumables.Rows.Count > 0)
        //{
        //    pnl_consumables.Controls.Add(new LiteralControl("<table border='1'>"));
        //    pnl_consumables.Controls.Add(new LiteralControl("<tr>"));
        //    pnl_consumables.Controls.Add(new LiteralControl("<td align='center'><b>Select</b></td>"));
        //    pnl_consumables.Controls.Add(new LiteralControl("<td align='center'><b>Consumable Name</b></td>"));
        //    pnl_consumables.Controls.Add(new LiteralControl("<td align='center'><b>Qty</b></td>"));
        //    pnl_consumables.Controls.Add(new LiteralControl("<td align='center'><b>Assigned Date</b></td>"));
        //    pnl_consumables.Controls.Add(new LiteralControl("<td align='center'><b>Status</b></td>"));
        //    pnl_consumables.Controls.Add(new LiteralControl("</tr>"));
        //    for (int i = 0; i < dt_getconsumables.Rows.Count; i++)
        //    {

        //        DataTable dt_consumablename = SqlHelper.ExecuteDataset(GlobalConnetionString.ClientConnection(), CommandType.Text, "select * from Consumables where ConId=" + dt_getconsumables.Rows[i]["consumableid"].ToString() + "").Tables[0];
        //        pnl_consumables.Controls.Add(new LiteralControl("<tr>"));
        //        CheckBox chk_status = new CheckBox();
        //        chk_status.ID = "chk_status_" + i;
        //        pnl_consumables.Controls.Add(new LiteralControl("<td align='left'>"));

        //        pnl_consumables.Controls.Add(chk_status);
        //        pnl_consumables.Controls.Add(new LiteralControl("</td>"));
        //        pnl_consumables.Controls.Add(new LiteralControl("<td align='left'>" + dt_consumablename.Rows[0]["ConName"].ToString() + "</td>"));
        //        TextBox txt_qty = new TextBox();
        //        txt_qty.ID = "txt_consumable" + i;
        //        txt_qty.Text = dt_getconsumables.Rows[i]["qty"].ToString();
        //        pnl_consumables.Controls.Add(new LiteralControl("<td>"));
        //        pnl_consumables.Controls.Add(txt_qty);
        //        pnl_consumables.Controls.Add(new LiteralControl("</td>"));
        //        if (dt_getconsumables.Rows[i]["AssignedDate"].ToString() != "")
        //        {
        //            pnl_consumables.Controls.Add(new LiteralControl("<td>"+Convert.ToDateTime(dt_getconsumables.Rows[i]["AssignedDate"].ToString()).ToShortDateString()+"</td>"));
        //        }
        //        else
        //        {
        //            pnl_consumables.Controls.Add(new LiteralControl("<td>-NA-</td>"));
        //        }
        //        pnl_consumables.Controls.Add(new LiteralControl("<td>" + dt_getconsumables.Rows[i]["StatusText"].ToString() + "</td>"));
        //        pnl_consumables.Controls.Add(new LiteralControl("</tr>"));
        //    }
        //    pnl_consumables.Controls.Add(new LiteralControl("</table></td></tr></table>"));
        //    totalcnt++;
        //}
        //else
        //{

        //}
    }
    protected void btn_saving_OnClick(object sender, EventArgs e)
    {
        SqlConnection db = new SqlConnection(GlobalConnetionString.ClientConnection());
        SqlTransaction transaction;
        db.Open();
        transaction = db.BeginTransaction();
        try
        {
            DataTable dt_getconsumables = SqlHelper.ExecuteDataset(transaction, CommandType.Text, "select * from PrismJobConsumables where jobid=" + combo_job.SelectedValue + "").Tables[0];
            if (dt_getconsumables.Rows.Count > 0)
            {
                foreach (GridDataItem item in radgrid_manageassetkits.Items)
                {


                    TextBox txt_qty = (TextBox)item.FindControl("txt_qty");
                    Label lbl_consumableid = (Label)item.FindControl("lbl_consumableid");
                    CheckBox chk_select = (CheckBox)item.FindControl("chk_select");
                    string radcomboval = radcombo_type.SelectedValue;
                    if (radcombo_type.SelectedValue == "")
                    {
                        radcomboval = "1";
                    }
                    else
                    {
                        radcomboval = radcombo_type.SelectedValue;
                    }
                    if (chk_select.Checked)
                    {
                        if (txt_qty.Text != "")
                        {

                            string updateq = "update PrismJobConsumables set qty=" + txt_qty.Text + ",ConsumableStatus='" + radcomboval + "' where jobconsumableid=" + lbl_consumableid.Text + "";
                            SqlHelper.ExecuteNonQuery(transaction, CommandType.Text, updateq);
                        }
                        else
                        {
                            string updateq = "update PrismJobConsumables set ConsumableStatus='" + radcomboval + "' where jobconsumableid=" + lbl_consumableid.Text + "";
                            SqlHelper.ExecuteNonQuery(transaction, CommandType.Text, updateq);
                        }

                    }
                    else
                    {
                        if (txt_qty.Text != "")
                        {
                            string updateq = "update PrismJobConsumables set qty=" + txt_qty.Text + " where jobconsumableid=" + lbl_consumableid.Text + "";
                            SqlHelper.ExecuteNonQuery(transaction, CommandType.Text, updateq);
                        }
                    }

                }

            }
        }
        catch (Exception ex)
        {
            transaction.Rollback();
        }
        transaction.Commit();
        //pnl_consumables.Controls.Clear();
        //pnl_consumables.Controls.Clear();
        
        lbl_message.Text = "";
        lbl_message.Text = "Record Saved Successfully";
        lbl_message.ForeColor = Color.Green;
        bindconsumables();
        
    }
    protected void btn_reset_OnClick(object sender, EventArgs e)
    {
        pnl_consumables.Controls.Clear();
        btn_saving.Visible = false;
        radcombo_type.Visible = false;

        lbl_message.Text = "";
        combo_job.ClearSelection();
        combo_job.EmptyMessage = "Select JobName";


        radgrid_manageassetkits.DataSource = null;
        radgrid_manageassetkits.DataBind();

    }
}