using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using System.Collections;
using System.Diagnostics;

public partial class Modules_Configuration_Manager_confMngrManageConstants : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
    }

    protected void btnCreateConstant_Click(object sender, EventArgs e)
    {
        //sqlGetUpdateConstants.Insert();
        //RadTabStrip1.SelectedIndex = RadTabStrip1.SelectedIndex + 1;
    }

    protected string crunchWord(string theSentance, int rmLength = 30)
    {
        if ((int)theSentance.Length > (int)rmLength)
        {
            return theSentance.Remove(rmLength) + "...";
        }
        else
        {
            return theSentance;
        }
    }

    protected void saveFlag(object sender, EventArgs e)
    {
        string newFlagName_text = ( String.IsNullOrWhiteSpace( newFlagName.Text)? "Empty": newFlagName.Text );
        string newFlagColor_text = ( String.IsNullOrEmpty( System.Drawing.ColorTranslator.ToHtml(newFlagColor.SelectedColor).ToString() )? "#fff": System.Drawing.ColorTranslator.ToHtml(newFlagColor.SelectedColor).ToString());

        editSaveEvent.InsertParameters.Add("flagName", newFlagName_text); //name
        editSaveEvent.InsertParameters.Add("flagColor", newFlagColor_text); //color

        //LOG EVENT OPTIONS CHANGES.. 
        auditLog logChange = new auditLog(Session["client_database"].ToString());
        logChange.addValue(new Dictionary<string, string> { 
            { "pageId", "21" }, 
            { "userId", ((ClientMaster)Session["UserMasterDetails"]).UserID.ToString() }, 
            { "attributeName", "event options settings created" }, 
            { "description", "event options setting created" }
        }, true);
        
        editSaveEvent.Insert();
        radListSelect.DataBind();
    }

    protected void UpdateEvent(object sender, EventArgs e)
    {
        GridEditFormItem row = (GridEditFormItem)(((Button)sender).NamingContainer);

        RadTextBox flagNameTextbox = (RadTextBox)row.FindControl("flagName");
        RadColorPicker flagColorTextbox = (RadColorPicker)row.FindControl("flagColor");
        Label tmpColorTextbox = (Label)row.FindControl("tmpColor");
        Label flagIdTextbox = (Label)row.FindControl("flagId");

        try
        {
            flagColorTextbox.SelectedColor = System.Drawing.ColorTranslator.FromHtml(tmpColorTextbox.Text);
        }
        catch (Exception ex)
        {
        }
    }

    protected void UpdateEvent(object sender, GridCommandEventArgs e)
    {
    }

    protected void radListSelect_ItemDataBound(object sender, GridItemEventArgs e)
    {
        if (e.Item is GridEditFormItem)
        {
            GridEditFormItem row = (GridEditFormItem)e.Item;

            RadColorPicker flagColorTextbox = (RadColorPicker)row.FindControl("flagColor");
            Label tmpColorTextbox = (Label)row.FindControl("tmpColor");
            try
            {
                flagColorTextbox.SelectedColor = System.Drawing.ColorTranslator.FromHtml(tmpColorTextbox.Text);
            }
            catch (Exception ex)
            {
            }
        }

        if (e.Item is GridDataItem)
        {

            GridDataItem dataItem = (GridDataItem)e.Item;
            TableCell myCell = dataItem["flagName"];
            myCell.Width = 100;

            myCell = dataItem["flagColor"];
            myCell.BackColor = System.Drawing.ColorTranslator.FromHtml(myCell.Text);
            myCell.ForeColor = System.Drawing.ColorTranslator.FromHtml(myCell.Text);
            myCell.Width=80;

        }
    }

    protected void radListSelect_ItemCommand(object sender, GridCommandEventArgs e)
    {
        if (e.CommandName == "Update")
        {
            if (e.Item is GridEditFormItem)
            {
                GridEditFormItem row = (GridEditFormItem)e.Item;

                Label lblflagId = (Label)row.FindControl("flagId");
                RadTextBox flagNameTextBox = (RadTextBox)row.FindControl("flagName");
                RadColorPicker flagColor = (RadColorPicker)row.FindControl("flagColor");

                editSaveEvent.UpdateParameters.Add( "id", lblflagId.Text ); //id
                editSaveEvent.UpdateParameters.Add( "flagName", flagNameTextBox.Text); //name
                editSaveEvent.UpdateParameters.Add( "flagColor", System.Drawing.ColorTranslator.ToHtml(flagColor.SelectedColor).ToString()); //color

                //LOG EVENT OPTIONS CHANGES.. 
                auditLog logChange = new auditLog(Session["client_database"].ToString());
                logChange.addValue(new Dictionary<string, string> { 
                    { "pageId", "21" }, 
                    { "userId", ((ClientMaster)Session["UserMasterDetails"]).UserID.ToString() }, 
                    { "attributeName", "event options settings updated" }, 
                    { "description", "event options setting updated" }
                }, true);
            }

            editSaveEvent.Update();
            radListSelect.DataBind();
        }
    }
}