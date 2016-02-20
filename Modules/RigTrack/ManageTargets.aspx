
                     <%@ Page Title="Manage Targets" Language="C#" MasterPageFile="~/Masters/RigTrack.master" AutoEventWireup="true" CodeFile="ManageTargets.aspx.cs" Inherits="Modules_RigTrack_ManageTargets" EnableEventValidation="false" MaintainScrollPositionOnPostback="true" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server"></asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="customCss" runat="server"></asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <script type="text/javascript">
        function OnChangeCalculations(obj1, obj2, objinclast, objazm, obj3, objPolardist, objpolardirection) {
            var tvdval = document.getElementById(obj2).value;
            var tvdval1 = document.getElementById(obj3).value;
            var NSval = document.getElementById(obj2).value;
            var EWval = document.getElementById(obj3).value;
            var TVDval3 = document.getElementById(obj1).value;

            $.ajax({
                type: "POST",
                url: 'ManageTargets.aspx/CalculatePolarDirection',
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

        function SetWindowSize() {
            var viewportWidth = $(window).width();
            var viewportHeight = $(window).height();
            graphWindow.setSize(math.ceil(viewportWidth - 90), math.ceil(viewportHeight - 90));
        }

        $(window).resize(function () {
            if (graphWindow.isVisible()) {

                SetWindowSize();
            }
        });

        function setGraphWindowSize(CurveGroupID) {
            SetWindowSize();
            graphWindow.setUrl("CreateGraph.aspx?CurveGroupID=" + CurveGroupID);
            graphWindow.center();
            graphWindow.set_modal(true);
        }

        function CalculateValues() {
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

            var shape = document.getElementById("<%= lblTargetShapeName.ClientID %>").innerText;
            if (shape == "Circle" || shape == "Square") {
                if (lblXDiameter.value != "") {
                    var coordinate = lblXDiameter.value / 2;
                    lblXCorner1.value = "-" + coordinate;
                    lblXCorner2.value = coordinate;
                    lblXCorner3.value = coordinate;
                    lblXCorner4.value = "-" + coordinate;

                    lblYCorner1.value = coordinate;
                    lblYCorner2.value = coordinate;
                    lblYCorner3.value = "-" + coordinate;
                    lblYCorner4.value = "-" + coordinate;
                }
            }
            if (shape == "Rectangle" || shape == "Ellipse") {
                if (lblXDiameter.value != "" && lblYDiameter.value != "") {
                    var coordinate = lblXDiameter.value / 2;

                    lblXCorner1.value = "-" + coordinate;
                    lblXCorner2.value = coordinate;
                    lblXCorner3.value = coordinate;
                    lblXCorner4.value = "-" + coordinate;
                }
            }
        }

        function CalculateValuesRectangle() {
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

            var shape = document.getElementById("<%= lblTargetShapeName.ClientID %>").innerText;
            if (shape == "Rectangle" || shape == "Ellipse") {
                if (lblYDiameter.value != "") {
                    var coordinate = lblYDiameter.value / 2;

                    lblYCorner1.value = coordinate;
                    lblYCorner2.value = coordinate;
                    lblYCorner3.value = "-" + coordinate;
                    lblYCorner4.value = "-" + coordinate;
                }
            }
        }



        // Jd Used For User show target shape 
        function DrawShapesEditButton(editBtnClick, vertices) {

            ClearGraph();



            var TargetShape;
            var numberOfVertices;

            TargetShape = editBtnClick;
            numberOfVertices = vertices;


            if (TargetShape == 1000) {

                var radiusMax = document.getElementById('txtXDiameter').value;
                var radiusTextBox = document.getElementById('txtXDiameter').value;
                radiusTextBox = radiusTextBox / 2;
                var canvas1 = document.getElementById('myCanvas');
                var centerX1 = canvas1.width / 2;
                var centerY1 = canvas1.height / 2;
                var context1 = canvas1.getContext('2d');
                var xoffset1 = centerX1;
                var yoffset1 = centerY1;

                if (radiusMax <= 220) {

                    context1.beginPath();
                    context1.arc(xoffset1, yoffset1, radiusTextBox, 0, Math.PI * 2, true);
                    context1.stroke();
                }

                else {


                    context1.beginPath();
                    context1.arc(xoffset1, yoffset1, 110, 0, Math.PI * 2, true);
                    context1.stroke();
                }


            }
            else if (TargetShape == 1001) {

                var squareRotation = document.getElementById('txtRotation').value;
                var squareLength = document.getElementById('txtXDiameter').value;
                var squareMaxSize = document.getElementById('txtXDiameter').value;
                squareLength = squareLength / 2;
                var squareHeight = document.getElementById('txtXDiameter').value;
                squareHeight = squareHeight / 2;


                var canvas3 = document.getElementById('myCanvas');
                var centerX3 = canvas3.width / 2;
                var centerY3 = canvas3.height / 2;
                var context3 = canvas3.getContext('2d');



                var finalXPosition2 = 0;
                finalXPosition2 = finalXPosition2 - squareLength / 2;

                var finalYPosition2 = 0;
                finalYPosition2 = finalYPosition2 + squareHeight / 2;

                // for max size of square 
                var finalXPosition3 = 0;
                finalXPosition3 = finalXPosition3 - 150 / 2;

                var finalYPosition3 = 0;
                finalYPosition3 = finalYPosition3 + 150 / 2;

                var xoffset3 = centerX3 + finalXPosition2;
                var yoffset3 = centerY3 - finalYPosition2;


                // for square max size 
                var xoffset4 = centerX3 + finalXPosition3;
                var yoffset4 = centerY3 - finalYPosition3;

                var rotationalValue = (squareRotation * (Math.PI / 180.00));



                var drawX = 0;
                var drawY = 0;
                var drawWidth = squareLength;
                var drawHeight = squareHeight;
                var drawcX = drawX + 0.5 * drawWidth;
                var drawcY = drawY + 0.5 * drawHeight;


                // for max square size 
                var drawcX2 = drawX + 0.5 * 150;
                var drawcY2 = drawY + 0.5 * 150;

                if (squareMaxSize <= 300) {

                    context3.save();
                    context3.beginPath();
                    context3.translate(xoffset3 + (drawWidth / 2), yoffset3 + (drawHeight / 2));
                    context3.rotate(rotationalValue);
                    context3.translate(-drawcX, -drawcY);
                    context3.scale(1, 1);

                    context3.strokeStyle = "black";
                    context3.rect(drawX, drawY, drawWidth, drawHeight);
                    context3.stroke();
                    context3.restore();

                }

                else {

                    context3.save();
                    context3.beginPath();
                    context3.translate(xoffset4 + (150 / 2), yoffset4 + (150 / 2));
                    context3.rotate(rotationalValue);
                    context3.translate(-drawcX2, -drawcY2);
                    context3.scale(1, 1);

                    context3.strokeStyle = "black";
                    context3.rect(drawX, drawY, 150, 150);
                    context3.stroke();
                    context3.restore();

                }


            }
            else if (TargetShape == 1002) {

                var rectangleRotation = document.getElementById('txtRotation').value;

                var rectangleLength = document.getElementById('txtXDiameter').value;
                rectangleLength = rectangleLength / 2;

                var rectangleHeight = document.getElementById('txtYDiameter').value;
                rectangleHeight = rectangleHeight / 2;



                var canvas3 = document.getElementById('myCanvas');
                var centerX3 = canvas3.width / 2;
                var centerY3 = canvas3.height / 2;
                var context3 = canvas3.getContext('2d');



                var finalXPosition2 = 0;
                finalXPosition2 = finalXPosition2 - rectangleLength / 2;

                var finalYPosition2 = 0;
                finalYPosition2 = finalYPosition2 + rectangleHeight / 2;

                // for max size of square 
                var finalXPosition3 = 0;
                finalXPosition3 = finalXPosition3 - 310 / 2;

                var finalYPosition3 = 0;
                finalYPosition3 = finalYPosition3 + 310 / 2;

                var xoffset3 = centerX3 + finalXPosition2;
                var yoffset3 = centerY3 - finalYPosition2;


                // for square max size 
                var xoffset4 = centerX3 + finalXPosition3;
                var yoffset4 = centerY3 - finalYPosition3;

                var rotationalValue = (rectangleRotation * (Math.PI / 180.00));



                var drawX = 0;
                var drawY = 0;
                var drawWidth = rectangleLength;
                var drawHeight = rectangleHeight;


                var drawcX = drawX + 0.5 * drawWidth;
                var drawcY = drawY + 0.5 * drawHeight;

                var drawcX2 = drawX + 0.5 * 155;
                var drawcY2 = drawY + 0.5 * 155;






                if (rectangleLength <= 155 && rectangleHeight > 155) {


                    context3.save();
                    context3.beginPath();
                    context3.translate(xoffset3 + (drawWidth / 2), yoffset4 + (310 / 2));
                    context3.rotate(rotationalValue);
                    context3.translate(-drawcX, -drawcY2);
                    context3.scale(1, 1);

                    context3.strokeStyle = "black";
                    context3.rect(drawX, drawY, drawWidth, 155);
                    context3.stroke();
                    context3.restore();

                }

                else if (rectangleLength > 155 && rectangleHeight <= 155) {

                    context3.save();
                    context3.beginPath();
                    context3.translate(xoffset4 + (310 / 2), yoffset3 + (drawHeight / 2));
                    context3.rotate(rotationalValue);
                    context3.translate(-drawcX2, -drawcY);
                    context3.scale(1, 1);

                    context3.strokeStyle = "black";
                    context3.rect(drawX, drawY, 155, drawHeight);
                    context3.stroke();
                    context3.restore();

                }

                else if (rectangleLength <= 155 && rectangleHeight <= 155) {

                    context3.save();
                    context3.beginPath();
                    context3.translate(xoffset3 + (drawWidth / 2), yoffset3 + (drawHeight / 2));
                    context3.rotate(rotationalValue);
                    context3.translate(-drawcX, -drawcY);
                    context3.scale(1, 1);

                    context3.strokeStyle = "black";
                    context3.rect(drawX, drawY, drawWidth, drawHeight);
                    context3.stroke();
                    context3.restore();

                }

                else {

                    context3.save();
                    context3.beginPath();
                    context3.translate(xoffset4 + (310 / 2), yoffset4 + (310 / 2));
                    context3.rotate(rotationalValue);
                    context3.translate(-drawcX2, -drawcY2);
                    context3.scale(1, 1);

                    context3.strokeStyle = "black";
                    context3.rect(drawX, drawY, 155, 155);
                    context3.stroke();
                    context3.restore();


                }




            }

            else if (TargetShape == 1003) {

                var polyRotation = document.getElementById('txtRotation').value;

                if (numberOfVertices == 3) {

                    var vertex1X = document.getElementById('txtXCorner1').value;
                    vertex1X = vertex1X / 2;
                    vertex1X = vertex1X / 5;

                    var vertex1Y = document.getElementById('txtYCorner1').value;
                    vertex1Y = vertex1Y / 2;
                    vertex1Y = vertex1Y / 5;


                    var vertex2X = document.getElementById('txtXCorner2').value;
                    vertex2X = vertex2X / 2;
                    vertex2X = vertex2X / 5;

                    var vertex2Y = document.getElementById('txtYCorner2').value;
                    vertex2Y = vertex2Y / 2;
                    vertex2Y = vertex2Y / 5;

                    var vertex3X = document.getElementById('txtXCorner3').value;
                    vertex3X = vertex3X / 2;
                    vertex3X = vertex3X / 5;

                    var vertex3Y = document.getElementById('txtYCorner3').value;
                    vertex3Y = vertex3Y / 2;
                    vertex3Y = vertex3Y / 5;
                }

                if (numberOfVertices == 4) {

                    var vertex1X = document.getElementById('txtXCorner1').value;
                    vertex1X = vertex1X / 2;
                    vertex1X = vertex1X / 5;

                    var vertex1Y = document.getElementById('txtYCorner1').value;
                    vertex1Y = vertex1Y / 2;
                    vertex1Y = vertex1Y / 5;


                    var vertex2X = document.getElementById('txtXCorner2').value;
                    vertex2X = vertex2X / 2;
                    vertex2X = vertex2X / 5;

                    var vertex2Y = document.getElementById('txtYCorner2').value;
                    vertex2Y = vertex2Y / 2;
                    vertex2Y = vertex2Y / 5;

                    var vertex3X = document.getElementById('txtXCorner3').value;
                    vertex3X = vertex3X / 2;
                    vertex3X = vertex3X / 5;

                    var vertex3Y = document.getElementById('txtYCorner3').value;
                    vertex3Y = vertex3Y / 2;
                    vertex3Y = vertex3Y / 5;

                    var vertex4X = document.getElementById('txtXCorner4').value;
                    vertex4X = vertex4X / 2;
                    vertex4X = vertex4X / 5;

                    var vertex4Y = document.getElementById('txtYCorner4').value;
                    vertex4Y = vertex4Y / 2;
                    vertex4Y = vertex4Y / 5;
                }

                if (numberOfVertices == 5) {

                    var vertex1X = document.getElementById('txtXCorner1').value;
                    vertex1X = vertex1X / 2;
                    vertex1X = vertex1X / 5;

                    var vertex1Y = document.getElementById('txtYCorner1').value;
                    vertex1Y = vertex1Y / 2;
                    vertex1Y = vertex1Y / 5;


                    var vertex2X = document.getElementById('txtXCorner2').value;
                    vertex2X = vertex2X / 2;
                    vertex2X = vertex2X / 5;

                    var vertex2Y = document.getElementById('txtYCorner2').value;
                    vertex2Y = vertex2Y / 2;
                    vertex2Y = vertex2Y / 5;

                    var vertex3X = document.getElementById('txtXCorner3').value;
                    vertex3X = vertex3X / 2;
                    vertex3X = vertex3X / 5;

                    var vertex3Y = document.getElementById('txtYCorner3').value;
                    vertex3Y = vertex3Y / 2;
                    vertex3Y = vertex3Y / 5;

                    var vertex4X = document.getElementById('txtXCorner4').value;
                    vertex4X = vertex4X / 2;
                    vertex4X = vertex4X / 5;

                    var vertex4Y = document.getElementById('txtYCorner4').value;
                    vertex4Y = vertex4Y / 2;
                    vertex4Y = vertex4Y / 5;


                    var vertex5X = document.getElementById('txtXCorner5').value;
                    vertex5X = vertex5X / 2;
                    vertex5X = vertex5X / 5;

                    var vertex5Y = document.getElementById('txtYCorner5').value;
                    vertex5Y = vertex5Y / 2;
                    vertex5Y = vertex5Y / 5;
                }

                if (numberOfVertices == 6) {

                    var vertex1X = document.getElementById('txtXCorner1').value;
                    vertex1X = vertex1X / 2;
                    vertex1X = vertex1X / 5;

                    var vertex1Y = document.getElementById('txtYCorner1').value;
                    vertex1Y = vertex1Y / 2;
                    vertex1Y = vertex1Y / 5;


                    var vertex2X = document.getElementById('txtXCorner2').value;
                    vertex2X = vertex2X / 2;
                    vertex2X = vertex2X / 5;

                    var vertex2Y = document.getElementById('txtYCorner2').value;
                    vertex2Y = vertex2Y / 2;
                    vertex2Y = vertex2Y / 5;

                    var vertex3X = document.getElementById('txtXCorner3').value;
                    vertex3X = vertex3X / 2;
                    vertex3X = vertex3X / 5;

                    var vertex3Y = document.getElementById('txtYCorner3').value;
                    vertex3Y = vertex3Y / 2;
                    vertex3Y = vertex3Y / 5;

                    var vertex4X = document.getElementById('txtXCorner4').value;
                    vertex4X = vertex4X / 2;
                    vertex4X = vertex4X / 5;

                    var vertex4Y = document.getElementById('txtYCorner4').value;
                    vertex4Y = vertex4Y / 2;
                    vertex4Y = vertex4Y / 5;


                    var vertex5X = document.getElementById('txtXCorner5').value;
                    vertex5X = vertex5X / 2;
                    vertex5X = vertex5X / 5;

                    var vertex5Y = document.getElementById('txtYCorner5').value;
                    vertex5Y = vertex5Y / 2;
                    vertex5Y = vertex5Y / 5;

                    var vertex6X = document.getElementById('txtXCorner6').value;
                    vertex6X = vertex6X / 2;
                    vertex6X = vertex6X / 5;

                    var vertex6Y = document.getElementById('txtYCorner6').value;
                    vertex6Y = vertex6Y / 2;
                    vertex6Y = vertex6Y / 5;
                }

                if (numberOfVertices == 7) {

                    var vertex1X = document.getElementById('txtXCorner1').value;
                    vertex1X = vertex1X / 2;
                    vertex1X = vertex1X / 5;

                    var vertex1Y = document.getElementById('txtYCorner1').value;
                    vertex1Y = vertex1Y / 2;
                    vertex1Y = vertex1Y / 5;


                    var vertex2X = document.getElementById('txtXCorner2').value;
                    vertex2X = vertex2X / 2;
                    vertex2X = vertex2X / 5;

                    var vertex2Y = document.getElementById('txtYCorner2').value;
                    vertex2Y = vertex2Y / 2;
                    vertex2Y = vertex2Y / 5;

                    var vertex3X = document.getElementById('txtXCorner3').value;
                    vertex3X = vertex3X / 2;
                    vertex3X = vertex3X / 5;

                    var vertex3Y = document.getElementById('txtYCorner3').value;
                    vertex3Y = vertex3Y / 2;
                    vertex3Y = vertex3Y / 5;

                    var vertex4X = document.getElementById('txtXCorner4').value;
                    vertex4X = vertex4X / 2;
                    vertex4X = vertex4X / 5;

                    var vertex4Y = document.getElementById('txtYCorner4').value;
                    vertex4Y = vertex4Y / 2;
                    vertex4Y = vertex4Y / 5;


                    var vertex5X = document.getElementById('txtXCorner5').value;
                    vertex5X = vertex5X / 2;
                    vertex5X = vertex5X / 5;

                    var vertex5Y = document.getElementById('txtYCorner5').value;
                    vertex5Y = vertex5Y / 2;
                    vertex5Y = vertex5Y / 5;

                    var vertex6X = document.getElementById('txtXCorner6').value;
                    vertex6X = vertex6X / 2;
                    vertex6X = vertex6X / 5;

                    var vertex6Y = document.getElementById('txtYCorner6').value;
                    vertex6Y = vertex6Y / 2;
                    vertex6Y = vertex6Y / 5;

                    var vertex7X = document.getElementById('txtXCorner7').value;
                    vertex7X = vertex7X / 2;
                    vertex7X = vertex7X / 5;

                    var vertex7Y = document.getElementById('txtYCorner7').value;
                    vertex7Y = vertex7Y / 2;
                    vertex7Y = vertex7Y / 5;
                }

                if (numberOfVertices == 8) {

                    var vertex1X = document.getElementById('txtXCorner1').value;
                    vertex1X = vertex1X / 2;
                    vertex1X = vertex1X / 5;

                    var vertex1Y = document.getElementById('txtYCorner1').value;
                    vertex1Y = vertex1Y / 2;
                    vertex1Y = vertex1Y / 5;


                    var vertex2X = document.getElementById('txtXCorner2').value;
                    vertex2X = vertex2X / 2;
                    vertex2X = vertex2X / 5;

                    var vertex2Y = document.getElementById('txtYCorner2').value;
                    vertex2Y = vertex2Y / 2;
                    vertex2Y = vertex2Y / 5;

                    var vertex3X = document.getElementById('txtXCorner3').value;
                    vertex3X = vertex3X / 2;
                    vertex3X = vertex3X / 5;

                    var vertex3Y = document.getElementById('txtYCorner3').value;
                    vertex3Y = vertex3Y / 2;
                    vertex3Y = vertex3Y / 5;

                    var vertex4X = document.getElementById('txtXCorner4').value;
                    vertex4X = vertex4X / 2;
                    vertex4X = vertex4X / 5;

                    var vertex4Y = document.getElementById('txtYCorner4').value;
                    vertex4Y = vertex4Y / 2;
                    vertex4Y = vertex4Y / 5;


                    var vertex5X = document.getElementById('txtXCorner5').value;
                    vertex5X = vertex5X / 2;
                    vertex5X = vertex5X / 5;

                    var vertex5Y = document.getElementById('txtYCorner5').value;
                    vertex5Y = vertex5Y / 2;
                    vertex5Y = vertex5Y / 5;

                    var vertex6X = document.getElementById('txtXCorner6').value;
                    vertex6X = vertex6X / 2;
                    vertex6X = vertex6X / 5;

                    var vertex6Y = document.getElementById('txtYCorner6').value;
                    vertex6Y = vertex6Y / 2;
                    vertex6Y = vertex6Y / 5;

                    var vertex7X = document.getElementById('txtXCorner7').value;
                    vertex7X = vertex7X / 2;
                    vertex7X = vertex7X / 5;

                    var vertex7Y = document.getElementById('txtYCorner7').value;
                    vertex7Y = vertex7Y / 2;
                    vertex7Y = vertex7Y / 5;

                    var vertex8X = document.getElementById('txtXCorner8').value;
                    vertex8X = vertex8X / 2;
                    vertex8X = vertex8X / 5;

                    var vertex8Y = document.getElementById('txtYCorner8').value;
                    vertex8Y = vertex8Y / 2;
                    vertex8Y = vertex8Y / 5;

                }


                // for max size of square 
                var finalXPosition3 = 0;
                finalXPosition3 = finalXPosition3 - 0 / 2;

                var finalYPosition3 = 0;
                finalYPosition3 = finalYPosition3 + 0 / 2;

                var canvas4 = document.getElementById('myCanvas');
                var centerX4 = canvas4.width / 2;
                var centerY4 = canvas4.height / 2;
                var context4 = canvas4.getContext('2d');
                var xoffset4 = centerX4 + finalXPosition3;
                var yoffset4 = centerY4 - finalYPosition3;



                var polyRotationalValue = (polyRotation * (Math.PI / 180.00));



                context4.save();
                context4.beginPath();
                context4.translate(xoffset4 + (0 / 2), yoffset4 + (0 / 2));
                context4.rotate(polyRotationalValue);
                //context4.translate(-drawcX, -drawcY);
                context4.scale(1, 1);


                context4.strokeStyle = "black";
                context4.lineWidth = 1;
                context4.beginPath();



                if (numberOfVertices == 3) {

                    context4.lineTo(vertex1X, -vertex1Y);
                    context4.lineTo(vertex2X, -vertex2Y);
                    context4.lineTo(vertex3X, -vertex3Y);

                }

                if (numberOfVertices == 4) {

                    context4.lineTo(vertex1X, -vertex1Y);
                    context4.lineTo(vertex2X, -vertex2Y);
                    context4.lineTo(vertex3X, -vertex3Y);
                    context4.lineTo(vertex4X, -vertex4Y);

                }

                if (numberOfVertices == 5) {

                    context4.lineTo(vertex1X, -vertex1Y);
                    context4.lineTo(vertex2X, -vertex2Y);
                    context4.lineTo(vertex3X, -vertex3Y);
                    context4.lineTo(vertex4X, -vertex4Y);
                    context4.lineTo(vertex5X, -vertex5Y);

                }


                if (numberOfVertices == 6) {

                    context4.lineTo(vertex1X, -vertex1Y);
                    context4.lineTo(vertex2X, -vertex2Y);
                    context4.lineTo(vertex3X, -vertex3Y);
                    context4.lineTo(vertex4X, -vertex4Y);
                    context4.lineTo(vertex5X, -vertex5Y);
                    context4.lineTo(vertex6X, -vertex6Y);

                }

                if (numberOfVertices == 7) {

                    context4.lineTo(vertex1X, -vertex1Y);
                    context4.lineTo(vertex2X, -vertex2Y);
                    context4.lineTo(vertex3X, -vertex3Y);
                    context4.lineTo(vertex4X, -vertex4Y);
                    context4.lineTo(vertex5X, -vertex5Y);
                    context4.lineTo(vertex6X, -vertex6Y);
                    context4.lineTo(vertex7X, -vertex7Y);

                }

                if (numberOfVertices == 8) {

                    context4.lineTo(vertex1X, -vertex1Y);
                    context4.lineTo(vertex2X, -vertex2Y);
                    context4.lineTo(vertex3X, -vertex3Y);
                    context4.lineTo(vertex4X, -vertex4Y);
                    context4.lineTo(vertex5X, -vertex5Y);
                    context4.lineTo(vertex6X, -vertex6Y);
                    context4.lineTo(vertex7X, -vertex7Y);
                    context4.lineTo(vertex8X, -vertex8Y);

                }



                context4.closePath();
                context4.stroke();
                context4.restore();

            }

            else if (TargetShape == 1004) {

                var ellipseRotation = document.getElementById('txtRotation').value;

                var ellipseLength = document.getElementById('txtXDiameter').value;
                ellipseLength = ellipseLength / 2;

                var ellipseHeight = document.getElementById('txtYDiameter').value;
                ellipseHeight = ellipseHeight / 2;



                var canvas6 = document.getElementById('myCanvas');
                var centerX6 = canvas6.width / 2;
                var centerY6 = canvas6.height / 2;
                var context6 = canvas6.getContext('2d');
                var xoffset6 = centerX6 + 0;
                var yoffset6 = centerY6 - 0;


                var ellipseRotationalValue = (ellipseRotation * (Math.PI / 180.00));


                if (ellipseLength <= 110 && ellipseHeight > 110) {

                    context6.beginPath();
                    context6.ellipse(xoffset6, yoffset6, ellipseLength, 110, ellipseRotationalValue, 0, 2 * Math.PI);

                    context6.strokeStyle = "black";
                    context6.lineWidth = 1;
                    context6.stroke();

                }

                else if (ellipseLength > 110 && ellipseHeight <= 110) {
                    context6.beginPath();
                    context6.ellipse(xoffset6, yoffset6, 110, ellipseHeight, ellipseRotationalValue, 0, 2 * Math.PI);

                    context6.strokeStyle = "black";
                    context6.lineWidth = 1;
                    context6.stroke();
                }

                else if (ellipseLength <= 110 && ellipseHeight <= 110) {
                    context6.beginPath();
                    context6.ellipse(xoffset6, yoffset6, ellipseLength, ellipseHeight, ellipseRotationalValue, 0, 2 * Math.PI);

                    context6.strokeStyle = "black";
                    context6.lineWidth = 1;
                    context6.stroke();
                }

                else {
                    context6.beginPath();
                    context6.ellipse(xoffset6, yoffset6, 110, 110, ellipseRotationalValue, 0, 2 * Math.PI);

                    context6.strokeStyle = "black";
                    context6.lineWidth = 1;
                    context6.stroke();

                }
            }

            else if (TargetShape == 1005) {

                var canvas7 = document.getElementById('myCanvas');
                var centerX7 = canvas7.width / 2;
                var centerY7 = canvas7.height / 2;
                var context7 = canvas7.getContext('2d');
                var xoffset7 = centerX7;
                var yoffset7 = centerY7;

                context7.beginPath();
                context7.arc(xoffset7, yoffset7, 10, 0, Math.PI * 2, true);
                context7.fillStyle = "black";
                context7.lineWidth = 1;
                context7.fill();


            }

        }






    </script>

    <asp:UpdateProgress ID="loader" runat="server" DisplayAfter="0">
        <ProgressTemplate>
            <div style="position: fixed; text-align: center; height: 100%; width: 100%; top: 0; right: 0; left: 0; padding-top: 15%; z-index: 9999999;">
                <div class="loader2">Loading...</div>
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>

    <fieldset>
        <asp:UpdatePanel runat="server" ID="updPnl1" UpdateMode="Conditional">
            <ContentTemplate>
                  <asp:HiddenField ID="hdnTargetID" runat="server" ClientIDMode="Static" />
                  <asp:HiddenField ID="hdnVertices" runat="server" ClientIDMode="Static" />
                <telerik:RadWindow ID="GraphWindow" runat="server"></telerik:RadWindow>
                <asp:Table ID="MainPage" runat="server" HorizontalAlign="Center" Width="100%">
                    <asp:TableRow>
                        <asp:TableCell ColumnSpan="7" HorizontalAlign="Center">
                            <h2>Manage Targets</h2>
                        </asp:TableCell>
                    </asp:TableRow>
                </asp:Table>
                <asp:Table ID="Table1" runat="server" HorizontalAlign="Center" Width="100%">
                    <asp:TableRow>
                        <asp:TableCell Width="20%">
                            <asp:Table ID="Table2" runat="server" HorizontalAlign="Left" BorderStyle="Double" BorderColor="#3A4F63">
                                <asp:TableRow>
                                    <asp:TableHeaderCell>
                                        Company:
                                    </asp:TableHeaderCell>
                                    <asp:TableHeaderCell>
                                        Job/Curve Group:
                                    </asp:TableHeaderCell>
                                    <%--   <asp:TableHeaderCell>
                                        Show Graph:
                                    </asp:TableHeaderCell>--%>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell>
                                        <telerik:RadDropDownList runat="server" ID="ddlCompany" Width="160px" DropDownHeight="200px" OnSelectedIndexChanged="ddlCompany_SelectedIndexChanged" AutoPostBack="true" AppendDataBoundItems="true">
                                            <Items>
                                                <telerik:DropDownListItem Value="0" Text="-Select-" />
                                            </Items>
                                        </telerik:RadDropDownList>
                                    </asp:TableCell>
                                    <asp:TableCell>
                                        <telerik:RadDropDownList runat="server" ID="ddlCurveGroup" Width="300px" DropDownHeight="200px" OnSelectedIndexChanged="ddlCurveGroup_SelectedIndexChanged" AutoPostBack="true" AppendDataBoundItems="true" CausesValidation="false">
                                            <Items>
                                                <telerik:DropDownListItem Value="0" Text="-Select-" />
                                            </Items>
                                        </telerik:RadDropDownList>
                                    </asp:TableCell>
                                    <asp:TableCell>
                                        <telerik:RadButton runat="server" ID="btnShowGraph" Visible="false" Text="Show Graph" Enabled="false" OnClientClicked="OpenGraphWindow" AutoPostBack="false"></telerik:RadButton>
                                    </asp:TableCell>
                                </asp:TableRow>
                            </asp:Table>
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell Width="30%">
                            <asp:Table ID="Table3" runat="server" HorizontalAlign="Left" Width="500px" BorderStyle="Double" BorderColor="#3A4F63">
                                <asp:TableRow>
                                    <asp:TableHeaderCell ColumnSpan="5">
                                        Target Shape: 
                                        <asp:Label ID="lblTargetShapeName" runat="server"></asp:Label>
                                    </asp:TableHeaderCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell><b>Point</b></asp:TableCell>
                                    <asp:TableCell HorizontalAlign="Center"><b>X-OFFSET</b></asp:TableCell>
                                    <asp:TableCell HorizontalAlign="Center"><b>Y-OFFSET</b></asp:TableCell>
                                    <asp:TableCell ColumnSpan="2" HorizontalAlign="Center">
                                        <b>Reference Options</b>
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell>
                                        <asp:Label ID="lblPointName1" runat="server" Font-Bold="true" Text="Target Offset" Width="100px"></asp:Label>
                                    </asp:TableCell>
                                    <asp:TableCell>
                                        <asp:TextBox ID="txtTargetXOffset" runat="server" Text="0.00" Enabled="false"></asp:TextBox>
                                        <asp:CompareValidator ID="CompareValidator1" ControlToValidate="txtTargetXOffset" ForeColor="Red"
                                            Type="Double" Display="Dynamic" Operator="DataTypeCheck" ErrorMessage="Not a valid number" runat="server"></asp:CompareValidator>
                                    </asp:TableCell>
                                    <asp:TableCell>
                                        <asp:TextBox ID="txtTargetYOffset" runat="server" Text="0.00" Enabled="false"></asp:TextBox>
                                        <asp:CompareValidator ID="CompareValidator2" ControlToValidate="txtTargetYOffset" ForeColor="Red"
                                            Type="Double" Display="Dynamic" Operator="DataTypeCheck" ErrorMessage="Not a valid number" runat="server"></asp:CompareValidator>
                                    </asp:TableCell>
                                    <asp:TableCell>
                                        <telerik:RadButton ID="btnToTarget" runat="server" ToggleType="Radio" Checked="true" ButtonType="ToggleButton"
                                            Text="To Target" GroupName="StandardButton" AutoPostBack="false" Enabled="false" Width="75px">
                                        </telerik:RadButton>
                                    </asp:TableCell>
                                    <asp:TableCell>
                                        <telerik:RadButton ID="btnToWellhead" runat="server" ToggleType="Radio" ButtonType="ToggleButton"
                                            Text="To Wellhead" GroupName="StandardButton" AutoPostBack="false" Enabled="false" Width="75px">
                                        </telerik:RadButton>
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell>
                                        <asp:Label ID="lblTargetOffset" runat="server" Font-Bold="true" Text="Diameter of Circle"></asp:Label>
                                    </asp:TableCell>
                                    <asp:TableCell>
                                        <asp:TextBox ID="txtXDiameter" ClientIDMode="Static" runat="server" Text="0.00" Enabled="false" onChange="return CalculateValues();"></asp:TextBox>
                                        <asp:CompareValidator ID="CompareValidator3" ControlToValidate="txtXDiameter" ForeColor="Red"
                                            Type="Double" Display="Dynamic" Operator="DataTypeCheck" ErrorMessage="Not a valid number" runat="server"></asp:CompareValidator>
                                    </asp:TableCell>
                                    <asp:TableCell>
                                        <asp:TextBox ID="txtYDiameter" ClientIDMode="Static" runat="server" Text="0.00" Enabled="false" onChange="return CalculateValuesRectangle();"></asp:TextBox>
                                        <asp:CompareValidator ID="CompareValidator4" ControlToValidate="txtYDiameter" ForeColor="Red"
                                            Type="Double" Display="Dynamic" Operator="DataTypeCheck" ErrorMessage="Not a valid number" runat="server"></asp:CompareValidator>
                                    </asp:TableCell>
                                    <asp:TableCell>
                                        <asp:Label ID="lblTargetShape" runat="server" Font-Bold="true" Text="Target Shape"></asp:Label>
                                    </asp:TableCell>
                                    <asp:TableCell>
                                        <telerik:RadDropDownList ID="ddlTargetShape" runat="server" OnSelectedIndexChanged="ddlTargetShape_SelectedIndexChanged" Enabled="false" AutoPostBack="true" Width="100px">
                                            <Items>
                                                <telerik:DropDownListItem Value="0" Text="--Select--" />
                                                <telerik:DropDownListItem Value="1000" Text="Circle" />
                                                <telerik:DropDownListItem Value="1001" Text="Square" />
                                                <telerik:DropDownListItem Value="1002" Text="Rectangle" />
                                                <telerik:DropDownListItem Value="1003" Text="Polygon" />
                                                <telerik:DropDownListItem Value="1004" Text="Ellipse" />
                                                <telerik:DropDownListItem Value="1005" Text="Point" />
                                            </Items>
                                        </telerik:RadDropDownList>
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell>
                                        <asp:Label ID="lblCorner1" runat="server" Font-Bold="true" Text="Corner 1"></asp:Label>
                                    </asp:TableCell>
                                    <asp:TableCell>
                                        <asp:TextBox ID="txtXCorner1" ClientIDMode="Static" runat="server" Text="0.00" Enabled="false"></asp:TextBox>
                                        <asp:CompareValidator ID="CompareValidator5" ControlToValidate="txtXCorner1" ForeColor="Red"
                                            Type="Double" Display="Dynamic" Operator="DataTypeCheck" ErrorMessage="Not a valid number" runat="server"></asp:CompareValidator>
                                    </asp:TableCell>
                                    <asp:TableCell>
                                        <asp:TextBox ID="txtYCorner1" ClientIDMode="Static" runat="server" Text="0.00" Enabled="false"></asp:TextBox>
                                        <asp:CompareValidator ID="CompareValidator6" ControlToValidate="txtYCorner1" ForeColor="Red"
                                            Type="Double" Display="Dynamic" Operator="DataTypeCheck" ErrorMessage="Not a valid number" runat="server"></asp:CompareValidator>
                                    </asp:TableCell>
                                    <asp:TableCell>
                                        <asp:Label ID="lblVertices" runat="server" Font-Bold="true" Text="Number of Vertices"></asp:Label>
                                    </asp:TableCell>
                                    <asp:TableCell>
                                        <telerik:RadDropDownList ID="ddlVertices" runat="server" AutoPostBack="true" Enabled="false" OnSelectedIndexChanged="ddlVertices_SelectedIndexChanged" Width="100px">
                                            <Items>
                                                <telerik:DropDownListItem Value="3" Text="3" />
                                                <telerik:DropDownListItem Value="4" Text="4" />
                                                <telerik:DropDownListItem Value="5" Text="5" />
                                                <telerik:DropDownListItem Value="6" Text="6" />
                                                <telerik:DropDownListItem Value="7" Text="7" />
                                                <telerik:DropDownListItem Value="8" Text="8" />
                                            </Items>
                                        </telerik:RadDropDownList>
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell>
                                        <asp:Label ID="lblCorner2" runat="server" Font-Bold="true" Text="Corner 2"></asp:Label>
                                    </asp:TableCell>
                                    <asp:TableCell>
                                        <asp:TextBox ID="txtXCorner2" ClientIDMode="Static" runat="server" Text="0.00" Enabled="false"></asp:TextBox>
                                        <asp:CompareValidator ID="CompareValidator7" ControlToValidate="txtXCorner2" ForeColor="Red"
                                            Type="Double" Display="Dynamic" Operator="DataTypeCheck" ErrorMessage="Not a valid number" runat="server"></asp:CompareValidator>
                                    </asp:TableCell>
                                    <asp:TableCell>
                                        <asp:TextBox ID="txtYCorner2" ClientIDMode="Static" runat="server" Text="0.00" Enabled="false"></asp:TextBox>
                                        <asp:CompareValidator ID="CompareValidator8" ControlToValidate="txtYCorner2" ForeColor="Red"
                                            Type="Double" Display="Dynamic" Operator="DataTypeCheck" ErrorMessage="Not a valid number" runat="server"></asp:CompareValidator>
                                    </asp:TableCell>
                                    <asp:TableCell>
                                        <asp:Label ID="lblRotation" runat="server" Font-Bold="true" Text="Rotation"></asp:Label>
                                    </asp:TableCell>
                                    <asp:TableCell>
                                        <asp:TextBox ID="txtRotation" ClientIDMode="Static" runat="server" Text="0.00" Enabled="false" Width="100px"></asp:TextBox>
                                        <asp:CompareValidator ID="CompareValidator9" ControlToValidate="txtRotation" ForeColor="Red"
                                            Type="Double" Display="Dynamic" Operator="DataTypeCheck" ErrorMessage="Not a valid number" runat="server"></asp:CompareValidator>
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell>
                                        <asp:Label ID="lblCorner3" runat="server" Font-Bold="true" Text="Corner 3"></asp:Label>
                                    </asp:TableCell>
                                    <asp:TableCell>
                                        <asp:TextBox ID="txtXCorner3" ClientIDMode="Static" runat="server" Text="0.00" Enabled="false"></asp:TextBox>
                                        <asp:CompareValidator ID="CompareValidator10" ControlToValidate="txtXCorner3" ForeColor="Red"
                                            Type="Double" Display="Dynamic" Operator="DataTypeCheck" ErrorMessage="Not a valid number" runat="server"></asp:CompareValidator>
                                    </asp:TableCell>
                                    <asp:TableCell>
                                        <asp:TextBox ID="txtYCorner3" ClientIDMode="Static" runat="server" Text="0.00" Enabled="false"></asp:TextBox>
                                        <asp:CompareValidator ID="CompareValidator11" ControlToValidate="txtYCorner3" ForeColor="Red"
                                            Type="Double" Display="Dynamic" Operator="DataTypeCheck" ErrorMessage="Not a valid number" runat="server"></asp:CompareValidator>
                                    </asp:TableCell>
                                    <asp:TableCell>
                                        Target Name:
                                    </asp:TableCell>
                                    <asp:TableCell>
                                        <asp:Label ID="lblTargetSelectedID" runat="server"></asp:Label>
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell>
                                        <asp:Label ID="lblCorner4" runat="server" Font-Bold="true" Text="Corner 4"></asp:Label>
                                    </asp:TableCell>
                                    <asp:TableCell>
                                        <asp:TextBox ID="txtXCorner4" ClientIDMode="Static" runat="server" Text="0.00" Enabled="false"></asp:TextBox>
                                        <asp:CompareValidator ID="CompareValidator12" ControlToValidate="txtXCorner4" ForeColor="Red"
                                            Type="Double" Display="Dynamic" Operator="DataTypeCheck" ErrorMessage="Not a valid number" runat="server"></asp:CompareValidator>
                                    </asp:TableCell>
                                    <asp:TableCell>
                                        <asp:TextBox ID="txtYCorner4" ClientIDMode="Static" runat="server" Text="0.00" Enabled="false"></asp:TextBox>
                                        <asp:CompareValidator ID="CompareValidator13" ControlToValidate="txtYCorner4" ForeColor="Red"
                                            Type="Double" Display="Dynamic" Operator="DataTypeCheck" ErrorMessage="Not a valid number" runat="server"></asp:CompareValidator>
                                    </asp:TableCell>
                                    <asp:TableCell ColumnSpan="2">
                                       <%-- <telerik:RadButton ID="btnDraw" runat="server" Text="Draw" AutoPostBack="false"  OnClientClicked="DrawShapes()" Enabled="false" Width="100%"></telerik:RadButton>--%>

                                        <telerik:RadButton ID="btnDraw" runat="server" Text="Draw" OnClick="btnDraw_Click" Width="200px"></telerik:RadButton>
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell>
                                        <asp:Label ID="lblCorner5" runat="server" Font-Bold="true" Text="Vertex 5" Visible="false"></asp:Label>
                                    </asp:TableCell>
                                    <asp:TableCell>
                                        <asp:TextBox ID="txtXCorner5" ClientIDMode="Static" runat="server" Text="0.00" Visible="false"></asp:TextBox>
                                        <asp:CompareValidator ID="CompareValidator14" ControlToValidate="txtXCorner5" ForeColor="Red"
                                            Type="Double" Display="Dynamic" Operator="DataTypeCheck" ErrorMessage="Not a valid number" runat="server"></asp:CompareValidator>
                                    </asp:TableCell>
                                    <asp:TableCell>
                                        <asp:TextBox ID="txtYCorner5" ClientIDMode="Static" runat="server" Text="0.00" Visible="false"></asp:TextBox>
                                        <asp:CompareValidator ID="CompareValidator15" ControlToValidate="txtYCorner5" ForeColor="Red"
                                            Type="Double" Display="Dynamic" Operator="DataTypeCheck" ErrorMessage="Not a valid number" runat="server"></asp:CompareValidator>
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell>
                                        <asp:Label ID="lblCorner6" runat="server" Font-Bold="true" Text="Vertex 6" Visible="false"></asp:Label>
                                    </asp:TableCell>
                                    <asp:TableCell>
                                        <asp:TextBox ID="txtXCorner6" ClientIDMode="Static" runat="server" Text="0.00" Visible="false"></asp:TextBox>
                                        <asp:CompareValidator ID="CompareValidator16" ControlToValidate="txtXCorner6" ForeColor="Red"
                                            Type="Double" Display="Dynamic" Operator="DataTypeCheck" ErrorMessage="Not a valid number" runat="server"></asp:CompareValidator>
                                    </asp:TableCell>
                                    <asp:TableCell>
                                        <asp:TextBox ID="txtYCorner6" ClientIDMode="Static" runat="server" Text="0.00" Visible="false"></asp:TextBox>
                                        <asp:CompareValidator ID="CompareValidator17" ControlToValidate="txtYCorner6" ForeColor="Red"
                                            Type="Double" Display="Dynamic" Operator="DataTypeCheck" ErrorMessage="Not a valid number" runat="server"></asp:CompareValidator>
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell>
                                        <asp:Label ID="lblCorner7" runat="server" Font-Bold="true" Text="Vertex 7" Visible="false"></asp:Label>
                                    </asp:TableCell>
                                    <asp:TableCell>
                                        <asp:TextBox ID="txtXCorner7" ClientIDMode="Static" runat="server" Text="0.00" Visible="false"></asp:TextBox>
                                        <asp:CompareValidator ID="CompareValidator18" ControlToValidate="txtXCorner7" ForeColor="Red"
                                            Type="Double" Display="Dynamic" Operator="DataTypeCheck" ErrorMessage="Not a valid number" runat="server"></asp:CompareValidator>
                                    </asp:TableCell>
                                    <asp:TableCell>
                                        <asp:TextBox ID="txtYCorner7" ClientIDMode="Static" runat="server" Text="0.00" Visible="false"></asp:TextBox>
                                        <asp:CompareValidator ID="CompareValidator19" ControlToValidate="txtYCorner7" ForeColor="Red"
                                            Type="Double" Display="Dynamic" Operator="DataTypeCheck" ErrorMessage="Not a valid number" runat="server"></asp:CompareValidator>
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell>
                                        <asp:Label ID="lblCorner8" runat="server" Font-Bold="true" Text="Vertex 8" Visible="false"></asp:Label>
                                    </asp:TableCell>
                                    <asp:TableCell>
                                        <asp:TextBox ID="txtXCorner8" ClientIDMode="Static" runat="server" Text="0.00" Visible="false"></asp:TextBox>
                                        <asp:CompareValidator ID="CompareValidator20" ControlToValidate="txtXCorner8" ForeColor="Red"
                                            Type="Double" Display="Dynamic" Operator="DataTypeCheck" ErrorMessage="Not a valid number" runat="server"></asp:CompareValidator>
                                    </asp:TableCell>
                                    <asp:TableCell>
                                        <asp:TextBox ID="txtYCorner8" ClientIDMode="Static" runat="server" Text="0.00" Visible="false"></asp:TextBox>
                                        <asp:CompareValidator ID="CompareValidator21" ControlToValidate="txtYCorner8" ForeColor="Red"
                                            Type="Double" Display="Dynamic" Operator="DataTypeCheck" ErrorMessage="Not a valid number" runat="server"></asp:CompareValidator>
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell ColumnSpan="2">
                                        <asp:RangeValidator ID="RangeValidator1" runat="server" Type="Double" MinimumValue="0" MaximumValue="360"
                                            ControlToValidate="txtRotation" ErrorMessage="Must be between 0-360" Display="Dynamic"></asp:RangeValidator>
                                    </asp:TableCell>
                                    <%--<asp:TableCell>
                                        <asp:Button ID="btnAssign" runat="server" Text="Assign to Target" />
                                    </asp:TableCell>--%>
                                </asp:TableRow>
                            </asp:Table>


                        </asp:TableCell>

                        <asp:TableCell Width="300px" HorizontalAlign="Left">
                                <canvas id="myCanvas" width="223px" height="223px" style="border:1px solid #010A15;" />

                        </asp:TableCell>
                    </asp:TableRow>
                </asp:Table>

                <%--<asp:Table ID="Table3" runat="server" HorizontalAlign="Right" Width="50%">
                    <asp:TableRow>
                        <asp:TableCell ColumnSpan="3">
                            <asp:Table ID="Table4" runat="server" HorizontalAlign="Right">
                                <asp:TableRow>
                                    <asp:TableCell>
                                        <telerik:RadButton ID="btnAddRow" runat="server" Text="Add Row" Enabled="false"></telerik:RadButton>
                                    </asp:TableCell>
                                    <asp:TableCell>
                                        <telerik:RadButton ID="btnSave" runat="server" Text="Save Target" Enabled="false"></telerik:RadButton>
                                    </asp:TableCell>
                                    <asp:TableCell>
                                        <telerik:RadButton ID="btnClear" runat="server" Text="Clear" Enabled="false"></telerik:RadButton>
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell>&nbsp;</asp:TableCell>
                                </asp:TableRow>
                            </asp:Table>
                        </asp:TableCell>
                    </asp:TableRow>
                </asp:Table>--%>
                <asp:Label ID="lblValidation" runat="server" Text="Target Name, TVD, N-S Coordinate and E-W Coordinate are required. TVD, N-S Coordinate and E-W Coordinate must be numeric." ForeColor="Red" Visible="false"></asp:Label>
                <asp:Label ID="lblValidationXYDiameter" runat="server" Text="Length Of Side Or Circle Diameter Can Not Be 0. Must Greater Than 0 To Draw Shapes" ForeColor="Red" Visible="false"></asp:Label>

                <telerik:RadGrid ID="RadGridTargets" AllowFilteringByColumn="false" AllowSorting="false" AutoGenerateColumns="false" runat="server" AllowMultiRowSelection="false" PageSize="30" OnInsertCommand="RadGridTargets_InsertCommand"
                    OnItemCreated="RadGridTargets_ItemCreated" OnNeedDataSource="RadGridTargets_NeedDataSource" OnUpdateCommand="RadGridTargets_UpdateCommand" OnItemDataBound="RadGridTargets_ItemDataBound" OnItemCommand="RadGridTargets_ItemCommand">
                    <ClientSettings EnableRowHoverStyle="true" AllowColumnHide="true">
                        <Selecting AllowRowSelect="true" />
                    </ClientSettings>
                    <MasterTableView EditMode="InPlace" AutoGenerateColumns="false" GridLines="Horizontal" NoMasterRecordsText="No data has been added." CommandItemDisplay="Top">
                        <CommandItemSettings ShowAddNewRecordButton="true" ShowRefreshButton="false" />
                        <Columns>
                            <%--<telerik:GridClientSelectColumn UniqueName="ClientSelectColumn">
                                                                </telerik:GridClientSelectColumn>--%>
                            <telerik:GridBoundColumn DataField="TargetID" Visible="false" HeaderText="TargetID" SortExpression="TargetID"
                                UniqueName="TargetID">
                            </telerik:GridBoundColumn>
                            <telerik:GridEditCommandColumn ButtonType="LinkButton" HeaderText="Edit"></telerik:GridEditCommandColumn>
                            <telerik:GridBoundColumn HeaderText="ID" ReadOnly="true" DataField="ID" UniqueName="ID"></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn HeaderText="Target Name*" DataField="TargetName" UniqueName="TargetName"></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="TargetShapeName" Visible="false"></telerik:GridBoundColumn>
                            <%--<telerik:GridTemplateColumn HeaderText="Target Shape" DataField="TargetShapeID" UniqueName="TargetShapeID">
                                <ItemTemplate>
                                    <asp:Label ID="lblTargetShape" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "TargetShapeName") %>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <telerik:RadDropDownList ID="ddlTargetShape" runat="server" OnSelectedIndexChanged="ddlTargetShape_SelectedIndexChanged" AutoPostBack="true">
                                        <Items>
                                            <telerik:DropDownListItem Value="0" Text="--Select--" />
                                            <telerik:DropDownListItem Value="1000" Text="Circle" />
                                            <telerik:DropDownListItem Value="1001" Text="Square" />
                                            <telerik:DropDownListItem Value="1002" Text="Rectangle" />
                                            <telerik:DropDownListItem Value="1003" Text="Polygon" />
                                            <telerik:DropDownListItem Value="1004" Text="Ellipse" />
                                            <telerik:DropDownListItem Value="1005" Text="Point" />
                                        </Items>
                                    </telerik:RadDropDownList>
                                </EditItemTemplate>
                            </telerik:GridTemplateColumn>--%>
                            <telerik:GridBoundColumn HeaderText="TVD*" DataField="TVD" UniqueName="TVD"></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn HeaderText="N-S Coordinate*" DataField="NSCoordinate" UniqueName="NSCoordinate"></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn HeaderText="E-W Coordinate*" DataField="EWCoordinate" UniqueName="EWCoordinate"></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn HeaderText="Polar Distance" DataField="PolarDistance" UniqueName="PolarDistance"></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn HeaderText="Polar Direction" DataField="PolarDirection" UniqueName="PolarDirection"></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn HeaderText="INC From Last Target" DataField="INCFromLastTarget" UniqueName="INCFromLastTarget"></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn HeaderText="AZM From Last Target" DataField="AZMFromLastTarget" UniqueName="AZMFromLastTarget"></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn HeaderText="Inclination at Target" DataField="InclinationAtTarget" UniqueName="InclinationAtTarget"></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn HeaderText="Azimuth at Target" DataField="AzimuthAtTarget" UniqueName="AzimuthAtTarget"></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn HeaderText="Number Verticals" DataField="NumberVertices" UniqueName="NumberVertices" Visible="false"></telerik:GridBoundColumn>
                            <%--<telerik:GridBoundColumn HeaderText="Rotation" DataField="Rotation" UniqueName="Rotation"></telerik:GridBoundColumn>--%>
                            <telerik:GridBoundColumn HeaderText="Target Thickness" DataField="TargetThickness" UniqueName="TargetThickness"></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="DrawingPatternName" Visible="false"></telerik:GridBoundColumn>
                            <telerik:GridTemplateColumn HeaderText="Drawing Pattern" DataField="DrawingPattern" UniqueName="DrawingPattern">
                                <ItemTemplate>
                                    <asp:Label ID="lblDrawingPattern" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "DrawingPatternName") %>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <telerik:RadDropDownList ID="ddlDrawingPattern" runat="server">
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
                                </EditItemTemplate>
                            </telerik:GridTemplateColumn>
                            <telerik:GridBoundColumn HeaderText="Target Comments" DataField="TargetComment" UniqueName="TargetComment"></telerik:GridBoundColumn>
                        </Columns>
                    </MasterTableView>
                </telerik:RadGrid>
            </ContentTemplate>
        </asp:UpdatePanel>
    </fieldset>

    <script type="text/javascript">
        function Graph(config, xGrid, yGrid, orginX, orginY) {
            // user defined properties
            this.canvas = document.getElementById(config.canvasId);
            this.minX = -10;
            this.minY = -10;
            this.maxX = 10;
            this.maxY = 10;
            this.unitsPerTick = config.unitsPerTick;


            if (orginX == null) {
                orginX = 300;
            }
            else {
                orginX = orginX;
            }


            if (orginY == null) {
                orginY = 300;
            }
            else {
                orginY = orginY;
            }



            // constants
            this.axisColor = '#aaa';
            this.font = '8pt Calibri';
            this.tickSize = 20;

            // relationships
            this.context = this.canvas.getContext('2d');
            this.rangeX = this.maxX - this.minX;
            this.rangeY = this.maxY - this.minY;
            this.unitX = this.canvas.width / this.rangeX;
            this.unitY = this.canvas.height / this.rangeY;
            this.centerY = orginY; //Math.round(Math.abs(this.minY / this.rangeY) * this.canvas.height);
            this.centerX = orginX; //Math.round(Math.abs(this.minX / this.rangeX) * this.canvas.width);
            this.iteration = (this.maxX - this.minX) / 1000;
            this.scaleX = this.canvas.width / this.rangeX;
            this.scaleY = this.canvas.height / this.rangeY;



            if (xGrid == null) {
                xGrid = 100;
            }

            else {
                xGrid = xGrid;
            }

            if (yGrid == null) {
                yGrid = 500;
            }
            else {
                yGrid = yGrid;
            }

            // draw x and y axis
            //this.drawXAxis(xGrid);
            //this.drawYAxis(yGrid);

        }

        Graph.prototype.transformContext = function () {
            var context = this.context;

            // move context to center of canvas
            this.context.translate(this.centerX, this.centerY);

            /*
             * stretch grid to fit the canvas window, and
             * invert the y scale so that that increments
             * as you move upwards
             */
            context.scale(this.scaleX, -this.scaleY);
        };
        var myGraph = new Graph({
            canvasId: 'myCanvas',
            minX: -10,
            minY: -10,
            maxX: 10,
            maxY: 10,
            unitsPerTick: 1
        });


        function ClearGraph() {
            var canvas = document.getElementById('myCanvas');
            var context = canvas.getContext('2d');

            context.clearRect(0, 0, canvas.width, canvas.height);

            var myGraph = new Graph({
                canvasId: 'myCanvas',
                minX: -10,
                minY: -10,
                maxX: 10,
                maxY: 10,
                unitsPerTick: 1
            });

            return false;

        }

    </script>

</asp:Content>
