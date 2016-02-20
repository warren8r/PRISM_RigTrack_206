using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using System.Collections;
using ToU;

public partial class _Default : System.Web.UI.Page
{
    SqlConnection sqlcon;
    public touUtility touUtility = new touUtility();

    protected void Page_Load(object sender, EventArgs e)
    {
        sqlcon = new SqlConnection(Session["client_database"].ToString());

        resetStyles();
        writeMessage("");
    }

    protected void writeMessage(String msg)
    {
        lbl_message_tab1.Text = msg;
    }

    protected void btnCreateSeason_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            btn_save_Click();
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        clearFields();
    }

    protected void RadAjaxPanel1_Load(object sender, EventArgs e)
    {
        RadAjaxManager1.ResponseScripts.Add(string.Format("isEmpty();"));
    }

    protected void checkedChanged(object sender, EventArgs e)
    {
        GridDataItem row = (GridDataItem)(((CheckBox)sender).NamingContainer);
        Label tmpLabel = (Label)row.FindControl("hidd_ID");
        int seasonID = Convert.ToInt32(tmpLabel.Text);
        string output = touUtility.toggleSeason(sqlcon, seasonID);

        rgPrograms_View.DataBind();
        writeMessage(output);
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

    protected void txtProgType_ServerValidate(object source, ServerValidateEventArgs args)
    {
        args.IsValid = true;

        if (args.Value == "") {
            args.IsValid = false;
            txtProgType.CssClass += " redBorder";
        }
    }

    protected void txtProgName_ServerValidate(object source, ServerValidateEventArgs args)
    {
        args.IsValid = true;

        if (args.Value == "") {
            args.IsValid = false;
            txtProgName.CssClass += " redBorder";
        }
    }

    protected void clearFields()
    {
        //clear our input fields
        txtProgType.ClearSelection();
        txtProgName.ClearSelection();
        txtProgName.Enabled = false;

        //clear out season inputs
        txtSeasonName1.Text = "";
        chk_Season_Jan1.Checked = false;
        chk_Season_Feb1.Checked = false;
        chk_Season_Mar1.Checked = false;
        chk_Season_Apr1.Checked = false;
        chk_Season_May1.Checked = false;
        chk_Season_Jun1.Checked = false;
        chk_Season_Jul1.Checked = false;
        chk_Season_Aug1.Checked = false;
        chk_Season_Sep1.Checked = false;
        chk_Season_Oct1.Checked = false;
        chk_Season_Nov1.Checked = false;
        chk_Season_Dec1.Checked = false;
        txtSeasonName2.Text = "";
        chk_Season_Jan2.Checked = false;
        chk_Season_Feb2.Checked = false;
        chk_Season_Mar2.Checked = false;
        chk_Season_Apr2.Checked = false;
        chk_Season_May2.Checked = false;
        chk_Season_Jun2.Checked = false;
        chk_Season_Jul2.Checked = false;
        chk_Season_Aug2.Checked = false;
        chk_Season_Sep2.Checked = false;
        chk_Season_Oct2.Checked = false;
        chk_Season_Nov2.Checked = false;
        chk_Season_Dec2.Checked = false;
        txtSeasonName3.Text = "";
        chk_Season_Jan3.Checked = false;
        chk_Season_Feb3.Checked = false;
        chk_Season_Mar3.Checked = false;
        chk_Season_Apr3.Checked = false;
        chk_Season_May3.Checked = false;
        chk_Season_Jun3.Checked = false;
        chk_Season_Jul3.Checked = false;
        chk_Season_Aug3.Checked = false;
        chk_Season_Sep3.Checked = false;
        chk_Season_Oct3.Checked = false;
        chk_Season_Nov3.Checked = false;
        chk_Season_Dec3.Checked = false;
        txtSeasonName4.Text = "";
        chk_Season_Jan4.Checked = false;
        chk_Season_Feb4.Checked = false;
        chk_Season_Mar4.Checked = false;
        chk_Season_Apr4.Checked = false;
        chk_Season_May4.Checked = false;
        chk_Season_Jun4.Checked = false;
        chk_Season_Jul4.Checked = false;
        chk_Season_Aug4.Checked = false;
        chk_Season_Sep4.Checked = false;
        chk_Season_Oct4.Checked = false;
        chk_Season_Nov4.Checked = false;
        chk_Season_Dec4.Checked = false;

        //rebind our tables
        rgPrograms_View.DataBind();
    }

    protected void resetStyles() {
        txtProgType.CssClass = txtProgType.CssClass.Replace(" redBorder", "");
        txtProgName.CssClass = txtProgName.CssClass.Replace(" redBorder", "");
    }

    protected void txtProgType_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
    {
        txtProgName.ClearSelection();

        if (txtProgType.SelectedIndex >= 0)
            txtProgName.Enabled = true;
        else
            txtProgName.Enabled = false;
    }

    protected void txtProgName_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
    {
        if (txtProgName.SelectedIndex < 1)
            return;
    }

    public void insertSeason(string season_name, string program_id, string season_no, string s1, string s2, string s3, string s4, string s5, 
                             string s6, string s7, string s8, string s9, string s10, string s11, string s12)
    {
        try
        {
            string s = "insert into touSeasonsEP(season_name,program_id,season_no,jan,feb,mar,apr,may,jun,jul,aug,sep,oct,nov,dec) values('" + season_name + "','" + program_id + "'," + season_no + ",'" +
                s1 + "','" + s2 + "','" + s3 + "','" + s4 + "','" + s5 + "','" + s6 + "','" + s7 + "','" + s8 + "','" + s9 + "','" + s10 + "','" + s11 + "','" + s12 + "')";
            SqlCommand sqlcmd = new SqlCommand(s, sqlcon);
            if (sqlcon.State == ConnectionState.Closed)
                sqlcon.Open();
            sqlcmd.ExecuteNonQuery();
            writeMessage("Season Added to '" + txtProgName.SelectedItem.Text + "' Successfully");
        }
        catch (Exception ex)
        {
        }
        finally
        {
            sqlcon.Close();
        }
    }

    public string bindCheck(CheckBox chkbx)
    {
        String status = "";
        if (chkbx.Checked)
        {
            status = "Y";
        }
        else
        {
            status = "N";
        }
        return status;

    }

    protected void txtdefineSeason_ServerValidate(object source, ServerValidateEventArgs args)
    {
        args.IsValid = true;

        int counter = 0;
        if (txtSeasonName1.Text != "")
            counter += 1;
        if (txtSeasonName2.Text != "")
            counter += 1;
        if (txtSeasonName3.Text != "")
            counter += 1;
        if (txtSeasonName4.Text != "")
            counter += 1;

        if (counter == 0)
        {
            args.IsValid = false;
        }
    }

    protected void btn_save_Click()
    {
        if (!String.IsNullOrEmpty(txtSeasonName1.Text))
        {
            if (!chk_Season_Jan1.Checked && !chk_Season_Feb1.Checked && !chk_Season_Mar1.Checked && !chk_Season_Apr1.Checked && !chk_Season_May1.Checked && !chk_Season_Jun1.Checked &&
                !chk_Season_Jul1.Checked && !chk_Season_Aug1.Checked && !chk_Season_Sep1.Checked && !chk_Season_Oct1.Checked && !chk_Season_Nov1.Checked && !chk_Season_Dec1.Checked) {
                writeMessage("You cannot create a season without any months.");
                return;
            }

            if (txtSeasonName1.Text == txtSeasonName2.Text || txtSeasonName1.Text == txtSeasonName3.Text || txtSeasonName1.Text == txtSeasonName4.Text)
            {
                writeMessage("You cannot have duplicate Season names.");
                return;
            }
        }

        if (!String.IsNullOrEmpty(txtSeasonName2.Text))
        {
            if (!chk_Season_Jan2.Checked && !chk_Season_Feb2.Checked && !chk_Season_Mar2.Checked && !chk_Season_Apr2.Checked && !chk_Season_May2.Checked && !chk_Season_Jun2.Checked &&
                !chk_Season_Jul2.Checked && !chk_Season_Aug2.Checked && !chk_Season_Sep2.Checked && !chk_Season_Oct2.Checked && !chk_Season_Nov2.Checked && !chk_Season_Dec2.Checked) {
                writeMessage("You cannot create a season without any months.");
                return;
            }

            if (txtSeasonName2.Text == txtSeasonName1.Text || txtSeasonName2.Text == txtSeasonName3.Text || txtSeasonName2.Text == txtSeasonName4.Text)
            {
                writeMessage("You cannot have duplicate Season names.");
                return;
            }
        }

        if (!String.IsNullOrEmpty(txtSeasonName3.Text))
        {
            if (!chk_Season_Jan3.Checked && !chk_Season_Feb3.Checked && !chk_Season_Mar3.Checked && !chk_Season_Apr3.Checked && !chk_Season_May3.Checked && !chk_Season_Jun3.Checked &&
                !chk_Season_Jul3.Checked && !chk_Season_Aug3.Checked && !chk_Season_Sep3.Checked && !chk_Season_Oct3.Checked && !chk_Season_Nov3.Checked && !chk_Season_Dec3.Checked) {
                writeMessage("You cannot create a season without any months.");
                return;
            }

            if (txtSeasonName3.Text == txtSeasonName1.Text || txtSeasonName3.Text == txtSeasonName2.Text || txtSeasonName3.Text == txtSeasonName4.Text)
            {
                writeMessage("You cannot have duplicate Season names.");
                return;
            }
        }

        if (!String.IsNullOrEmpty(txtSeasonName4.Text))
        {
            if (!chk_Season_Jan4.Checked && !chk_Season_Feb4.Checked && !chk_Season_Mar4.Checked && !chk_Season_Apr4.Checked && !chk_Season_May4.Checked && !chk_Season_Jun4.Checked &&
                !chk_Season_Jul4.Checked && !chk_Season_Aug4.Checked && !chk_Season_Sep4.Checked && !chk_Season_Oct4.Checked && !chk_Season_Nov4.Checked && !chk_Season_Dec4.Checked) {
                writeMessage("You cannot create a season without any months.");
                return;
            }

            if (txtSeasonName4.Text == txtSeasonName1.Text || txtSeasonName4.Text == txtSeasonName2.Text || txtSeasonName4.Text == txtSeasonName3.Text)
            {
                writeMessage("You cannot have duplicate Season names.");
                return;
            }
        }

        string season, jan, feb, mar, apr, may, june, jul, aug, sep, oct, nov, dec;
        if (txtProgName.SelectedIndex >= 0)
        {
            int counter = 0;
            season = ""; jan = ""; feb = ""; mar = ""; apr = ""; may = ""; june = ""; jul = ""; aug = ""; sep = ""; oct = ""; nov = ""; dec = "";

            if (txtSeasonName1.Text != "")
            {
                season = txtSeasonName1.Text;
                jan = bindCheck(chk_Season_Jan1).ToString();
                feb = bindCheck(chk_Season_Feb1).ToString();
                mar = bindCheck(chk_Season_Mar1).ToString();
                apr = bindCheck(chk_Season_Apr1).ToString();
                may = bindCheck(chk_Season_May1).ToString();
                june = bindCheck(chk_Season_Jun1).ToString();
                jul = bindCheck(chk_Season_Jul1).ToString();
                aug = bindCheck(chk_Season_Aug1).ToString();
                sep = bindCheck(chk_Season_Sep1).ToString();
                oct = bindCheck(chk_Season_Oct1).ToString();
                nov = bindCheck(chk_Season_Nov1).ToString();
                dec = bindCheck(chk_Season_Dec1).ToString();
                insertSeason(season, txtProgName.SelectedValue, "1", jan, feb, mar, apr, may, june, jul, aug, sep, oct, nov, dec);
                counter+=1;
            }

            season = ""; jan = ""; feb = ""; mar = ""; apr = ""; may = ""; june = ""; jul = ""; aug = ""; sep = ""; oct = ""; nov = ""; dec = "";

            if (txtSeasonName2.Text != "")
            {
                season = txtSeasonName2.Text;
                jan = bindCheck(chk_Season_Jan2).ToString();
                feb = bindCheck(chk_Season_Feb2).ToString();
                mar = bindCheck(chk_Season_Mar2).ToString();
                apr = bindCheck(chk_Season_Apr2).ToString();
                may = bindCheck(chk_Season_May2).ToString();
                june = bindCheck(chk_Season_Jun2).ToString();
                jul = bindCheck(chk_Season_Jul2).ToString();
                aug = bindCheck(chk_Season_Aug2).ToString();
                sep = bindCheck(chk_Season_Sep2).ToString();
                oct = bindCheck(chk_Season_Oct2).ToString();
                nov = bindCheck(chk_Season_Nov2).ToString();
                dec = bindCheck(chk_Season_Dec2).ToString();
                insertSeason(season, txtProgName.SelectedValue, "2", jan, feb, mar, apr, may, june, jul, aug, sep, oct, nov, dec);
                counter += 1;
            }

            season = ""; jan = ""; feb = ""; mar = ""; apr = ""; may = ""; june = ""; jul = ""; aug = ""; sep = ""; oct = ""; nov = ""; dec = "";

            if (txtSeasonName3.Text != "")
            {
                season = txtSeasonName3.Text;
                jan = bindCheck(chk_Season_Jan3).ToString();
                feb = bindCheck(chk_Season_Feb3).ToString();
                mar = bindCheck(chk_Season_Mar3).ToString();
                apr = bindCheck(chk_Season_Apr3).ToString();
                may = bindCheck(chk_Season_May3).ToString();
                june = bindCheck(chk_Season_Jun3).ToString();
                jul = bindCheck(chk_Season_Jul3).ToString();
                aug = bindCheck(chk_Season_Aug3).ToString();
                sep = bindCheck(chk_Season_Sep3).ToString();
                oct = bindCheck(chk_Season_Oct3).ToString();
                nov = bindCheck(chk_Season_Nov3).ToString();
                dec = bindCheck(chk_Season_Dec3).ToString();
                insertSeason(season, txtProgName.SelectedValue, "3", jan, feb, mar, apr, may, june, jul, aug, sep, oct, nov, dec);
                counter += 1;
            }

            season = ""; jan = ""; feb = ""; mar = ""; apr = ""; may = ""; june = ""; jul = ""; aug = ""; sep = ""; oct = ""; nov = ""; dec = "";

            if (txtSeasonName4.Text != "")
            {
                season = txtSeasonName4.Text;
                jan = bindCheck(chk_Season_Jan4).ToString();
                feb = bindCheck(chk_Season_Feb4).ToString();
                mar = bindCheck(chk_Season_Mar4).ToString();
                apr = bindCheck(chk_Season_Apr4).ToString();
                may = bindCheck(chk_Season_May4).ToString();
                june = bindCheck(chk_Season_Jun4).ToString();
                jul = bindCheck(chk_Season_Jul4).ToString();
                aug = bindCheck(chk_Season_Aug4).ToString();
                sep = bindCheck(chk_Season_Sep4).ToString();
                oct = bindCheck(chk_Season_Oct4).ToString();
                nov = bindCheck(chk_Season_Nov4).ToString();
                dec = bindCheck(chk_Season_Dec4).ToString();
                insertSeason(season, txtProgName.SelectedValue, "4", jan, feb, mar, apr, may, june, jul, aug, sep, oct, nov, dec);
                counter += 1;
            }
            if (counter > 0)
            {
                writeMessage(counter + " Season Definitions Added Successfully");
                clearFields();
            }
            else
                writeMessage("You must define a season to continue.");
        }
    }
    protected void chk_Season_CheckedChanged(object sender, EventArgs e)
    {
        CheckBox chkbx = sender as CheckBox;
        if (chkbx.Checked)
        {
            if (chkbx.ID.Contains("Jan"))
            {
                chk_Season_Jan1.Checked = false;
                chk_Season_Jan2.Checked = false;
                chk_Season_Jan3.Checked = false;
                chk_Season_Jan4.Checked = false;
            }
            if (chkbx.ID.Contains("Feb"))
            {
                chk_Season_Feb1.Checked = false;
                chk_Season_Feb2.Checked = false;
                chk_Season_Feb3.Checked = false;
                chk_Season_Feb4.Checked = false;
            }
            if (chkbx.ID.Contains("Mar"))
            {
                chk_Season_Mar1.Checked = false;
                chk_Season_Mar2.Checked = false;
                chk_Season_Mar3.Checked = false;
                chk_Season_Mar4.Checked = false;
            }
            if (chkbx.ID.Contains("Apr"))
            {
                chk_Season_Apr1.Checked = false;
                chk_Season_Apr2.Checked = false;
                chk_Season_Apr3.Checked = false;
                chk_Season_Apr4.Checked = false;
            }
            if (chkbx.ID.Contains("May"))
            {
                chk_Season_May1.Checked = false;
                chk_Season_May2.Checked = false;
                chk_Season_May3.Checked = false;
                chk_Season_May4.Checked = false;
            }
            if (chkbx.ID.Contains("Jun"))
            {
                chk_Season_Jun1.Checked = false;
                chk_Season_Jun2.Checked = false;
                chk_Season_Jun3.Checked = false;
                chk_Season_Jun4.Checked = false;
            }
            if (chkbx.ID.Contains("Jul"))
            {
                chk_Season_Jul1.Checked = false;
                chk_Season_Jul2.Checked = false;
                chk_Season_Jul3.Checked = false;
                chk_Season_Jul4.Checked = false;
            }
            if (chkbx.ID.Contains("Aug"))
            {
                chk_Season_Aug1.Checked = false;
                chk_Season_Aug2.Checked = false;
                chk_Season_Aug3.Checked = false;
                chk_Season_Aug4.Checked = false;
            }
            if (chkbx.ID.Contains("Sep"))
            {
                chk_Season_Sep1.Checked = false;
                chk_Season_Sep2.Checked = false;
                chk_Season_Sep3.Checked = false;
                chk_Season_Sep4.Checked = false;
            }
            if (chkbx.ID.Contains("Oct"))
            {
                chk_Season_Oct1.Checked = false;
                chk_Season_Oct2.Checked = false;
                chk_Season_Oct3.Checked = false;
                chk_Season_Oct4.Checked = false;
            }
            if (chkbx.ID.Contains("Nov"))
            {
                chk_Season_Nov1.Checked = false;
                chk_Season_Nov2.Checked = false;
                chk_Season_Nov3.Checked = false;
                chk_Season_Nov4.Checked = false;
            }
            if (chkbx.ID.Contains("Dec"))
            {
                chk_Season_Dec1.Checked = false;
                chk_Season_Dec2.Checked = false;
                chk_Season_Dec3.Checked = false;
                chk_Season_Dec4.Checked = false;
            }

            chkbx.Checked = true;
        }
    }
}
