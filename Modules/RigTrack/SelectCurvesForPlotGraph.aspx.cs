using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

public partial class Modules_RigTrack_SelectCurvesForPlotGraph : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            string curveGroupID = "";
            if (Request.QueryString["CurveGroupID"] != null)
            {
                curveGroupID = Request.QueryString["CurveGroupID"].ToString();
                DataTable dt = new DataTable();
                dt = RigTrack.DatabaseObjects.RigTrackDO.GetCurvesForPlot(Int32.Parse(curveGroupID));
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    RadListBoxItem newItem = new RadListBoxItem(dt.Rows[i]["Name"].ToString(), dt.Rows[i]["ID"].ToString());

                    ListBox1.Items.Add(newItem);
                }
            }
        }
    }
}