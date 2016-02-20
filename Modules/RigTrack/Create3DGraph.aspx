<%@ Page Title="Create 3D Graph" Language="C#" MasterPageFile="~/Masters/3DGraph.master" AutoEventWireup="true" CodeFile="Create3DGraph.aspx.cs" Inherits="Modules_RigTrack_Create3DGraph" EnableEventValidation="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="customCss" runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">



    <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
        <script type="text/javascript">
           //Variable used for drawing comment offsets
            var commentX = -1;
            var commentCount = 0;
            function Close() {
                commentCount = 0; commentX = -1;
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

                if (redrawPressed == false) {

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

            function UpdateCurveColor() {

                var ddl = $find('<%=ddlCurveColor.ClientID%>');
                var colorPicker = $find('<%=RadColorPickerCurve.ClientID%>');

                var curveID = ddl.get_selectedItem().get_value();
                var curveColor = colorPicker.get_selectedColor();

                $.ajax({
                    type: "POST",
                    url: 'Create3DGraph.aspx/UpdateCurveColor',
                    data: "{'curveID':'" + curveID + "'," + "'curveColor':'" + curveColor + "'}",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function () {
                        getValue();
                        },
                    failure: function () {
                    }
                });
            }

            function EnableDisableRedraw() {
                var checked = false;
                var combo = $find('<%=ddlCurve.ClientID %>');
                //var button = $find('<%=btnRedraw.ClientID %>');
                for (var i = 0; i < combo.get_items().get_count() ; i++) {
                    if (combo.get_items().getItem(i).get_checked()) {
                        checked = true;
                    }
                }
            }

            function getValue() {
                redrawPressed = true;// red draw button pressed used for reload button, reload curve group or curves
                var curveName = "Curve Name: ";
                var combo = $find('<%=ddlCurve.ClientID %>');
                var returnList = [];
                var colorList = [];
                for (var i = 0; i < combo.get_items().get_count() ; i++) {
                    var object = {};
                    if (combo.get_items().getItem(i).get_checked()) {
                        object['ID'] = combo.get_items().getItem(i).get_value();
                        object['Name'] = combo.get_items().getItem(i).get_text();
                        curveName = curveName + combo.get_items().getItem(i).get_text() + ', ';
                        returnList.push(object);
                       
                    }

                }
                document.getElementById('<%=Master.FindControl("lblCurveName").ClientID %>').innerText = curveName.substring(0, curveName.length - 2);
                GraphSelectedCurves(returnList);
                bindTargetsCurves(returnList);

                return redrawPressed;
            }


            function CompanyChange(sender, eventArgs) {
                document.getElementById('CurveColor').innerHTML = null;
                commentCount = 0; commentX = -1;
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
                        url: 'Create3DGraph.aspx/BindAllCurveGroups',
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
                        url: 'Create3DGraph.aspx/BindCurveGroupsForCompany',
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



            // jd comes from the 2d graph 3d button 
            function OnClientItemSelected2(curveGroupID) {
                commentCount = 0; commentX = -1;
                $('#Graphdiv').empty();
                var hiddenField = document.getElementById('<%= hiddenField.ClientID %>');
                hiddenField.value = curveGroupID;
                document.getElementById('CurveColor').innerHTML = null;

                PopulateCurveDropdown(curveGroupID);

                $.ajax({
                    type: "POST",
                    url: 'Create3DGraph.aspx/GetGraphDetails',
                    data: '{curvegrpID: "' + curveGroupID + '" }',
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (data) {

                       var displayval = data.d;

                        // Initialize camera away from the actually scene to get rid of the black flicker 
                       CameraInitFlickerFix();

                        var lastTarget;
                        for (var t = 0; t < displayval.length; t++) {

                            if (displayval.length > 1) {

                                if (t > 0) {
                                    if (displayval[0].tvd > displayval[t].tvd) {
                                        lastTarget = 0;

                                    }

                                    else if (displayval[t].tvd > displayval[t - 1].tvd) {

                                        lastTarget = t;
                                    }

                                    else {
                                        lastTarget = 0;
                                    }
                                }
                            }

                            else {
                                lastTarget = t;
                            }

                        }

                        lastTargetX = displayval[lastTarget].xcoordinate;
                        lastTargetY = -displayval[lastTarget].tvd;
                        lastTargetZ = displayval[lastTarget].zcoordinate;



                        $.ajax({
                            type: "POST",
                            url: 'Create3DGraph.aspx/GetSurveyDetails',
                            data: '{curvegrpID: "' + curveGroupID + '"}',//item.get_value() + '"}',
                            contentType: "application/json; charset=utf-8",
                            dataType: "json",
                            success: function (data) {

                                var displayvalSurvey = data.d;// used for getting the survey information from the database

                                orginX = displayvalSurvey[0].VerticalSection;
                                orginY = -displayvalSurvey[0].TVD;
                                orginZ = displayvalSurvey[0].NS;


                                // Camera start position
                                CameraPosition = { x: displayvalSurvey[0].VerticalSection + 350, y: -displayvalSurvey[0].TVD + 4000, z: displayvalSurvey[0].NS + 9000 };
                                // Camera create
                                camera = new THREE.PerspectiveCamera(60, GraphWidth / GraphHeight, 1, 20000);
                                camera.position.set(CameraPosition.x, CameraPosition.y, CameraPosition.z);
                                scene.add(camera);

                                //controls orbit / and camera look at last target
                                controls = new THREE.OrbitControls(camera, displayval[lastTarget].xcoordinate, -displayval[lastTarget].tvd, displayval[lastTarget].zcoordinate, renderer.domElement);
                                // controls.target(new three.Vector3(displayval[lastTarget].xcoordinate, -displayval[lastTarget].tvd, displayval[lastTarget].zcoordinate));
                                controls.damping = 0.2;
                                controls.addEventListener('change', render);

                                animate();

                                var counter = 0;
                                for (s = 0; s < displayvalSurvey.length; s++) {
                                    var prev;
                                    if (s != 0) {
                                        prev = s - 1;

                                    }

                                    else {
                                        prev = s;
                                    }

                                    var CurveColor = displayvalSurvey[s].CurveColor;

                                    //create survey curve example cyclinder
                                    var numPoints = 50;
                                    var start = new THREE.Vector3(displayvalSurvey[prev].VerticalSection, -displayvalSurvey[prev].TVD, displayvalSurvey[prev].NS);
                                    var middle = new THREE.Vector3(displayvalSurvey[prev].VerticalSection, -displayvalSurvey[prev].TVD, displayvalSurvey[prev].NS);
                                    var end = new THREE.Vector3(displayvalSurvey[s].VerticalSection, -displayvalSurvey[s].TVD, displayvalSurvey[s].NS);

                                    var curveQuad = new THREE.QuadraticBezierCurve3(start, middle, end);
                                    var tube = new THREE.TubeGeometry(curveQuad, numPoints, 11, 20, false);
                                    var mesh = new THREE.Mesh(tube, new THREE.MeshPhongMaterial({ color: CurveColor }));

                                    if (displayvalSurvey[s].RowNumber != 0) {
                                        counter = counter + 1;

                                        mesh.name = 'Survey' + counter + "," + displayvalSurvey[s].VerticalSection + "," + -displayvalSurvey[s].TVD + "," + displayvalSurvey[s].NS;
                                        scene.add(mesh);
                                    }

                                    DrawSurveyComments(displayvalSurvey[s].SurveyComment, displayvalSurvey[s].VerticalSection, -displayvalSurvey[s].TVD, displayvalSurvey[prev].NS);
                                }


                                DrawSurfaceGrid(displayval, lastTarget);

                                return orginX, orginY, orginZ;

                            },
                            failure: function (data) {

                            }
                        });


                        for (i = 0; i < displayval.length; i++) {

                            var splitval = displayval[i].shape;


                            if (splitval == "1000") {

                                if (displayval[i] === displayval[lastTarget]) {

                                    DrawGridsAndNumbers(displayval, lastTarget);
                                }

                                DrawCircleTarget(displayval, i);
                            }
                            else if (splitval == "1001") {

                                if (displayval[i] === displayval[lastTarget]) {

                                    DrawGridsAndNumbers(displayval, lastTarget);
                                }

                                DrawSquareTarget(displayval, i);
                            }

                            else if (splitval == "1002") {

                                if (displayval[i] === displayval[lastTarget]) {

                                    DrawGridsAndNumbers(displayval, lastTarget);
                                }

                                DrawRectangleTarget(displayval, i);
                            }



                            else if (splitval == "1003") {

                                if (displayval[i] === displayval[lastTarget]) {

                                    DrawGridsAndNumbers(displayval, lastTarget);
                                }

                                DrawPolygonTarget(displayval, i);
                            }


                            else if (splitval == "1004") {

                                if (displayval[i] === displayval[lastTarget]) {

                                    DrawGridsAndNumbers(displayval, lastTarget);
                                }

                                DrawEllipseTarget(displayval, i);
                            }

                            else if (splitval == "1005") {

                                if (displayval[i] === displayval[lastTarget]) {

                                    DrawGridsAndNumbers(displayval, lastTarget);
                                }

                                DrawPointTarget(displayval, i);
                            }
                        }


                        return lastTargetX, lastTargetY, lastTargetZ;


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
                commentCount = 0; commentX = -1;
                redrawPressed = false;
                var item = eventArgs.get_item();
                $('#Graphdiv').empty();
                document.getElementById('CurveColor').innerHTML =null;

                // if not select or so 0
                if (item.get_value() != 0)
                {

                    PopulateCurveDropdown(item.get_value());

                    bindTargets(item.get_value());

                    var hiddenField = document.getElementById('<%= hiddenField.ClientID %>');
                    hiddenField.value = item.get_value();

                if (item.get_value() == 0) {

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
                    return false;
                }

                $.ajax({
                    type: "POST",
                    url: 'Create3DGraph.aspx/GetReport',
                    data: '{curveGroupID: "' + item.get_value() + '"}',
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

                        var curveName = "Curve Name: ";
                        var combo = $find('<%=ddlCurve.ClientID %>');
                        for (var i = 0; i < combo.get_items().get_count() ; i++) {
                            curveName = curveName + combo.get_items().getItem(i).get_text() + ', ';
                        }
                        document.getElementById('<%=Master.FindControl("lblCurveName").ClientID %>').innerText = curveName.substring(0, curveName.length - 2);

                    },
                    failure: function (data) {
                    }

                });



                $.ajax({
                    type: "POST",
                    url: 'Create3DGraph.aspx/GetGraphDetails',
                    data: '{curvegrpID: "' + item.get_value() + '" }',
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (data) {

                        var displayval = data.d;

                        // Initialize camera away from the actually scene to get rid of the black flicker 
                        CameraInitFlickerFix();

                        var lastTarget;
                        for (var t = 0; t < displayval.length; t++) {

                            if (displayval.length > 1) {

                                if (t > 0) {
                                    if (displayval[0].tvd > displayval[t].tvd) {
                                        lastTarget = 0;

                                    }

                                    else if (displayval[t].tvd > displayval[t - 1].tvd) {

                                        lastTarget = t;
                                    }

                                    else {
                                        lastTarget = 0;
                                    }
                                }
                            }

                            else {
                                lastTarget = t;
                            }

                        }



                        //  Zooming variables for zooming into target
                        lastTargetX = displayval[lastTarget].xcoordinate;
                        lastTargetY = -displayval[lastTarget].tvd;
                        lastTargetZ = displayval[lastTarget].zcoordinate;


                        $.ajax({
                            type: "POST",
                            url: 'Create3DGraph.aspx/GetSurveyDetails',
                            data: '{curvegrpID: "' + item.get_value() + '"}',
                            contentType: "application/json; charset=utf-8",
                            dataType: "json",
                            success: function (data) {

                          
                                var displayvalSurvey = data.d;// used for getting the survey information from the database

                                orginX = displayvalSurvey[0].VerticalSection;
                                orginY = -displayvalSurvey[0].TVD;
                                orginZ = displayvalSurvey[0].NS;

                              
                                // Camera start position
                                CameraPosition = { x: displayvalSurvey[0].VerticalSection + 350, y: -displayvalSurvey[0].TVD + 4000, z: displayvalSurvey[0].NS + 9000 };
                                // Camera create
                                camera = new THREE.PerspectiveCamera(60, GraphWidth / GraphHeight, 1, 20000);
                                camera.position.set(CameraPosition.x, CameraPosition.y, CameraPosition.z);
                                scene.add(camera);

                                //controls orbit / and camera look at last target
                                controls = new THREE.OrbitControls(camera, displayval[lastTarget].xcoordinate, -displayval[lastTarget].tvd, displayval[lastTarget].zcoordinate, renderer.domElement);
                                controls.damping = 0.2;
                                controls.addEventListener('change', render);

                                animate();

                                var counter = 0;
                                for (s = 0; s < displayvalSurvey.length; s++) {
                                    var prev;
                                    if (s != 0) {
                                        prev = s - 1;

                                    }

                                    else {
                                        prev = s;
                                    }


                                    var CurveColor = displayvalSurvey[s].CurveColor;

                                    //create survey curve example cyclinder
                                    var numPoints = 50;
                                    var start = new THREE.Vector3(displayvalSurvey[prev].VerticalSection, -displayvalSurvey[prev].TVD, displayvalSurvey[prev].NS);
                                    var middle = new THREE.Vector3(displayvalSurvey[prev].VerticalSection, -displayvalSurvey[prev].TVD, displayvalSurvey[prev].NS);
                                    var end = new THREE.Vector3(displayvalSurvey[s].VerticalSection, -displayvalSurvey[s].TVD, displayvalSurvey[s].NS);

                                    var curveQuad = new THREE.QuadraticBezierCurve3(start, middle, end);
                                    var tube = new THREE.TubeGeometry(curveQuad, numPoints, 11, 20, false);
                                    var mesh = new THREE.Mesh(tube, new THREE.MeshPhongMaterial({ color: CurveColor }));
                                    

                                    if (displayvalSurvey[s].RowNumber != 0) {

                                        counter = counter + 1;
                                        mesh.name = 'Survey' + counter + "," + displayvalSurvey[s].VerticalSection + "," + -displayvalSurvey[s].TVD + "," + displayvalSurvey[s].NS;
                                        scene.add(mesh);
                                    }

                                    DrawSurveyComments(displayvalSurvey[s].SurveyComment, displayvalSurvey[s].VerticalSection, -displayvalSurvey[s].TVD, displayvalSurvey[prev].NS);

                                }


                                DrawSurfaceGrid(displayval, lastTarget);

                                return orginX, orginY, orginZ;


                            },
                            failure: function (data) {

                            }
                        });


                        for (i = 0; i < displayval.length; i++) {

                            var splitval = displayval[i].shape;

                            if (splitval == "1000") {

                                if (displayval[i] === displayval[lastTarget]) {

                                    DrawGridsAndNumbers(displayval, lastTarget);
                                }
                               
                                DrawCircleTarget(displayval, i);

                            }
                            else if (splitval == "1001") {

                                if (displayval[i] === displayval[lastTarget]) {

                                    DrawGridsAndNumbers(displayval,lastTarget);
                                }

                                DrawSquareTarget(displayval, i);
                               
                            }

                            else if (splitval == "1002") {

                                if (displayval[i] === displayval[lastTarget]) {

                                    DrawGridsAndNumbers(displayval, lastTarget);
                                }

                                DrawRectangleTarget(displayval, i);
                     
                            }


                            else if (splitval == "1003") {

                                if (displayval[i] === displayval[lastTarget]) {

                                    DrawGridsAndNumbers(displayval, lastTarget);
                                }

                                DrawPolygonTarget(displayval, i);
                            }


                            else if (splitval == "1004") {

                                if (displayval[i] === displayval[lastTarget]) {

                                    DrawGridsAndNumbers(displayval, lastTarget);

                                }

                                DrawEllipseTarget(displayval, i); 
                            }

                            else if (splitval == "1005") {

                                if (displayval[i] === displayval[lastTarget]) {

                                    DrawGridsAndNumbers(displayval, lastTarget);
                                }

                                DrawPointTarget(displayval, i);
                            }
                        }


                        return lastTargetX, lastTargetY, lastTargetZ;

                    },
                    failure: function (response) {
                        var r = jQuery.parseJSON(response.responseText);
                        alert("Message: " + r.Message);
                        alert("StackTrace: " + r.StackTrace);
                        alert("ExceptionType: " + r.ExceptionType);
                    }
                });

                return redrawPressed;
            }// end if item i s not 0 or so select
            }

            // jd Reload Graph Button Press
            function ReloadGraph() 
            {
                commentCount = 0; commentX = -1;
                var raddropdownlist = $find('<%=ddlCurveGroup.ClientID %>');
                var selecteditem = raddropdownlist.get_selectedItem().get_value();


                if (redrawPressed == true) {
                    var combo = $find('<%=ddlCurve.ClientID %>');
                    var returnList = [];
                    for (var i = 0; i < combo.get_items().get_count() ; i++) {
                        var object = {};
                        if (combo.get_items().getItem(i).get_checked()) {
                            object['ID'] = combo.get_items().getItem(i).get_value();
                            returnList.push(object);
                        }

                    }
                    GraphSelectedCurves(returnList);
                    
                }
           
                else
                {
                    $('#Graphdiv').empty();
                    $.ajax({
                        type: "POST",
                        url: 'Create3DGraph.aspx/GetGraphDetails',
                        data: '{curvegrpID: "' + selecteditem + '" }',
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (data) {

                            var displayval = data.d;

                            // Initialize camera away from the actually scene to get rid of the black flicker 
                            CameraInitFlickerFix();

                            var lastTarget;
                            for (var t = 0; t < displayval.length; t++) {

                                if (displayval.length > 1) {

                                    if (t > 0) {
                                        if (displayval[0].tvd > displayval[t].tvd) {
                                            lastTarget = 0;

                                        }

                                        else if (displayval[t].tvd > displayval[t - 1].tvd) {

                                            lastTarget = t;
                                        }

                                        else {
                                            lastTarget = 0;
                                        }
                                    }
                                }

                                else {
                                    lastTarget = t;
                                }

                            }

                            lastTargetX = displayval[lastTarget].xcoordinate;
                            lastTargetY = -displayval[lastTarget].tvd;
                            lastTargetZ = displayval[lastTarget].zcoordinate;



                            $.ajax({
                                type: "POST",
                                url: 'Create3DGraph.aspx/GetSurveyDetails',
                                data: '{curvegrpID: "' + selecteditem + '"}',
                                contentType: "application/json; charset=utf-8",
                                dataType: "json",
                                success: function (data) {

                                    var displayvalSurvey = data.d;// used for getting the survey information from the database

                                    orginX = displayvalSurvey[0].VerticalSection;
                                    orginY = -displayvalSurvey[0].TVD;
                                    orginZ = displayvalSurvey[0].NS;



                                    // Camera start position
                                    CameraPosition = { x: displayvalSurvey[0].VerticalSection + 350, y: -displayvalSurvey[0].TVD + 4000, z: displayvalSurvey[0].NS + 9000 };
                                    // Camera create
                                    camera = new THREE.PerspectiveCamera(60, GraphWidth / GraphHeight, 1, 20000);
                                    camera.position.set(CameraPosition.x, CameraPosition.y, CameraPosition.z);
                                    scene.add(camera);

                                    //controls orbit / and camera look at last target
                                    controls = new THREE.OrbitControls(camera, displayval[lastTarget].xcoordinate, -displayval[lastTarget].tvd, displayval[lastTarget].zcoordinate, renderer.domElement);
                                    // controls.target(new three.Vector3(displayval[lastTarget].xcoordinate, -displayval[lastTarget].tvd, displayval[lastTarget].zcoordinate));
                                    controls.damping = 0.2;
                                    controls.addEventListener('change', render);


                                    animate();

                                    var counter = 0;
                                    for (s = 0; s < displayvalSurvey.length; s++) {
                                        var prev;
                                        if (s != 0) {
                                            prev = s - 1;

                                        }

                                        else {
                                            prev = s;
                                        }

                                        var CurveColor = displayvalSurvey[s].CurveColor;
                                        
                                        //create survey curve example cyclinder
                                        var numPoints = 50;
                                        var start = new THREE.Vector3(displayvalSurvey[prev].VerticalSection, -displayvalSurvey[prev].TVD, displayvalSurvey[prev].NS);
                                        var middle = new THREE.Vector3(displayvalSurvey[prev].VerticalSection, -displayvalSurvey[prev].TVD, displayvalSurvey[prev].NS);
                                        var end = new THREE.Vector3(displayvalSurvey[s].VerticalSection, -displayvalSurvey[s].TVD, displayvalSurvey[s].NS);

                                        var curveQuad = new THREE.QuadraticBezierCurve3(start, middle, end);
                                        var tube = new THREE.TubeGeometry(curveQuad, numPoints, 11, 20, false);
                                        var mesh = new THREE.Mesh(tube, new THREE.MeshPhongMaterial({ color: CurveColor }));

                                        if (displayvalSurvey[s].RowNumber != 0) {

                                            counter = counter + 1;
                                            mesh.name = 'Survey' + counter + "," + displayvalSurvey[s].VerticalSection + "," + -displayvalSurvey[s].TVD + "," + displayvalSurvey[s].NS;
                                            scene.add(mesh);
                                        }

                                        DrawSurveyComments(displayvalSurvey[s].SurveyComment, displayvalSurvey[s].VerticalSection, -displayvalSurvey[s].TVD, displayvalSurvey[prev].NS);
                                    }


                                    DrawSurfaceGrid(displayval, lastTarget);

                                    return orginX, orginY, orginZ;
                                },
                                failure: function (data) {

                                }
                            });


                            for (i = 0; i < displayval.length; i++) {

                                var splitval = displayval[i].shape;


                                if (splitval == "1000") {

                                    if (displayval[i] === displayval[lastTarget]) {

                                        DrawGridsAndNumbers(displayval, lastTarget);
                                    }

                                    DrawCircleTarget(displayval, i);

                                }
                                else if (splitval == "1001") {

                                    if (displayval[i] === displayval[lastTarget]) {

                                        DrawGridsAndNumbers(displayval, lastTarget);
                                    }

                                    DrawSquareTarget(displayval, i);
                                }

                                else if (splitval == "1002") {

                                    if (displayval[i] === displayval[lastTarget]) {

                                        DrawGridsAndNumbers(displayval, lastTarget);
                                    }

                                    DrawRectangleTarget(displayval, i);
                                }



                                else if (splitval == "1003") {

                                    if (displayval[i] === displayval[lastTarget]) {

                                        DrawGridsAndNumbers(displayval, lastTarget);
                                    }

                                    DrawPolygonTarget(displayval, i);
                                }


                                else if (splitval == "1004") {

                                    if (displayval[i] === displayval[lastTarget]) {

                                        DrawGridsAndNumbers(displayval, lastTarget);
                                    }

                                    DrawEllipseTarget(displayval, i);
                                }

                                else if (splitval == "1005") {

                                    if (displayval[i] === displayval[lastTarget]) {

                                        DrawGridsAndNumbers(displayval, lastTarget);
                                    }

                                    DrawPointTarget(displayval, i);
                                }
                            }

                            return lastTargetX, lastTargetY, lastTargetZ;
                        },
                        failure: function (response) {
                            var r = jQuery.parseJSON(response.responseText);
                            alert("Message: " + r.Message);
                            alert("StackTrace: " + r.StackTrace);
                            alert("ExceptionType: " + r.ExceptionType);
                        }
                    });

                } // end else 

                
            }

            function bindTargets(ddlvalue) {
                var radCombo = $find('<%=ddlTargets.ClientID %>');


                $.ajax({
                    type: "POST",
                    url: 'Create3DGraph.aspx/GetTargetDetails',
                    data: '{curvegrpID: "' + ddlvalue + '" }',
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (data) {

                        var items = data.d;

                        radCombo.get_items().clear();
                        var select = new Telerik.Web.UI.DropDownListItem;
                        select.set_text("-Select-");
                        select.set_value("0");
                        radCombo.get_items().add(select);
                        select.select();


                        for (var i = 0; i < items.length; i++) {
                            var item = items[i];

                            var comboItem = new Telerik.Web.UI.DropDownListItem();
                            comboItem.set_text(item.Text);
                            comboItem.set_value(item.Value);
                            radCombo.get_items().add(comboItem);

                        }
                        radCombo.commitChanges();
                    }
                });
            }

            function bindTargetsCurves(returnList) {

                var curveID = '';
                for (var i = 0; i < returnList.length; i++) {
                    if (i > 0) {

                        curveID = curveID + ',' + returnList[i]['ID'];
                    }
                    else {
                        curveID = returnList[i]['ID'];
                    }
                }
                var radCombo = $find('<%=ddlTargets.ClientID %>');


                if (curveID != '') {

                    $.ajax({
                        type: "POST",
                        url: 'Create3DGraph.aspx/GetTargetDetailsForAllCurves',
                        data: '{curveID: "' + curveID + '" }',
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (data) {

                            var items = data.d;

                            radCombo.get_items().clear();
                            var select = new Telerik.Web.UI.DropDownListItem;
                            select.set_text("-Select-");
                            select.set_value("0");
                            radCombo.get_items().add(select);
                            select.select();


                            for (var i = 0; i < items.length; i++) {
                                var item = items[i];

                                var comboItem = new Telerik.Web.UI.DropDownListItem();
                                comboItem.set_text(item.Text);
                                comboItem.set_value(item.Value);
                                radCombo.get_items().add(comboItem);

                            }
                            radCombo.commitChanges();
                        }
                    });
                }
             }


            //jd for drawing curves survey data from selecting curve then press redraw
            function GraphSelectedCurves(returnList) {
                $('#Graphdiv').empty();
                commentX = -1;
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
                        url: 'Create3DGraph.aspx/GetTargetGraphDetails',
                        data: '{curveID: "' + curveID + '" }',
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (data) {

                            var displayval = data.d;

                            // Initialize camera away from the actually scene to get rid of the black flicker 
                            CameraInitFlickerFix();

                            var lastTarget = 0;
                            for (var t = 0; t < displayval.length; t++) {


                                if (t > 0) {
                                    if (displayval[t].tvd > displayval[t-1].tvd) {
                                        lastTarget = t;

                                    }
                                    else {
                                        lastTarget = t - 1;
                                    }
                                }

                                else {

                                    lastTarget = t;
                                }

                            }
                          
                            lastTargetX = displayval[lastTarget].xcoordinate;
                            lastTargetY = -displayval[lastTarget].tvd;
                            lastTargetZ = displayval[lastTarget].zcoordinate;



                            $.ajax({
                                type: "POST",
                                url: 'Create3DGraph.aspx/GetGraphDetailsPerCurves',
                                data: '{curves: "' + curveID + '"}',
                                contentType: "application/json; charset=utf-8",
                                dataType: "json",
                                success: function (data) {

                                

                                    var displayvalSurvey = data.d;// used for getting the survey information from the database

                                    orginX = displayvalSurvey[0].VerticalSection;
                                    orginY = -displayvalSurvey[0].TVD;
                                    orginZ = displayvalSurvey[0].NS;


                                    // Camera start position
                                    CameraPosition = { x: displayvalSurvey[0].VerticalSection + 350, y: -displayvalSurvey[0].TVD + 4000, z: displayvalSurvey[0].NS + 9000 };
                                    // Camera create
                                    camera = new THREE.PerspectiveCamera(60, GraphWidth / GraphHeight, 1, 20000);
                                    camera.position.set(CameraPosition.x, CameraPosition.y, CameraPosition.z);
                                    scene.add(camera);

                                    //controls orbit / and camera look at last target
                                    controls = new THREE.OrbitControls(camera, displayval[lastTarget].xcoordinate, -displayval[lastTarget].tvd, displayval[lastTarget].zcoordinate, renderer.domElement);
                                    controls.damping = 0.2;
                                    controls.addEventListener('change', render);

                                    animate();
                                 

                                   
                                    var newColor = displayvalSurvey[0].CurveColor; 
                                    

                                    currentCurveID = displayvalSurvey[0].CurveID;
                                    var currentCurveName = displayvalSurvey[0].Name;
                                    var firstPanel = document.getElementById('CurveColor');
                                    var html = "Curve Name/Colors ";
                                    firstPanel.innerHTML = html;
                                    firstPanel.innerHTML += '<label ID="curve' + currentCurveID + '"/><br />';
                                    var newLabel = document.getElementById('curve' + currentCurveID);
                                    newLabel.innerHTML = currentCurveName;
                                    newLabel.style.color = newColor;
                                    firstPanel.innerHTML += ', &nbsp;';
                                    
                                 
                                    var prevCruveID;
                                    var counter = 0;
                                    for (s = 0; s < displayvalSurvey.length; s++) {
                                      
                                        var prev;
                                        if (s != 0) {
                                            prev = s - 1;

                                        }

                                        else {
                                            prev = s;
                                        }

                                        
                                        
                                        if (displayvalSurvey[s].CurveID != currentCurveID) {
                                            currentCurveID = displayvalSurvey[s].CurveID;
                                            currentCurveName = displayvalSurvey[s].Name;
                                            newColor = displayvalSurvey[s].CurveColor;//'#' + Math.floor(Math.random() * 16777215).toString(16);
                                            
                                           
                                            var panel = document.getElementById('CurveColor');
                                            
                                            panel.innerHTML +=  ' <label ID="curve' + currentCurveID + '"/><br />'
                                            newLabel = document.getElementById('curve' + currentCurveID);
                                            newLabel.innerHTML = currentCurveName;
                                            newLabel.style.color = newColor;
                                            panel.innerHTML += ', &nbsp;';
                                             
                                        }
                                            

                                        //create survey curve example cyclinder
                                        var numPoints = 50;
                                        var start = new THREE.Vector3(displayvalSurvey[prev].VerticalSection, -displayvalSurvey[prev].TVD, displayvalSurvey[prev].NS);
                                        var middle = new THREE.Vector3(displayvalSurvey[prev].VerticalSection, -displayvalSurvey[prev].TVD, displayvalSurvey[prev].NS);
                                        var end = new THREE.Vector3(displayvalSurvey[s].VerticalSection, -displayvalSurvey[s].TVD, displayvalSurvey[s].NS);

                                        var curveQuad = new THREE.QuadraticBezierCurve3(start, middle, end);
                                        var tube = new THREE.TubeGeometry(curveQuad, numPoints, 11, 20, false);
                                        var mesh = new THREE.Mesh(tube, new THREE.MeshPhongMaterial({ color: newColor }));
                                    
                                        if (displayvalSurvey[s].RowNumber != 0) {
                                            counter = counter + 1;
                                            mesh.name = 'Survey' + counter + "," + displayvalSurvey[s].VerticalSection + "," + -displayvalSurvey[s].TVD + "," + displayvalSurvey[s].NS;
                                            scene.add(mesh);
                                        }

                                        DrawSurveyComments(displayvalSurvey[s].SurveyComment, displayvalSurvey[s].VerticalSection, -displayvalSurvey[s].TVD, displayvalSurvey[prev].NS);

                                    }


                                    DrawSurfaceGrid(displayval, lastTarget);

                                    return orginX, orginY, orginZ, currentCurveID;


                                },
                                failure: function (data) {

                                }
                            });


                            for (i = 0; i < displayval.length; i++) {

                                var splitval = displayval[i].shape;

                                if (splitval == "1000") {

                                    if (displayval[i] === displayval[lastTarget]) {

                                        DrawGridsAndNumbers(displayval, lastTarget);
                                    }

                                    DrawCircleTarget(displayval, i);

                                }
                                else if (splitval == "1001") {

                                    if (displayval[i] === displayval[lastTarget]) {

                                        DrawGridsAndNumbers(displayval, lastTarget);
                                    }

                                    DrawSquareTarget(displayval, i);
                                 
                                }

                                else if (splitval == "1002") {

                                    if (displayval[i] === displayval[lastTarget]) {

                                        DrawGridsAndNumbers(displayval, lastTarget);
                                    }

                                    DrawRectangleTarget(displayval, i);
                                }

                                else if (splitval == "1003") {

                                    if (displayval[i] === displayval[lastTarget]) {

                                        DrawGridsAndNumbers(displayval, lastTarget);
                                    }

                                    DrawPolygonTarget(displayval, i);
                                }


                                else if (splitval == "1004") {

                                    if (displayval[i] === displayval[lastTarget]) {

                                        DrawGridsAndNumbers(displayval, lastTarget);
                                    }

                                    DrawEllipseTarget(displayval, i);
                                }

                                else if (splitval == "1005") {

                                    if (displayval[i] === displayval[lastTarget]) {

                                        DrawGridsAndNumbers(displayval, lastTarget);
                                    }

                                    DrawPointTarget(displayval, i);
                                }
                            }

                           
                            return lastTargetX, lastTargetY, lastTargetZ;

                            

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



            // jd for zooming into target selected from drop down
            function OnClientItemSelectedTargets(sender, eventArgs) {

                var item = eventArgs.get_item();
                var combo = $find('<%= ddlCurveGroup.ClientID %>');
                var selecteditem = combo.get_selectedItem().get_value();

                $.ajax({
                    type: "POST",
                    url: 'CreateGraph.aspx/GetGraphDetailsOnTargetID',
                    data: '{targetID: "' + item.get_value() + '",curveID:"' + selecteditem + '" }',
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (data) {

                        var displayval = data.d;



                        for (i = 0; i < displayval.length; i++) {

                            var splitval = displayval[i].shape;


                            if (splitval == "1000") {
                                lastTargetXDropDown = displayval[i].xcoordinate;
                                lastTargetYDropDown = -displayval[i].tvd;
                                lastTargetZDropDown = displayval[i].zcoordinate;

                                LastTargetDropDown();

                            }
                            else if (splitval == "1001") {
                                lastTargetXDropDown = displayval[i].xcoordinate;
                                lastTargetYDropDown = -displayval[i].tvd;
                                lastTargetZDropDown = displayval[i].zcoordinate;

                                LastTargetDropDown();

                            }

                            else if (splitval == "1002") {

                                lastTargetXDropDown = displayval[i].xcoordinate;
                                lastTargetYDropDown = -displayval[i].tvd;
                                lastTargetZDropDown = displayval[i].zcoordinate;

                                LastTargetDropDown();

                            }

                            else if (splitval == "1003") {

                                lastTargetXDropDown = displayval[i].xcoordinate;
                                lastTargetYDropDown = -displayval[i].tvd;
                                lastTargetZDropDown = displayval[i].zcoordinate;

                                LastTargetDropDown();
                            }


                            else if (splitval == "1004") {

                                lastTargetXDropDown = displayval[i].xcoordinate;
                                lastTargetYDropDown = -displayval[i].tvd;
                                lastTargetZDropDown = displayval[i].zcoordinate;

                                LastTargetDropDown();

                            }

                            else if (splitval == "1005") {

                                lastTargetXDropDown = displayval[i].xcoordinate;
                                lastTargetYDropDown = -displayval[i].tvd;
                                lastTargetZDropDown = displayval[i].zcoordinate;

                                LastTargetDropDown();

                            }
                        }


                        return lastTargetXDropDown, lastTargetYDropDown, lastTargetZDropDown;

                    },
                    failure: function (response) {
                        var r = jQuery.parseJSON(response.responseText);
                        alert("Message: " + r.Message);
                        alert("StackTrace: " + r.StackTrace);
                        alert("ExceptionType: " + r.ExceptionType);
                    }
                });
            }


            function PopulateCurveDropdown(item) {
                var curveName = "Curve Name: ";

                var CurveGroupID = item;
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
                            var ddlCurveColor = $find("<%= ddlCurveColor.ClientID %>");
                             combo.enable();
                             combo.clearItems();
                             ddlCurveColor.get_items().clear();
                             for (var i = 0; i < curves.length; i++) {
                                 var comboItem = new Telerik.Web.UI.RadComboBoxItem();
                                 var curveItem = new Telerik.Web.UI.DropDownListItem;
                                 curveItem.set_text(curves[i].Name);
                                 curveItem.set_value(curves[i].ID);
                                 comboItem.set_text(curves[i].Name);
                                 comboItem.set_value(curves[i].ID);
                                 curveName = curveName + curves[i].Name + ', ';
                                 combo.get_items().add(comboItem);
                                 ddlCurveColor.get_items().add(curveItem);
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

                             document.getElementById('<%=Master.FindControl("lblCurveName").ClientID %>').innerText = curveName.substring(0, curveName.length - 2);
                             //combo.commitChanges();
                         }
                     });

                 }
                 else {
                     var combo = $find("<%= ddlCurve.ClientID %>");
                     combo.disable();
                 }
             }


        </script>
    </telerik:RadCodeBlock>

   <%-- // jd This has All The Draw Calls And Init, Every thing Graph Related Except Control Calls Like Text Boxes, DropDowns, Combo Boxes etc.--%>
    <script type="text/javascript" src="../../js/3DGraph/3DGraphMain.js"></script>

    



    <%-- // Jd New CSS Loading Animation--%>
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
                <asp:HiddenField ID="hiddenField" runat="server" ClientIDMode="Static" />
                <asp:HiddenField ID="hiddenField2" runat="server" ClientIDMode="Static" />
                <asp:HiddenField ID="hiddenFieldCurves" runat="server" ClientIDMode="Static" />

                <asp:Table ID="Table1" runat="server" HorizontalAlign="Center" Width="100%">


                    <asp:TableRow>
                        <asp:TableCell Width="45%"></asp:TableCell>
                        <asp:TableCell>
                            <h20>Create 3D Graph</h20>
                          <%--  <asp:Label ID="LabelHeader" runat="server" Font-Bold="true" Text="Create 3D Graph" ForeColor="Black"></asp:Label>--%>

                        </asp:TableCell>
                        <asp:TableCell>
                            <asp:Label ID="lblXAxis" runat="server" Font-Bold="true" Text="(V-S)  X-Axis____" ForeColor="Red"></asp:Label>
                            <asp:Label ID="lblYAxis" runat="server" Font-Bold="true" Text="(TVD)  Y-Axis____" ForeColor="Green"></asp:Label>
                            <asp:Label ID="lblZAxis" runat="server" Font-Bold="true" Text="(N/S)  Z-Axis____" ForeColor="Blue"></asp:Label>
                        </asp:TableCell>
                          <asp:TableCell Width="10%"></asp:TableCell>
                    </asp:TableRow>
                </asp:Table>


                <asp:Table ID="MainPage" runat="server" HorizontalAlign="Center" Width="100%">

                  
                 
                    <asp:TableRow>
                        <asp:TableCell CssClass="FormCenter">
                            <asp:Table ID="Table7" runat="server" HorizontalAlign="Center"  BackColor="#F1F1F1" BorderStyle="Double" BorderColor="#3A4F63">
                                <asp:TableRow>
                                    <asp:TableHeaderCell CssClass="HeaderCenter">
                                        Company: 
                                    </asp:TableHeaderCell>
                                    <asp:TableHeaderCell CssClass="HeaderCenter">
						Curve Group ID
                                    </asp:TableHeaderCell>

                                    <asp:TableHeaderCell CssClass="HeaderCenter" ColumnSpan="2">
						Curves
                                    </asp:TableHeaderCell>

                                    <asp:TableHeaderCell CssClass="HeaderCenter">
						Targets
                                    </asp:TableHeaderCell>

                                     <asp:TableHeaderCell CssClass="HeaderCenter">
					
                                    </asp:TableHeaderCell>

                                       <asp:TableHeaderCell CssClass="HeaderCenter">
					
                                    </asp:TableHeaderCell>

                                       <asp:TableHeaderCell CssClass="HeaderCenter">
					
                                    </asp:TableHeaderCell>

                                       <asp:TableHeaderCell CssClass="HeaderCenter">
					
                                    </asp:TableHeaderCell>

                                       <asp:TableHeaderCell CssClass="HeaderCenter">
					
                                    </asp:TableHeaderCell>
                                    
                                    <asp:TableHeaderCell CssClass="HeaderCenter" ColumnSpan="3">
                        Curve Color
                                    </asp:TableHeaderCell>

                                    

                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell>
                                        <telerik:RadDropDownList runat="server" ID="ddlCompany" Width="140px" DropDownWidth="200px" AppendDataBoundItems="true" DropDownHeight="200px"
                                            OnClientItemSelected="CompanyChange">
                                            <Items>
                                                <telerik:DropDownListItem Value="0" Text="-Select-" />
                                            </Items>
                                        </telerik:RadDropDownList>
                                    </asp:TableCell>


                                    <asp:TableCell>
                                        <telerik:RadDropDownList runat="server" ID="ddlCurveGroup" Width="140px" DropDownWidth="200px" AppendDataBoundItems="true" DropDownHeight="200px"
                                            OnClientItemSelected="OnClientItemSelected">
                                       
                                            <Items>
                                                <telerik:DropDownListItem Value="0" Text="-Select-" />
                                            </Items>
                                        </telerik:RadDropDownList>
                                    </asp:TableCell>


                                    <asp:TableCell>
                                        <telerik:RadComboBox ID="ddlCurve" runat="server" CheckBoxes="true" EnableCheckAllItemsCheckBox="true" DropDownWidth="200px" Width="140px" Enabled="false" OnClientDropDownClosed="EnableDisableRedraw">
                                        </telerik:RadComboBox>
                                         
                                    </asp:TableCell>

                                    <asp:TableCell>
                                         <asp:Button ID="btnRedraw" runat="server" CssClass="button-RedrawButton" ForeColor="Black" Text="Redraw" AutoPostBack="false" OnClientClick="getValue(); return false;"></asp:Button>
                                    </asp:TableCell>


                                    <asp:TableCell>
                                        <telerik:RadDropDownList runat="server" ID="ddlTargets" Width="140px" DropDownWidth="200px" AppendDataBoundItems="true" DropDownHeight="200px"
                                            OnClientItemSelected="OnClientItemSelectedTargets">

                                            <Items>
                                                <telerik:DropDownListItem Value="0" Text="-Select-" />
                                            </Items>
                                        </telerik:RadDropDownList>
                                    </asp:TableCell>

                                    <asp:TableCell>
                                        <telerik:RadButton ID="btnView2D" runat="server" Text="View 2D Plot" OnClick="btnView2D_Click" OnClientClicked="AssignCurves"></telerik:RadButton>
                                    </asp:TableCell>
                                    <asp:TableCell>
                                        <asp:Button ID="btnReload" runat="server" CssClass="button-ReloadButton" ForeColor="Black" Text="Reload" OnClientClick="ReloadGraph(); return false;" AutoPostBack="false"></asp:Button>
                                    </asp:TableCell>
                                    <asp:TableCell>
                                        <asp:Button ID="btnPrint" CssClass="button-PrintButton" ForeColor="Black" Text="Print" runat="server" OnClientClick="javascript:window.print();" />
                                    </asp:TableCell>

                                    <asp:TableCell>
                                        <telerik:RadButton ID="btnOrgin" runat="server" Text="Orgin" OnClientClicked="orgin" AutoPostBack="false"></telerik:RadButton>
                                    </asp:TableCell>

                                    <asp:TableCell>
                                        <telerik:RadButton ID="btnLastTarget" runat="server" Text="LastTarget" OnClientClicked="LastTarget" AutoPostBack="false"></telerik:RadButton>
                                    </asp:TableCell>

                                    <asp:TableCell>
                                        <telerik:RadDropDownList ID="ddlCurveColor" runat="server" Width="140px" ></telerik:RadDropDownList>
                                    </asp:TableCell>
                                    <asp:TableCell>
                                        <telerik:RadColorPicker runat="server" ID="RadColorPickerCurve" PaletteModes="WebPalette" ShowIcon="true" ShowEmptyColor="false"></telerik:RadColorPicker>
                                    </asp:TableCell>
                                    <asp:TableCell>
                                        <asp:Button ID="btnApply" Text="Apply" runat="server" OnClientClick="UpdateCurveColor(); return false;" />
                                    </asp:TableCell>

                                </asp:TableRow>
                            </asp:Table>
                        </asp:TableCell>
                    </asp:TableRow>
                </asp:Table>



                <div id="CurveColor">
                    <asp:Label ID="CurveColorInner" runat="server" Text=""></asp:Label>

                </div>

               <%-- // 3d graph Div--%>
                <table>
                    <tr>
                        <td>
                            <div id="Graphdiv">
                                 <canvas id="cCanvas" width="100%" height="100%" /> 

                            </div>   
                        </td>
                    </tr>
                </table>


            </ContentTemplate>
            <Triggers>
            </Triggers>
        </asp:UpdatePanel>
    </fieldset>

</asp:Content>
