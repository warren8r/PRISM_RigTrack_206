$(document).ready(function () {

    $(function () {
        $("#eventsBtn").click(function () {
            $("#events > option:selected").each(function () {
                $(this).remove().appendTo("#grp_outageManagement");
            });
        });

        $("#button2").click(function () {
            $("#list2 > option:selected").each(function () {
                $(this).remove().appendTo("#events");
            });
        });
    });
});