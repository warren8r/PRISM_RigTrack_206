
 var currentCurveID;
//  redraw button pressed 
var redrawPressed = false;
// targets zoom from drop down
var lastTargetXDropDown;
var lastTargetYDropDown;
var lastTargetZDropDown;

// last Target Variables gets returned 
var lastTargetX;
var lastTargetY;
var lastTargetZ;

var orginX;
var orginY;
var orginZ;

// canvas container and camera variables
var container
var camera, scene, renderer;
var controls;

        
// square variables 
var squareLength;
var squareWidth;
var squareShape;
var squareGeom;
var squareMesh;
var squareXOffset;
var squareYOffset;
var squareRotation;

// Rectangle variables 
var rectangleLength;
var rectangleWidth;
var rectangleShape;
var rectangleGeom;
var rectangleMesh;
var rectangleXOffset;
var rectangleYOffset;
var rectangleRotation;

// Polygon
var polygonShape;
var polygonGeom;
var polygonMesh;
var polygonMaterial;
var polygonXOffset;
var polygonYOffset;
var polygonRotation;
var polygonVertices;
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

// Ellipse
var ellipseXLength;
var ellipseYLength;
var ellipseRotation;
var ellipseMaterial;
var ellipse;
var ellipsePath;
var ellipseGeometry;
var ellipseLine;
var ellipseXOffset;
var ellipseYOffset;

// point
var geometryPoint;
var materialPoint;
var PointMesh;
var radiusPoint;
var segmentsPoint;
var PointXOffset;
var PointYOffset;

//sphere variables
var geometrySphere;
var materialSphere;
var SphereMesh;
// for size of sphere
var radius;
var segments;
var circleXOffset;
var circleYOffset;

var GridNum
var canvas2;
var context2;
var texture2;
var material2;
var mesh2;
var QuatCamera;

var CameraPosition;
var target;
var tween;
       
// ray casting variables for collision of objects to get their position and other assets about the mesh
var projector;
var mouse = { x: 0, y: 0 };
var INTERSECTED;
var INTERSECTEDclick;

// For  2 billboards on mouse click
var textureClick;
var materialClick;
var meshClick;
var canvasClick;
var contextClick;


// graph width and height 
var GraphWidth;
var GraphHeight;


// for setting the targets color green if weel plan
var targetName;
var targetColor;
   
function init() {


    // set graph width and height of canvas to render 3d graph on 
    GraphWidth = window.innerWidth;
    GraphHeight = window.innerHeight;

    scene = new THREE.Scene();
    container = document.getElementById('Graphdiv');
    document.body.appendChild(container);

    // lighting 1 front 
    var light = new THREE.DirectionalLight(0xffffff, 1.5);
    light.position.set(0, 0, 1000);
    scene.add(light);

    // lighting  2 back
    var light2 = new THREE.DirectionalLight(0xffffff, 1.5);
    light2.position.set(0, 0, -1000);
    scene.add(light2);


    // WebGl Renderer
    renderer = new THREE.WebGLRenderer({ antialias: false });
    //renderer.setClearColor(scene.fog.color);
    renderer.setClearColor(0xf0f0f0);
    renderer.setPixelRatio(window.devicePixelRatio);
    renderer.setSize(GraphWidth, GraphHeight);
    renderer.gammaInput = true;
    renderer.gammaOutput = true;
    container.appendChild(renderer.domElement);


    // Canvas renderer
    //renderer = new THREE.CanvasRenderer();
    //renderer.setClearColor(0xf0f0f0);
    //renderer.setPixelRatio(window.devicePixelRatio);
    //renderer.setSize(window.innerWidth, window.innerHeight);
    //container.appendChild(renderer.domElement);

    // initialize object to perform world/screen calculations/ used for collision of objects selected from mouse
    projector = new THREE.Projector();

    // when the mouse moves, call the given function
    document.addEventListener('mousedown', onDocumentMouseDown, false);

    window.addEventListener('resize', onWindowResize, false);

}

function DrawSurfaceGrid(displayval, lastTarget) {

    // surface Grid 0, 0, 0
    var sizeSurface = 1000;
    var stepSurface = 100;
    var gridSurface = new THREE.GridHelper(sizeSurface, stepSurface);
    gridSurface.position.set(displayval[lastTarget].xcoordinate, 0, displayval[lastTarget].zcoordinate);
    gridSurface.setColors(new THREE.Color(0x663300), new THREE.Color(0x663300));
    gridSurface.name = "SurfaceGrid";
    scene.add(gridSurface);

    // surface grid numbers
    XNegativeNumbers(displayval[lastTarget].xcoordinate, 0, displayval[lastTarget].zcoordinate);
    XPostiveNumbers(displayval[lastTarget].xcoordinate, 0, displayval[lastTarget].zcoordinate);
    ZPostiveNumbers(displayval[lastTarget].xcoordinate, 0, displayval[lastTarget].zcoordinate);
    ZNegativeNumbers(displayval[lastTarget].xcoordinate, 0, displayval[lastTarget].zcoordinate);

}

