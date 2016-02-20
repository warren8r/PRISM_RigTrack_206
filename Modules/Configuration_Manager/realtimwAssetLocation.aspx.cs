using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Artem.Google.UI;
using System.Data;

using System.Drawing;
using System.Data;
using System.Data.SqlClient;
using Telerik.Web.UI;
using System.Web.Services;
using System.Text;
public partial class Modules_Manage_Events_eventDashboard : System.Web.UI.Page
{
    public DataTable dtMap = new DataTable();
    public static DataTable dt_job = new DataTable();
    public static DataTable dt_warehouse = new DataTable();
    public static DataTable dt_Rig = new DataTable();
    public static DataTable dt_gridwarehouse = new DataTable();
    public DataTable dtMapFirstRow = new DataTable();
    public static DataTable dt_mapWarehouse = new DataTable();
    public static DataTable dt_mapjob = new DataTable();
    public static DataTable dt_comp_assigned = new System.Data.DataTable();
    static string imageurl = "";
    string style = "font-weight:bold;color:#307D7E;";
    protected void Page_Load(object sender, EventArgs e)
    {
        imageurl = Page.ResolveUrl("~") + "images/Prism-Logo1_RGBTP_1.png";
        GoogleMap1.Zoom = 4;

        //until we've settled on this query I'll disable event category and name
        // DropDownList2.Enabled = false;
        // ddlCatNames.Enabled = false;
        //string ss = SqlDataSource1.SelectCommand;
        RadGrid1.Visible = false;
        if (!IsPostBack)
        {
            // GoogleMap1.Visible = false;
            
            RadGrid1.DataSource = jobAssetLocation(getSelectedAssets(), combo_job.SelectedValue, ddl_status.SelectedValue);
            RadGrid1.DataBind();
            string query_warehousemap = "select distinct PAW.ID as warehouse_id,PAW.Name as Warehousename,PA.Id as PrismAssetId," +
                   " PA.AssetId,PAN.Id,clientAssetName,PAN.AssetName,SerialNumber,PAL.AssetStatus,CurrentLocationType,CurrentLocationID,PAW.primaryLatLong,PAL.AssetCurrentLocationID" +
                   "  ,(PAW.primaryAddress1+','+PAW.primaryAddress2+','+PAW.primaryCity+','+PAW.primaryState+','+PAW.primaryCountry+'.') as Address " +
                   " from   Prism_Assets PA,PrismAssetName PAN,clientAssets AC ,PrismAssetCurrentLocation PAL,PrsimWarehouses PAW where" +
                   " PAN.Id=PA.AssetName and   PA.WarehouseId=PAW.ID and AC.clientAssetID=PA.AssetCategoryId and " +
                   " PAL.AssetID=PA.Id and PAN.Id=PA.AssetName";

            DataTable dt_localmapWarehouse = SqlHelper.ExecuteDataset(GlobalConnetionString.ClientConnection(), CommandType.Text, query_warehousemap).Tables[0];
            DataTable dt_localmapjob = SqlHelper.ExecuteDataset(GlobalConnetionString.ClientConnection(), CommandType.Text,
                "select mj.jid,RT.rigtypename  as Rigname,MJ.jobname,PA.AssetId,PA.Id as PrismAssetId," +
                  " PAN.Id,clientAssetName,PAN.AssetName,SerialNumber,PAL.AssetStatus,CurrentLocationType, CurrentLocationID,mj.primaryLatLong,PAL.AssetCurrentLocationID " +
                  " ,(MJ.primaryAddress1+','+MJ.primaryAddress2+', '+MJ.primaryCity+','+MJ.primaryState+','+MJ.primaryCountry+'.') as Address from " +
                  " PrismJobAssignedAssets JAA,manageJobOrders MJ,RigTypes RT,Prism_Assets PA,PrismAssetName PAN,clientAssets AC ,PrismAssetCurrentLocation PAL" +
                  " where MJ.rigtypeid=RT.rigtypeid and MJ.jid=JAA.JobId and PAN.Id=PA.AssetName and  PA.Id=JAA.AssetId and AC.clientAssetID=PA.AssetCategoryId" +
                  " and PAL.AssetID=PA.Id and PAN.Id=PA.AssetName and JAA.AssignmentStatus='Active'").Tables[0];
            dt_mapjob = jobMapDatatable(dt_localmapjob, "J");
            warehouseMapDatatable(dt_localmapWarehouse);
            dt_job = SqlHelper.ExecuteDataset(GlobalConnetionString.ClientConnection(), CommandType.Text, "Select * from manageJobOrders").Tables[0];
            dt_warehouse = SqlHelper.ExecuteDataset(GlobalConnetionString.ClientConnection(), CommandType.Text, "Select * from PrsimWarehouses").Tables[0];
            dt_comp_assigned = SqlHelper.ExecuteDataset(GlobalConnetionString.ClientConnection(), CommandType.Text,
                "select * from PrismJobAssetAssignedComponents PJCA,Prism_ComponentNames PCN,Prism_Components PC" +
                " where PJCA.CompID =PC.CompID and PCN.componet_id=PC.Componentid ").Tables[0];
            btn_view_Click(null, null);
        }

    }
    public DataTable jobMapDatatable(DataTable dtJob_source, string type)
    {
        DataTable dt_localjobmap = new DataTable();
        int max = dtJob_source.Rows.Count - 1;
        DataColumn dcjob;
        dt_localjobmap.Rows.Clear();
        dt_localjobmap.Columns.Clear();

        for (int column = 0; column < dtJob_source.Columns.Count; column++)
        {
            dcjob = new DataColumn();
            dcjob.ColumnName = dtJob_source.Columns[column].ToString();
            dt_localjobmap.Columns.Add(dcjob);
        }
        for (int jobrow = 0; jobrow < dtJob_source.Rows.Count; jobrow++)//int jobrow = dtJob_source.Rows.Count; jobrow > 0; jobrow--) 
        {
            DataRow[] row_asset = null;
            if (type == "J")
            {
                row_asset = dtJob_source.Select("PrismAssetId='" + dtJob_source.Rows[jobrow]["PrismAssetId"].ToString() + "' and jid='" + dtJob_source.Rows[jobrow]["jid"].ToString() + "'");
            }
            else
            {
                row_asset = dtJob_source.Select("PrismAssetId=" + dtJob_source.Rows[jobrow]["PrismAssetId"].ToString());
            }

            if (row_asset.Length > 0)
            {
                DataTable dt_localjob = row_asset.CopyToDataTable();

                switch (row_asset.Length)
                {
                    case 1:
                        {
                            //newCustomersRow = row_asset[0];
                            dt_localjobmap.ImportRow(row_asset[0]);
                            break;
                        }
                    default:
                        {

                            var maxRow = dt_localjob.Select("AssetCurrentLocationID = MAX(AssetCurrentLocationID)");
                            dt_localjobmap.ImportRow(maxRow[0]);
                            break;
                        }
                }
            }
            foreach (var dr in row_asset)
            {
                dtJob_source.Rows.Remove(dr);
            }
            dtJob_source.AcceptChanges();
            jobrow--;
        }
        return dt_localjobmap;
    }
    public void warehouseMapDatatable(DataTable dtwarehouse_source)
    {
        int max = dtwarehouse_source.Rows.Count - 1;
        DataColumn dcwarehouse;
        dt_mapWarehouse.Rows.Clear();
        dt_mapWarehouse.Columns.Clear();

        for (int column = 0; column < dtwarehouse_source.Columns.Count; column++)
        {
            dcwarehouse = new DataColumn();
            dcwarehouse.ColumnName = dtwarehouse_source.Columns[column].ToString();
            dt_mapWarehouse.Columns.Add(dcwarehouse);
        }
        for (int jobrow = 0; jobrow < dtwarehouse_source.Rows.Count; jobrow++)//int jobrow = dtJob_source.Rows.Count; jobrow > 0; jobrow--) 
        {
            DataRow[] row_asset = dtwarehouse_source.Select("PrismAssetId='" + dtwarehouse_source.Rows[jobrow]["PrismAssetId"].ToString() + "' and warehouse_id='" + dtwarehouse_source.Rows[jobrow]["warehouse_id"].ToString() + "'");
            if (row_asset.Length > 0)
            {
                DataTable dt_localjob = row_asset.CopyToDataTable();

                switch (row_asset.Length)
                {
                    case 1:
                        {
                            //newCustomersRow = row_asset[0];
                            dt_mapWarehouse.ImportRow(row_asset[0]);
                            break;
                        }
                    default:
                        {

                            var maxRow = dt_localjob.Select("AssetCurrentLocationID = MAX(AssetCurrentLocationID)");
                            dt_mapWarehouse.ImportRow(maxRow[0]);
                            break;
                        }
                }
            }
            foreach (var dr in row_asset)
            {
                dtwarehouse_source.Rows.Remove(dr);
            }
            dtwarehouse_source.AcceptChanges();
            jobrow--;
        }
    }
    public string getSelectedAssets()
    {
        string strAssets = "", strAssets1 = "";
        for (int asset = 0; asset < combo_assets.Items.Count; asset++)
        {
            if (combo_assets.Items[asset].Checked)
            {
                strAssets1 += combo_assets.Items[asset].Value + ",";
            }
        }
        if (strAssets1 != "")
        {
            strAssets = strAssets1.Remove(strAssets1.Length - 1, 1);
        }
        else
        {
            strAssets = "";
        }
        return strAssets;
    }
    public DataTable jobAssetLocation(string assets, string jobid, string status)
    {
        DataTable dt_JobLocation = new DataTable();
        // string query = " select MJ.jobname,MJ.jid,JA.id as AssetAssignid,MJ.jobid,A.AssetId,PA.AssetName,SerialNumber,clientAssetName,WA.Name as Warehouse," +
        //     " (u.firstName+' '+u.lastName) as username,JA.ModifiedDate,JAS.StatusText as AssetStatus,JAS.StatusText as StatusText,JA.AssetStatus as StatusId," +
        //     " WA.primaryLatLong  as WarehouseGIS,MJ.primaryLatLong as JoborderGIS,GIs =  CASE JA.AssetStatus  WHEN '1'  THEN WA.primaryLatLong" +
        //      " WHEN '2' THEN WA.primaryLatLong     WHEN '3' THEN MJ.primaryLatLong END from Prism_Assets A,PrismJobAssignedAssets JA,PrsimWarehouses WA,clientAssets AT," +
        //      " Users u,manageJobOrders MJ,PrsimJobAssetStatus JAS,PrismAssetName PA where  AT.clientAssetID=A.AssetCategoryId and A.Id=JA.AssetId and WA.ID=A.WarehouseId  and  " +
        //      "MJ.jid=JA.JobId and  JA.ModifiedBy=u.userID and JA.AssetStatus=JAS.Id  and A.AssetName=PA.ID ";

        // if (jobid != "" && jobid != "0")
        // {
        //     query += " and MJ.jid=" + jobid + "";
        // }
        // if (assets != "" && assets != "0")
        // {
        //     query += " and A.Id in (" + assets + ")";
        // }
        // if (status != ""&& status != "0")
        // {
        //     query += " and JA.AssetStatus=" + status + "";
        // }
        // dt_JobLocation = SqlHelper.ExecuteDataset(GlobalConnetionString.ClientConnection(), CommandType.Text, query).Tables[0];
        // int TotalCount = dt_JobLocation.Rows.Count;
        ////bindGoogleMap(dt_JobLocation);
        // if (TotalCount == 0)
        // {
        //     norecords.Visible = true;
        // }
        // else
        // {
        //     norecords.Visible = false;
        // }

        // lblCount.Text = TotalCount.ToString();
        // dtMap = dt_JobLocation;
        return dt_JobLocation;


    }
    protected void btnFilterGIS_Click(object sender, EventArgs e)
    {
        RadGrid1.DataSource = jobAssetLocation(getSelectedAssets(), combo_job.SelectedValue, ddl_status.SelectedValue);
        RadGrid1.DataBind();
    }
    public string GetLabelText(object dataItem)
    {
        string text = "";
        //int? val = dataItem as int?;

        switch (dataItem.ToString())
        {
            case "Available":
                text = "Pending&#160;Shipmet/In-Transit";
                break;
            case "In Use":
                text = "On&#160;Job";
                break;

        }
        return text;
    }
    protected void RadGrid1_ItemDataBound(object sender, GridItemEventArgs e)
    {
        //// RadGrid grid = (RadGrid)sender;
        // if ((e.Item.ItemType == GridItemType.Item) || (e.Item.ItemType == GridItemType.AlternatingItem))
        // {

        //     GridDataItem dataItem = e.Item as GridDataItem;
        //     GridColumn column = RadGrid1.MasterTableView.GetColumn("GIs");
        //    // Label lbl_AssetStatus = (Label)dataItem.FindControl("lbl_AssetStatus");
        //     Label lbl_jobid = (Label)dataItem.FindControl("lbl_jobid");
        //     Label lbl_AssetId = (Label)dataItem.FindControl("lbl_AssetId");
        //     Label lbl_AssetName = (Label)dataItem.FindControl("lbl_AssetName");
        //     Label lbl_SerialNumber = (Label)dataItem.FindControl("lbl_SerialNumber");
        //     Label lbl_jobname = (Label)dataItem.FindControl("lbl_jobname");

        //     if (dataItem["GIs"].Text != string.Empty
        //        && dataItem["GIs"].Text != "&nbsp;")
        //     {
        //         //instantiate new user controls per alert..
        //         string s = dataItem["GIs"].Text;
        //         string[] words = s.Split(',');
        //         GoogleMap1.Latitude = Convert.ToDouble(words[0]);
        //         GoogleMap1.Longitude = Convert.ToDouble(words[1]);
        //         //GoogleMap1.Center = new LatLng(Convert.ToDouble(words[0]) + "," + Convert.ToDouble(words[1]));
        //         Marker marker2 = new Marker();                
        //         string flagName = dataItem["StatusId"].Text;
        //         // string  str="<table border='1' class='gis-form'><tr><td colspan='6' align='center' style='font-weight:bold;size:15px'>PRISM</td></tr>";
        //         string str = "<table width='100%' border='1'>";
        //         str += "<tr class='logo'> <td colspan='7'><table border='0' cellpadding='0' cellspacing='0' width='100%' class='logo'>" +
        //         "<tr><td align='left' style='padding-left: 10px; width:80px' ><b style='color: White; font-size: 25px'>" +
        //           "<img src='" + imageurl + "'/></b>" +
        //           "</td><td style='color:Red; font-size:13px; font-weight:bold' align='left' >Project Resource Information & Sales Management</td></tr></table> </td></tr>" +
        //             //Start Header Row
        //           "<tr><td style='font-weight:bold'>Job Name</td><td style='font-weight:bold'>Job ID</td><td style='font-weight:bold'>Asset Id</td>" +
        //           "<td style='font-weight:bold'>Asset Name</td><td style='font-weight:bold'>Serial #</td><td style='font-weight:bold'>Asset Status</td>" +
        //           "<td style='font-weight:bold'>Asset Location</td></tr>";
        //             //Close Header Row
        //         DataRow[] row_map = dtMap.Select("jobname='" + lbl_jobname.Text+"'");
        //         for (int asset = 0; asset < row_map.Length; asset++)
        //         {
        //             //str += "<tr><td>" + lbl_jobname.Text + "</td><td>" + lbl_jobid.Text + "</td>" +
        //             // "<td>" + dataItem["AssetId"].Text + "</td><td>" + lbl_AssetName.Text + "</td>" +
        //             // "<td>" + lbl_SerialNumber.Text + "</td><td>" + lbl_AssetStatus.Text + "</td>";
        //             if (lbl_SerialNumber.Text == row_map[asset]["SerialNumber"].ToString())
        //             {
        //                 str += "<tr style='background-color:#99FF66'>";
        //             }
        //             str += "<td>" + row_map[asset]["jobname"].ToString() + "</td><td>" + row_map[asset]["jobid"].ToString() + "</td>" +
        //             "<td>" + row_map[asset]["AssetId"].ToString() + "</td><td>" + row_map[asset]["AssetName"].ToString() + "</td>" +
        //             "<td>" + row_map[asset]["SerialNumber"].ToString() + "</td><td>" + row_map[asset]["AssetStatus"].ToString() + "</td>";

        //             switch (row_map[asset]["StatusId"].ToString())
        //             {
        //                 case "1":
        //                 case "2":
        //                     {
        //                         str += "<td>Warehouse</td></tr>";
        //                         break;
        //                     }
        //                 case "3":
        //                     {
        //                         str += "<td>Job</td></tr>";
        //                         break;
        //                     }
        //             }
        //         }
        //         marker2.Info = str+"</table>";
        //         marker2.Position.Latitude = Convert.ToDouble(words[0]);
        //         marker2.Position.Longitude = Convert.ToDouble(words[1]);


        //         marker2.Title = "PRSIM JOB:" + lbl_jobname.Text + " and Asset Location ";// +e.Row.Cells[3].Text + '-' + e.Row.Cells[24].Text + '-' + e.Row.Cells[2].Text + " Alert";
        //         try
        //         {

        //             switch (flagName)
        //             {
        //                 case "1":
        //                     {
        //                         marker2.Icon = "/alertmarker.aspx?color=CC0033";

        //                         break;
        //                     }
        //                 case "3":
        //                     {
        //                         marker2.Icon = "/alertmarker.aspx?color=66FF33";

        //                         break;
        //                     }
        //                 case "2":
        //                     {
        //                         marker2.Icon = "/alertmarker.aspx?color=FF6600";

        //                         break;
        //                     }

        //             }

        //         }
        //         catch (Exception ex)
        //         {

        //             // throw;
        //         }


        //         marker2.Animation = MarkerAnimation.Drop;
        //         marker2.Clickable = true;

        //        // GoogleMarker.Markers.Add(marker2);
        //     }
        // }
    }
    protected void RadGrid1_PageIndexChanged(object sender, Telerik.Web.UI.GridPageChangedEventArgs e)
    {
        RadGrid1.DataSource = jobAssetLocation(getSelectedAssets(), combo_job.SelectedValue, ddl_status.SelectedValue);
        RadGrid1.DataBind();
    }
    protected void RadGrid1_GroupsChanging(object sender, Telerik.Web.UI.GridGroupsChangingEventArgs e)
    {
        RadGrid1.DataSource = jobAssetLocation(getSelectedAssets(), combo_job.SelectedValue, ddl_status.SelectedValue);
        RadGrid1.DataBind();
    }
    protected void RadGrid1_SortCommand(object sender, Telerik.Web.UI.GridSortCommandEventArgs e)
    {
        RadGrid1.DataSource = jobAssetLocation(getSelectedAssets(), combo_job.SelectedValue, ddl_status.SelectedValue);
        RadGrid1.DataBind();
    }
    protected void radcombo_assetcat_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
    {
        string aseetcategoryids = "";
        try
        {
            string query = "select DISTINCT PA.Id,PA.AssetName as  [AssetName] from Prism_Assets P,PrismAssetName PA  where  P.AssetName=PA.ID ";
            if (radcombo_assetcat.SelectedValue != "0" && radcombo_assetcat.Items.Count > 0)
            {
                foreach (RadComboBoxItem radcbiSource in radcombo_assetcat.CheckedItems)
                {
                    aseetcategoryids += radcbiSource.Value + ",";
                }
                aseetcategoryids = aseetcategoryids.Remove(aseetcategoryids.LastIndexOf(","), 1);
                query += " And p.AssetCategoryId in(" + aseetcategoryids + ")";
            }
            combo_assetsTop.DataSource = SqlHelper.ExecuteDataset(GlobalConnetionString.ClientConnection(), CommandType.Text, query).Tables[0];
            combo_assetsTop.DataBind();
        }
        catch (Exception ex)
        {

        }
    }
    protected void combo_assetsTop_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
    {
        string aseetids = "";
        string query = "select DISTINCT P.Id,P.SerialNumber as SerialNumber from Prism_Assets P,PrismAssetName PA  where  P.AssetName=PA.ID ";
        if (combo_assetsTop.SelectedValue != "0" && combo_assetsTop.Items.Count > 0)
        {
            foreach (RadComboBoxItem radcbiSource in combo_assetsTop.CheckedItems)
            {
                aseetids += radcbiSource.Value + ",";
            }
            if (aseetids != "")
            {
                aseetids = aseetids.Remove(aseetids.LastIndexOf(","), 1);
                query += "   AND  PA.ID in(" + aseetids + ")";
            }

        }
        COMBO_SERIALNUMBER.DataSource = SqlHelper.ExecuteDataset(GlobalConnetionString.ClientConnection(), CommandType.Text, query).Tables[0];
        COMBO_SERIALNUMBER.DataBind();

    }
    protected void combo_Rig_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
    {
    }
    public string getAssetStatus(string input)
    {
        string output = "";
        switch (input)
        {
            case "Available":
                {
                    output = "Pending&#160;Shipmet/In-Transit";
                    break;
                }
            default:
                {
                    output = "On&#160;Job";
                    break;
                }
        }

        return output;
    }
    //public string getHTML_new()
    //{
    //    string assetid="";
    //    string str = "<table width='100%' border='1'>";
    //    str += "<tr><td style='" + style + "'>Asset&#160;Name</td><td style='" + style + "'>Serial&#160;#</td><td style='" + style + "'>Asset&#160;Location</td></tr>";
    //    //Close Header Row
    //    if(lbl_assetids.Text!="")
    //    {
    //        assetid=lbl_assetids.Text.Remove(lbl_assetids.Text.Length-1,1);
    //        string[] arrAssetId = assetid.Split(',');
    //        DataRow[] row_map = dtMapFirstRow.Select("PrismAssetId=" + assetid);
    //        for (int asset = 0; asset < row_map.Length; asset++)
    //        {
    //            str += "<tr><td>" + row_map[asset]["AssetName"].ToString() + "</td><td>" + row_map[asset]["SerialNumber"].ToString() + "</td><td>" + row_map[asset]["Address"].ToString() + "</td></tr>";
    //        }
    //    }

