<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UploadImg.aspx.cs" Inherits="DianYing.Web.Manage.UploadImg" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta charset="utf-8" />
    <title>上传图片</title>
    <link rel="stylesheet" type="text/css" href="/Style/homework.css">
    <link href="/Js/uploadifyAuth/uploadify.css" rel="stylesheet" type="text/css" />
    <script src="/Js/jquery.js" type="text/javascript"></script>
    <script src="/Js/uploadifyAuth/jquery.uploadify.min.js" type="text/javascript"></script>
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
                        $("#imgID").attr("src", "/dyImg/img" + "/" + data.substring(0, 2) + "/" + data+".jpg");
                        parent.SetVal("ImagePath", data);
                    }
                },
                "onUploadError": function (file, errorCode, errorMsg, errorString) {
                    alert(errorMsg + "///");
                }
            });
        });
    </script>
</head>
<body>
    <form method="post" action="/Manage/UploadHandler.ashx">
    <div class="ftp-img">
     
        <img id="imgID" src="<%=imgPath %>" width="130px" height="140px">
      
        <div class="ftp-imgbt"><input type="file" id="fileinput" name="fileinput" /></div>
        ﻿<p class="describe" id="intro"> 图片格式为jpg、jpeg、gif或png，大小不超过2M。</p>
    </div>
    </form>
</body>
</html>
