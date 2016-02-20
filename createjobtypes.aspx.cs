using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;
using System.Data;
using Telerik.Web.UI;

public partial class createjobtypes : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void btn_crtjob_Click(object sender, EventArgs e)
    {
        if (btn_crtjob.Text != "Update")
        {
            string str_jobtype_insert = "insert into jobTypes(jobtype,jobtypedescription)values('" + radtxt_jname.Text + "','" + radtxt_jobdesc.Text + "')";
            int cnt = SqlHelper.ExecuteNonQuery(GlobalConnetionString.ClientConnection(), CommandType.Text, str_jobtype_insert);
            if (cnt > 0)
            {
                lbl_jcrt.Text = "Job Type Created Successfully";
                lbl_jcrt.ForeColor = Color.Green;
            }
            else
            {
                lbl_jcrt.Text = "Job not created";
                lbl_jcrt.ForeColor = Color.Red;
            }
            //radcombo_jobtype.DataBind();
            //RadWindow_ContentTemplate.VisibleOnPageLoad = true;
            radtxt_jname.Text = "";
            radtxt_jobdesc.Text = "";
        }
        else
        {
            string str_jobtype_update = "update jobTypes set jobtype='" + radtxt_jname.Text + "',jobtypedescription='" + radtxt_jobdesc.Text + "' where jobtypeid=" + lbl_updateid.Text + "";
            int cnt1 = SqlHelper.ExecuteNonQuery(GlobalConnetionString.ClientConnection(), CommandType.Text, str_jobtype_update);
            if (cnt1 > 0)
            {
                lbl_jcrt.Text = "Job Type Updated Successfully";
                lbl_jcrt.ForeColor = Color.Green;
            }
            else
            {
                lbl_jcrt.Text = "Job not updated";
                lbl_jcrt.ForeColor = Color.Red;
            }
        }
        radgrid_existingJobs.Rebind();

    }
    protected void lnk_edit_Click(object sender, EventArgs e)
    {
        GridDataItem row = (GridDataItem)(((LinkButton)sender).NamingContainer);
        Label lbl_jobtypeid = (Label)row.FindControl("lbl_jobtypeid");
        DataTable dt_selectjobtypes = SqlHelper.ExecuteDataset(GlobalConnetionString.ClientConnection(), CommandType.Text, "select * from jobTypes where jobtypeid=" + lbl_jobtypeid.Text + "").Tables[0];
        if (dt_selectjobtypes.Rows.Count > 0)
        {
            radtxt_jname.Text = dt_selectjobtypes.Rows[0]["jobtype"].ToString();
            radtxt_jobdesc.Text = dt_selectjobtypes.Rows[0]["jobtypedescription"].ToString();
            lbl_updateid.Text = dt_selectjobtypes.Rows[0]["jobtypeid"].ToString();
            btn_crtjob.Text = "Update";
        }
    }
}