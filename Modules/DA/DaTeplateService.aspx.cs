using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using Telerik.Web.UI;
using System.Net;
using System.IO;
using Ionic.Zip;
using Ionic.Zlib;
using System.Collections;
using System.Xml;
using System.Drawing;
public partial class Modules_DA_DaLaunchService : System.Web.UI.Page
{
    public static DataTable dt_Service, dt_serviceattributes, dtPreVEEZipFiles, dtPreVEEExtractedFiles, dt_existingtemp;
    protected void Page_Load(object sender, EventArgs e)
    {
        dt_Service = SqlHelper.ExecuteDataset(GlobalConnetionString.ClientConnection(), CommandType.Text, "select * from daService where status='Active'").Tables[0];
        dt_serviceattributes = SqlHelper.ExecuteDataset(GlobalConnetionString.ClientConnection(), CommandType.Text, "select * from daServiceAttributes").Tables[0];
        if (!IsPostBack)
        {

            RadComboBoxFill.FillRadcombobox(radcombo_services, dt_Service, "serviceName", "serviceId", "0");

        }
        bindAttributes();

    }
    public void bindgrid()
    {
        dt_existingtemp = SqlHelper.ExecuteDataset(GlobalConnetionString.ClientConnection(), CommandType.Text, "select * from daServiceTemplates where serviceId=" + radcombo_services.SelectedValue + " and templateId not in(select templateId from daScheduledServices where serviceId=" + radcombo_services.SelectedValue + ")").Tables[0];
        radgrid_Template.DataSource = dt_existingtemp;
        radgrid_Template.DataBind();
    }
    protected void radcombo_services_SelectedIndexChanged(object sender, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs e)
    {
        DataRow[] row = dt_Service.Select("serviceId=" + radcombo_services.SelectedValue);
        txt_description.Text = row[0]["serviceDescription"].ToString();
    }
    

    protected void CheckChanged(object sender, EventArgs e)
    {
        GridDataItem row = (GridDataItem)(((CheckBox)sender).NamingContainer);
        Label tmpLabel = (Label)row.FindControl("lbl_tempid");
        Label lbl_statuscheck = (Label)row.FindControl("lbl_statuscheck");
        int eventCategoryId = Convert.ToInt32(tmpLabel.Text);
        if (lbl_statuscheck.Text == "Active")
        {
            SqlHelper.ExecuteNonQuery(GlobalConnetionString.ClientConnection(), CommandType.Text, "update daServiceTemplates set status='Inactive' where templateId=" + eventCategoryId + "");
        }
        else
        {
            SqlHelper.ExecuteNonQuery(GlobalConnetionString.ClientConnection(), CommandType.Text, "update daServiceTemplates set status='Active' where templateId=" + eventCategoryId + "");
        }
        bindgrid();
    }

    protected void radgrid_Template_ItemDataBound(object sender, GridItemEventArgs e)
    {
        if (e.Item is GridDataItem)
        {
            GridDataItem item = (GridDataItem)e.Item;
            CheckBox isChecked = (CheckBox)item.FindControl("isChecked");
            Label lbl_statuscheck = (Label)item.FindControl("lbl_statuscheck");
            if (lbl_statuscheck.Text == "Active")
            {
                lbl_statuscheck.ForeColor = System.Drawing.Color.Green;
                isChecked.Checked = true;
            }
            else
            {
                lbl_statuscheck.ForeColor = System.Drawing.Color.Red;
                isChecked.Checked = false;
            }
        }
    }
    public void bindAttributes()
    {
        if (radcombo_services.SelectedIndex > 0)
        {
            DataRow[] row_attributes = dt_serviceattributes.Select("serviceId=" + radcombo_services.SelectedValue);
            panel_attributes.Controls.Add(new LiteralControl("<table border=0><tr>"));
            for (int attribute = 0; attribute < row_attributes.Length; attribute++)
            {
                Label lbl_attributename = new Label();
                lbl_attributename.ID = "lbl_" + row_attributes[attribute]["serviceAttrId"].ToString();
                lbl_attributename.Text = row_attributes[attribute]["serviceAttr"].ToString();
                panel_attributes.Controls.Add(new LiteralControl("<td>"));
                panel_attributes.Controls.Add(lbl_attributename);
                panel_attributes.Controls.Add(new LiteralControl("<br/>"));
                Telerik.Web.UI.RadComboBox radattribute = new Telerik.Web.UI.RadComboBox();
                radattribute.ID = "rad_" + row_attributes[attribute]["serviceAttrId"].ToString();
                string[] arrAttributes = row_attributes[attribute]["serviceAttrValues"].ToString().Split(',');
                for (int id = 0; id < arrAttributes.Length; id++)
                {
                    RadComboBoxItem item1 = new RadComboBoxItem(arrAttributes[id].ToString(), arrAttributes[id].ToString()); //Creates new item for RadCombobox  
                    radattribute.Items.Add(item1);
                }
                panel_attributes.Controls.Add(radattribute);
                panel_attributes.Controls.Add(new LiteralControl("</td>"));
            }
            panel_attributes.Controls.Add(new LiteralControl("<td><br/>"));
            panel_attributes.Controls.Add(btn_launch);
            panel_attributes.Controls.Add(new LiteralControl("</td></tr></table>"));
        }
    }
    protected void btn_viewservice_Click(object sender, EventArgs e)
    {
        DataRow[] row = dt_Service.Select("serviceId=" + radcombo_services.SelectedValue);
        lbl_servicename.Text = row[0]["serviceName"].ToString();
        hidden_url.Value = row[0]["URL"].ToString();
        lbl_serviceurl.Text = "<span style='font-weight:bold'>URL: </span>" + row[0]["URL"].ToString();
        txt_description.Text = row[0]["serviceDescription"].ToString();
        bindgrid();
        radgrid_Template.Visible = true;
        td_info.Visible = true;
        //td_template.Visible = true;

    }
    public void launchURL()
    {
        DataRow[] row_attributes = dt_serviceattributes.Select("serviceId=" + radcombo_services.SelectedValue);
        panel_attributes.Visible = true;
        string querystring = "";
        for (int attribute = 0; attribute < row_attributes.Length; attribute++)
        {
            Label lbl_attribute = (Label)panel_attributes.FindControl("lbl_" + row_attributes[attribute]["serviceAttrId"].ToString());
            Telerik.Web.UI.RadComboBox radattribute = (Telerik.Web.UI.RadComboBox)panel_attributes.FindControl("rad_" + row_attributes[attribute]["serviceAttrId"].ToString());
            querystring += lbl_attribute.Text + "=" + radattribute.SelectedValue + "&";
        }

        if (!String.IsNullOrEmpty(querystring))
            querystring = querystring.Remove(querystring.Length - 1, 1);

        hidden_serviceurl.Value = hidden_url.Value + "?" + querystring;
        lbl_url.Text = "<span style='color:green;font-weight:bold;'>" + radcombo_services.SelectedItem.Text + "</span> Service URL: " + hidden_url.Value + "?" + querystring;
    }
    protected void btn_launch_Click(object sender, EventArgs e)
    {
        lbl_url.Text = "";
        launchURL();
        SqlHelper.ExecuteNonQuery(GlobalConnetionString.ClientConnection(), CommandType.Text, "Insert into daServiceTemplates(serviceId,templateName,URL)" +
            " values ('" + radcombo_services.SelectedValue + "','" + txt_templatename.Text + "','" + hidden_serviceurl.Value + "')");
        bindgrid();
        radgrid_Template.Visible = true;
    }
}