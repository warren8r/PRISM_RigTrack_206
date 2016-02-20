using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Artem.Google.Net;
using Telerik.Web.UI;
using System.Globalization;

public partial class Controls_configMangrSDP_mngCollectors : System.Web.UI.UserControl
{
    #region Properties
    
    private string _PageName;
    
    public string PageName
    {
        get
        {
            return _PageName;
        }
        set
        {
            _PageName = value;
        }
    }
    
    #endregion
    
    #region global_classes

    MDM.Collector Collector = new MDM.Collector();

    #endregion
    
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Telerik.Web.UI.RadMaskedTextBox txtSDPNumber = (Telerik.Web.UI.RadMaskedTextBox)FormView1.FindControl("txtSDPNumber");

            txtSDPNumber.Text = Collector.GenerateNewAccountID(0);
            RadDropDownList ddlPrimaryCountry = (RadDropDownList)FormView1.FindControl("ddlPrimaryCountry");
            ddlPrimaryCountry.SelectedValue = "US";
            RadDropDownList ddlSecondaryCountry = (RadDropDownList)FormView1.FindControl("ddlSecondaryCountry");
            ddlSecondaryCountry.SelectedValue = "US";
        }
        //try
        //{
        //    //Label lblLabel = (Label)FormView1.FindControl("lblLabel");
        //    //Label lblLabel1 = (Label)FormView1.FindControl("lblLabel");

        //    //var lblString = ;

        //    //lblLabel.Text = lblString;
        //    //lblLabel1.Text = lblString;
        //}
        //catch (Exception ex)
        //{
        //    PageName = ex.ToString();
        //  //  throw ex;
        //}
     

    }
    
    protected void btnAddGIS_Click(object sender, EventArgs e)
    {
    }
    
    protected void sqlGetUpdateSDP_Inserted(object sender, SqlDataSourceStatusEventArgs e)
    {
        //  radSDPList.DataBind();
        // GridView1.DataBind();
        radGrid1.DataBind();

        n1.Title = PageName + " Notice";
        n1.Text = PageName + " created.";
        n1.Show();
    }
    
    protected void lnkBtnGatherGIS_Click(object sender, EventArgs e)
    {
        Telerik.Web.UI.RadTextBox txtAddress1 = (Telerik.Web.UI.RadTextBox)FormView1.FindControl("txtprimaryAddress1");
        Telerik.Web.UI.RadTextBox txtAddress2 = (Telerik.Web.UI.RadTextBox)FormView1.FindControl("txtprimaryAddress2");
        Telerik.Web.UI.RadTextBox txtCity = (Telerik.Web.UI.RadTextBox)FormView1.FindControl("txtprimaryCity");
        //Telerik.Web.UI.RadDropDownList ddlRadState = (Telerik.Web.UI.RadDropDownList)FormView1.FindControl("ddlPrimaryState");
        RadDropDownList ddlRadState = (RadDropDownList)FormView1.FindControl("ddlPrimaryState");
        Telerik.Web.UI.RadDropDownList ddlCountry = (Telerik.Web.UI.RadDropDownList)FormView1.FindControl("ddlPrimaryCountry");
        Telerik.Web.UI.RadTextBox txtPostal = (Telerik.Web.UI.RadTextBox)FormView1.FindControl("txtprimaryPostalCode");
        Telerik.Web.UI.RadTextBox txtCoordLatLong = (Telerik.Web.UI.RadTextBox)FormView1.FindControl("txtPrimaryGIS");
        
        HyperLink hypPrimaryMap = (HyperLink)FormView1.FindControl("hypPrimaryMapLink");
        Label lblPrimaryMatch = (Label)FormView1.FindControl("lblPrimaryMatch");
        TextBox txtMatchDB = (TextBox)FormView1.FindControl("txtMatchDB");

        RadButton lnkBtnGatherGISPrimary = (RadButton)FormView1.FindControl("lnkBtnGatherGISPrimary");

        Telerik.Web.UI.RadWindow WindowPrimary = (Telerik.Web.UI.RadWindow)FormView1.FindControl("WindowPrimary");



        Artem.Google.UI.GoogleMap GoogleMap1 = (Artem.Google.UI.GoogleMap)FindControl("GoogleMap1");

        string addressToCheck = txtAddress1.Text + " " + txtAddress2.Text + " " + txtCity.Text + ", " + ddlRadState.SelectedItem.Value + " " + txtPostal.Text + " " + ddlCountry.SelectedItem.Value;


        try
        {
            if (txtAddress1.Text != String.Empty)
            {
                // Make request to Google API
                GeoRequest gr = new GeoRequest(addressToCheck);
                GeoResponse gRes = gr.GetResponse();

                Debug.WriteLine(addressToCheck);
                // Set Latitude/Longitude in textbox
                txtCoordLatLong.Text = gRes.Results[0].Geometry.Location.Latitude.ToString() + "," + gRes.Results[0].Geometry.Location.Longitude.ToString();
                hypPrimaryMap.NavigateUrl = "http://maps.google.com/?q=" + gRes.Results[0].Geometry.Location.Latitude.ToString() + "," + gRes.Results[0].Geometry.Location.Longitude.ToString();

                GoogleMap1.Latitude = gRes.Results[0].Geometry.Location.Latitude;
                GoogleMap1.Longitude = gRes.Results[0].Geometry.Location.Longitude;

                Debug.WriteLine("Lat:" + GoogleMap1.Latitude.ToString());
                Debug.WriteLine("Lon:" + GoogleMap1.Longitude.ToString());

                string resultLocationMatch = gRes.Results[0].Geometry.LocationType.ToString();
                Debug.WriteLine(resultLocationMatch);

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
                Debug.WriteLine(resultLocationMatch);

                lblPrimaryMatch.Text = gRes.Results[0].Types[0].ToString().ToUpper() + " (" + resultAccuracy + ")";
                txtMatchDB.Text = lblPrimaryMatch.Text;
                lblPrimaryMatch.Visible = false;
                hypPrimaryMap.Visible = true;


            }
        }
        catch (Exception)
        {
            lnkBtnGatherGISPrimary.Enabled = true;
            throw;
        }
       
    }
    
    protected void lnkBtnGatherGIS2_Click(object sender, EventArgs e)
    {
        //GIS Functionality for the second Contact
        Telerik.Web.UI.RadTextBox txtAddress1 = (Telerik.Web.UI.RadTextBox)FormView1.FindControl("txtSecondaryAddress1");
        Telerik.Web.UI.RadTextBox txtAddress2 = (Telerik.Web.UI.RadTextBox)FormView1.FindControl("txtSecondaryAddress2");
        Telerik.Web.UI.RadTextBox txtCity = (Telerik.Web.UI.RadTextBox)FormView1.FindControl("txtSecondaryCity");
        //Telerik.Web.UI.RadDropDownList ddlRadState = (Telerik.Web.UI.RadDropDownList)FormView1.FindControl("ddlSecondaryState");
        RadDropDownList ddlRadState = (RadDropDownList)FormView1.FindControl("ddlSecondaryState");
        Telerik.Web.UI.RadDropDownList ddlCountry = (Telerik.Web.UI.RadDropDownList)FormView1.FindControl("ddlSecondaryCountry");
        Telerik.Web.UI.RadTextBox txtPostal = (Telerik.Web.UI.RadTextBox)FormView1.FindControl("txtSecondaryPostal");
        Telerik.Web.UI.RadTextBox txtCoordLatLong = (Telerik.Web.UI.RadTextBox)FormView1.FindControl("txtSecondaryGIS");
       // HyperLink hypPrimaryMap = (HyperLink)FormView1.FindControl("hypSecondaryMap");
        Label lblPrimaryMatch = (Label)FormView1.FindControl("lblSecondaryLatLongAccry");
        Telerik.Web.UI.RadWindow WindowPrimary = (Telerik.Web.UI.RadWindow)FormView1.FindControl("WindowPrimary");

        Artem.Google.UI.GoogleMap GoogleMap1 = (Artem.Google.UI.GoogleMap)FindControl("GoogleMap2");

        string addressToCheck = txtAddress1.Text + " " + txtAddress2.Text + " " + txtCity.Text + ", " + ddlRadState.SelectedItem.Value + " " + txtPostal.Text + " " + ddlCountry.SelectedItem.Value;
        
        if (txtAddress1.Text != String.Empty)
        {
            // Make request to Google API
            GeoRequest gr = new GeoRequest(addressToCheck);
            GeoResponse gRes = gr.GetResponse();
            
            Debug.WriteLine(addressToCheck);
            // Set Latitude/Longitude in textbox
            txtCoordLatLong.Text = gRes.Results[0].Geometry.Location.Latitude.ToString() + "," + gRes.Results[0].Geometry.Location.Longitude.ToString();
           // hypPrimaryMap.NavigateUrl = "http://maps.google.com/?q=" + gRes.Results[0].Geometry.Location.Latitude.ToString() + "," + gRes.Results[0].Geometry.Location.Longitude.ToString();
            
            GoogleMap1.Latitude = gRes.Results[0].Geometry.Location.Latitude;
            GoogleMap1.Longitude = gRes.Results[0].Geometry.Location.Longitude;
            
            Debug.WriteLine("Lat:" + GoogleMap1.Latitude.ToString());
            Debug.WriteLine("Lon:" + GoogleMap1.Longitude.ToString());
            
            string resultLocationMatch = gRes.Results[0].Geometry.LocationType.ToString();
            Debug.WriteLine(resultLocationMatch);
            
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
            Debug.WriteLine(resultLocationMatch);
            
            lblPrimaryMatch.Text = gRes.Results[0].Types[0].ToString().ToUpper() + " (" + resultAccuracy + ")";
            //hypPrimaryMap.Visible = true;
        }
    }
    
    protected void radSDPList_ItemCommand(object sender, GridCommandEventArgs e)
    {
        if (e.CommandName == RadGrid.SelectCommandName)
        {
            //  Response.Write("Primary key for the clicked item from ItemCommand: " + (e.Item as GridDataItem).GetDataKeyValue("ID").ToString() + "<br>");
            // lblSelectedRecord.Text = (e.Item as GridDataItem).GetDataKeyValue("ID").ToString();


          

        }
    }
    
    protected void radSDPList_SelectedIndexChanged(object sender, EventArgs e)
    {
        //GridDataItem gdiItem = radSDPList.MasterTableView.FindItemByKeyValue("ID", (radSDPList.SelectedItems[0] as GridDataItem).GetDataKeyValue("ID").ToString());
        //gdiItem.Selected = true;
        //lblSelectedRecord.Text = (radSDPList.SelectedItems[0] as GridDataItem).GetDataKeyValue("ID").ToString();
        //lblSelectedRecord.Text = radSDPList.SelectedValue.ToString(); 
    }
    
    protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
    {
        //int MRLID = Convert.ToInt32(GridView1.SelectedDataKey.Value);
        //lblSelectedRecord.Text = MRLID.ToString();
        FormView1.ChangeMode(FormViewMode.Edit);
        FormView1.DataBind();
    }
    
    protected void FormView1_ItemCommand(object sender, FormViewCommandEventArgs e)
    {
        if (e.CommandName == "Cancel")
        {
            //GridView1.SelectedIndex = -1;
            radGrid1.SelectedIndexes.Clear();
        }
        //if (e.CommandName == "Update")
        //{
        //    FormView1.DataBind();
        //}
        //if (e.CommandName == "Update")
        //{
        //    sqlGetUpdateSDP.Update();
        //    GridView1.DataBind();
        //}
    }
    
    protected void LinkButton1_Click1(object sender, EventArgs e)
    {
        Telerik.Web.UI.RadMaskedTextBox txtSDPNumber = (Telerik.Web.UI.RadMaskedTextBox)FormView1.FindControl("txtSDPNumber");
    
        txtSDPNumber.Text = Collector.GenerateNewAccountID(0);
    }
    
    //protected void lnkNewSDPType_Click(object sender, EventArgs e)
    //{
    //    Panel pnlEditSDPTypes = (Panel)FormView1.FindControl("pnlEditSDPTypes");
    //    if (pnlEditSDPTypes.Visible == false)
    //    {
    //        pnlEditSDPTypes.Visible = true;
    //    }
    //    else
    //    {
    //        pnlEditSDPTypes.Visible = false;
    //    }
    //}
    
    protected void lnkNewDwellingType_Click(object sender, EventArgs e)
    {
        //Panel pnlEditSDPDwellingTypes = (Panel)FormView1.FindControl("pnlSDPDwellingTypes");
        ////  FormView FormView1 = (FormView)pnlEditSDPDwellingTypes.FindControl("FormView1");
        //// FormView1.DataBind();
        //if (pnlEditSDPDwellingTypes.Visible == false)
        //{
        //    //FormView1.DataBind();
        //    pnlEditSDPDwellingTypes.Visible = true;
        //}
        //else
        //{
        //    pnlEditSDPDwellingTypes.Visible = false;
        //}
    }
    
    public bool CheckNull(object value)
    {
        return DBNull.Value.Equals(value);
    }
    
    protected void FormView1_DataBound(object sender, EventArgs e)
    {
        if (FormView1.CurrentMode == FormViewMode.Edit)
        {
            //DataRowView drv = (DataRowView)FormView1.DataItem;
            RadDropDownList ddlRadState = (RadDropDownList)FormView1.Row.FindControl("ddlPrimaryState");
            RadDropDownList ddlCountryList = (RadDropDownList)FormView1.Row.FindControl("ddlPrimaryCountry");
            //RadDropDownList radDDLSecondaryStateList = (RadDropDownList)FormView1.Row.FindControl("ddlSecondaryState");
            RadDropDownList radDDLSecondaryStateList = (RadDropDownList)FormView1.Row.FindControl("ddlSecondaryState");
            RadDropDownList radDDLSecondaryCountryList = (RadDropDownList)FormView1.Row.FindControl("ddlSecondaryCountry");

            Label lblSelectedPrimaryState = (Label)FormView1.FindControl("primaryState");
            Label lblSelectedPrimaryCountry = (Label)FormView1.FindControl("primaryCountry");
            Label lblSelectedSecondaryState = (Label)FormView1.FindControl("secondaryState");
            Label lblSelectedSecondaryCountry = (Label)FormView1.FindControl("secondaryCountry");

            RadMaskedTextBox txtSDPNumber = (RadMaskedTextBox)FormView1.FindControl("txtSDPNumber");
            RadTextBox txtSDPName = (RadTextBox)FormView1.FindControl("txtCollectorName");

            LinkButton lnkGenerateID = (LinkButton)FormView1.FindControl("LinkButton1");

            lnkGenerateID.ForeColor = System.Drawing.Color.Gray;
            txtSDPName.ReadOnly = true;
            txtSDPName.Enabled = false;

            txtSDPNumber.ReadOnly = true;
            txtSDPNumber.Enabled = false;


            
            // Primary State Dropdown - EDIT
            ddlRadState.ClearSelection();
            foreach (DropDownListItem item in ddlRadState.Items)
            {
                if (item.Value.Equals(lblSelectedPrimaryState.Text))
                {
                    item.Selected = true;
                }
            }
            
            // Primary Country Dropdown - EDIT
            ddlCountryList.ClearSelection();
            foreach (DropDownListItem item in ddlCountryList.Items)
            {
                if (item.Value.Equals(lblSelectedPrimaryCountry.Text))
                {
                    item.Selected = true;
                }
            }

            // Secondary State Dropdown - EDIT
            radDDLSecondaryStateList.ClearSelection();
            foreach (DropDownListItem item in radDDLSecondaryStateList.Items)
            {
                if (item.Value.Equals(lblSelectedSecondaryState.Text))
                {
                    item.Selected = true;
                }
            }


            
            // Secondary Country Dropdown - EDIT
            radDDLSecondaryCountryList.ClearSelection();
            foreach (DropDownListItem item in radDDLSecondaryCountryList.Items)
            {
                if (item.Value.Equals(lblSelectedSecondaryCountry.Text))
                {
                    item.Selected = true;
                }
            }
            //Show GIS for Primary MAP Link
        //     hypPrimaryMapLink.Visible = true;

            RadDropDownList ddlPrimaryCountry = (RadDropDownList)FormView1.FindControl("ddlPrimaryCountry");
            Panel pnlUS = (Panel)FormView1.FindControl("pnlUS");
            Panel pnlNonUS = (Panel)FormView1.FindControl("pnlNonUS");

            if (ddlPrimaryCountry.SelectedValue != "US")
            {
                pnlUS.Visible = false;
                pnlNonUS.Visible = true;
            }
            else
            {
                pnlUS.Visible = true;
                pnlNonUS.Visible = false;
            }


            RadDropDownList ddlSecondaryCountry = (RadDropDownList)FormView1.FindControl("ddlSecondaryCountry");
            Panel pnlUS0 = (Panel)FormView1.FindControl("pnlUS0");
            Panel pnlNonUS0 = (Panel)FormView1.FindControl("pnlNonUS0");

            if (ddlSecondaryCountry.SelectedValue != "US")
            {
                pnlUS0.Visible = false;
                pnlNonUS0.Visible = true;
            }
            else
            {
                pnlUS0.Visible = true;
                pnlNonUS0.Visible = false;
            }








        }


    }
    
    protected void sqlGetUpdateSDP_Updated(object sender, SqlDataSourceStatusEventArgs e)
    {


        n1.Title = PageName + " Notice";
        n1.Text = PageName + " updated.";
        n1.Show();
        radGrid1.DataBind();
        radGrid1.SelectedIndexes.Clear();
    }
    
    protected void radGrid1_SelectedIndexChanged(object sender, EventArgs e)
    {
        int MRLID = Convert.ToInt32(radGrid1.SelectedValue);

        lblSelectedRecord.Text = MRLID.ToString();
        lblSelectedRecord.Visible = false;
        FormView1.ChangeMode(FormViewMode.Edit);
        FormView1.DataBind();
    }
    
    protected void FormView1_ItemCreated(object sender, EventArgs e)
    {
        if (FormView1.CurrentMode == FormViewMode.Insert)
        {
            CheckBox chkActive = (CheckBox)FormView1.FindControl("CheckBox1");
            
            if (chkActive != null)
            {
                chkActive.Checked = true;
            }
        }
    }
    
    protected void CheckBox2_CheckedChanged(object sender, EventArgs e)
    {
        CheckBox CheckBox2 = (CheckBox)FormView1.FindControl("Checkbox2");
        
        RadTextBox txtPrimaryAddress1 = (RadTextBox)FormView1.FindControl("txtPrimaryAddress1");
        RadTextBox txtPrimaryAddress2 = (RadTextBox)FormView1.FindControl("txtPrimaryAddress2");
        RadTextBox txtPrimaryCity = (RadTextBox)FormView1.FindControl("txtPrimaryCity");
        RadTextBox txtPrimaryPostal = (RadTextBox)FormView1.FindControl("txtprimaryPostalCode");
        RadDropDownList ddlPrimaryState = (RadDropDownList)FormView1.FindControl("ddlPrimaryState");
        RadDropDownList ddlPrimaryCountry = (RadDropDownList)FormView1.FindControl("ddlPrimaryCountry");
        
        RadTextBox txtSecondaryAddress1 = (RadTextBox)FormView1.FindControl("txtSecondaryAddress1");
        RadTextBox txtSecondaryAddress2 = (RadTextBox)FormView1.FindControl("txtSecondaryAddress2");
        RadTextBox txtSecondaryCity = (RadTextBox)FormView1.FindControl("txtSecondaryCity");
        RadTextBox txtSecondaryPostal = (RadTextBox)FormView1.FindControl("txtSecondaryPostalCode");
        RadDropDownList ddlSecondaryState = (RadDropDownList)FormView1.FindControl("ddlSecondaryState");
        RadDropDownList ddlSecondaryCountry = (RadDropDownList)FormView1.FindControl("ddlSecondaryCountry");
        
        if (CheckBox2.Checked == true)
        {
            //copy contents from the primary to the secondary
            txtSecondaryAddress1.Text = txtPrimaryAddress1.Text;
            txtSecondaryAddress2.Text = txtPrimaryAddress2.Text;
            txtSecondaryCity.Text = txtPrimaryCity.Text;
            txtSecondaryPostal.Text = txtPrimaryPostal.Text;
            // Secondary State Dropdown - EDIT
            ddlSecondaryState.ClearSelection();

           // ddlSecondaryState.SelectedText = ddlPrimaryState.SelectedText;
            ddlSecondaryState.SelectedIndex = ddlPrimaryState.SelectedIndex;
            ddlSecondaryCountry.ClearSelection();

            ddlSecondaryCountry.SelectedText = ddlPrimaryCountry.SelectedText;
        }
    }
    protected void chkActiveEdit_CheckedChanged(object sender, EventArgs e)
    {
        CheckBox CheckBox2 = (CheckBox)FormView1.FindControl("chkActiveEdit");

        RadTextBox txtPrimaryAddress1 = (RadTextBox)FormView1.FindControl("txtPrimaryAddress1");
        RadTextBox txtPrimaryAddress2 = (RadTextBox)FormView1.FindControl("txtPrimaryAddress2");
        RadTextBox txtPrimaryCity = (RadTextBox)FormView1.FindControl("txtPrimaryCity");
        RadTextBox txtPrimaryPostal = (RadTextBox)FormView1.FindControl("txtprimaryPostalCode");
        RadDropDownList ddlPrimaryState = (RadDropDownList)FormView1.FindControl("ddlPrimaryState");
        RadDropDownList ddlPrimaryCountry = (RadDropDownList)FormView1.FindControl("ddlPrimaryCountry");

        RadTextBox txtSecondaryAddress1 = (RadTextBox)FormView1.FindControl("txtSecondaryAddress1");
        RadTextBox txtSecondaryAddress2 = (RadTextBox)FormView1.FindControl("txtSecondaryAddress2");
        RadTextBox txtSecondaryCity = (RadTextBox)FormView1.FindControl("txtSecondaryCity");
        RadTextBox txtSecondaryPostal = (RadTextBox)FormView1.FindControl("txtsecondaryPostal");
        RadDropDownList ddlSecondaryState = (RadDropDownList)FormView1.FindControl("ddlSecondaryState");
        RadDropDownList ddlSecondaryCountry = (RadDropDownList)FormView1.FindControl("ddlSecondaryCountry");

        if (CheckBox2.Checked == true)
        {
            //copy contents from the primary to the secondary
            txtSecondaryAddress1.Text = txtPrimaryAddress1.Text;
            txtSecondaryAddress2.Text = txtPrimaryAddress2.Text;
            txtSecondaryCity.Text = txtPrimaryCity.Text;
            txtSecondaryPostal.Text = txtPrimaryPostal.Text;
            // Secondary State Dropdown - EDIT
            ddlSecondaryState.ClearSelection();
            //foreach (DropDownListItem item in ddlSecondaryState.Items)
            //{
            //    if (item.Value.Equals(ddlPrimaryState.SelectedValue.ToString()))
            //    {
            //        item.Selected = true;
            //    }
            //}
            ddlSecondaryState.SelectedIndex = ddlPrimaryState.SelectedIndex;
        }
    }
    protected void CheckBox2_CheckedChanged1(object sender, EventArgs e)
    {
        CheckBox CheckBox2 = (CheckBox)FormView1.FindControl("Checkbox2");

        RadTextBox txtPrimaryAddress1 = (RadTextBox)FormView1.FindControl("txtPrimaryAddress1");
        RadTextBox txtPrimaryAddress2 = (RadTextBox)FormView1.FindControl("txtPrimaryAddress2");
        RadTextBox txtPrimaryCity = (RadTextBox)FormView1.FindControl("txtPrimaryCity");
        RadTextBox txtPrimaryPostal = (RadTextBox)FormView1.FindControl("txtprimaryPostalCode");
        RadDropDownList ddlPrimaryState = (RadDropDownList)FormView1.FindControl("ddlPrimaryState");
        RadDropDownList ddlPrimaryCountry = (RadDropDownList)FormView1.FindControl("ddlPrimaryCountry");

        RadTextBox txtSecondaryAddress1 = (RadTextBox)FormView1.FindControl("txtSecondaryAddress1");
        RadTextBox txtSecondaryAddress2 = (RadTextBox)FormView1.FindControl("txtSecondaryAddress2");
        RadTextBox txtSecondaryCity = (RadTextBox)FormView1.FindControl("txtSecondaryCity");
        RadTextBox txtSecondaryPostal = (RadTextBox)FormView1.FindControl("txtSecondaryPostal");
        RadDropDownList ddlSecondaryState = (RadDropDownList)FormView1.FindControl("ddlSecondaryState");
        RadDropDownList ddlSecondaryCountry = (RadDropDownList)FormView1.FindControl("ddlSecondaryCountry");

        if (CheckBox2.Checked == true)
        {
            //copy contents from the primary to the secondary
            txtSecondaryAddress1.Text = txtPrimaryAddress1.Text;
            txtSecondaryAddress2.Text = txtPrimaryAddress2.Text;
            txtSecondaryCity.Text = txtPrimaryCity.Text;
            txtSecondaryPostal.Text = txtPrimaryPostal.Text;
            // Secondary State Dropdown - EDIT
            ddlSecondaryState.ClearSelection();
            //foreach (DropDownListItem item in ddlSecondaryState.Items)
            //{
            //    if (item.Value.Equals(ddlPrimaryState.SelectedValue.ToString()))
            //    {
            //        item.Selected = true;
            //    }
            //}
            ddlSecondaryState.SelectedText = ddlPrimaryState.SelectedText;


        }
    }
    protected void ddlPrimaryCountry_SelectedIndexChanged(object sender, DropDownListEventArgs e)
    {
        RadDropDownList ddlPrimaryCountry = (RadDropDownList)FormView1.FindControl("ddlPrimaryCountry");
        Panel pnlUS = (Panel)FormView1.FindControl("pnlUS");
        Panel pnlNonUS = (Panel)FormView1.FindControl("pnlNonUS");

        if (ddlPrimaryCountry.SelectedValue != "US")
        {
            pnlUS.Visible = false;
            pnlNonUS.Visible = true;
        }
        else
        {
            pnlUS.Visible = true;
            pnlNonUS.Visible = false;
        }
    }
    protected void ddlPrimaryCountry2_SelectedIndexChanged(object sender, DropDownListEventArgs e)
    {
        RadDropDownList ddlPrimaryCountry = (RadDropDownList)FormView1.FindControl("ddlSecondaryCountry");
        Panel pnlUS = (Panel)FormView1.FindControl("pnlUS0");
        Panel pnlNonUS = (Panel)FormView1.FindControl("pnlNonUS0");

        if (ddlPrimaryCountry.SelectedValue != "US")
        {
            pnlUS.Visible = false;
            pnlNonUS.Visible = true;
        }
        else
        {
            pnlUS.Visible = true;
            pnlNonUS.Visible = false;
        }
    }
    protected void UpdateButton_Click(object sender, EventArgs e)
    {

    }
    protected void ddlPrimaryState_SelectedIndexChanged(object sender, DropDownListEventArgs e)
    {
        RadDropDownList ddlPrimaryCountry = (RadDropDownList)FormView1.FindControl("ddlPrimaryCountry");
        RadDropDownList ddlPrimaryState = (RadDropDownList)FormView1.FindControl("ddlPrimaryState");
        if (ddlPrimaryCountry.SelectedValue != "US")
        {
            //pnlUS.Visible = false;
            //pnlNonUS.Visible = true;
            // Primary State Dropdown - EDIT
            ddlPrimaryState.ClearSelection();
            foreach (DropDownListItem item in ddlPrimaryState.Items)
            {
                if (item.Value.Equals(""))
                {
                    item.Selected = true;
                }
            }

        }
        else
        {
            //pnlUS.Visible = true;
            //pnlNonUS.Visible = false;
            //Clear the value in textbox 
        }
    }
    protected void txtPrimaryFirst_TextChanged(object sender, EventArgs e)
    {

        RadTextBox txtPrimaryFirst = (RadTextBox)FormView1.FindControl("txtPrimaryFirst");

       // RadAjaxPanel1.EnableAJAX = txtPrimaryFirst.AutoPostBack;
        string myString = txtPrimaryFirst.Text;
        TextInfo myTI = new CultureInfo("en-US", false).TextInfo;
        string a = myTI.ToLower(myString);
        txtPrimaryFirst.Text = myTI.ToTitleCase(a);
       
    }
    protected void FormView1_ItemInserting(object sender, FormViewInsertEventArgs e)
    {
        RadMaskedTextBox number = (RadMaskedTextBox)FormView1.FindControl("txtSDPNumber");
        Label lblError = (Label)FormView1.FindControl("lblError");

        if (Collector.DoesIDExist(Convert.ToInt32(number.Text.ToString())))
        {
            lblError.Text = "Duplicate Collector ID detected.";
            e.Cancel = true;
        }
        else
        {
            lblError.Text = "";
            e.Cancel = false;
        }

     

   
    }
   
}