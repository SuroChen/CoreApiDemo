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
            //url: "data/UserInfo.json",
            url: "http://localhost:8802/api/UserInfo/getUserInfoes/",
            type: "GET"
        }).done(function (data) {
            var tem = "";
            $.each(data, function (i, item) {
                tem += "编号：" + item.id + "，姓名：" + item.name + "，密码：" + item.password + "，年龄：" + item.age + "<br/>";
            });
            $("#divShow").empty().append(tem);
        }).fail(function (xhr, textStatus, err) {
            alert("获取数据失败，请联系系统管理员。");
        });
    };

    function onSelectById(e) {
        $.ajax({
            url: "http://localhost:8802/api/UserInfo/getUserInfoByID/" + $("#id").val(),
            type: "GET"
        }).done(function (data) {
            var tem = "编号：" + data.id + "，姓名：" + data.name + "，密码：" + data.password + "，年龄：" + data.age;
            $("#divShow").empty().append(tem);
        }).fail(function (xhr, textStatus, err) {
            alert("获取数据失败，请联系系统管理员。");
        });
    };

    function onInsert(e) {
        var obj = new Object();
        obj.name = $("#name").val();
        obj.password = $("#password").val();
        obj.age = $("#age").val();

        $.ajax({
            url: "http://localhost:8802/api/UserInfo/addUserInfo/",
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
            url: "http://localhost:8802/api/UserInfo/destroyUserInfo/" + $("#id").val(),
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
        obj.name = $("#name").val();
        obj.password = $("#password").val();
        obj.age = $("#age").val();

        $.ajax({
            url: "http://localhost:8802/api/UserInfo/modifyUserInfo/",
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