function DrawGridsAndNumbers(displayval,lastTarget) {
    // Grids-----------------
    // Make Grid Using Grid Helper
    var size = 1000;
    var step = 100;
    var gridSize = 3000;
    var gridPlus = 2000;
    var prev = -displayval[lastTarget].tvd;

    // Bottom Grid
    var gridHelper = new THREE.GridHelper(size, step);
    gridHelper.position.set(displayval[lastTarget].xcoordinate, -displayval[lastTarget].tvd, displayval[lastTarget].zcoordinate);
    gridHelper.name = "BottomGrid";
    scene.add(gridHelper);


    // Back grid
    var gridXY = new THREE.GridHelper(size, step);
    gridXY.position.set(displayval[lastTarget].xcoordinate, -displayval[lastTarget].tvd + 1000, displayval[lastTarget].zcoordinate - 1000);
    gridXY.rotation.x = Math.PI / 2;
    gridXY.setColors(new THREE.Color(0x009900), new THREE.Color(0x009900));
    gridXY.name = "BackGrid";
    scene.add(gridXY);

    if (displayval[lastTarget].tvd >= 2000) {

        for (var j = 0; j < 1000; j++) {
            var gridXY2 = new THREE.GridHelper(size, step);
            gridXY2.position.set(displayval[lastTarget].xcoordinate, prev + gridSize, displayval[lastTarget].zcoordinate - 1000);
            gridXY2.rotation.x = Math.PI / 2;
            gridXY2.setColors(new THREE.Color(0x009900), new THREE.Color(0x009900));
            gridXY2.name = "BackGrid";
            scene.add(gridXY2);
            prev = prev + 2000;
            gridPlus = gridPlus + 2000;

            if (displayval[lastTarget].tvd >= gridPlus) {
                continue;
            }

            else {
                break;
            }

        }
    }


    // grid x numbers postive
    XNegativeNumbers(displayval[lastTarget].xcoordinate, -displayval[lastTarget].tvd, displayval[lastTarget].zcoordinate);
    XPostiveNumbers(displayval[lastTarget].xcoordinate, -displayval[lastTarget].tvd, displayval[lastTarget].zcoordinate);

    tvdDepthBack(displayval[lastTarget].xcoordinate, -displayval[lastTarget].tvd, displayval[lastTarget].zcoordinate);
    ZPostiveNumbers(displayval[lastTarget].xcoordinate, -displayval[lastTarget].tvd, displayval[lastTarget].zcoordinate);
    ZNegativeNumbers(displayval[lastTarget].xcoordinate, -displayval[lastTarget].tvd, displayval[lastTarget].zcoordinate);
}

function XNegativeNumbers(XstartPosition, YstartPosition, ZstartPosition) {
    // Creating X Grid Numbers Postive
    var xPos = XstartPosition + 30;
    var yPos = YstartPosition;
    var zPos = ZstartPosition;
    var GridNum;

    GridNum = XstartPosition - 100;
    GridNum = Math.round(GridNum * 100) / 100;

    for (var i = 0; i < 10; i++) {


        // create Grid Number Labels 50
        var canvas2 = document.createElement('canvas');
        var context2 = canvas2.getContext('2d');
        context2.font = "Bold 30px Arial";
        context2.fillStyle = "#ff0000";
        context2.fillText(GridNum, 0, 80);


        // canvas contents will be used for a texture
        var texture2 = new THREE.Texture(canvas2)
        texture2.needsUpdate = true;

        var material2 = new THREE.MeshBasicMaterial({ map: texture2, side: THREE.DoubleSide });
        material2.transparent = true;
        material2.map.minFilter = THREE.LinearFilter;

        var mesh2 = new THREE.Mesh(
            new THREE.PlaneBufferGeometry(canvas2.width, canvas2.height),
            material2
          );

        mesh2.name = "xNegativeNumbers";
        mesh2.position.set(xPos - 135, yPos, zPos + 112);
        mesh2.rotation.x = Math.PI / -2;
        mesh2.rotation.z = Math.PI / -2;

        xPos = xPos + -100;
        GridNum = GridNum + -100;
        GridNum = Math.round(GridNum * 100) / 100;

        scene.add(mesh2);
    }

}

function XPostiveNumbers(XstartPosition, YstartPosition, ZstartPosition) {
    // Creating X Grid Numbers Postive
    var xPos = XstartPosition + 233;
    var yPos = YstartPosition;
    var zPos = ZstartPosition;
    var GridNum = XstartPosition + 100;
    GridNum = Math.round(GridNum * 100) / 100;

    for (var i = 0; i < 10; i++) {


        // create Grid Number Labels 50
        var canvas2 = document.createElement('canvas');
        var context2 = canvas2.getContext('2d');
        context2.font = "Bold 30px Arial";
        context2.fillStyle = "#ff0000";
        context2.fillText(GridNum, 0, 80);


        // canvas contents will be used for a texture
        var texture2 = new THREE.Texture(canvas2)
        texture2.needsUpdate = true;

        var material2 = new THREE.MeshBasicMaterial({ map: texture2, side: THREE.DoubleSide });
        material2.transparent = true;
        material2.map.minFilter = THREE.LinearFilter;

        var mesh2 = new THREE.Mesh(
            new THREE.PlaneBufferGeometry(canvas2.width, canvas2.height),
            material2
          );
        mesh2.name = "xPostiveNumbers";
        mesh2.position.set(xPos - 135, yPos, zPos + 112);
             
        mesh2.rotation.x = Math.PI / -2;
        mesh2.rotation.z = Math.PI / -2;

        xPos = xPos + 100;

        GridNum = GridNum + 100;
        GridNum = Math.round(GridNum * 100) / 100;

        scene.add(mesh2);
    }

}

