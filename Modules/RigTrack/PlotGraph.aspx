<%@ Page Language="C#" MasterPageFile="~/Masters/2DPlotGraph.master" Title="Create Plot Graph" AutoEventWireup="true" CodeFile="PlotGraph.aspx.cs" Inherits="Modules_RigTrack_PlotGraph" EnableEventValidation="false" MaintainScrollPositionOnPostback="true" %>

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
    <script type="text/javascript">


        var $ = $telerik.$;
        var parentRadWindowManager;
        var parentPage;

        function Close() {
            GetRadWindow().close();
        }
        function GetRadWindow() {
            var oWindow = null;
            if (window.radWindow) oWindow = window.radWindow;
            else if (window.frameElement.radWindow) oWindow = window.frameElement.radWindow;

            return oWindow;
        }

        function AssignCurves() {

            var hiddenField2 = document.getElementById('<%= hiddenField2.ClientID %>');
            hiddenField2.value = "";

            if (redrawCheck == false) {

            }

            else {

                var combo = $find('<%=ddlCurve.ClientID %>');
                for (var i = 0; i < combo.get_items().get_count() ; i++) {
                    if (combo.get_items().getItem(i).get_checked()) {
                        hiddenField2.value += combo.get_items().getItem(i).get_value() + ",";
                    }
                }
            }

        }

        function getValue() {

            redrawCheck = true;
            relativeX = 400;
            relativeY = 300;
            var combo = $find('<%=ddlCurve.ClientID %>');
            var returnList = [];
            for (var i = 0; i < combo.get_items().get_count() ; i++) {
                var object = {};
                if (combo.get_items().getItem(i).get_checked()) {
                    object['ID'] = combo.get_items().getItem(i).get_value();
                    object['Name'] = combo.get_items().getItem(i).get_text();
                    
                    returnList.push(object);
                }
                
            }
            GraphSelectedCurves(returnList);

            return redrawCheck;
        }

        function CompanyChange(sender, eventArgs) {
          
            var item = eventArgs.get_item();

            document.getElementById('<%=Master.FindControl("lblJobNumber").ClientID %>').innerText = "Job Number: ";
            document.getElementById('<%=Master.FindControl("lblStateCountry").ClientID %>').innerText = "State/Country: ";
            document.getElementById('<%=Master.FindControl("lblCompany").ClientID %>').innerText = "Company: ";
            document.getElementById('<%=Master.FindControl("lblDeclination").ClientID %>').innerText = "Declination: ";
            document.getElementById('<%=Master.FindControl("lblLeaseWell").ClientID %>').innerText = "Lease/Well: ";
            document.getElementById('<%=Master.FindControl("lblGrid").ClientID %>').innerText = "Grid: ";
            document.getElementById('<%=Master.FindControl("lblLocation").ClientID %>').innerText = "Location: ";
            document.getElementById('<%=Master.FindControl("lblJobName").ClientID %>').innerText = "Job Name: ";
            document.getElementById('<%=Master.FindControl("lblRigName").ClientID %>').innerText = "Rig Name: ";
            document.getElementById('<%=Master.FindControl("lblCurveName").ClientID %>').innerText = "Curve Name: ";
            document.getElementById('<%=Master.FindControl("lblRKB").ClientID %>').innerText = "RKB: ";
            document.getElementById('<%=Master.FindControl("lblDateTime").ClientID %>').innerText = "Date Time: ";
            document.getElementById('<%=Master.FindControl("lblGLorMSL").ClientID %>').innerText = "GL or MSL: ";

            if (item.get_value() == 0) {
                //Get All
                $.ajax({
                    type: "POST",
                    url: 'PlotGraph.aspx/BindAllCurveGroups',
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (data) {
                        var curveGroups = data.d;
                        var dropdown = $find("<%= ddlCurveGroup.ClientID %>");
                        dropdown.get_items().clear();
                        var select = new Telerik.Web.UI.DropDownListItem;
                        select.set_text("-Select-");
                        select.set_value("0");
                        dropdown.get_items().add(select);
                        select.select();
                        for (i = 0; i < curveGroups.length; i++) {
                            var comboItem = new Telerik.Web.UI.DropDownListItem;
                            comboItem.set_text(curveGroups[i].CurveGroupName);
                            comboItem.set_value(curveGroups[i].ID);
                            dropdown.get_items().add(comboItem);

                        }
                        dropdown.commitChanges();
                    },
                    failure: function (data) {
                    }
                });
            }
            else {
                //Get Company's
                $.ajax({
                    type: "POST",
                    url: 'PlotGraph.aspx/BindCurveGroupsForCompany',
                    data: '{CompanyID: "' + item.get_value() + '"}',
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (data) {
                        var curveGroups = data.d;
                        var dropdown = $find("<%= ddlCurveGroup.ClientID %>");
                            dropdown.get_items().clear();
                            var select = new Telerik.Web.UI.DropDownListItem;
                            select.set_text("-Select-");
                            select.set_value("0");
                            dropdown.get_items().add(select);
                            select.select();
                            for (i = 0; i < curveGroups.length; i++) {
                                var comboItem = new Telerik.Web.UI.DropDownListItem;
                                comboItem.set_text(curveGroups[i].CurveGroupName);
                                comboItem.set_value(curveGroups[i].ID);
                                dropdown.get_items().add(comboItem);

                            }
                            dropdown.commitChanges();

                        },
                        failure: function (data) {
                        }

                    });
                }
        }

        function PopulateCurveDropdown(item) {
            //var raddropdownlist = $find("<%=ddlCurveGroup.ClientID %>");
            var CurveGroupID = item;
            var curveName = document.getElementById('<%=Master.FindControl("lblCurveName").ClientID %>') //= "Curve Name: " + curves[0].CurveName;
            curveName.innerHTML = "Curve Name: ";

            var curves = document.getElementById('<%= hiddenFieldCurves.ClientID %>').value;
            var curveArray = curves.split(",");

            if (CurveGroupID != 0) {
                $.ajax({
                    type: "POST",
                    url: 'PlotGraph.aspx/GetCurves',
                    data: '{curveGroupID: "' + CurveGroupID + '"}',
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (data) {
                        var curves = data.d;
                        var combo = $find("<%= ddlCurve.ClientID %>");
                        combo.enable();
                        combo.clearItems();
                        for (var i = 0; i < curves.length; i++) {
                            var comboItem = new Telerik.Web.UI.RadComboBoxItem();
                            comboItem.set_text(curves[i].Name);
                            comboItem.set_value(curves[i].ID);
                            combo.get_items().add(comboItem);
                            if (i > 0) {
                                curveName.innerHTML += ', ';
                            }
                            curveName.innerHTML += curves[i].Name;
                           
                        }
                        for (var j = 0; j < combo.get_items().get_count() ; j++) {
                            combo.get_items().getItem(j).set_checked(true);

                        }
                        if (document.getElementById('<%= hiddenFieldCurves.ClientID %>').value != "") {
                            var arrayLength = curveArray.length;
                            for (var k = 0; k < combo.get_items().get_count() ; k++) {
                                combo.get_items().getItem(k).set_checked(false);
                            }
                            for (var l = 0; l < arrayLength; l++) {
                                combo.findItemByValue(curveArray[l]).set_checked(true);
                            }
                            getValue();
                        }
                        document.getElementById('<%= hiddenFieldCurves.ClientID %>').value = "";
                        //combo.commitChanges();
                    }
                });
                $.ajax({
                    type: "POST",
                    url: 'PlotGraph.aspx/GetReport',
                    data: '{curveGroupID: "' + CurveGroupID + '"}',
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (data) {
                        var curves = data.d;

                        document.getElementById('<%=Master.FindControl("lblJobNumber").ClientID %>').innerText = "Job Number: " + curves[0].JobNumber;
                        document.getElementById('<%=Master.FindControl("lblStateCountry").ClientID %>').innerText = "State/Country: " + curves[0].StateCountry;
                        document.getElementById('<%=Master.FindControl("lblCompany").ClientID %>').innerText = "Company: " + curves[0].Company;
                        document.getElementById('<%=Master.FindControl("lblDeclination").ClientID %>').innerText = "Declination: " + curves[0].Declination;
                        document.getElementById('<%=Master.FindControl("lblLeaseWell").ClientID %>').innerText = "Lease/Well: " + curves[0].LeaseWell;
                        document.getElementById('<%=Master.FindControl("lblGrid").ClientID %>').innerText = "Grid: " + curves[0].Grid;
                        document.getElementById('<%=Master.FindControl("lblLocation").ClientID %>').innerText = "Location: " + curves[0].Location;
                        document.getElementById('<%=Master.FindControl("lblJobName").ClientID %>').innerText = "Job Name: " + curves[0].JobName;
                        document.getElementById('<%=Master.FindControl("lblRigName").ClientID %>').innerText = "Rig Name: " + curves[0].RigName;
                        
                        document.getElementById('<%=Master.FindControl("lblRKB").ClientID %>').innerText = "RKB: " + curves[0].RKB;
                        document.getElementById('<%=Master.FindControl("lblDateTime").ClientID %>').innerText = "Date Time: " + curves[0].DateTime;
                        document.getElementById('<%=Master.FindControl("lblGLorMSL").ClientID %>').innerText = "GL or MSL: " + curves[0].GLorMSL;
                    }
                });
            }
            else {
                var combo = $find("<%= ddlCurve.ClientID %>");
                combo.disable();
            }
        }

        function GraphSelectedCurves(returnList) {
            ClearGraph();

            var curveName = document.getElementById('<%=Master.FindControl("lblCurveName").ClientID %>') //= "Curve Name: " + curves[0].CurveName;
            curveName.innerHTML = "Curve Name: ";
            //ClearGraph2();
            var curveID = '';
            for (var i = 0; i < returnList.length; i++) {
                if (i > 0) {

                    curveID = curveID + ',' + returnList[i]['ID'];
                    curveName.innerHTML += ',' +  returnList[i]['Name'];
                }
                else {
                    curveID = returnList[i]['ID'];
                    curveName.innerHTML += returnList[i]['Name'];
                }
            }

           
            if (curveID != '') {
                $.ajax({
                    type: "POST",
                    url: 'PlotGraph.aspx/GetGraphDetailsPerCurves',
                    data: '{curves: "' + curveID + '"}',
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (data) {


                        // call smaller graph draw based on curve id not curve group
                        ItemSelectedFromServerCurveSmall(curveID);

                        var displayval = data.d;

                        for (i = 0; i < displayval.length; i++) {
                            var prev;
                            if (i != 0) {
                                prev = i - 1;

                            }

                            else {
                                prev = i;
                            }

                            DrawSurveyCurves(displayval, i, prev);
                        }


                    },
                    failure: function (response) {
                        var r = jQuery.parseJSON(response.responseText);
                        alert("Message: " + r.Message);
                        alert("StackTrace: " + r.StackTrace);
                        alert("ExceptionType: " + r.ExceptionType);
                    },
                    error: function (xhr, textStatus, error) {
                        alert(xhr.statusText);
                        alert(textStatus);
                        alert(error);
                    }

                });
            }
            
        }


        function GraphSelectedCurvesZoom(returnList, getEveryXCoord, getEveryYCoord) {
            //ClearGraph();
            //ClearGraph2();
            var curveID = '';
            for (var i = 0; i < returnList.length; i++) {
                if (i > 0) {

                    curveID = curveID + ',' + returnList[i]['ID'];
                }
                else {
                    curveID = returnList[i]['ID'];
                }
            }


            if (curveID != '') {
                $.ajax({
                    type: "POST",
                    url: 'PlotGraph.aspx/GetGraphDetailsPerCurves',
                    data: '{curves: "' + curveID + '"}',
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (data) {


                        var displayval = data.d;

                        for (i = 0; i < displayval.length; i++) {
                            var prev;
                            if (i != 0) {
                                prev = i - 1;

                            }

                            else {
                                prev = i;
                            }

                            DrawSurveyCurvesZoom(displayval, i, prev, getEveryXCoord, getEveryYCoord);
                        }


                    },
                    failure: function (response) {
                        var r = jQuery.parseJSON(response.responseText);
                        alert("Message: " + r.Message);
                        alert("StackTrace: " + r.StackTrace);
                        alert("ExceptionType: " + r.ExceptionType);
                    },
                    error: function (xhr, textStatus, error) {
                        alert(xhr.statusText);
                        alert(textStatus);
                        alert(error);
                    }

                });
            }

        }


        function GraphSelectedCurvesClick(returnList, getEveryXCoord, getEveryYCoord, relativeX, relativeY) {
         
            var curveID = '';
            for (var i = 0; i < returnList.length; i++) {
                if (i > 0) {

                    curveID = curveID + ',' + returnList[i]['ID'];
                }
                else {
                    curveID = returnList[i]['ID'];
                }
            }


            if (curveID != '') {
                $.ajax({
                    type: "POST",
                    url: 'PlotGraph.aspx/GetGraphDetailsPerCurves',
                    data: '{curves: "' + curveID + '"}',
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (data) {

                        var displayval = data.d;

                        for (i = 0; i < displayval.length; i++) {
                            var prev;
                            if (i != 0) {
                                prev = i - 1;

                            }

                            else {
                                prev = i;
                            }

                            DrawSurveyCurvesClick(displayval, i, prev, getEveryXCoord, getEveryYCoord, relativeX, relativeY);
                        }


                    },
                    failure: function (response) {
                        var r = jQuery.parseJSON(response.responseText);
                        alert("Message: " + r.Message);
                        alert("StackTrace: " + r.StackTrace);
                        alert("ExceptionType: " + r.ExceptionType);
                    },
                    error: function (xhr, textStatus, error) {
                        alert(xhr.statusText);
                        alert(textStatus);
                        alert(error);
                    }

                });
            }

        }

        function pageLoad() {
            
        }

        function OpenWindow() {
            parentPage = GetRadWindow().BrowserWindow;
            parentRadWindowManager = parentPage.GetRadWindowManager();
            

            var dropdown = $find("<%=ddlCurveGroup.ClientID %>");
            var curveGroupID = dropdown.get_selectedItem().get_value();
            var isOpera = !!window.opera || navigator.userAgent.indexOf(' OPR/') >= 0;
            // Opera 8.0+ (UA detection to detect Blink/v8-powered Opera)
            var isFirefox = typeof InstallTrigger !== 'undefined';   // Firefox 1.0+
            var isSafari = Object.prototype.toString.call(window.HTMLElement).indexOf('Constructor') > 0;
            // At least Safari 3+: "[object HTMLElementConstructor]"
            var isChrome = !!window.chrome && !isOpera;              // Chrome 1+
            var isIE = /*@cc_on!@*/false || !!document.documentMode;   // At least IE6


            if (isFirefox) {
            }
            else {
                event.preventDefault();
            }
            //curveWindow.show();
            setWindowSize(curveGroupID);
        }
        function setWindowSize(curveGroupID) {
            var curveWindow = parentRadWindowManager.getWindowByName("CurveSelectionWindow");
            
            //parentRadWindowManager.open("SelectCurvesForPlotGraph.aspx?CurveGroupID=" + curveGroupID, "CurveWindow");
            
            curveWindow.SetSize(750, 550);


            curveWindow.SetUrl("SelectCurvesForPlotGraph.aspx?CurveGroupID=" + curveGroupID);
            curveWindow.Show();
            //curveWindow.center();
            curveWindow.set_modal(true);
        }



        function SaveComments() {

            var raddropdownlist = $find("<%=ddlCurveGroup.ClientID %>");
            var CurveGroupID = raddropdownlist.get_selectedItem().get_value();
            var comments = document.getElementById("<%=txtPlotComments.ClientID%>").value;
            var confirmation = document.getElementById("<%= lblSaveComments.ClientID%>");
            // call web method to save comment
            PageMethods.btnSaveComments_Click(CurveGroupID, comments);

            
            confirmation.innerHTML = "&nbsp Updated Comments!";
                
        }

        function Close() {
            GetRadWindow().close(); // Close the window 
        }
        function GetRadWindow() {
            var oWindow = null;
            if (window.radWindow) oWindow = window.radWindow; //Will work in Moz in all cases, including clasic dialog 
            else if (window.frameElement.radWindow) oWindow = window.frameElement.radWindow; //IE (and Moz as well) 

            return oWindow;
        }


      

        // Big Graph Calling from pop up window view edit jobs
        function ItemSelectedFromServer(curveGroupID) {


            //var item = eventArgs.get_item();
            ClearGraph();

            PopulateCurveDropdown(curveGroupID);

            ItemSelectedFromServer3(curveGroupID);
            //bindTargets(item.get_value());
            $.ajax({
                type: "POST",
                url: 'PlotGraph.aspx/GetSurveyDetails',
                data: '{curveGroupID: "' + curveGroupID + '" }',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (data) {
                    var displayval = data.d;

                    for (i = 0; i < displayval.length; i++) {
                        var prev;
                        if (i != 0) {
                            prev = i - 1;

                        }

                        else {
                            prev = i;
                        }

                        DrawSurveyCurves(displayval, i, prev);
                     
                    }

                },
                failure: function (response) {
                    var r = jQuery.parseJSON(response.responseText);
                    alert("Message: " + r.Message);
                    alert("StackTrace: " + r.StackTrace);
                    alert("ExceptionType: " + r.ExceptionType);
                }
            });
        }


        // jd used for zooming in out big graph
        function ItemSelectedFromServer2(curveGroupID, getEveryXCoord, getEveryYCoord) {



            $.ajax({
                type: "POST",
                url: 'PlotGraph.aspx/GetSurveyDetails',
                data: '{curveGroupID: "' + curveGroupID + '" }',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (data) {
                    var displayval = data.d;

                    for (i = 0; i < displayval.length; i++) {

                        var prev;
                        if (i != 0) {
                            prev = i - 1;

                        }

                        else {
                            prev = i;
                        }

                        DrawSurveyCurvesZoom(displayval, i, prev, getEveryXCoord, getEveryYCoord);
                    }


                },
                failure: function (response) {
                    var r = jQuery.parseJSON(response.responseText);
                    alert("Message: " + r.Message);
                    alert("StackTrace: " + r.StackTrace);
                    alert("ExceptionType: " + r.ExceptionType);
                }
            });
        }


        // jd used for when you click the big graph
        function ItemSelectedFromServerZoom2(curveGroupID, getEveryXCoord, getEveryYCoord, relativeX, relativeY) {



            $.ajax({
                type: "POST",
                url: 'PlotGraph.aspx/GetSurveyDetails',
                data: '{curveGroupID: "' + curveGroupID + '" }',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (data) {
                    var displayval = data.d;


                    for (i = 0; i < displayval.length; i++) {

                        var prev;
                        if (i != 0) {
                            prev = i - 1;

                        }

                        else {
                            prev = i;
                        }

                        DrawSurveyCurvesClick(displayval, i, prev, getEveryXCoord, getEveryYCoord, relativeX, relativeY);
                    }


                },
                failure: function (response) {
                    var r = jQuery.parseJSON(response.responseText);
                    alert("Message: " + r.Message);
                    alert("StackTrace: " + r.StackTrace);
                    alert("ExceptionType: " + r.ExceptionType);
                }
            });
        }


        // JD Used For The Smaller Graph,calling from pop up window view edit jobs
        function ItemSelectedFromServer3(curveGroupID) {

            ClearGraph2();

            $.ajax({
                type: "POST",
                url: 'PlotGraph.aspx/GetGraphDetails',
                data: '{curveGroupID: "' + curveGroupID + '" }',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (data) {


                    $.ajax({
                        type: "POST",
                        url: 'PlotGraph.aspx/GetSurveyDetails',
                        data: '{curveGroupID: "' + curveGroupID + '"}',
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (data) {

                            var displayvalSurvey = data.d;// used for getting the survey information from the database

                            for (s = 0; s < displayvalSurvey.length; s++) {
                                var prev;
                                if (s != 0) {
                                    prev = s - 1;

                                }

                                else {
                                    prev = s;
                                }

                                DrawSmallGraphSurvey(displayvalSurvey, s, prev);

                            }

                        },
                        failure: function (data) {

                        }
                    });


                    var displayval = data.d; // used for getting the target data from the database


                    for (i = 0; i < displayval.length; i++) {
                        var splitval = displayval[i].shape;

                        if (splitval == "1000") {

                            DrawCircleTarget(displayval, i);

                        }
                        else if (splitval == "1001") {

                            DrawSquareTarget(displayval, i);

                        }
                        else if (splitval == "1002") {

                            DrawRectangleTarget(displayval, i);
                        }

                        else if (splitval == "1003") {

                            DrawPolygonTarget(displayval, i);
                        }

                        else if (splitval == "1004") {

                            DrawEllipseTarget(displayval, i);
                        }

                        else if (splitval == "1005") {

                            DrawPointTarget(displayval, i);
                        }
                    }

                },
                failure: function (response) {
                    var r = jQuery.parseJSON(response.responseText);
                    alert("Message: " + r.Message);
                    alert("StackTrace: " + r.StackTrace);
                    alert("ExceptionType: " + r.ExceptionType);
                }
            });

          
        }

        // JD Used For The Smaller Graph Drawing from  redraw button
        function ItemSelectedFromServerCurveSmall(curveID) {

            ClearGraph2();

            $.ajax({
                type: "POST",
                url: 'PlotGraph.aspx/GetTargetGraphDetails',
                data: '{curveID: "' + curveID + '" }',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (data) {


                    $.ajax({
                        type: "POST",
                        url: 'PlotGraph.aspx/GetGraphDetailsPerCurves',
                        data: '{curves: "' + curveID + '"}',
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (data) {

                            var displayvalSurvey = data.d;// used for getting the survey information from the database

                            for (s = 0; s < displayvalSurvey.length; s++) {
                                var prev;
                                if (s != 0) {
                                    prev = s - 1;

                                }

                                else {
                                    prev = s;
                                }

                                DrawSmallGraphSurvey(displayvalSurvey, s, prev);

                            }

                        },
                        failure: function (data) {

                        }
                    });


                    var displayval = data.d; // used for getting the target data from the database


                    for (i = 0; i < displayval.length; i++) {
                        var splitval = displayval[i].shape;

                        if (splitval == "1000") {

                            DrawCircleTarget(displayval, i);
                        }
                        else if (splitval == "1001") {

                            DrawSquareTarget(displayval, i);
                        }
                        else if (splitval == "1002") {

                            DrawRectangleTarget(displayval, i);
                        }

                        else if (splitval == "1003") {

                            DrawPolygonTarget(displayval, i);
                        }

                        else if (splitval == "1004") {

                            DrawEllipseTarget(displayval, i);
                        }

                        else if (splitval == "1005") {

                            DrawPointTarget(displayval, i);
                        }
                    }

                },
                failure: function (response) {
                    var r = jQuery.parseJSON(response.responseText);
                    alert("Message: " + r.Message);
                    alert("StackTrace: " + r.StackTrace);
                    alert("ExceptionType: " + r.ExceptionType);
                }
            });


        }

       

        // jd used for zooming into smaller graph
        function ItemSelectedFromServer4(curveGroupID, getEveryXCoordSGraph, getEveryYCoordSGraph) {

            $.ajax({
                type: "POST",
                url: 'PlotGraph.aspx/GetGraphDetails',
                data: '{curveGroupID: "' + curveGroupID + '" }',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (data) {

                    $.ajax({
                        type: "POST",
                        url: 'PlotGraph.aspx/GetSurveyDetails',
                        data: '{curveGroupID: "' + curveGroupID + '"}',
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (data) {

                            var displayvalSurvey = data.d;// used for getting the survey information from the database

                            for (s = 0; s < displayvalSurvey.length; s++) {
                                var prev;
                                if (s != 0) {
                                    prev = s - 1;

                                }

                                else {
                                    prev = s;
                                }

                                DrawSmallGraphSurveyZoom(displayvalSurvey, s, prev, getEveryXCoordSGraph, getEveryYCoordSGraph);

                            }

                        },
                        failure: function (data) {

                        }
                    });


                    var displayval = data.d;

                    for (i = 0; i < displayval.length; i++) {
                        var splitval = displayval[i].shape;

                        if (splitval == "1000") {

                            DrawCircleTargetZoom(displayval, i, getEveryXCoordSGraph, getEveryYCoordSGraph);
                        }
                        else if (splitval == "1001") {

                            DrawSquareTargetZoom(displayval, i, getEveryXCoordSGraph, getEveryYCoordSGraph);
                        }
                        else if (splitval == "1002") {

                            DrawRectangleTargetZoom(displayval, i, getEveryXCoordSGraph, getEveryYCoordSGraph);
                        }

                        else if (splitval == "1003") {

                            DrawPolygonTargetZoom(displayval, i, getEveryXCoordSGraph, getEveryYCoordSGraph);
                        }

                        else if (splitval == "1004") {

                            DrawEllipseTargetZoom(displayval, i, getEveryXCoordSGraph, getEveryYCoordSGraph);
                        }

                        else if (splitval == "1005") {

                            DrawPointTargetZoom(displayval, i, getEveryXCoordSGraph, getEveryYCoordSGraph);
                        }
                    }


                },
                failure: function (response) {
                    var r = jQuery.parseJSON(response.responseText);
                    alert("Message: " + r.Message);
                    alert("StackTrace: " + r.StackTrace);
                    alert("ExceptionType: " + r.ExceptionType);
                }
            });
        }

        // for zooming into small graph for just curves 
        function ZoomSmallerGraphCurves(returnList, getEveryXCoordSGraph, getEveryYCoordSGraph) {

            var curveID = '';
            for (var i = 0; i < returnList.length; i++) {
                if (i > 0) {

                    curveID = curveID + ',' + returnList[i]['ID'];
                }
                else {
                    curveID = returnList[i]['ID'];
                }
            }

            $.ajax({
                type: "POST",
                url: 'PlotGraph.aspx/GetTargetGraphDetails',
                data: '{curveID: "' + curveID + '" }',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (data) {



                    $.ajax({
                        type: "POST",
                        url: 'PlotGraph.aspx/GetGraphDetailsPerCurves',
                        data: '{curves: "' + curveID + '"}',
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (data) {

                            var displayvalSurvey = data.d;// used for getting the survey information from the database

                            for (s = 0; s < displayvalSurvey.length; s++) {
                                var prev;
                                if (s != 0) {
                                    prev = s - 1;

                                }

                                else {
                                    prev = s;
                                }

                                DrawSmallGraphSurveyZoom(displayvalSurvey, s, prev, getEveryXCoordSGraph, getEveryYCoordSGraph);

                            }

                        },
                        failure: function (data) {

                        }
                    });


                    var displayval = data.d;

                    for (i = 0; i < displayval.length; i++) {
                        var splitval = displayval[i].shape;

                        if (splitval == "1000") {

                            DrawCircleTargetZoom(displayval, i, getEveryXCoordSGraph, getEveryYCoordSGraph);
                        }
                        else if (splitval == "1001") {

                            DrawSquareTargetZoom(displayval, i, getEveryXCoordSGraph, getEveryYCoordSGraph);
                        }
                        else if (splitval == "1002") {

                            DrawRectangleTargetZoom(displayval, i, getEveryXCoordSGraph, getEveryYCoordSGraph); 
                        }

                        else if (splitval == "1003") {

                            DrawPolygonTargetZoom(displayval, i, getEveryXCoordSGraph, getEveryYCoordSGraph);
                        }

                        else if (splitval == "1004") {

                            DrawEllipseTargetZoom(displayval, i, getEveryXCoordSGraph, getEveryYCoordSGraph);
                        }

                        else if (splitval == "1005") {

                            DrawPointTargetZoom(displayval, i, getEveryXCoordSGraph, getEveryYCoordSGraph);
                        }
                    }


                },
                failure: function (response) {
                    var r = jQuery.parseJSON(response.responseText);
                    alert("Message: " + r.Message);
                    alert("StackTrace: " + r.StackTrace);
                    alert("ExceptionType: " + r.ExceptionType);
                }
            });
        }


        // js used for when user changes the drop down, but  the drop down is hidden anyway, mainly for testing the graph faster
        function OnClientItemSelected(sender, eventArgs) {

            relativeX = 400;
            relativeY = 300;

            ClearGraph();
          
            redrawCheck = false;
            var item = eventArgs.get_item();
            if (item.get_value != '0') {
                
                $.ajax({
                    type: "POST",
                    url: 'PlotGraph.aspx/GetPlotComments',
                    data: '{curveGroupID: "' + item.get_value() + '"}',
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (data) {
                        
                        var commentBox = document.getElementById('<%= txtPlotComments.ClientID %>');
                        commentBox.value = data.d;
                    },
                    failure: function (data) {
                        var r = jQuery.parseJSON(respsone.responseText);
                        alert("Message: " + r.Message);
                        alert("StackTrace: " + r.StackTrace);
                        alert("ExceptionType: " + r.ExceptionType);
                    }
                });

            }

         
            //bindTargets(item.get_value());

            PopulateCurveDropdown(item.get_value());

            var hiddenField = document.getElementById('<%= hiddenField.ClientID %>');
            hiddenField.value = item.get_value();

            $.ajax({
                type: "POST",
                url: 'PlotGraph.aspx/GetSurveyDetails',
                data: '{curveGroupID: "' + item.get_value() + '" }',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (data) {
                    var displayval = data.d;
                    OnClientItemSelected2(sender, eventArgs);


                    for (i = 0; i < displayval.length; i++) {
                        var prev;
                        if (i != 0) {
                            prev = i - 1;

                        }

                        else {
                            prev = i;
                        }

                        DrawSurveyCurves(displayval, i, prev);
                  
                    }

                },
                failure: function (response) {
                    var r = jQuery.parseJSON(response.responseText);
                    alert("Message: " + r.Message);
                    alert("StackTrace: " + r.StackTrace);
                    alert("ExceptionType: " + r.ExceptionType);
                }
            });

            return redrawCheck;
        }

        // jd used for the smaller graph called with in OnClientItemSelected
        function OnClientItemSelected2(sender, eventArgs) {


            var item = eventArgs.get_item();
            ClearGraph2();

            if (item.get_value() != 0) {
                $.ajax({
                    type: "POST",
                    url: 'PlotGraph.aspx/GetGraphDetails',
                    data: '{curveGroupID: "' + item.get_value() + '" }',
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (data) {


                        $.ajax({
                            type: "POST",
                            url: 'PlotGraph.aspx/GetSurveyDetails',
                            data: '{curveGroupID: "' + item.get_value() + '"}',
                            contentType: "application/json; charset=utf-8",
                            dataType: "json",
                            success: function (data) {

                                var displayvalSurvey = data.d;// used for getting the survey information from the database

                                for (s = 0; s < displayvalSurvey.length; s++) {
                                    var prev;
                                    if (s != 0) {
                                        prev = s - 1;

                                    }

                                    else {
                                        prev = s;
                                    }

                                    DrawSmallGraphSurvey(displayvalSurvey, s, prev);
                                }

                            },
                            failure: function (data) {

                            }
                        });

                        var displayval = data.d; // used for getting the target data from the database


                        for (i = 0; i < displayval.length; i++) {
                            var splitval = displayval[i].shape;

                            if (splitval == "1000") {

                                DrawCircleTarget(displayval, i);

                            }
                            else if (splitval == "1001") {

                                DrawSquareTarget(displayval, i);
                            }
                            else if (splitval == "1002") {

                                DrawRectangleTarget(displayval, i);
                            }

                            else if (splitval == "1003") {

                                DrawPolygonTarget(displayval, i);
                            }

                            else if (splitval == "1004") {

                                DrawEllipseTarget(displayval, i);
                            }

                            else if (splitval == "1005") {

                                DrawPointTarget(displayval, i);
                            }
                        }

                    },
                    failure: function (response) {
                        var r = jQuery.parseJSON(response.responseText);
                        alert("Message: " + r.Message);
                        alert("StackTrace: " + r.StackTrace);
                        alert("ExceptionType: " + r.ExceptionType);
                    }
                });
            }
         
        }

      
    </script>

     <script type="text/javascript" src="../../js/2DGraph/2DGraphMain.js"></script>

    <fieldset>


        <asp:UpdatePanel runat="server" ID="updPnl1" UpdateMode="Conditional">
            <ContentTemplate>
                <asp:HiddenField ID="hiddenField" runat="server" ClientIDMode="Static" />
                <asp:HiddenField ID="hiddenField2" runat="server" ClientIDMode="Static" />
                <asp:HiddenField ID="hiddenFieldCurves" runat="server" ClientIDMode="Static" />
                <telerik:RadWindow ID="CurveSelectionWindow" runat="server"></telerik:RadWindow>
                <asp:Table ID="MainPage" runat="server" HorizontalAlign="Center" Width="100%">

                    <asp:TableRow>
                        <asp:TableCell ColumnSpan="7" HorizontalAlign="Center">
                            <h2>Plot Graph</h2>
                        </asp:TableCell>
                    </asp:TableRow>

                </asp:Table>

                <asp:UpdatePanel runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <asp:Table ID="Table6" runat="server" HorizontalAlign="Center" Width="100%" BorderStyle="Double" BorderColor="#3A4F63">


                            <asp:TableRow>

                                   <asp:TableCell>
                                    <asp:Table ID="Table7" runat="server" HorizontalAlign="Center">

                                        <asp:TableRow>
                                            <asp:TableHeaderCell CssClass="HeaderCenter">
                        Company
                                            </asp:TableHeaderCell>
                                            <asp:TableHeaderCell CssClass="HeaderCenter">
						Curve Group ID
                                            </asp:TableHeaderCell>

                                            <asp:TableHeaderCell CssClass="HeaderCenter">
                        Curves
                                            </asp:TableHeaderCell>

                                            <asp:TableHeaderCell CssClass="HeaderCenter">
                        
                                            </asp:TableHeaderCell>

                                            <asp:TableHeaderCell CssClass="HeaderCenter">
                      
                                            </asp:TableHeaderCell>


                                            <asp:TableHeaderCell CssClass="HeaderCenter">
                        EW
                                            </asp:TableHeaderCell>

                                            <asp:TableHeaderCell CssClass="HeaderCenter">
                        NS
                                            </asp:TableHeaderCell>




                                        </asp:TableRow>


                                        <asp:TableRow>
                                            <asp:TableCell>
                                                <telerik:RadDropDownList runat="server" ID="ddlCompany" Width="130px" AppendDataBoundItems="true" DropDownWidth="300px" DropDownHeight="200px"
                                                    OnClientItemSelected="CompanyChange">
                                                    <Items>
                                                        <telerik:DropDownListItem Value="0" Text="-Select-" />
                                                    </Items>
                                                </telerik:RadDropDownList>
                                            </asp:TableCell>
                                            <asp:TableCell>
                                                <telerik:RadDropDownList runat="server" ID="ddlCurveGroup" Width="130px" AppendDataBoundItems="true" DropDownWidth="300px" DropDownHeight="200px" 
                                                    OnClientItemSelected="OnClientItemSelected"> 
                                                    <Items>
                                                        <telerik:DropDownListItem Value="0" Text="-Select-" />
                                                    </Items>
                                                </telerik:RadDropDownList>
                                            </asp:TableCell>

                                           
                                            <asp:TableCell>
                                                <telerik:RadComboBox ID="ddlCurve" runat="server" CheckBoxes="true" EnableCheckAllItemsCheckBox="true" DropDownWidth="200px" Width="130px" Enabled="false">
                                                </telerik:RadComboBox>
                                            </asp:TableCell>

                                            <asp:TableCell>
                                                <asp:Button ID="btnRedraw" runat="server" CssClass="button-RedrawButton" ForeColor="Black" Text="Redraw" AutoPostBack="false" OnClientClick="getValue(); return false;"></asp:Button>
                                            </asp:TableCell>


                                            <asp:TableCell>
                                                <telerik:RadButton ID="btnView3D" runat="server" Text="View 3D Graph" OnClick="btnView3D_Click" OnClientClicked="AssignCurves"></telerik:RadButton>
                                            </asp:TableCell>


                                              <asp:TableCell>
                                        <telerik:RadTextBox runat="server" ClientIDMode="Static" ID="txtEW" Width="70px" />
                                    </asp:TableCell>

                                               <asp:TableCell>
                                        <telerik:RadTextBox runat="server" ClientIDMode="Static" ID="txtNS" Width="70px" />
                                    </asp:TableCell>

                                        </asp:TableRow>

                                    </asp:Table>
                                </asp:TableCell>

                               

                                <asp:TableCell Width="2%">
                                    <asp:Button ID="btnZoomIn" runat="server" CssClass="button-graphZoomOut2" OnClientClick="zoomIn(); return false;" />
                                </asp:TableCell>

                                <asp:TableCell Width="2%">
                                    <asp:Button ID="btnZoomOut" runat="server" CssClass="button-graphZoomIn2" OnClientClick="zoomOut(); return false;" />
                                </asp:TableCell>


                                <asp:TableCell>
                                    
                                    <asp:Label ID="lblComments" Text="Plot Comments" Font-Bold="true" runat="server"></asp:Label>
                                    <asp:Label ID="lblSaveComments" Text="" runat="server" ForeColor="Green"></asp:Label>
                                    <asp:TextBox ID="txtPlotComments" runat="server" TextMode="MultiLine" Width="140px"></asp:TextBox>
                                </asp:TableCell>

                                <asp:TableCell>

                                    <asp:Button ID="btnSaveComments" Text="Save Comments" runat="server" ClientIDMode="Static" OnClientClick="SaveComments(); return false;" />

                                </asp:TableCell>

                          
                                <asp:TableCell>
                                    <asp:Button ID="btnPrint" CssClass="button-PrintButton" ForeColor="Black" Text="Print" runat="server" OnClientClick="javascript:window.print();" />
                                </asp:TableCell>
                                <asp:TableCell>
                                   
                              
                                </asp:TableCell>
                                <asp:TableCell Width="2%">
                                    <asp:Button ID="btnZoomIn2" runat="server" CssClass="button-graphZoomOut2" OnClientClick="zoomIn2(); return false;" />
                                </asp:TableCell>

                                <asp:TableCell Width="2%">
                                    <asp:Button ID="btnZoomOut2" runat="server" CssClass="button-graphZoomIn2" OnClientClick="zoomOut2(); return false;" />
                                </asp:TableCell>

                                 <asp:TableCell Width="50%"></asp:TableCell>

                            </asp:TableRow>

                        </asp:Table>
                    </ContentTemplate>
                </asp:UpdatePanel>


                <asp:Table ID="CanvasTable" runat="server" Width="100%" HorizontalAlign="Center">
                    
                    <asp:TableRow>
                        <asp:TableCell Width="800px">
                                <canvas id="myCanvas" width="800px" height="600px" style="border:1px solid #010A15;" /> </asp:TableCell>

                       
                        <asp:TableCell>
                              <canvas id="myCanvas2" width="400px" height="400px" style="border:1px solid #010A15;" />

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


    <script type="text/javascript">

        //var offset = $(this).offset();
        var relativeX; //  offset.left
        var relativeY;  //  offset.top
        var zoomCheck;
        $(document).ready(function () {

            $("#myCanvas").mousemove(function (e) {

                //var offset = $(this).offset();
                var relativeX = (e.offsetX - 400.00) * 2.5;  //  offset.left
                var relativeY = (-e.offsetY + 300.00) * 17;   //  offset.top


                $('#<%= txtEW.ClientID %>').val(relativeX);
                $('#<%= txtNS.ClientID %>').val(relativeY);
        
             });

           

            $("#myCanvas").mousedown(function (e) {

                relativeX = 800 - e.offsetX;
                relativeY = 600 - e.offsetY;

            

                 getEveryXCoord = 100;
                 getEveryYCoord = 500;

                 zoomCheck = true;

             
               
                // clear the graph and make a new 1
                var canvasClear = document.getElementById('myCanvas');
                var contextClear = canvasClear.getContext('2d');

                contextClear.clearRect(0, 0, canvasClear.width, canvasClear.height);

                var myGraph = new Graph({
                    canvasId: 'myCanvas',
                    minX: -10,
                    minY: -10,
                    maxX: 10,
                    maxY: 10,
                    unitsPerTick: 1

                }, 100, 500, relativeX, relativeY);


                
                if (redrawCheck == false) {
                    var raddropdownlist = $find('<%=ddlCurveGroup.ClientID %>');
                    var CurveGroupID = raddropdownlist.get_selectedItem().get_value();
                    if (CurveGroupID != 0) {
                        ItemSelectedFromServerZoom2(CurveGroupID, 100, 500, relativeX, relativeY);
                    }
                }

                else {
                    var combo = $find('<%=ddlCurve.ClientID %>');
                    var returnList = [];
                    for (var i = 0; i < combo.get_items().get_count() ; i++) {
                        var object = {};
                        if (combo.get_items().getItem(i).get_checked()) {
                            object['ID'] = combo.get_items().getItem(i).get_value();
                            returnList.push(object);
                        }

                    }

                    GraphSelectedCurvesClick(returnList, getEveryXCoord, getEveryYCoord, relativeX, relativeY);
                }

                


                 return relativeX, relativeY, zoomCheck;



             });

         });


    </script>



     <script type="text/javascript" src="../../js/2DGraph/2DGraphInitialize.js"></script>

    

    <script type="text/javascript">
        var getEveryXCoord = 100;
        var getEveryYCoord = 500;

        // for smaller graph
        var getEveryXCoordSGraph = 50;
        var getEveryYCoordSGraph = 50;

        var redrawCheck = false;


        function zoomIn() {

            // Jd Loop Through Every X Coord of every Target To Set The Grid Numbers
            getEveryXCoord = getEveryXCoord + 5;
            getEveryYCoord = getEveryYCoord + 25;

            // clear the graph and make a new 1
            var canvasClear = document.getElementById('myCanvas');
            var contextClear = canvasClear.getContext('2d');

            contextClear.clearRect(0, 0, canvasClear.width, canvasClear.height);

            var myGraph = new Graph({
                canvasId: 'myCanvas',
                minX: -10,
                minY: -10,
                maxX: 10,
                maxY: 10,
                unitsPerTick: 1
            }, getEveryXCoord, getEveryYCoord, relativeX, relativeY);


         

            if (redrawCheck == false) {

                var raddropdownlist = $find('<%=ddlCurveGroup.ClientID %>');
                var CurveGroupID = raddropdownlist.get_selectedItem().get_value();
                if (CurveGroupID != 0) {


                    if (zoomCheck == true) {
                        ItemSelectedFromServerZoom2(CurveGroupID, getEveryXCoord, getEveryYCoord, relativeX, relativeY);
                    }
                    else {
                      
                        ItemSelectedFromServer2(CurveGroupID, getEveryXCoord, getEveryYCoord);
                    }

                }

            }


            else {
                var combo = $find('<%=ddlCurve.ClientID %>');
                var returnList = [];
                for (var i = 0; i < combo.get_items().get_count() ; i++) {
                    var object = {};
                    if (combo.get_items().getItem(i).get_checked()) {
                        object['ID'] = combo.get_items().getItem(i).get_value();
                        returnList.push(object);
                    }

                }


                if (zoomCheck == true) {
                    GraphSelectedCurvesClick(returnList, getEveryXCoord, getEveryYCoord, relativeX, relativeY);
                }

                else {
                    GraphSelectedCurvesZoom(returnList, getEveryXCoord, getEveryYCoord);
                }
       
            }
             
        
             return getEveryXCoord, getEveryYCoord;

         }

         function zoomOut() {
             if (getEveryXCoord > 5 & getEveryYCoord > -100) {

                 // Jd Loop Through Every X Coord of every Target To Set The Grid Numbers
                 getEveryXCoord = getEveryXCoord - 5;
                 getEveryYCoord = getEveryYCoord - 25;


                 // clear the graph and make a new 1
                 var canvasClear = document.getElementById('myCanvas');
                 var contextClear = canvasClear.getContext('2d');

                 contextClear.clearRect(0, 0, canvasClear.width, canvasClear.height);

                 var myGraph = new Graph({
                     canvasId: 'myCanvas',
                     minX: -10,
                     minY: -10,
                     maxX: 10,
                     maxY: 10,
                     unitsPerTick: 1
                 }, getEveryXCoord, getEveryYCoord, relativeX, relativeY);

                 if (redrawCheck == false) {

                     var raddropdownlist = $find('<%=ddlCurveGroup.ClientID %>');
                     var CurveGroupID = raddropdownlist.get_selectedItem().get_value();
                     if (CurveGroupID != 0) {


                         if (zoomCheck == true) {
                             ItemSelectedFromServerZoom2(CurveGroupID, getEveryXCoord, getEveryYCoord, relativeX, relativeY);
                         }
                         else {

                             ItemSelectedFromServer2(CurveGroupID, getEveryXCoord, getEveryYCoord);
                         }

                     }

                 }


                 else {
                     var combo = $find('<%=ddlCurve.ClientID %>');
                     var returnList = [];
                     for (var i = 0; i < combo.get_items().get_count() ; i++) {
                         var object = {};
                         if (combo.get_items().getItem(i).get_checked()) {
                             object['ID'] = combo.get_items().getItem(i).get_value();
                             returnList.push(object);
                         }

                     }

                     if (zoomCheck == true) {
                         GraphSelectedCurvesClick(returnList, getEveryXCoord, getEveryYCoord, relativeX, relativeY);
                     }

                     else {
                         GraphSelectedCurvesZoom(returnList, getEveryXCoord, getEveryYCoord);
                     }
                 }

                return getEveryXCoord, getEveryYCoord;
            }

         }


        // zoom in 2 for smaller graph

        function zoomIn2() {


            // Jd Loop Through Every X Coord of every Target To Set The Grid Numbers
            getEveryXCoordSGraph = getEveryXCoordSGraph + 5;
            getEveryYCoordSGraph = getEveryYCoordSGraph + 5;


            // clear the graph and make a new 1
            var canvasClear = document.getElementById('myCanvas2');
            var contextClear = canvasClear.getContext('2d');

            contextClear.clearRect(0, 0, canvasClear.width, canvasClear.height);

            var myGraph2 = new Graph2({
                canvasId: 'myCanvas2',
                minX: -10,
                minY: -10,
                maxX: 10,
                maxY: 10,
                unitsPerTick: 1
            }, getEveryXCoordSGraph, getEveryYCoordSGraph);

            if (redrawCheck == false) {
                var raddropdownlist = $find('<%=ddlCurveGroup.ClientID %>');
                var CurveGroupID = raddropdownlist.get_selectedItem().get_value();
                if (CurveGroupID != 0) {
                    ItemSelectedFromServer4(CurveGroupID, getEveryXCoordSGraph, getEveryYCoordSGraph);

                }
            }

            else {

                var combo = $find('<%=ddlCurve.ClientID %>');
                var returnList = [];
                for (var i = 0; i < combo.get_items().get_count() ; i++) {
                    var object = {};
                    if (combo.get_items().getItem(i).get_checked()) {
                        object['ID'] = combo.get_items().getItem(i).get_value();
                        returnList.push(object);
                    }

                }

                ZoomSmallerGraphCurves(returnList, getEveryXCoordSGraph, getEveryYCoordSGraph)

            }


          

            return getEveryXCoordSGraph, getEveryYCoordSGraph;

        }

        function zoomOut2() {
            if (getEveryXCoordSGraph > 5 & getEveryYCoordSGraph > 5) {

                // Jd Loop Through Every X Coord of every Target To Set The Grid Numbers
                getEveryXCoordSGraph = getEveryXCoordSGraph - 5;
                getEveryYCoordSGraph = getEveryYCoordSGraph - 5;


                // clear the graph and make a new 1
                var canvasClear = document.getElementById('myCanvas2');
                var contextClear = canvasClear.getContext('2d');

                contextClear.clearRect(0, 0, canvasClear.width, canvasClear.height);

                var myGraph2 = new Graph2({
                    canvasId: 'myCanvas2',
                    minX: -10,
                    minY: -10,
                    maxX: 10,
                    maxY: 10,
                    unitsPerTick: 1
                }, getEveryXCoordSGraph, getEveryYCoordSGraph);


                if (redrawCheck == false) {
                    var raddropdownlist = $find('<%=ddlCurveGroup.ClientID %>');
                   var CurveGroupID = raddropdownlist.get_selectedItem().get_value();
                   if (CurveGroupID != 0) {
                       ItemSelectedFromServer4(CurveGroupID, getEveryXCoordSGraph, getEveryYCoordSGraph);

                   }
               }

               else {

                   var combo = $find('<%=ddlCurve.ClientID %>');
                   var returnList = [];
                   for (var i = 0; i < combo.get_items().get_count() ; i++) {
                       var object = {};
                       if (combo.get_items().getItem(i).get_checked()) {
                           object['ID'] = combo.get_items().getItem(i).get_value();
                           returnList.push(object);
                       }

                   }

                   ZoomSmallerGraphCurves(returnList, getEveryXCoordSGraph, getEveryYCoordSGraph)

               }


                 return getEveryXCoordSGraph, getEveryYCoordSGraph;
             }

         }
    </script>

</asp:Content>

