using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using Telerik.Web.UI;
using System.Drawing;
using System.Text;

public partial class Modules_Configuration_Manager_OpsManagerDashBoard : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            DataTable dtGet = SqlHelper.ExecuteDataset(GlobalConnetionString.ClientConnection(), CommandType.Text, "select * from tbljob").Tables[0];
            RadGrid1.DataSource = dtGet;
            RadGrid1.DataBind();
        }
    }
    protected void RadGrid1_ItemDataBound(object sender, GridItemEventArgs e)
    {
        // RadGrid grid = (RadGrid)sender;
        if (e.Item.ItemType == GridItemType.Item)
        {
            GridDataItem dataItem = e.Item as GridDataItem;
            Label lbl_Jobno = (Label)dataItem.FindControl("lbl_Jobno");
            Label lbl_StartDate = (Label)dataItem.FindControl("lbl_StartDate");
            Label lbl_DDHandsDD1 = (Label)dataItem.FindControl("lbl_DDHandsDD1");
            Label lbl_DDHandsDD2 = (Label)dataItem.FindControl("lbl_DDHandsDD2");
            Label lbl_EquipmentNeeded = (Label)dataItem.FindControl("lbl_EquipmentNeeded");
            Label lbl_RunNo = (Label)dataItem.FindControl("lbl_RunNo");
            Label lbl_HoleDepth = (Label)dataItem.FindControl("lbl_HoleDepth");
            Label lbl_HrsFtDrilled = (Label)dataItem.FindControl("lbl_HrsFtDrilled");
            Label lbl_Activity = (Label)dataItem.FindControl("lbl_Activity");
            DataTable dttblJOBDATE = SqlHelper.ExecuteDataset(GlobalConnetionString.ClientConnection(), CommandType.Text, "select  top(1) * from tblJOBDATE where [Job ID]='" + lbl_Jobno.Text + "' order by Date Desc").Tables[0];
            DataTable dttblJOBDATEitems = SqlHelper.ExecuteDataset(GlobalConnetionString.ClientConnection(), CommandType.Text, "select  top(2) * from tblJOBDATEitems where [JOb ID]='" + lbl_Jobno.Text + "' order by Date Desc").Tables[0];
            if (dttblJOBDATE.Rows.Count > 0)
            {
                if(Convert.ToDateTime(dttblJOBDATE.Rows[0]["Date"])==DateTime.Now.AddDays(-1))
                {
                    e.Item.ForeColor=Color.Black;
                }
                else
                {
                    e.Item.ForeColor=Color.Red;
                }
                lbl_StartDate.Text = Convert.ToDateTime(dttblJOBDATE.Rows[0]["Date"]).ToShortDateString();
                lbl_DDHandsDD1.Text = dttblJOBDATE.Rows[0]["DD1"].ToString();
                lbl_DDHandsDD2.Text = dttblJOBDATE.Rows[0]["DD2"].ToString();
                lbl_EquipmentNeeded.Text = dttblJOBDATE.Rows[0]["General Comment"].ToString();
            }
            if (dttblJOBDATEitems.Rows.Count > 0)
            {
                lbl_RunNo.Text = dttblJOBDATEitems.Rows[0]["BHA#"].ToString();
                lbl_HoleDepth.Text = dttblJOBDATEitems.Rows[0]["End Depth"].ToString();
                int drilledcnt = Convert.ToInt32(dttblJOBDATEitems.Rows[0]["End Depth"]) - Convert.ToInt32(dttblJOBDATEitems.Rows[1]["End Depth"]);
                lbl_HrsFtDrilled.Text = drilledcnt.ToString();
                lbl_Activity.Text = dttblJOBDATEitems.Rows[0]["Comment"].ToString();
            }
        }
        
    }
    protected void btn_view_Click(object sender, EventArgs e)
    {
        DataTable dtGet = SqlHelper.ExecuteDataset(GlobalConnetionString.ClientConnection(), CommandType.Text, "select * from tbljob where WorkOrder='" + radcombo_job.SelectedValue + "'").Tables[0];
        RadGrid1.DataSource = dtGet;
        RadGrid1.DataBind();
    }
}