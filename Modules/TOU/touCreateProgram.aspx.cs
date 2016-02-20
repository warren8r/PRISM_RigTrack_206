using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using ToU;

public partial class _Default : System.Web.UI.Page
{
    SqlConnection sqlcon;

    protected void Page_Load(object sender, EventArgs e)
    {
        sqlcon = new SqlConnection(Session["client_database"].ToString());

        //reset temporary changes
        resetStyles();
        writeMessage("");
    }

    protected void btnCreateUser_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            try
            {
                sqlcon.Open();
                using (SqlCommand sqlcmd = new SqlCommand("createProgram", sqlcon))
                {
                    sqlcmd.CommandType = CommandType.StoredProcedure;
                    sqlcmd.Parameters.AddWithValue("progName", txtProgName.Text);
                    sqlcmd.Parameters.AddWithValue("typeName", ddl_ProgType.Text);

                    writeMessage(sqlcmd.ExecuteScalar().ToString());

                    //success
                    clearFields();
                    rgPrograms.DataBind();
                }
            }
            catch (Exception ex)
            {
                writeMessage("Please contact your system administrator.");
            }
            finally
            {
                sqlcon.Close();
            }
        }
    }

    protected void CreateNewType_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            try
            {
                sqlcon.Open();
                using (SqlCommand sqlcmd = new SqlCommand("createProgramType", sqlcon))
                {
                    sqlcmd.CommandType = CommandType.StoredProcedure;
                    sqlcmd.Parameters.AddWithValue("typeName", txt_progTypeName.Text);

                    writeMessage(sqlcmd.ExecuteScalar().ToString());

                    //success
                    clearDialog();
                    ddl_ProgType.DataBind();
                }
            }
            catch (Exception ex)
            {
                writeMessage("Please contact your system administrator.");
            }
            finally
            {
                sqlcon.Close();
            }
        }
    }

    protected void btn_createType_Click(object sender, EventArgs e)
    {
        addTypePanel.Visible = true;
    }

    protected void cancelEvent_Click(Object sender, EventArgs e)
    {
        clearDialog();
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        clearFields();
    }

    protected void writeMessage(String msg)
    {
        lbl_message.Text = msg;
    }

    protected void clearDialog()
    {
        txt_progTypeName.Text = "";
        addTypePanel.Visible = false;
    }

    protected void clearFields()
    {
        clearDialog();

        ddl_ProgType.ClearSelection();
        txtProgName.Text = "";
    }

    protected void resetStyles()
    {
        ddl_ProgType.CssClass = ddl_ProgType.CssClass.Replace(" redBorder", "");
        txtProgName.Style.Remove("border-color");
        txt_progTypeName.Style.Remove("border-color");
    }


    protected void checkedChanged(object sender, EventArgs e)
    {
        GridDataItem row = (GridDataItem)(((CheckBox)sender).NamingContainer);
        Label id = (Label)row.FindControl("hidd_ID");
        string progID = id.Text;

        try
        {
            sqlcon.Open();
            using (SqlCommand sqlcmd = new SqlCommand("toggleProgram", sqlcon))
            {
                sqlcmd.CommandType = CommandType.StoredProcedure;
                sqlcmd.Parameters.AddWithValue("@progID", progID);
                sqlcmd.ExecuteNonQuery();

                //success
                rgPrograms.DataBind();
            }
        }
        catch (Exception) {
            writeMessage("Please contact your system administrator.");
        }
        finally {
            sqlcon.Close();
        }
    }

    protected void ddl_ProgType_ServerValidate(object source, ServerValidateEventArgs args)
    {
        args.IsValid = true;

        if (ddl_ProgType.SelectedIndex == -1)
        {
            args.IsValid = false;
            ddl_ProgType.CssClass += " redBorder";
        }
    }

    protected void txtProgName_ServerValidate(object source, ServerValidateEventArgs args)
    {
        args.IsValid = true;

        if (String.IsNullOrEmpty(args.Value)) {
            args.IsValid = false;
            txtProgName.Style.Add("border-color", "Red");
        }
    }

    protected void txtProgNameUnique_ServerValidate(object source, ServerValidateEventArgs args)
    {
        args.IsValid = true;

        String name = txtProgName.Text;
        if (ddl_ProgType.SelectedIndex >= 0 && !String.IsNullOrEmpty(name))
        {
            int type = int.Parse(ddl_ProgType.SelectedValue);
            DataTable dtProgExist = touUtility.dtblfillPrograms(sqlcon, type);
            if (dtProgExist.AsEnumerable().Any(obj => obj["progName"] as String == name))
            {
                args.IsValid = false;
                txtProgName.Style.Add("border-color", "Red");
            }
        }
    }

    protected void txt_progTypeName_ServerValidate(object source, ServerValidateEventArgs args)
    {
        args.IsValid = true;

        if (String.IsNullOrEmpty(args.Value)) {
            args.IsValid = false;
            txt_progTypeName.Style.Add("border-color", "Red");
        }
    }

    protected void rgPrograms_ItemDataBound(object sender, GridItemEventArgs e)
    {
        if (e.Item is GridDataItem)
        {
            GridDataItem item = (GridDataItem)e.Item;
            CheckBox isActive = (CheckBox)item.FindControl("chkActive");


            //if the program is disabled we will gray out the row for the user
            if (isActive != null && !isActive.Checked)
            {
                item.Style.Add("background-color", "#CECECE");
                item.Style.Add("text-shadow", "0 1px 0 #fff");
            }
        }
    }
}
