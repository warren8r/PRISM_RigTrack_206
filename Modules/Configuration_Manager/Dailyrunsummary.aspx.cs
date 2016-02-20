using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Telerik.Web.UI;
using System.Data.SqlClient;
using System.Drawing;
using System.Diagnostics;
using System.IO;
using System.Reflection;


public partial class Modules_Configuration_Manager_Dailyrunsummary : System.Web.UI.Page
{
    public static DataTable dt_Runinfo, dt_Forminfo,dt_Summary;
    DataTable dt_Runnumber;
    SqlConnection db = new SqlConnection(GlobalConnetionString.ClientConnection());
    SqlTransaction transaction;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            DataTable dt = SqlHelper.ExecuteDataset(GlobalConnetionString.ClientConnection(), CommandType.Text,
               "select * from manageJobOrders where status!='Closed' and (jid in (select jobid from PrismJobAssignedAssets) or jid in (select jobid from PrismJobAssignedPersonals)) and jobordercreatedid<>''").Tables[0];
            RadComboBoxFill.FillRadcombobox(radcombo_job, dt, "jobname", "jid", "0");
            dt_Runinfo = SqlHelper.ExecuteDataset(GlobalConnetionString.ClientConnection(), CommandType.Text, 
                "select * from PrismJobRunDetails RD LEFT OUTER JOIN  PrismJobRunDetailsFormInfo FI ON RD.runid=FI.RunId order by date").Tables[0];
            if (radcombo_job.Items.Count > 0)
            {
                radcombo_job.SelectedIndex = 1;
                btn_view_Click(null, null);
            }
            
        }
    }
    protected void grid_hours_ItemCommand(object obj, GridCommandEventArgs e)
    {
        if (e.CommandName == Telerik.Web.UI.RadGrid.ExportToExcelCommandName ||
                   e.CommandName == Telerik.Web.UI.RadGrid.ExportToWordCommandName ||
                   e.CommandName == Telerik.Web.UI.RadGrid.ExportToCsvCommandName ||
                   e.CommandName == Telerik.Web.UI.RadGrid.ExportToPdfCommandName)
        {
            grid_hours.ExportSettings.ExportOnlyData = true;
            grid_hours.ExportSettings.OpenInNewWindow = true;
            grid_hours.ExportSettings.IgnorePaging = true;
            grid_hours.ExportSettings.FileName = "Export";
        }
    }
    private void DisplayMessage(bool isError, string text)
    {
        if (isError)
        {
            //this.Label1.Text = text;
            //this.Label3.Text = text;
            lbl_message.Text = text;
        }
        else
        {
            //this.Label2.Text = text;
            //this.Label4.Text = text;
            lbl_message.Text = text;
        }
    }
    protected void btn_view_Click(object sender, EventArgs e)
    {
        //SqlGetRunsummary.SelectCommand = "SELECT * FROM [PrismDailyRunSummary] where JobId=" + radcombo_job.SelectedValue;
    }
   
    protected void btn_generate_Click(object sender, EventArgs e)
    {
        //string runnumber = "";

        DataRow[] row_run = dt_Runinfo.Select("jid=" + radcombo_job.SelectedValue);
        if (row_run.Length > 0)
        {
            dt_Runnumber = row_run.CopyToDataTable();
            var query = row_run.Select(x => x.Field<int>("runnumber")).Distinct();
            foreach (var runnumber in query)
            {
                //runnumber += value+",";
                if (dt_Forminfo != null)
                {
                    dt_Forminfo.Clear();
                }
                //Console.WriteLine(value);
                DataRow[] rowdata = dt_Runnumber.Select("runnumber=" + runnumber);
                if (rowdata.Length > 0)
                {
                    dt_Forminfo = rowdata.CopyToDataTable();
                    //int minLavel = Convert.ToInt32(dt.Compute("min(AccountLevel)", string.Empty));

                    List<DateTime> levelsDate = dt_Forminfo.AsEnumerable().Select(al => al.Field<DateTime>("Date")).Distinct().ToList();
                    DateTime startdate = levelsDate.Min();
                    DateTime enddate = levelsDate.Max();
                    //POPPET_SIZE
                    List<string> levelsPOPPET_SIZE = dt_Forminfo.AsEnumerable().Select(al => al.Field<string>("POPPET_SIZE")).Distinct().ToList();
                    // int min = levelsPOPPET_SIZE.Min();
                    string POPPET_SIZE = levelsPOPPET_SIZE.Max();
                    //ORIFICE_SIZE
                    List<string> levelsORIFICE_SIZE = dt_Forminfo.AsEnumerable().Select(al => al.Field<string>("ORIFICE_SIZE")).Distinct().ToList();
                    string ORIFICE_SIZE = levelsORIFICE_SIZE.Max();
                    //PULSE_WIDTH
                    List<string> levelsPULSE_WIDTH = dt_Forminfo.AsEnumerable().Select(al => al.Field<string>("PULSE_WIDTH")).Distinct().ToList();
                    string PULSE_WIDTH = levelsPULSE_WIDTH.Max();
                    //PULSE_AMPLITUDE
                    List<string> levelsPULSE_AMPLITUDE = dt_Forminfo.AsEnumerable().Select(al => al.Field<string>("PULSE_AMPLITUDE")).Distinct().ToList();
                    string PULSE_AMPLITUDE = levelsPULSE_AMPLITUDE.Max();
                    //ORIFICE_SIZE
                    List<string> levelsTOTAL_CONNECTED = dt_Forminfo.AsEnumerable().Select(al => al.Field<string>("TOTAL_CONNECTED")).Distinct().ToList();
                    string TOTAL_CONNECTED = levelsTOTAL_CONNECTED.Max();
                    //TOTAL_CIRC
                    List<string> levelsTOTAL_CIRC = dt_Forminfo.AsEnumerable().Select(al => al.Field<string>("TOTAL_CIRC")).Distinct().ToList();
                    string TOTAL_CIRC = levelsTOTAL_CIRC.Max();
                    //DEPTH_START
                    List<string> levelsDEPTH_START = dt_Forminfo.AsEnumerable().Select(al => al.Field<string>("DEPTH_START")).Distinct().ToList();
                    string DEPTH_START = levelsDEPTH_START.Max();
                    //DEPTH_END
                    List<string> levelsDEPTH_END = dt_Forminfo.AsEnumerable().Select(al => al.Field<string>("DEPTH_END")).Distinct().ToList();
                    string DEPTH_END = levelsDEPTH_END.Max();
                    //INCStart
                    List<string> levelsINCStart = dt_Forminfo.AsEnumerable().Select(al => al.Field<string>("INCStart")).Distinct().ToList();
                    string INCStart = levelsINCStart.Max();
                    //INCEnd
                    List<string> levelsINCEnd = dt_Forminfo.AsEnumerable().Select(al => al.Field<string>("INCEnd")).Distinct().ToList();
                    string INCEnd = levelsINCEnd.Max();
                    //AZMStart
                    List<string> levelsAZMStart = dt_Forminfo.AsEnumerable().Select(al => al.Field<string>("AZMStart")).Distinct().ToList();
                    string AZMStart = levelsAZMStart.Max();
                    //AZMEnd
                    List<string> levelsAZMEnd = dt_Forminfo.AsEnumerable().Select(al => al.Field<string>("AZMEnd")).Distinct().ToList();
                    string AZMEnd = levelsAZMEnd.Max();
                    //MAGFStart
                    List<string> levelsMAGFStart = dt_Forminfo.AsEnumerable().Select(al => al.Field<string>("MAGFStart")).Distinct().ToList();
                    string MAGFStart = levelsMAGFStart.Max();
                    //MAGFEnd
                    List<string> levelsMAGFEnd = dt_Forminfo.AsEnumerable().Select(al => al.Field<string>("MAGFEnd")).Distinct().ToList();
                    string MAGFEnd = levelsMAGFEnd.Max();
                    //GRAVStart
                    List<string> levelsGRAVStart = dt_Forminfo.AsEnumerable().Select(al => al.Field<string>("GRAVStart")).Distinct().ToList();
                    string GRAVStart = levelsGRAVStart.Max();
                    //GRAVEnd
                    List<string> levelsGRAVEnd = dt_Forminfo.AsEnumerable().Select(al => al.Field<string>("GRAVEnd")).Distinct().ToList();
                    string GRAVEnd = levelsGRAVEnd.Max();
                    //TEMPERATURE_C
                    List<string> levelsTEMPERATURE_C = dt_Forminfo.AsEnumerable().Select(al => al.Field<string>("TEMPERATURE_C")).Distinct().ToList();
                    string TEMPERATURE_C = levelsTEMPERATURE_C.Max();
                    //TEMPERATURE_F
                    List<string> levelsTEMPERATURE_F = dt_Forminfo.AsEnumerable().Select(al => al.Field<string>("TEMPERATURE_F")).Distinct().ToList();
                    string TEMPERATURE_F = levelsTEMPERATURE_F.Max();
                    //AVER_PUMP_PRESSURE
                    List<string> levelsAVER_PUMP_PRESSURE = dt_Forminfo.AsEnumerable().Select(al => al.Field<string>("AVER_PUMP_PRESSURE")).Distinct().ToList();
                    string AVER_PUMP_PRESSURE = levelsAVER_PUMP_PRESSURE.Max();
                    //AVER_FLOW_RATE
                    List<string> levelsAVER_FLOW_RATE = dt_Forminfo.AsEnumerable().Select(al => al.Field<string>("AVER_FLOW_RATE")).Distinct().ToList();
                    string AVER_FLOW_RATE = levelsAVER_FLOW_RATE.Max();
                    //MUD_WEIGHT
                    List<string> levelsMUD_WEIGHT = dt_Forminfo.AsEnumerable().Select(al => al.Field<string>("MUD_WEIGHT")).Distinct().ToList();
                    string MUD_WEIGHT = levelsMUD_WEIGHT.Max();
                    //SOLIDS
                    List<string> levelsSOLIDS = dt_Forminfo.AsEnumerable().Select(al => al.Field<string>("SOLIDS")).Distinct().ToList();
                    string SOLIDS = levelsSOLIDS.Max();
                    //SAND
                    List<string> levelsSAND = dt_Forminfo.AsEnumerable().Select(al => al.Field<string>("SAND")).Distinct().ToList();
                    string SAND = levelsSAND.Max();
                    try
                    {
                        if (ConnectionState.Closed == db.State)
                            db.Open();
                        transaction = db.BeginTransaction();
                        dt_Summary = SqlHelper.ExecuteDataset(transaction, CommandType.Text, "Select * from PrismDailyRunSummary").Tables[0];
                        DataRow[] row_summary = dt_Summary.Select("JobId=" + radcombo_job.SelectedValue + " and RunNumber=" + runnumber + "");
                        string queryRunsummary = "";
                        if (row_summary.Length == 0)
                        {
                            queryRunsummary = "Insert into PrismDailyRunSummary(JobId,RunNumber,[FaileY/N],StartDate,StratTime,EndDate,EndTime,CircHours,EndDepth,MaxTemp," +
                                "FlowRate,MudType,[Sand/Solids],BHAOD,Poppet,Orifice,PulseWidth,PulseAmp,[GR Pre-Run Bkgnd],[Gre Pre-Run High],[Gre Post-Run Bkgnd],[Gre Post-Run High]," +
                                "CalFactor,GammaOffset,SurveyOffset,StartDepth,[Batt #1 Voltage No Load],[Batt #1 Voltage Load],[Batt #2 Voltage No Load],[Batt #2 Voltage Load])" +
                                " values(" + radcombo_job.SelectedValue + "," + runnumber + ",'False','" + startdate.ToString() + "','" + startdate.ToShortTimeString() + "','" + enddate.ToString() + "'," +
                                "'" + enddate.ToShortTimeString() + "','" + TOTAL_CIRC + "','" + DEPTH_END + "','','" + AVER_FLOW_RATE + "','','" + SAND + "','','" + POPPET_SIZE + "','" + ORIFICE_SIZE + "'," +
                                "'" + PULSE_WIDTH + "','" + PULSE_AMPLITUDE + "','','','','','','','','" + DEPTH_START + "','','','','')";
                        }
                        else
                        {
                            queryRunsummary = "Update PrismDailyRunSummary set StartDate='" + startdate.ToString() + "',StratTime='" + startdate.ToShortTimeString() + "'," +
                                " EndDate='" + enddate.ToString() + "',EndTime='" + enddate.ToShortTimeString() + "',CircHours='" + TOTAL_CIRC + "',EndDepth='" + DEPTH_END + "'," +
                                " MaxTemp='',FlowRate='" + AVER_FLOW_RATE + "',MudType='',[Sand/Solids]='" + SAND + "',BHAOD='',Poppet='" + POPPET_SIZE + "',Orifice='" + ORIFICE_SIZE + "'," +
                                " PulseWidth='" + PULSE_WIDTH + "',PulseAmp='" + PULSE_AMPLITUDE + "', StartDepth='" + DEPTH_START + "' where JobId=" + radcombo_job.SelectedValue + " and RunNumber=" + runnumber + "";
                        }

                        SqlHelper.ExecuteNonQuery(transaction, CommandType.Text, queryRunsummary);
                        transaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                    }
                }
            }

        }
        grid_hours.Rebind();
    }

    protected void grid_hours_UpdateCommand(object sender, GridCommandEventArgs e)
    {
        //Get the GridEditableItem of the RadGrid     
        GridEditableItem editedItem = e.Item as GridEditableItem;
        //Get the primary key value using the DataKeyValue.     
        string SummaryId = editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex]["SummaryId"].ToString();
        bool faileyn = false;
        CheckBox chk = editedItem["FaileY"].Controls[0] as CheckBox;
        if(chk.Checked)
          faileyn = true;        
        else
            faileyn = false;
        string StartDate = (editedItem["StartDate"].Controls[0] as TextBox).Text; // access the value in column using 'UniqueName' of that column        
        string EndDate = (editedItem["EndDate"].Controls[0] as TextBox).Text;
        string CircHours = (editedItem["CircHours"].Controls[0] as TextBox).Text;
        string EndDepth = (editedItem["EndDepth"].Controls[0] as TextBox).Text;
        string MaxTemp = (editedItem["MaxTemp"].Controls[0] as TextBox).Text;
        string FlowRate = (editedItem["FlowRate"].Controls[0] as TextBox).Text;
        string MudType = (editedItem["MudType"].Controls[0] as TextBox).Text;
        string SandSolids = (editedItem["SandSolids"].Controls[0] as TextBox).Text;
        string BHAOD = (editedItem["BHAOD"].Controls[0] as TextBox).Text;
        string Poppet = (editedItem["Poppet"].Controls[0] as TextBox).Text;
        string Orifice = (editedItem["Orifice"].Controls[0] as TextBox).Text;
        string PulseWidth = (editedItem["PulseWidth"].Controls[0] as TextBox).Text;
        string PulseAmp = (editedItem["PulseAmp"].Controls[0] as TextBox).Text;
        string GRPreRunBkgnd = (editedItem["GRPreRunBkgnd"].Controls[0] as TextBox).Text;
        string GrePreRunHigh = (editedItem["GrePreRunHigh"].Controls[0] as TextBox).Text;
        string GrePostRunBkgnd = (editedItem["GrePostRunBkgnd"].Controls[0] as TextBox).Text;
        string GrePostRunHigh = (editedItem["GrePostRunHigh"].Controls[0] as TextBox).Text;
        string CalFactor = (editedItem["CalFactor"].Controls[0] as TextBox).Text;
        string GammaOffset = (editedItem["GammaOffset"].Controls[0] as TextBox).Text;
        string SurveyOffset = (editedItem["SurveyOffset"].Controls[0] as TextBox).Text;
        string StartDepth = (editedItem["StartDepth"].Controls[0] as TextBox).Text;
        string Batt1VoltageNoLoad = (editedItem["Batt1VoltageNoLoad"].Controls[0] as TextBox).Text;
        string Batt1VoltageLoad = (editedItem["Batt1VoltageLoad"].Controls[0] as TextBox).Text;
        string Batt2VoltageNoLoad = (editedItem["Batt2VoltageNoLoad"].Controls[0] as TextBox).Text;
        string Batt2VoltageLoad = (editedItem["Batt2VoltageLoad"].Controls[0] as TextBox).Text;
        try
        {
            string queryupdate = "   UPDATE [PrismDailyRunSummary] SET [FaileY/N]='"+faileyn+"', StartDate='" + StartDate + "',EndDate='" + EndDate + "',CircHours='" + CircHours + "',EndDepth='" + EndDepth + "',MaxTemp='" + MaxTemp + "'," +
                " FlowRate='" + FlowRate + "',MudType='" + MudType + "',[Sand/Solids]='" + SandSolids + "',BHAOD='" + BHAOD + "',Poppet='" + Poppet + "',Orifice='" + Orifice + "',PulseWidth='" + PulseWidth + "',PulseAmp='" + PulseAmp + "'," +
                " [GR Pre-Run Bkgnd]='" + GRPreRunBkgnd + "',[Gre Pre-Run High]='" + GrePreRunHigh + "',[Gre Post-Run Bkgnd]='" + GrePostRunBkgnd + "',[Gre Post-Run High]='" + GrePostRunHigh + "'," +
                " CalFactor='" + CalFactor + "',GammaOffset='" + GammaOffset + "',SurveyOffset='" + SurveyOffset + "',StartDepth='" + StartDepth + "',[Batt #1 Voltage No Load]='" + Batt1VoltageNoLoad + "'," +
                " [Batt #1 Voltage Load]='" + Batt1VoltageLoad + "',[Batt #2 Voltage No Load]='" + Batt2VoltageNoLoad + "',[Batt #2 Voltage Load]='" + Batt2VoltageLoad + "' WHERE SummaryId =" + SummaryId + "";
            SqlHelper.ExecuteNonQuery(GlobalConnetionString.ClientConnection(), CommandType.Text, queryupdate);


        }
        catch (Exception ex)
        {
            grid_hours.Controls.Add(new LiteralControl("Unable to update Employee. Reason: " + ex.Message));
            //e.Canceled = true;
        }
        editedItem.Edit = false;
    }
}