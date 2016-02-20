<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/DialogMaster.master" AutoEventWireup="true" CodeFile="ManageUserDocsbyAdmin.aspx.cs" Inherits="ManageUserDocsbyAdmin" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentBody" Runat="Server">
    <asp:ScriptManager ID="ScriptManager1" 
            EnablePageMethods="true" 
            EnablePartialRendering="true" runat="server" />
            <%--<asp:UpdatePanel ID="up1" runat="server">
    <ContentTemplate>
<asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="up1">
        <ProgressTemplate>
            <div align="center" class="contactmain">
                <img src="loading1.gif" alt="Loading" /><br />
                <span style="color:Red">Loading Please Wait....</span>
             </div>
        </ProgressTemplate>
    </asp:UpdateProgress>--%>
    <table>
    <tr>
        <td>
            <telerik:RadProgressManager ID="Radprogressmanager1" runat="server"></telerik:RadProgressManager>

    <table border="0" width="100%">
        <tr>
            <td colspan="2">
                <table  border="0" width="100%">
                    <tr><td>Task Order Documents</td></tr>
                    <tr>
                    <td >
                        <telerik:RadGrid ID="RadGrid2" runat="server" AllowPaging="True" AllowSorting="True" CssClass="mdmGrid active"
                    CellSpacing="0" DataSourceID="SqlDataSource1" GridLines="None" Width="80%" OnItemCommand="RadGrid2_ItemCommand">
                    <ClientSettings AllowColumnsReorder="True" ReorderColumnsOnClient="True">
                        <Selecting AllowRowSelect="True" />
                    </ClientSettings>
                    <MasterTableView AutoGenerateColumns="False" DataKeyNames="EventTaskOrderDocID" DataSourceID="SqlDataSource1">
                
                        <Columns>
                    
                            <telerik:GridBoundColumn DataField="eventCode" FilterControlAltText="Filter eventDetail column"
                                HeaderText="eventCode" ReadOnly="True" SortExpression="eventCode"
                                UniqueName="eventCode">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="DocumentDisplayName" FilterControlAltText="Filter categoryName column"
                                HeaderText="Task Order Documents" SortExpression="DocumentDisplayName" UniqueName="DocumentDisplayName">
                            </telerik:GridBoundColumn> 
                            <telerik:GridTemplateColumn HeaderText="Download" >
                                <ItemTemplate> 
                                <asp:LinkButton ID="lnk_download" runat="server" CommandName="downloaddoc" Text="Download"></asp:LinkButton>                          
                                <asp:Label runat="server" ID="lbl_EventTaskOrderDocID" Text='<%# Eval("EventTaskOrderDocID") %>' style="display:none;"></asp:Label>
                                <asp:Label runat="server" ID="lbl_documentid" Text='<%# Eval("documentid") %>' style="display:none;"></asp:Label>
                                <asp:Label runat="server" ID="lbl_docname" Text='<%# Eval("DocumentName") %>' style="display:none;"></asp:Label>                         
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                            <telerik:GridTemplateColumn HeaderText="Mandatory Status" >
                                <ItemTemplate> 
                                
                                <%--<asp:Label runat="server" ID="lbl_manstatus" Text='<%# Eval("mandatoryDocument") %>' ></asp:Label>--%>  
                                <asp:Label ID="lbl_manstatus" runat="server" ForeColor='<%# (bool)Eval("mandatoryDocument") ? System.Drawing.Color.Green : System.Drawing.Color.Red %>' Text='<%# string.Format("{0}", (bool)Eval("mandatoryDocument") ? "Yes" : "No") %>'></asp:Label>                       
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>               
                        </Columns>
                        <EditFormSettings>
                            <EditColumn FilterControlAltText="Filter EditCommandColumn column">
                            </EditColumn>
                        </EditFormSettings>
                        <PagerStyle PageSizeControlType="RadComboBox"></PagerStyle>
                    </MasterTableView>
                    <PagerStyle PageSizeControlType="RadComboBox"></PagerStyle>
                    <FilterMenu EnableImageSprites="False">
                    </FilterMenu>
                </telerik:RadGrid>
                 <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:local_database %>"
                    SelectCommand="SELECT  etod.EventTaskOrderDocID,etod.documentid,etod.mandatoryDocument, e.eventCode,d.DocumentDisplayName,d.DocumentName from 
                    EventTaskOrderDocuments etod, events e, documents d where e.id=etod.eventCodeID and d.DocumentID=etod.DocumentID and e.id=@eventid order by etod.EventTaskOrderDocID desc">
                     <SelectParameters>
                         <asp:QueryStringParameter Name="eventid" QueryStringField="eventid" DefaultValue="1" DbType="Int32" />
                     </SelectParameters>
                </asp:SqlDataSource>
                    </td>
                </tr>
                </table>
            </td>
        </tr>

        <tr><td style="height:20px"></td></tr>
        
        
        <tr>
            <td colspan="2" align="left">
                <u>User Uploaded Documents</u>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <telerik:RadGrid ID="RadGrid1" runat="server" AllowPaging="True" AllowSorting="True" CssClass="mdmGrid active"
            CellSpacing="0" DataSourceID="eventsDocs" GridLines="None" Width="100%" OnItemCommand="RadGrid1_ItemCommand" OnItemDataBound="RadGrid1_ItemDataBound">
            <ClientSettings AllowColumnsReorder="True" ReorderColumnsOnClient="True">
                <Selecting AllowRowSelect="True" />
            </ClientSettings>
            <MasterTableView AutoGenerateColumns="False" DataKeyNames="EventUploadedDocID" DataSourceID="eventsDocs">
                
                <Columns>
                    <%--<telerik:GridTemplateColumn HeaderText="Remove" >
                        <ItemTemplate> 
                        <asp:LinkButton ID="lnk_remove" runat="server" CommandName="removedoc" Text="Remove" OnClientClick="return confirm('Are you certain you want to delete this document?');"></asp:LinkButton>                          
                                                 
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                    
                    <telerik:GridBoundColumn DataField="DocumentDisplayName" FilterControlAltText="Filter categoryName column"
                        HeaderText="Task Order Documents" SortExpression="DocumentDisplayName" UniqueName="DocumentDisplayName">
                    </telerik:GridBoundColumn> --%>
                    <telerik:GridBoundColumn DataField="name" FilterControlAltText="Filter name column"
                        HeaderText="User Name" SortExpression="name" UniqueName="name">
                    </telerik:GridBoundColumn>
                    
                    
                    <telerik:GridBoundColumn DataField="UploadedDate" FilterControlAltText="Filter UploadedDate column"
                        HeaderText="Uploaded Date" SortExpression="UploadedDate" UniqueName="UploadedDate">
                    </telerik:GridBoundColumn>
                    <telerik:GridTemplateColumn HeaderText="Uploaded Docs" >
                        <ItemTemplate> 
                        <asp:LinkButton ID="lnk_download" runat="server" CommandName="downloaddoc" Text='<%# Eval("UploadedDocument") %>'></asp:LinkButton>                          
                        <asp:Label runat="server" ID="lbl_EventTaskOrderDocID" Text='<%# Eval("EventUploadedDocID") %>' style="display:none;"></asp:Label>
                        <asp:Label runat="server" ID="lbl_documentid" Text='<%# Eval("UploadedDocumentId") %>' style="display:none;"></asp:Label>
                        <asp:Label runat="server" ID="lbl_docname" Text='<%# Eval("DocumentName") %>' style="display:none;"></asp:Label>                         
                        </ItemTemplate>
                    </telerik:GridTemplateColumn> 
                    <telerik:GridTemplateColumn HeaderText="Task Order Docs to download" >
                        <ItemTemplate> 
                        <asp:LinkButton ID="lnk_download_task" runat="server" CommandName="downloadtaskorderdoc" Text='<%# Eval("TaskOrderDocument") %>'></asp:LinkButton>                          
                        <%--<asp:Label runat="server" ID="lbl_EventTaskOrderDocID" Text='<%# Eval("EventUploadedDocID") %>' style="display:none;"></asp:Label>--%>
                        <asp:Label runat="server" ID="lbl_documentid_task" Text='<%# Eval("TaskOrderDocumentId") %>' style="display:none;"></asp:Label>
                        <asp:Label runat="server" ID="lbl_docname_task" Text='<%# Eval("TaskOrderDocumentName") %>' style="display:none;"></asp:Label>                         
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>  
                    <telerik:GridBoundColumn DataField="DocumentStatus" FilterControlAltText="Filter DocumentStatus column"
                        HeaderText="Document Status" ReadOnly="True" SortExpression="DocumentStatus"
                        UniqueName="DocumentStatus">
                    </telerik:GridBoundColumn>   
                    <telerik:GridTemplateColumn HeaderText="Accept/Reject" ItemStyle-Width="120px">
                            <ItemTemplate>
                                <%--<asp:CheckBox runat="server" ID="isChecked_a"   AutoPostBack="true" OnCheckedChanged="CheckChanged_a"  /><br />--%>
                                <asp:RadioButton ID="rd_accept" runat="server" Text="Accept" GroupName="s" AutoPostBack="true" OnCheckedChanged="CheckChanged_a" />
                                <asp:RadioButton ID="rd_reject" runat="server" ForeColor="Red" Text="Reject" GroupName="s" AutoPostBack="true" OnCheckedChanged="CheckChanged_r" />
                                <asp:Label ID="lbl_acceptstatus" runat="server" Text='<%# Eval("Closed") %>' style="display:none"></asp:Label>
                                
                            </ItemTemplate>
                            
                        </telerik:GridTemplateColumn>              
                </Columns>
                <EditFormSettings>
                    <EditColumn FilterControlAltText="Filter EditCommandColumn column">
                    </EditColumn>
                </EditFormSettings>
                <PagerStyle PageSizeControlType="RadComboBox"></PagerStyle>
            </MasterTableView>
            <PagerStyle PageSizeControlType="RadComboBox"></PagerStyle>
            <FilterMenu EnableImageSprites="False">
            </FilterMenu>
        </telerik:RadGrid>
         <asp:SqlDataSource ID="eventsDocs" runat="server" ConnectionString="<%$ ConnectionStrings:local_database %>"
            SelectCommand="SELECT  eud.EventUploadedDocID,eud.DocumentStatus,eud.Closed,eud.documentid as UploadedDocumentId,etod.documentid as TaskOrderDocumentId,eud.UserID,(u.firstName+' '+u.lastName) as name, eud.UploadedDate,  e.eventCode,d.DocumentDisplayName as UploadedDocument,d.DocumentName,d2.DocumentDisplayName as TaskOrderDocument, d2.DocumentName as TaskOrderDocumentName from 
            EventUploadedDocuments eud, EventTaskOrderDocuments etod, eventami e, documents d, documents d2, Users u where eud.EventTaskOrderDocID=etod.EventTaskOrderDocID 
            and u.userID=eud.UserID and e.id=eud.EventID and d.DocumentID=eud.DocumentID and d2.DocumentID=etod.DocumentID and e.id=@eventamiid order by eud.EventUploadedDocID desc">
             <SelectParameters>
                 <asp:QueryStringParameter Name="eventamiid" QueryStringField="eventamiid" DefaultValue="1" DbType="Int32" />
             </SelectParameters>
        </asp:SqlDataSource>
            </td>
        </tr>
        
    </table>

   

        </td>
    </tr>
</table>
<%--</ContentTemplate>
            
            </asp:UpdatePanel>--%>
</asp:Content>

