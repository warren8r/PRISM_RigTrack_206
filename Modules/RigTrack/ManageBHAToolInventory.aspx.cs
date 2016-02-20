using MDM;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
public partial class Modules_RigTrack_ManageBHAToolInventory : System.Web.UI.Page
{
    public string DBConnectionString
    {
        get { return Session["client_database"].ToString(); }
    }
    Collector col = new Collector();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //ddlCurveGroup.DataSource = RigTrack.DatabaseObjects.RigTrackDO.GetAllCurveGroupNames();
            //ddlCurveGroup.DataTextField = "CurveGroupName";
            //ddlCurveGroup.DataValueField = "ID";
            //ddlCurveGroup.DataBind();
            lblMode.Text = "Create";
            btnSaveAssetName.Text = "Create";
            hdnSelectedMeterID.Value = "0";
            bindBindBHAToolInventory();
           // BHAbindToolGroups();
            //BHAbindTooTypes();
            BHAbindManufacturer();
            ddl_toolGroup.DataBind();
            ddl_BHAToolType.DataBind();
        }
       
    }
    public void BHAbindManufacturer()
    {
        ddl_manufacturer.Items.Clear();
        //ddl_manufacturer.Items.Add(new RadComboBoxItem("Select", "0"));
        DataTable dt_getassetnamesbycat = SqlHelper.ExecuteDataset(GlobalConnetionString.ClientConnection(), CommandType.Text,
            "SELECT [ID],[Manufacturer] from [RigTrack].[tblCreateToolManufacturer]").Tables[0];
        RadComboBoxFill.FillRadcombobox(ddl_manufacturer, dt_getassetnamesbycat, "Manufacturer", "ID", "0");
    }
    public void BHAbindTooTypes()
    {
        ddl_BHAToolType.Items.Clear();
      
        RadComboBoxFill.FillRadcombobox(ddl_BHAToolType, SqlHelper.ExecuteDataset(GlobalConnetionString.ClientConnection(), CommandType.Text,
            "SELECT [ToolTypeID],[ToolTypeName] from [RigTrack].[BHAToolTypes] where GroupID=" + ddl_toolGroup .SelectedValue+ "").Tables[0], "ToolTypeName", "ToolTypeID", "0");
    }
    public void BHAbindToolGroups()
    {
        ddl_toolGroup.Items.Clear();
        
        RadComboBoxFill.FillRadcombobox(ddl_toolGroup, SqlHelper.ExecuteDataset(GlobalConnetionString.ClientConnection(), CommandType.Text,
            "SELECT 0 AS [ID], 'Select Groupname' AS [BHAGroupName] UNION select [ID],[BHAGroupName] from [RigTrack].[tblCreateBHAToolGroup]").Tables[0], "BHAGroupName", "ID", "0");
    }
    public void bindBindBHAToolInventory()
    {
        radgrdMeterList.DataSource = RefreshMeterList("");
    }
    protected void RadAjaxManager1_AjaxRequest(object sender, Telerik.Web.UI.AjaxRequestEventArgs e)
    {
        //ddl_manufacturer.Items.Clear();
        
        BHAbindManufacturer();
        ddl_toolGroup.DataBind();
        ddl_BHAToolType.DataBind();
        
    }
   
    protected void ddl_assetcategory_OnSelectedIndexChanged(object sender, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs e)
    {
       /* DataTable dt_getassetnamesbycat = SqlHelper.ExecuteDataset(GlobalConnetionString.ClientConnection(), CommandType.Text, "Select * from PrismAssetName where AssetCategoryId=" + ddl_assetcategory.SelectedValue + "").Tables[0];
        //ddl_assetname
        if (ddl_assetcategory.SelectedIndex != 0)
        {
            RadComboBoxFill.FillRadcombobox(ddl_assetname, dt_getassetnamesbycat, "AssetName", "Id", "0");
        }
        * */
    }
    public bool uniqueAssetInsert(string assetname, string serialnumber)
    {
        bool status = true;
        DataTable dt_prismasset = SqlHelper.ExecuteDataset(GlobalConnetionString.ClientConnection(), CommandType.Text, "Select * from [RigTrack].[Prism_BHAAssets]").Tables[0];
        if (dt_prismasset.Rows.Count > 0)
        {
            DataRow[] row_asset = dt_prismasset.Select("AssetName='" + assetname + "' and SerialNumber='" + serialnumber + "'");
            if (row_asset.Length == 0)
                status = true;
            else
                status = false;
        }
        return status;
    }
    protected void btnSaveAssetName_Click(object sender, EventArgs e)
    {

        if (lblMode.Text != "Edit")
        {
            if (uniqueAssetInsert(ddl_toolGroup.SelectedValue, radtxtSerialNumber.Text))
            {
                txtAssetNumber.Text = col.GenerateNewAccountID(0);

                try
                {
                    string queryInsert = "Insert into [RigTrack].[Prism_BHAAssets](AssetId,WarehouseId,BHAGroupId,BHATypeId,AssetName,SerialNumber,Description," +
                                       "Manufacturer,Status," +
                                       //"AFE,AltPartNumber,InserviceDate,CostAdjustDate,RetireDate,Department,VerifiedLocation,VerifiedDate,MonthstoDepreciate,Hoursesincelastservice," +
                                       "ODFrac,IDFrac,Length,TopConnection,BottomConnection,FishingNeck,StabCenterPoint,StabBladeOD,Weight,EI,SizeCategory,Cost) values"+
                                       " ('" + txtAssetNumber.Text + "','" + ddl_warehouse.SelectedValue + "','"+ddl_toolGroup.SelectedValue+"'," +
                                       "'" + ddl_BHAToolType.SelectedValue + "','" + ddl_toolGroup.SelectedValue + "','" + radtxtSerialNumber.Text + "'," +
                                       "'" + txt_description.Text + "'," +
                                       "'" + ddl_manufacturer.SelectedValue + "',1," +
                                       "'" + txtODFrac.Text + "','" + txtIDFrac.Text + "','" + txtLength.Text + "'," +
                                       "'" + txtTopConnection.Text + "','" + txtBottomConnection.Text + "','" + txtFishingNeck.Text + "'," +
                                       "'" + txtStabCenterPoint.Text + "','" + txtStabBladeOD.Text + "','" + txtWeight.Text + "','" + txtEI.Text + "','" + txtSizeCategory.Text + "','"+ txt_cost.Text+"')";
                                       
                    int insertcnt = SqlHelper.ExecuteNonQuery(GlobalConnetionString.ClientConnection(), CommandType.Text, queryInsert);
                    string notificationsendtowhome = eventNotification.sendEventNotification("AC01");
                    if (notificationsendtowhome != "")
                    {
                        bool status = eventNotification.sendEventNotificationWithEmail(notificationsendtowhome, "AC01", "ASSET", txtAssetNumber.Text, ddl_toolGroup.SelectedItem.Text,
                               "", "", "Create", "", "");
                    }
                    string str_insert_q = "insert into PrismEvent(eventCode,eventInfo,eventTime,userAssignedTimeStamp,Source)values(" +
                    "'AC01','Asset Created','" + DatesbetweenDatatable.getdatetimeformat(DateTime.Now.ToString()) + "'" +
                ",'" + DatesbetweenDatatable.getdatetimeformat(DateTime.Now.ToString()) + "','ASSET')";
                    SqlHelper.ExecuteNonQuery(GlobalConnetionString.ClientConnection(), CommandType.Text, str_insert_q);
                    lblMessage.Text = "Tool Detials Inserted Successfully";
                    lblMessage.ForeColor = Color.Green;
                    if (insertcnt > 0)
                    {
                        DataTable dt_maxid = SqlHelper.ExecuteDataset(GlobalConnetionString.ClientConnection(), CommandType.Text, "SELECT * FROM  [RigTrack].[Prism_BHAAssets] WHERE  Id = IDENT_CURRENT('[RigTrack].[Prism_BHAAssets]')").Tables[0];
                        string Id = dt_maxid.Rows[0]["Id"].ToString();
                        #region Commented
                        //foreach (RadListBoxItem item in RadListBox4.Items)
                        //{
                        //    string aaa = item.Value;
                        //    string compstatus = "";
                        //    if (rd_yes.Checked)
                        //    {
                        //        compstatus = "Yes";
                        //    }
                        //    else
                        //    {
                        //        compstatus = "No";
                        //    }
                        //    string insertq = "insert into PrismAssetComponents(AssetId,CompID,AssignmentStatus, AssignedDate,componentstatus)values(" + Id + "," + item.Value + ",'Active', '" + DatesbetweenDatatable.getdatetimeformat(DateTime.Now.ToString()) + "','" + compstatus + "')";
                        //    SqlHelper.ExecuteNonQuery(GlobalConnetionString.ClientConnection(), CommandType.Text, insertq);
                        //}
                        #endregion
                        SqlHelper.ExecuteNonQuery(GlobalConnetionString.ClientConnection(), CommandType.Text,
                            "Insert into PrismAssetCurrentLocation(AssetID,FromLocationType,FromLocationID,ToLocationType,ToLocationID,AssetMovedDate,AssetStatus,CurrentLocationType,CurrentLocationID)" +
                            " values(" + Id + ",'WareHouse'," + ddl_warehouse.SelectedValue + ",'WareHouse'," + ddl_warehouse.SelectedValue + ",'" + DateTime.Now + "','Available','WareHouse'," + ddl_warehouse.SelectedValue + ")");

                        //uploadeddocs(Id);
                    }
                    ClearSelectedMeterFields();

                }
                catch (Exception ex)
                {
                }

            }
            else
            {
                lblMessage.ForeColor = Color.Red;
                lblMessage.Text = "Tool name and Serial number combination should be Unique";
            }
        }
        else
        {
            try
            {
                string queryUpdate = "Update [RigTrack].[Prism_BHAAssets] set WarehouseId='" + ddl_warehouse.SelectedValue + "',BHAGroupId='"+ddl_toolGroup.SelectedValue+"'," +
                    " BHATypeId='" + ddl_BHAToolType.SelectedValue + "',AssetName='" + ddl_toolGroup.SelectedValue + "',SerialNumber='" + radtxtSerialNumber.Text + "'," +
                    "ODFrac='" + txtODFrac.Text + "',IDFrac='" + txtIDFrac.Text + "',Length='" + txtLength.Text + "',Description='" + txt_description.Text + "'," +
                    " Manufacturer="+ddl_manufacturer.SelectedValue+",TopConnection='" + txtTopConnection.Text + "',BottomConnection='" + txtBottomConnection.Text + "',FishingNeck='" + txtFishingNeck.Text + "'," +
                    " StabCenterPoint='" + txtStabCenterPoint.Text + "',StabBladeOD='" + txtStabBladeOD.Text + "',Weight='" + txtWeight.Text + "',EI='" + txtEI.Text + "',SizeCategory='" + txtSizeCategory.Text + "',";
                if (txt_cost.Text != "")
                    queryUpdate += " Cost='" + txt_cost.Text + "'";
                else
                    queryUpdate += " Cost=NULL";
                #region Comments
                //string queryUpdate = "Update Prism_Assets set WarehouseId='" + ddl_warehouse.SelectedValue + "'," +
                //    " AssetCategoryId='" + ddl_assetcategory.SelectedValue + "',AssetName='" + ddl_assetname.SelectedValue + "',SerialNumber='" + radtxtSerialNumber.Text + "'," +
                //    "Type='" + radtxtType.Text + "',Make='" + radtxtMeterMake.Text + "',PartNumber='" + ddl_partnumber.SelectedValue + "',Description='" + txt_description.Text + "'," +
                //    " Plant='" + ddl_plant.SelectedValue + "',ResponsibleParty='" + ddl_responsibleparty.SelectedValue + "',DepreciationType='" + ddl_depreciationtype.SelectedValue + "'," +
                //    " Size='" + ddl_size.SelectedValue + "',Owner='" + ddl_owner.SelectedValue + "',PuechasedNew='" + isChecked(chk_purchasenew) + "',Costadjust='" + isChecked(chk_costadjust) + "'," +
                //    " Depreciate='" + isChecked(chk_depreciate) + "',Manufacturer='" + ddl_manufacturer.SelectedValue + "',ManufactureCountry='" + ddl_manufacturecountry.SelectedValue + "'," +
                //    "ManufractureDate='" + date_manufracture.SelectedDate + "',AFE='" + txt_AFE.Text + "',AltPartNumber='" + txt_altpartnumber.Text + "',InserviceDate='" + date_inservice.SelectedDate + "'," +
                //    "CostAdjustDate='" + date_costadjusted.SelectedDate + "',RetireDate='" + date_retire.SelectedDate + "',Department='" + ddl_department.SelectedValue + "',VerifiedLocation='" + ddl_verfiedlocation.SelectedValue + "'," +
                //    " VerifiedDate='" + date_verified.SelectedDate + "',MonthstoDepreciate='" + txt_months_depreciate.Text + "',Hoursesincelastservice='" + txt_hourselastservice.Text + "'," +
                //    "Netvalue='" + txt_netvalue.Text + "',Weight_LBS='" + txt_weightlbs.Text + "',Weight_Kgs='" + txt_weightkgs.Text + "',ScheduleB='" + ddl_scheduleb.SelectedValue + "'," +
                //    "ABCCode='" + txt_abc.Text + "',Status='" + isChecked(chkActive) + "',Notes='" + txt_notes.Text + "',runhrmaintenance='" + txtrunmaintenance.Text + "',maintenancepercentage='" + txt_maintenance.Text + "',";
                //if (txt_cost.Text != "")
                //    queryUpdate += " Cost='" + txt_cost.Text + "'";
                //else
                //    queryUpdate += " Cost=NULL";

                //if (txt_totrunhrs.Text != "")
                //    queryUpdate += " Cost=" + txt_totrunhrs.Text + "";
                //else
                //    queryUpdate += " Cost=NULL";
                #endregion
                queryUpdate += " where ID=" + hdnSelectedMeterID.Value + "";
                SqlHelper.ExecuteNonQuery(GlobalConnetionString.ClientConnection(), CommandType.Text, queryUpdate);
                #region Comments
                //string delete = "delete from PrismAssetComponents where AssetId=" + hdnSelectedMeterID.Value + "";
                //SqlHelper.ExecuteNonQuery(GlobalConnetionString.ClientConnection(), CommandType.Text, delete);
                //foreach (RadListBoxItem item in RadListBox4.Items)
                //{
                //    string aaa = item.Value;
                //    string compstatus = "";
                //    if (rd_yes.Checked)
                //    {
                //        compstatus = "Yes";
                //    }
                //    else
                //    {
                //        compstatus = "No";
                //    }
                //    string insertq = "insert into PrismAssetComponents(AssetId,CompID,AssignmentStatus, AssignedDate,componentstatus)values(" + hdnSelectedMeterID.Value + "," + item.Value + ",'Active', '" + DatesbetweenDatatable.getdatetimeformat(DateTime.Now.ToString()) + "','" + compstatus + "')";
                //    SqlHelper.ExecuteNonQuery(GlobalConnetionString.ClientConnection(), CommandType.Text, insertq);
                //}
                #endregion
                lblMessage.Text = "Tool Detials Update Successfully";

                lblMessage.ForeColor = Color.Green;

                ClearSelectedMeterFields();
            }
            catch (Exception ex) { }
        }
        lblMessage.Text = lblMessage.Text;
        lblMessage.ForeColor = lblMessage.ForeColor;
        radgrdMeterList.Rebind();
        //lbl_docid.Text = "0";
        //radgrdMeterList.Rebind();


    }
    private DataTable RefreshMeterList(string AssetKeyid)
    {
        // Instantiate a DataBase connection
        SqlConnection sqlConn = new SqlConnection(DBConnectionString);

        // Instantiate a DataTable to hold the resultset
        DataTable dt = new DataTable();

        try
        {
            // Open the database connection
            sqlConn.Open();
                string query = "select  p.Id,AssetId,PPA.BHAGroupName as AssetName,P.BHAGroupId as asset_id,c.Name as warehouse,m.Manufacturer as Manufacturer," +
                " ca.ToolTypeName as assetcategory,SerialNumber,Status,p.ODFrac,p.IDFrac,p.Length,p.TopConnection,p.BottomConnection,p.FishingNeck,p.StabCenterPoint,"+
                " p.StabBladeOD,p.Description,p.Weight,p.EI,p.SizeCategory,c.ID as Warehouseid,p.BHAGroupId,p.BHATypeId,p.Cost,m.ID as ManufacturerID " +
                " from [RigTrack].[Prism_BHAAssets] p,PrsimWarehouses c," +
               " [RigTrack].[BHAToolTypes] ca,[RigTrack].[tblCreateBHAToolGroup] PPA,[RigTrack].[tblCreateToolManufacturer] m" +
               " where p.Manufacturer=m.ID and p.WarehouseId=c.ID and p.BHATypeId=ca.ToolTypeID and P.[BHAGroupId]=PPA.Id ";
             
            if (AssetKeyid != "")
            {
               query+=" and p.Id=" + AssetKeyid + "";
            }
            query += "order by p.Id desc";
            // Create a SqlCommand with the SELECT statement to retrieve all the meters
            using (SqlCommand sqlcmd = new SqlCommand(query, sqlConn))
            {
                // Run the SELECT statement to fill the DataTable
                SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
                da.Fill(dt);
            }
        }
        catch (Exception ex)
        { }
        finally
        {
            sqlConn.Close();
        }
        return dt;
        // Set the RADGrid's DataSource to the DataTable
       

    }
    protected void radgrdMeterList_ItemCommand(object sender, GridCommandEventArgs e)
    {
        // Did the user click the "Edit" button?
        if (e.CommandName == "EditMeter")
        {
            GridDataItem item = (GridDataItem)e.Item;
            string dataKeyValue = Convert.ToString(((GridDataItem)(item)).GetDataKeyValue("ID"));
            hdnSelectedMeterID.Value = item["ID"].Text;
            Label lbl_assetid = (Label)item.FindControl("lbl_assetid");
            btnSaveAssetName.Text = "Update";
            lblMode.Text = "Edit";
            //DataTable dt = GetSelectedMeterFields(dataKeyValue);
            DataTable dt = RefreshMeterList(dataKeyValue);
            if (dt != null &&
                dt.Rows.Count > 0)
            {
                LoadSelectedMeterFields(dt);

            }
        }
    }
    private DataTable GetSelectedMeterFields(string AssetKeyid)
    {
        // Instantiate a DataBase connection
        SqlConnection sqlConn = new SqlConnection(DBConnectionString);

        // Instantiate a DataTable for the return value
        DataTable dt = new DataTable();

        try
        {
            // Open the database connection
            sqlConn.Open();
            //string query = "select p.Id,AssetId,PPA.AssetName as AssetName,P.Assetname as asset_id,c.Name as warehouse,ca.clientAssetName as assetcategory,SerialNumber," +
            // "Status,p.ODFrac,p.IDFrac,p.Length,p.TopConnection,p.BottomConnection,p.PinTop,p.PinBottom,p.Comments,p.Description from Prism_Assets p,PrsimWarehouses c,clientAssets ca,PrismAssetName PPA" +
            //  " where p.WarehouseId=c.ID and p.AssetCategoryId=ca.clientAssetID and P.AssetName=PPA.Id and p.Id=" + AssetKeyid + "";
            string query = "select top(5) *, p.Id,AssetId,PPA.AssetName as AssetName,P.Assetname as asset_id,c.Name as warehouse,ca.ToolTypeName as assetcategory," +
                " SerialNumber,Status,p.ODFrac,p.IDFrac,p.Length,p.TopConnection,p.BottomConnection,p.FishingNeck,p.StabCenterPoint,p.StabBladeOD,p.Description,p.Weight,"+
                " p.EI,p.SizeCategory from [RigTrack].[Prism_BHAAssets] p,PrsimWarehouses c,[RigTrack].[BHAToolTypes] ca,PrismAssetName PPA" +
                " where p.WarehouseId=c.ID and p.BHATypeId=ca.ToolTypeID and P.AssetName=PPA.Id and p.Id=" + AssetKeyid + "";
            // Create a SqlCommand with the SELECT statement to retrieve all the meters
            using (SqlCommand sqlcmd = new SqlCommand(query, sqlConn))
            {

                // Add a parameter to specify the ID value for the meter row
                // sqlcmd.Parameters.AddWithValue("@ID", hdnSelectedMeterID.Value);

                // Run the SELECT statement to fill the DataTable
                SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
                da.Fill(dt);
            }
        }
        catch (Exception ex)
        { }
        finally
        {
            sqlConn.Close();
        }

        // Return the DataTable
        return dt;
    }
    private void LoadSelectedMeterFields(DataTable dt)
    {
        DataTable dt_getassetnamesbycat = SqlHelper.ExecuteDataset(GlobalConnetionString.ClientConnection(), CommandType.Text, "Select * from PrismAssetName where Id=" + dt.Rows[0]["asset_id"].ToString() + "").Tables[0];
        //ddl_assetname
        //ddl_toolGroup.SelectedValue = dt.Rows[0]["asset_id"].ToString();
        //ddl_toolGroup.DataSource = dt_getassetnamesbycat;
        //ddl_toolGroup.DataBind();
        // RadComboBoxFill.FillRadcombobox(ddl_assetname, dt_getassetnamesbycat, "AssetName", "Id", "0");

        ddl_warehouse.SelectedValue = dt.Rows[0]["WarehouseId"].ToString();
        ddl_BHAToolType.SelectedValue = dt.Rows[0]["BHATypeId"].ToString();
        ddl_toolGroup.SelectedValue = dt.Rows[0]["BHAGroupId"].ToString();
        txtAssetNumber.Text = dt.Rows[0]["AssetId"].ToString();
        //ddl_assetname.SelectedValue = dt.Rows[0]["asset_id"].ToString();
        radtxtSerialNumber.Text = dt.Rows[0]["SerialNumber"].ToString();
        
        txt_description.Text = dt.Rows[0]["Description"].ToString();
        //BHAbindTypes();
        ddl_manufacturer.SelectedValue = dt.Rows[0]["ManufacturerID"].ToString();
        txtODFrac.Text = dt.Rows[0]["ODFrac"].ToString();
        txtIDFrac.Text = dt.Rows[0]["IDFrac"].ToString();
        txtLength.Text = dt.Rows[0]["Length"].ToString();
        
        txtTopConnection.Text = dt.Rows[0]["TopConnection"].ToString();
        txtBottomConnection.Text = dt.Rows[0]["BottomConnection"].ToString();
        txtFishingNeck.Text = dt.Rows[0]["FishingNeck"].ToString();
        txtStabCenterPoint.Text = dt.Rows[0]["StabCenterPoint"].ToString();
        txtStabBladeOD.Text = dt.Rows[0]["StabBladeOD"].ToString();
        txtWeight.Text = dt.Rows[0]["Weight"].ToString();
        txtEI.Text = dt.Rows[0]["EI"].ToString();
        txtSizeCategory.Text = dt.Rows[0]["SizeCategory"].ToString();
        txt_cost.Text= dt.Rows[0]["Cost"].ToString();
    }
    protected void radgrdMeterList_PageIndexChanged(object sender, GridPageChangedEventArgs e)
    {
        radgrdMeterList.CurrentPageIndex = e.NewPageIndex;

    }
    protected void radgrdMeterList_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
    {
        bindBindBHAToolInventory();
    }
    private void ClearSelectedMeterFields()
    {
        // Clear the contents of the Create/Edit panel fields
        
        //lblActive.Text = "Active";
        //lblActive.Attributes.Remove("style");
        txt_description.Text = string.Empty;
        ddl_toolGroup.SelectedValue = "0";
        radtxtSerialNumber.Text = string.Empty;

        btnSaveAssetName.Text = "Create";
        txtAssetNumber.Text = string.Empty;
        ddl_warehouse.SelectedValue = "0";
        ddl_BHAToolType.SelectedValue = "0";
        
        ddl_manufacturer.SelectedValue = "0";
        txtODFrac.Text = "";
        txtIDFrac.Text = "";
        txtLength.Text = "";
        
        txtTopConnection.Text = "";
        txtBottomConnection.Text = "";
        txtFishingNeck.Text = "";
        txtStabCenterPoint.Text = "";
        txtStabBladeOD.Text = "";
        txtWeight.Text = "";
        txtEI.Text = "";
        txtSizeCategory.Text = "";
        txt_cost.Text = "";
    }
    protected void ddl_toolGroup_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
    {

    }
    protected void btnCancelAssetName_Click(object sender, EventArgs e)
    {
        ClearSelectedMeterFields();
    }
}