using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Modules_DA_LaunchVEE : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void RadDropDownList1_SelectedIndexChanged(object sender, Telerik.Web.UI.DropDownListEventArgs e)
    {
        rptRoutineDetail.DataBind();


    }
}