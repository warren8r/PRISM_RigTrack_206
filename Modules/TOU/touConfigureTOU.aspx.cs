using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using Telerik.Web.UI;
using ToU;

public partial class Modules_TOU_touConfigureTOU : System.Web.UI.Page
{
    SqlConnection sqlcon;
    touUtility touUtility = new touUtility();

    protected void Page_Load(object sender, EventArgs e)
    {
        sqlcon = new SqlConnection(Session["client_database"].ToString());

        resetStyles();
        writeMessage("");
        //rg_tax.DataSource = new object[] { };
        checkboxCheck();
    }

    protected void writeMessage(String msg)
    {
        lbl_message_tab1.Text = msg;
        lbl_message_tab2.Text = msg;
    }

    protected void clearFields()
    {
        //clear our input fields
        txt_fuelAdjCode.Text = "";
        txt_fuelAdjCharge.Text = "";
        txt_taxcode.Text = "";
        txt_desc.Text = "";
        txt_countrytax.Text = "";
        txt_citytax.Text = "";
        txt_othertax.Text = "";
        txt_statetax.Text = "";
        txt_localtax.Text = "";

        //clear checks
        chk_countrytax.Checked = false;
        chk_citytax.Checked = false;
        chk_othertax.Checked = false;
        chk_statetax.Checked = false;
        chk_localtax.Checked = false;

        //clear our datefields
        rdp_effectiveDate.Clear();
        rdp_effectiveDate2.Clear();

        //rebind our tables
        rg_fuel.DataBind();
        rg_tax.DataBind();
    }

    protected void txt_fuelAdjCode_ServerValidate(object source, ServerValidateEventArgs args)
    {
        args.IsValid = true;

        if (args.Value == "")
        {
            args.IsValid = false;
            txt_fuelAdjCode.Style.Add("border-color", "Red");
        }
    }

    protected void txt_fuelAdjCharge_ServerValidate(object source, ServerValidateEventArgs args)
    {
        args.IsValid = true;

        if (args.Value == "")
        {
            args.IsValid = false;
            txt_fuelAdjCharge.Style.Add("border-color", "Red");
        }
    }

    protected void rdp_effectiveDate_ServerValidate(object source, ServerValidateEventArgs args)
    {
        if (RadMultiPage1.SelectedIndex != 0)
            return;

        if (rdp_effectiveDate.IsEmpty)
        {
            args.IsValid = false;
            rdp_effectiveDate.DateInput.Style.Add("border-color", "Red");
        }
    }

    protected void resetStyles()
    {
        txt_fuelAdjCode.Style.Remove("border-color");
        txt_fuelAdjCharge.Style.Remove("border-color");
        rdp_effectiveDate.DateInput.Style.Remove("border-color");
        txt_taxcode.Style.Remove("border-color");
        txt_desc.Style.Remove("border-color");
        rdp_effectiveDate2.DateInput.Style.Remove("border-color");
        txt_countrytax.Style.Remove("border-color");
        txt_citytax.Style.Remove("border-color");
        txt_othertax.Style.Remove("border-color");
        txt_statetax.Style.Remove("border-color");
        txt_localtax.Style.Remove("border-color");
    }
    protected void btnCreate_tab1_Click(object sender, EventArgs e)
    {
        if (IsValid)
        {
            writeMessage(createFuelAdjustment(txt_fuelAdjCode.Text, txt_fuelAdjCharge.Text, (DateTime)rdp_effectiveDate.SelectedDate));
            clearFields();
        }
    }
    protected void btn_clear_tab1_Click(object sender, EventArgs e)
    {
        clearFields();
    }

