using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;
using System.Reflection;
using System.Diagnostics;
using Telerik.Web.UI;
public partial class Modules_Configuration_Manager_CreateShippingDoc : System.Web.UI.Page
{
    public static DataTable dt_Job, dt_Users, dt_RigTypes, dt_Operators, dt_JobAssignedKitAsset, dt_PrismAssetName, dt_Prism_Assets;
    string styletext = "style='font-weight:bold;color:#597791;' ", stylevalue = "";
    
    protected void Page_Load(object sender, EventArgs e)
    {
        //btn_export.Visible = false;
        btn_genarate.Visible = false;
        RadGrid_kits.Visible = false;
        //if (!IsPostBack)
        //{
            dt_Job = SqlHelper.ExecuteDataset(GlobalConnetionString.ClientConnection(), CommandType.Text, "Select * from manageJobOrders").Tables[0];
            dt_Users = SqlHelper.ExecuteDataset(GlobalConnetionString.ClientConnection(), CommandType.Text, "Select * from Users").Tables[0];
            dt_RigTypes = SqlHelper.ExecuteDataset(GlobalConnetionString.ClientConnection(), CommandType.Text, "select * from RigTypes").Tables[0];
            dt_Operators = SqlHelper.ExecuteDataset(GlobalConnetionString.ClientConnection(), CommandType.Text, "select *  from PrismJobAssignedPersonals").Tables[0];
            dt_JobAssignedKitAsset = SqlHelper.ExecuteDataset(GlobalConnetionString.ClientConnection(), CommandType.Text, "select * from PrismJobAssignedKitAssetDetails").Tables[0];
            dt_PrismAssetName = SqlHelper.ExecuteDataset(GlobalConnetionString.ClientConnection(), CommandType.Text, "select * from PrismAssetName").Tables[0];
            dt_Prism_Assets = SqlHelper.ExecuteDataset(GlobalConnetionString.ClientConnection(), CommandType.Text, "select * from Prism_Assets").Tables[0];
        //}
    }
    public string getMWDOperators(string jobid)
    {
      string strOperators="",strOperators1="";
        DataRow[] row_operators=dt_Operators.Select("Jobid="+jobid);
        for(int op=0;op<row_operators.Length;op++)
        {
            DataRow[] row_opinfo = dt_Users.Select("userID=" + row_operators[op]["Userid"].ToString());
            strOperators1 += row_opinfo[0]["firstName"].ToString() + " " + row_opinfo[0]["lastName"].ToString() + ",";
        }
        if(strOperators1!="")
        {
            strOperators=strOperators1.Remove(strOperators1.Length-1,1);
        }
        return strOperators;
    }

