function DrawSurveyCurves(displayval, i, prev) {
    var xWidth3;
    var getEveryXCoord2;
    var getEveryYCoord2;

    var xPosition3 = displayval[i].VerticalSection;
    var xPosition4 = displayval[prev].VerticalSection;
    var surveyComment = displayval[i].SurveyComment;

    if (xPosition3 < 0) {
        xWidth3 = -400;
        getEveryXCoord2 = -100 * 10;
    }
    else {
        xWidth3 = 400;
        getEveryXCoord2 = 100 * 10;
    }

    var xPositionPlusX3 = getEveryXCoord2;
    var finalXPosition3 = xPosition3 / xPositionPlusX3 * xWidth3;// final x position value for the point
    var finalXPosition4 = xPosition4 / xPositionPlusX3 * xWidth3;// final x position value for the point


    //TVD y placement on graph // 575 is y 0 position
    var yWidth3;
    var yPosition3;
    if (displayval[i].TVD != 0) {
        yPosition3 = -displayval[i].TVD
    }

    else {
        yPosition3 = displayval[i].TVD
    }

    var yPosition4 = -displayval[prev].TVD;


    if (yPosition3 < 0) {
        yWidth3 = -300;
        getEveryYCoord2 = -500 * 10; // 236.95 * 14;
    }

    else {
        yWidth3 = 300;
        getEveryYCoord2 = 500 * 10; // 236.95 * 14;
    }


    var yPositionPlusY3 = getEveryYCoord2;
    var finalYPosition3 = yPosition3 / yPositionPlusY3 * yWidth3;// final y position value for the point
    var finalYPosition4 = yPosition4 / yPositionPlusY3 * yWidth3;// final y position value for the point


    var canvas1 = document.getElementById('myCanvas');
    var centerX1 = canvas1.width / 2;
    var centerY1 = canvas1.height / 2; // or - 25   / 24 or  1.04347826

    var context1 = canvas1.getContext('2d');
    var xoffset1 = centerX1 + finalXPosition3;
    var yoffset1 = centerY1 - finalYPosition3;

    var xoffset2 = centerX1 + finalXPosition4;
    var yoffset2 = centerY1 - finalYPosition4;

    context1.beginPath();
    context1.font = 'italic 8pt Calibri';
    context1.fillText(surveyComment, xoffset1, yoffset1);
    context1.arc(xoffset1, yoffset1, 2, 0, Math.PI * 2, true);
    context1.stroke();


    if (i != 0 && displayval[i].RowNumber != 0) {
        // draw line to survey circles
        var canvas_line1 = document.getElementById('myCanvas');

        var ctx1 = canvas_line1.getContext("2d")
        ctx1.lineWidth = 1;
        ctx1.strokeStyle = "#333";
        ctx1.beginPath();
        ctx1.moveTo(xoffset2, yoffset2);
        ctx1.lineTo(xoffset1, yoffset1);
        ctx1.stroke();
    }

}

function DrawSurveyCurvesZoom(displayval, i, prev, getEveryXCoord, getEveryYCoord) {

    var xWidth3;
    var getEveryXCoord2;
    var getEveryYCoord2;

    var xPosition3 = displayval[i].VerticalSection;
    var xPosition4 = displayval[prev].VerticalSection;
    var surveyComment = displayval[i].SurveyComment;

    if (xPosition3 < 0) {
        xWidth3 = -400;
        getEveryXCoord2 = -getEveryXCoord * 10;
    }
    else {
        xWidth3 = 400;
        getEveryXCoord2 = getEveryXCoord * 10;
    }

    var xPositionPlusX3 = getEveryXCoord2;
    var finalXPosition3 = xPosition3 / xPositionPlusX3 * xWidth3;// final x position value for the point
    var finalXPosition4 = xPosition4 / xPositionPlusX3 * xWidth3;// final x position value for the point


    //TVD y placement on graph // 575 is y 0 position
    var yWidth3;
    var yPosition3;
    if (displayval[i].TVD != 0) {
        yPosition3 = -displayval[i].TVD
    }

    else {
        yPosition3 = displayval[i].TVD
    }

    var yPosition4 = -displayval[prev].TVD;


    if (yPosition3 < 0) {
        yWidth3 = -300;
        getEveryYCoord2 = -getEveryYCoord * 10; // 236.95 * 14;
    }

    else {
        yWidth3 = 300;
        getEveryYCoord2 = getEveryYCoord * 10; // 236.95 * 14;
    }


    var yPositionPlusY3 = getEveryYCoord2;
    var finalYPosition3 = yPosition3 / yPositionPlusY3 * yWidth3;// final y position value for the point
    var finalYPosition4 = yPosition4 / yPositionPlusY3 * yWidth3;// final y position value for the point


    var canvas1 = document.getElementById('myCanvas');
    var centerX1 = canvas1.width / 2;
    var centerY1 = canvas1.height / 2; // or - 25   / 24 or  1.04347826

    var context1 = canvas1.getContext('2d');
    var xoffset1 = centerX1 + finalXPosition3;
    var yoffset1 = centerY1 - finalYPosition3;

    var xoffset2 = centerX1 + finalXPosition4;
    var yoffset2 = centerY1 - finalYPosition4;

    context1.beginPath();
    context1.font = 'italic 8pt Calibri';
    context1.fillText(surveyComment, xoffset1, yoffset1);
    context1.arc(xoffset1, yoffset1, 2, 0, Math.PI * 2, true);
    context1.stroke();


    if (i != 0 && displayval[i].RowNumber != 0) {
        // draw line to survey circles
        var canvas_line1 = document.getElementById('myCanvas');

        var ctx1 = canvas_line1.getContext("2d")
        ctx1.lineWidth = 1;
        ctx1.strokeStyle = "#333";
        ctx1.beginPath();
        ctx1.moveTo(xoffset2, yoffset2);
        ctx1.lineTo(xoffset1, yoffset1);
        ctx1.stroke();
    }

}

function DrawSurveyCurvesClick(displayval, i, prev, getEveryXCoord, getEveryYCoord, relativeX, relativeY) {

    var xWidth3;
    var getEveryXCoord2;
    var getEveryYCoord2;

    var xPosition3 = displayval[i].VerticalSection;
    var xPosition4 = displayval[prev].VerticalSection;
    var surveyComment = displayval[i].SurveyComment;

    if (xPosition3 < 0) {
        xWidth3 = -400;
        getEveryXCoord2 = -getEveryXCoord * 10;
    }
    else {
        xWidth3 = 400;
        getEveryXCoord2 = getEveryXCoord * 10;
    }

    var xPositionPlusX3 = getEveryXCoord2;
    var finalXPosition3 = xPosition3 / xPositionPlusX3 * xWidth3;// final x position value for the point
    var finalXPosition4 = xPosition4 / xPositionPlusX3 * xWidth3;// final x position value for the point


    //TVD y placement on graph // 575 is y 0 position
    var yWidth3;
    var yPosition3;
    if (displayval[i].TVD != 0) {
        yPosition3 = -displayval[i].TVD
    }

    else {
        yPosition3 = displayval[i].TVD
    }

    var yPosition4 = -displayval[prev].TVD;


    if (yPosition3 < 0) {
        yWidth3 = -300;
        getEveryYCoord2 = -getEveryYCoord * 10; // 236.95 * 14;
    }

    else {
        yWidth3 = 300;
        getEveryYCoord2 = getEveryYCoord * 10; // 236.95 * 14;
    }


    var yPositionPlusY3 = getEveryYCoord2;
    var finalYPosition3 = yPosition3 / yPositionPlusY3 * yWidth3;// final y position value for the point
    var finalYPosition4 = yPosition4 / yPositionPlusY3 * yWidth3;// final y position value for the point


    var canvas1 = document.getElementById('myCanvas');
    var centerX1 = relativeX;//canvas1.width / 2;
    var centerY1 = relativeY;//canvas1.height / 2; // or - 25   / 24 or  1.04347826

    var context1 = canvas1.getContext('2d');
    var xoffset1 = centerX1 + finalXPosition3;
    var yoffset1 = centerY1 - finalYPosition3;

    var xoffset2 = centerX1 + finalXPosition4;
    var yoffset2 = centerY1 - finalYPosition4;

    context1.beginPath();
    context1.font = 'italic 8pt Calibri';
    context1.fillText(surveyComment, xoffset1, yoffset1);
    context1.arc(xoffset1, yoffset1, 2, 0, Math.PI * 2, true);
    context1.stroke();


    if (i != 0 && displayval[i].RowNumber != 0) {
        // draw line to survey circles
        var canvas_line1 = document.getElementById('myCanvas');

        var ctx1 = canvas_line1.getContext("2d")
        ctx1.lineWidth = 1;
        ctx1.strokeStyle = "#333";
        ctx1.beginPath();
        ctx1.moveTo(xoffset2, yoffset2);
        ctx1.lineTo(xoffset1, yoffset1);
        ctx1.stroke();
    }

}

