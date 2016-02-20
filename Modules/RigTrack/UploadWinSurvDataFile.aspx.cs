using Artem.Google.Net;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Odbc;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using System.Drawing;

public partial class Modules_Configuration_Manager_UploadWinSurvDataFile : System.Web.UI.Page
{
    MDM.Collector Collector = new MDM.Collector();
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void btn_view_OnClick(object sender, EventArgs e)
    {
        string filenamese = DateTime.Now.ToString("yyyy'_'MM'_'dd'_'HH'_'mm'_'ss"); // 2007-07-21T15:12:57
        string filename = filenamese + "_" + file_dayrun.FileName;
        file_dayrun.PostedFile.SaveAs(Server.MapPath("~/MdfFiles\\") + filename);
        string path = Server.MapPath("~/MdfFiles\\") + filename;
        string getexistornot = getexistingrecordsornot(path);
        if (getexistornot == "Success")
        {
            getdataandinsertdata("tbljob", path);
            getdataandinsertdata("tblbhaHydinputs", path);
            getdataandinsertdata("TblBHAINFO", path);
            getdataandinsertdata("tblbhaitems", path);
            getdataandinsertdata("tblBHAMWDRuns", path);

            getdataandinsertdata("tblJobCasing", path);
            getdataandinsertdata("tblJOBcost", path);
            getdataandinsertdata("tblJOBDATE", path);
            getdataandinsertdata("tblJOBDATEitems", path);
            getdataandinsertdata("Tbljobinventory", path);
            getdataandinsertdata("tblJobsurveys", path);
            getdataandinsertdata("tblJobSurvinfo", path);
            getdataandinsertdata("tbljobunits", path);
            getdataandinsertdata("tblReturned", path);
            getdataandinsertdata("tblserreturnjob", path);
            DataTable dt_maxidtblid = SqlHelper.ExecuteDataset(GlobalConnetionString.ClientConnection(), CommandType.Text, "SELECT MAX([Job Id]) as jobid FROM  tbljob").Tables[0];
            if (dt_maxidtblid.Rows.Count > 0)
            {
                combo_job.Text = dt_maxidtblid.Rows[0]["jobid"].ToString();
                info.Visible = true;
            }
            lbl_message.Text = "Data Uploaded Successfully";
            lbl_message.ForeColor = Color.Green;
        }
        else
        {
            lbl_message.Text = "Error";
            lbl_message.ForeColor = Color.Red;
        }
    }
    private List<Telerik.Web.UI.UploadedFileInfo> uploadedFiles = new List<Telerik.Web.UI.UploadedFileInfo>();
    public List<Telerik.Web.UI.UploadedFileInfo> UploadedFiles
    {
        get { return uploadedFiles; }
        set { uploadedFiles = value; }
    }

