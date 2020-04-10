$(document).on('click', '.toReport', function () {
    var chkBx = $('.chk');
    var model = "[";
    for (var index = 0; index < chkBx.length; index++) {
        var name = chkBx[index].dataset["table"] + "|" + chkBx[index].dataset["name"];
        var direction = $('*[data-Selecttable="directoryObjects"]')[0].value;
        var item = "{" + name + ":" + direction + "}";
        model = model + item + ",";
    }
    model = model + "]";
    debugger
});

