using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using ToU;

public partial class Modules_TOU_touManageDayTypes : System.Web.UI.Page
{
    SqlConnection sqlcon;
    touUtility touUtility = new touUtility();

    protected void Page_Load(object sender, EventArgs e)
    {
        sqlcon = new SqlConnection(Session["client_database"].ToString());

        resetStyles();
        writeMessage("");
    }

    protected void writeMessage(String msg)
    {
        lbl_message.Text = msg;
    }

    protected void btn_accept_Click(object sender, EventArgs e)
    {
        if (IsValid)
        {
            switch (rbl_defineType.SelectedIndex)
            {
                case 0:
                    {
                        writeMessage(createDayDefinition_Days(txtDayDefName.Text, cbl_DaysofWeek.Items[0].Selected, cbl_DaysofWeek.Items[1].Selected, cbl_DaysofWeek.Items[2].Selected, cbl_DaysofWeek.Items[3].Selected, cbl_DaysofWeek.Items[4].Selected, cbl_DaysofWeek.Items[5].Selected, cbl_DaysofWeek.Items[6].Selected));
                    } break;
                case 1:
                    {
                        writeMessage(createDayDefinition_DateRange(txtDayDefName.Text, (DateTime)rdp_start.SelectedDate, (DateTime)rdp_end.SelectedDate));
                    } break;
                case 2:
                    {
                        writeMessage(createDayDefinition_Date(txtDayDefName.Text, (DateTime)rdp_specifydate.SelectedDate));
                    } break;
            }
            clearFields();
        }
    }

    protected void btn_clear_Click(object sender, EventArgs e)
    {
        clearFields();
    }

