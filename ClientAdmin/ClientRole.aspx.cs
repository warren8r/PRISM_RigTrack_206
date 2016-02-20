using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using Telerik.Web.UI;
using System.Drawing;

public partial class Role : System.Web.UI.Page
{
    DataTable dt_roles = new DataTable();
    DataTable dt_Table, dt_subscription;
    public static DataTable dt_rolepermission;
    public static DataTable dt_modules = new DataTable();
    public static DataTable dt_rolenotification = new DataTable();
    SqlConnection db = new SqlConnection(GlobalConnetionString.ClientConnection());
    SqlTransaction transaction;
    ClientMaster userMaster = new ClientMaster();
    protected void Page_Load(object sender, EventArgs e)
    {
        lbl_message.Text = "";
        try
        {
            userMaster = (ClientMaster)Session["UserMasterDetails"];
        }
        catch (Exception ex)
        {
            userMaster = null;
        }

        if (userMaster == null)
        {
            Response.Redirect("~/ClientLogin.aspx");
        }
        else
        {
            this.Session["userRoleID"] = userMaster.UserTypeID;
        }
        SQLDataSourceUserAccess.ConnectionString = GlobalConnetionString.ClientConnection();
        SqlDataSource1.ConnectionString = GlobalConnetionString.ClientConnection();
        //radtxt_rolename.Enabled = true;

        dt_rolepermission = SqlHelper.ExecuteDataset(GlobalConnetionString.ClientConnection(), CommandType.Text, "select * from UserRoles ur  left outer join UserTypePermissions up " +
                " on  ur.userRoleID=up.userRoleID left outer join Modules m on up.moduleID=m.moduleID WHERE m.parentId = '0' order by ur.userRoleID ,m.moduleID").Tables[0];
        dt_rolenotification = SqlHelper.ExecuteDataset(GlobalConnetionString.ClientConnection(), CommandType.Text, "select * from Prism_UserRole_Notification").Tables[0];
        bindModules();

        dt_subscription = SqlHelper.ExecuteDataset(GlobalConnetionString.ConnectionString, CommandType.Text,
                               "select * from ClientSubscription where clientID='" + Session["clientID"].ToString() + "'").Tables[0];
        if (dt_subscription.Rows.Count > 0)
        {
            dt_roles = SqlHelper.ExecuteDataset(GlobalConnetionString.ClientConnection(), CommandType.Text,
        "Select * from UserRoles").Tables[0];
            lbl_total_usertypes.Text = "Total User Type(s) Used <span style='color:green;font-weight:bold;'> (" + dt_roles.Rows.Count + ")</span>";
            lbl_max_roles.Text = "Maximum User Type(s) Allowed <span style='color:green;font-weight:bold;'> (" + dt_subscription.Rows[0]["maxUserTypes"].ToString() + ")</span>";

            if (dt_roles.Rows.Count >= Convert.ToInt32(dt_subscription.Rows[0]["maxUserTypes"].ToString()))
            {
                btn_create.Enabled = false;
                hidd_maxcount.Value = "1";
            }
            else
            {
                btn_create.Enabled = true;
                hidd_maxcount.Value = "0";
            }


        }
        //}
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
            //GridDataItem item = (GridDataItem)e.Item;
            //Label lbl_statuscheck = (Label)item.FindControl("lbl_statuscheck");
            //Label lbl_eventid = (Label)item.FindControl("lbl_eventid");
            //Label lbl_notification = (Label)item.FindControl("lbl_notification");
            //CheckBox isChecked = (CheckBox)item.FindControl("isChecked");
            //if (lbl_notification.Text == "True")
            //{
            //    lbl_statuscheck.Text = "Required";
            //    lbl_statuscheck.ForeColor = Color.Green;
            //    isChecked.Checked = true;
            //}
            //else
            //{
            //    lbl_statuscheck.Text = "Not&#160;Required";
            //    //lbl_statuscheck.Text = dt_exist.Rows[0]["repairstatus"].ToString();
            //    lbl_statuscheck.ForeColor = Color.Red;
            //    isChecked.Checked = false;
            //}


        }
    }
    public void bindModules()
    {
        int rowcount = 1, itemrow = 0;
        dt_modules = SqlHelper.ExecuteDataset(GlobalConnetionString.ClientConnection(), CommandType.Text,
            "Select * from Modules WHERE Modules.parentId = '0'").Tables[0];
        panel_modules.Controls.Add(new LiteralControl("<table border='1' style='width:500px'><tr>"));
        for (int module = 1; module <= dt_modules.Rows.Count; module++)
        {
            HiddenField hidden_moduleid = new HiddenField();
            hidden_moduleid.ID = dt_modules.Rows[module - 1]["moduleID"].ToString();
            panel_modules.Controls.Add(new LiteralControl("<td align='center'><strong>" + dt_modules.Rows[module - 1]["moduleName"].ToString() + "</strong></td>"));
            panel_modules.Controls.Add(hidden_moduleid);
            int incrementVal = 11;
            if (itemrow + incrementVal > dt_modules.Rows.Count)
            {
                incrementVal = 10;
            }
            if (rowcount % 7 == 0)
            {
                panel_modules.Controls.Add(new LiteralControl("</tr><tr>"));
                for (int moduleaccess = itemrow; moduleaccess < itemrow + incrementVal; moduleaccess++)
                {
                    DropDownList ddl_module = new DropDownList();
                    ddl_module.Attributes.Add("style", "width:90px");
                    //ddl_module.Width = 30;
                    ddl_module.ID = "ddl_" + dt_modules.Rows[moduleaccess]["moduleID"].ToString();
                    ddl_module.DataTextField = "accessType";
                    ddl_module.DataValueField = "accessTypeID";
                    ddl_module.DataSourceID = "SQLDataSourceUserAccess";
                    panel_modules.Controls.Add(new LiteralControl("<td>"));
                    panel_modules.Controls.Add(ddl_module);
                    panel_modules.Controls.Add(new LiteralControl("</td>"));

                }
                itemrow = itemrow + incrementVal;

                rowcount = 0;
                panel_modules.Controls.Add(new LiteralControl("</tr>"));
            }
            if (module == dt_modules.Rows.Count && module % 7 != 0)
            {
                panel_modules.Controls.Add(new LiteralControl("</tr><tr>"));
                for (int moduleaccess = itemrow; moduleaccess < itemrow + incrementVal - 4; moduleaccess++)
                {
                    DropDownList ddl_module = new DropDownList();
                    ddl_module.Attributes.Add("style", "width:90px");
                    //ddl_module.Width = 30;
                    ddl_module.ID = "ddl_" + dt_modules.Rows[moduleaccess]["moduleID"].ToString();
                    ddl_module.DataTextField = "accessType";
                    ddl_module.DataValueField = "accessTypeID";
                    ddl_module.DataSourceID = "SQLDataSourceUserAccess";
                    panel_modules.Controls.Add(new LiteralControl("<td>"));
                    panel_modules.Controls.Add(ddl_module);
                    panel_modules.Controls.Add(new LiteralControl("</td>"));

                }
                itemrow = itemrow + incrementVal;

                rowcount = 0;
                panel_modules.Controls.Add(new LiteralControl("</tr>"));
            }

            rowcount++;
        }
        panel_modules.Controls.Add(new LiteralControl("</table>"));

    }
    //public void bindModules()
    //{
    //    int rowcount = 1, itemrow = 0;
    //    dt_modules = SqlHelper.ExecuteDataset(GlobalConnetionString.ClientConnection(), CommandType.Text,
    //        "Select * from Modules WHERE Modules.parentId = '0'").Tables[0];
    //    panel_modules.Controls.Add(new LiteralControl("<table border='1' style='width:500px'><tr>"));
    //    for (int module = 1; module <= dt_modules.Rows.Count; module++)
    //    {
    //        HiddenField hidden_moduleid = new HiddenField();
    //        hidden_moduleid.ID = dt_modules.Rows[module - 1]["moduleID"].ToString();
    //        panel_modules.Controls.Add(new LiteralControl("<td align='center'><strong>" + dt_modules.Rows[module - 1]["moduleName"].ToString() + "</strong></td>"));
    //        panel_modules.Controls.Add(hidden_moduleid);
    //        int incrementVal = 7;
    //        if (itemrow + incrementVal > dt_modules.Rows.Count)
    //        {
    //            incrementVal = 6;
    //        }
    //        if (rowcount % 7 == 0)
    //        {
    //            panel_modules.Controls.Add(new LiteralControl("</tr><tr>"));
    //            for (int moduleaccess = itemrow; moduleaccess < itemrow + incrementVal; moduleaccess++)
    //            {
    //                DropDownList ddl_module = new DropDownList();
    //                ddl_module.Attributes.Add("style", "width:90px");
    //                //ddl_module.Width = 30;
    //                ddl_module.ID = "ddl_" + dt_modules.Rows[moduleaccess]["moduleID"].ToString();
    //                ddl_module.DataTextField = "accessType";
    //                ddl_module.DataValueField = "accessTypeID";
    //                ddl_module.DataSourceID = "SQLDataSourceUserAccess";
    //                panel_modules.Controls.Add(new LiteralControl("<td>"));
    //                panel_modules.Controls.Add(ddl_module);
    //                panel_modules.Controls.Add(new LiteralControl("</td>"));

    //            }
    //            itemrow = itemrow + incrementVal;

    //            rowcount = 0;
    //            panel_modules.Controls.Add(new LiteralControl("</tr>"));
    //        }
    //        if (module == dt_modules.Rows.Count && module % 7 != 0)
    //        {
    //            panel_modules.Controls.Add(new LiteralControl("</tr><tr>"));
    //            for (int moduleaccess = itemrow; moduleaccess < itemrow + incrementVal - 5; moduleaccess++)
    //            {
    //                DropDownList ddl_module = new DropDownList();
    //                ddl_module.Attributes.Add("style", "width:90px");
    //                //ddl_module.Width = 30;
    //                ddl_module.ID = "ddl_" + dt_modules.Rows[moduleaccess]["moduleID"].ToString();
    //                ddl_module.DataTextField = "accessType";
    //                ddl_module.DataValueField = "accessTypeID";
    //                ddl_module.DataSourceID = "SQLDataSourceUserAccess";
    //                panel_modules.Controls.Add(new LiteralControl("<td>"));
    //                panel_modules.Controls.Add(ddl_module);
    //                panel_modules.Controls.Add(new LiteralControl("</td>"));

    //            }
    //            itemrow = itemrow + incrementVal;

    //            rowcount = 0;
    //            panel_modules.Controls.Add(new LiteralControl("</tr>"));
    //        }

    //        rowcount++;
    //    }
    //    panel_modules.Controls.Add(new LiteralControl("</table>"));

    //}
    public bool validateRolenameexistinDB(SqlTransaction tr)
    {
        dt_roles = SqlHelper.ExecuteDataset(tr, CommandType.Text,
            "Select * from UserRoles").Tables[0];

        DataRow[] row = dt_roles.Select("userRole='" + radtxt_rolename.Text.Trim() + "'");
        if (row.Length == 0)
            return true;
        else
            return false;
    }
    protected void Page_Init(object sender, EventArgs e)
    {
        GridBoundColumn boundcolumn1 = new GridBoundColumn();
        boundcolumn1.HeaderText = "User&#160;Type";
        boundcolumn1.DataField = "userRole";
        //boundcolumn1.Visible = false;
        boundcolumn1.ReadOnly = true;
        grid_roleaccess.MasterTableView.Columns.Add(boundcolumn1);

        dt_Table = SqlHelper.ExecuteDataset(GlobalConnetionString.ClientConnection(), CommandType.Text,
            "select distinct u.userRoleID,u.userRole from UserRoles u,UserTypePermissions up,Modules m where u.userRoleID=up.userRoleID and m.moduleID=up.moduleID and m.parentId = '0'").Tables[0];
        for (int column = 1; column < 8; column++)
        {
            DataTable dt_ModuleTable = SqlHelper.ExecuteDataset(GlobalConnetionString.ClientConnection(), CommandType.Text,
            "select * from Modules m where m.parentId = '0' and m.moduleID=" + column + "").Tables[0];
            if (dt_ModuleTable.Rows.Count > 0)
            {
                GridDropDownColumn dropDownColumn = new GridDropDownColumn();
                dropDownColumn.UniqueName = dt_ModuleTable.Rows[0]["moduleID"].ToString() + "DropDownColumnUniqueName";
                dropDownColumn.ItemStyle.Width = 80;
                dropDownColumn.HeaderText = dt_ModuleTable.Rows[0]["moduleName"].ToString();
                dropDownColumn.DataField = "userRole";
                dropDownColumn.ListTextField = "accessType";
                dropDownColumn.ListValueField = "accessTypeID";
                dropDownColumn.DataSourceID = "SQLDataSourceUserAccess";
                grid_roleaccess.MasterTableView.Columns.Add(dropDownColumn);
            }
        }
        grid_roleaccess.MasterTableView.Columns.Add(new GridEditCommandColumn());
        //grid_roleaccess.DataBind();


    }
    protected void btn_create_Click(object sender, EventArgs e)
    {
        db.Open();
        transaction = db.BeginTransaction();
        if (btn_create.Text == "Create")
        {

            try
            {
                if (validateRolenameexistinDB(transaction))
                {
                    SqlHelper.ExecuteNonQuery(transaction, CommandType.Text,
                        "Insert into UserRoles(userRole,userRoleDescription) values('" + radtxt_rolename.Text + "','" + radtxt_roledesc.Text + "')");
                    dt_roles = SqlHelper.ExecuteDataset(transaction, CommandType.Text,
                        "Select * from UserRoles").Tables[0];

                    DataRow[] row = dt_roles.Select("userRole='" + radtxt_rolename.Text.Trim() + "'");
                    for (int module = 0; module < dt_modules.Rows.Count; module++)
                    {
                        DropDownList ddl_module = new DropDownList();
                        ddl_module = (DropDownList)panel_modules.FindControl("ddl_" + dt_modules.Rows[module]["moduleID"].ToString());
                        SqlHelper.ExecuteNonQuery(transaction, CommandType.Text,
                            "Insert into UserTypePermissions(moduleID,userRoleID,accessTypeID)" +
                            " values('" + dt_modules.Rows[module]["moduleID"].ToString() + "','" + row[0]["userRoleID"].ToString() + "','" + ddl_module.SelectedValue + "')");
                    }
                    foreach (GridDataItem item in radgrid_notificationstatus.Items)
                    {
                        CheckBox isChecked = (CheckBox)item.FindControl("isChecked");
                        Label lbl_eventid = (Label)item.FindControl("lbl_eventid");
                        if (isChecked.Checked)
                        {
                            DataRow[] row_role = dt_rolenotification.Select("userRoleID=" + row[0]["userRoleID"].ToString() + " and ID=" + lbl_eventid.Text + "");
                            if (row_role.Length == 0)
                            {
                                string queryinsertrolenoti = "Insert into Prism_UserRole_Notification(userRoleID,ID,status) values" +
                                    "(" + row[0]["userRoleID"].ToString() + "," + lbl_eventid.Text + ",'Active')";
                                SqlHelper.ExecuteNonQuery(transaction, CommandType.Text, queryinsertrolenoti);
                            }
                        }
                    }
                    lbl_message.ForeColor = Color.Green;
                    lbl_message.Text = "Role Information inserted successfully";
                    transaction.Commit();
                }
                else
                {
                    lbl_message.ForeColor = Color.Red;
                    lbl_message.Text = "Role name already exist!";
                }

                //LOG USER CREATED
                auditLog logChange = new auditLog(Session["client_database"].ToString());
                logChange.addValue(new Dictionary<string, string> { 
                { "pageId", "14" }, 
                { "userId", ((ClientMaster)Session["UserMasterDetails"]).UserID.ToString() }, 
                { "attributeName", "User Type Information, Module Access" }, 
                { "description", "created" }
                //{ "system", "false" },
                //{ "supperseded", "1" } 
            }, true);
            }
            catch (Exception ex)
            {
                transaction.Rollback();
            }

        }
        else
        {
            try
            {
                SqlHelper.ExecuteNonQuery(transaction, CommandType.Text,
                    "Update UserRoles set userRole='" + radtxt_rolename.Text + "', userRoleDescription='" + radtxt_roledesc.Text + "' where userRoleID='" + hidden_roleid.Value + "'");
                for (int module = 0; module < dt_modules.Rows.Count; module++)
                {
                    DropDownList ddl_module = new DropDownList();
                    ddl_module = (DropDownList)panel_modules.FindControl("ddl_" + dt_modules.Rows[module]["moduleID"].ToString());
                    DataRow[] row_access = dt_rolepermission.Select("moduleID='" + dt_modules.Rows[module]["moduleID"].ToString() + "' and userRoleID='" + hidden_roleid.Value + "'");
                    if (row_access.Length > 0)
                    {
                        string sss = dt_modules.Rows[module]["moduleName"].ToString();
                        SqlHelper.ExecuteNonQuery(transaction, CommandType.Text,
                            "Update UserTypePermissions set moduleID='" + dt_modules.Rows[module]["moduleID"].ToString() + "'" +
                        " ,userRoleID='" + hidden_roleid.Value + "',accessTypeID='" + ddl_module.SelectedValue + "' where userTypePermID='" + row_access[0]["userTypePermID"].ToString() + "'");
                    }
                    else
                    {
                        SqlHelper.ExecuteNonQuery(transaction, CommandType.Text,
                            "Insert into UserTypePermissions(moduleID,userRoleID,accessTypeID)" +
                             " values('" + dt_modules.Rows[module]["moduleID"].ToString() + "','" + hidden_roleid.Value + "','" + ddl_module.SelectedValue + "')");
                    }
                }
                foreach (GridDataItem item in radgrid_notificationstatus.Items)
                {
                    CheckBox isChecked = (CheckBox)item.FindControl("isChecked");
                    Label lbl_eventid = (Label)item.FindControl("lbl_eventid");
                    if (isChecked.Checked)
                    {
                        DataRow[] row_role = dt_rolenotification.Select("userRoleID=" + hidden_roleid.Value + " and ID=" + lbl_eventid.Text + "");
                        if (row_role.Length == 0)
                        {
                            string queryinsertrolenoti = "Insert into Prism_UserRole_Notification(userRoleID,ID,status) values" +
                                "(" + hidden_roleid.Value + "," + lbl_eventid.Text + ",'Active')";
                            SqlHelper.ExecuteNonQuery(transaction, CommandType.Text, queryinsertrolenoti);
                        }
                        else
                        {
                            string queryupdate = "Update Prism_UserRole_Notification set ID=" + lbl_eventid.Text + ",status='Active' where userRoleID=" + hidden_roleid.Value + "";
                            SqlHelper.ExecuteNonQuery(transaction, CommandType.Text, queryupdate);
                        }
                    }
                }
                lbl_message.ForeColor = Color.Green;
                lbl_message.Text = "Role Information updated successfully";
                btn_create.Text = "Create";
                transaction.Commit();
                //LOG USER CREATED
                auditLog logChange = new auditLog(Session["client_database"].ToString());
                logChange.addValue(new Dictionary<string, string> { 
                { "pageId", "14" }, 
                { "userId", ((ClientMaster)Session["UserMasterDetails"]).UserID.ToString() }, 
                { "attributeName", "User Type Information, Module Access" }, 
                { "description", "updated" }
                //{ "system", "false" },
                //{ "supperseded", "1" } 
            }, true);
            }
            catch (Exception ex)
            {
                transaction.Rollback();
            }
        }
        radtxt_rolename.Text = string.Empty;
        radtxt_roledesc.Text = string.Empty;
        foreach (GridDataItem item in radgrid_notificationstatus.Items)
        {
            CheckBox isChecked = (CheckBox)item.FindControl("isChecked");
            isChecked.Checked = false;
        }
        for (int module = 0; module < dt_modules.Rows.Count; module++)
        {
            DropDownList ddl_module = new DropDownList();
            ddl_module = (DropDownList)panel_modules.FindControl("ddl_" + dt_modules.Rows[module]["moduleID"].ToString());
            ddl_module.SelectedIndex = 0;
        }
        db.Close();
        dt_rolepermission = SqlHelper.ExecuteDataset(GlobalConnetionString.ClientConnection(), CommandType.Text, "select * from UserRoles ur  left outer join " +
            " UserTypePermissions up  on  ur.userRoleID=up.userRoleID left outer join Modules m on up.moduleID=m.moduleID WHERE m.parentId = '0' order by  m.moduleID").Tables[0];
        grid_roleaccess.Rebind();
        if (hidd_maxcount.Value == "1")
            btn_create.Enabled = false;
        else
            btn_create.Enabled = true;

    }
    protected void radtxt_rolename_TextChanged(object sender, EventArgs e)
    {
        db.Open();
        transaction = db.BeginTransaction();
        try
        {
            if (validateRolenameexistinDB(transaction))
            {
                lbl_message.Text = "";
            }
            else
            {
                lbl_message.ForeColor = Color.Red;
                lbl_message.Text = "Role name already exist!";
            }
            transaction.Commit();
            db.Close();
        }
        catch (Exception ex)
        {
            transaction.Rollback();
        }
    }
    protected void btn_reset_Click(object sender, EventArgs e)
    {
        radtxt_rolename.Text = string.Empty;
        radtxt_roledesc.Text = string.Empty;
        btn_create.Text = "Create";
        lbl_message.Text = "";
        grid_roleaccess.Rebind();
    }
    protected void grid_roleaccess_ItemDataBound(object sender, GridItemEventArgs e)
    {
        if (e.Item.ItemType == GridItemType.AlternatingItem || e.Item.ItemType == GridItemType.Item)
        {
            //e.Item.Attributes.Add("onmouseover", "this.style.cursor='hand';this.style.backgroundColor='orangered'");
            //e.Item.Attributes.Add("onmouseout", "this.style.backgroundColor='white'");

            DataRow[] row_access = dt_rolepermission.Select("userRoleID=" + (e.Item as GridDataItem).GetDataKeyValue("userRoleID").ToString() + "");
            GridDataItem dataBoundItem = e.Item as GridDataItem;
            dataBoundItem.Attributes.Add("Onclick", "javascript:window.scrollTo(0,0);");
            int accessTypeID = 0;
            if (row_access.Length > 0)
            {
                if (row_access[0]["userRole"].ToString() != "Client Admin")
                {
                    if (dt_rolepermission != null)
                    {
                        if (dt_rolepermission.Rows[accessTypeID]["accessTypeID"].ToString() != "")
                        {
                            for (int access = 3; access < dataBoundItem.Cells.Count - 1; access++)
                            {
                                if (row_access.Length - 1 >= accessTypeID)
                                {
                                    dataBoundItem.Cells[access].Text = MailSending.getAccessName(row_access[accessTypeID]["accessTypeID"].ToString());

                                }
                                else
                                {
                                    dataBoundItem.Cells[access].Text = "No Access";
                                }
                                accessTypeID++;
                            }
                        }
                    }

                }
                else
                {
                    if (userMaster.UserTypeID != 1)
                    {
                        if (row_access[0]["userRole"].ToString() == "Client Admin")
                        {
                            dataBoundItem.Enabled = false;
                        }
                    }
                    for (int access = 3; access < dataBoundItem.Cells.Count - 1; access++)
                    {

                        dataBoundItem.Cells[access].Text = "Read/Write";

                    }
                }
            }


        }
        if (e.Item is GridEditableItem && (e.Item as GridEditableItem).IsInEditMode)
        {
            GridEditableItem editedItem = e.Item as GridEditableItem;

            GridEditManager editMan = editedItem.EditManager;
            hidden_roleid.Value = (e.Item as GridDataItem).GetDataKeyValue("userRoleID").ToString();
            DataRow[] row_access = dt_rolepermission.Select("userRoleID=" + (e.Item as GridDataItem).GetDataKeyValue("userRoleID").ToString() + "");
            for (int column = 0; column < row_access.Length; column++)
            {
                GridDropDownListColumnEditor editor = editMan.GetColumnEditor(row_access[column]["moduleID"].ToString() + "DropDownColumnUniqueName") as GridDropDownListColumnEditor;
                editor.SelectedValue = row_access[column]["accessTypeID"].ToString();

            }
            // e.Item.Attributes.Add("Onclick", "javascript: alert('hi');");
            e.Canceled = true;
        }

    }
    protected void grid_roleaccess_ItemCommand(object sender, GridCommandEventArgs e)
    {
        if (e.CommandName == RadGrid.EditCommandName)
        {
            GridDataItem editedItem = e.Item as GridDataItem;

            hidden_roleid.Value = (e.Item as GridDataItem).GetDataKeyValue("userRoleID").ToString();
            DataRow[] row_access = dt_rolepermission.Select("userRoleID=" + (e.Item as GridDataItem).GetDataKeyValue("userRoleID").ToString() + "");
            if (row_access.Length > 0)
            {
                radtxt_rolename.Text = row_access[0]["userRole"].ToString();
                radtxt_roledesc.Text = row_access[0]["userRoleDescription"].ToString();
            }
            foreach (GridDataItem item in radgrid_notificationstatus.Items)
            {
                CheckBox isChecked = (CheckBox)item.FindControl("isChecked");
                Label lbl_eventid = (Label)item.FindControl("lbl_eventid");
                DataRow[] row_role = dt_rolenotification.Select("userRoleID=" + row_access[0]["userRoleID"].ToString() + " and ID=" + lbl_eventid.Text + "");
                if (row_role.Length != 0)
                {
                    isChecked.Checked = true;
                }
                else
                    isChecked.Checked = false;
            }

            for (int module = 0; module < dt_modules.Rows.Count; module++)
            {
                DropDownList ddl_module = new DropDownList();
                ddl_module = (DropDownList)panel_modules.FindControl("ddl_" + dt_modules.Rows[module]["moduleID"].ToString());
                if (row_access.Length - 1 >= module && row_access[module]["accessTypeID"].ToString() != "")
                {
                    ddl_module.SelectedValue = row_access[module]["accessTypeID"].ToString();
                }
                else
                {
                    ddl_module.SelectedValue = "3";
                }
            }
            //  grid_roleaccess.Rebind();
            btn_create.Text = "Save";
            //btn_create.CssClass = "save";
            e.Canceled = true;

            dt_subscription = SqlHelper.ExecuteDataset(GlobalConnetionString.ConnectionString, CommandType.Text,
                                  "select * from ClientSubscription where clientID='" + Session["clientID"].ToString() + "'").Tables[0];
            if (dt_subscription.Rows.Count > 0)
            {
                dt_roles = SqlHelper.ExecuteDataset(GlobalConnetionString.ClientConnection(), CommandType.Text,
            "Select * from UserRoles").Tables[0];
                lbl_total_usertypes.Text = "Total User Type(s) Used <span style='color:green;font-weight:bold;'> (" + dt_roles.Rows.Count + ")</span>";
                lbl_max_roles.Text = "Maximum User Type(s) Allowed <span style='color:green;font-weight:bold;'> (" + dt_subscription.Rows[0]["maxUserTypes"].ToString() + ")</span>";

                if (dt_roles.Rows.Count >= Convert.ToInt32(dt_subscription.Rows[0]["maxUserTypes"].ToString()) && btn_create.Text == "Create")
                    btn_create.Enabled = false;
                else
                    btn_create.Enabled = true;
            }
            //editedItem.ForeColor = Color.GreenYellow;
            //foreach (GridDataItem dataItem in grid_roleaccess.Items)
            //{
            //    dataItem.BackColor = System.Drawing.Color.White;
            //}
            //editedItem.BackColor = System.Drawing.Color.Green;

        }

    }

}