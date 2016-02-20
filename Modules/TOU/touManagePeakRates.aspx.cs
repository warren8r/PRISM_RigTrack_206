using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using ToU;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Drawing;
using Telerik.Charting;
using Telerik.Charting.Styles;

public partial class Modules_TOU_touManagePeakRates : System.Web.UI.Page
{
    SqlConnection con;
    SqlCommand cmd;
    SqlDataAdapter da;
    SqlDataReader dr;
    DataSet ds;
    //static int u = 0;
    Hashtable seasons = new Hashtable();
    Hashtable peaks = new Hashtable();
    TextBox peak_cost, txt_stopkwh, txt_startkwh;
    Label peak_id, Peak, lbl_seasonname, lbl_seasonid;

    Hashtable season_months = new Hashtable();
    protected void Page_Load(object sender, EventArgs e)
    {
        con = new SqlConnection(Session["client_database"].ToString());
        writeMessage("");

        if (Page.IsPostBack)
        {
            if (txtProgName.SelectedIndex >= 0)
                bin_details();
        }
        if (!IsPostBack)
        {
            //CollapseAll(trv_admin);
            dsm_program_bond();
            Bind_Programs();
        }
    }

    protected void createChart(String chartTitle, Hashtable peaks, DataTable data)
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
        radChart.Clear();
        radChart.ChartTitle.TextBlock.Text = chartTitle;
        //String fontName = radChart.ChartTitle.TextBlock.Appearance.TextProperties.Font.Name;
        //radChart.ChartTitle.TextBlock.Appearance.TextProperties.Font = new Font(fontName, 12);
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
        for (int i = 0; i < data.Rows.Count; i++)
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