    public string getexistingrecordsornot(string path)
    {
        string tablename = "tbljob";
        string insertmessage = "";
        DataTable dt=new DataTable();
        
        //string aaa = Server.MapPath(file_dayrun.FileName);//Path.GetFullPath(file_dayrun.PostedFile.FileName);//Server.MapPath(file_dayrun.FileName);
            //string myConnectionString = @"Driver={Microsoft Access Driver (*.mdb)};" + "Dbq=" + aaa + ";DSN=myDSN;Uid=Admin;Pwd=;";
        string strAccessConn = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + path + ";";
            //string strAccessConn = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=BugTypes.MDB";
            DataSet myDataSet = new DataSet();
            //OleDbConnection myAccessConn = null;
            string strAccessSelect = "SELECT * FROM " + tablename + "";
            OleDbConnection myAccessConn = null;
            try
            {
                myAccessConn = new OleDbConnection(strAccessConn);
            }
            catch (Exception ex)
            {
                
            }
            try
            {


                OleDbCommand myAccessCommand = new OleDbCommand(strAccessSelect, myAccessConn);
                OleDbDataAdapter myDataAdapter = new OleDbDataAdapter(myAccessCommand);
                myAccessConn.Open();
                myDataAdapter.Fill(myDataSet, tablename);
                DataTable dtgetexistingjobs = myDataSet.Tables[0];
                DataTable dtgetdetfrommanagejoborders=SqlHelper.ExecuteDataset(GlobalConnetionString.ClientConnection(), CommandType.Text, "select * from manageJobOrders").Tables[0];
                
                for (int k = 0; k < dtgetexistingjobs.Rows.Count; k++)
                {
                    DataRow[] dr = dtgetdetfrommanagejoborders.Select("JobName='" + dtgetexistingjobs.Rows[k]["Job ID"].ToString()+"'");

                    if (dr.Length == 0)
                    {
                        string rigtypeid = getrigtype(dtgetexistingjobs.Rows[k]["Rig Name"].ToString());
                        string jobtypeid = getjobtypeid(dtgetexistingjobs.Rows[k]["Job Type"].ToString());
                        string location = dtgetexistingjobs.Rows[k]["Location"].ToString();
                        string state = dtgetexistingjobs.Rows[k]["State/Province"].ToString();
                        string county = dtgetexistingjobs.Rows[k]["County/Parish"].ToString();
                        string country = dtgetexistingjobs.Rows[k]["Country"].ToString();

                        string address = location + " " + state + " " + county + " " + country;
                        string gislotlonginfo = getgislastlong(address);
                        string txtAssetNumber = Collector.GenerateNewAccountID(0);
                        string queryInsert = "Insert into manageJobOrders(bitActive,jobname,jobid,jobtype,startdate,enddate,Customer,primaryFirst,primaryLast," +
                                           "primaryAddress1,primaryAddress2,primaryCity,primaryState,primaryCountry,primaryPostalCode,primaryPhone1,primaryPhone2,primaryEmail,primaryLatLong," +
                                           "primaryLatLongAccuracy,secondaryFirst,secondaryLast,secondaryAddress1,secondaryAddress2,secondaryCity,secondaryState,secondaryCountry,secondaryPostalCode," +
                                           "secondaryPhone1,secondaryEmail,secondaryPhone2,secondaryLatLong,secondaryLatLongAccuracy,status,opManagerId,jobordercreatedid,approveddatetime,opmgrnotes,salecreateddate,jobsource,rigtypeid,jobfrom) values ('True','" + dtgetexistingjobs.Rows[k]["Job ID"].ToString() + "'," +
                                           "'" + txtAssetNumber + "','" + jobtypeid + "','" + DatesbetweenDatatable.getdatetimeformat(DateTime.Now.ToString()) + "','" + DatesbetweenDatatable.getdatetimeformat(DateTime.Now.AddMonths(1).ToString()) + "','','" + dtgetexistingjobs.Rows[k]["NAME1"].ToString() + "'," +
                                           "'" + dtgetexistingjobs.Rows[k]["NAME2"].ToString() + "','" + location + "','','" + county + "','" + state + "'," +
                                           "'" + country + "','','','',''," +
                                           "'" + gislotlonginfo + "','','',''," +
                                           "'','','','',''," +
                                           "'','','','','',''," +
                                           "'Approved'," + Session["userId"].ToString() + ",'J" + txtAssetNumber + "','" + DatesbetweenDatatable.getdatetimeformat(DateTime.Now.ToString()) + "','','" + DatesbetweenDatatable.getdatetimeformat(DateTime.Now.ToString()) + "','Direct'," + rigtypeid + ",'WinSurv')";
                        int insertcnt = SqlHelper.ExecuteNonQuery(GlobalConnetionString.ClientConnection(), CommandType.Text, queryInsert);
                        if (insertcnt > 0)
                        {
                            insertmessage = "Success";
                        }
                        else
                        {
                            insertmessage = "Error";
                        }
                    }
                    else
                    {
                        SqlHelper.ExecuteNonQuery(GlobalConnetionString.ClientConnection(), CommandType.Text, "delete tbljob where [Job ID]=" + dr[0]["JobName"].ToString() + " ");
                        SqlHelper.ExecuteNonQuery(GlobalConnetionString.ClientConnection(), CommandType.Text, "delete tblbhaHydinputs where [Job Id]=" + dr[0]["JobName"].ToString() + " ");
                        SqlHelper.ExecuteNonQuery(GlobalConnetionString.ClientConnection(), CommandType.Text, "delete TblBHAINFO where [job no id]=" + dr[0]["JobName"].ToString() + " ");
                        SqlHelper.ExecuteNonQuery(GlobalConnetionString.ClientConnection(), CommandType.Text, "delete tblbhaitems where [Job no ID]=" + dr[0]["JobName"].ToString() + " ");
                        SqlHelper.ExecuteNonQuery(GlobalConnetionString.ClientConnection(), CommandType.Text, "delete tblBHAMWDRuns where [job no id]=" + dr[0]["JobName"].ToString() + " ");
                        SqlHelper.ExecuteNonQuery(GlobalConnetionString.ClientConnection(), CommandType.Text, "delete tblJobCasing where [Job ID]=" + dr[0]["JobName"].ToString() + " ");
                        SqlHelper.ExecuteNonQuery(GlobalConnetionString.ClientConnection(), CommandType.Text, "delete tblJOBcost where [JOB ID]=" + dr[0]["JobName"].ToString() + " ");
                        SqlHelper.ExecuteNonQuery(GlobalConnetionString.ClientConnection(), CommandType.Text, "delete tblJOBDATE where [Job ID]=" + dr[0]["JobName"].ToString() + " ");
                        SqlHelper.ExecuteNonQuery(GlobalConnetionString.ClientConnection(), CommandType.Text, "delete tblJOBDATEitems where [Job ID]=" + dr[0]["JobName"].ToString() + " ");
                        SqlHelper.ExecuteNonQuery(GlobalConnetionString.ClientConnection(), CommandType.Text, "delete Tbljobinventory where [JOBID]=" + dr[0]["JobName"].ToString() + " ");
                        SqlHelper.ExecuteNonQuery(GlobalConnetionString.ClientConnection(), CommandType.Text, "delete tblJobsurveys where [JobID]=" + dr[0]["JobName"].ToString() + " ");
                        SqlHelper.ExecuteNonQuery(GlobalConnetionString.ClientConnection(), CommandType.Text, "delete tblJobSurvinfo where [JobID]=" + dr[0]["JobName"].ToString() + " ");
                        SqlHelper.ExecuteNonQuery(GlobalConnetionString.ClientConnection(), CommandType.Text, "delete tbljobunits where [JOB ID]=" + dr[0]["JobName"].ToString() + " ");
                        SqlHelper.ExecuteNonQuery(GlobalConnetionString.ClientConnection(), CommandType.Text, "delete tblReturned where [JOB ID]=" + dr[0]["JobName"].ToString() + " ");
                        SqlHelper.ExecuteNonQuery(GlobalConnetionString.ClientConnection(), CommandType.Text, "delete tblserreturnjob where [JOB  ID]='" + dr[0]["JobName"].ToString() + "'");
                        insertmessage = "Success";
                    }
                }
            }
            catch (Exception ex)
            {
                insertmessage = "Error";
                
            }
            return insertmessage;
    }
    public string getrigtype(string rigname)
    {
        DataTable dt_getrig = SqlHelper.ExecuteDataset(GlobalConnetionString.ClientConnection(), CommandType.Text, "select * from RigTypes where rigtypename='" + rigname + "'").Tables[0];
            string rigtypeid="";
            if(dt_getrig.Rows.Count==0)
            {
                SqlHelper.ExecuteNonQuery(GlobalConnetionString.ClientConnection(), CommandType.Text, "insert into RigTypes(rigtypename)values('"+rigname+"')");

                DataTable dt_maxid = SqlHelper.ExecuteDataset(GlobalConnetionString.ClientConnection(), CommandType.Text, "SELECT * FROM  RigTypes WHERE  rigtypeid = IDENT_CURRENT('RigTypes')").Tables[0];
                rigtypeid = dt_maxid.Rows[0]["rigtypeid"].ToString();
            }
            else
            {
                rigtypeid=dt_getrig.Rows[0]["rigtypeid"].ToString();
            }

        return rigtypeid;
    }
    public string getjobtypeid(string jobtypename)
    {
        string jobtypeid = "";
        DataTable dt_getrig = SqlHelper.ExecuteDataset(GlobalConnetionString.ClientConnection(), CommandType.Text, "select * from jobTypes where jobtype='" + jobtypename + "'").Tables[0];
        
        if(dt_getrig.Rows.Count==0)
        {
            SqlHelper.ExecuteNonQuery(GlobalConnetionString.ClientConnection(), CommandType.Text, "insert into jobTypes(jobtype)values('"+jobtypename+"')");

            DataTable dt_maxid = SqlHelper.ExecuteDataset(GlobalConnetionString.ClientConnection(), CommandType.Text, "SELECT * FROM  jobTypes WHERE  jobtypeid = IDENT_CURRENT('jobTypes')").Tables[0];
            jobtypeid = dt_maxid.Rows[0]["jobtypeid"].ToString();
        }
        else
        {
            jobtypeid=dt_getrig.Rows[0]["jobtypeid"].ToString();
        }
        return jobtypeid;
    }
    public string getgislastlong(string address)
    {
        string lotlonginfo = "";

        string addressToCheck = address;
        //string addressToCheck = "Kersey CO Weld US"; //txtprimaryAddress1.Text + " " + txtprimaryAddress2.Text + " " + txtprimaryCity.Text + ", " + ddlPrimaryState.SelectedItem.Value + " " + txtprimaryPostalCode.Text + " " + ddlPrimaryCountry.SelectedItem.Value;

        try
        {
            
                GeoRequest gr = new GeoRequest(addressToCheck);
                GeoResponse gRes = gr.GetResponse();
                
                Debug.WriteLine(addressToCheck);
                // Set Latitude/Longitude in textbox
                lotlonginfo = gRes.Results[0].Geometry.Location.Latitude.ToString() + "," + gRes.Results[0].Geometry.Location.Longitude.ToString();
                //txtPrimaryGIS.Text = gRes.Results[0].Geometry.Location.Latitude.ToString() + "," + gRes.Results[0].Geometry.Location.Longitude.ToString();
                //hypPrimaryMapLink.NavigateUrl = "http://maps.google.com/?q=" + gRes.Results[0].Geometry.Location.Latitude.ToString() + "," + gRes.Results[0].Geometry.Location.Longitude.ToString();

                //GoogleMap1.Latitude = gRes.Results[0].Geometry.Location.Latitude;
                //GoogleMap1.Longitude = gRes.Results[0].Geometry.Location.Longitude;

                //Debug.WriteLine("Lat:" + GoogleMap1.Latitude.ToString());
                //Debug.WriteLine("Lon:" + GoogleMap1.Longitude.ToString());

                string resultLocationMatch = gRes.Results[0].Geometry.LocationType.ToString();
                Debug.WriteLine(resultLocationMatch);

                string resultAccuracy = "";
                switch (resultLocationMatch)
                {
                    // See: http://stackoverflow.com/questions/3015370/how-to-get-the-equivalent-of-the-accuracy-in-google-map-geocoder-v3
                    case "ROOFTOP":
                        resultAccuracy = "Precise";
                        break;
                    case "RANGE_INTERPOLATED":
                        resultAccuracy = "Approximate";
                        break;
                    case "GEOMETRIC_CENTER":
                        resultAccuracy = "Approximate";
                        break;
                    case "APPROXIMATE":
                        resultAccuracy = "Approximate";
                        break;
                    default:
                        resultAccuracy = resultLocationMatch;
                        break;
                }
                Debug.WriteLine(resultLocationMatch);

                //lotlonginfo = gRes.Results[0].Types[0].ToString().ToUpper() + " (" + resultAccuracy + ")";
                


            
        }
        catch (Exception ex)
        {
            //lnkBtnGatherGISPrimary.Enabled = true;
            throw;
        }
        
        return lotlonginfo;
    }
    public void getdataandinsertdata(string tablename,string path)
    {

            //string aaa = Server.MapPath(file_dayrun.FileName);//Path.GetFullPath(file_dayrun.PostedFile.FileName);//Server.MapPath(file_dayrun.FileName);
            //string myConnectionString = @"Driver={Microsoft Access Driver (*.mdb)};" + "Dbq=" + aaa + ";DSN=myDSN;Uid=Admin;Pwd=;";
            string strAccessConn = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + path + ";";
            //string strAccessConn = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=BugTypes.MDB";
            DataSet myDataSet = new DataSet();
            //OleDbConnection myAccessConn = null;
            string strAccessSelect = "SELECT * FROM " + tablename + "";
            OleDbConnection myAccessConn = null;
            try
            {
                myAccessConn = new OleDbConnection(strAccessConn);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: Failed to create a database connection. \n{0}", ex.Message);
                return;
            }
            try
            {


                OleDbCommand myAccessCommand = new OleDbCommand(strAccessSelect, myAccessConn);
                OleDbDataAdapter myDataAdapter = new OleDbDataAdapter(myAccessCommand);
                myAccessConn.Open();
                myDataAdapter.Fill(myDataSet, tablename);
                SqlBulkCopy bulkcopy = new SqlBulkCopy(GlobalConnetionString.ClientConnection());
                //I assume you have created the table previously
                //Someone else here already showed how  
                bulkcopy.DestinationTableName = myDataSet.Tables[0].TableName;
                try
                {
                    bulkcopy.WriteToServer(myDataSet.Tables[0]);
                }
                catch (Exception e)
                {
                    // messagebox.show(e.message);
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: Failed to retrieve the required data from the DataBase.\n{0}", ex.Message);
                return;
            }
       


        //
        ////string constr = @"Data Source=.\SQLEXPRESS;AttachDbFilename=|DataDirectory|\" + aaa + ";Integrated Security=True;User Instance=True;";
        ////string connectionString = "Data Source=.\\SQLEXPRESS;AttachDbFilename={0}\\Seminar Library CSE KU\\" + file_dayrun.PostedFile.FileName + ";Integrated Security=True;Connect Timeout=30;User Instance=True";
        //string constr = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=|DataDirectory|\" + aaa + ";Persist Security Info=False;";

        ////constr = string.Format(connectionString, aaa);
        ////else
        ////        connectionString = string.Format(connectionString, Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData));
        

        ////String connString = "Data Source=.\\SQLEXPRESS;AttachDbFilename=" + aaa + ";Integrated Security=True;Connect Timeout=30;User Instance=True";
        //SqlConnection con = new SqlConnection(constr);
        //con.Open();

        //using (SqlCommand command = new SqlCommand("SELECT * from tblbhaHydinputs where [Job Id] = " + row, con))
        //{
        //    SqlDataReader reader = command.ExecuteReader();
        //    while (reader.Read())
        //    {


        //    }
        //}
        //con.Close();
        

    }
}