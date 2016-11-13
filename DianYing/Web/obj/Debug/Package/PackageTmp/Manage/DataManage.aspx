<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DataManage.aspx.cs" Inherits="DianYing.Web.Manage.DataManage" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns:wb="http://open.weibo.com/wb">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=UTF-8">
    <title>后台管理 </title>
    <link href="/Style/diyibk.min.css" rel="stylesheet" type="text/css" />
    <link href="/Style/Default.css" rel="stylesheet" type="text/css" />
    <link href="/Style/Page.css" rel="stylesheet" type="text/css" />
     <link href="/JS/artDialog4.1.7/skins/blue.css" rel="stylesheet" type="text/css" />
    <script src="/JS/jquery-1.8.2.js" type="text/javascript"></script>
    <script src="/JS/artDialog4.1.7/artDialog.js" type="text/javascript"></script>
    <script src="/JS/artDialog4.1.7/plugins/iframeTools.js" type="text/javascript"></script>
 
   
    <style>
        /** Loader **/#loader
        {
            height: 48px;
            text-align: center;
            padding: 25px 0 25px 0;
        }
        #loaderCircle
        {
            width: 48px;
            height: 48px;
            margin: 0 auto;
        }
        .x-movie-detail
        {
            background-color: #fff;
            padding: 10px;
            max-width: 294px;
            width: 294px;
            margin: 0px 9px 10px 0px;
            overflow-y: hidden;
            position: relative;
            box-shadow: 0 0 30px #eee;
        }
    </style>
    <script type="text/javascript">
        function SetChannelID(c) {
            $("#ChannelID").val(c);
            $("#ShowChannel").html("<span>电影</span>");
            return false;
        }
        function Select() {
            var bigImagePath = "";
            if ($("#BigImagePath").attr("checked") == "checked") {
                bigImagePath = "bigImagePath";
             }
            window.location.href = "DataManage.aspx?ChannelID=" + $("#ChannelID").val() + "&MovieName=" + $("#search_input").val() + "&BigImagePath=" + bigImagePath;
        }
        function SetHomeImg(id) {
            art.dialog.open('/Manage/HomeManage.aspx?MovieId='+id, { title: '推荐到首页', width: 450, height: 300 });
        }
        function deletefromhome(movieId) {
         

            $.ajax({
                type: "post",
                url: "/Manage/HomeManage.aspx",
                data: "method=delete&MovieId=" + movieId,
                success: function (data) {
                 
                    if (data == "True") {
                        art.artDialog.alert("成功移除！请刷新页面看效果");
                        window.location.href = "/Manage/DataManage.aspx";
                    } else {
                        art.artDialog.alert("移除失败！");
                    }
                },
                error: function () { alert("error!"); }
            });

        }
        function deleteMovie(movieId) {
            if (confirm("删除后不可恢复，确认删除?")) {

                $.ajax({
                    type: "post",
                    url: "/Manage/MovieInfo.aspx",
                    data: "method=delete&MovieId=" + movieId,
                    success: function (data) {

                        if (data == "True") {
                            art.artDialog.alert("删除成功！");
                            window.location.href = "/Manage/DataManage.aspx";
                        } else {
                            art.artDialog.alert("删除失败！");
                        }
                    },
                    error: function () { alert("error!"); }
                });
            }
        }
    </script>
