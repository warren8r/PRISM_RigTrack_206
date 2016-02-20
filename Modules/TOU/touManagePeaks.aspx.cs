using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using System.Data;
using System.Data.SqlClient;
using ToU;
using System.Collections;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using ASP;

public partial class Modules_TOU_touManagePeaks : System.Web.UI.Page
{
    SqlConnection sqlcon;
    touUtility touUtility = new touUtility();
    List<Table> tables = new List<Table>();
    
    string starthr1 = "", stophr1 = "", starthr2 = "", stophr2 = "", starthr3 = "", stophr3 = "", starthr4 = "", stophr4 = "", starthr5 = "", stophr5 = "";
    string week_day_text1 = "", week_day_text2 = "", week_day_text3 = "", week_day_text4 = "", week_day_text5 = "";
    string ddl_text1 = "", ddl_text2 = "", ddl_text3 = "", ddl_text4 = "", ddl_text5 = "";

    int doopweekday = 0, doopholiday = 0, doopsaturday = 0, doopsunday = 0;
    int start_hr1 = 0, stop_hr1 = 0, start_hr2 = 0, stop_hr2 = 0, start_hr3 = 0, stop_hr3 = 0, start_hr4 = 0, stop_hr4 = 0, start_hr5 = 0, stop_hr5 = 0;

    Hashtable seasons = new Hashtable();
    Hashtable season_months = new Hashtable();
    Hashtable peaks = new Hashtable();

    private DataSet getdataset()
    {
        String Select_peaktype = "select id as ET_PEAKTYPE_ID,peakType as ET_PEAKTYPE from touPeakTypes";
        SqlDataAdapter da = new SqlDataAdapter(Select_peaktype, sqlcon);
        DataSet ds = new DataSet();
        da.Fill(ds, "Peaktypes");
        return ds;
    }

    protected void gv_peaks_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        DataTable mytable = new DataTable();
        DataColumn productidcolumn = new DataColumn("ET_PEAKTYPE_ID");
        DataColumn productnamecolumn = new DataColumn("ET_PEAKTYPE");

        mytable.Columns.Add(productidcolumn);
        mytable.Columns.Add(productnamecolumn);

        DataSet ds = new DataSet();
        ds = getdataset();
        int categoryid = 0;
        string expression = string.Empty;

        if (e.Row.RowType == DataControlRowType.DataRow)
        {

            DropDownList ddl_weekdays1 = (DropDownList)e.Row.FindControl("ddl_weekdays1");
            DropDownList ddl_weekdays2 = (DropDownList)e.Row.FindControl("ddl_weekdays2");
            DropDownList ddl_weekdays3 = (DropDownList)e.Row.FindControl("ddl_weekdays3");
            DropDownList ddl_weekdays4 = (DropDownList)e.Row.FindControl("ddl_weekdays4");
            DropDownList ddl_weekdays5 = (DropDownList)e.Row.FindControl("ddl_weekdays5");

            DropDownList ddl_holiday1 = (DropDownList)e.Row.FindControl("ddl_holiday1");
            DropDownList ddl_holiday2 = (DropDownList)e.Row.FindControl("ddl_holiday2");
            DropDownList ddl_holiday3 = (DropDownList)e.Row.FindControl("ddl_holiday3");
            DropDownList ddl_holiday4 = (DropDownList)e.Row.FindControl("ddl_holiday4");
            DropDownList ddl_holiday5 = (DropDownList)e.Row.FindControl("ddl_holiday5");

            DropDownList ddl_saturday1 = (DropDownList)e.Row.FindControl("ddl_saturday1");
            DropDownList ddl_saturday2 = (DropDownList)e.Row.FindControl("ddl_saturday2");
            DropDownList ddl_saturday3 = (DropDownList)e.Row.FindControl("ddl_saturday3");
            DropDownList ddl_saturday4 = (DropDownList)e.Row.FindControl("ddl_saturday4");
            DropDownList ddl_saturday5 = (DropDownList)e.Row.FindControl("ddl_saturday5");

            DropDownList ddl_sunday1 = (DropDownList)e.Row.FindControl("ddl_sunday1");
            DropDownList ddl_sunday2 = (DropDownList)e.Row.FindControl("ddl_sunday2");
            DropDownList ddl_sunday3 = (DropDownList)e.Row.FindControl("ddl_sunday3");
            DropDownList ddl_sunday4 = (DropDownList)e.Row.FindControl("ddl_sunday4");
            DropDownList ddl_sunday5 = (DropDownList)e.Row.FindControl("ddl_sunday5");

            DataRow[] rows = ds.Tables[0].Select(expression);

            foreach (DataRow row in rows)
            {
                DataRow newrow = mytable.NewRow();
                newrow["ET_PEAKTYPE_ID"] = row["ET_PEAKTYPE_ID"];
                newrow["ET_PEAKTYPE"] = row["ET_PEAKTYPE"];
                mytable.Rows.Add(newrow);
            }
            ddl_weekdays1.DataSource = mytable;
            ddl_weekdays1.DataTextField = "ET_PEAKTYPE";
            ddl_weekdays1.DataValueField = "ET_PEAKTYPE_ID";
            ddl_weekdays1.DataBind();
            ddl_weekdays1.Items.Insert(0, "Select");

            ddl_weekdays2.DataSource = mytable;
            ddl_weekdays2.DataTextField = "ET_PEAKTYPE";
            ddl_weekdays2.DataValueField = "ET_PEAKTYPE_ID";
            ddl_weekdays2.DataBind();
            ddl_weekdays2.Items.Insert(0, "Select");

            ddl_weekdays3.DataSource = mytable;
            ddl_weekdays3.DataTextField = "ET_PEAKTYPE";
            ddl_weekdays3.DataValueField = "ET_PEAKTYPE_ID";
            ddl_weekdays3.DataBind();
            ddl_weekdays3.Items.Insert(0, "Select");

            ddl_weekdays4.DataSource = mytable;
            ddl_weekdays4.DataTextField = "ET_PEAKTYPE";
            ddl_weekdays4.DataValueField = "ET_PEAKTYPE_ID";
            ddl_weekdays4.DataBind();
            ddl_weekdays4.Items.Insert(0, "Select");

            ddl_weekdays5.DataSource = mytable;
            ddl_weekdays5.DataTextField = "ET_PEAKTYPE";
            ddl_weekdays5.DataValueField = "ET_PEAKTYPE_ID";
            ddl_weekdays5.DataBind();
            ddl_weekdays5.Items.Insert(0, "Select");

            ddl_holiday1.DataSource = mytable;
            ddl_holiday1.DataTextField = "ET_PEAKTYPE";
            ddl_holiday1.DataValueField = "ET_PEAKTYPE_ID";
            ddl_holiday1.DataBind();
            ddl_holiday1.Items.Insert(0, "Select");

            ddl_holiday2.DataSource = mytable;
            ddl_holiday2.DataTextField = "ET_PEAKTYPE";
            ddl_holiday2.DataValueField = "ET_PEAKTYPE_ID";
            ddl_holiday2.DataBind();
            ddl_holiday2.Items.Insert(0, "Select");

            ddl_holiday3.DataSource = mytable;
            ddl_holiday3.DataTextField = "ET_PEAKTYPE";
            ddl_holiday3.DataValueField = "ET_PEAKTYPE_ID";
            ddl_holiday3.DataBind();
            ddl_holiday3.Items.Insert(0, "Select");

            ddl_holiday4.DataSource = mytable;
            ddl_holiday4.DataTextField = "ET_PEAKTYPE";
            ddl_holiday4.DataValueField = "ET_PEAKTYPE_ID";
            ddl_holiday4.DataBind();
            ddl_holiday4.Items.Insert(0, "Select");

            ddl_holiday5.DataSource = mytable;
            ddl_holiday5.DataTextField = "ET_PEAKTYPE";
            ddl_holiday5.DataValueField = "ET_PEAKTYPE_ID";
            ddl_holiday5.DataBind();
            ddl_holiday5.Items.Insert(0, "Select");

            ddl_saturday1.DataSource = mytable;
            ddl_saturday1.DataTextField = "ET_PEAKTYPE";
            ddl_saturday1.DataValueField = "ET_PEAKTYPE_ID";
            ddl_saturday1.DataBind();
            ddl_saturday1.Items.Insert(0, "Select");

            ddl_saturday2.DataSource = mytable;
            ddl_saturday2.DataTextField = "ET_PEAKTYPE";
            ddl_saturday2.DataValueField = "ET_PEAKTYPE_ID";
            ddl_saturday2.DataBind();
            ddl_saturday2.Items.Insert(0, "Select");

            ddl_saturday3.DataSource = mytable;
            ddl_saturday3.DataTextField = "ET_PEAKTYPE";
            ddl_saturday3.DataValueField = "ET_PEAKTYPE_ID";
            ddl_saturday3.DataBind();
            ddl_saturday3.Items.Insert(0, "Select");

            ddl_saturday4.DataSource = mytable;
            ddl_saturday4.DataTextField = "ET_PEAKTYPE";
            ddl_saturday4.DataValueField = "ET_PEAKTYPE_ID";
            ddl_saturday4.DataBind();
            ddl_saturday4.Items.Insert(0, "Select");

            ddl_saturday5.DataSource = mytable;
            ddl_saturday5.DataTextField = "ET_PEAKTYPE";
            ddl_saturday5.DataValueField = "ET_PEAKTYPE_ID";
            ddl_saturday5.DataBind();
            ddl_saturday5.Items.Insert(0, "Select");

            ddl_sunday1.DataSource = mytable;
            ddl_sunday1.DataTextField = "ET_PEAKTYPE";
            ddl_sunday1.DataValueField = "ET_PEAKTYPE_ID";
            ddl_sunday1.DataBind();
            ddl_sunday1.Items.Insert(0, "Select");

            ddl_sunday2.DataSource = mytable;
            ddl_sunday2.DataTextField = "ET_PEAKTYPE";
            ddl_sunday2.DataValueField = "ET_PEAKTYPE_ID";
            ddl_sunday2.DataBind();
            ddl_sunday2.Items.Insert(0, "Select");

            ddl_sunday3.DataSource = mytable;
            ddl_sunday3.DataTextField = "ET_PEAKTYPE";
            ddl_sunday3.DataValueField = "ET_PEAKTYPE_ID";
            ddl_sunday3.DataBind();
            ddl_sunday3.Items.Insert(0, "Select");

            ddl_sunday4.DataSource = mytable;
            ddl_sunday4.DataTextField = "ET_PEAKTYPE";
            ddl_sunday4.DataValueField = "ET_PEAKTYPE_ID";
            ddl_sunday4.DataBind();
            ddl_sunday4.Items.Insert(0, "Select");

            ddl_sunday5.DataSource = mytable;
            ddl_sunday5.DataTextField = "ET_PEAKTYPE";
            ddl_sunday5.DataValueField = "ET_PEAKTYPE_ID";
            ddl_sunday5.DataBind();
            ddl_sunday5.Items.Insert(0, "Select");

        }
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
        if (!(txtProgName.SelectedIndex >= 0))
            return;


