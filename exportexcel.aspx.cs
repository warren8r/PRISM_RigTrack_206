using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using eis = Telerik.Web.UI.ExportInfrastructure;
using System.Data;

public partial class exportexcel : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        DataTable dt = new DataTable();

        dt.Columns.Add("column1");
        dt.Columns.Add("column2");
        dt.Columns.Add("column3");
        dt.Columns.Add("column4");
        dt.Columns.Add("column5");
        dt.Columns.Add("column6");
        dt.Columns.Add("column7");
        dt.Columns.Add("column8");
        dt.Columns.Add("column9");
        dt.Columns.Add("column10");
        dt.Columns.Add("column11");
        dt.Columns.Add("column12");
        dt.Columns.Add("column13");
        dt.Columns.Add("column14");
        dt.Columns.Add("column15");
        dt.Columns.Add("column16");
        dt.Columns.Add("column17");
        dt.Columns.Add("column18");
        dt.Columns.Add("column19");
        dt.Columns.Add("column20");
        dt.Columns.Add("column21");
        dt.Columns.Add("column22");
        dt.Columns.Add("column23"); 
        dt.Columns.Add("column24");
        dt.Columns.Add("column25");
        dt.Columns.Add("column26");
        dt.Columns.Add("column27");
        dt.Columns.Add("column28");
        dt.Columns.Add("column29");
        dt.Columns.Add("column30");
        dt.Columns.Add("column31");
        dt.Columns.Add("column32");

        dt.Columns.Add();
        for(int i=0; i < 350; i++)
        {
            DataRow dr = dt.NewRow();
            dr["column1"] = "TempData";
            dr["column2"] = "TempData";
            dr["column3"] = "TempData";
            dr["column4"] = "TempData";
            dr["column5"] = "TempData";
            dr["column6"] = "TempData";
            dr["column7"] = "TempData";
            dr["column8"] = "TempData";
            dr["column9"] = "TempData";
            dr["column10"] = "TempData";
            dr["column11"] = "TempData";
            dr["column12"] = "TempData";
            dr["column13"] = true;
            dr["column14"] = true;
            dr["column15"] = "TempData";
            dr["column16"] = "TempData";
            dr["column17"] = "TempData";
            dr["column18"] = true;
            dr["column19"] = "TempData";
            dr["column20"] = "TempData";
            dr["column21"] = "TempData";
            dr["column22"] = "TempData";
            dr["column23"] = "TempData";
            dr["column24"] = "TempData";
            dr["column25"] = "TempData";
            dr["column26"] = "TempData";
            dr["column27"] = "TempData";
            dr["column28"] = "TempData";
            dr["column29"] = "TempData";
            dr["column30"] = "TempData";
            dr["column31"] = "TempData";
            dr["column32"] = "TempData";
            dt.Rows.Add(dr);
        }
        RadGrid1.DataSource = dt;
        RadGrid1.DataBind();
    }
    protected void RadGrid1_ItemCommand(object obj, GridCommandEventArgs e)
    {
        if (e.CommandName == Telerik.Web.UI.RadGrid.ExportToExcelCommandName ||
            e.CommandName == Telerik.Web.UI.RadGrid.ExportToWordCommandName ||
            e.CommandName == Telerik.Web.UI.RadGrid.ExportToPdfCommandName ||
            e.CommandName == Telerik.Web.UI.RadGrid.ExportToCsvCommandName)
        {
            RadGrid1.ExportSettings.IgnorePaging = true;
            RadGrid1.ExportSettings.OpenInNewWindow = true;
            if (e.CommandName == Telerik.Web.UI.RadGrid.ExportToExcelCommandName)
                RadGrid1.MasterTableView.ExportToExcel();
            else if (e.CommandName == Telerik.Web.UI.RadGrid.ExportToWordCommandName)
                RadGrid1.MasterTableView.ExportToWord();
            else if (e.CommandName == Telerik.Web.UI.RadGrid.ExportToCsvCommandName)
                RadGrid1.MasterTableView.ExportToCSV();
            else if (e.CommandName == Telerik.Web.UI.RadGrid.ExportToPdfCommandName)
                RadGrid1.MasterTableView.ExportToPdf();
        }

    }
    protected void RadGrid1_PageIndexChanged(object sender, GridPageChangedEventArgs e)
    {
    }
}