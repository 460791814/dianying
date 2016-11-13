<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="HomeManage.aspx.cs" Inherits="DianYing.Web.Manage.HomeManage" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta charset="utf-8" />
    <title>上传图片</title>
    <link rel="stylesheet" type="text/css" href="/Style/homework.css">
    <link href="/Style/diyibk.min.css" rel="stylesheet" type="text/css" />
    <link href="/Js/uploadifyAuth/uploadify.css" rel="stylesheet" type="text/css" />
    <script src="/Js/jquery.js" type="text/javascript"></script>
    <script src="/Js/uploadifyAuth/jquery.uploadify.min.js" type="text/javascript"></script>
        <script src="/JS/artDialog4.1.7/artDialog.js" type="text/javascript"></script>
    <script src="/JS/artDialog4.1.7/plugins/iframeTools.js" type="text/javascript"></script>
      <link href="/JS/artDialog4.1.7/skins/blue.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">
        var isUpdate = '<%=Request["isshow"] %>';
        var path = '<%=path %>';
        $(function () {

            if (isUpdate == "true") {
                $("#shangchuan").css("display", "none");
                $("#intro").css("display", "none");
            }
        })
        $(function () {
            $("#fileinput").uploadify({
                "auto": true,
                "buttonClass": "",
                "buttonCursor": "hand",     // arrow、hand
                "buttonImage": null,
                "buttonText": "浏览",
                "checkExisting": false,     // 检查文件是否存在的处理程序url
                "debug": false,
                "fileObjName": "fileinput",      // 请求文件名称
                "fileSizeLimit": "2MB",     // 文件大小限制 100KB
                "fileTypeDesc": "请选择文件",     // 选择文件框说明
                "fileTypeExts": "*.jpg; *.gif;*.jpeg;*.png",
                "formData": {},      // 表单数据
                "height": 20,
                "width": 125,
                "itemTemplate": false,      // 文件上传队列html（文件参数：${instanceID}、${fileID}、${fileName}、${fileSize}）
                "method": "post",
                "multi": false,
                "overrideEvents": ["onSelectError", "onDialogOpen", "onDialogClose"/**/],       // 重写的事件 ["onUploadProgress"]
                "preventCaching": true,
                "progressData": "percentage",       // speed、percentage
                "queueID": false,       // 队列标签ID
                "swf": "/Js/uploadify/uploadify.swf",
                "uploader": "UploadHandler.ashx",     // 上传处理程序的url
                "onUploadSuccess": function (file, data, response) {

                    if (data == "1") {
                        alert("请上传小于2M的图片");
                    } else if (data == "2") {
                        alert("图片格式不正确！");
                    } else {
                        $("#imgID").attr("src", "/dyImg/bigImg" + "/" + data.substring(0, 2) + "/" + data + ".jpg");
                        $("#bigImg").val(data);
                    }
                },
                "onUploadError": function (file, errorCode, errorMsg, errorString) {
                    alert(errorMsg + "///");
                }
            });
        });
    </script>
    <script type="text/javascript">
        function save() {
            var img = $("#bigImg").val();
            var movieId = $("#MovieId").val();

            if (img == "") {
                art.artDialog.alert("请先上传图片！");
                return;
            }

            $.ajax({
                type: "post",
                url: "/Manage/HomeManage.aspx",
                data: "method=save&img=" + img + "&MovieId=" + movieId,
                success: function (data) {
                    alert(data);
                    if (data == "True") {
                        art.artDialog.alert("保存成功！");
                        //art.artDialog.close();
                    } else {
                        art.artDialog.alert("保存失败！");
                    }
                },
                error: function () { alert("error!"); }
            });

        }
    </script>
</head>
<body>
    <form method="post" action="/Manage/UploadBigImg.ashx">
    <div class="ftp-img">
     
        <img id="imgID" src="<%=imgPath %>" width="130px" height="140px">
      
        <div class="ftp-imgbt"><input type="file" id="fileinput" name="fileinput" /></div>
        ﻿<p class="describe" id="intro"> 图片格式为jpg、jpeg、gif或png，大小不超过2M。</p>
    </div>
    </form>
    <input type="hidden" name="MovieId" id="MovieId" value="<%=this.Request["MovieId"]%>">
    <input type="hidden" name="name" id="bigImg" value="" />
    <input type="button" name="name" onclick="save()" class="btn btn-primary" value="上传首页大图" />
</body>
</html>
