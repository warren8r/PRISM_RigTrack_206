using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Telerik.Web.UI;
using System.Drawing;


public partial class Modules_Configuration_Manager_AssetRepairStatus : System.Web.UI.Page
{
   
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
           radgrid_notificationstatus.Rebind();

        }
    }
    protected void btn_maintain_Click(object sender, EventArgs e)
    {
        string updatequery = "", insertquery = "", updatequery_Repair = "";
        GridDataItem row = (GridDataItem)(((RadButton)sender).NamingContainer);
        Label lbl_eventid = (Label)row.FindControl("lbl_eventid");
        Label lbl_statuscheck = (Label)row.FindControl("lbl_statuscheck");
        CheckBox isChecked = (CheckBox)row.FindControl("isChecked");

        RadButton btn_maintain = (RadButton)row.FindControl("btn_maintain");
        if (btn_maintain.Text != "Required")

            updatequery = "update events set eventnotification='True' where Id=" + lbl_eventid.Text + "";

        else

            updatequery = "update events set eventnotification='False' where Id=" + lbl_eventid.Text + "";

        int updatecnt = SqlHelper.ExecuteNonQuery(GlobalConnetionString.ClientConnection(), CommandType.Text, updatequery);
        radgrid_notificationstatus.Rebind();
    }
    protected void CheckChanged(object sender, EventArgs e)
    {
        if (hidd_acc.Value == "1")
        {
            string updatequery = "", insertquery = "", updatequery_Repair = "";
            GridDataItem row = (GridDataItem)(((CheckBox)sender).NamingContainer);
            Label lbl_eventid = (Label)row.FindControl("lbl_eventid");
            Label lbl_statuscheck = (Label)row.FindControl("lbl_statuscheck");
            CheckBox isChecked = (CheckBox)row.FindControl("isChecked");

            if (isChecked.Checked)

                updatequery = "update events set eventnotification='True' where Id=" + lbl_eventid.Text + "";

            else

                updatequery = "update events set eventnotification='False' where Id=" + lbl_eventid.Text + "";

            int updatecnt = SqlHelper.ExecuteNonQuery(GlobalConnetionString.ClientConnection(), CommandType.Text, updatequery);

        }
        radgrid_notificationstatus.Rebind();
    }

    protected void radgrid_notificationstatus_PageIndexChanged(object sender, Telerik.Web.UI.GridPageChangedEventArgs e)
    {
        radgrid_notificationstatus.CurrentPageIndex = e.NewPageIndex;
        radgrid_notificationstatus.Rebind();
    }
    protected void radgrid_notificationstatus_ItemDataBound(object sender, GridItemEventArgs e)
    {
        if (e.Item is GridDataItem)
        {
            GridDataItem item = (GridDataItem)e.Item;
            Label lbl_statuscheck = (Label)item.FindControl("lbl_statuscheck");
            Label lbl_eventid = (Label)item.FindControl("lbl_eventid");
            Label lbl_notification = (Label)item.FindControl("lbl_notification");            
            CheckBox isChecked = (CheckBox)item.FindControl("isChecked");
            RadButton btn_maintain = (RadButton)item.FindControl("btn_maintain");
            if (lbl_notification.Text == "True")
            {

                btn_maintain.Text = "Required";

                //lbl_statuscheck.ForeColor = Color.Red;
                isChecked.Checked = true;
            }
            else if (lbl_notification.Text == "False")
            {
                btn_maintain.Text = "Not Required";
                btn_maintain.ForeColor = Color.Red;
                //lbl_statuscheck.Text = dt_exist.Rows[0]["repairstatus"].ToString();
                //lbl_statuscheck.ForeColor = Color.Green;
                isChecked.Checked = false;
            }
            else
            {
                btn_maintain.Text = "Required";
                //btn_maintain.ForeColor = Color.Red;
            }
            ////lbl_statuscheck.Text = "In-Use";
            //if (lbl_statuscheck.Text != "Required")
            //{
            //    //lbl_statuscheck.Text = dt_exist.Rows[0]["repairstatus"].ToString();
            //    lbl_statuscheck.ForeColor = Color.Red;
            //    isChecked.Checked = true;
            //}
            //else
            //{
            //    lbl_statuscheck.Text = "Not&#160;Required";
            //    //lbl_statuscheck.Text = dt_exist.Rows[0]["repairstatus"].ToString();
            //    lbl_statuscheck.ForeColor = Color.Green;
            //    isChecked.Checked = false;
            //}
            

        }
    }
}