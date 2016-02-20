<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/RigTrack.master" AutoEventWireup="true" CodeFile="version.aspx.cs" Inherits="version" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="customCss" Runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<h3>MDM</h3>
<p>MDM Version 3.0.0</p>
<h3>SQL Server</h3>
    <telerik:RadGrid ID="RadGrid1" runat="server" AllowPaging="True" 
        CellSpacing="0" DataSourceID="SqlDataSource1" GridLines="None" Height="70px">


        <ClientSettings>
            <Scrolling AllowScroll="True" UseStaticHeaders="True" />
        </ClientSettings>

<MasterTableView DataSourceID="SqlDataSource1">
<CommandItemSettings ExportToPdfText="Export to PDF"></CommandItemSettings>

<RowIndicatorColumn Visible="True" FilterControlAltText="Filter RowIndicator column">
<HeaderStyle Width="20px"></HeaderStyle>
</RowIndicatorColumn>

<ExpandCollapseColumn Visible="True" FilterControlAltText="Filter ExpandColumn column">
<HeaderStyle Width="20px"></HeaderStyle>
</ExpandCollapseColumn>

<EditFormSettings>
<EditColumn FilterControlAltText="Filter EditCommandColumn column"></EditColumn>
</EditFormSettings>




<PagerStyle PageSizeControlType="RadComboBox"></PagerStyle>
</MasterTableView>

<PagerStyle PageSizeControlType="RadComboBox"></PagerStyle>

<FilterMenu EnableImageSprites="False"></FilterMenu>
    </telerik:RadGrid>

    <asp:SqlDataSource ID="SqlDataSource1" runat="server"  
        ConnectionString="<%$ ConnectionStrings:MDM_Staging_Client_1ConnectionString %>"  
        SelectCommand="Select @@Version as [SQL Version]"></asp:SqlDataSource>
        <br />
        
       
          
            

<h3>MDM Databases</h3>
            <telerik:RadGrid ID="RadGrid3" runat="server" AllowPaging="True" 
        CellSpacing="0" DataSourceID="SqlDataSource3" GridLines="None" Height="220px" >


        <ClientSettings>
            <Scrolling AllowScroll="True" UseStaticHeaders="True" />
        </ClientSettings>

<MasterTableView DataSourceID="SqlDataSource3">
<CommandItemSettings ExportToPdfText="Export to PDF"></CommandItemSettings>

<RowIndicatorColumn Visible="True" FilterControlAltText="Filter RowIndicator column">
<HeaderStyle Width="20px"></HeaderStyle>
</RowIndicatorColumn>

<ExpandCollapseColumn Visible="True" FilterControlAltText="Filter ExpandColumn column">
<HeaderStyle Width="20px"></HeaderStyle>
</ExpandCollapseColumn>

<EditFormSettings>
<EditColumn FilterControlAltText="Filter EditCommandColumn column"></EditColumn>
</EditFormSettings>




<PagerStyle PageSizeControlType="RadComboBox"></PagerStyle>
</MasterTableView>

<PagerStyle PageSizeControlType="RadComboBox"></PagerStyle>

<FilterMenu EnableImageSprites="False"></FilterMenu>
    </telerik:RadGrid>

    <asp:SqlDataSource ID="SqlDataSource3" runat="server"  
        ConnectionString="<%$ ConnectionStrings:MDM_Staging_Client_1ConnectionString %>"  
        SelectCommand="select db_name(dbid) as Name,str(convert(dec(15),sum(size))* 8192/ 1048576,10,2)+ N' MB' as Size from sys.sysaltfiles 
WHERE Name LIKE '%MDM%' group by dbid   order by 2 desc"></asp:SqlDataSource>
<br />
 <asp:Timer ID="Timer1" Interval="10000" runat="server" 
        ontick="Timer1_Tick" />
        <asp:UpdatePanel runat="server" ID="pnlTik" UpdateMode="Conditional">
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="Timer1" EventName="Tick" />
            </Triggers>
            <ContentTemplate>
            <h3>Operating System</h3>
            <telerik:RadGrid ID="RadGrid2" runat="server" AllowPaging="True" 
        CellSpacing="0" DataSourceID="SqlDataSource2" GridLines="None" Height="70px" >


        <ClientSettings>
            <Scrolling AllowScroll="True" UseStaticHeaders="True" />
        </ClientSettings>

<MasterTableView DataSourceID="SqlDataSource2">
<CommandItemSettings ExportToPdfText="Export to PDF"></CommandItemSettings>

<RowIndicatorColumn Visible="True" FilterControlAltText="Filter RowIndicator column">
<HeaderStyle Width="20px"></HeaderStyle>
</RowIndicatorColumn>

<ExpandCollapseColumn Visible="True" FilterControlAltText="Filter ExpandColumn column">
<HeaderStyle Width="20px"></HeaderStyle>
</ExpandCollapseColumn>

<EditFormSettings>
<EditColumn FilterControlAltText="Filter EditCommandColumn column"></EditColumn>
</EditFormSettings>




<PagerStyle PageSizeControlType="RadComboBox"></PagerStyle>
</MasterTableView>

<PagerStyle PageSizeControlType="RadComboBox"></PagerStyle>

<FilterMenu EnableImageSprites="False"></FilterMenu>
    </telerik:RadGrid>

    <asp:SqlDataSource ID="SqlDataSource2" runat="server"  
        ConnectionString="<%$ ConnectionStrings:MDM_Staging_Client_1ConnectionString %>"  
        SelectCommand="SELECT