function DrawSmallGraphSurvey(displayvalSurvey, s, prev) {

    var xWidth3;
    var getEveryXCoord2;
    var getEveryYCoord2;

    var xPosition3 = displayvalSurvey[s].EW;
    var xPosition4 = displayvalSurvey[prev].EW;


    if (xPosition3 < 0) {
        xWidth3 = -200;
        getEveryXCoord2 = -50 * 10;

    }
    else {
        xWidth3 = 200;
        getEveryXCoord2 = 50 * 10;

    }

    var xPositionPlusX3 = getEveryXCoord2;
    var finalXPosition3 = xPosition3 / xPositionPlusX3 * xWidth3;
    var finalXPosition4 = xPosition4 / xPositionPlusX3 * xWidth3;// final x position value for the point

    //y grid and target calculations
    var yWidth3;


    var yPosition3 = displayvalSurvey[s].NS;
    var yPosition4 = displayvalSurvey[prev].NS;


    if (yPosition3 < 0) {
        yWidth3 = -200;
        getEveryYCoord2 = -50 * 10;
    }
    else {
        yWidth3 = 200;
        getEveryYCoord2 = 50 * 10;
    }

    var yPositionPlusY3 = getEveryYCoord2;
    var finalYPosition3 = yPosition3 / yPositionPlusY3 * yWidth3;
    var finalYPosition4 = yPosition4 / yPositionPlusY3 * yWidth3;// final y position value for the point

    var canvas1 = document.getElementById('myCanvas2');
    var centerX1 = canvas1.width / 2;
    var centerY1 = canvas1.height / 2;

    var xoffset1 = centerX1 + finalXPosition3;
    var yoffset1 = centerY1 - finalYPosition3;

    var xoffset2 = centerX1 + finalXPosition4;
    var yoffset2 = centerY1 - finalYPosition4;



    if (displayvalSurvey[s].RowNumber != 0) {
        // draw line to survey circles
        var canvas_line1 = document.getElementById('myCanvas2');

        var ctx1 = canvas_line1.getContext("2d")
        ctx1.lineWidth = 1;
        ctx1.strokeStyle = "#333";
        ctx1.beginPath();
        ctx1.moveTo(xoffset2, yoffset2);
        ctx1.lineTo(xoffset1, yoffset1);
        ctx1.stroke();
    }
}

function DrawSmallGraphSurveyZoom(displayvalSurvey, s, prev, getEveryXCoordSGraph, getEveryYCoordSGraph) {

    var xWidth3;
    var getEveryXCoord2;
    var getEveryYCoord2;

    var xPosition3 = displayvalSurvey[s].EW;
    var xPosition4 = displayvalSurvey[prev].EW;


    if (xPosition3 < 0) {
        xWidth3 = -200;
        getEveryXCoord2 = -getEveryXCoordSGraph * 10;

    }
    else {
        xWidth3 = 200;
        getEveryXCoord2 = getEveryXCoordSGraph * 10;

    }

    var xPositionPlusX3 = getEveryXCoord2;
    var finalXPosition3 = xPosition3 / xPositionPlusX3 * xWidth3;
    var finalXPosition4 = xPosition4 / xPositionPlusX3 * xWidth3;// final x position value for the point

    //y grid and target calculations
    var yWidth3;


    var yPosition3 = displayvalSurvey[s].NS;
    var yPosition4 = displayvalSurvey[prev].NS;


    if (yPosition3 < 0) {
        yWidth3 = -200;
        getEveryYCoord2 = -getEveryYCoordSGraph * 10;
    }
    else {
        yWidth3 = 200;
        getEveryYCoord2 = getEveryYCoordSGraph * 10;
    }

    var yPositionPlusY3 = getEveryYCoord2;
    var finalYPosition3 = yPosition3 / yPositionPlusY3 * yWidth3;
    var finalYPosition4 = yPosition4 / yPositionPlusY3 * yWidth3;// final y position value for the point

    var canvas1 = document.getElementById('myCanvas2');
    var centerX1 = canvas1.width / 2;
    var centerY1 = canvas1.height / 2;

    var xoffset1 = centerX1 + finalXPosition3;
    var yoffset1 = centerY1 - finalYPosition3;

    var xoffset2 = centerX1 + finalXPosition4;
    var yoffset2 = centerY1 - finalYPosition4;



    if (displayvalSurvey[s].RowNumber != 0) {
        // draw line to survey circles
        var canvas_line1 = document.getElementById('myCanvas2');

        var ctx1 = canvas_line1.getContext("2d")
        ctx1.lineWidth = 1;
        ctx1.strokeStyle = "#333";
        ctx1.beginPath();
        ctx1.moveTo(xoffset2, yoffset2);
        ctx1.lineTo(xoffset1, yoffset1);
        ctx1.stroke();
    }


}

function DrawCircleTarget(displayval, i) {
    var xWidth3;
    var getEveryXCoord2;
    var getEveryYCoord2;


    var xPosition3 = displayval[i].xcoordinate + displayval[i].TargetOffsetXoffset;


    if (xPosition3 < 0) {
        xWidth3 = -200;
        getEveryXCoord2 = -50 * 10;

    }
    else {
        xWidth3 = 200;
        getEveryXCoord2 = 50 * 10;

    }

    var xPositionPlusX3 = getEveryXCoord2;
    var finalXPosition3 = xPosition3 / xPositionPlusX3 * xWidth3;

    //y grid and target calculations
    var yWidth3;



    var yPosition3 = displayval[i].ycoordinate + +displayval[i].TargetOffsetYoffset;


    if (yPosition3 < 0) {
        yWidth3 = -200;
        getEveryYCoord2 = -50 * 10;
    }
    else {
        yWidth3 = 200;
        getEveryYCoord2 = 50 * 10;
    }

    var yPositionPlusY3 = getEveryYCoord2;
    var finalYPosition3 = yPosition3 / yPositionPlusY3 * yWidth3;


    var radiusTextBox = displayval[i].DiameterOfCircleXoffset;
    radiusTextBox = radiusTextBox / 2;
    radiusTextBox = radiusTextBox / 5;

    var canvas1 = document.getElementById('myCanvas2');
    var centerX1 = canvas1.width / 2;
    var centerY1 = canvas1.height / 2;
    var context1 = canvas1.getContext('2d');
    var xoffset1 = centerX1 + finalXPosition3;
    var yoffset1 = centerY1 - finalYPosition3;


    context1.beginPath();
    context1.strokeStyle = 'black';
    context1.arc(xoffset1, yoffset1, radiusTextBox, 0, Math.PI * 2, true);
    context1.stroke();
    context1.closePath();

    if (displayval[i] === displayval[0]) {



        // draw red circle
        var canvas55 = document.getElementById('myCanvas2');
        var centerX55 = canvas55.width / 2;
        var centerY55 = canvas55.height / 2;
        var context55 = canvas55.getContext('2d');
        var xoffset55 = centerX55 + 0;
        var yoffset55 = centerY55 - 0;

        context55.beginPath();
        context55.strokeStyle = 'red';
        context55.arc(xoffset55, yoffset55, 3, 0, Math.PI * 2, true);
        context55.stroke();
        context55.closePath();

    }

}