        gridbind(txtProgName.SelectedValue.ToString());
    }

    public void gridbind(string programid)
    {
        String Select_Seasons = "Select * from touSeasonsEP where PROGRAM_id=" + programid + "";
        SqlDataAdapter da = new SqlDataAdapter(Select_Seasons, sqlcon);
        DataSet ds = new DataSet();
        da.Fill(ds, "Seasons");
        if (ds.Tables["seasons"].Rows.Count > 0)
        {
            div_scroll.Visible = true;
            gv_peaks.Visible = true;
            btnConfirm.Visible = true;
            btnClear.Visible = true;
            gv_peaks.DataSource = ds;
            gv_peaks.DataBind();
        }
        else
        {
            div_scroll.Visible = false;
            gv_peaks.DataSource = null;
            gv_peaks.Visible = false;
        }
    }
    
    protected void Page_Load(object sender, EventArgs e)
    {
        sqlcon = new SqlConnection(Session["client_database"].ToString());


        if (!IsPostBack)
        {
            Bind_Programs();
        }
        //div_scroll.Visible = false;

        //resetStyles();
        writeMessage("");
    }

    protected void writeMessage(String msg)
    {
        lbl_message.Text = msg;
    }

    protected void btnClear_OnClick(object sender, EventArgs e)
    {
        clearFields();
        //ph_insertpeaks.Controls.Clear();
    }

    protected void RadAjaxPanel1_Load(object sender, EventArgs e)
    {
    }

    protected void clearFields()
    {
        txtProgType.ClearSelection();
        txtProgName.ClearSelection();
        txtProgName.Enabled = false;
        btnConfirm.Visible = false;
        btnClear.Visible = false;
        div_scroll.Visible = false;
    }

    protected void btnConfirm_Click(object sender, EventArgs e)
    {
        addPeaks();
        //writeMessage("Peak added successfully.");
        //clearFields();
    }

    /*Validation on Input*/
    protected bool checkRow(DropDownList[] ddl, TextBox[,] hours, String seasonName, String peakType)
    {
        IEnumerable<int>[] arrayRange = new IEnumerable<int>[5];
        for (int i = 0; i < 5; i++)
        {
            if (!String.IsNullOrEmpty(ddl[i].SelectedValue))
            {
                String start_hour = hours[i, 0].Text;
                String stop_hour = hours[i, 1].Text;

                if (!String.IsNullOrEmpty(start_hour) && !String.IsNullOrEmpty(stop_hour))
                {
                    arrayRange[i] = Enumerable.Range(Convert.ToInt32(start_hour), Convert.ToInt32(stop_hour) - Convert.ToInt32(start_hour));
                }
            }
        }

        foreach (IEnumerable<int> ienum in arrayRange)
        {
            foreach (IEnumerable<int> ienum2 in arrayRange)
            {
                if (ienum != null && ienum2 != null && ienum != ienum2)
                {
                    IEnumerable<int> both = ienum.Intersect(ienum2);

                    if (both.Any())
                    {
                        writeMessage(seasonName + " - " + peakType + ": You cannot have intersecting hours.");
                        return false;
                    }
                }
            }
        }

        bool allnumbersused = true;
        for (int i = 0; i <= 23; i++)
        {
            bool exists = false;
            foreach (IEnumerable<int> ienum in arrayRange)
            {
                if (ienum != null)
                {
                    if (ienum.Contains(i))
                        exists = true;
                }
            }
            if (!exists)
            {
                allnumbersused = false;
                break;
            }
        }

        if (!allnumbersused)
        {
            writeMessage(seasonName + " - " + peakType + ": You must account for every hour.");
            return false;
        }

        return true;
    }

    protected void addPeaks()
    {
        {
            if (dropdown_validaion())
            {
                if (textboxhoursvalidation())
                {
                    //Insertion

                    foreach (GridViewRow Rows in gv_peaks.Rows)
                    {

                        DropDownList ddl_week_day_text1 = (DropDownList)Rows.FindControl("ddl_weekdays1");
                        DropDownList ddl_week_day_text2 = (DropDownList)Rows.FindControl("ddl_weekdays2");
                        DropDownList ddl_week_day_text3 = (DropDownList)Rows.FindControl("ddl_weekdays3");
                        DropDownList ddl_week_day_text4 = (DropDownList)Rows.FindControl("ddl_weekdays4");
                        DropDownList ddl_week_day_text5 = (DropDownList)Rows.FindControl("ddl_weekdays5");

                        DropDownList ddl_holiday_text1 = (DropDownList)Rows.FindControl("ddl_holiday1");
                        DropDownList ddl_holiday_text2 = (DropDownList)Rows.FindControl("ddl_holiday2");
                        DropDownList ddl_holiday_text3 = (DropDownList)Rows.FindControl("ddl_holiday3");
                        DropDownList ddl_holiday_text4 = (DropDownList)Rows.FindControl("ddl_holiday4");
                        DropDownList ddl_holiday_text5 = (DropDownList)Rows.FindControl("ddl_holiday5");

                        DropDownList ddl_saturday_text1 = (DropDownList)Rows.FindControl("ddl_saturday1");
                        DropDownList ddl_saturday_text2 = (DropDownList)Rows.FindControl("ddl_saturday2");
                        DropDownList ddl_saturday_text3 = (DropDownList)Rows.FindControl("ddl_saturday3");
                        DropDownList ddl_saturday_text4 = (DropDownList)Rows.FindControl("ddl_saturday4");
                        DropDownList ddl_saturday_text5 = (DropDownList)Rows.FindControl("ddl_saturday5");

                        DropDownList ddl_sunday_text1 = (DropDownList)Rows.FindControl("ddl_sunday1");
                        DropDownList ddl_sunday_text2 = (DropDownList)Rows.FindControl("ddl_sunday2");
                        DropDownList ddl_sunday_text3 = (DropDownList)Rows.FindControl("ddl_sunday3");
                        DropDownList ddl_sunday_text4 = (DropDownList)Rows.FindControl("ddl_sunday4");
                        DropDownList ddl_sunday_text5 = (DropDownList)Rows.FindControl("ddl_sunday5");

                        TextBox txt_week_start1 = (TextBox)Rows.FindControl("txt_weekdays11");
                        TextBox txt_week_stop1 = (TextBox)Rows.FindControl("txt_weekdays12");
                        TextBox txt_week_start2 = (TextBox)Rows.FindControl("txt_weekdays21");
                        TextBox txt_week_stop2 = (TextBox)Rows.FindControl("txt_weekdays22");
                        TextBox txt_week_start3 = (TextBox)Rows.FindControl("txt_weekdays31");
                        TextBox txt_week_stop3 = (TextBox)Rows.FindControl("txt_weekdays32");
                        TextBox txt_week_start4 = (TextBox)Rows.FindControl("txt_weekdays41");
                        TextBox txt_week_stop4 = (TextBox)Rows.FindControl("txt_weekdays42");
                        TextBox txt_week_start5 = (TextBox)Rows.FindControl("txt_weekdays51");
                        TextBox txt_week_stop5 = (TextBox)Rows.FindControl("txt_weekdays52");

                        TextBox txt_holi_start1 = (TextBox)Rows.FindControl("txt_holiday11");
                        TextBox txt_holi_stop1 = (TextBox)Rows.FindControl("txt_holiday12");
                        TextBox txt_holi_start2 = (TextBox)Rows.FindControl("txt_holiday21");
                        TextBox txt_holi_stop2 = (TextBox)Rows.FindControl("txt_holiday22");
                        TextBox txt_holi_start3 = (TextBox)Rows.FindControl("txt_holiday31");
                        TextBox txt_holi_stop3 = (TextBox)Rows.FindControl("txt_holiday32");
                        TextBox txt_holi_start4 = (TextBox)Rows.FindControl("txt_holiday41");
                        TextBox txt_holi_stop4 = (TextBox)Rows.FindControl("txt_holiday42");
                        TextBox txt_holi_start5 = (TextBox)Rows.FindControl("txt_holiday51");
                        TextBox txt_holi_stop5 = (TextBox)Rows.FindControl("txt_holiday52");

                        TextBox txt_saturday_start1 = (TextBox)Rows.FindControl("txt_saturday11");
                        TextBox txt_saturday_stop1 = (TextBox)Rows.FindControl("txt_saturday12");
                        TextBox txt_saturday_start2 = (TextBox)Rows.FindControl("txt_saturday21");
                        TextBox txt_saturday_stop2 = (TextBox)Rows.FindControl("txt_saturday22");
                        TextBox txt_saturday_start3 = (TextBox)Rows.FindControl("txt_saturday31");
                        TextBox txt_saturday_stop3 = (TextBox)Rows.FindControl("txt_saturday32");
                        TextBox txt_saturday_start4 = (TextBox)Rows.FindControl("txt_saturday41");
                        TextBox txt_saturday_stop4 = (TextBox)Rows.FindControl("txt_saturday42");
                        TextBox txt_saturday_start5 = (TextBox)Rows.FindControl("txt_saturday51");
                        TextBox txt_saturday_stop5 = (TextBox)Rows.FindControl("txt_saturday52");

                        TextBox txt_sunday_start1 = (TextBox)Rows.FindControl("txt_sunday11");
                        TextBox txt_sunday_stop1 = (TextBox)Rows.FindControl("txt_sunday12");
                        TextBox txt_sunday_start2 = (TextBox)Rows.FindControl("txt_sunday21");
                        TextBox txt_sunday_stop2 = (TextBox)Rows.FindControl("txt_sunday22");
                        TextBox txt_sunday_start3 = (TextBox)Rows.FindControl("txt_sunday31");
                        TextBox txt_sunday_stop3 = (TextBox)Rows.FindControl("txt_sunday32");
                        TextBox txt_sunday_start4 = (TextBox)Rows.FindControl("txt_sunday41");
                        TextBox txt_sunday_stop4 = (TextBox)Rows.FindControl("txt_sunday42");
                        TextBox txt_sunday_start5 = (TextBox)Rows.FindControl("txt_sunday51");
                        TextBox txt_sunday_stop5 = (TextBox)Rows.FindControl("txt_sunday52");
                        //Int32 PEAKS_NEW_ID=0;
                        //season id from db

                        Label lbl_Sean_Id = (Label)Rows.FindControl("lbl_Season_ID");
                        Label lbl_Sean = (Label)Rows.FindControl("lbl_season");


                        //validation
                        DropDownList[] ddl_week_day = new DropDownList[5];
                        ddl_week_day[0] = (DropDownList)Rows.FindControl("ddl_weekdays1");
                        ddl_week_day[1] = (DropDownList)Rows.FindControl("ddl_weekdays2");
                        ddl_week_day[2] = (DropDownList)Rows.FindControl("ddl_weekdays3");
                        ddl_week_day[3] = (DropDownList)Rows.FindControl("ddl_weekdays4");
                        ddl_week_day[4] = (DropDownList)Rows.FindControl("ddl_weekdays5");

                        DropDownList[] ddl_holiday = new DropDownList[5];
                        ddl_holiday[0] = (DropDownList)Rows.FindControl("ddl_holiday1");
                        ddl_holiday[1] = (DropDownList)Rows.FindControl("ddl_holiday2");
                        ddl_holiday[2] = (DropDownList)Rows.FindControl("ddl_holiday3");
                        ddl_holiday[3] = (DropDownList)Rows.FindControl("ddl_holiday4");
                        ddl_holiday[4] = (DropDownList)Rows.FindControl("ddl_holiday5");

                        DropDownList[] ddl_saturday = new DropDownList[5];
                        ddl_saturday[0] = (DropDownList)Rows.FindControl("ddl_saturday1");
                        ddl_saturday[1] = (DropDownList)Rows.FindControl("ddl_saturday2");
                        ddl_saturday[2] = (DropDownList)Rows.FindControl("ddl_saturday3");
                        ddl_saturday[3] = (DropDownList)Rows.FindControl("ddl_saturday4");
                        ddl_saturday[4] = (DropDownList)Rows.FindControl("ddl_saturday5");

                        DropDownList[] ddl_sunday = new DropDownList[5];
                        ddl_sunday[0] = (DropDownList)Rows.FindControl("ddl_sunday1");
                        ddl_sunday[1] = (DropDownList)Rows.FindControl("ddl_sunday2");
                        ddl_sunday[2] = (DropDownList)Rows.FindControl("ddl_sunday3");
                        ddl_sunday[3] = (DropDownList)Rows.FindControl("ddl_sunday4");
                        ddl_sunday[4] = (DropDownList)Rows.FindControl("ddl_sunday5");

                        TextBox[,] txt_week = new TextBox[5, 2];
                        txt_week[0, 0] = (TextBox)Rows.FindControl("txt_weekdays11");
                        txt_week[0, 1] = (TextBox)Rows.FindControl("txt_weekdays12");
                        txt_week[1, 0] = (TextBox)Rows.FindControl("txt_weekdays21");
                        txt_week[1, 1] = (TextBox)Rows.FindControl("txt_weekdays22");
                        txt_week[2, 0] = (TextBox)Rows.FindControl("txt_weekdays31");
                        txt_week[2, 1] = (TextBox)Rows.FindControl("txt_weekdays32");
                        txt_week[3, 0] = (TextBox)Rows.FindControl("txt_weekdays41");
                        txt_week[3, 1] = (TextBox)Rows.FindControl("txt_weekdays42");
                        txt_week[4, 0] = (TextBox)Rows.FindControl("txt_weekdays51");
                        txt_week[4, 1] = (TextBox)Rows.FindControl("txt_weekdays52");

                        TextBox[,] txt_holi = new TextBox[5, 2];
                        txt_holi[0, 0] = (TextBox)Rows.FindControl("txt_holiday11");
                        txt_holi[0, 1] = (TextBox)Rows.FindControl("txt_holiday12");
                        txt_holi[1, 0] = (TextBox)Rows.FindControl("txt_holiday21");
                        txt_holi[1, 1] = (TextBox)Rows.FindControl("txt_holiday22");
                        txt_holi[2, 0] = (TextBox)Rows.FindControl("txt_holiday31");
                        txt_holi[2, 1] = (TextBox)Rows.FindControl("txt_holiday32");
                        txt_holi[3, 0] = (TextBox)Rows.FindControl("txt_holiday41");
                        txt_holi[3, 1] = (TextBox)Rows.FindControl("txt_holiday42");
                        txt_holi[4, 0] = (TextBox)Rows.FindControl("txt_holiday51");
                        txt_holi[4, 1] = (TextBox)Rows.FindControl("txt_holiday52");

                        TextBox[,] txt_saturday = new TextBox[5, 2];
                        txt_saturday[0, 0] = (TextBox)Rows.FindControl("txt_saturday11");
                        txt_saturday[0, 1] = (TextBox)Rows.FindControl("txt_saturday12");
                        txt_saturday[1, 0] = (TextBox)Rows.FindControl("txt_saturday21");
                        txt_saturday[1, 1] = (TextBox)Rows.FindControl("txt_saturday22");
                        txt_saturday[2, 0] = (TextBox)Rows.FindControl("txt_saturday31");
                        txt_saturday[2, 1] = (TextBox)Rows.FindControl("txt_saturday32");
                        txt_saturday[3, 0] = (TextBox)Rows.FindControl("txt_saturday41");
                        txt_saturday[3, 1] = (TextBox)Rows.FindControl("txt_saturday42");
                        txt_saturday[4, 0] = (TextBox)Rows.FindControl("txt_saturday51");
                        txt_saturday[4, 1] = (TextBox)Rows.FindControl("txt_saturday52");

                        TextBox[,] txt_sunday = new TextBox[5, 2];
                        txt_sunday[0, 0] = (TextBox)Rows.FindControl("txt_sunday11");
                        txt_sunday[0, 1] = (TextBox)Rows.FindControl("txt_sunday12");
                        txt_sunday[1, 0] = (TextBox)Rows.FindControl("txt_sunday21");
                        txt_sunday[1, 1] = (TextBox)Rows.FindControl("txt_sunday22");
                        txt_sunday[2, 0] = (TextBox)Rows.FindControl("txt_sunday31");
                        txt_sunday[2, 1] = (TextBox)Rows.FindControl("txt_sunday32");
                        txt_sunday[3, 0] = (TextBox)Rows.FindControl("txt_sunday41");
                        txt_sunday[3, 1] = (TextBox)Rows.FindControl("txt_sunday42");
                        txt_sunday[4, 0] = (TextBox)Rows.FindControl("txt_sunday51");
                        txt_sunday[4, 1] = (TextBox)Rows.FindControl("txt_sunday52");

                        if (!checkRow(ddl_week_day, txt_week, lbl_Sean.Text, "WeekDays"))
                            return;
                        if (!checkRow(ddl_holiday, txt_holi, lbl_Sean.Text, "Holidays"))
                            return;
                        if (!checkRow(ddl_saturday, txt_saturday, lbl_Sean.Text, "Saturdays"))
                            return;
                        if (!checkRow(ddl_sunday, txt_sunday, lbl_Sean.Text, "Sundays"))
                            return;


                        //weekdays insertion


                        Insert_Peak_details(txtProgName.SelectedValue, lbl_Sean_Id.Text.Trim(),
                            "Week Days", ddl_week_day_text1.SelectedValue, txt_week_start1.Text.Trim(), txt_week_stop1.Text.Trim(), "Y");

                        Insert_Peak_details(txtProgName.SelectedValue, lbl_Sean_Id.Text.Trim(),
                               "Week Days", ddl_week_day_text2.SelectedValue, txt_week_start2.Text.Trim(), txt_week_stop2.Text.Trim(), "Y");

                        Insert_Peak_details(txtProgName.SelectedValue, lbl_Sean_Id.Text.Trim(),
                               "Week Days", ddl_week_day_text3.SelectedValue, txt_week_start3.Text.Trim(), txt_week_stop3.Text.Trim(), "Y");

                        Insert_Peak_details(txtProgName.SelectedValue, lbl_Sean_Id.Text.Trim(),
                               "Week Days", ddl_week_day_text4.SelectedValue, txt_week_start4.Text.Trim(), txt_week_stop4.Text.Trim(), "Y");

                        Insert_Peak_details(txtProgName.SelectedValue, lbl_Sean_Id.Text.Trim(),
                               "Week Days", ddl_week_day_text5.SelectedValue, txt_week_start5.Text.Trim(), txt_week_stop5.Text.Trim(), "Y");

                        //holidays insertion

                        Insert_Peak_details(txtProgName.SelectedValue, lbl_Sean_Id.Text.Trim(),
                               "Holidays", ddl_holiday_text1.SelectedValue, txt_holi_start1.Text.Trim(), txt_holi_stop1.Text.Trim(), "Y");

                        Insert_Peak_details(txtProgName.SelectedValue, lbl_Sean_Id.Text.Trim(),
                               "Holidays", ddl_holiday_text2.SelectedValue, txt_holi_start2.Text.Trim(), txt_holi_stop2.Text.Trim(), "Y");

                        Insert_Peak_details(txtProgName.SelectedValue, lbl_Sean_Id.Text.Trim(),
                               "Holidays", ddl_holiday_text3.SelectedValue, txt_holi_start3.Text.Trim(), txt_holi_stop3.Text.Trim(), "Y");

                        Insert_Peak_details(txtProgName.SelectedValue, lbl_Sean_Id.Text.Trim(),
                               "Holidays", ddl_holiday_text4.SelectedValue, txt_holi_start4.Text.Trim(), txt_holi_stop4.Text.Trim(), "Y");

                        Insert_Peak_details(txtProgName.SelectedValue, lbl_Sean_Id.Text.Trim(),
                               "Holidays", ddl_holiday_text5.SelectedValue, txt_holi_start5.Text.Trim(), txt_holi_stop5.Text.Trim(), "Y");

                        //saturdays insertion

                        Insert_Peak_details(txtProgName.SelectedValue, lbl_Sean_Id.Text.Trim(),
                                "Saturdays", ddl_saturday_text1.SelectedValue, txt_saturday_start1.Text.Trim(), txt_saturday_stop1.Text.Trim(), "Y");

                        Insert_Peak_details(txtProgName.SelectedValue, lbl_Sean_Id.Text.Trim(),
                               "Saturdays", ddl_saturday_text2.SelectedValue, txt_saturday_start2.Text.Trim(), txt_saturday_stop2.Text.Trim(), "Y");

                        Insert_Peak_details(txtProgName.SelectedValue, lbl_Sean_Id.Text.Trim(),
                               "Saturdays", ddl_saturday_text3.SelectedValue, txt_saturday_start3.Text.Trim(), txt_saturday_stop3.Text.Trim(), "Y");

                        Insert_Peak_details(txtProgName.SelectedValue, lbl_Sean_Id.Text.Trim(),
                               "Saturdays", ddl_saturday_text4.SelectedValue, txt_saturday_start4.Text.Trim(), txt_saturday_stop4.Text.Trim(), "Y");

                        Insert_Peak_details(txtProgName.SelectedValue, lbl_Sean_Id.Text.Trim(),
                               "Saturdays", ddl_saturday_text5.SelectedValue, txt_saturday_start5.Text.Trim(), txt_saturday_stop5.Text.Trim(), "Y");

                        //sundays insertion

                        Insert_Peak_details(txtProgName.SelectedValue, lbl_Sean_Id.Text.Trim(),
                               "Sundays", ddl_sunday_text1.SelectedValue, txt_sunday_start1.Text.Trim(), txt_sunday_stop1.Text.Trim(), "Y");

                        Insert_Peak_details(txtProgName.SelectedValue, lbl_Sean_Id.Text.Trim(),
                               "Sundays", ddl_sunday_text2.SelectedValue, txt_sunday_start2.Text.Trim(), txt_sunday_stop2.Text.Trim(), "Y");

                        Insert_Peak_details(txtProgName.SelectedValue, lbl_Sean_Id.Text.Trim(),
                               "Sundays", ddl_sunday_text3.SelectedValue, txt_sunday_start3.Text.Trim(), txt_sunday_stop3.Text.Trim(), "Y");

                        Insert_Peak_details(txtProgName.SelectedValue, lbl_Sean_Id.Text.Trim(),
                               "Sundays", ddl_sunday_text4.SelectedValue, txt_sunday_start4.Text.Trim(), txt_sunday_stop4.Text.Trim(), "Y");

                        Insert_Peak_details(txtProgName.SelectedValue, lbl_Sean_Id.Text.Trim(),
                               "Sundays", ddl_sunday_text5.SelectedValue, txt_sunday_start5.Text.Trim(), txt_sunday_stop5.Text.Trim(), "Y");
                        lbl_message.Text = "Records Inserted Sucessfully";
                        lbl_message.ForeColor = System.Drawing.Color.Blue;

                    }
                    clearFields();
                    Bind_Programs();


                }
                else
                {
                    div_scroll.Visible = true;
                }

            }
            else
            {
                div_scroll.Visible = true;
            }
        }
    }

    public bool dropdown_validaion()
    {
        bool validation_dropdown_eachrecord = true;
        foreach (GridViewRow Rows in gv_peaks.Rows)
        {
            DropDownList ddl_week_day_text1 = (DropDownList)Rows.FindControl("ddl_weekdays1");
            DropDownList ddl_week_day_text2 = (DropDownList)Rows.FindControl("ddl_weekdays2");
            DropDownList ddl_week_day_text3 = (DropDownList)Rows.FindControl("ddl_weekdays3");
            DropDownList ddl_week_day_text4 = (DropDownList)Rows.FindControl("ddl_weekdays4");
            DropDownList ddl_week_day_text5 = (DropDownList)Rows.FindControl("ddl_weekdays5");

            DropDownList ddl_holiday_text1 = (DropDownList)Rows.FindControl("ddl_holiday1");
            DropDownList ddl_holiday_text2 = (DropDownList)Rows.FindControl("ddl_holiday2");
            DropDownList ddl_holiday_text3 = (DropDownList)Rows.FindControl("ddl_holiday3");
            DropDownList ddl_holiday_text4 = (DropDownList)Rows.FindControl("ddl_holiday4");
            DropDownList ddl_holiday_text5 = (DropDownList)Rows.FindControl("ddl_holiday5");

            DropDownList ddl_saturday_text1 = (DropDownList)Rows.FindControl("ddl_saturday1");
            DropDownList ddl_saturday_text2 = (DropDownList)Rows.FindControl("ddl_saturday2");
            DropDownList ddl_saturday_text3 = (DropDownList)Rows.FindControl("ddl_saturday3");
            DropDownList ddl_saturday_text4 = (DropDownList)Rows.FindControl("ddl_saturday4");
            DropDownList ddl_saturday_text5 = (DropDownList)Rows.FindControl("ddl_saturday5");

            DropDownList ddl_sunday_text1 = (DropDownList)Rows.FindControl("ddl_sunday1");
            DropDownList ddl_sunday_text2 = (DropDownList)Rows.FindControl("ddl_sunday2");
            DropDownList ddl_sunday_text3 = (DropDownList)Rows.FindControl("ddl_sunday3");
            DropDownList ddl_sunday_text4 = (DropDownList)Rows.FindControl("ddl_sunday4");
            DropDownList ddl_sunday_text5 = (DropDownList)Rows.FindControl("ddl_sunday5");


            Label lbl_seasonname = (Label)Rows.FindControl("lbl_season");

            if (!validate_dropdown_peaktype(ddl_week_day_text1, ddl_week_day_text2, ddl_week_day_text3, ddl_week_day_text4, ddl_week_day_text5, lbl_seasonname.Text, "Weekdays"))
            {
                doopweekday++;
                validation_dropdown_eachrecord = false;
                return validation_dropdown_eachrecord;
            }
            if (!validate_dropdown_peaktype(ddl_holiday_text1, ddl_holiday_text2, ddl_holiday_text3, ddl_holiday_text4, ddl_holiday_text5, lbl_seasonname.Text, "Holidays"))
            {
                doopholiday++;
                validation_dropdown_eachrecord = false;
                return validation_dropdown_eachrecord;
            }
            if (!validate_dropdown_peaktype(ddl_saturday_text1, ddl_saturday_text2, ddl_saturday_text3, ddl_saturday_text4, ddl_saturday_text5, lbl_seasonname.Text, "Saturdays"))
            {
                doopsaturday++;
                validation_dropdown_eachrecord = false;
                return validation_dropdown_eachrecord;
            }
            if (!validate_dropdown_peaktype(ddl_sunday_text1, ddl_sunday_text2, ddl_sunday_text3, ddl_sunday_text4, ddl_sunday_text5, lbl_seasonname.Text, "Sundays"))
            {
                doopsunday++;
                validation_dropdown_eachrecord = false;
                return validation_dropdown_eachrecord;
            }



        }
        if ((doopweekday == doopholiday && doopholiday == doopsaturday && doopsaturday == doopsunday && doopsunday == gv_peaks.Rows.Count))
        {
            validation_dropdown_eachrecord = false;
            return validation_dropdown_eachrecord;
        }
        else
        {
            validation_dropdown_eachrecord = true;
            return validation_dropdown_eachrecord;
        }
    }

    public bool validate_dropdown_peaktype(DropDownList ddl_weekdays1, DropDownList ddl_weekdays2,
       DropDownList ddl_weekdays3, DropDownList ddl_weekdays4, DropDownList ddl_weekdays5, string seasonname, string daytype)
    {

        String week_day_text1 = ddl_weekdays1.SelectedItem.Text;
        String week_day_text2 = ddl_weekdays2.SelectedItem.Text;
        String week_day_text3 = ddl_weekdays3.SelectedItem.Text;
        String week_day_text4 = ddl_weekdays4.SelectedItem.Text;
        String week_day_text5 = ddl_weekdays5.SelectedItem.Text;



        if (week_day_text1 == "Select" && week_day_text2 == "Select" && week_day_text3 == "Select" &&
            week_day_text4 == "Select" && week_day_text5 == "Select")
        {
            lbl_message.ForeColor = System.Drawing.Color.Red;
            lbl_message.Text = "Select <b>" + seasonname.ToString() + "</b>  Peaktype In <b>" + daytype.ToString() + "</b>";
            return false;
        }
        else
        {
            return true;
        }
    }

    public bool textboxhoursvalidation()
    {
        bool textstatus = true;
        string errorText = "";
        string weekdaystart = "", weekdaystop = "";
        string holidaystart = "", holidaystop = "";
        string saturdaystart = "", saturdaystop = "";
        string sundaystrat = "", sundaystop = "";


        foreach (GridViewRow Rows in gv_peaks.Rows)
        {
            DropDownList ddl_week_day_text1 = (DropDownList)Rows.FindControl("ddl_weekdays1");
            DropDownList ddl_week_day_text2 = (DropDownList)Rows.FindControl("ddl_weekdays2");
            DropDownList ddl_week_day_text3 = (DropDownList)Rows.FindControl("ddl_weekdays3");
            DropDownList ddl_week_day_text4 = (DropDownList)Rows.FindControl("ddl_weekdays4");
            DropDownList ddl_week_day_text5 = (DropDownList)Rows.FindControl("ddl_weekdays5");

            DropDownList ddl_holiday_text1 = (DropDownList)Rows.FindControl("ddl_holiday1");
            DropDownList ddl_holiday_text2 = (DropDownList)Rows.FindControl("ddl_holiday2");
            DropDownList ddl_holiday_text3 = (DropDownList)Rows.FindControl("ddl_holiday3");
            DropDownList ddl_holiday_text4 = (DropDownList)Rows.FindControl("ddl_holiday4");
            DropDownList ddl_holiday_text5 = (DropDownList)Rows.FindControl("ddl_holiday5");

            DropDownList ddl_saturday_text1 = (DropDownList)Rows.FindControl("ddl_saturday1");
            DropDownList ddl_saturday_text2 = (DropDownList)Rows.FindControl("ddl_saturday2");
            DropDownList ddl_saturday_text3 = (DropDownList)Rows.FindControl("ddl_saturday3");
            DropDownList ddl_saturday_text4 = (DropDownList)Rows.FindControl("ddl_saturday4");
            DropDownList ddl_saturday_text5 = (DropDownList)Rows.FindControl("ddl_saturday5");

            DropDownList ddl_sunday_text1 = (DropDownList)Rows.FindControl("ddl_sunday1");
            DropDownList ddl_sunday_text2 = (DropDownList)Rows.FindControl("ddl_sunday2");
            DropDownList ddl_sunday_text3 = (DropDownList)Rows.FindControl("ddl_sunday3");
            DropDownList ddl_sunday_text4 = (DropDownList)Rows.FindControl("ddl_sunday4");
            DropDownList ddl_sunday_text5 = (DropDownList)Rows.FindControl("ddl_sunday5");

            TextBox txt_week_start1 = (TextBox)Rows.FindControl("txt_weekdays11");
            TextBox txt_week_stop1 = (TextBox)Rows.FindControl("txt_weekdays12");
            TextBox txt_week_start2 = (TextBox)Rows.FindControl("txt_weekdays21");
            TextBox txt_week_stop2 = (TextBox)Rows.FindControl("txt_weekdays22");
            TextBox txt_week_start3 = (TextBox)Rows.FindControl("txt_weekdays31");
            TextBox txt_week_stop3 = (TextBox)Rows.FindControl("txt_weekdays32");
            TextBox txt_week_start4 = (TextBox)Rows.FindControl("txt_weekdays41");
            TextBox txt_week_stop4 = (TextBox)Rows.FindControl("txt_weekdays42");
            TextBox txt_week_start5 = (TextBox)Rows.FindControl("txt_weekdays51");
            TextBox txt_week_stop5 = (TextBox)Rows.FindControl("txt_weekdays52");

            TextBox txt_holi_start1 = (TextBox)Rows.FindControl("txt_holiday11");
            TextBox txt_holi_stop1 = (TextBox)Rows.FindControl("txt_holiday12");
            TextBox txt_holi_start2 = (TextBox)Rows.FindControl("txt_holiday21");
            TextBox txt_holi_stop2 = (TextBox)Rows.FindControl("txt_holiday22");
            TextBox txt_holi_start3 = (TextBox)Rows.FindControl("txt_holiday31");
            TextBox txt_holi_stop3 = (TextBox)Rows.FindControl("txt_holiday32");
            TextBox txt_holi_start4 = (TextBox)Rows.FindControl("txt_holiday41");
            TextBox txt_holi_stop4 = (TextBox)Rows.FindControl("txt_holiday42");
            TextBox txt_holi_start5 = (TextBox)Rows.FindControl("txt_holiday51");
            TextBox txt_holi_stop5 = (TextBox)Rows.FindControl("txt_holiday52");

            TextBox txt_saturday_start1 = (TextBox)Rows.FindControl("txt_saturday11");
            TextBox txt_saturday_stop1 = (TextBox)Rows.FindControl("txt_saturday12");
            TextBox txt_saturday_start2 = (TextBox)Rows.FindControl("txt_saturday21");
            TextBox txt_saturday_stop2 = (TextBox)Rows.FindControl("txt_saturday22");
            TextBox txt_saturday_start3 = (TextBox)Rows.FindControl("txt_saturday31");
            TextBox txt_saturday_stop3 = (TextBox)Rows.FindControl("txt_saturday32");
            TextBox txt_saturday_start4 = (TextBox)Rows.FindControl("txt_saturday41");
            TextBox txt_saturday_stop4 = (TextBox)Rows.FindControl("txt_saturday42");
            TextBox txt_saturday_start5 = (TextBox)Rows.FindControl("txt_saturday51");
            TextBox txt_saturday_stop5 = (TextBox)Rows.FindControl("txt_saturday52");

            TextBox txt_sunday_start1 = (TextBox)Rows.FindControl("txt_sunday11");
            TextBox txt_sunday_stop1 = (TextBox)Rows.FindControl("txt_sunday12");
            TextBox txt_sunday_start2 = (TextBox)Rows.FindControl("txt_sunday21");
            TextBox txt_sunday_stop2 = (TextBox)Rows.FindControl("txt_sunday22");
            TextBox txt_sunday_start3 = (TextBox)Rows.FindControl("txt_sunday31");
            TextBox txt_sunday_stop3 = (TextBox)Rows.FindControl("txt_sunday32");
            TextBox txt_sunday_start4 = (TextBox)Rows.FindControl("txt_sunday41");
            TextBox txt_sunday_stop4 = (TextBox)Rows.FindControl("txt_sunday42");
            TextBox txt_sunday_start5 = (TextBox)Rows.FindControl("txt_sunday51");
            TextBox txt_sunday_stop5 = (TextBox)Rows.FindControl("txt_sunday52");

            Label lbl_seasonname = (Label)Rows.FindControl("lbl_season");

            weekdaystart = txt_week_start1.Text + "|" + txt_week_start2.Text + "|" + txt_week_start3.Text + "|" + txt_week_start4.Text + "|" + txt_week_start5.Text;
            weekdaystop = txt_week_stop1.Text + "|" + txt_week_stop2.Text + "|" + txt_week_stop3.Text + "|" + txt_week_stop4.Text + "|" + txt_week_stop5.Text;
            holidaystart = txt_holi_start1.Text + "|" + txt_holi_start2.Text + "|" + txt_holi_start3.Text + "|" + txt_holi_start4.Text + "|" + txt_holi_start5.Text;
            holidaystop = txt_holi_stop1.Text + "|" + txt_holi_stop2.Text + "|" + txt_holi_stop3.Text + "|" + txt_holi_stop4.Text + "|" + txt_holi_stop5.Text;
            saturdaystart = txt_saturday_start1.Text + "|" + txt_saturday_start2.Text + "|" + txt_saturday_start3.Text + "|" + txt_saturday_start4.Text + "|" + txt_saturday_start5.Text;
            saturdaystop = txt_saturday_stop1.Text + "|" + txt_saturday_stop2.Text + "|" + txt_saturday_stop3.Text + "|" + txt_saturday_stop4.Text + "|" + txt_saturday_stop5.Text;
            sundaystrat = txt_sunday_start1.Text + "|" + txt_sunday_start2.Text + "|" + txt_sunday_start3.Text + "|" + txt_sunday_start4.Text + "|" + txt_sunday_start5.Text;
            sundaystop = txt_sunday_stop1.Text + "|" + txt_sunday_stop2.Text + "|" + txt_sunday_stop3.Text + "|" + txt_sunday_stop4.Text + "|" + txt_sunday_stop5.Text;

            if (((txt_week_start1.Text != "" && txt_week_stop1.Text != "") ||
                (txt_week_start2.Text != "" && txt_week_stop2.Text != "") ||
                (txt_week_start3.Text != "" && txt_week_stop3.Text != "") ||
                (txt_week_start4.Text != "" && txt_week_stop4.Text != "") ||
                (txt_week_start5.Text != "" && txt_week_stop5.Text != "")) ||
                ddl_week_day_text1.SelectedItem.Text != "Select" || ddl_week_day_text2.SelectedItem.Text != "Select" ||
                ddl_week_day_text3.SelectedItem.Text != "Select" || ddl_week_day_text4.SelectedItem.Text != "Select" ||
                ddl_week_day_text5.SelectedItem.Text != "Select")
            {
                if (!validate_textbox(txt_week_start1, txt_week_stop1, txt_week_start2,
               txt_week_stop2, txt_week_start3, txt_week_stop3, txt_week_start4,
               txt_week_stop4, txt_week_start5, txt_week_stop5, ddl_week_day_text1,
               ddl_week_day_text2, ddl_week_day_text3, ddl_week_day_text4, ddl_week_day_text5, "Weekdays", lbl_seasonname.Text))
                {
                    textstatus = false;
                    return textstatus;
                }

                if (!starthourshould1(weekdaystart))
                {
                    lbl_message.ForeColor = System.Drawing.Color.Red;
                    lbl_message.Text = "Season <b>" + lbl_seasonname.Text + "</b>  Weekdays Start hour must have hour 1";
                    textstatus = false;
                    return textstatus;
                }
                if (!stophour24(weekdaystop))
                {
                    lbl_message.ForeColor = System.Drawing.Color.Red;
                    lbl_message.Text = "Season <b>" + lbl_seasonname.Text + "</b>  Weekdays Stop hour must have hour 24";
                    textstatus = false;
                    return textstatus;
                }

            }
            if (((txt_holi_start1.Text != "" && txt_holi_stop1.Text != "") ||
                (txt_holi_start2.Text != "" && txt_holi_stop2.Text != "") ||
                (txt_holi_start3.Text != "" && txt_holi_stop3.Text != "") ||
                (txt_holi_start4.Text != "" && txt_holi_stop4.Text != "") ||
                (txt_holi_start5.Text != "" && txt_holi_stop5.Text != "")) ||
                ddl_holiday_text1.SelectedItem.Text != "Select" || ddl_holiday_text2.SelectedItem.Text != "Select" ||
                ddl_holiday_text3.SelectedItem.Text != "Select" || ddl_holiday_text4.SelectedItem.Text != "Select" ||
                ddl_holiday_text5.SelectedItem.Text != "Select")
            {
                if (!validate_textbox(txt_holi_start1, txt_holi_stop1, txt_holi_start2,
              txt_holi_stop2, txt_holi_start3, txt_holi_stop3, txt_holi_start4,
              txt_holi_stop4, txt_holi_start5, txt_holi_stop5, ddl_holiday_text1,
              ddl_holiday_text2, ddl_holiday_text3, ddl_holiday_text4, ddl_holiday_text5, "Holidays", lbl_seasonname.Text))
                {
                    textstatus = false;
                    return textstatus;
                }
                if (!starthourshould1(holidaystart))
                {
                    lbl_message.ForeColor = System.Drawing.Color.Red;
                    lbl_message.Text = "Season <b>" + lbl_seasonname.Text + "</b>  Weekdays Start hour must have hour 1";
                    textstatus = false;
                    return textstatus;
                }
                if (!stophour24(holidaystop))
                {
                    lbl_message.ForeColor = System.Drawing.Color.Red;
                    lbl_message.Text = "Season <b>" + lbl_seasonname.Text + "</b>  Weekdays Stop hour must have hour 24";
                    textstatus = false;
                    return textstatus;
                }

            }
            if (((txt_saturday_start1.Text != "" && txt_saturday_stop1.Text != "") ||
                (txt_saturday_start2.Text != "" && txt_saturday_stop2.Text != "") ||
                (txt_saturday_start3.Text != "" && txt_saturday_stop3.Text != "") ||
                (txt_saturday_start4.Text != "" && txt_saturday_stop4.Text != "") ||
                (txt_saturday_start5.Text != "" && txt_saturday_stop5.Text != "")) ||
                ddl_saturday_text1.SelectedItem.Text != "Select" || ddl_saturday_text2.SelectedItem.Text != "Select" ||
                ddl_saturday_text3.SelectedItem.Text != "Select" || ddl_saturday_text4.SelectedItem.Text != "Select" ||
                ddl_saturday_text5.SelectedItem.Text != "Select")
            {

                if (!validate_textbox(txt_saturday_start1, txt_saturday_stop1, txt_saturday_start2,
                txt_saturday_stop2, txt_saturday_start3, txt_saturday_stop3, txt_saturday_start4,
                txt_saturday_stop4, txt_saturday_start5, txt_saturday_stop5, ddl_saturday_text1,
                ddl_saturday_text2, ddl_saturday_text3, ddl_saturday_text4, ddl_saturday_text5, "Saturdays", lbl_seasonname.Text))
                {
                    textstatus = false;
                    return textstatus;
                }
                if (!starthourshould1(saturdaystart))
                {
                    lbl_message.ForeColor = System.Drawing.Color.Red;
                    lbl_message.Text = "Season <b>" + lbl_seasonname.Text + "</b>  Weekdays Start hour must have hour 1";
                    textstatus = false;
                    return textstatus;
                }
                if (!stophour24(saturdaystop))
                {
                    lbl_message.ForeColor = System.Drawing.Color.Red;
                    lbl_message.Text = "Season <b>" + lbl_seasonname.Text + "</b>  Weekdays Stop hour must have hour 24";
                    textstatus = false;
                    return textstatus;
                }

            }
            if (((txt_sunday_start1.Text != "" && txt_sunday_stop1.Text != "") ||
               (txt_sunday_start2.Text != "" && txt_sunday_stop2.Text != "") ||
               (txt_sunday_start3.Text != "" && txt_sunday_stop3.Text != "") ||
               (txt_sunday_start4.Text != "" && txt_sunday_stop4.Text != "") ||
               (txt_sunday_start5.Text != "" && txt_sunday_stop5.Text != "")) ||
               ddl_sunday_text1.SelectedItem.Text != "Select" || ddl_sunday_text2.SelectedItem.Text != "Select" ||
               ddl_sunday_text3.SelectedItem.Text != "Select" || ddl_sunday_text4.SelectedItem.Text != "Select" ||
               ddl_sunday_text5.SelectedItem.Text != "Select")
            {
                if (!validate_textbox(txt_sunday_start1, txt_sunday_stop1, txt_sunday_start2,
                txt_sunday_stop2, txt_sunday_start3, txt_sunday_stop3, txt_sunday_start4,
                txt_sunday_stop4, txt_sunday_start5, txt_sunday_stop5, ddl_sunday_text1,
                ddl_sunday_text2, ddl_sunday_text3, ddl_sunday_text4, ddl_sunday_text5, "Sundays", lbl_seasonname.Text))
                {
                    textstatus = false;
                    return textstatus;
                }
                if (!starthourshould1(sundaystrat))
                {
                    lbl_message.ForeColor = System.Drawing.Color.Red;
                    lbl_message.Text = "Season <b>" + lbl_seasonname.Text + "</b>  Weekdays Start hour must have hour 1";
                    textstatus = false;
                    return textstatus;
                }
                if (!stophour24(sundaystop))
                {
                    lbl_message.ForeColor = System.Drawing.Color.Red;
                    lbl_message.Text = "Season <b>" + lbl_seasonname.Text + "</b>  Weekdays Stop hour must have hour 24";
                    textstatus = false;
                    return textstatus;
                }
            }
        }
        return textstatus;
    }
    public bool validate_textbox(TextBox txt_starthr1, TextBox txt_stophr1, TextBox txt_starthr2,
        TextBox txt_stophr2, TextBox txt_starthr3, TextBox txt_stophr3, TextBox txt_starthr4,
        TextBox txt_stophr4, TextBox txt_starthr5, TextBox txt_stophr5, DropDownList ddl_1,
        DropDownList ddl_2, DropDownList ddl_3, DropDownList ddl_4, DropDownList ddl_5, string Peakcolumn, string seasonname)
    {
        string Starttimeunique = "", Stoptimeunique = "";
        string[] arr_starttime, arr_stoptime;
        bool textcheck = true;
        int startarraylength = 0, stoparraylength = 0;

        starthr1 = txt_starthr1.Text;
        stophr1 = txt_stophr1.Text;
        starthr2 = txt_starthr2.Text;
        stophr2 = txt_stophr2.Text;
        starthr3 = txt_starthr3.Text;
        stophr3 = txt_stophr3.Text;
        starthr4 = txt_starthr4.Text;
        stophr4 = txt_stophr4.Text;
        starthr5 = txt_starthr5.Text;
        stophr5 = txt_stophr5.Text;

        ddl_text1 = ddl_1.SelectedItem.Text;
        ddl_text2 = ddl_2.SelectedItem.Text;
        ddl_text3 = ddl_3.SelectedItem.Text;
        ddl_text4 = ddl_4.SelectedItem.Text;
        ddl_text5 = ddl_5.SelectedItem.Text;


        if ((ddl_text1 != "Select") && (starthr1 == "" || stophr1 == ""))
        {
            lbl_message.ForeColor = System.Drawing.Color.Red;
            lbl_message.Text = "Season <b>" + seasonname.ToString() + "</b>  " + Peakcolumn.ToString() + "  Start hour and Stop hour should not be empty";
            textcheck = false;
            return textcheck;
        }
        if ((ddl_text2 != "Select") && (starthr2 == "" || stophr2 == ""))
        {
            lbl_message.ForeColor = System.Drawing.Color.Red;
            lbl_message.Text = "Season <b>" + seasonname.ToString() + "</b>  " + Peakcolumn.ToString() + "  Start hour and Stop hour should not be empty";
            textcheck = false;
            return textcheck;
        }
        if ((ddl_text3 != "Select") && (starthr3 == "" || stophr3 == ""))
        {
            lbl_message.ForeColor = System.Drawing.Color.Red;
            lbl_message.Text = "Season <b>" + seasonname.ToString() + "</b> " + Peakcolumn.ToString() + "  Start hour and Stop hour should not be empty";
            textcheck = false;
            return textcheck;
        }
        if ((ddl_text4 != "Select") && (starthr4 == "" || stophr4 == ""))
        {
            lbl_message.ForeColor = System.Drawing.Color.Red;
            lbl_message.Text = "Season <b>" + seasonname.ToString() + "</b>  " + Peakcolumn.ToString() + "  Start hour and Stop hour should not be empty";
            textcheck = false;
            return textcheck;
        }
        if ((ddl_text5 != "Select") && (starthr5 == "" || stophr5 == ""))
        {
            lbl_message.ForeColor = System.Drawing.Color.Red;
            lbl_message.Text = "Season<b>" + seasonname.ToString() + "</b> " + Peakcolumn.ToString() + "  Start hour and Stop hour should not be empty";
            textcheck = false;
            return textcheck;
        }


        if (ddl_text1 != "Select" && starthr1 != "")
        {
            if (Starttimeunique == "")
            {
                Starttimeunique = starthr1;
            }
            else
            {
                Starttimeunique += "|" + starthr1;
            }
            start_hr1 = Convert.ToInt32(starthr1);
        }
        if (ddl_text1 != "Select" && stophr1 != "")
        {
            if (Stoptimeunique == "")
            {
                Stoptimeunique = stophr1;
            }
            else
            {
                Stoptimeunique += "|" + stophr1;
            }
            stop_hr1 = Convert.ToInt32(stophr1);
        }
        if (ddl_text2 != "Select" && starthr2 != "")
        {
            if (Starttimeunique == "")
            {
                Starttimeunique = starthr2;
            }
            else
            {
                Starttimeunique += "|" + starthr2;
            }
            start_hr2 = Convert.ToInt32(starthr2);
        }
        if (ddl_text2 != "Select" && stophr2 != "")
        {
            if (Stoptimeunique == "")
            {
                Stoptimeunique = stophr2;
            }
            else
            {
                Stoptimeunique += "|" + stophr2;
            }
            stop_hr2 = Convert.ToInt32(stophr2);
        }
        if (ddl_text3 != "Select" && starthr3 != "")
        {
            if (Starttimeunique == "")
            {
                Starttimeunique = starthr3;
            }
            else
            {
                Starttimeunique += "|" + starthr3;
            }
            start_hr3 = Convert.ToInt32(starthr3);
        }
        if (ddl_text3 != "Select" && stophr3 != "")
        {
            if (Stoptimeunique == "")
            {
                Stoptimeunique = stophr3;
            }
            else
            {
                Stoptimeunique += "|" + stophr3;
            }
            stop_hr3 = Convert.ToInt32(stophr3);
        }
        if (ddl_text4 != "Select" && starthr4 != "")
        {
            if (Starttimeunique == "")
            {
                Starttimeunique = starthr4;
            }
            else
            {
                Starttimeunique += "|" + starthr4;
            }
            start_hr4 = Convert.ToInt32(starthr4);
        }
        if (ddl_text4 != "Select" && stophr4 != "")
        {
            if (Stoptimeunique == "")
            {
                Stoptimeunique = stophr4;
            }
            else
            {
                Stoptimeunique += "|" + stophr4;
            }
            stop_hr4 = Convert.ToInt32(stophr4);
        }
        if (ddl_text5 != "Select" && starthr5 != "")
        {
            if (Starttimeunique == "")
            {
                Starttimeunique = starthr5;
            }
            else
            {
                Starttimeunique += "|" + starthr5;
            }
            start_hr5 = Convert.ToInt32(starthr5);
        }
        if (ddl_text5 != "Select" && stophr5 != "")
        {
            if (Stoptimeunique == "")
            {
                Stoptimeunique = stophr5;
            }
            else
            {
                Stoptimeunique += "|" + stophr5;
            }
            stop_hr5 = Convert.ToInt32(stophr5);
        }
        startarraylength = Starttimeunique.Split('|').Length;
        stoparraylength = Stoptimeunique.Split('|').Length;
        arr_starttime = new string[startarraylength];
        arr_stoptime = new string[stoparraylength];



        if (Starttimeunique != "" && Stoptimeunique != "")
        {


            arr_starttime = Starttimeunique.Split('|');
            arr_stoptime = Stoptimeunique.Split('|');
        }



        if (!Unique_items_in_textbox(arr_starttime))
        {
            lbl_message.ForeColor = System.Drawing.Color.Red;
            lbl_message.Text = "Season <b>" + seasonname.ToString() + "</b>   " + Peakcolumn.ToString() + "  Start hr should be Unique";
            textcheck = false;
            return textcheck;
        }

        if (!Unique_items_in_textbox(arr_stoptime))
        {
            lbl_message.ForeColor = System.Drawing.Color.Red;
            lbl_message.Text = "Season <b>" + seasonname.ToString() + "</b>   " + Peakcolumn.ToString() + "  Stop hr should be Unique";
            textcheck = false;
            return textcheck;
        }
        if (!Startarray_Stoparray(arr_starttime, arr_stoptime))
        {
            lbl_message.ForeColor = System.Drawing.Color.Red;
            lbl_message.Text = "Season <b>" + seasonname.ToString() + "</b>   " + Peakcolumn.ToString() + "  Stop hour should be greater than Start hour ";
            textcheck = false;
            return textcheck;
        }
        if (!hr_shouldbe_less_24_greater_1(arr_starttime, arr_stoptime))
        {
            lbl_message.ForeColor = System.Drawing.Color.Red;
            lbl_message.Text = "Season <b>" + seasonname.ToString() + "</b>  " + Peakcolumn.ToString() + "  Stop hour and Start hour should be between 1 to 24 ";
            textcheck = false;
            return textcheck;
        }

        //if ((ddl_text1 != "Select") && (starthr1 != "" || stophr1 != ""))
        //{
        //    if (!Conflict_validation_Start_hr_Stop_hr(arr_starttime, arr_stoptime, Starttimeunique, Stoptimeunique))
        //    {
        //        lbl_message.ForeColor = System.Drawing.Color.Red;
        //        lbl_message.Text = "Season <b>" + seasonname.ToString() + "</b>    " + Peakcolumn.ToString() + "  Conflict with start hour and stop hour ";
        //        textcheck = false;
        //        return textcheck;
        //    }
        //}
        //if ((ddl_text2 != "Select") && (starthr2 != "" || stophr2 != ""))
        //{
        //    if (!Conflict_validation_Start_hr_Stop_hr(arr_starttime, arr_stoptime, Starttimeunique, Stoptimeunique))
        //    {
        //        lbl_message.ForeColor = System.Drawing.Color.Red;
        //        lbl_message.Text = "Season <b>" + seasonname.ToString() + "</b>     " + Peakcolumn.ToString() + "  Conflict with start hour and stop hour ";
        //        textcheck = false;
        //        return textcheck;
        //    }
        //}
        //if ((ddl_text3 != "Select") && (starthr3 != "" || stophr3 != ""))
        //{
        //    if (!Conflict_validation_Start_hr_Stop_hr(arr_starttime, arr_stoptime, Starttimeunique, Stoptimeunique))
        //    {
        //        lbl_message.ForeColor = System.Drawing.Color.Red;
        //        lbl_message.Text = "Season <b>" + seasonname.ToString() + "</b>     " + Peakcolumn.ToString() + "  Conflict with start hour and stop hour ";
        //        textcheck = false;
        //        return textcheck;
        //    }
        //}
        //if ((ddl_text4 != "Select") && (starthr4 != "" || stophr4 != ""))
        //{
        //    if (!Conflict_validation_Start_hr_Stop_hr(arr_starttime, arr_stoptime, Starttimeunique, Stoptimeunique))
        //    {
        //        lbl_message.ForeColor = System.Drawing.Color.Red;
        //        lbl_message.Text = "Season <b>" + seasonname.ToString() + "</b>    " + Peakcolumn.ToString() + "  Conflict with start hour and stop hour ";
        //        textcheck = false;
        //        return textcheck;
        //    }
        //}
        //if ((ddl_text5 != "Select") && (starthr5 != "" || stophr5 != ""))
        //{
        //    if (!Conflict_validation_Start_hr_Stop_hr(arr_starttime, arr_stoptime, Starttimeunique, Stoptimeunique))
        //    {
        //        lbl_message.ForeColor = System.Drawing.Color.Red;
        //        lbl_message.Text = "Season <b>" + seasonname.ToString() + "</b>     " + Peakcolumn.ToString() + "  Conflict with start hour and stop hour ";
        //        textcheck = false;
        //        return textcheck;
        //    }
        //}
        return textcheck;
    }

    public bool Unique_items_in_textbox(string[] textarray)
    {
        bool numberscheck = true;
        for (int k = 0; k < textarray.Length; k++)
        {
            for (int n = k + 1; n < textarray.Length; n++)
            {
                if (textarray[k].ToString() == textarray[n].ToString())
                {
                    numberscheck = false;

                }

            }
        }
        return numberscheck;
    }

    public bool Conflict_validation_Start_hr_Stop_hr(string[] arr_txt_starttime, string[] arr_txt_stoptime, string Starttimeinput, string Stoptimeinput)
    {
        bool numbercheck = true;
        int starthour = 0, stophour = 0, hourdif = 0, incrimenthour = 0, int_betweenhours = 0;
        string betweenhours = "", Specialtestvalues = "";
        string[] arr_betweenhours = null, arr_specail;
        if (arr_txt_starttime.Length == arr_txt_stoptime.Length)
        {
            for (int k = 0; k < arr_txt_starttime.Length; k++)
            {
                starthour = Convert.ToInt32(arr_txt_starttime[k].ToString());
                stophour = Convert.ToInt32(arr_txt_stoptime[k].ToString());
                hourdif = stophour - starthour;

                if (hourdif >= 1)
                {
                    for (int j = 0; j < hourdif - 1; j++)
                    {
                        incrimenthour = ++starthour;
                        if (betweenhours == "")
                        {
                            betweenhours = incrimenthour.ToString();
                        }
                        else
                        {
                            betweenhours += "|" + incrimenthour.ToString();
                        }
                    }
                }

                int_betweenhours = betweenhours.Split('|').Length;
                arr_betweenhours = new string[int_betweenhours];
                arr_betweenhours = betweenhours.Split('|');

                if (k > 0)
                {
                    for (int d = 0; d < int_betweenhours; d++)
                    {
                        for (int n = d + 1; n < int_betweenhours; n++)
                        {
                            if (arr_betweenhours[d].ToString() == arr_betweenhours[n].ToString())
                            {
                                numbercheck = false;

                            }

                        }//3 for loop
                    }//2 for loop
                }

            }//1 for loop
            //specailcase for             
            Specialtestvalues = Starttimeinput + "|" + Stoptimeinput;
            arr_specail = Specialtestvalues.Split('|');

            for (int f = 0; f < arr_specail.Length; f++)
            {
                for (int t = 0; t < arr_betweenhours.Length; t++)
                {
                    if (arr_specail[f].ToString() == arr_betweenhours[t].ToString())
                    {
                        numbercheck = false;

                    }

                }//3 for loop
            }//2


        }
        return numbercheck;
    }

    public bool Startarray_Stoparray(string[] arr_txt_starttime, string[] arr_txt_stoptime)
    {
        bool numberscheck = true;
        for (int k = 0; k < arr_txt_starttime.Length; k++)
        {

            if (Convert.ToInt32(arr_txt_starttime[k].ToString()) > Convert.ToInt32(arr_txt_stoptime[k].ToString()))
            {
                numberscheck = false;

            }


        }
        return numberscheck;
    }

    public bool hr_shouldbe_less_24_greater_1(string[] arr_txt_starttime, string[] arr_txt_stoptime)
    {
        bool numberscheck = true;
        for (int m = 0; m < arr_txt_starttime.Length; m++)
        {
            if (!((Convert.ToInt32(arr_txt_starttime[m])) >= 0 && (Convert.ToInt32(arr_txt_stoptime[m])) <= 24))
            {
                numberscheck = false;
            }
        }



        return numberscheck;
    }

    public void Insert_Peak_details(string programid, string seasonid, string weekname,
       string peak_type_id, string starthour, string stophour, string status)
    {
        SqlCommand cmd = new SqlCommand();
        string Insert_into_weekdays_peak = "";
        try
        {
            if (peak_type_id == "Select")
            {
                peak_type_id = "0";
            }
            Insert_into_weekdays_peak = "Insert into touPeaksEP(programID,";
            Insert_into_weekdays_peak += "seasonID,ET_WEEK_NAME,ET_PEAKTYPE_ID,ET_START_HR,ET_STOP_HR,PEAK_STATUS) values (";
            Insert_into_weekdays_peak += "'" + programid.ToString() + "'," + seasonid.ToString() + ",'" + weekname.ToString() + "',";
            Insert_into_weekdays_peak += "'" + peak_type_id.ToString() + "','" + starthour.ToString() + "',";
            Insert_into_weekdays_peak += "'" + stophour.ToString() + "','" + status.ToString() + "')";
            cmd = new SqlCommand(Insert_into_weekdays_peak, sqlcon);
            if (ConnectionState.Closed == sqlcon.State)
            {
                sqlcon.Open();
            }

            cmd.ExecuteNonQuery();
        }
        catch (Exception ex)
        {
        }
        finally
        {
            cmd.Dispose();
            sqlcon.Close();
        }

    }

    public void Bind_Programs()
    {
        string Que_Get_programs = "select distinct  PR.progName, PE.programID,pT.typeName as PROGRAM_type ";
        Que_Get_programs += " from  dbo.touPeaksEP PE,dbo.touPrograms PR, dbo.progType PT where PE.programID=PR.id AND PR.progTypeID=PT.ID";
        SqlDataAdapter da = new SqlDataAdapter(Que_Get_programs, sqlcon);
        DataSet ds = new DataSet();
        da.Fill(ds, "PROGRAMS_PEAKS");
        if (ds.Tables["PROGRAMS_PEAKS"].Rows.Count > 0)
        {
            //update viewer for existing seasons
            Gv_Prog_Peaks.DataSource = ds.Tables["PROGRAMS_PEAKS"];
            Gv_Prog_Peaks.DataBind();
        }
        else
        {
            lbl_message.Text = "No Records Found";
            lbl_message.ForeColor = System.Drawing.Color.Red;
        }



    }
    public bool starthourshould1(string Starttimeunique)
    {
        bool numberscheck = true;
        if (!Starttimeunique.Contains("0")) //it's now 0
        {
            numberscheck = false;
        }
        return numberscheck;
    }
    public bool stophour24(string Stoptimeunique)
    {
        bool numberscheck = true;
        if (!Stoptimeunique.Contains("24"))
        {
            numberscheck = false;
        }
        return numberscheck;
    }
    protected void Gv_Prog_Peaks_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "ed")
        {
            GridViewRow Row = (GridViewRow)((LinkButton)e.CommandSource).NamingContainer;
            string Program_id = ((Label)Row.FindControl("lbl_Program_id")).Text; ;
            string Program_name = ((Label)Row.FindControl("lbl_Program_Name")).Text; ;

            string Que_get_Season_Id = "  Select * from touSeasonsEP where PROGRAM_id=" + Program_id;
            SqlDataAdapter da = new SqlDataAdapter(Que_get_Season_Id, sqlcon);
            DataSet ds = new DataSet();
            da.Fill(ds, "Que_get_Season_Id");
            //Update_Grid(gv_peaks, ds, Program_id, "");
            //div_scroll.Visible = true;
            Peak_details_view(Program_id, Program_name);
            for (int i = 0; i < Gv_Prog_Peaks.Rows.Count; i++)
            {
                //Gv_Prog_Peaks.Rows[i].BackColor = System.Drawing.Color.White;
                Gv_Prog_Peaks.Rows[i].BackColor = System.Drawing.ColorTranslator.FromHtml("#D1DEB6");
            }
            //Row.BackColor = System.Drawing.ColorTranslator.FromHtml("#D1DEB6");
            Row.BackColor = System.Drawing.ColorTranslator.FromHtml("#BFD299");
            clearFields();
        }
    }
    public void Peak_details_view(string prog_id, string prog_name)
    {
        panel_seasons.Controls.Add(new LiteralControl("<table border='0'>"));
        panel_seasons.Controls.Add(new LiteralControl("<tr><td colspan='2' class='label' style='background-color:#4B6C9E;color:#ffffff;font-weight:bold;' align='center' class='label'>Program Name: " + prog_name.ToUpper() + "</td></tr>"));
        panel_seasons.Controls.Add(new LiteralControl("<tr><td colspan='2'  class='label' style='background-color:#4B6C9E;color:#ffffff;font-weight:bold;' align='center'  class='label'>Season Peak Hour Definitions</td></tr>"));
        panel_seasons.Controls.Add(new LiteralControl("<tr><td colspan='2'  class='label' align='center'>"));
        //Display Season and Peak Definitions
        seasons.Clear();
        season_months.Clear();
        //season
        seasonsBind(prog_id);
        SeasonMonthsBind(prog_id);
        panel_seasons.Controls.Add(new LiteralControl("<table border=1>"));
        string myJavaScript = ""; //declare javascript up here so we only call it once
        foreach (object id in seasons.Keys)
        {
            int season_id = Convert.ToInt32(id);
            string season_name = seasons[season_id + ""].ToString();
            string season_monthnames = season_months[season_id + ""].ToString();
            peaksBind(season_id, prog_id);
            panel_seasons.Controls.Add(new LiteralControl("<tr align='center'><td  style='background-color:#4B6C9E;color:#ffffff;font-weight:bold;'  class='label'> Season Name: " + season_name + "(" + season_monthnames + ")</td></tr>"));
            panel_seasons.Controls.Add(new LiteralControl("<tr><td align='center'>"));
            panel_seasons.Controls.Add(new LiteralControl("<table border=1 width='100%' style='font-family:Verdana;font-size:12px;'><tr><td style='font-weight:bold'>Peak Name</td><td style='font-weight:bold'>Weekdays</td><td style='font-weight:bold'>Holidays</td><td style='font-weight:bold'>Saturdays</td><td style='font-weight:bold'>Sundays</td></tr>"));
            placeholder_master.Controls.Add(new LiteralControl("<div style='background-color:#BFD299; display:inline-block; margin: 0 auto;-moz-border-radius: 15px;border-radius: 15px;margin:15px;padding:10px'>"));
            placeholder_master.Controls.Add(new LiteralControl("<div style='font-weight:bold;'>Season Name: " + season_name + "(" + season_monthnames + ")</div>"));
            foreach (object peakid in peaks.Keys)
            {

                int peak_id = Convert.ToInt32(peakid);
                string peak_name = peaks[peakid + ""].ToString();


                string PeakHrSql = "select convert(int,et_start_hr) as start,et_week_name, et_start_hr, et_stop_hr from touPeaksEP where  programID=" + prog_id + " and seasonID=" + season_id + " and et_peaktype_id=" + peak_id + "group by et_week_name, et_start_hr, et_stop_hr order by convert(int,et_start_hr) ";
                SqlDataAdapter da = new SqlDataAdapter(PeakHrSql, sqlcon);
                DataSet ds = new DataSet();
                da.Fill(ds, "peakdefs");
                string weekdays = "", saturdays = "", sundays = "", holidays = "";
                for (int i = 0; i < ds.Tables["peakdefs"].Rows.Count; i++)
                {
                    string week_type = ds.Tables["peakdefs"].Rows[i]["et_week_name"].ToString();
                    string start_hr = ds.Tables["peakdefs"].Rows[i]["et_start_hr"].ToString();
                    string stop_hr = ds.Tables["peakdefs"].Rows[i]["et_stop_hr"].ToString();
                    int start_hour = Convert.ToInt32(start_hr);
                    int stop_hour = Convert.ToInt32(stop_hr);

                    string stt_hour = "";
                    string stp_hour = "";
                    string Value = "";
                    if (start_hour == 0 && stop_hour == 24)
                        Value = "Whole Day(24 hrs)";
                    else
                    {
                        if (start_hour <= 12)
                        {
                            if (start_hour == 0)
                                stt_hour = "Mid Night";

                            else
                                stt_hour = (start_hour) + " AM";
                        }
                        else
                        {
                            if (start_hour == 12)
                                stt_hour = "12 PM";
                            else
                                stt_hour = (start_hour - 12) + " PM";
                        }

                        if (stop_hour <= 12)
                        {
                            if (stop_hour == 12)
                                stp_hour = "12 PM";
                            else
                                stp_hour = (stop_hour) + " AM";
                        }
                        else
                        {
                            if (stop_hour == 24)
                                stp_hour = "Mid Night";
                            else
                                stp_hour = (stop_hour - 12) + " PM";
                        }

                        Value = stt_hour + " - " + stp_hour;
                    }




                    switch (week_type)
                    {
                        case "Holidays":
                            {
                                if (holidays.Equals(""))
                                    holidays += Value;
                                else
                                    holidays += "<br>" + Value;
                                break;
                            }

                        case "Week Days":
                            {
                                if (weekdays.Equals(""))
                                    weekdays += Value;
                                else
                                    weekdays += "<br>" + Value;
                                break;
                            }
                        case "Saturdays":
                            {
                                if (saturdays.Equals(""))
                                    saturdays += Value;
                                else
                                    saturdays += "<br>" + Value;
                                break;
                            }
                        case "Sundays":
                            {
                                if (sundays.Equals(""))
                                    sundays += Value;
                                else
                                    sundays += "<br>" + Value;
                                break;

                            }
                        default:
                            {
                                break;

                            }
                    }




                }

                panel_seasons.Controls.Add(new LiteralControl("<tr><td  class='label'>" + peak_name + "</td><td>" + checkEmpty(weekdays) + "</td><td>" + checkEmpty(holidays) + "</td><td>" + checkEmpty(saturdays) + "</td><td>" + checkEmpty(sundays) + "</td></tr>"));
                
                /*insert time charts here*/
                String[] peakNames = new string[] { "Week Days", "Holidays", "Saturdays", "Sundays" };
                String[] peakValues = new string[] { checkEmpty(weekdays), checkEmpty(holidays), checkEmpty(saturdays), checkEmpty(sundays) };



                placeholder_master.Controls.Add(new LiteralControl("<div style='font-weight:bold;padding:10px;'>" + peak_name + "</div>"));
                for (int i = 0; i < peakNames.Count(); i++)
                {
                    placeholder_master.Controls.Add(new LiteralControl("<div style='display:inline-block;'>"));
                    placeholder_master.Controls.Add(new LiteralControl("<div>" + peakNames[i] + "</div>"));
                    placeholder_master.Controls.Add(new LiteralControl("<div style='display:inline-block;position:relative;padding:2px;'>"));
                    placeholder_master.Controls.Add(new LiteralControl("<div id='placeholder" + id + "_" + peakid + "_" + i + "' style='height:100px; width:100px'></div>"));
                    placeholder_master.Controls.Add(new LiteralControl("<div style='background-image:url(/images/clockface.png); background-size: 100px 100px; height: 100px; width: 100px;position:absolute;top:0px;left:0px;'></div>"));
                    placeholder_master.Controls.Add(new LiteralControl("</div></div>"));
                }

                for (int i = 0; i < peakNames.Count(); i++)
                {
                    int start_hour = 0;
                    int tot_hour = 0;

                    for (int x = 0; x < ds.Tables["peakdefs"].Rows.Count; x++)
                    {
                        string week_type = ds.Tables["peakdefs"].Rows[x]["et_week_name"].ToString();
                        if (week_type == peakNames[i])
                        {
                            string start_hr = ds.Tables["peakdefs"].Rows[x]["et_start_hr"].ToString();
                            string stop_hr = ds.Tables["peakdefs"].Rows[x]["et_stop_hr"].ToString();
                            start_hour = System.Convert.ToInt32(start_hr);
                            tot_hour = System.Convert.ToInt32(stop_hr) - start_hour;
                        }
                    }

                    //if no data draw an empty clock
                    if (start_hour == 0 && tot_hour == 0)
                        myJavaScript += "$(function() {drawPie('#placeholder" + id + "_" + peakid + "_" + i + "', " + 0 + ", " + .1 + ")});";
                    else
                        myJavaScript += "$(function() {drawPie('#placeholder" + id + "_" + peakid + "_" + i + "', " + (start_hour) + ", " + (tot_hour) + ")});";
                }

            }
            placeholder_master.Controls.Add(new LiteralControl("</div>"));
            panel_seasons.Controls.Add(new LiteralControl("</table></td></tr>"));


            peaks.Clear();
        }
        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "AutoComplete", myJavaScript, true);
        panel_seasons.Controls.Add(new LiteralControl("</table>"));

        panel_seasons.Controls.Add(new LiteralControl("</td></tr></table>"));
    }
    private string checkEmpty(string inValue)
    {
        if (inValue.Equals(""))
        {
            return " NA ";
        }
        else
            return inValue;
    }
    private void seasonsBind(string program_id)
    {
        SqlCommand cmd = new SqlCommand();
        seasons.Clear();
        try
        {
            string Que_get_season = "select season_id,season_name from touSeasonsEP where program_id = " + program_id.ToString() + " order by season_id desc";
            cmd = new SqlCommand(Que_get_season, sqlcon);
            sqlcon.Open();
            SqlDataReader dr = cmd.ExecuteReader();



            while (dr.Read())
            {

                seasons.Add(dr["season_id"].ToString(), dr["season_name"].ToString());

            }
        }
        catch (Exception ex)
        {
        }
        finally
        {

            cmd.Dispose();
            sqlcon.Close();
        }
    }
    public void SeasonMonthsBind(string program_id)
    {


        foreach (object id in seasons.Keys)
        {
            int season_id = Convert.ToInt32(id);
            string season_name = seasons[season_id + ""].ToString();



            string SeasonsSql = "select * from touSeasonsEP where  program_id=" + program_id.ToString() + " and season_id=" + season_id + " ";
            SqlDataAdapter da = new SqlDataAdapter(SeasonsSql, sqlcon);
            DataSet ds = new DataSet();
            da.Fill(ds, "season_names");
            string jan = "", feb = "", mar = "", apr = "", may = "", jun = "", jul = "", aug = "", sep = "", oct = "", nov = "", dec = "";
            string months = "";
            for (int i = 0; i < ds.Tables["season_names"].Rows.Count; i++)
            {
                jan = ds.Tables["season_names"].Rows[i]["jan"].ToString();
                feb = ds.Tables["season_names"].Rows[i]["feb"].ToString();
                mar = ds.Tables["season_names"].Rows[i]["mar"].ToString();
                apr = ds.Tables["season_names"].Rows[i]["apr"].ToString();
                may = ds.Tables["season_names"].Rows[i]["may"].ToString();
                jun = ds.Tables["season_names"].Rows[i]["jun"].ToString();
                jul = ds.Tables["season_names"].Rows[i]["jul"].ToString();
                aug = ds.Tables["season_names"].Rows[i]["aug"].ToString();
                sep = ds.Tables["season_names"].Rows[i]["sep"].ToString();
                oct = ds.Tables["season_names"].Rows[i]["oct"].ToString();
                nov = ds.Tables["season_names"].Rows[i]["nov"].ToString();
                dec = ds.Tables["season_names"].Rows[i]["dec"].ToString();

                if (jan.Equals("Y"))
                {
                    if (months.Equals(""))
                        months += "January";
                    else
                        months += ", January";
                }

                if (feb.Equals("Y"))
                {
                    if (months.Equals(""))
                        months += "February";
                    else
                        months += ", February";
                }

                if (mar.Equals("Y"))
                {
                    if (months.Equals(""))
                        months += "March";
                    else
                        months += ", March";
                }

                if (apr.Equals("Y"))
                {
                    if (months.Equals(""))
                        months += "April";
                    else
                        months += ", April";
                }

                if (may.Equals("Y"))
                {
                    if (months.Equals(""))
                        months += "May";
                    else
                        months += ", May";
                }

                if (jun.Equals("Y"))
                {
                    if (months.Equals(""))
                        months += "June";
                    else
                        months += ", June";
                }

                if (jul.Equals("Y"))
                {
                    if (months.Equals(""))
                        months += "July";
                    else
                        months += ", July";
                }

                if (aug.Equals("Y"))
                {
                    if (months.Equals(""))
                        months += "August";
                    else
                        months += ", August";
                }

                if (sep.Equals("Y"))
                {
                    if (months.Equals(""))
                        months += "September";
                    else
                        months += ", September";
                }

                if (oct.Equals("Y"))
                {
                    if (months.Equals(""))
                        months += "October";
                    else
                        months += ", October";
                }

                if (nov.Equals("Y"))
                {
                    if (months.Equals(""))
                        months += "November";
                    else
                        months += ", November";
                }

                if (dec.Equals("Y"))
                {
                    if (months.Equals(""))
                        months += "December";
                    else
                        months += ", December";
                }


            }
            season_months.Add(season_id + "", months);

        }


    }

    public void peaksBind(int season_id, string prog_id)
    {
        SqlCommand cmd = new SqlCommand();
        try
        {
            string Select_Peaks = " select distinct b.id as et_peaktype_id ,b.peakType as et_peaktype ";
            Select_Peaks += " from touPeaksEP a, touPeakTypes b where a.programID =" + prog_id.ToString() + "";
            Select_Peaks += " and a.seasonID =" + season_id.ToString() + "  and a.et_peaktype_id = b.id";

            cmd = new SqlCommand(Select_Peaks, sqlcon);
            if (ConnectionState.Closed == sqlcon.State)
                sqlcon.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                peaks.Add(dr["et_peaktype_id"].ToString(), dr["et_peaktype"].ToString());
            }
        }
        catch (Exception ex)
        {
        }
        finally
        {
            cmd.Dispose();
            sqlcon.Close();
        }
    }
} 