[ms_ticks] AS ms_since_restart,
[ms_ticks]/1000 AS seconds_since_restart,
CAST([ms_ticks]/1000/60.0 AS DECIMAL(15,2)) AS minutes_since_restart,
CAST([ms_ticks]/1000/60/60.0 AS DECIMAL(15,2)) AS hours_since_restart,
CAST([ms_ticks]/1000/60/60/24.0 AS DECIMAL(15,2)) AS days_since_restart,
DATEADD(s,((-1)*([ms_ticks]/1000)),GETDATE()) AS time_of_last_restart
FROM sys.[dm_os_sys_info];"></asp:SqlDataSource>
            </ContentTemplate>
        </asp:UpdatePanel>
            
            <br />
            <h3>SQL Activity</h3>
             <asp:Timer ID="Timer2" Interval="6000" runat="server" 
        ontick="Timer2_Tick" />
        <asp:UpdatePanel runat="server" ID="UpdatePanel2" UpdateMode="Conditional">
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="Timer2" EventName="Tick" />
            </Triggers>
            <ContentTemplate>
            <telerik:RadGrid ID="RadGrid4" runat="server" AllowPaging="True" 
        CellSpacing="0" DataSourceID="SqlDataSource4" GridLines="None">


        <ClientSettings>
            <Scrolling AllowScroll="True" UseStaticHeaders="True" />
        </ClientSettings>

<MasterTableView DataSourceID="SqlDataSource4">
<CommandItemSettings ExportToPdfText="Export to PDF"></CommandItemSettings>

<RowIndicatorColumn Visible="True" FilterControlAltText="Filter RowIndicator column">
<HeaderStyle Width="20px"></HeaderStyle>
</RowIndicatorColumn>

<ExpandCollapseColumn Visible="True" FilterControlAltText="Filter ExpandColumn column">
<HeaderStyle Width="20px"></HeaderStyle>
</ExpandCollapseColumn>

<EditFormSettings>
<EditColumn FilterControlAltText="Filter EditCommandColumn column"></EditColumn>
</EditFormSettings>




<PagerStyle PageSizeControlType="RadComboBox"></PagerStyle>
</MasterTableView>

<PagerStyle PageSizeControlType="RadComboBox"></PagerStyle>

<FilterMenu EnableImageSprites="False"></FilterMenu>
    </telerik:RadGrid>

    <asp:SqlDataSource ID="SqlDataSource4" runat="server"  
        ConnectionString="<%$ ConnectionStrings:MDM_Staging_Client_1ConnectionString %>"  
        SelectCommand="Declare @dbName varchar(1000)
set @dbName='MDM'

;WITH DBConn(SPID,[Status],[Login],HostName,DBName,Command,LastBatch,ProgramName)
As
(
SELECT 
    SPID = s.session_id,
    Status = UPPER(COALESCE
        (
            r.status,
            ot.task_state,
            s.status, 
        '')),
    [Login] = s.login_name,
    HostName = COALESCE
        (
            s.[host_name],
            '  .'
        ),
    DBName = COALESCE
        (
            DB_NAME(COALESCE
            (
                r.database_id,
                t.database_id
            )),
            ''
        ),
    Command = COALESCE
        (
            r.Command,
            r.wait_type,
            wt.wait_type,
            r.last_wait_type,
            ''
        ),
    LastBatch = COALESCE
        (
            r.start_time,
            s.last_request_start_time
        ),
    ProgramName = COALESCE
        (
            s.program_name, 
            ''
        )
FROM
    sys.dm_exec_sessions s
LEFT OUTER JOIN
    sys.dm_exec_requests r
ON
    s.session_id = r.session_id
LEFT OUTER JOIN
    sys.dm_exec_connections c
ON
    s.session_id = c.session_id
LEFT OUTER JOIN
(
    SELECT 
        request_session_id,
        database_id = MAX(resource_database_id)
    FROM
        sys.dm_tran_locks
    GROUP BY
        request_session_id
) t
ON
    s.session_id = t.request_session_id
LEFT OUTER JOIN
    sys.dm_os_waiting_tasks wt
ON 
    s.session_id = wt.session_id
LEFT OUTER JOIN
    sys.dm_os_tasks ot
ON 
    s.session_id = ot.session_id
LEFT OUTER JOIN
(
    SELECT
        ot.session_id,
        CPU_Time = MAX(usermode_time)
    FROM
        sys.dm_os_tasks ot
    INNER JOIN
        sys.dm_os_workers ow
    ON
        ot.worker_address = ow.worker_address
    INNER JOIN
        sys.dm_os_threads oth
    ON
        ow.thread_address = oth.thread_address
    GROUP BY
        ot.session_id
) tt
ON
    s.session_id = tt.session_id
WHERE
    COALESCE
    (
        r.command,
        r.wait_type,
        wt.wait_type,
        r.last_wait_type,
        'a'
    ) >= COALESCE
    (
        '', 
        'a'
    )
)

Select * from DBConn
where DBName like '%'+@dbName+'%' AND DBName NOT Like '%TFS%' AND Command <> '' AND Login <> 'sa' AND ProgramName <> 'Microsoft SQL Server Management Studio' order by LastBatch desc"></asp:SqlDataSource>
  </ContentTemplate>
        </asp:UpdatePanel>
        <br /><br /><br /><br />
    </asp:Content>