function DrawSquareTarget(displayval, i) {

    var squareRotation = displayval[i].Rotation;

    var squareLength = displayval[i].DiameterOfCircleXoffset;
    squareLength = squareLength / 2;
    squareLength = squareLength / 5;

    var squareHeight = displayval[i].DiameterOfCircleXoffset;
    squareHeight = squareHeight / 2;
    squareHeight = squareHeight / 5;

    var xWidth2;
    var getEveryXCoord3;
    var getEveryYCoord3;

    var xPosition2 = displayval[i].xcoordinate + displayval[i].TargetOffsetXoffset;

    if (xPosition2 < 0) {
        xWidth2 = -200;
        getEveryXCoord3 = -50 * 10;
    }
    else {
        xWidth2 = 200;
        getEveryXCoord3 = 50 * 10;
    }

    var xPositionPlusX2 = getEveryXCoord3;
    var finalXPosition2 = xPosition2 / xPositionPlusX2 * xWidth2;
    finalXPosition2 = finalXPosition2 - squareLength / 2;

    //y grid and target calculations
    var yWidth2;


    var yPosition2 = displayval[i].ycoordinate + displayval[i].TargetOffsetYoffset;


    if (yPosition2 < 0) {
        yWidth2 = -200;
        getEveryYCoord3 = -50 * 10;

    }
    else {
        yWidth2 = 200;
        getEveryYCoord3 = 50 * 10;

    }

    var yPositionPlusY2 = getEveryYCoord3;
    var finalYPosition2 = yPosition2 / yPositionPlusY2 * yWidth2;
    finalYPosition2 = finalYPosition2 + squareHeight / 2;

    var canvas3 = document.getElementById('myCanvas2');
    var centerX3 = canvas3.width / 2;
    var centerY3 = canvas3.height / 2;
    var context3 = canvas3.getContext('2d');

    var xoffset3 = centerX3 + finalXPosition2;
    var yoffset3 = centerY3 - finalYPosition2;

    var rotationalValue = (squareRotation * Math.PI / 180);



    var drawX = 0;
    var drawY = 0;
    var drawWidth = squareLength;
    var drawHeight = squareHeight;
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


    if (displayval[i] === displayval[0]) {

        // draw red circle
        var canvas95 = document.getElementById('myCanvas2');
        var centerX95 = canvas95.width / 2;
        var centerY95 = canvas95.height / 2;
        var context95 = canvas95.getContext('2d');
        var xoffset95 = centerX95 + 0;
        var yoffset95 = centerY95 - 0;

        context95.beginPath();
        context95.strokeStyle = 'red';
        context95.arc(xoffset95, yoffset95, 3, 0, Math.PI * 2, true);
        context95.stroke();

    }

}

function DrawRectangleTarget(displayval, i) {

    var rectangleRotation = displayval[i].Rotation;

    var rectangleLength = displayval[i].DiameterOfCircleXoffset;
    rectangleLength = rectangleLength / 2;
    rectangleLength = rectangleLength / 5;

    var rectangleHeight = displayval[i].DiameterOfCircleYoffset;
    rectangleHeight = rectangleHeight / 2;
    rectangleHeight = rectangleHeight / 5;

    var xWidth2;
    var getEveryXCoord3;
    var getEveryYCoord3;


    var xPosition2 = displayval[i].xcoordinate + displayval[i].TargetOffsetXoffset;


    if (xPosition2 < 0) {
        xWidth2 = -200;
        getEveryXCoord3 = -50 * 10;
    }
    else {
        xWidth2 = 200;
        getEveryXCoord3 = 50 * 10;
    }

    var xPositionPlusX2 = getEveryXCoord3;
    var finalXPosition2 = xPosition2 / xPositionPlusX2 * xWidth2;
    finalXPosition2 = finalXPosition2 - rectangleLength / 2;

    //y grid and target calculations
    var yWidth2;


    var yPosition2 = displayval[i].ycoordinate + displayval[i].TargetOffsetYoffset;


    if (yPosition2 < 0) {
        yWidth2 = -200;
        getEveryYCoord3 = -50 * 10;

    }
    else {
        yWidth2 = 200;
        getEveryYCoord3 = 50 * 10;

    }

    var yPositionPlusY2 = getEveryYCoord3;
    var finalYPosition2 = yPosition2 / yPositionPlusY2 * yWidth2;
    finalYPosition2 = finalYPosition2 + rectangleHeight / 2;


    var canvas3 = document.getElementById('myCanvas2');
    var centerX3 = canvas3.width / 2;
    var centerY3 = canvas3.height / 2;
    var context3 = canvas3.getContext('2d');

    var xoffset3 = centerX3 + finalXPosition2;
    var yoffset3 = centerY3 - finalYPosition2;

    var rotationalValue = (rectangleRotation * Math.PI / 180);



    var drawX = 0;
    var drawY = 0;
    var drawWidth = rectangleLength;
    var drawHeight = rectangleHeight;
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

    if (displayval[i] === displayval[0]) {

        // draw red circle
        var canvas95 = document.getElementById('myCanvas2');
        var centerX95 = canvas95.width / 2;
        var centerY95 = canvas95.height / 2;
        var context95 = canvas95.getContext('2d');
        var xoffset95 = centerX95 + 0;
        var yoffset95 = centerY95 - 0;

        context95.beginPath();
        context95.strokeStyle = 'red';
        context95.arc(xoffset95, yoffset95, 3, 0, Math.PI * 2, true);
        context95.stroke();
        //----------------------------

    }

}