function tvdDepthBack(XstartPosition, YstartPosition, ZstartPosition) {
    // Creating X Grid Numbers Postive
    var xpos = XstartPosition + 140;
    var yPos = 0;
    var zPos = ZstartPosition - 1000;
    var GridNum = 0;
    //-------
    var xposBig = XstartPosition + 140;
    var yPosBig = 0;
    var zPosBig = ZstartPosition - 1000;
    var GridNumBig = 0;

    var DivideCount = YstartPosition / -100;

    GridNum = Math.round(GridNum * 100) / 100;
    GridNumBig = Math.round(GridNumBig * 100) / 100;


    var count = 0;

    for (var i = 0; i < DivideCount; i++) {

        count = count + 1;

        if (count == 5) {

            // Create Grid Number Labels 50
            var canvasBig = document.createElement('canvas');
            var contextBig = canvasBig.getContext('2d');
            contextBig.font = "Bold 100px Arial";

            if (GridNumBig == 0) {

                contextBig.fillStyle = "#a5682a";
            }
            else {
                contextBig.fillStyle = "#030B03";
            }

            contextBig.fillText(GridNumBig, 0, 100);

            // Canvas contents will be used for a texture
            var textureBig = new THREE.Texture(canvasBig)
            textureBig.needsUpdate = true;

            var materialBig = new THREE.MeshBasicMaterial({ map: textureBig, side: THREE.DoubleSide });
            materialBig.transparent = true;
            materialBig.map.minFilter = THREE.LinearFilter;

            var meshBig = new THREE.Mesh(
                new THREE.PlaneBufferGeometry(canvasBig.width, canvasBig.height),
                materialBig
              );
            meshBig.name = "BigTVDNumbers";
            meshBig.position.set(xposBig + 1050, yPosBig, zPosBig);
            yPosBig = yPosBig + -500;
            GridNumBig = GridNumBig - 500;
            GridNumBig = Math.round(GridNumBig * 100) / 100;
            scene.add(meshBig);

            count = 0;
        }


        // Creates the smaller labels in the center of the Grid Number Labels
        var canvas2 = document.createElement('canvas');
        var context2 = canvas2.getContext('2d');
        context2.font = "Bold 30px Arial";
        context2.fillStyle = "#030B03";
        context2.fillText(GridNum, 0, 80);


        // Canvas contents will be used for a texture
        var texture2 = new THREE.Texture(canvas2)
        texture2.needsUpdate = true;

        var material2 = new THREE.MeshBasicMaterial({ map: texture2, side: THREE.DoubleSide });
        material2.transparent = true;
        material2.map.minFilter = THREE.LinearFilter;

        var mesh2 = new THREE.Mesh(
            new THREE.PlaneBufferGeometry(canvas2.width, canvas2.height),
            material2
          );
        mesh2.name = "SmallTVDNumbers";
        mesh2.position.set(xpos, yPos, zPos);
        yPos = yPos + -100;
        GridNum = GridNum - 100;
        GridNum = Math.round(GridNum * 100) / 100;
        scene.add(mesh2);

        if (GridNum != YstartPosition) {
            continue;
        }

        else {
            break;
        }
    }

}

function ZPostiveNumbers(XstartPosition, YstartPosition, ZstartPosition) {
    // Creating X Grid Numbers Postive
    var xPos = XstartPosition + 140;
    var yPos = YstartPosition;
    var zPos = ZstartPosition + 100;
    var GridNum = ZstartPosition + 100;
    GridNum = Math.round(GridNum * 100) / 100;

    for (var i = 0; i < 10; i++) {


        // create Grid Number Labels 50
        var canvas2 = document.createElement('canvas');
        var context2 = canvas2.getContext('2d');
        context2.font = "Bold 30px Arial";
        context2.fillStyle = "#0000ff";
        context2.fillText(GridNum, 0, 80);


        // canvas contents will be used for a texture
        var texture2 = new THREE.Texture(canvas2)
        texture2.needsUpdate = true;

        var material2 = new THREE.MeshBasicMaterial({ map: texture2, side: THREE.DoubleSide });
        material2.transparent = true;
        material2.map.minFilter = THREE.LinearFilter;

        var mesh2 = new THREE.Mesh(
            new THREE.PlaneBufferGeometry(canvas2.width, canvas2.height),
            material2
          );
        mesh2.name = "zPostiveNumbers";
        mesh2.position.set(xPos, yPos, zPos);
        mesh2.rotation.x = Math.PI / -2;
        zPos = zPos + 100;
        GridNum = GridNum + 100;
        GridNum = Math.round(GridNum * 100) / 100;

        scene.add(mesh2);
    }

}

function ZNegativeNumbers(XstartPosition, YstartPosition, ZstartPosition) {
    // Creating X Grid Numbers Postive
    var xPos = XstartPosition + 140;
    var yPos = YstartPosition;
    var zPos = ZstartPosition - 100;
    var GridNum = ZstartPosition - 100;
    GridNum = Math.round(GridNum * 100) / 100;

    for (var i = 0; i < 10; i++) {


        // create Grid Number Labels 50
        var canvas2 = document.createElement('canvas');
        var context2 = canvas2.getContext('2d');
        context2.font = "Bold 30px Arial";
        context2.fillStyle = "#0000ff";
        context2.fillText(GridNum, 0, 80);


        // canvas contents will be used for a texture
        var texture2 = new THREE.Texture(canvas2)
        texture2.needsUpdate = true;

        var material2 = new THREE.MeshBasicMaterial({ map: texture2, side: THREE.DoubleSide });
        material2.transparent = true;
        material2.map.minFilter = THREE.LinearFilter;

        var mesh2 = new THREE.Mesh(
            new THREE.PlaneBufferGeometry(canvas2.width, canvas2.height),
            material2
          );
        mesh2.name = "zNegativeNumbers";
        mesh2.position.set(xPos, yPos, zPos);
        mesh2.rotation.x = Math.PI / -2;
        zPos = zPos + -100;
        GridNum = GridNum + -100;
        GridNum = Math.round(GridNum * 100) / 100;

        scene.add(mesh2);
    }

}
        
