using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

public partial class Modules_RigTrack_ManageCompany : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ddlCountry.DataSource = RigTrack.DatabaseObjects.RigTrackDO.GetAllCountries();
            ddlCountry.DataTextField = "Name";
            ddlCountry.DataValueField = "ID";
            ddlCountry.DataBind();
            ddlCountry.SelectedValue = "1220";
            ddlCountry_SelectedIndexChanged(null, null);

            ddlState.DataSource = RigTrack.DatabaseObjects.RigTrackDO.GetAllStates();
            ddlState.DataTextField = "Name";
            ddlState.DataValueField = "ID";
            ddlState.DataBind();


            Button btn = (Button)Page.Master.FindControl("btnClose");
            Label lb1 = (Label)Page.Master.FindControl("lbl_welcomename");
            LinkButton lb2 = (LinkButton)Page.Master.FindControl("lnk_logout");
            Label lb3 = (Label)Page.Master.FindControl("lbl_role");
            LinkButton lb4 = (LinkButton)Page.Master.FindControl("lnk_myaccount");
            Label lb5 = (Label)Page.Master.FindControl("lbl_welcomeStatic");
            Label lb6 = (Label)Page.Master.FindControl("lbl_roleStatic");
            RadMenu menu = (RadMenu)Page.Master.FindControl("RadMenu1");

            
            
            
            if (Request.QueryString["Modal"] != null)
            {
                if (Request.QueryString["Modal"].ToString() == "true")
                {
                    btnClose.Visible = true;
                    btn.Visible = true;

                   
                    lb1.Visible = false;
                    lb2.Visible = false;
                    lb3.Visible = false;
                    lb4.Visible = false;
                    lb5.Visible = false;
                    lb6.Visible = false;
                    menu.Visible = false;
                    Page.Master.FindControl("lnk_password").Visible = false;
                }
                else
                {
                    btnClose.Visible = false;
                    btn.Visible = false;

                    lb1.Visible = true;
                    lb2.Visible = true;
                    lb3.Visible = true;
                    lb4.Visible = true;
                    lb5.Visible = true;
                    lb6.Visible = true;
                    menu.Visible = true;
                    Page.Master.FindControl("lnk_password").Visible = true;
                    
                }
            }
        }
    }

    protected void btnClear_Click(object sender, EventArgs e)
    {
        ClearText();
    }

    protected void btnCreate_Click(object sender, EventArgs e)
    {
        RigTrack.DatabaseTransferObjects.CompanyDTO companyDTO = new RigTrack.DatabaseTransferObjects.CompanyDTO();

        companyDTO.ID = 0;
        companyDTO.CompanyName = txtCompanyName.Text;
        companyDTO.CompanyAddress1 = txtCompanyAddressOne.Text;
        companyDTO.CompanyAddress2 = txtCompanyAddressTwo.Text;
        companyDTO.CompanyContactFirstName = txtContactFirstName.Text;
        companyDTO.CompanyContactLastName = txtContactLastName.Text;
        companyDTO.ContactPhone = txtContactNumber.Text;
        companyDTO.ContactEmail = txtEmail.Text;
        companyDTO.City = txtCity.Text;
        companyDTO.StateID = Int32.Parse(ddlState.SelectedValue);
        companyDTO.CountryID = Int32.Parse(ddlCountry.SelectedValue);
        companyDTO.Zip = txtZipcode.Text;
        if (rbActive.Checked)
        {
            companyDTO.isActive = true;
        }
        else
        {
            companyDTO.isActive = false;
        }

        if (AttachmentUpload.UploadedFiles.Count > 0)
        {
            companyDTO.isAttachment = true;
        }
        else
        {
            companyDTO.isAttachment = false;
        }

        int companyID = RigTrack.DatabaseObjects.RigTrackDO.InsertUpdateCompany(companyDTO);

        if (AttachmentUpload.UploadedFiles.Count > 0)
        {
            foreach (UploadedFile file in AttachmentUpload.UploadedFiles)
            {
                string FileName1 = file.GetName();

                byte[] FileBytes = new byte[file.InputStream.Length];
                file.InputStream.Read(FileBytes, 0, FileBytes.Length);
                string Extension1 = file.GetExtension();
                string FileType = null;

                switch (Extension1)
                {
                    case ".doc":
                        FileType = "application/vnd.ms-word";
                        break;
                    case ".docx":
                        FileType = "application/vnd.ms-word";
                        break;
                    case ".xls":
                        FileType = "application/vnd.ms-excel";
                        break;
                    case ".xlsx":
                        FileType = "application/vnd.ms-excel";
                        break;
                    case ".jpg":
                        FileType = "image/jpg";
                        break;
                    case ".png":
                        FileType = "image/png";
                        break;
                    case ".gif":
                        FileType = "image/gif";
                        break;
                    case ".pdf":
                        FileType = "application/pdf";
                        break;
                }

                int attachmentID = RigTrack.DatabaseObjects.RigTrackDO.AttachCompanyLogo(FileName1, FileBytes, FileType, companyID);
            }
        }

        DataTable dt = RigTrack.DatabaseObjects.RigTrackDO.GetCompany();
        RadGridCompany.DataSource = dt;
        RadGridCompany.DataBind();

        ClearText();
    }

    protected void RadGridCompany_ItemCreated(object sender, Telerik.Web.UI.GridItemEventArgs e)
    {

    }

    protected void RadGridCompany_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
    {
        DataTable dt = RigTrack.DatabaseObjects.RigTrackDO.GetCompany();
        RadGridCompany.DataSource = dt;
    }

    protected void RadGridCompany_UpdateCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
    {
        RigTrack.DatabaseTransferObjects.CompanyDTO companyDTO = new RigTrack.DatabaseTransferObjects.CompanyDTO();

        if(e.Item is GridEditableItem && e.Item.IsInEditMode)
        {
            GridEditableItem item = e.Item as GridEditableItem;

            companyDTO.ID = Int32.Parse(((TextBox)item["ID"].Controls[0]).Text);
            companyDTO.CompanyName = ((TextBox)item["CompanyName"].Controls[0]).Text;
            companyDTO.CompanyAddress1 = ((TextBox)item["CompanyAddress1"].Controls[0]).Text;
            companyDTO.CompanyAddress2 = ((TextBox)item["CompanyAddress2"].Controls[0]).Text;
            companyDTO.City = ((TextBox)item["City"].Controls[0]).Text;
            companyDTO.Zip = ((TextBox)item["Zip"].Controls[0]).Text;
            companyDTO.CompanyContactFirstName = ((TextBox)item["CompanyContactFirstName"].Controls[0]).Text;
            companyDTO.CompanyContactLastName = ((TextBox)item["CompanyContactLastName"].Controls[0]).Text;
            companyDTO.ContactPhone = ((TextBox)item["ContactPhone"].Controls[0]).Text;
            companyDTO.ContactEmail = ((TextBox)item["ContactEmail"].Controls[0]).Text;
            companyDTO.StateID = Int32.Parse(((RadDropDownList)item.FindControl("ddlState")).SelectedValue);
            companyDTO.CountryID = Int32.Parse(((RadDropDownList)item.FindControl("ddlCountry")).SelectedValue);

            RadAsyncUpload gridAttachmentUpload = item.FindControl("GridAttachmentUpload") as RadAsyncUpload;
            if (gridAttachmentUpload.UploadedFiles.Count > 0 || ((TextBox)item["isAttachment"].Controls[0]).Text == "True")
            {
                companyDTO.isAttachment = true;
            }
            else
            {
                companyDTO.isAttachment = false;
            }

            RadioButton rbActive = item.FindControl("rbGridActive") as RadioButton;
            RadioButton rbInactive = item.FindControl("rbGridInactive") as RadioButton;
            if (rbActive.Checked)
            {
                companyDTO.isActive = true;
            }
            else if (rbInactive.Checked)
            {
                companyDTO.isActive = false;
            }

            int companyID = RigTrack.DatabaseObjects.RigTrackDO.InsertUpdateCompany(companyDTO);

            
            if (gridAttachmentUpload.UploadedFiles.Count > 0)
            {
                foreach (UploadedFile file in gridAttachmentUpload.UploadedFiles)
                {
                    string FileName1 = file.GetName();
                    byte[] FileBytes = new byte[file.InputStream.Length];
                    file.InputStream.Read(FileBytes, 0, FileBytes.Length);
                    string Extension1 = file.GetExtension();
                    string FileType = null;

                    switch (Extension1)
                    {
                        case ".doc":
                            FileType = "application/vnd.ms-word";
                            break;
                        case ".docx":
                            FileType = "application/vnd.ms-word";
                            break;
                        case ".xls":
                            FileType = "application/vnd.ms-excel";
                            break;
                        case ".xlsx":
                            FileType = "application/vnd.ms-excel";
                            break;
                        case ".jpg":
                            FileType = "image/jpg";
                            break;
                        case ".png":
                            FileType = "image/png";
                            break;
                        case ".gif":
                            FileType = "image/gif";
                            break;
                        case ".pdf":
                            FileType = "application/pdf";
                            break;
                    }

                    int attachmentID = RigTrack.DatabaseObjects.RigTrackDO.AttachCompanyLogo(FileName1, FileBytes, FileType, companyID);
                }
            }
        }

        ClearText();
    }

    protected void RadGridCompany_ItemDataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
    {
        if (e.Item is GridEditableItem && e.Item.IsInEditMode)
        {
            GridEditableItem item = e.Item as GridEditableItem;

            RadDropDownList ddl = item.FindControl("ddlState") as RadDropDownList;
            DataTable stateDT = RigTrack.DatabaseObjects.RigTrackDO.GetAllStates();
            ddl.DataSource = RigTrack.DatabaseObjects.RigTrackDO.GetAllStates();
            ddl.DataTextField = "Name";
            ddl.DataValueField = "ID";
            ddl.DataBind();
            ddl.SelectedText = (item["StateName"].Controls[0] as TextBox).Text;
            ddl.Width = Unit.Pixel(120);

            RadDropDownList ddl2 = item.FindControl("ddlCountry") as RadDropDownList;
            DataTable countryDT = RigTrack.DatabaseObjects.RigTrackDO.GetAllCountries();
            ddl2.DataSource = RigTrack.DatabaseObjects.RigTrackDO.GetAllCountries();
            ddl2.DataTextField = "Name";
            ddl2.DataValueField = "ID";
            ddl2.DataBind();
            ddl2.SelectedText = (item["CountryName"].Controls[0] as TextBox).Text;
            ddl2.Width = Unit.Pixel(140);

            TextBox txtGridID = (TextBox)item["ID"].Controls[0];
            TextBox txtGridCompanyName = (TextBox)item["CompanyName"].Controls[0];
            TextBox txtGridCompanyAddress1 = (TextBox)item["CompanyAddress1"].Controls[0];
            TextBox txtGridCompanyAddress2 = (TextBox)item["CompanyAddress2"].Controls[0];
            TextBox txtGridCity = (TextBox)item["City"].Controls[0];
            TextBox txtGridZipCode = (TextBox)item["Zip"].Controls[0];
            TextBox txtGridContactFirstName = (TextBox)item["CompanyContactFirstName"].Controls[0];
            TextBox txtGridContactLastName = (TextBox)item["CompanyContactLastName"].Controls[0];
            TextBox txtGridContactPhone = (TextBox)item["ContactPhone"].Controls[0];
            TextBox txtGridContactEmail = (TextBox)item["ContactEmail"].Controls[0];

            txtGridID.Width = Unit.Pixel(100);
            txtGridCompanyName.Width = Unit.Pixel(105);
            txtGridCompanyAddress1.Width = Unit.Pixel(105);
            txtGridCompanyAddress2.Width = Unit.Pixel(70);
            txtGridCity.Width = Unit.Pixel(105);
            txtGridZipCode.Width = Unit.Pixel(70);
            txtGridContactFirstName.Width = Unit.Pixel(70);
            txtGridContactLastName.Width = Unit.Pixel(70);
            txtGridContactPhone.Width = Unit.Pixel(105);
            txtGridContactEmail.Width = Unit.Pixel(160);

            RadioButton rbActive = item.FindControl("rbGridActive") as RadioButton;
            RadioButton rbInactive = item.FindControl("rbGridInactive") as RadioButton;

            if (((TextBox)item["isActive"].Controls[0]).Text == "True")
            {
                rbActive.Checked = true;
            }
            else
            {
                rbInactive.Checked = true;
            }
            
        }

        if (e.Item is GridEditableItem && e.Item.IsInEditMode == false)
        {
            GridEditableItem item = (GridEditableItem)e.Item;
            Label lblIsAttachment = item.FindControl("lblAttachment") as Label;
            Label lblIsActive = item.FindControl("lblActive") as Label;
            if (lblIsAttachment.Text == "True")
            {
                lblIsAttachment.Text = "Yes";
            }
            else if (lblIsAttachment.Text == "False")
            {
                lblIsAttachment.Text = "No";
            }
            if (lblIsActive.Text == "True")
            {
                lblIsActive.Text = "Active";
            }
            else if (lblIsActive.Text == "False")
            {
                lblIsActive.Text = "Inactive";
            }
        }
    }

    protected void RadGridCompany_ItemCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
    {

    }

    protected void ddlCountry_SelectedIndexChanged(object sender, DropDownListEventArgs e)
    {
        if (ddlCountry.SelectedValue == "1220")
        {
            ddlState.Enabled = true;
        }
        else
        {
            ddlState.Enabled = false;
        }
    }

    protected void ClearText()
    {
        txtCompanyName.Text = "";
        txtCompanyAddressOne.Text = "";
        txtCompanyAddressTwo.Text = "";
        txtContactFirstName.Text = "";
        txtContactLastName.Text = "";
        txtContactNumber.Text = "";
        txtEmail.Text = "";
        txtCity.Text = "";
        ddlState.SelectedValue = "0";
        ddlCountry.SelectedValue = "1220";
        ddlCountry_SelectedIndexChanged(null, null);

        txtZipcode.Text = "";
        rbActive.Checked = true;
        rbInactive.Checked = false;

        DataTable dt = RigTrack.DatabaseObjects.RigTrackDO.GetCompany();
        RadGridCompany.DataSource = dt;
        RadGridCompany.DataBind();
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        RigTrack.DatabaseTransferObjects.CompanyDTO companyDTO = new RigTrack.DatabaseTransferObjects.CompanyDTO();
        companyDTO.CompanyName = txtCompanyName.Text;
        companyDTO.City = txtCity.Text;
        companyDTO.StateID = Int32.Parse(ddlState.SelectedValue);

        DataTable dt = RigTrack.DatabaseObjects.RigTrackDO.SearchCompany(companyDTO);
        RadGridCompany.DataSource = dt;
        RadGridCompany.DataBind();
    }
}