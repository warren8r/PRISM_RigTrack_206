﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


public partial class version : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
       
    }
    protected void Timer1_Tick(object sender, EventArgs e)
    {
        RadGrid2.DataBind();
    }
    protected void Timer2_Tick(object sender, EventArgs e)
    {
        RadGrid4.DataBind();
    }
}