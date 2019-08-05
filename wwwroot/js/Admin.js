
function GetAction(controller, action, element) {
    var url = "/" + controller + "/" + action;
    $.get(url, null, function (data) {
        $(element).html(data);
    });
}

function CreateUser(controller, action, element, Obj) {

    var url = "/" + controller + "/" + action;
    $.post(url, Obj, function (data) {
        $(element).html(data);
    });
}
