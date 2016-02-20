function populateAutoComplete(objID, availableTypes) {
    var active_color = '#000';
    var inactive_color = '#999';

    $("#" + objID).autocomplete({
        source: availableTypes,
        autoFocus: true
    });

    $("#" + objID).focusout(function () {
        if (jQuery.inArray($(this).val(), availableTypes) == -1) {
            $(this).val('');
        }
    });

    $("#" + objID).focusin(function () {
        if ($(this).val() == "Autocomplete...") {
            $(this).css("color", active_color);
            $(this).val('');
        }
    });

    $("#" + objID).blur(function () {
        if ($(this).val() == '') {
            $(this).css("color", inactive_color);
            $(this).val("Autocomplete...");
        }
    });

    //assign default parameters of autocomplete box
    if ($("#" + objID).val() == '') {
        $("#" + objID).css("color", inactive_color);
        $("#" + objID).val("Autocomplete...");
    }
}