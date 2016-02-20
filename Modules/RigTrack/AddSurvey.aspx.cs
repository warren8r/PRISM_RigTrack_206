using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using Telerik.Web.UI;
public partial class Modules_RigTrack_AddSurvey : System.Web.UI.Page
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
    protected void btnSave_Click(object sender, EventArgs e)
    {
        Response.Redirect("CreateCurveGroup.aspx");
    }
    protected void btnClear_Click(object sender, EventArgs e)
    {
        Response.Redirect("CreateCurveGroup.aspx");
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("CreateCurveGroup.aspx");
    }
    #endregion

}