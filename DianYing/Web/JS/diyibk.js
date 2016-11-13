

        //新用户注册
$(function () {
    $("#captcha_img").click(function () {
        $("#captcha_img").attr("src", "/yzm.aspx?r=" + Math.ceil(100));
    })
    $("#refresh_captcha").click(function () {
        $("#captcha_img").attr("src", "/yzm.aspx?r=" + Math.ceil(100));
        return false;
    })

    $("#search_btn").click(function () {
        var keyword = $("#search_input").val();
        window.location.href = "/category/?keyword=" + keyword;

    })


    //            $("#nav_setting").click(function () {
    //                var c = $("#nav_setting").attr("class");
    //                if (c.indexOf('open') > -1) {
    //                    $("#nav_setting").attr("dropdown-toggle");
    //                } else {
    //                    $("#nav_setting").attr("class", "dropdown-toggle open");
    //                    alert($("#nav_setting").attr("class"));
    //                }
    //            })
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
            data: "method=login&account=" + userName + "&password=" + passWord,
            success: function (data) {

                if (data == "success") {
                    window.location.reload();

                } else {
                    loginError("用户名或密码错误！");
                    $("#login_sign_in").attr("disabled", "").html("立即登录");
                }
            }
        });
    })
    $("#pop_register_account").blur(function () {

        var userName = $("#pop_register_account").val().replace(/[ ]/g, "");
        if (userName == "") {
            error("请填写用户名！");
        }
        if (!EmailCheck(userName)) {
            error("邮箱的格式不正确。");
            return false;
        }
        if (userName.length > 60) {
            error("邮箱的地址过长。");
            return false;
        }
        $.ajax({
            type: "post",

            url: "/Login.aspx",
            data: "method=same&account=" + userName,
            success: function (data) {

                if (data == "True") {
                    error("该用户名已被注册！");
                } else {
                    error("该用户名可用！");
                }
            }

        });
    })
    $("#register_complete").click(function () {

        var userName = $("#pop_register_account").val().replace(/[ ]/g, "");
        var pass = $("#pop_register_pwd").val().replace(/[ ]/g, "");
        var repass = $("#pop_register_pwd_confirm").val().replace(/[ ]/g, "");
        var yanzhengma = $("#pop_register_captcha").val().replace(/[ ]/g, "");
        if (userName == "") {
            error("请填写用户名。");
            return false;
        }
        if (!EmailCheck(userName)) {
            error("邮箱的格式不正确。");
            return false;
        }
        if (userName.length > 60) {
            error("邮箱的地址过长。");
            return false;
        }
        if (pass.length>20||pass.length<6) {
            error("密码长度为8-20字符");
            return false;
        }
        if (pass != repass) {
            error("两次输入的密码不一致。");
            return false;
        }
        if (yanzhengma=="")
        {
            error("请输入验证码。");
            return false;
        }
        $.ajax({
            type: "post",
            url: "/Login.aspx",
            data: "method=register&account=" + userName + "&password=" + pass + "&re_password=" + repass + "&yzm=" + yanzhengma,
            success: function (data) {

                if (data == "success") {
                    window.location.reload();

                } else {
                    error(data);
                }
            }
        });

    })

})
        function loginError(error_tip) {
            $("#login_error").html(
                            '<div id="login_error_tips" class="alert alert-error" style="height:20px;overflow:hidden;width:260px;display:none;margin-bottom:6px;"><button type="button" class="close" data-dismiss="alert">&times;</button><strong></strong></div>');
            $("#login_error_tips").show().children("strong").text(error_tip);
        }
        function error(error_tip) {
            $("#register_error").html(
                            '<div id="register_error_tips" class="alert alert-error" style="height:20px;overflow:hidden;width:260px;display:none;margin-bottom:6px;"><button type="button" class="close" data-dismiss="alert">&times;</button><strong></strong></div>');
            $("#register_error_tips").show().children("strong").text(error_tip);
        }
        function Identity(m,uid,mid) {
            $.ajax({
                type: "post",
                url: "/Handler/IdentityHandler.ashx",
                data: "method=register&account=" + userName + "&password=" + pass + "&re_password=" + repass,
                success: function (data) {

                    if (data == "success") {
                        window.location.reload();

                    } else {
                        error("注册失败");
                    }
                }
            });
        }

        function qqlogin() {
            window.location.href = "http://v.diyibk.com/QQlogin/Login.aspx?ComeUrl=" + window.location.href;

        }
        function EmailCheck(objName) {
            
            var pattern = /^([\.a-zA-Z0-9_-])+@([a-zA-Z0-9_-])+(\.[a-zA-Z0-9_-])+/;
            if (!pattern.test(objName)) {
               
                return false;
            }
            return true;
        }   

