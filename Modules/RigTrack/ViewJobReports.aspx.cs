using System;
using System.Configuration;
using System.Data;
using System.IO;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

public partial class Modules_RigTrack_ViewJobReports : System.Web.UI.Page
{
   
    protected void Page_Load(object sender, EventArgs e)
    {
        
        ScriptManager manager = ScriptManager.GetCurrent(this.Page);
        manager.RegisterPostBackControl(this.btnExportToASCII);
        if (!IsPostBack)
        {
            int CurveGroupID = 0;
            try
            {
                CurveGroupID = Convert.ToInt32(Request.QueryString["CurveGroupID"]);
                DataTable reportDT = RigTrack.DatabaseObjects.RigTrackDO.GetAllJobReports(CurveGroupID);

              

                Label lblJobNumber = (Label)(Page.Master.FindControl("lblJobNumber"));
                Label lblStateCountry = (Label)(Page.Master.FindControl("lblStateCountry"));
                Label lblCompany = (Label)(Page.Master.FindControl("lblCompany"));
                
                Label lblDeclination = (Label)(Page.Master.FindControl("lblDeclination"));
                Label lblLeaseWell = (Label)(Page.Master.FindControl("lblLeaseWell"));
                Label lblGrid = (Label)(Page.Master.FindControl("lblGrid"));
                Label lblLocation = (Label)(Page.Master.FindControl("lblLocation"));
                Label lblJobName = (Label)(Page.Master.FindControl("lblJobName"));
                Label lblRigName = (Label)(Page.Master.FindControl("lblRigName"));
                Label lblCurveName = (Label)(Page.Master.FindControl("lblCurveName"));
                Label lblRKB = (Label)(Page.Master.FindControl("lblRKB"));
                Label lblDateTime = (Label)(Page.Master.FindControl("lblDateTime"));
                Label lblGLorMSL = (Label)(Page.Master.FindControl("lblGLorMSL"));

                lblJobNumber.Text += " " + reportDT.Rows[0]["JobNumber"].ToString();
                lblStateCountry.Text += " " + reportDT.Rows[0]["StateCountry"].ToString();
                lblCompany.Text += " " + reportDT.Rows[0]["CompanyName"].ToString();
                lblDeclination.Text += " " + reportDT.Rows[0]["Declination"].ToString();
                lblLeaseWell.Text += " " + reportDT.Rows[0]["LeaseWell"].ToString();
                lblGrid.Text += " " + reportDT.Rows[0]["Grid"].ToString();
                lblLocation.Text += " " + reportDT.Rows[0]["JobLocation"].ToString();
                lblJobName.Text += " " + reportDT.Rows[0]["CurveGroupName"].ToString();

                
                lblRigName.Text += " " + reportDT.Rows[0]["RigName"].ToString();
                lblCurveName.Text += " " + reportDT.Rows[0]["CurveName"].ToString();
                lblRKB.Text += " " + reportDT.Rows[0]["RKB"].ToString();
                lblDateTime.Text += " " + reportDT.Rows[0]["CurrentDateTime"].ToString();
                lblGLorMSL.Text += " " + reportDT.Rows[0]["GLorMSL"].ToString();

                //Used to build ascii
                
                
                ViewState["CurveGroupName"] = reportDT.Rows[0]["CurveGroupName"].ToString();
                ViewState["Company"] = reportDT.Rows[0]["CompanyName"].ToString();
                ViewState["JobNumber"] = reportDT.Rows[0]["JobNumber"].ToString();
                ViewState["LeaseWell"] = reportDT.Rows[0]["LeaseWell"].ToString();
                ViewState["RigName"] = reportDT.Rows[0]["RigName"].ToString();
                ViewState["JobLocation"] = reportDT.Rows[0]["JobLocation"].ToString();

                string stateCountry = reportDT.Rows[0]["StateCountry"].ToString();
                string[] stateCountrySplit;
                stateCountrySplit = stateCountry.Split('/');
                ViewState["State"] = stateCountrySplit[0];
                ViewState["Country"] = stateCountrySplit[1];
                ViewState["MeasurementUnit"] = reportDT.Rows[0]["MeasurementUnit"].ToString();

            }
            catch (Exception ex)
            {
                int i = 0;
            }
            bindToolJobDetails(CurveGroupID);
            //Get Curves For curve group
            DataTable dt1 = RigTrack.DatabaseObjects.RigTrackDO.GetCurvesForCurveGroupActive(CurveGroupID);
            ddlCurve.DataSource = dt1;
            ddlCurve.DataTextField = "Name";
            ddlCurve.DataValueField = "ID";
            ddlCurve.SelectedIndex = 1;
            ddlCurve.DataBind();
            
            //Bind Dropdown
            //Set drop down to first curve
            //Get selected curve info

            DataTable dt = RigTrack.DatabaseObjects.RigTrackDO.GetSurveysForJobReport(Int32.Parse(ddlCurve.SelectedValue));
            DataTable dt2 = RigTrack.DatabaseObjects.RigTrackDO.GetCurveInfo(Int32.Parse(ddlCurve.SelectedValue));

            //Curve ID 
            ViewState["CurveID"] = dt2.Rows[0]["ID"].ToString();
            ViewState["CurveName"] = dt2.Rows[0]["Name"].ToString();
            ViewState["CurveType"] = dt2.Rows[0]["CurveType"].ToString();
            ViewState["CurveNumber"] = dt2.Rows[0]["Number"].ToString();
            //Curve Name
            //CurveType 
            //Curve Number

            //DataTable dt = RigTrack.DatabaseObjects.RigTrackDO.GetAllJobReports(CurveGroupID);
            RadGrid1.DataSource = dt;
            RadGrid1.DataBind();

            ViewState["StartDepth"] = dt.Rows[0]["MD"].ToString();
            ViewState["StopDepth"] = dt.Rows[dt.Rows.Count - 1]["MD"].ToString();

            //updPnl1.Update();
        }
    }
    public void bindToolJobDetails(int CurveGroupID)
    {
        grdJobList.DataSource = jobOrders("", "", CurveGroupID);
        grdJobList.DataBind();
        bindBHADetails(CurveGroupID);
        DataTable dt_bhaItemsInfo = SqlHelper.ExecuteDataset(GlobalConnetionString.ClientConnection(), CommandType.Text, "select p.Id,AssetId,PPA.AssetName as AssetName,P.Assetname as asset_id,c.Name as warehouse,ca.clientAssetName as assetcategory,SerialNumber," +
             "Status,p.ODFrac,p.IDFrac,p.Length,p.TopConnection,p.BottomConnection,p.PinTop,p.PinBottom,p.Comments,p.Description from Prism_Assets p,PrsimWarehouses c,clientAssets ca,PrismAssetName PPA" +
              " ,[RigTrack].[tblBHADataInfo] BI,[RigTrack].[tblBHADataItemsInfo] BHAI where  p.WarehouseId=c.ID and p.AssetCategoryId=ca.clientAssetID "+
              " and P.AssetName=PPA.Id and BHAI.BHAID=BI.ID and p.Id=BHAI.ToolID and BI.JOBID=" + CurveGroupID + "").Tables[0];
        radgrdMeterList.DataSource = dt_bhaItemsInfo;
        radgrdMeterList.DataBind();
    }
    public void bindBHADetails(int JOBID)
    {
        string query = "select J.CurveGroupName+' '+Convert(varchar,J.ID) as JobName,[BHANumber],[BHADesc],[BHAType],[BitSno],[BitDesc],[ODFrac]" +
",[BitLength],[Connection],[BitType],[BearingType],[BitMfg],[BitNumber],[NUMJETS],[InnerRow],[OuterRow],[DullChar],[Location],[BearingSeals],[Guage]" +
",[OtherDullChar],[ReasonPulled],[MotorDesc],[MotorMFG],[NBStabilizer],[Model],[Revolutions],[Bend],[RotorJet]" +
",[BittoBend],[PropBUR],[RealBUR],[PadOD],[AverageDifferential],[Lobes],[OffBottomDifference],[Stages],[StallPressure]" +
",[BittoSensor],[BittoGamma],[BittoResistivity],[BittoPorosity],[BittoDNSC],[BittoGyro],B.[CreateDate] as BHACreatedDate" +
" from [RigTrack].[tblBHADataInfo]B,[RigTrack].[tblCurveGroup] J where B.JOBID=J.ID";
        if (JOBID != 0)
            query += " and  j.ID=" + JOBID + "";



        DataTable dt_users = SqlHelper.ExecuteDataset(GlobalConnetionString.ClientConnection(), CommandType.Text, query).Tables[0];
        if (dt_users.Rows.Count > 0)
        {
            //DataTable dt = RigTrack.DatabaseObjects.RigTrackDO.GetCurvesForTarget(Convert.ToInt32(ViewState["TargetID"].ToString()));
            RadGridBHAInfo.DataSource = dt_users;
            RadGridBHAInfo.DataBind();
        }
        else
        {
            DataTable dt = new DataTable();
            RadGridBHAInfo.DataSource = dt;
        }
    }
    protected void RadGrid_kits_ItemDataBound(object sender, GridItemEventArgs e)
    {
        // Is the bound item and GridDataItem?
        if (e.Item is GridDataItem)
        {
            GridDataItem item = (GridDataItem)e.Item;
            Label lbl_kitid = (Label)item.FindControl("lbl_kitid");
            Label lbl_jobid = (Label)item.FindControl("lbl_jobid");
            Label lbl_kitstatusid = (Label)item.FindControl("lbl_kitstatusid");
            Label lbl_kitstatus = (Label)item.FindControl("lbl_kitstatus");
            if (lbl_kitstatusid.Text == "2")
            {
                lbl_kitstatus.Text = "In Transit";
            }
            else if (lbl_kitstatusid.Text == "3")
            {
                lbl_kitstatus.Text = "Delivered";
            }
            else
            {
                lbl_kitstatus.Text = "Pending Shipmet";
            }
            RadGrid radgridkitassets = (RadGrid)item.FindControl("radgridkitassets");
            string query_getassets = "select a.AssetName from PrismKitAssetFromKitName k,PrismAssetName a where a.ID=k.assetids and k.assetkitid=" + lbl_kitid.Text + " ";
            DataTable dt_getassetids = SqlHelper.ExecuteDataset(GlobalConnetionString.ClientConnection(), CommandType.Text, query_getassets).Tables[0];
            radgridkitassets.DataSource = dt_getassetids;
            radgridkitassets.DataBind();
            //string assets = "";
            //for (int i = 0; i < dt_getassetids.Rows.Count; i++)
            //{

            //}


        }
    }
    protected void gridJobAssets_ItemDataBound(object sender, GridItemEventArgs e)
    {
        // Is the bound item and GridDataItem?
        if (e.Item is GridDataItem)
        {
            // Yes, get the item
            GridDataItem item = (GridDataItem)e.Item;
            Label lbl_KitName = (Label)item.FindControl("lbl_KitName");
            if (lbl_KitName.Text == "")
            {
                lbl_KitName.Text = "--NA--";
            }

        }
    }
    protected void grdJobList_PageIndexChanged(object source, GridPageChangedEventArgs e)
    {

        grdJobList.CurrentPageIndex = e.NewPageIndex;
        grdJobList.DataSource = jobOrders("", "", 0);
        grdJobList.DataBind();
    }
    protected void grdJobList_ItemCommand(object source, GridCommandEventArgs e)
    {

        if (e.CommandName == RadGrid.ExpandCollapseCommandName)
        {

            GridDataItem item = e.Item as GridDataItem;

            if (!item.Expanded)
            {

                GridNestedViewItem nestedItem = (GridNestedViewItem)item.ChildItem;

                string dataKeyValue = Convert.ToString(((GridDataItem)(nestedItem.ParentItem)).GetDataKeyValue("ID"));

                //RadGrid gridJobPersonals = (RadGrid)nestedItem.FindControl("gridJobPersonals");
                RadGrid gridJobAssets = (RadGrid)nestedItem.FindControl("gridJobAssets");
                //RadGrid gridServices = (RadGrid)nestedItem.FindControl("gridServices");
                //RadGrid RadGrid_con = (RadGrid)nestedItem.FindControl("RadGrid_con");
                RadGrid RadGrid_kits = (RadGrid)nestedItem.FindControl("RadGrid_kits");

                //    gridJobPersonals.DataSource = SqlHelper.ExecuteDataset(GlobalConnetionString.ClientConnection(), CommandType.Text,
                //"select (u.firstName+' '+u.lastName) as username,r.userRole from PrismJobAssignedPersonals PA,Users u,UserRoles r where u.userRoleID=r.userRoleID and PA.UserId=u.UserId  and PA.JobId=" + dataKeyValue + "").Tables[0];

                //    gridJobPersonals.DataBind();
                string selectq = "select MJ.ID,A.AssetId,PA.AssetName,JA.KitName,SerialNumber,clientAssetName,WA.Name as Warehouse,(u.firstName+' '+u.lastName) as username,JA.ModifiedDate,JAS.StatusText as AssetStatus   " +
            " from Prism_Assets A,PrismJobAssignedAssets JA,PrsimWarehouses WA,clientAssets AT,Users u,[RigTrack].tblCurveGroup MJ,PrsimJobAssetStatus JAS,PrismAssetName PA where AT.clientAssetID=A.AssetCategoryId and" +
            " WA.ID=A.WarehouseId  and MJ.ID=JA.JobId and JA.AssetId=A.Id  and JAS.Id=JA.AssetStatus and" +
            " JA.ModifiedBy=u.userID and A.AssetName=PA.ID And JA.JobId=" + dataKeyValue + " and JA.kitname is null and JA.AssignmentStatus='Active'";
                gridJobAssets.DataSource = SqlHelper.ExecuteDataset(GlobalConnetionString.ClientConnection(), CommandType.Text, selectq).Tables[0];
                gridJobAssets.DataBind();

                //    gridServices.DataSource = SqlHelper.ExecuteDataset(GlobalConnetionString.ClientConnection(), CommandType.Text,
                //"select * from PrismJobServiceAssignment PS,PrismService S where PS.ServiceID=S.ID  and PS.JobId=" + dataKeyValue + "").Tables[0];

                //    gridServices.DataBind();

                //    RadGrid_con.DataSource = SqlHelper.ExecuteDataset(GlobalConnetionString.ClientConnection(), CommandType.Text,
                //"select * from PrismJobConsumables Pc,manageJobOrders S,Consumables c where Pc.jobid=S.jid and Pc.consumableid=c.ConID and Pc.jobid=" + dataKeyValue + "").Tables[0];

                //    RadGrid_con.DataBind();

                RadGrid_kits.DataSource = SqlHelper.ExecuteDataset(GlobalConnetionString.ClientConnection(), CommandType.Text,
            "select * from PrismJobKits Pk,PrismAssetKitDetails k  where Pk.kitid=k.assetkitid and Pk.jobid=" + dataKeyValue + "").Tables[0];

                RadGrid_kits.DataBind();

            }

        }

    }
    public DataTable jobOrders(string startdate, string stopdate, int jobid)
    {
        string query = "select  JO.ID,JO.JobNumber,jo.CurveGroupName as JOB,JO.JobStartDate,JO.JobLocation," +
            " RigName,LeaseWell from [RigTrack].tblCurveGroup JO" +
            " where ID in (select distinct JobId from PrismJobAssignedAssets where AssignMentStatus='Active')";
        if (jobid != 0)
        {
            query += " and JO.ID=" + jobid + "";
        }
        query += " order by JO.CreateDate desc";
        return SqlHelper.ExecuteDataset(GlobalConnetionString.ClientConnection(), CommandType.Text, query).Tables[0];
    }
    protected void grdJobList_SortCommand(object source, GridSortCommandEventArgs e)
    {
        //Default sort order Descending


        if (!e.Item.OwnerTableView.SortExpressions.ContainsExpression(e.SortExpression))
        {
            GridSortExpression sortExpr = new GridSortExpression();
            sortExpr.FieldName = e.SortExpression;
            sortExpr.SortOrder = GridSortOrder.Ascending;

            e.Item.OwnerTableView.SortExpressions.AddSortExpression(sortExpr);
            grdJobList.DataSource = jobOrders("", "", 0);
            grdJobList.DataBind();
        }
        else
        {
            GridSortExpression sortExpr = new GridSortExpression();
            sortExpr.FieldName = e.SortExpression;
            sortExpr.SortOrder = GridSortOrder.Descending;

            e.Item.OwnerTableView.SortExpressions.AddSortExpression(sortExpr);
            grdJobList.DataSource = jobOrders("", "", 0);
            grdJobList.DataBind();
        }
    }