    protected void btnCreate_tab2_Click(object sender, EventArgs e)
    {
        if (IsValid)
        {
            double taxCity = 0.0;
            double taxLocal = 0.0;
            double taxCountry = 0.0;
            double taxState = 0.0;
            double taxOther = 0.0;

            try {
                taxCity = double.Parse(txt_citytax.Text);
            }
            catch (Exception) { } 
            try {
                taxLocal = double.Parse(txt_localtax.Text);
            }
            catch (Exception) { }
            try {
                taxCountry = double.Parse(txt_countrytax.Text);
            }
            catch (Exception) { }
            try {
                taxState = double.Parse(txt_statetax.Text);
            }
            catch (Exception) { }
            try {
                taxOther = double.Parse(txt_othertax.Text);
            }
            catch (Exception) { }

            writeMessage(createTaxAdjustment(txt_taxcode.Text, txt_desc.Text, taxCity, taxLocal, taxCountry, taxState, taxOther, (DateTime)rdp_effectiveDate2.SelectedDate));
            
            clearFields();
        }
    }
    protected void btn_clear_tab2_Click(object sender, EventArgs e)
    {
        clearFields();
    }

    protected string createTaxAdjustment(string taxCode, string taxDescription, double taxCity, double taxLocal, double taxCountry, double taxState, double taxOther, DateTime effectiveDate)
    {
        try
        {
            sqlcon.Open();
            using (SqlCommand sqlcmd = new SqlCommand("INSERT INTO touTaxAdjust (taxCode, taxDescription, taxCity, taxLocal, taxCountry, taxState, taxOther, effectiveDate, isActive) VALUES (@taxCode, @taxDescription, @taxCity, @taxLocal, @taxCountry, @taxState, @taxOther, @effectiveDate, @isActive)", sqlcon))
            {
                sqlcmd.Parameters.AddWithValue("@taxCode", taxCode);
                sqlcmd.Parameters.AddWithValue("@taxDescription", taxDescription);
                sqlcmd.Parameters.AddWithValue("@taxCity", taxCity);
                sqlcmd.Parameters.AddWithValue("@taxLocal", taxLocal);
                sqlcmd.Parameters.AddWithValue("@taxCountry", taxCity);
                sqlcmd.Parameters.AddWithValue("@taxState", taxState);
                sqlcmd.Parameters.AddWithValue("@taxOther", taxOther);
                sqlcmd.Parameters.AddWithValue("@effectiveDate", effectiveDate);
                sqlcmd.Parameters.AddWithValue("@isActive", true);
                sqlcmd.ExecuteNonQuery();
            }

            return ("You have successfully created the fuel adjustment.");
        }
        catch (SqlException sqlex)
        {
            if (sqlex.Number == 2627)
                return ("A tax adjustment with the code " + taxCode + " already exists.");
            else
                return ("Please contact your system administrator.");
        }
        catch (Exception ex)
        {
            return ("Please contact your system administrator.");
        }
        finally
        {
            sqlcon.Close();
        }
    }

    protected string createFuelAdjustment(string fuelCode, string fuelCharge, DateTime effectiveDate)
    {
        try
        {
            sqlcon.Open();
            using (SqlCommand sqlcmd = new SqlCommand("INSERT INTO touFuelAdjust (fuelCode, fuelCharge, effectiveDate, isActive) VALUES (@fuelCode, @fuelCharge, @effectiveDate, @isActive)", sqlcon))
            {
                sqlcmd.Parameters.AddWithValue("@fuelCode", fuelCode);
                sqlcmd.Parameters.AddWithValue("@fuelCharge", fuelCharge);
                sqlcmd.Parameters.AddWithValue("@effectiveDate", effectiveDate);
                sqlcmd.Parameters.AddWithValue("@isActive", true);
                sqlcmd.ExecuteNonQuery();
            }

            return ("You have successfully created the fuel adjustment.");
        }
        catch (SqlException sqlex)
        {
            if (sqlex.Number == 2627)
                return ("A fuel adjustment with the code " + fuelCode + " already exists.");
            else
                return ("Please contact your system administrator.");
        }
        catch (Exception ex)
        {
            return ("Please contact your system administrator.");
        }
        finally
        {
            sqlcon.Close();
        }
    }

