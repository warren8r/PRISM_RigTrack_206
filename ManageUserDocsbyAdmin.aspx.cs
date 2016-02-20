using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using Telerik.Web.UI;
using System.Data;

public partial class ManageUserDocsbyAdmin : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void RadGrid2_ItemCommand(object source, GridCommandEventArgs e)
    {
        if (e.CommandName == "downloaddoc")
        {
            //byte[] binaryData = (byte[])data.Tables[0].Rows[0]["BinaryData"];
            Label lbl_docname = (Label)e.Item.FindControl("lbl_docname");
            //Response.Clear();
            //Response.ContentType = "application/octet-stream";
            //Response.AddHeader("content-disposition", "attachment; filename=" + lbl_docname.Text);
            //Response.Flush();
            //Response.End();
            string path = MapPath("/Documents/" + lbl_docname.Text);
            byte[] bts = System.IO.File.ReadAllBytes(path);
            Response.Clear();
            Response.ClearHeaders();
            Response.AddHeader("Content-Type", "Application/octet-stream");
            Response.AddHeader("Content-Length", bts.Length.ToString());
            Response.AddHeader("Content-Disposition", "attachment; filename=" + lbl_docname.Text);
            Response.BinaryWrite(bts);
            Response.Flush();
            Response.End();

        }
    }
    protected void CheckChanged_a(object sender, EventArgs e)
    {
        GridDataItem row = (GridDataItem)(((CheckBox)sender).NamingContainer);
        Label lbl_EventTaskOrderDocID = (Label)row.FindControl("lbl_EventTaskOrderDocID");
        string updatequery = "update EventUploadedDocuments set DocumentStatus='Accepted',Closed='Yes',AdminID=" + Session["userId"] + ",AdminModifiedDate='" + DateTime.Now + "' where EventUploadedDocID="+lbl_EventTaskOrderDocID.Text+"";
        SqlHelper.ExecuteNonQuery(GlobalConnetionString.ClientConnection(), CommandType.Text, updatequery);
        RadGrid1.Rebind();
        
    }
    protected void CheckChanged_r(object sender, EventArgs e)
    {
        GridDataItem row = (GridDataItem)(((CheckBox)sender).NamingContainer);
        Label lbl_EventTaskOrderDocID = (Label)row.FindControl("lbl_EventTaskOrderDocID");
        string updatequery = "update EventUploadedDocuments set DocumentStatus='Rejected',Closed='No',AdminID=" + Session["userId"] + ",AdminModifiedDate='" + DateTime.Now + "' where EventUploadedDocID=" + lbl_EventTaskOrderDocID.Text + "";
        SqlHelper.ExecuteNonQuery(GlobalConnetionString.ClientConnection(), CommandType.Text, updatequery);
        RadGrid1.Rebind();

    }
    protected void RadGrid1_ItemDataBound(object sender, GridItemEventArgs e)
    {
        if (e.Item is GridDataItem)
        {
            GridDataItem item = (GridDataItem)e.Item;
            Label lbl_acceptstatus = (Label)item.FindControl("lbl_acceptstatus");
            RadioButton rd_accept = (RadioButton)item.FindControl("rd_accept");
            RadioButton rd_reject = (RadioButton)item.FindControl("rd_reject");
            if (lbl_acceptstatus.Text == "Yes")
            {
                rd_accept.Checked = true;
                rd_reject.Checked = false;
            }
            else if (lbl_acceptstatus.Text == "No")
            {
                rd_reject.Checked = true;
                rd_accept.Checked = false;
            }
            
        }
    }
    protected void RadGrid1_ItemCommand(object source, GridCommandEventArgs e)
    {
        if (e.CommandName == "downloaddoc")
        {
            //byte[] binaryData = (byte[])data.Tables[0].Rows[0]["BinaryData"];
            Label lbl_docname = (Label)e.Item.FindControl("lbl_docname");
            //Response.Clear();
            //Response.ContentType = "application/octet-stream";
            //Response.AddHeader("content-disposition", "attachment; filename=" + lbl_docname.Text);
            //Response.Flush();
            //Response.End();
            string path = MapPath("/Documents/" + lbl_docname.Text);
            byte[] bts = System.IO.File.ReadAllBytes(path);
            Response.Clear();
            Response.ClearHeaders();
            Response.AddHeader("Content-Type", "Application/octet-stream");
            Response.AddHeader("Content-Length", bts.Length.ToString());
            Response.AddHeader("Content-Disposition", "attachment; filename=" + lbl_docname.Text);
            Response.BinaryWrite(bts);
            Response.Flush();
            Response.End();

        }
        if (e.CommandName == "downloadtaskorderdoc")
        {
            Label lbl_docname_task = (Label)e.Item.FindControl("lbl_docname_task");
            //Response.Clear();
            //Response.ContentType = "application/octet-stream";
            //Response.AddHeader("content-disposition", "attachment; filename=" + lbl_docname.Text);
            //Response.Flush();
            //Response.End();
            string path = MapPath("/Documents/" + lbl_docname_task.Text);
            byte[] bts = System.IO.File.ReadAllBytes(path);
            Response.Clear();
            Response.ClearHeaders();
            Response.AddHeader("Content-Type", "Application/octet-stream");
            Response.AddHeader("Content-Length", bts.Length.ToString());
            Response.AddHeader("Content-Disposition", "attachment; filename=" + lbl_docname_task.Text);
            Response.BinaryWrite(bts);
            Response.Flush();
            Response.End();
        }
        
        RadGrid1.Rebind();
    }
}