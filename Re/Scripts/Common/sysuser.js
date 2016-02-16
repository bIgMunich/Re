
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
    getDataByDeptId({ "DeptId": treeNode.id });
}

function getDataByDeptId(p) {
    var htmls = "";
    $.ajax({
        url: "/SysUser/GetList",
        async: false,
        type: "post",
        data: p,
        success: function (json) {
            if (json != null && json != undefined) {
                if (json.data != null) {
                    for (var i = 0; i < json.data.length; i++) {
                        htmls += "<tr>";
                        htmls += "<td>" + json.data[i].Id + "</td>";
                        htmls += "<td>" + json.data[i].Account + "</td>";
                        htmls += "<td>" + json.data[i].RealName + "</td>";
                        htmls += "<td>" + json.data[i].DeptId + "</td>";
                        htmls += "<td>" + json.data[i].Type + "</td>";
                        htmls += "<td><a href='javascript:' onclick='permiss(" + json.data[i].Id + ")'>分配权限</a></td>";
                        htmls += "</tr>";
                    }
                    paerSpance.getpager(json.pager, p, getDataByDeptId);
                    //getpager(json.pager, p);
                }
            }
        }
    });
    $("#showUser").html(htmls);
}

//function getpager(self, p) {
//    var strhtml = "";
//    var currentIndex = self.CurrenetPageIndex;
//    if (self.PageCount > 0 && self.PageCount <= 10) {
//        for (var i = 1; i <= self.PageCount; i++) {
//            if (self.CurrenetPageIndex == i) {
//                strhtml += "<li class=\"active\"><a href=\"javascript:\" data-pc=\"" + i + "\">" + i + "</a></li>";
//            } else {
//                strhtml += "<li><a href=\"javascript:\" data-pc=\"" + i + "\">" + i + "</a></li>";
//            }
//        }
//    }
//    if (self.PageCount > 10) {
//        var beginIndex = 1;
//        var endIndex = self.PageCount;
//        if (currentIndex - 5 > 0) {
//            beginIndex = currentIndex - 5;
//        }
//        if (currentIndex + 5 <= self.PageCount) {
//            endIndex = currentIndex + 5;
//        }
//        for (var i = beginIndex; i <= endIndex; i++) {
//            if (self.CurrenetPageIndex == i) {
//                strhtml += "<li class=\"active\"><a href=\"javascript:\" data-pc=\"" + i + "\">" + i + "</a></li>";
//            } else {
//                strhtml += "<li><a href=\"javascript:\" data-pc=\"" + i + "\">" + i + "</a></li>";
//            }
//        }
//    }
//    $(".pagination").html(strhtml);
//    $(".pagination li a").click(function () {
//        p.page = $(this).data("pc");
//        getDataByDeptId(p);
//    });
//}



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