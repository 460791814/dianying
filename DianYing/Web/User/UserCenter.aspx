<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UserCenter.aspx.cs" Inherits="DianYing.Web.User.UserCenter" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title></title>
    <script src="/JS/jquery-1.8.2.js" type="text/javascript"></script>
    <link href="/Style/diyibk.min.css" rel="stylesheet" type="text/css" />
    <link href="/Style/Default.css" rel="stylesheet" type="text/css" />
    <link href="/Style/Page.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">
        function iFrameHeight() {
         
            var ifm = document.getElementById("iframepage");
            var subWeb = document.frames ? document.frames["iframepage"].document : ifm.contentDocument;
            if (ifm != null && subWeb != null) {
                ifm.height = subWeb.body.scrollHeight;
            }
        }
        function view(t,d) {
            $("#iframepage").attr("src", "Identify.aspx?action=" + d)
            $(t).siblings().removeClass();
            $(t).attr("class", "active");
        }
    </script>
</head>
<body>
    <%=head.HTML%>
    <div id="main_content" style="width: 960px;" class="container">
        <div class="row">
            <div class="span3 user-sidebar">
                <div class="well" style="background: #dfdfdf;">
                    <div class="portrait">
                    </div>
                    <div>
                        <ul class="nav nav-pills nav-stacked">
                            <li class="active" onclick="view(this,'like')"><a href="javascript:void(0)"><i class="icon-white icon-heart"></i>喜欢</a> </li>
                            <li onclick="view(this,'plan')"><a href="javascript:void(0)"><i class="icon-time"></i>计划</a> </li>
                            <li onclick="view(this,'seen')"><a href="javascript:void(0)"><i class="icon-check"></i>看过</a> </li>
                        </ul>
                    </div>
                </div>
            </div>
            <div class="span9">
                <iframe src="Identify.aspx?action=like" frameborder="0" id="iframepage" scrolling="no" width="100%"
                    onload="iFrameHeight()"></iframe>
            </div>
        </div>
        <div class="well" style="text-align: center; margin-top: 20px; font-size: 13px;">
            <div class="">
            </div>
            <div class="muted">
                Copyright 第一博客</div>
        </div>
        <div style="display: none;">
        </div>
    </div>
    <%=foot.HTML %>
</body>
</html>
