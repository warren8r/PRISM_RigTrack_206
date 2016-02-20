using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;

public partial class Modules_Configuration_Manager_confMngrManageSDPAccounts : System.Web.UI.Page
{
    public void Page_Load(object sender, EventArgs e)
    {   //establish DB connection
/*
        SqlConnection firstClientCon = new SqlConnection("uid=mdmsand; password=mdmsand$; server=172.18.12.200; database=MDM_Staging_Client_1");
        SqlCommand cmd = new SqlCommand("SELECT name FROM utility_3 WHERE ID=2", firstClientCon);
        firstClientCon.Open();
        DataSet results = new DataSet();
        SqlDataAdapter da = new SqlDataAdapter();
        da.SelectCommand = cmd;
        da.Fill(results);
        firstClientCon.Close();
*/
    }

    protected void DropDownList2_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void sqlDSParentAsset_Selecting(object sender, SqlDataSourceSelectingEventArgs e)
    {

    }
    protected void sqlDSClientList_Selecting(object sender, SqlDataSourceSelectingEventArgs e)
    {

    }
    protected void Button1_Click(object sender, EventArgs e)
    {
    }
    protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
}