using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

/// <summary>
/// Summary description for RadComboBoxFill
/// </summary>
public class RadComboBoxFill
{
	public RadComboBoxFill()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    public static void FillRadcombobox(Telerik.Web.UI.RadComboBox dd, DataTable dt, string textField, string valueField, string defaultValue)
    {
        DataRow drw = dt.NewRow();
        drw[textField] = "Select";
        drw[valueField] = defaultValue;
        dt.Rows.InsertAt(drw, 0);
        dd.DataTextField = textField;
        dd.DataValueField = valueField;
        dd.DataSource = dt;
        dd.DataBind();
        dd.SelectedIndex = 0;
    }
    public static void FillRadcombobox_ddl(Telerik.Web.UI.RadDropDownList dd, DataTable dt, string textField, string valueField, string defaultValue)
    {
        DataRow drw = dt.NewRow();
        drw[textField] = "Select";
        drw[valueField] = defaultValue;
        dt.Rows.InsertAt(drw, 0);
        dd.DataTextField = textField;
        dd.DataValueField = valueField;
        dd.DataSource = dt;
        dd.DataBind();
        dd.SelectedIndex = 0;
    }
    public static void FillRadcomboboxSelectALL(Telerik.Web.UI.RadComboBox dd, DataTable dt, string textField, string valueField, string defaultValue)
    {
        DataRow drw = dt.NewRow();
        drw[textField] = "Select All";
        drw[valueField] = defaultValue;
        dt.Rows.InsertAt(drw, 0);
        dd.DataTextField = textField;
        dd.DataValueField = valueField;
        dd.DataSource = dt;
        dd.DataBind();
        dd.SelectedIndex = 0;
    }
}