    protected void RadGrid1_PageIndexChanged(object source, Telerik.Web.UI.GridPageChangedEventArgs e)
    {
        this.RadGrid1.CurrentPageIndex = e.NewPageIndex;

        DataTable dt = RigTrack.DatabaseObjects.RigTrackDO.GetSurveysForJobReport(Int32.Parse(ddlCurve.SelectedValue));

        RadGrid1.DataSource = dt;
        RadGrid1.DataBind();
    }
    protected void RadGrid1_PageSizeChanged(object sender, GridPageSizeChangedEventArgs e)
    {

        DataTable dt = RigTrack.DatabaseObjects.RigTrackDO.GetSurveysForJobReport(Int32.Parse(ddlCurve.SelectedValue));

        RadGrid1.DataSource = dt;
        RadGrid1.DataBind();
    }

    protected void RadGrid1_ItemCreated(object sender, GridItemEventArgs e)
    {
        if (e.Item is GridCommandItem)
        {
            RadScriptManager RadScriptManager1 = (RadScriptManager)(Page.Master.FindControl("RadScriptManager1"));
            Button btnExcel = (e.Item as GridCommandItem).FindControl("ExportToExcelButton") as Button;
            Button btnPDF = (e.Item as GridCommandItem).FindControl("ExportToPdfButton") as Button;
            Button btnCSV = (e.Item as GridCommandItem).FindControl("ExportToCsvButton") as Button;
            Button btnWord = (e.Item as GridCommandItem).FindControl("ExportToWordButton") as Button;
            RadScriptManager1.RegisterPostBackControl(btnExcel);
            RadScriptManager1.RegisterPostBackControl(btnPDF);
            RadScriptManager1.RegisterPostBackControl(btnCSV);
            RadScriptManager1.RegisterPostBackControl(btnWord);
        }
    }
    protected void RadGrid1_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
    {
        DataTable dt = RigTrack.DatabaseObjects.RigTrackDO.GetSurveysForJobReport(Int32.Parse(ddlCurve.SelectedValue));
        RadGrid1.DataSource = dt;
    }
    protected void RadGrid1_ItemCommand(object sender, GridCommandEventArgs e)
    {
        Label lblCompany = (Label)(Page.Master.FindControl("lblCompany"));
        Label lblJobName = (Label)(Page.Master.FindControl("lblJobName"));
        Label lblDate = (Label)(Page.Master.FindControl("lblDateTime"));

        string company = lblCompany.Text.Replace("Company: ", "");
        string jobName = lblJobName.Text.Replace("Job Name: ", "");
        string date = lblDate.Text.Replace("Date Time: ", "");
        DateTime dt = Convert.ToDateTime(date);

        if (e.CommandName == RadGrid.ExportToPdfCommandName)
        {
            RadGrid1.ExportSettings.FileName = company + "_" + jobName + "_" + dt.ToString("MM/dd/yyyy");
        }
        if (e.CommandName == RadGrid.ExportToExcelCommandName)
        {
            RadGrid1.ExportSettings.FileName = company + "_" + jobName + "_" + dt.ToString("MM/dd/yyyy");
        }
        if (e.CommandName == RadGrid.ExportToWordCommandName)
        {
            RadGrid1.ExportSettings.FileName = company + "_" + jobName + "_" + dt.ToString("MM/dd/yyyy");
        }
        if (e.CommandName == RadGrid.ExportToCsvCommandName)
        {
            RadGrid1.ExportSettings.FileName = company + "_" + jobName + "_" + dt.ToString("MM/dd/yyyy");
        }
    }
    protected void btnExportToASCII_Click(object sender, EventArgs e)
    {
        string txt = string.Empty;
        string Version = ConfigurationManager.AppSettings["VersionNumber"].ToString();

        DateTime dateNow = DateTime.Now;

        string fileName = string.Empty;

        Label lblCompany = (Label)(Page.Master.FindControl("lblCompany"));
        Label lblJobName = (Label)(Page.Master.FindControl("lblJobName"));

        string measurement = ViewState["MeasurementUnit"].ToString();
        measurement = measurement[0].ToString();


        txt += string.Format("# Exported with Rightrack Version : {0} on {1}", Version, dateNow);
        txt += "\r\n";

        txt += "~Well Information Block";
        txt += "\r\n";
        txt += "Start Depth\t:" + "\t\t" + ViewState["StartDepth"].ToString();
        txt += "\r\n";
        txt += "Stop Depth\t:" + "\t\t" + ViewState["StopDepth"].ToString();
        txt += "\r\n";
        txt += "Company \t :" + "\t\t" + ViewState["Company"].ToString();
        txt += "\r\n";
        txt += "Job Number \t :" + "\t\t" + ViewState["JobNumber"].ToString();
        txt += "\r\n";
        txt += "Well \t\t : " + "\t\t" + ViewState["LeaseWell"].ToString();
        txt += "\r\n";
        txt += "Rig Name \t :" + "\t\t" + ViewState["RigName"].ToString();
        txt += "\r\n";
        txt += "Location \t :" + "\t\t" + ViewState["JobLocation"].ToString();
        txt += "\r\n";
        txt += "State \t\t :" + "\t\t" + ViewState["State"].ToString();
        txt += "\r\n";
        txt += "Country \t :" + "\t\t" + ViewState["Country"].ToString();
        txt += "\r\n";
        txt += "Log Date \t :" + "\t\t" + dateNow.Date.ToShortDateString();
        txt += "\r\n";

        txt += "~Curve Information Block";
        txt += "\r\n";
        txt += "CurveID \t :" + "\t\t" + ddlCurve.SelectedValue;
        txt += "\r\n";
        txt += "Curve Name \t :" + "\t\t" + ViewState["CurveName"].ToString();
        txt += "\r\n";
        txt += "Curve Type \t :" + "\t\t" + ViewState["CurveType"].ToString();
        txt += "\r\n";
        txt += "Curve Number :" + "\t\t" + ViewState["CurveNumber"].ToString();
        txt += "\r\n";
        txt += "MD\t\t\t\t "+measurement+":" + "\t\t" + "1 Measurement Depth";
        txt += "\r\n";
        txt += "Inc\t\t\t\t DEG:" + "\t\t" + "2 Inclination";
        txt += "\r\n";
        txt += "Az\t\t\t\t DEG:" + "\t\t" + "3 Azimuth";
        txt += "\r\n";
        txt += "TVD\t\t\t\t "+measurement+":" + "\t\t" + "4 Total Vertical Depth";
        txt += "\r\n";
        txt += "SubseaTVD\t\t " + measurement+":" + "\t\t" + "5 Subseas Total Vertical Depth";
        txt += "\r\n";
        txt += "NS\t\t\t\t "+measurement+":" + "\t\t" + "6 North/South";
        txt += "\r\n";
        txt += "EW\t\t\t\t "+measurement+":" + "\t\t" + "7 East/West";
        txt += "\r\n";
        txt += "VS\t\t\t\t " +measurement+":" + "\t\t" + "8 Vertical Section";
        txt += "\r\n";
        txt += "CL\t\t\t\t "+measurement+ ":" + "\t\t" + "9 CL";
        txt += "\r\n";
        txt += "Closure Distance "+measurement+":" + "\t\t" + "10 Closure Distance";
        txt += "\r\n";
        txt += "Closure Direction " +measurement+":" + "\t\t" + "11 Closure Direction";
        txt += "\r\n";
        txt += "DLS\t\t\t\t "+measurement+":" + "\t\t" + "12 Dog Leg Serverity";
        txt += "\r\n";
        txt += "DLA\t\t\t\t DEG:" + "\t\t" + "13 Dog Leg Angle";
        txt += "\r\n";
        txt += "BR\t\t\t\t "+measurement+":" + "\t\t" + "14 B/Rate";
        txt += "\r\n";
        txt += "WR\t\t\t\t "+measurement+":" + "\t\t" + "15 Walk Rate";
        txt += "\r\n";
        txt += "TFO\t\t\t\t DEG:" + "\t\t" + "16 Toolface orientation";
        txt += "\r\n";



        txt += "~ASCII LOG DATA SECTION";
        txt += "\r\n";



        

        RadGrid1.AllowPaging = false;
        RadGrid1.Rebind();
        foreach (GridDataItem item in RadGrid1.Items)
        {

            foreach (TableCell cell in item.Cells)
            {

                
                
                double outNum;
                
                if (double.TryParse(cell.Text, out outNum))
                {
                    txt += cell.Text + "\t\t";
                }
            }
            txt += "\r\n";
        }
        RadGrid1.AllowPaging = true;
        RadGrid1.Rebind();

        //int  CurveGroupID = Convert.ToInt32(Request.QueryString["CurveGroupID"]);

        fileName = string.Format("attachment;filename={0}_{1}_{2}_GammaExport.las", dateNow.Date.ToShortDateString(), ViewState["CurveGroupName"].ToString(), ViewState["CurveName"].ToString());

        Response.Clear();
        Response.Buffer = true;
        Response.AddHeader("content-disposition", fileName);
        Response.Charset = "";
        Response.ContentType = "application/text";
        Response.Output.Write(txt);
        Response.Flush();
        Response.End();
    }
    protected void ddlCurve_SelectedIndexChanged(object sender, DropDownListEventArgs e)
    {
        
            DataTable dt = RigTrack.DatabaseObjects.RigTrackDO.GetSurveysForJobReport(Int32.Parse(ddlCurve.SelectedValue));
            DataTable dt2 = RigTrack.DatabaseObjects.RigTrackDO.GetCurveInfo(Int32.Parse(ddlCurve.SelectedValue));
            if (dt.Rows.Count <= 0 || dt2.Rows.Count <= 0)
            {
                return;
            }
            
            
            ViewState["CurveID"] = dt2.Rows[0]["ID"].ToString();
            ViewState["CurveName"] = dt2.Rows[0]["Name"].ToString();
            ViewState["CurveType"] = dt2.Rows[0]["CurveType"].ToString();
            ViewState["CurveNumber"] = dt2.Rows[0]["Number"].ToString();
            //DataTable dt = RigTrack.DatabaseObjects.RigTrackDO.GetAllJobReports(CurveGroupID);
            ViewState["StartDepth"] = dt.Rows[0]["MD"].ToString();
            ViewState["StopDepth"] = dt.Rows[dt.Rows.Count - 1]["MD"].ToString();
            RadGrid1.DataSource = dt;
            RadGrid1.DataBind();
            updPnl1.Update();
        
    }
}