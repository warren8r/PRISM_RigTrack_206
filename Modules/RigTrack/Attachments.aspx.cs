using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using Telerik.Web.UI;
public partial class Modules_RigTrack_Attachments : System.Web.UI.Page
{
    #region Page Behavior
    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)
        {
            int CurveGroupID = 0;
            try
            {
                CurveGroupID = Convert.ToInt32(Request.QueryString["CurveGroupID"]);
            }
            catch (Exception) { }
            DataTable dt = RigTrack.DatabaseObjects.RigTrackDO.GetAttachments(CurveGroupID);
            RadGrid1.DataSource = dt;
            RadGrid1.DataBind();
        }
    }
    #endregion
    #region Buttons
    protected void RadGrid1_ItemCommand(object source, GridCommandEventArgs e)
    {

        if (e.CommandName == "ViewAttachment")
        {

            //Response.ContentType = "text/plain";
            //Response.AppendHeader("Content-Disposition", "attachment;filename= errorLog.txt");
            //Response.AddHeader("content-length", "0");
            //Response.Flush();
            //Response.End();


            GridDataItem item = (GridDataItem)e.Item;
            int itemId = (int)item.GetDataKeyValue("ID");
            int CurveGroupID = Convert.ToInt32(Request.QueryString["CurveGroupID"]);
            DataTable dt = RigTrack.DatabaseObjects.RigTrackDO.GetAttachments(CurveGroupID);

            foreach (DataRow row in dt.Rows)
            {
                if ((int)row["ID"] == itemId)
                {
                    byte[] binaryData = (byte[])row["Attachment"];
                    string attachmentName = row["Name"].ToString();

                    Response.Clear();
                    Response.ContentType = "application/octet-stream";
                    Response.AddHeader("content-disposition", "attachment; filename=" + attachmentName);
                    Response.BinaryWrite(binaryData);
                    Response.End();

                    break;
                }
            }
        }
    }
    protected void RadGrid1_ItemDataBound(object sender, GridItemEventArgs e)
    {
        if (e.Item is GridNestedViewItem)
        {
            GridNestedViewItem nestedItem = (GridNestedViewItem)e.Item;
            LinkButton filenamelinkbutton = (LinkButton)nestedItem.FindControl("ViewAttachmentLinkButton");
            ScriptManager.GetCurrent(Page).RegisterPostBackControl(filenamelinkbutton);
        }

    }
    #endregion
    #region Utility Methods
    #endregion

    protected void RadGrid1_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
    {
        int CurveGroupID = Convert.ToInt32(Request.QueryString["CurveGroupID"]);

        DataTable dt = RigTrack.DatabaseObjects.RigTrackDO.GetAttachments(CurveGroupID);
        RadGrid1.DataSource = dt;
        RadGrid1.DataBind();

    }
}