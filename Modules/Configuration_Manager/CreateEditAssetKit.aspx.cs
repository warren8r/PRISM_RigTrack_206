using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Telerik.Web.UI;
using System.Drawing;
using System.Data.SqlClient;


public partial class Modules_Configuration_Manager_CreateEditAssetKit : System.Web.UI.Page
{
    SqlConnection db = new SqlConnection(GlobalConnetionString.ClientConnection());
    SqlCommand cmdInsert;
    SqlTransaction transaction;
    protected void Page_Load(object sender, EventArgs e)
    {
        //bind(null);
        lblmessagetop.Text = "";
    }
    protected void radcombo_assetnames_ItemCheck(object sender, Telerik.Web.UI.RadListBoxItemEventArgs e)
    {
        RadListBoxItem item = (RadListBoxItem)e.Item;
        string a = item.Value;
    }
    //protected void radcombo_assetnames_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    string aaa = radcombo_assetnames.SelectedValue;
    //}
    protected void btn_adddet_OnClick(object sender, EventArgs e)
    {
        //bind(null);
        lbl_message.Text = "";
    }
    public void bind(DataTable editedval)
    {
        //pnl_dynamic.Controls.Clear();
        //DataTable dt_getexistcomponents = SqlHelper.ExecuteDataset(GlobalConnetionString.ClientConnection(), CommandType.Text, "select * from Prism_ComponentNames").Tables[0];
        //if (editedval==null)
        //{
            
        //    pnl_dynamic.Controls.Add(new LiteralControl("<table border='0' style='width:500px'><tr><td>"));
        //    for (int asset = 0; asset < radcombo_assetnames.CheckedItems.Count; asset++)
        //    {
        //        pnl_dynamic.Controls.Add(new LiteralControl("<table style='border:solid 1px #000000'><tr><td>"));
        //        TextBox txt = new TextBox();
        //        txt.ID = "txt_qty_" + asset;
        //        Label lbl_assetid = new Label();
        //        lbl_assetid.Text = radcombo_assetnames.CheckedItems[asset].Value;
        //        lbl_assetid.ID = "lbl_assetid_" + asset;
        //        lbl_assetid.Visible = false;
        //        pnl_dynamic.Controls.Add(new LiteralControl("<tr>"));
        //        pnl_dynamic.Controls.Add(new LiteralControl("<td><b>Asset Name:</b></td><td style='color:green'><b>" + radcombo_assetnames.CheckedItems[asset].Text + "</b></td></tr>"));
        //        pnl_dynamic.Controls.Add(new LiteralControl("</tr>"));
        //        pnl_dynamic.Controls.Add(new LiteralControl("<tr>"));
        //        pnl_dynamic.Controls.Add(new LiteralControl("<td align='right'><b>Qty:</b></td>"));

        //        pnl_dynamic.Controls.Add(new LiteralControl("<td align='left'>"));
        //        pnl_dynamic.Controls.Add(txt);
        //        pnl_dynamic.Controls.Add(lbl_assetid);
        //        pnl_dynamic.Controls.Add(new LiteralControl("</td>"));
        //        pnl_dynamic.Controls.Add(new LiteralControl("</tr>"));
        //        pnl_dynamic.Controls.Add(new LiteralControl("<tr>"));
        //        pnl_dynamic.Controls.Add(new LiteralControl("<td colspan='2' ><div style='overflow:scroll;height:100px;width:350px'>"));

        //        RadGrid grid = new RadGrid();
        //        grid.ID = "radgrid_" + asset;
        //        grid.AutoGenerateColumns = false;
        //        grid.DataSource = dt_getexistcomponents;

        //        //string templateColumnName = "ContactName";
        //        GridTemplateColumn templateColumn = new GridTemplateColumn();
        //        //templateColumn.ItemTemplate = new MyTemplate(templateColumnName);
        //        templateColumn.HeaderText = "";
        //        templateColumn.UniqueName = "checkbc";
        //        templateColumn.ItemTemplate = new RadGridCheckBoxTemplate();
        //        //grid.MasterTableView.DataKeyNames = "componet_id";
        //        GridTemplateColumn templateColumntxt = new GridTemplateColumn();
        //        //templateColumn.ItemTemplate = new MyTemplate(templateColumnName);
        //        templateColumntxt.HeaderText = "Qty";
        //        templateColumntxt.UniqueName = "qtyc";
        //        templateColumntxt.ItemTemplate = new RadGridTextboxTemplate();

        //        //CheckBox chk_componentchk = new CheckBox();
        //        //templateColumn.
        //        GridBoundColumn boundColumn1 = new GridBoundColumn();
        //        boundColumn1.DataField = "ComponentName";
        //        boundColumn1.UniqueName = "ComponentName";
        //        boundColumn1.HeaderText = "Component Name";
        //        GridBoundColumn boundColumn_cid = new GridBoundColumn();
        //        boundColumn_cid.DataField = "componet_id";
        //        boundColumn_cid.UniqueName = "componet_id";
        //        //boundColumn_cid.
        //        boundColumn_cid.Visible = true;
        //        grid.MasterTableView.Columns.Add(boundColumn_cid);
        //        grid.MasterTableView.Columns.Add(templateColumn);
        //        grid.MasterTableView.Columns.Add(boundColumn1);
        //        grid.MasterTableView.Columns.Add(templateColumntxt);
        //        //templateColumn.ItemTemplate = new RadGridCheckBoxTemplate();
        //        grid.DataBind();
        //        pnl_dynamic.Controls.Add(grid);
        //        pnl_dynamic.Controls.Add(new LiteralControl("</div>"));

        //        pnl_dynamic.Controls.Add(new LiteralControl("</td>"));
        //        pnl_dynamic.Controls.Add(new LiteralControl("</tr>"));
        //        pnl_dynamic.Controls.Add(new LiteralControl("</td></tr></table>"));
        //    }
        //    pnl_dynamic.Controls.Add(new LiteralControl("</td></tr></table>"));
        //}
        //else
        //{
        //    pnl_dynamic.Controls.Add(new LiteralControl("<table border='0' style='width:500px'><tr><td>"));
        //    for (int asset = 0; asset < editedval.Rows.Count; asset++)
        //    {
        //        pnl_dynamic.Controls.Add(new LiteralControl("<table style='border:solid 1px #000000'><tr><td>"));
        //        TextBox txt = new TextBox();
        //        txt.ID = "txt_qty_" + asset;
        //        txt.Text = editedval.Rows[asset]["qty"].ToString();
        //        Label lbl_assetid = new Label();
        //        lbl_assetid.Text = editedval.Rows[asset]["Id"].ToString();
        //        lbl_assetid.ID = "lbl_assetid_" + asset;
        //        lbl_assetid.Visible = false;
        //        pnl_dynamic.Controls.Add(new LiteralControl("<tr>"));
        //        pnl_dynamic.Controls.Add(new LiteralControl("<td><b>Asset Name:</b></td><td style='color:green'><b>" + editedval.Rows[asset]["AssetName"].ToString() + "</b></td></tr>"));
        //        pnl_dynamic.Controls.Add(new LiteralControl("</tr>"));
        //        pnl_dynamic.Controls.Add(new LiteralControl("<tr>"));
        //        pnl_dynamic.Controls.Add(new LiteralControl("<td align='right'><b>Qty:</b></td>"));

        //        pnl_dynamic.Controls.Add(new LiteralControl("<td align='left'>"));
        //        pnl_dynamic.Controls.Add(txt);
        //        pnl_dynamic.Controls.Add(lbl_assetid);
        //        pnl_dynamic.Controls.Add(new LiteralControl("</td>"));
        //        pnl_dynamic.Controls.Add(new LiteralControl("</tr>"));
        //        pnl_dynamic.Controls.Add(new LiteralControl("<tr>"));
        //        pnl_dynamic.Controls.Add(new LiteralControl("<td colspan='2' ><div style='overflow:scroll;height:100px;width:350px'>"));

        //        RadGrid grid = new RadGrid();
        //        grid.ID = "radgrid_" + asset;
        //        grid.AutoGenerateColumns = false;
        //        grid.DataSource = dt_getexistcomponents;
        //        //grid.MasterTableView.DataKeyNames = new string[] { "CustomerID" };
        //        //grid.MasterTableView.DataKeyNames=String.
        //        grid.ItemDataBound += new GridItemEventHandler(grid_ItemDataBound);
        //        //string templateColumnName = "ContactName";
        //        GridTemplateColumn templateColumn = new GridTemplateColumn();
        //        //templateColumn.ItemTemplate = new MyTemplate(templateColumnName);
        //        templateColumn.HeaderText = "";
        //        templateColumn.UniqueName = "checkbc";
        //        templateColumn.ItemTemplate = new RadGridCheckBoxTemplate();
        //        //grid.MasterTableView.DataKeyNames = "componet_id";
        //        GridTemplateColumn templateColumntxt = new GridTemplateColumn();
        //        //templateColumn.ItemTemplate = new MyTemplate(templateColumnName);
        //        templateColumntxt.HeaderText = "Qty";
        //        templateColumntxt.UniqueName = "qtyc";
        //        templateColumntxt.ItemTemplate = new RadGridTextboxTemplate();

        //        //CheckBox chk_componentchk = new CheckBox();
        //        //templateColumn.
        //        GridBoundColumn boundColumn1 = new GridBoundColumn();
        //        boundColumn1.DataField = "ComponentName";
        //        boundColumn1.UniqueName = "ComponentName";
        //        boundColumn1.HeaderText = "Component Name";
                
        //        GridBoundColumn boundColumn_cid = new GridBoundColumn();
        //        boundColumn_cid.DataField = "componet_id";
        //        boundColumn_cid.UniqueName = "componet_id";
        //        //boundColumn_cid.
        //        boundColumn_cid.Visible = true;
        //        grid.MasterTableView.Columns.Add(boundColumn_cid);
        //        grid.MasterTableView.Columns.Add(templateColumn);
        //        grid.MasterTableView.Columns.Add(boundColumn1);
        //        grid.MasterTableView.Columns.Add(templateColumntxt);
        //        //templateColumn.ItemTemplate = new RadGridCheckBoxTemplate();
        //        grid.DataBind();
        //        pnl_dynamic.Controls.Add(grid);
        //        pnl_dynamic.Controls.Add(new LiteralControl("</div>"));

        //        pnl_dynamic.Controls.Add(new LiteralControl("</td>"));
        //        pnl_dynamic.Controls.Add(new LiteralControl("</tr>"));
        //        pnl_dynamic.Controls.Add(new LiteralControl("</td></tr></table>"));
        //    }
        //    pnl_dynamic.Controls.Add(new LiteralControl("</td></tr></table>"));
        //}
        
        
    }
    //protected void RadGrid2_ColumnCreated(object sender, GridColumnCreatedEventArgs e)
    //{
    //    if (e.Column is GridGroupSplitterColumn)
    //    {
    //        e.Column.Visible = false;
    //    }
    //}
    //protected void RadGrid2_ItemCreated(object sender, GridItemEventArgs e)
    //{
    //    if (e.Item is GridGroupHeaderItem)
    //    {
    //        (e.Item as GridGroupHeaderItem).Cells[0].Controls.Clear();
    //        (e.Item as GridGroupHeaderItem).Cells[0].Visible = false;
    //    }
    //}
    protected void RadGrid2_GroupsChanging(object sender, Telerik.Web.UI.GridGroupsChangingEventArgs e)
    {
        RadGrid2.GroupingSettings.ShowUnGroupButton = true;
        if (e.Action == Telerik.Web.UI.GridGroupsChangingAction.Ungroup)
        {
            if (e.TableView.GroupByExpressions.Count == 1)
            {
                e.Canceled = true;
                RadGrid2.GroupingSettings.ShowUnGroupButton = false;
            }
            if (e.TableView.GroupByExpressions.Count == 2)
            {
                RadGrid2.GroupingSettings.ShowUnGroupButton = false;
            }
        }
    }
    protected void grid_ItemDataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
    {
        if (e.Item is GridDataItem)
        {
            //DataTable dt = editedval;
            GridDataItem ditem = (GridDataItem)e.Item;
            //string assetid = "lbl_assetid_" + asset;
            //Label lbl_assetid = pnl_dynamic.FindControl(assetid) as Label;
            CheckBox chk = (CheckBox)ditem["checkbc"].Controls[0];
            string aaa = (ditem["componet_id"] as TableCell).Text;
            //Label lbl_assetid = (Label)ditem.FindControl("lbl_assetid");
            //DataTable dt_getkitassetcomonents = SqlHelper.ExecuteDataset(GlobalConnetionString.ClientConnection(), CommandType.Text, "select * from PrismKitAssetComponentDetails where component_id=" + aaa + " and assetid=" + lbl_assetid.Text + "").Tables[0];
            //if (dt_getkitassetcomonents.Rows.Count > 0)
            //{
            //    chk.Checked = true;
 
            //}
            //for (int c = 0; c < dt_getkitassetcomonents.Rows.Count;c++)
            //{
            //    if (dt_getkitassetcomonents.Rows[c]["component_id"].ToString() == aaa)
            //    {
                    
            //    }
            //}
        }
    }
    protected void btn_save_OnClick(object sender, EventArgs e)
    {
        db.Open();
        transaction = db.BeginTransaction();
        if (btn_save.Text!="Update")
        {
            try
            {
                string str_insertquery_main = "insert into PrismAssetKitDetails(kitname,kitdesc,createddate)values('" + txt_kitname.Text + "','" + txt_kitdesc.Text + "','" + DatesbetweenDatatable.getdatetimeformat(DateTime.Now.ToString()) + "')";
                int insertcnt = SqlHelper.ExecuteNonQuery(transaction, CommandType.Text, str_insertquery_main);
                if (insertcnt > 0)
                {
                    DataTable dt_maxid = SqlHelper.ExecuteDataset(transaction, CommandType.Text, "SELECT * FROM  PrismAssetKitDetails WHERE  assetkitid = IDENT_CURRENT('PrismAssetKitDetails')").Tables[0];
                    string assetkitids = dt_maxid.Rows[0]["assetkitid"].ToString();
                    DataTable dt_getexistcomponents = SqlHelper.ExecuteDataset(transaction, CommandType.Text, "select * from Prism_ComponentNames").Tables[0];
                    foreach (GridDataItem item in RadGrid2.Items)
                    {
                        Label lbl_assetid = (Label)item.FindControl("lbl_assetid");
                        CheckBox chk = (CheckBox)item.FindControl("chk_assets");
                        TextBox txt_qty = (TextBox)item.FindControl("txt_qty");
                        if (chk.Checked)
                        {
                            string str_insertqueryforassedet = "insert into PrismKitAssetFromKitName(assetkitid,assetids,qty)values(" + assetkitids + "," + lbl_assetid.Text + "," + txt_qty.Text + ")";
                            SqlHelper.ExecuteNonQuery(transaction, CommandType.Text, str_insertqueryforassedet);
                        }

                    }
                }

                transaction.Commit();
                lbl_message.Text = "Record Saved Successfully";
                radgrid_manageassetkits.Rebind();
                lbl_message.ForeColor = Color.Green;
                clearitems();
                db.Close();
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                db.Close();
            }
        }
        else
        {
            try
            {
                
                DataTable dt_getexistkitnames_det = SqlHelper.ExecuteDataset(GlobalConnetionString.ClientConnection(), CommandType.Text, "select * from PrismKitAssetFromKitName k,PrismAssetName a where a.ID=k.kitassetid and k.assetkitid=" + lbl_assetkitidupdate.Text + "").Tables[0];
                if (dt_getexistkitnames_det.Rows.Count > 0)
                {
                    string updatemaintabledet = "update PrismAssetKitDetails set kitdesc='" + txt_kitdesc.Text + "' where assetkitid=" + lbl_assetkitidupdate.Text + "";
                    SqlHelper.ExecuteNonQuery(transaction, CommandType.Text, updatemaintabledet);
                    foreach (GridDataItem item in RadGrid2.Items)
                    {
                        Label lbl_assetid = (Label)item.FindControl("lbl_assetid");
                        CheckBox chk = (CheckBox)item.FindControl("chk_assets");
                        TextBox txt_qty = (TextBox)item.FindControl("txt_qty");
                        if (chk.Checked)
                        {
                            DataRow[] dr_exist = dt_getexistkitnames_det.Select("assetids=" + lbl_assetid.Text + "");
                            if (dr_exist.Length == 0)
                            {
                                string str_insertqueryforassedet = "insert into PrismKitAssetFromKitName(assetkitid,assetids,qty)values(" + lbl_assetkitidupdate.Text + "," + lbl_assetid.Text + "," + txt_qty.Text + ")";
                                SqlHelper.ExecuteNonQuery(transaction, CommandType.Text, str_insertqueryforassedet);
                            }
                            else
                            {
                                string str_updatequeryforassedet = "update PrismKitAssetFromKitName set qty=" + txt_qty.Text + " where assetkitid=" + lbl_assetkitidupdate.Text + " and assetids=" + lbl_assetid.Text + "";
                                SqlHelper.ExecuteNonQuery(transaction, CommandType.Text, str_updatequeryforassedet);
                            }
                        }

                    }
                }
                transaction.Commit();
                lbl_message.Text = "Record Updated Successfully";
                radgrid_manageassetkits.Rebind();
                lbl_message.ForeColor = Color.Green;
                clearitems();
                db.Close();
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                db.Close();
            }
        }
        RadGrid2.Rebind();


        //db.Open();
        //transaction = db.BeginTransaction();
        //try
        //{
        //    string str_insertquery_main = "insert into PrismAssetKitDetails(kitname,kitdesc,createddate)values('" + txt_kitname.Text + "','" + txt_kitdesc.Text + "','" + DatesbetweenDatatable.getdatetimeformat(DateTime.Now.ToString()) + "')";
        //    int insertcnt = SqlHelper.ExecuteNonQuery(transaction, CommandType.Text, str_insertquery_main);
        //    if (insertcnt > 0)
        //    {
        //        DataTable dt_maxid = SqlHelper.ExecuteDataset(transaction, CommandType.Text, "SELECT * FROM  PrismAssetKitDetails WHERE  assetkitid = IDENT_CURRENT('PrismAssetKitDetails')").Tables[0];
        //        string assetids = dt_maxid.Rows[0]["assetkitid"].ToString();
        //        DataTable dt_getexistcomponents = SqlHelper.ExecuteDataset(transaction, CommandType.Text, "select * from Prism_ComponentNames").Tables[0];
        //        for (int asset = 0; asset < radcombo_assetnames.CheckedItems.Count; asset++)
        //        {
        //            string gridid = "radgrid_" + asset;
        //            RadGrid rad = pnl_dynamic.FindControl(gridid) as RadGrid;
        //            string assetid = "lbl_assetid_" + asset;
        //            Label lbl_assetid = pnl_dynamic.FindControl(assetid) as Label;
        //            string assetqty = "txt_qty_" + asset;
        //            TextBox txt_assetqty = pnl_dynamic.FindControl(assetqty) as TextBox;
        //            string str_insertqueryforassedet = "insert into PrismKitAssetFromKitName(assetkitid,assetids,qty)values(" + assetids + "," + radcombo_assetnames.Items[asset].Value + "," + txt_assetqty.Text + ")";
        //            SqlHelper.ExecuteNonQuery(transaction, CommandType.Text, str_insertqueryforassedet);
        //            foreach (GridDataItem item in rad.MasterTableView.Items)
        //            {
        //                //var boundcolumn = (GridBoundColumn)rad.Columns[0];
        //                //GridColumn column = rad.MasterTableView.GetColumn("ComponentId");
        //                //GridBoundColumn detailColumn = (GridBoundColumn)item.ChildItem.NestedTableViews[0].GetColumnSafe("componet_id");
        //                CheckBox chk = (CheckBox)item["checkbc"].Controls[0];
        //                if (chk.Checked)
        //                {
        //                    TextBox txt_qty = (TextBox)item["qtyc"].Controls[0];
        //                    //TextBox txtEquipment = item["componet_id"].Controls[0] as TextBox; 
        //                    string aaa = (item["componet_id"] as TableCell).Text;
        //                    string str_insertkitassetcomp = "insert into PrismKitAssetComponentDetails(component_id,assetid,quantity)values(" + aaa + "," + lbl_assetid.Text + "," + txt_qty.Text + ")";
        //                    SqlHelper.ExecuteNonQuery(transaction, CommandType.Text, str_insertkitassetcomp);
        //                }
        //                //Telerik.Web.UI.GridBoundColumn boundcolumn12 = (GridBoundColumn)item["componet_id"].Controls[0];
        //                //qtyc
        //                //string id = item.GetDataKeyValue("componet_id");
        //                //string aaa=item["checkbc"].Text;
        //                //GridDataBou CurrentRowDataItem = item[""].

        //            }
        //        }
        //    }
        //    transaction.Commit();
        //    lbl_message.Text = "Record Saved Successfully";
        //    lbl_message.ForeColor = Color.Green;
        //    clearitems();
        //    db.Close();
        //}
        //catch (Exception ex)
        //{
        //    transaction.Rollback();
        //    db.Close();
        //}


    }
    protected void lnk_viewedit_Click(object sender, EventArgs e)
    {
        RadGrid2.Rebind();
        lbl_message.Text = "";
        GridDataItem row = (GridDataItem)(((LinkButton)sender).NamingContainer);
        Label lbl_assetkitid = (Label)row.FindControl("lbl_assetkitid");
        lbl_assetkitidupdate.Text = lbl_assetkitid.Text;
        string selectexistassignedkittojob = "select * from PrismJobKits where kitid=" + lbl_assetkitid.Text + "";
        DataTable dt_existassignedkit = SqlHelper.ExecuteDataset(GlobalConnetionString.ClientConnection(), CommandType.Text, selectexistassignedkittojob).Tables[0];
        
        DataTable dt_getexistkitnames = SqlHelper.ExecuteDataset(GlobalConnetionString.ClientConnection(), CommandType.Text, "select * from PrismAssetKitDetails where assetkitid=" + lbl_assetkitid.Text + "").Tables[0];
        if (dt_getexistkitnames.Rows.Count > 0)
        {
            txt_kitname.Text = dt_getexistkitnames.Rows[0]["kitname"].ToString();
            txt_kitdesc.Text = dt_getexistkitnames.Rows[0]["kitdesc"].ToString();
            txt_topkitname.Text = txt_kitname.Text;
            txt_topdesc.Text = txt_kitdesc.Text;
            DataTable dt_getexistkitnames_det = SqlHelper.ExecuteDataset(GlobalConnetionString.ClientConnection(), CommandType.Text, "select * from PrismKitAssetFromKitName k,PrismAssetName a where a.ID=k.assetids and k.assetkitid=" + lbl_assetkitid.Text + "").Tables[0];
            //bind(dt_getexistkitnames_det);
            for (int i = 0; i < dt_getexistkitnames_det.Rows.Count; i++)
            {
                foreach (GridDataItem item in RadGrid2.Items)
                {
                    Label lbl_assetid = (Label)item.FindControl("lbl_assetid");
                    CheckBox chk = (CheckBox)item.FindControl("chk_assets");
                    TextBox txt_qty = (TextBox)item.FindControl("txt_qty");
                    if (lbl_assetid.Text == dt_getexistkitnames_det.Rows[i]["assetids"].ToString())
                    {
                        chk.Checked = true;
                        txt_qty.Text = dt_getexistkitnames_det.Rows[i]["qty"].ToString();
                    }
                }
            }
        }
        if (dt_existassignedkit.Rows.Count > 0)
        {
            btn_save.Text = "Update";
           
            btn_save.Enabled = false;
            lbl_messageforassigned.Text = "This Kit is already assigned to jobs, you can not update this kit details";
            lbl_messageforassigned.ForeColor = Color.Red;
            btn_saveas.Visible = true;
            btn_save.Enabled = false;
        }
        else
        {
            btn_save.Text = "Update";
          
            lbl_messageforassigned.Text = "";
            btn_saveas.Visible = false;
            btn_save.Visible = true;
            btn_save.Enabled = true;
        }
    }
    public void clearitems()
    {
        txt_kitname.Text = "";
        txt_kitdesc.Text = "";
        txt_topdesc.Text = "";
        txt_topkitname.Text = "";
        
        //radcombo_assetnames.DataBind();
        //lbl_message.Text = "";
        pnl_dynamic.Controls.Clear();
    }
    protected void radgrid_manageassetkits_ItemDataBound(object sender, GridItemEventArgs e)
    {

        if (e.Item is GridDataItem)
        {
            GridDataItem item = (GridDataItem)e.Item;
            Label lbl_assetkitid = (Label)item.FindControl("lbl_assetkitid");
            //RadToolTip RadToolTip1 = (RadToolTip)item.FindControl("RadToolTip1");
            //LinkButton lnk_assetnames = (LinkButton)item.FindControl("lnk_assetnames");
            string tooltiptext = "";
            DataTable dt_getexistkitnames_det = SqlHelper.ExecuteDataset(GlobalConnetionString.ClientConnection(), CommandType.Text,
                "select a.AssetName,qty from PrismKitAssetFromKitName k,PrismAssetName a where a.ID=k.assetids and k.assetkitid=" + lbl_assetkitid.Text + "").Tables[0];
            RadGrid radgridkitassets = (RadGrid)item.FindControl("radgridkitassets");
            radgridkitassets.DataSource = dt_getexistkitnames_det;
            radgridkitassets.DataBind();

            //for (int i = 0; i < dt_getexistkitnames_det.Rows.Count; i++)
            //{
            //    tooltiptext += dt_getexistkitnames_det.Rows[i]["AssetName"].ToString()+",</br>";
            //}
            //lnk_assetnames.ToolTip = tooltiptext;
            //TableCell cell = (TableCell)item["UniqueName"];
            //cell.BackColor = System.Drawing.Color.Red;
        }
    }
    protected void btn_savenew_Click(object sender, EventArgs e)
    {
        lblmessagetop.Text = "";
         DataTable dt_getexistkitnames = SqlHelper.ExecuteDataset(GlobalConnetionString.ClientConnection(), CommandType.Text,
             "select * from PrismAssetKitDetails where kitname='" + txt_topkitname.Text.TrimEnd().TrimStart() + "'").Tables[0];
         if (dt_getexistkitnames.Rows.Count == 0)
         {
             txt_kitname.Text = txt_topkitname.Text;
             txt_kitdesc.Text = txt_topdesc.Text;
             lbl_messageforassigned.Text = "";
             btn_save.Text = "Save";
             btn_save_OnClick(null, null);
             
             btn_save.Enabled = true;
             btn_saveas.Visible = false;
             RadGrid2.Rebind();
             clearitems();
         }
         else
         {
             lblmessagetop.Text = "Kitname already exist";
             lblmessagetop.ForeColor = Color.Red;
             Page.RegisterStartupScript("callWin", "<script type='text/javascript'>OpenWindow();</script>");
            
            
         }
    }
    protected void btn_reset_Click(object sender, EventArgs e)
    {
        clearitems();
    }
}