<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TaskLog.aspx.cs" Inherits="TaskLog"   MasterPageFile="~/Masters/DialogMaster.master"  %>

<asp:Content ID="Content1" ContentPlaceHolderID="contentBody" Runat="Server">
    <telerik:RadScriptManager ID="ajaxRequest" runat="server"
                EnablePageMethods="true" >
    </telerik:RadScriptManager>
    <h1>Task Log</h1>
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
                    <telerik:GridTemplateColumn HeaderText="Uploaded Doc" >
                        <ItemTemplate> 
                        <asp:LinkButton ID="lnk_download" runat="server" CommandName="downloaddoc" Text='<%# Eval("UploadedDocument") %>'></asp:LinkButton>                          
                        <asp:Label runat="server" ID="lbl_EventTaskOrderDocID" Text='<%# Eval("EventUploadedDocID") %>' style="display:none;"></asp:Label>
                        <asp:Label runat="server" ID="lbl_documentid" Text='<%# Eval("UploadedDocumentId") %>' style="display:none;"></asp:Label>
                        <asp:Label runat="server" ID="lbl_docname" Text='<%# Eval("DocumentName") %>' style="display:none;"></asp:Label>                         
                        </ItemTemplate>
                    </telerik:GridTemplateColumn> 
                    <telerik:GridTemplateColumn HeaderText="Task Order Doc to download" >
                        <ItemTemplate> 
                        <asp:LinkButton ID="lnk_download_task" runat="server" CommandName="downloadtaskorderdoc" Text='<%# Eval("TaskOrderDocument") %>'></asp:LinkButton>                          
                        <%--<asp:Label runat="server" ID="lbl_EventTaskOrderDocID" Text='<%# Eval("EventUploadedDocID") %>' style="display:none;"></asp:Label>--%>
                        <asp:Label runat="server" ID="lbl_documentid_task" Text='<%# Eval("TaskOrderDocumentId") %>' style="display:none;"></asp:Label>
                        <asp:Label runat="server" ID="lbl_docname_task" Text='<%# Eval("TaskOrderDocumentName") %>' style="display:none;"></asp:Label>                         
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>  
                    <telerik:GridBoundColumn DataField="DocumentStatus" FilterControlAltText="Filter DocumentStatus column"
                        HeaderText="DocumentStatus" ReadOnly="True" SortExpression="DocumentStatus"
                        UniqueName="DocumentStatus">
                    </telerik:GridBoundColumn>   
                                
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
            EventUploadedDocuments eud, EventTaskOrderDocuments etod, eventami e, documents d, documents d2, Users u 
            where eud.EventTaskOrderDocID=etod.EventTaskOrderDocID and u.userID=eud.UserID and e.id=eud.EventID and d.DocumentID=eud.DocumentID and 
            d2.DocumentID=etod.DocumentID and e.id=@eventamiid order by eud.EventUploadedDocID desc">
             <SelectParameters>
                 <asp:QueryStringParameter Name="eventamiid" QueryStringField="eventamiid" DefaultValue="1" DbType="Int32" />
             </SelectParameters>
        </asp:SqlDataSource>
</asp:Content>