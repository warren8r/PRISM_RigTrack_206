


// jd added for newjobRequest pop up
function RaisJobPopUp(content) {

    // $.ui.dialog.prototype._focusTabbable = function () { }
    $(function () {

        $("#confirmDialog").dialog(
            {
                height: 700,
                width: 1000,
                title: 'Dynamic Dialog',
                modal: true,

                open: function () {
                    $("#confirmDialog").load('../CommonJobPortal/RaiseJob.aspx');

                },

                close: function () {
                    //                    $("#confirmDialog").empty();
                    //                    $(document).focus();

                }


            });

    });


}




// LS - error dialog for error messages, just pass in error message
function ErrorDialog(content) {
    $(function () {
        $("#confirmDialog").color = "red"
        $("#confirmDialog").text(content)
        $("#confirmDialog").dialog(
            {
                title: "Error",
                resizable: false,
                modal: true,
                color: "red",
                buttons:
                {
                    'OK': function () {
                        // $(this).dialog("close");
                        $("#confirmDialog").dialog("close");

                        // _DialogConfirmed(); // MUST implement on local page
                    }
                }

            });

    });
    //Dialog('Error', content);


}


// LS - error dialog for error messages, just pass in error message
function SuccessDialog(content) {
    $(function () {
        $("#confirmDialog").color = "red"
        $("#confirmDialog").text(content)
        $("#confirmDialog").dialog(
            {
                title: "Success",
                resizable: false,
                modal: true,
                color: "red",
                buttons:
                {
                    'OK': function () {
                        $("#confirmDialog").dialog("close");
                        //_DialogConfirmed(); // MUST implement on local page
                    }
                }

            });

    });
}


// LS - error dialog for error messages, just pass in error message
function AttachmentSuccessDialog(content) {
    $(function () {
        $("#confirmDialog").color = "red"
        $("#confirmDialog").text(content)
        $("#confirmDialog").dialog(
            {
                title: "Attachment Success",
                resizable: false,
                modal: true,
                color: "red",
                buttons:
                {
                    'OK': function () {
                        $("#confirmDialog").dialog("close");
                        //_DialogConfirmed(); // MUST implement on local page
                    }
                }

            });

    });
}



// LS - generic dialog for displaying basic data. Pass in the title and content.
function Dialog(title, content) {
    $(function () {
        // setup
        $('#dialog').attr('title', title);
        $('#dialog').html(content);

        $('#dialog').dialog(); // display
    });
}

var dialogConfirmed = false;

// jd - generic confirm dialog
function ConfirmDialog(obj, e, content) {
    $(function () {
        if (dialogConfirmed === false) {
            e.preventDefault();

            $('#confirmDialog').attr('title', 'Confirm');
            $('#confirmDialog').html(content);

            $("#confirmDialog").dialog(
                {

                    resizable: false,
                    height: 180,
                    width: 350,
                    modal: true,
                    buttons:
                    {
                        'Confirm': function () {
                            $("#confirmDialog").dialog("close");
                            dialogConfirmed = true;
                            obj.click();
                        },
                        'Cancel': function () {
                            $("#confirmDialog").dialog("close");
                            dialogConfirmed = false;
                        }
                    }
                });
        } else {
            dialogConfirmed = false;
        }

        return dialogConfirmed;
    });
}


// JD - generic confirm dialog
function ConfirmDialogClear(obj, e, content) {
    $(function () {
        if (dialogConfirmed === false) {
            e.preventDefault();

            $('#confirmDialog').attr('title', 'Confirm');
            $('#confirmDialog').html(content);

            $("#confirmDialog").dialog(
                {

                    resizable: false,
                    height: 180,
                    width: 350,
                    modal: true,
                    buttons:
                    {
                        'Yes': function () {
                            $("#confirmDialog").dialog("close");
                            dialogConfirmed = true;
                            obj.click();
                        },
                        'No': function () {
                            $("#confirmDialog").dialog("close");
                            dialogConfirmed = false;
                        }
                    }
                });
        } else {
            dialogConfirmed = false;
        }

        return dialogConfirmed;
    });
}







function ConfirmDialogEmail(obj, e, content) {
    $(function () {
        if (dialogConfirmed === false) {

            e.preventDefault();



            $('#confirmDialog').attr('title', 'Email Letter');
            $('#confirmDialog').html(content);


            $("#confirmDialog").dialog(
                {

                    resizable: false,
                    height: 250,
                    width: 500,
                    modal: true,

                    //appendTo: '#ConfirmDialogEmail',



                    buttons:
                    {
                        'Ok': function () {

                            $("#confirmDialog").dialog("close");
                            dialogConfirmed = true;
                            obj.click();
                        },
                        'Cancel': function () {
                            $("#confirmDialog").dialog("close");
                            dialogConfirmed = false;
                        }
                    }
                });
        }
        else {
            dialogConfirmed = false;
        }

        return dialogConfirmed;

    });
}

function ContractorAttachmentDialog(title, content) {
    $(function () {
        $('#confirmDialog').attr("title", title);
        $('#confirmDialog').html(content);

        $("#confirmDialog").dialog(
            {
                resizable: false,
                modal: true,
                buttons:
                {
                    'Confirm': function () {
                        $("#confirmDialog").dialog("close");
                        //_DialogConfirmed(); // MUST implement on local page
                    },
                    'Cancel': function () {
                        $("#confirmDialog").dialog("close");
                        //_DialogRejected(); // MUST implement on local page
                    }
                }
            });
    });
}