    protected void btn_Reset_Click(object sender, EventArgs e)
    {
        combo_job.SelectedIndex = -1;
        combo_kits.SelectedIndex = -1;
        combo_assets.SelectedIndex = -1;

    }
    protected void btn_genarate_Click(object sender, EventArgs e)
    {
        Microsoft.Office.Interop.Excel.Application excelApp = new Microsoft.Office.Interop.Excel.ApplicationClass();
        excelApp.ScreenUpdating = false;
        excelApp.DisplayAlerts = false;
        string unique = DateTime.Now.ToString("MMddyyyyHHMMss");
        object misValue = System.Reflection.Missing.Value;
        if (File.Exists(Server.MapPath("ShippindDocSheet.xlsx")))
        {

            File.Delete(Server.MapPath("ShippindDocSheet.xlsx"));
        }
        File.Copy(Server.MapPath("ShippindDcoSheet_copy.xlsx"), Server.MapPath("ShippindDocSheet.xlsx"));
        //Opening Excel file(myData.xlsx)
        Microsoft.Office.Interop.Excel.Workbook workbook = excelApp.Workbooks.Open(Server.MapPath("ShippindDocSheet.xlsx"), Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value);

        Microsoft.Office.Interop.Excel.Sheets excelSheets = workbook.Worksheets;
        string currentSheet = "Sheet1";
        Microsoft.Office.Interop.Excel.Worksheet excelWorksheet = (Microsoft.Office.Interop.Excel.Worksheet)excelSheets.get_Item(currentSheet);
        Microsoft.Office.Interop.Excel.Range range = (Microsoft.Office.Interop.Excel.Range)excelWorksheet.UsedRange;
        string strOperators = "", strOperators1 = "";
        int operatcol = 6;
        DataRow[] row_operators = dt_Operators.Select("Jobid=" + combo_job.SelectedValue);
        for (int op = 0; op < row_operators.Length; op++)
        {
            DataRow[] row_opinfo = dt_Users.Select("userID=" + row_operators[op]["Userid"].ToString());
            strOperators1 += row_opinfo[0]["firstName"].ToString() + " " + row_opinfo[0]["lastName"].ToString() + ",";
            //excelWorksheet.Cells[ operatcol+op,11] = row_opinfo[0]["firstName"].ToString() + " " + row_opinfo[0]["lastName"].ToString();
        }
        if (strOperators1 != "")
        {
            strOperators = strOperators1.Remove(strOperators1.Length - 1, 1);
        }
        string Top1 = "", Top2 = "", seleteditem = "", seleteditemText = "";
        DataRow[] row_job = dt_Job.Select("jid=" + combo_job.SelectedValue);

        DataRow[] row_opManagerId = dt_Users.Select("userID=" + row_job[0]["opManagerId"].ToString());
        DataRow[] row_rig = null, row_kitasset = null;
        if (row_job[0]["rigtypeid"].ToString() != null)
        {
            row_rig = dt_RigTypes.Select("rigtypeid=" + row_job[0]["rigtypeid"].ToString());
        }
        if (combo_assets.SelectedIndex > -1)
        {
            seleteditem = "Asset Name";
            seleteditemText = combo_assets.SelectedItem.Text;
            row_kitasset = null;
        }
        else if (combo_kits.SelectedIndex > -1)
        {
            seleteditem = "Kit Name";
            seleteditemText = combo_kits.SelectedItem.Text;
            row_kitasset = dt_JobAssignedKitAsset.Select("jobid=" + combo_job.SelectedValue + " and kitid=" + combo_kits.SelectedValue);

        }
        //Job Id
        excelWorksheet.Cells[3, 2] = row_job[0]["jobid"].ToString();
        //KIt/AssetName
        excelWorksheet.Cells[3, 4] = seleteditemText;
        //Location
        excelWorksheet.Cells[4, 2] = row_job[0]["primaryAddress1"].ToString();
        //Operator
        excelWorksheet.Cells[4, 4] = row_opManagerId[0]["firstName"].ToString() + " " + row_opManagerId[0]["lastName"].ToString();
        //RIG
        excelWorksheet.Cells[5, 2] = row_rig[0]["rigtypename"].ToString();
        //JOB AssignedDate
        excelWorksheet.Cells[5, 4] = row_job[0]["JobAssignedDate"].ToString();
        //MDM OPERATORS
        excelWorksheet.Cells[6, 2] = strOperators;
        

        //excelWorksheet.Cells[8, 3] = row_job[0]["jobid"].ToString();

        //excelWorksheet.Cells[5, 8] = row_job[0]["primaryAddress1"].ToString();
        //
        //for (int c = 0; c < RadGrid_kits.Items.Count; c++)
        //{

        //}
        //foreach (GridDataItem item in RadGrid_kits.Items)
        //{

        //}
        if (combo_kits.SelectedIndex > -1)
        {
            string querycat = "select pan.kitname as Category,a.AssetName,CASE WHEN pd.assigned ='Assign Later' THEN 'Not Assigned' ELSE Convert(varchar,pa.AssetId) END as AssetSNo from PrismJobAssignedKitAssetDetails pd,Prism_Assets pa," +
                    " PrismAssetKitDetails pan,PrismAssetName a where pd.assetid=pa.Id and pd.kitid=pan.assetkitid and pd.assetnameid=a.Id and pd.jobid=" + combo_job.SelectedValue + " and pd.kitid=" + combo_kits.SelectedValue + "";
            DataTable dt = SqlHelper.ExecuteDataset(GlobalConnetionString.ClientConnection(), CommandType.Text, querycat).Tables[0];
            int row = 10;
            for (int r = 0; r < dt.Rows.Count; r++)
            {
                int rownumber = row + r;
                int col1 = 2;
                int col2 = 3;
                int col3 = 4;
                excelWorksheet.Cells[rownumber, col1] = dt.Rows[r]["Category"].ToString();
                excelWorksheet.Cells[rownumber, col2] = dt.Rows[r]["AssetName"].ToString();
                excelWorksheet.Cells[rownumber, col3] = dt.Rows[r]["AssetSNo"].ToString();
            }

        }


        //int iCol = 1;
        ////foreach (DataColumn c in dt.Columns)
        ////{
        ////    iCol++;
        ////    excelWorksheet.Cells[1, iCol] = c.ColumnName;
        ////}
        ////// for each row of data...
        //int iRow = 9;
        //foreach (DataRow r in dt.Rows)
        //{
        //    iRow++;

        //    foreach (DataColumn c in dt.Columns)
        //    {
        //        iCol++;
        //        excelWorksheet.Cells[iRow + 1, iCol] = r[c.ColumnName];
        //    }
        //}
       

        workbook.Save();

        //workbook.SaveAs(Server.MapPath("RunReport.xlsx"), Excel.XlFileFormat.xlWorkbookNormal, misValue, misValue, misValue, misValue, Excel.XlSaveAsAccessMode.xlExclusive, misValue, misValue, misValue, misValue, misValue);
        workbook.Close(true, misValue, misValue);

        excelApp.Quit();
        //Process[] pros = Process.GetProcesses();
        //for (int i = 0; i < pros.Count(); i++)
        //{
        //    if (pros[i].ProcessName.ToLower().Contains("excel"))
        //    {
        //        pros[i].Kill();
        //    }
        //}

        string FilePath = Server.MapPath("ShippindDocSheet.xlsx");
        FileInfo fileInfo = new FileInfo(FilePath);
        Response.Clear();
        Response.AddHeader("Content-Disposition", "attachment;filename=" + fileInfo.Name);
        Response.AddHeader("Content-Length", fileInfo.Length.ToString());
        Response.ContentType = "application/octet-stream";
        Response.Flush();
        Response.WriteFile(fileInfo.FullName);
        Response.End();
    }
    public void ExportToExcel()
    {

    }
    protected void btn_show_Click(object sender, EventArgs e)
    {
        //string querycat = "select pan.kitname as Category,a.AssetName,CASE WHEN pd.assigned ='Assign Later' THEN 'Not Assigned' ELSE Convert(varchar,pa.AssetId) END as AssetSNo from PrismJobAssignedKitAssetDetails pd,Prism_Assets pa," +
        //        " PrismAssetKitDetails pan,PrismAssetName a where pd.assetid=pa.Id and pd.kitid=pan.assetkitid and pd.assetnameid=a.Id and pd.jobid=" + combo_job.SelectedValue + " and pd.kitid=" + combo_kits.SelectedValue + "";
        //DataTable dt = SqlHelper.ExecuteDataset(GlobalConnetionString.ClientConnection(), CommandType.Text, querycat).Tables[0];
        //int row = 10;
        //string val="";
        //for (int col = 2; col < dt.Columns.Count + 2; col++)
        //{
        //    for (int r = 10; r < dt.Rows.Count + 10; r++)
        //    {
        //        val += dt.Rows[r].ToString() + ","; 
        //    }
        //}
        //string aaa = val;
        //string str_q = "select m.jobid,kn.kitname,u.firstName+' '+u.lastName as OperatorName,m.primaryAddress1 as location"+
        //    " from manageJobOrders m,PrismJobKits k,PrismAssetKitDetails kn,Users u where m.jid=k.jobid and k.kitid=kn.assetkitid and m.opManagerId=u.userID and m.jid="+combo_job.SelectedValue+"";
        //DataTable dt_det = SqlHelper.ExecuteDataset(GlobalConnetionString.ClientConnection(), CommandType.Text, str_q).Tables[0];
        //RadGrid_kits.DataSource = dt_det;
        //RadGrid_kits.DataBind();
        //HIDE PART
        //btn_export.Visible = true;
        btn_genarate.Visible = true;
        //Microsoft.Office.Interop.Excel.Application excelApp = new Microsoft.Office.Interop.Excel.ApplicationClass();
        //excelApp.ScreenUpdating = false;
        //excelApp.DisplayAlerts = false;

        //object misValue = System.Reflection.Missing.Value;
        //if (File.Exists(Server.MapPath("ShippindDcoSheet_copy.xlsx")))
        //{
            
        //    File.Delete(Server.MapPath("ShippindDcoSheet_copy.xlsx"));
        //}
        //File.Copy(Server.MapPath("ShippindDcoSheet.xlsx"), Server.MapPath("ShippindDcoSheet_copy.xlsx"));
        ////Opening Excel file(myData.xlsx)
        //Microsoft.Office.Interop.Excel.Workbook workbook = excelApp.Workbooks.Open(Server.MapPath("ShippindDcoSheet_copy.xlsx"), Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value);

        //Microsoft.Office.Interop.Excel.Sheets excelSheets = workbook.Worksheets;
        //string currentSheet = "Sheet1";
        //Microsoft.Office.Interop.Excel.Worksheet excelWorksheet = (Microsoft.Office.Interop.Excel.Worksheet)excelSheets.get_Item(currentSheet);
        //Microsoft.Office.Interop.Excel.Range range = (Microsoft.Office.Interop.Excel.Range)excelWorksheet.UsedRange;
        //excelWorksheet.Cells[5,1] = seleteditem;
        //excelWorksheet.Cells[5,3] = seleteditemText;
        //excelWorksheet.Cells[6,3] = row_opManagerId[0]["firstName"].ToString() + " " + row_opManagerId[0]["lastName"].ToString();
        //excelWorksheet.Cells[7,3] = row_rig[0]["rigtypename"].ToString();
        //excelWorksheet.Cells[8,3] = row_job[0]["jobid"].ToString();

        //excelWorksheet.Cells[5,8] = row_job[0]["primaryAddress1"].ToString();
        //excelWorksheet.Cells[6,8] = row_job[0]["JobAssignedDate"].ToString();
        
        string strOperators = "", strOperators1 = "";
        int operatcol = 6;
        DataRow[] row_operators = dt_Operators.Select("Jobid=" + combo_job.SelectedValue);
        for (int op = 0; op < row_operators.Length; op++)
        {
            DataRow[] row_opinfo = dt_Users.Select("userID=" + row_operators[op]["Userid"].ToString());
            strOperators1 += row_opinfo[0]["firstName"].ToString() + " " + row_opinfo[0]["lastName"].ToString() + ",";
            //excelWorksheet.Cells[ operatcol+op,11] = row_opinfo[0]["firstName"].ToString() + " " + row_opinfo[0]["lastName"].ToString();
        }
        if (strOperators1 != "")
        {
            strOperators = strOperators1.Remove(strOperators1.Length - 1, 1);
        }
        
        string Top1 = "",Top2="",seleteditem="",seleteditemText="";
        DataRow[] row_job = dt_Job.Select("jid=" + combo_job.SelectedValue);
        
        DataRow[] row_opManagerId = dt_Users.Select("userID=" + row_job[0]["opManagerId"].ToString());
        DataRow[] row_rig = null,row_kitasset=null;
        if (row_job[0]["rigtypeid"].ToString() != null)
        {
             row_rig = dt_RigTypes.Select("rigtypeid=" + row_job[0]["rigtypeid"].ToString());
        }
        if(combo_assets.SelectedIndex>-1)
        {
            seleteditem="Asset Name";
            seleteditemText=combo_assets.SelectedItem.Text;
            row_kitasset = null;
        }
        else if(combo_kits.SelectedIndex>-1)
        {
            RadGrid_kits.Visible = true;
            seleteditem="Kit Name";
            seleteditemText=combo_kits.SelectedItem.Text;
           row_kitasset= dt_JobAssignedKitAsset.Select("jobid="+combo_job.SelectedValue+" and kitid="+combo_kits.SelectedValue);
           
        }

        //RadGrid_kits.DataSource = row_kitasset.CopyToDataTable();
        //RadGrid_kits.DataBind();
        Top1 = "<table border='1' cellpadding='2' cellspacing='3' width='100%'>";
        Top1 += "<tr><td " + styletext + " align='right'>Job #:</td><td align='left' >" + row_job[0]["jobid"].ToString() + "</td><td " + styletext + " align='right'>" + seleteditem + " :</td><td align='left'>" + seleteditemText + "</td></tr><tr><td " + styletext + " align='right'>Location:</td><td align='left'>" + row_job[0]["primaryAddress1"].ToString() + "</td>";
        Top1 += "<td " + styletext + " align='right'>Operator:</td><td align='left'>" + row_opManagerId[0]["firstName"].ToString() + " " + row_opManagerId[0]["lastName"].ToString() + "</td></tr>";
        Top1 += "<tr><td " + styletext + " align='right'>Rig :</td><td align='left'>" + row_rig[0]["rigtypename"].ToString() + "</td><td " + styletext + " align='right'>Date:</td><td align='left'>" + Convert.ToDateTime(row_job[0]["JobAssignedDate"].ToString()).ToShortDateString() + "</td></tr>";
        Top1 += "<tr><td " + styletext + " align='right'>MWD Operators:</td><td colspan='3'>" + strOperators + "</td></tr></table>";
        
        panel_Top1.Controls.Add(new LiteralControl(Top1));

        if (row_kitasset != null)
        {
            string querycat1 = "select pan.kitname as Category,a.AssetName,CASE WHEN pd.assigned ='Assign Later' THEN 'Not Assigned' ELSE Convert(varchar,pa.AssetId) END as AssetSNo from PrismJobAssignedKitAssetDetails pd,Prism_Assets pa," +
                " PrismAssetKitDetails pan,PrismAssetName a where pd.assetid=pa.Id and pd.kitid=pan.assetkitid and pd.assetnameid=a.Id and pd.jobid="+combo_job.SelectedValue+" and pd.kitid="+combo_kits.SelectedValue+"";
            DataTable dt_det = SqlHelper.ExecuteDataset(GlobalConnetionString.ClientConnection(), CommandType.Text, querycat1).Tables[0];
            RadGrid_kits.DataSource = dt_det;
            RadGrid_kits.DataBind();
            //panel_Top1.Controls.Add(new LiteralControl(Top1));

            //Top2 = "<table border='1' cellpadding='0' cellspacing='0' width='100%'><tr><td " + styletext + " align='center'>Category:</td>" +
            //"<td " + styletext + "  align='center'>Asset&#160;Name:</td><td " + styletext + "  align='center'>Asset&#160;S.No</td></tr>";

            //for (int cat = 0; cat < row_kitasset.Length; cat++)
            //{
            //    DataRow[] rowasset = null, rowassetsno = null;
            //    rowasset = dt_PrismAssetName.Select("Id=" + row_kitasset[cat]["assetnameid"].ToString());
            //    if (row_kitasset[cat]["assetid"].ToString() != null && row_kitasset[cat]["assetid"].ToString() != "")
            //    {
            //        rowassetsno = dt_Prism_Assets.Select("Id=" + row_kitasset[cat]["assetid"].ToString());
            //    }
            //    Top2 += "<tr><td >" + seleteditemText + "</td><td >" + rowasset[0]["AssetName"].ToString() + "</td>";
            //    if (rowassetsno != null)
            //    {
            //        Top2 += "<td >" + rowassetsno[0]["AssetId"].ToString() + "</td>";
            //    }
            //    else
            //    {
            //        Top2 += "<td >Not&#160;Assigned</td>";
            //    }
            //    Top2 += "</tr>";
            //}

            //Top2 += "</table>";
            //panel_Top2.Controls.Add(new LiteralControl(Top2));
        }
        //workbook.Save();

        ////workbook.SaveAs(Server.MapPath("RunReport.xlsx"), Excel.XlFileFormat.xlWorkbookNormal, misValue, misValue, misValue, misValue, Excel.XlSaveAsAccessMode.xlExclusive, misValue, misValue, misValue, misValue, misValue);
        //workbook.Close(true, misValue, misValue);
        //excelApp.Quit();
       

    }
    protected void combo_job_SelectedIndexChanged(object sender, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs e)
    {
        SqlGetAssets.SelectCommand = "select  pn.AssetName + ' ('+pa.SerialNumber+')' as AssetName,pj.AssetId as AID from PrismJobAssignedAssets pj," +
            " Prism_Assets pa,PrismAssetName pn where pj.AssetId=pa.Id and pa.AssetName=pn.Id and pj.KitName is null and pj.JobId=" + combo_job.SelectedValue + "";
        SqlGetKits.SelectCommand = "select  PK.assetkitid as Kitid,PK.kitname  from PrismJobKits JK,PrismAssetKitDetails PK  where "+
            " JK.kitid=PK.assetkitid and JK.jobid=" + combo_job.SelectedValue + "";
        //combo_serialnumberforasset.Items.Insert(0, new RadComboBoxItem("Select", "Select"));

        //combo_assets

        RadGrid_kits.Visible = false;
    }
}