function DrawPolygonTarget(displayval, i) {

    var polyRotation = displayval[i].Rotation;

    var numberOfVertices = displayval[i].NumberVertices;


    var vertex1X;
    var vertex1Y;


    var vertex2X;
    var vertex2Y;

    var vertex3X;
    var vertex3Y;

    var vertex4X;
    var vertex4Y;


    var vertex5X;
    var vertex5Y;

    var vertex6X;
    var vertex6Y;

    var vertex7X;
    var vertex7Y;

    var vertex8X;
    var vertex8Y;



    if (numberOfVertices == 3) {

        vertex1X = displayval[i].Corner1Xofffset;
        vertex1X = vertex1X / 2;
        vertex1X = vertex1X / 5;

        vertex1Y = displayval[i].Corner1Yoffset;
        vertex1Y = vertex1Y / 2;
        vertex1Y = vertex1Y / 5;


        vertex2X = displayval[i].Corner2Xoffset;
        vertex2X = vertex2X / 2;
        vertex2X = vertex2X / 5;

        vertex2Y = displayval[i].Corner2Yoffset;
        vertex2Y = vertex2Y / 2;
        vertex2Y = vertex2Y / 5;

        vertex3X = displayval[i].Corner3Xoffset;
        vertex3X = vertex3X / 2;
        vertex3X = vertex3X / 5;

        vertex3Y = displayval[i].Corner3Yoffset;
        vertex3Y = vertex3Y / 2;
        vertex3Y = vertex3Y / 5;
    }

    if (numberOfVertices == 4) {

        vertex1X = displayval[i].Corner1Xofffset;
        vertex1X = vertex1X / 2;
        vertex1X = vertex1X / 5;

        vertex1Y = displayval[i].Corner1Yoffset;
        vertex1Y = vertex1Y / 2;
        vertex1Y = vertex1Y / 5;


        vertex2X = displayval[i].Corner2Xoffset;
        vertex2X = vertex2X / 2;
        vertex2X = vertex2X / 5;

        vertex2Y = displayval[i].Corner2Yoffset;
        vertex2Y = vertex2Y / 2;
        vertex2Y = vertex2Y / 5;

        vertex3X = displayval[i].Corner3Xoffset;
        vertex3X = vertex3X / 2;
        vertex3X = vertex3X / 5;

        vertex3Y = displayval[i].Corner3Yoffset;
        vertex3Y = vertex3Y / 2;
        vertex3Y = vertex3Y / 5;

        vertex4X = displayval[i].Corner4Xoffset;
        vertex4X = vertex4X / 2;
        vertex4X = vertex4X / 5;

        vertex4Y = displayval[i].Corner4Yoffset;
        vertex4Y = vertex4Y / 2;
        vertex4Y = vertex4Y / 5;
    }

    if (numberOfVertices == 5) {


        vertex1X = displayval[i].Corner1Xofffset;
        vertex1X = vertex1X / 2;
        vertex1X = vertex1X / 5;

        vertex1Y = displayval[i].Corner1Yoffset;
        vertex1Y = vertex1Y / 2;
        vertex1Y = vertex1Y / 5;


        vertex2X = displayval[i].Corner2Xoffset;
        vertex2X = vertex2X / 2;
        vertex2X = vertex2X / 5;

        vertex2Y = displayval[i].Corner2Yoffset;
        vertex2Y = vertex2Y / 2;
        vertex2Y = vertex2Y / 5;

        vertex3X = displayval[i].Corner3Xoffset;
        vertex3X = vertex3X / 2;
        vertex3X = vertex3X / 5;

        vertex3Y = displayval[i].Corner3Yoffset;
        vertex3Y = vertex3Y / 2;
        vertex3Y = vertex3Y / 5;

        vertex4X = displayval[i].Corner4Xoffset;
        vertex4X = vertex4X / 2;
        vertex4X = vertex4X / 5;

        vertex4Y = displayval[i].Corner4Yoffset;
        vertex4Y = vertex4Y / 2;
        vertex4Y = vertex4Y / 5;


        vertex5X = displayval[i].Corner5Xoffset;
        vertex5X = vertex5X / 2;
        vertex5X = vertex5X / 5;

        vertex5Y = displayval[i].Corner5Yoffset;
        vertex5Y = vertex5Y / 2;
        vertex5Y = vertex5Y / 5;
    }

    if (numberOfVertices == 6) {

        vertex1X = displayval[i].Corner1Xofffset;
        vertex1X = vertex1X / 2;
        vertex1X = vertex1X / 5;

        vertex1Y = displayval[i].Corner1Yoffset;
        vertex1Y = vertex1Y / 2;
        vertex1Y = vertex1Y / 5;


        vertex2X = displayval[i].Corner2Xoffset;
        vertex2X = vertex2X / 2;
        vertex2X = vertex2X / 5;

        vertex2Y = displayval[i].Corner2Yoffset;
        vertex2Y = vertex2Y / 2;
        vertex2Y = vertex2Y / 5;

        vertex3X = displayval[i].Corner3Xoffset;
        vertex3X = vertex3X / 2;
        vertex3X = vertex3X / 5;

        vertex3Y = displayval[i].Corner3Yoffset;
        vertex3Y = vertex3Y / 2;
        vertex3Y = vertex3Y / 5;

        vertex4X = displayval[i].Corner4Xoffset;
        vertex4X = vertex4X / 2;
        vertex4X = vertex4X / 5;

        vertex4Y = displayval[i].Corner4Yoffset;
        vertex4Y = vertex4Y / 2;
        vertex4Y = vertex4Y / 5;


        vertex5X = displayval[i].Corner5Xoffset;
        vertex5X = vertex5X / 2;
        vertex5X = vertex5X / 5;

        vertex5Y = displayval[i].Corner5Yoffset;
        vertex5Y = vertex5Y / 2;
        vertex5Y = vertex5Y / 5;

        vertex6X = displayval[i].Corner6Xoffset;
        vertex6X = vertex6X / 2;
        vertex6X = vertex6X / 5;

        vertex6Y = displayval[i].Corner6Yoffset;
        vertex6Y = vertex6Y / 2;
        vertex6Y = vertex6Y / 5;
    }

    if (numberOfVertices == 7) {

        vertex1X = displayval[i].Corner1Xofffset;
        vertex1X = vertex1X / 2;
        vertex1X = vertex1X / 5;

        vertex1Y = displayval[i].Corner1Yoffset;
        vertex1Y = vertex1Y / 2;
        vertex1Y = vertex1Y / 5;


        vertex2X = displayval[i].Corner2Xoffset;
        vertex2X = vertex2X / 2;
        vertex2X = vertex2X / 5;

        vertex2Y = displayval[i].Corner2Yoffset;
        vertex2Y = vertex2Y / 2;
        vertex2Y = vertex2Y / 5;

        vertex3X = displayval[i].Corner3Xoffset;
        vertex3X = vertex3X / 2;
        vertex3X = vertex3X / 5;

        vertex3Y = displayval[i].Corner3Yoffset;
        vertex3Y = vertex3Y / 2;
        vertex3Y = vertex3Y / 5;

        vertex4X = displayval[i].Corner4Xoffset;
        vertex4X = vertex4X / 2;
        vertex4X = vertex4X / 5;

        vertex4Y = displayval[i].Corner4Yoffset;
        vertex4Y = vertex4Y / 2;
        vertex4Y = vertex4Y / 5;


        vertex5X = displayval[i].Corner5Xoffset;
        vertex5X = vertex5X / 2;
        vertex5X = vertex5X / 5;

        vertex5Y = displayval[i].Corner5Yoffset;
        vertex5Y = vertex5Y / 2;
        vertex5Y = vertex5Y / 5;

        vertex6X = displayval[i].Corner6Xoffset;
        vertex6X = vertex6X / 2;
        vertex6X = vertex6X / 5;

        vertex6Y = displayval[i].Corner6Yoffset;
        vertex6Y = vertex6Y / 2;
        vertex6Y = vertex6Y / 5;

        vertex7X = displayval[i].Corner7Xoffset;
        vertex7X = vertex7X / 2;
        vertex7X = vertex7X / 5;

        vertex7Y = displayval[i].Corner7Yoffset;
        vertex7Y = vertex7Y / 2;
        vertex7Y = vertex7Y / 5;
    }

    if (numberOfVertices == 8) {

        vertex1X = displayval[i].Corner1Xofffset;
        vertex1X = vertex1X / 2;
        vertex1X = vertex1X / 5;

        vertex1Y = displayval[i].Corner1Yoffset;
        vertex1Y = vertex1Y / 2;
        vertex1Y = vertex1Y / 5;


        vertex2X = displayval[i].Corner2Xoffset;
        vertex2X = vertex2X / 2;
        vertex2X = vertex2X / 5;

        vertex2Y = displayval[i].Corner2Yoffset;
        vertex2Y = vertex2Y / 2;
        vertex2Y = vertex2Y / 5;

        vertex3X = displayval[i].Corner3Xoffset;
        vertex3X = vertex3X / 2;
        vertex3X = vertex3X / 5;

        vertex3Y = displayval[i].Corner3Yoffset;
        vertex3Y = vertex3Y / 2;
        vertex3Y = vertex3Y / 5;

        vertex4X = displayval[i].Corner4Xoffset;
        vertex4X = vertex4X / 2;
        vertex4X = vertex4X / 5;

        vertex4Y = displayval[i].Corner4Yoffset;
        vertex4Y = vertex4Y / 2;
        vertex4Y = vertex4Y / 5;


        vertex5X = displayval[i].Corner5Xoffset;
        vertex5X = vertex5X / 2;
        vertex5X = vertex5X / 5;

        vertex5Y = displayval[i].Corner5Yoffset;
        vertex5Y = vertex5Y / 2;
        vertex5Y = vertex5Y / 5;

        vertex6X = displayval[i].Corner6Xoffset;
        vertex6X = vertex6X / 2;
        vertex6X = vertex6X / 5;

        vertex6Y = displayval[i].Corner6Yoffset;
        vertex6Y = vertex6Y / 2;
        vertex6Y = vertex6Y / 5;

        vertex7X = displayval[i].Corner7Xoffset;
        vertex7X = vertex7X / 2;
        vertex7X = vertex7X / 5;

        vertex7Y = displayval[i].Corner7Yoffset;
        vertex7Y = vertex7Y / 2;
        vertex7Y = vertex7Y / 5;

        vertex8X = displayval[i].Corner8Xoffset;
        vertex8X = vertex8X / 2;
        vertex8X = vertex8X / 5;

        vertex8Y = displayval[i].Corner8Yoffset;
        vertex8Y = vertex8Y / 2;
        vertex8Y = vertex8Y / 5;

    }

    var xWidth4;
    var getEveryXCoord5;
    var getEveryYCoord5;

    var xPosition4 = displayval[i].xcoordinate + displayval[i].TargetOffsetXoffset;



    if (xPosition4 < 0) {
        xWidth4 = -200;
        getEveryXCoord5 = -50 * 10;

    }
    else {
        xWidth4 = 200;
        getEveryXCoord5 = 50 * 10;
    }

    var xPositionPlusX4 = getEveryXCoord5;
    var finalXPosition4 = xPosition4 / xPositionPlusX4 * xWidth4;

    //y grid and target calculations
    var yWidth4;


    var yPosition4 = displayval[i].ycoordinate + displayval[i].TargetOffsetYoffset;


    if (yPosition4 < 0) {
        yWidth4 = -200;
        getEveryYCoord5 = -50 * 10;
    }
    else {
        yWidth4 = 200;
        getEveryYCoord5 = 50 * 10;
    }

    var yPositionPlusY4 = getEveryYCoord5;
    var finalYPosition4 = yPosition4 / yPositionPlusY4 * yWidth4;

    var canvas4 = document.getElementById('myCanvas2');
    var centerX4 = canvas4.width / 2;
    var centerY4 = canvas4.height / 2;
    var context4 = canvas4.getContext('2d');
    var xoffset4 = centerX4 + finalXPosition4;
    var yoffset4 = centerY4 - finalYPosition4;

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


    if (displayval[i] === displayval[0]) {

        // draw red circle
        var canvas96 = document.getElementById('myCanvas2');
        var centerX96 = canvas96.width / 2;
        var centerY96 = canvas96.height / 2;
        var context96 = canvas96.getContext('2d');
        var xoffset96 = centerX96 + 0;
        var yoffset96 = centerY96 - 0;

        context96.beginPath();
        context96.strokeStyle = 'red';
        context96.arc(xoffset96, yoffset96, 3, 0, Math.PI * 2, true);
        context96.stroke();

    }

}

