
var setting = {
    view: {
        addHoverDom: addHoverDom,
        removeHoverDom: removeHoverDom,
        selectedMulti: false
    },
    edit: {
        enable: true,
        editNameSelectAll: true,
        showRemoveBtn: true,
        showRenameBtn: true
    },
    data: {
        simpleData: {
            enable: true
        }
    },
    callback: {
        beforeDrag: beforeDrag,
        beforeEditName: beforeEditName,
        beforeRemove: beforeRemove,
        onRename: onRename,
        onRemove: onRemove,
    }
};

function removeHoverDom(treeId, treeNode) {
};

function showRemoveBtn(treeId, treeNode) {
    return !treeNode;
}

function beforeRemove(treeId, treeNode) {
    className = (className === "dark" ? "" : "dark");
    var zTree = $.fn.zTree.getZTreeObj("treeDemo");
    zTree.selectNode(treeNode);
    return confirm("确认删除 节点 -- " + treeNode.name + " 吗？");
}

function onRemove(e, treeId, treeNode) {
    $.ajax({
        url: "/SysDept/Delete",
        async: false,
        type: "post",
        data: { "Id": treeNode.id },
        success: function (json) {
            if (json.Flag) {
                $.fn.zTree.init($("#treeDemo"), setting, getData());
            } else {
                alert(json.Message);
                $.fn.zTree.init($("#treeDemo"), setting, getData());
            }
        }
    });
}

function getData() {
    var zNodes = "";
    $.ajax({
        url: "/SysDept/GetTreeTest",
        async: false,
        type: "post",
        success: function (json) {
            zNodes = json;
        }
    });
    return zNodes;
}

var log, className = "dark";
function beforeDrag(treeId, treeNodes) {
    return false;
}
function beforeEditName(treeId, treeNode) {
    className = (className === "dark" ? "" : "dark");
    editWindow(treeNode.id, treeNode.pId, treeNode.name);
}

function onRename(e, treeId, treeNode, isCancel) {
    showLog((isCancel ? "<span style='color:red'>" : "") + "[ " + getTime() + " onRename ]&nbsp;&nbsp;&nbsp;&nbsp; " + treeNode.name + (isCancel ? "</span>" : ""));
}

function showLog(str) {
    if (!log) log = $("#log");
    log.append("<li class='" + className + "'>" + str + "</li>");
    if (log.children("li").length > 8) {
        log.get(0).removeChild(log.children("li")[0]);
    }
}
function getTime() {
    var now = new Date(),
    h = now.getHours(),
    m = now.getMinutes(),
    s = now.getSeconds(),
    ms = now.getMilliseconds();
    return (h + ":" + m + ":" + s + " " + ms);
}

var newCount = 1;
function addHoverDom(treeId, treeNode) {
    var sObj = $("#" + treeNode.tId + "_span");
    if (treeNode.editNameFlag || $("#addBtn_" + treeNode.tId).length > 0) return;
    var addStr = "<span class='button add' id='addBtn_" + treeNode.tId
        + "' title='add node' onfocus='this.blur();'></span>";
    sObj.after(addStr);
    var btn = $("#addBtn_" + treeNode.tId);
    if (btn) btn.bind("click", function () {
        var zTree = $.fn.zTree.getZTreeObj("treeDemo");
        addWindow(treeNode.id);
        return false;
    });
};

function addWindow(parentId) {
    var windowH = $(window).height();
    var windowW = $(window).width();
    var offsetH = (windowH - 150) / 2;
    var offsetw = (windowW - 300) / 2;
    $("#windowMask").show();
    $("#windowDiv").css("top", offsetH).css("left", offsetw).show();
    $("#ParentId").val(parentId);
}

function editWindow(id, pid, name) {
    var windowH = $(window).height();
    var windowW = $(window).width();
    var offsetH = (windowH - 150) / 2;
    var offsetw = (windowW - 300) / 2;
    $("#windowMask").show();
    $("#windowDiv").css("top", offsetH).css("left", offsetw).show();
    $("#Id").val(id);
    $("#DeptName").val(name);
    $("#DeptCode").val("编码");
    $("#ParentId").val(pid);
}

function Add() {
    if ($("#Id").val() == "") {
        $.ajax({
            url: "/SysDept/Add",
            async: false,
            data: { "ParentId": $("#ParentId").val(), "DeptCode": $("#DeptCode").val(), "DeptName": $("#DeptName").val() },
            type: "post",
            success: function (json) {
                if (json.Flag) {
                    $("#windowDiv").hide();
                    $("#windowMask").hide();
                    $("#Id").val("");
                    $("#DeptName").val("");
                    $("#DeptCode").val("");
                    $("#ParentId").val("");
                    $.fn.zTree.init($("#treeDemo"), setting, getData());
                } else {
                    alert(json.Message);
                }
            }
        });
    } else {
        $.ajax({
            url: "/SysDept/Update",
            async: false,
            data: { "ParentId": $("#ParentId").val(), "Id": $("#Id").val(), "DeptCode": $("#DeptCode").val(), "DeptName": $("#DeptName").val() },
            type: "post",
            success: function (json) {
                if (json.Flag) {
                    $("#windowDiv").hide();
                    $("#windowMask").hide();
                    $("#Id").val("");
                    $("#DeptName").val("");
                    $("#DeptCode").val("");
                    $("#ParentId").val("");
                    $.fn.zTree.init($("#treeDemo"), setting, getData());
                } else {
                    alert(json.Message);
                }
            }
        });
    }
}

function selectAll() {
    var zTree = $.fn.zTree.getZTreeObj("treeDemo");
    zTree.setting.edit.editNameSelectAll = $("#selectAll").attr("checked");
}

$(document).ready(function () {
    $.fn.zTree.init($("#treeDemo"), setting, getData());
    $("#selectAll").bind("click", selectAll);
});

function Calse() {
    $("#Id").val("");
    $("#DeptName").val("");
    $("#DeptCode").val("");
    $("#ParentId").val("");
    $("#windowDiv").hide();
    $("#windowMask").hide();
}
