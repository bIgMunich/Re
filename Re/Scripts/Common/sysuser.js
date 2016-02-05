
var setting = {
    view: {
        selectedMulti: true
    },
    data: {
        simpleData: {
            enable: true
        }
    },
    callback: {
        beforeClick: beforeClick,
        onClick: onClick
    }
};
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
function beforeClick(treeId, treeNode, clickFlag) {
    className = (className === "dark" ? "" : "dark");
    return (treeNode.click != false);
}
function onClick(event, treeId, treeNode, clickFlag) {
    $("#DeptId").val(treeNode.id);
    getDataByDeptId(treeNode.id);
}

function getDataByDeptId(deptId) {
    var htmls = "";
    $.ajax({
        url: "/SysUser/GetList",
        async: false,
        type: "post",
        data: { "DeptId": deptId },
        success: function (json) {
            if (json != null && json != undefined) {
                for (var i = 0; i < json.length; i++) {
                    htmls += "<tr>";
                    htmls += "<td>" + json[i].Id + "</td>";
                    htmls += "<td>" + json[i].Account + "</td>";
                    htmls += "<td>" + json[i].RealName + "</td>";
                    htmls += "<td>" + json[i].DeptId + "</td>";
                    htmls += "<td>" + json[i].Type + "</td>";
                    htmls += "<td><a href='javascript:' onclick='permiss(" + json[i].Id + ")'>分配权限</a></td>";
                    htmls += "</tr>";
                }
            }
        }
    });
    $("#showUser").html(htmls);
}

function getTime() {
    var now = new Date(),
    h = now.getHours(),
    m = now.getMinutes(),
    s = now.getSeconds(),
    ms = now.getMilliseconds();
    return (h + ":" + m + ":" + s + " " + ms);
}

$(document).ready(function () {
    $.fn.zTree.init($("#treeDemo"), setting, getData());
});

function create() {
    if ($("#DeptId").val() == "") {
        alert("请选择部门");
        return;
    }
    $("#windowMask").show();
    $("#windowDiv").css("top", offsetH).css("left", offsetw).show();
}

function calse() {
    $("#windowMask").hide();
    $("#windowDiv").hide();
}

var windowH = $(window).height();
var windowW = $(window).width();
var offsetH = (windowH - 400) / 2;
var offsetw = (windowW - 300) / 2;

function permiss(id) {
    $("#UserId").val(id);
    $("#windowMask").show();
    $("#roleList").css("top", offsetH).css("left", offsetw).show();
    getRoleData();
}

function getRoleByUserId() {
    $.ajax({
        url: "/SysUserRole/GetList",
        async: false,
        type: "post",
        data: { userId: $("#UserId").val() },
        success: function (json) {
            if (json != null) {
                for (var i = 0; i < json.length; i++) {
                    $("#" + json[i].RoleId).attr("checked", true);
                }
            }
        }
    });
}

function getRoleData() {
    var htmls = "";
    $.ajax({
        url: "/SysRole/GetList",
        async: false,
        type: "post",
        success: function (json) {
            if (json != null) {
                for (var i = 0; i < json.length; i++) {
                    htmls += "<tr>";
                    htmls += "<td><input id=" + json[i].Id + " type='checkbox' onclick='saveRole(this)'  data-id=" + json[i].Id + " /></td>";
                    htmls += "<td>" + json[i].RoleName + "</td>";
                    htmls += "</tr>";
                }
            }
        }
    });
    $("#showRole").html(htmls);
    getRoleByUserId();
}

function save() {
    $.ajax({
        url: '/SysUser/Add',
        async: false,
        type: "post",
        data: { "DeptId": $("#DeptId").val(), "RealName": $("#RealName").val(), "Account": $("#Account").val(), "Type": $("Type").val() },
        success: function (json) {
            if (json != null && json != undefined) {
                if (json.Flag) {
                    alert("保存成功");
                    getDataByDeptId($("#DeptId").val());
                    $("#windowMask").hide();
                    $("#windowDiv").hide();
                } else {
                    alert(json.Message);
                }
            }
        }
    });
}

function saveRole(obj) {
    var self = $(obj);
    if (self.attr("checked")) {
        $.ajax({
            url: "/SysUserRole/Add",
            async: false,
            type: "post",
            data: { "UserId": $("#UserId").val(), "RoleId": self.attr("id") },
            success: function (json) {
                if (json != null && json != undefined) {
                    if (json.Flag) {
                    } else {
                        alert(json.Message);
                        self.attr("checked", false);
                    }
                }
            }
        });
    } else {
        $.ajax({
            url: "/SysUserRole/Delete",
            async: false,
            type: "post",
            data: { "UserId": $("#UserId").val(), "RoleId": self.attr("id") },
            success: function (json) {
                if (json != null && json != undefined) {
                    if (json.Flag) {
                    } else {
                        alert(json.Message);
                        self.attr("checked", true);
                    }
                }
            }
        });
    }
}

$(".deleteFlag").click(function () {
    $("#windowMask").hide();
    $("#roleList").hide();
});