function DrawEllipseTarget(displayval, i) {

    var ellipseRotation = displayval[i].Rotation;

    var ellipseLength = displayval[i].DiameterOfCircleXoffset;
    ellipseLength = ellipseLength / 2;
    ellipseLength = ellipseLength / 5;

    var ellipseHeight = displayval[i].DiameterOfCircleYoffset;
    ellipseHeight = ellipseHeight / 2;
    ellipseHeight = ellipseHeight / 5;


    var xWidth5;
    var getEveryXCoord6;
    var getEveryYCoord6;


    var xPosition5 = displayval[i].xcoordinate + displayval[i].TargetOffsetXoffset;


    if (xPosition5 < 0) {
        xWidth5 = -200;
        getEveryXCoord6 = -50 * 10;

    }
    else {
        xWidth5 = 200;
        getEveryXCoord6 = 50 * 10;
    }

    var xPositionPlusX5 = getEveryXCoord6;
    var finalXPosition5 = xPosition5 / xPositionPlusX5 * xWidth5;

    //y grid and target calculations
    var yWidth5;

    var yPosition5 = displayval[i].ycoordinate + displayval[i].TargetOffsetYoffset;


    if (yPosition5 < 0) {
        yWidth5 = -200;
        getEveryYCoord6 = -50 * 10;
    }
    else {
        yWidth5 = 200;
        getEveryYCoord6 = 50 * 10;
    }

    var yPositionPlusY5 = getEveryYCoord6;
    var finalYPosition5 = yPosition5 / yPositionPlusY5 * yWidth5;


    var canvas6 = document.getElementById('myCanvas2');
    var centerX6 = canvas6.width / 2;
    var centerY6 = canvas6.height / 2;
    var context6 = canvas6.getContext('2d');
    var xoffset6 = centerX6 + finalXPosition5;
    var yoffset6 = centerY6 - finalYPosition5;

    var ellipseRotationalValue = (ellipseRotation * (Math.PI / 180.00));

    context6.beginPath();
    context6.ellipse(xoffset6, yoffset6, ellipseLength, ellipseHeight, ellipseRotationalValue, 0, 2 * Math.PI);

    context6.strokeStyle = "black";
    context6.lineWidth = 1;
    context6.stroke();


    if (displayval[i] === displayval[0]) {



        // draw red circle
        var canvas97 = document.getElementById('myCanvas2');
        var centerX97 = canvas97.width / 2;
        var centerY97 = canvas97.height / 2;
        var context97 = canvas97.getContext('2d');
        var xoffset97 = centerX97 + 0;
        var yoffset97 = centerY97 - 0;

        context97.beginPath();
        context97.strokeStyle = 'red';
        context97.arc(xoffset97, yoffset97, 3, 0, Math.PI * 2, true);
        context97.stroke();
        //----------------------------


    }

}

function DrawPointTarget(displayval, i) {

    var xWidth6;
    var getEveryXCoord7;
    var getEveryYCoord7;


    var xPosition6 = displayval[i].xcoordinate + displayval[i].TargetOffsetXoffset;


    if (xPosition6 < 0) {
        xWidth6 = -200;
        getEveryXCoord7 = -50 * 10;
    }
    else {
        xWidth6 = 200;
        getEveryXCoord7 = 50 * 10;
    }

    var xPositionPlusX6 = getEveryXCoord7;
    var finalXPosition6 = xPosition6 / xPositionPlusX6 * xWidth6;

    //y grid and target calculations
    var yWidth6;


    var yPosition6 = displayval[i].ycoordinate + displayval[i].TargetOffsetYoffset;


    if (yPosition6 < 0) {
        yWidth6 = -200;
        getEveryYCoord7 = -50 * 10;
    }
    else {
        yWidth6 = 200;
        getEveryYCoord7 = 50 * 10;
    }

    var yPositionPlusY6 = getEveryYCoord7;
    var finalYPosition6 = yPosition6 / yPositionPlusY6 * yWidth6;

    var canvas7 = document.getElementById('myCanvas2');
    var centerX7 = canvas7.width / 2;
    var centerY7 = canvas7.height / 2;
    var context7 = canvas7.getContext('2d');
    var xoffset7 = centerX7 + finalXPosition6;
    var yoffset7 = centerY7 - finalYPosition6;

    context7.beginPath();
    context7.fillStyle = "black";
    context7.arc(xoffset7, yoffset7, 2, 0, Math.PI * 2, true);
    context7.stroke();
    context7.fill();


    if (displayval[i] === displayval[0]) {


        // draw red circle
        var canvas98 = document.getElementById('myCanvas2');
        var centerX98 = canvas98.width / 2;
        var centerY98 = canvas98.height / 2;
        var context98 = canvas98.getContext('2d');
        var xoffset98 = centerX98 + 0;
        var yoffset98 = centerY98 - 0;

        context98.beginPath();
        context98.strokeStyle = 'red';
        context98.arc(xoffset98, yoffset98, 3, 0, Math.PI * 2, true);
        context98.stroke();
        //----------------------------


    }

}

function DrawCircleTargetZoom(displayval, i, getEveryXCoordSGraph, getEveryYCoordSGraph) {

    var xWidth3;
    var getEveryXCoord2;
    var getEveryYCoord2;

    var xPosition3 = displayval[i].xcoordinate + displayval[i].TargetOffsetXoffset;


    if (xPosition3 < 0) {
        xWidth3 = -200;
        getEveryXCoord2 = -getEveryXCoordSGraph * 10;

    }
    else {
        xWidth3 = 200;
        getEveryXCoord2 = getEveryXCoordSGraph * 10;

    }

    var xPositionPlusX3 = getEveryXCoord2;
    var finalXPosition3 = xPosition3 / xPositionPlusX3 * xWidth3;

    //y grid and target calculations
    var yWidth3;


    var yPosition3 = displayval[i].ycoordinate + displayval[i].TargetOffsetYoffset;


    if (yPosition3 < 0) {
        yWidth3 = -200;
        getEveryYCoord2 = -getEveryYCoordSGraph * 10;
    }
    else {
        yWidth3 = 200;
        getEveryYCoord2 = getEveryYCoordSGraph * 10;
    }

    var yPositionPlusY3 = getEveryYCoord2;
    var finalYPosition3 = yPosition3 / yPositionPlusY3 * yWidth3;



    var radiusTextBox = displayval[i].DiameterOfCircleXoffset;
    radiusTextBox = radiusTextBox / 2;
    radiusTextBox = radiusTextBox / 5;

    var canvas1 = document.getElementById('myCanvas2');
    var centerX1 = canvas1.width / 2;
    var centerY1 = canvas1.height / 2;
    var context1 = canvas1.getContext('2d');
    var xoffset1 = centerX1 + finalXPosition3;
    var yoffset1 = centerY1 - finalYPosition3;


    context1.beginPath();
    context1.strokeStyle = 'black';
    context1.arc(xoffset1, yoffset1, radiusTextBox, 0, Math.PI * 2, true);
    context1.stroke();
    context1.closePath();

    if (displayval[i] === displayval[0]) {


        // draw red circle
        var canvas55 = document.getElementById('myCanvas2');
        var centerX55 = canvas55.width / 2;
        var centerY55 = canvas55.height / 2;
        var context55 = canvas55.getContext('2d');
        var xoffset55 = centerX55 + 0;
        var yoffset55 = centerY55 - 0;

        context55.beginPath();
        context55.strokeStyle = 'red';
        context55.arc(xoffset55, yoffset55, 3, 0, Math.PI * 2, true);
        context55.stroke();
        context55.closePath();

    }

}

