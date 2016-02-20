function Graph(config, xGrid, yGrid, orginX, orginY) {
    // user defined properties
    this.canvas = document.getElementById(config.canvasId);
    this.minX = -10;
    this.minY = -10;
    this.maxX = 10;
    this.maxY = 10;
    this.unitsPerTick = config.unitsPerTick;


    if (orginX == null) {
        orginX = 400;
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
    yPos = this.centerY - yPosIncrement;// old t position 


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



function Graph2(config, xGrid, yGrid) {
    // user defined properties
    this.canvas = document.getElementById(config.canvasId);
    this.minX = -10;
    this.minY = -10;
    this.maxX = 10;
    this.maxY = 10;
    this.unitsPerTick = config.unitsPerTick;


    // constants
    this.axisColor = '#aaa';
    this.font = '7pt Calibri';
    this.tickSize = 20;

    // relationships
    this.context = this.canvas.getContext('2d');
    this.rangeX = this.maxX - this.minX;
    this.rangeY = this.maxY - this.minY;
    this.unitX = this.canvas.width / this.rangeX;
    this.unitY = this.canvas.height / this.rangeY;
    this.centerY = 200;//Math.round(Math.abs(this.minY / this.rangeY) * this.canvas.height);
    this.centerX = 200; //Math.round(Math.abs(this.minX / this.rangeX) * this.canvas.width);
    this.iteration = (this.maxX - this.minX) / 1000;
    this.scaleX = this.canvas.width / this.rangeX;
    this.scaleY = this.canvas.height / this.rangeY;



    if (xGrid == null) {
        xGrid = 50;
    }

    else {
        xGrid = xGrid;
    }

    if (yGrid == null) {
        yGrid = 50;
    }
    else {
        yGrid = yGrid;
    }

    // draw x and y axis
    this.drawXAxis(xGrid);
    this.drawYAxis(yGrid);

}



Graph2.prototype.drawXAxis = function (xGrid) {
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

Graph2.prototype.drawYAxis = function (yGrid) {
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
    yPos = this.centerY - yPosIncrement;// old t position 


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



Graph2.prototype.transformContext = function () {
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
var myGraph2 = new Graph2({
    canvasId: 'myCanvas2',
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

function ClearGraph2() {
    var canvas = document.getElementById('myCanvas2');
    var context = canvas.getContext('2d');

    context.clearRect(0, 0, canvas.width, canvas.height);

    var myGraph = new Graph2({
        canvasId: 'myCanvas2',
        minX: -10,
        minY: -10,
        maxX: 10,
        maxY: 10,
        unitsPerTick: 1
    });

    return false;

}

