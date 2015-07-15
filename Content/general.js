$(document).ready(function () {
    // Date Picker Function
    $(function () {
        $(".DatePicker").datepicker({
            dateFormat: 'd-M-yy',
            showOn: 'button',
            buttonImageOnly: true,
            buttonImage: '/Content/images/calendar.png'
        });
    });

    // J-Query functions to stop the fomr submitting when the enter key is pressed 
    // Runs the click command of the button that is related to the field that the enter key is pressed in
    $("#formAssets").keypress(function (e) {
        if (e.which == 13) {
            return false;
        }
    });
    $("#tbSearchDescription").keyup(function (event) {
        if (event.keyCode == 13) {
            $("#btnSearchDescription").click();
        }
    });
    $("#tbSearchHostName").keyup(function (event) {
        if (event.keyCode == 13) {
            $("#btnSearchHostName").click();
        }
    });
    $("#tbSearchIp").keyup(function (event) {
        if (event.keyCode == 13) {
            $("#btnSearchIp").click();
        }
    });
});