function DrawSquareTargetZoom(displayval, i, getEveryXCoordSGraph, getEveryYCoordSGraph) {
    var squareRotation = displayval[i].Rotation;

    var squareLength = displayval[i].DiameterOfCircleXoffset;
    squareLength = squareLength / 2;
    squareLength = squareLength / 5;

    var squareHeight = displayval[i].DiameterOfCircleXoffset;
    squareHeight = squareHeight / 2;
    squareHeight = squareHeight / 5;

    var xWidth2;
    var getEveryXCoord3;
    var getEveryYCoord3;


    var xPosition2 = displayval[i].xcoordinate + displayval[i].TargetOffsetXoffset;


    if (xPosition2 < 0) {
        xWidth2 = -200;
        getEveryXCoord3 = -getEveryXCoordSGraph * 10;
    }
    else {
        xWidth2 = 200;
        getEveryXCoord3 = getEveryXCoordSGraph * 10;
    }

    var xPositionPlusX2 = getEveryXCoord3;
    var finalXPosition2 = xPosition2 / xPositionPlusX2 * xWidth2;
    finalXPosition2 = finalXPosition2 - squareLength / 2;

    //y grid and target calculations
    var yWidth2;

    var yPosition2 = displayval[i].ycoordinate + displayval[i].TargetOffsetYoffset;


    if (yPosition2 < 0) {
        yWidth2 = -200;
        getEveryYCoord3 = -getEveryYCoordSGraph * 10;

    }
    else {
        yWidth2 = 200;
        getEveryYCoord3 = getEveryYCoordSGraph * 10;
    }

    var yPositionPlusY2 = getEveryYCoord3;
    var finalYPosition2 = yPosition2 / yPositionPlusY2 * yWidth2;
    finalYPosition2 = finalYPosition2 + squareHeight / 2;

    var canvas3 = document.getElementById('myCanvas2');
    var centerX3 = canvas3.width / 2;
    var centerY3 = canvas3.height / 2;
    var context3 = canvas3.getContext('2d');

    var xoffset3 = centerX3 + finalXPosition2;
    var yoffset3 = centerY3 - finalYPosition2;

    var rotationalValue = (squareRotation * Math.PI / 180);



    var drawX = 0;
    var drawY = 0;
    var drawWidth = squareLength;
    var drawHeight = squareHeight;
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

    if (displayval[i] === displayval[0]) {



        // draw red circle
        var canvas95 = document.getElementById('myCanvas2');
        var centerX95 = canvas95.width / 2;
        var centerY95 = canvas95.height / 2;
        var context95 = canvas95.getContext('2d');
        var xoffset95 = centerX95 + 0;
        var yoffset95 = centerY95 - 0;

        context95.beginPath();
        context95.strokeStyle = 'red';
        context95.arc(xoffset95, yoffset95, 3, 0, Math.PI * 2, true);
        context95.stroke();

    }


}

function DrawRectangleTargetZoom(displayval, i, getEveryXCoordSGraph, getEveryYCoordSGraph) {

    var rectangleRotation = displayval[i].Rotation;

    var rectangleLength = displayval[i].DiameterOfCircleXoffset;
    rectangleLength = rectangleLength / 2;
    rectangleLength = rectangleLength / 5;

    var rectangleHeight = displayval[i].DiameterOfCircleYoffset;
    rectangleHeight = rectangleHeight / 2;
    rectangleHeight = rectangleHeight / 5;

    var xWidth2;
    var getEveryXCoord3;
    var getEveryYCoord3;

    var xPosition2 = displayval[i].xcoordinate + displayval[i].TargetOffsetXoffset;

    if (xPosition2 < 0) {
        xWidth2 = -200;
        getEveryXCoord3 = -getEveryXCoordSGraph * 10;
    }
    else {
        xWidth2 = 200;
        getEveryXCoord3 = getEveryXCoordSGraph * 10;
    }

    var xPositionPlusX2 = getEveryXCoord3;
    var finalXPosition2 = xPosition2 / xPositionPlusX2 * xWidth2;
    finalXPosition2 = finalXPosition2 - rectangleLength / 2;

    //y grid and target calculations
    var yWidth2;


    var yPosition2 = displayval[i].ycoordinate + displayval[i].TargetOffsetYoffset;


    if (yPosition2 < 0) {
        yWidth2 = -200;
        getEveryYCoord3 = -getEveryYCoordSGraph * 10;

    }
    else {
        yWidth2 = 200;
        getEveryYCoord3 = getEveryYCoordSGraph * 10;

    }

    var yPositionPlusY2 = getEveryYCoord3;
    var finalYPosition2 = yPosition2 / yPositionPlusY2 * yWidth2;
    finalYPosition2 = finalYPosition2 + rectangleHeight / 2;


    var canvas3 = document.getElementById('myCanvas2');
    var centerX3 = canvas3.width / 2;
    var centerY3 = canvas3.height / 2;
    var context3 = canvas3.getContext('2d');

    var xoffset3 = centerX3 + finalXPosition2;
    var yoffset3 = centerY3 - finalYPosition2;

    var rotationalValue = (rectangleRotation * Math.PI / 180);


    var drawX = 0;
    var drawY = 0;
    var drawWidth = rectangleLength;
    var drawHeight = rectangleHeight;
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

    if (displayval[i] === displayval[0]) {



        // draw red circle
        var canvas95 = document.getElementById('myCanvas2');
        var centerX95 = canvas95.width / 2;
        var centerY95 = canvas95.height / 2;
        var context95 = canvas95.getContext('2d');
        var xoffset95 = centerX95 + 0;
        var yoffset95 = centerY95 - 0;

        context95.beginPath();
        context95.strokeStyle = 'red';
        context95.arc(xoffset95, yoffset95, 3, 0, Math.PI * 2, true);
        context95.stroke();
        //----------------------------


    }

}