    protected void checkedChanged(object sender, EventArgs e)
    {
        GridDataItem row = (GridDataItem)(((CheckBox)sender).NamingContainer);
        Label tmpLabel = (Label)row.FindControl("hidd_ID");
        int fuelID = Convert.ToInt32(tmpLabel.Text);
        string output = touUtility.toggleFuelAdjust(sqlcon, fuelID);

        rg_fuel.DataBind();
        writeMessage(output);
    }

    protected void checkedChangedTax(object sender, EventArgs e)
    {
        GridDataItem row = (GridDataItem)(((CheckBox)sender).NamingContainer);
        Label tmpLabel = (Label)row.FindControl("hidd_ID");
        int taxID = Convert.ToInt32(tmpLabel.Text);
        string output = touUtility.toggleTaxAdjust(sqlcon, taxID);

        rg_tax.DataBind();
        writeMessage(output);
    }

    protected void rg_fuel_ItemDataBound(object sender, GridItemEventArgs e)
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
    protected void rg_tax_ItemDataBound(object sender, GridItemEventArgs e)
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
    protected void txt_taxcode_ServerValidate(object source, ServerValidateEventArgs args)
    {
        args.IsValid = true;

        if (args.Value == "")
        {
            args.IsValid = false;
            txt_taxcode.Style.Add("border-color", "Red");
        }
    }
    protected void txt_desc_ServerValidate(object source, ServerValidateEventArgs args)
    {
        args.IsValid = true;

        if (args.Value == "")
        {
            args.IsValid = false;
            txt_desc.Style.Add("border-color", "Red");
        }
    }
    protected void rdp_effectiveDate2_ServerValidate(object source, ServerValidateEventArgs args)
    {
        args.IsValid = true;

        if (args.Value == "")
        {
            args.IsValid = false;
            rdp_effectiveDate2.DateInput.Style.Add("border-color", "Red");
        }
    }
    protected void txt_countrytax_ServerValidate(object source, ServerValidateEventArgs args)
    {
        args.IsValid = true;


        if (chk_countrytax.Checked && args.Value == "")
        {
            args.IsValid = false;
            txt_countrytax.Style.Add("border-color", "Red");
        }
    }
    protected void txt_citytax_ServerValidate(object source, ServerValidateEventArgs args)
    {
        args.IsValid = true;

        if (chk_citytax.Checked && args.Value == "")
        {
            args.IsValid = false;
            txt_citytax.Style.Add("border-color", "Red");
        }
    }
    protected void txt_othertax_ServerValidate(object source, ServerValidateEventArgs args)
    {
        args.IsValid = true;

        if (chk_othertax.Checked && args.Value == "")
        {
            args.IsValid = false;
            txt_othertax.Style.Add("border-color", "Red");
        }
    }
    protected void txt_statetax_ServerValidate(object source, ServerValidateEventArgs args)
    {
        args.IsValid = true;

        if (chk_statetax.Checked && args.Value == "")
        {
            args.IsValid = false;
            txt_statetax.Style.Add("border-color", "Red");
        }
    }
    protected void txt_localtax_ServerValidate(object source, ServerValidateEventArgs args)
    {
        args.IsValid = true;

        if (chk_localtax.Checked && args.Value == "")
        {
            args.IsValid = false;
            txt_localtax.Style.Add("border-color", "Red");
        }
    }

    protected void checkboxCheck()
    {
        if (chk_countrytax.Checked)
            txt_countrytax.Enabled = true;
        else
            txt_countrytax.Enabled = false;

        if (chk_citytax.Checked)
            txt_citytax.Enabled = true;
        else
            txt_citytax.Enabled = false;

        if (chk_othertax.Checked)
            txt_othertax.Enabled = true;
        else
            txt_othertax.Enabled = false;

        if (chk_statetax.Checked)
            txt_statetax.Enabled = true;
        else
            txt_statetax.Enabled = false;

        if (chk_localtax.Checked)
            txt_localtax.Enabled = true;
        else
            txt_localtax.Enabled = false;
    }
}