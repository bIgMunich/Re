
init();
function init() {
    var strHtml = "";
    $.ajax({
        url: "/SysRole/GetList",
        async: false,
        type: "post",
        success: function (json) {
            if (json != null && json != undefined) {
                for (var i = 0; i < json.length; i++) {
                    strHtml += "<tr>";
                    strHtml += "<td>" + json[i].Id + "</td>";
                    strHtml += "<td>" + json[i].RoleName + "</td>";
                    strHtml += '<td><a href="/Admin/SysTemplate/ActionTree?RoleId=' + json[i].Id + '">授权</a></td>';
                    strHtml += "</tr>";
                }
            }
        }
    });
    $("#showRole").html(strHtml);
}
function openWindow() {
    var windowH = $(window).height();
    var windowW = $(window).width();
    var offsetH = (windowH - 200) / 2;
    var offsetw = (windowW - 300) / 2;
    $("#windowMask").show();
    $("#windowDiv").css("top", offsetH).css("left", offsetw).show();
}

function save() {
    $.ajax({
        url: "/SysRole/Add",
        async: false,
        type: "post",
        data: { "RoleName": $("#RoleName").val() },
        success: function (json) {
            if (json != null && json != undefined) {
                if (json.Flag) {
                    alert("保存成功");
                    init();
                    closeWindow();
                } else {
                    alert(json.Message);
                }
            }
        }
    });
}

function closeWindow() {
    $("#windowDiv").hide();
    $("#windowMask").hide();
    $("#RoleName").val("");
}