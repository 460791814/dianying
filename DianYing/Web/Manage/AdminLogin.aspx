<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AdminLogin.aspx.cs" Inherits="DianYing.Web.Manage.AdminLogin" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="/Style/diyibk.min.css" rel="stylesheet" type="text/css" />
    <link href="/Style/Default.css" rel="stylesheet" type="text/css" />
    <link href="/Style/Page.css" rel="stylesheet" type="text/css" />
    <script src="/JS/jquery-1.8.2.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(function () {
            $("#login_sign_in").click(function () {
               
                var userName = $("#pop_login_account").val().replace(/[ ]/g, "");
                var passWord = $("#pop_login_password").val().replace(/[ ]/g, "");
                if (userName == "") {
                    loginError("请输入用户名！");
                    return false;
                }
                if (passWord == "") {

                    loginError("请输入密码！");
                    return false;
                }
                $("#login_sign_in").attr("disabled", "disabled").html("登录中...");
                $.ajax({
                    type: "post",
                    url: "/Login.aspx",
                    data: "method=managelogin&account=" + userName + "&password=" + passWord,
                    success: function (data) {

                        if (data == "success") {
                            window.location.href = "/Manage/DataManage.aspx";

                        } else {
                            alert("用户名或密码错误！");
                            $("#login_sign_in").attr("disabled", "").html("立即登录");
                        }
                    }
                });
            })
        })
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <div  class="container" style="width:360px">
            <input id="login_xsrf" type="hidden" name="_xsrf" value="">
            <div class="pop-login-dialog-left">
                <fieldset class="span6" style="margin-top: 20px; margin-left: 30px;">
                    <legend style="margin-bottom: 0;">登录</legend>
                    <div id="login_error">
                    </div>
                    <div id="login_email" class="control-group" style="height: 40px; width: 300px; background-color: #eef1f0;">
                        <label for="pop_login_account" style="overflow: hidden; font-size: 14px; width: 80px;
                            padding-left: 5px; margin-top: 9px; float: left;">
                            账号：</label>
                        <div class="controls" style="padding-top: 5px;">
                            <div class=" input-prepend">
                                <input class="fresh-control" style="border: 0px none; margin-left: 10px; width: 180px;"
                                    type="text" id="pop_login_account" name="account" value="" placeholder="管理员账号">
                            </div>
                        </div>
                    </div>
                    <div id="login_pwd" class="control-group" style="height: 40px; width: 300px; background-color: #eef1f0;">
                        <label for="pop_login_password" style="overflow: hidden; font-size: 14px; width: 80px;
                            padding-left: 5px; margin-top: 9px; float: left;">
                            密码：</label>
                        <div class="controls" style="padding-top: 5px;">
                            <div class=" input-prepend">
                                <input class="fresh-control" style="border: 0px none; margin-left: 10px; width: 180px;"
                                    type="password" id="pop_login_password" name="password" value="" placeholder="管理员密码">
                            </div>
                        </div>
                    </div>
                    <div style="position: absolute; margin-left: 90px; bottom: 50px;">
                        <button id="login_sign_in" class="btn btn-primary" type="button" style="width: 100px;">
                            立即登录
                        </button>
                       
                    </div>
            </div>
        </div>
    </div>
    </form>
</body>
</html>