function DrawSurveyComments(Comment, xSurvey, ySurvey, zSurvey) {
    // Drawing tvd label next to target-----------
            
    GridNum = Comment;
    if (!GridNum || GridNum == '&nbsp;') {
    }
    else {
        // create Target Label
        canvas2 = document.createElement('canvas');
        context2 = canvas2.getContext('2d');
        context2.font = "Bold 50px Arial";
        context2.fillStyle = "#000000";
        context2.fillText(GridNum, 0, 80, 300);

        // canvas contents will be used for a texture
        texture2 = new THREE.Texture(canvas2)
        texture2.needsUpdate = true;

        material2 = new THREE.MeshBasicMaterial({ map: texture2, side: THREE.DoubleSide });
        material2.transparent = true;
        material2.map.minFilter = THREE.LinearFilter;

        mesh2 = new THREE.Mesh(
           new THREE.PlaneBufferGeometry(canvas2.width, canvas2.height),
           material2
         );
                
        if (commentX == xSurvey) {
            mesh2.position.set(xSurvey - 163, ySurvey, zSurvey);
            if (commentCount >= 2) {
                mesh2.position.set(xSurvey - 163, ySurvey + 100, zSurvey);
            }
        }
        else {
            mesh2.position.set(xSurvey + 163, ySurvey, zSurvey);
            if (commentCount >= 2) {
                mesh2.position.set(xSurvey + 163, ySurvey + 100, zSurvey);
            }
        }
        mesh2.name = "SurveyComment";
        scene.add(mesh2);
        commentX = xSurvey;
        commentCount++;
    }
    //-------------------------------------------

}

function ObjectClickPosition(xSurvey, ySurvey, zSurvey) {
           

    // Round The nUmbers before Displaying
    xSurvey = Math.round(xSurvey * 100) / 100;
    ySurvey = Math.round(ySurvey * 100) / 100;
    zSurvey = Math.round(zSurvey * 100) / 100;

    scene.remove(meshClick);
    GridNum = "X" + "  " + xSurvey + "," + " " + "Y" + "  " + ySurvey + "," + "  " + "Z" + "  " + zSurvey;


    // create Target Label
    canvasClick = document.createElement('canvas');
    contextClick = canvasClick.getContext('2d');
    contextClick.font = "Bold 45px Arial";
    contextClick.fillStyle = "#670494";
    contextClick.fillText(GridNum, 0, 80, 290);


    // canvas contents will be used for a texture
    textureClick = new THREE.Texture(canvasClick)
    textureClick.needsUpdate = true;

    materialClick = new THREE.MeshBasicMaterial({ map: textureClick, side: THREE.DoubleSide });
    materialClick.transparent = true;
    materialClick.map.minFilter = THREE.LinearFilter;

    meshClick = new THREE.Mesh(
       new THREE.PlaneBufferGeometry(canvasClick.width, canvasClick.height),
       materialClick
     );

    meshClick.name = "ClickComment";
    meshClick.position.set(+xSurvey - 200, +ySurvey + 10, +zSurvey + 55);
    scene.add(meshClick);
            
         
    //-------------------------------------------

}

function DrawCircleTarget(displayval, i) {

    // Get Target name And Set To Green If well plan Target
    targetName = displayval[i].targetName;

    if (targetName == "Well Plan Target") {
        targetColor = 0x00CC00;
    }
    else {
        targetColor = 0xFF0000;
    }

    // // new circle Target 
    materialSphere = new THREE.MeshBasicMaterial({ color: targetColor, side: THREE.DoubleSide });
    radius = displayval[i].DiameterOfCircleXoffset / 2;

    segments = 64;

    circleXOffset = displayval[i].xcoordinate + displayval[i].TargetOffsetXoffset;
    circleYOffset = displayval[i].zcoordinate + displayval[i].TargetOffsetYoffset;

    geometrySphere = new THREE.CircleGeometry(radius, segments);
    SphereMesh = new THREE.Mesh(geometrySphere, materialSphere);

    if (displayval[i].TargetOffsetXoffset > 0 || displayval[i].TargetOffsetYoffset > 0) {

        SphereMesh.position.set(circleXOffset, -displayval[i].tvd, circleYOffset);
    }
    else if (displayval[i].TargetOffsetXoffset < 0 || displayval[i].TargetOffsetYoffset < 0) {

        SphereMesh.position.set(circleXOffset, -displayval[i].tvd, circleYOffset);
    }
    else {
        SphereMesh.position.set(displayval[i].xcoordinate, -displayval[i].tvd, displayval[i].zcoordinate);
    }
    SphereMesh.name = "CircleTarget";

    SphereMesh.rotation.x = Math.PI / -2;
    scene.add(SphereMesh);


    renderer.render(scene, camera);

}

