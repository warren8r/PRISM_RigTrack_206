<%@ Page Language="C#" MasterPageFile="~/Masters/RigTrack.master" AutoEventWireup="true" CodeFile="AddSurvey_B.aspx.cs" Inherits="Modules_RigTrack_AddSurvey_B" EnableEventValidation="true" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="customCss" runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script type="text/javascript">

        
        function Close() {
            var customArg = new Object();
            var param1 = document.getElementById("<%= txtMeasuredDepth.ClientID %>");
            
            var param2 = document.getElementById("<%= txtInclination.ClientID %>");
            var param3 = document.getElementById("<%= txtAzimuth.ClientID %>");

            //Add params for comments.
            


            customArg.Arg1 = param1.value;
               
            customArg.Arg2 = param2.value;
            customArg.Arg3 = param3.value;
            if (customArg.Arg1 && customArg.Arg2 && customArg.Arg3) {
                
                var win = GetRadWindow();
                
                win.close(customArg);

            }
            else {
                alert("Additional fields must be filled in");
                
            }//- can close / else validate
          // Close the window 

        }
        function GetRadWindow() {
            var oWindow = null;
            if (window.radWindow) oWindow = window.radWindow; //Will work in Moz in all cases, including clasic dialog 
            else if (window.frameElement.radWindow) oWindow = window.frameElement.radWindow; //IE (and Moz as well) 
            return oWindow;
        }

        


        function CloseNoParams() {
            var win = GetRadWindow();
            win.close();
        }
    </script>



    <fieldset>

         <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <asp:Table ID="MainPage" runat="server" HorizontalAlign="Center" Width="100%">

                    <asp:TableRow>
                        <asp:TableCell ColumnSpan="7" HorizontalAlign="Center">
                            <h2>Adding/ Editing a Survey</h2>
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell>&nbsp;</asp:TableCell>
                    </asp:TableRow>

           
                        <asp:TableRow>
                            <asp:TableCell CssClass="FormCenter" ColumnSpan="3">
                                <asp:Table ID="Table3" runat="server" HorizontalAlign="Center">
                                    <asp:TableRow>
                                        <asp:TableHeaderCell CssClass="HeaderCenter">
						Survey ID
                                        </asp:TableHeaderCell>


                                        <asp:TableHeaderCell CssClass="HeaderCenter">
					   Survey Name
                                        </asp:TableHeaderCell>

                                           <asp:TableHeaderCell CssClass="HeaderCenter">
						Curve Id
                                        </asp:TableHeaderCell>

                                            <asp:TableHeaderCell CssClass="HeaderCenter">
						Curve #
                                        </asp:TableHeaderCell>


                                    </asp:TableRow>
                                    <asp:TableRow>

                                        <asp:TableCell>
                                            <telerik:RadTextBox ID="txtSurveyID" runat="server" Width="200px" ></telerik:RadTextBox>
                                        </asp:TableCell>

                                        <asp:TableCell>
                                             <telerik:RadTextBox ID="txtSurveyName" runat="server" Width="200px"></telerik:RadTextBox>
                                        </asp:TableCell>

                                        <asp:TableCell>
                                             <telerik:RadTextBox ID="txtCurveID" runat="server" Width="200px"></telerik:RadTextBox>
                                        </asp:TableCell>

                                        
                                        <asp:TableCell>
                                             <telerik:RadTextBox ID="txtCurveNumber" runat="server" Width="200px"></telerik:RadTextBox>
                                        </asp:TableCell>

                                    </asp:TableRow>

                                </asp:Table>
                            </asp:TableCell>
                        </asp:TableRow>



                      <asp:TableRow>
                            <asp:TableCell CssClass="FormCenter" ColumnSpan="3">
                                <asp:Table ID="Table1" runat="server" HorizontalAlign="Center">
                                    <asp:TableRow>
                                        <asp:TableHeaderCell CssClass="HeaderCenter">
						Measured Depth
                                        </asp:TableHeaderCell>


                                        <asp:TableHeaderCell CssClass="HeaderCenter">
					  Inclination
                                        </asp:TableHeaderCell>

                                           <asp:TableHeaderCell CssClass="HeaderCenter">
						Azimuth
                                        </asp:TableHeaderCell>

                                            <asp:TableHeaderCell CssClass="HeaderCenter">
						Quadrant Dir>
                                        </asp:TableHeaderCell>

                                        

                                    </asp:TableRow>
                                    <asp:TableRow>

                                        <asp:TableCell>
                                            <telerik:RadTextBox ID="txtMeasuredDepth" runat="server" Width="200px" ></telerik:RadTextBox>
                                        </asp:TableCell>

                                        <asp:TableCell>
                                             <telerik:RadTextBox ID="txtInclination" runat="server" Width="200px"></telerik:RadTextBox>
                                        </asp:TableCell>

                                        <asp:TableCell>
                                             <telerik:RadTextBox ID="txtAzimuth" runat="server" Width="200px"></telerik:RadTextBox>
                                        </asp:TableCell>

                                        <asp:TableCell>
                                             <telerik:RadTextBox ID="txtQuadrantDirection" runat="server" Width="200px"></telerik:RadTextBox>
                                        </asp:TableCell>

                                    </asp:TableRow>

                                </asp:Table>
                            </asp:TableCell>
                        </asp:TableRow>


                       <asp:TableRow>
                            <asp:TableCell CssClass="FormCenter" ColumnSpan="3">
                                <asp:Table ID="Table2" runat="server" HorizontalAlign="Center">
                                    <asp:TableRow>
                                        <asp:TableHeaderCell CssClass="HeaderCenter">
						Comment
                                        </asp:TableHeaderCell>


                                        <asp:TableHeaderCell CssClass="HeaderCenter">
					  Directional Input Method/ Azimuth
                                        </asp:TableHeaderCell>

                                        
                                        <asp:TableHeaderCell CssClass="HeaderCenter">
					  Directional Input Method/ Quadrant
                                        </asp:TableHeaderCell>

                                   

                                        

                                    </asp:TableRow>
                                    <asp:TableRow>

                                         <asp:TableCell>
                                             <telerik:RadTextBox ID="TxtComment" runat="server" Width="200px"></telerik:RadTextBox>
                                        </asp:TableCell>

                                        <asp:TableCell HorizontalAlign="Center">

                                          <telerik:RadButton ID="btnRadioAzimuth" runat="server" ToggleType="Radio" ButtonType="ToggleButton"
                                            GroupName="DInputMethodGroup" AutoPostBack="false" BackColor="transparent"  CssClass="animated infinite pulse" HoveredCssClass="yesNoHovered" Skin="WebBlue">
                                            <ToggleStates>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                            </ToggleStates>
                                        </telerik:RadButton>
                                          
                                        </asp:TableCell>

                                        <asp:TableCell HorizontalAlign="Center">

                                            
                                          <telerik:RadButton ID="btnQuadrant" runat="server" ToggleType="Radio" ButtonType="ToggleButton"
                                            GroupName="DInputMethodGroup" AutoPostBack="false" BackColor="transparent" CssClass="animated infinite pulse" HoveredCssClass="yesNoHovered" Skin="WebBlue">
                                            <ToggleStates>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadioChecked"></telerik:RadButtonToggleState>
                                                <telerik:RadButtonToggleState PrimaryIconCssClass="rbToggleRadio"></telerik:RadButtonToggleState>
                                            </ToggleStates>
                                        </telerik:RadButton>
                                          

                                        </asp:TableCell>

                                       

                                     

                                    </asp:TableRow>

                                </asp:Table>
                            </asp:TableCell>
                        </asp:TableRow>

                </asp:Table>



              


                <asp:Table ID="TableButtons" runat="server" Width="100%">

                    <asp:TableRow>
                        <asp:TableCell>
                            &nbsp;
                        </asp:TableCell>
                    </asp:TableRow>

                    <asp:TableRow>

                        <asp:TableCell Width="35%"></asp:TableCell>

                        <asp:TableCell>
                            <asp:Button ID="BtnSave" runat="server" Text="Save" OnClientClick="Close(); return true;"/>
                        </asp:TableCell>

                        <asp:TableCell Width="10%"></asp:TableCell>

                        <asp:TableCell>
                            <asp:Button ID="BtnClear" runat="server" Text="Clear" OnClick="BtnClear_Click" />
                        </asp:TableCell>

                          <asp:TableCell Width="10%"></asp:TableCell>

                         <asp:TableCell>
                            <asp:Button ID="BtnDone" runat="server" Text="Done" OnClick="BtnDone_Click" />
                        </asp:TableCell>

                         <asp:TableCell Width="10%"></asp:TableCell>

                           <asp:TableCell>
                            <asp:Button ID="BtnCancel" runat="server" Text="Cancel" OnClientClick="CloseNoParams();"  />
                        </asp:TableCell>

                      
                        <asp:TableCell Width="35%"></asp:TableCell>
                    </asp:TableRow>
                </asp:Table>


            </ContentTemplate>
          
        </asp:UpdatePanel>
    </fieldset>

</asp:Content>

