<%@ Page Title="Create Targets" Language="C#" MasterPageFile="~/Masters/RigTrack.master" AutoEventWireup="true" CodeFile="CreateTargets.aspx.cs" Inherits="Modules_RigTrack_CreateTargets" EnableEventValidation="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="customCss" runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script type="text/javascript">
        var $ = $telerik.$;
        var graphWindow;

        function pageLoad() {
            graphWindow = $find("<%=GraphWindow.ClientID %>");
        }

        function OpenGraphWindow() {
            var curveGroupID = $find("<%= ddlCurveGroup.ClientID %>");
            curveGroupID = curveGroupID.get_selectedItem().get_value();
            //alert(curveGroupID);
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
            graphWindow.show();
            setGraphWindowSize(curveGroupID);
        }

        function setWindowSize() {
            var viewportWidth = $(window).width();
            var viewportHeight = $(window).height();
            graphWindow.setSize(Math.ceil(viewportWidth - 90), Math.ceil(viewportHeight - 90));
        }

        $(window).resize(function(){
            if (graphWindow.isVisible()) {
                setWindowSize();
            }
        });

        function setGraphWindowSize(CurveGroupID) {
            setWindowSize();
            graphWindow.setUrl("CreateGraph.aspx?CurveGroupID=" + CurveGroupID);
            graphWindow.center();
            graphWindow.set_modal(true);
        }

        function openwin() {
            window.radopen(null, "window_service");
        }


    </script>

    <script type="text/javascript">

        function OnClientItemSelected(sender, eventArgs) {

            var item = eventArgs.get_item();
            //var TargetOffset = 
            document.getElementById("<%= hidShapeType.ClientID %>").value = item.get_text();
            var lblTargetXOffset = document.getElementById("<%= txtTargetXOffset.ClientID %>");
            var lblTargetYOffset = document.getElementById("<%= txtTargetYOffset.ClientID %>");

            var lblXDiameter = document.getElementById("<%= txtXDiameter.ClientID %>");
            var lblYDiameter = document.getElementById("<%= txtYDiameter.ClientID %>");

            var lblXCorner1 = document.getElementById("<%= txtXCorner1.ClientID %>");
            var lblYCorner1 = document.getElementById("<%= txtYCorner1.ClientID %>");

            var lblXCorner2 = document.getElementById("<%= txtXCorner2.ClientID %>");
            var lblYCorner2 = document.getElementById("<%= txtYCorner2.ClientID %>");

            var lblXCorner3 = document.getElementById("<%= txtXCorner3.ClientID %>");
            var lblYCorner3 = document.getElementById("<%= txtYCorner3.ClientID %>");

            var lblXCorner4 = document.getElementById("<%= txtXCorner4.ClientID %>");
            var lblYCorner4 = document.getElementById("<%= txtYCorner4.ClientID %>");

            var lblTargetOffset = document.getElementById("<%= lblTargetOffset.ClientID %>");
            var lblCorner1 = document.getElementById("<%= lblCorner1.ClientID %>");
            var lblCorner2 = document.getElementById("<%= lblCorner2.ClientID %>");
            var lblCorner3 = document.getElementById("<%= lblCorner3.ClientID %>");
            var lblCorner4 = document.getElementById("<%= lblCorner4.ClientID %>");
            //var svgcircle = document.getElementById("svgcircle");
            //var svgrect = document.getElementById("svgrect");
            //var svgellipse = document.getElementById("svgellipse");
            //var svgpolygon = document.getElementById("svgpolygon");
            //var svgsquare = document.getElementById("svgsquare");
            if (item.get_text() == "Circle") {
                var HTML = "<svg height='250' width='250' id='svgcircle' viewPort='0 0 120 120'><defs>";
                HTML += "<pattern width='6' id='circleverticle' height='6' patternUnits='userSpaceOnUse'>";
                HTML += "<path d='M 0 0 L 0 0 0 10' fill='none' stroke='gray' stroke-width='1'/>";
                HTML += "</pattern>";
                HTML += "</defs>";
                HTML += "<circle cx='60' cy='60' r='3' style='stroke: none; fill:red;'/>";
                HTML += "<circle cx='60' cy='60' r='58' stroke='black' stroke-width='2' fill='url(#circleverticle)' /></svg>";
                document.getElementById("divCircle").innerHTML = HTML;
                //svgcircle.style.display = "block";
                //svgrect.style.display = "none";
                //svgellipse.style.display = "none";
                //svgpolygon.style.display = "none";
                //svgsquare.style.display = "none";
                lblTargetOffset.innerHTML = "Diameter of Circle";
                lblCorner1.innerHTML = "Corner 1";
                lblCorner2.innerHTML = "Corner 2";
                lblCorner3.innerHTML = "Corner 3";
                lblCorner4.innerHTML = "Corner 4";

                lblTargetXOffset.value = ".00";
                lblTargetYOffset.value = ".00";

                lblXDiameter.value = ".00";
                lblYDiameter.style.visibility = "hidden";

                lblXCorner1.value = ".00";
                lblYCorner1.value = ".00";

                lblXCorner2.value = ".00";
                lblYCorner2.value = ".00";

                lblXCorner3.value = ".00";
                lblYCorner3.value = ".00";

                lblXCorner4.value = ".00";
                lblYCorner4.value = ".00";
                lblXDiameter.style.visibility = "visible";
                lblYDiameter.style.visibility = "visible";

                lblXCorner1.style.visibility = "visible";
                lblYCorner1.style.visibility = "visible";

                lblXCorner2.style.visibility = "visible";
                lblYCorner2.style.visibility = "visible";

                lblXCorner3.style.visibility = "visible";
                lblYCorner3.style.visibility = "visible";

                lblXCorner4.style.visibility = "visible";
                lblYCorner4.style.visibility = "visible";
            }
            else if (item.get_text() == "Square") {

                //svgcircle.style.display = "none";
                //svgrect.style.display = "none";
                //svgellipse.style.display = "none";
                //svgpolygon.style.display = "none";
                //svgsquare.style.display = "block";

                lblTargetOffset.innerHTML = "Length of Side";
                lblCorner1.innerHTML = "Corner 1";
                lblCorner2.innerHTML = "Corner 2";
                lblCorner3.innerHTML = "Corner 3";
                lblCorner4.innerHTML = "Corner 4";

                lblTargetXOffset.value = ".00";
                lblTargetYOffset.value = ".00";

                lblXDiameter.value = ".00";
                lblYDiameter.style.visibility = "hidden";

                lblXCorner1.value = ".00";
                lblYCorner1.value = ".00";

                lblXCorner2.value = ".00";
                lblYCorner2.value = ".00";

                lblXCorner3.value = ".00";
                lblYCorner3.value = ".00";

                lblXCorner4.value = ".00";
                lblYCorner4.value = ".00";
                lblXDiameter.style.visibility = "visible";
                lblYDiameter.style.visibility = "visible";

                lblXCorner1.style.visibility = "visible";
                lblYCorner1.style.visibility = "visible";

                lblXCorner2.style.visibility = "visible";
                lblYCorner2.style.visibility = "visible";

                lblXCorner3.style.visibility = "visible";
                lblYCorner3.style.visibility = "visible";

                lblXCorner4.style.visibility = "visible";
                lblYCorner4.style.visibility = "visible";
            }
            else if (item.get_text() == "Rectangle") {
                var HTML = "<div style='width:200px;height:200px;border:solid 1px #000000'><svg height='200px' width='200px' id='svgrectangle' ><defs>";
                HTML += "<pattern width='8' id='rectangleverticle' height='8' patternUnits='userSpaceOnUse'>";
                HTML += "<path d='M 8 0 L 0 0 0' fill='none' stroke='gray' stroke-width='1'/>";
                HTML += "</pattern>";
                HTML += "<pattern id='grid' width='200' height='200' patternUnits='userSpaceOnUse'>";
                HTML += "<rect width='200' height='200' fill='url(#rectangleverticle)'/>";
                //HTML += "<path d='M 0 0 L 0 0 0 185' fill='none' stroke='Black' stroke-width='2'/>";
                HTML += "</pattern>"
                HTML += "</defs>";
                HTML += "<circle cx='100' cy='100' r='3' style='stroke: none; fill:red;'/>";
                HTML += "<rect width='200' height='200' fill='url(#grid)' /></svg></div>";
                document.getElementById("divCircle").innerHTML = HTML;
                lblTargetOffset.innerHTML = "X and Y Lengths";
                lblYDiameter.style.visibility = "visible";
                lblCorner1.innerHTML = "Corner 1";
                lblCorner2.innerHTML = "Corner 2";
                lblCorner3.innerHTML = "Corner 3";
                lblCorner4.innerHTML = "Corner 4";

                lblTargetXOffset.value = ".00";
                lblTargetYOffset.value = ".00";

                lblXDiameter.value = ".00";
                lblYDiameter.value = ".00";

                lblXCorner1.value = ".00";
                lblYCorner1.value = ".00";

                lblXCorner2.value = ".00";
                lblYCorner2.value = ".00";

                lblXCorner3.value = ".00";
                lblYCorner3.value = ".00";

                lblXCorner4.value = ".00";
                lblYCorner4.value = ".00";
                lblXDiameter.style.visibility = "visible";
                lblYDiameter.style.visibility = "visible";

                lblXCorner1.style.visibility = "visible";
                lblYCorner1.style.visibility = "visible";

                lblXCorner2.style.visibility = "visible";
                lblYCorner2.style.visibility = "visible";

                lblXCorner3.style.visibility = "visible";
                lblYCorner3.style.visibility = "visible";

                lblXCorner4.style.visibility = "visible";
                lblYCorner4.style.visibility = "visible";
            }
            else if (item.get_text() == "Polygon") {
                var HTML = "<svg width='220' height='250' style='border:solid 1px #000000'><defs>";
                HTML += "<pattern id='Pattern2' width='8' height='8' patternUnits='userSpaceOnUse'>";
                HTML += "<path d='M 8 0 L 0 0 0 0' fill='none' stroke='gray' stroke-width='1'/>";
                HTML += "</pattern>";
                HTML += "<pattern id='Pattern3' width='200' height='210' patternUnits='userSpaceOnUse'>";
                HTML += "<rect width='200' height='210' fill='url(#Pattern2)' />";
                HTML += "</pattern>"
                HTML += "</defs>";
                HTML += "<circle cx='110' cy='110' r='3' style='stroke: none; fill:red;'/>";
                HTML += "<polygon points='100,3 220,210 15,250' fill='url(#Pattern3)' style='stroke:black;stroke-width:1' /></svg>";
                document.getElementById("divCircle").innerHTML = HTML;
                //svgcircle.style.display = "none";
                //svgrect.style.display = "none";
                //svgellipse.style.display = "none";
                //svgpolygon.style.display = "block";
                //svgsquare.style.display = "none";
                lblTargetOffset.innerHTML = "Vertex 1";
                lblCorner1.innerHTML = "Vertex 2";
                lblCorner2.innerHTML = "Vertex 3";
                lblCorner3.innerHTML = "Vertex 4";
                lblCorner4.innerHTML = "";
                lblYDiameter.style.visibility = "visible";
                lblTargetXOffset.value = ".00";
                lblTargetYOffset.value = ".00";

                lblXDiameter.value = ".00";
                lblYDiameter.value = ".00";

                lblXCorner1.value = ".00";
                lblYCorner1.value = ".00";

                lblXCorner2.value = ".00";
                lblYCorner2.value = ".00";

                lblXCorner3.value = ".00";
                lblYCorner3.value = ".00";
                lblXDiameter.style.visibility = "visible";
                lblYDiameter.style.visibility = "visible";

                lblXCorner1.style.visibility = "visible";
                lblYCorner1.style.visibility = "visible";

                lblXCorner2.style.visibility = "visible";
                lblYCorner2.style.visibility = "visible";

                lblXCorner3.style.visibility = "visible";
                lblYCorner3.style.visibility = "visible";

                lblXCorner4.style.visibility = "hidden";
                lblYCorner4.style.visibility = "hidden";
            }
            else if (item.get_text() == "Ellipse") {

                var HTML = "<svg height='120' width='180' id='svgellipse' ><defs>";
                HTML += "<pattern width='8' id='ellipseverticle' height='8' patternUnits='userSpaceOnUse'>";
                HTML += "<path d='M 0 0 L 0 0 0 8' fill='none' stroke='gray' stroke-width='1'/>";
                HTML += "</pattern>";
                HTML += "<pattern id='gridellipse' width='200' height='120' patternUnits='userSpaceOnUse' >";
                HTML += "<rect width='200' height='120' fill='url(#ellipseverticle)'/>";
                HTML += "</pattern>"
                HTML += "</defs>";
                HTML += "<circle cx='80' cy='80' r='3' style='stroke: none; fill:red;'/>";
                HTML += "<ellipse cx='80' cy='80' rx='70' ry='40' fill='url(#gridellipse)' stroke='black' stroke-width='1'/></svg>";
                document.getElementById("divCircle").innerHTML = HTML;
                lblTargetOffset.innerHTML = "X and Y Lengths";
                lblCorner1.innerHTML = "Corner 1";
                lblCorner2.innerHTML = "Corner 2";
                lblCorner3.innerHTML = "Corner 3";
                lblCorner4.innerHTML = "Corner 4";
                lblYDiameter.style.visibility = "hidden";
                lblTargetXOffset.value = ".00";
                lblTargetYOffset.value = ".00";

                lblXDiameter.value = ".00";
                lblYDiameter.value = ".00";

                lblXCorner1.value = ".00";
                lblYCorner1.value = ".00";

                lblXCorner2.value = ".00";
                lblYCorner2.value = ".00";

                lblXCorner3.value = ".00";
                lblYCorner3.value = ".00";

                lblXCorner4.value = ".00";
                lblYCorner4.value = ".00";
                lblXDiameter.style.visibility = "visible";
                lblYDiameter.style.visibility = "visible";

                lblXCorner1.style.visibility = "visible";
                lblYCorner1.style.visibility = "visible";

                lblXCorner2.style.visibility = "visible";
                lblYCorner2.style.visibility = "visible";

                lblXCorner3.style.visibility = "visible";
                lblYCorner3.style.visibility = "visible";

                lblXCorner4.style.visibility = "visible";
                lblYCorner4.style.visibility = "visible";
            }
            else if (item.get_text() == "Point") {

                lblTargetOffset.innerHTML = "";

                lblCorner1.innerHTML = "";
                lblCorner2.innerHTML = "";
                lblCorner3.innerHTML = "";
                lblCorner4.innerHTML = "";

                lblTargetXOffset.value = ".00";
                lblTargetYOffset.value = ".00";

                lblXDiameter.style.visibility = "hidden";
                lblYDiameter.style.visibility = "hidden";

                lblXCorner1.style.visibility = "hidden";
                lblYCorner1.style.visibility = "hidden";

                lblXCorner2.style.visibility = "hidden";
                lblYCorner2.style.visibility = "hidden";

                lblXCorner3.style.visibility = "hidden";
                lblYCorner3.style.visibility = "hidden";

                lblXCorner4.style.visibility = "hidden";
                lblYCorner4.style.visibility = "hidden";
            }
            var lblTargetShapeName = document.getElementById("<%= lblTargetShapeName.ClientID %>");
            lblTargetShapeName.innerHTML = item.get_text();
            
            var radwindow = $find('<%=RadWindow1.ClientID %>');
            radwindow.show();
        }
              function uncheckOther(chk, index,tID) {
                  var grid = $find("<%=RadGrid1.ClientID %>");

            if (grid) {
                var MasterTable = grid.get_masterTableView();
                var Rows = MasterTable.get_dataItems();

                for (var i = 0; i < Rows.length; i++) {
                    var row = Rows[i];
                    var Chk1 = $(row.get_element()).find("input[id*='checkColumn']").get(0);

                    if (Chk1.id != chk.id) {
                        Chk1.checked = false;
                        var masterTableView = grid.get_masterTableView();
                        var row = masterTableView.get_dataItems()[index];
                        row.disabled = true;

                    }
                    else {
                        
                        
                        var lblTargetSelectedID = document.getElementById("<%= lblTargetSelectedID.ClientID %>");
                        
                        lblTargetSelectedID.innerHTML = tID;
                        
                        var masterTableView = grid.get_masterTableView();
                        var row = masterTableView.get_dataItems()[index];
                        row.disabled = false;
                        
                        
                        
                    }
                }
            }
        }
        //function KeyPressed(key) {
        //    if (gridCtrl.get_element().disabled) {
        //        return false;
        //    }
        //}
        function CaliculateValues() {
            
            var lblXDiameter = document.getElementById("<%= txtXDiameter.ClientID %>");
            var lblYDiameter = document.getElementById("<%= txtYDiameter.ClientID %>");

            var lblXCorner1 = document.getElementById("<%= txtXCorner1.ClientID %>");
            var lblYCorner1 = document.getElementById("<%= txtYCorner1.ClientID %>");

            var lblXCorner2 = document.getElementById("<%= txtXCorner2.ClientID %>");
            var lblYCorner2 = document.getElementById("<%= txtYCorner2.ClientID %>");

            var lblXCorner3 = document.getElementById("<%= txtXCorner3.ClientID %>");
            var lblYCorner3 = document.getElementById("<%= txtYCorner3.ClientID %>");

            var lblXCorner4 = document.getElementById("<%= txtXCorner4.ClientID %>");
            var lblYCorner4 = document.getElementById("<%= txtYCorner4.ClientID %>");
            var shape = document.getElementById("<%= hidShapeType.ClientID %>").value;
            if (shape == "Circle") {
                if (lblXDiameter.value != "") {
                    var cal1 = lblXDiameter.value / 2;
                    lblXCorner1.value = "-" + cal1;
                    lblXCorner2.value = cal1;
                    lblXCorner3.value = cal1;
                    lblXCorner4.value = "-" + cal1;

                    lblYCorner1.value = cal1;
                    lblYCorner2.value = cal1;
                    lblYCorner3.value = "-" + cal1;
                    lblYCorner4.value = "-" + cal1;
                }
            }
            if (shape == "Rectangle") {
                if (lblXDiameter.value != "" && lblYDiameter.value != "") {
                    var cal1 = lblXDiameter.value / 2;

                    lblXCorner1.value = "-" + cal1;
                    lblXCorner2.value = cal1;
                    lblXCorner3.value = cal1;
                    lblXCorner4.value = "-" + cal1;

                }
            }

        }
        function CaliculateValuesRectangle() {
            var lblXDiameter = document.getElementById("<%= txtXDiameter.ClientID %>");
            var lblYDiameter = document.getElementById("<%= txtYDiameter.ClientID %>");

            var lblXCorner1 = document.getElementById("<%= txtXCorner1.ClientID %>");
            var lblYCorner1 = document.getElementById("<%= txtYCorner1.ClientID %>");

            var lblXCorner2 = document.getElementById("<%= txtXCorner2.ClientID %>");
            var lblYCorner2 = document.getElementById("<%= txtYCorner2.ClientID %>");

            var lblXCorner3 = document.getElementById("<%= txtXCorner3.ClientID %>");
            var lblYCorner3 = document.getElementById("<%= txtYCorner3.ClientID %>");

            var lblXCorner4 = document.getElementById("<%= txtXCorner4.ClientID %>");
            var lblYCorner4 = document.getElementById("<%= txtYCorner4.ClientID %>");
            var shape = document.getElementById("<%= hidShapeType.ClientID %>").value;
            if (shape == "Rectangle") {
                if (lblYDiameter.value != "") {

                    var cal2 = lblYDiameter.value / 2;

                    lblYCorner1.value = cal2;
                    lblYCorner2.value = cal2;
                    lblYCorner3.value = "-" + cal2;
                    lblYCorner4.value = "-" + cal2;
                }
            }
        }
        function OnChangeCaliculations(obj1, obj2, objinclast, objazm, obj3, objPolardist, objpolardirection) {
            var tvdval = document.getElementById(obj2).value;
            var tvdval1 = document.getElementById(obj3).value;
            var NSval = document.getElementById(obj2).value;
            var EWval = document.getElementById(obj3).value;
            var TVDval3 = document.getElementById(obj1).value;

            $.ajax({
                type: "POST",
                url: 'CreateTargets.aspx/CaliculatePloarDirection',
                data: '{north: "' + tvdval + '",east: "' + tvdval1 + '" }',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (data) {
                    var displayval = data.d;
                    var splitval = displayval.split(',');
                    document.getElementById(objpolardirection).value = splitval[0];
                    document.getElementById(objPolardist).value = splitval[1];
                    document.getElementById(objinclast).value = 176 + (TVDval3 * .0001) + (NSval * .001) - (EWval * .001);
                    document.getElementById(objazm).value = 269.2279 + (TVDval3 * .0001) + (NSval * .002) - (EWval * .003);
                },
                failure: function (response) {
                    var r = jQuery.parseJSON(response.responseText);
                    alert("Message: " + r.Message);
                    alert("StackTrace: " + r.StackTrace);
                    alert("ExceptionType: " + r.ExceptionType);
                }
            });


        }
        //function OnRowSelecting(sender, args) 
        //{ 
        //    var key=args.getDataKeyValue("ID"); 
        //    alert(key); 
        //} 
        function closePopup() {
            var radwindow = $find('<%=RadWindow1.ClientID %>');
            radwindow.hide();
            return false;
        }
        

        function TargetShapeDetails() {
            
            var targetShapeDetails = {
                targetID: document.getElementById("<%= lblTargetSelectedID.ClientID %>").innerHTML,
                TargetXOffset: (document.getElementById("<%= txtTargetXOffset.ClientID %>").value=="")?"0.00":document.getElementById("<%= txtTargetXOffset.ClientID %>").value,
                TargetYOffset: (document.getElementById("<%= txtTargetYOffset.ClientID %>").value=="")?"0.00":document.getElementById("<%= txtTargetYOffset.ClientID %>").value,
                XDiameter: (document.getElementById("<%= txtXDiameter.ClientID %>").value=="")?"0.00":document.getElementById("<%= txtYDiameter.ClientID %>").value,
                YDiameter: (document.getElementById("<%= txtYDiameter.ClientID %>").value=="")?"0.00":document.getElementById("<%= txtYDiameter.ClientID %>").value,
                XCorner1: (document.getElementById("<%= txtXCorner1.ClientID %>").value=="")?"0.00":document.getElementById("<%= txtXCorner1.ClientID %>").value,
                YCorner1: (document.getElementById("<%= txtYCorner1.ClientID %>").value=="")?"0.00":document.getElementById("<%= txtYCorner1.ClientID %>").value,
                XCorner2: (document.getElementById("<%= txtXCorner2.ClientID %>").value == "") ? "0.00" : document.getElementById("<%= txtXCorner2.ClientID %>").value,
                YCorner2: (document.getElementById("<%= txtYCorner2.ClientID %>").value == "") ? "0.00" : document.getElementById("<%= txtYCorner2.ClientID %>").value,
                XCorner3: (document.getElementById("<%= txtXCorner3.ClientID %>").value == "") ? "0.00" : document.getElementById("<%= txtXCorner3.ClientID %>").value,
                YCorner3: (document.getElementById("<%= txtYCorner3.ClientID %>").value == "") ? "0.00" : document.getElementById("<%= txtYCorner3.ClientID %>").value,
                XCorner4: (document.getElementById("<%= txtXCorner4.ClientID %>").value == "") ? "0.00" : document.getElementById("<%= txtXCorner4.ClientID %>").value,
                YCorner4: (document.getElementById("<%= txtYCorner4.ClientID %>").value == "") ? "0.00" : document.getElementById("<%= txtYCorner4.ClientID %>").value
            }

            $.ajax({

                type: "POST",
                url: 'CreateTargets.aspx/AddTargetShapeDetails',
                data: "{TargetDetails:" + JSON.stringify(targetShapeDetails) + "}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (data) {
                    
                },
                failure: function (response) {
                    var r = jQuery.parseJSON(response.responseText);
                    alert("Message: " + r.Message);
                    alert("StackTrace: " + r.StackTrace);
                    alert("ExceptionType: " + r.ExceptionType);
                }
            });
            var radwindow = $find('<%=RadWindow1.ClientID %>');
                        radwindow.hide();
            return false;
        }
        function OnPatternChanged(shapeName, index, shapePattern)
        {
            var shape = document.getElementById(shapeName).innerText.trim().toLowerCase();
            var Pattern = document.getElementById(shapePattern).innerText.trim().toLowerCase();
           
            var lblTargetXOffset = document.getElementById("<%= txtTargetXOffset.ClientID %>");
            var lblTargetYOffset = document.getElementById("<%= txtTargetYOffset.ClientID %>");

            var lblXDiameter = document.getElementById("<%= txtXDiameter.ClientID %>");
            var lblYDiameter = document.getElementById("<%= txtYDiameter.ClientID %>");

            var lblXCorner1 = document.getElementById("<%= txtXCorner1.ClientID %>");
            var lblYCorner1 = document.getElementById("<%= txtYCorner1.ClientID %>");

            var lblXCorner2 = document.getElementById("<%= txtXCorner2.ClientID %>");
            var lblYCorner2 = document.getElementById("<%= txtYCorner2.ClientID %>");

            var lblXCorner3 = document.getElementById("<%= txtXCorner3.ClientID %>");
            var lblYCorner3 = document.getElementById("<%= txtYCorner3.ClientID %>");

            var lblXCorner4 = document.getElementById("<%= txtXCorner4.ClientID %>");
            var lblYCorner4 = document.getElementById("<%= txtYCorner4.ClientID %>");

            var lblTargetOffset = document.getElementById("<%= lblTargetOffset.ClientID %>");
            var lblCorner1 = document.getElementById("<%= lblCorner1.ClientID %>");
            var lblCorner2 = document.getElementById("<%= lblCorner2.ClientID %>");
            var lblCorner3 = document.getElementById("<%= lblCorner3.ClientID %>");
            var lblCorner4 = document.getElementById("<%= lblCorner4.ClientID %>");
            
           
            if (shape == "circle") {

                


                
                if (Pattern == "vertical line") {
                    
                    var HTML = "<svg height='250' width='250' id='svgcircle' viewPort='0 0 120 120'><defs>";
                    HTML += "<pattern width='6' id='circleverticle' height='6' patternUnits='userSpaceOnUse'>";
                    HTML += "<path d='M 0 0 L 0 0 0 10' fill='none' stroke='gray' stroke-width='1'/>";
                    HTML += "</pattern>";
                    HTML += "</defs>";
                    HTML += "<circle cx='60' cy='60' r='3' style='stroke: none; fill:red;'/>";
                    HTML += "<circle cx='60' cy='60' r='58' stroke='black' stroke-width='2' fill='url(#circleverticle)' /></svg>";
                    document.getElementById("divCircle").innerHTML = HTML;
                    
                }
                else if(Pattern=="horizontal"){
                    var HTML = "<svg height='250' width='250' id='svgcircle' viewPort='0 0 120 120'><defs>";
                    HTML += "<pattern width='6' id='circleverticle' height='6' patternUnits='userSpaceOnUse'>";
                    HTML += "<path d='M 10 0 L 0 0 0 0' fill='none' stroke='gray' stroke-width='1'/>";
                    HTML += "</pattern>";
                    HTML += "</defs>";
                    HTML += "<circle cx='60' cy='60' r='3' style='stroke: none; fill:red;'/>";
                    HTML += "<circle cx='60' cy='60' r='58' stroke='black' stroke-width='2' fill='url(#circleverticle)' /></svg>";
                    document.getElementById("divCircle").innerHTML = HTML;
                }
                else if (Pattern == "upward diagonal") {
                    var HTML = "<svg height='250' width='250' id='svgcircle' viewPort='0 0 120 120'><defs>";
                    HTML += "<pattern width='6' id='circleverticle' height='6' patternUnits='userSpaceOnUse' patternTransform='rotate(30)'>";
                    HTML += "<path d='M 10 0 L 0 0 0 0' fill='none' stroke='gray' stroke-width='1'/>";
                    HTML += "</pattern>";
                    HTML += "</defs>";
                    HTML += "<circle cx='60' cy='60' r='3' style='stroke: none; fill:red;'/>";
                    HTML += "<circle cx='60' cy='60' r='58' stroke='black' stroke-width='2' fill='url(#circleverticle)' /></svg>";
                    document.getElementById("divCircle").innerHTML = HTML;
                }
                else if (Pattern == "downward diagonal") {
                    var HTML = "<svg height='250' width='250' id='svgcircle' viewPort='0 0 120 120'><defs>";
                    HTML += "<pattern width='6' id='circleverticle' height='6' patternUnits='userSpaceOnUse' patternTransform='rotate(30)'>";
                    HTML += "<path d='M 0 0 L 0 0 0 10' fill='none' stroke='gray' stroke-width='1'/>";
                    HTML += "</pattern>";
                    HTML += "</defs>";
                    HTML += "<circle cx='60' cy='60' r='3' style='stroke: none; fill:red;'/>";
                    HTML += "<circle cx='60' cy='60' r='58' stroke='black' stroke-width='2' fill='url(#circleverticle)' /></svg>";
                    document.getElementById("divCircle").innerHTML = HTML;
                }
                else if (Pattern == "cross") {
                    var HTML = "<svg height='250' width='250' id='svgcircle' viewPort='0 0 120 120'><defs>";
                    HTML += "<pattern width='6' id='circleverticle' height='6' patternUnits='userSpaceOnUse' >";
                    HTML += "<path d='M 10 0 L 0 0 0 10' fill='none' stroke='gray' stroke-width='1'/>";
                    HTML += "</pattern>";
                    HTML += "</defs>";
                    HTML += "<circle cx='60' cy='60' r='3' style='stroke: none; fill:red;'/>";
                    HTML += "<circle cx='60' cy='60' r='58' stroke='black' stroke-width='2' fill='url(#circleverticle)' /></svg>";
                    document.getElementById("divCircle").innerHTML = HTML;
                }
                else if (Pattern == "diagonal cross") {
                    var HTML = "<svg height='250' width='250' id='svgcircle' viewPort='0 0 120 120'><defs>";
                    HTML += "<pattern width='6' id='circleverticle' height='6' patternUnits='userSpaceOnUse' patternTransform='rotate(30)'>";
                    HTML += "<path d='M 10 0 L 0 0 0 10' fill='none' stroke='gray' stroke-width='1'/>";
                    HTML += "</pattern>";
                    HTML += "</defs>";
                    HTML += "<circle cx='60' cy='60' r='3' style='stroke: none; fill:red;'/>";
                    HTML += "<circle cx='60' cy='60' r='58' stroke='black' stroke-width='2' fill='url(#circleverticle)' /></svg>";
                    document.getElementById("divCircle").innerHTML = HTML;
                }
                
                
                lblTargetOffset.innerHTML = "Diameter of Circle";
                lblCorner1.innerHTML = "Corner 1";
                lblCorner2.innerHTML = "Corner 2";
                lblCorner3.innerHTML = "Corner 3";
                lblCorner4.innerHTML = "Corner 4";

                lblYDiameter.style.visibility = "hidden";

                lblXDiameter.style.visibility = "visible";
                lblYDiameter.style.visibility = "visible";

                lblXCorner1.style.visibility = "visible";
                lblYCorner1.style.visibility = "visible";

                lblXCorner2.style.visibility = "visible";
                lblYCorner2.style.visibility = "visible";

                lblXCorner3.style.visibility = "visible";
                lblYCorner3.style.visibility = "visible";

                lblXCorner4.style.visibility = "visible";
                lblYCorner4.style.visibility = "visible";
            }
            else if (shape == "square") {

                
                lblTargetOffset.innerHTML = "Length of Side";
                lblCorner1.innerHTML = "Corner 1";
                lblCorner2.innerHTML = "Corner 2";
                lblCorner3.innerHTML = "Corner 3";
                lblCorner4.innerHTML = "Corner 4";

                lblYDiameter.style.visibility = "hidden";

                lblXDiameter.style.visibility = "visible";
                lblYDiameter.style.visibility = "visible";

                lblXCorner1.style.visibility = "visible";
                lblYCorner1.style.visibility = "visible";

                lblXCorner2.style.visibility = "visible";
                lblYCorner2.style.visibility = "visible";

                lblXCorner3.style.visibility = "visible";
                lblYCorner3.style.visibility = "visible";

                lblXCorner4.style.visibility = "visible";
                lblYCorner4.style.visibility = "visible";
            }
            else if (shape == "rectangle") {
                
                if (Pattern == "vertical line") {

                    var HTML = "<div style='width:200px;height:200px;border:solid 1px #000000'><svg height='200px' width='200px' id='svgrectangle' ><defs>";
                    HTML += "<pattern width='8' id='rectangleverticle' height='8' patternUnits='userSpaceOnUse'>";
                    HTML += "<path d='M 8 0 L 0 0 0' fill='none' stroke='gray' stroke-width='1'/>";
                    HTML += "</pattern>";
                    HTML += "<pattern id='grid' width='200' height='200' patternUnits='userSpaceOnUse'>";
                    HTML += "<rect width='200' height='200' fill='url(#rectangleverticle)'/>";
                    //HTML += "<path d='M 0 0 L 0 0 0 185' fill='none' stroke='Black' stroke-width='2'/>";
                    HTML += "</pattern>"
                    HTML += "</defs>";
                    HTML+="<circle cx='100' cy='100' r='3' style='stroke: none; fill:red;'/>";
                    HTML += "<rect width='200' height='200' fill='url(#grid)' /></svg></div>";
                    document.getElementById("divCircle").innerHTML = HTML;

                }
                else if (Pattern == "horizontal") {
                    var HTML = "<div style='width:200px;height:200px;border:solid 1px #000000'><svg height='200px' width='200px' id='svgrectangle'><defs>";
                    HTML += "<pattern width='8' id='rectangleverticle' height='8' patternUnits='userSpaceOnUse'>";
                    HTML += "<path d='M 8 0 L 0 0 0' fill='none' stroke='gray' stroke-width='1'/>";
                    HTML += "</pattern>";
                    HTML += "<pattern id='grid' width='200' height='200' patternUnits='userSpaceOnUse'>";
                    HTML += "<rect width='200' height='200' fill='url(#rectangleverticle)'/>";
                    //HTML += "<path d='M 200 0 L 0 0 0 0' fill='none' stroke='Black' stroke-width='2'/>";
                    HTML += "</pattern>"
                    HTML += "</defs>";
                    HTML += "<circle cx='100' cy='100' r='3' style='stroke: none; fill:red;'/>";
                    HTML += "<rect width='200' height='200' fill='url(#grid)' /></svg></div>";
                    document.getElementById("divCircle").innerHTML = HTML;
                }
                else if (Pattern == "upward diagonal") {
                    var HTML = "<div style='width:200px;height:200px;border:solid 1px #000000'><svg height='200px' width='200px' id='svgrectangle' ><defs>";
                    HTML += "<pattern width='8' id='rectangleverticle' height='8' patternUnits='userSpaceOnUse'>";
                    HTML += "<path d='M 8 0 L 0 0 0' fill='none' stroke='gray' stroke-width='1'/>";
                    HTML += "</pattern>";
                    HTML += "<pattern id='grid' width='200' height='200' patternUnits='userSpaceOnUse' patternTransform='rotate(30)'>";
                    HTML += "<rect width='200' height='200' fill='url(#rectangleverticle)'/>";
                    //HTML += "<path d='M 185 0 L 0 0 0 0' fill='none' stroke='Black' stroke-width='2'/>";
                    HTML += "</pattern>"
                    HTML += "</defs>";
                    HTML += "<circle cx='100' cy='100' r='3' style='stroke: none; fill:red;'/>";
                    HTML += "<rect width='200' height='200' fill='url(#grid)' /></svg></div>";
                    document.getElementById("divCircle").innerHTML = HTML;

                }
                else if (Pattern == "downward diagonal") {
                    var HTML = "<div style='width:200px;height:200px;border:solid 1px #000000'><svg height='200px' width='200px' id='svgrectangle' ><defs>";
                    HTML += "<pattern width='8' id='rectangleverticle' height='8' patternUnits='userSpaceOnUse'>";
                    HTML += "<path d='M 8 0 L 0 0 0' fill='none' stroke='gray' stroke-width='1'/>";
                    HTML += "</pattern>";
                    HTML += "<pattern id='grid' width='200' height='200' patternUnits='userSpaceOnUse' patternTransform='rotate(30)'>";
                    HTML += "<rect width='200' height='200' fill='url(#rectangleverticle)'/>";
                    //HTML += "<path d='M 0 0 L 0 0 0 185' fill='none' stroke='Black' stroke-width='2'/>";
                    HTML += "</pattern>"
                    HTML += "</defs>";
                    HTML += "<circle cx='100' cy='100' r='3' style='stroke: none; fill:red;'/>";
                    HTML += "<rect width='200' height='200' fill='url(#grid)' /></svg></div>";
                    document.getElementById("divCircle").innerHTML = HTML;
                }
                else if (Pattern == "cross") {
                    var HTML = "<div style='width:200px;height:200px;border:solid 1px #000000'><svg height='200px' width='200px' id='svgrectangle' ><defs>";
                    HTML += "<pattern width='8' id='rectangleverticle' height='8' patternUnits='userSpaceOnUse'>";
                    HTML += "<path d='M 8 0 L 0 0 0 8' fill='none' stroke='gray' stroke-width='1'/>";
                    HTML += "</pattern>";
                    HTML += "<pattern id='grid' width='200' height='200' patternUnits='userSpaceOnUse' >";
                    HTML += "<rect width='200' height='200' fill='url(#rectangleverticle)'/>";
                    //HTML += "<path d='M 185 0 L 0 0 0 185' fill='none' stroke='Black' stroke-width='2'/>";
                    HTML += "</pattern>"
                    HTML += "</defs>";
                    HTML += "<circle cx='100' cy='100' r='3' style='stroke: none; fill:red;'/>";
                    HTML += "<rect width='200' height='200' fill='url(#grid)' /></svg></div>";
                    document.getElementById("divCircle").innerHTML = HTML;
                }
                else if (Pattern == "diagonal cross") {
                    var HTML = "<div style='width:200px;height:200px;border:solid 1px #000000'><svg height='200px' width='200px' id='svgrectangle' ><defs>";
                    HTML += "<pattern width='8' id='rectangleverticle' height='8' patternUnits='userSpaceOnUse'>";
                    HTML += "<path d='M 8 0 L 0 0 0 8' fill='none' stroke='gray' stroke-width='1'/>";
                    HTML += "</pattern>";
                    HTML += "<pattern id='grid' width='200' height='200' patternUnits='userSpaceOnUse' patternTransform='rotate(30)'>";
                    HTML += "<rect width='200' height='200' fill='url(#rectangleverticle)'/>";
                    //HTML += "<path d='M 185 0 L 0 0 0 185' fill='none' stroke='Black' stroke-width='2'/>";
                    HTML += "</pattern>"
                    HTML += "</defs>";
                    HTML += "<circle cx='100' cy='100' r='3' style='stroke: none; fill:red;'/>";
                    HTML += "<rect width='200' height='200' fill='url(#grid)' /></svg></div>";
                    document.getElementById("divCircle").innerHTML = HTML;
                }
                lblTargetOffset.innerHTML = "X and Y Lengths";
                lblYDiameter.style.visibility = "visible";
                lblCorner1.innerHTML = "Corner 1";
                lblCorner2.innerHTML = "Corner 2";
                lblCorner3.innerHTML = "Corner 3";
                lblCorner4.innerHTML = "Corner 4";

                lblXDiameter.style.visibility = "visible";
                lblYDiameter.style.visibility = "visible";

                lblXCorner1.style.visibility = "visible";
                lblYCorner1.style.visibility = "visible";

                lblXCorner2.style.visibility = "visible";
                lblYCorner2.style.visibility = "visible";

                lblXCorner3.style.visibility = "visible";
                lblYCorner3.style.visibility = "visible";

                lblXCorner4.style.visibility = "visible";
                lblYCorner4.style.visibility = "visible";
            }
            else if (shape == "polygon") {

                if (Pattern == "vertical line") {

                    var HTML = "<svg width='220' height='250' style='border:solid 1px #000000'><defs>";
                    HTML += "<pattern id='Pattern2' width='8' height='8' patternUnits='userSpaceOnUse'>";
                    HTML += "<path d='M 0 0 L 0 0 0 8' fill='none' stroke='gray' stroke-width='1'/>";
                    HTML += "</pattern>";
                    HTML += "<pattern id='Pattern3' width='200' height='210' patternUnits='userSpaceOnUse'>";
                    HTML += "<rect width='200' height='210' fill='url(#Pattern2)' />";
                    HTML += "</pattern>"
                    HTML += "</defs>";
                    HTML += "<circle cx='110' cy='110' r='3' style='stroke: none; fill:red;'/>";
                    HTML += "<polygon points='100,3 220,210 15,250' fill='url(#Pattern3)' style='stroke:black;stroke-width:1' /></svg>";
                    document.getElementById("divCircle").innerHTML = HTML;

                }
                else if (Pattern == "horizontal") {
                    var HTML = "<svg width='220' height='250' style='border:solid 1px #000000'><defs>";
                    HTML += "<pattern id='Pattern2' width='8' height='8' patternUnits='userSpaceOnUse'>";
                    HTML += "<path d='M 8 0 L 0 0 0 0' fill='none' stroke='gray' stroke-width='1'/>";
                    HTML += "</pattern>";
                    HTML += "<pattern id='Pattern3' width='200' height='210' patternUnits='userSpaceOnUse'>";
                    HTML += "<rect width='200' height='210' fill='url(#Pattern2)' />";
                    HTML += "</pattern>"
                    HTML += "</defs>";
                    HTML += "<circle cx='110' cy='110' r='3' style='stroke: none; fill:red;'/>";
                    HTML += "<polygon points='100,3 220,210 15,250' fill='url(#Pattern3)' style='stroke:black;stroke-width:1' /></svg>";
                    document.getElementById("divCircle").innerHTML = HTML;
                }
                else if (Pattern == "upward diagonal") {

                    var HTML = "<svg width='220' height='250' style='border:solid 1px #000000'><defs>";
                    HTML += "<pattern id='Pattern2' width='8' height='8' patternUnits='userSpaceOnUse'>";
                    HTML += "<path d='M 8 0 L 0 0 0 0' fill='none' stroke='gray' stroke-width='1'/>";
                    HTML += "</pattern>";
                    HTML += "<pattern id='Pattern3' width='200' height='210' patternUnits='userSpaceOnUse' patternTransform='rotate(30)'>";
                    HTML += "<rect width='200' height='210' fill='url(#Pattern2)' />";
                    HTML += "</pattern>"
                    HTML += "</defs>";
                    HTML += "<circle cx='110' cy='110' r='3' style='stroke: none; fill:red;'/>";
                    HTML += "<polygon points='100,3 220,210 15,250' fill='url(#Pattern3)' style='stroke:black;stroke-width:1' /></svg>";
                    document.getElementById("divCircle").innerHTML = HTML;


                }
                else if (Pattern == "downward diagonal") {
                    var HTML = "<svg width='220' height='250' style='border:solid 1px #000000'><defs>";
                    HTML += "<pattern id='Pattern2' width='8' height='8' patternUnits='userSpaceOnUse'>";
                    HTML += "<path d='M 0 0 L 0 0 0 8' fill='none' stroke='gray' stroke-width='1'/>";
                    HTML += "</pattern>";
                    HTML += "<pattern id='Pattern3' width='200' height='210' patternUnits='userSpaceOnUse'  patternTransform='rotate(30)'>";
                    HTML += "<rect width='200' height='210' fill='url(#Pattern2)' />";
                    HTML += "</pattern>"
                    HTML += "</defs>";
                    HTML += "<circle cx='110' cy='110' r='3' style='stroke: none; fill:red;'/>";
                    HTML += "<polygon points='100,3 220,210 15,250' fill='url(#Pattern3)' style='stroke:black;stroke-width:1' /></svg>";
                    document.getElementById("divCircle").innerHTML = HTML;
                }
                else if (Pattern == "cross") {
                    var HTML = "<svg width='220' height='250' style='border:solid 1px #000000'><defs>";
                    HTML += "<pattern id='Pattern2' width='8' height='8' patternUnits='userSpaceOnUse'>";
                    HTML += "<path d='M 8 0 L 0 0 0 8' fill='none' stroke='gray' stroke-width='1'/>";
                    HTML += "</pattern>";
                    HTML += "<pattern id='Pattern3' width='200' height='210' patternUnits='userSpaceOnUse' >";
                    HTML += "<rect width='200' height='210' fill='url(#Pattern2)' />";
                    HTML += "</pattern>"
                    HTML += "</defs>";
                    HTML += "<circle cx='110' cy='110' r='3' style='stroke: none; fill:red;'/>";
                    HTML += "<polygon points='100,3 220,210 15,250' fill='url(#Pattern3)' style='stroke:black;stroke-width:1' /></svg>";
                    document.getElementById("divCircle").innerHTML = HTML;
                }
                else if (Pattern == "diagonal cross") {
                    var HTML = "<svg width='220' height='250' style='border:solid 1px #000000'><defs>";
                    HTML += "<pattern id='Pattern2' width='8' height='8' patternUnits='userSpaceOnUse'>";
                    HTML += "<path d='M 8 0 L 0 0 0 8' fill='none' stroke='gray' stroke-width='1'/>";
                    HTML += "</pattern>";
                    HTML += "<pattern id='Pattern3' width='200' height='210' patternUnits='userSpaceOnUse'  patternTransform='rotate(30)'>";
                    HTML += "<rect width='200' height='210' fill='url(#Pattern2)' />";
                    HTML += "</pattern>"
                    HTML += "</defs>";
                    HTML += "<circle cx='110' cy='110' r='3' style='stroke: none; fill:red;'/>";
                    HTML += "<polygon points='100,3 220,210 15,250' fill='url(#Pattern3)' style='stroke:black;stroke-width:1' /></svg>";
                    document.getElementById("divCircle").innerHTML = HTML;
                }
                lblTargetOffset.innerHTML = "Vertex 1";
                lblCorner1.innerHTML = "Vertex 2";
                lblCorner2.innerHTML = "Vertex 3";
                lblCorner3.innerHTML = "Vertex 4";
                lblCorner4.innerHTML = "";
                lblYDiameter.style.visibility = "visible";
                
                lblXDiameter.style.visibility = "visible";
                lblYDiameter.style.visibility = "visible";

                lblXCorner1.style.visibility = "visible";
                lblYCorner1.style.visibility = "visible";

                lblXCorner2.style.visibility = "visible";
                lblYCorner2.style.visibility = "visible";

                lblXCorner3.style.visibility = "visible";
                lblYCorner3.style.visibility = "visible";

                lblXCorner4.style.visibility = "hidden";
                lblYCorner4.style.visibility = "hidden";
            }
            else if (shape == "ellipse") {

                if (Pattern == "vertical line") {

                    var HTML = "<svg height='120' width='180' id='svgellipse' ><defs>";
                    HTML += "<pattern width='8' id='ellipseverticle' height='8' patternUnits='userSpaceOnUse'>";
                    HTML += "<path d='M 0 0 L 0 0 0 8' fill='none' stroke='gray' stroke-width='1'/>";
                    HTML += "</pattern>";
                    HTML += "<pattern id='gridellipse' width='200' height='120' patternUnits='userSpaceOnUse' >";
                    HTML += "<rect width='200' height='120' fill='url(#ellipseverticle)'/>";
                    HTML += "</pattern>"
                    HTML += "</defs>";
                    HTML += "<circle cx='80' cy='80' r='3' style='stroke: none; fill:red;'/>";
                    HTML += "<ellipse cx='80' cy='80' rx='70' ry='40' fill='url(#gridellipse)' stroke='black' stroke-width='1'/></svg>";
                    document.getElementById("divCircle").innerHTML = HTML;

                }
                else if (Pattern == "horizontal") {
                    var HTML = "<svg height='120' width='180' id='svgellipse' ><defs>";
                    HTML += "<pattern width='8' id='ellipseverticle' height='8' patternUnits='userSpaceOnUse'>";
                    HTML += "<path d='M 8 0 L 0 0 0 0' fill='none' stroke='gray' stroke-width='1'/>";
                    HTML += "</pattern>";
                    HTML += "<pattern id='gridellipse' width='200' height='120' patternUnits='userSpaceOnUse' >";
                    HTML += "<rect width='200' height='120' fill='url(#ellipseverticle)'/>";
                    HTML += "</pattern>"
                    HTML += "</defs>";
                    HTML += "<circle cx='80' cy='80' r='3' style='stroke: none; fill:red;'/>";
                    HTML += "<ellipse cx='80' cy='80' rx='70' ry='40' fill='url(#gridellipse)' stroke='black' stroke-width='1'/></svg>";
                    document.getElementById("divCircle").innerHTML = HTML;
                }
                else if (Pattern == "upward diagonal") {

                    var HTML = "<svg height='120' width='180' id='svgellipse' ><defs>";
                    HTML += "<pattern width='8' id='ellipseverticle' height='8' patternUnits='userSpaceOnUse'>";
                    HTML += "<path d='M 8 0 L 0 0 0 0' fill='none' stroke='gray' stroke-width='1'/>";
                    HTML += "</pattern>";
                    HTML += "<pattern id='gridellipse' width='200' height='120' patternUnits='userSpaceOnUse' patternTransform='rotate(30)'>";
                    HTML += "<rect width='200' height='120' fill='url(#ellipseverticle)'/>";
                    HTML += "</pattern>"
                    HTML += "</defs>";
                    HTML += "<circle cx='80' cy='80' r='3' style='stroke: none; fill:red;'/>";
                    HTML += "<ellipse cx='80' cy='80' rx='70' ry='40' fill='url(#gridellipse)' stroke='black' stroke-width='1'/></svg>";
                    document.getElementById("divCircle").innerHTML = HTML;
                   

                }
                else if (Pattern == "downward diagonal") {
                    var HTML = "<svg height='120' width='180' id='svgellipse' ><defs>";
                    HTML += "<pattern width='8' id='ellipseverticle' height='8' patternUnits='userSpaceOnUse'>";
                    HTML += "<path d='M 0 0 L 0 0 0 8' fill='none' stroke='gray' stroke-width='1'/>";
                    HTML += "</pattern>";
                    HTML += "<pattern id='gridellipse' width='200' height='120' patternUnits='userSpaceOnUse' patternTransform='rotate(30)'>";
                    HTML += "<rect width='200' height='120' fill='url(#ellipseverticle)'/>";
                    HTML += "</pattern>"
                    HTML += "</defs>";
                    HTML += "<circle cx='80' cy='80' r='3' style='stroke: none; fill:red;'/>";
                    HTML += "<ellipse cx='80' cy='80' rx='70' ry='40' fill='url(#gridellipse)' stroke='black' stroke-width='1'/></svg>";
                    document.getElementById("divCircle").innerHTML = HTML;
                }
                else if (Pattern == "cross") {
                    var HTML = "<svg height='120' width='180' id='svgellipse' ><defs>";
                    HTML += "<pattern width='8' id='ellipseverticle' height='8' patternUnits='userSpaceOnUse'>";
                    HTML += "<path d='M 8 0 L 0 0 0 8' fill='none' stroke='gray' stroke-width='1'/>";
                    HTML += "</pattern>";
                    HTML += "<pattern id='gridellipse' width='200' height='120' patternUnits='userSpaceOnUse'>";
                    HTML += "<rect width='200' height='120' fill='url(#ellipseverticle)'/>";
                    HTML += "</pattern>"
                    HTML += "</defs>";
                    HTML += "<circle cx='80' cy='80' r='3' style='stroke: none; fill:red;'/>";
                    HTML += "<ellipse cx='80' cy='80' rx='70' ry='40' fill='url(#gridellipse)' stroke='black' stroke-width='1'/></svg>";
                    document.getElementById("divCircle").innerHTML = HTML;
                }
                else if (Pattern == "diagonal cross") {
                    var HTML = "<svg height='120' width='180' id='svgellipse' ><defs>";
                    HTML += "<pattern width='8' id='ellipseverticle' height='8' patternUnits='userSpaceOnUse'>";
                    HTML += "<path d='M 8 0 L 0 0 0 8' fill='none' stroke='gray' stroke-width='1'/>";
                    HTML += "</pattern>";
                    HTML += "<pattern id='gridellipse' width='200' height='120' patternUnits='userSpaceOnUse'  patternTransform='rotate(30)'>";
                    HTML += "<rect width='200' height='120' fill='url(#ellipseverticle)'/>";
                    HTML += "</pattern>"
                    HTML += "</defs>";
                    HTML += "<circle cx='80' cy='80' r='3' style='stroke: none; fill:red;'/>";
                    HTML += "<ellipse cx='80' cy='80' rx='70' ry='40' fill='url(#gridellipse)' stroke='black' stroke-width='1'/></svg>";
                    document.getElementById("divCircle").innerHTML = HTML;
                }

                
                lblTargetOffset.innerHTML = "X and Y Lengths";
                lblCorner1.innerHTML = "Corner 1";
                lblCorner2.innerHTML = "Corner 2";
                lblCorner3.innerHTML = "Corner 3";
                lblCorner4.innerHTML = "Corner 4";
                lblYDiameter.style.visibility = "hidden";

                lblXDiameter.style.visibility = "visible";
                lblYDiameter.style.visibility = "visible";

                lblXCorner1.style.visibility = "visible";
                lblYCorner1.style.visibility = "visible";

                lblXCorner2.style.visibility = "visible";
                lblYCorner2.style.visibility = "visible";

                lblXCorner3.style.visibility = "visible";
                lblYCorner3.style.visibility = "visible";

                lblXCorner4.style.visibility = "visible";
                lblYCorner4.style.visibility = "visible";
            }
            else {
                
            }
            var radwindow = $find('<%=RadWindow1.ClientID %>');
            radwindow.show();
            return false;
        }
    </script>

    <%-- // Jd New CSS Loading Animation--%>
    <asp:UpdateProgress ID="loader" runat="server" DisplayAfter="0">
        <ProgressTemplate>

            <div style="position: fixed; text-align: center; height: 100%; width: 100%; top: 0; right: 0; left: 0; padding-top: 15%; z-index: 9999999;">

                <div class="loader2">Loading...</div>

            </div>

        </ProgressTemplate>
    </asp:UpdateProgress>


    <fieldset>
        <asp:UpdatePanel runat="server" ID="updPnl1" UpdateMode="Always">


            

            <ContentTemplate>

                <telerik:RadWindow Behaviors="Close" ID="GraphWindow" runat="server"></telerik:RadWindow>

                <asp:Table ID="MainPage" runat="server" HorizontalAlign="Center" Width="100%">

                    <asp:TableRow>
                        <asp:TableCell ColumnSpan="7" HorizontalAlign="Center">
                            <h2>Manage Targets</h2>
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell>&nbsp;</asp:TableCell>
                    </asp:TableRow>

                </asp:Table>

                <telerik:RadWindow Behaviors="Close" ID="RadWindow1" runat="server" Width="750px" Height="500px">
                    <ContentTemplate>
                        <table>
                            <tr>
                                <td colspan="2">
                                    <b>Target Shape : </b>
                                    <asp:Label ID="lblTargetShapeName" runat="server" Font-Bold="true" ></asp:Label>
                                </td>
                            </tr>
                            <tr><td style="height:5px"></td></tr>
                            <tr>
                                <td>
                                    <table>
                            <tr>
                                <td align="center">
                                    <table border="1">
                                        <tr>
                                            <td colspan="3">
                                                <b>Target Boundary points</b>
                                            </td>
                                        </tr>
                                        
                                        <tr>
                                            <td><b>Point</b></td>
                                            <td><b>X-OFFSET</b></td>
                                            <td><b>Y-OFFSET</b></td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label ID="lblPointName1" runat="server" Font-Bold="true" Text="Target Offset"></asp:Label></td>
                                            <td>
                                                <asp:TextBox ID="txtTargetXOffset" runat="server" Width="60px"></asp:TextBox></td>
                                            <td>
                                                <asp:TextBox ID="txtTargetYOffset" runat="server" Width="60px"></asp:TextBox></td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label ID="lblTargetOffset" runat="server" Font-Bold="true" Text="Diameter of Circle"></asp:Label></td>
                                            <td>
                                                <asp:TextBox ID="txtXDiameter" runat="server" Width="60px" onChange="return CaliculateValues();"></asp:TextBox></td>
                                            <td>
                                                <asp:TextBox ID="txtYDiameter" runat="server" Width="60px" onChange="return CaliculateValuesRectangle();"></asp:TextBox></td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label ID="lblCorner1" runat="server" Font-Bold="true" Text="Corner 1"></asp:Label></td>
                                            <td>
                                                <asp:TextBox ID="txtXCorner1" runat="server" Width="60px"></asp:TextBox></td>
                                            <td>
                                                <asp:TextBox ID="txtYCorner1" runat="server" Width="60px"></asp:TextBox></td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label ID="lblCorner2" runat="server" Font-Bold="true" Text="Corner 2"></asp:Label></td>
                                            <td>
                                                <asp:TextBox ID="txtXCorner2" runat="server" Width="60px"></asp:TextBox></td>
                                            <td>
                                                <asp:TextBox ID="txtYCorner2" runat="server" Width="60px"></asp:TextBox></td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label ID="lblCorner3" runat="server" Font-Bold="true" Text="Corner 3"></asp:Label></td>
                                            <td>
                                                <asp:TextBox ID="txtXCorner3" runat="server" Width="60px"></asp:TextBox></td>
                                            <td>
                                                <asp:TextBox ID="txtYCorner3" runat="server" Width="60px"></asp:TextBox></td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label ID="lblCorner4" runat="server" Font-Bold="true" Text="Corner 4"></asp:Label></td>
                                            <td>
                                                <asp:TextBox ID="txtXCorner4" runat="server" Width="60px"></asp:TextBox></td>
                                            <td>
                                                <asp:TextBox ID="txtYCorner4" runat="server" Width="60px"></asp:TextBox></td>
                                        </tr>

                                    </table>
                                </td>
                            </tr>
                                        <tr><td style="height:5px"></td></tr>
                            <tr>
                                <td>
                                    <table>
                                        <tr>
                                            <td>
                                                <b>Reference Options</b>
                                                <br />

                                                <table>
                                                    <tr>
                                                        <td>
                                                            <telerik:RadButton ID="RadButton16" runat="server" ToggleType="Radio" Checked="true" ButtonType="ToggleButton"
                                                                Text="To Target" GroupName="StandardButton" AutoPostBack="false">
                                                            </telerik:RadButton>

                                                            <telerik:RadButton ID="RadButton17" runat="server" ToggleType="Radio"
                                                                Text="To Wellhead" GroupName="StandardButton" ButtonType="ToggleButton" AutoPostBack="false">
                                                            </telerik:RadButton>
                                                        </td>
                                                    </tr>
                                                </table>

                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                                        <tr><td style="height:5px"></td></tr>
                            <tr>
                                <td>Target ID :
                                    <asp:Label ID="lblTargetSelectedID" runat="server"></asp:Label>
                                </td>
                            </tr>
                            
                        </table>
                                </td>
                                <td valign="top">
                                    <table>
                                        <tr>
                                            <td>
                                                <div id="divCircle"></div>
                                                <%--<svg height="250" width="250" id="svgcircle" viewPort="0 0 120 120" style="display:none">
                                                    
                                                    <defs>
                                                        <pattern id="Triangle" 
                                                                 width="6" height="6"
                                                                 patternUnits="userSpaceOnUse" >
                                                            <path d="M 10 0 L 0 0 0 0" fill="none" stroke="gray" stroke-width="1"/>
	                                                    </pattern>
                                                    </defs>

                                                    <circle cx="60" cy="60" r="58" stroke="black" stroke-width="2"
                                                            fill="url(#Triangle)" />
                                                    
                                                </svg>
                                                <svg height="250" width="250" id="svgcircleverticle" viewPort="0 0 120 120" style="display:none">
                                                    
                                                    <defs>
                                                        <pattern id="verticlecircle" 
                                                                 width="6" height="6"
                                                                 patternUnits="userSpaceOnUse" >
                                                            <path d="M 0 0 L 0 0 0 10" fill="none" stroke="gray" stroke-width="1"/>
	                                                    </pattern>
                                                    </defs>

                                                    <circle cx="60" cy="60" r="58" stroke="black" stroke-width="2"
                                                            fill="url(#verticlecircle)" />
                                                    
                                                </svg>
                                                <svg width="300" height="300" id="svgrect" style="display:none">
                                                   <defs>
                                                          <pattern id="smallGrid" width="8" height="8" patternUnits="userSpaceOnUse">
                                                            <path d="M 8 0 L 0 0 0 0" fill="none" stroke="gray" stroke-width="1"/>
                                                          </pattern>
                                                          <pattern id="grid" width="184" height="184" patternUnits="userSpaceOnUse">
                                                            <rect width="185" height="185" fill="url(#smallGrid)"/>
                                                            <path d="M 185 0 L 0 0 0 185" fill="none" stroke="Black" stroke-width="2"/>
                                                          </pattern>
                                                           
                                                        <rect width="185" height="185" fill="url(#grid)" />
                                                </svg>
                                                <svg width="300" height="150" id="svgsquare" style="display:none">
                                                   <defs>
                                                          <pattern id="smallGridsquare" width="8" height="8" patternUnits="userSpaceOnUse">
                                                            <path d="M 8 0 L 0 0 0 0" fill="none" stroke="gray" stroke-width="1"/>
                                                          </pattern>
                                                          <pattern id="gridsquare" width="184px" height="125px" patternUnits="userSpaceOnUse">
                                                            <rect width="185px" height="115px" fill="url(#smallGridsquare)"/>
                                                            <path d="M 185 0 L 0 0 0 115" fill="none" stroke="Black" stroke-width="2"/>
                                                          </pattern>
                                                           
                                                        </defs>
                                                         
                                                        <rect width="185px" height="115px" fill="url(#gridsquare)" />
                                                </svg>
                                                <svg height="140" width="500" id="svgellipse" style="display:none">
                                                        <ellipse cx="200" cy="80" rx="100" ry="50"
                                                        style="fill:yellow;stroke:black;stroke-width:3" />
                                                    </svg>
                                                <svg height="210" width="500" id="svgpolygon" style="display:none">
                                                            <polygon points="200,10 250,190 160,210" style="fill:lime;stroke:black;stroke-width:3" />
                                                        </svg>--%> 

                                                
                                                
                                            </td>
                                        </tr>
                                        <tr><td>
                                            <%--<canvas id="myCanvas" width="578" height="200"></canvas>
                                                                <script type="text/javascript">
                                                                  var canvas = document.getElementById('myCanvas');
                                                                  var context = canvas.getContext('2d');
                                                                  var centerX = canvas.width / 2;
                                                                  var centerY = canvas.height / 2;
                                                                  var radius = 70;

                                                                  context.beginPath();
                                                                  context.arc(centerX, centerY, radius, 0, 2 * Math.PI, false);
                                                                  context.fillStyle = 'green';
                                                                  context.fill();
                                                                  context.lineWidth = 1;
                                                                  for (var x = 0.5; x < 300; x += 10) {
                                                                      context.moveTo(x, 0);
                                                                      context.lineTo(x, 255);
                                                                  }
                                                                  for (var y = 0.5; y < 255; y += 10) {
                                                                      context.moveTo(0, y);
                                                                      context.lineTo(500, y);
                                                                  }
                                                                  context.strokeStyle = "#eee";

                                                                  context.stroke();
                                                                </script>--%>
                                           <%-- <svg width="400" height="110">
                                                   <rect width="300" height="100" style="fill:rgb(0,0,255);stroke-width:3;stroke:rgb(0,0,0)" />
                                                </svg>--%>
                                            <%--<map><area shape="circle" coords="50,25,4" href="#" alt="foo" /></map>--%>
                                            <%--<img src ="../../images/clock.png" width="145" height="126" alt="Planets"
                                                         usemap="#planetmap">

                                                        <map name="planetmap">
                                                           <area shape="rect" coords="0,0,82,126" href="../../images/info_small.png"  alt="Sun">
                                                           <area shape="circle" coords="90,58,3"  alt="Mercury">
                                                           <area shape="circle" coords="124,58,8" alt="Venus">
                                                        </map> --%>
                                            
                                            </td></tr>
                                    </table>
                                </td>
                            </tr>
                            <tr><td style="height:5px"></td></tr>
                            <tr>
                                <td colspan="2" align="right">
                                    <asp:Button ID="btnAssign" runat="server" Text="Assign To Target" OnClientClick="return TargetShapeDetails();" />
                                </td>
                            </tr>
                        </table>
                        
                    </ContentTemplate>
                </telerik:RadWindow>
                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                    <tr>
                        <td>
                            <table>
                                <tr>
                                    <td align="center">
                                        <asp:Label ID="lblMessage" runat="server" Font-Bold="true"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <table>
                                            <%--<tr>
                            <td>
                                Target ID : 
                            </td>
                            <td>
                                <asp:Label ID="lblTID" runat="server"></asp:Label>
                            </td>
                        </tr>--%>
                                            <tr>
                                                <td>Job/Curve Group ID: 
                                                </td>
                                                <td>
                                                    <%--<telerik:RadComboBox ID="radcomboGrpName" runat="server" AppendDataBoundItems="true">
                                    <Items>
                                        <telerik:RadComboBoxItem Text="Select" Value="0" />
                                    </Items>
                                </telerik:RadComboBox>--%>
                                                    <telerik:RadDropDownList runat="server" ID="ddlCurveGroup" Width="300px" AppendDataBoundItems="true" DropDownHeight="200px"
                                                        AutoPostBack="true"
                                                        OnSelectedIndexChanged="ddlCurveGroup_SelectedIndexChanged">
                                                        <Items>
                                                            <telerik:DropDownListItem Value="0" Text="-Select-" />
                                                        </Items>
                                                    </telerik:RadDropDownList>
                                                </td>
                                                <td style="width:10px"></td>
                                                <td>
                                                    <telerik:RadButton  ID="btnShowCurve" runat="server" Text="Show Graph" OnClientClicked="OpenGraphWindow" AutoPostBack="false" Enabled="false"></telerik:RadButton>
                                                </td>
                                                <%--<td>
                                Target Name : 
                            </td>
                            <td>
                                <telerik:RadTextBox ID="radTargetName" runat="server" Width="200px"></telerik:RadTextBox>
                            </td>--%>
                                            </tr>
                                            <tr>
                                                
                                            </tr>
                                        </table>
                                    </td>
                                    <%--<td>
                    <table>
                        <tr>
                            <td>
                                Curve Group Name : 
                            </td>
                            <td>
                                <telerik:RadComboBox ID="radcomboGrpName" runat="server" AppendDataBoundItems="true">
                                    <Items>
                                        <telerik:RadComboBoxItem Text="Select" Value="0" />
                                    </Items>
                                </telerik:RadComboBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Curve ID : 
                            </td>
                            <td>
                                <telerik:RadComboBox ID="RadComboBox1" runat="server">
                                    <Items>
                                        <telerik:RadComboBoxItem Text="Select" Value="0" />
                                    </Items>
                                </telerik:RadComboBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Curve Number : 
                            </td>
                            <td>
                                <telerik:RadComboBox ID="radcomboCurveNumber" runat="server" AppendDataBoundItems="true">
                                    <Items>
                                        <telerik:RadComboBoxItem Text="Select" Value="0" />
                                    </Items>
                                </telerik:RadComboBox>
                            </td>
                        </tr>
                    </table>
                </td>--%>
                                </tr>
                                <%--<tr>
                <td colspan="2">
                    <table border="1">
                        <tr>
                            <td colspan="3">
                                <b>Target Boundary points</b>
                            </td>
                        </tr>
                        <tr>
                            <td><b>Point</b></td>
                            <td><b>X-OFFSET</b></td>
                            <td><b>Y-OFFSET</b></td>
                        </tr>
                        <tr>
                            <td><asp:Label ID="lblPointName1" runat="server" Font-Bold="true" Text="Target Offset"></asp:Label></td>
                            <td><asp:TextBox ID="txtTargetXOffset" runat="server" Width="60px"></asp:TextBox></td>
                            <td><asp:TextBox ID="txtTargetYOffset" runat="server" Width="60px"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td><asp:Label ID="lblTargetOffset" runat="server"  Font-Bold="true"  Text="Diameter of Circle"></asp:Label></td>
                            <td><asp:TextBox ID="txtXDiameter" runat="server" Width="60px" onChange="return CaliculateValues();"></asp:TextBox></td>
                            <td><asp:TextBox ID="txtYDiameter" runat="server" Width="60px" onChange="return CaliculateValuesRectangle();"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td><asp:Label ID="lblCorner1" runat="server" Font-Bold="true" Text="Corner 1"></asp:Label></td>
                            <td><asp:TextBox ID="txtXCorner1" runat="server" Width="60px"></asp:TextBox></td>
                            <td><asp:TextBox ID="txtYCorner1" runat="server" Width="60px"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td><asp:Label ID="lblCorner2" runat="server" Font-Bold="true" Text="Corner 2"></asp:Label></td>
                            <td><asp:TextBox ID="txtXCorner2" runat="server" Width="60px"></asp:TextBox></td>
                            <td><asp:TextBox ID="txtYCorner2" runat="server" Width="60px"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td><asp:Label ID="lblCorner3" runat="server" Font-Bold="true" Text="Corner 3"></asp:Label></td>
                            <td><asp:TextBox ID="txtXCorner3" runat="server" Width="60px"></asp:TextBox></td>
                            <td><asp:TextBox ID="txtYCorner3" runat="server" Width="60px"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td><asp:Label ID="lblCorner4" runat="server" Font-Bold="true" Text="Corner 4"></asp:Label></td>
                            <td><asp:TextBox ID="txtXCorner4" runat="server" Width="60px"></asp:TextBox></td>
                            <td><asp:TextBox ID="txtYCorner4" runat="server" Width="60px"></asp:TextBox></td>
                        </tr>
                        
                    </table>
                </td>
            </tr>--%>
                                <%--<tr>
                <td colspan="2">
                    <b>Reference Options</b>
                    <br />
                    
                        <table>
                            <tr>
                                <td>
                                    <telerik:RadButton ID="RadButton16" runat="server" ToggleType="Radio" Checked="true" ButtonType="ToggleButton"
                                        Text="To Target" GroupName="StandardButton" AutoPostBack="false">
                                    </telerik:RadButton>
                                    
                                    <telerik:RadButton ID="RadButton17" runat="server" ToggleType="Radio" 
                                        Text="To Wellhead" GroupName="StandardButton" ButtonType="ToggleButton" AutoPostBack="false">
                                    </telerik:RadButton>
                                </td>
                            </tr>
                        </table>
                    
                </td>
            </tr>--%>
                                <tr>
                                    <td style="height: 30px"></td>
                                </tr>
                                <tr>
                                    
                                    <td align="right">
                                        <table>
                                            <tr>
                                                <td>
                                                    <Telerik:RadButton ID="btnAddRow" runat="server" Text="Add Row" Enabled="false" OnClick="btnAddRow_Click" />
                                               </td>
                                                <td>
                                                    <asp:Button ID="btntopSave" runat="server" Enabled="false" OnClick="btnSave_Click" OnClientClick="return validation();" Text="Save Target"></asp:Button>

                                                   
                                                </td>
                                                <td width="4%"></td>
                                                <td>
                                                    <asp:Button ID="btntopClear" runat="server" Text="Clear" OnClick="btnCancel_Click"></asp:Button>
                                                </td>
                                               

                                            </tr>
                                        </table>
                                    </td>
                                    
                                </tr>
                                <tr>
                                    <td colspan="2">
                                        <table >
                                            <tr>
                                               
                                                <td>
                                                    <telerik:RadGrid ID="RadGrid1" OnItemDataBound="RadGrid1_ItemDataBound" GridLines="None" runat="server"
                                                        
                                                        AllowPaging="false"
                                                        AutoGenerateColumns="False" ShowHeader="true" Enabled="false">
                                                        <ClientSettings>
                                                            <Selecting AllowRowSelect="True" />
                                                            <Scrolling AllowScroll="false" />
                                                            <%--<ClientEvents OnRowClick="OnRowSelecting"/>--%>
                                                        </ClientSettings>
                                                        <MasterTableView CommandItemDisplay="none"
                                                            HorizontalAlign="NotSet" EditMode="InPlace" AutoGenerateColumns="False" ShowHeadersWhenNoRecords="true"
                                                            InsertItemDisplay="Top"
                                                            InsertItemPageIndexAction="ShowItemOnFirstPage">

                                                            <%--<SortExpressions>
                    <telerik:GridSortExpression FieldName="ProductID" SortOrder="Descending" />
                </SortExpressions>--%>
                                                            <Columns>
                                                                <%--<telerik:GridClientSelectColumn UniqueName="ClientSelectColumn">
                                                                </telerik:GridClientSelectColumn>--%>
                                                                <telerik:GridBoundColumn DataField="TargetID" Visible="false" HeaderText="TargetID" SortExpression="TargetID"
                                                                    UniqueName="TargetID">
                                                                    <HeaderStyle Width="50px" />
                                                                    <ItemStyle Width="50px" />
                                                                    <FooterStyle Width="50px" />
                                                                </telerik:GridBoundColumn>
                                                                <telerik:GridTemplateColumn UniqueName="TemplateColumn">
                                                                    <ItemTemplate>
                                                                        <asp:CheckBox ID="checkColumn" runat="server" AutoPostBack="true" OnCheckedChanged="CheckBox1_CheckedChanged" />
                                                                        <asp:Label ID="lblDisplayID" Text='<%# Eval("ID") %>' runat="server" Visible="false" Width="30px"></asp:Label>
                                                                    </ItemTemplate>
                                                                </telerik:GridTemplateColumn>
                                                                <telerik:GridTemplateColumn HeaderText="ID" DataField="TargetID" UniqueName="TargetId1"
                                                                    HeaderStyle-Width="30px" ItemStyle-Width="30px">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblTargetID" Text='<%# Eval("ID") %>' runat="server" Width="30px"></asp:Label>
                                                                    </ItemTemplate>
                                                                </telerik:GridTemplateColumn>
                                                                 <telerik:GridTemplateColumn HeaderText="Target Name" DataField="TargetName" UniqueName="TargetName" HeaderStyle-Width="90px">
                                                                    <ItemTemplate>
                                                                        <telerik:RadTextBox ID="radtxtTargetName" Text='<%# Eval("TargetName") %>' runat="server" Width="50px"></telerik:RadTextBox>
                                                                    </ItemTemplate>

                                                                </telerik:GridTemplateColumn>
                                                                <telerik:GridTemplateColumn HeaderText="Target Shape" DataField="TargetShapeID" UniqueName="TargetShapeID" HeaderStyle-Width="80px">
                                                                    <ItemTemplate>
                                                                        <telerik:RadDropDownList ID="ddlTargetShape" SelectedValue='<%# Bind("TargetShapeID") %>' runat="server" Width="80px" AppendDataBoundItems="true">
                                                                            <Items>
                                                                                <telerik:DropDownListItem Value="0" Text="--Select--" />
                                                                                <telerik:DropDownListItem Value="1000" Text="Circle" />
                                                                                <telerik:DropDownListItem Value="1002" Text="Rectangle" />
                                                                                <telerik:DropDownListItem Value="1001" Text="Square" />
                                                                                <telerik:DropDownListItem Value="1003" Text="Polygon" />
                                                                                <telerik:DropDownListItem Value="1004" Text="Ellipse" />
                                                                                <telerik:DropDownListItem Value="1005" Text="Point" />
                                                                            </Items>
                                                                        </telerik:RadDropDownList>
                                                                    </ItemTemplate>

                                                                </telerik:GridTemplateColumn>
                                                                <telerik:GridTemplateColumn HeaderText="TVD" DataField="TVD" UniqueName="TVD" HeaderStyle-Width="90px">
                                                                    <ItemTemplate>
                                                                        <telerik:RadTextBox ID="radtxtTVD" Text='<%# Eval("TVD") %>' runat="server" Width="50px"></telerik:RadTextBox>
                                                                    </ItemTemplate>

                                                                </telerik:GridTemplateColumn>
                                                                <telerik:GridTemplateColumn HeaderText="N-S Coordinate" DataField="NSCoordinate" UniqueName="NSCoordinate" HeaderStyle-Width="90px">
                                                                    <ItemTemplate>
                                                                        <telerik:RadTextBox ID="radtxtNSC" runat="server" Text='<%# Eval("NSCoordinate") %>' Width="50px"></telerik:RadTextBox>
                                                                    </ItemTemplate>

                                                                </telerik:GridTemplateColumn>
                                                                <telerik:GridTemplateColumn HeaderText="E-W Coordinate" DataField="EWCoordinate" UniqueName="EWCoordinate" HeaderStyle-Width="90px">
                                                                    <ItemTemplate>
                                                                        <telerik:RadTextBox ID="radtxtESC" runat="server" Text='<%# Eval("EWCoordinate") %>' Width="50px"></telerik:RadTextBox>
                                                                    </ItemTemplate>

                                                                </telerik:GridTemplateColumn>
                                                                <telerik:GridTemplateColumn HeaderText="Polar Distance" DataField="PolarDistance" UniqueName="PolarDistance" HeaderStyle-Width="90px">
                                                                    <ItemTemplate>
                                                                        <telerik:RadTextBox ID="radtxtPolardistance" runat="server" Text='<%# Eval("PolarDistance") %>' Width="50px" ReadOnly="true"></telerik:RadTextBox>
                                                                    </ItemTemplate>

                                                                </telerik:GridTemplateColumn>
                                                                <telerik:GridTemplateColumn HeaderText="Polar Direction" DataField="PolarDirection" UniqueName="PolarDirection" HeaderStyle-Width="90px">
                                                                    <ItemTemplate>
                                                                        <telerik:RadTextBox ID="radtxtPolardirection" runat="server" Text='<%# Eval("PolarDirection") %>' Width="50px" ReadOnly="true"></telerik:RadTextBox>
                                                                    </ItemTemplate>

                                                                </telerik:GridTemplateColumn>
                                                                <telerik:GridTemplateColumn HeaderText="INC From Last Target"  DataField="INCFromLastTarget" UniqueName="INCFromLastTarget" HeaderStyle-Width="90px">
                                                                    <ItemTemplate>
                                                                        <telerik:RadTextBox ID="INCLAST" runat="server" ReadOnly="true" ForeColor="Red" Text='<%# Eval("INCFromLastTarget")  %>' Width="50px"></telerik:RadTextBox>
                                                                    </ItemTemplate>

                                                                </telerik:GridTemplateColumn>
                                                                <telerik:GridTemplateColumn HeaderText="AZM From Last Target"  DataField="AZMFromLastTarget" UniqueName="AZMFromLastTarget" HeaderStyle-Width="90px">
                                                                    <ItemTemplate>
                                                                        <telerik:RadTextBox ID="AZMFromLastTarget" runat="server" Width="50px" ForeColor="Red" Text='<%# Eval("AZMFromLastTarget")   %>' ReadOnly="true"></telerik:RadTextBox>
                                                                    </ItemTemplate>
                                                                </telerik:GridTemplateColumn>
                                                                <telerik:GridTemplateColumn HeaderText="Inclination at Target" DataField="InclinationAtTarget" UniqueName="InclinationAtTarget" HeaderStyle-Width="90px">
                                                                    <ItemTemplate>
                                                                        <telerik:RadTextBox ID="INCAtTarget" runat="server" Width="50px" Text='<%# Eval("InclinationAtTarget") %>'></telerik:RadTextBox>
                                                                    </ItemTemplate>
                                                                </telerik:GridTemplateColumn>
                                                                <telerik:GridTemplateColumn HeaderText="Azimuth at Target" DataField="AzimuthAtTarget" UniqueName="AzimuthAtTarget" HeaderStyle-Width="90px">
                                                                    <ItemTemplate>
                                                                        <telerik:RadTextBox ID="AZMTAtTarget" runat="server" Width="50px" Text='<%# Eval("AzimuthAtTarget") %>'></telerik:RadTextBox>
                                                                    </ItemTemplate>
                                                                </telerik:GridTemplateColumn>
                                                                <telerik:GridTemplateColumn HeaderText="Number Vertices" DataField="NumberVertices" UniqueName="NumberVertices" HeaderStyle-Width="90px">
                                                                    <ItemTemplate>