function DrawSquareTarget(displayval, i) {
    // Draw Square 
    squareRotation = displayval[i].Rotation;
    squareLength = displayval[i].DiameterOfCircleXoffset / 2;
    squareWidth = displayval[i].DiameterOfCircleXoffset / 2;

    squareXOffset = displayval[i].xcoordinate + displayval[i].TargetOffsetXoffset;
    squareYOffset = displayval[i].zcoordinate + displayval[i].TargetOffsetYoffset;

    squareShape = new THREE.Shape();
    squareShape.moveTo(0, 0);
    squareShape.lineTo(0, squareWidth);
    squareShape.lineTo(squareLength, squareWidth);
    squareShape.lineTo(squareLength, 0);
    squareShape.lineTo(0, 0);


    // Get Target name And Set To Green If well plan Target
    targetName = displayval[i].targetName;

    if (targetName == "Well Plan Target") {
        targetColor = 0x00CC00;
    }
    else {
        targetColor = 0xFF0000;
    }

    squareGeom = new THREE.ShapeGeometry(squareShape);
    squareMesh = new THREE.Mesh(squareGeom, new THREE.MeshBasicMaterial({ color: targetColor, side: THREE.DoubleSide }));

    if (displayval[i].TargetOffsetXoffset > 0 || displayval[i].TargetOffsetYoffset > 0) {

        squareMesh.position.set(squareXOffset, -displayval[i].tvd, squareYOffset);
    }
    else if (displayval[i].TargetOffsetXoffset < 0 || displayval[i].TargetOffsetYoffset < 0) {

        squareMesh.position.set(squareXOffset, -displayval[i].tvd, squareYOffset);
    }
    else {
        squareMesh.position.set(displayval[i].xcoordinate, -displayval[i].tvd, displayval[i].zcoordinate);
    }


    squareGeom.applyMatrix(new THREE.Matrix4().makeTranslation(-squareLength / 2, -squareWidth / 2, 0));
    squareMesh.name = "SquareTarget";
    squareMesh.rotation.x = Math.PI / -2;
    squareMesh.rotation.z = squareRotation * (Math.PI / 180.00);


    if (displayval[i].DiameterOfCircleXoffset != 0) {
        scene.add(squareMesh);
    }


    renderer.render(scene, camera);
}

function DrawRectangleTarget(displayval, i) {
    // Draw Rectangle
    rectangleRotation = displayval[i].Rotation;
    rectangleLength = displayval[i].DiameterOfCircleXoffset / 2;
    rectangleWidth = displayval[i].DiameterOfCircleYoffset / 2;

    rectangleXOffset = displayval[i].xcoordinate + displayval[i].TargetOffsetXoffset;
    rectangleYOffset = displayval[i].zcoordinate + displayval[i].TargetOffsetYoffset;

    rectangleShape = new THREE.Shape();
    rectangleShape.moveTo(0, 0);
    rectangleShape.lineTo(0, rectangleWidth);
    rectangleShape.lineTo(rectangleLength, rectangleWidth);
    rectangleShape.lineTo(rectangleLength, 0);
    rectangleShape.lineTo(0, 0);

    // Get Target name And Set To Green If well plan Target
    targetName = displayval[i].targetName;

    if (targetName == "Well Plan Target") {
        targetColor = 0x00CC00;
    }
    else {
        targetColor = 0xFF0000;
    }


    rectangleGeom = new THREE.ShapeGeometry(rectangleShape);
    rectangleMesh = new THREE.Mesh(rectangleGeom, new THREE.MeshBasicMaterial({ color: targetColor, side: THREE.DoubleSide }));

    if (displayval[i].TargetOffsetXoffset > 0 || displayval[i].TargetOffsetYoffset > 0) {

        rectangleMesh.position.set(rectangleXOffset, -displayval[i].tvd, rectangleYOffset);
    }
    else if (displayval[i].TargetOffsetXoffset < 0 || displayval[i].TargetOffsetYoffset < 0) {

        rectangleMesh.position.set(rectangleXOffset, -displayval[i].tvd, rectangleYOffset);
    }
    else {
        rectangleMesh.position.set(displayval[i].xcoordinate, -displayval[i].tvd, displayval[i].zcoordinate);
    }


    rectangleGeom.applyMatrix(new THREE.Matrix4().makeTranslation(-rectangleLength / 2, -rectangleWidth / 2, 0));

    rectangleMesh.name = "RectangleTarget";
    rectangleMesh.rotation.x = Math.PI / -2;
    rectangleMesh.rotation.z = rectangleRotation * (Math.PI / 180.00);


    if (displayval[i].DiameterOfCircleXoffset != 0 || displayval[i].DiameterOfCircleYoffset != 0) {
        scene.add(rectangleMesh);
    }


    renderer.render(scene, camera);

}

