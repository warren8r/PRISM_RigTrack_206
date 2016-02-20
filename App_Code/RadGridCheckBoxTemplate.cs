using System;

using System.Web.UI;

using System.Web.UI.HtmlControls;

using System.Web.UI.WebControls;
using Telerik.Web.UI;
/// <summary>
/// Summary description for RadGridCheckBoxTemplate
/// </summary>
public class RadGridCheckBoxTemplate : System.Web.UI.ITemplate
{
	public RadGridCheckBoxTemplate()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    public void InstantiateIn(System.Web.UI.Control container)
    {

        CheckBox checkbox = new CheckBox();

        checkbox.ID = "download";

        

        container.Controls.Add(checkbox);

    }
}