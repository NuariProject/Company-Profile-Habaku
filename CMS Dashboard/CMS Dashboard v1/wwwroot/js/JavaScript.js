function DropdownGeneral(type, url, obj, idtag, form) {
    $("#" + idtag + "").empty()
    $("#" + idtag + "").prepend("<option value=''>Silakan Pilih " + form + "</option>");
    $.ajax({
        type: type, // Metode HTTP
        url: url, // Place Function
        data: obj,
        success: function (response) {
            $.each(response, function (index, value) {
                $("#" + idtag + "").append($('<option>', {
                    value: value.value,
                    text: value.text
                }));
            });
        },
        failure: function (err) {
            //NotifError("Error", err)
        }
    });
}

$(document).ready(function () {
    DropdownMenu();
});
function DropdownMenu() {
    debugger;
    obj = [];
    DropdownGeneral("GET", "/Section/GetMenuList", obj, 'menulist', 'Menu');
}