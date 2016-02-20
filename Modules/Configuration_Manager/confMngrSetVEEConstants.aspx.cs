using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Diagnostics;

public partial class Modules_Configuration_Manager_confMngrSetVEEConstants : System.Web.UI.Page
{
    public void Page_Load(object sender, EventArgs e)
    {   

    }
    
    private void InsertConstant() //method to insert VEE constants
    {
        SqlConnection conn = new SqlConnection("uid=sa; password=sa; server=172.18.12.200; database=Elster");
        string sql = "INSERT INTO veeConstants (CTR,VTR,spike) VALUES (@Val1,@Val2,@Val3)";
        try
        {
            conn.Open();

            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@Val1", txtCTR.Text); //Current Transformer Ratio
            cmd.Parameters.AddWithValue("@Val2", txtVTR.Text);  //Voltage Transformer Ratio
            cmd.Parameters.AddWithValue("@Val3", txtSpike.Text);  //Peak Ratio Constant
            cmd.CommandType = CommandType.Text;
            cmd.ExecuteNonQuery();
        }
        catch (System.Data.SqlClient.SqlException ex)
        {
            string msg = "Insert Error:";
            msg += ex.Message;
            throw new Exception(msg);
        }
        finally
        {
            conn.Close();
        }
    }


    protected void Button1_Click1(object sender, EventArgs e)
    {
        InsertConstant();
    }
}