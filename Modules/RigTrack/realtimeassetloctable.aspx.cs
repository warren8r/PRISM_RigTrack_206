using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Services;
using System.Data;
using System.Text;

public partial class realtimeassetloctable : System.Web.UI.Page
{
    
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    [WebMethod]
    public static string getHTML_new(string assetids)
    {
        DataTable dt_job = SqlHelper.ExecuteDataset(GlobalConnetionString.ClientConnection(), CommandType.Text, "Select * from [RigTrack].tblCurveGroup").Tables[0];
        DataTable dt_warehouse = SqlHelper.ExecuteDataset(GlobalConnetionString.ClientConnection(), CommandType.Text, "Select * from PrsimWarehouses").Tables[0];
        DataTable dt_AssetCurrLoc = SqlHelper.ExecuteDataset(GlobalConnetionString.ClientConnection(), CommandType.Text, "select * from PrismAssetCurrentLocation").Tables[0];

        


        //DataTable dtMapFirstRow = new DataTable();
        string style = "font-weight:bold;color:#307D7E;";
        string assetids_n = "";
        string str = "";
        if (assetids != "")
        {
            assetids_n = assetids.Remove(assetids.Length - 1, 1);
            string query_select = "select pn.AssetName as AssetName,* from Prism_Assets pa,PrismAssetName pn,clientAssets pc where pa.AssetName=pn.Id and pa.AssetCategoryId=pc.clientAssetID and pa.id in(" + assetids_n + ")";
            DataTable dt_asset = SqlHelper.ExecuteDataset(GlobalConnetionString.ClientConnection(), CommandType.Text, query_select).Tables[0];
            string assetid = "";
            str = "<table width='100%' border='1'>";
            str += "<tr><td style='" + style + "'>Asset&#160;Name</td><td style='" + style + "'>Serial&#160;#</td><td style='" + style + "'>Location</td></tr>";
            //Close Header Row

            
            for (int asset = 0; asset < dt_asset.Rows.Count; asset++)
                {
                    int maxassetID = 0;
                    string mapurl = "";
                    StringBuilder currLocAddress = new StringBuilder();
                    try
                    {
                        maxassetID = Convert.ToInt32(dt_AssetCurrLoc.Compute("Max ( AssetCurrentLocationID ) ", "AssetID = " + dt_asset.Rows[asset]["Id"].ToString() + "").ToString());
                        DataRow[] row_location = dt_AssetCurrLoc.Select("AssetCurrentLocationID=" + maxassetID);
                        //lbl_locationame.Text = row_location[0]["CurrentLocationType"].ToString();
                        
                        switch (row_location[0]["CurrentLocationType"].ToString())
                        {
                            case "WareHouse":
                                {
                                    DataRow[] row_warehouse = dt_warehouse.Select("ID=" + row_location[0]["CurrentLocationID"].ToString());
                                    if (row_warehouse.Length > 0)
                                    {
                                        currLocAddress.Append("Warehouse Location: <br> Address:" + row_warehouse[0]["primaryAddress1"].ToString() + " " + row_warehouse[0]["primaryAddress2"].ToString() + "<br>City:" + row_warehouse[0]["primaryCity"].ToString() + "<br>State:" + row_warehouse[0]["primaryState"].ToString() + "<br>Country:" + row_warehouse[0]["primaryCountry"].ToString() + "<br>PostalCode:" + row_warehouse[0]["primaryPostalCode"].ToString());
                                        //lnk_map.PostBackUrl = mapurl;
                                    }

                                    break;
                                }
                            case "Job":
                                {
                                    DataRow[] row_job = dt_job.Select("ID=" + row_location[0]["CurrentLocationID"].ToString());
                                    if (row_job.Length > 0)
                                    {
                                        currLocAddress.Append("Job Location:<br>Address:" + row_job[0]["primaryAddress1"].ToString() + " " + row_job[0]["primaryAddress2"].ToString() + "<br>City:" + row_job[0]["primaryCity"].ToString() + "<br>State:" + row_job[0]["primaryState"].ToString() + "<br>Country:" + row_job[0]["primaryCountry"].ToString() + "<br>PostalCode:" + row_job[0]["primaryPostalCode"].ToString());
                                        
                                    }
                                    break;
                                }
                        }
                    }
                    catch (Exception ex)
                    {
                        //lbl_locationame.Text = "";
                    }


                    str += "<tr><td>" + dt_asset.Rows[asset]["AssetName"].ToString() + "</td><td>" + dt_asset.Rows[asset]["SerialNumber"].ToString() + "</td><td>" + currLocAddress.ToString() + "</td></tr>";
                }
           

            str += "</table>";
        }
        return str;
    }

