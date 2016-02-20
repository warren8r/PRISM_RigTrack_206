using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using Telerik.Web.UI;
using System.Net;
using System.IO;
using Ionic.Zip;
using Ionic.Zlib;
using System.Collections;
using System.Xml;
using System.Drawing;
public partial class Modules_DA_DaLaunchService : System.Web.UI.Page
{
    public static DataTable dt_Service, dt_serviceattributes, dtPreVEEZipFiles, dtPreVEEExtractedFiles, dtExistingMetersInfo;
    int metertoleranceduration = 3;
    double spikeCheckConstant = 1.8;
    protected void Page_Load(object sender, EventArgs e)
    {
        dt_Service = SqlHelper.ExecuteDataset(GlobalConnetionString.ClientConnection(), CommandType.Text, "select * from daService  where status='Active'").Tables[0];
        if (!IsPostBack)
        {


            RadComboBoxFill.FillRadcombobox(radcombo_services, dt_Service, "serviceName", "serviceId", "0");
            DataTable dt_timetolduration = SqlHelper.ExecuteDataset(GlobalConnetionString.ClientConnection(), CommandType.Text, "SELECT * FROM  veeConstants ").Tables[0];
            if (dt_timetolduration.Rows.Count > 0)
            {
                metertoleranceduration = Convert.ToInt32(dt_timetolduration.Rows[0]["meterTimeToleranceDur"]);
                spikeCheckConstant = Convert.ToDouble(WebUtility.NullToZero(dt_timetolduration.Rows[0]["spikeCheckThreshold"].ToString()));
            }

        }
        dt_serviceattributes = SqlHelper.ExecuteDataset(GlobalConnetionString.ClientConnection(), CommandType.Text, "select * from daServiceAttributes").Tables[0];
        bindAttributes();

    }
    protected void radcombo_temname_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
    {
        if (radcombo_temname.Text != "Select")
        {
            //dt_serviceattributes = SqlHelper.ExecuteDataset(GlobalConnetionString.ClientConnection(), CommandType.Text, "select * from daServiceAttributes").Tables[0];
            DataTable dt_templatename = SqlHelper.ExecuteDataset(GlobalConnetionString.ClientConnection(), CommandType.Text, "select * from daServiceTemplates where templateId=" + radcombo_temname.SelectedValue + "").Tables[0];
            if (dt_templatename.Rows.Count > 0)
            {
                string[] arr_n = dt_templatename.Rows[0]["URL"].ToString().Split('?');
                string[] arr_direction = arr_n[1].ToString().Split('&');
                lbl_direcion.Text = arr_direction[0].ToString();
                lbl_mtype.Text = arr_direction[1].ToString();
            }
            //RadComboBoxFill.FillRadcombobox(radcombo_temname, dt_templatename, "templateName", "templateId", "0");
        }
        else
        {
            lbl_direcion.Text = "";
            lbl_mtype.Text = "";
        }
    }
    protected void radcombo_services_SelectedIndexChanged(object sender, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs e)
    {
        DataRow[] row = dt_Service.Select("serviceId=" + radcombo_services.SelectedValue);
        txt_description.Text = row[0]["serviceDescription"].ToString();
    }
    public void bindAttributes()
    {
        if (radcombo_services.SelectedIndex > 0)
        {
            DataRow[] row_attributes = dt_serviceattributes.Select("serviceId=" + radcombo_services.SelectedValue);
            panel_attributes.Controls.Add(new LiteralControl("<table border=0><tr>"));
            for (int attribute = 0; attribute < row_attributes.Length; attribute++)
            {
                Label lbl_attributename = new Label();
                lbl_attributename.ID = "lbl_" + row_attributes[attribute]["serviceAttrId"].ToString();
                lbl_attributename.Text = row_attributes[attribute]["serviceAttr"].ToString();
                panel_attributes.Controls.Add(new LiteralControl("<td>"));
                panel_attributes.Controls.Add(lbl_attributename);
                panel_attributes.Controls.Add(new LiteralControl("<br />"));
                Telerik.Web.UI.RadComboBox radattribute = new Telerik.Web.UI.RadComboBox();
                radattribute.ID = "rad_" + row_attributes[attribute]["serviceAttrId"].ToString();
                string[] arrAttributes = row_attributes[attribute]["serviceAttrValues"].ToString().Split(',');
                for (int id = 0; id < arrAttributes.Length; id++)
                {
                    RadComboBoxItem item1 = new RadComboBoxItem(arrAttributes[id].ToString(), arrAttributes[id].ToString()); //Creates new item for RadCombobox  
                    radattribute.Items.Add(item1);
                }
                panel_attributes.Controls.Add(radattribute);
                panel_attributes.Controls.Add(new LiteralControl("</td>"));
            }
            panel_attributes.Controls.Add(new LiteralControl("<td  align='left'><br />"));
            panel_attributes.Controls.Add(btn_launch);
            panel_attributes.Controls.Add(new LiteralControl("</td><td  align='left'><br />"));
            panel_attributes.Controls.Add(img_ttip);
            panel_attributes.Controls.Add(new LiteralControl("</td></tr></table>"));
        }
    }
    protected void btn_viewservice_Click(object sender, EventArgs e)
    {
        DataRow[] row = dt_Service.Select("serviceId=" + radcombo_services.SelectedValue);
        lbl_servicename.Text = row[0]["serviceName"].ToString();
        hidden_url.Value = row[0]["URL"].ToString();
        lbl_serviceurl.Text = "<span style='font-weight:bold'>URL: </span>" + row[0]["URL"].ToString();
        txt_description.Text = row[0]["serviceDescription"].ToString();
        //bindAttributes();
        td_info.Visible = true;
        //td_template.Visible = true;
        if (radcombo_services.Text != "Select")
        {
            DataTable dt_templatename = SqlHelper.ExecuteDataset(GlobalConnetionString.ClientConnection(), CommandType.Text,
                "select * from daServiceTemplates where serviceId=" + radcombo_services.SelectedValue + " and templateId not in(select templateId from daScheduledServices where serviceId=" + radcombo_services.SelectedValue + ") and status='Active'").Tables[0];
            RadComboBoxFill.FillRadcombobox(radcombo_temname, dt_templatename, "templateName", "templateId", "0");
        }

    }
    public void launchURL()
    {
        DataRow[] row_attributes = dt_serviceattributes.Select("serviceId=" + radcombo_services.SelectedValue);
        panel_attributes.Visible = true;
        string querystring = "";
        for (int attribute = 0; attribute < row_attributes.Length; attribute++)
        {
            Label lbl_attribute = (Label)panel_attributes.FindControl("lbl_" + row_attributes[attribute]["serviceAttrId"].ToString());
            Telerik.Web.UI.RadComboBox radattribute = (Telerik.Web.UI.RadComboBox)panel_attributes.FindControl("rad_" + row_attributes[attribute]["serviceAttrId"].ToString());
            querystring += lbl_attribute.Text + "=" + radattribute.SelectedValue + "&";
        }
        if (!String.IsNullOrEmpty(querystring))
            querystring = querystring.Remove(querystring.Length - 1, 1);

        lbl_url.Text = "<span style='color:green;font-weight:bold;'>" + radcombo_services.SelectedItem.Text + "</span> Service URL: " + hidden_url.Value + "?" + querystring;
    }
    protected void btn_launch_Click(object sender, EventArgs e)
    {

        dtExistingMetersInfo = SqlHelper.ExecuteDataset(GlobalConnetionString.ClientConnection(), CommandType.Text, "Select * from meter").Tables[0];
        lbl_url.Text = "";
        launchURL();
        HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create("http://67.40.65.182:65432/zip/Default.aspx");
        webRequest.KeepAlive = false;
        webRequest.ProtocolVersion = HttpVersion.Version10;
        //webRequest.ServicePoint.ConnectionLimit = 24;
        webRequest.Headers.Add("UserAgent", "Pentia; MSI");
        //webRequest.KeepAlive = false;
        //webRequest.ProtocolVersion = HttpVersion.Version10; 
        using (WebResponse webResponse = webRequest.GetResponse())
        {
            //FileStream fZip = File.Create(webResponse.GetResponseStream);
            string zipfilename = DateTime.Now.ToString("yyyyMMddHHmmss");// +"_" + DateTime.Now.Ticks;
            FileStream writer = new FileStream(Server.MapPath("~/PreVEEFiles/ZipFiles/" + zipfilename + ".zip"), FileMode.Create);
            string contentType = webResponse.ContentType;
            using (Stream stream = webResponse.GetResponseStream())
            {
                long length = webResponse.ContentLength;
                int bufferSize = 2048;
                int readCount;
                byte[] buffer = new byte[2048];

                readCount = stream.Read(buffer, 0, bufferSize);
                while (readCount > 0)
                {
                    writer.Write(buffer, 0, readCount);
                    readCount = stream.Read(buffer, 0, bufferSize);
                }

                stream.Close();
                webResponse.Close();
                writer.Close();
                //Unzipping the zip file from response file
                string path = Server.MapPath("~/PreVEEFiles/ZipFiles/" + zipfilename + ".zip");
                int int_dtPreVEEZipFiles = SqlHelper.ExecuteNonQuery(GlobalConnetionString.ClientConnection(), CommandType.Text, "Insert into daPreVEEZipFiles(serviceId,ZipFileName)" +
                 " values ('" + radcombo_services.SelectedValue + "','" + zipfilename + "')");
                if (int_dtPreVEEZipFiles > 0)
                {

                    using (var zip = Ionic.Zip.ZipFile.Read(path))
                    {
                        try
                        {
                            dtPreVEEZipFiles = SqlHelper.ExecuteDataset(GlobalConnetionString.ClientConnection(), CommandType.Text, "SELECT * FROM    daPreVEEZipFiles WHERE  PreVEEZipFileId = IDENT_CURRENT('daPreVEEZipFiles')").Tables[0];
                            string PreVEEZipFileId = dtPreVEEZipFiles.Rows[0]["PreVEEZipFileId"].ToString();
                            for (int i = 0; i < zip.Entries.Count; i++)
                            {
                                SqlHelper.ExecuteNonQuery(GlobalConnetionString.ClientConnection(), CommandType.Text,
                                    "Insert into daPreVEEExtractedFiles(PreVEEZipFileId,ExtractedFileName)" +
                                    " values ('" + PreVEEZipFileId + "','" + zip[i].FileName + "')");
                            }
                            Directory.CreateDirectory(Path.Combine(Server.MapPath("~/PreVEEFiles/ExtractedFiles"), zipfilename));
                            Directory.CreateDirectory(Path.Combine(Server.MapPath("~/PreVEEFiles/ImportedFiles"), zipfilename));
                            zip.ExtractAll(Server.MapPath("~/PreVEEFiles/ExtractedFiles/" + zipfilename), ExtractExistingFileAction.OverwriteSilently);
                            dtPreVEEExtractedFiles = SqlHelper.ExecuteDataset(GlobalConnetionString.ClientConnection(), CommandType.Text, "SELECT * FROM    daPreVEEExtractedFiles").Tables[0];
                        }
                        catch (Exception ex)
                        {
                            lbl_message.Text = ex.Message;
                        }

                        try
                        {

                            string sourceFile = "", destinationFile = "", message = "";
                            var ext = new string[] { ".XML", ".xml" };
                            FileInfo[] XMLcollection = (from fi in new DirectoryInfo(Server.MapPath("~/PreVEEFiles/ExtractedFiles/" + zipfilename)).GetFiles()
                                                        where ext.Contains(fi.Extension.ToUpper())
                                                        select fi)
                                                    .ToArray();

                            for (int newfile = 0; newfile < XMLcollection.Length; newfile++)
                            {

                                // uploadedElsterFile.PostedFile.SaveAs(localFileName);
                                Hashtable attributes = new Hashtable();
                                string strParentTable = "", queryAMRDEFInsert = "", AMRDEFColumns = "", AMRDEFValues = "", AMRDEF_Id = "",
                                    queryScheduleExecutionInsert = "", ScheduleExecutionColumns = "", ScheduleExecutionValues = "", ScheduleExecution_Id = "",
                                    queryScheduleInfoInsert = "", ScheduleInfoColumns = "", ScheduleInfoValues = "",
                                    MetersRead_Id = "", MetersNotRead_Id = "",
                                    queryMeterReadingsInsert = "", MeterReadingsColumns = "", MeterReadingsValues = "", MeterReadings_Id = "",
                                    queryMeterInsert = "", MeterColumns = "", MeterValues = "", Meter_Id = "", Meter_SerialNumber = "",
                                    queryClockInsert = "", ClockColumns = "", ClockValues = "",
                                    queryEventSpecInsert = "", EventSpecColumns = "", EventSpecValues = "", EventData_Id = "",
                                    queryEventInsert = "", EventColumns = "", EventValues = "", Event_Id = "",
                                    queryInstrumentationValueInsert = "", InstrumentationValueColumns = "", InstrumentationValueValues = "",
                                    queryOutageCountSummaryInsert = "", OutageCountSummary_Id = "",
                                    queryOutageCountInsert = "", OutageCountColumns = "", OutageCountValues = "",
                                    queryConsumptionDataInsert = "", ConsumptionData_Id = "",
                                    queryConsumptionSpecInsert = "", ConsumptionSpecColumns = "", ConsumptionSpecValues = "",
                                    queryReadingInsert = "", ReadingColumns = "", ReadingValues = "",
                                    queryLoadProfileSummaryInsert = "", LoadProfileSummaryColumns = "", LoadProfileSummaryValues = "", LoadProfileSummary_Id = "",
                                    queryChannelInsert = "", ChannelColumns = "", ChannelValues = "",
                                    queryIntervalDataInsert = "", IntervalData_Id = "",
                                    queryReverseEnergySummaryInsert = "", ReverseEnergySummary_Id = "",
                                    queryReverseEnergyInsert = "", ReverseEnergyColumns = "", ReverseEnergyValues = "",
                                    queryIntervalSpecInsert = "",
                                    queryMaxDemandSpecInsert = "", MaxDemandSpecColumns = "", MaxDemandSpecValues = "",
                                    queryQualityFlagsInsert = "", QualityFlagsColumns = "", QualityFlagsValues = "", MaxDemandData_Id = "",
                                    queryDemandResetCountInsert = "", DemandResetCountColumns = "", DemandResetCountValues = "",
                                    ReadingQualityIndicatorColumns = "", ReadingQualityIndicatorValues = "",
                                    StatusColumns = "", StatusValues = "",
                                    queryEventDataInsert = "", queryReadingQualityIndicatorInsert = "",
                                    CumulativeDemandSpec_colname = "", CumulativeDemandSpec_value = "", CumulativeDemandData_Id = "", queryCumulativeDemandDataInsert = "",
                                    queryCumulativeDemandSpecInsert = "", queryMaxDemandDataInsert = "", str_readingid = "", str_channelnumber = "",
                                    str_query_daInertvalMeterData = "", str_daintervalmeterdataid = "", QualityFlagsUpdateColumns = "", QualityFlagsUpdateValues = "",
                                    str_pulseoverflowvalue = "", str_daintervalmeterdataid_m = "";

                                string timestampvalue = "", rawreadingvalue = "";
                                string str_ReadingRangeStartTimestamp = "", str_ReadingRangeEndTimestamp = "",
                                                                  str_direction = "", str_intervalsec = "", str_NumIntervalsRead = "", str_query_daInertvalMeterData_meter = "";
                                int returnScheduleInfocount = 0, returnMaxDemandDatacount = 0, returnCumulativeDemandDatacount = 0, returnCumulativeDemandSpeccount = 0,
                                    returnDemandResetCountcount = 0, returnInstrumentationValuecount = 0;
                                int returnAMRDEFcount = 0, returnScheduleExecutioncount = 0, returnMetersNotReadcount = 0, returnMetersReaddcount = 0, returnMeterReadingscount = 0,
                                    returnMetercount = 0, returnClockcount = 0, returnOutageCountSummarycount = 0,
                                    returnOutageCountcount = 0, returnConsumptionDatacount = 0, returnConsumptionSpeccount = 0, returnReadingcount = 0,
                                    returnLoadProfileSummarycount = 0, returnChannelcount = 0, returnIntervalDatacount = 0, returnIntervalSpeccount = 0,
                                    returnQualityFlagsInsertcount = 0;
                                DataTable dtAMRDEF, dtScheduleExecution, dtMeterReadings, dtMeter, dtOutageCountSummary,
                                     dtConsumptionData, dtMaxDemandData, dtReverseEnergySummary, dtCumulativeDemandData,
                                     dtLoadProfileSummary, dtIntervalData, dtMetersNotRead, dtMetersRead, dtReadingid;
                                DataRow[] dr_mserial = null, dr_mserial_rec = null;
                                bool intervalDataFlag = false, consumptionDataFlag = false, boolExistTimezoneindex = false;
                                Decimal SumOfIntervalValues = 0;
                                Decimal delivered = 0, received = 0;
                                Decimal sumofint = 0;
                                Decimal sumofdel = 0;
                                Decimal sumofrec = 0;
                                SqlConnection db = new SqlConnection(GlobalConnetionString.ConnectionStringAttributes);
                                SqlTransaction transaction;
                                SqlConnection dbMDM = new SqlConnection(GlobalConnetionString.ClientConnection());
                                SqlTransaction transactionMDM;
                                db.Open();
                                dbMDM.Open();
                                //db.ConnectionTimeout(120);
                                transaction = db.BeginTransaction();
                                transactionMDM = dbMDM.BeginTransaction();
                                try
                                {

                                    DataTable dt_Readings = new DataTable();//ReadingValues_Delivered
                                    DataColumn ReadingValues_Delivered = new DataColumn();
                                    ReadingValues_Delivered.ColumnName = "ReadingValues_Delivered";
                                    dt_Readings.Columns.Add(ReadingValues_Delivered);

                                    DataColumn ReadingValues_Received = new DataColumn();
                                    ReadingValues_Received.ColumnName = "ReadingValues_Received";
                                    dt_Readings.Columns.Add(ReadingValues_Received);

                                    DataTable dt_Delivered = new DataTable();
                                    DataTable dt_Received = new DataTable();


                                    //1stcolumn

                                    DataColumn dc1_UOM = new DataColumn();
                                    dc1_UOM.ColumnName = "UOM";
                                    dt_Delivered.Columns.Add(dc1_UOM);

                                    DataColumn dc1_Direction = new DataColumn();
                                    dc1_Direction.ColumnName = "Direction";
                                    dt_Delivered.Columns.Add(dc1_Direction);

                                    DataColumn dc1_IntervalSeconds = new DataColumn();
                                    dc1_IntervalSeconds.ColumnName = "IntervalSeconds";
                                    dt_Delivered.Columns.Add(dc1_IntervalSeconds);

                                    //2ndcolumn
                                    DataColumn dc1_NumIntervalsRead = new DataColumn();
                                    dc1_NumIntervalsRead.ColumnName = "NumIntervalsRead";
                                    dt_Delivered.Columns.Add(dc1_NumIntervalsRead);
                                    //3rdcolumn
                                    DataColumn dc1_FirstIntervalTimestamp = new DataColumn();
                                    dc1_FirstIntervalTimestamp.ColumnName = "FirstIntervalTimestamp";
                                    dt_Delivered.Columns.Add(dc1_FirstIntervalTimestamp);
                                    //4thcolumn
                                    DataColumn dc1_ReadingRangeStartTimestamp = new DataColumn();
                                    dc1_ReadingRangeStartTimestamp.ColumnName = "ReadingRangeStartTimestamp";
                                    dt_Delivered.Columns.Add(dc1_ReadingRangeStartTimestamp);
                                    //5thcolumn
                                    DataColumn dc1_ReadingRangeEndTimestamp = new DataColumn();
                                    dc1_ReadingRangeEndTimestamp.ColumnName = "ReadingRangeEndTimestamp";
                                    dt_Delivered.Columns.Add(dc1_ReadingRangeEndTimestamp);
                                    //6thcolumn
                                    DataColumn dc1_SumOfIntervalValues = new DataColumn();
                                    dc1_SumOfIntervalValues.ColumnName = "SumOfIntervalValues";
                                    dt_Delivered.Columns.Add(dc1_SumOfIntervalValues);
                                    //7thcolumn
                                    DataColumn dc1_Multiplier = new DataColumn();
                                    dc1_Multiplier.ColumnName = "Multiplier";
                                    dt_Delivered.Columns.Add(dc1_Multiplier);

                                    DataColumn dc1_totalintervalcnt = new DataColumn();
                                    dc1_totalintervalcnt.ColumnName = "TotalIntervalCount";
                                    dt_Delivered.Columns.Add(dc1_totalintervalcnt);

                                    DataColumn dc1_Deliveredreadings = new DataColumn();
                                    dc1_Deliveredreadings.ColumnName = "TotalReadingValues";
                                    dt_Delivered.Columns.Add(dc1_Deliveredreadings);



                                    DataColumn dc1_meterserialnumber = new DataColumn();
                                    dc1_meterserialnumber.ColumnName = "MeterSerialNumber";
                                    dt_Delivered.Columns.Add(dc1_meterserialnumber);

                                    DataColumn dc1_meterid = new DataColumn();
                                    dc1_meterid.ColumnName = "MeterID";
                                    dt_Delivered.Columns.Add(dc1_meterid);

                                    DataTable dt_timeintervalDelivered = new DataTable();
                                    DataTable dt_timeintervalReceived = new DataTable();

                                    using (XmlTextReader reader = new XmlTextReader(Server.MapPath("~/PreVEEFiles/ExtractedFiles/" + zipfilename + "/" + XMLcollection[newfile].Name)))
                                    {
                                        while (reader.Read())
                                        {
                                            if (reader.IsStartElement())
                                            {
                                                switch (reader.Name)
                                                {
                                                    case "AMRDEF":
                                                        {

                                                            if (reader.HasAttributes)
                                                            {
                                                                for (int i = 0; i < reader.AttributeCount; i++)
                                                                {
                                                                    reader.MoveToAttribute(i);

                                                                    AMRDEFColumns += reader.Name + ",";
                                                                    AMRDEFValues += "'" + reader.Value + "',";
                                                                }
                                                                queryAMRDEFInsert = "Insert into AMRDEF(FileName," + AMRDEFColumns.Remove(AMRDEFColumns.Length - 1, 1) + ")" +
                                                                    " values ('" + XMLcollection[newfile].Name + "'," + AMRDEFValues.Remove(AMRDEFValues.Length - 1, 1) + ")";
                                                                returnAMRDEFcount = SqlHelper.ExecuteNonQuery(transaction, CommandType.Text, queryAMRDEFInsert);
                                                                if (returnAMRDEFcount > 0)
                                                                {
                                                                    dtAMRDEF = SqlHelper.ExecuteDataset(transaction, CommandType.Text, "SELECT * FROM    AMRDEF WHERE  AMRDEF_Id = IDENT_CURRENT('AMRDEF')").Tables[0];
                                                                    AMRDEF_Id = dtAMRDEF.Rows[0]["AMRDEF_Id"].ToString();
                                                                }
                                                            }
                                                            AMRDEFColumns = ""; AMRDEFValues = "";
                                                        }
                                                        break;
                                                    case "ScheduleExecution":
                                                        {
                                                            if (reader.HasAttributes)
                                                            {
                                                                for (int i = 0; i < reader.AttributeCount; i++)
                                                                {
                                                                    reader.MoveToAttribute(i);

                                                                    ScheduleExecutionColumns += reader.Name + ",";
                                                                    ScheduleExecutionValues += "'" + reader.Value + "',";
                                                                }
                                                                queryScheduleExecutionInsert = "Insert into ScheduleExecution(AMRDEF_Id," + ScheduleExecutionColumns.Remove(ScheduleExecutionColumns.Length - 1, 1) + ") " +
                                                                    " values (" + AMRDEF_Id + "," + ScheduleExecutionValues.Remove(ScheduleExecutionValues.Length - 1, 1) + ")";
                                                                returnScheduleExecutioncount = SqlHelper.ExecuteNonQuery(transaction, CommandType.Text, queryScheduleExecutionInsert);
                                                                if (returnScheduleExecutioncount > 0)
                                                                {
                                                                    dtScheduleExecution = SqlHelper.ExecuteDataset(transaction, CommandType.Text, "SELECT * FROM  ScheduleExecution WHERE  ScheduleExecution_Id = IDENT_CURRENT('ScheduleExecution')").Tables[0];
                                                                    ScheduleExecution_Id = dtScheduleExecution.Rows[0]["ScheduleExecution_Id"].ToString();
                                                                }
                                                                ScheduleExecutionColumns = ""; ScheduleExecutionValues = "";
                                                            }
                                                            break;
                                                        }
                                                    case "ScheduleInfo":
                                                        {
                                                            if (reader.HasAttributes)
                                                            {
                                                                for (int i = 0; i < reader.AttributeCount; i++)
                                                                {
                                                                    reader.MoveToAttribute(i);

                                                                    ScheduleInfoColumns += reader.Name + ",";
                                                                    ScheduleInfoValues += "'" + reader.Value + "',";
                                                                }
                                                                if (ScheduleExecution_Id != "")
                                                                {
                                                                    queryScheduleInfoInsert = "Insert into ScheduleInfo(AMRDEF_Id,ScheduleExecution_Id," + ScheduleInfoColumns.Remove(ScheduleInfoColumns.Length - 1, 1) + ") " +
                                                                        " values (" + AMRDEF_Id + "," + ScheduleExecution_Id + "," + ScheduleInfoValues.Remove(ScheduleInfoValues.Length - 1, 1) + ")";
                                                                }
                                                                else
                                                                {
                                                                    queryScheduleInfoInsert = "Insert into ScheduleInfo(AMRDEF_Id," + ScheduleInfoColumns.Remove(ScheduleInfoColumns.Length - 1, 1) + ") " +
                                                                       " values (" + AMRDEF_Id + "," + ScheduleInfoValues.Remove(ScheduleInfoValues.Length - 1, 1) + ")";
                                                                }
                                                                returnScheduleInfocount = SqlHelper.ExecuteNonQuery(transaction, CommandType.Text, queryScheduleInfoInsert);
                                                                ScheduleInfoColumns = ""; ScheduleInfoValues = "";
                                                            }
                                                            break;
                                                        }
                                                    case "MetersNotRead":
                                                        {
                                                            strParentTable = "MetersNotRead";

                                                            string queryMetersNotReadInsert = "Insert into MetersNotRead(AMRDEF_Id,ScheduleExecution_Id) values (" + AMRDEF_Id + "," + ScheduleExecution_Id + ")";
                                                            returnMetersNotReadcount = SqlHelper.ExecuteNonQuery(transaction, CommandType.Text, queryMetersNotReadInsert);
                                                            if (returnMetersNotReadcount > 0)
                                                            {
                                                                dtMetersNotRead = SqlHelper.ExecuteDataset(transaction, CommandType.Text, "SELECT * FROM  MetersNotRead WHERE  MetersNotRead_Id = IDENT_CURRENT('MetersNotRead')").Tables[0];
                                                                MetersNotRead_Id = dtMetersNotRead.Rows[0]["MetersNotRead_Id"].ToString();
                                                            }

                                                            break;
                                                        }
                                                    case "MetersRead":
                                                        {
                                                            strParentTable = "MetersRead";

                                                            string queryMetersReadInsert = "Insert into MetersRead(AMRDEF_Id,ScheduleExecution_Id) values (" + AMRDEF_Id + "," + ScheduleExecution_Id + ")";
                                                            returnMetersReaddcount = SqlHelper.ExecuteNonQuery(transaction, CommandType.Text, queryMetersReadInsert);
                                                            if (returnMetersReaddcount > 0)
                                                            {
                                                                dtMetersRead = SqlHelper.ExecuteDataset(transaction, CommandType.Text, "SELECT * FROM  MetersRead WHERE  MetersRead_Id = IDENT_CURRENT('MetersRead')").Tables[0];
                                                                MetersRead_Id = dtMetersRead.Rows[0]["MetersRead_Id"].ToString();
                                                            }

                                                            break;
                                                        }
                                                    case "MeterReadings":
                                                        {
                                                            strParentTable = "MeterReadings";
                                                            if (reader.HasAttributes)
                                                            {
                                                                for (int i = 0; i < reader.AttributeCount; i++)
                                                                {
                                                                    reader.MoveToAttribute(i);

                                                                    MeterReadingsColumns += reader.Name + ",";
                                                                    MeterReadingsValues += "'" + reader.Value + "',";
                                                                }
                                                                queryMeterReadingsInsert = "Insert into MeterReadings(AMRDEF_Id," + MeterReadingsColumns.Remove(MeterReadingsColumns.Length - 1, 1) + ") " +
                                                                    " values (" + AMRDEF_Id + "," + MeterReadingsValues.Remove(MeterReadingsValues.Length - 1, 1) + ")";
                                                                returnMeterReadingscount = SqlHelper.ExecuteNonQuery(transaction, CommandType.Text, queryMeterReadingsInsert);
                                                                if (returnMeterReadingscount > 0)
                                                                {
                                                                    dtMeterReadings = SqlHelper.ExecuteDataset(transaction, CommandType.Text, "SELECT * FROM  MeterReadings WHERE  MeterReadings_Id = IDENT_CURRENT('MeterReadings')").Tables[0];
                                                                    MeterReadings_Id = dtMeterReadings.Rows[0]["MeterReadings_Id"].ToString();
                                                                }
                                                                MeterReadingsColumns = ""; MeterReadingsValues = "";
                                                            }
                                                            break;
                                                        }
                                                    case "Meter":
                                                        {
                                                            string dbFiled = "", dbValue = "";
                                                            switch (strParentTable)
                                                            {
                                                                case "MetersNotRead":
                                                                    {
                                                                        dbFiled = "MetersNotRead_Id";
                                                                        dbValue = MetersNotRead_Id.ToString();
                                                                        break;
                                                                    }
                                                                case "MetersRead":
                                                                    {
                                                                        dbFiled = "MetersRead_Id";
                                                                        dbValue = MetersRead_Id.ToString();
                                                                        break;
                                                                    }
                                                                case "MeterReadings":
                                                                    {
                                                                        dbFiled = "MeterReadings_Id";
                                                                        dbValue = MeterReadings_Id.ToString();
                                                                        break;
                                                                    }
                                                            }
                                                            if (reader.HasAttributes)
                                                            {
                                                                bool boolExistMeter = true;

                                                                for (int i = 0; i < reader.AttributeCount; i++)
                                                                {
                                                                    reader.MoveToAttribute(i);

                                                                    MeterColumns += reader.Name + ",";
                                                                    MeterValues += "'" + reader.Value + "',";
                                                                    if (reader.Name == "SerialNumber")
                                                                    {
                                                                        DataRow[] rowExistMeter = dtExistingMetersInfo.Select("serialNumber='" + reader.Value + "'");
                                                                        if (rowExistMeter.Length == 0)
                                                                        {
                                                                            boolExistMeter = false;
                                                                        }
                                                                        DataRow[] dr_timezoneindex = dtExistingMetersInfo.Select("TimeZoneIndex=" + reader.Value + "");
                                                                        if (dr_timezoneindex.Length == 0)
                                                                        {
                                                                            boolExistTimezoneindex = false;
                                                                        }
                                                                    }
                                                                    if (reader.Name == "TimeZoneIndex")
                                                                    {

                                                                        DataRow[] dr_timezoneindex = dtExistingMetersInfo.Select("TimeZoneIndex=" + reader.Value + "");
                                                                        if (dr_timezoneindex.Length == 0)
                                                                        {
                                                                            boolExistTimezoneindex = false;
                                                                        }
                                                                    }
                                                                }
                                                                queryMeterInsert = "Insert into Meter(" + dbFiled + "," + MeterColumns.Remove(MeterColumns.Length - 1, 1) + ") " +
                                                                    " values (" + dbValue + "," + MeterValues.Remove(MeterValues.Length - 1, 1) + ")";
                                                                returnMetercount = SqlHelper.ExecuteNonQuery(transaction, CommandType.Text, queryMeterInsert);
                                                                if (returnMetercount > 0)
                                                                {
                                                                    dtMeter = SqlHelper.ExecuteDataset(transaction, CommandType.Text, "SELECT * FROM  Meter WHERE  Meter_Id = IDENT_CURRENT('Meter')").Tables[0];
                                                                    Meter_Id = dtMeter.Rows[0]["Meter_Id"].ToString();
                                                                    Meter_SerialNumber = dtMeter.Rows[0]["SerialNumber"].ToString();



                                                                    //dbMDM.Open();
                                                                    //transactionMDM = dbMDM.BeginTransaction();

                                                                    if (!boolExistMeter)
                                                                    {


                                                                        string EventInfo = "";
                                                                        EventInfo = "Meter " + Meter_SerialNumber + " is Not Available in MDM";
                                                                        string queryeventAMI = "Insert into eventAMI(EventInfo,ElsterMeter_Id,ElsterMeterSerialNumber,TimeStamp,EventCode) " +
                                                                            " values('" + EventInfo + "','" + Meter_Id + "','" + dtMeter.Rows[0]["SerialNumber"].ToString() + "','" + DateTime.Now + "','MNF')";
                                                                        SqlHelper.ExecuteNonQuery(transactionMDM, CommandType.Text, queryeventAMI);




                                                                    }
                                                                    else
                                                                    {
                                                                        if (!boolExistTimezoneindex)
                                                                        {
                                                                            string EventInfo = "";
                                                                            EventInfo = "TimeZoneIndex for " + Meter_SerialNumber + " does not match in MDM";
                                                                            string queryeventAMI = "Insert into eventAMI(EventInfo,ElsterMeter_Id,ElsterMeterSerialNumber,TimeStamp,EventCode) " +
                                                                                " values('" + EventInfo + "','" + Meter_Id + "','" + dtMeter.Rows[0]["SerialNumber"].ToString() + "','" + DateTime.Now + "','TZ')";
                                                                            SqlHelper.ExecuteNonQuery(transactionMDM, CommandType.Text, queryeventAMI);
                                                                        }
                                                                    }
                                                                    //transactionMDM.Commit();

                                                                }
                                                                MeterColumns = ""; MeterValues = "";
                                                            }
                                                            break;
                                                        }
                                                    case "Clock":
                                                        {
                                                            DateTime MeterTime = new DateTime();
                                                            DateTime ServerTime = new DateTime();
                                                            if (reader.HasAttributes)
                                                            {
                                                                for (int i = 0; i < reader.AttributeCount; i++)
                                                                {
                                                                    reader.MoveToAttribute(i);

                                                                    ClockColumns += reader.Name + ",";
                                                                    ClockValues += "'" + reader.Value + "',";
                                                                    if (reader.Name == "MeterTime")
                                                                    {
                                                                        MeterTime = Convert.ToDateTime(reader.Value);
                                                                    }
                                                                    if (reader.Name == "ServerTime")
                                                                    {
                                                                        ServerTime = Convert.ToDateTime(reader.Value);
                                                                    }

                                                                }
                                                                TimeSpan ts = ServerTime - MeterTime;
                                                                queryClockInsert = "Insert into Clock(MeterReadings_Id," + ClockColumns.Remove(ClockColumns.Length - 1, 1) + ") " +
                                                                    " values (" + MeterReadings_Id + "," + ClockValues.Remove(ClockValues.Length - 1, 1) + ")";
                                                                returnClockcount = SqlHelper.ExecuteNonQuery(transaction, CommandType.Text, queryClockInsert);
                                                                ClockColumns = ""; ClockValues = "";
                                                                if (ts.Minutes > metertoleranceduration || ts.Minutes < -metertoleranceduration)
                                                                {
                                                                    //dbMDM.Open();
                                                                    //    transactionMDM = dbMDM.BeginTransaction();

                                                                    string EventInfo = "";
                                                                    EventInfo = "Time tolerance check on Meter " + Meter_SerialNumber + " failed";
                                                                    string queryeventAMI = "Insert into eventAMI(EventInfo,ElsterMeter_Id,ElsterMeterSerialNumber,TimeStamp,EventCode) values('" + EventInfo + "','" + Meter_Id + "','" + Meter_SerialNumber + "','" + DateTime.Now + "','TTM')";
                                                                    SqlHelper.ExecuteNonQuery(transactionMDM, CommandType.Text, queryeventAMI);


                                                                }
                                                            }
                                                            break;
                                                        }
                                                    case "EventData":
                                                        {

                                                            queryEventDataInsert = "Insert into EventData(MeterReadings_Id) " +
                                                                " values (" + MeterReadings_Id + ")";
                                                            int returnEventDatacount = SqlHelper.ExecuteNonQuery(transaction, CommandType.Text, queryEventDataInsert);
                                                            if (returnEventDatacount > 0)
                                                            {
                                                                DataTable dtEventData = SqlHelper.ExecuteDataset(transaction, CommandType.Text, "SELECT * FROM  EventData WHERE  EventData_Id = IDENT_CURRENT('EventData')").Tables[0];
                                                                EventData_Id = dtEventData.Rows[0]["EventData_Id"].ToString();
                                                            }
                                                            break;
                                                        }
                                                    case "EventSpec":
                                                        {
                                                            if (reader.HasAttributes)
                                                            {
                                                                for (int i = 0; i < reader.AttributeCount; i++)
                                                                {
                                                                    reader.MoveToAttribute(i);

                                                                    EventSpecColumns += reader.Name + ",";
                                                                    EventSpecValues += "'" + reader.Value + "',";
                                                                }
                                                                queryEventSpecInsert = "Insert into EventSpec(EventData_Id," + EventSpecColumns.Remove(EventSpecColumns.Length - 1, 1) + ") " +
                                                                    " values (" + EventData_Id + "," + EventSpecValues.Remove(EventSpecValues.Length - 1, 1) + ")";
                                                                int returnEventSpeccount = SqlHelper.ExecuteNonQuery(transaction, CommandType.Text, queryEventSpecInsert);
                                                                EventSpecColumns = ""; EventSpecValues = "";
                                                            }
                                                            break;
                                                        }
                                                    case "Event":
                                                        {
                                                            if (reader.HasAttributes)
                                                            {
                                                                for (int i = 0; i < reader.AttributeCount; i++)
                                                                {
                                                                    reader.MoveToAttribute(i);

                                                                    EventColumns += reader.Name + ",";
                                                                    EventValues += "'" + reader.Value + "',";
                                                                }
                                                                queryEventInsert = "Insert into Event(EventData_Id," + EventColumns.Remove(EventColumns.Length - 1, 1) + ") " +
                                                                    " values (" + EventData_Id + "," + EventValues.Remove(EventValues.Length - 1, 1) + ")";
                                                                int returnEventcount = SqlHelper.ExecuteNonQuery(transaction, CommandType.Text, queryEventInsert);

                                                                string queryamiEventInsert = "Insert into eventAMI(" + EventColumns.Remove(EventColumns.Length - 1, 1) + ",ElsterMeter_Id,ElsterMeterSerialNumber) " +
                                                                    " values (" + EventValues.Remove(EventValues.Length - 1, 1) + "," + Meter_Id + "," + Meter_SerialNumber + ")";
                                                                SqlHelper.ExecuteNonQuery(transactionMDM, CommandType.Text, queryamiEventInsert);

                                                                if (returnEventcount > 0)
                                                                {
                                                                    DataTable dtEvent = SqlHelper.ExecuteDataset(transaction, CommandType.Text, "SELECT * FROM  Event WHERE  Event_Id = IDENT_CURRENT('Event')").Tables[0];
                                                                    Event_Id = dtEvent.Rows[0]["Event_Id"].ToString();
                                                                }
                                                                EventColumns = ""; EventValues = "";
                                                            }
                                                            break;
                                                        }
                                                    case "InstrumentationValue":
                                                        {
                                                            if (reader.HasAttributes)
                                                            {
                                                                for (int i = 0; i < reader.AttributeCount; i++)
                                                                {
                                                                    reader.MoveToAttribute(i);

                                                                    InstrumentationValueColumns += reader.Name + ",";
                                                                    InstrumentationValueValues += "'" + reader.Value + "',";
                                                                }
                                                                queryInstrumentationValueInsert = "Insert into InstrumentationValue(MeterReadings_Id," + InstrumentationValueColumns.Remove(InstrumentationValueColumns.Length - 1, 1) + ") " +
                                                                    " values (" + MeterReadings_Id + "," + InstrumentationValueValues.Remove(InstrumentationValueValues.Length - 1, 1) + ")";
                                                                returnInstrumentationValuecount = SqlHelper.ExecuteNonQuery(transaction, CommandType.Text, queryInstrumentationValueInsert);
                                                                InstrumentationValueColumns = ""; InstrumentationValueValues = "";
                                                            }
                                                            break;
                                                        }
                                                    case "OutageCountSummary":
                                                        {

                                                            queryOutageCountSummaryInsert = "Insert into OutageCountSummary(MeterReadings_Id) " +
                                                                " values (" + MeterReadings_Id + ")";
                                                            returnOutageCountSummarycount = SqlHelper.ExecuteNonQuery(transaction, CommandType.Text, queryOutageCountSummaryInsert);
                                                            if (returnOutageCountSummarycount > 0)
                                                            {
                                                                dtOutageCountSummary = SqlHelper.ExecuteDataset(transaction, CommandType.Text, "SELECT * FROM  OutageCountSummary WHERE  OutageCountSummary_Id = IDENT_CURRENT('OutageCountSummary')").Tables[0];
                                                                OutageCountSummary_Id = dtOutageCountSummary.Rows[0]["OutageCountSummary_Id"].ToString();
                                                            }

                                                            break;
                                                        }
                                                    case "OutageCount":
                                                        {
                                                            if (reader.HasAttributes)
                                                            {
                                                                for (int i = 0; i < reader.AttributeCount; i++)
                                                                {
                                                                    reader.MoveToAttribute(i);

                                                                    OutageCountColumns += reader.Name + ",";
                                                                    OutageCountValues += "'" + reader.Value + "',";
                                                                }
                                                                queryOutageCountInsert = "Insert into OutageCount(OutageCountSummary_Id," + OutageCountColumns.Remove(OutageCountColumns.Length - 1, 1) + ") " +
                                                                    " values (" + OutageCountSummary_Id + "," + OutageCountValues.Remove(OutageCountValues.Length - 1, 1) + ")";
                                                                returnOutageCountcount = SqlHelper.ExecuteNonQuery(transaction, CommandType.Text, queryOutageCountInsert);
                                                                OutageCountColumns = ""; OutageCountValues = "";
                                                            }
                                                            break;
                                                        }
                                                    case "ReadingQualityIndicator":
                                                        {
                                                            if (reader.HasAttributes)
                                                            {
                                                                for (int i = 0; i < reader.AttributeCount; i++)
                                                                {
                                                                    reader.MoveToAttribute(i);

                                                                    ReadingQualityIndicatorColumns += reader.Name + ",";
                                                                    ReadingQualityIndicatorValues += "'" + reader.Value + "',";
                                                                }
                                                                queryReadingQualityIndicatorInsert = "Insert into ReadingQualityIndicator(MeterReadings_Id," + ReadingQualityIndicatorColumns.Remove(ReadingQualityIndicatorColumns.Length - 1, 1) + ") " +
                                                                    " values (" + MeterReadings_Id + "," + ReadingQualityIndicatorValues.Remove(ReadingQualityIndicatorValues.Length - 1, 1) + ")";
                                                                returnMetercount = SqlHelper.ExecuteNonQuery(transaction, CommandType.Text, queryReadingQualityIndicatorInsert);

                                                                ReadingQualityIndicatorColumns = ""; ReadingQualityIndicatorValues = "";
                                                            }
                                                            break;
                                                        }
                                                    case "ReverseEnergySummary":
                                                        {

                                                            queryReverseEnergySummaryInsert = "Insert into ReverseEnergySummary(MeterReadings_Id) " +
                                                                " values (" + MeterReadings_Id + ")";
                                                            int returnReverseEnergySummarycount = SqlHelper.ExecuteNonQuery(transaction, CommandType.Text, queryReverseEnergySummaryInsert);
                                                            if (returnReverseEnergySummarycount > 0)
                                                            {
                                                                dtReverseEnergySummary = SqlHelper.ExecuteDataset(transaction, CommandType.Text, "SELECT * FROM  ReverseEnergySummary WHERE  ReverseEnergySummary_Id = IDENT_CURRENT('ReverseEnergySummary')").Tables[0];
                                                                ReverseEnergySummary_Id = dtReverseEnergySummary.Rows[0]["ReverseEnergySummary_Id"].ToString();
                                                            }

                                                            break;
                                                        }
                                                    case "ReverseEnergy":
                                                        {
                                                            if (reader.HasAttributes)
                                                            {
                                                                for (int i = 0; i < reader.AttributeCount; i++)
                                                                {
                                                                    reader.MoveToAttribute(i);

                                                                    ReverseEnergyColumns += reader.Name + ",";
                                                                    ReverseEnergyValues += "'" + reader.Value + "',";
                                                                }
                                                                queryReverseEnergyInsert = "Insert into ReverseEnergy(ReverseEnergySummary_Id," + ReverseEnergyColumns.Remove(ReverseEnergyColumns.Length - 1, 1) + ") " +
                                                                    " values (" + ReverseEnergySummary_Id + "," + ReverseEnergyValues.Remove(ReverseEnergyValues.Length - 1, 1) + ")";
                                                                int returnReverseEnergycount = SqlHelper.ExecuteNonQuery(transaction, CommandType.Text, queryReverseEnergyInsert);
                                                                ReverseEnergyColumns = ""; ReverseEnergyValues = "";
                                                            }
                                                            break;
                                                        }
                                                    case "ConsumptionData":
                                                        {

                                                            consumptionDataFlag = true;
                                                            intervalDataFlag = false;
                                                            queryConsumptionDataInsert = "Insert into ConsumptionData(MeterReadings_Id) " +
                                                                " values (" + MeterReadings_Id + ")";
                                                            returnConsumptionDatacount = SqlHelper.ExecuteNonQuery(transaction, CommandType.Text, queryConsumptionDataInsert);
                                                            if (returnConsumptionDatacount > 0)
                                                            {
                                                                dtConsumptionData = SqlHelper.ExecuteDataset(transaction, CommandType.Text, "SELECT * FROM  ConsumptionData WHERE  ConsumptionData_Id = IDENT_CURRENT('ConsumptionData')").Tables[0];
                                                                ConsumptionData_Id = dtConsumptionData.Rows[0]["ConsumptionData_Id"].ToString();
                                                            }

                                                            break;
                                                        }
                                                    case "MaxDemandData":
                                                        {

                                                            queryMaxDemandDataInsert = "Insert into MaxDemandData(MeterReadings_Id) " +
                                                               " values (" + MeterReadings_Id + ")";
                                                            returnMaxDemandDatacount = SqlHelper.ExecuteNonQuery(transaction, CommandType.Text, queryMaxDemandDataInsert);
                                                            if (returnMaxDemandDatacount > 0)
                                                            {
                                                                dtMaxDemandData = SqlHelper.ExecuteDataset(transaction, CommandType.Text, "SELECT * FROM  MaxDemandData WHERE  MaxDemandData_Id = IDENT_CURRENT('MaxDemandData')").Tables[0];
                                                                MaxDemandData_Id = dtMaxDemandData.Rows[0]["MaxDemandData_Id"].ToString();
                                                            }

                                                            break;
                                                        }
                                                    case "MaxDemandSpec":
                                                        {
                                                            if (reader.HasAttributes)
                                                            {
                                                                for (int i = 0; i < reader.AttributeCount; i++)
                                                                {
                                                                    reader.MoveToAttribute(i);

                                                                    MaxDemandSpecColumns += reader.Name + ",";
                                                                    MaxDemandSpecValues += "'" + reader.Value + "',";
                                                                }
                                                                queryMaxDemandSpecInsert = "Insert into MaxDemandSpec(MaxDemandData_Id," + MaxDemandSpecColumns.Remove(MaxDemandSpecColumns.Length - 1, 1) + ") " +
                                                                      " values (" + MaxDemandData_Id + "," + MaxDemandSpecValues.Remove(MaxDemandSpecValues.Length - 1, 1) + ")";
                                                                returnMaxDemandDatacount = SqlHelper.ExecuteNonQuery(transaction, CommandType.Text, queryMaxDemandSpecInsert);
                                                            }
                                                            MaxDemandSpecColumns = ""; MaxDemandSpecValues = "";
                                                            break;
                                                        }
                                                    case "DemandResetCount":
                                                        {
                                                            if (reader.HasAttributes)
                                                            {
                                                                for (int i = 0; i < reader.AttributeCount; i++)
                                                                {
                                                                    reader.MoveToAttribute(i);

                                                                    DemandResetCountColumns += reader.Name + ",";
                                                                    DemandResetCountValues += "'" + reader.Value + "',";
                                                                }
                                                                queryDemandResetCountInsert = "Insert into DemandResetCount(MeterReadings_Id," + DemandResetCountColumns.Remove(DemandResetCountColumns.Length - 1, 1) + ") " +
                                                                      " values (" + MeterReadings_Id + "," + DemandResetCountValues.Remove(DemandResetCountValues.Length - 1, 1) + ")";
                                                                returnDemandResetCountcount = SqlHelper.ExecuteNonQuery(transaction, CommandType.Text, queryDemandResetCountInsert);
                                                            }
                                                            DemandResetCountColumns = ""; DemandResetCountValues = "";
                                                            break;
                                                        }
                                                    case "CumulativeDemandData":
                                                        {

                                                            queryCumulativeDemandDataInsert = "Insert into CumulativeDemandData(MeterReadings_Id) " +
                                                                " values (" + MeterReadings_Id + ")";
                                                            returnCumulativeDemandDatacount = SqlHelper.ExecuteNonQuery(transaction, CommandType.Text, queryCumulativeDemandDataInsert);
                                                            if (returnCumulativeDemandDatacount > 0)
                                                            {
                                                                dtCumulativeDemandData = SqlHelper.ExecuteDataset(transaction, CommandType.Text, "SELECT * FROM  CumulativeDemandData WHERE  CumulativeDemandData_Id = IDENT_CURRENT('CumulativeDemandData')").Tables[0];
                                                                CumulativeDemandData_Id = dtCumulativeDemandData.Rows[0]["CumulativeDemandData_Id"].ToString();
                                                            }

                                                            break;
                                                        }
                                                    case "CumulativeDemandSpec":
                                                        {
                                                            if (reader.HasAttributes)
                                                            {
                                                                for (int i = 0; i < reader.AttributeCount; i++)
                                                                {
                                                                    reader.MoveToAttribute(i);

                                                                    CumulativeDemandSpec_colname += reader.Name + ",";
                                                                    CumulativeDemandSpec_value += "'" + reader.Value + "',";
                                                                }
                                                                queryCumulativeDemandSpecInsert = "Insert into CumulativeDemandSpec(CumulativeDemandData_Id," + CumulativeDemandSpec_colname.Remove(CumulativeDemandSpec_colname.Length - 1, 1) + ") " +
                                                                    " values (" + CumulativeDemandData_Id + "," + CumulativeDemandSpec_value.Remove(CumulativeDemandSpec_value.Length - 1, 1) + ")";
                                                                returnCumulativeDemandSpeccount = SqlHelper.ExecuteNonQuery(transaction, CommandType.Text, queryCumulativeDemandSpecInsert);
                                                                CumulativeDemandSpec_colname = ""; CumulativeDemandSpec_value = "";
                                                            }
                                                            break;
                                                        }
                                                    case "ConsumptionSpec":
                                                        {
                                                            if (reader.HasAttributes)
                                                            {
                                                                for (int i = 0; i < reader.AttributeCount; i++)
                                                                {
                                                                    reader.MoveToAttribute(i);

                                                                    ConsumptionSpecColumns += reader.Name + ",";
                                                                    ConsumptionSpecValues += "'" + reader.Value + "',";

                                                                    if (reader.Name == "Direction")
                                                                    {
                                                                        if (reader.Value == "Delivered")
                                                                        {
                                                                            str_channelnumber = "1";
                                                                        }
                                                                        else if (reader.Value == "Received")
                                                                        {
                                                                            str_channelnumber = "2";
                                                                        }
                                                                        else
                                                                        {
                                                                            str_channelnumber = "3";
                                                                        }
                                                                    }
                                                                }

                                                                queryConsumptionSpecInsert = "Insert into ConsumptionSpec(ConsumptionData_Id," + ConsumptionSpecColumns.Remove(ConsumptionSpecColumns.Length - 1, 1) + ") " +
                                                                    " values (" + ConsumptionData_Id + "," + ConsumptionSpecValues.Remove(ConsumptionSpecValues.Length - 1, 1) + ")";
                                                                returnConsumptionSpeccount = SqlHelper.ExecuteNonQuery(transaction, CommandType.Text, queryConsumptionSpecInsert);
                                                                ConsumptionSpecColumns = ""; ConsumptionSpecValues = "";
                                                            }
                                                            break;
                                                        }

                                                    case "Reading":
                                                        {

                                                            if (reader.HasAttributes)
                                                            {

                                                                //DataRow row = dt_Delivered.NewRow();
                                                                for (int i = 0; i < reader.AttributeCount; i++)
                                                                {
                                                                    reader.MoveToAttribute(i);
                                                                    if (reader.Name == "RawReading")
                                                                    {

                                                                        //DataRow row_readings = dt_Readings.NewRow();
                                                                        ReadingColumns += "value ,";
                                                                        rawreadingvalue = reader.Value;
                                                                        if (intervalDataFlag)
                                                                        {
                                                                            if (str_channelnumber == "1")
                                                                            {
                                                                                dr_mserial[0]["TotalReadingValues"] = WebUtility.NullToZero(dr_mserial[0]["TotalReadingValues"].ToString()) + Convert.ToDecimal(reader.Value);
                                                                                //delivered += Convert.ToDecimal(reader.Value);
                                                                                //row_readings["ReadingValues_Delivered"] = reader.Value;
                                                                            }
                                                                            else if (str_channelnumber == "2")
                                                                            {
                                                                                dr_mserial_rec[0]["TotalReadingValues"] = WebUtility.NullToZero(dr_mserial_rec[0]["TotalReadingValues"].ToString()) + Convert.ToDecimal(reader.Value);

                                                                                //received += Convert.ToDecimal(reader.Value);
                                                                                //row_readings["ReadingValues_Received"] = reader.Value;
                                                                            }
                                                                        }
                                                                    }
                                                                    else if (reader.Name == "TimeStamp")
                                                                    {
                                                                        ReadingColumns += reader.Name + ",";
                                                                        timestampvalue = reader.Value;
                                                                    }
                                                                    else
                                                                    {
                                                                        ReadingColumns += reader.Name + ",";
                                                                        rawreadingvalue = reader.Value;
                                                                    }
                                                                    ReadingValues += "'" + reader.Value + "',";
                                                                }

                                                                string id = "";
                                                                string value = "";
                                                                if (consumptionDataFlag)
                                                                {
                                                                    id = "ConsumptionData_Id";
                                                                    value = ConsumptionData_Id;
                                                                }
                                                                else if (intervalDataFlag)
                                                                {
                                                                    id = "IntervalData_Id";
                                                                    value = IntervalData_Id;
                                                                }

                                                                queryReadingInsert = "Insert into Reading(" + id + "," + ReadingColumns.Remove(ReadingColumns.Length - 1, 1) + ") " +
                                                                    " values (" + value + "," + ReadingValues.Remove(ReadingValues.Length - 1, 1) + ")";
                                                                returnReadingcount = SqlHelper.ExecuteNonQuery(transaction, CommandType.Text, queryReadingInsert);
                                                                int did;
                                                                //if (str_channelnumber == "1")
                                                                //{
                                                                //    //delete a row from dt_timeintervalDelivered based on timestampvalue present in datatable


                                                                //    DataRow[] drr = dt_timeintervalDelivered.Select("Interval=' " + timestampvalue + " ' ");
                                                                //    for (int i = 0; i < drr.Length; i++)
                                                                //        dt_timeintervalDelivered.Rows.Remove(drr[i]);
                                                                //    dt_timeintervalDelivered.AcceptChanges();
                                                                //}
                                                                //else if (str_channelnumber == "2")
                                                                //{
                                                                //    //dt_timeintervalReceived
                                                                //    DataRow[] drr = dt_timeintervalDelivered.Select("Interval=' " + timestampvalue + " ' ");
                                                                //    for (int i = 0; i < drr.Length; i++)
                                                                //        dt_timeintervalDelivered.Rows.Remove(drr[i]);
                                                                //    dt_timeintervalDelivered.AcceptChanges();
                                                                //}



                                                                DataTable dtReadingid_version = SqlHelper.ExecuteDataset(transactionMDM, CommandType.Text, "SELECT * FROM  daIntervalMeterDataRevs where MeterSerialNumber=" + Meter_SerialNumber + " and TimeStamp='" + timestampvalue + "' and MeterDirectionId=" + str_channelnumber + "").Tables[0];
                                                                int versioncnt = Convert.ToInt32(dtReadingid_version.Rows.Count) + 1;
                                                                if (dtReadingid_version.Rows.Count == 0)
                                                                {
                                                                    //int versioncnt_meter = Convert.ToInt32(dtReadingid_version.Rows.Count) + 1;
                                                                    str_query_daInertvalMeterData_meter = "insert into daIntervalMeterData(MeterId,TimeStamp,MeterData,ActualMeterRead,Version,MeterDirectionId,MeterSerialNumber,Source)values(" +
                                                                        "" + Meter_Id + ",'" + timestampvalue + "'," + rawreadingvalue + "," + rawreadingvalue + "," + versioncnt + "," + str_channelnumber + ",'" + Meter_SerialNumber + "','VALIDATED')";

                                                                }
                                                                else
                                                                {
                                                                    str_query_daInertvalMeterData_meter = "Update daIntervalMeterData set MeterData=" + rawreadingvalue + ",ActualMeterRead=" + rawreadingvalue + ",Version=" + versioncnt + ", source='VALIDATED' where MeterSerialNumber=" + Meter_SerialNumber + " and TimeStamp='" + timestampvalue + "' and MeterDirectionId=" + str_channelnumber + "";
                                                                }
                                                                int countinsertrid_meter = SqlHelper.ExecuteNonQuery(transactionMDM, CommandType.Text, str_query_daInertvalMeterData_meter);
                                                                if (countinsertrid_meter > 0)
                                                                {
                                                                    DataTable dtReadingid_interval_meter = SqlHelper.ExecuteDataset(transactionMDM, CommandType.Text, "SELECT * FROM  daIntervalMeterData WHERE  IntervalMeterDataId = IDENT_CURRENT('daIntervalMeterData')").Tables[0];
                                                                    str_daintervalmeterdataid_m = dtReadingid_interval_meter.Rows[0]["IntervalMeterDataId"].ToString();
                                                                }
                                                                //INSERT IN daIntervalMeterDataRevs
                                                                //int versioncnt = Convert.ToInt32(dtReadingid_version.Rows.Count) + 1;
                                                                str_query_daInertvalMeterData = "insert into daIntervalMeterDataRevs(MeterId,TimeStamp,MeterData,ActualMeterRead,Version,MeterDirectionId,MeterSerialNumber,Source)values(" +
                                                                    "" + Meter_Id + ",'" + timestampvalue + "'," + rawreadingvalue + "," + rawreadingvalue + "," + versioncnt + "," + str_channelnumber + ",'" + Meter_SerialNumber + "','VALIDATED')";
                                                                int countinsertrid = SqlHelper.ExecuteNonQuery(transactionMDM, CommandType.Text, str_query_daInertvalMeterData);
                                                                if (countinsertrid > 0)
                                                                {
                                                                    DataTable dtReadingid_interval = SqlHelper.ExecuteDataset(transactionMDM, CommandType.Text, "SELECT * FROM  daIntervalMeterDataRevs WHERE  IntervalMeterDataId = IDENT_CURRENT('daIntervalMeterDataRevs')").Tables[0];
                                                                    str_daintervalmeterdataid = dtReadingid_interval.Rows[0]["IntervalMeterDataId"].ToString();
                                                                }
                                                                if (returnReadingcount > 0)
                                                                {
                                                                    dtReadingid = SqlHelper.ExecuteDataset(transaction, CommandType.Text, "SELECT * FROM  Reading WHERE  Reading_Id = IDENT_CURRENT('Reading')").Tables[0];
                                                                    str_readingid = dtReadingid.Rows[0]["Reading_Id"].ToString();
                                                                }
                                                                ReadingColumns = ""; ReadingValues = "";
                                                            }
                                                            break;
                                                        }
                                                    case "LoadProfileSummary":
                                                        {

                                                            queryLoadProfileSummaryInsert = "Insert into LoadProfileSummary(MeterReadings_Id) " +
                                                                " values (" + MeterReadings_Id + ")";
                                                            returnLoadProfileSummarycount = SqlHelper.ExecuteNonQuery(transaction, CommandType.Text, queryLoadProfileSummaryInsert);
                                                            if (returnLoadProfileSummarycount > 0)
                                                            {
                                                                dtLoadProfileSummary = SqlHelper.ExecuteDataset(transaction, CommandType.Text, "SELECT * FROM  LoadProfileSummary WHERE  LoadProfileSummary_Id = IDENT_CURRENT('LoadProfileSummary')").Tables[0];
                                                                LoadProfileSummary_Id = dtLoadProfileSummary.Rows[0]["LoadProfileSummary_Id"].ToString();
                                                            }

                                                            break;
                                                        }
                                                    case "Channel":
                                                        {


                                                            if (reader.HasAttributes)
                                                            {
                                                                DataRow row = dt_Delivered.NewRow();
                                                                for (int i = 0; i < reader.AttributeCount; i++)
                                                                {
                                                                    reader.MoveToAttribute(i);

                                                                    ChannelColumns += reader.Name + ",";
                                                                    ChannelValues += "'" + reader.Value + "',";

                                                                    row[reader.Name] = reader.Value;

                                                                    if (reader.Name == "ReadingRangeStartTimestamp")
                                                                    {
                                                                        str_ReadingRangeStartTimestamp = reader.Value;
                                                                    }
                                                                    if (reader.Name == "ReadingRangeEndTimestamp")
                                                                    {
                                                                        str_ReadingRangeEndTimestamp = reader.Value;
                                                                    }
                                                                    if (reader.Name == "Direction")
                                                                    {
                                                                        str_direction = reader.Value;
                                                                    }
                                                                    if (reader.Name == "IntervalSeconds")
                                                                    {
                                                                        str_intervalsec = reader.Value;
                                                                    }
                                                                    if (reader.Name == "NumIntervalsRead")
                                                                    {
                                                                        str_NumIntervalsRead = reader.Value;
                                                                    }
                                                                    if (reader.Name == "SumOfIntervalValues")
                                                                    {
                                                                        SumOfIntervalValues = Convert.ToDecimal(reader.Value);
                                                                    }

                                                                }
                                                                row["MeterID"] = Meter_Id;
                                                                if (str_ReadingRangeStartTimestamp != "" && str_ReadingRangeEndTimestamp != "")
                                                                {
                                                                    int seccount = Convert.ToInt32(str_intervalsec) / 60;
                                                                    if (str_direction == "Delivered")
                                                                    {

                                                                        dt_timeintervalDelivered = DatesbetweenDatatable.getdatatable(Convert.ToDateTime(str_ReadingRangeStartTimestamp), Convert.ToDateTime(str_ReadingRangeEndTimestamp), seccount);
                                                                        row["TotalIntervalCount"] = dt_timeintervalDelivered.Rows.Count.ToString();

                                                                        if (str_NumIntervalsRead != dt_timeintervalDelivered.Rows.Count.ToString())
                                                                        {

                                                                            string EventInfo = "";
                                                                            EventInfo = "Time tolerance on Data failed. No. of Intervals Received to No. of Intervals required does not match for meter " + Meter_SerialNumber + " between " + str_ReadingRangeStartTimestamp + " and " + str_ReadingRangeEndTimestamp + " for " + str_direction + " meter data";
                                                                            string queryeventAMI = "Insert into eventAMI(EventInfo,ElsterMeter_Id,ElsterMeterSerialNumber,TimeStamp,EventCode) values('" + EventInfo + "','" + Meter_Id + "','" + Meter_SerialNumber + "','" + DateTime.Now + "','TTD')";
                                                                            SqlHelper.ExecuteNonQuery(transactionMDM, CommandType.Text, queryeventAMI);
                                                                            //transactionMDM.Commit();

                                                                        }
                                                                    }
                                                                    else
                                                                    {
                                                                        dt_timeintervalReceived = DatesbetweenDatatable.getdatatable(Convert.ToDateTime(str_ReadingRangeStartTimestamp), Convert.ToDateTime(str_ReadingRangeEndTimestamp), seccount);
                                                                        row["TotalIntervalCount"] = dt_timeintervalReceived.Rows.Count.ToString();

                                                                        if (str_NumIntervalsRead != dt_timeintervalReceived.Rows.Count.ToString())
                                                                        {

                                                                            string EventInfo = "";
                                                                            EventInfo = "Time tolerance on Data failed. No. of Intervals Received to No. of Intervals required does not match for meter " + Meter_SerialNumber + " between " + str_ReadingRangeStartTimestamp + " and " + str_ReadingRangeEndTimestamp + " for " + str_direction + " meter data";
                                                                            string queryeventAMI = "Insert into eventAMI(EventInfo,ElsterMeter_Id,ElsterMeterSerialNumber,TimeStamp,EventCode) values('" + EventInfo + "','" + Meter_Id + "','" + Meter_SerialNumber + "','" + DateTime.Now + "','TTD')";
                                                                            SqlHelper.ExecuteNonQuery(transactionMDM, CommandType.Text, queryeventAMI);
                                                                            //transactionMDM.Commit();

                                                                        }
                                                                    }

                                                                }
                                                                row["MeterSerialNumber"] = Meter_SerialNumber;
                                                                //DataTable dt_timeinterval = dt_Delivered.Copy();


                                                                queryChannelInsert = "Insert into Channel(LoadProfileSummary_Id," + ChannelColumns.Remove(ChannelColumns.Length - 1, 1) + ") " +
                                                                    " values (" + LoadProfileSummary_Id + "," + ChannelValues.Remove(ChannelValues.Length - 1, 1) + ")";
                                                                returnChannelcount = SqlHelper.ExecuteNonQuery(transaction, CommandType.Text, queryChannelInsert);


                                                                dt_Delivered.Rows.Add(row);
                                                                if (str_direction == "Delivered")
                                                                {
                                                                    dr_mserial = dt_Delivered.Select("MeterSerialNumber='" + Meter_SerialNumber + "' and Direction='Delivered'");
                                                                }
                                                                //if (dr_mserial.Length > 0)
                                                                //{

                                                                //    dr_mserial[0]["TotalReadingValues"] = delivered;
                                                                //    //dt_Delivered.Rows.Add(rownew);
                                                                //}
                                                                if (str_direction == "Received")
                                                                {
                                                                    dr_mserial_rec = dt_Delivered.Select("MeterSerialNumber='" + Meter_SerialNumber + "' and Direction='Received'");
                                                                }
                                                                //if (dr_mserial_rec.Length > 0)
                                                                //{

                                                                //    dr_mserial_rec[0]["TotalReadingValues"] = received;
                                                                //}

                                                                ChannelColumns = ""; ChannelValues = "";
                                                            }
                                                            break;
                                                        }
                                                    case "IntervalData":
                                                        {

                                                            consumptionDataFlag = false;
                                                            intervalDataFlag = true;
                                                            queryIntervalDataInsert = "Insert into IntervalData(MeterReadings_Id) " +
                                                                " values (" + MeterReadings_Id + ")";
                                                            returnIntervalDatacount = SqlHelper.ExecuteNonQuery(transaction, CommandType.Text, queryIntervalDataInsert);
                                                            if (returnIntervalDatacount > 0)
                                                            {
                                                                dtIntervalData = SqlHelper.ExecuteDataset(transaction, CommandType.Text, "SELECT * FROM  IntervalData WHERE  IntervalData_Id = IDENT_CURRENT('IntervalData')").Tables[0];
                                                                IntervalData_Id = dtIntervalData.Rows[0]["IntervalData_Id"].ToString();
                                                            }

                                                            break;
                                                        }
                                                    case "IntervalSpec":
                                                        {
                                                            if (reader.HasAttributes)
                                                            {
                                                                for (int i = 0; i < reader.AttributeCount; i++)
                                                                {
                                                                    reader.MoveToAttribute(i);

                                                                    LoadProfileSummaryColumns += reader.Name + ",";
                                                                    LoadProfileSummaryValues += "'" + reader.Value + "',";
                                                                    if (reader.Name == "Direction")
                                                                    {
                                                                        if (reader.Value == "Delivered")
                                                                        {
                                                                            str_channelnumber = "1";
                                                                        }
                                                                        else if (reader.Value == "Received")
                                                                        {
                                                                            str_channelnumber = "2";
                                                                        }
                                                                        else
                                                                        {
                                                                            str_channelnumber = "3";
                                                                        }
                                                                    }
                                                                }
                                                                queryIntervalSpecInsert = "Insert into IntervalSpec(IntervalData_Id," + LoadProfileSummaryColumns.Remove(LoadProfileSummaryColumns.Length - 1, 1) + ") " +
                                                                    " values (" + IntervalData_Id + "," + LoadProfileSummaryValues.Remove(LoadProfileSummaryValues.Length - 1, 1) + ")";
                                                                returnIntervalSpeccount = SqlHelper.ExecuteNonQuery(transaction, CommandType.Text, queryIntervalSpecInsert);

                                                                LoadProfileSummaryColumns = ""; LoadProfileSummaryValues = "";
                                                            }
                                                            break;
                                                        }
                                                    case "QualityFlags":
                                                        {
                                                            if (reader.HasAttributes)
                                                            {
                                                                for (int i = 0; i < reader.AttributeCount; i++)
                                                                {
                                                                    reader.MoveToAttribute(i);

                                                                    QualityFlagsColumns += reader.Name + ",";
                                                                    QualityFlagsValues += "'" + reader.Value + "',";

                                                                    QualityFlagsUpdateColumns += "" + reader.Name + " = '" + reader.Value + "',";

                                                                    if (reader.Name == "PulseOverflow")
                                                                    {
                                                                        str_pulseoverflowvalue = reader.Value;
                                                                    }
                                                                }
                                                                //if (str_readingid == "")
                                                                //{
                                                                //    queryQualityFlagsInsert = "Insert into QualityFlags(" + QualityFlagsColumns.Remove(QualityFlagsColumns.Length - 1, 1) + ") " +
                                                                //        " values (" + QualityFlagsValues.Remove(QualityFlagsValues.Length - 1, 1) + ")";
                                                                //}
                                                                //else
                                                                //{
                                                                queryQualityFlagsInsert = "Insert into QualityFlags(Reading_Id," + QualityFlagsColumns.Remove(QualityFlagsColumns.Length - 1, 1) + ") " +
                                                                    " values (" + str_readingid + "," + QualityFlagsValues.Remove(QualityFlagsValues.Length - 1, 1) + ")";
                                                                //}
                                                                returnQualityFlagsInsertcount = SqlHelper.ExecuteNonQuery(transaction, CommandType.Text, queryQualityFlagsInsert);
                                                                //str_daintervalmeterdataid_m
                                                                string query_update = "Update daIntervalMeterData set " + QualityFlagsUpdateColumns.Remove(QualityFlagsUpdateColumns.Length - 1, 1) + " where IntervalMeterDataId=" + str_daintervalmeterdataid_m + "";
                                                                int countupdate = SqlHelper.ExecuteNonQuery(transactionMDM, CommandType.Text, query_update);

                                                                string query_update_revs = "Update daIntervalMeterDataRevs set " + QualityFlagsUpdateColumns.Remove(QualityFlagsUpdateColumns.Length - 1, 1) + " where IntervalMeterDataId=" + str_daintervalmeterdataid + "";
                                                                int countupdaterevs = SqlHelper.ExecuteNonQuery(transactionMDM, CommandType.Text, query_update);

                                                                if (QualityFlagsColumns != "")
                                                                {
                                                                    string EventInfo = "";
                                                                    EventInfo = QualityFlagsColumns.Remove(QualityFlagsColumns.Length - 1, 1) + " occured for meter " + Meter_SerialNumber + " at TimeStamp " + timestampvalue + " for " + str_direction + " meter data";
                                                                    string queryeventAMI = "Insert into eventAMI(EventInfo,ElsterMeter_Id,ElsterMeterSerialNumber,TimeStamp) values('" + EventInfo + "','" + Meter_Id + "','" + Meter_SerialNumber + "','" + DateTime.Now + "')";
                                                                    SqlHelper.ExecuteNonQuery(transactionMDM, CommandType.Text, queryeventAMI);
                                                                }

                                                                QualityFlagsColumns = ""; QualityFlagsValues = "";
                                                                QualityFlagsUpdateColumns = "";
                                                            }
                                                            break;
                                                        }
                                                    case "Status":
                                                        {
                                                            if (reader.HasAttributes)
                                                            {
                                                                for (int i = 0; i < reader.AttributeCount; i++)
                                                                {
                                                                    reader.MoveToAttribute(i);

                                                                    StatusColumns += reader.Name + ",";
                                                                    StatusValues += "'" + reader.Value + "',";
                                                                }
                                                                string queryStatusInsert = "Insert into Status(Meter_Id," + StatusColumns.Remove(StatusColumns.Length - 1, 1) + ") " +
                                                                    " values (" + Meter_Id + "," + StatusValues.Remove(StatusValues.Length - 1, 1) + ")";
                                                                returnQualityFlagsInsertcount = SqlHelper.ExecuteNonQuery(transaction, CommandType.Text, queryStatusInsert);
                                                                StatusColumns = ""; StatusValues = "";
                                                            }
                                                            break;
                                                        }

                                                }
                                            }
                                        }
                                    }

                                    //SqlCommand.CommandTimeout = int.MaxValue;


                                    message += XMLcollection[newfile].Name + ",";
                                    //sumofint = SumOfIntervalValues;
                                    //sumofdel = delivered;
                                    //sumofrec = received;
                                    //if (str_channelnumber == "1")
                                    //{
                                    //    if (SumOfIntervalValues != sumofdel)
                                    //    {
                                    //        //dbMDM.Open();
                                    //        //transactionMDM = dbMDM.BeginTransaction();
                                    //        //try
                                    //        //{
                                    //        //    string EventInfo = "";
                                    //        //    EventInfo = "Sumcheck failed for meter " + Meter_SerialNumber + " between " + str_ReadingRangeStartTimestamp + " and " + str_ReadingRangeEndTimestamp + " for Delivered meter data";
                                    //        //    string queryeventAMI = "Insert into eventAMI(EventInfo,ElsterMeter_Id,ElsterMeterSerialNumber) values('" + EventInfo + "','" + Meter_Id + "','" + Meter_SerialNumber + "')";
                                    //        //    SqlHelper.ExecuteNonQuery(transactionMDM, CommandType.Text, queryeventAMI);
                                    //        //    transactionMDM.Commit();
                                    //        //}
                                    //        //catch (Exception ex)
                                    //        //{
                                    //        //    transactionMDM.Rollback();
                                    //        //}
                                    //        //dbMDM.Close();
                                    //    }
                                    //}
                                    //else
                                    //{
                                    //    if (SumOfIntervalValues != sumofrec)
                                    //    {

                                    //    }
                                    //}
                                    //DataRow rownew = dt_Delivered.NewRow();
                                    //rownew["DeliverReadingValues"] = delivered;
                                    //rownew["ReceivedReadingValues"] = received;
                                    //dt_Delivered.Rows.Add(rownew);
                                    //DataTable dt = dt_Delivered.Copy();
                                    //dt_Delivered.Columns.Add(new DataColumn("TotalReadingValues1", Type.GetType("System.String")));
                                    //DataRow[] dr_mserial = dt_Delivered.Select("MeterSerialNumber='" + Meter_SerialNumber + "' and Direction='Delivered'");
                                    //if (dr_mserial.Length > 0)
                                    //{

                                    //    dr_mserial[0]["TotalReadingValues"] = delivered;
                                    //    //dt_Delivered.Rows.Add(rownew);
                                    //}
                                    //DataRow[] dr_mserial_rec = dt_Delivered.Select("MeterSerialNumber='" + Meter_SerialNumber + "' and Direction='Received'");
                                    //if (dr_mserial_rec.Length > 0)
                                    //{

                                    //    dr_mserial_rec[0]["TotalReadingValues"] = received;
                                    //}

                                    for (int n = 0; n < dt_Delivered.Rows.Count; n++)
                                    {
                                        if (dt_Delivered.Rows[n]["Direction"].ToString() == "Delivered")
                                        {
                                            if (dt_Delivered.Rows[n]["SumOfIntervalValues"].ToString() != dt_Delivered.Rows[n]["TotalReadingValues"].ToString())
                                            {

                                                string EventInfo = "";
                                                EventInfo = "Sumcheck failed for meter " + dt_Delivered.Rows[n]["MeterSerialNumber"].ToString() + " between " + dt_Delivered.Rows[n]["ReadingRangeStartTimestamp"].ToString() + " and " + dt_Delivered.Rows[n]["ReadingRangeEndTimestamp"].ToString() + " for Delivered meter data";
                                                string queryeventAMI = "Insert into eventAMI(EventInfo,ElsterMeter_Id,ElsterMeterSerialNumber,TimeStamp,EventCode) values('" + EventInfo + "','" + Meter_Id + "','" + Meter_SerialNumber + "','" + DateTime.Now + "','SCF')";
                                                SqlHelper.ExecuteNonQuery(transactionMDM, CommandType.Text, queryeventAMI);


                                            }

                                        }
                                        else
                                        {
                                            if (dt_Delivered.Rows[n]["SumOfIntervalValues"].ToString() != dt_Delivered.Rows[n]["TotalReadingValues"].ToString())
                                            {

                                                string EventInfo = "";
                                                EventInfo = "Sumcheck failed for meter " + dt_Delivered.Rows[n]["MeterSerialNumber"].ToString() + " between " + dt_Delivered.Rows[n]["ReadingRangeStartTimestamp"].ToString() + " and " + dt_Delivered.Rows[n]["ReadingRangeEndTimestamp"].ToString() + " for Received meter data";
                                                string queryeventAMI = "Insert into eventAMI(EventInfo,ElsterMeter_Id,ElsterMeterSerialNumber,TimeStamp,EventCode) values('" + EventInfo + "','" + Meter_Id + "','" + Meter_SerialNumber + "','" + DateTime.Now + "','SCF')";
                                                SqlHelper.ExecuteNonQuery(transactionMDM, CommandType.Text, queryeventAMI);


                                            }
                                        }
                                        int direction = 1;
                                        string serialnumber = dt_Delivered.Rows[n]["MeterSerialNumber"].ToString();
                                        if (dt_Delivered.Rows[n]["Direction"].ToString() == "Delivered")
                                            direction = 1;
                                        else if (dt_Delivered.Rows[n]["Direction"].ToString() == "Received")
                                            direction = 2;
                                        int minutes = Convert.ToInt32(dt_Delivered.Rows[n]["IntervalSeconds"]) / 60;

                                        DataTable dt_gettimeintervals = DatesbetweenDatatable.getdatatable(Convert.ToDateTime(dt_Delivered.Rows[n]["ReadingRangeStartTimestamp"].ToString()), Convert.ToDateTime(dt_Delivered.Rows[n]["ReadingRangeEndTimestamp"].ToString()), Convert.ToInt32(dt_Delivered.Rows[n]["IntervalSeconds"].ToString()) / 60);
                                        DataTable dt_getMeterDataPoints = SqlHelper.ExecuteDataset(transactionMDM, CommandType.Text, "select * from daintervalmeterdata where source='VALIDATED' and MeterSerialNumber=" + dt_Delivered.Rows[n]["MeterSerialNumber"].ToString() + " and TimeStamp between '" + dt_Delivered.Rows[n]["ReadingRangeStartTimestamp"].ToString() + "' and '" + dt_Delivered.Rows[n]["ReadingRangeEndTimestamp"].ToString() + "' and MeterDirectionId=" + direction).Tables[0];
                                        //DataTable dt_MissingIntervals=dt_getMeterDataPoints.mer
                                        //SPIKE CHECK CODE
                                        DateTime starttimeinterval = Convert.ToDateTime(dt_Delivered.Rows[n]["ReadingRangeStartTimestamp"].ToString());
                                        DateTime endtimeinterval = Convert.ToDateTime(dt_Delivered.Rows[n]["ReadingRangeEndTimestamp"].ToString());
                                        bool spikeCheck = true;
                                        while (spikeCheck)
                                        {
                                            DateTime firstendintervaladdhrs = starttimeinterval.AddHours(24);
                                            //1st HIGHEST 3rd

                                            DataTable dt_nthHighest = new DataTable();
                                            DataColumn dcHDate = new DataColumn();
                                            dcHDate.ColumnName = "Date";
                                            DataColumn dc1Value = new DataColumn();
                                            DataColumn dc3Value = new DataColumn();
                                            dc1Value.ColumnName = "1stHighestValue";
                                            dc3Value.ColumnName = "3rdHighestValue";
                                            dt_nthHighest.Columns.Add(dcHDate);
                                            dt_nthHighest.Columns.Add(dc1Value);
                                            dt_nthHighest.Columns.Add(dc3Value);
                                            DateTime StartingDate = starttimeinterval;
                                            DateTime EndingDate = firstendintervaladdhrs;
                                            //foreach (DateTime date in GetDateRange(StartingDate, EndingDate))
                                            //{
                                            //    WL(date.ToShortDateString());
                                            //}
                                            //DataTable dt_getmeteridfromserialno = SqlHelper.ExecuteDataset(transactionMDM, CommandType.Text, "select MeterDetailsID from MeterDetails where SerialNumber=" + Meter_SerialNumber + "").Tables[0];
                                            //DataTable dt_nthgetMeterDataPoints = SqlHelper.ExecuteDataset(transactionMDM, CommandType.Text,
                                            //    "select MeterDataID,ElsterIntervalDataID,CONVERT(varchar, TimeStamp,101) as TimeStamp,Value,MeterDataID from MeterDataPoints where MeterDetailsID=" + dt_getmeteridfromserialno.Rows[0]["MeterDetailsID"].ToString() + " and" +
                                            //    " TimeStamp between '" + DatesbetweenDatatable.getdatetimeformat(starttimeinterval.ToString()) + "' and '" + DatesbetweenDatatable.getdatetimeformat(firstendintervaladdhrs.ToString()) + "'  order by TimeStamp,Value desc").Tables[0];


                                            //foreach (DateTime date in GetDateRange(StartingDate, EndingDate))
                                            //{
                                            DataRow[] row_day = dt_getMeterDataPoints.Select("TimeStamp >= '" + DatesbetweenDatatable.getdatetimeformat(starttimeinterval.ToString()) + "' and TimeStamp <='" + DatesbetweenDatatable.getdatetimeformat(firstendintervaladdhrs.ToString()) + "'");
                                            DataTable dt_Data = new DataTable();
                                            dt_Data = row_day.CopyToDataTable();
                                            //dt_Data.DefaultView.Sort = "Value ASC";
                                            DataRow row = dt_nthHighest.NewRow();
                                            row[dcHDate] = dt_Data.Rows[0]["TimeStamp"].ToString();
                                            row[dc1Value] = dt_Data.Rows[0]["MeterData"].ToString();
                                            DataView dView = new DataView(dt_Data);
                                            dView.Sort = "MeterData DESC";
                                            DataTable temp = dView.ToTable("DistinctTable", true, "MeterData");
                                            //DataTable Spike_Estimation = dView.ToTable();

                                            if (temp.Rows.Count > 3)
                                            {

                                                double thirdValue = Convert.ToDouble(WebUtility.NullToZero(temp.Rows[2]["MeterData"].ToString()));
                                                double firstvalue = Convert.ToDouble(WebUtility.NullToZero(temp.Rows[0]["MeterData"].ToString()));

                                                double ratio = (firstvalue - thirdValue) / thirdValue;
                                                if (ratio >= spikeCheckConstant)
                                                {
                                                    string EventInfo = "";
                                                    EventInfo = "Spike Check failed for meter " + dt_Delivered.Rows[n]["MeterSerialNumber"].ToString() + " for " + dt_Delivered.Rows[n]["Direction"].ToString() + " between " + StartingDate + " and " + EndingDate + " ";
                                                    string queryeventAMI = "Insert into eventAMI(EventInfo,ElsterMeter_Id,ElsterMeterSerialNumber,TimeStamp,EventCode) values('" + EventInfo + "','" + Meter_Id + "','" + Meter_SerialNumber + "','" + DatesbetweenDatatable.getdatetimeformat(DateTime.Now.ToString()) + "','SC')";
                                                    SqlHelper.ExecuteNonQuery(transactionMDM, CommandType.Text, queryeventAMI);
                                                    DataRow[] dr_gettimestampvaluesbymeterdata = dt_Data.Select("MeterData='" + temp.Rows[0]["MeterData"].ToString() + "'");
                                                    for (int time = 0; time < dr_gettimestampvaluesbymeterdata.Length; time++)
                                                    {
                                                        DataRow[] dr_gettimestampversion = dt_Data.Select("TimeStamp='" + dr_gettimestampvaluesbymeterdata[time]["TimeStamp"].ToString() + "'");
                                                        int version = Convert.ToInt32(dr_gettimestampversion[0]["Version"]) + 1;
                                                        string q_update = "update daIntervalMeterData set MeterData=" + thirdValue + ",Version=" + version + ",Source='ESTIMATED',EstimationInfo='Spike Check' where IntervalMeterDataId=" + dr_gettimestampvaluesbymeterdata[time]["IntervalMeterDataId"].ToString() + "";
                                                        int updatcnt = SqlHelper.ExecuteNonQuery(transactionMDM, CommandType.Text, q_update);
                                                        int insertrevscnt = SqlHelper.ExecuteNonQuery(transactionMDM, CommandType.Text, "insert into daintervalmeterdataRevs(TimeStamp,MeterData,ActualMeterRead,Version,MeterDirectionId,MeterSerialNumber,source,EstimationInfo)values('" + DatesbetweenDatatable.getdatetimeformat(dr_gettimestampvaluesbymeterdata[time]["TimeStamp"].ToString()) + "'," + thirdValue + "," + firstvalue + "," + version + "," + direction + ",'" + serialnumber + "','ESTIMATED','Spike Check')");
                                                    }
                                                }

                                                //row[dc3Value] = nValue;
                                            }
                                            //else if (dt_Data.Rows.Count == 2)
                                            //{


                                            //    //DataTable dt_2nd = SqlHelper.ExecuteDataset(transactionMDM, CommandType.Text, "SELECT  TOP 1 Value FROM    (SELECT DISTINCT TOP 2 Value from daintervalmeterdata where" +
                                            //    //    " MeterSerialNumber=" + Meter_SerialNumber + " and  TimeStamp between '" +starttimeinterval + "' and '" + firstendintervaladdhrs+"')").Tables[0];
                                            //    row[dc3Value] = nValue;
                                            //}
                                            //else
                                            //{
                                            //    row[dc3Value] = dt_Data.Rows[0]["MeterData"].ToString();
                                            //}
                                            //dt_nthHighest.Rows.Add(row);
                                            //}

                                            starttimeinterval = starttimeinterval.AddHours(24);

                                            if (starttimeinterval >= endtimeinterval)
                                                spikeCheck = false;
                                        }

                                        //END OF SPIKE CHECK



                                        DataTable dtIntervals = new DataTable();
                                        DataColumn dcDate = new DataColumn();

                                        DataTable dtIntervals_Range = new DataTable();
                                        DataColumn dcStart_interval = new DataColumn();
                                        DataColumn dcEnd_interval = new DataColumn();

                                        DataColumn dcStart_interval_value = new DataColumn();
                                        DataColumn dcEnd_interval_value = new DataColumn();

                                        dcDate.ColumnName = "Missing Intervals";
                                        dtIntervals.Columns.Add(dcDate);

                                        dcStart_interval.ColumnName = "StartIntervals";
                                        dtIntervals_Range.Columns.Add(dcStart_interval);
                                        dcEnd_interval.ColumnName = "EndIntervals";
                                        dtIntervals_Range.Columns.Add(dcEnd_interval);
                                        dcStart_interval_value.ColumnName = "Value1";
                                        dtIntervals_Range.Columns.Add(dcStart_interval_value);
                                        dcEnd_interval_value.ColumnName = "Value2";
                                        dtIntervals_Range.Columns.Add(dcEnd_interval_value);
                                        //DataTable TableC = dt_gettimeintervals.AsEnumerable().Where(ra => !dt_getMeterDataPoints.AsEnumerable().Any(rb => rb.Field<int>("TimeStamp") != ra.Field<int>("TimeStamp"))).CopyToDataTable();

                                        for (int i = 0; i < dt_gettimeintervals.Rows.Count; i++)
                                        {

                                            DataRow[] dr_getslots = dt_getMeterDataPoints.Select("TimeStamp='" + dt_gettimeintervals.Rows[i]["Interval"].ToString() + "'");
                                            if (dr_getslots.Length == 0)
                                            {
                                                DataRow row = dtIntervals.NewRow();
                                                row[dcDate] = dt_gettimeintervals.Rows[i]["Interval"].ToString();
                                                dtIntervals.Rows.Add(row);





                                            }
                                        }
                                        int count = 0;
                                        try
                                        {

                                            if (dtIntervals.Rows.Count > 0)
                                            {
                                                DataRow row_Range = dtIntervals_Range.NewRow();

                                                row_Range[dcStart_interval] = Convert.ToDateTime(dtIntervals.Rows[count]["Missing Intervals"].ToString()).AddMinutes(-minutes);
                                                DateTime dt_getdatetimeofprev = Convert.ToDateTime(dtIntervals.Rows[count]["Missing Intervals"].ToString()).AddMinutes(-minutes);
                                                DataRow[] dr_getvaluepreviousinfirst = dt_getMeterDataPoints.Select("TimeStamp='" + dt_getdatetimeofprev + "'");
                                                if (dr_getvaluepreviousinfirst.Length == 0)
                                                {
                                                    row_Range[dcStart_interval_value] = "0";
                                                }
                                                else
                                                {
                                                    row_Range[dcStart_interval_value] = dr_getvaluepreviousinfirst[0]["MeterData"].ToString();
                                                }
                                                while (count <= dtIntervals.Rows.Count - 1)
                                                {
                                                    if (count != dtIntervals.Rows.Count - 1)
                                                    {
                                                        count++;
                                                        if (Convert.ToDateTime(dtIntervals.Rows[count]["Missing Intervals"].ToString()) != Convert.ToDateTime(dtIntervals.Rows[count - 1]["Missing Intervals"].ToString()).AddMinutes(minutes))
                                                        {
                                                            row_Range[dcEnd_interval] = Convert.ToDateTime(dtIntervals.Rows[count - 1]["Missing Intervals"].ToString()).AddMinutes(minutes); ;

                                                            DateTime dt_getdatetimeofnext = Convert.ToDateTime(dtIntervals.Rows[count - 1]["Missing Intervals"].ToString()).AddMinutes(minutes);
                                                            DataRow[] dr_getvaluepreviousinnext = dt_getMeterDataPoints.Select("TimeStamp='" + dt_getdatetimeofnext + "'");
                                                            if (dr_getvaluepreviousinnext.Length == 0)
                                                            {
                                                                row_Range[dcEnd_interval_value] = "0";
                                                            }
                                                            else
                                                            {
                                                                row_Range[dcEnd_interval_value] = dr_getvaluepreviousinnext[0]["MeterData"].ToString();
                                                            }

                                                            dtIntervals_Range.Rows.Add(row_Range);
                                                            if (count != dtIntervals.Rows.Count - 1)
                                                            {
                                                                row_Range = dtIntervals_Range.NewRow();
                                                                row_Range[dcStart_interval] = Convert.ToDateTime(dtIntervals.Rows[count]["Missing Intervals"].ToString()).AddMinutes(-minutes);
                                                                DateTime dt_getdatetimeofprev1 = Convert.ToDateTime(dtIntervals.Rows[count]["Missing Intervals"].ToString()).AddMinutes(-minutes);
                                                                DataRow[] dr_getvaluepreviousinfirst1 = dt_getMeterDataPoints.Select("TimeStamp='" + dt_getdatetimeofprev1 + "'");
                                                                if (dr_getvaluepreviousinfirst1.Length == 0)
                                                                {
                                                                    row_Range[dcStart_interval_value] = "0";
                                                                }
                                                                else
                                                                {
                                                                    row_Range[dcStart_interval_value] = dr_getvaluepreviousinfirst1[0]["MeterData"].ToString();
                                                                }

                                                            }
                                                        }
                                                    }
                                                    else
                                                    {
                                                        count++;
                                                        //row_Range = dtIntervals_Range.NewRow();
                                                        row_Range[dcEnd_interval] = Convert.ToDateTime(dtIntervals.Rows[dtIntervals.Rows.Count - 1]["Missing Intervals"].ToString()).AddMinutes(minutes);

                                                        DateTime dt_getdatetimeofnext = Convert.ToDateTime(dtIntervals.Rows[count - 1]["Missing Intervals"].ToString()).AddMinutes(minutes);
                                                        DataRow[] dr_getvaluepreviousinnext = dt_getMeterDataPoints.Select("TimeStamp='" + dt_getdatetimeofnext + "'");
                                                        if (dr_getvaluepreviousinnext.Length == 0)
                                                        {
                                                            row_Range[dcEnd_interval_value] = "0";
                                                        }
                                                        else
                                                        {
                                                            row_Range[dcEnd_interval_value] = dr_getvaluepreviousinnext[0]["MeterData"].ToString();
                                                        }
                                                        dtIntervals_Range.Rows.Add(row_Range);
                                                    }
                                                }
                                            }


                                            DataTable dt1 = dtIntervals.Copy();
                                            DataTable dt2 = dtIntervals_Range.Copy();

                                            foreach (DataRow dr in dt2.Rows)
                                            {
                                                try
                                                {

                                                    DateTime start_intervaltime = Convert.ToDateTime(dr["StartIntervals"].ToString());
                                                    DateTime end_intervaltime = Convert.ToDateTime(dr["EndIntervals"].ToString());
                                                    decimal avgMeterData = (Convert.ToDecimal(dr["Value1"].ToString()) + Convert.ToDecimal(dr["Value2"].ToString())) / 2;
                                                    bool timeFlag = true;
                                                    while (timeFlag)
                                                    {
                                                        start_intervaltime = start_intervaltime.AddMinutes(minutes);

                                                        //insert or update into daintervalmeterdata table source == "ESTIMATED"
                                                        DataTable dt_intcnt = SqlHelper.ExecuteDataset(transactionMDM, CommandType.Text, "select * from daintervalmeterdata where MeterSerialNumber='" + serialnumber + "' and TimeStamp='" + DatesbetweenDatatable.getdatetimeformat(start_intervaltime.ToString()) + "' and MeterDirectionId=" + direction + "").Tables[0];
                                                        if (dt_intcnt.Rows.Count == 0)
                                                        {
                                                            int insertcnt = SqlHelper.ExecuteNonQuery(transactionMDM, CommandType.Text, "insert into daintervalmeterdata(TimeStamp,MeterData,Version,MeterDirectionId,MeterSerialNumber,source,EstimationInfo)values('" + DatesbetweenDatatable.getdatetimeformat(start_intervaltime.ToString()) + "'," + avgMeterData + ",1," + direction + ",'" + serialnumber + "','ESTIMATED','Missing Interval')");
                                                            int insertrevscnt = SqlHelper.ExecuteNonQuery(transactionMDM, CommandType.Text, "insert into daintervalmeterdataRevs(TimeStamp,MeterData,Version,MeterDirectionId,MeterSerialNumber,source,EstimationInfo)values('" + DatesbetweenDatatable.getdatetimeformat(start_intervaltime.ToString()) + "'," + avgMeterData + ",1," + direction + ",'" + serialnumber + "','ESTIMATED','Missing Interval')");
                                                        }
                                                        else
                                                        {
                                                            int versioncnt = Convert.ToInt32(dt_intcnt.Rows[0]["Version"]) + 1;
                                                            int insertcnt = SqlHelper.ExecuteNonQuery(transactionMDM, CommandType.Text, "update daintervalmeterdata set MeterData =" + avgMeterData + " ,Version=" + versioncnt + ",source='ESTIMATED',EstimationInfo='Missing Interval',ActualMeterRead=" + dt_intcnt.Rows[0]["MeterData"] + " where MeterSerialNumber='" + serialnumber + "' and TimeStamp='" + DatesbetweenDatatable.getdatetimeformat(start_intervaltime.ToString()) + "' and MeterDirectionId=" + direction + "");
                                                            int insertrevscnt = SqlHelper.ExecuteNonQuery(transactionMDM, CommandType.Text, "insert into daintervalmeterdataRevs(TimeStamp,MeterData,Version,MeterDirectionId,MeterSerialNumber,source,EstimationInfo)values('" + DatesbetweenDatatable.getdatetimeformat(start_intervaltime.ToString()) + "'," + avgMeterData + "," + versioncnt + "," + direction + ",'" + serialnumber + "','ESTIMATED','Missing Interval')");
                                                        }

                                                        if (start_intervaltime.AddMinutes(minutes).Equals(end_intervaltime))
                                                        {
                                                            timeFlag = false;
                                                        }
                                                        //DataRow[] dr_estmatedinterval=serialnumber
                                                        //insert into daintervalmeterdatarevs table

                                                    }
                                                }
                                                catch (Exception ex)
                                                {
                                                    lbl_message.Text = ex.Message;
                                                    lbl_message.ForeColor = Color.Red;
                                                    transaction.Rollback();
                                                    transactionMDM.Rollback();
                                                }
                                            }
                                        }
                                        catch (Exception ex)
                                        {
                                            lbl_message.Text = ex.Message;
                                            lbl_message.ForeColor = Color.Red;
                                            transaction.Rollback();
                                            transactionMDM.Rollback();
                                        }

                                        //transactionMDM.Commit();
                                    }

                                    transactionMDM.Commit();

                                    transaction.Commit();

                                    sourceFile = Server.MapPath("~/PreVEEFiles/ExtractedFiles/" + zipfilename + "/" + XMLcollection[newfile].Name);
                                    destinationFile = Server.MapPath("~/PreVEEFiles/ImportedFiles/" + zipfilename + "/" + XMLcollection[newfile].Name);
                                    System.IO.File.Copy(sourceFile, destinationFile, true);
                                    try
                                    {
                                        // System.IO.File.Delete(Server.MapPath(@"~/NewFiles\" + XMLcollection[newfile].Name));
                                        DataRow[] row_xmlFile = dtPreVEEExtractedFiles.Select("ExtractedFileName='" + XMLcollection[newfile].Name + "'");
                                        if (row_xmlFile.Length > 0)
                                        {
                                            SqlHelper.ExecuteNonQuery(GlobalConnetionString.ClientConnection(), CommandType.Text,
                                                "Update daPreVEEExtractedFiles set ExtractedFileStatus='True' where ExtractedFileName='" + XMLcollection[newfile].Name + "'");
                                        }

                                    }
                                    catch (System.IO.IOException ex)
                                    {
                                        lbl_message.Text = ex.Message;
                                        lbl_message.ForeColor = Color.Red;
                                        transaction.Rollback();
                                        transactionMDM.Rollback();
                                    }
                                    //}
                                }
                                catch (SqlException ex)
                                {
                                    DataRow[] row_xmlFile = dtPreVEEExtractedFiles.Select("ExtractedFileName='" + XMLcollection[newfile].Name + "'");
                                    if (row_xmlFile.Length > 0)
                                    {
                                        SqlHelper.ExecuteNonQuery(GlobalConnetionString.ClientConnection(), CommandType.Text,
                                            "Update daPreVEEExtractedFiles set ExtractedFileStatus='False' where ExtractedFileName='" + XMLcollection[newfile].Name + "'");
                                    }
                                    lbl_message.Text = ex.Message;
                                    lbl_message.ForeColor = Color.Red;
                                    transaction.Rollback();
                                    transactionMDM.Rollback();
                                }


                                db.Close();
                                dbMDM.Close();
                            }
                            lbl_message.ForeColor = Color.Green;
                            if (message != "")
                            {
                                lbl_message.Text = message.Remove(message.Length - 1, 1) + " Data Migrated Successfully";
                                id_msg.Visible = true;
                            }
                            else
                                lbl_message.Text = "Data Migration Failed";
                        }
                        catch (Exception ex)
                        {
                            lbl_message.Text = ex.Message;
                            lbl_message.ForeColor = Color.Red;
                            //transaction.Rollback();
                        }
                    }
                }
            }
        }
    }
    protected void btn_savetemplate_Click(object sender, EventArgs e)
    {
        lbl_url.Text = "";
        launchURL();
        SqlHelper.ExecuteNonQuery(GlobalConnetionString.ClientConnection(), CommandType.Text, "Insert into daServiceTemplates(serviceId,templateName,URL)" +
            " values ('" + radcombo_services.SelectedValue + "','" + txt_templatename.Text + "','" + lbl_url.Text + "')");

    }
}