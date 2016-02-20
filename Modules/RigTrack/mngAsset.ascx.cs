using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Text;
using Artem.Google.Net;
using Telerik.Web.UI;
using MDM;
using System.Drawing;

public partial class controls_confgMangrMeter_mngMeter : System.Web.UI.UserControl
{
    #region properties

    public string DBConnectionString
    {
        get { return Session["client_database"].ToString(); }
    }
    #region global_classes

    MDM.Collector Collector = new MDM.Collector();

    #endregion
    #endregion

    string pageId = "16";

    public void Page_Load(object sender, EventArgs e)
    {
        // Register JavaScripts for the page
        RegisterJavaScript();

        ddl_assetname.Enabled = true;
        // Clear the messages
        lblMessage.Text = string.Empty;
        lbl_message.Text = string.Empty;
        if (!IsPostBack)
        {
            lblMode.Text = "Create";
            ClearSelectedMeterFields();
            //hdnSelectedMeterID.Value = "0";
            radgrdMeterList.Rebind();
            RefreshCountryCodeLists();
            RefreshStateCodeLists();
            compcategory.Attributes.Add("style", "display:none");
        }
        if(rd_yes.Checked)
            compcategory.Attributes.Add("style", "display:block");
        else
            compcategory.Attributes.Add("style", "display:none");
        
    }

    #region Event Handlers

    protected void chkActive_CheckedChanged(object sender, EventArgs e)
    {
        // Set the text and color of the label next to the Status CheckBox to reflect the Checked state of the checkbox
        SetStatusAttributes(chkActive.Checked);

    }
    protected void ddl_assetcategory_OnSelectedIndexChanged(object sender, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs e)
    {
        DataTable dt_getassetnamesbycat = SqlHelper.ExecuteDataset(GlobalConnetionString.ClientConnection(), CommandType.Text, "Select * from PrismAssetName where AssetCategoryId=" + ddl_assetcategory.SelectedValue + "").Tables[0];
        //ddl_assetname
        if (ddl_assetcategory.SelectedIndex != 0)
        {
            RadComboBoxFill.FillRadcombobox(ddl_assetname, dt_getassetnamesbycat, "AssetName", "Id", "0");
        }
    }

    protected void chkSecSameAsPrimAddress_CheckedChanged(object sender, EventArgs e)
    {

    }

    protected void ddlPrimaryCountry_SelectedIndexChanged(object sender, DropDownListEventArgs e)
    {
        // If the currently selected primary country is the United States, show the state dropdownlist otherwise show a textbox
        ShowPrimaryStateList(e.Value == "US" ? true : false);
    }

    protected void lnkbtnPrimaryGatherGIS_Click(object sender, EventArgs e)
    {

    }

