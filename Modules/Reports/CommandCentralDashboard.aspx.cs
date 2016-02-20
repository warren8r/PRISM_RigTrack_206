using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using Telerik.Web.UI;
public partial class Modules_Reports_CommandCentralDashboard : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            grid_rig.DataSource = SqlHelper.ExecuteDataset(GlobalConnetionString.ClientConnection(), CommandType.Text,
                " select distinct mj.jid,mj.jobname,(um.firstName+' '+um.lastName) as Projectmanager, programManagerId ,mj.startdate ,mj.enddate ,RT.rigtypename  as Rigname   from" +
                " Users um,manageJobOrders mj,RigTypes RT where um.userId=mj.programManagerId and mj.bitActive='True'  and mj.rigtypeid=RT.rigtypeid ").Tables[0];
            grid_rig.DataBind();
        }
    }
    protected void grid_rig_ItemCommand(object obj, GridCommandEventArgs e)
    {
        if (e.CommandName == Telerik.Web.UI.RadGrid.ExportToExcelCommandName ||
                   e.CommandName == Telerik.Web.UI.RadGrid.ExportToWordCommandName ||
                   e.CommandName == Telerik.Web.UI.RadGrid.ExportToCsvCommandName ||
                   e.CommandName == Telerik.Web.UI.RadGrid.ExportToPdfCommandName)
        {
            grid_rig.ExportSettings.ExportOnlyData = true;
            grid_rig.ExportSettings.OpenInNewWindow = true;
            grid_rig.ExportSettings.IgnorePaging = true;
            grid_rig.ExportSettings.FileName = "Export";
        }
    }
}