function DrawPolygonTarget(displayval, i) {
    // Draw Polygon 3 to 8 vertices
    polygonVertices = displayval[i].NumberVertices;

    vertex1X = displayval[i].Corner1Xofffset;
    vertex1Y = displayval[i].Corner1Yoffset;

    vertex2X = displayval[i].Corner2Xoffset;
    vertex2Y = displayval[i].Corner2Yoffset;

    vertex3X = displayval[i].Corner3Xoffset;
    vertex3Y = displayval[i].Corner3Yoffset;

    vertex4X = displayval[i].Corner4Xoffset;
    vertex4Y = displayval[i].Corner4Yoffset;

    vertex5X = displayval[i].Corner5Xoffset;
    vertex5Y = displayval[i].Corner5Yoffset;

    vertex6X = displayval[i].Corner6Xoffset;
    vertex6Y = displayval[i].Corner6Yoffset;

    vertex7X = displayval[i].Corner47offset;
    vertex7Y = displayval[i].Corner47offset;

    vertex8X = displayval[i].Corner8Xoffset;
    vertex8Y = displayval[i].Corner8Yoffset;


    // Draw Polygon 3 to 8 vertices 
    polygonRotation = displayval[i].Rotation;
    polygonXOffset = displayval[i].xcoordinate + displayval[i].TargetOffsetXoffset;
    polygonYOffset = displayval[i].zcoordinate + displayval[i].TargetOffsetYoffset;

    // Get Target name And Set To Green If well plan Target
    targetName = displayval[i].targetName;

    if (targetName == "Well Plan Target") {
        targetColor = 0x00CC00;
    }
    else {
        targetColor = 0xFF0000;
    }


    if (displayval[i].NumberVertices == 3) {
        polygonMaterial = new THREE.LineBasicMaterial({ color: targetColor });

        polygonGeom = new THREE.Geometry();
        polygonGeom.vertices.push(
            new THREE.Vector3(vertex1X, vertex1Y),
            new THREE.Vector3(vertex2X, vertex2Y),
            new THREE.Vector3(vertex3X, vertex3Y),
            new THREE.Vector3(vertex1X, vertex1Y)
        );

        polygonMesh = new THREE.Line(polygonGeom, polygonMaterial);
    }

    if (displayval[i].NumberVertices == 4) {

        polygonMaterial = new THREE.LineBasicMaterial({ color: targetColor });

        polygonGeom = new THREE.Geometry();
        polygonGeom.vertices.push(
            new THREE.Vector3(vertex1X, vertex1Y),
            new THREE.Vector3(vertex2X, vertex2Y),
            new THREE.Vector3(vertex3X, vertex3Y),
            new THREE.Vector3(vertex4X, vertex4Y),
            new THREE.Vector3(vertex1X, vertex1Y)
        );

        polygonMesh = new THREE.Line(polygonGeom, polygonMaterial);

    }


    if (displayval[i].NumberVertices == 5) {

        polygonMaterial = new THREE.LineBasicMaterial({ color: targetColor });

        polygonGeom = new THREE.Geometry();
        polygonGeom.vertices.push(
            new THREE.Vector3(vertex1X, vertex1Y),
            new THREE.Vector3(vertex2X, vertex2Y),
            new THREE.Vector3(vertex3X, vertex3Y),
            new THREE.Vector3(vertex4X, vertex4Y),
            new THREE.Vector3(vertex5X, vertex5Y),
            new THREE.Vector3(vertex1X, vertex1Y)
        );

        polygonMesh = new THREE.Line(polygonGeom, polygonMaterial);

    }


    if (displayval[i].NumberVertices == 6) {

        polygonMaterial = new THREE.LineBasicMaterial({ color: targetColor });

        polygonGeom = new THREE.Geometry();
        polygonGeom.vertices.push(
            new THREE.Vector3(vertex1X, vertex1Y),
            new THREE.Vector3(vertex2X, vertex2Y),
            new THREE.Vector3(vertex3X, vertex3Y),
            new THREE.Vector3(vertex4X, vertex4Y),
            new THREE.Vector3(vertex5X, vertex5Y),
            new THREE.Vector3(vertex6X, vertex6Y),
            new THREE.Vector3(vertex1X, vertex1Y)
        );

        polygonMesh = new THREE.Line(polygonGeom, polygonMaterial);

    }

    if (displayval[i].NumberVertices == 7) {

        polygonMaterial = new THREE.LineBasicMaterial({ color: targetColor });

        polygonGeom = new THREE.Geometry();
        polygonGeom.vertices.push(
            new THREE.Vector3(vertex1X, vertex1Y),
            new THREE.Vector3(vertex2X, vertex2Y),
            new THREE.Vector3(vertex3X, vertex3Y),
            new THREE.Vector3(vertex4X, vertex4Y),
            new THREE.Vector3(vertex5X, vertex5Y),
            new THREE.Vector3(vertex6X, vertex6Y),
             new THREE.Vector3(vertex7X, vertex7Y),
            new THREE.Vector3(vertex1X, vertex1Y)
        );

        polygonMesh = new THREE.Line(polygonGeom, polygonMaterial);

    }

    if (displayval[i].NumberVertices == 8) {

        polygonMaterial = new THREE.LineBasicMaterial({ color: targetColor });

        polygonGeom = new THREE.Geometry();
        polygonGeom.vertices.push(
            new THREE.Vector3(vertex1X, vertex1Y),
            new THREE.Vector3(vertex2X, vertex2Y),
            new THREE.Vector3(vertex3X, vertex3Y),
            new THREE.Vector3(vertex4X, vertex4Y),
            new THREE.Vector3(vertex5X, vertex5Y),
            new THREE.Vector3(vertex6X, vertex6Y),
            new THREE.Vector3(vertex7X, vertex7Y),
            new THREE.Vector3(vertex8X, vertex8Y),
            new THREE.Vector3(vertex1X, vertex1Y)
        );

        polygonMesh = new THREE.Line(polygonGeom, polygonMaterial);

    }


    if (displayval[i].TargetOffsetXoffset > 0 || displayval[i].TargetOffsetYoffset > 0) {

        polygonMesh.position.set(polygonXOffset, -displayval[i].tvd, polygonYOffset);
    }
    else if (displayval[i].TargetOffsetXoffset < 0 || displayval[i].TargetOffsetYoffset < 0) {

        polygonMesh.position.set(polygonXOffset, -displayval[i].tvd, polygonYOffset);
    }
    else {
        polygonMesh.position.set(displayval[i].xcoordinate, -displayval[i].tvd, displayval[i].zcoordinate);
    }


    polygonGeom.applyMatrix(new THREE.Matrix4().makeTranslation(vertex1X / 2, vertex1Y / 2, 0));
    polygonMesh.name = "PolygonTarget";
    polygonMesh.rotation.x = Math.PI / -2;
    polygonMesh.rotation.z = (-polygonRotation) * (Math.PI / 180.00);

    // do some kind of check wheather to draw or not still need to do
    // if (displayval[i].DiameterOfCircleXoffset != 0 || displayval[i].DiameterOfCircleYoffset != 0) {
    scene.add(polygonMesh);
    //}


    renderer.render(scene, camera);

}