    [WebMethod]
    public static string moveselectedassets(string assetids, string locid, string loctype, string userid)
    {

        DataTable dt_job = SqlHelper.ExecuteDataset(GlobalConnetionString.ClientConnection(), CommandType.Text, "Select * from [RigTrack].tblCurveGroup").Tables[0];
        DataTable dt_warehouse = SqlHelper.ExecuteDataset(GlobalConnetionString.ClientConnection(), CommandType.Text, "Select * from PrsimWarehouses").Tables[0];
        DataTable dt_AssetCurrLoc = SqlHelper.ExecuteDataset(GlobalConnetionString.ClientConnection(), CommandType.Text, "select * from PrismAssetCurrentLocation").Tables[0];


        string assetids_n = "";
        string str = "";
        if (assetids != "")
        {
            assetids_n = assetids.Remove(assetids.Length - 1, 1);
            string query_select = "select pn.AssetName as AssetName,* from Prism_Assets pa,PrismAssetName pn,clientAssets pc where pa.AssetName=pn.Id and pa.AssetCategoryId=pc.clientAssetID and pa.id in(" + assetids_n + ")";
            DataTable dt_asset = SqlHelper.ExecuteDataset(GlobalConnetionString.ClientConnection(), CommandType.Text, query_select).Tables[0];
            string assetid = "";

            //Close Header Row


            for (int asset = 0; asset < dt_asset.Rows.Count; asset++)
            {
                int maxassetID = 0;
                string mapurl = "";

                try
                {
                    maxassetID = Convert.ToInt32(dt_AssetCurrLoc.Compute("Max ( AssetCurrentLocationID ) ", "AssetID = " + dt_asset.Rows[asset]["Id"].ToString() + "").ToString());
                    DataRow[] row_location = dt_AssetCurrLoc.Select("AssetCurrentLocationID=" + maxassetID);
                    //lbl_locationame.Text = row_location[0]["CurrentLocationType"].ToString();
                    string fromLocType = "", fromLocId = "";
                    string ToLocType = "", ToLocId = "";
                    string currLocType = "", currLocId = "";
                    string status = "";
                    if (loctype == "Job")
                    {
                        string selectPrismJobAssignedAssets = "select * from PrismJobAssignedAssets where assetid=" + dt_asset.Rows[asset]["Id"].ToString();

                        DataTable selectedAsset = SqlHelper.ExecuteDataset(GlobalConnetionString.ClientConnection(), CommandType.Text, selectPrismJobAssignedAssets).Tables[0];

                        if (selectedAsset.Rows.Count > 0)
                        {

                            string updatePrismJobAssignedAssets = "update PrismJobAssignedAssets set jobid=" + locid + ", AssetStatus=1 where assetid=" + dt_asset.Rows[asset]["Id"].ToString();
                            SqlHelper.ExecuteNonQuery(GlobalConnetionString.ClientConnection(), CommandType.Text, updatePrismJobAssignedAssets);
                        }
                        else
                        {
                            string insertPrismJobAssignedAssets = "insert into PrismJobAssignedAssets(jobid,assetid,AssetStatus,ModifiedBy,ModifiedDate)values(" + locid + "," + dt_asset.Rows[asset]["Id"].ToString() + ",1," + userid + ",'" + DatesbetweenDatatable.getdatetimeformat(DateTime.Now.ToString()) + "')";
                            SqlHelper.ExecuteNonQuery(GlobalConnetionString.ClientConnection(), CommandType.Text, insertPrismJobAssignedAssets);
                        }


                    }
                    else if (loctype == "WareHouse")
                    {
                        string updatePrismJobAssignedAssets = "delete from PrismJobAssignedAssets where assetid=" + dt_asset.Rows[asset]["Id"].ToString();

                        SqlHelper.ExecuteNonQuery(GlobalConnetionString.ClientConnection(), CommandType.Text, updatePrismJobAssignedAssets);
                    }

                    if (!(row_location[0]["CurrentLocationType"].ToString() == loctype && row_location[0]["CurrentLocationID"].ToString() == locid))
                    {

                        fromLocType = row_location[0]["CurrentLocationType"].ToString();
                        fromLocId = row_location[0]["CurrentLocationID"].ToString();
                        currLocType = row_location[0]["CurrentLocationType"].ToString();
                        currLocId = row_location[0]["CurrentLocationID"].ToString();
                        ToLocType = loctype;
                        ToLocId = locid;
                        status = "Available";

                        string insertCurrentLocationQuery = "insert into PrismAssetCurrentLocation(AssetID,FromLocationType,FromLocationID,ToLocationType,ToLocationID,AssetStatus,CurrentLocationType,CurrentLocationID)values(" +
                            "" + dt_asset.Rows[asset]["Id"].ToString() + ",'" + fromLocType + "'," + fromLocId + ",'" + ToLocType + "'," + ToLocId + ",'" + status + "','" + currLocType + "','" + currLocId + "')";

                        SqlHelper.ExecuteNonQuery(GlobalConnetionString.ClientConnection(), CommandType.Text, insertCurrentLocationQuery);


                    }







                    //switch (row_location[0]["CurrentLocationType"].ToString())
                    //{
                    //    case "WareHouse":
                    //        {
                    //            DataRow[] row_warehouse = dt_warehouse.Select("ID=" + row_location[0]["CurrentLocationID"].ToString());
                    //            if (row_warehouse.Length > 0)
                    //            {
                    //                //currLocAddress.Append("Warehouse Location: <br> Address:" + row_warehouse[0]["primaryAddress1"].ToString() + " " + row_warehouse[0]["primaryAddress2"].ToString() + "<br>City:" + row_warehouse[0]["primaryCity"].ToString() + "<br>State:" + row_warehouse[0]["primaryState"].ToString() + "<br>Country:" + row_warehouse[0]["primaryCountry"].ToString() + "<br>PostalCode:" + row_warehouse[0]["primaryPostalCode"].ToString());
                    //                //lnk_map.PostBackUrl = mapurl;
                    //            }

                    //            break;
                    //        }
                    //    case "Job":
                    //        {
                    //            DataRow[] row_job = dt_job.Select("jid=" + row_location[0]["CurrentLocationID"].ToString());
                    //            if (row_job.Length > 0)
                    //            {
                    //                //currLocAddress.Append("Job Location:<br>Address:" + row_location[0]["primaryAddress1"].ToString() + " " + row_location[0]["primaryAddress2"].ToString() + "<br>City:" + row_location[0]["primaryCity"].ToString() + "<br>State:" + row_location[0]["primaryState"].ToString() + "<br>Country:" + row_location[0]["primaryCountry"].ToString() + "<br>PostalCode:" + row_location[0]["primaryPostalCode"].ToString());

                    //            }
                    //            break;
                    //        }
                    //}
                }
                catch (Exception ex)
                {
                    str = "error";
                }



            }
            str = "success";


        }
        return str;
    }
}