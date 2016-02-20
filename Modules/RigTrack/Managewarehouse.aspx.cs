using System;
using System.Linq;


public partial class Modules_Configuration_Manager_SDP : System.Web.UI.Page
{
    /// <summary>
    /// SDP Management Page, using Telerik DevSuite and Artem Geocoding API
    /// Author: Jason Burton, Limitless Healthcare IT
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    
    #region Page_Load
    
    protected void Page_Load(object sender, EventArgs e)
    {
        //use dynamic connection strings
        //sqldsInsertSDP.ConnectionString = Session["client_database"].ToString();
        //sqldsViewEditGIS.ConnectionString = Session["client_database"].ToString();
        //ConstantClass constant = new ConstantClass();

        //RadTabStrip1.Tabs[0].Text = constant.ReturnConstantWarehouse(3).ToString() + "s";
        //RadTabStrip1.Tabs[1].Text = constant.ReturnConstantWarehouse(4).ToString() + "s";
        ////RadTabStrip1.Tabs[2].Text = constant.ReturnConstant(5).ToString() + "s";

        //if (Request.QueryString["t"] != null)
        //    try
        //    {
        //        int id = 0;
        //        id = int.Parse(Request.QueryString["t"]);
        //        RadTabStrip1.SelectedIndex = 2;
        //        RadMultiPage1.SelectedIndex = 2;
        //    }
        //    catch
        //    {
        //        // deal with it
        //    }
    }

    #endregion
    
    /// <summary>
    /// The navigation piece. Requires an index. No parameters.
    /// Author: Jason Burton, Limitless Healthcare IT 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    
    #region NavigationButtonsInsertEvent
        
    protected void RadButton1_Click(object sender, EventArgs e)
    {
        //RadTabStrip1.SelectedIndex = 1;
        //RadMultiPage1.SelectedIndex = 1;
    }
        
    protected void RadButton2_Click(object sender, EventArgs e)
    {
        //RadTabStrip1.SelectedIndex = 2;
        //RadMultiPage1.SelectedIndex = 2;
    }
    
    #endregion

   
}