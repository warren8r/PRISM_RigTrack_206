using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.ServiceProcess;
public partial class veeConstants : System.Web.UI.Page
{
    DataTable dt_veeconstant;
    protected void Page_Load(object sender, EventArgs e)
    {

        dt_veeconstant = SqlHelper.ExecuteDataset(GlobalConnetionString.ClientConnection(), CommandType.Text, "Select * from veeConstants").Tables[0];
        if (!IsPostBack)
        {
            bindveeConstantData();


            //ServiceController service = new ServiceController();

            //service.ServiceName = "MDMService";
            ////service.MachineName = "IAS-INDIA-PC";
            //string svcStatus = service.Status.ToString();
            //if (svcStatus == "Stopped")
            //{
            //    btn_stop.Enabled = false;
            //    btn_start.Enabled = true;
            //}
            //else
            //{
            //    btn_stop.Enabled = true;
            //    btn_start.Enabled = false;
            //}
        }
    }
    public void bindveeConstantData()
    {
        if (dt_veeconstant.Rows.Count > 0)
        {
            txt_timetolerance.Text = dt_veeconstant.Rows[0]["meterTimeToleranceDur"].ToString();
            rbtn_zeropluse.SelectedValue = dt_veeconstant.Rows[0]["resMeterZeroPulse"].ToString();
            rbtn_dailybilling.SelectedValue = dt_veeconstant.Rows[0]["dailyIntervalBilling"].ToString();
            txt_CTR.Text = dt_veeconstant.Rows[0]["CTR"].ToString();
            txt_VTR.Text = dt_veeconstant.Rows[0]["VTR"].ToString();
            txt_SpikeCheck.Text = dt_veeconstant.Rows[0]["spikeCheckThreshold"].ToString();
            txt_kvarThreshold.Text = dt_veeconstant.Rows[0]["kVARhThreshold"].ToString();
            txt_highlowconstant.Text = dt_veeconstant.Rows[0]["highLowConstant"].ToString();
            radcombo_datamig.SelectedValue = dt_veeconstant.Rows[0]["webservicetype"].ToString();
            radtxt_percentage.Text = dt_veeconstant.Rows[0]["highlowpercentage"].ToString();
            hidden_veeid.Value = "1";
        }
        else
        {
            hidden_veeid.Value = "0";
        }
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        ServiceController service = new ServiceController();

        service.ServiceName = "MDMService";
        //service.MachineName = "IAS-INDIA-PC";
        string svcStatus = service.Status.ToString();
        if (svcStatus == "Stopped")
        {
            service.Start();
            btn_stop.Enabled = true;
            btn_start.Enabled = false;
        }
    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        ServiceController service = new ServiceController();

        service.ServiceName = "MDMService";
        //service.MachineName = "IAS-INDIA-PC";
        string svcStatus = service.Status.ToString();
        if (svcStatus == "Running")
        {
            service.Stop();
            btn_stop.Enabled = false;
            btn_start.Enabled = true;
        }
    }
    protected void btn_save_Click(object sender, EventArgs e)
    {
        if (hidden_veeid.Value == "0")
        {
            string queryInsert = "Insert into veeConstants(meterTimeToleranceDur,resMeterZeroPulse,dailyIntervalBilling," +
            " CTR,VTR,spikeCheckThreshold,kVARhThreshold,highLowConstant,webservicetype,highlowpercentage) values" +
            " ('" + txt_timetolerance.Text + "','" + rbtn_zeropluse.SelectedValue + "','" + rbtn_dailybilling.SelectedValue + "'," +
            " '" + txt_CTR.Text + "','" + txt_VTR.Text + "','" + txt_SpikeCheck.Text + "','" + txt_kvarThreshold.Text + "','" + txt_highlowconstant.Text + "','" + radcombo_datamig.SelectedValue + "'," + radtxt_percentage.Text + ")";
            try
            {
                int count = SqlHelper.ExecuteNonQuery(GlobalConnetionString.ClientConnection(), CommandType.Text, queryInsert);
                lbl_message.Text = "veeConstant details inserted successfully";
                lbl_message.ForeColor = Color.Green;
            }
            catch (Exception ex)
            {
                lbl_message.Text = ex.Message;
                lbl_message.ForeColor = Color.Red;
            }
        }
        else
        {
            string queryUpdate = "Update veeConstants set meterTimeToleranceDur='" + txt_timetolerance.Text + "',resMeterZeroPulse='" + rbtn_zeropluse.SelectedValue + "'," +
                " dailyIntervalBilling='" + rbtn_dailybilling.SelectedValue + "',CTR='" + txt_CTR.Text + "',VTR='" + txt_VTR.Text + "',spikeCheckThreshold='" + txt_SpikeCheck.Text + "'" +
            " ,kVARhThreshold='" + txt_kvarThreshold.Text + "',highLowConstant='" + txt_highlowconstant.Text + "',webservicetype='" + radcombo_datamig.SelectedValue + "',highlowpercentage="+radtxt_percentage.Text+"";
            try
            {
                int count = SqlHelper.ExecuteNonQuery(GlobalConnetionString.ClientConnection(), CommandType.Text, queryUpdate);
                lbl_message.Text = "veeConstant details updated successfully";
                lbl_message.ForeColor = Color.Green;
            }
            catch (Exception ex)
            {
                lbl_message.Text = ex.Message;
                lbl_message.ForeColor = Color.Red;
            }
        }


    }
    protected void btn_cancel_Click(object sender, EventArgs e)
    {
        bindveeConstantData();
    }
}