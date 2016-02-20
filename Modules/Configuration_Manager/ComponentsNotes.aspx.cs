using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using Telerik.Web.UI;
using System.Data;
using System.Drawing;

public partial class Modules_Configuration_Manager_ComponentsNotes : System.Web.UI.Page
{
    Label lbl_assetrid_new = new Label();
    SqlConnection con = new SqlConnection(GlobalConnetionString.ClientConnection());
    SqlTransaction tran;
    protected void Page_Load(object sender, EventArgs e)
    {
        lbl_message.Text = "";
        if (Request.QueryString["Componentid"] != "")
        {
            hidd_assetridid.Value = Request.QueryString["Componentid"].ToString();
            //lbl_assetrid_new.Text = Request.QueryString["Assetid"].ToString();

            //dt_Assets = SqlHelper.ExecuteDataset(GlobalConnetionString.ClientConnection(), CommandType.Text, "Select * from Prism_Assets").Tables[0];
            //DataRow[] roasset = dt_Assets.Select("Id=" + lbl_assetrid_new.Text+" and repairstatus='Ok' ");
            if (Request.QueryString["status"].ToString() == "EDIT")
            {
                btn_crtjob.Visible = true;
                td_notes.Visible = true;
            }
            else
            {
                btn_crtjob.Visible = false;
                td_notes.Visible = false;
            }
            // where  ConId=" + lbl_conid.Text + " and AssetRid=" + lbl_assetrid_new.Text + "").Tables[0];

        }
    }
    protected void btn_crtjob_Click(object sender, EventArgs e)
    {


        try
        {

            if (con.State == ConnectionState.Closed)
                con.Open();
            tran = con.BeginTransaction();

            string str_jobtype_insert = "insert into MainteneceNotes(ComponentMainteneceId,Notes,UserId,Datetime)values('" + hidd_assetridid.Value + "','" + radtxt_jobdesc.Text + "','" + Session["UserId"] + "','" + DateTime.Now + "')";
            tran.Commit();
        }
        catch (Exception ex)
        {
            tran.Rollback();
        }


        if (btn_crtjob.Text != "Update")
        {
            string str_jobtype_insert = "insert into MainteneceNotes(ComponentMainteneceId,Notes,UserId,Datetime)values('" + hidd_assetridid.Value + "','" + radtxt_jobdesc.Text + "','" + Session["UserId"] + "','" + DateTime.Now + "')";
            int cnt = SqlHelper.ExecuteNonQuery(GlobalConnetionString.ClientConnection(), CommandType.Text, str_jobtype_insert);
            if (cnt > 0)
            {
                lbl_jcrt.Text = "Notes Added Successfully";
                lbl_jcrt.ForeColor = Color.Green;
            }
            else
            {
                lbl_jcrt.Text = "Notes not created";
                lbl_jcrt.ForeColor = Color.Red;
            }
            //radcombo_jobtype.DataBind();
            //RadWindow_ContentTemplate.VisibleOnPageLoad = true;

            radtxt_jobdesc.Text = "";
        }
        else
        {
            if (lbl_updatednotesid.Text != "")
            {
                string str_jobtype_update = "update MainteneceNotes set Notes='" + radtxt_jobdesc.Text + "' where NotesID=" + lbl_updatednotesid.Text + "";
                int cnt1 = SqlHelper.ExecuteNonQuery(GlobalConnetionString.ClientConnection(), CommandType.Text, str_jobtype_update);
                if (cnt1 > 0)
                {
                    lbl_jcrt.Text = "Notes Updated Successfully";
                    lbl_jcrt.ForeColor = Color.Green;
                }
                else
                {
                    lbl_jcrt.Text = "Notes not updated";
                    lbl_jcrt.ForeColor = Color.Red;
                }
            }
            else
            {
                lbl_jcrt.Text = "Notes not updated";
                lbl_jcrt.ForeColor = Color.Red;
            }
        }
        radgrid_existingJobs.Rebind();

    }
    protected void radgrid_existingJobs_ItemDataBound(object sender, GridItemEventArgs e)
    {
        if (e.Item is GridDataItem)
        {
            GridDataItem item = (GridDataItem)e.Item;
            LinkButton lnk_edit = (LinkButton)item.FindControl("lnk_edit");

            if (Request.QueryString["status"].ToString() == "EDIT")
            {
                lnk_edit.Visible = true;

            }
            else
            {
                lnk_edit.Visible = false;
            }
        }
    }
    protected void lnk_edit_Click(object sender, EventArgs e)
    {
        GridDataItem row = (GridDataItem)(((LinkButton)sender).NamingContainer);
        Label lbl_notesid = (Label)row.FindControl("lbl_notesid");
        DataTable dt_selectjobtypes = SqlHelper.ExecuteDataset(GlobalConnetionString.ClientConnection(), CommandType.Text, "select * from MainteneceNotes where NotesID=" + lbl_notesid.Text + "").Tables[0];
        if (dt_selectjobtypes.Rows.Count > 0)
        {
            lbl_updatednotesid.Text = lbl_notesid.Text;
            //radtxt_jname.Text = dt_selectjobtypes.Rows[0]["jobtype"].ToString();
            radtxt_jobdesc.Text = dt_selectjobtypes.Rows[0]["Notes"].ToString();
            //lbl_updateid.Text = dt_selectjobtypes.Rows[0]["jobtypeid"].ToString();
            btn_crtjob.Text = "Update";
        }
    }
}