function DrawPolygonTargetZoom(displayval, i, getEveryXCoordSGraph, getEveryYCoordSGraph) {

    var polyRotation = displayval[i].Rotation;

    var numberOfVertices = displayval[i].NumberVertices;


    var vertex1X;
    var vertex1Y;


    var vertex2X;
    var vertex2Y;

    var vertex3X;
    var vertex3Y;

    var vertex4X;
    var vertex4Y;


    var vertex5X;
    var vertex5Y;

    var vertex6X;
    var vertex6Y;

    var vertex7X;
    var vertex7Y;

    var vertex8X;
    var vertex8Y;



    if (numberOfVertices == 3) {

        vertex1X = displayval[i].Corner1Xofffset;
        vertex1X = vertex1X / 2;
        vertex1X = vertex1X / 5;

        vertex1Y = displayval[i].Corner1Yoffset;
        vertex1Y = vertex1Y / 2;
        vertex1Y = vertex1Y / 5;


        vertex2X = displayval[i].Corner2Xoffset;
        vertex2X = vertex2X / 2;
        vertex2X = vertex2X / 5;

        vertex2Y = displayval[i].Corner2Yoffset;
        vertex2Y = vertex2Y / 2;
        vertex2Y = vertex2Y / 5;

        vertex3X = displayval[i].Corner3Xoffset;
        vertex3X = vertex3X / 2;
        vertex3X = vertex3X / 5;

        vertex3Y = displayval[i].Corner3Yoffset;
        vertex3Y = vertex3Y / 2;
        vertex3Y = vertex3Y / 5;
    }

    if (numberOfVertices == 4) {

        vertex1X = displayval[i].Corner1Xofffset;
        vertex1X = vertex1X / 2;
        vertex1X = vertex1X / 5;

        vertex1Y = displayval[i].Corner1Yoffset;
        vertex1Y = vertex1Y / 2;
        vertex1Y = vertex1Y / 5;


        vertex2X = displayval[i].Corner2Xoffset;
        vertex2X = vertex2X / 2;
        vertex2X = vertex2X / 5;

        vertex2Y = displayval[i].Corner2Yoffset;
        vertex2Y = vertex2Y / 2;
        vertex2Y = vertex2Y / 5;

        vertex3X = displayval[i].Corner3Xoffset;
        vertex3X = vertex3X / 2;
        vertex3X = vertex3X / 5;

        vertex3Y = displayval[i].Corner3Yoffset;
        vertex3Y = vertex3Y / 2;
        vertex3Y = vertex3Y / 5;

        vertex4X = displayval[i].Corner4Xoffset;
        vertex4X = vertex4X / 2;
        vertex4X = vertex4X / 5;

        vertex4Y = displayval[i].Corner4Yoffset;
        vertex4Y = vertex4Y / 2;
        vertex4Y = vertex4Y / 5;
    }

    if (numberOfVertices == 5) {


        vertex1X = displayval[i].Corner1Xofffset;
        vertex1X = vertex1X / 2;
        vertex1X = vertex1X / 5;

        vertex1Y = displayval[i].Corner1Yoffset;
        vertex1Y = vertex1Y / 2;
        vertex1Y = vertex1Y / 5;


        vertex2X = displayval[i].Corner2Xoffset;
        vertex2X = vertex2X / 2;
        vertex2X = vertex2X / 5;

        vertex2Y = displayval[i].Corner2Yoffset;
        vertex2Y = vertex2Y / 2;
        vertex2Y = vertex2Y / 5;

        vertex3X = displayval[i].Corner3Xoffset;
        vertex3X = vertex3X / 2;
        vertex3X = vertex3X / 5;

        vertex3Y = displayval[i].Corner3Yoffset;
        vertex3Y = vertex3Y / 2;
        vertex3Y = vertex3Y / 5;

        vertex4X = displayval[i].Corner4Xoffset;
        vertex4X = vertex4X / 2;
        vertex4X = vertex4X / 5;

        vertex4Y = displayval[i].Corner4Yoffset;
        vertex4Y = vertex4Y / 2;
        vertex4Y = vertex4Y / 5;


        vertex5X = displayval[i].Corner5Xoffset;
        vertex5X = vertex5X / 2;
        vertex5X = vertex5X / 5;

        vertex5Y = displayval[i].Corner5Yoffset;
        vertex5Y = vertex5Y / 2;
        vertex5Y = vertex5Y / 5;
    }

    if (numberOfVertices == 6) {

        vertex1X = displayval[i].Corner1Xofffset;
        vertex1X = vertex1X / 2;
        vertex1X = vertex1X / 5;

        vertex1Y = displayval[i].Corner1Yoffset;
        vertex1Y = vertex1Y / 2;
        vertex1Y = vertex1Y / 5;


        vertex2X = displayval[i].Corner2Xoffset;
        vertex2X = vertex2X / 2;
        vertex2X = vertex2X / 5;

        vertex2Y = displayval[i].Corner2Yoffset;
        vertex2Y = vertex2Y / 2;
        vertex2Y = vertex2Y / 5;

        vertex3X = displayval[i].Corner3Xoffset;
        vertex3X = vertex3X / 2;
        vertex3X = vertex3X / 5;

        vertex3Y = displayval[i].Corner3Yoffset;
        vertex3Y = vertex3Y / 2;
        vertex3Y = vertex3Y / 5;

        vertex4X = displayval[i].Corner4Xoffset;
        vertex4X = vertex4X / 2;
        vertex4X = vertex4X / 5;

        vertex4Y = displayval[i].Corner4Yoffset;
        vertex4Y = vertex4Y / 2;
        vertex4Y = vertex4Y / 5;


        vertex5X = displayval[i].Corner5Xoffset;
        vertex5X = vertex5X / 2;
        vertex5X = vertex5X / 5;

        vertex5Y = displayval[i].Corner5Yoffset;
        vertex5Y = vertex5Y / 2;
        vertex5Y = vertex5Y / 5;

        vertex6X = displayval[i].Corner6Xoffset;
        vertex6X = vertex6X / 2;
        vertex6X = vertex6X / 5;

        vertex6Y = displayval[i].Corner6Yoffset;
        vertex6Y = vertex6Y / 2;
        vertex6Y = vertex6Y / 5;
    }

    if (numberOfVertices == 7) {

        vertex1X = displayval[i].Corner1Xofffset;
        vertex1X = vertex1X / 2;
        vertex1X = vertex1X / 5;

        vertex1Y = displayval[i].Corner1Yoffset;
        vertex1Y = vertex1Y / 2;
        vertex1Y = vertex1Y / 5;


        vertex2X = displayval[i].Corner2Xoffset;
        vertex2X = vertex2X / 2;
        vertex2X = vertex2X / 5;

        vertex2Y = displayval[i].Corner2Yoffset;
        vertex2Y = vertex2Y / 2;
        vertex2Y = vertex2Y / 5;

        vertex3X = displayval[i].Corner3Xoffset;
        vertex3X = vertex3X / 2;
        vertex3X = vertex3X / 5;

        vertex3Y = displayval[i].Corner3Yoffset;
        vertex3Y = vertex3Y / 2;
        vertex3Y = vertex3Y / 5;

        vertex4X = displayval[i].Corner4Xoffset;
        vertex4X = vertex4X / 2;
        vertex4X = vertex4X / 5;

        vertex4Y = displayval[i].Corner4Yoffset;
        vertex4Y = vertex4Y / 2;
        vertex4Y = vertex4Y / 5;


        vertex5X = displayval[i].Corner5Xoffset;
        vertex5X = vertex5X / 2;
        vertex5X = vertex5X / 5;

        vertex5Y = displayval[i].Corner5Yoffset;
        vertex5Y = vertex5Y / 2;
        vertex5Y = vertex5Y / 5;

        vertex6X = displayval[i].Corner6Xoffset;
        vertex6X = vertex6X / 2;
        vertex6X = vertex6X / 5;

        vertex6Y = displayval[i].Corner6Yoffset;
        vertex6Y = vertex6Y / 2;
        vertex6Y = vertex6Y / 5;

        vertex7X = displayval[i].Corner7Xoffset;
        vertex7X = vertex7X / 2;
        vertex7X = vertex7X / 5;

        vertex7Y = displayval[i].Corner7Yoffset;
        vertex7Y = vertex7Y / 2;
        vertex7Y = vertex7Y / 5;
    }

    if (numberOfVertices == 8) {

        vertex1X = displayval[i].Corner1Xofffset;
        vertex1X = vertex1X / 2;
        vertex1X = vertex1X / 5;

        vertex1Y = displayval[i].Corner1Yoffset;
        vertex1Y = vertex1Y / 2;
        vertex1Y = vertex1Y / 5;


        vertex2X = displayval[i].Corner2Xoffset;
        vertex2X = vertex2X / 2;
        vertex2X = vertex2X / 5;

        vertex2Y = displayval[i].Corner2Yoffset;
        vertex2Y = vertex2Y / 2;
        vertex2Y = vertex2Y / 5;

        vertex3X = displayval[i].Corner3Xoffset;
        vertex3X = vertex3X / 2;
        vertex3X = vertex3X / 5;

        vertex3Y = displayval[i].Corner3Yoffset;
        vertex3Y = vertex3Y / 2;
        vertex3Y = vertex3Y / 5;

        vertex4X = displayval[i].Corner4Xoffset;
        vertex4X = vertex4X / 2;
        vertex4X = vertex4X / 5;

        vertex4Y = displayval[i].Corner4Yoffset;
        vertex4Y = vertex4Y / 2;
        vertex4Y = vertex4Y / 5;


        vertex5X = displayval[i].Corner5Xoffset;
        vertex5X = vertex5X / 2;
        vertex5X = vertex5X / 5;

        vertex5Y = displayval[i].Corner5Yoffset;
        vertex5Y = vertex5Y / 2;
        vertex5Y = vertex5Y / 5;

        vertex6X = displayval[i].Corner6Xoffset;
        vertex6X = vertex6X / 2;
        vertex6X = vertex6X / 5;

        vertex6Y = displayval[i].Corner6Yoffset;
        vertex6Y = vertex6Y / 2;
        vertex6Y = vertex6Y / 5;

        vertex7X = displayval[i].Corner7Xoffset;
        vertex7X = vertex7X / 2;
        vertex7X = vertex7X / 5;

        vertex7Y = displayval[i].Corner7Yoffset;
        vertex7Y = vertex7Y / 2;
        vertex7Y = vertex7Y / 5;

        vertex8X = displayval[i].Corner8Xoffset;
        vertex8X = vertex8X / 2;
        vertex8X = vertex8X / 5;

        vertex8Y = displayval[i].Corner8Yoffset;
        vertex8Y = vertex8Y / 2;
        vertex8Y = vertex8Y / 5;

    }

    var xWidth4;
    var getEveryXCoord5;
    var getEveryYCoord5;


    var xPosition4 = displayval[i].xcoordinate + displayval[i].TargetOffsetXoffset;


    if (xPosition4 < 0) {
        xWidth4 = -200;
        getEveryXCoord5 = -getEveryXCoordSGraph * 10;

    }
    else {
        xWidth4 = 200;
        getEveryXCoord5 = getEveryXCoordSGraph * 10;
    }

    var xPositionPlusX4 = getEveryXCoord5;
    var finalXPosition4 = xPosition4 / xPositionPlusX4 * xWidth4;

    //y grid and target calculations
    var yWidth4;


    var yPosition4 = displayval[i].ycoordinate + displayval[i].TargetOffsetYoffset;


    if (yPosition4 < 0) {
        yWidth4 = -200;
        getEveryYCoord5 = -getEveryYCoordSGraph * 10;
    }
    else {
        yWidth4 = 200;
        getEveryYCoord5 = getEveryYCoordSGraph * 10;
    }

    var yPositionPlusY4 = getEveryYCoord5;
    var finalYPosition4 = yPosition4 / yPositionPlusY4 * yWidth4;


    var canvas4 = document.getElementById('myCanvas2');
    var centerX4 = canvas4.width / 2;
    var centerY4 = canvas4.height / 2;
    var context4 = canvas4.getContext('2d');
    var xoffset4 = centerX4 + finalXPosition4;
    var yoffset4 = centerY4 - finalYPosition4;


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


    if (displayval[i] === displayval[0]) {



        // draw red circle
        var canvas96 = document.getElementById('myCanvas2');
        var centerX96 = canvas96.width / 2;
        var centerY96 = canvas96.height / 2;
        var context96 = canvas96.getContext('2d');
        var xoffset96 = centerX96 + 0;
        var yoffset96 = centerY96 - 0;

        context96.beginPath();
        context96.strokeStyle = 'red';
        context96.arc(xoffset96, yoffset96, 3, 0, Math.PI * 2, true);
        context96.stroke();

    }

}

