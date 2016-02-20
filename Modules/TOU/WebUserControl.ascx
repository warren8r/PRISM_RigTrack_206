<%@ Control Language="C#" ClassName="WebUserControl" %>

<script runat="server">
    public event EventHandler InsertAbove;
    public event EventHandler InsertBellow;
    public event EventHandler Remove;

    protected void InserAboveClick(object sender, EventArgs e)
    {
        if (InsertAbove != null)
        {
            InsertAbove(this, e);
        }
    }

    protected void InsertBellowClick(object sender, EventArgs e)
    {
        if (InsertBellow != null)
        {
            InsertBellow(this, e);
        }
    }

    protected void RemoveClick(object sender, EventArgs e)
    {
        if (Remove != null)
        {
            Remove(this, e);
        }
    }

    public string Text
    {
        get
        {
            return c_textBox.Text;
        }
    }
    
    public string PeakID
    {
        get
        {
            return c_ID.Text;
        }
        set
        {
            c_ID.Text = value;
        }
    }
    
    public string PeakName
    {
        get
        {
            return c_Name.Text;
        }
        set
        {
            c_Name.Text = value;
        }
    }

    public string StartHR
    {
        get
        {
            return c_textBox.Text;
        }
        set
        {
            c_textBox.Text = value;
        }
    }

    public string StopHR
    {
        get
        {
            return c_textBox2.Text;
        }
        set
        {
            c_textBox2.Text = value;
        }
    }
    
    protected void Page_Load(object sender, EventArgs e)
    {
        c_ID.Attributes.Add("readonly", "readonly");
        c_Name.Attributes.Add("readonly", "readonly");  
    }
</script>

<div>
    <span style="width: 200px;">
        <asp:TextBox runat="server" ID="c_ID" style="background-color:transparent; border: none; display:none;" />
        <asp:TextBox runat="server" ID="c_Name" style="background-color:transparent; border: none;" />
        <asp:TextBox runat="server" ID="c_textBox" />
        <asp:TextBox runat="server" ID="c_textBox2" />
        <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/images/delete.PNG" Height="12px" Width="12px" onclick="RemoveClick"></asp:ImageButton>
    </span><%--<span>
        <asp:Button runat="server" ID="c_insertAboveButton" Text="Insert Above" OnClick="InserAboveClick" />
        <asp:Button runat="server" ID="c_insertBellowButton" Text="Insert Bellow" OnClick="InsertBellowClick" />
        <asp:Button runat="server" ID="c_removeButtom" Text="Remove" OnClick="RemoveClick" />
    </span>--%>
</div>