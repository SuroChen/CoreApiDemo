$(document).ready(function () {
    $("#selectUserInfos").click(function () {
        onSelectAll();
    });

    $("#selectUserInfoById").click(function () {
        onSelectById();
    });

    $("#insertUserInfo").click(function () {
        onInsert();
    });

    $("#deleteUserInfo").click(function () {
        onDelete();
    });

    $("#updateUserInfo").click(function () {
        onUpdate();
    });

    function onSelectAll(e) {
        $.ajax({
            url: "api/User/getUsers/",
            type: "GET"
        }).done(function (data) {
            var tem = "";
            $.each(data, function (i, item) {
                tem += "编号：" + item.id + "，用户：" + item.username + "，密码：" + item.password + "，姓名：" + item.fullname + "<br/>";
            });
            $("#divShow").empty().append(tem);
        }).fail(function (xhr, textStatus, err) {
            alert("获取数据失败，请联系系统管理员。");
        });
    };

    function onSelectById(e) {
        $.ajax({
            url: "api/UserInfo/getUser/" + $("#id").val(),
            type: "GET"
        }).done(function (data) {
            var tem = "编号：" + data.id + "，用户：" + data.username + "，密码：" + data.password + "，姓名：" + data.fullname;
            $("#divShow").empty().append(tem);
        }).fail(function (xhr, textStatus, err) {
            alert("获取数据失败，请联系系统管理员。");
        });
    };

    function onInsert(e) {
        var obj = new Object();
        obj.username = $("#username").val();
        obj.password = $("#password").val();
        obj.fullname = $("#fullname").val();

        $.ajax({
            url: "api/UserInfo/addUser/",
            data: obj,
            type: "POST"
        }).done(function (data) {
            onSelectAll();
            alert('新增数据成功。');
        }).fail(function (xhr, textStatus, err) {
            alert('新增数据失败，请联系系统管理员。');
        });
    };

    function onDelete(e) {
        $.ajax({
            url: "api/UserInfo/destroyUser/" + $("#id").val(),
            type: 'DELETE'
        }).done(function (data) {
            onSelectAll();
            alert('删除数据成功。');
        }).fail(function (xhr, textStatus, err) {
            alert('删除数据失败，请联系系统管理员。');
        });
    };

    function onUpdate(e) {
        var obj = new Object();
        obj.id = $("#id").val();
        obj.username = $("#username").val();
        obj.password = $("#password").val();
        obj.fullname = $("#fullname").val();

        $.ajax({
            url: "api/UserInfo/modifyUser/" + obj.id,
            data: obj,
            type: 'PUT'
        }).done(function (data) {
            onSelectAll();
            alert('更新数据成功。');
        }).fail(function (xhr, textStatus, err) {
            alert('更新数据失败，请联系系统管理员。');
        });
    };
});
