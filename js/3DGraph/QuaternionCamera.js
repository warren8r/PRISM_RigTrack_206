

THREE.FreeCamera = function (camera, x,y,z) {
    var scope = this;
    
    camera.rotation.set(0, 0, 0);
    camera.position.set(x, y, z);
    //camera.useQuaternion = true;

    var MOVE = {
        LEFT: {
            value: 0,
            name: "Left",
            code: "L"
        },
        RIGHT: {
            value: 1,
            name: "Right",
            code: "R"
        },
        FORWARD: {
            value: 2,
            name: "Forward",
            code: "F"
        },
        BACK: {
            value: 3,
            name: "Back",
            code: "B"
        },
        UP: {
            value: 4,
            name: "Up",
            code: "U"
        },
        DOWN: {
            value: 5,
            name: "Down",
            code: "D"
        }
    };
    var camera_scale = .5;
    var camera_direction = new THREE.Vector3(0, 0, 1);
    var camera_up = new THREE.Vector3(0, 1, 0);
    var camera_position_delta = new THREE.Vector3(0, 0, 0);
    var camera_position = new THREE.Vector3(0, 0, 0);
    var camera_look_at = new THREE.Vector3(0, 0, 2);

    var max_pitch_rate = 5.0;
    var max_heading_rate = 5.0;
    var camera_pitch = 0.0;
    var camera_heading = 0.0;

    var mouse_delta_x = 0.0;
    var mouse_delta_y = 0.0;
    var mouse_pos_x = 0.0;
    var mouse_pos_y = 0.0;
    var move_camera = false;

    var isOnObject = false;
    var canJump = false;

    var onMouseMove = function (event) {
        //alert("OnMouseMove");
        if (scope.enabled === false) return;

        var movementX = event.movementX || event.mozMovementX || /*event.webkitMovementX || */0;
        var movementY = event.movementY || event.mozMovementY || /*event.webkitMovementY || */0;

        mouse_delta_x = mouse_pos_x - movementX;
        mouse_delta_y = mouse_pos_y - movementY;
        if (move_camera) {
            ChangeHeading(.08 * mouse_delta_x);
            ChangePitch(.08 * mouse_delta_y);
        }
        mouse_pos_x = movementX;
        mouse_pos_y = movementY;
    };

    var onMouseDown = function (event) {
        //alert("OnMousedown");
        var movementX = event.movementX || event.mozMovementX || /*event.webkitMovementX ||*/ 0;
        var movementY = event.movementY || event.mozMovementY || /*event.webkitMovementY ||*/ 0;
        mouse_pos_x = movementX;
        mouse_pos_y = movementY;
        move_camera = true;
        
    }
    var onMouseUp = function (event) {
        //alert("OnMouseUp");
        var movementX = event.movementX || event.mozMovementX || /*event.webkitMovementX ||*/ 0;
        var movementY = event.movementY || event.mozMovementY || /*event.webkitMovementY ||*/ 0;
        mouse_pos_x = movementX;
        mouse_pos_y = movementY;
        move_camera = false;
        
    }

    var ChangePitch = function (degrees) {
        
        if (degrees < -max_pitch_rate) {
            degrees = -max_pitch_rate;
        } else if (degrees > max_pitch_rate) {
            degrees = max_pitch_rate;
        }
        camera_pitch += degrees;
        if (camera_pitch > 360.0) {
            camera_pitch -= 360.0;
        } else if (camera_pitch < -360.0) {
            camera_pitch += 360.0;
        }
        
    }
    var ChangeHeading = function (degrees) {
        
        if (degrees < -max_heading_rate) {
            degrees = -max_heading_rate;
        } else if (degrees > max_heading_rate) {
            degrees = max_heading_rate;
        }

        if (camera_pitch > 90 && camera_pitch < 270 || (camera_pitch < -90 && camera_pitch > -270)) {
            camera_heading -= degrees;
        } else {
            camera_heading += degrees;
        }

        if (camera_heading > 360.0) {
            camera_heading -= 360.0;
        } else if (camera_heading < -360.0) {
            camera_heading += 360.0;
        }
    }

    var moveCamera = function (move) {
       
        if (scope.enabled === false) return;
        var t = new THREE.Vector3(0, 0, 0);
        switch (move) {
            case MOVE.UP:
                t.copy(camera_up);
                t.multiplyScalar(camera_scale);
                camera_position_delta.add(t);
                
                break;
            case MOVE.DOWN:
                t.copy(camera_up);
                t.multiplyScalar(camera_scale);
                camera_position_delta.sub(t);
                
                break;
            case MOVE.LEFT:
                t.crossVectors(camera_direction, camera_up);
                t.multiplyScalar(camera_scale);
                camera_position_delta.sub(t);
                
                break;
            case MOVE.RIGHT:
                t.crossVectors(camera_direction, camera_up);
                t.multiplyScalar(camera_scale);
                camera_position_delta.add(t);
                
                break;
            case MOVE.FORWARD:
                t.copy(camera_direction);
                t.multiplyScalar(camera_scale);
                camera_position_delta.sub(t);
                
                break;
        }
    }
    var onKeyDown = function (event) {
        
        switch (event.keyCode) {
            case 81: //Q
            case 33: //PgUP
                moveCamera(MOVE.DOWN);
                break;
            case 69: //E
            case 34: //PgDown
                moveCamera(MOVE.UP);
                break;
            case 38: //up arrow
            case 87: //W
                moveCamera(MOVE.FORWARD);
                break;
            case 37: //left
            case 65: //A
                moveCamera(MOVE.LEFT);
                break;
            case 40: //down
            case 83: //s
                moveCamera(MOVE.BACK);
                break;
            case 39: //right
            case 68: //D
                moveCamera(MOVE.RIGHT);
                break;
        }
    };

    document.addEventListener('mousemove', onMouseMove, false);
    document.addEventListener('mousedown', onMouseDown, false);
    document.addEventListener('mouseup', onMouseUp, false);
    document.addEventListener('keydown', onKeyDown, false);
    this.enabled = true;

    this.getObject = function () {
        return camera;
    };

    this.isOnObject = function (boolean) {
        isOnObject = boolean;
        canJump = boolean;
    };

    this.getDirection = function () {
        return camera_direction;
    }();

    this.getLookAt = function (delta) {
        return camera_look_at;
    };

    this.update = function () {
        
        var axis = new THREE.Vector3(0, 0, 0);
        camera_direction.subVectors(camera_look_at, camera_position);
        camera_direction.normalize();
        axis.crossVectors(camera_direction, camera_up);
        var pitch_quat = new THREE.Quaternion(0, 0, 0, 1);
        var heading_quat = new THREE.Quaternion(0, 0, 0, 1);
        var temp = new THREE.Quaternion(0, 0, 0, 1);
        pitch_quat.setFromAxisAngle(axis, camera_pitch * Math.PI / 180.0);
        heading_quat.setFromAxisAngle(camera_up, camera_heading * Math.PI / 180.0);
        temp.multiplyQuaternions(pitch_quat, heading_quat);
        camera_direction.applyQuaternion(temp);
        camera_position.add(camera_position_delta);
        camera_look_at.addVectors(camera_position, camera_direction);

        camera.position.copy(camera_position);
        camera.up.copy(camera_up);
        camera.lookAt(camera_look_at);
        if (move_camera == false) {
            camera_pitch = camera_pitch * .5;
            camera_heading = camera_heading * .5;
        }
        camera_position_delta.multiplyScalar(.8);


        
    };
    

};
THREE.FreeCamera.prototype = Object.create(THREE.EventDispatcher.prototype);
THREE.FreeCamera.prototype.constructor = THREE.FreeCamera;