function DrawEllipseTargetZoom(displayval, i, getEveryXCoordSGraph, getEveryYCoordSGraph) {

    var ellipseRotation = displayval[i].Rotation;

    var ellipseLength = displayval[i].DiameterOfCircleXoffset;
    ellipseLength = ellipseLength / 2;
    ellipseLength = ellipseLength / 5;

    var ellipseHeight = displayval[i].DiameterOfCircleYoffset;
    ellipseHeight = ellipseHeight / 2;
    ellipseHeight = ellipseHeight / 5;

    var xWidth5;
    var getEveryXCoord6;
    var getEveryYCoord6;

    var xPosition5 = displayval[i].xcoordinate + displayval[i].TargetOffsetXoffset;

    if (xPosition5 < 0) {
        xWidth5 = -200;
        getEveryXCoord6 = -getEveryXCoordSGraph * 10;

    }
    else {
        xWidth5 = 200;
        getEveryXCoord6 = getEveryXCoordSGraph * 10;
    }

    var xPositionPlusX5 = getEveryXCoord6;
    var finalXPosition5 = xPosition5 / xPositionPlusX5 * xWidth5;

    //y grid and target calculations
    var yWidth5;


    var yPosition5 = displayval[i].ycoordinate + displayval[i].TargetOffsetYoffset;


    if (yPosition5 < 0) {
        yWidth5 = -200;
        getEveryYCoord6 = -getEveryYCoordSGraph * 10;
    }
    else {
        yWidth5 = 200;
        getEveryYCoord6 = getEveryYCoordSGraph * 10;
    }

    var yPositionPlusY5 = getEveryYCoord6;
    var finalYPosition5 = yPosition5 / yPositionPlusY5 * yWidth5;


    var canvas6 = document.getElementById('myCanvas2');
    var centerX6 = canvas6.width / 2;
    var centerY6 = canvas6.height / 2;
    var context6 = canvas6.getContext('2d');
    var xoffset6 = centerX6 + finalXPosition5;
    var yoffset6 = centerY6 - finalYPosition5;

    var ellipseRotationalValue = (ellipseRotation * (Math.PI / 180.00));

    context6.beginPath();
    context6.ellipse(xoffset6, yoffset6, ellipseLength, ellipseHeight, ellipseRotationalValue, 0, 2 * Math.PI);

    context6.strokeStyle = "black";
    context6.lineWidth = 1;
    context6.stroke();

    if (displayval[i] === displayval[0]) {

        // draw red circle
        var canvas97 = document.getElementById('myCanvas2');
        var centerX97 = canvas97.width / 2;
        var centerY97 = canvas97.height / 2;
        var context97 = canvas97.getContext('2d');
        var xoffset97 = centerX97 + 0;
        var yoffset97 = centerY97 - 0;

        context97.beginPath();
        context97.strokeStyle = 'red';
        context97.arc(xoffset97, yoffset97, 3, 0, Math.PI * 2, true);
        context97.stroke();

    }

}

function DrawPointTargetZoom(displayval, i, getEveryXCoordSGraph, getEveryYCoordSGraph) {

    var xWidth6;
    var getEveryXCoord7;
    var getEveryYCoord7;



    var xPosition6 = displayval[i].xcoordinate + displayval[i].TargetOffsetXoffset;


    if (xPosition6 < 0) {
        xWidth6 = -200;
        getEveryXCoord7 = -getEveryXCoordSGraph * 10;
    }
    else {
        xWidth6 = 200;
        getEveryXCoord7 = getEveryXCoordSGraph * 10;
    }

    var xPositionPlusX6 = getEveryXCoord7;
    var finalXPosition6 = xPosition6 / xPositionPlusX6 * xWidth6;

    //y grid and target calculations
    var yWidth6;

    var yPosition6 = displayval[i].ycoordinate + displayval[i].TargetOffsetYoffset;


    if (yPosition6 < 0) {
        yWidth6 = -200;
        getEveryYCoord7 = -getEveryYCoordSGraph * 10;
    }
    else {
        yWidth6 = 200;
        getEveryYCoord7 = getEveryYCoordSGraph * 10;
    }

    var yPositionPlusY6 = getEveryYCoord7;
    var finalYPosition6 = yPosition6 / yPositionPlusY6 * yWidth6;

    var canvas7 = document.getElementById('myCanvas2');
    var centerX7 = canvas7.width / 2;
    var centerY7 = canvas7.height / 2;
    var context7 = canvas7.getContext('2d');
    var xoffset7 = centerX7 + finalXPosition6;
    var yoffset7 = centerY7 - finalYPosition6;

    context7.beginPath();
    context7.fillStyle = "black";
    context7.arc(xoffset7, yoffset7, 2, 0, Math.PI * 2, true);
    context7.stroke();
    context7.fill();


    if (displayval[i] === displayval[0]) {

        // draw red circle
        var canvas98 = document.getElementById('myCanvas2');
        var centerX98 = canvas98.width / 2;
        var centerY98 = canvas98.height / 2;
        var context98 = canvas98.getContext('2d');
        var xoffset98 = centerX98 + finalXWorkCurve5;
        var yoffset98 = centerY98 - finalYWorkCurve5;

        context98.beginPath();
        context98.strokeStyle = 'red';
        context98.arc(xoffset98, yoffset98, 3, 0, Math.PI * 2, true);
        context98.stroke();

    }

}
