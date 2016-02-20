using System;
using System.Data;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

public partial class Modules_DA_SetupWebService : System.Web.UI.Page
{
    DataTable dtService, dtServiceAttributes;

    protected void Page_Load(object sender, EventArgs e)
    {

        dtServiceAttributes = SqlHelper.ExecuteDataset(GlobalConnetionString.ClientConnection(), CommandType.Text, "select * from daServiceAttributes").Tables[0];
        //string[] postSource = dtService
        //             .AsEnumerable()
        //             .Select<System.Data.DataRow, String>(x => x.Field<String>("Title"))
        //             .ToArray();


        if (!IsPostBack)
        {
            gridbind();
        }
    }
    public void gridbind()
    {
        dtService = SqlHelper.ExecuteDataset(GlobalConnetionString.ClientConnection(), CommandType.Text, "select * from daservice").Tables[0];
        radgrid_services.DataSource = dtService;
        radgrid_services.DataBind();
    }
    public string DataRowToString(DataRow dr)
    {
        return dr["serviceName"].ToString();
    }
    protected void CheckChanged(object sender, EventArgs e)
    {
        GridDataItem row = (GridDataItem)(((CheckBox)sender).NamingContainer);
        Label tmpLabel = (Label)row.FindControl("lbl_tempid");
        Label lbl_statuscheck = (Label)row.FindControl("lbl_statuscheck");
        int eventCategoryId = Convert.ToInt32(tmpLabel.Text);
        if (lbl_statuscheck.Text == "Active")
        {
            SqlHelper.ExecuteNonQuery(GlobalConnetionString.ClientConnection(), CommandType.Text, "update daService set status='Inactive' where serviceId=" + eventCategoryId + "");
        }
        else
        {
            SqlHelper.ExecuteNonQuery(GlobalConnetionString.ClientConnection(), CommandType.Text, "update daService set status='Active' where serviceId=" + eventCategoryId + "");
        }
        gridbind();
    }
    protected void radgrid_services_ItemDataBound(object sender, GridItemEventArgs e)
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
    protected void btn_submit_Click(object sender, EventArgs e)
    {
        gridbind();
        DataRow[] row_service = dtService.Select("serviceName='" + txt_servicename.Text + "'");
        
        try
        {
            if (btn_submit.Text == "Submit")
            {
                if (row_service.Length == 0)
                {
                    string serviceId = "";
                    SqlHelper.ExecuteNonQuery(GlobalConnetionString.ClientConnection(), CommandType.Text, "Insert into daService(serviceName,serviceDescription,URL,userName,passWord)" +
                        " values('" + txt_servicename.Text + "','" + txt_servicedesc.Text + "','" + txt_serviceurl.Text + "','" + txt_serviceusername.Text + "','" + txt_servicepassword.Text + "')");
                    dtService = SqlHelper.ExecuteDataset(GlobalConnetionString.ClientConnection(), CommandType.Text, "SELECT * FROM    daService WHERE  serviceId = IDENT_CURRENT('daService')").Tables[0];
                    serviceId = dtService.Rows[0]["serviceId"].ToString();
                    if (serviceId != "")
                    {
                        if (txt_parameters_1.Text != "" && txt_attr_1.Text != "")
                        {
                            SqlHelper.ExecuteNonQuery(GlobalConnetionString.ClientConnection(), CommandType.Text, "Insert into daServiceAttributes(serviceId,serviceAttr,serviceAttrValues)" +
                            " values('" + serviceId + "','" + txt_parameters_1.Text + "','" + txt_attr_1.Text + "')");
                        }
                        if (txt_parameters_2.Text != "" && txt_attr_2.Text != "")
                        {
                            SqlHelper.ExecuteNonQuery(GlobalConnetionString.ClientConnection(), CommandType.Text, "Insert into daServiceAttributes(serviceId,serviceAttr,serviceAttrValues)" +
                            " values('" + serviceId + "','" + txt_parameters_2.Text + "','" + txt_attr_2.Text + "')");
                        }
                        if (txt_parameters_3.Text != "" && txt_attr_3.Text != "")
                        {
                            SqlHelper.ExecuteNonQuery(GlobalConnetionString.ClientConnection(), CommandType.Text, "Insert into daServiceAttributes(serviceId,serviceAttr,serviceAttrValues)" +
                            " values('" + serviceId + "','" + txt_parameters_3.Text + "','" + txt_attr_3.Text + "')");
                        }


                    }
                }
                else
                {
                    lbl_message.Text = txt_servicename.Text + " already exists.";
                    return;
                } 
            }
            else
            {
                SqlHelper.ExecuteNonQuery(GlobalConnetionString.ClientConnection(), CommandType.Text, "Update daService set serviceName='" + txt_servicename.Text + "'," +
                    " serviceDescription='" + txt_servicedesc.Text + "',URL='" + txt_serviceurl.Text + "',userName='" + txt_serviceusername.Text + "'," +
                    " passWord='" + txt_servicepassword.Text + "' where serviceId=" + hidden_serviceid.Value + "");
                //1st Attribute
                if (hidden_serviceattr1id.Value != "")
                {
                    SqlHelper.ExecuteNonQuery(GlobalConnetionString.ClientConnection(), CommandType.Text, "Update daServiceAttributes  set serviceAttr='" + txt_parameters_1.Text + "'," +
                        " serviceAttrValues='" + txt_attr_1.Text + "' where serviceAttrId=" + hidden_serviceattr1id.Value + "");
                }
                else if (txt_parameters_1.Text != "" && txt_attr_1.Text != "")
                {
                    SqlHelper.ExecuteNonQuery(GlobalConnetionString.ClientConnection(), CommandType.Text, "Insert into daServiceAttributes(serviceId,serviceAttr,serviceAttrValues)" +
                        " values('" + hidden_serviceid.Value + "','" + txt_parameters_1.Text + "','" + txt_attr_1.Text + "')");
                }
                //2nd Attribute
                if (hidden_serviceattr2id.Value != "")
                {
                    SqlHelper.ExecuteNonQuery(GlobalConnetionString.ClientConnection(), CommandType.Text, "Update daServiceAttributes  set serviceAttr='" + txt_parameters_2.Text + "'," +
                        " serviceAttrValues='" + txt_attr_2.Text + "' where serviceAttrId=" + hidden_serviceattr2id.Value + "");
                }
                else if (txt_parameters_2.Text != "" && txt_attr_2.Text != "")
                {
                    SqlHelper.ExecuteNonQuery(GlobalConnetionString.ClientConnection(), CommandType.Text, "Insert into daServiceAttributes(serviceId,serviceAttr,serviceAttrValues)" +
                        " values('" + hidden_serviceid.Value + "','" + txt_parameters_2.Text + "','" + txt_attr_2.Text + "')");
                }
                //3rd Attribute
                if (hidden_serviceattr3id.Value != "")
                {
                    SqlHelper.ExecuteNonQuery(GlobalConnetionString.ClientConnection(), CommandType.Text, "Update daServiceAttributes  set serviceAttr='" + txt_parameters_3.Text + "'," +
                        " serviceAttrValues='" + txt_attr_3.Text + "' where serviceAttrId=" + hidden_serviceattr3id.Value + "");
                }
                else if (txt_parameters_3.Text != "" && txt_attr_3.Text != "")
                {
                    SqlHelper.ExecuteNonQuery(GlobalConnetionString.ClientConnection(), CommandType.Text, "Insert into daServiceAttributes(serviceId,serviceAttr,serviceAttrValues)" +
                        " values('" + hidden_serviceid.Value + "','" + txt_parameters_3.Text + "','" + txt_attr_3.Text + "')");
                }


            }
            DataTable dtService1 = SqlHelper.ExecuteDataset(GlobalConnetionString.ClientConnection(), CommandType.Text, "select * from daservice ").Tables[0];
            // dtServiceAttributes = SqlHelper.ExecuteDataset(GlobalConnetionString.ClientConnection(), CommandType.Text, "select * from daServiceAttributes ").Tables[0];
            //if (!IsPostBack)
            //{
            radgrid_services.DataSource = dtService1;
            radgrid_services.DataBind();

            radgrid_services.Rebind();
        }
        catch (Exception ex) { }
        finally
        {
        }
    }
    protected void btn_reset_Click(object sender, EventArgs e)
    {

    }
    public void clear_text()
    {
        //txt_parameters_1.Text = "";
        //txt_parameters_2.Text = "";
        //txt_parameters_3.Text = "";
        //txt_parameters_4.Text = "";
        //txt_parameters_5.Text = "";
        //txt_productname.Text = "";
        //txt_servicedesc.Text = "";
        //txt_servicepassword.Text = "";
        //txt_serviceurl.Text = "";
        //txt_serviceusername.Text = "";
        //txt_xml_name_1.Text = "";
        //txt_xml_name_2.Text = "";
        //txt_xml_name_3.Text = "";
        //txt_xml_name_4.Text = "";
        //txt_xml_name_5.Text = "";
    }
    protected void radgrid_services_ItemCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
    {
        gridbind();
        if (e.CommandName == RadGrid.EditCommandName)
        {
            clearall();
            btn_submit.Text = "Update";
            GridDataItem editedItem = e.Item as GridDataItem;
            hidden_serviceid.Value = (e.Item as GridDataItem).GetDataKeyValue("serviceId").ToString();
            DataRow[] row_service = dtService.Select("serviceId=" + (e.Item as GridDataItem).GetDataKeyValue("serviceId").ToString());
            DataRow[] row_serviceAttribute = dtServiceAttributes.Select("serviceId=" + (e.Item as GridDataItem).GetDataKeyValue("serviceId").ToString());
            if (row_service.Length > 0)
            {
                txt_servicename.Text = row_service[0]["serviceName"].ToString();
                txt_servicedesc.Text = row_service[0]["serviceDescription"].ToString();
                txt_serviceurl.Text = row_service[0]["URL"].ToString();
                txt_serviceusername.Text = row_service[0]["userName"].ToString();
                txt_servicepassword.Text = row_service[0]["passWord"].ToString();
                if (row_serviceAttribute.Length > 0)
                {
                    //for (int i = 0; i < row_serviceAttribute.Length; i++)
                    //{
                    if (row_serviceAttribute.Length == 1)
                    {
                        txt_parameters_1.Text = row_serviceAttribute[0]["serviceAttr"].ToString();
                        txt_attr_1.Text = row_serviceAttribute[0]["serviceAttrValues"].ToString();
                        hidden_serviceattr1id.Value = row_serviceAttribute[0]["serviceAttrId"].ToString();
                    }
                    else if (row_serviceAttribute.Length == 2)
                    {
                        txt_parameters_1.Text = row_serviceAttribute[0]["serviceAttr"].ToString();
                        txt_attr_1.Text = row_serviceAttribute[0]["serviceAttrValues"].ToString();
                        hidden_serviceattr1id.Value = row_serviceAttribute[0]["serviceAttrId"].ToString();
                        txt_parameters_2.Text = row_serviceAttribute[1]["serviceAttr"].ToString();
                        txt_attr_2.Text = row_serviceAttribute[1]["serviceAttrValues"].ToString();
                        hidden_serviceattr2id.Value = row_serviceAttribute[1]["serviceAttrId"].ToString();
                    }
                    else
                    {
                        txt_parameters_1.Text = row_serviceAttribute[0]["serviceAttr"].ToString();
                        txt_attr_1.Text = row_serviceAttribute[0]["serviceAttrValues"].ToString();
                        hidden_serviceattr1id.Value = row_serviceAttribute[0]["serviceAttrId"].ToString();
                        txt_parameters_2.Text = row_serviceAttribute[1]["serviceAttr"].ToString();
                        txt_attr_2.Text = row_serviceAttribute[1]["serviceAttrValues"].ToString();
                        hidden_serviceattr2id.Value = row_serviceAttribute[1]["serviceAttrId"].ToString();
                        txt_parameters_3.Text = row_serviceAttribute[2]["serviceAttr"].ToString();
                        txt_attr_3.Text = row_serviceAttribute[2]["serviceAttrValues"].ToString();
                        hidden_serviceattr3id.Value = row_serviceAttribute[2]["serviceAttrId"].ToString();
                    }
                }
                e.Canceled = true;
                //}
            }
        }
        radgrid_services.Rebind();
    }
    public void clearall()
    {
        btn_submit.Text = "Submit";
        txt_servicename.Text = "";
        txt_servicedesc.Text = "";
        txt_servicepassword.Text = "";
        txt_serviceurl.Text = "";
        txt_serviceusername.Text = "";
        txt_parameters_3.Text = "";
        txt_parameters_2.Text = "";
        txt_parameters_1.Text = "";
        txt_attr_3.Text = "";
        txt_attr_2.Text = "";
        txt_attr_1.Text = "";
    }
    protected void txt_servicename_TextChanged(object sender, EventArgs e)
    {
        gridbind();
        DataRow[] row_service = dtService.Select("serviceName='" + txt_servicename.Text + "'");
        if (row_service.Length != 0)
        {
            lbl_message.Text = txt_servicename.Text + " already exists.";
        }
    }
    protected void btn_reset_Click1(object sender, EventArgs e)
    {
        clearall();
    }
}