function DrawEllipseTarget(displayval, i) {

    ellipseXOffset = displayval[i].xcoordinate + displayval[i].TargetOffsetXoffset;
    ellipseYOffset = displayval[i].zcoordinate + displayval[i].TargetOffsetYoffset;

    ellipseXLength = displayval[i].DiameterOfCircleXoffset;
    ellipseYLength = displayval[i].DiameterOfCircleYoffset;
    ellipseRotation = displayval[i].Rotation;

    // Get Target name And Set To Green If well plan Target
    targetName = displayval[i].targetName;

    if (targetName == "Well Plan Target") {
        targetColor = 0x00CC00;
    }
    else {
        targetColor = 0xFF0000;
    }

    ellipseMaterial = new THREE.LineBasicMaterial({ color: targetColor });
    ellipse = new THREE.EllipseCurve(0, 0, ellipseXLength, ellipseYLength, 0, 2.0 * Math.PI, false);

    ellipsePath = new THREE.CurvePath();
    ellipsePath.add(ellipse);
    ellipseGeometry = ellipsePath.createPointsGeometry(100);
    ellipseGeometry.computeTangents();
    ellipseLine = new THREE.Line(ellipseGeometry, ellipseMaterial);

    if (displayval[i].TargetOffsetXoffset > 0 || displayval[i].TargetOffsetYoffset > 0) {

        ellipseLine.position.set(ellipseXOffset, -displayval[i].tvd, ellipseYOffset);
    }
    else if (displayval[i].TargetOffsetXoffset < 0 || displayval[i].TargetOffsetYoffset < 0) {

        ellipseLine.position.set(ellipseXOffset, -displayval[i].tvd, ellipseYOffset);
    }
    else {
        ellipseLine.position.set(displayval[i].xcoordinate, -displayval[i].tvd, displayval[i].zcoordinate);
    }

    ellipseLine.name = "EllipseTarget";
    ellipseLine.rotation.x = Math.PI / -2;
    ellipseLine.rotation.z = ellipseRotation * (Math.PI / 180.00);

    scene.add(ellipseLine);

    renderer.render(scene, camera);
}

function DrawPointTarget(displayval, i) {
    // Get Target name And Set To Green If well plan Target
    targetName = displayval[i].targetName;

    if (targetName == "Well Plan Target") {
        targetColor = 0x00CC00;
    }
    else {
        targetColor = 0xFF0000;
    }

    //Draw new circle Target 
    materialPoint = new THREE.MeshBasicMaterial({ color: targetColor, side: THREE.DoubleSide });
    radiusPoint = 15;

    segmentsPoint = 64;

    PointXOffset = displayval[i].xcoordinate + displayval[i].TargetOffsetXoffset;
    PointYOffset = displayval[i].zcoordinate + displayval[i].TargetOffsetYoffset;

    geometryPoint = new THREE.CircleGeometry(radiusPoint, segmentsPoint);
    PointMesh = new THREE.Mesh(geometryPoint, materialPoint);

    if (displayval[i].TargetOffsetXoffset > 0 || displayval[i].TargetOffsetYoffset > 0) {

        PointMesh.position.set(PointXOffset, -displayval[i].tvd, PointYOffset);
    }
    else if (displayval[i].TargetOffsetXoffset < 0 || displayval[i].TargetOffsetYoffset < 0) {

        PointMesh.position.set(PointXOffset, -displayval[i].tvd, PointYOffset);
    }
    else {
        PointMesh.position.set(displayval[i].xcoordinate, -displayval[i].tvd, displayval[i].zcoordinate);
    }

    PointMesh.name = "PointTarget";
    PointMesh.rotation.x = Math.PI / -2;
    scene.add(PointMesh);


    renderer.render(scene, camera);
}

function onWindowResize() {

    camera.aspect = GraphWidth / GraphHeight;
    camera.updateProjectionMatrix();

    renderer.setSize(window.innerWidth, window.innerHeight);

    render();
}

function animate() {

    requestAnimationFrame(animate);

    render();

    camera.lookAt(camera.position);
    //camera.position.update();

    TWEEN.update();
    //controls.update();
    update();
           

}

function render() {

    renderer.render(scene, camera);
}


// Jd for getting the mouse position to send ray collision from mouse
$(document).ready(function () {
    $("#Graphdiv").mousemove(function (e) {

        mouse.x = (e.offsetX / GraphWidth) * 2 - 1;
        mouse.y = -(e.offsetY / GraphHeight) * 2 + 1;

    });
});

