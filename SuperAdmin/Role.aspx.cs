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
    DataTable dt_Table;
    public static DataTable dt_rolepermission;
    public static DataTable dt_modules = new DataTable();
    protected void Page_Load(object sender, EventArgs e)
    {
        //radtxt_rolename.Enabled = true;

        dt_rolepermission = SqlHelper.ExecuteDataset(GlobalConnetionString.ConnectionString, CommandType.Text, "select * from SuperUserRoles ur  left outer join SuperUserTypePermissions up " +
            " on  ur.userRoleID=up.userRoleID left outer join SuperUserModules m on up.moduleID=m.moduleID").Tables[0];

        bindModules();
        //}

    }
    public void bindModules()
    {
        int rowcount = 1, itemrow = 0;
        dt_modules = SqlHelper.ExecuteDataset(GlobalConnetionString.ConnectionString, CommandType.Text, "Select * from SuperUserModules").Tables[0];
        panel_modules.Controls.Add(new LiteralControl("<table border='1' style='width:500px'><tr>"));


        for (int module = 1; module <= dt_modules.Rows.Count; module++)
        {
            HiddenField hidden_moduleid = new HiddenField();
            hidden_moduleid.ID = dt_modules.Rows[module - 1]["moduleID"].ToString();
            panel_modules.Controls.Add(new LiteralControl("<td align='center'><strong>" + dt_modules.Rows[module - 1]["moduleName"].ToString() + "</strong></td>"));
            panel_modules.Controls.Add(hidden_moduleid);
            int incrementVal = 8;

            if (itemrow + incrementVal > dt_modules.Rows.Count)
            {
                incrementVal = 6;
            }
            if (rowcount % 8 == 0)
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
            if (module == dt_modules.Rows.Count && module % 8 != 0)
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

            rowcount++;
        }

        //for (int moduleaccess = 0; moduleaccess < 6; moduleaccess++)
        //{
        //    DropDownList ddl_module = new DropDownList();
        //    ddl_module.Attributes.Add("style", "width:90px");
        //    //ddl_module.Width = 30;
        //    ddl_module.ID = "ddl_" + dt_modules.Rows[moduleaccess]["moduleID"].ToString();
        //    ddl_module.DataTextField = "accessType";
        //    ddl_module.DataValueField = "accessTypeID";
        //    ddl_module.DataSourceID = "SQLDataSourceUserAccess";
        //    panel_modules.Controls.Add(new LiteralControl("<td>"));
        //    panel_modules.Controls.Add(ddl_module);
        //    panel_modules.Controls.Add(new LiteralControl("</td>"));
        //}
        //if (rowcount == 6)
        //{
        //    panel_modules.Controls.Add(new LiteralControl("</tr>"));
        //}

        panel_modules.Controls.Add(new LiteralControl("</table>"));

    }
    public bool validateRolenameexistinDB()
    {
        dt_roles = SqlHelper.ExecuteDataset(GlobalConnetionString.ConnectionString, CommandType.Text, "Select * from SuperUserRoles").Tables[0];

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

        dt_Table = SqlHelper.ExecuteDataset(GlobalConnetionString.ConnectionString, CommandType.Text, "select * from SuperUserRoles ur right outer join SuperUserModules m on ur.userRoleID=m.moduleID").Tables[0];
        for (int column = 0; column < dt_Table.Rows.Count; column++)
        {
            GridDropDownColumn dropDownColumn = new GridDropDownColumn();
            dropDownColumn.UniqueName = dt_Table.Rows[column]["moduleID"].ToString() + "DropDownColumnUniqueName";
            dropDownColumn.ItemStyle.Width = 80;
            dropDownColumn.HeaderText = dt_Table.Rows[column]["moduleName"].ToString();
            dropDownColumn.DataField = "moduleName";
            dropDownColumn.ListTextField = "accessType";
            dropDownColumn.ListValueField = "accessTypeID";
            dropDownColumn.DataSourceID = "SQLDataSourceUserAccess";
            grid_roleaccess.MasterTableView.Columns.Add(dropDownColumn);
        }
        grid_roleaccess.MasterTableView.Columns.Add(new GridEditCommandColumn());

    }
    protected void btn_create_Click(object sender, EventArgs e)
    {

        if (btn_create.Text == "Create")
        {
            if (validateRolenameexistinDB())
            {
                SqlHelper.ExecuteNonQuery(GlobalConnetionString.ConnectionString, CommandType.Text, "Insert into SuperUserRoles(userRole,userRoleDescription) values('" + radtxt_rolename.Text + "','" + radtxt_roledesc.Text + "')");
                dt_roles = SqlHelper.ExecuteDataset(GlobalConnetionString.ConnectionString, CommandType.Text, "Select * from SuperUserRoles").Tables[0];

                DataRow[] row = dt_roles.Select("userRole='" + radtxt_rolename.Text.Trim() + "'");
                for (int module = 0; module < dt_modules.Rows.Count; module++)
                {
                    DropDownList ddl_module = new DropDownList();
                    ddl_module = (DropDownList)panel_modules.FindControl("ddl_" + dt_modules.Rows[module]["moduleID"].ToString());
                    SqlHelper.ExecuteNonQuery(GlobalConnetionString.ConnectionString, CommandType.Text, "Insert into SuperUserTypePermissions(moduleID,userRoleID,accessTypeID)" +
                        " values('" + dt_modules.Rows[module]["moduleID"].ToString() + "','" + row[0]["userRoleID"].ToString() + "','" + ddl_module.SelectedValue + "')");
                }
                lbl_message.ForeColor = Color.Green;
                lbl_message.Text = "Role Information inserted successfully";

            }
            else
            {
                lbl_message.ForeColor = Color.Red;
                lbl_message.Text = "Role name already exist!";
            }
        }
        else
        {
            SqlHelper.ExecuteNonQuery(GlobalConnetionString.ConnectionString, CommandType.Text, "Update SuperUserRoles set userRole='" + radtxt_rolename.Text + "', userRoleDescription='" + radtxt_roledesc.Text + "' where userRoleID='" + hidden_roleid.Value + "'");
            for (int module = 0; module < dt_modules.Rows.Count; module++)
            {
                DropDownList ddl_module = new DropDownList();
                ddl_module = (DropDownList)panel_modules.FindControl("ddl_" + dt_modules.Rows[module]["moduleID"].ToString());
                DataRow[] row_access = dt_rolepermission.Select("moduleID='" + dt_modules.Rows[module]["moduleID"].ToString() + "' and userRoleID='" + hidden_roleid.Value + "'");
                if (row_access.Length > 0)
                {
                    SqlHelper.ExecuteNonQuery(GlobalConnetionString.ConnectionString, CommandType.Text, "Update SuperUserTypePermissions set moduleID='" + dt_modules.Rows[module]["moduleID"].ToString() + "'" +
                    " ,userRoleID='" + hidden_roleid.Value + "',accessTypeID='" + ddl_module.SelectedValue + "' where userTypePermID='" + row_access[0]["userTypePermID"].ToString() + "'");
                }
                else
                {
                    SqlHelper.ExecuteNonQuery(GlobalConnetionString.ConnectionString, CommandType.Text, "Insert into SuperUserTypePermissions(moduleID,userRoleID,accessTypeID)" +
                         " values('" + dt_modules.Rows[module]["moduleID"].ToString() + "','" + hidden_roleid.Value + "','" + ddl_module.SelectedValue + "')");
                }
            }
            lbl_message.ForeColor = Color.Green;
            lbl_message.Text = "Role Information updated successfully";
            btn_create.Text = "Create";
        }
        radtxt_rolename.Text = string.Empty;
        radtxt_roledesc.Text = string.Empty;
        dt_rolepermission = SqlHelper.ExecuteDataset(GlobalConnetionString.ConnectionString, CommandType.Text, "select * from SuperUserRoles ur  left outer join " +
            " SuperUserTypePermissions up  on  ur.userRoleID=up.userRoleID left outer join SuperUserModules m on up.moduleID=m.moduleID").Tables[0];
        grid_roleaccess.Rebind();

    }

    protected void radtxt_rolename_TextChanged(object sender, EventArgs e)
    {
        if (validateRolenameexistinDB())
        {
            lbl_message.Text = "";
        }
        else
        {
            lbl_message.ForeColor = Color.Red;
            lbl_message.Text = "Role name already exist!";
        }
    }
    protected void btn_reset_Click(object sender, EventArgs e)
    {
        radtxt_rolename.Text = string.Empty;
        radtxt_roledesc.Text = string.Empty;
        btn_create.Text = "Create";
        grid_roleaccess.Rebind();
    }

    protected void grid_roleaccess_ItemDataBound(object sender, GridItemEventArgs e)
    {
        if (e.Item.ItemType == GridItemType.AlternatingItem || e.Item.ItemType == GridItemType.Item)
        {
            DataRow[] row_access = dt_rolepermission.Select("userRoleID=" + (e.Item as GridDataItem).GetDataKeyValue("userRoleID").ToString() + "");
            GridDataItem dataBoundItem = e.Item as GridDataItem;
            int accessTypeID = 0;
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
            radtxt_rolename.Text = row_access[0]["userRole"].ToString();
            radtxt_roledesc.Text = row_access[0]["userRoleDescription"].ToString();

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
            grid_roleaccess.Rebind();
            btn_create.Text = "Update";
            e.Canceled = true;
        }

    }
}