    protected void ddlSecondaryCountry_SelectedIndexChanged(object sender, DropDownListEventArgs e)
    {
        // If the currently selected secondary country is the United States, show the state dropdownlist otherwise show a textbox
        ShowSecondaryStateList(e.Value == "US" ? true : false);
    }
    public bool isChecked(CheckBox chk)
    {
        if (chk.Checked)
            return true;
        else
            return false;
    }
    public void uploadeddocs(string Id)
    {
        string query = "", filename = "", uniqueFilename = "", query_EventTaskOrderDocuments;
        string cnt = radupload_docs.InitialFileInputsCount.ToString();

        for (int file = 0; file < radupload_docs.UploadedFiles.Count; file++)
        {
            try
            {
                query = "";
                query_EventTaskOrderDocuments = "";
                filename = radupload_docs.UploadedFiles[file].GetName();
                uniqueFilename = radupload_docs.UploadedFiles[file].GetNameWithoutExtension() + "_" + DateTime.Now.ToString("MMddyyyyhhmmss") + "" + radupload_docs.UploadedFiles[file].GetExtension();
                radupload_docs.UploadedFiles[file].SaveAs(Server.MapPath("../../Documents/") + uniqueFilename);
                query = "Insert into Documents(DocumentDisplayName,DocumentName) values('" + filename + "','" + uniqueFilename + "')";
                int documentinsert = SqlHelper.ExecuteNonQuery(GlobalConnetionString.ClientConnection(), CommandType.Text, query);
                if (documentinsert > 0)
                {
                    DataTable dt_docs = SqlHelper.ExecuteDataset(GlobalConnetionString.ClientConnection(), CommandType.Text, "SELECT * FROM    Documents WHERE  DocumentID = IDENT_CURRENT('Documents')").Tables[0];
                    query_EventTaskOrderDocuments = "Insert into JobAssetDocuments(jid,DocumentID,UserID,UploadedDate,type) values " +
                        " ('" + Id + "','" + dt_docs.Rows[0]["DocumentID"].ToString() + "','" + Session["userId"].ToString() + "','" + DateTime.Now.ToString() + "','SA')";
                    SqlHelper.ExecuteNonQuery(GlobalConnetionString.ClientConnection(), CommandType.Text, query_EventTaskOrderDocuments);
                    RadGrid1.Rebind();
                }
            }
            catch (Exception ex)
            {
                lbl_message.Text = ex.Message;
            }

        }
    }
    public bool uniqueAssetInsert(string assetname, string serialnumber)
    {
        bool status = true;
        DataTable dt_prismasset = SqlHelper.ExecuteDataset(GlobalConnetionString.ClientConnection(), CommandType.Text, "Select * from Prism_Assets").Tables[0];
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

    protected void radbtnSaveMeter_Click(object sender, EventArgs e)
    {

        if (lblMode.Text != "Edit")
        {
            if (uniqueAssetInsert(ddl_assetname.SelectedValue, radtxtSerialNumber.Text))
            {
                txtAssetNumber.Text = Collector.GenerateNewAccountID(0);

                try
                {
                    string queryInsert = "Insert into Prism_Assets(AssetId,WarehouseId,AssetCategoryId,AssetName,SerialNumber,Type,Make,PartNumber,Description,Plant," +
                                       "ResponsibleParty,DepreciationType,Size,Owner,PuechasedNew,Costadjust,Depreciate,Manufacturer,ManufactureCountry,ManufractureDate," +
                                       "AFE,AltPartNumber,InserviceDate,CostAdjustDate,RetireDate,Department,VerifiedLocation,VerifiedDate,MonthstoDepreciate,Hoursesincelastservice," +
                                       "Netvalue,Weight_LBS,Weight_Kgs,ScheduleB,ABCCode,Status,runhrmaintenance,maintenancepercentage,Cost,Notes,previoususedhrs) values ('" + txtAssetNumber.Text + "','" + ddl_warehouse.SelectedValue + "'," +
                                       "'" + ddl_assetcategory.SelectedValue + "','" + ddl_assetname.SelectedValue + "','" + radtxtSerialNumber.Text + "','" + radtxtType.Text + "','" + radtxtMeterMake.Text + "','" + ddl_partnumber.SelectedValue + "'," +
                                       "'" + txt_description.Text + "','" + ddl_plant.SelectedValue + "','" + ddl_responsibleparty.SelectedValue + "','" + ddl_depreciationtype.SelectedValue + "','" + ddl_size.SelectedValue + "'," +
                                       "'" + ddl_owner.SelectedValue + "','" + isChecked(chk_purchasenew) + "','" + isChecked(chk_costadjust) + "','" + isChecked(chk_depreciate) + "','" + ddl_manufacturer.SelectedValue + "'," +
                                       "'" + ddl_manufacturecountry.SelectedValue + "','" + date_manufracture.SelectedDate + "','" + txt_AFE.Text + "','" + txt_altpartnumber.Text + "','" + date_inservice.SelectedDate + "'," +
                                       "'" + date_costadjusted.SelectedDate + "','" + date_retire.SelectedDate + "','" + ddl_department.SelectedValue + "','" + ddl_verfiedlocation.SelectedValue + "','" + date_verified.SelectedDate + "'," +
                                       "'" + txt_months_depreciate.Text + "','" + txt_hourselastservice.Text + "','" + txt_netvalue.Text + "','" + txt_weightlbs.Text + "','" + txt_weightkgs.Text + "','" + ddl_scheduleb.SelectedValue + "'," +
                                       "'" + txt_abc.Text + "','" + isChecked(chkActive) + "',"+txtrunmaintenance.Text+","+txt_maintenance.Text+"";
                    if (txt_cost.Text != "")
                    {
                        queryInsert += "," + txt_cost.Text + ",'" + txt_notes.Text + "'";
                    }
                    else
                    {
                        queryInsert += ",NULL,'" + txt_notes.Text + "'";
                    }

                    if (txt_totrunhrs.Text != "")
                    {
                        queryInsert += "," + txt_totrunhrs.Text + ")";
                    }
                    else
                    {
                        queryInsert += ",NULL)";
                    }
                    
                    //queryInsert += "," + txtrunmaintenance.Text + ",";
                    int insertcnt = SqlHelper.ExecuteNonQuery(GlobalConnetionString.ClientConnection(), CommandType.Text, queryInsert);
                    string notificationsendtowhome = eventNotification.sendEventNotification("AC01");
                    if (notificationsendtowhome != "")
                    {
                        bool status = eventNotification.sendEventNotificationWithEmail(notificationsendtowhome, "AC01", "ASSET", txtAssetNumber.Text, ddl_assetname.SelectedItem.Text,
                               "", "", "Create", "", "");
                    }
                    string str_insert_q = "insert into PrismEvent(eventCode,eventInfo,eventTime,userAssignedTimeStamp,Source)values(" +
                    "'AC01','Asset Created','" + DatesbetweenDatatable.getdatetimeformat(DateTime.Now.ToString()) + "'" +
                ",'" + DatesbetweenDatatable.getdatetimeformat(DateTime.Now.ToString()) + "','ASSET')";
                    SqlHelper.ExecuteNonQuery(GlobalConnetionString.ClientConnection(), CommandType.Text, str_insert_q);
                    lbl_message.Text = "Asset Detials Inserted Successfully";
                    lbl_message.ForeColor = Color.Green;
                    if (insertcnt > 0)
                    {
                        DataTable dt_maxid = SqlHelper.ExecuteDataset(GlobalConnetionString.ClientConnection(), CommandType.Text, "SELECT * FROM  Prism_Assets WHERE  Id = IDENT_CURRENT('Prism_Assets')").Tables[0];
                        string Id = dt_maxid.Rows[0]["Id"].ToString();
                        foreach (RadListBoxItem item in RadListBox4.Items)
                        {
                            string aaa = item.Value;
                            string compstatus = "";
                            if (rd_yes.Checked)
                            {
                                compstatus = "Yes";
                            }
                            else
                            {
                                compstatus = "No";
                            }
                            string insertq = "insert into PrismAssetComponents(AssetId,CompID,AssignmentStatus, AssignedDate,componentstatus)values(" + Id + "," + item.Value + ",'Active', '" + DatesbetweenDatatable.getdatetimeformat(DateTime.Now.ToString()) + "','" + compstatus + "')";
                            SqlHelper.ExecuteNonQuery(GlobalConnetionString.ClientConnection(), CommandType.Text, insertq);
                        }
                        SqlHelper.ExecuteNonQuery(GlobalConnetionString.ClientConnection(), CommandType.Text,
                            "Insert into PrismAssetCurrentLocation(AssetID,FromLocationType,FromLocationID,ToLocationType,ToLocationID,AssetMovedDate,AssetStatus,CurrentLocationType,CurrentLocationID)" +
                            " values(" + Id + ",'WareHouse'," + ddl_warehouse.SelectedValue + ",'WareHouse'," + ddl_warehouse.SelectedValue + ",'" + DateTime.Now + "','Available','WareHouse'," + ddl_warehouse.SelectedValue + ")");

                        uploadeddocs(Id);
                    }
                    ClearSelectedMeterFields();

                }
                catch (Exception ex)
                {
                }

            }
            else
            {
                lbl_message.ForeColor = Color.Red;
                lbl_message.Text = "Asset name and Serial number combination should be Unique";
            }
        }
        else
        {
            try
            {

                string queryUpdate = "Update Prism_Assets set AssetId='" + txtAssetNumber.Text + "',WarehouseId='" + ddl_warehouse.SelectedValue + "'," +
                    " AssetCategoryId='" + ddl_assetcategory.SelectedValue + "',AssetName='" + ddl_assetname.SelectedValue + "',SerialNumber='" + radtxtSerialNumber.Text + "'," +
                    "Type='" + radtxtType.Text + "',Make='" + radtxtMeterMake.Text + "',PartNumber='" + ddl_partnumber.SelectedValue + "',Description='" + txt_description.Text + "'," +
                    " Plant='" + ddl_plant.SelectedValue + "',ResponsibleParty='" + ddl_responsibleparty.SelectedValue + "',DepreciationType='" + ddl_depreciationtype.SelectedValue + "'," +
                    " Size='" + ddl_size.SelectedValue + "',Owner='" + ddl_owner.SelectedValue + "',PuechasedNew='" + isChecked(chk_purchasenew) + "',Costadjust='" + isChecked(chk_costadjust) + "'," +
                    " Depreciate='" + isChecked(chk_depreciate) + "',Manufacturer='" + ddl_manufacturer.SelectedValue + "',ManufactureCountry='" + ddl_manufacturecountry.SelectedValue + "'," +
                    "ManufractureDate='" + date_manufracture.SelectedDate + "',AFE='" + txt_AFE.Text + "',AltPartNumber='" + txt_altpartnumber.Text + "',InserviceDate='" + date_inservice.SelectedDate + "'," +
                    "CostAdjustDate='" + date_costadjusted.SelectedDate + "',RetireDate='" + date_retire.SelectedDate + "',Department='" + ddl_department.SelectedValue + "',VerifiedLocation='" + ddl_verfiedlocation.SelectedValue + "'," +
                    " VerifiedDate='" + date_verified.SelectedDate + "',MonthstoDepreciate='" + txt_months_depreciate.Text + "',Hoursesincelastservice='" + txt_hourselastservice.Text + "'," +
                    "Netvalue='" + txt_netvalue.Text + "',Weight_LBS='" + txt_weightlbs.Text + "',Weight_Kgs='" + txt_weightkgs.Text + "',ScheduleB='" + ddl_scheduleb.SelectedValue + "'," +
                    "ABCCode='" + txt_abc.Text + "',Status='" + isChecked(chkActive) + "',Notes='" + txt_notes.Text + "',runhrmaintenance='" + txtrunmaintenance.Text + "',maintenancepercentage='"+txt_maintenance.Text+"',";
                if (txt_cost.Text != "")
                    queryUpdate += " Cost='" + txt_cost.Text + "'";
                else
                    queryUpdate += " Cost=NULL";
                
                //if (txt_totrunhrs.Text != "")
                //    queryUpdate += " Cost=" + txt_totrunhrs.Text + "";
                //else
                //    queryUpdate += " Cost=NULL";

                //queryUpdate += " where ID=" + hdnSelectedMeterID.Value + "";
                SqlHelper.ExecuteNonQuery(GlobalConnetionString.ClientConnection(), CommandType.Text, queryUpdate);
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
                lbl_message.Text = "Asset Detials Update Successfully";
                
                lbl_message.ForeColor = Color.Green;

                ClearSelectedMeterFields();
            }
            catch (Exception ex) { }
        }
        lblMessage.Text = lbl_message.Text;
        lblMessage.ForeColor = lbl_message.ForeColor;
        radgrdMeterList.Rebind();
        lbl_docid.Text = "0";
        RadGrid1.Rebind();
        

    }

    protected void radbtnCancelMeter_Click(object sender, EventArgs e)
    {
        // Is the page in "Edit" mode
        if (lblMode.Text == "Edit")
        {
            // Yes, put the page into "Create" mode
            lblMode.Text = "Create";
            //hdnSelectedMeterID.Value = "0";
            radbtnCancelMeter.Text = "Reset";
        }

        // Refresh the page
        radgrdMeterList.SelectedIndexes.Clear();
        ClearSelectedMeterFields();
    }

    protected void radgrdMeterList_ItemCommand(object sender, GridCommandEventArgs e)
    {
        // Did the user click the "Edit" button?
        if (e.CommandName == "EditMeter")
        {
            // Yes, get the item that contains the button that was clicked
            GridDataItem item = (GridDataItem)e.Item;
            string dataKeyValue = Convert.ToString(((GridDataItem)(item)).GetDataKeyValue("ID"));
            Label lbl_assetid = (Label)item.FindControl("lbl_assetid");
            DataTable dt_getststus = SqlHelper.ExecuteDataset(GlobalConnetionString.ClientConnection(), CommandType.Text, "select * from PrismAssetComponents where AssetId=" + lbl_assetid.Text + "").Tables[0];
            if (dt_getststus.Rows.Count > 0)
            {
                rd_yes.Checked = true;
                rd_no.Checked = false;
                compcategory.Attributes.Add("style", "display:block");
            }
            else
            {
                rd_no.Checked = true;
                rd_yes.Checked = false;
                compcategory.Attributes.Add("style", "display:none");
            }
            if (rd_yes.Checked)
            {
                compcategory.Attributes.Add("style", "display:block");
            }
            else
            {
                compcategory.Attributes.Add("style", "display:none");
            }
            // Select the item
            item.Selected = true;
            bindselectedcomponents(dataKeyValue);
            lbl_docid.Text = item["ID"].Text;
            // Get the ID of the item selected
            //hdnSelectedMeterID.Value = item["ID"].Text;
            ddl_assetname.Enabled = false;
            // Put the page into "Edit" mode
            lblMode.Text = "Edit";
            //radbtnCancelMeter.Text = "Cancel";
            
            // Clear the Create/Edit panel fields
            ClearSelectedMeterFields();

            // Load the Create/Edit panel fields with data from the selected meter
            DataTable dt = GetSelectedMeterFields(dataKeyValue);
            if (dt != null &&
                dt.Rows.Count > 0)
            {
                LoadSelectedMeterFields(dt);
                
            }
        }
    }
    public void bindselectedcomponents(string assetid)
    {
        string squery = "select CompID as ID, (ComponentName+'('+Serialno+')') as NAME from Prism_Components p,Prism_ComponentNames pc where pc.componet_id=p.Componentid and CompID in (select CompID from PrismAssetComponents where AssetId=" + assetid + ")";
        DataTable dt_components = SqlHelper.ExecuteDataset(GlobalConnetionString.ClientConnection(), CommandType.Text, squery).Tables[0];
        RadListBox4.DataSource = dt_components;
        RadListBox4.DataBind();
    }
    protected void radgrdMeterList_ItemDataBound(object sender, GridItemEventArgs e)
    {
        // Is the bound item and GridDataItem?
        if (e.Item is GridDataItem)
        {
            // Yes, get the item
            GridDataItem item = (GridDataItem)e.Item;
            Label lbl_assetid = (Label)item.FindControl("lbl_assetid");
            Label lbl_assetname = (Label)item.FindControl("lbl_assetname");
            //Label lbl_assetnamepopup = (Label)item.FindControl("lbl_assetnamepopup");
            //lbl_assetnamepopup.Text = "Asset Name:" + lbl_assetname.Text;
            //RadGrid radgridcomponents = (RadGrid)item.FindControl("radgridcomponents");
            //LinkButton lnk_assetnames = (LinkButton)item.FindControl("lnk_assetnames");
            //string query_components = "select ComponentName as NAME,Serialno from PrismAssetComponents pa,Prism_Components pc,"+
            //    " Prism_ComponentNames pn where pa.CompID=pc.CompID and pc.Componentid=pn.componet_id and pa.componentstatus='Yes' and pa.AssetId=" + lbl_assetid.Text + "";
            //DataTable dt_getcompdet = SqlHelper.ExecuteDataset(GlobalConnetionString.ClientConnection(), CommandType.Text, query_components).Tables[0];
            //Label lbl_comp = (Label)item.FindControl("lbl_comp");
            //if (dt_getcompdet.Rows.Count == 0)
            //{
            //    lbl_comp.Text = "- NA -";
            //    lnk_assetnames.Visible = false;
            //}
            //else
            //{
            //    lbl_comp.Text = "";
            //    lnk_assetnames.Visible = true;
            //}
            //radgridcomponents.DataSource = dt_getcompdet;
            //radgridcomponents.DataBind();
            string selectq = "select * from PrismJobRunDetails Run, PrismJobRunHourdetails RunHours" +
                " where Run.runid=RunHours.runid";
            DataTable dt_getassetrunhrs = SqlHelper.ExecuteDataset(GlobalConnetionString.ClientConnection(), CommandType.Text, selectq).Tables[0];
            DataTable dt_prism_assets = SqlHelper.ExecuteDataset(GlobalConnetionString.ClientConnection(), CommandType.Text, "select * from prism_assets").Tables[0];
            Label lbl_totalrunhrs = (Label)item.FindControl("lbl_totalrunhrs");
            Label lbl_totalCumulativerunhrs = (Label)item.FindControl("lbl_totalCumulativerunhrs");
            Label lbl_RunHrsToMaintenance = (Label)item.FindControl("lbl_RunHrsToMaintenance");
            Label lbl_maintenancepercentage = (Label)item.FindControl("lbl_maintenancepercentage");

            DataRow[] dr_runhrval = dt_getassetrunhrs.Select("AssetId=" + lbl_assetid.Text + "");
            string obj_sum = dt_getassetrunhrs.Compute("sum(dailyrunhrs)", "AssetId=" + lbl_assetid.Text + "").ToString();
            DataRow[] dr_prismupdate = dt_prism_assets.Select("ID=" + lbl_assetid.Text + "");
            if (obj_sum != "")
            {
                if (dr_prismupdate[0]["previoususedhrs"].ToString() != "")
                {
                    lbl_totalCumulativerunhrs.Text = (Convert.ToDecimal(obj_sum) + Convert.ToDecimal(dr_prismupdate[0]["previoususedhrs"])).ToString();
                }
                else
                {
                    lbl_totalCumulativerunhrs.Text = Convert.ToDecimal(obj_sum).ToString();
                }
            }
            else 
            {
                lbl_totalCumulativerunhrs.Text = dr_prismupdate[0]["previoususedhrs"].ToString();
            }
            if (dr_runhrval.Length > 0)
            {
                lbl_totalrunhrs.Text = dr_runhrval[0]["dailyrunhrs"].ToString();
            }
            else
            {
                lbl_totalrunhrs.Text = "0";
            }
            if (dr_prismupdate.Length > 0)
            {
                if (dr_prismupdate[0]["LastMaintanenceDate"].ToString() == "" || dr_prismupdate[0]["LastMaintanenceDate"].ToString() =="1/1/1900 12:00:00 AM")
                {
                    if (dr_prismupdate[0]["previoususedhrs"].ToString() != "")
                    {
                        if (obj_sum != "")
                        {
                            if (dr_prismupdate[0]["previoususedhrs"].ToString() != "")
                            {
                                lbl_totalrunhrs.Text = (Convert.ToDecimal(obj_sum) + Convert.ToDecimal(dr_prismupdate[0]["previoususedhrs"])).ToString();
                            }
                            else
                            {
                                lbl_totalrunhrs.Text = Convert.ToDecimal(obj_sum).ToString();
                            }
                            //lbl_totalrunhrs.Text = (Convert.ToDecimal(obj_sum) + Convert.ToDecimal(dr_prismupdate[0]["previoususedhrs"])).ToString();
                        }
                        else
                        {
                            lbl_totalrunhrs.Text = Convert.ToDecimal(dr_prismupdate[0]["previoususedhrs"]).ToString();
                        }
                    }
                }
                else
                {
                    string obj_sumofdate = dt_getassetrunhrs.Compute("sum(dailyrunhrs)", "Date>'" + Convert.ToDateTime(dr_prismupdate[0]["LastMaintanenceDate"].ToString()) + "' and AssetId=" + lbl_assetid.Text + "").ToString();
                    if (obj_sumofdate != "")
                    {
                        //DataRow[] dr_prismupdatedate = dt_prism_assets.Select("Date>" + Convert.ToDateTime(dr_prismupdate[0]["LastMaintanenceDate"].ToString()) + "");
                        lbl_totalrunhrs.Text = obj_sumofdate;
                    }
                }
            }
            Decimal runhrm=0,maintper=0;
            if (lbl_RunHrsToMaintenance.Text == "")
            {
                runhrm = 0;
            }
            else
            {
                runhrm = Convert.ToDecimal(lbl_RunHrsToMaintenance.Text);
            }
            if (lbl_maintenancepercentage.Text == "")
            {
                maintper = 0;
            }
            else
            {
                maintper = Convert.ToDecimal(lbl_maintenancepercentage.Text);
            }
            Decimal cutoff = runhrm - (maintper * runhrm / 100);
            if(lbl_totalrunhrs.Text!="")
            {
                if (Convert.ToDecimal(lbl_totalrunhrs.Text) >= cutoff)
                {
                    //e.Item.BackColor = Color.Red;
                    e.Item.ForeColor = Color.Red;
                }
            //lbl_totalCumulativerunhrs.Text = cum.ToString();
            //PrismAssetComponents
            // Get the Active checkbox in the item
            }
            CheckBox chkActive = (CheckBox)item.FindControl("chkActive");

            // if the meter is In-Active, gray out the row and make the text red
            if (chkActive != null
                && !chkActive.Checked)
            {
                // Set the row background color to gray
                item.Style.Add("background-color", "#CECECE");

                // Set the text color of the checkbox's label to red
                Label lblActive = (Label)item.FindControl("lblActive");
                if (lblActive != null)
                {
                    lblActive.Style.Add("color", "Red");
                }
            }
        }

    }

    protected void radgrdMeterList_PageIndexChanged(object sender, GridPageChangedEventArgs e)
    {
        radgrdMeterList.CurrentPageIndex = e.NewPageIndex;

    }
    protected void radgrdMeterList_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
    {
        RefreshMeterList();
    }
    protected void lnl_generateassetid_Click(object sender, EventArgs e)
    {


        txtAssetNumber.Text = Collector.GenerateNewAccountID(0);
    }
    #endregion

    #region User functions

    /// <summary>
    /// Initializes the fields in the Create/Edit panel
    /// </summary>
    private void ClearSelectedMeterFields()
    {
        // Clear the contents of the Create/Edit panel fields
        chkActive.Checked = true;
        lblActive.Text = "Active";
        lblActive.Attributes.Remove("style");
        ddl_assetname.SelectedValue = "0";
        radtxtSerialNumber.Text = string.Empty;
        radtxtMeterMake.Text = string.Empty;
        radtxtType.Text = string.Empty;
        txtAssetNumber.Text = string.Empty;
        ddl_warehouse.SelectedValue = "0";
        ddl_assetcategory.SelectedValue = "0";
        radtxtMeterMake.Text = string.Empty;
        ddl_partnumber.SelectedValue = "0";
        txt_description.Text = string.Empty;
        ddl_plant.SelectedValue = "0";
        ddl_responsibleparty.SelectedValue = "0";
        ddl_depreciationtype.SelectedValue = "0";
        ddl_size.SelectedValue = "0";
        ddl_owner.SelectedValue = "0";
        chk_purchasenew.Checked = false;
        chk_costadjust.Checked = false;
        chk_depreciate.Checked = false;
        ddl_manufacturer.SelectedValue = "0";
        ddl_manufacturecountry.SelectedValue = "0";
        date_manufracture.SelectedDate = null;
        txt_AFE.Text = string.Empty;
        txt_altpartnumber.Text = string.Empty;
        date_inservice.SelectedDate = null;
        date_costadjusted.SelectedDate = null;
        date_retire.SelectedDate = null;
        ddl_department.SelectedValue = "0";
        ddl_verfiedlocation.SelectedValue = "0";
        date_verified.SelectedDate = null;
        txt_months_depreciate.Text = string.Empty;
        txt_hourselastservice.Text = string.Empty;
        txt_netvalue.Text = string.Empty;
        txt_weightlbs.Text = string.Empty;
        txt_weightkgs.Text = string.Empty;
        ddl_scheduleb.SelectedValue = "0";
        txt_abc.Text = string.Empty;
        txt_cost.Text = string.Empty;
        txt_totrunhrs.Text = string.Empty;
        txt_notes.Text = string.Empty;
        chkActive.Checked = false;
        RadListBox2.Items.Clear();
        RadListBox3.Items.Clear();
        txtrunmaintenance.Text = "";
        txt_maintenance.Text = "";

    }

    /// <summary>
    /// Gets the latitude, longitude and accuracy for the address information passed in <paramref name="address"/>
    /// </summary>
    /// <param name="address">Street address, city, state, postal code and country of the location for which GIS data is requested</param>
    /// <param name="latLong">Latitude and longitude, separated by a comma, of the address</param>
    /// <param name="latLongAccuracy">Accuracy of the latitude and longitude returned by the function</param>
    /// <returns>True if the location was found or False if not</returns>
    private bool GetGISData(string address, out string latLong, out string latLongAccuracy)
    {
        // Initialize results
        latLong = string.Empty;
        latLongAccuracy = string.Empty;

        // Make request to Google API
        GeoRequest gr = new GeoRequest(address);
        GeoResponse gRes = gr.GetResponse();

        // Were there any results returned?
        if (gRes.Results.Count > 0)
        {
            // Yes, set the returned latitude and longitude
            latLong = gRes.Results[0].Geometry.Location.Latitude.ToString() + "," + gRes.Results[0].Geometry.Location.Longitude.ToString();

            // Get the accuracy  value returned by the Google API call
            string resultLocationMatch = gRes.Results[0].Geometry.LocationType.ToString();

            // Append our definition to the end of the accuracy returned by Google
            string resultAccuracy = "";
            switch (resultLocationMatch)
            {
                // See: http://stackoverflow.com/questions/3015370/how-to-get-the-equivalent-of-the-accuracy-in-google-map-geocoder-v3
                case "ROOFTOP":
                    resultAccuracy = "Precise";
                    break;
                case "RANGE_INTERPOLATED":
                    resultAccuracy = "Approximate";
                    break;
                case "GEOMETRIC_CENTER":
                    resultAccuracy = "Approximate";
                    break;
                case "APPROXIMATE":
                    resultAccuracy = "Approximate";
                    break;
                default:
                    resultAccuracy = resultLocationMatch;
                    break;
            }

            // Set the returned accuracy value
            latLongAccuracy = gRes.Results[0].Types[0].ToString().ToUpper() + " (" + resultAccuracy + ")";

            // Return true for success
            return true;
        }
        else
        {
            return false;
        }

    }

    /// <summary>
    /// Get the data for the meter
    /// </summary>
    /// <returns>A DataTable containing the data from the meter row for the selected meter</returns>
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
            string query = "select *, p.Id,AssetId,PPA.AssetName as AssetName,P.Assetname as asset_id,c.Name as warehouse,ca.clientAssetName as assetcategory,SerialNumber," +
            "Status,p.Cost as DailyCharge,p.runhrmaintenance as RunHrsToMaintenance,p.maintenancepercentage,p.previoususedhrs from Prism_Assets p,PrsimWarehouses c,clientAssets ca,PrismAssetName PPA" +
             " where p.WarehouseId=c.ID and p.AssetCategoryId=ca.clientAssetID and P.AssetName=PPA.Id and p.Id=" + AssetKeyid + "";
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

    /// <summary>
    /// Add a meter to the meter table
    /// </summary>
    /// <returns>True if the add succeeds or False if it fails</returns>
    private bool InsertNewMeter()
    {
        //// Instantiate a DataBase connection
        //SqlConnection sqlConn = new SqlConnection(DBConnectionString);

        //// Create and INSERT statement to add the meter row
        //string insertStmt = "INSERT INTO meter(isActive, meterName, serialNumber, meterIRN, manufacturer, meterType, primaryFirst, primaryLast, primaryLatLong, primaryLatLongAccuracy"
        //                  + ", primaryAddress1, primaryAddress2, primaryCity, primaryState, primaryCountry, primaryPostalCode, primaryPhone1, primaryPhone2, primaryEmail"
        //                  + ", secondaryFirst, bitSecondarySamePrimaryAddress, secondaryLast, secondaryAddress1, secondaryAddress2, secondaryCity, secondaryState, secondaryCountry"
        //                  + ", secondaryPostalCode, secondaryPhone1, secondaryPhone2, secondaryEmail)"
        //                  + " OUTPUT INSERTED.id "
        //                  + " VALUES (1, @MeterName, @SerialNumber, @MeterIRN, @Manufacturer, @MeterType, @PrimaryFirst, @PrimaryLast, @PrimaryLatLong, @PrimaryLatLongAccuracy"
        //                  + ", @PrimaryAddress1, @PrimaryAddress2, @PrimaryCity, @PrimaryState, @PrimaryCountry, @PrimaryPostalCode, @PrimaryPhone1, @PrimaryPhone2, @PrimaryEmail"
        //                  + ", @SecondaryFirst, @SecondarySamePrimaryAddress, @SecondaryLast, @SecondaryAddress1, @SecondaryAddress2, @SecondaryCity, @SecondaryState, @SecondaryCountry"
        //                  + ", @SecondaryPostalCode, @SecondaryPhone1, @SecondaryPhone2, @SecondaryEmail);";
        //try
        //{
        //    // Open the database connection
        //    sqlConn.Open();

        //    // Create a SqlCommand for the INSERT statement
        //    using (SqlCommand sqlCmd = new SqlCommand(insertStmt, sqlConn))
        //    {
        //        // Add parameters to the INSERT for the data needed to create the row
        //        sqlCmd.Parameters.AddWithValue("@MeterName", ddl_assetname.SelectedValue);
        //        sqlCmd.Parameters.AddWithValue("@SerialNumber", radtxtSerialNumber.Text);
        //        sqlCmd.Parameters.AddWithValue("@MeterIRN", radtxtIRNumber.Text);
        //        sqlCmd.Parameters.AddWithValue("@Manufacturer", radtxtMeterMake.Text);
        //        sqlCmd.Parameters.AddWithValue("@MeterType", radtxtType.Text);
        //        sqlCmd.Parameters.AddWithValue("@PrimaryFirst", txtPrimaryFirst.Text);
        //        sqlCmd.Parameters.AddWithValue("@PrimaryLatLong", txtPrimaryLatLong.Text);
        //        sqlCmd.Parameters.AddWithValue("@PrimaryLatLongAccuracy", hdnPrimaryLatLongAccuracy.Value);
        //        sqlCmd.Parameters.AddWithValue("@PrimaryLast", txtPrimaryLast.Text);
        //        sqlCmd.Parameters.AddWithValue("@PrimaryAddress1", txtPrimaryAddress1.Text);
        //        sqlCmd.Parameters.AddWithValue("@PrimaryAddress2", txtPrimaryAddress2.Text);
        //        sqlCmd.Parameters.AddWithValue("@PrimaryCity", txtPrimaryCity.Text);

        //        sqlCmd.Parameters.AddWithValue("@PrimaryCountry", ddlPrimaryCountry.SelectedValue);
        //        if (ddlPrimaryCountry.SelectedValue == "US")
        //        {
        //            sqlCmd.Parameters.AddWithValue("@PrimaryState", ddlPrimaryState.SelectedValue);
        //        }
        //        else
        //        {
        //            sqlCmd.Parameters.AddWithValue("@PrimaryState", txtPrimaryProvince.Text);
        //        }

        //        sqlCmd.Parameters.AddWithValue("@PrimaryPostalCode", txtPrimaryPostalCode.Text);
        //        sqlCmd.Parameters.AddWithValue("@PrimaryPhone1", txtPrimaryPhone1.Text);
        //        sqlCmd.Parameters.AddWithValue("@PrimaryPhone2", txtPrimaryPhone2.Text);
        //        sqlCmd.Parameters.AddWithValue("@PrimaryEmail", txtPrimaryEmail.Text);

        //        sqlCmd.Parameters.AddWithValue("@SecondaryFirst", txtSecondaryFirst.Text);
        //        sqlCmd.Parameters.AddWithValue("@SecondaryLast", txtSecondaryLast.Text);
        //        sqlCmd.Parameters.AddWithValue("@SecondaryAddress1", txtSecondaryAddress1.Text);
        //        sqlCmd.Parameters.AddWithValue("@SecondaryAddress2", txtSecondaryAddress2.Text);
        //        sqlCmd.Parameters.AddWithValue("@SecondaryCity", txtSecondaryCity.Text);

        //        sqlCmd.Parameters.AddWithValue("@SecondaryCountry", ddlSecondaryCountry.SelectedValue);
        //        if (ddlSecondaryCountry.SelectedValue == "US")
        //        {
        //            sqlCmd.Parameters.AddWithValue("@SecondaryState", ddlSecondaryState.SelectedValue);
        //        }
        //        else
        //        {
        //            sqlCmd.Parameters.AddWithValue("@SecondaryState", txtSecondaryProvince.Text);
        //        }

        //        sqlCmd.Parameters.AddWithValue("@SecondaryPostalCode", txtSecondaryPostalCode.Text);
        //        sqlCmd.Parameters.AddWithValue("@SecondaryPhone1", txtSecondaryPhone1.Text);
        //        sqlCmd.Parameters.AddWithValue("@SecondaryPhone2", txtSecondaryPhone2.Text);
        //        sqlCmd.Parameters.AddWithValue("@SecondaryEmail", txtSecondaryEmail.Text);
        //        sqlCmd.Parameters.AddWithValue("@SecondarySamePrimaryAddress", chkSecSameAsPrimAddress.Checked);

        //        // Insert the meter row, saving the returned ID in a local variable
        //        Int32 newId = (Int32)sqlCmd.ExecuteScalar();

        //        // Log meter changes
        //        auditLog logChange = new auditLog(Session["client_database"].ToString());
        //        logChange.addValue(new Dictionary<string, string> { 
        //            { "pageId", pageId },     
        //            { "userId", ((ClientMaster)Session["UserMasterDetails"]).UserID.ToString() }, 
        //            { "attributeName", "meter" }, 
        //            { "description", "meter created" },
        //            { "columnId", newId.ToString() }
        //        }, true);

        //        // Return true to indicate success
        //        return true;
        //    }
        //}
        //catch (Exception ex)
        //{
        //    // Return false to indicate failure
        //    return false;
        //}
        //finally
        //{
        //    sqlConn.Close();
        //}

        // Return false to indicate failure
        return false;
    }

    /// <summary>
    /// Load data in the <paramref name="dt"/> row into the Create/Edit panel fields
    /// </summary>
    /// <param name="dt">Contains the meter row</param>
    /// 
    public bool getCheckBoxStaus(string input)
    {
        if (input == "True")
            return true;
        else
            return false;
    }
    private void LoadSelectedMeterFields(DataTable dt)
    {
        DataTable dt_getassetnamesbycat = SqlHelper.ExecuteDataset(GlobalConnetionString.ClientConnection(), CommandType.Text, "Select * from PrismAssetName where Id=" + dt.Rows[0]["asset_id"].ToString() + "").Tables[0];
        //ddl_assetname
        ddl_assetname.SelectedValue = dt.Rows[0]["asset_id"].ToString();
        ddl_assetname.DataSource = dt_getassetnamesbycat;
        ddl_assetname.DataBind();
           // RadComboBoxFill.FillRadcombobox(ddl_assetname, dt_getassetnamesbycat, "AssetName", "Id", "0");
        
        ddl_warehouse.SelectedValue = dt.Rows[0]["WarehouseId"].ToString();
        ddl_assetcategory.SelectedValue = dt.Rows[0]["AssetCategoryId"].ToString();
        txtAssetNumber.Text = dt.Rows[0]["AssetId"].ToString();
        //ddl_assetname.SelectedValue = dt.Rows[0]["asset_id"].ToString();
        radtxtSerialNumber.Text = dt.Rows[0]["SerialNumber"].ToString();
        radtxtType.Text = dt.Rows[0]["Type"].ToString();
        radtxtMeterMake.Text = dt.Rows[0]["Make"].ToString();
        ddl_partnumber.SelectedValue = dt.Rows[0]["PartNumber"].ToString();
        txt_description.Text = dt.Rows[0]["Description"].ToString();
        ddl_plant.SelectedValue = dt.Rows[0]["Plant"].ToString();
        ddl_responsibleparty.SelectedValue = dt.Rows[0]["ResponsibleParty"].ToString();
        ddl_depreciationtype.SelectedValue = dt.Rows[0]["DepreciationType"].ToString();
        ddl_size.SelectedValue = dt.Rows[0]["Size"].ToString();
        //ddl_status.SelectedValue = dt.Rows[0]["AssetCategoryId"].ToString();
        ddl_owner.SelectedValue = dt.Rows[0]["Owner"].ToString();
        chk_purchasenew.Checked = getCheckBoxStaus(dt.Rows[0]["PuechasedNew"].ToString());
        chk_costadjust.Checked = getCheckBoxStaus(dt.Rows[0]["Costadjust"].ToString());
        chk_depreciate.Checked = getCheckBoxStaus(dt.Rows[0]["Depreciate"].ToString());
        ddl_manufacturer.SelectedValue = dt.Rows[0]["Manufacturer"].ToString();
        ddl_manufacturecountry.SelectedValue = dt.Rows[0]["ManufactureCountry"].ToString();
        if (dt.Rows[0]["ManufractureDate"].ToString() != "1/1/1900 12:00:00 AM")
            date_manufracture.SelectedDate = Convert.ToDateTime(dt.Rows[0]["ManufractureDate"]).Date;
        else
            date_manufracture.SelectedDate = null;

        txt_AFE.Text = dt.Rows[0]["AFE"].ToString();
        txt_altpartnumber.Text = dt.Rows[0]["AltPartNumber"].ToString();
        if (dt.Rows[0]["InserviceDate"].ToString() != "1/1/1900 12:00:00 AM")
            date_inservice.SelectedDate = Convert.ToDateTime(dt.Rows[0]["InserviceDate"]).Date;
        else
            date_inservice.SelectedDate = null;
        if (dt.Rows[0]["CostAdjustDate"].ToString() != "1/1/1900 12:00:00 AM")
            date_costadjusted.SelectedDate = Convert.ToDateTime(dt.Rows[0]["CostAdjustDate"]).Date;
        else
            date_costadjusted.SelectedDate = null;
        if (dt.Rows[0]["RetireDate"].ToString() != "1/1/1900 12:00:00 AM")
            date_retire.SelectedDate = Convert.ToDateTime(dt.Rows[0]["RetireDate"]).Date;
        else
            date_retire.SelectedDate = null;
        ddl_department.SelectedValue = dt.Rows[0]["Department"].ToString();
        ddl_verfiedlocation.SelectedValue = dt.Rows[0]["VerifiedLocation"].ToString();
        if (dt.Rows[0]["VerifiedDate"].ToString() != "1/1/1900 12:00:00 AM")
            date_verified.SelectedDate = Convert.ToDateTime(dt.Rows[0]["VerifiedDate"]);
        txt_months_depreciate.Text = dt.Rows[0]["MonthstoDepreciate"].ToString();
        txt_hourselastservice.Text = dt.Rows[0]["Hoursesincelastservice"].ToString();
        txt_netvalue.Text = dt.Rows[0]["Netvalue"].ToString();
        txt_weightlbs.Text = dt.Rows[0]["Weight_LBS"].ToString();
        txt_weightkgs.Text = dt.Rows[0]["Weight_Kgs"].ToString();
        ddl_scheduleb.SelectedValue = dt.Rows[0]["ScheduleB"].ToString();
        txt_abc.Text = dt.Rows[0]["ABCCode"].ToString();
        chkActive.Checked = getCheckBoxStaus(dt.Rows[0]["Status"].ToString());
        txt_cost.Text = dt.Rows[0]["Cost"].ToString();
        txt_totrunhrs.Text = dt.Rows[0]["previoususedhrs"].ToString();
        txt_notes.Text = dt.Rows[0]["Notes"].ToString();
        txt_maintenance.Text = dt.Rows[0]["maintenancepercentage"].ToString();
        txtrunmaintenance.Text = dt.Rows[0]["runhrmaintenance"].ToString();
        
        //if (dt.Rows[0]["componentstatus"].ToString() == "Yes")
        //{
        //    rd_yes.Checked = true;
        //    compcategory.Attributes.Add("style", "display:block");
        //}
        //else
        //{
        //    rd_no.Checked = false;
        //    compcategory.Attributes.Add("style", "display:none");
        //}
        
        
        //chkActive.Checked = Convert.ToBoolean(dt.Rows[0]["isActive"]);
        //SetStatusAttributes(chkActive.Checked);
        ////radtxtMeterID.Text = dt.Rows[0]["meterID"].ToString();
        //ddl_assetname.SelectedValue = dt.Rows[0]["meterName"].ToString();
        //radtxtIRNumber.Text = dt.Rows[0]["meterIRN"].ToString();
        //radtxtSerialNumber.Text = dt.Rows[0]["serialNumber"].ToString();
        //radtxtType.Text = dt.Rows[0]["meterType"].ToString();
        //radtxtMeterMake.Text = dt.Rows[0]["manufacturer"].ToString();

        //txtPrimaryAddress1.Text = dt.Rows[0]["primaryAddress1"].ToString();
        //txtPrimaryAddress2.Text = dt.Rows[0]["primaryAddress2"].ToString();
        //txtPrimaryCity.Text = dt.Rows[0]["primaryCity"].ToString();

        //string countryCode;
        //if (!Convert.IsDBNull(dt.Rows[0]["primaryCountry"]))
        //{
        //    countryCode = dt.Rows[0]["primaryCountry"].ToString();
        //}
        //else
        //{
        //    countryCode = "US";
        //}

        //SetRadDDLSelectedValue(ddlPrimaryCountry, countryCode);

        //if (countryCode == "US")
        //{
        //    ShowPrimaryStateList(true);
        //    SetRadDDLSelectedValue(ddlPrimaryState, dt.Rows[0]["primaryState"].ToString());
        //}
        //else
        //{
        //    ShowPrimaryStateList(false);
        //    txtPrimaryProvince.Text = dt.Rows[0]["primaryState"].ToString();
        //}

        //txtPrimaryPostalCode.Text = dt.Rows[0]["primaryPostalCode"].ToString();

        //txtPrimaryLatLong.Text = dt.Rows[0]["primaryLatLong"].ToString();
        //if (txtPrimaryLatLong.Text != string.Empty)
        //{
        //    hypPrimaryMapLink.Visible = true;
        //    hypPrimaryMapLink.NavigateUrl = "http://maps.google.com/?q=" + txtPrimaryLatLong.Text;
        //}

        //hdnPrimaryLatLongAccuracy.Value = dt.Rows[0]["primaryLatLongAccuracy"].ToString();

        //txtPrimaryFirst.Text = dt.Rows[0]["primaryFirst"].ToString();
        //txtPrimaryLast.Text = dt.Rows[0]["primaryLast"].ToString();
        //txtPrimaryPhone1.Text = dt.Rows[0]["primaryPhone1"].ToString();
        //txtPrimaryPhone2.Text = dt.Rows[0]["primaryPhone2"].ToString();
        //txtPrimaryEmail.Text = dt.Rows[0]["primaryEmail"].ToString();

        //txtSecondaryAddress1.Text = dt.Rows[0]["secondaryAddress1"].ToString();
        //txtSecondaryAddress2.Text = dt.Rows[0]["secondaryAddress2"].ToString();
        //txtSecondaryCity.Text = dt.Rows[0]["secondaryCity"].ToString();

        //if (!Convert.IsDBNull(dt.Rows[0]["secondaryCountry"]))
        //{
        //    countryCode = dt.Rows[0]["secondaryCountry"].ToString();
        //}
        //else
        //{
        //    countryCode = "US";
        //}

        //SetRadDDLSelectedValue(ddlSecondaryCountry, countryCode);

        //if (countryCode == "US")
        //{
        //    ShowSecondaryStateList(true);
        //    SetRadDDLSelectedValue(ddlSecondaryState, dt.Rows[0]["secondaryState"].ToString());
        //}
        //else
        //{
        //    ShowSecondaryStateList(false);
        //    txtSecondaryProvince.Text = dt.Rows[0]["secondaryState"].ToString();
        //}

        //if (!Convert.IsDBNull(dt.Rows[0]["secondaryCountry"]))
        //{
        //    SetRadDDLSelectedValue(ddlSecondaryCountry, dt.Rows[0]["secondaryCountry"].ToString());
        //}

        //txtSecondaryPostalCode.Text = dt.Rows[0]["secondaryPostalCode"].ToString();

        //if (!Convert.IsDBNull(dt.Rows[0]["bitSecondarySamePrimaryAddress"]))
        //{
        //    chkSecSameAsPrimAddress.Checked = Convert.ToBoolean(dt.Rows[0]["bitSecondarySamePrimaryAddress"]);
        //}

        //txtSecondaryFirst.Text = dt.Rows[0]["secondaryFirst"].ToString();
        //txtSecondaryLast.Text = dt.Rows[0]["secondaryLast"].ToString();
        //txtSecondaryPhone1.Text = dt.Rows[0]["secondaryPhone1"].ToString();
        //txtSecondaryPhone2.Text = dt.Rows[0]["secondaryPhone2"].ToString();
        //txtSecondaryEmail.Text = dt.Rows[0]["secondaryEmail"].ToString();
    }

    /// <summary>
    /// Set the SelectedValue of <paramref name="ddl"/> to <paramref name="selectValue"/>
    /// </summary>
    /// <param name="ddl">The target list</param>
    /// <param name="selectValue">The value to select</param>
    private void SetRadDDLSelectedValue(RadDropDownList ddl, string selectValue)
    {
        try
        {
            // Does the DDL have any items?
            if (ddl.Items.Count > 0)
            {
                // Yes, set the ddl SelectedValue to the passed value
                ddl.SelectedValue = selectValue;
                // Did the set work?
                if (ddl.SelectedValue != selectValue)
                {
                    // No, set the dropdownlist selectedindex to the not selected value
                    ddl.SelectedIndex = -1;
                }
            }
        }
        catch (Exception ex)
        {
        }
    }

    /// <summary>
    /// Load the RADGrid with all the meters in the meter table
    /// </summary>
    private void RefreshMeterList()
    {
        // Instantiate a DataBase connection
        SqlConnection sqlConn = new SqlConnection(DBConnectionString);

        // Instantiate a DataTable to hold the resultset
        DataTable dt = new DataTable();

        try
        {
            // Open the database connection
            sqlConn.Open();
            string query = "select top (10) p.Id,AssetId,PPA.AssetName as AssetName,P.Assetname as asset_id,c.Name as warehouse,ca.clientAssetName as assetcategory,SerialNumber," +
             "Status,p.Cost as DailyCharge,p.runhrmaintenance as RunHrsToMaintenance,p.maintenancepercentage,p.previoususedhrs from Prism_Assets p,PrsimWarehouses c,clientAssets ca,PrismAssetName PPA" +
              " where p.WarehouseId=c.ID and p.AssetCategoryId=ca.clientAssetID and P.AssetName=PPA.Id order by p.Id desc";
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

        // Set the RADGrid's DataSource to the DataTable
        radgrdMeterList.DataSource = dt;

    }

    /// <summary>
    /// Add JavaScript functions to the page
    /// </summary>
    private void RegisterJavaScript()
    {
        // Check to see if the client script is already registered.
        StringBuilder script = new StringBuilder();
        script.Append("<script type=\"text/javascript\">" + Environment.NewLine);
        script.Append(" function ConfirmSave(button, args)" + Environment.NewLine);
        script.Append(" {" + Environment.NewLine);
        script.Append("     if (!Page_IsValid)" + Environment.NewLine);
        script.Append("     {" + Environment.NewLine);
        script.Append("         button.set_autoPostBack(false);" + Environment.NewLine);
        script.Append("     }" + Environment.NewLine);
        script.Append("     else " + Environment.NewLine);
        script.Append("     {" + Environment.NewLine);
        script.Append("         if(!confirm('Click \"OK\" to save your changes'))" + Environment.NewLine);
        script.Append("         {" + Environment.NewLine);
        script.Append("             button.set_autoPostBack(false);" + Environment.NewLine);
        script.Append("         }" + Environment.NewLine);
        script.Append("         else " + Environment.NewLine);
        script.Append("         {" + Environment.NewLine);
        script.Append("             button.set_autoPostBack(true);" + Environment.NewLine);
        script.Append("         }" + Environment.NewLine);
        script.Append("     }" + Environment.NewLine);
        script.Append(" }" + Environment.NewLine);
        script.Append(" </script>");

        // Get the type of this module
        Type csType = this.GetType();

        // Get a ClientScriptManager reference from the Page class.
        ClientScriptManager cs = Page.ClientScript;

        // Check to see if the client script is already registered.
        if (!cs.IsClientScriptBlockRegistered(csType, "ConfirmSave"))
        {
            // Register the script
            cs.RegisterClientScriptBlock(csType, "ConfirSave", script.ToString());
        }

    }

    /// <summary>
    /// Refresh the countries listed in the Primary and Secondary dropdownlists
    /// </summary>
    private void RefreshCountryCodeLists()
    {
        //// Refresh the primary state code list
        //ddlPrimaryCountry.DataBind();
        //SetRadDDLSelectedValue(ddlPrimaryCountry, "US");

        //// Refresh the secondary state code list
        //ddlSecondaryCountry.DataBind();
        //SetRadDDLSelectedValue(ddlSecondaryCountry, "US");
    }

    /// <summary>
    /// Refresh the states listed in the Primary and Secondary drowpdownlists
    /// </summary>
    private void RefreshStateCodeLists()
    {
        //// Refresh the primary state code list
        //ddlPrimaryState.DataBind();
        //ddlPrimaryState.SelectedIndex = -1;

        //// Refresh the secondary state code list
        //ddlSecondaryState.DataBind();
        //ddlSecondaryState.SelectedIndex = -1;
    }

    /// <summary>
    /// Set the attributes of the Status field in the Create/Edit panel
    /// </summary>
    /// <param name="isActive">The Active/In-Active status of the meter</param>
    private void SetStatusAttributes(bool isActive)
    {
        lblActive.Text = GetStatusText(isActive);

        if (!isActive)
        {
            lblActive.Attributes.Add("style", "color: red;");
        }
        else
        {
            lblActive.Attributes.Remove("style");
        }
    }

    /// <summary>
    /// Shows/hides the Primary State dropdownlist and textbox based on the value of <paramref name="visiblity"/>
    /// </summary>
    /// <param name="visibility">True to show the dropdownlist, False to show the textbox</param>
    private void ShowPrimaryStateList(bool visibility)
    {
        //pnlPrimaryUS.Visible = visibility;
        //pnlPrimaryNonUS.Visible = !visibility;
    }


    /// <summary>
    /// Shows/hides the Secondary State dropdownlist and textbox based on the value of <paramref name="visiblity"/>
    /// </summary>
    /// <param name="visibility">True to show the dropdownlist, False to show the textbox</param>
    private void ShowSecondaryStateList(bool visibility)
    {
        //pnlSecondaryUS.Visible = visibility;
        //pnlSecondaryNonUS.Visible = !visibility;
    }

    /// <summary>
    /// Get the text to show in the Status field
    /// </summary>
    /// <param name="isActive">True if the meter is Active, False if not</param>
    /// <returns>Text indicating the status of the meter</returns>
    private string GetStatusText(bool isActive)
    {
        // Set the Status text
        if (isActive)
        {
            return "Active";
        }
        else
        {
            return "In-Active";
        }
    }

    /// <summary>
    /// Pops up a message box on the Windows screen
    /// </summary>
    /// <param name="msg"></param>
    private void PopupNotify(string msg)
    {
        //// Popup a window to show the user the passed message
        //radnotMessage.Title = "Manage Meter Notice";
        //radnotMessage.Text = msg;
        //radnotMessage.Show();
    }

    /// <summary>
    /// Updates the selected meter with the data enter by the user
    /// </summary>
    /// <returns>True if the update success, False if it fails</returns>
    private bool UpdateSelectedMeter()
    {
        //// Instantiate a DataBase connection
        //SqlConnection sqlConn = new SqlConnection(DBConnectionString);

        //// Build an UPDATE statement to update the meter from the user entered data
        //string updateStmt = "UPDATE meter SET isActive = @Active, meterName = @MeterName, serialNumber = @SerialNumber, meterIRN = @MeterIRN, manufacturer = @MeterMake"
        //                  + ", meterType = @MeterType, primaryLatLong = @PrimaryLatLong, primaryLatLongAccuracy = @PrimaryLatLongAccuracy, primaryFirst = @PrimaryFirst, primaryLast = @PrimaryLast"
        //                  + ", primaryAddress1 = @PrimaryAddress1, primaryAddress2 = @PrimaryAddress2, primaryCity = @PrimaryCity, primaryState = @PrimaryState, primaryCountry = @PrimaryCountry"
        //                  + ", primaryPostalCode = @PrimaryPostalCode, primaryPhone1 = @PrimaryPhone1, primaryPhone2 = @PrimaryPhone2, primaryEmail = @PrimaryEmail"
        //                  + ", bitSecondarySamePrimaryAddress = @bitSecSameAsPrimAddress, secondaryFirst = @SecondaryFirst, secondaryLast = @SecondaryLast, secondaryAddress1 = @SecondaryAddress1"
        //                  + ", secondaryAddress2 = @SecondaryAddress2, secondaryCity = @SecondaryCity, secondaryState = @SecondaryState, secondaryCountry = @SecondaryCountry"
        //                  + ", secondaryPostalCode = @SecondaryPostalCode, secondaryPhone1 = @SecondaryPhone1, secondaryPhone2 = @SecondaryPhone2, secondaryEmail = @SecondaryEmail"
        //                  + " WHERE (id = @ID)";


        //try
        //{
        //    // Open the database connection
        //    sqlConn.Open();

        //    // Create a SqlCommand for the UPDATE statement
        //    using (SqlCommand sqlCmd = new SqlCommand(updateStmt, sqlConn))
        //    {

        //        // Log meter changes
        //        auditLog logChange = new auditLog(Session["client_database"].ToString());
        //        logChange.addValue(new Dictionary<string, string> { 
        //            { "pageId", pageId },     
        //            { "userId", ((ClientMaster)Session["UserMasterDetails"]).UserID.ToString() }, 
        //            { "attributeName", "meter was changed.." }, 
        //            { "description", "old values (column name: column value)" },
        //            { "query", "SELECT  meterName, serialNumber, meterIRN, manufacturer, meterType FROM meter WHERE id = '" + hdnSelectedMeterID.Value + "'" },
        //            { "columnId", hdnSelectedMeterID.Value}
        //        }, true);

        //        // Add parameters for the user entere values
        //        sqlCmd.Parameters.AddWithValue("@Active", chkActive.Checked);
        //        sqlCmd.Parameters.AddWithValue("@MeterName", ddl_assetname.SelectedValue);
        //        sqlCmd.Parameters.AddWithValue("@SerialNumber", radtxtSerialNumber.Text);
        //        sqlCmd.Parameters.AddWithValue("@MeterIRN", radtxtIRNumber.Text);
        //        sqlCmd.Parameters.AddWithValue("@MeterMake", radtxtMeterMake.Text);
        //        sqlCmd.Parameters.AddWithValue("@MeterType", radtxtType.Text);
        //        sqlCmd.Parameters.AddWithValue("@PrimaryLatLong", txtPrimaryLatLong.Text);
        //        sqlCmd.Parameters.AddWithValue("@PrimaryLatLongAccuracy", "");
        //        sqlCmd.Parameters.AddWithValue("@PrimaryFirst", txtPrimaryFirst.Text);
        //        sqlCmd.Parameters.AddWithValue("@PrimaryLast", txtPrimaryLast.Text);
        //        sqlCmd.Parameters.AddWithValue("@PrimaryAddress1", txtPrimaryAddress1.Text);
        //        sqlCmd.Parameters.AddWithValue("@PrimaryAddress2", txtPrimaryAddress2.Text);
        //        sqlCmd.Parameters.AddWithValue("@PrimaryCity", txtPrimaryCity.Text);

        //        sqlCmd.Parameters.AddWithValue("@PrimaryCountry", ddlPrimaryCountry.SelectedValue);
        //        if (ddlPrimaryCountry.SelectedValue == "US")
        //        {
        //            sqlCmd.Parameters.AddWithValue("@PrimaryState", ddlPrimaryState.SelectedValue);
        //        }
        //        else
        //        {
        //            sqlCmd.Parameters.AddWithValue("@PrimaryState", txtPrimaryProvince.Text);
        //        }

        //        sqlCmd.Parameters.AddWithValue("@PrimaryPostalCode", txtPrimaryPostalCode.Text);
        //        sqlCmd.Parameters.AddWithValue("@PrimaryPhone1", txtPrimaryPhone1.Text);
        //        sqlCmd.Parameters.AddWithValue("@PrimaryPhone2", txtPrimaryPhone2.Text);
        //        sqlCmd.Parameters.AddWithValue("@PrimaryEmail", txtPrimaryEmail.Text);
        //        sqlCmd.Parameters.AddWithValue("@bitSecSameAsPrimAddress", chkSecSameAsPrimAddress.Checked);
        //        sqlCmd.Parameters.AddWithValue("@SecondaryFirst", txtSecondaryFirst.Text);
        //        sqlCmd.Parameters.AddWithValue("@SecondaryLast", txtSecondaryLast.Text);
        //        sqlCmd.Parameters.AddWithValue("@SecondaryAddress1", txtSecondaryAddress1.Text);
        //        sqlCmd.Parameters.AddWithValue("@SecondaryAddress2", txtSecondaryAddress2.Text);
        //        sqlCmd.Parameters.AddWithValue("@SecondaryCity", txtSecondaryCity.Text);

        //        sqlCmd.Parameters.AddWithValue("@SecondaryCountry", ddlSecondaryCountry.SelectedValue);
        //        if (ddlSecondaryCountry.SelectedValue == "US")
        //        {
        //            sqlCmd.Parameters.AddWithValue("@SecondaryState", ddlSecondaryState.SelectedValue);
        //        }
        //        else
        //        {
        //            sqlCmd.Parameters.AddWithValue("@SecondaryState", txtSecondaryProvince.Text);
        //        }


        //        sqlCmd.Parameters.AddWithValue("@SecondaryPostalCode", txtSecondaryPostalCode.Text);
        //        sqlCmd.Parameters.AddWithValue("@SecondaryPhone1", txtSecondaryPhone1.Text);
        //        sqlCmd.Parameters.AddWithValue("@SecondaryPhone2", txtSecondaryPhone2.Text);
        //        sqlCmd.Parameters.AddWithValue("@SecondaryEmail", txtSecondaryEmail.Text);
        //        sqlCmd.Parameters.AddWithValue("@ID", hdnSelectedMeterID.Value);

        //        // Run the UPDATE statement
        //        int rtnCode = sqlCmd.ExecuteNonQuery();
        //        if (rtnCode > 0)
        //        {
        //            // Return true to indicate sucess
        //            return true;
        //        }
        //    }
        //}
        //catch (Exception ex)
        //{
        //    // Return false to indicate failure
        //    return false;
        //}
        //finally
        //{
        //    if (sqlConn.State == ConnectionState.Open)
        //    {
        //        sqlConn.Close();
        //    }
        //}

        // Return false to indicate failure
        return false;
    }

    /// <summary>
    /// Validate the user entered changes to the meter
    /// </summary>
    /// <returns>True if the changes are valid, False if not</returns>
    private bool ValidEditMeter()
    {
        //Page.Validate();
        //if (!Page.IsValid)
        //{
        //    return false;
        //}

        //// Instantiate a DataBase connection
        //SqlConnection sqlConn = new SqlConnection(DBConnectionString);

        //// Initialise the return value, assume success (true)
        bool rtnValue = true;
        //try
        //{
        //    // Open a the database connection
        //    sqlConn.Open();

        //    try
        //    {
        //        // Build a SELECT statement to see if the meter IRN already exists
        //        using (SqlCommand sqlCmd = new SqlCommand("SELECT COUNT(*) FROM meter WHERE [meterIRN] = @MeterIRN AND [ID] <> @ID", sqlConn))
        //        {
        //            // Add parameters with values to the SELECT
        //            sqlCmd.Parameters.AddWithValue("@ID", hdnSelectedMeterID.Value);
        //            sqlCmd.Parameters.AddWithValue("@MeterIRN", radtxtIRNumber.Text);

        //            // Instantiate an integer variable to hold the returned count and run the SELECT Statement to get its value
        //            int rtn = 0;
        //            rtn = (Int32)sqlCmd.ExecuteScalar();

        //            // If the meter IRN already exists, show an error message and set the return value to 'false' (not valid)
        //            if (rtn != 0)
        //            {
        //                WriteMessage(lblMessage, "This meter IRN number already exists", false);
        //                rtnValue = false;
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        WriteMessage(lblMessage, "Could not validate the Meter IRN.  A database error ocurred.", false);
        //        rtnValue = false;
        //    }

        //    try
        //    {
        //        // Build a SELECT statement to see it the Serial Number already exists
        //        using (SqlCommand sqlCmd = new SqlCommand("SELECT COUNT(*) FROM meter WHERE [serialNumber] = @SerialNumber AND [ID] <> @ID", sqlConn))
        //        {
        //            // Add parameters with values to the SELECT
        //            sqlCmd.Parameters.AddWithValue("@ID", hdnSelectedMeterID.Value);
        //            sqlCmd.Parameters.AddWithValue("@SerialNumber", radtxtSerialNumber.Text);

        //            // Instantiate an integer variable to hold the returned count and run the SELECT Statement to get its value
        //            int rtn = 0;
        //            rtn = (Int32)sqlCmd.ExecuteScalar();

        //            // If the Serial Number already exists, show an error message and set the return value to 'false' (not valid)
        //            if (rtn != 0)
        //            {
        //                WriteMessage(lblMessage, "This Serial Number number already exists", false);
        //                rtnValue = false;
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        WriteMessage(lblMessage, "Could not validate the Serial Number.  A database error ocurred.", false);
        //        rtnValue = false;
        //    }
        //}
        //catch (Exception ex)
        //{
        //    WriteMessage(lblMessage, "Could not validate the new meter.  A database error ocurred.", false);
        //    rtnValue = false;
        //}
        //finally
        //{
        //    // Close the database connection
        //    sqlConn.Close();
        //}

        // Return the rtnValue (true or false)
        return rtnValue;
    }

    /// <summary>
    /// Validate the user entered data for a new meter
    /// </summary>
    /// <returns>True if the new meter data is valid, False if not</returns>
    private bool ValidNewMeter()
    {
        // Fire the page's validator fields
        Page.Validate();

        // Did any validators fail?
        if (!Page.IsValid)
        {
            // Yes, exit returning false (not valid)
            return false;
        }

        // Instantiate a DataBase connection
        SqlConnection sqlConn = new SqlConnection(DBConnectionString);

        // Initialise the return value, assume valid (true)
        bool rtnValue = true;
        //try
        //{
        //    // Open a the database connection
        //    sqlConn.Open();

        //    try
        //    {
        //        // Build a SELECT statement to see if the meter IRN already exists
        //        using (SqlCommand sqlCmd = new SqlCommand("SELECT COUNT(*) FROM meter WHERE [meterIRN] = @MeterIRN", sqlConn))
        //        {
        //            // Add parameters with values to the SELECT
        //            sqlCmd.Parameters.AddWithValue("@MeterIRN", radtxtIRNumber.Text);

        //            // Instantiate an integer variable to hold the returned count and run the SELECT Statement to get its value
        //            int rtn = 0;
        //            rtn = (Int32)sqlCmd.ExecuteScalar();

        //            // If the meter IRN already exists, show an error message and set the return value to 'false' (not valid)
        //            if (rtn != 0)
        //            {
        //                WriteMessage(lblMessage, "This meter IRN number already exists", false);
        //                rtnValue = false;
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        WriteMessage(lblMessage, "Could not validate the Meter IRN.  A database error ocurred.", false);
        //        rtnValue = false;
        //    }

        //    try
        //    {
        //        // Build a SELECT statement to see it the Serial Number already exists
        //        using (SqlCommand sqlCmd = new SqlCommand("SELECT COUNT(*) FROM meter WHERE [serialNumber] = @SerialNumber", sqlConn))
        //        {
        //            // Add parameters with values to the SELECT
        //            sqlCmd.Parameters.AddWithValue("@SerialNumber", radtxtSerialNumber.Text);

        //            // Instantiate an integer variable to hold the returned count and run the SELECT Statement to get its value
        //            int rtn = 0;
        //            rtn = (Int32)sqlCmd.ExecuteScalar();

        //            // If the Serial Number already exists, show an error message and set the return value to 'false' (not valid)
        //            if (rtn != 0)
        //            {
        //                WriteMessage(lblMessage, "This Serial Number number already exists", false);
        //                rtnValue = false;
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        WriteMessage(lblMessage, "Could not validate the Serial Number.  A database error ocurred.", false);
        //        rtnValue = false;
        //    }
        //}
        //catch (Exception ex)
        //{
        //    WriteMessage(lblMessage, "Could not validate the new meter.  A database error ocurred.", false);
        //    rtnValue = false;
        //}
        //finally
        //{
        //    // Close the database connection
        //    sqlConn.Close();
        //}

        // Return the rtnValue (true or false)
        return rtnValue;
    }

    /// <summary>
    /// Add a message to the page's Message area
    /// </summary>
    /// <param name="lbl">The page's message area</param>
    /// <param name="msg">The text of the message</param>
    /// <param name="success">True if the message is not an error, False if it is</param>
    private void WriteMessage(Label lbl, string msg, bool success)
    {
        // Put the passed msg text in the page's error message field
        if (lbl.Text == String.Empty)
        {
            lbl.Text = msg;
        }
        else
        {
            lbl.Text += "<br />" + msg;
        }

        // Set the font color, size and weight of the message based on the passed success flag
        if (success)
        {
            lbl.Attributes.Add("Style", "color: Green; font-size: 10pt; font-weight: bold;");
        }
        else
        {
            lbl.Attributes.Add("Style", "color: Red; font-size: 10pt; font-weight: bold;");

        }
    }

    #endregion

}
