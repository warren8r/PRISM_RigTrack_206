using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Artem.Google.UI;
using System.Data;
using Telerik.Web.UI;


public partial class Modules_Manage_Events_eventDashboard : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
            
        GoogleMap1.Zoom = 4;

        //until we've settled on this query I'll disable event category and name
       // DropDownList2.Enabled = false;
       // ddlCatNames.Enabled = false;

        if (!IsPostBack)
        {

            RadDatePicker1.SelectedDate = DateTime.Now.AddDays(-7);
            RadDatePicker2.SelectedDate = DateTime.Now;
          
            //SqlDataSource1.SelectParameters[2].DefaultValue = "";
            gridGRO.DataBind();
        }

       // gridGRO.Visible = true;
        gridPane.Scrolling = Telerik.Web.UI.SplitterPaneScrolling.None;
        gridGRO.DataBind();
    //    lblSQL.Text = SqlDataSource1.SelectCommand.ToString();
    }

    protected void gridGRO_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (e.Row.Cells[28].Text != string.Empty
                && e.Row.Cells[28].Text != "&nbsp;")
            {
                //instantiate new user controls per alert..
                string s = e.Row.Cells[28].Text;
                string[] words = s.Split(',');
                GoogleMap1.Latitude = Convert.ToDouble(words[0]);
                GoogleMap1.Longitude = Convert.ToDouble(words[1]);
                //GoogleMap1.Center = new LatLng(Convert.ToDouble(words[0]) + "," + Convert.ToDouble(words[1]));

                Marker marker2 = new Marker();
                marker2.Position.Latitude = Convert.ToDouble(words[0]);
                marker2.Position.Longitude = Convert.ToDouble(words[1]);
                marker2.Title = "Event " + e.Row.Cells[3].Text + '-' + e.Row.Cells[24].Text + '-' + e.Row.Cells[2].Text + " Alert";


                try
                {
                    //string flagColor = 
                    //switch(flagColor){
                    //    case "":
                    //        break;
                    //    default:
                    //        marker2.Icon = "/images/markers/yellow.gif";
                    //        break;
                    //}
                    string flagName = e.Row.Cells[24].Text;
                    switch (flagName)
                    {
                        case "Low Priority":
                            marker2.Icon = "/alertmarker.aspx?color=" + e.Row.Cells[29].Text.Remove(0, 1);
                            break;
                        case "Normal":
                            marker2.Icon = "/alertmarker.aspx?color=" + e.Row.Cells[29].Text.Remove(0, 1);
                            break;
                        case "Urgent":
                            marker2.Icon = "/alertmarker.aspx?color=" + e.Row.Cells[29].Text.Remove(0, 1);
                            break;
                        case "Important":
                            marker2.Icon = "/alertmarker.aspx?color=" + e.Row.Cells[29].Text.Remove(0, 1);
                            break;
                        case "Critical":
                            marker2.Icon = "/alertmarker.aspx?color=" + e.Row.Cells[29].Text.Remove(0, 1);
                            break;
                        default:
                            marker2.Icon = "/alertmarker.aspx?color=" + e.Row.Cells[29].Text.Remove(0, 1);
                            break;
                    }

                }
                catch (Exception ex)
                {

                    // throw;
                }


                marker2.Animation = MarkerAnimation.Drop;
                marker2.Clickable = true;
                GoogleMarker.Markers.Add(marker2);
            }
        }
    }

    protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
    {
    }
    protected void btnFilterGIS_Click(object sender, EventArgs e)
    {

        gridGRO.DataBind();
    }
    protected void SqlDataSource1_Selecting(object sender, SqlDataSourceSelectingEventArgs e)
    {
        //if (ddlCatNames.SelectedText == "- Select All -")
        //{
        //    SqlDataSource1.SelectParameters[2].DefaultValue = "";
        //    SqlDataSource1.DataBind();
        //}
        

    }
    protected void RadDatePicker1_SelectedDateChanged(object sender, Telerik.Web.UI.Calendar.SelectedDateChangedEventArgs e)
    {
        gridGRO.DataBind();
    }
    protected void RadDatePicker2_SelectedDateChanged(object sender, Telerik.Web.UI.Calendar.SelectedDateChangedEventArgs e)
    {
        gridGRO.DataBind();
    }

    
    protected void ddlCatNames_SelectedIndexChanged(object sender, Telerik.Web.UI.DropDownListEventArgs e)
    {
        DropDownList2.Items.Clear();
        
        DropDownList2.DataBind();
        DropDownList2.Items.Insert(0, new DropDownListItem("- Select All -", ""));
        gridGRO.DataBind();
       // lblSQL.Text = SqlDataSource1.SelectCommand.ToString();
    }
    protected void ddlCatNames2_SelectedIndexChanged(object sender, Telerik.Web.UI.DropDownListEventArgs e)
    {
      
        gridGRO.DataBind();
    }
    protected void RadGrid1_PageIndexChanged(object sender, Telerik.Web.UI.GridPageChangedEventArgs e)
    {
        gridGRO.DataBind();
    }
    protected void RadGrid1_GroupsChanging(object sender, Telerik.Web.UI.GridGroupsChangingEventArgs e)
    {
        gridGRO.DataBind();
    }
    protected void RadGrid1_SortCommand(object sender, Telerik.Web.UI.GridSortCommandEventArgs e)
    {
        gridGRO.DataBind();
    }
    protected void gridGRO_DataBound(object sender, EventArgs e)
    {
        if (gridGRO.Rows.Count > 3)
        {
            GoogleMap1.Zoom = 5;
        }
    }
    protected void SqlDataSource1_Disposed(object sender, EventArgs e)
    {
       
    }
    protected void SqlDataSource1_Selected(object sender, SqlDataSourceStatusEventArgs e)
    {
        //What to do after data is loaded.
       
        int TotalCount = 0;
        //foreach (GridDataItem item in RadGrid1.Items)
        //{
          
        //        TotalCount++;
        //}

        TotalCount = e.AffectedRows;
       



        if (TotalCount == 0)
        {
            norecords.Visible = true;
        }
        else
        {
            norecords.Visible = false;
        }  
        
        lblCount.Text = TotalCount.ToString();
    }
}