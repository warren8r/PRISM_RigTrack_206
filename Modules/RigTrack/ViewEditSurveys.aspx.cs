using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

public partial class Modules_RigTrack_ViewEditSurveys : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void btnSearchCurve_Click(object sender, EventArgs e)
    {

    }
    protected void btnAddSurvey_Click(object sender, EventArgs e)
    {

    }
    protected void RadGridSurveys_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
    {

    }
    protected void chkbx_CheckedChanged(object sender, EventArgs e)
    {
        ((sender as CheckBox).NamingContainer as GridItem).Selected = (sender as CheckBox).Checked;

        
    }
}