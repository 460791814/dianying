<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LinkInfo.aspx.cs" Inherits="DianYing.Web.Manage.LinkInfo" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
       <link href="/Style/diyibk.min.css" rel="stylesheet" type="text/css" />
    <link href="/Style/Default.css" rel="stylesheet" type="text/css" />
    <link href="/Style/Page.css" rel="stylesheet" type="text/css" />
      <script src="/JS/jquery-1.8.2.js" type="text/javascript"></script>
    <script src="/JS/artDialog4.1.7/artDialog.js" type="text/javascript"></script>
    <script src="/JS/artDialog4.1.7/plugins/iframeTools.js" type="text/javascript"></script>
      <link href="/JS/artDialog4.1.7/skins/blue.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">
        function tijiao() {
            var movieId = $("#HidMovieId").val();
            var sourceId = $("#HidSourceId").val();
            var movieName = $("#MovieName").val();
            var episode = $("#Episode").val();
            var url = $("#MovieURL").val();
            var site = $("#Site").val();
            var dataPara = "";

            if (sourceId > 0) {
                dataPara += "method=update";
                dataPara += "&SourceId=" + sourceId;
            } else {
                dataPara += "method=add";
                dataPara += "&MovieId=" + movieId;
            } 
            if (movieName == "") {
                art.artDialog.alert("电影名称不能为空");
                return false;
            } else {
                dataPara += "&MovieName=" + movieName;
            }

            if (episode == "") {
                art.artDialog.alert("电影当前集数不能为空");
                return false;
            } else {
                dataPara += "&Episode=" + episode;
            }
            if (site == "") {
                art.artDialog.alert("电影来源网站的域名不能为空");
                return false;
            } else {
                dataPara += "&Site=" + site;
            }
            if (url == "") {
                art.artDialog.alert("电影播放连接不能为空");
                return false;
            } else {
                dataPara += "&MovieURL=" + url;
            }


            alert(dataPara);
            $.ajax({
                type: "post",
                url: "LinkInfo.aspx",
                data: dataPara,
                success: function (data) {

                    if (data == "True") {
                        art.artDialog.alert("保存成功！");
                        parent.init();
                    } else {
                        art.artDialog.alert("保存失败！");
                    }
                },
                error: function () { alert("error!"); }
            });
        }
    
    </script>
</head>
<body style="padding-top:0px">
    <form id="form1" runat="server">
    <div>
        <div class="span9">
            <div class="well home_content">
                <fieldset>
              
                    
                    <div class="">
                        <form class="form-horizontal" method="post">
                    
                        <input type="hidden" name="HidMovieId" id="HidMovieId" value="<%=eSource.MovieId %>">
                         <input type="hidden" name="HidSourceId" id="HidSourceId" value="<%=eSource.Id %>">
                  
                        <div class="control-group">
                            <label class="control-label">
                                电影名称</label>
                            <div class="controls">
                                <input type="text" name="MovieName" id="MovieName" value="<%= eSource.MovieName %>">
                            </div>
                        </div>
                                                <div class="control-group">
                            <label class="control-label">
                                电影集数</label>
                            <div class="controls">
                                <input type="text" name="Episode" id="Episode" value="<%=eSource.Episode %>">
                            </div>
                        </div>
                        <div class="control-group">
                            <label class="control-label">
                                来源（域名）</label>
                            <div class="controls">
                                <input type="text" name="Site" id="Site" value="<%= eSource.Site %>">
                            </div>
                        </div>
                        <div class="control-group">
                            <label class="control-label">
                                电影连接</label>
                            <div class="controls">
                                <textarea id="MovieURL" style="margin: 0px 0px 10px; width: 300px; height: 100px;">
 <%= eSource.MovieURL%>
</textarea>
                            </div>
                        </div>
                        
     

                        <div class="control-group">
                            <div class="controls">
                                <input type="button" class="btn btn-primary" value="保存修改" onclick="tijiao()"
                                    name="save_btn">
                            </div>
                        </div>
                        </form>
                    </div>
                </fieldset>
            </div>
        </div>
    </div>
    </form>
</body>
</html>