    protected string createDayDefinition_Days(string name, bool mon, bool tue, bool wed, bool thurs, bool fri, bool sat, bool sun)
    {
        try
        {
            sqlcon.Open();
            using (SqlCommand sqlcmd = new SqlCommand("INSERT INTO touDayDefinitions (type, name, mon, tue, wed, thurs, fri, sat, sun) VALUES (@type, @name, @mon, @tue, @wed, @thurs, @fri, @sat, @sun)", sqlcon))
            {
                sqlcmd.Parameters.AddWithValue("@type", "days");
                sqlcmd.Parameters.AddWithValue("@name", name);
                sqlcmd.Parameters.AddWithValue("@mon", mon);
                sqlcmd.Parameters.AddWithValue("@tue", tue);
                sqlcmd.Parameters.AddWithValue("@wed", wed);
                sqlcmd.Parameters.AddWithValue("@thurs", thurs);
                sqlcmd.Parameters.AddWithValue("@fri", fri);
                sqlcmd.Parameters.AddWithValue("@sat", sat);
                sqlcmd.Parameters.AddWithValue("@sun", sun);
                sqlcmd.ExecuteNonQuery();
            }

            return ("You have successfully created the definition " + name + ".");
        }
        catch (SqlException sqlex)
        {
            if (sqlex.Number == 2627)
                return ("A definition with the name " + name + " already exists.");
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

    protected string createDayDefinition_DateRange(string name, DateTime startDate, DateTime stopDate)
    {
        try
        {
            sqlcon.Open();
            using (SqlCommand sqlcmd = new SqlCommand("INSERT INTO touDayDefinitions (type, name, startDate, stopDate) VALUES (@type, @name, @startDate, @stopDate)", sqlcon))
            {
                sqlcmd.Parameters.AddWithValue("@type", "daterange");
                sqlcmd.Parameters.AddWithValue("@name", name);
                sqlcmd.Parameters.AddWithValue("@startDate", startDate);
                sqlcmd.Parameters.AddWithValue("@stopDate", stopDate);
                sqlcmd.ExecuteNonQuery();
            }

            return ("You have successfully created the definition " + name + ".");
        }
        catch (SqlException sqlex)
        {
            if (sqlex.Number == 2627)
                return ("A definition with the name " + name + " already exists.");
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

    protected string createDayDefinition_Date(string name, DateTime date)
    {
        try
        {
            sqlcon.Open();
            using (SqlCommand sqlcmd = new SqlCommand("INSERT INTO touDayDefinitions (type, name, Date) VALUES (@type, @name, @date)", sqlcon))
            {
                sqlcmd.Parameters.AddWithValue("@type", "date");
                sqlcmd.Parameters.AddWithValue("@name", name);
                sqlcmd.Parameters.AddWithValue("@date", date);
                sqlcmd.ExecuteNonQuery();
            }

            return ("You have successfully created the definition " + name + ".");
        }
        catch (SqlException sqlex)
        {
            if (sqlex.Number == 2627)
                return ("A definition with the name " + name + " already exists.");
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

    protected void rbl_defineType_SelectedIndexChanged(object sender, EventArgs e)
    {
        int selectedIndex = Convert.ToInt32(rbl_defineType.SelectedValue);
        rmp_definiteType.SelectedIndex = selectedIndex;
    }

    //protected void txtProgType_TextChanged(object sender, EventArgs e)
    //{
    //    txtProgName.Text = "";

    //    if (txtProgType.Text != "Autocomplete...")
    //        txtProgName.Enabled = true;
    //    else
    //        txtProgName.Enabled = false;
    //}

    //protected void txtProgName_TextChanged(object sender, EventArgs e)
    //{
    //    if (txtProgName.Text == "Autocomplete..." || txtProgName.Text == "")
    //        return;

    //    populateSeasonList(touUtility.retrieveProgamID(sqlcon, txtProgName.Text));
    //}

    protected void populateSeasonList(int programID)
    {
        /*
        DataTable dt_assigned = touUtility.dtblfillAssignedSeasons(sqlcon, programID);
        DataTable dt_allSeasons = touUtility.dtblfillSeasons(sqlcon);

        ddl_selectSeason.Items.Clear();
        foreach (DataRow dr in dt_allSeasons.Rows)
        {
            foreach (DataRow dr_ass in dt_assigned.Rows)
            {
                if(dr_ass["seasonID"].ToString() == dr["ID"].ToString())
                {
                    Telerik.Web.UI.DropDownListItem li = new Telerik.Web.UI.DropDownListItem();
                    li.Text = dr["seasonName"].ToString();
                    li.Value = dr["ID"].ToString();

                    ddl_selectSeason.Items.Add(li);
                }
            }
        }

        if (ddl_selectSeason.Items.Count < 1)
            ddl_selectSeason.Items.Add(new Telerik.Web.UI.DropDownListItem("N/A", "0"));
        */
    }

    //protected void ddl_selectSeason_ServerValidate(object source, ServerValidateEventArgs args)
    //{
    //    args.IsValid = true;

    //    if (ddl_selectSeason.SelectedIndex < 0)
    //    {
    //        args.IsValid = false;
    //        div_selectSeason.Style.Add("border-color", "Red");
    //        div_selectSeason.Style.Add("border-width", "thin");
    //        div_selectSeason.Style.Add("border-style", "solid");
    //    }
    //}

    protected void txtDayDefName_ServerValidate(object source, ServerValidateEventArgs args)
    {
        args.IsValid = true;

        if (args.Value == "")
        {
            args.IsValid = false;
            txtDayDefName.Style.Add("border-color", "Red");
        }
    }

    protected void cbl_DaysofWeek_ServerValidate(object source, ServerValidateEventArgs args)
    {
        if (rbl_defineType.SelectedIndex != 0)
            return;

        args.IsValid = false;
        CheckBoxList cbl = cbl_DaysofWeek;
        foreach (ListItem cb in cbl.Items)
        {
            if (cb.Selected)
                args.IsValid = true;
        }

        if (!args.IsValid)
        {
            RadPageView1.Style.Add("border-color", "Red");
            RadPageView1.Style.Add("border-width", "thin");
            RadPageView1.Style.Add("border-style", "solid");
        }
    }

    protected void rdp_DateRange_ServerValidate(object source, ServerValidateEventArgs args)
    {
        if (rbl_defineType.SelectedIndex != 1)
            return;


        if (rdp_start.IsEmpty || rdp_end.IsEmpty)
        {
            args.IsValid = false;
            rdp_start.DateInput.Style.Add("border-color", "Red");
            rdp_end.DateInput.Style.Add("border-color", "Red");
        }
    }

    protected void rdp_SelectDate_ServerValidate(object source, ServerValidateEventArgs args)
    {
        if (rbl_defineType.SelectedIndex != 2)
            return;


        if (rdp_specifydate.IsEmpty)
        {
            args.IsValid = false;
            rdp_specifydate.DateInput.Style.Add("border-color", "Red");
        }
    }

    protected void clearFields()
    {
        //clear our input fields
        txtDayDefName.Text = "";

        //clear our dropdownlist
        //ddl_selectSeason.ClearSelection();

        //clear our datefields
        rdp_start.Clear();
        rdp_end.Clear();
        rdp_specifydate.Clear();

        //clear our selection
        rbl_defineType.SelectedIndex = 0;
        rmp_definiteType.SelectedIndex = 0;

        foreach (ListItem li in cbl_DaysofWeek.Items)
            li.Selected = false;

        //rebind our tables
        rgPrograms_View.DataBind();
    }

    protected void resetStyles()
    {
        //ddl_selectSeason.Style.Remove("border-color");
        txtDayDefName.Style.Remove("border-color");
        RadPageView1.Style.Remove("border-color");
        RadPageView1.Style.Remove("border-width");
        RadPageView1.Style.Remove("border-style");
        //div_selectSeason.Style.Remove("border-color");
        //div_selectSeason.Style.Remove("border-width");
        //div_selectSeason.Style.Remove("border-style");
        rdp_start.DateInput.Style.Remove("border-color");
        rdp_end.DateInput.Style.Remove("border-color");
        rdp_specifydate.DateInput.Style.Remove("border-color");
    }
}