    //    str += "</table>";
    //    return str;
    //}
    [WebMethod]
    public string getHTML_new_samepath(string assetids)
    {
        string assetid = "";
        string str = "<table width='100%' border='1'>";
        str += "<tr><td style='" + style + "'>Asset&#160;Name</td><td style='" + style + "'>Serial&#160;#</td><td style='" + style + "'>Asset&#160;Location</td></tr>";
        //Close Header Row
        if (assetids != "")
        {
            assetid = assetids.Remove(assetids.Length - 1, 1);
            string[] arrAssetId = assetid.Split(',');
            DataRow[] row_map = dtMapFirstRow.Select("PrismAssetId=" + assetid);
            for (int asset = 0; asset < row_map.Length; asset++)
            {
                str += "<tr><td>" + row_map[asset]["AssetName"].ToString() + "</td><td>" + row_map[asset]["SerialNumber"].ToString() + "</td><td>" + row_map[asset]["Address"].ToString() + "</td></tr>";
            }
        }

        str += "</table>";
        return str;
    }
    protected void grid_1st_ItemDataBound(object sender, GridItemEventArgs e)
    {
        // RadGrid grid = (RadGrid)sender;
        if ((e.Item.ItemType == GridItemType.Item) || (e.Item.ItemType == GridItemType.AlternatingItem))
        {
            string GIS = "", Title = "";
            GridDataItem dataItem = e.Item as GridDataItem;
            //  GridColumn column = RadGrid1.MasterTableView.GetColumn("GIs");
            Label lbl_lbl_CurrentLocationType = (Label)dataItem.FindControl("lbl_CurrentLocationType");
            //dataItem["CurrentLocationID"].Text;
            Marker marker2 = new Marker();
            StringBuilder currLocAddress = new StringBuilder();
            //string currLocAddress = "";
            if (dataItem["CurrentLocationType"].Text == "Job")
            {
                DataRow[] row_JOB = dt_job.Select("jid=" + dataItem["CurrentLocationID"].Text);
                GIS = row_JOB[0]["primaryLatLong"].ToString();
                marker2.Icon = "../../images/pin2.png";
                Title = row_JOB[0]["jobname"].ToString();
                currLocAddress.Append("Job Location:<br> Name:" + row_JOB[0]["primaryFirst"].ToString() + " " + row_JOB[0]["primaryLast"].ToString() + "<br>Address:" + row_JOB[0]["primaryAddress1"].ToString() + " " + row_JOB[0]["primaryAddress2"].ToString() + "<br>City:" + row_JOB[0]["primaryCity"].ToString() + "<br>State:" + row_JOB[0]["primaryState"].ToString() + "<br>Country:" + row_JOB[0]["primaryCountry"].ToString() + "<br>PostalCode:" + row_JOB[0]["primaryPostalCode"].ToString());
            }
            else
            {
                DataRow[] row_warehouse = dt_warehouse.Select("ID=" + dataItem["CurrentLocationID"].Text);
                GIS = row_warehouse[0]["primaryLatLong"].ToString();
                marker2.Icon = "../../images/pin1.png";

                Title = row_warehouse[0]["Name"].ToString();
                currLocAddress.Append("Warehouse Location: <br> Name:" + row_warehouse[0]["primaryFirst"].ToString() + " " + row_warehouse[0]["primaryLast"].ToString() + "<br>Address:" + row_warehouse[0]["primaryAddress1"].ToString() + " " + row_warehouse[0]["primaryAddress2"].ToString() + "<br>City:" + row_warehouse[0]["primaryCity"].ToString() + "<br>State:" + row_warehouse[0]["primaryState"].ToString() + "<br>Country:" + row_warehouse[0]["primaryCountry"].ToString() + "<br>PostalCode:" + row_warehouse[0]["primaryPostalCode"].ToString());
            }
            if (GIS != string.Empty
              && GIS != "&nbsp;")
            {
                //instantiate new user controls per alert..
                string s = GIS;
                string[] words = s.Split(',');
                GoogleMap1.Latitude = Convert.ToDouble(words[0]);
                GoogleMap1.Longitude = Convert.ToDouble(words[1]);
                //GoogleMap1.Center = new LatLng(Convert.ToDouble(words[0]) + "," + Convert.ToDouble(words[1]));
                // string style = "font-weight:bold;color:#307D7E;";
                //string flagName = dataItem["StatusId"].Text;
                // string  str="<table border='1' class='gis-form'><tr><td colspan='6' align='center' style='font-weight:bold;size:15px'>PRISM</td></tr>";
                string str = "<table width='100%' border='1'>";
                str += "<tr><td colspan='8' algn='center'><input  title='Select assets to relocate (or) Move selected assets to this location' style='background-color:green;color:white;cursor:pointer;' type='button' value='Select assets to relocate (or) Move selected assets to this location' onclick='javascript:return movetoassetloc(this," + dataItem["CurrentLocationID"].Text + "," + "\"" + dataItem["CurrentLocationType"].Text + "\"" + "," + "\"" + currLocAddress + "\"" + "); return false;' border='0' style='background-color:#acbfce;font-family:verdana;border:solid 1px #000000;color:white'/></td></tr>";
                str += "<tr><td colspan='8' style='height:5px'></td></tr>";
                //Start Header Row
                str += "<tr><td style='" + style + "'>Select</td><td style='" + style + "'>Asset&#160;Id</td><td style='" + style + "'>Asset&#160;Category</td>" +
                  "<td style='" + style + "'>Asset&#160;Name</td><td style='" + style + "'>Serial&#160;#</td>" +
                " <td style='" + style + "'>Asset&#160;Status</td>" +
                  "<td style='" + style + "'>Job&#160;Name</td><td style='" + style + "'>Asset&#160;Location</td></tr>";
                //Close Header Row

                


                DataRow[] row_map = dtMapFirstRow.Select("CurrentLocationID='" + dataItem["CurrentLocationID"].Text + "' and CurrentLocationType='" + dataItem["CurrentLocationType"].Text + "'");
                if (row_map.Length > 0)
                {
                    for (int asset = 0; asset < row_map.Length; asset++)
                    {
                        string[] arrinfo = null;
                        string arr1 = "", arr2 = "";
                        arrinfo = JobAssetStatus(row_map, asset);
                        if (arrinfo[0] != null)
                        {
                            arr1 = arrinfo[0].ToString();
                        }
                        else
                        {
                            arr1 = "";
                        }
                        if (arrinfo[1] != null)
                        {
                            arr2 = arrinfo[1].ToString();
                        }
                        else
                        {
                            arr2 = "";
                        }
                        //if (lbl_assetids.Text == "")
                        //{
                        //row_map[asset]["ClientAssetName"].ToString()," + row_map[asset]["SerialNumber"].ToString() + "," + row_map[asset]["Address"].ToString() + "
                        str += "<tr ><td><input type='checkbox' name='ASSET' onclick='javascript:return getAssetIDS(this); return false;' value='" + row_map[asset]["PrismAssetId"].ToString() + "'></td>" +
                        " <td>" + row_map[asset]["AssetId"].ToString() + "</td><td>" + row_map[asset]["ClientAssetName"].ToString() + "</td><td>" + row_map[asset]["AssetName"].ToString() + "</td>" +
                    "<td>" + row_map[asset]["SerialNumber"].ToString() + "</td><td>" + arr2 + "</td><td>" + arr1 + "</td>" +
                        "<td>" + dataItem["CurrentLocationType"].Text + "(" + row_map[asset]["Address"].ToString() + ")</td></tr>";
                        //}
                        //else
                        //{
                        //    string[] assetcontainids = lbl_assetids.Text.Split(',');
                        //    for (int i = 0; i < assetcontainids.Length; i++)
                        //    {
                        //        if (row_map[asset]["PrismAssetId"].ToString() == assetcontainids[i].ToString())
                        //        {
                        //            str += "<tr ><td><input type='checkbox' checked='true' name='ASSET' onclick='javascript:return getAssetIDS(this); return false;' value='" + row_map[asset]["PrismAssetId"].ToString() + "'></td>" +
                        //    " <td>" + row_map[asset]["AssetId"].ToString() + "</td><td>" + row_map[asset]["ClientAssetName"].ToString() + "</td><td>" + row_map[asset]["AssetName"].ToString() + "</td>" +
                        //"<td>" + row_map[asset]["SerialNumber"].ToString() + "</td><td>" + arrinfo[1].ToString() + "</td><td>" + arrinfo[0].ToString() + "</td>" +
                        //    "<td>" + dataItem["CurrentLocationType"].Text + "(" + row_map[asset]["Address"].ToString() + ")</td></tr>";
                        //        }
                        //        else
                        //        {
                        //            str += "<tr ><td><input type='checkbox' name='ASSET' checked='false' onclick='javascript:return getAssetIDS(this); return false;' value='" + row_map[asset]["PrismAssetId"].ToString() + "'></td>" +
                        //    " <td>" + row_map[asset]["AssetId"].ToString() + "</td><td>" + row_map[asset]["ClientAssetName"].ToString() + "</td><td>" + row_map[asset]["AssetName"].ToString() + "</td>" +
                        //"<td>" + row_map[asset]["SerialNumber"].ToString() + "</td><td>" + arrinfo[1].ToString() + "</td><td>" + arrinfo[0].ToString() + "</td>" +
                        //    "<td>" + dataItem["CurrentLocationType"].Text + "(" + row_map[asset]["Address"].ToString() + ")</td></tr>";
                        //        }
                        //    }
                        //}
                    }
                }
                else
                {
                    str += "<tr ><td colspan='7' align='center'> <b><span style color:red;>No Assets Found</style></b></td></tr>";
                }
                //getAssetStatus(row_map[asset]["AssetStatus"].ToString()) 
                marker2.Info = str + "</table>";
                marker2.Position.Latitude = Convert.ToDouble(words[0]);
                marker2.Position.Longitude = Convert.ToDouble(words[1]);


                marker2.Title = dataItem["CurrentLocationType"].Text + ":" + Title;// +e.Row.Cells[3].Text + '-' + e.Row.Cells[24].Text + '-' + e.Row.Cells[2].Text + " Alert";
                // marker2.Icon = "/alertmarker.aspx?color=66FF33";

                marker2.Animation = MarkerAnimation.Drop;
                marker2.Clickable = true;
                marker2.RaiseOnDrag = true;
                GoogleMarker.Width = Unit.Pixel(960);

                GoogleMarker.Markers.Add(marker2);





            }
        }
    }
    public string[] JobAssetStatus(DataRow[] row, int location)
    {
        string[] arrinfo = new string[2];// null;
        DataRow[] rowjob = null;
        if (row[location]["CurrentLocationType"].ToString() == "Job")
        {
            rowjob = dt_job.Select("jid=" + row[location]["CurrentLocationID"].ToString());
            if (rowjob.Length > 0)
            {
                arrinfo[0] = rowjob[0]["jobname"].ToString() + "(" + rowjob[0]["jobid"].ToString() + ")";

            }
            else
            {
                arrinfo[0] = "";

            }
            arrinfo[1] = "On&#160;Job";
        }
        else if (row[location]["ToLocationType"].ToString() == "WareHouse" && row[location]["CurrentLocationType"].ToString() == "WareHouse")
        {
            //rowjob = dt_job.Select("jid=" + row[location]["ToLocationID"].ToString());
            //if (rowjob.Length > 0)
            //{
            //    arrinfo[0] = rowjob[0]["jobname"].ToString() + "(" + rowjob[0]["jobid"].ToString() + ")";

            //}
            //else
            //{
            arrinfo[0] = "";

            //}
            arrinfo[1] = "Available";
        }
        else if (row[location]["ToLocationType"].ToString() == "Job" && row[location]["CurrentLocationType"].ToString() == "WareHouse")
        {
            rowjob = dt_job.Select("jid=" + row[location]["ToLocationID"].ToString());
            if (rowjob.Length > 0)
            {
                arrinfo[0] = rowjob[0]["jobname"].ToString() + "(" + rowjob[0]["jobid"].ToString() + ")";

            }
            else
            {
                arrinfo[0] = "";

            }
            arrinfo[1] = "Pending&#160;Shipmet/In-Transit";
        }
        //if (row[location]["ToLocationType"].ToString() != row[location]["CurrentLocationType"].ToString() && row[location]["CurrentLocationType"].ToString() == "WareHouse")
        //{
        //    rowjob = dt_job.Select("jid=" + row[location]["ToLocationID"].ToString());
        //    arrinfo[0] = rowjob[0]["jobname"].ToString() + "(" + rowjob[0]["jobid"].ToString() + ")";
        //    arrinfo[1] = "Pending&#160;Shipmet/In-Transit";
        //}
        //else
        //{
        //    if (row[location]["ToLocationType"].ToString() != row[location]["CurrentLocationType"].ToString())
        //    {
        //        rowjob = dt_job.Select("jid=" + row[location]["CurrentLocationID"].ToString());
        //        if (rowjob.Length > 0)
        //        {
        //            arrinfo[0] = rowjob[0]["jobname"].ToString() + "(" + rowjob[0]["jobid"].ToString() + ")";

        //        }
        //        else
        //        {
        //            arrinfo[0] = "";

        //        }
        //        arrinfo[1] = "Available";
        //    }
        //    else
        //    {
        //        rowjob = dt_job.Select("jid=" + row[location]["CurrentLocationID"].ToString());
        //        if (rowjob.Length > 0)
        //        {
        //            arrinfo[0] = rowjob[0]["jobname"].ToString() + "(" + rowjob[0]["jobid"].ToString() + ")";
        //            arrinfo[1] = "On&#160;Job";
        //        }
        //        else
        //        {
        //            arrinfo[0] = "";
        //            arrinfo[1] = "Available";
        //        }

        //    }

        //}
        return arrinfo;
    }
    public DataTable jobFirstRowFilterAssetLocation(string assetcategories, string assetnames, string serailnumbers, bool serialno, bool assetstatus)
    {
        DataTable dt_JobLocation;
        string query = "  select AssetCurrentLocationID,PA.Id as PrismAssetId, PA.AssetId,PAN.Id,clientAssetName,PAN.AssetName,SerialNumber," +
            "(primaryAddress1+','+primaryAddress2+','+primaryCity+','+primaryState+','+primaryCountry+'.') as Address," +
            "PAL.AssetStatus,ToLocationType,ToLocationID,CurrentLocationType,CurrentLocationID" +
                " from PrismAssetCurrentLocation PAL,Prism_Assets PA,PrsimWarehouses WA,PrismAssetName PAN,clientAssets AC where " +
               " PAL.AssetID=PA.Id and PAN.Id=PA.AssetName AND PA.WarehouseId=WA.ID  AND AC.clientAssetID=PA.AssetCategoryId";

        if (assetcategories != "" && assetcategories != "0")
        {
            query += " and PA.AssetCategoryId in (" + assetcategories + ")";
        }
        if (assetnames != "" && assetnames != "0")
        {
            query += " and PA.AssetName in  (" + assetnames + ")";
        }
        if (serailnumbers != "" && serailnumbers != "0")
        {
            query += " and PA.id in(" + serailnumbers + ")";
        }
        if (serialno)
        {
            query += "  and  PA.SerialNumber  like  '%" + txt_assetseralno.Text.TrimEnd().TrimStart() + "%'";
        }
        if (assetstatus)
        {
            query += " and PAL.AssetStatus='" + combo_assetstatus.SelectedValue + "'";
        }
        dt_JobLocation = SqlHelper.ExecuteDataset(GlobalConnetionString.ClientConnection(), CommandType.Text, query).Tables[0];
        int TotalCount = dt_JobLocation.Rows.Count;
        //bindGoogleMap(dt_JobLocation);
        if (TotalCount == 0)
        {
            norecords.Visible = true;
        }
        else
        {
            norecords.Visible = false;
        }

        //lblCount.Text = TotalCount.ToString();

        dtMapFirstRow = jobMapDatatable(dt_JobLocation, "C");
        return dtMapFirstRow;// dt_JobLocation;


    }
    public void clearSelectedItemsfromComboBoxes(RadComboBox source)
    {
        foreach (RadComboBoxItem radcbiSource in source.CheckedItems) { radcbiSource.Checked = false; }
    }
    protected void btn_Reset_Top_Click(object sender, EventArgs e)
    {
        clearSelectedItemsfromComboBoxes(radcombo_assetcat);
        combo_assetsTop.Items.Clear();
        COMBO_SERIALNUMBER.Items.Clear();
        //clearSelectedItemsfromComboBoxes(combo_assetsTop);
        //clearSelectedItemsfromComboBoxes(COMBO_SERIALNUMBER);
        clearSelectedItemsfromComboBoxes(combo_Rig);
        clearSelectedItemsfromComboBoxes(combo_jobstop);
        clearSelectedItemsfromComboBoxes(combo_warehouse);
        txt_assetseralno.Text = "";
        combo_assetstatus.SelectedIndex = 0;
        DataTable dt_localmapWarehouse = SqlHelper.ExecuteDataset(GlobalConnetionString.ClientConnection(), CommandType.Text,
              "select distinct PAW.ID as warehouse_id,PAW.Name as Warehousename,MJ.jobname,PA.Id as PrismAssetId," +
                 " PA.AssetId,PAN.Id,clientAssetName,PAN.AssetName,SerialNumber,PAL.AssetStatus,CurrentLocationType,CurrentLocationID,PAW.primaryLatLong,PAL.AssetCurrentLocationID" +
                 "  ,(MJ.primaryAddress1+','+MJ.primaryAddress2+','+MJ.primaryCity+','+MJ.primaryState+','+MJ.primaryCountry+'.') as Address " +
                 " from    PrismJobAssignedAssets JAA," +
                 " manageJobOrders MJ,Prism_Assets PA,PrismAssetName PAN,clientAssets AC ,PrismAssetCurrentLocation PAL,PrsimWarehouses PAW where" +
                 " MJ.jid=JAA.JobId and PAN.Id=PA.AssetName and  PA.Id=JAA.AssetId and  PA.WarehouseId=PAW.ID and AC.clientAssetID=PA.AssetCategoryId and " +
                 " PAL.AssetID=PA.Id and PAN.Id=PA.AssetName ").Tables[0];
        DataTable dt_localmapjob = SqlHelper.ExecuteDataset(GlobalConnetionString.ClientConnection(), CommandType.Text,
            "select mj.jid,RT.rigtypename  as Rigname,MJ.jobname,PA.AssetId,PA.Id as PrismAssetId," +
              " PAN.Id,clientAssetName,PAN.AssetName,SerialNumber,PAL.AssetStatus,CurrentLocationType, CurrentLocationID,mj.primaryLatLong,PAL.AssetCurrentLocationID " +
              " ,(MJ.primaryAddress1+','+MJ.primaryAddress2+','+MJ.primaryCity+','+MJ.primaryState+','+MJ.primaryCountry+'.') as Address from " +
              " PrismJobAssignedAssets JAA,manageJobOrders MJ,RigTypes RT,Prism_Assets PA,PrismAssetName PAN,clientAssets AC ,PrismAssetCurrentLocation PAL" +
              " where MJ.rigtypeid=RT.rigtypeid and MJ.jid=JAA.JobId and PAN.Id=PA.AssetName and  PA.Id=JAA.AssetId and AC.clientAssetID=PA.AssetCategoryId" +
              " and PAL.AssetID=PA.Id and PAN.Id=PA.AssetName").Tables[0];
        dt_mapjob = jobMapDatatable(dt_localmapjob, "J");
        warehouseMapDatatable(dt_localmapWarehouse);
        dt_job = SqlHelper.ExecuteDataset(GlobalConnetionString.ClientConnection(), CommandType.Text, "Select * from manageJobOrders").Tables[0];
        dt_warehouse = SqlHelper.ExecuteDataset(GlobalConnetionString.ClientConnection(), CommandType.Text, "Select * from PrsimWarehouses").Tables[0];
    }
    protected void btn_view_Click(object sender, EventArgs e)
    {
        string assetCategory = "", assetname = "", serailnumber = "";

        foreach (RadComboBoxItem radcbiSource in radcombo_assetcat.CheckedItems)
        {
            assetCategory += radcbiSource.Value + ",";
        }
        if (assetCategory != "")
        {
            assetCategory = assetCategory.Remove(assetCategory.LastIndexOf(","), 1);
        }
        foreach (RadComboBoxItem radcbiSource in combo_assetsTop.CheckedItems)
        {
            assetname += radcbiSource.Value + ",";
        }
        if (assetname != "")
        {
            assetname = assetname.Remove(assetname.LastIndexOf(","), 1);
        }
        foreach (RadComboBoxItem radcbiSource in COMBO_SERIALNUMBER.CheckedItems)
        {
            serailnumber += radcbiSource.Value + ",";
        }
        if (serailnumber != "")
        {
            serailnumber = serailnumber.Remove(serailnumber.LastIndexOf(","), 1);
        }
        grid_1st.DataSource = jobFirstRowFilterAssetLocation(assetCategory, assetname, serailnumber, false, false);
        grid_1st.DataBind();
    }
    protected void btn_rig_Click(object sender, EventArgs e)
    {
        string rigType = "";

        foreach (RadComboBoxItem radcbiSource in combo_Rig.CheckedItems)
        {
            rigType += radcbiSource.Value + ",";
        }

        //string query="select RT.rigtypename  as Rigname,MJ.jobname,PA.AssetId,PAN.Id,clientAssetName,PAN.AssetName,SerialNumber,PAL.AssetStatus,CurrentLocationType,CurrentLocationID from "+
        //    "   PrismJobAssignedAssets JAA,manageJobOrders MJ,RigTypes RT,Prism_Assets PA,PrismAssetName PAN,clientAssets AC ,PrismAssetCurrentLocation PAL"+
        //    " where MJ.rigtypeid=RT.rigtypeid and MJ.jid=JAA.JobId and PAN.Id=PA.AssetName and  PA.Id=JAA.AssetId and AC.clientAssetID=PA.AssetCategoryId and PAL.AssetID=PA.Id and PAN.Id=PA.AssetName ";
        string query = "select RT.rigtypename  as Rigname,MJ.jobname,primaryLatLong" +
            ",(primaryAddress1+', '+primaryAddress2+', '+primaryCity+', '+primaryState+', '+primaryCountry+'.')" +
            " as Address from manageJobOrders MJ,RigTypes RT where MJ.rigtypeid=RT.rigtypeid and MJ.status='Approved'";
        if (rigType != "")
        {
            rigType = rigType.Remove(rigType.LastIndexOf(","), 1);
            query += " and  RT.rigtypeid in(" + rigType + ")";
        }
        dt_Rig = SqlHelper.ExecuteDataset(GlobalConnetionString.ClientConnection(), CommandType.Text, query).Tables[0];
        grid_ex_rig.DataSource = dt_Rig;
        grid_ex_rig.DataBind();
    }
    protected void grid_rig_ItemDataBound(object sender, GridItemEventArgs e)
    {
        // RadGrid grid = (RadGrid)sender;
        if ((e.Item.ItemType == GridItemType.Item) || (e.Item.ItemType == GridItemType.AlternatingItem))
        {
            string GIS = "", Title = "";
            GridDataItem dataItem = e.Item as GridDataItem;
            //  GridColumn column = RadGrid1.MasterTableView.GetColumn("GIs");
            Label lbl_lbl_CurrentLocationType = (Label)dataItem.FindControl("lbl_CurrentLocationType");
            Marker marker2 = new Marker();
            //dataItem["CurrentLocationID"].Text;
            if (dataItem["CurrentLocationType"].Text == "Job")
            {
                DataRow[] row_JOB = dt_job.Select("jid=" + dataItem["CurrentLocationID"].Text);
                GIS = row_JOB[0]["primaryLatLong"].ToString();
                marker2.Icon = "../../images/pin2.png";
                Title = row_JOB[0]["jobname"].ToString();
            }
            else
            {
                DataRow[] row_warehouse = dt_warehouse.Select("ID=" + dataItem["CurrentLocationID"].Text);
                GIS = row_warehouse[0]["primaryLatLong"].ToString();
                marker2.Icon = "../../images/pin1.png";
                Title = row_warehouse[0]["Name"].ToString();
            }

            if (GIS != string.Empty
              && GIS != "&nbsp;")
            {
                //instantiate new user controls per alert..
                string s = GIS;
                string[] words = s.Split(',');
                GoogleMap1.Latitude = Convert.ToDouble(words[0]);
                GoogleMap1.Longitude = Convert.ToDouble(words[1]);
                string str = "<table width='100%' border='1'>";
                str += "<tr class='logo'> <td colspan='8'><table border='0' cellpadding='0' cellspacing='0' width='100%' class='logo'>" +
                "<tr><td align='left' style='padding-left: 10px; width:80px' ><b style='color: White; font-size: 25px'>" +
                  "<img src='" + imageurl + "'/></b>" +
                  "</td><td style='color:Red; font-size:13px; font-weight:bold' align='left' >Project Resource Information & Sales Management</td></tr></table> </td></tr>" +
                    //Start Header Row
                  "<tr><td style='" + style + "'>Rig&#160;Name</td><td style='" + style + "'>Job&#160;Name</td><td style='" + style + "'>Asset&#160;Id</td>" +
                  "<td style='" + style + "'>Asset&#160;Category</td><td style='" + style + "'>Asset&#160;Name</td>" +
                  "<td style='" + style + "'>Serial&#160;#</td><td style='" + style + "'>Asset&#160;Status</td>" +
                  "<td style='" + style + "'>Asset&#160;Location</td></tr>";
                //Close Header Row

                DataRow[] row_map = dt_Rig.Select("CurrentLocationID='" + dataItem["CurrentLocationID"].Text + "' and CurrentLocationType='" + dataItem["CurrentLocationType"].Text + "'");
                if (row_map.Length > 0)
                {
                    for (int asset = 0; asset < row_map.Length; asset++)
                    {
                        str += "<tr ><td>" + row_map[asset]["Rigname"].ToString() + "</td><td>" + row_map[asset]["jobname"].ToString() + "</td><td>" + row_map[asset]["AssetId"].ToString() + "</td>" +
                        "<td>" + row_map[asset]["ClientAssetName"].ToString() + "</td><td>" + row_map[asset]["AssetName"].ToString() + "</td>" +
                        "<td>" + row_map[asset]["SerialNumber"].ToString() + "</td><td>" + getAssetStatus(row_map[asset]["AssetStatus"].ToString()) + "</td>" +
                        "<td>" + dataItem["CurrentLocationType"].Text + "(" + row_map[asset]["Address"].ToString() + ")</td></tr>";
                    }
                }
                else
                {
                    str += "<tr ><td colspan='8' align='center'> <b><span style color:red;>No Assets Found</style></b></td></tr>";
                }

                marker2.Info = str + "</table>";
                marker2.Position.Latitude = Convert.ToDouble(words[0]);
                marker2.Position.Longitude = Convert.ToDouble(words[1]);

                marker2.Title = dataItem["CurrentLocationType"].Text + ":" + Title;

                // "../../images/mm_20_blue.png";// "/alertmarker.aspx?color=66FF33";


                marker2.Animation = MarkerAnimation.Drop;
                marker2.Clickable = true;

                GoogleMarker.Markers.Add(marker2);

            }
        }
    }
    protected void grid_warehouse_ItemDataBound(object sender, GridItemEventArgs e)
    {
        // RadGrid grid = (RadGrid)sender;
        if ((e.Item.ItemType == GridItemType.Item) || (e.Item.ItemType == GridItemType.AlternatingItem))
        {
            string GIS = "";
            GridDataItem dataItem = e.Item as GridDataItem;
            //  GridColumn column = RadGrid1.MasterTableView.GetColumn("GIs");
            Label lbl_warehouseid = (Label)dataItem.FindControl("lbl_warehouseid");
            Marker marker2 = new Marker();
            //dataItem["CurrentLocationID"].Text;
            marker2.Icon = "../../images/pin1.png";
            string str = "<table width='100%' border='1'>";
            str += "<tr class='logo'> <td colspan='9'><table border='0' cellpadding='0' cellspacing='0' width='100%' class='logo'>" +
            "<tr><td align='left' style='padding-left: 10px; width:80px' ><b style='color: White; font-size: 25px'>" +
              "<img src='" + imageurl + "'/></b>" +
              "</td><td style='color:Red; font-size:13px; font-weight:bold' align='left' >Project Resource Information & Sales Management</td></tr></table> </td></tr>" +
                //Start Header Row
              "<tr><td style='" + style + "'>Select</td><td style='" + style + "'>Warehouse&#160;Name</td><td style='" + style + "'>Asset&#160;Id</td>" +
              "<td style='" + style + "'>Asset&#160;Category</td><td style='" + style + "'>Asset&#160;Name</td>" +
              "<td style='" + style + "'>Serial&#160;#</td><td style='" + style + "'>Asset&#160;Status</td>" +
              "<td style='" + style + "'>Asset&#160;Location</td></tr>";
            //Close Header Row

            // DataRow[] row_map = dt_gridwarehouse.Select("CurrentLocationID='" + dataItem["CurrentLocationID"].Text + "' and CurrentLocationType='" + dataItem["CurrentLocationType"].Text + "'");
            DataRow[] row_map = dt_mapWarehouse.Select("warehouse_id=" + lbl_warehouseid.Text);
            if (row_map.Length > 0)
            {
                for (int asset = 0; asset < row_map.Length; asset++)
                {
                    // str += "<tr ><td>" + row_map[asset]["jobname"].ToString() + "</td><td>" + row_map[asset]["Warehousename"].ToString() + "</td><td>" + row_map[asset]["AssetId"].ToString() + "</td>" +

                    str += "<tr ><td><input type='checkbox' name='ASSET' onclick='javascript:return getAssetIDS(this); return false;' value='" + row_map[asset]["PrismAssetId"].ToString() + "'></td>"+
                    "<td>" + row_map[asset]["Warehousename"].ToString() + "</td><td>" + row_map[asset]["AssetId"].ToString() + "</td>" +
                    "<td>" + row_map[asset]["ClientAssetName"].ToString() + "</td><td>" + row_map[asset]["AssetName"].ToString() + "</td>" +
                    "<td>" + row_map[asset]["SerialNumber"].ToString() + "</td><td>" + getAssetStatus(row_map[asset]["AssetStatus"].ToString()) + "</td>" +
                    "<td>" + row_map[asset]["CurrentLocationType"].ToString() + "(" + row_map[asset]["Address"].ToString() + ")</td></tr>";
                    if (row_map[asset]["CurrentLocationType"].ToString() == "Job")
                    {
                        //DataRow[] row_JOB = dt_job.Select("jid=" + dataItem["CurrentLocationID"].Text);
                        GIS = row_map[asset]["primaryLatLong"].ToString();
                        //marker2.Icon = "../../images/pin2.png";
                    }
                    else
                    {
                        //DataRow[] row_warehouse = dt_warehouse.Select("ID=" + dataItem["CurrentLocationID"].Text);
                        GIS = row_map[asset]["primaryLatLong"].ToString();

                    }

                    if (GIS != string.Empty
              && GIS != "&nbsp;")
                    {
                        //instantiate new user controls per alert..
                        string s = GIS;
                        string[] words = s.Split(',');
                        GoogleMap1.Latitude = Convert.ToDouble(words[0]);
                        GoogleMap1.Longitude = Convert.ToDouble(words[1]);
                        marker2.Info = str + "</table>";
                        marker2.Position.Latitude = Convert.ToDouble(words[0]);
                        marker2.Position.Longitude = Convert.ToDouble(words[1]);
                        marker2.Title = "Warehouse : " + row_map[asset]["Warehousename"].ToString().ToUpper();

                        marker2.Animation = MarkerAnimation.Drop;
                        marker2.Clickable = true;

                        GoogleMarker.Markers.Add(marker2);

                    }
                }
            }
            else
            {
                str += "<tr ><td colspan='9' align='center'> <b><span style color:red;>No Assets Found</style></b></td></tr>";
            }
        }
    }
    protected void grid_job_ItemDataBound(object sender, GridItemEventArgs e)
    {
        // RadGrid grid = (RadGrid)sender;
        if ((e.Item.ItemType == GridItemType.Item) || (e.Item.ItemType == GridItemType.AlternatingItem))
        {
            string GIS = "";
            GridDataItem dataItem = e.Item as GridDataItem;
            //  GridColumn column = RadGrid1.MasterTableView.GetColumn("GIs");
            Label lbl_jobid = (Label)dataItem.FindControl("lbl_jobid");
            Marker marker2 = new Marker();
            StringBuilder currLocAddress = new StringBuilder();
            //string currLocAddress = "";
            DataRow[] row_job = dt_job.Select("jid=" + lbl_jobid.Text);
            if (row_job.Length > 0)
            {
                currLocAddress.Append("Job Location:<br> Name:" + row_job[0]["primaryFirst"].ToString() + " " + row_job[0]["primaryLast"].ToString() + "<br>Address:" + row_job[0]["primaryAddress1"].ToString() + " " + row_job[0]["primaryAddress2"].ToString() + "<br>City:" + row_job[0]["primaryCity"].ToString() + "<br>State:" + row_job[0]["primaryState"].ToString() + "<br>Country:" + row_job[0]["primaryCountry"].ToString() + "<br>PostalCode:" + row_job[0]["primaryPostalCode"].ToString());
            }
            
            //dataItem["CurrentLocationID"].Text;
           
            string str = "<table width='100%' border='1'>";
            str += "<tr><td colspan='9' algn='center'><input  title='Select assets to relocate (or) Move selected assets to this location' style='background-color:green;color:white;cursor:pointer;' type='button'" +
                "value='Select assets to relocate (or) Move selected assets to this location' onclick='javascript:return movetoassetloc(this," + lbl_jobid.Text + "," + "\"JOB\"" + "," + "\"" + currLocAddress + "\"" + "); return false;' border='0' style='background-color:#acbfce;font-family:verdana;border:solid 1px #000000;color:white'/></td></tr>";
            str += "<tr><td colspan='9' style='height:5px'></td></tr>";
         //   string str = "<table width='100%' border='1'>";
          //  str += "<tr class='logo'> <td colspan='9'><table border='0' cellpadding='0' cellspacing='0' width='100%' class='logo'>" +
            //"<tr><td align='left' style='padding-left: 10px; width:80px' ><b style='color: White; font-size: 25px'>" +
            //  "<img src='" + imageurl + "'/></b>" +
            //  "</td><td style='color:Red; font-size:13px; font-weight:bold' align='left' >Project Resource Information & Sales Management</td></tr></table> </td></tr>" +
                //Start Header Row
            str += "<tr><td style='" + style + "'>Select</td><td style='" + style + "'>Rig&#160;Name</td><td style='" + style + "'>Job&#160;Name</td><td style='" + style + "'>Asset&#160;Id</td>" +
              "<td style='" + style + "'>Asset&#160;Category</td><td style='" + style + "'>Asset&#160;Name</td>" +
              "<td style='" + style + "'>Serial&#160;#</td><td style='" + style + "'>Asset&#160;Status</td>"+//<td style='" + style + "'>Comp.&#160;Name</td>" +
              "<td style='" + style + "'>Asset&#160;Location</td></tr>";
            //Close Header Row
            marker2.Icon = "../../images/pin2.png";
            // DataRow[] row_map = dt_gridwarehouse.Select("CurrentLocationID='" + dataItem["CurrentLocationID"].Text + "' and CurrentLocationType='" + dataItem["CurrentLocationType"].Text + "'");
            DataRow[] row_map = dt_mapjob.Select("jid='" + lbl_jobid.Text+"'");
            if (row_map.Length > 0)
            {
                for (int asset = 0; asset < row_map.Length; asset++)
                {
                    str += "<tr ><td><input type='checkbox' name='ASSET' onclick='javascript:return getAssetIDS(this); return false;' value='" + row_map[asset]["PrismAssetId"].ToString() + "'></td>" +
                    "<td>" + row_map[asset]["Rigname"].ToString() + "</td><td>" + row_map[asset]["jobname"].ToString() + "</td><td>" + row_map[asset]["AssetId"].ToString() + "</td>" +
                    "<td>" + row_map[asset]["ClientAssetName"].ToString() + "</td><td>" + row_map[asset]["AssetName"].ToString() + "</td>" +
                    "<td>" + row_map[asset]["SerialNumber"].ToString() + "</td><td>" + getAssetStatus(row_map[asset]["AssetStatus"].ToString()) + "</td>";
                    //str += "<td><table>";
                    //DataRow[] row_comp=dt_comp_assigned.Select("AssetId="+row_map[asset]["PrismAssetId"].ToString());
                    //for (int comp = 0; comp < row_comp.Length; comp++)
                    //{
                    //    str += "<tr><td>" + row_comp[comp]["ComponentName"].ToString() + "(" + row_comp[comp]["Serialno"].ToString() + "),</td><tr>";
                    //}
                    //str += "</table></td><td>" + row_map[asset]["CurrentLocationType"].ToString() + "(" + row_map[asset]["Address"].ToString() + ")</td></tr>";
                    str += "<td>" + row_map[asset]["CurrentLocationType"].ToString() + "(" + row_map[asset]["Address"].ToString() + ")</td></tr>";
                    if (row_map[asset]["CurrentLocationType"].ToString() == "Job")
                    {
                        //DataRow[] row_JOB = dt_job.Select("jid=" + dataItem["CurrentLocationID"].Text);
                        GIS = row_map[asset]["primaryLatLong"].ToString();
                        
                    }
                    else
                    {
                        //DataRow[] row_warehouse = dt_warehouse.Select("ID=" + dataItem["CurrentLocationID"].Text);
                        GIS = row_map[asset]["primaryLatLong"].ToString();
                        //marker2.Icon = "../../images/pin1.png";

                    }

                    if (GIS != string.Empty
              && GIS != "&nbsp;")
                    {
                        //instantiate new user controls per alert..
                        string s = GIS;
                        string[] words = s.Split(',');
                        GoogleMap1.Latitude = Convert.ToDouble(words[0]);
                        GoogleMap1.Longitude = Convert.ToDouble(words[1]);
                        marker2.Info = str + "</table>";
                        marker2.Position.Latitude = Convert.ToDouble(words[0]);
                        marker2.Position.Longitude = Convert.ToDouble(words[1]);

                        marker2.Title = "Job : " + row_map[asset]["jobname"].ToString().ToUpper();



                    }
                }//for loop
            }
            else
            {

                row_map = dt_job.Select("jid=" + lbl_jobid.Text);
                GIS = row_map[0]["primaryLatLong"].ToString();
                if (GIS != string.Empty
             && GIS != "&nbsp;")
                {
                    //instantiate new user controls per alert..
                    string s = GIS;
                    string[] words = s.Split(',');
                    GoogleMap1.Latitude = Convert.ToDouble(words[0]);
                    GoogleMap1.Longitude = Convert.ToDouble(words[1]);

                    marker2.Info = str + "<tr ><td colspan='9' align='center'> <b><span style color:red;>No Assets Found</style></b></td></tr></table>";
                    marker2.Position.Latitude = Convert.ToDouble(words[0]);
                    marker2.Position.Longitude = Convert.ToDouble(words[1]);

                    marker2.Title = "Job : " + row_map[0]["jobname"].ToString().ToUpper();
                }
            }
            marker2.Animation = MarkerAnimation.Drop;
            marker2.Clickable = true;

            GoogleMarker.Markers.Add(marker2);
        }
    }
    protected void grid_ex_rig_ItemDataBound(object sender, GridItemEventArgs e)
    {
        // RadGrid grid = (RadGrid)sender;
        if ((e.Item.ItemType == GridItemType.Item) || (e.Item.ItemType == GridItemType.AlternatingItem))
        {
            string GIS = "";

            GridDataItem dataItem = e.Item as GridDataItem;
            Marker marker2 = new Marker();
            marker2.Icon = "../../images/pin8.png";
            Label lbl_gis = (Label)dataItem.FindControl("lbl_gis");
            Label lbl_Rigname = (Label)dataItem.FindControl("lbl_Rigname");
            Label lbl_jobname = (Label)dataItem.FindControl("lbl_jobname");
            Label lbl_Address = (Label)dataItem.FindControl("lbl_Address");

            GIS = lbl_gis.Text;
            if (GIS != string.Empty
              && GIS != "&nbsp;")
            {
                //instantiate new user controls per alert..
                string s = GIS;
                string[] words = s.Split(',');
                GoogleMap1.Latitude = Convert.ToDouble(words[0]);
                GoogleMap1.Longitude = Convert.ToDouble(words[1]);

                string str = "<table width='100%' border='1'>";
                str += "<tr class='logo'> <td colspan='8'><table border='0' cellpadding='0' cellspacing='0' width='100%' class='logo'>" +
                "<tr><td align='left' style='padding-left: 10px; width:80px' ><b style='color: White; font-size: 25px'>" +
                  "<img src='" + imageurl + "'/></b>" +
                  "</td><td style='color:Red; font-size:13px; font-weight:bold' align='left' >Project Resource Information & Sales Management</td></tr></table> </td></tr>" +
                    //Start Header Row
                  "<tr><td style='" + style + "'>Rig&#160;Name</td><td style='" + style + "'>Job&#160;Name</td><td style='" + style + "'>Address</td></tr>" +
                " <tr ><td>" + lbl_Rigname.Text + "</td><td>" + lbl_jobname.Text + "</td><td>" + lbl_Address.Text + "</td></tr></table>";
                //Close Header Row

                marker2.Info = str;
                marker2.Position.Latitude = Convert.ToDouble(words[0]);
                marker2.Position.Longitude = Convert.ToDouble(words[1]);


                marker2.Title = lbl_Rigname.Text;
                // "../../images/mm_20_blue.png";// "/alertmarker.aspx?color=66FF33";


                marker2.Animation = MarkerAnimation.Drop;
                marker2.Clickable = true;

                GoogleMarker.Markers.Add(marker2);

            }
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
    protected void asset_serialno_Click(object sender, EventArgs e)
    {
        //DataTable dt_localstatus = new System.Data.DataTable();
        //string query = "select PAL.AssetCurrentLocationID,PA.ID as PrismAssetId,MJ.jid,RT.rigtypename  as Rigname,MJ.jobname,PA.AssetId,PAN.Id,clientAssetName,PAN.AssetName,SerialNumber,PAL.AssetStatus," +
        //    "CurrentLocationType,CurrentLocationID ,(primaryAddress1+', '+primaryAddress2+', '+primaryCity+', '+primaryState+', '+primaryCountry+'.') as Address from " +
        //    "   PrismJobAssignedAssets JAA,manageJobOrders MJ,RigTypes RT,Prism_Assets PA,PrismAssetName PAN,clientAssets AC ,PrismAssetCurrentLocation PAL" +
        //    " where MJ.rigtypeid=RT.rigtypeid and MJ.jid=JAA.JobId and PAN.Id=PA.AssetName and  PA.Id=JAA.AssetId and AC.clientAssetID=PA.AssetCategoryId and "+
        //    " PAL.AssetID=PA.Id and PAN.Id=PA.AssetName    and  SerialNumber  like  '%"+txt_assetseralno.Text.TrimEnd().TrimStart()+"%'";

        //dt_localstatus = SqlHelper.ExecuteDataset(GlobalConnetionString.ClientConnection(), CommandType.Text, query).Tables[0];
        //dt_Rig = jobMapDatatable(dt_localstatus, "J");
        //grid_rig.DataSource = dt_Rig;
        //grid_rig.DataBind();
        grid_1st.DataSource = jobFirstRowFilterAssetLocation("", "", "", true, false);
        grid_1st.DataBind();
    }
    protected void btn_viewwarehouse_Click(object sender, EventArgs e)
    {
        string Warehousenames = "";

        foreach (RadComboBoxItem radcbiSource in combo_warehouse.CheckedItems)
        {
            Warehousenames += radcbiSource.Value + ",";
        }

        //string query = "select RT.rigtypename  as Rigname,MJ.jobname,PAW.Name as Warehousename,PA.AssetId,PAN.Id,clientAssetName,PAN.AssetName,SerialNumber,PAL.AssetStatus,CurrentLocationType,CurrentLocationID from " +
        //    "   PrismJobAssignedAssets JAA,manageJobOrders MJ,RigTypes RT,Prism_Assets PA,PrismAssetName PAN,clientAssets AC ,PrismAssetCurrentLocation PAL,PrsimWarehouses PAW" +
        //    " where MJ.rigtypeid=RT.rigtypeid and MJ.jid=JAA.JobId and PAN.Id=PA.AssetName and  PA.Id=JAA.AssetId and PA.WarehouseId=PAW.ID and AC.clientAssetID=PA.AssetCategoryId and PAL.AssetID=PA.Id and PAN.Id=PA.AssetName ";
        string query = "select * from PrsimWarehouses where bitActive='True'";
        if (Warehousenames != "")
        {
            Warehousenames = Warehousenames.Remove(Warehousenames.LastIndexOf(","), 1);
            query += " and  ID in(" + Warehousenames + ")";
        }

        dt_gridwarehouse = SqlHelper.ExecuteDataset(GlobalConnetionString.ClientConnection(), CommandType.Text, query).Tables[0];
        grid_warehouse.DataSource = dt_gridwarehouse;
        grid_warehouse.DataBind();
    }
    protected void btn_job_view_Click(object sender, EventArgs e)
    {
        string Jobnames = "";

        foreach (RadComboBoxItem radcbiSource in combo_jobstop.CheckedItems)
        {
            Jobnames += radcbiSource.Value + ",";
        }

        string query = "select * from manageJobOrders where bitActive='True'";
        //select RT.rigtypename  as Rigname,MJ.jobname,PA.AssetId,PAN.Id,clientAssetName,PAN.AssetName,SerialNumber,PAL.AssetStatus,CurrentLocationType,CurrentLocationID from " +
        //    "   PrismJobAssignedAssets JAA,manageJobOrders MJ,RigTypes RT,Prism_Assets PA,PrismAssetName PAN,clientAssets AC ,PrismAssetCurrentLocation PAL" +
        //    " where MJ.rigtypeid=RT.rigtypeid and MJ.jid=JAA.JobId and PAN.Id=PA.AssetName and  PA.Id=JAA.AssetId and AC.clientAssetID=PA.AssetCategoryId and PAL.AssetID=PA.Id and PAN.Id=PA.AssetName ";
        if (Jobnames != "")
        {
            Jobnames = Jobnames.Remove(Jobnames.LastIndexOf(","), 1);
            query += " and  jid in(" + Jobnames + ")";
        }
        dt_Rig = SqlHelper.ExecuteDataset(GlobalConnetionString.ClientConnection(), CommandType.Text, query).Tables[0];
        grid_job.DataSource = dt_Rig;
        grid_job.DataBind();
    }
    protected void btn_assetstatus_view_Click(object sender, EventArgs e)
    {
        //DataTable dt_localstatus = new System.Data.DataTable();
        //string query = "select PAL.AssetCurrentLocationID,PA.ID as PrismAssetId,RT.rigtypename  as Rigname,MJ.jid,MJ.jobname,PA.AssetId,PAN.Id,clientAssetName,PAN.AssetName,SerialNumber,PAL.AssetStatus,CurrentLocationType," +
        //"CurrentLocationID ,(primaryAddress1+', '+primaryAddress2+', '+primaryCity+', '+primaryState+', '+primaryCountry+'.') as Address from " +
        //    "   PrismJobAssignedAssets JAA,manageJobOrders MJ,RigTypes RT,Prism_Assets PA,PrismAssetName PAN,clientAssets AC ,PrismAssetCurrentLocation PAL" +
        //    " where MJ.rigtypeid=RT.rigtypeid and MJ.jid=JAA.JobId and PAN.Id=PA.AssetName and  PA.Id=JAA.AssetId and AC.clientAssetID=PA.AssetCategoryId and " +
        //    " PAL.AssetID=PA.Id and PAN.Id=PA.AssetName";
        //if (combo_assetstatus.SelectedValue != "0")
        //{
        //    query += " and PAL.AssetStatus='" + combo_assetstatus.SelectedValue + "'";
        //}

        // dt_localstatus = SqlHelper.ExecuteDataset(GlobalConnetionString.ClientConnection(), CommandType.Text, query).Tables[0];
        // dt_Rig = jobMapDatatable(dt_localstatus, "J");
        // grid_rig.DataSource = dt_Rig;
        //grid_rig.DataBind();
        grid_1st.DataSource = jobFirstRowFilterAssetLocation("", "", "", false, true);
        grid_1st.DataBind();
    }
}