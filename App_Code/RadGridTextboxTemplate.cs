using System;

using System.Web.UI;

using System.Web.UI.HtmlControls;

using System.Web.UI.WebControls;
using Telerik.Web.UI;

/// <summary>
/// Summary description for RadGridTextboxTemplate
/// </summary>
public class RadGridTextboxTemplate : System.Web.UI.ITemplate
{
	public RadGridTextboxTemplate()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    public void InstantiateIn(System.Web.UI.Control container)
    {

        TextBox txt = new TextBox();

        txt.ID = "txt_new";

        txt.Width = 50;

        container.Controls.Add(txt);

    }
}