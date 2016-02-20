<%@ Page Title="Create Graph" Language="C#" MasterPageFile="~/Masters/RigTrack.master" AutoEventWireup="true" CodeFile="CreateGraph.aspx.cs" Inherits="Modules_RigTrack_CreateGraph" EnableEventValidation="false" MaintainScrollPositionOnPostback="true" %>

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


        function Close() {
            GetRadWindow().close(); // Close the window 
        }
        function GetRadWindow() {
            var oWindow = null;
            if (window.radWindow) oWindow = window.radWindow; //Will work in Moz in all cases, including clasic dialog 
            else if (window.frameElement.radWindow) oWindow = window.frameElement.radWindow; //IE (and Moz as well) 

            return oWindow;
        }


        // jd for drawing polygon
        function drawShape(ctx, x, y, points, radius1, radius2, alpha0) {
            //points: number of points (or number of sides for polygons)
            //radius1: "outer" radius of the star
            //radius2: "inner" radius of the star (if equal to radius1, a polygon is drawn)
            //angle0: initial angle (clockwise), by default, stars and polygons are 'pointing' up
            var i, angle, radius;
            if (radius2 !== radius1) {
                points = 2 * points;
            }
            for (i = 0; i <= points; i++) {
                angle = i * 2 * Math.PI / points - Math.PI / 2 + alpha0;
                radius = i % 2 === 0 ? radius1 : radius2;
                ctx.lineTo(x + radius * Math.cos(angle), y + radius * Math.sin(angle));
            }
        }



        function ItemSelectedFromServer(curveGroupID) {


            //var item = eventArgs.get_item();
            ClearGraph();
            //bindTargets(item.get_value());
            $.ajax({
                type: "POST",
                url: 'CreateGraph.aspx/GetGraphDetails',
                data: '{curvegrpID: "' + curveGroupID + '" }',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (data) {
                    var displayval = data.d;

                    for (i = 0; i < displayval.length; i++) {
                        var splitval = displayval[i].shape;

                        if (splitval == "1000") {

                            var xWidth3;
                            var getEveryXCoord2;
                            var getEveryYCoord2;
                            // calculation for position of target and grid values
                            var xPositionFirstTarget2 = displayval[0].xcoordinate;
                            var xPosition3 = displayval[i].xcoordinate;
                            var subtractFirstTargetX2 = xPosition3 - xPositionFirstTarget2;

                            if (xPosition3 < 0) {
                                xWidth3 = -500;
                                getEveryXCoord2 = -100 * 10;

                            }
                            else {
                                xWidth3 = 500;
                                getEveryXCoord2 = 100 * 10;

                            }

                            var xPositionPlusX3 = getEveryXCoord2;
                            var finalXPosition3 = subtractFirstTargetX2 / xPositionPlusX3 * xWidth3;

                            //y grid and target calculations
                            var yWidth3;

                            // calculation for position of target and grid values
                            var yPositionFirstTarget2 = displayval[0].ycoordinate;
                            var yPosition3 = displayval[i].ycoordinate;
                            var subtractFirstTargetY2 = yPosition3 - yPositionFirstTarget2;

                            if (yPosition3 < 0) {
                                yWidth3 = -300;
                                getEveryYCoord2 = -100 * 10;
                            }
                            else {
                                yWidth3 = 300;
                                getEveryYCoord2 = 100 * 10;
                            }

                            var yPositionPlusY3 = getEveryYCoord2;
                            var finalYPosition3 = subtractFirstTargetY2 / yPositionPlusY3 * yWidth3;

                            var canvas1 = document.getElementById('myCanvas');
                            var centerX1 = canvas1.width / 2;
                            var centerY1 = canvas1.height / 2;
                            var context1 = canvas1.getContext('2d');
                            var xoffset1 = centerX1 + finalXPosition3;
                            var yoffset1 = centerY1 - finalYPosition3;

                            context1.beginPath();
                            context1.arc(xoffset1, yoffset1, 10, 0, Math.PI * 2, true);
                            context1.stroke();

                            if (displayval[i] === displayval[0]) {

                                // work curve x position 
                                var xPositionWorkCurve = -displayval[i].xcoordinate;
                                var xPositionWorkCurveGrid = getEveryXCoord2;
                                var finalXWorkCurve = xPositionWorkCurve / xPositionWorkCurveGrid * xWidth3;


                                // work curve y position 
                                var yPositionWorkCurve = -displayval[i].ycoordinate;
                                var yPositionWorkCurveGrid = getEveryYCoord2;
                                var finalYWorkCurve = yPositionWorkCurve / yPositionWorkCurveGrid * yWidth3;

                                // draw red circle
                                var canvas55 = document.getElementById('myCanvas');
                                var centerX55 = canvas55.width / 2;
                                var centerY55 = canvas55.height / 2;
                                var context55 = canvas55.getContext('2d');
                                var xoffset55 = centerX55 + finalXWorkCurve;
                                var yoffset55 = centerY55 - finalYWorkCurve;

                                context55.beginPath();
                                context55.strokeStyle = 'red';
                                context55.arc(xoffset55, yoffset55, 10, 0, Math.PI * 2, true);
                                context55.stroke();
                                //----------------------------


                                // draw line from red circle to first target
                                var canvas_line1 = document.getElementById('myCanvas');

                                var ctx1 = canvas_line1.getContext("2d")
                                ctx1.lineWidth = 2;
                                ctx1.strokeStyle = "#333";
                                ctx1.beginPath();
                                ctx1.moveTo(xoffset55, yoffset55);
                                ctx1.lineTo(xoffset1, yoffset1);
                                ctx1.stroke();
                            }

                        }
                        else if (splitval == "1001") {

                            var xWidth2;
                            var getEveryXCoord3;
                            var getEveryYCoord3;

                            // calculation for position of target and grid values
                            var xPositionFirstTarget = displayval[0].xcoordinate;
                            var xPosition2 = displayval[i].xcoordinate;
                            var subtractFirstTargetX = xPosition2 - xPositionFirstTarget;

                            if (xPosition2 < 0) {
                                xWidth2 = -500;
                                getEveryXCoord3 = -100 * 10;
                            }
                            else {
                                xWidth2 = 500;
                                getEveryXCoord3 = 100 * 10;
                            }

                            var xPositionPlusX2 = getEveryXCoord3;
                            var finalXPosition2 = subtractFirstTargetX / xPositionPlusX2 * xWidth2;
                            finalXPosition2 = finalXPosition2 - 30 / 2;

                            //y grid and target calculations
                            var yWidth2;

                            // calculation for position of target and grid values
                            var yPositionFirstTarget = displayval[0].ycoordinate;
                            var yPosition2 = displayval[i].ycoordinate;
                            var subtractFirstTargetY = yPosition2 - yPositionFirstTarget;

                            if (yPosition2 < 0) {
                                yWidth2 = -300;
                                getEveryYCoord3 = -100 * 10;

                            }
                            else {
                                yWidth2 = 300;
                                getEveryYCoord3 = 100 * 10;

                            }

                            var yPositionPlusY2 = getEveryYCoord3;
                            var finalYPosition2 = subtractFirstTargetY / yPositionPlusY2 * yWidth2;
                            finalYPosition2 = finalYPosition2 + 30 / 2;

                            var canvas3 = document.getElementById('myCanvas');
                            var centerX3 = canvas3.width / 2;
                            var centerY3 = canvas3.height / 2;
                            var context3 = canvas3.getContext('2d');

                            var xoffset3 = centerX3 + finalXPosition2;
                            var yoffset3 = centerY3 - finalYPosition2;

                            var rotationalValue = (45 * Math.PI / 180);



                            var drawX = 0;
                            var drawY = 0;
                            var drawWidth = 30;
                            var drawHeight = 30;
                            var drawcX = drawX + 0.5 * drawWidth;
                            var drawcY = drawY + 0.5 * drawHeight;

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
                        else if (splitval == "1002") {

                            var xWidth2;
                            var getEveryXCoord3;
                            var getEveryYCoord3;

                            // calculation for position of target and grid values
                            var xPositionFirstTarget = displayval[0].xcoordinate;
                            var xPosition2 = displayval[i].xcoordinate;
                            var subtractFirstTargetX = xPosition2 - xPositionFirstTarget;

                            if (xPosition2 < 0) {
                                xWidth2 = -500;
                                getEveryXCoord3 = -100 * 10;
                            }
                            else {
                                xWidth2 = 500;
                                getEveryXCoord3 = 100 * 10;
                            }

                            var xPositionPlusX2 = getEveryXCoord3;
                            var finalXPosition2 = subtractFirstTargetX / xPositionPlusX2 * xWidth2;
                            finalXPosition2 = finalXPosition2 - 30 / 2;

                            //y grid and target calculations
                            var yWidth2;

                            // calculation for position of target and grid values
                            var yPositionFirstTarget = displayval[0].ycoordinate;
                            var yPosition2 = displayval[i].ycoordinate;
                            var subtractFirstTargetY = yPosition2 - yPositionFirstTarget;

                            if (yPosition2 < 0) {
                                yWidth2 = -300;
                                getEveryYCoord3 = -100 * 10;

                            }
                            else {
                                yWidth2 = 300;
                                getEveryYCoord3 = 100 * 10;

                            }

                            var yPositionPlusY2 = getEveryYCoord3;
                            var finalYPosition2 = subtractFirstTargetY / yPositionPlusY2 * yWidth2;
                            finalYPosition2 = finalYPosition2 + 20 / 2;

                            var canvas3 = document.getElementById('myCanvas');
                            var centerX3 = canvas3.width / 2;
                            var centerY3 = canvas3.height / 2;
                            var context3 = canvas3.getContext('2d');

                            var xoffset3 = centerX3 + finalXPosition2;
                            var yoffset3 = centerY3 - finalYPosition2;

                            //context3.rotate(20 * Math.PI/ 180); // rotate Square 

                            context3.rect(xoffset3, yoffset3, 30, 20);
                            context3.lineWidth = 1;
                            context3.strokeStyle = 'black';
                            context3.stroke();

                        }

                        else if (splitval == "1003") {

                            var xWidth4;
                            var getEveryXCoord5;
                            var getEveryYCoord5;

                            // calculation for position of target and grid values
                            var xPositionFirstTarget4 = displayval[0].xcoordinate;
                            var xPosition4 = displayval[i].xcoordinate;
                            var subtractFirstTargetX4 = xPosition4 - xPositionFirstTarget4;

                            if (xPosition4 < 0) {
                                xWidth4 = -500;
                                getEveryXCoord5 = -100 * 10;

                            }
                            else {
                                xWidth4 = 500;
                                getEveryXCoord5 = 100 * 10;
                            }

                            var xPositionPlusX4 = getEveryXCoord5;
                            var finalXPosition4 = subtractFirstTargetX4 / xPositionPlusX4 * xWidth4;

                            //y grid and target calculations
                            var yWidth4;

                            // calculation for position of target and grid values
                            var yPositionFirstTarget4 = displayval[0].ycoordinate;
                            var yPosition4 = displayval[i].ycoordinate;
                            var subtractFirstTargetY4 = yPosition4 - yPositionFirstTarget4;

                            if (yPosition4 < 0) {
                                yWidth4 = -300;
                                getEveryYCoord5 = -100 * 10;
                            }
                            else {
                                yWidth4 = 300;
                                getEveryYCoord5 = 100 * 10;
                            }

                            var yPositionPlusY4 = getEveryYCoord5;
                            var finalYPosition4 = subtractFirstTargetY4 / yPositionPlusY4 * yWidth4;


                            var canvas4 = document.getElementById('myCanvas');
                            var centerX4 = canvas4.width / 2;
                            var centerY4 = canvas4.height / 2;
                            var context4 = canvas4.getContext('2d');
                            var xoffset4 = centerX4 + finalXPosition4;
                            var yoffset4 = centerY4 - finalYPosition4;
                            var numberOfVertices = displayval[i].numberOfVertices;


                            context4.beginPath();
                            drawShape(context4, xoffset4, yoffset4, numberOfVertices, 20, 20, 0);
                            context4.moveTo(xoffset4, yoffset4);
                            context4.strokeStyle = "black";
                            context4.lineWidth = 1;
                            context4.stroke();

                        }

                        else if (splitval == "1004") {

                            var xWidth5;
                            var getEveryXCoord6;
                            var getEveryYCoord6;

                            // calculation for position of target and grid values
                            var xPositionFirstTarget5 = displayval[0].xcoordinate;
                            var xPosition5 = displayval[i].xcoordinate;
                            var subtractFirstTargetX5 = xPosition5 - xPositionFirstTarget5;

                            if (xPosition5 < 0) {
                                xWidth5 = -500;
                                getEveryXCoord6 = -100 * 10;

                            }
                            else {
                                xWidth5 = 500;
                                getEveryXCoord6 = 100 * 10;
                            }

                            var xPositionPlusX5 = getEveryXCoord6;
                            var finalXPosition5 = subtractFirstTargetX5 / xPositionPlusX5 * xWidth5;

                            //y grid and target calculations
                            var yWidth5;

                            // calculation for position of target and grid values
                            var yPositionFirstTarget5 = displayval[0].ycoordinate;
                            var yPosition5 = displayval[i].ycoordinate;
                            var subtractFirstTargetY5 = yPosition5 - yPositionFirstTarget5;

                            if (yPosition5 < 0) {
                                yWidth5 = -300;
                                getEveryYCoord6 = -100 * 10;
                            }
                            else {
                                yWidth5 = 300;
                                getEveryYCoord6 = 100 * 10;
                            }

                            var yPositionPlusY5 = getEveryYCoord6;
                            var finalYPosition5 = subtractFirstTargetY5 / yPositionPlusY5 * yWidth5;


                            var canvas6 = document.getElementById('myCanvas');
                            var centerX6 = canvas6.width / 2;
                            var centerY6 = canvas6.height / 2;
                            var context6 = canvas6.getContext('2d');
                            var xoffset6 = centerX6 + finalXPosition5;
                            var yoffset6 = centerY6 - finalYPosition5;

                            context6.beginPath();
                            context6.ellipse(xoffset6, yoffset6, 25, 50, 45, 0, 2 * Math.PI);

                            context6.strokeStyle = "black";
                            context6.lineWidth = 1;
                            context6.stroke();
                        }

                        else if (splitval == "1005") {

                            var xWidth6;
                            var getEveryXCoord7;
                            var getEveryYCoord7;


                            // calculation for position of target and grid values
                            var xPositionFirstTarget6 = displayval[0].xcoordinate;
                            var xPosition6 = displayval[i].xcoordinate;
                            var subtractFirstTargetX6 = xPosition6 - xPositionFirstTarget6;

                            if (xPosition6 < 0) {
                                xWidth6 = -500;
                                getEveryXCoord7 = -100 * 10;
                            }
                            else {
                                xWidth6 = 500;
                                getEveryXCoord7 = 100 * 10;
                            }

                            var xPositionPlusX6 = getEveryXCoord7;
                            var finalXPosition6 = subtractFirstTargetX6 / xPositionPlusX6 * xWidth6;

                            //y grid and target calculations
                            var yWidth6;


                            // calculation for position of target and grid values
                            var yPositionFirstTarget6 = displayval[0].ycoordinate;
                            var yPosition6 = displayval[i].ycoordinate;
                            var subtractFirstTargetY6 = yPosition6 - yPositionFirstTarget6;

                            if (yPosition6 < 0) {
                                yWidth6 = -300;
                                getEveryYCoord7 = -100 * 10;
                            }
                            else {
                                yWidth6 = 300;
                                getEveryYCoord7 = 100 * 10;
                            }

                            var yPositionPlusY6 = getEveryYCoord7;
                            var finalYPosition6 = subtractFirstTargetY6 / yPositionPlusY6 * yWidth6;

                            var canvas7 = document.getElementById('myCanvas');
                            var centerX7 = canvas7.width / 2;
                            var centerY7 = canvas7.height / 2;
                            var context7 = canvas7.getContext('2d');
                            var xoffset7 = centerX7 + finalXPosition6;
                            var yoffset7 = centerY7 - finalYPosition6;

                            context7.beginPath();
                            context7.fillStyle = "black";
                            context7.arc(xoffset7, yoffset7, 5, 0, Math.PI * 2, true);
                            context7.stroke();
                            context7.fill();
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

        // jd this is for the zooming in and out buttons
        function ItemSelectedFromServer2(curveGroupID, getEveryXCoord, getEveryYCoord) {


            //var item = eventArgs.get_item();
            //ClearGraph();
            //bindTargets(item.get_value());
            $.ajax({
                type: "POST",
                url: 'CreateGraph.aspx/GetGraphDetails',
                data: '{curvegrpID: "' + curveGroupID + '" }',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (data) {
                    var displayval = data.d;

                    for (i = 0; i < displayval.length; i++) {
                        var splitval = displayval[i].shape;

                        if (splitval == "1000") {

                            var xWidth3;
                            var getEveryXCoord2;
                            var getEveryYCoord2;
                            // calculation for position of target and grid values
                            var xPositionFirstTarget2 = displayval[0].xcoordinate;
                            var xPosition3 = displayval[i].xcoordinate;
                            var subtractFirstTargetX2 = xPosition3 - xPositionFirstTarget2;

                            if (xPosition3 < 0) {
                                xWidth3 = -500;
                                getEveryXCoord2 = -getEveryXCoord * 10;

                            }
                            else {
                                xWidth3 = 500;
                                getEveryXCoord2 = getEveryXCoord * 10;

                            }

                            var xPositionPlusX3 = getEveryXCoord2;
                            var finalXPosition3 = subtractFirstTargetX2 / xPositionPlusX3 * xWidth3;

                            //y grid and target calculations
                            var yWidth3;

                            // calculation for position of target and grid values
                            var yPositionFirstTarget2 = displayval[0].ycoordinate;
                            var yPosition3 = displayval[i].ycoordinate;
                            var subtractFirstTargetY2 = yPosition3 - yPositionFirstTarget2;

                            if (yPosition3 < 0) {
                                yWidth3 = -300;
                                getEveryYCoord2 = -getEveryYCoord * 10;
                            }
                            else {
                                yWidth3 = 300;
                                getEveryYCoord2 = getEveryYCoord * 10;
                            }

                            var yPositionPlusY3 = getEveryYCoord2;
                            var finalYPosition3 = subtractFirstTargetY2 / yPositionPlusY3 * yWidth3;

                            var canvas1 = document.getElementById('myCanvas');
                            var centerX1 = canvas1.width / 2;
                            var centerY1 = canvas1.height / 2;
                            var context1 = canvas1.getContext('2d');
                            var xoffset1 = centerX1 + finalXPosition3;
                            var yoffset1 = centerY1 - finalYPosition3;

                            context1.beginPath();
                            context1.arc(xoffset1, yoffset1, 10, 0, Math.PI * 2, true);
                            context1.stroke();

                            if (displayval[i] === displayval[0]) {

                                // work curve x position 
                                var xPositionWorkCurve = -displayval[i].xcoordinate;
                                var xPositionWorkCurveGrid = getEveryXCoord2;
                                var finalXWorkCurve = xPositionWorkCurve / xPositionWorkCurveGrid * xWidth3;


                                // work curve y position 
                                var yPositionWorkCurve = -displayval[i].ycoordinate;
                                var yPositionWorkCurveGrid = getEveryYCoord2;
                                var finalYWorkCurve = yPositionWorkCurve / yPositionWorkCurveGrid * yWidth3;

                                // draw red circle
                                var canvas55 = document.getElementById('myCanvas');
                                var centerX55 = canvas55.width / 2;
                                var centerY55 = canvas55.height / 2;
                                var context55 = canvas55.getContext('2d');
                                var xoffset55 = centerX55 + finalXWorkCurve;
                                var yoffset55 = centerY55 - finalYWorkCurve;

                                context55.beginPath();
                                context55.strokeStyle = 'red';
                                context55.arc(xoffset55, yoffset55, 10, 0, Math.PI * 2, true);
                                context55.stroke();
                                //----------------------------


                                // draw line from red circle to first target
                                var canvas_line1 = document.getElementById('myCanvas');

                                var ctx1 = canvas_line1.getContext("2d")
                                ctx1.lineWidth = 2;
                                ctx1.strokeStyle = "#333";
                                ctx1.beginPath();
                                ctx1.moveTo(xoffset55, yoffset55);
                                ctx1.lineTo(xoffset1, yoffset1);
                                ctx1.stroke();
                            }

                        }
                        else if (splitval == "1001") {

                            var xWidth2;
                            var getEveryXCoord3;
                            var getEveryYCoord3;

                            // calculation for position of target and grid values
                            var xPositionFirstTarget = displayval[0].xcoordinate;
                            var xPosition2 = displayval[i].xcoordinate;
                            var subtractFirstTargetX = xPosition2 - xPositionFirstTarget;

                            if (xPosition2 < 0) {
                                xWidth2 = -500;
                                getEveryXCoord3 = -getEveryXCoord * 10;
                            }
                            else {
                                xWidth2 = 500;
                                getEveryXCoord3 = getEveryXCoord * 10;
                            }

                            var xPositionPlusX2 = getEveryXCoord3;
                            var finalXPosition2 = subtractFirstTargetX / xPositionPlusX2 * xWidth2;
                            finalXPosition2 = finalXPosition2 - 30 / 2;

                            //y grid and target calculations
                            var yWidth2;

                            // calculation for position of target and grid values
                            var yPositionFirstTarget = displayval[0].ycoordinate;
                            var yPosition2 = displayval[i].ycoordinate;
                            var subtractFirstTargetY = yPosition2 - yPositionFirstTarget;

                            if (yPosition2 < 0) {
                                yWidth2 = -300;
                                getEveryYCoord3 = -getEveryYCoord * 10;

                            }
                            else {
                                yWidth2 = 300;
                                getEveryYCoord3 = getEveryYCoord * 10;

                            }

                            var yPositionPlusY2 = getEveryYCoord3;
                            var finalYPosition2 = subtractFirstTargetY / yPositionPlusY2 * yWidth2;
                            finalYPosition2 = finalYPosition2 + 30 / 2;

                            var canvas3 = document.getElementById('myCanvas');
                            var centerX3 = canvas3.width / 2;
                            var centerY3 = canvas3.height / 2;
                            var context3 = canvas3.getContext('2d');

                            var xoffset3 = centerX3 + finalXPosition2;
                            var yoffset3 = centerY3 - finalYPosition2;

                            var rotationalValue = (45 * (Math.PI / 180));

                            var drawX = 0;
                            var drawY = 0;
                            var drawWidth = 30;
                            var drawHeight = 30;
                            var drawcX = drawX + 0.5 * drawWidth;
                            var drawcY = drawY + 0.5 * drawHeight;

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
                        else if (splitval == "1002") {

                            var xWidth2;
                            var getEveryXCoord3;
                            var getEveryYCoord3;

                            // calculation for position of target and grid values
                            var xPositionFirstTarget = displayval[0].xcoordinate;
                            var xPosition2 = displayval[i].xcoordinate;
                            var subtractFirstTargetX = xPosition2 - xPositionFirstTarget;

                            if (xPosition2 < 0) {
                                xWidth2 = -500;
                                getEveryXCoord3 = -getEveryXCoord * 10;
                            }
                            else {
                                xWidth2 = 500;
                                getEveryXCoord3 = getEveryXCoord * 10;
                            }

                            var xPositionPlusX2 = getEveryXCoord3;
                            var finalXPosition2 = subtractFirstTargetX / xPositionPlusX2 * xWidth2;
                            finalXPosition2 = finalXPosition2 - 30 / 2;

                            //y grid and target calculations
                            var yWidth2;

                            // calculation for position of target and grid values
                            var yPositionFirstTarget = displayval[0].ycoordinate;
                            var yPosition2 = displayval[i].ycoordinate;
                            var subtractFirstTargetY = yPosition2 - yPositionFirstTarget;

                            if (yPosition2 < 0) {
                                yWidth2 = -300;
                                getEveryYCoord3 = -getEveryYCoord * 10;

                            }
                            else {
                                yWidth2 = 300;
                                getEveryYCoord3 = getEveryYCoord * 10;

                            }

                            var yPositionPlusY2 = getEveryYCoord3;
                            var finalYPosition2 = subtractFirstTargetY / yPositionPlusY2 * yWidth2;
                            finalYPosition2 = finalYPosition2 + 20 / 2;

                            var canvas3 = document.getElementById('myCanvas');
                            var centerX3 = canvas3.width / 2;
                            var centerY3 = canvas3.height / 2;
                            var context3 = canvas3.getContext('2d');

                            var xoffset3 = centerX3 + finalXPosition2;
                            var yoffset3 = centerY3 - finalYPosition2;

                            //context3.rotate(20 * Math.PI/ 180); // rotate Square 

                            context3.rect(xoffset3, yoffset3, 30, 20);
                            context3.lineWidth = 1;
                            context3.strokeStyle = 'black';
                            context3.stroke();

                        }

                        else if (splitval == "1003") {

                            var xWidth4;
                            var getEveryXCoord5;
                            var getEveryYCoord5;

                            // calculation for position of target and grid values
                            var xPositionFirstTarget4 = displayval[0].xcoordinate;
                            var xPosition4 = displayval[i].xcoordinate;
                            var subtractFirstTargetX4 = xPosition4 - xPositionFirstTarget4;

                            if (xPosition4 < 0) {
                                xWidth4 = -500;
                                getEveryXCoord5 = -getEveryXCoord * 10;

                            }
                            else {
                                xWidth4 = 500;
                                getEveryXCoord5 = getEveryXCoord * 10;
                            }

                            var xPositionPlusX4 = getEveryXCoord5;
                            var finalXPosition4 = subtractFirstTargetX4 / xPositionPlusX4 * xWidth4;

                            //y grid and target calculations
                            var yWidth4;

                            // calculation for position of target and grid values
                            var yPositionFirstTarget4 = displayval[0].ycoordinate;
                            var yPosition4 = displayval[i].ycoordinate;
                            var subtractFirstTargetY4 = yPosition4 - yPositionFirstTarget4;

                            if (yPosition4 < 0) {
                                yWidth4 = -300;
                                getEveryYCoord5 = -getEveryYCoord * 10;
                            }
                            else {
                                yWidth4 = 300;
                                getEveryYCoord5 = getEveryYCoord * 10;
                            }

                            var yPositionPlusY4 = getEveryYCoord5;
                            var finalYPosition4 = subtractFirstTargetY4 / yPositionPlusY4 * yWidth4;


                            var canvas4 = document.getElementById('myCanvas');
                            var centerX4 = canvas4.width / 2;
                            var centerY4 = canvas4.height / 2;
                            var context4 = canvas4.getContext('2d');
                            var xoffset4 = centerX4 + finalXPosition4;
                            var yoffset4 = centerY4 - finalYPosition4;
                            var numberOfVertices = displayval[i].numberOfVertices;


                            context4.beginPath();
                            drawShape(context4, xoffset4, yoffset4, numberOfVertices, 20, 20, 0);
                            context4.moveTo(xoffset4, yoffset4);
                            context4.strokeStyle = "black";
                            context4.lineWidth = 1;
                            context4.stroke();

                        }

                        else if (splitval == "1004") {

                            var xWidth5;
                            var getEveryXCoord6;
                            var getEveryYCoord6;

                            // calculation for position of target and grid values
                            var xPositionFirstTarget5 = displayval[0].xcoordinate;
                            var xPosition5 = displayval[i].xcoordinate;
                            var subtractFirstTargetX5 = xPosition5 - xPositionFirstTarget5;

                            if (xPosition5 < 0) {
                                xWidth5 = -500;
                                getEveryXCoord6 = -getEveryXCoord * 10;

                            }
                            else {
                                xWidth5 = 500;
                                getEveryXCoord6 = getEveryXCoord * 10;
                            }

                            var xPositionPlusX5 = getEveryXCoord6;
                            var finalXPosition5 = subtractFirstTargetX5 / xPositionPlusX5 * xWidth5;

                            //y grid and target calculations
                            var yWidth5;

                            // calculation for position of target and grid values
                            var yPositionFirstTarget5 = displayval[0].ycoordinate;
                            var yPosition5 = displayval[i].ycoordinate;
                            var subtractFirstTargetY5 = yPosition5 - yPositionFirstTarget5;

                            if (yPosition5 < 0) {
                                yWidth5 = -300;
                                getEveryYCoord6 = -getEveryYCoord * 10;
                            }
                            else {
                                yWidth5 = 300;
                                getEveryYCoord6 = getEveryYCoord * 10;
                            }

                            var yPositionPlusY5 = getEveryYCoord6;
                            var finalYPosition5 = subtractFirstTargetY5 / yPositionPlusY5 * yWidth5;


                            var canvas6 = document.getElementById('myCanvas');
                            var centerX6 = canvas6.width / 2;
                            var centerY6 = canvas6.height / 2;
                            var context6 = canvas6.getContext('2d');
                            var xoffset6 = centerX6 + finalXPosition5;
                            var yoffset6 = centerY6 - finalYPosition5;

                            context6.beginPath();
                            context6.ellipse(xoffset6, yoffset6, 25, 50, 45, 0, 2 * Math.PI);

                            context6.strokeStyle = "black";
                            context6.lineWidth = 1;
                            context6.stroke();
                        }

                        else if (splitval == "1005") {

                            var xWidth6;
                            var getEveryXCoord7;
                            var getEveryYCoord7;


                            // calculation for position of target and grid values
                            var xPositionFirstTarget6 = displayval[0].xcoordinate;
                            var xPosition6 = displayval[i].xcoordinate;
                            var subtractFirstTargetX6 = xPosition6 - xPositionFirstTarget6;

                            if (xPosition6 < 0) {
                                xWidth6 = -500;
                                getEveryXCoord7 = -getEveryXCoord * 10;
                            }
                            else {
                                xWidth6 = 500;
                                getEveryXCoord7 = getEveryXCoord * 10;
                            }

                            var xPositionPlusX6 = getEveryXCoord7;
                            var finalXPosition6 = subtractFirstTargetX6 / xPositionPlusX6 * xWidth6;

                            //y grid and target calculations
                            var yWidth6;


                            // calculation for position of target and grid values
                            var yPositionFirstTarget6 = displayval[0].ycoordinate;
                            var yPosition6 = displayval[i].ycoordinate;
                            var subtractFirstTargetY6 = yPosition6 - yPositionFirstTarget6;

                            if (yPosition6 < 0) {
                                yWidth6 = -300;
                                getEveryYCoord7 = -getEveryYCoord * 10;
                            }
                            else {
                                yWidth6 = 300;
                                getEveryYCoord7 = getEveryYCoord * 10;
                            }

                            var yPositionPlusY6 = getEveryYCoord7;
                            var finalYPosition6 = subtractFirstTargetY6 / yPositionPlusY6 * yWidth6;

                            var canvas7 = document.getElementById('myCanvas');
                            var centerX7 = canvas7.width / 2;
                            var centerY7 = canvas7.height / 2;
                            var context7 = canvas7.getContext('2d');
                            var xoffset7 = centerX7 + finalXPosition6;
                            var yoffset7 = centerY7 - finalYPosition6;

                            context7.beginPath();
                            context7.fillStyle = "black";
                            context7.arc(xoffset7, yoffset7, 5, 0, Math.PI * 2, true);
                            context7.stroke();
                            context7.fill();
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


        function OnClientItemSelected(sender, eventArgs) {


            var item = eventArgs.get_item();
            ClearGraph();
            getEveryXCoord = 100;
            getEveryYCoord = 100;
            bindTargets(item.get_value());
            $.ajax({
                type: "POST",
                url: 'CreateGraph.aspx/GetGraphDetails',
                data: '{curvegrpID: "' + item.get_value() + '" }',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (data) {
                    var displayval = data.d;

                    var targetTVD = displayval[0].tvd;
                    document.getElementById("txtTVD").value = targetTVD; // set the text box tvd

                    for (i = 0; i < displayval.length; i++) {
                        var splitval = displayval[i].shape;


                        if (splitval == "1000") {

                            var xWidth3;
                            var getEveryXCoord2;
                            var getEveryYCoord2;
                            // calculation for position of target and grid values
                            var xPositionFirstTarget2 = displayval[0].xcoordinate;
                            var xPosition3 = displayval[i].xcoordinate;
                            var subtractFirstTargetX2 = xPosition3 - xPositionFirstTarget2;

                            if (xPosition3 < 0) {
                                xWidth3 = -500;
                                getEveryXCoord2 = -100 * 10;

                            }
                            else {
                                xWidth3 = 500;
                                getEveryXCoord2 = 100 * 10;

                            }

                            var xPositionPlusX3 = getEveryXCoord2;
                            var finalXPosition3 = subtractFirstTargetX2 / xPositionPlusX3 * xWidth3;

                            //y grid and target calculations
                            var yWidth3;

                            // calculation for position of target and grid values
                            var yPositionFirstTarget2 = displayval[0].ycoordinate;
                            var yPosition3 = displayval[i].ycoordinate;
                            var subtractFirstTargetY2 = yPosition3 - yPositionFirstTarget2;

                            if (yPosition3 < 0) {
                                yWidth3 = -300;
                                getEveryYCoord2 = -100 * 10;
                            }
                            else {
                                yWidth3 = 300;
                                getEveryYCoord2 = 100 * 10;
                            }

                            var yPositionPlusY3 = getEveryYCoord2;
                            var finalYPosition3 = subtractFirstTargetY2 / yPositionPlusY3 * yWidth3;

                            var canvas1 = document.getElementById('myCanvas');
                            var centerX1 = canvas1.width / 2;
                            var centerY1 = canvas1.height / 2;
                            var context1 = canvas1.getContext('2d');
                            var xoffset1 = centerX1 + finalXPosition3;
                            var yoffset1 = centerY1 - finalYPosition3;

                            context1.beginPath();
                            context1.arc(xoffset1, yoffset1, 10, 0, Math.PI * 2, true);
                            context1.stroke();

                            if (displayval[i] === displayval[0]) {

                                // work curve x position 
                                var xPositionWorkCurve = -displayval[i].xcoordinate;
                                var xPositionWorkCurveGrid = getEveryXCoord2;
                                var finalXWorkCurve = xPositionWorkCurve / xPositionWorkCurveGrid * xWidth3;


                                // work curve y position 
                                var yPositionWorkCurve = -displayval[i].ycoordinate;
                                var yPositionWorkCurveGrid = getEveryYCoord2;
                                var finalYWorkCurve = yPositionWorkCurve / yPositionWorkCurveGrid * yWidth3;

                                // draw red circle
                                var canvas55 = document.getElementById('myCanvas');
                                var centerX55 = canvas55.width / 2;
                                var centerY55 = canvas55.height / 2;
                                var context55 = canvas55.getContext('2d');
                                var xoffset55 = centerX55 + finalXWorkCurve;
                                var yoffset55 = centerY55 - finalYWorkCurve;

                                context55.beginPath();
                                context55.strokeStyle = 'red';
                                context55.arc(xoffset55, yoffset55, 10, 0, Math.PI * 2, true);
                                context55.stroke();
                                //----------------------------


                                // draw line from red circle to first target
                                var canvas_line1 = document.getElementById('myCanvas');

                                var ctx1 = canvas_line1.getContext("2d")
                                ctx1.lineWidth = 2;
                                ctx1.strokeStyle = "#333";
                                ctx1.beginPath();
                                ctx1.moveTo(xoffset55, yoffset55);
                                ctx1.lineTo(xoffset1, yoffset1);
                                ctx1.stroke();
                            }


                        }
                        else if (splitval == "1001") {

                            var xWidth2;
                            var getEveryXCoord3;
                            var getEveryYCoord3;

                            // calculation for position of target and grid values
                            var xPositionFirstTarget = displayval[0].xcoordinate;
                            var xPosition2 = displayval[i].xcoordinate;
                            var subtractFirstTargetX = xPosition2 - xPositionFirstTarget;

                            if (xPosition2 < 0) {
                                xWidth2 = -500;
                                getEveryXCoord3 = -100 * 10;
                            }
                            else {
                                xWidth2 = 500;
                                getEveryXCoord3 = 100 * 10;
                            }

                            var xPositionPlusX2 = getEveryXCoord3;
                            var finalXPosition2 = subtractFirstTargetX / xPositionPlusX2 * xWidth2;
                            finalXPosition2 = finalXPosition2 - 30 / 2;

                            //y grid and target calculations
                            var yWidth2;

                            // calculation for position of target and grid values
                            var yPositionFirstTarget = displayval[0].ycoordinate;
                            var yPosition2 = displayval[i].ycoordinate;
                            var subtractFirstTargetY = yPosition2 - yPositionFirstTarget;

                            if (yPosition2 < 0) {
                                yWidth2 = -300;
                                getEveryYCoord3 = -100 * 10;

                            }
                            else {
                                yWidth2 = 300;
                                getEveryYCoord3 = 100 * 10;

                            }

                            var yPositionPlusY2 = getEveryYCoord3;
                            var finalYPosition2 = subtractFirstTargetY / yPositionPlusY2 * yWidth2;
                            finalYPosition2 = finalYPosition2 + 30 / 2;

                            var canvas3 = document.getElementById('myCanvas');
                            var centerX3 = canvas3.width / 2;
                            var centerY3 = canvas3.height / 2;
                            var context3 = canvas3.getContext('2d');

                            var xoffset3 = centerX3 + finalXPosition2;
                            var yoffset3 = centerY3 - finalYPosition2;


                            var rotationalValue = (45 * (Math.PI / 180));

                            var drawX = 0;
                            var drawY = 0;
                            var drawWidth = 30;
                            var drawHeight = 30;
                            var drawcX = drawX + 0.5 * drawWidth;
                            var drawcY = drawY + 0.5 * drawHeight;

                            var rotationalValue = (45 * (Math.PI / 180));

                            var drawX = 0;
                            var drawY = 0;
                            var drawWidth = 30;
                            var drawHeight = 30;
                            var drawcX = drawX + 0.5 * drawWidth;
                            var drawcY = drawY + 0.5 * drawHeight;

                            context3.save();
                            context3.beginPath();
                            context3.translate(xoffset3 + (drawWidth / 2), yoffset3 + (drawHeight / 2));
                            context3.rotate(rotationalValue);
                            context3.translate(-drawcX, -drawcY);
                            context3.scale(1, 1);

                            //context3.fillStyle = "black";
                            context3.strokeStyle = "black";
                            context3.rect(drawX, drawY, drawWidth, drawHeight);
                            context3.stroke();
                            //context3.fillRect(drawX, drawY, drawWidth, drawHeight);
                            context3.restore();

                            //context3.save();
                            //context3.beginPath();
                            //context3.translate(xoffset3 +(drawWidth / 2), yoffset3 + (drawHeight / 2));
                            //context3.rotate(45 *( Math.PI / 180));
                            //context3.translate(-drawcX, -drawcY);
                            //context3.scale(1, 1);
                            //context3.fillStyle = "red";
                            //context3.fillRect(drawX , drawY , drawWidth, drawHeight);
                            //context3.restore();


                        }
                        else if (splitval == "1002") {

                            var xWidth2;
                            var getEveryXCoord3;
                            var getEveryYCoord3;

                            // calculation for position of target and grid values
                            var xPositionFirstTarget = displayval[0].xcoordinate;
                            var xPosition2 = displayval[i].xcoordinate;
                            var subtractFirstTargetX = xPosition2 - xPositionFirstTarget;

                            if (xPosition2 < 0) {
                                xWidth2 = -500;
                                getEveryXCoord3 = -100 * 10;
                            }
                            else {
                                xWidth2 = 500;
                                getEveryXCoord3 = 100 * 10;
                            }

                            var xPositionPlusX2 = getEveryXCoord3;
                            var finalXPosition2 = subtractFirstTargetX / xPositionPlusX2 * xWidth2;
                            finalXPosition2 = finalXPosition2 - 30 / 2;

                            //y grid and target calculations
                            var yWidth2;

                            // calculation for position of target and grid values
                            var yPositionFirstTarget = displayval[0].ycoordinate;
                            var yPosition2 = displayval[i].ycoordinate;
                            var subtractFirstTargetY = yPosition2 - yPositionFirstTarget;

                            if (yPosition2 < 0) {
                                yWidth2 = -300;
                                getEveryYCoord3 = -100 * 10;

                            }
                            else {
                                yWidth2 = 300;
                                getEveryYCoord3 = 100 * 10;

                            }

                            var yPositionPlusY2 = getEveryYCoord3;
                            var finalYPosition2 = subtractFirstTargetY / yPositionPlusY2 * yWidth2;
                            finalYPosition2 = finalYPosition2 + 20 / 2;

                            var canvas3 = document.getElementById('myCanvas');
                            var centerX3 = canvas3.width / 2;
                            var centerY3 = canvas3.height / 2;
                            var context3 = canvas3.getContext('2d');

                            var xoffset3 = centerX3 + finalXPosition2;
                            var yoffset3 = centerY3 - finalYPosition2;

                            //context3.rotate(20 * Math.PI/ 180); // rotate Square 

                            context3.rect(xoffset3, yoffset3, 30, 20);
                            context3.lineWidth = 1;
                            context3.strokeStyle = 'black';
                            context3.stroke();

                        }

                        else if (splitval == "1003") {

                            var xWidth4;
                            var getEveryXCoord5;
                            var getEveryYCoord5;

                            // calculation for position of target and grid values
                            var xPositionFirstTarget4 = displayval[0].xcoordinate;
                            var xPosition4 = displayval[i].xcoordinate;
                            var subtractFirstTargetX4 = xPosition4 - xPositionFirstTarget4;

                            if (xPosition4 < 0) {
                                xWidth4 = -500;
                                getEveryXCoord5 = -100 * 10;

                            }
                            else {
                                xWidth4 = 500;
                                getEveryXCoord5 = 100 * 10;
                            }

                            var xPositionPlusX4 = getEveryXCoord5;
                            var finalXPosition4 = subtractFirstTargetX4 / xPositionPlusX4 * xWidth4;

                            //y grid and target calculations
                            var yWidth4;

                            // calculation for position of target and grid values
                            var yPositionFirstTarget4 = displayval[0].ycoordinate;
                            var yPosition4 = displayval[i].ycoordinate;
                            var subtractFirstTargetY4 = yPosition4 - yPositionFirstTarget4;

                            if (yPosition4 < 0) {
                                yWidth4 = -300;
                                getEveryYCoord5 = -100 * 10;
                            }
                            else {
                                yWidth4 = 300;
                                getEveryYCoord5 = 100 * 10;
                            }

                            var yPositionPlusY4 = getEveryYCoord5;
                            var finalYPosition4 = subtractFirstTargetY4 / yPositionPlusY4 * yWidth4;


                            var canvas4 = document.getElementById('myCanvas');
                            var centerX4 = canvas4.width / 2;
                            var centerY4 = canvas4.height / 2;
                            var context4 = canvas4.getContext('2d');
                            var xoffset4 = centerX4 + finalXPosition4;
                            var yoffset4 = centerY4 - finalYPosition4;
                            var numberOfVertices = displayval[i].numberOfVertices;


                            context4.beginPath();
                            drawShape(context4, xoffset4, yoffset4, numberOfVertices, 20, 20, 0);
                            context4.moveTo(xoffset4, yoffset4);
                            context4.strokeStyle = "black";
                            context4.lineWidth = 1;
                            context4.stroke();
                        }

                        else if (splitval == "1004") {

                            var xWidth5;
                            var getEveryXCoord6;
                            var getEveryYCoord6;

                            // calculation for position of target and grid values
                            var xPositionFirstTarget5 = displayval[0].xcoordinate;
                            var xPosition5 = displayval[i].xcoordinate;
                            var subtractFirstTargetX5 = xPosition5 - xPositionFirstTarget5;

                            if (xPosition5 < 0) {
                                xWidth5 = -500;
                                getEveryXCoord6 = -100 * 10;

                            }
                            else {
                                xWidth5 = 500;
                                getEveryXCoord6 = 100 * 10;
                            }

                            var xPositionPlusX5 = getEveryXCoord6;
                            var finalXPosition5 = subtractFirstTargetX5 / xPositionPlusX5 * xWidth5;

                            //y grid and target calculations
                            var yWidth5;

                            // calculation for position of target and grid values
                            var yPositionFirstTarget5 = displayval[0].ycoordinate;
                            var yPosition5 = displayval[i].ycoordinate;
                            var subtractFirstTargetY5 = yPosition5 - yPositionFirstTarget5;

                            if (yPosition5 < 0) {
                                yWidth5 = -300;
                                getEveryYCoord6 = -100 * 10;
                            }
                            else {
                                yWidth5 = 300;
                                getEveryYCoord6 = 100 * 10;
                            }

                            var yPositionPlusY5 = getEveryYCoord6;
                            var finalYPosition5 = subtractFirstTargetY5 / yPositionPlusY5 * yWidth5;


                            var canvas6 = document.getElementById('myCanvas');
                            var centerX6 = canvas6.width / 2;
                            var centerY6 = canvas6.height / 2;
                            var context6 = canvas6.getContext('2d');
                            var xoffset6 = centerX6 + finalXPosition5;
                            var yoffset6 = centerY6 - finalYPosition5;

                            context6.beginPath();
                            context6.ellipse(xoffset6, yoffset6, 25, 50, 45, 0, 2 * Math.PI);

                            context6.strokeStyle = "black";
                            context6.lineWidth = 1;
                            context6.stroke();
                        }

                        else if (splitval == "1005") {

                            var xWidth6;
                            var getEveryXCoord7;
                            var getEveryYCoord7;


                            // calculation for position of target and grid values
                            var xPositionFirstTarget6 = displayval[0].xcoordinate;
                            var xPosition6 = displayval[i].xcoordinate;
                            var subtractFirstTargetX6 = xPosition6 - xPositionFirstTarget6;

                            if (xPosition6 < 0) {
                                xWidth6 = -500;
                                getEveryXCoord7 = -100 * 10;
                            }
                            else {
                                xWidth6 = 500;
                                getEveryXCoord7 = 100 * 10;
                            }

                            var xPositionPlusX6 = getEveryXCoord7;
                            var finalXPosition6 = subtractFirstTargetX6 / xPositionPlusX6 * xWidth6;

                            //y grid and target calculations
                            var yWidth6;


                            // calculation for position of target and grid values
                            var yPositionFirstTarget6 = displayval[0].ycoordinate;
                            var yPosition6 = displayval[i].ycoordinate;
                            var subtractFirstTargetY6 = yPosition6 - yPositionFirstTarget6;

                            if (yPosition6 < 0) {
                                yWidth6 = -300;
                                getEveryYCoord7 = -100 * 10;
                            }
                            else {
                                yWidth6 = 300;
                                getEveryYCoord7 = 100 * 10;
                            }

                            var yPositionPlusY6 = getEveryYCoord7;
                            var finalYPosition6 = subtractFirstTargetY6 / yPositionPlusY6 * yWidth6;

                            var canvas7 = document.getElementById('myCanvas');
                            var centerX7 = canvas7.width / 2;
                            var centerY7 = canvas7.height / 2;
                            var context7 = canvas7.getContext('2d');
                            var xoffset7 = centerX7 + finalXPosition6;
                            var yoffset7 = centerY7 - finalYPosition6;

                            context7.beginPath();
                            context7.fillStyle = "black";
                            context7.arc(xoffset7, yoffset7, 5, 0, Math.PI * 2, true);
                            context7.stroke();
                            context7.fill();
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




        function bindTargets(ddlvalue) {
            var radCombo = $find('<%=ddlTargetID.ClientID %>');

            radCombo.clearItems();
            radCombo.clearSelection();
            radCombo.set_emptyMessage("-Select-");
            $.ajax({
                type: "POST",
                url: 'CreateGraph.aspx/GetTargetDetails',
                data: '{curvegrpID: "' + ddlvalue + '" }',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (data) {

                    var items = data.d;


                    for (var i = 0; i < items.length; i++) {
                        var item = items[i];

                        var comboItem = new Telerik.Web.UI.RadComboBoxItem();
                        comboItem.set_text(item.Text);
                        comboItem.set_value(item.Value);
                        radCombo.get_items().add(comboItem);
                    }
                }
            });
        }
        function OnClientSelectedIndexChanged(sender, eventArgs) {

            var item = eventArgs.get_item();
            var combo = $find('<%= ddlCurveGroup.ClientID %>');
            var selecteditem = combo.get_selectedItem().get_value();
            //ClearGraph();
            getEveryXCoord = 100;
            getEveryYCoord = 100;
            $.ajax({
                type: "POST",
                url: 'CreateGraph.aspx/GetGraphDetailsOnTargetID',
                data: '{targetID: "' + item.get_value() + '",curveID:"' + selecteditem + '" }',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (data) {
                    var displayval = data.d;
                    var splitval = displayval[0].shape;
                    $('#<%= txtTVD.ClientID %>').val(displayval[0].tvd);
                    if (splitval == "1000") {

                        var xWidth3;
                        var xPlusPlus3 = displayval[0].xcoordinate;

                        // calculation for position of target and grid values
                        var xPosition3 = displayval[0].xcoordinate;

                        if (xPosition3 < 0) {
                            xWidth3 = -500;
                            xPlusPlus3 = -displayval[0].xcoordinate + -displayval[0].xcoordinate;
                        }
                        else {
                            xWidth3 = 500;
                            xPlusPlus3 = displayval[0].xcoordinate + displayval[0].xcoordinate;
                        }

                        var xPositionPlusX3 = displayval[0].xcoordinate + displayval[0].xcoordinate;
                        var finalXPosition3 = xPosition3 / xPositionPlusX3 * xWidth3;


                        // x grid values 
                        var xGrid3 = xPlusPlus3;
                        xGrid3 = xGrid3 / 10;
                        //xGrid = Math.round(xGrid);
                        //xGrid.toFixed(2);

                        //y grid and target calculations
                        var yWidth3;
                        var yPlusPlus3 = displayval[0].ycoordinate;

                        // calculation for position of target and grid values
                        var yPosition3 = displayval[0].ycoordinate;

                        if (yPosition3 < 0) {
                            yWidth3 = -300;
                            yPlusPlus3 = -displayval[0].ycoordinate + -displayval[0].ycoordinate;
                        }
                        else {
                            yWidth3 = 300;
                            yPlusPlus3 = displayval[0].ycoordinate + displayval[0].ycoordinate;
                        }

                        var yPositionPlusY3 = displayval[0].ycoordinate + displayval[0].ycoordinate;
                        var finalYPosition3 = yPosition3 / yPositionPlusY3 * yWidth3;


                        // y grid values 
                        var yGrid3 = yPlusPlus3;
                        yGrid3 = yGrid3 / 10;


                        // clear the graph and make a new 1
                        var canvasCircle = document.getElementById('myCanvas');
                        var contextCircle = canvasCircle.getContext('2d');

                        contextCircle.clearRect(0, 0, canvasCircle.width, canvasCircle.height);

                        var myGraph = new Graph({
                            canvasId: 'myCanvas',
                            minX: -10,
                            minY: -10,
                            maxX: 10,
                            maxY: 10,
                            unitsPerTick: 1
                        }, xGrid3, yGrid3);


                        var canvas3 = document.getElementById('myCanvas');
                        var centerX3 = canvas3.width / 2;
                        var centerY3 = canvas3.height / 2;
                        var context3 = canvas3.getContext('2d');
                        var xoffset3 = centerX3 + finalXPosition3;
                        var yoffset3 = centerY3 - finalYPosition3;

                        context3.beginPath();
                        context3.arc(xoffset3, yoffset3, 10, 0, Math.PI * 2, true);
                        context3.stroke();

                    }
                    else if (splitval == "1001") {

                        var xWidth2;
                        var xPlusPlus2 = displayval[0].xcoordinate;

                        // calculation for position of target and grid values
                        var xPosition2 = displayval[0].xcoordinate;

                        if (xPosition2 < 0) {
                            xWidth2 = -500;
                            xPlusPlus2 = -displayval[0].xcoordinate + -displayval[0].xcoordinate;
                        }
                        else {
                            xWidth2 = 500;
                            xPlusPlus2 = displayval[0].xcoordinate + displayval[0].xcoordinate;
                        }

                        var xPositionPlusX2 = displayval[0].xcoordinate + displayval[0].xcoordinate;
                        var finalXPosition2 = xPosition2 / xPositionPlusX2 * xWidth2;
                        finalXPosition2 = finalXPosition2 - 30 / 2;


                        // x grid values 
                        var xGrid2 = xPlusPlus2;
                        xGrid2 = xGrid2 / 10;
                        //xGrid = Math.round(xGrid);
                        //xGrid.toFixed(2);

                        //y grid and target calculations
                        var yWidth2;
                        var yPlusPlus2 = displayval[0].ycoordinate;

                        // calculation for position of target and grid values
                        var yPosition2 = displayval[0].ycoordinate;

                        if (yPosition2 < 0) {
                            yWidth2 = -300;
                            yPlusPlus2 = -displayval[0].ycoordinate + -displayval[0].ycoordinate;
                        }
                        else {
                            yWidth2 = 300;
                            yPlusPlus2 = displayval[0].ycoordinate + displayval[0].ycoordinate;
                        }

                        var yPositionPlusY2 = displayval[0].ycoordinate + displayval[0].ycoordinate;
                        var finalYPosition2 = yPosition2 / yPositionPlusY2 * yWidth2;
                        finalYPosition2 = finalYPosition2 + 30 / 2;


                        // y grid values 
                        var yGrid2 = yPlusPlus2;
                        yGrid2 = yGrid2 / 10;


                        // clear the graph and make a new 1
                        var canvasSquare = document.getElementById('myCanvas');
                        var contextSquare = canvasSquare.getContext('2d');

                        contextSquare.clearRect(0, 0, canvasSquare.width, canvasSquare.height);

                        var myGraph = new Graph({
                            canvasId: 'myCanvas',
                            minX: -10,
                            minY: -10,
                            maxX: 10,
                            maxY: 10,
                            unitsPerTick: 1
                        }, xGrid2, yGrid2);



                        var canvas4 = document.getElementById('myCanvas');
                        var centerX4 = canvas4.width / 2;
                        var centerY4 = canvas4.height / 2;
                        var context4 = canvas4.getContext('2d');
                        var xoffset4 = centerX4 + finalXPosition2;
                        var yoffset4 = centerY4 - finalYPosition2;

                        context4.rect(xoffset4, yoffset4, 30, 30);
                        context4.lineWidth = 1;
                        context4.strokeStyle = 'black';
                        context4.stroke();

                    }
                    else if (splitval == "1002") {

                        var xWidth;
                        var xPlusPlus = displayval[0].xcoordinate;

                        // calculation for position of target and grid values
                        var xPosition = displayval[0].xcoordinate;

                        if (xPosition < 0) {
                            xWidth = -500;
                            xPlusPlus = -displayval[0].xcoordinate + -displayval[0].xcoordinate;
                        }
                        else {
                            xWidth = 500;
                            xPlusPlus = displayval[0].xcoordinate + displayval[0].xcoordinate;
                        }

                        var xPositionPlusX = displayval[0].xcoordinate + displayval[0].xcoordinate;
                        var finalXPosition = xPosition / xPositionPlusX * xWidth;
                        finalXPosition = finalXPosition - 30 / 2; // 30 is the width of the rectangle


                        // x grid values 
                        var xGrid = xPlusPlus;
                        xGrid = xGrid / 10;
                        //xGrid = Math.round(xGrid);
                        //xGrid.toFixed(2);

                        //y grid and target calculations
                        var yWidth;
                        var yPlusPlus = displayval[0].ycoordinate;

                        // calculation for position of target and grid values
                        var yPosition = displayval[0].ycoordinate;

                        if (yPosition < 0) {
                            yWidth = -300;
                            yPlusPlus = -displayval[0].ycoordinate + -displayval[0].ycoordinate;
                        }
                        else {
                            yWidth = 300;
                            yPlusPlus = displayval[0].ycoordinate + displayval[0].ycoordinate;
                        }

                        var yPositionPlusX = displayval[0].ycoordinate + displayval[0].ycoordinate;
                        var finalYPosition = yPosition / yPositionPlusX * yWidth;
                        finalYPosition = finalYPosition + 20 / 2; // 20 is the height of the rectangle


                        // y grid values 
                        var yGrid = yPlusPlus;
                        yGrid = yGrid / 10;



                        // clear the graph and make a new 1
                        var canvasRec = document.getElementById('myCanvas');
                        var contextRec = canvasRec.getContext('2d');

                        contextRec.clearRect(0, 0, canvasRec.width, canvasRec.height);

                        var myGraph = new Graph({
                            canvasId: 'myCanvas',
                            minX: -10,
                            minY: -10,
                            maxX: 10,
                            maxY: 10,
                            unitsPerTick: 1
                        }, xGrid, yGrid);

                        var canvas2 = document.getElementById('myCanvas');
                        var centerX2 = canvas2.width / 2;
                        var centerY2 = canvas2.height / 2;
                        var context2 = canvas2.getContext('2d');
                        var xoffset = centerX2 + finalXPosition;
                        var yoffset = centerY2 - finalYPosition;

                        context2.rect(xoffset, yoffset, 30, 20);
                        context2.lineWidth = 1;
                        context2.strokeStyle = 'black';
                        context2.stroke();

                    }

                    else if (splitval == "1003") {

                        var xWidth4;
                        var xPlusPlus4 = displayval[0].xcoordinate;

                        // calculation for position of target and grid values
                        var xPosition4 = displayval[0].xcoordinate;

                        if (xPosition4 < 0) {
                            xWidth4 = -500;
                            xPlusPlus4 = -displayval[0].xcoordinate + -displayval[0].xcoordinate;
                        }
                        else {
                            xWidth4 = 500;
                            xPlusPlus4 = displayval[0].xcoordinate + displayval[0].xcoordinate;
                        }

                        var xPositionPlusX4 = displayval[0].xcoordinate + displayval[0].xcoordinate;
                        var finalXPosition4 = xPosition4 / xPositionPlusX4 * xWidth4;


                        // x grid values 
                        var xGrid4 = xPlusPlus4;
                        xGrid4 = xGrid4 / 10;
                        //xGrid = Math.round(xGrid);
                        //xGrid.toFixed(2);

                        //y grid and target calculations
                        var yWidth4;
                        var yPlusPlus4 = displayval[0].ycoordinate;

                        // calculation for position of target and grid values
                        var yPosition4 = displayval[0].ycoordinate;

                        if (yPosition4 < 0) {
                            yWidth4 = -300;
                            yPlusPlus4 = -displayval[0].ycoordinate + -displayval[0].ycoordinate;
                        }
                        else {
                            yWidth4 = 300;
                            yPlusPlus4 = displayval[0].ycoordinate + displayval[0].ycoordinate;
                        }

                        var yPositionPlusY4 = displayval[0].ycoordinate + displayval[0].ycoordinate;
                        var finalYPosition4 = yPosition4 / yPositionPlusY4 * yWidth4;


                        // y grid values 
                        var yGrid4 = yPlusPlus4;
                        yGrid4 = yGrid4 / 10;


                        // clear the graph and make a new 1
                        var canvasPoly = document.getElementById('myCanvas');
                        var contextPoly = canvasPoly.getContext('2d');

                        contextPoly.clearRect(0, 0, canvasPoly.width, canvasPoly.height);

                        var myGraph = new Graph({
                            canvasId: 'myCanvas',
                            minX: -10,
                            minY: -10,
                            maxX: 10,
                            maxY: 10,
                            unitsPerTick: 1
                        }, xGrid4, yGrid4);

                        var canvas5 = document.getElementById('myCanvas');
                        var centerX5 = canvas5.width / 2;
                        var centerY5 = canvas5.height / 2;
                        var context5 = canvas5.getContext('2d');
                        var xoffset5 = centerX5 + finalXPosition4;
                        var yoffset5 = centerY5 - finalYPosition4;
                        var numberOfVertices = displayval[0].numberOfVertices;

                        var numberOfSides = numberOfVertices,
                            size = 20,
                            Xcenter = xoffset5,
                            Ycenter = yoffset5;

                        context5.beginPath();
                        context5.moveTo(Xcenter + size * Math.cos(0), Ycenter + size * Math.sin(0));

                        for (var i = 1; i <= numberOfSides; i += 1) {
                            context5.lineTo(Xcenter + size * Math.cos(i * 2 * Math.PI / numberOfSides), Ycenter + size * Math.sin(i * 2 * Math.PI / numberOfSides));
                        }

                        context5.strokeStyle = "black";
                        context5.lineWidth = 1;
                        context5.stroke();

                    }

                    else if (splitval == "1004") {

                        var xWidth5;
                        var xPlusPlus5 = displayval[0].xcoordinate;

                        // calculation for position of target and grid values
                        var xPosition5 = displayval[0].xcoordinate;

                        if (xPosition5 < 0) {
                            xWidth5 = -500;
                            xPlusPlus5 = -displayval[0].xcoordinate + -displayval[0].xcoordinate;
                        }
                        else {
                            xWidth5 = 500;
                            xPlusPlus5 = displayval[0].xcoordinate + displayval[0].xcoordinate;
                        }

                        var xPositionPlusX5 = displayval[0].xcoordinate + displayval[0].xcoordinate;
                        var finalXPosition5 = xPosition5 / xPositionPlusX5 * xWidth5;


                        // x grid values 
                        var xGrid5 = xPlusPlus5;
                        xGrid5 = xGrid5 / 10;
                        //xGrid = Math.round(xGrid);
                        //xGrid.toFixed(2);

                        //y grid and target calculations
                        var yWidth5;
                        var yPlusPlus5 = displayval[0].ycoordinate;

                        // calculation for position of target and grid values
                        var yPosition5 = displayval[0].ycoordinate;

                        if (yPosition5 < 0) {
                            yWidth5 = -300;
                            yPlusPlus5 = -displayval[0].ycoordinate + -displayval[0].ycoordinate;
                        }
                        else {
                            yWidth5 = 300;
                            yPlusPlus5 = displayval[0].ycoordinate + displayval[0].ycoordinate;
                        }

                        var yPositionPlusY5 = displayval[0].ycoordinate + displayval[0].ycoordinate;
                        var finalYPosition5 = yPosition5 / yPositionPlusY5 * yWidth5;


                        // y grid values 
                        var yGrid5 = yPlusPlus5;
                        yGrid5 = yGrid5 / 10;


                        // clear the graph and make a new 1
                        var canvasEllipse = document.getElementById('myCanvas');
                        var contextEllipse = canvasEllipse.getContext('2d');

                        contextEllipse.clearRect(0, 0, canvasEllipse.width, canvasEllipse.height);

                        var myGraph = new Graph({
                            canvasId: 'myCanvas',
                            minX: -10,
                            minY: -10,
                            maxX: 10,
                            maxY: 10,
                            unitsPerTick: 1
                        }, xGrid5, yGrid5);

                        var canvas6 = document.getElementById('myCanvas');
                        var centerX6 = canvas6.width / 2;
                        var centerY6 = canvas6.height / 2;
                        var context6 = canvas6.getContext('2d');
                        var xoffset6 = centerX6 + finalXPosition5;
                        var yoffset6 = centerY6 - finalYPosition5;

                        context6.beginPath();
                        context6.ellipse(xoffset6, yoffset6, 25, 50, 45, 0, 2 * Math.PI);
                        context6.strokeStyle = "black";
                        context6.lineWidth = 1;
                        context6.stroke();
                    }

                    else if (splitval == "1005") {

                        var xWidth6;
                        var xPlusPlus6 = displayval[0].xcoordinate;

                        // calculation for position of target and grid values
                        var xPosition6 = displayval[0].xcoordinate;

                        if (xPosition6 < 0) {
                            xWidth6 = -500;
                            xPlusPlus6 = -displayval[0].xcoordinate + -displayval[0].xcoordinate;
                        }
                        else {
                            xWidth6 = 500;
                            xPlusPlus6 = displayval[0].xcoordinate + displayval[0].xcoordinate;
                        }

                        var xPositionPlusX6 = displayval[0].xcoordinate + displayval[0].xcoordinate;
                        var finalXPosition6 = xPosition6 / xPositionPlusX6 * xWidth6;


                        // x grid values 
                        var xGrid6 = xPlusPlus6;
                        xGrid6 = xGrid6 / 10;
                        //xGrid = Math.round(xGrid);
                        //xGrid.toFixed(2);

                        //y grid and target calculations
                        var yWidth6;
                        var yPlusPlus6 = displayval[0].ycoordinate;

                        // calculation for position of target and grid values
                        var yPosition6 = displayval[0].ycoordinate;

                        if (yPosition6 < 0) {
                            yWidth6 = -300;
                            yPlusPlus6 = -displayval[0].ycoordinate + -displayval[0].ycoordinate;
                        }
                        else {
                            yWidth6 = 300;
                            yPlusPlus6 = displayval[0].ycoordinate + displayval[0].ycoordinate;
                        }

                        var yPositionPlusY6 = displayval[0].ycoordinate + displayval[0].ycoordinate;
                        var finalYPosition6 = yPosition6 / yPositionPlusY6 * yWidth6;


                        // y grid values 
                        var yGrid6 = yPlusPlus6;
                        yGrid6 = yGrid6 / 10;


                        // clear the graph and make a new 1
                        var canvasPoint = document.getElementById('myCanvas');
                        var contextPoint = canvasPoint.getContext('2d');

                        contextPoint.clearRect(0, 0, canvasPoint.width, canvasPoint.height);

                        var myGraph = new Graph({
                            canvasId: 'myCanvas',
                            minX: -10,
                            minY: -10,
                            maxX: 10,
                            maxY: 10,
                            unitsPerTick: 1
                        }, xGrid6, yGrid6);

                        var canvas7 = document.getElementById('myCanvas');
                        var centerX7 = canvas7.width / 2;
                        var centerY7 = canvas7.height / 2;
                        var context7 = canvas7.getContext('2d');
                        var xoffset7 = centerX7 + finalXPosition6;
                        var yoffset7 = centerY7 - finalYPosition6;

                        context7.beginPath();
                        context7.fillStyle = "black";
                        context7.arc(xoffset7, yoffset7, 5, 0, Math.PI * 2, true);
                        context7.stroke();
                        context7.fill();
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

    </script>

    <fieldset>


        <asp:UpdatePanel runat="server" ID="updPnl1" UpdateMode="Conditional">
            <ContentTemplate>

                <asp:Table ID="MainPage" runat="server" HorizontalAlign="Center" Width="100%">

                    <asp:TableRow>
                        <asp:TableCell ColumnSpan="7" HorizontalAlign="Center">
                            <h2>Create Graph</h2>
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell>&nbsp;</asp:TableCell>
                    </asp:TableRow>


                </asp:Table>

                <asp:Table ID="Table6" runat="server" HorizontalAlign="Center" Width="100%">


                    <asp:TableRow>
                        <asp:TableCell CssClass="FormCenter" ColumnSpan="3">
                            <asp:Table ID="Table7" runat="server" HorizontalAlign="Center">


                                <asp:TableRow>

                                    <asp:TableHeaderCell CssClass="HeaderCenter">
						Curve Group ID
                                    </asp:TableHeaderCell>

                                    <asp:TableHeaderCell CssClass="HeaderCenter">
						Target ID
                                    </asp:TableHeaderCell>

                                </asp:TableRow>


                                <asp:TableRow>

                                    <asp:TableCell>

                                        <telerik:RadDropDownList runat="server" ID="ddlCurveGroup" Width="160px" AppendDataBoundItems="true" DropDownWidth="300px" DropDownHeight="200px"
                                            OnClientItemSelected="OnClientItemSelected">
                                            <Items>
                                                <telerik:DropDownListItem Value="0" Text="-Select-" />
                                            </Items>
                                        </telerik:RadDropDownList>
                                    </asp:TableCell>


                                    <asp:TableCell>
                                        <telerik:RadComboBox ID="ddlTargetID" runat="server"
                                            DataTextField="ID" DataValueField="ID"
                                            AppendDataBoundItems="true" OnClientSelectedIndexChanged="OnClientSelectedIndexChanged">
                                            <Items>
                                                <telerik:RadComboBoxItem Value="0" Text="-Select-" />
                                            </Items>
                                        </telerik:RadComboBox>

                                    </asp:TableCell>


                                </asp:TableRow>

                            </asp:Table>
                        </asp:TableCell>
                    </asp:TableRow>

                </asp:Table>




                <asp:Table ID="Table1" runat="server" HorizontalAlign="Center" Width="100%">
                    <asp:TableRow>

                        <asp:TableCell CssClass="FormCenter" ColumnSpan="3">
                            <asp:Table ID="Table4" runat="server" HorizontalAlign="Center" BackColor="#F1F1F1" BorderStyle="Double" BorderColor="#3A4F63">

                                <asp:TableRow>

                                    <asp:TableHeaderCell CssClass="HeaderCenter">
						MD
                                    </asp:TableHeaderCell>

                                </asp:TableRow>


                                <asp:TableRow>
                                    <asp:TableCell>
                                        <telerik:RadTextBox runat="server" ID="txtMD" Width="160px" />
                                    </asp:TableCell>
                                </asp:TableRow>



                                <asp:TableRow>

                                    <asp:TableHeaderCell CssClass="HeaderCenter">
						INC
                                    </asp:TableHeaderCell>

                                </asp:TableRow>


                                <asp:TableRow>
                                    <asp:TableCell>
                                        <telerik:RadTextBox runat="server" ID="TxtINC" Width="160px" />
                                    </asp:TableCell>
                                </asp:TableRow>


                                <asp:TableRow>

                                    <asp:TableHeaderCell CssClass="HeaderCenter">
						Azimuth
                                    </asp:TableHeaderCell>

                                </asp:TableRow>


                                <asp:TableRow>
                                    <asp:TableCell>
                                        <telerik:RadTextBox runat="server" ID="txtAzimuth" Width="160px" />
                                    </asp:TableCell>
                                </asp:TableRow>


                                <asp:TableRow>

                                    <asp:TableHeaderCell CssClass="HeaderCenter">
						TVD
                                    </asp:TableHeaderCell>

                                </asp:TableRow>


                                <asp:TableRow>
                                    <asp:TableCell>
                                        <telerik:RadTextBox runat="server" ClientIDMode="Static" ID="txtTVD" Width="160px" />
                                    </asp:TableCell>
                                </asp:TableRow>



                                <asp:TableRow>

                                    <asp:TableHeaderCell CssClass="HeaderCenter">
						E-W
                                    </asp:TableHeaderCell>

                                </asp:TableRow>


                                <asp:TableRow>
                                    <asp:TableCell>
                                        <telerik:RadTextBox runat="server" ID="txtEW" Width="160px" />
                                    </asp:TableCell>
                                </asp:TableRow>

                                <asp:TableRow>

                                    <asp:TableHeaderCell CssClass="HeaderCenter">
						N-S
                                    </asp:TableHeaderCell>

                                </asp:TableRow>


                                <asp:TableRow>
                                    <asp:TableCell>
                                        <telerik:RadTextBox runat="server" ID="txtNS" Width="160px" />
                                    </asp:TableCell>
                                </asp:TableRow>


                                <asp:TableRow>

                                    <asp:TableHeaderCell CssClass="HeaderCenter">
						Build Rate
                                    </asp:TableHeaderCell>

                                </asp:TableRow>


                                <asp:TableRow>
                                    <asp:TableCell>
                                        <telerik:RadTextBox runat="server" ID="txtBuildRate" Width="160px" />
                                    </asp:TableCell>
                                </asp:TableRow>



                                <asp:TableRow>

                                    <asp:TableHeaderCell CssClass="HeaderCenter">
						Walk Rate
                                    </asp:TableHeaderCell>

                                </asp:TableRow>


                                <asp:TableRow>
                                    <asp:TableCell>
                                        <telerik:RadTextBox runat="server" ID="txtWalkRate" Width="160px" />
                                    </asp:TableCell>
                                </asp:TableRow>


                                <asp:TableRow>

                                    <asp:TableHeaderCell CssClass="HeaderCenter">
						DLS
                                    </asp:TableHeaderCell>

                                </asp:TableRow>


                                <asp:TableRow>
                                    <asp:TableCell>
                                        <telerik:RadTextBox runat="server" ClientIDMode="Static" ID="txtDLS" Width="160px" />
                                    </asp:TableCell>
                                </asp:TableRow>

                                <asp:TableRow>

                                    <asp:TableHeaderCell CssClass="HeaderCenter">
						Hold Len
                                    </asp:TableHeaderCell>

                                </asp:TableRow>


                                <asp:TableRow>
                                    <asp:TableCell>
                                        <telerik:RadTextBox runat="server" ID="txtHoldLen" Width="160px" />
                                    </asp:TableCell>
                                </asp:TableRow>


                                <asp:TableRow>

                                    <asp:TableHeaderCell CssClass="HeaderCenter">
						X Postion 
                                    </asp:TableHeaderCell>



                                </asp:TableRow>

                                <asp:TableRow>
                                    <asp:TableCell>
                                        <telerik:RadTextBox runat="server" ID="TxtMouseXPositon" Width="160px" />
                                    </asp:TableCell>
                                </asp:TableRow>

                                <asp:TableRow>

                                    <asp:TableHeaderCell CssClass="HeaderCenter">
						Y Position 
                                    </asp:TableHeaderCell>
                                </asp:TableRow>

                                <asp:TableRow>
                                    <asp:TableCell>
                                        <telerik:RadTextBox runat="server" ID="TxtMouseYPosition" Width="160px" />
                                    </asp:TableCell>
                                </asp:TableRow>


                                <asp:TableRow>

                                    <asp:TableHeaderCell CssClass="HeaderCenter">
						Spacing
                                    </asp:TableHeaderCell>

                                </asp:TableRow>


                                <asp:TableRow>
                                    <asp:TableCell>
                                        <telerik:RadDropDownList runat="server" ID="ddlSpacing" Width="160px" AppendDataBoundItems="true">
                                            <Items>
                                                <telerik:DropDownListItem Value="0" Text="-Select-" />
                                            </Items>
                                        </telerik:RadDropDownList>
                                    </asp:TableCell>
                                </asp:TableRow>

                            </asp:Table>

                        </asp:TableCell>


                        <asp:TableCell>
                                <canvas id="myCanvas" width="1000px" height="600px" style="border:1px solid #010A15;" />
                        </asp:TableCell>

                    </asp:TableRow>


                </asp:Table>

                <asp:Table ID="Table2" runat="server" HorizontalAlign="Center" Width="100%">

                    <asp:TableRow>

                        <asp:TableCell Width="20%"></asp:TableCell>

                        <asp:TableCell>
                            <asp:Button ID="btnZoomIn" runat="server" CssClass="button-graphZoomOut" OnClientClick="zoomIn(); return false;" />
                        </asp:TableCell>

                        <asp:TableCell>
                            <asp:Button ID="btnZoomOut" runat="server" CssClass="button-graphZoomIn" OnClientClick="zoomOut(); return false;" />
                        </asp:TableCell>


                        <asp:TableCell>
                            <asp:Label ID="Label1" runat="server" Text="DLS" />
                        </asp:TableCell>

                        <asp:TableCell>
                            <asp:Button ID="BtnDLSUp" runat="server" Text="^" Width="5px" OnClientClick="DLSup(); return false;" />
                        </asp:TableCell>

                        <asp:TableCell>
                            <asp:Button ID="btnDLSDown" runat="server" Text="v" Width="5px" OnClientClick="DLSdown(); return false;" />
                        </asp:TableCell>


                        <asp:TableCell>
                            <telerik:RadTextBox runat="server" ClientIDMode="Static" ID="txtDLSUpAndDown" Width="160px" />
                        </asp:TableCell>



                        <asp:TableCell>
                            <asp:Label ID="Label2" runat="server" Text="Project PT" />
                        </asp:TableCell>

                        <asp:TableCell>
                            <telerik:RadDropDownList runat="server" ID="ddlProjectionPT" Width="160px" AppendDataBoundItems="true">
                                <Items>
                                    <telerik:DropDownListItem Value="0" Text="-Select-" />
                                </Items>
                            </telerik:RadDropDownList>
                        </asp:TableCell>


                        <asp:TableCell>
                            <asp:Button ID="btnClose" runat="server" OnClientClick="Close();" Text="Close" Visible="false" />
                        </asp:TableCell>

                        <asp:TableCell Width="30%"></asp:TableCell>


                    </asp:TableRow>



                </asp:Table>


                   <asp:Table ID="Table3" runat="server" HorizontalAlign="Center" Width="100%">

                    <asp:TableRow>

                        <asp:TableCell Width="20%"></asp:TableCell>

                        <asp:TableCell>
                            <asp:CheckBox ID="cbCurrentTrend" runat="server" />
                        </asp:TableCell>

                           <asp:TableCell>
                            <asp:Label ID="lblShowCurves" runat="server" Text="Current Trend" />
                        </asp:TableCell>


                        
                        <asp:TableCell>
                            <asp:CheckBox ID="cbProposalCurve" runat="server" />
                        </asp:TableCell>

                           <asp:TableCell>
                            <asp:Label ID="Label3" runat="server" Text="Proposal Curve" />
                        </asp:TableCell>


                              
                        <asp:TableCell>
                            <asp:CheckBox ID="cbStraightLineTVD" runat="server" />
                        </asp:TableCell>

                           <asp:TableCell>
                            <asp:Label ID="Label4" runat="server" Text="StraightLineTVD" />
                        </asp:TableCell>

                         <asp:TableCell>
                            <asp:CheckBox ID="cbMinDLSCurve" runat="server" />
                        </asp:TableCell>

                           <asp:TableCell>
                            <asp:Label ID="Label5" runat="server" Text="Min DLS Curve" />
                        </asp:TableCell>

                          <asp:TableCell>
                            <asp:CheckBox ID="cbBuildAndWalk" runat="server" />
                        </asp:TableCell>

                           <asp:TableCell>
                            <asp:Label ID="Label6" runat="server" Text="Build And Walk" />
                        </asp:TableCell>

                         <asp:TableCell>
                            <asp:CheckBox ID="cbDLSCurve" runat="server" />
                        </asp:TableCell>

                           <asp:TableCell>
                            <asp:Label ID="Label7" runat="server" Text="DLS Curve" />
                        </asp:TableCell>

                         <asp:TableCell>
                           <asp:Button ID="btnShowVertical" runat="server" Text="Show Vertical" />
                        </asp:TableCell>

                          


                        <asp:TableCell Width="30%"></asp:TableCell>


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
        function Graph(config, xGrid, yGrid) {
            // user defined properties
            this.canvas = document.getElementById(config.canvasId);
            this.minX = config.minX;
            this.minY = config.minY;
            this.maxX = config.maxX;
            this.maxY = config.maxY;
            this.unitsPerTick = config.unitsPerTick;


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
            this.centerY = Math.round(Math.abs(this.minY / this.rangeY) * this.canvas.height);
            this.centerX = Math.round(Math.abs(this.minX / this.rangeX) * this.canvas.width);
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
                yGrid = 100;
            }
            else {
                yGrid = yGrid;
            }

            // draw x and y axis
            this.drawXAxis(xGrid);
            this.drawYAxis(yGrid);

        }


        Graph.prototype.drawXAxis = function (xGrid) {
            var context = this.context;
            context.save();
            context.beginPath();
            context.moveTo(0, this.centerY);
            context.lineTo(this.canvas.width, this.centerY);
            context.strokeStyle = this.axisColor;
            context.lineWidth = 2;
            context.stroke();

            // draw tick marks
            var xPosIncrement = this.unitsPerTick * this.unitX;
            var xPos;
            context.font = this.font;
            context.textAlign = 'center';
            context.textBaseline = 'top';

            // draw left tick marks
            xPos = this.centerX - xPosIncrement;
            unit = -xGrid;   //-1 * this.unitsPerTick;
            while (xPos > 0) {
                context.moveTo(xPos, this.centerY - this.tickSize / 2);
                context.lineTo(xPos, this.centerY + this.tickSize / 2);
                context.stroke();
                context.fillText(unit, xPos, this.centerY + this.tickSize / 2 + 3);
                unit -= xGrid;  // this.unitsPerTick;
                unit = Math.round(unit * 100) / 100;
                xPos = Math.round(xPos - xPosIncrement);
            }

            // draw right tick marks
            xPos = this.centerX + xPosIncrement;
            unit = xGrid;  //this.unitsPerTick;
            while (xPos < this.canvas.width) {
                context.moveTo(xPos, this.centerY - this.tickSize / 2);
                context.lineTo(xPos, this.centerY + this.tickSize / 2);
                context.stroke();
                context.fillText(unit, xPos, this.centerY + this.tickSize / 2 + 3);
                unit += xGrid; // this.unitsPerTick;
                unit = Math.round(unit * 100) / 100;
                xPos = Math.round(xPos + xPosIncrement);
            }
            context.restore();
        };

        Graph.prototype.drawYAxis = function (yGrid) {
            var context = this.context;
            context.save();
            context.beginPath();
            context.moveTo(this.centerX, 0);
            context.lineTo(this.centerX, this.canvas.height);
            context.strokeStyle = this.axisColor;
            context.lineWidth = 2;
            context.stroke();

            // draw tick marks
            var yPosIncrement = this.unitsPerTick * this.unitY;
            var yPos, unit;
            context.font = this.font;
            context.textAlign = 'right';
            context.textBaseline = 'middle';

            // draw top tick marks
            yPos = this.centerY - yPosIncrement;
            unit = yGrid; //this.unitsPerTick;
            while (yPos > 0) {
                context.moveTo(this.centerX - this.tickSize / 2, yPos);
                context.lineTo(this.centerX + this.tickSize / 2, yPos);
                context.stroke();
                context.fillText(unit, this.centerX - this.tickSize / 2 - 3, yPos);
                unit += yGrid; //this.unitsPerTick;
                unit = Math.round(unit * 100) / 100;
                yPos = Math.round(yPos - yPosIncrement);
            }

            // draw bottom tick marks
            yPos = this.centerY + yPosIncrement;
            unit = -yGrid; //-1 * this.unitsPerTick;
            while (yPos < this.canvas.height) {
                context.moveTo(this.centerX - this.tickSize / 2, yPos);
                context.lineTo(this.centerX + this.tickSize / 2, yPos);
                context.stroke();
                context.fillText(unit, this.centerX - this.tickSize / 2 - 3, yPos);
                unit -= yGrid; //this.unitsPerTick;
                unit = Math.round(unit * 100) / 100;
                yPos = Math.round(yPos + yPosIncrement);
            }
            context.restore();
        };

        Graph.prototype.drawEquation = function (equation, color, thickness) {
            var context = this.context;
            context.save();
            context.save();
            this.transformContext();

            context.beginPath();
            context.moveTo(this.minX, equation(this.minX));

            for (var x = this.minX + this.iteration; x <= this.maxX; x += this.iteration) {
                context.lineTo(x, equation(x));
            }

            context.restore();
            context.lineJoin = 'round';
            context.lineWidth = thickness;
            context.strokeStyle = color;
            context.stroke();
            context.restore();
        };

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



    </script>

    <%-- // X and Y Position over canvas hover --%>

    <script type="text/javascript">
        $(document).ready(function () {

            $("#myCanvas").mousemove(function (e) {

                //var offset = $(this).offset();
                var relativeX = (e.offsetX - 500.0);  //  offset.left
                var relativeY = (-e.offsetY + 300);   //  offset.top


                // Matt these calculation will be help full
                // work curve x position 
                //var xPositionWorkCurve = -1050;
                //var xPositionWorkCurveGrid = getEveryXCoord2;
                //var finalXWorkCurve = xPositionWorkCurve / xPositionWorkCurveGrid * xWidth3;


                //// work curve y position 
                //var yPositionWorkCurve = -1050;
                //var yPositionWorkCurveGrid = getEveryYCoord2;
                //var finalYWorkCurve = yPositionWorkCurve / yPositionWorkCurveGrid * yWidth3;


                $('#<%= TxtMouseXPositon.ClientID %>').val(relativeX);
                $('#<%= TxtMouseYPosition.ClientID %>').val(relativeY);
                $('#<%= txtEW.ClientID %>').val(relativeX);
                $('#<%= txtNS.ClientID %>').val(relativeY);



            });

        });



    </script>


    <%-- /// DLS UP And Down Buttons--%>

    <script type="text/javascript">
        var getEveryXCoord = 100;
        var getEveryYCoord = 100;


        function zoomIn() {


            // Jd Loop Through Every X Coord of every Target To Set The Grid Numbers
            getEveryXCoord = getEveryXCoord + 100;
            getEveryYCoord = getEveryYCoord + 100;


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
            }, getEveryXCoord, getEveryYCoord);


            var raddropdownlist = $find('<%=ddlCurveGroup.ClientID %>');
            var CurveGroupID = raddropdownlist.get_selectedItem().get_value();
            if (CurveGroupID != 0) {
                ItemSelectedFromServer2(CurveGroupID, getEveryXCoord, getEveryYCoord);
            }

            return getEveryXCoord, getEveryYCoord;

        }

        function zoomOut() {
            if (getEveryXCoord != 100) {

                // Jd Loop Through Every X Coord of every Target To Set The Grid Numbers
                getEveryXCoord = getEveryXCoord - 100;
                getEveryYCoord = getEveryYCoord - 100;


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
                }, getEveryXCoord, getEveryYCoord);


                var raddropdownlist = $find('<%=ddlCurveGroup.ClientID %>');
                var CurveGroupID = raddropdownlist.get_selectedItem().get_value();
                if (CurveGroupID != 0) {
                    ItemSelectedFromServer2(CurveGroupID, getEveryXCoord, getEveryYCoord);
                }

                return getEveryXCoord, getEveryYCoord;
            }

        }

        var ddUp = 1.0;
        function DLSup() {

            ddUp = ddUp + 0.1;

            ddUp = Math.round(ddUp * 100) / 100;
            document.getElementById("txtDLSUpAndDown").value = ddUp;
            document.getElementById("txtDLS").value = ddUp;

            return ddUp;

        }

        function DLSdown() {

            ddUp = ddUp - 0.1;

            ddUp = Math.round(ddUp * 100) / 100;
            document.getElementById("txtDLSUpAndDown").value = ddUp;
            document.getElementById("txtDLS").value = ddUp;

            return ddUp;

        }
    </script>


    <script type="text/javascript">

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
