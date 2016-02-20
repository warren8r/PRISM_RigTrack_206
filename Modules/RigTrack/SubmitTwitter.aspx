<%@ Page Language="C#" MasterPageFile="~/Masters/RigTrack.master" AutoEventWireup="true" CodeFile="SubmitTwitter.aspx.cs" Inherits="Modules_RigTrack_SubmitTwitter" EnableEventValidation="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="customCss" runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">


   
     <%-- // Jd New CSS Loading Animation--%>
    <asp:UpdateProgress ID="loader" runat="server" DisplayAfter="0">
        <ProgressTemplate>

            <div style="position: fixed; text-align: center; height: 100%; width: 100%; top: 0; right: 0; left: 0; padding-top: 15%; z-index: 9999999;">

                <div class="loader2">Loading...</div>

            </div>

        </ProgressTemplate>
    </asp:UpdateProgress>


    <fieldset>


        <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
              

                <asp:Table ID="MainPage" runat="server" HorizontalAlign="Center" Width="100%">

                    <asp:TableRow>
                        <asp:TableCell ColumnSpan="7" HorizontalAlign="Center">
                            <h2>Submit Twitter </h2>
                        </asp:TableCell>
                    </asp:TableRow>
                   
                </asp:Table>


               

                <asp:Table ID="Table11" runat="server" HorizontalAlign="Center" Width="100%">


                    <asp:TableRow>
                        <asp:TableCell CssClass="FormCenter" ColumnSpan="3">
                            <asp:Table ID="Table12" runat="server" HorizontalAlign="Center" BorderStyle="Double" BorderColor="#3A4F63">


                                <asp:TableRow>


                                    <asp:TableHeaderCell CssClass="HeaderCenter">
						Send Twitter Messages
                                    </asp:TableHeaderCell>

                                  

                                </asp:TableRow>


                                <asp:TableRow>


                                    <asp:TableCell>
                                      
                                         <asp:Button ID="btnSubmitTwitter" runat="server" Text="Send Tweets" Width="250px"  OnClick="btnSubmitTwitter_Click"/>
                                    </asp:TableCell>

                                      <asp:TableCell>
                                         <asp:Timer ID="MainTimer" runat="server" Interval="180000"  OnTick="MainTimer_Tick" />
                                     </asp:TableCell>


                                  
                                </asp:TableRow>

                                  

                            </asp:Table>
                        </asp:TableCell>
                    </asp:TableRow>

                </asp:Table>


            </ContentTemplate>
            
        </asp:UpdatePanel>

    </fieldset>

        <div style="text-align: center;" class="DivFooter">
            <hr style="width: 777px" />
            Copyright&copy;2016 - Limitless Healthcare IT - All Rights Reserved<br />
        </div>


   


</asp:Content>
