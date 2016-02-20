using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

public partial class createRigTypes : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void btn_createrig_Click(object sender, EventArgs e)
    {
        if (btn_createrig.Text != "Update")
        {
            string str_jobtype_insert = "insert into RigTypes(rigtypename,rigtypedesc)values('" + radtxt_tigtypename.Text + "','" + radtxt_rigtypedesc.Text + "')";
            int cnt = SqlHelper.ExecuteNonQuery(GlobalConnetionString.ClientConnection(), CommandType.Text, str_jobtype_insert);
            if (cnt > 0)
            {
                lbl_rigtypemsg.Text = "Rig Type Created Successfully";
                lbl_rigtypemsg.ForeColor = Color.Green;
            }
            else
            {
                lbl_rigtypemsg.Text = "Rig Type not created";
                lbl_rigtypemsg.ForeColor = Color.Red;
            }
        }
        else
        {
            string str_jobtype_update = "update RigTypes set rigtypename='" + radtxt_tigtypename.Text + "',rigtypedesc='" + radtxt_rigtypedesc.Text + "' where rigtypeid=" + lbl_updateid.Text + "";
            int cnt1 = SqlHelper.ExecuteNonQuery(GlobalConnetionString.ClientConnection(), CommandType.Text, str_jobtype_update);
            if (cnt1 > 0)
            {
                lbl_rigtypemsg.Text = "Job Type Updated Successfully";
                lbl_rigtypemsg.ForeColor = Color.Green;
            }
            else
            {
                lbl_rigtypemsg.Text = "Job not updated";
                lbl_rigtypemsg.ForeColor = Color.Red;
            }
        }
        radgrid2.DataBind();
        //RadWindow2.VisibleOnPageLoad = true;
        radtxt_tigtypename.Text = "";
        radtxt_rigtypedesc.Text = "";

    }
    protected void lnk_edit_Click(object sender, EventArgs e)
    {
        GridDataItem row = (GridDataItem)(((LinkButton)sender).NamingContainer);
        Label lbl_rigtypeid = (Label)row.FindControl("lbl_rigtypeid");
        DataTable dt_selectjobtypes = SqlHelper.ExecuteDataset(GlobalConnetionString.ClientConnection(), CommandType.Text, "select * from RigTypes where rigtypeid=" + lbl_rigtypeid.Text + "").Tables[0];
        if (dt_selectjobtypes.Rows.Count > 0)
        {
            radtxt_tigtypename.Text = dt_selectjobtypes.Rows[0]["rigtypename"].ToString();
            radtxt_rigtypedesc.Text = dt_selectjobtypes.Rows[0]["rigtypedesc"].ToString();
            lbl_updateid.Text = dt_selectjobtypes.Rows[0]["rigtypeid"].ToString();
            btn_createrig.Text = "Update";
        }
    }
}