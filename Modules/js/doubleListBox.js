$(document).ready(function () {

    $(function () {
        $("#include").click(function () {
            $("#excluded > option:selected").each(function () {
                $(this).remove().appendTo("#included");
            });
        });

        $("#exclude").click(function () {
            $("#included > option:selected").each(function () {
                $(this).remove().appendTo("#excluded");
            });
        });
    });
});