</head>
<body>
   <%=head.HTML%>
    <div id="main_content" style="width: 960px;" class="container">
       
        <div class="subnav subnav-fixed" style="margin-top: 0px; height: 31px; width: 960px;
            margin-bottom: 6px;">
            <ul class="nav nav-pills">
                <li class="dropdown active">
                    <select style="width:100px" id="ChannelID">
                    <option  value="0">所有电影</option>
                        <%=GetChannel(em.ChannelID) %>
                   
                    <input type="checkbox" name="name" id="BigImagePath" runat="server" value="推荐到首页" />推荐到首页
                    <div class="diyibk-search" style="margin-left: 200px; margin-top: -10px">
                        <div class="input-append">
                            <input type="text" id="search_input" name="word" value="<%=em.MovieName %>" autocomplete="off"
                                class="diyibk-search-text">
                            <input type="hidden" name="fr" value="video">
                            <span class="diyibk-search-btn">
                                <input type="button" tabindex="2" value="影视搜索" onclick="Select()" id="search_btn"></span>
                        </div>
                  
                </li>
            </ul>
        </div>
        <div id="category" class="x-category-result x-usermovie-controls" style="">
            <ul id="videolist" class="x-movie-list nav nav-pills" style="width: 969px; padding: 0px;">
                <asp:Repeater ID="RepList" runat="server">
                    <ItemTemplate>
                        <li>
                            <div class="x-movie-detail">
                                <div class="pull-left x-movie-mediumimg">
                                    <a data-movie="playmask" class="z-movie-playlink" target="_blank" href="/movie/<%#Eval("MovieId")%>">
                                        <div>
                                            <img alt="<%#Eval("MovieName") %>" src="http://t2.baidu.com/it/u=<%#Eval("ImagePath") %>&fm=20">
                                        </div>
                                        <div class="z-movie-playmask" style="visibility: hidden;">
                                        </div>
                                    </a>
                                </div>
                                <div class="x-movie-desc pull-left">
                                    <p>
                                        <a target="_blank" href="/Manage/MovieInfo.aspx?MovieId=<%#Eval("MovieId")%>">
                                            <%#Eval("MovieName")%></a> <span class="muted">
                                                <%#Convert.ToInt32(Eval("Year")) > 0 ?Eval("Year") :"" %>
                                            </span>
                                    </p>
                                    <table cellspacing="0" cellpadding="0">
                                        <tbody>
                                            <%#Convert.ToInt32(Eval("DBScore")) > 0?"<tr><td><span class=\"x-item-name\">评分：</span> <span class=\"badge\" style=\"color: green; font-weight: bold;\">"+Eval("DBScore") +"</span></td></tr>":"" %>
                                            <tr>
                                                <td>
                                                    <span class="x-item-name">类型：</span>
                                                    <%#GetType(Eval("Type").ToString())%>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <span class="x-item-name">主演：</span>
                                                    <%#QuChu(Eval("Actor").ToString())%>
                                                </td>
                                            </tr>
                                        </tbody>
                                    </table>
                                    <div class="btn-group x-usermovie-buttons x-usermovie-controls">
                                        <a href="/Manage/MovieInfo.aspx?MovieId=<%#Eval("MovieId") %>" class="btn-mini  btn x-tooltip mark btn x-tooltip" title="修改电影基本资料" data-toggle="tooltip"
                                            data-type="like"><span>修改</span></a>|
                                             <a href="/Manage/PlayLinkInfo.aspx?MovieId=<%#Eval("MovieId") %>" class="btn-mini  btn x-tooltip mark btn x-tooltip" title="修改电影播放链接" data-toggle="tooltip"
                                            data-type="like"><span>详情</span></a>|

                                    
                                            <%#GetTuiJian(Eval("BigImagePath"),Eval("MovieId").ToString())%>|
                                             <a href="javascript:void(0)" class="btn-mini  btn x-tooltip mark btn x-tooltip" title="删除该电影" data-toggle="tooltip" onclick="deleteMovie('<%#Eval("MovieId") %>')"
                                            data-type="like"><span>删除</span></a>|
                                    </div>
                                </div>
                                <div style="clear: both">
                                </div>
                            </div>
                        </li>
                    </ItemTemplate>
                </asp:Repeater>
            </ul>
        </div>
        <div class="clearfix" id="pager_out">
            <div class="fl" style="padding: 8px 0 0 120px;">
            </div>
            <div class="pagin fr" id="pager_in">
                <%=PageString%>
            </div>
        </div>
        <div class="well" style="text-align: center; margin-top: 20px; font-size: 13px;">
            <div class="">
            </div>
            <div class="muted">
                Copyright &copy; 2014 第一博客
            </div>
        </div>
        <div style="display: none;">
        </div>
    </div>
    <div class="btn-group btn-group-vertical z-rightedge-container" style="left: 50%;">
        <a href="javascript:void(0);" class="btn btn-small z-rightedge-buttom" id="gotop"><i
            class="icon-arrow-up"></i></br>回</br>到</br>顶</br>部 </a><a href="javascript:void(0)"
                style="" class="btn btn-small z-rightedge-buttom" alt="">反</br>馈</br>意</br>见
        </a>
    </div>
</body>
</html>