<%--                                                                        <telerik:RadTextBox ID="NumberofVerticles" runat="server" Width="50px" Text='<%# Eval("NumberVertices") %>'></telerik:RadTextBox>--%>
                                                                        <telerik:RadDropDownList ID="NumberofVerticles" runat="server" SelectedValue='<%# Bind("NumberVertices") %>' Width="65px" AppendDataBoundItems="true">
                                                                            <Items>
                                                                                <telerik:DropDownListItem Value="1" Text="-Select-" />
                                                                                <telerik:DropDownListItem Value="3" Text="3" />
                                                                                <telerik:DropDownListItem Value="4" Text="4" />
                                                                                <telerik:DropDownListItem Value="5" Text="5" />
                                                                                <telerik:DropDownListItem Value="6" Text="6" />
                                                                                <telerik:DropDownListItem Value="7" Text="7" />
                                                                                <telerik:DropDownListItem Value="8" Text="8" />
                                                                            </Items>
                                                                        </telerik:RadDropDownList>
                                                                    </ItemTemplate>
                                                                </telerik:GridTemplateColumn>
                                                                <telerik:GridTemplateColumn HeaderText="Rotation" DataField="Rotation" UniqueName="Rotation" HeaderStyle-Width="90px">
                                                                    <ItemTemplate>
                                                                        <telerik:RadTextBox ID="Rotation" runat="server" Width="50px" Text='<%# Eval("Rotation") %>'></telerik:RadTextBox>
                                                                    </ItemTemplate>
                                                                </telerik:GridTemplateColumn>
                                                                <telerik:GridTemplateColumn HeaderText="Target Thickness" DataField="TargetThickness" UniqueName="TargetThickness" HeaderStyle-Width="90px">
                                                                    <ItemTemplate>
                                                                        <telerik:RadTextBox ID="Thickness" runat="server" Width="50px" Text='<%# Eval("TargetThickness") %>'></telerik:RadTextBox>
                                                                    </ItemTemplate>
                                                                </telerik:GridTemplateColumn>
                                                                <telerik:GridTemplateColumn HeaderText="Drawing Pattern" DataField="DrawingPattern" UniqueName="DrawingPattern" HeaderStyle-Width="130px">
                                                                    <ItemTemplate>
                                                                        <%--<telerik:RadTextBox ID="Pat" runat="server" Width="50px"></telerik:RadTextBox>--%>
                                                                        <telerik:RadDropDownList ID="ddlPattern" runat="server" SelectedValue='<%# Bind("DrawingPattern") %>' Width="120px" AppendDataBoundItems="true">
                                                                            <Items>
                                                                                <telerik:DropDownListItem Value="1008" Text="--Select--" />
                                                                                <telerik:DropDownListItem Value="1000" Text="Transparent" />
                                                                                <telerik:DropDownListItem Value="1001" Text="Horizontal" />
                                                                                <telerik:DropDownListItem Value="1002" Text="Vertical Line" />
                                                                                <telerik:DropDownListItem Value="1003" Text="Upward Diagonal" />
                                                                                <telerik:DropDownListItem Value="1004" Text="Downward Diagonal" />
                                                                                <telerik:DropDownListItem Value="1005" Text="Cross" />
                                                                                <telerik:DropDownListItem Value="1006" Text="Diagonal Cross" />
                                                                                <telerik:DropDownListItem Value="1007" Text="Solid" />
                                                                            </Items>
                                                                        </telerik:RadDropDownList>
                                                                        <asp:Button ID="btnShowPattern" runat="server" Text="Show"></asp:Button>
                                                                    </ItemTemplate>
                                                                </telerik:GridTemplateColumn>
                                                                <telerik:GridTemplateColumn HeaderText="Target Comments" DataField="TargetComment" UniqueName="TargetComment" HeaderStyle-Width="90px">
                                                                    <ItemTemplate>
                                                                        <telerik:RadTextBox ID="Comments" runat="server" Width="130px" Text='<%# Eval("TargetComment") %>'></telerik:RadTextBox>
                                                                    </ItemTemplate>
                                                                </telerik:GridTemplateColumn>
                                                            </Columns>
                                                        </MasterTableView>
                                                        <ClientSettings AllowKeyboardNavigation="true"></ClientSettings>
                                                    </telerik:RadGrid>

                                                </td>
                                                
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    
                                    <td align="right">
                                        <table>
                                            <tr>
                                               
                                                <td>
                                                    <Telerik:RadButton ID="btnBottomAddRow" runat="server" Text="Add Row" Enabled="false" OnClick="btnAddRow_Click" />
                                               </td>
                                                <td>
                                                    <asp:Button ID="btnSave" runat="server" Enabled="false" OnClick="btnSave_Click" OnClientClick="return validation();" Text="Save Target"></asp:Button>

                                                    <script type="text/javascript">
                                                        function validation() {
                                                            var valueFind = $find("<%=ddlCurveGroup.ClientID%>").get_selectedItem().get_value();
                                                            if (valueFind == "0") {
                                                                alert("Please select Curve/Work Group ID");
                                                                return false;
                                                            }

                                                        }
                                                    </script>
                                                </td>
                                                <td width="4%"></td>
                                                <td>
                                                    <asp:Button ID="btnClear" runat="server" Text="Clear" OnClick="btnCancel_Click"></asp:Button>
                                                </td>
                                               

                                            </tr>
                                        </table>
                                    </td>
                                    
                                </tr>
                                <tr>
                                    <td style="height: 50px">
                                        <asp:HiddenField ID="hidShapeType" runat="server" />
                                    </td>
                                </tr>
                            </table>
                            <%--<div align="left" class="contactmain">
                                <img src="../../images/ManageCurveGroups/2a_CreateTargets.png" alt="CreateTargets" /><br />
                            </div>--%>
                        </td>
                    </tr>
                    <%--<tr>
                       <asp:Button ID="btnSave" runat="server" OnClick="btnSave_Click" Text="Save" > </asp:Button>
                        &nbsp;
                        &nbsp;
                       <asp:Button ID="btnClear" runat="server" OnClick="btnClear_Click" Text="Clear" > </asp:Button>
                        &nbsp;
                        &nbsp;
                       <asp:Button ID="btnCancel" runat="server" OnClick="btnCancel_Click" Text="Cancel" > </asp:Button>
</tr>--%>
                </table>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="btnSave" EventName="Click" />
                <%--<asp:AsyncPostBackTrigger ControlID="btnClear" EventName="Click" />        
        <asp:AsyncPostBackTrigger ControlID="btnCancel" EventName="Click" /> --%>
            </Triggers>
        </asp:UpdatePanel>
    </fieldset>
    <div style="text-align: center;" class="DivFooter">
        <hr style="width: 777px" />
        Copyright&copy;2016 - Limitless Healthcare IT - All Rights Reserved<br />
    </div>
</asp:Content>