    public void Bind_Programs()
    {
        string Que_Get_programs = "select distinct  PR.progName, PE.programID,PT.typeName as PROGRAM_type ";
        Que_Get_programs += " from  dbo.touPeaksEP PE,dbo.touPrograms PR,ET_SLAB_RATES ES, progType PT  where PE.programID=PR.ID and ES.PROGRAM_id=PE.programID and PR.progTypeID=PT.ID";
        da = new SqlDataAdapter(Que_Get_programs, con);
        ds = new DataSet();
        da.Fill(ds, "PROGRAMS_PEAKS");
        if (ds.Tables["PROGRAMS_PEAKS"].Rows.Count > 0)
        {
            Gv_Prog_Peaks.DataSource = ds.Tables["PROGRAMS_PEAKS"];
            Gv_Prog_Peaks.DataBind();

        }
        else
        {
            error.Text = "No Records Found";
            error.ForeColor = Color.Red;

        }



    }
    public void dsm_program_bond()
    {
        string Program_type = "", Select_Programs = "";

        if (txtProgType.SelectedIndex > -1)
            Program_type = txtProgType.SelectedValue;

        Select_Programs = "select id,progName from touPrograms where id in (select distinct programID from touPeaksEP) and id not in (select distinct program_id from et_slab_rates) and  progTypeID='" + Program_type.Trim() + "'";
        da = new SqlDataAdapter(Select_Programs, con);
        ds = new DataSet();
        da.Fill(ds, "Programs");
        //ddl_programname.DataTextField = "PROGRAM_name";
        //ddl_programname.DataValueField = "PROGRAM_id";
        //ddl_programname.DataSource = ds;
        //ddl_programname.DataBind();
        //ddl_programname.Items.Insert(0, "-- Select --");
        panel_cont.Visible = false;
        btn_add.Visible = false;
        btnClear.Visible = false;
        for (int i = 0; i < Gv_Prog_Peaks.Rows.Count; i++)
        {
            Gv_Prog_Peaks.Rows[i].BackColor = Color.White;
        }

    }
    protected void rbtn_business_CheckedChanged(object sender, EventArgs e)
    {
        dsm_program_bond();
    }
    protected void rbtn_residential_CheckedChanged(object sender, EventArgs e)
    {
        dsm_program_bond();
    }
    public void bin_details()
    {
        int check_read = 0;


        try
        {
            string Que_get_season = "select season_id,season_name from touSeasonsEP where program_id = " + txtProgName.SelectedValue.ToString() + " order by season_id desc";
            cmd = new SqlCommand(Que_get_season, con);
            con.Open();
            dr = cmd.ExecuteReader();

            panel_cont.Controls.Add(new LiteralControl("<table border='0' width=100%>"));

            while (dr.Read())
            {
                check_read++;
                seasons.Add(dr["season_id"].ToString(), dr["season_name"].ToString());

            }
        }
        catch (Exception ex)
        {
        }
        finally
        {
            if (check_read > 0)
            {
                dr.Dispose();
            }
            cmd.Dispose();
            con.Close();
        }
        Peak_details_bind();
        panel_cont.Controls.Add(new LiteralControl("</table>"));

    }
    public void Peak_details_bind()
    {
        int length = seasons.Keys.Count;


        foreach (object inValue in seasons.Keys)
        {
            int season_id = Convert.ToInt32(inValue);
            string season_name = (string)seasons[season_id + ""];



            //header season addition
            panel_cont.Controls.Add(new LiteralControl("<tr ><td align='center'>"));

            panel_cont.Controls.Add(new LiteralControl("<table cellpadding='0' cellspacing='0' style='border:solid 1px #000000'><tr>"));

            lbl_seasonname = new Label();
            lbl_seasonid = new Label();
            lbl_seasonid.Text = season_id.ToString();
            lbl_seasonid.Font.Size = FontUnit.XXSmall;
            lbl_seasonid.Font.Name = "verdana";
            lbl_seasonname.Text = season_name.ToString();
            lbl_seasonname.Font.Size = 10;
            lbl_seasonname.Font.Bold = true;

            lbl_seasonname.Font.Name = "verdana";
            panel_cont.Controls.Add(new LiteralControl("<td align='center' colspan=3 style='background-color:#4B6C9E; color:#ffffff'>"));
            panel_cont.Controls.Add(lbl_seasonname);
            panel_cont.Controls.Add(new LiteralControl("</td>"));

            //season id disaplay

            //panel_cont.Controls.Add(new LiteralControl("<td align='center' colspan=3>"));
            //panel_cont.Controls.Add(lbl_seasonid);
            //panel_cont.Controls.Add(new LiteralControl("</td>"));

            panel_cont.Controls.Add(new LiteralControl("</tr>"));


            panel_cont.Controls.Add(new LiteralControl("<tr><td style='color:blue;font-family:Verdana;font-size:10px;'> "));
            panel_cont.Controls.Add(new LiteralControl("<table border='0'><tr><td>Start kWh</td></tr>"));
            for (int i = 0; i < 5; i++)
            {
                panel_cont.Controls.Add(new LiteralControl("<tr><td>"));
                txt_startkwh = new TextBox();
                txt_startkwh.ID = "txt_startkwh_" + lbl_seasonid.Text + "_" + i;
                string txtstartid = "txt_startkwh_" + lbl_seasonid.Text + "_" + i;
                string start_id = lbl_seasonid.Text + "_" + i;
                if (i == 0)
                {
                    txt_startkwh.Text = i.ToString();
                    //txt_startkwh.ReadOnly = true;
                }
                else
                {
                    //txt_startkwh.ReadOnly = true;
                }
                txt_startkwh.Width = 60;
                txt_startkwh.Font.Size = FontUnit.XXSmall;
                txt_startkwh.Font.Name = "verdana";

                txt_startkwh.Attributes.Add("onkeydown", "javascript:return isNumeric(event.keyCode);");

                txt_startkwh.Attributes.Add("onpaste", "javascript:return false");

                txt_startkwh.Attributes.Add("onkeyup", "javascript:keyUP(event.keyCode)");

                //txt_startkwh.Attributes.Add("onfocusout", "javascript:return getnextvalue('" + txtstartid + "');");

                panel_cont.Controls.Add(txt_startkwh);
                panel_cont.Controls.Add(new LiteralControl("</td></tr>"));
            }
            panel_cont.Controls.Add(new LiteralControl("</table>"));

            panel_cont.Controls.Add(new LiteralControl("</td><td>"));
            panel_cont.Controls.Add(new LiteralControl("<table><tr><td style='color:blue;font-family:Verdana;font-size:10px;'>Stop kWh</td></tr>"));
            for (int j = 0; j < 5; j++)
            {
                panel_cont.Controls.Add(new LiteralControl("<tr><td>"));
                txt_stopkwh = new TextBox();
                txt_stopkwh.ID = "txt_stopkwh_" + lbl_seasonid.Text + "_" + j;
                string txtstartid = "txt_stopkwh_" + lbl_seasonid.Text + "_" + j;
                string start_id = "txt_startkwh_" + lbl_seasonid.Text + "_" + (j + 1);
                txt_stopkwh.Width = 60;
                txt_stopkwh.Font.Size = FontUnit.XXSmall;
                txt_stopkwh.Font.Name = "verdana";

                txt_stopkwh.Attributes.Add("onkeydown", "javascript:return isNumeric(event.keyCode);");

                txt_stopkwh.Attributes.Add("onpaste", "javascript:return false");

                txt_stopkwh.Attributes.Add("onkeyup", "javascript:keyUP(event.keyCode)");
                if (j != 4)
                {
                    txt_stopkwh.Attributes.Add("onfocusout", "javascript:return getnextvalue('" + txtstartid + "','" + start_id + "');");
                }

                panel_cont.Controls.Add(txt_stopkwh);
                panel_cont.Controls.Add(new LiteralControl("</td></tr>"));
            }
            panel_cont.Controls.Add(new LiteralControl("</table>"));
            panel_cont.Controls.Add(new LiteralControl("</td><td>"));

            panel_cont.Controls.Add(new LiteralControl("<table><tr>"));


            // Peak Rates

            try
            {
                string Select_Peaks = " select distinct a.et_peaktype_id as et_peaktype_id ,b.peakType as et_peaktype ";
                Select_Peaks += " from touPeaksEP a, touPeakTypes b  where a.programID =" + txtProgName.SelectedValue.ToString() + "";
                Select_Peaks += " and a.seasonID =" + season_id.ToString() + "  and a.et_peaktype_id = b.id";

                cmd = new SqlCommand(Select_Peaks, con);
                if (ConnectionState.Closed == con.State)
                    con.Open();
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
                con.Close();
            }

            ////


            foreach (object inValue_2 in peaks.Keys)
            {
                panel_cont.Controls.Add(new LiteralControl("<td>"));
                panel_cont.Controls.Add(new LiteralControl("<table border=0>"));
                panel_cont.Controls.Add(new LiteralControl("<tr>"));
                int peaktype_id = Convert.ToInt32(inValue_2);
                string peaktype_name = peaks[inValue_2 + ""].ToString();
                //table formation lable


                /////////////Peak Name
                Peak = new Label();
                Peak.ID = "peakname_" + season_id + "_" + peaktype_id;
                Peak.Text = peaktype_name.ToString() + "($)";
                Peak.Font.Size = FontUnit.XXSmall;
                Peak.Font.Name = "verdana";
                ////////////////Peak Name
                peak_id = new Label();
                peak_id.ID = "peakid_" + season_id + "_" + peaktype_id;
                peak_id.Visible = true;
                peak_id.Text = peaktype_id.ToString();
                peak_id.Font.Size = FontUnit.XXSmall;
                peak_id.Font.Name = "verdana";

                panel_cont.Controls.Add(new LiteralControl("<td >"));
                panel_cont.Controls.Add(Peak);
                //panel_cont.Controls.Add(peak_id);
                panel_cont.Controls.Add(new LiteralControl("</td ></tr>"));

                //////////////////////////////Peak_cost

                for (int j = 0; j < 5; j++)
                {

                    panel_cont.Controls.Add(new LiteralControl("<tr><td align='center'>"));
                    peak_cost = new TextBox();
                    peak_cost.ID = "peakrate_" + season_id + "_" + peaktype_id + "_" + j;
                    peak_cost.Font.Size = FontUnit.XXSmall;
                    peak_cost.Width = System.Web.UI.WebControls.Unit.Pixel(60);
                    peak_cost.Font.Name = "verdana";


                    peak_cost.Attributes.Add("onkeydown", "javascript:return isNumeric(event.keyCode);");

                    peak_cost.Attributes.Add("onpaste", "javascript:return false");

                    peak_cost.Attributes.Add("onkeyup", "javascript:keyUP(event.keyCode)");

                    panel_cont.Controls.Add(peak_cost);

                    panel_cont.Controls.Add(new LiteralControl("</td></tr>"));
                    //panel_cont.Controls.Add(new LiteralControl("<br>"));

                }
                panel_cont.Controls.Add(new LiteralControl("</tr>"));
                panel_cont.Controls.Add(new LiteralControl("</table>"));
                panel_cont.Controls.Add(new LiteralControl("</td>"));

            }
            peaks.Clear();

            panel_cont.Controls.Add(new LiteralControl("</tr></table>"));



            panel_cont.Controls.Add(new LiteralControl("</td>"));

            panel_cont.Controls.Add(new LiteralControl("</tr></table>"));

            panel_cont.Controls.Add(new LiteralControl("</td></tr>"));



        }//foreach



    }//method
    protected void btn_add_Click(object sender, EventArgs e)
    {
        foreach (object inValue in seasons.Keys)
        {
            int season_id = Convert.ToInt32(inValue);
            string season_name = (string)seasons[season_id + ""];

            try
            {
                string Select_Peaks = " select distinct a.et_peaktype_id as et_peaktype_id ,b.peakType as et_peaktype ";
                Select_Peaks += " from touPeaksEP a, touPeakTypes b where a.programID =" + txtProgName.SelectedValue.ToString() + "";
                Select_Peaks += " and a.seasonID =" + season_id.ToString() + "  and a.et_peaktype_id = b.id";

                cmd = new SqlCommand(Select_Peaks, con);
                if (ConnectionState.Closed == con.State)
                    con.Open();
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
                con.Close();
            }

            foreach (object inValue_2 in peaks.Keys)
            {
                int peaktype_id = Convert.ToInt32(inValue_2);
                string peaktype_name = peaks[inValue_2 + ""].ToString();
                try
                {

                    string deleteSql = "delete from ET_SLAB_RATES where program_id=" + txtProgName.SelectedValue.ToString() + " and season_id=" + season_id + " and et_peaktype_id=" + peaktype_id;
                    cmd = new SqlCommand(deleteSql, con);
                    if (ConnectionState.Closed == con.State)
                    {
                        con.Open();
                    }
                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {

                }
                finally
                {
                    cmd.Dispose();
                    con.Close();
                }

                for (int i = 0; i < 5; i++)
                {
                    string start_kwh = ((TextBox)panel_cont.FindControl("txt_startkwh_" + season_id + "_" + i)).Text;
                    string stop_kwh = ((TextBox)panel_cont.FindControl("txt_stopkwh_" + season_id + "_" + i)).Text;


                    if (!start_kwh.Equals(""))
                    {
                        string peak_rate = ((TextBox)panel_cont.FindControl("peakrate_" + season_id + "_" + peaktype_id + "_" + i)).Text;
                        if (stop_kwh.Equals(""))
                        {
                            stop_kwh = "ALL";
                        }

                        try
                        {
                            string sql = "insert into ET_SLAB_RATES (program_id,season_id,et_peaktype_id,slab_start,slab_stop,peak_rate)";
                            sql += "values (" + txtProgName.SelectedValue.ToString() + "," + season_id + "," + peaktype_id + ",'" + start_kwh + "','" + stop_kwh + "','" + peak_rate + "')";
                            cmd = new SqlCommand(sql, con);
                            if (ConnectionState.Closed == con.State)
                            {
                                con.Open();
                            }
                            cmd.ExecuteNonQuery();
                        }
                        catch (Exception ex)
                        {
                        }
                        finally
                        {
                            cmd.Dispose();
                            con.Close();

                        }
                    }



                }
            }


            peaks.Clear();
            clearFields();
            writeMessage("Peak Rates Successfully Applied.");
            Bind_Programs();
        }

    }

    protected void writeMessage(String message)
    {
        lbl_message.Text = message;
    }
    protected void clearFields()
    {
        txtProgName.ClearSelection();
        txtProgType.ClearSelection();
        btn_add.Visible = false;
        btnClear.Visible = false;
        panel_cont.Controls.Clear();
    }
    protected void btn_view_Click(object sender, EventArgs e)
    {
        for (int i = 0; i < Gv_Prog_Peaks.Rows.Count; i++)
        {
            //Gv_Prog_Peaks.Rows[i].BackColor = Color.White;
            Gv_Prog_Peaks.Rows[i].BackColor = System.Drawing.ColorTranslator.FromHtml("#D1DEB6");
        }
        seasons.Clear();
        try
        {
            string Que_get_season = "select season_id,season_name from touSeasonsEP where program_id = " + txtProgName.SelectedValue.ToString() + " order by season_id desc";
            cmd = new SqlCommand(Que_get_season, con);
            con.Open();
            dr = cmd.ExecuteReader();

            panel_cont.Controls.Add(new LiteralControl("<table border=0 width=100%>"));

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
            con.Close();
        }
        foreach (object inValue in seasons.Keys)
        {
            int season_id = Convert.ToInt32(inValue);
            string season_name = (string)seasons[season_id + ""];

            try
            {
                string Select_Peaks = " select distinct a.et_peaktype_id as et_peaktype_id ,b.peakType as et_peaktype ";
                Select_Peaks += " from touPeaksEP a, touPeakTypes b where a.programID =" + txtProgName.SelectedValue.ToString() + "";
                Select_Peaks += " and a.seasonID =" + season_id.ToString() + "  and a.et_peaktype_id = b.id";

                cmd = new SqlCommand(Select_Peaks, con);
                if (ConnectionState.Closed == con.State)
                    con.Open();
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
                con.Close();
            }

            foreach (object inValue_2 in peaks.Keys)
            {
                int peaktype_id = Convert.ToInt32(inValue_2);
                string peaktype_name = peaks[inValue_2 + ""].ToString();

                string sql = "select slab_start,slab_stop,peak_rate from ET_SLAB_RATES where program_id=" + txtProgName.SelectedValue.ToString() + " and season_id=" + season_id + " and et_peaktype_id=" + peaktype_id;
                da = new SqlDataAdapter(sql, con);
                ds = new DataSet();
                da.Fill(ds, "Slab_rates");
                if (ds.Tables["Slab_rates"].Rows.Count != 0)
                {
                    for (int i = 0; i < ds.Tables["Slab_rates"].Rows.Count; i++)
                    {
                        ((TextBox)panel_cont.FindControl("txt_startkwh_" + season_id + "_" + i)).Text = ds.Tables["Slab_rates"].Rows[i]["slab_start"].ToString();
                        ((TextBox)panel_cont.FindControl("txt_stopkwh_" + season_id + "_" + i)).Text = ds.Tables["Slab_rates"].Rows[i]["slab_stop"].ToString();

                        ((TextBox)panel_cont.FindControl("peakrate_" + season_id + "_" + peaktype_id + "_" + i)).Text = ds.Tables["Slab_rates"].Rows[i]["peak_rate"].ToString();
                        ((TextBox)panel_cont.FindControl("txt_startkwh_" + season_id + "_" + i)).Enabled = true;
                        ((TextBox)panel_cont.FindControl("txt_stopkwh_" + season_id + "_" + i)).Enabled = true;

                        ((TextBox)panel_cont.FindControl("peakrate_" + season_id + "_" + peaktype_id + "_" + i)).Enabled = true;
                        ((TextBox)panel_cont.FindControl("txt_startkwh_" + season_id + "_" + i)).ReadOnly = true;
                        ((TextBox)panel_cont.FindControl("txt_stopkwh_" + season_id + "_" + i)).ReadOnly = true;

                        ((TextBox)panel_cont.FindControl("peakrate_" + season_id + "_" + peaktype_id + "_" + i)).ReadOnly = true;
                    }
                    for (int i = ds.Tables["Slab_rates"].Rows.Count; i < 5; i++)
                    {
                        ((TextBox)panel_cont.FindControl("txt_startkwh_" + season_id + "_" + i)).Text = "";
                        ((TextBox)panel_cont.FindControl("txt_stopkwh_" + season_id + "_" + i)).Text = "";

                        ((TextBox)panel_cont.FindControl("peakrate_" + season_id + "_" + peaktype_id + "_" + i)).Text = "";
                        ((TextBox)panel_cont.FindControl("txt_startkwh_" + season_id + "_" + i)).Enabled = false;
                        ((TextBox)panel_cont.FindControl("txt_stopkwh_" + season_id + "_" + i)).Enabled = false;

                        ((TextBox)panel_cont.FindControl("peakrate_" + season_id + "_" + peaktype_id + "_" + i)).Enabled = false;
                    }
                }
                else
                {
                    for (int i = 0; i < 5; i++)
                    {
                        ((TextBox)panel_cont.FindControl("txt_startkwh_" + season_id + "_" + i)).Enabled = true;
                        ((TextBox)panel_cont.FindControl("txt_stopkwh_" + season_id + "_" + i)).Enabled = true;

                        ((TextBox)panel_cont.FindControl("peakrate_" + season_id + "_" + peaktype_id + "_" + i)).Enabled = true;
                        ((TextBox)panel_cont.FindControl("txt_startkwh_" + season_id + "_" + i)).ReadOnly = false;
                        ((TextBox)panel_cont.FindControl("txt_stopkwh_" + season_id + "_" + i)).ReadOnly = false;

                        ((TextBox)panel_cont.FindControl("peakrate_" + season_id + "_" + peaktype_id + "_" + i)).ReadOnly = false;

                        if (i == 0)
                        {
                            ((TextBox)panel_cont.FindControl("txt_startkwh_" + season_id + "_" + i)).Text = "0";
                            ((TextBox)panel_cont.FindControl("txt_stopkwh_" + season_id + "_" + i)).Text = "";

                            ((TextBox)panel_cont.FindControl("peakrate_" + season_id + "_" + peaktype_id + "_" + i)).Text = "";
                        }
                        else
                        {
                            ((TextBox)panel_cont.FindControl("txt_startkwh_" + season_id + "_" + i)).Text = "";
                            ((TextBox)panel_cont.FindControl("txt_stopkwh_" + season_id + "_" + i)).Text = "";

                            ((TextBox)panel_cont.FindControl("peakrate_" + season_id + "_" + peaktype_id + "_" + i)).Text = "";
                        }
                    }
                }
                if (ds.Tables["Slab_rates"].Rows.Count > 0)
                {
                    btn_add.Visible = false;
                    btnClear.Visible = false;
                    panel_cont.Visible = true;

                }
                else
                {
                    panel_cont.Visible = true;
                    btn_add.Visible = true;
                    btnClear.Visible = true;
                }

            }

            peaks.Clear();
        }

    }
    protected void Gv_Prog_Peaks_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "ed")
        {
            GridViewRow Row = (GridViewRow)((LinkButton)e.CommandSource).NamingContainer;
            string Program_id = ((Label)Row.FindControl("lbl_Program_id")).Text; ;
            string Program_name = ((Label)Row.FindControl("lbl_Program_Name")).Text; ;

            string Que_get_Season_Id = "  Select * from touSeasonsEP where PROGRAM_id=" + Program_id;
            da = new SqlDataAdapter(Que_get_Season_Id, con);
            ds = new DataSet();
            da.Fill(ds, "Que_get_Season_Id");
            Peak_details_view(Program_id, Program_name);
            for (int i = 0; i < Gv_Prog_Peaks.Rows.Count; i++)
            {
                //Gv_Prog_Peaks.Rows[i].BackColor = Color.White;
                Gv_Prog_Peaks.Rows[i].BackColor = System.Drawing.ColorTranslator.FromHtml("#D1DEB6");
            }
            Row.BackColor = System.Drawing.ColorTranslator.FromHtml("#BFD299");
            //Row.BackColor = Color.LightBlue;
            //Row.BackColor = System.Drawing.ColorTranslator.FromHtml("#4B6C9E");
            clearFields();
            panel_cont.Controls.Clear();
            btn_add.Visible = false;
            btnClear.Visible = false;



        }
    }
    public void peaksBind(int season_id, string prog_id)
    {
        try
        {
            string Select_Peaks = " select distinct a.et_peaktype_id as et_peaktype_id ,b.peakType as et_peaktype ";
            Select_Peaks += " from touPeaksEP a, touPeakTypes b where a.programID =" + prog_id.ToString() + "";
            Select_Peaks += " and a.seasonID =" + season_id.ToString() + "  and a.et_peaktype_id = b.id";

            cmd = new SqlCommand(Select_Peaks, con);
            if (ConnectionState.Closed == con.State)
                con.Open();
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
            con.Close();
        }
    }
    private void seasonsBind(string program_id)
    {
        seasons.Clear();
        try
        {
            string Que_get_season = "select season_id,season_name from touSeasonsEP where program_id = " + program_id.ToString() + " order by season_id desc";
            cmd = new SqlCommand(Que_get_season, con);
            con.Open();
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
            cmd.Dispose();
            con.Close();
        }
    }
    public void Peak_details_view(string prog_id, string prog_name)
    {
        panel_seasons.Controls.Add(new LiteralControl("<table border='0' width='740px'>"));
        panel_seasons.Controls.Add(new LiteralControl("<tr><td colspan='2' style='background-color:#4B6C9E;color:#ffffff;font-weight:bold;' class='label' align='center'>Program Name: " + prog_name.ToUpper() + "</td></tr>"));
        panel_seasons.Controls.Add(new LiteralControl("<tr><td colspan='2' style='background-color:#4B6C9E;color:#ffffff;font-weight:bold;' class='label' align='center'>Season Peak Hour Definitions for Weekdays, Holidays, Stardays and Sundays</td></tr>"));
        panel_seasons.Controls.Add(new LiteralControl("<tr><td colspan='2' align='center'>"));
        //Display Season and Peak Definitions
        seasons.Clear();
        season_months.Clear();
        //season
        seasonsBind(prog_id);
        SeasonMonthsBind(prog_id);
        panel_seasons.Controls.Add(new LiteralControl("<table border=1>"));
        foreach (object id in seasons.Keys)
        {
            int season_id = Convert.ToInt32(id);
            string season_name = seasons[season_id + ""].ToString();
            string season_monthnames = season_months[season_id + ""].ToString();
            peaksBind(season_id, prog_id);
            panel_seasons.Controls.Add(new LiteralControl("<tr align='center'><td  style='background-color:#4B6C9E;color:#ffffff;font-weight:bold;' class='label'> Season Name: " + season_name + "(" + season_monthnames + ")</td></tr>"));
            panel_seasons.Controls.Add(new LiteralControl("<tr><td align='center'>"));
            panel_seasons.Controls.Add(new LiteralControl("<table border=1 width='100%'  class='label'><tr><td style='font-weight:bold'>Peak Name</td><td style='font-weight:bold'>Weekdays</td><td style='font-weight:bold'>Holidays</td><td style='font-weight:bold'>Saturdays</td><td style='font-weight:bold'>Sundays</td></tr>"));
            foreach (object peakid in peaks.Keys)
            {

                int peak_id = Convert.ToInt32(peakid);
                string peak_name = peaks[peakid + ""].ToString();


                string PeakHrSql = "select et_week_name, et_start_hr, et_stop_hr from touPeaksEP where  programID=" + prog_id + " and seasonID=" + season_id + " and et_peaktype_id=" + peak_id + " order by et_start_hr";
                da = new SqlDataAdapter(PeakHrSql, con);
                ds = new DataSet();
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




                }

                panel_seasons.Controls.Add(new LiteralControl("<tr><td>" + peak_name + "</td><td>" + checkEmpty(weekdays) + "</td><td>" + checkEmpty(holidays) + "</td><td>" + checkEmpty(saturdays) + "</td><td>" + checkEmpty(sundays) + "</td></tr>"));

            }
            panel_seasons.Controls.Add(new LiteralControl("</table></td></tr>"));


            peaks.Clear();
        }
        panel_seasons.Controls.Add(new LiteralControl("</table>"));

        panel_seasons.Controls.Add(new LiteralControl("</td></tr>"));


        panel_seasons.Controls.Add(new LiteralControl("<tr><td colspan='2'><br/></td></tr>"));
        panel_seasons.Controls.Add(new LiteralControl("<tr><td colspan='2' align='center' style='background-color:#4B6C9E;color:#ffffff;font-weight:bold;' class='label'>Peak Rates for different Slabs</td></tr>"));


        panel_seasons.Controls.Add(new LiteralControl("<tr><td colspan='2' align='center' style=' font-family:Verdana;font-size:12px;'>"));
        //Display Season and Peak Definitions
        panel_seasons.Controls.Add(new LiteralControl("<table border=1>"));
        foreach (object id in seasons.Keys)
        {
            int season_id = Convert.ToInt32(id);
            string season_name = seasons[season_id + ""].ToString();
            string season_monthnames = season_months[season_id + ""].ToString();
            peaksBind(prog_id, season_id);
            panel_seasons.Controls.Add(new LiteralControl("<tr align='center'><td  style='background-color:#4B6C9E;color:#ffffff;font-weight:bold;' class='label'> Season Name: " + season_name + "(" + season_monthnames + ")</td></tr>"));
            panel_seasons.Controls.Add(new LiteralControl("<tr><td align='center'>"));
            panel_seasons.Controls.Add(new LiteralControl("<table border=1><tr><td  style='font-weight:bold'>Start kWh</td><td  style='font-weight:bold''>Stop kWh</td>"));
            foreach (object peakid in peaks.Keys)
            {

                int peak_id = Convert.ToInt32(peakid);
                string peak_name = peaks[peakid + ""].ToString();
                panel_seasons.Controls.Add(new LiteralControl("<td style='font-weight:bold'>" + peak_name + "($)</td>"));

            }
            panel_seasons.Controls.Add(new LiteralControl("</tr>"));
            //try{
            string PeakSlabSql = "select distinct convert(int,slab_start) as start,slab_start,slab_stop from et_slab_rates where program_id=" + prog_id + " and season_id=" + season_id + "  order by convert(int,slab_start)";
            da = new SqlDataAdapter(PeakSlabSql, con);
            if (ConnectionState.Closed == con.State)
            {
                con.Open();
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
                panel_seasons.Controls.Add(new LiteralControl("<tr><td>" + startKwh + "</td><td>" + stopKwh + "</td>"));

                //init DataRow for Charting purposes
                DataRow dr_ChartInfo = dt_ChartInfo.NewRow();
                dr_ChartInfo[0] = startKwh;
                dr_ChartInfo[1] = stopKwh;

                int count = 2;
                foreach (object peakid in peaks.Keys)
                {

                    int peak_id = Convert.ToInt32(peakid);
                    string peak_name = peaks[peakid + ""].ToString();


                    string PeakRateSql = "select peak_rate from et_slab_rates where program_id=" + prog_id + " and season_id=" + season_id + " and et_peaktype_id=" + peak_id + " and slab_start = '" + startKwh + "' and slab_stop = '" + stopKwh + "'";
                    da = new SqlDataAdapter(PeakRateSql, con);
                    if (ConnectionState.Closed == con.State)
                    {
                        con.Open();
                    }
                    ds = new DataSet();
                    da.Fill(ds, "peakrates");
                    for (int j = 0; j < ds.Tables["peakrates"].Rows.Count; j++)
                    {

                        string PeakRate = ds.Tables["peakrates"].Rows[j]["peak_rate"].ToString();
                        
                        double price = 0.00;
                        if(!String.IsNullOrEmpty(PeakRate))
                            price = System.Convert.ToDouble(PeakRate);
                        
                        string Rate = String.Format("{0:#,##0.000000;(#,##0.000000);Zero}", price).ToString();

                        panel_seasons.Controls.Add(new LiteralControl("<td>" + Rate + "</td>"));

                        dr_ChartInfo[count++] = PeakRate;
                    }


                }
                dt_ChartInfo.Rows.Add(dr_ChartInfo);
                panel_seasons.Controls.Add(new LiteralControl("</tr>"));
            }
            panel_seasons.Controls.Add(new LiteralControl("</table></td></tr>"));
            String season_full = season_name + "(" + season_monthnames + ")";
            if (season_full.Count() > 40)
                createChart(season_name, peaks, dt_ChartInfo);
            else
                createChart(season_full, peaks, dt_ChartInfo);
            peaks.Clear();
        }
        panel_seasons.Controls.Add(new LiteralControl("</table>"));
        panel_seasons.Controls.Add(new LiteralControl("</td></tr>"));
        panel_seasons.Controls.Add(new LiteralControl("</table>"));

        foreach (Control ctrl in placeholder_barchart.Controls)
        {
            if (ctrl is RadChart)
                ((RadChart)ctrl).DataBind();
        }
    }
    private void peaksBind(string prog_id, int season_id)
    {
        try
        {
            string Select_Peaks = " select distinct a.et_peaktype_id as et_peaktype_id ,b.peakType as et_peaktype ";
            Select_Peaks += " from touPeaksEP a, touPeakTypes b where a.programID =" + prog_id.ToString() + "";
            Select_Peaks += " and a.seasonID =" + season_id.ToString() + "  and a.et_peaktype_id = b.id";

            cmd = new SqlCommand(Select_Peaks, con);
            if (ConnectionState.Closed == con.State)
                con.Open();
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
            con.Close();
        }
    }

    public void SeasonMonthsBind(string program_id)
    {


        foreach (object id in seasons.Keys)
        {
            int season_id = Convert.ToInt32(id);
            string season_name = seasons[season_id + ""].ToString();



            string SeasonsSql = "select * from touSeasonsEP where  program_id=" + program_id.ToString() + " and season_id=" + season_id + " ";
            da = new SqlDataAdapter(SeasonsSql, con);
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

        }


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

        btn_view_Click(sender, e);
    }
    protected void btnClear_OnClick(object sender, EventArgs e)
    {
        clearFields();
        //ph_insertpeaks.Controls.Clear();
    }
}