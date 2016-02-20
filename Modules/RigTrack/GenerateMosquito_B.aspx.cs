using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Modules_RigTrack_GenerateMosquito_B : System.Web.UI.Page
{
    #region page load
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
        }
    }
    #endregion
    #region bottom buttons
    protected void btnClear_Click(object sender, EventArgs e)
    {
        //Response.Redirect("CreateCurveGroup.aspx");
    }
    #endregion
}