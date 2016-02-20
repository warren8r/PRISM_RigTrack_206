using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using ToU;
using System.Collections;
using Telerik.Web.UI;
using Telerik.Charting;
using Telerik.Web.UI.HtmlChart.PlotArea;
using Telerik.Web.UI.HtmlChart;
using Telerik.Charting.Styles;
using System.Drawing;

public partial class Modules_TOU_touManageTOU : System.Web.UI.Page
{
    SqlConnection sqlcon;
    touUtility touUtility = new touUtility();

    SqlCommand cmd;
    SqlDataAdapter da, da1;
    SqlDataReader dr;
    DataSet ds, ds1;

    ///textbox texts
    Hashtable seasons = new Hashtable();
    Hashtable season_months = new Hashtable();
    Hashtable peaks = new Hashtable();
    TextBox txt_monthly_charge;
    string PROGRAM_type = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        sqlcon = new SqlConnection(Session["client_database"].ToString());

        //drawCharts(5);

        writeMessage("");
        if (!IsPostBack)
            pageToggle(false);
    }

    protected void createChart(String chartTitle,Hashtable peaks, DataTable data)
    {
        /*
        RadHtmlChart radChart = new RadHtmlChart();
        radChart.ChartTitle.Text = chartTitle;
        radChart.ChartTitle.Appearance.Align = ChartTitleAlign.Left;
        radChart.ChartTitle.Appearance.TextStyle.FontSize = 16;
        radChart.PlotArea.Appearance.FillStyle.BackgroundColor = System.Drawing.ColorTranslator.FromHtml("#F5F5F5");
        radChart.PlotArea.XAxis.MajorGridLines.Visible = false;
        radChart.PlotArea.XAxis.MinorGridLines.Visible = false;
        radChart.PlotArea.YAxis.MajorGridLines.Visible = false;
        radChart.PlotArea.YAxis.MinorGridLines.Visible = false;
        radChart.Width = 400;
        radChart.Height = 266;


        radChart.PlotArea.XAxis.TitleAppearance.Text = "Peaks";
        radChart.PlotArea.XAxis.LabelsAppearance.RotationAngle = 90;
        radChart.PlotArea.XAxis.LabelsAppearance.Visible = true;
        radChart.PlotArea.YAxis.LabelsAppearance.Visible = true;*/


        RadChart radChart = new RadChart();
        radChart.ChartTitle.TextBlock.Text = chartTitle;
        //String fontName = radChart.ChartTitle.TextBlock.Appearance.TextProperties.Font.Name;
        //radChart.ChartTitle.TextBlock.Appearance.TextProperties.Font = new Font(fontName, 5);
        //radChart.ChartTitle.TextBlock.Appearance.TextProperties.Font = new System.Drawing.Font(FontFamily.GenericSansSerif, 11);
        //radChart.ChartTitle.TextBlock.Appearance.TextProperties.Font = new System.Drawing.Font("Verdana", 8f, System.Drawing.FontStyle.Regular);
        radChart.ChartTitle.TextBlock.Appearance.TextProperties.Font = new System.Drawing.Font("Verdana", 11f, System.Drawing.FontStyle.Regular);
        //radChart.ChartTitle.Appearance.Position.AlignedPosition = AlignedPositions.TopLeft;
        radChart.ChartTitle.Appearance.Position.Auto = false;
        radChart.ChartTitle.Appearance.Position.X = 0;
        radChart.ChartTitle.Appearance.Position.Y = 0;
        radChart.ChartTitle.Appearance.FillStyle.MainColor = System.Drawing.Color.Transparent;
        radChart.ChartTitle.Appearance.FillStyle.SecondColor = System.Drawing.Color.Transparent;

        radChart.Appearance.FillStyle.MainColor = System.Drawing.Color.Transparent;
        radChart.Appearance.Border.Visible = false;
        radChart.PlotArea.Appearance.FillStyle.MainColor = System.Drawing.ColorTranslator.FromHtml("#F5F5F5");
        radChart.PlotArea.Appearance.FillStyle.SecondColor = System.Drawing.ColorTranslator.FromHtml("#F5F5F5");
        radChart.Width = 500;
        radChart.Height = 366;
        radChart.AutoLayout = true;

        radChart.PlotArea.XAxis.Appearance.MajorGridLines.Visible = false;
        radChart.PlotArea.XAxis.Appearance.MinorGridLines.Visible = false;
        radChart.PlotArea.YAxis.Appearance.MajorGridLines.Visible = false;
        radChart.PlotArea.YAxis.Appearance.MinorGridLines.Visible = false;

        radChart.PlotArea.XAxis.Appearance.TextAppearance.TextProperties.Color = System.Drawing.Color.Black;
        radChart.PlotArea.YAxis.Appearance.TextAppearance.TextProperties.Color = System.Drawing.Color.Black;

        List<string> names = parseHash(peaks);
        for(int i = 0; i < data.Rows.Count; i++)
        {
            ChartSeries columnData = new ChartSeries();
            columnData.Name = data.Rows[i][0] + " to " + data.Rows[i][1];
            //columnData.DefaultLabelValue = "$#Y%";
            columnData.DefaultLabelValue = "#Y{C}";
            for (int x = 2; x < data.Columns.Count; x++)
            {
                double dbl = 0.0;
                try
                {
                    dbl = Convert.ToDouble(data.Rows[i][x]);
                }
                catch (Exception) { }
                ChartSeriesItem Item = new ChartSeriesItem(dbl);
                columnData.Items.Add(Item);
            }

            columnData.Appearance.TextAppearance.TextProperties.Color = System.Drawing.Color.Black;
            radChart.Series.Add(columnData);
        }

        radChart.PlotArea.XAxis.AutoScale = false;
        radChart.PlotArea.XAxis.AddRange(1, names.Count, 1);
        for (int i = 0; i < names.Count; i++)
            radChart.PlotArea.XAxis[i].TextBlock.Text = names[i];


        /*DataTable dt = new DataTable();
        dt.Columns.Add("peakName");
        dt.Columns.Add("Series");
        dt.Columns.Add("Value");
        List<string> names = parseHash(peaks);
        foreach (DataRow dr in data.Rows)
        {
            for (int i = 2; i < data.Columns.Count; i++)
            {
                DataRow newRow = dt.NewRow();
                newRow[0] = dr[0] + " to " + dr[1];
                newRow[1] = names[i - 2];
                newRow[2] = dr[i];
                dt.Rows.Add(newRow);
            }
        }*/
        
        /*for(int i = 0; i < data.Rows.Count; i++)
        {
            BarSeries columnData = new BarSeries();

            radChart.PlotArea.Series.Add(columnData);
        //columnData.LabelsAppearance.ClientTemplate = "Label";
            columnData.TooltipsAppearance.ClientTemplate = data.Rows[i][0] + " to " + data.Rows[i][1];
            columnData.Name = data.Rows[i][0] + " to " + data.Rows[i][1];

            for (int x = 2; x < data.Columns.Count; x++)
            {
                SeriesItem Item = new SeriesItem(Convert.ToDecimal(data.Rows[i][x]));
                columnData.Items.Add(Item);
            }
        }*/

        placeholder_barchart.Controls.Add(new LiteralControl("<div style='display:inline-block;'>"));
        placeholder_barchart.Controls.Add(radChart);
        placeholder_barchart.Controls.Add(new LiteralControl("</div>"));
    }

    protected List<string> parseHash(Hashtable hashtable)
    {
        List<string> returnString = new List<string>();

        foreach (object value in hashtable.Values)
        {
            returnString.Add(value as String);
        }
        return returnString;
    }

    protected DataRow createRow(DataRow dr, string Series, string Energy, double Cost)
    {
        dr[0] = Series;
        dr[1] = Energy;
        dr[2] = Cost;
        return dr;
    }

    protected void drawCharts(int numCharts)
    {
        Random rnd = new Random();
        
        for (int i = 0; i < numCharts; i++)
        {
            placeholder_master.Controls.Add(new LiteralControl("<div style='display:inline-block;'>"));
            placeholder_master.Controls.Add(new LiteralControl("<div>header</div>"));
            placeholder_master.Controls.Add(new LiteralControl("<div style='display:inline-block;position:relative;padding:2px;'>"));
            placeholder_master.Controls.Add(new LiteralControl("<div id='placeholder" + i + "' style='height:100px; width:100px'></div>"));
            placeholder_master.Controls.Add(new LiteralControl("<div style='background-image:url(/images/clockface.png); background-size: 100px 100px; height: 100px; width: 100px;position:absolute;top:0px;left:0px;'></div>"));
            placeholder_master.Controls.Add(new LiteralControl("</div></div>"));
        }

        string myJavaScript = "";
        for (int i = 0; i < numCharts; i++)
        {
            int num = rnd.Next(12) + 1;
            myJavaScript += "$(function() {drawPie('#placeholder" + i + "', " + num + ", " + num + ")});";
        }
        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "AutoComplete", myJavaScript, true);
    }

    protected DataColumn createCol(string colName)
    {
        DataColumn dc = new DataColumn(colName);
        return dc;
    }   

    protected void RadAjaxPanel1_Load(object sender, EventArgs e)
    {
        //javascript is lost on AjaxUpdate so let's register our AutoComplete script again.
        //string myJavaScript = "var availableTypes = " + touUtility.getTypes(touUtility.dtblfillTypes(sqlcon)) +
        //                      "var availableProgs = " + touUtility.getProgs(touUtility.dtblfillPrograms_Active(sqlcon, touUtility.retrieveTypeID(sqlcon, txtProgType.Text))) +
        //                      "populateAutoComplete('" + txtProgType.ClientID + "', availableTypes);" +
        //                      "populateAutoComplete('" + txtProgName.ClientID + "', availableProgs);";

        //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "AutoComplete", myJavaScript, true);
    }

    protected void txtProgType_SelectedIndexChanged(object sender, EventArgs e)
    {
        txtProgName.Enabled = false;

        if (txtProgType.SelectedIndex >= 0)
            txtProgName.Enabled = true;
    }

    protected void txtProgName_SelectedIndexChanged(object sender, EventArgs e)
    {
        clearFields_Form();
        pageToggle(false);
    }

    public void pageToggle(bool isActive)
    {
        txt_graceperiod.Enabled = isActive;
        txt_latefee.Enabled = isActive;
        txt_Monthly_Service_Charge.Enabled = isActive;
        txt_paymentdue.Enabled = isActive;
        txt_standardDeposit.Enabled = isActive;
        txt_billdate.Enabled = isActive;
        ddl_fuelcode.Enabled = isActive;
        ddl_taxcode.Enabled = isActive;
        btn_save.Enabled = isActive;
        btn_cancel.Enabled = isActive;
    }

    protected void btn_save_Click(object sender, EventArgs e)
    {
        double serviceCharge, lateFee, stdDeposit;
        int fuelAdjustID, taxAdjustID, progID;
        int gracePeriod, paymentDue;
        DateTime genDate;

        try
        {
            serviceCharge = Convert.ToDouble(txt_Monthly_Service_Charge.Text);
            lateFee = Convert.ToDouble(txt_latefee.Text);
            stdDeposit = Convert.ToDouble(txt_standardDeposit.Text);

            gracePeriod = Convert.ToInt32(txt_graceperiod.Text);
            paymentDue = Convert.ToInt32(txt_paymentdue.Text);

            genDate = (DateTime)txt_billdate.SelectedDate;

            progID = touUtility.retrieveProgamID(sqlcon, txtProgName.Text);
            taxAdjustID = Convert.ToInt32(ddl_taxcode.SelectedValue);
            fuelAdjustID = Convert.ToInt32(ddl_fuelcode.SelectedValue);
        }
        catch (Exception ex)
        {
            writeMessage("Validation has been disabled. Please ensure you have entered the correct values in each field before continuing.");
            return;
        }


        try
        {
            sqlcon.Open();
            using (SqlCommand sqlcmd = new SqlCommand("INSERT INTO touProgramsInfo (progID, fuelAdjustID, taxAdjustID, billGenerationDate, paymentDue, gracePeriod, lateFee, serviceCharge, stdDeposit) VALUES (@progID, @fuelAdjustID, @taxAdjustID, @billGenerationDate, @paymentDue, @gracePeriod, @lateFee, @serviceCharge, @stdDeposit)", sqlcon))
            {
                sqlcmd.Parameters.AddWithValue("@progID", progID);
                sqlcmd.Parameters.AddWithValue("@fuelAdjustID", fuelAdjustID);
                sqlcmd.Parameters.AddWithValue("@taxAdjustID", taxAdjustID);
                sqlcmd.Parameters.AddWithValue("@billGenerationDate", genDate);
                sqlcmd.Parameters.AddWithValue("@paymentDue", paymentDue);
                sqlcmd.Parameters.AddWithValue("@gracePeriod", gracePeriod);
                sqlcmd.Parameters.AddWithValue("@lateFee", lateFee);
                sqlcmd.Parameters.AddWithValue("@serviceCharge", serviceCharge);
                sqlcmd.Parameters.AddWithValue("@stdDeposit", stdDeposit);
                sqlcmd.ExecuteNonQuery();
            }

            writeMessage("Successfully saved.");
        }
        catch (Exception ex)
        {
            writeMessage(ex + "Please contact your system administrator.");
        }
        finally
        {
            sqlcon.Close();
        }
    }

    protected void clearFields_All()
    {
        //clear our input fields
        txtProgName.ClearSelection();
        txtProgType.ClearSelection();
        txt_graceperiod.Text = "";
        txt_latefee.Text = "";
        txt_Monthly_Service_Charge.Text = "";
        txt_paymentdue.Text = "";
        txt_standardDeposit.Text = "";

        //clear our datefield
        txt_billdate.Clear();

        //rebind our tables
        //radChart.Visible = false;
    }

    protected void clearFields_Form()
    {
        //clear our input fields
        txt_graceperiod.Text = "";
        txt_latefee.Text = "";
        txt_Monthly_Service_Charge.Text = "";
        txt_paymentdue.Text = "";
        txt_standardDeposit.Text = "";

        //clear our datefield
        txt_billdate.Clear();

        //rebind our tables
        //radChart.Visible = false;
    }

    protected void btn_cancel_Click(object sender, EventArgs e)
    {
        clearFields_All();
        txtProgName.Enabled = false;
        pageToggle(false);
    }

    protected void writeMessage(String outMessage)
    {
        lbl_message.Text = outMessage;
    }

    protected void btn_view_Click(object sender, EventArgs e)
    {
        clearFields_Form();

        if (txtProgName.Text == "Autocomplete..." || txtProgName.Text == "")
        {
            writeMessage("You did not select a valid Program Name.");
            return;
        }

        int progID = touUtility.retrieveProgamID(sqlcon, txtProgName.Text);
        DataTable dt = new DataTable();

        try
        {
            sqlcon.Open();
            using (SqlDataAdapter sqladapter = new SqlDataAdapter("SELECT * FROM touProgramsInfo WHERE progID=@progID", sqlcon))
            {
                sqladapter.SelectCommand.Parameters.AddWithValue("@progID", progID);
                sqladapter.Fill(dt);
            }
        }
        catch (Exception ex) { }
        finally
        {
            sqlcon.Close();
        }

        pageToggle(true);
        if (dt.Rows.Count < 1)
        {
            writeMessage("There was no previous data.");
        }
        else
        {
            ddl_fuelcode.SelectedValue = dt.Rows[0][2].ToString();
            ddl_taxcode.SelectedValue = dt.Rows[0][3].ToString();

            txt_billdate.SelectedDate = Convert.ToDateTime(dt.Rows[0][4]);

            txt_paymentdue.Text = dt.Rows[0][5].ToString();
            txt_graceperiod.Text = dt.Rows[0][6].ToString();
            txt_latefee.Text = dt.Rows[0][7].ToString();
            txt_Monthly_Service_Charge.Text = dt.Rows[0][8].ToString();
            txt_standardDeposit.Text = dt.Rows[0][10].ToString();

            //txt_deposit.Text = dt.Rows[0][2].ToString();
            
            writeMessage("Saved data has been loaded.");
            displayProgDetails(progID.ToString());
            //radChart.Visible = true;
        }
    }

    private void seasonsBind(string PROGRAM_id)
    {
        try
        {
            string Que_get_season = "select season_id,season_name from touSeasonsEP where program_id = " + PROGRAM_id + " order by season_id desc";
            cmd = new SqlCommand(Que_get_season, sqlcon);
            sqlcon.Open();
            dr = cmd.ExecuteReader();

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
            dr.Dispose();

            cmd.Dispose();
            sqlcon.Close();
        }
    }

    private void peaksBind(int season_id, string PROGRAM_id)
    {
        try
        {
            string Select_Peaks = " select distinct a.et_peaktype_id as et_peaktype_id ,b.peakType as et_peaktype ";
            Select_Peaks += " from touPeaksEP a, touPeakTypes b  where a.programID =" + PROGRAM_id + "";
            Select_Peaks += " and a.seasonID =" + season_id.ToString() + "  and a.et_peaktype_id = b.id";

            cmd = new SqlCommand(Select_Peaks, sqlcon);
            if (ConnectionState.Closed == sqlcon.State)
                sqlcon.Open();
            dr = cmd.ExecuteReader();
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
            dr.Dispose();
            cmd.Dispose();
            sqlcon.Close();
        }
    }

    public void displayProgDetails(string PROGRAM_id)
    {
        //PROGRAM_id = ddl_programname.SelectedValue;
        string category_name = "", Prog_Status = "", Prog_Service_crg = "";
        string program_type = "", Program_name = "", _Fuel_Adjustmentcharge = "", _taxid = "";

        Label _lb_design = new Label();
        seasonsBind(PROGRAM_id);
        //string sql = "select c.category_name from ET_PROGRAMS p, ET_PRODUCT_CATEGORY c where c.category_id = p.category_id and p.program_id=" + ddl_programname.SelectedValue;
        string sql = "select pr.progName,pt.typeName,fa.fuelCharge,p.serviceCharge " +
            " as Service_charge,ta.id from touProgramsInfo p, touPrograms pr, progType pt, touFuelAdjust fa, touTaxAdjust ta " +
            "where p.progID=pr.id and pr.progTypeID=pt.ID and p.fuelAdjustID=fa.id and p.taxAdjustID=ta.id and p.progID=" + PROGRAM_id;

        cmd = new SqlCommand(sql, sqlcon);
        if (ConnectionState.Closed == sqlcon.State)
        {
            sqlcon.Open();
        }
        dr = cmd.ExecuteReader();
        while (dr.Read())
        {
            Program_name = dr["progName"].ToString();
            Prog_Service_crg = dr["Service_charge"].ToString();
            PROGRAM_type = dr["typeName"].ToString();
            _Fuel_Adjustmentcharge = dr["fuelCharge"].ToString();
            _taxid = dr["id"].ToString();
        }
        //category_name = Convert.ToString(cmd.ExecuteScalar());

        cmd.Dispose();
        sqlcon.Close();


        displayDetails.Controls.Add(new LiteralControl("<table border='0' style='border:solid 1px #000000' width='700px' class='label'>"));
        displayDetails.Controls.Add(new LiteralControl("<tr><td colspan='2' align='center' style='border-bottom:solid 1px #000000;font-family:Verdana;font-size:10px;font-weight:bold;'>Program Details</td></tr>"));
        displayDetails.Controls.Add(new LiteralControl("<tr><td style='font-family:Verdana;font-size:10px;'> "));
        displayDetails.Controls.Add(new LiteralControl("<span style=' font-family:Verdana;font-size:10px;font-weight:bold;'> "));
        displayDetails.Controls.Add(new LiteralControl("Program Name: </span>" + Program_name.ToUpper() + "</td>"));
        //<td> </td></tr>"));
        displayDetails.Controls.Add(new LiteralControl("<td style='font-family:Verdana;font-size:10px;font-weight:bold;'> Monthly Service Charge :"));
        double price = 0;
        if (Prog_Service_crg.ToString() != "")
        {
            price = System.Convert.ToDouble(Prog_Service_crg.ToString());
        }
        else
        {
            price = 0.0;
        }
        displayDetails.Controls.Add(new LiteralControl(" " + String.Format("{0:$#,##0.00;($#,##0.00);Zero}", price).ToString() + "</td></tr>"));

        displayDetails.Controls.Add(new LiteralControl("<tr><td style='font-family:Verdana;font-size:10px;' colspan='1'> "));
        displayDetails.Controls.Add(new LiteralControl("<span style=' font-family:Verdana;font-size:10px;font-weight:bold;'>  "));
        displayDetails.Controls.Add(new LiteralControl("Program Type: </span>" + PROGRAM_type + "</td>"));
        displayDetails.Controls.Add(new LiteralControl("<td style=' font-family:Verdana;font-size:10px;font-weight:bold;'>Fuel&#160;Adjustment&#160;Charge :" + _Fuel_Adjustmentcharge.ToString() + "$&nbsp;(per kwh)"));

        displayDetails.Controls.Add(new LiteralControl("&nbsp;&nbsp;"));
        displayDetails.Controls.Add(new LiteralControl("</td></tr>"));

        displayDetails.Controls.Add(new LiteralControl("<tr><td colspan='2'><br/></td></tr>"));

        displayDetails.Controls.Add(new LiteralControl("<tr><td colspan='2' align='center' style='background-color:#4B6C9E;color:#ffffff;font-weight:bold;font-family:verdana;font-size:10px;'>Season Definitions</td></tr>"));
        displayDetails.Controls.Add(new LiteralControl("<tr><td colspan='2' align='center'>"));
        //Season names table
        displayDetails.Controls.Add(new LiteralControl("<table border=1>"));
        displayDetails.Controls.Add(new LiteralControl("<tr align='center'><td style='background-color:#4B6C9E;color:#ffffff;font-weight:bold;font-family:Verdana;font-size:10px;'> Season Name </td><td  style='background-color:#4B6C9E;color:#ffffff;font-weight:bold;font-family:Verdana;font-size:10px;'> Months </td></tr>"));

        //Clear the data in Hashtable which contains the names of the months for a particular Month.
        season_months.Clear();
        foreach (object id in seasons.Keys)
        {
            int season_id = System.Convert.ToInt32(id);
            string season_name = seasons[season_id + ""].ToString();

            displayDetails.Controls.Add(new LiteralControl("<tr><td  style='font-family:Verdana;font-size:10px;'>" + season_name + "</td>"));

            string SeasonsSql = "select * from touSeasonsEP where  program_id=" + PROGRAM_id + " and season_id=" + season_id + " ";
            da = new SqlDataAdapter(SeasonsSql, sqlcon);
            ds = new DataSet();
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
            displayDetails.Controls.Add(new LiteralControl("<td style='font-family:Verdana;font-size:10px;'>" + months + "</td></tr>"));
        }

        displayDetails.Controls.Add(new LiteralControl("</table>"));

        displayDetails.Controls.Add(new LiteralControl("</td><tr>"));

        displayDetails.Controls.Add(new LiteralControl("<tr><td colspan='2'><br/></td></tr>"));
        displayDetails.Controls.Add(new LiteralControl("<tr><td colspan='2' style='background-color:#4B6C9E;color:#ffffff;font-weight:bold;font-family:Verdana;font-size:10px;' align='center'>Season Peak Hour Definitions for Weekdays, Holidays, Stardays and Sundays</td></tr>"));
        displayDetails.Controls.Add(new LiteralControl("<tr><td colspan='2' align='center'>"));
        //Display Season and Peak Definitions
        displayDetails.Controls.Add(new LiteralControl("<table border=1>"));

        string myJavaScript = ""; //declare javascript up here so we only call it once
        foreach (object id in seasons.Keys)
        {
            int season_id = System.Convert.ToInt32(id);
            string season_name = seasons[season_id + ""].ToString();
            string season_monthnames = season_months[season_id + ""].ToString();
            peaksBind(season_id, PROGRAM_id);
            displayDetails.Controls.Add(new LiteralControl("<tr align='center'><td  style='background-color:#4B6C9E;color:#ffffff;font-weight:bold;font-family:Verdana;font-size:10px;'> Season Name: " + season_name + "(" + season_monthnames + ")</td></tr>"));
            displayDetails.Controls.Add(new LiteralControl("<tr><td align='center'>"));
            displayDetails.Controls.Add(new LiteralControl("<table border=1 width='100%' style='font-family:Verdana;font-size:10px;'><tr><td style='font-weight:bold'>Peak Name</td><td style='font-weight:bold'>Weekdays</td><td style='font-weight:bold'>Holidays</td><td style='font-weight:bold'>Saturdays</td><td style='font-weight:bold'>Sundays</td></tr>"));
            placeholder_master.Controls.Add(new LiteralControl("<div style='background-color:#BFD299; display:inline-block; margin: 0 auto;-moz-border-radius: 15px;border-radius: 15px;margin:15px;padding:10px'>"));
            placeholder_master.Controls.Add(new LiteralControl("<div style='font-weight:bold;'>Season Name: " + season_name + "(" + season_monthnames + ")</div>"));
            foreach (object peakid in peaks.Keys)
            {

                int peak_id = System.Convert.ToInt32(peakid);
                string peak_name = peaks[peakid + ""].ToString();


                string PeakHrSql = "select convert(int,et_start_hr) as start,et_week_name, et_start_hr, et_stop_hr from touPeaksEP where  programID=" + PROGRAM_id + " and seasonID=" + season_id + " and et_peaktype_id=" + peak_id + " order by convert(int,et_start_hr)";
                //Response.Write(PeakHrSql);
                //return;
                da = new SqlDataAdapter(PeakHrSql, sqlcon);
                ds = new DataSet();
                da.Fill(ds, "peakdefs");
                string weekdays = "", saturdays = "", sundays = "", holidays = "";
                for (int i = 0; i < ds.Tables["peakdefs"].Rows.Count; i++)
                {
                    string week_type = ds.Tables["peakdefs"].Rows[i]["et_week_name"].ToString();
                    string start_hr = ds.Tables["peakdefs"].Rows[i]["et_start_hr"].ToString();
                    string stop_hr = ds.Tables["peakdefs"].Rows[i]["et_stop_hr"].ToString();
                    int start_hour = System.Convert.ToInt32(start_hr);
                    int stop_hour = System.Convert.ToInt32(stop_hr);

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
                            if (start_hour == 13)
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
                    ////switch (week_type)
                    ////{
                    ////    case "Holidays":
                    ////        {
                    ////            if (holidays.Equals(""))
                    ////                holidays += start_hr + " - " + stop_hr;
                    ////            else
                    ////                holidays += "<br>" + start_hr + " - " + stop_hr;
                    ////            break;
                    ////        }

                    ////    case "Week Days":
                    ////        {
                    ////            if (weekdays.Equals(""))
                    ////                weekdays += start_hr + " - " + stop_hr;
                    ////            else
                    ////                weekdays += "<br>" + start_hr + " - " + stop_hr;
                    ////            break;
                    ////        }
                    ////    case "Saturdays":
                    ////        {
                    ////            if (saturdays.Equals(""))
                    ////                saturdays += start_hr + " - " + stop_hr;
                    ////            else
                    ////                saturdays += "<br>" + start_hr + " - " + stop_hr;
                    ////            break;
                    ////        }
                    ////    case "Sundays":
                    ////        {
                    ////            if (sundays.Equals(""))
                    ////                sundays += start_hr + " - " + stop_hr;
                    ////            else
                    ////                sundays += "<br>" + start_hr + " - " + stop_hr;
                    ////            break;

                    ////        }
                    ////    default:
                    ////        {
                    ////            break;

                    ////        }
                    ////}



                }

                displayDetails.Controls.Add(new LiteralControl("<tr><td>" + peak_name + "</td><td>" + checkEmpty(weekdays) + "</td><td>" + checkEmpty(holidays) + "</td><td>" + checkEmpty(saturdays) + "</td><td>" + checkEmpty(sundays) + "</td></tr>"));

                /*insert time charts here*/
                String[] peakNames = new string[] {"Week Days","Holidays","Saturdays","Sundays"};
                String[] peakValues = new string[] {checkEmpty(weekdays),checkEmpty(holidays),checkEmpty(saturdays),checkEmpty(sundays)};



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
            displayDetails.Controls.Add(new LiteralControl("</table></td></tr>"));
            peaks.Clear();
        }
        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "AutoComplete", myJavaScript, true);
        displayDetails.Controls.Add(new LiteralControl("</table>"));

        displayDetails.Controls.Add(new LiteralControl("</td></tr>"));


        displayDetails.Controls.Add(new LiteralControl("<tr><td colspan='2'><br/></td></tr>"));
        displayDetails.Controls.Add(new LiteralControl("<tr><td colspan='2' align='center' style='background-color:#4B6C9E;color:#ffffff;font-weight:bold;font-family:Verdana;font-size:10px;'>Peak Rates for different Slabs</td></tr>"));


        displayDetails.Controls.Add(new LiteralControl("<tr><td colspan='2' align='center' style=' font-family:Verdana;font-size:10px;'>"));
        //Display Season and Peak Definitions
        displayDetails.Controls.Add(new LiteralControl("<table border=1>"));
        foreach (object id in seasons.Keys)
        {
            int season_id = System.Convert.ToInt32(id);
            string season_name = seasons[season_id + ""].ToString();
            string season_monthnames = season_months[season_id + ""].ToString();
            peaksBind(season_id, PROGRAM_id);
            displayDetails.Controls.Add(new LiteralControl("<tr align='center'><td  style='background-color:#4B6C9E;color:#ffffff;font-weight:bold;font-family:Verdana;font-size:10px;'> Season Name: " + season_name + "(" + season_monthnames + ")</td></tr>"));
            displayDetails.Controls.Add(new LiteralControl("<tr><td align='center'>"));
            displayDetails.Controls.Add(new LiteralControl("<table border=1><tr><td  style='font-weight:bold'>Start kWh</td><td  style='font-weight:bold''>Stop kWh</td>"));
            foreach (object peakid in peaks.Keys)
            {

                int peak_id = System.Convert.ToInt32(peakid);
                string peak_name = peaks[peakid + ""].ToString();
                displayDetails.Controls.Add(new LiteralControl("<td style='font-weight:bold'>" + peak_name + "($)</td>"));

            }
            displayDetails.Controls.Add(new LiteralControl("</tr>"));
            //try{
            string PeakSlabSql = "select distinct convert(int,slab_start) as start, slab_start,slab_stop from et_slab_rates where program_id=" + PROGRAM_id + " and season_id=" + season_id + " order by convert(int,slab_start)";
            da = new SqlDataAdapter(PeakSlabSql, sqlcon);
            if (ConnectionState.Closed == sqlcon.State)
            {
                sqlcon.Open();
            }
            DataSet ds1 = new DataSet();
            da.Fill(ds1, "slabs");

            ChartSeries[] chartSeries = new ChartSeries[peaks.Count];
            for (int i = 0; i < chartSeries.Count(); i++)
            {
                chartSeries[i] = new ChartSeries();
                chartSeries[i].Type = ChartSeriesType.Bar;
            }

            //init DataTable for Charting purposes
            DataTable dt_ChartInfo = new DataTable();
            dt_ChartInfo.Columns.Add("Start kWh");
            dt_ChartInfo.Columns.Add("Stop kWh");
            foreach (object peakid in peaks.Keys)
                dt_ChartInfo.Columns.Add(peaks[peakid + ""] as String);

            for (int i = 0; i < ds1.Tables["slabs"].Rows.Count; i++)
            {

                string startKwh = ds1.Tables["slabs"].Rows[i]["slab_start"].ToString();
                string stopKwh = ds1.Tables["slabs"].Rows[i]["slab_stop"].ToString();
                displayDetails.Controls.Add(new LiteralControl("<tr><td>" + startKwh + "</td><td>" + stopKwh + "</td>"));

                //init DataRow for Charting purposes
                DataRow dr_ChartInfo = dt_ChartInfo.NewRow();
                dr_ChartInfo[0] = startKwh;
                dr_ChartInfo[1] = stopKwh;

                int count = 2;
                foreach (object peakid in peaks.Keys)
                {
                    int peak_id = System.Convert.ToInt32(peakid);
                    string peak_name = peaks[peakid + ""].ToString();

                    string PeakRateSql = "select peak_rate from et_slab_rates where program_id=" + PROGRAM_id + " and season_id=" + season_id + " and et_peaktype_id=" + peak_id + " and slab_start = '" + startKwh + "' and slab_stop = '" + stopKwh + "'";
                    da = new SqlDataAdapter(PeakRateSql, sqlcon);
                    if (ConnectionState.Closed == sqlcon.State)
                    {
                        sqlcon.Open();
                    }
                    ds = new DataSet();
                    da.Fill(ds, "peakrates");
                    for (int j = 0; j < ds.Tables["peakrates"].Rows.Count; j++)
                    {
                        string PeakRate = ds.Tables["peakrates"].Rows[j]["peak_rate"].ToString();
                        displayDetails.Controls.Add(new LiteralControl("<td>" + PeakRate + "</td>"));

                        dr_ChartInfo[count++] = PeakRate;
                    }
                }
                dt_ChartInfo.Rows.Add(dr_ChartInfo);
                displayDetails.Controls.Add(new LiteralControl("</tr>"));
            }
            displayDetails.Controls.Add(new LiteralControl("</table></td></tr>"));
            String season_full = season_name + "(" + season_monthnames + ")";
            if (season_full.Count() > 40)
                createChart(season_name, peaks, dt_ChartInfo);
            else
                createChart(season_full, peaks, dt_ChartInfo);
            peaks.Clear();
        }
        displayDetails.Controls.Add(new LiteralControl("</table>"));
        displayDetails.Controls.Add(new LiteralControl("</td></tr>"));
        displayDetails.Controls.Add(new LiteralControl("</table>"));

        //return _lb_design;

    }

    public void displayProgDetails_backup(int programID)
    {
        string category_name = "", Prog_Status = "", Prog_Service_crg = "";
        string program_type = "", Program_name = "", _Fuel_Adjustmentcharge = "", _taxid = "";


        //create seasons HashTable
        DataTable dt_assSeason = touUtility.dtblfillAssignedSeasons(sqlcon, programID);
        DataTable dt_season = touUtility.dtblfillSeasons(sqlcon);
        Hashtable seasons = new Hashtable();
        foreach(DataRow drow in dt_season.Rows)
        {
            foreach (DataRow drow_sub in dt_assSeason.Rows)
            {
                if(drow_sub["seasonID"].ToString() == drow["ID"].ToString())
                    seasons.Add(drow["ID"].ToString(), drow["seasonName"].ToString());
            }
        }
        //end seasons HashTable

        //assign global vars inside here
        SqlCommand cmd = new SqlCommand();
        SqlDataReader dr;
        SqlDataAdapter da;
        DataSet ds;
        string PROGRAM_type = "";

        Hashtable season_months = new Hashtable();
        //end global vars


        Label _lb_design = new Label();
        string sql = "select prog.progName, progType.typeName, fuel.fuelCharge, fuel.fuelCharge, proginf.serviceCharge, tax.ID  " +
                     "from touPrograms prog " +
                     "inner join progType on progType.ID=prog.progTypeID " +
                     "inner join touProgramsInfo proginf on prog.ID=proginf.progID " +
                     "inner join touFuelAdjust fuel on fuel.ID=proginf.fuelAdjustID " +
                     "inner join touTaxAdjust tax on tax.ID=proginf.taxAdjustID " +
                     "where prog.id='" + programID + "'";

        cmd = new SqlCommand(sql, sqlcon);
        if (ConnectionState.Closed == sqlcon.State)
        {
            sqlcon.Open();
        }
        dr = cmd.ExecuteReader();
        while (dr.Read())
        {
            Program_name = dr["progName"].ToString();
            category_name = "remove me";
            Prog_Status = "remove me";
            Prog_Service_crg = dr["serviceCharge"].ToString();
            PROGRAM_type = dr["typeName"].ToString();
            _Fuel_Adjustmentcharge = dr["fuelCharge"].ToString();
            _taxid = dr["ID"].ToString();
        }
        cmd.Dispose();
        sqlcon.Close();


        displayDetails.Controls.Add(new LiteralControl("<table border='0' style='border:solid 1px #000000' width='700px' class='label'>"));
        displayDetails.Controls.Add(new LiteralControl("<tr><td colspan='2' align='center' style='border-bottom:solid 1px #000000;font-family:Verdana;font-size:10px;font-weight:bold;'>Program Details</td></tr>"));
        displayDetails.Controls.Add(new LiteralControl("<tr><td  style='font-family:Verdana;font-size:10px;'> "));
        displayDetails.Controls.Add(new LiteralControl("<span style=' font-family:Verdana;font-size:10px;font-weight:bold;'>"));
        displayDetails.Controls.Add(new LiteralControl("Program Category: </span> " + category_name + "</td>"));
        //<td> </td></tr>"));
        displayDetails.Controls.Add(new LiteralControl("<td style='font-family:Verdana;font-size:10px;font-weight:bold;'> Monthly Service Charge :"));
        double price = 0;
        if (Prog_Service_crg.ToString() != "")
        {
            price = System.Convert.ToDouble(Prog_Service_crg.ToString());
        }
        else
        {
            price = 0.0;
        }
        displayDetails.Controls.Add(new LiteralControl(" " + String.Format("{0:$#,##0.00;($#,##0.00);Zero}", price).ToString() + "</td></tr>"));

        displayDetails.Controls.Add(new LiteralControl("<tr><td style='font-family:Verdana;font-size:10px;'> "));
        displayDetails.Controls.Add(new LiteralControl("<span style=' font-family:Verdana;font-size:10px;font-weight:bold;'> "));
        displayDetails.Controls.Add(new LiteralControl("Program Name: </span>" + Program_name.ToUpper() + "</td>"));
        displayDetails.Controls.Add(new LiteralControl("<td style=' font-family:Verdana;font-size:10px;font-weight:bold;'>Fuel&#160;Adjustment&#160;Charge :" + _Fuel_Adjustmentcharge.ToString() + "$&nbsp;(per kwh)"));
        displayDetails.Controls.Add(new LiteralControl("&nbsp;&nbsp;"));
        displayDetails.Controls.Add(new LiteralControl("</td></tr>"));

        displayDetails.Controls.Add(new LiteralControl("<tr><td style='font-family:Verdana;font-size:10px;' colspan='1'> "));
        displayDetails.Controls.Add(new LiteralControl("<span style=' font-family:Verdana;font-size:10px;font-weight:bold;'>  "));
        displayDetails.Controls.Add(new LiteralControl("Program Type: </span>" + PROGRAM_type + "</td>"));

        displayDetails.Controls.Add(new LiteralControl("</tr>"));

        displayDetails.Controls.Add(new LiteralControl("<tr><td colspan='2'><br/></td></tr>"));

        displayDetails.Controls.Add(new LiteralControl("<tr><td colspan='2' align='center' style='background-color:#4B6C9E;color:#ffffff;font-weight:bold;font-family:verdana;font-size:10px;'>Season Definitions</td></tr>"));
        displayDetails.Controls.Add(new LiteralControl("<tr><td colspan='2' align='center'>"));

        displayDetails.Controls.Add(new LiteralControl("<table border=1>"));
        displayDetails.Controls.Add(new LiteralControl("<tr align='center'><td style='background-color:#4B6C9E;color:#ffffff;font-weight:bold;font-family:Verdana;font-size:10px;'> Season Name </td><td  style='background-color:#4B6C9E;color:#ffffff;font-weight:bold;font-family:Verdana;font-size:10px;'> Months </td></tr>"));


        season_months.Clear();
        foreach (object id in seasons.Keys)
        {
            int season_id = System.Convert.ToInt32(id);
            string season_name = seasons[season_id + ""].ToString();

            displayDetails.Controls.Add(new LiteralControl("<tr><td  style='font-family:Verdana;font-size:10px;'>" + season_name + "</td>"));

            string SeasonsSql = "select seasonID, progID, seasonName, startMonth, endMonth from touSeasonProgram " +
                                "left join touSeasons on touSeasons.ID=touSeasonProgram.seasonID " +
                                "left join touPrograms on touPrograms.ID=touSeasonProgram.progID " +
                                "where  touSeasonProgram.progID='" + programID + "' and touSeasonProgram.seasonID='" + season_id + "' ";
            da = new SqlDataAdapter(SeasonsSql, sqlcon);
            ds = new DataSet();
            da.Fill(ds, "season_names");
            string startMonth = "", endMonth = "";
            string months = "";

            string[] strMonths = new string[12] {"January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December"};
            for (int i = 0; i < ds.Tables["season_names"].Rows.Count; i++)
            {
                startMonth = ds.Tables["season_names"].Rows[i]["startMonth"].ToString();
                endMonth = ds.Tables["season_names"].Rows[i]["endMonth"].ToString();

                bool begin = false;
                for(int x=0;x<12;x++)
                {
                    if(strMonths[x].Trim()==startMonth.Trim())
                        begin = true;

                    if(begin)
                    {
                        if (months.Equals(""))
                            months += strMonths[x];
                        else
                            months += ", " + strMonths[x];
                    }
                    
                    if(strMonths[x].Trim()==endMonth.Trim())
                        break;

                    if(x == 11)
                        x=0;
                }
            }
            season_months.Add(season_id + "", months);
            displayDetails.Controls.Add(new LiteralControl("<td style='font-family:Verdana;font-size:10px;'>" + months + "</td></tr>"));
        }

        displayDetails.Controls.Add(new LiteralControl("</table>"));

        displayDetails.Controls.Add(new LiteralControl("</td><tr>"));

        displayDetails.Controls.Add(new LiteralControl("<tr><td colspan='2'><br/></td></tr>"));
        displayDetails.Controls.Add(new LiteralControl("<tr><td colspan='2' style='background-color:#4B6C9E;color:#ffffff;font-weight:bold;font-family:Verdana;font-size:10px;' align='center'>Season Peak Hour Definitions for Weekdays, Holidays, Stardays and Sundays</td></tr>"));
        displayDetails.Controls.Add(new LiteralControl("<tr><td colspan='2' align='center'>"));

        displayDetails.Controls.Add(new LiteralControl("<table border=1>"));
        foreach (object id in seasons.Keys)
        {
            int season_id = System.Convert.ToInt32(id);
            string season_name = seasons[season_id + ""].ToString();
            string season_monthnames = season_months[season_id + ""].ToString();
            DataTable dt_peaks = touUtility.peaksBind(new SqlConnection(Session["client_database"].ToString()), season_id, programID);
            Hashtable peaks = new Hashtable();
            foreach (DataRow drow in dt_peaks.Rows)
            {
                peaks.Add(drow["id"].ToString(), drow["peakType"].ToString());
            }
            displayDetails.Controls.Add(new LiteralControl("<tr align='center'><td  style='background-color:#4B6C9E;color:#ffffff;font-weight:bold;font-family:Verdana;font-size:10px;'> Season Name: " + season_name + "(" + season_monthnames + ")</td></tr>"));
            displayDetails.Controls.Add(new LiteralControl("<tr><td align='center'>"));
            //displayDetails.Controls.Add(new LiteralControl("<table border=1 width='100%' style='font-family:Verdana;font-size:10px;'><tr><td style='font-weight:bold'>Peak Name</td><td style='font-weight:bold'>Weekdays</td><td style='font-weight:bold'>Holidays</td><td style='font-weight:bold'>Saturdays</td><td style='font-weight:bold'>Sundays</td></tr>"));
            displayDetails.Controls.Add(new LiteralControl("<table border=1 width='100%' style='font-family:Verdana;font-size:10px;'><tr><td style='font-weight:bold'>Peak Name</td>"));

            DataTable dt_daytypes = touUtility.dtblfillDayTypes(new SqlConnection(Session["client_database"].ToString()), season_id);
            Hashtable daytypes = new Hashtable();
            foreach (DataRow drow in dt_daytypes.Rows)
                daytypes.Add(drow["ID"].ToString(), drow["name"].ToString());

            foreach (object daytypeid in daytypes.Keys)
                displayDetails.Controls.Add(new LiteralControl("<td style='font-weight:bold'>" + daytypes[daytypeid] as String + "</td>"));
            displayDetails.Controls.Add(new LiteralControl("</tr>"));
            foreach (object peakid in peaks.Keys)
            {

                int peak_id = System.Convert.ToInt32(peakid);
                string peak_name = peaks[peakid + ""].ToString();


                string PeakHrSql = "select convert(int,startHR) as start, (select name from touDayDefinitions WHERE touDayDefinitions.id=touPeaks.dayTypeID) as name, startHR, stopHR from touPeaks where seasonID=" + season_id + " and peakTypeID=" + peak_id + " order by convert(int,startHR)";
                da = new SqlDataAdapter(PeakHrSql, sqlcon);
                ds = new DataSet();
                da.Fill(ds, "peakdefs");
                string weekdays = "", saturdays = "", sundays = "", holidays = "";

                Hashtable hash_peakDefs = new Hashtable();
                for (int i = 0; i < ds.Tables["peakdefs"].Rows.Count; i++)
                {
                    string week_type = ds.Tables["peakdefs"].Rows[i]["name"].ToString();
                    string start_hr = ds.Tables["peakdefs"].Rows[i]["startHR"].ToString();
                    string stop_hr = ds.Tables["peakdefs"].Rows[i]["stopHR"].ToString();
                    int start_hour = System.Convert.ToInt32(start_hr);
                    int stop_hour = System.Convert.ToInt32(stop_hr);

                    string stt_hour = "";
                    string stp_hour = "";
                    string Value = "";
                    if (start_hour == 1 && stop_hour == 24)
                        Value = "Whole Day(24 hrs)";
                    else
                    {
                        if (start_hour <= 12)
                        {
                            if (start_hour == 1)
                                stt_hour = "Mid Night";

                            else
                                stt_hour = (start_hour - 1) + " AM";
                        }
                        else
                        {
                            if (start_hour == 13)
                                stt_hour = "12 PM";
                            else
                                stt_hour = (start_hour - 13) + " PM";
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

                    hash_peakDefs.Add(week_type, Value);

                    //switch (week_type)
                    //{
                    //    case "Holidays":
                    //        {
                    //            if (holidays.Equals(""))
                    //                holidays += Value;
                    //            else
                    //                holidays += "<br>" + Value;
                    //            break;
                    //        }

                    //    case "Week Days":
                    //        {
                    //            if (weekdays.Equals(""))
                    //                weekdays += Value;
                    //            else
                    //                weekdays += "<br>" + Value;
                    //            break;
                    //        }
                    //    case "Saturdays":
                    //        {
                    //            if (saturdays.Equals(""))
                    //                saturdays += Value;
                    //            else
                    //                saturdays += "<br>" + Value;
                    //            break;
                    //        }
                    //    case "Sundays":
                    //        {
                    //            if (sundays.Equals(""))
                    //                sundays += Value;
                    //            else
                    //                sundays += "<br>" + Value;
                    //            break;

                    //        }
                    //    default:
                    //        {
                    //            break;

                    //        }
                    //}
                }
                displayDetails.Controls.Add(new LiteralControl("<tr><td>" + peak_name + "</td>"));
                foreach (object dtID in daytypes.Keys)
                {
                    string Value = "";
                    foreach (object pdID in hash_peakDefs.Keys)
                    {
                        if (daytypes[dtID].ToString().Trim() == pdID.ToString().Trim())
                            Value = hash_peakDefs[pdID] as String;
                    }
                    displayDetails.Controls.Add(new LiteralControl("<td>" + checkEmpty(Value) + "</td>"));
                }
                displayDetails.Controls.Add(new LiteralControl("</tr>"));

            }
            displayDetails.Controls.Add(new LiteralControl("</table></td></tr>"));
            peaks.Clear();
        }
        displayDetails.Controls.Add(new LiteralControl("</table>"));

        displayDetails.Controls.Add(new LiteralControl("</td></tr>"));
        displayDetails.Controls.Add(new LiteralControl("<tr><td colspan='2'><br/></td></tr>"));
        displayDetails.Controls.Add(new LiteralControl("<tr><td colspan='2' align='center' style='background-color:#4B6C9E;color:#ffffff;font-weight:bold;font-family:Verdana;font-size:10px;'>Peak Rates for different Slabs</td></tr>"));


        displayDetails.Controls.Add(new LiteralControl("<tr><td colspan='2' align='center' style=' font-family:Verdana;font-size:10px;'>"));
        //Display Season and Peak Definitions
        displayDetails.Controls.Add(new LiteralControl("<table border=1>"));
        foreach (object id in seasons.Keys)
        {
            int season_id = System.Convert.ToInt32(id);
            string season_name = seasons[season_id + ""].ToString();
            string season_monthnames = season_months[season_id + ""].ToString();
            DataTable dt_peaks = touUtility.peaksBind(new SqlConnection(Session["client_database"].ToString()), season_id, programID);
            Hashtable peaks = new Hashtable();
            foreach (DataRow drow in dt_peaks.Rows)
            {
                peaks.Add(drow["id"].ToString(), drow["peakType"].ToString());
            }
            displayDetails.Controls.Add(new LiteralControl("<tr align='center'><td  style='background-color:#4B6C9E;color:#ffffff;font-weight:bold;font-family:Verdana;font-size:10px;'> Season Name: " + season_name + "(" + season_monthnames + ")</td></tr>"));
            displayDetails.Controls.Add(new LiteralControl("<tr><td align='center'>"));
            displayDetails.Controls.Add(new LiteralControl("<table border=1><tr><td  style='font-weight:bold'>Start kWh</td><td  style='font-weight:bold''>Stop kWh</td>"));
            foreach (object peakid in peaks.Keys)
            {

                int peak_id = System.Convert.ToInt32(peakid);
                string peak_name = peaks[peakid + ""].ToString();
                displayDetails.Controls.Add(new LiteralControl("<td style='font-weight:bold'>" + peak_name + "($)</td>"));

            }
            displayDetails.Controls.Add(new LiteralControl("</tr>"));
            //try{
            string PeakSlabSql = "select distinct convert(int,slabStart) as start, slabStart,slabStop from touSlabRates where programID=" + programID + " and seasonID=" + season_id + " order by convert(int,slabStart)";
            da = new SqlDataAdapter(PeakSlabSql, sqlcon);
            if (ConnectionState.Closed == sqlcon.State)
            {
                sqlcon.Open();
            }
            DataSet ds1 = new DataSet();
            da.Fill(ds1, "slabs");
            for (int i = 0; i < ds1.Tables["slabs"].Rows.Count; i++)
            {

                string startKwh = ds1.Tables["slabs"].Rows[i]["slabStart"].ToString();
                string stopKwh = ds1.Tables["slabs"].Rows[i]["slabStop"].ToString();
                displayDetails.Controls.Add(new LiteralControl("<tr><td>" + startKwh + "</td><td>" + stopKwh + "</td>"));


                foreach (object peakid in peaks.Keys)
                {

                    int peak_id = System.Convert.ToInt32(peakid);
                    string peak_name = peaks[peakid + ""].ToString();


                    string PeakRateSql = "select peakRate from touSlabRates where programID=" + programID + " and seasonID=" + season_id + " and peakTypeID=" + peak_id + " and slabStart = '" + startKwh + "' and slabStop = '" + stopKwh + "'";
                    da = new SqlDataAdapter(PeakRateSql, sqlcon);
                    if (ConnectionState.Closed == sqlcon.State)
                    {
                        sqlcon.Open();
                    }
                    ds = new DataSet();
                    da.Fill(ds, "peakrates");
                    for (int j = 0; j < ds.Tables["peakrates"].Rows.Count; j++)
                    {

                        string PeakRate = ds.Tables["peakrates"].Rows[j]["peakRate"].ToString();
                        displayDetails.Controls.Add(new LiteralControl("<td>" + PeakRate + "</td>"));

                    }


                }
                displayDetails.Controls.Add(new LiteralControl("</tr>"));
            }
            displayDetails.Controls.Add(new LiteralControl("</table></td></tr>"));


            peaks.Clear();
        }
        displayDetails.Controls.Add(new LiteralControl("</table>"));
        displayDetails.Controls.Add(new LiteralControl("</td></tr>"));
        displayDetails.Controls.Add(new LiteralControl("</table>"));
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
}