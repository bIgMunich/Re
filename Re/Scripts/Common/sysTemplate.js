init();
function init() {
    var strHtml = "";
    $.ajax({
        url: "/Admin/SysTemplate/GetViewListByType",
        async: false,
        type: "post",
        data: { "Type": 0 },
        success: function (json) {
            if (json != null && json != undefined) {
                for (var i = 0; i < json.length; i++) {
                    strHtml += "<tr>";
                    strHtml += "<td><input type='checkbox' data-id=" + json[i].Id + " data-lever=" + json[i].Lever + " data-type=" + json[i].Type + " /></td>";
                    strHtml += "<td>" + json[i].Id + "</td>";
                    strHtml += "<td>" + json[i].Template + "</td>";
                    strHtml += "<td>" + json[i].TemplateCode + "</td>";
                    strHtml += "<td>" + json[i].ParentName + "</td>";
                    strHtml += "<td>" + json[i].TypeName + "</td>";
                    strHtml += "<td>" + json[i].VaildName + "</td>";
                    strHtml += "<td><a href='/Admin/SysTemplate/Test?Id=" + json[i].Id + "'>添加</a></td>";
                    strHtml += "</tr>";
                }
            }
        }
    });
    $("#showTemplate").html(strHtml);
}

function getSigleCheck() {
    var objcheckList = $("input[type='checkbox']");
    if (objcheckList != null && objcheckList.length > 0) {
        if (getCheckedCount(objcheckList) == 1) {
            for (var i = 0; i < objcheckList.length; i++) {
                if (objcheckList[i].checked) {
                    var id = $(objcheckList[i]).attr("data-id");
                    var lever = parseInt($(objcheckList[i]).attr("data-lever")) + 1;
                    $("#ParentId").val(id);
                    $("#Lever").val(lever);
                    return true;
                }
            }
        }
        if (getCheckedCount(objcheckList) == 0) {
            return true;
        }
        if (getCheckedCount(objcheckList) > 1) {
            alert("只能选择一个");
            return false;
        }
    } else {
        return true;
    }
}

function getCheckedCount(obj) {
    var count = 0;
    if (obj != null && obj.length > 0) {
        for (var i = 0; i < obj.length; i++) {
            if (obj[i].checked) {
                count++;
            }
        }
        return count;
    }
}

function openWindow() {
    if (getSigleCheck()) {
        var windowH = $(window).height();
        var windowW = $(window).width();
        var offsetH = (windowH - 150) / 2;
        var offsetw = (windowW - 300) / 2;
        $("#windowMask").show();
        $("#windowDiv").css("top", offsetH).css("left", offsetw).show();
    }
}

function save() {
    $.ajax({
        url: "/SysTemplate/Add",
        async: false,
        data: { "ParentId": $("#ParentId").val(), "Template": $("#Template").val(), "TemplateCode": $("#TemplateCode").val(), "TemplateUrl": $("#TemplateUrl").val(), "Lever": $("#Lever").val() },
        type: "post",
        success: function (json) {
            if (json != null && json != undefined) {
                if (json.Flag) {
                    alert("保存成功");
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
    $("#Id").val("");
    $("#Template").val("");
    $("#TemplateUrl").val("");
    $("#ParentId").val("");
    $("#Lever").val();
    $("#TemplateCode").val("");
}