function onDocumentMouseDown(event) {
    // find intersections

    // create a Ray with origin at the mouse position
    //   and direction into the scene (camera direction)
    var vector = new THREE.Vector3(mouse.x, mouse.y, 1);
    projector.unprojectVector(vector, camera);
    var ray = new THREE.Raycaster(camera.position, vector.sub(camera.position).normalize());

    // create an array containing all objects in the scene with which the ray intersects
    var intersects = ray.intersectObjects(scene.children);

    // INTERSECTED = the object in the scene currently closest to the camera 
    //		and intersected by the Ray projected from the mouse position 	

    // if there is one (or more) intersections
    if (intersects.length > 0) {
               
        // if the closest object intersected is not the currently stored intersection object
        if (intersects[0].object != INTERSECTEDclick) {

              
            // store reference to closest object as current intersection object
            INTERSECTEDclick = intersects[0].object;

            var parseArray = INTERSECTEDclick.name.split(/,/);

            var x = INTERSECTEDclick.position.x;
            var y = INTERSECTEDclick.position.y;
            var z = INTERSECTEDclick.position.z;

            if (parseArray[0] != "SurveyComment" && parseArray[0] != "zNegativeNumbers" && parseArray[0] != "zPostiveNumbers" &&
                parseArray[0] != "BigTVDNumbers" && parseArray[0] != "SmallTVDNumbers" && parseArray[0] != "xNegativeNumbers" &&
                parseArray[0] != "xPostiveNumbers" && parseArray[0] != "SurfaceGrid" && parseArray[0] != "BottomGrid" &&
                parseArray[0] != "BackGrid" && parseArray[0] != "ClickComment")
            {

                // for checking if the curves are clicked on
                if (parseArray[0] == parseArray[0]) {
                    ObjectClickPosition(parseArray[1], parseArray[2], parseArray[3]);

                }

                if (parseArray[0] == "CircleTarget" || parseArray[0] == "SquareTarget" || parseArray[0] == "RectangleTarget" ||
                    parseArray[0] == "PolygonTarget" || parseArray[0] == "EllipseTarget" || parseArray[0] == "PointTarget") {
                    ObjectClickPosition(x, y, z);
                }
                    
            }
              
        }

    }
    else // there are no intersections
    {
               
        INTERSECTEDclick = null;
    }

}

function update() {
    // find intersections

    // create a Ray with origin at the mouse position
    //   and direction into the scene (camera direction)
    var vector = new THREE.Vector3(mouse.x, mouse.y, 1);
    projector.unprojectVector(vector, camera);
    var ray = new THREE.Raycaster(camera.position, vector.sub(camera.position).normalize());

    // create an array containing all objects in the scene with which the ray intersects
    var intersects = ray.intersectObjects(scene.children);

    // INTERSECTED = the object in the scene currently closest to the camera 
    //		and intersected by the Ray projected from the mouse position 	


    // if there is one (or more) intersections
    if (intersects.length > 0) {
        // if the closest object intersected is not the currently stored intersection object
        if (intersects[0].object != INTERSECTED) {
            // restore previous intersection object (if it exists) to its original color
            if (INTERSECTED)
                INTERSECTED.material.color.setHex(INTERSECTED.currentHex);
            // store reference to closest object as current intersection object
            INTERSECTED = intersects[0].object;
            // store color of closest object (for later restoration)
            INTERSECTED.currentHex = INTERSECTED.material.color.getHex();
            // set a new color for closest object
            INTERSECTED.material.color.setHex(0xffff00);

        }
              
    }
    else // there are no intersections
    {
        // restore previous intersection object (if it exists) to its original color
        if (INTERSECTED)
            INTERSECTED.material.color.setHex(INTERSECTED.currentHex);
        // remove previous intersection object reference
        //     by setting current intersection object to "nothing"
        INTERSECTED = null;
    }

    controls.update();
            
}

function LastTarget() {


    controls.target = new THREE.Vector3(lastTargetX, lastTargetY, lastTargetZ);

    CameraPosition = camera.position;
    target = { x: lastTargetX, y: lastTargetY + 100, z: lastTargetZ + 1000 };
    tween = new TWEEN.Tween(CameraPosition).to(target, 3000);

    tween.onUpdate(function () {
        camera.position.x = CameraPosition.x;
        camera.position.y = CameraPosition.y;
        camera.position.z = CameraPosition.z;
    });

    // tween.delay(2000);
    tween.easing(TWEEN.Easing.Linear.None);

    tween.start();

}


function LastTargetDropDown() {


    controls.target = new THREE.Vector3(lastTargetXDropDown, lastTargetYDropDown, lastTargetZDropDown);

    CameraPosition = camera.position;
    target = { x: lastTargetXDropDown, y: lastTargetYDropDown + 100, z: lastTargetZDropDown + 1000 };
    tween = new TWEEN.Tween(CameraPosition).to(target, 3000);

    tween.onUpdate(function () {
        camera.position.x = CameraPosition.x;
        camera.position.y = CameraPosition.y;
        camera.position.z = CameraPosition.z;
    });

    // tween.delay(2000);
    tween.easing(TWEEN.Easing.Linear.None);

    tween.start();

}


function orgin() {


    controls.target = new THREE.Vector3(orginX, orginY, orginZ);

    CameraPosition = camera.position;
    target = { x: orginX, y: orginY + 100, z: orginZ + 1000 };
    tween = new TWEEN.Tween(CameraPosition).to(target, 3000);

    tween.onUpdate(function () {
        camera.position.x = CameraPosition.x;
        camera.position.y = CameraPosition.y;
        camera.position.z = CameraPosition.z;
    });

    // tween.delay(2000);
    tween.easing(TWEEN.Easing.Linear.None);

    tween.start();

}

function CameraInitFlickerFix() {

    init();// initialize the 3d graph


    // Camera start position so you dont see the black flicker, just sets the camera away and then below reloads the camera to look at the last targets position
    CameraPosition = { x: 0, y: 0, z: -20000 };
    // Camera create
    camera = new THREE.PerspectiveCamera(60, GraphWidth / GraphHeight, 1, 20000);
    camera.position.set(CameraPosition.x, CameraPosition.y, CameraPosition.z);
    scene.add(camera);

}

