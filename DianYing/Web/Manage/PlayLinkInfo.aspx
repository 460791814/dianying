<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PlayLinkInfo.aspx.cs" Inherits="DianYing.Web.Manage.PlayLinkInfo" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns:wb="http://open.weibo.com/wb">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=UTF-8">
    <title>
        <%=em.MovieName %>
        -电影-第一影院</title>
    <meta name="viewport" content="minimal-ui">
    <meta name="apple-mobile-web-app-capable" content="yes">
    <link href="/Style/diyibk.min.css" rel="stylesheet" type="text/css" />
    <link href="/Style/Default.css" rel="stylesheet" type="text/css" />
    <link href="/Style/share.css" rel="stylesheet" type="text/css" />
    <link rel="shortcut icon" type="image/x-icon" href="/static/images/favicon.ico?v=4fdcf" />
    <meta name="keywords" content="<%=em.MovieName %> <%=em.Actor %>,在线观看,电影 ">
    <meta name="description" content="<%=em.Intro %>">
    <style>
        body
        {
            padding-top: 50px;
            position: relative;
            background-repeat: no-repeat;
            background-color: #f4f4f4;
            background-attachment: fixed;
            background-position: left 40px;
        }
        .tudou_yp
        {
            margin: 5px auto 10px;
            line-height: 20px;
            text-align: left;
        }
        .tudou_yp li
        {
            width: 100%;
        }
        .td_tou
        {
            padding: 0 10px;
            margin: 5px 0;
            overflow: hidden;
            height: 22px;
            line-height: 22px;
            background-color: #F6F6F6;
        }
        .td_tou a
        {
            color: #3391E8 !important;
        }
        .td_tou .star_xx
        {
            width: 60px;
            height: 22px;
            padding: 0 0 0 10px;
            display: inline-block;
            vertical-align: middle;
            position: relative;
            z-index: 0;
        }
        .td_tou .star_xx .xx_b, .td_tou .star_xx .xx_a
        {
            width: 60px;
            height: 11px;
            position: absolute;
            top: 5px;
            z-index: 0;
            background: url(../Images/yf_info_bg.png) no-repeat -170px -220px;
        }
        .td_tou .star_xx .xx_a
        {
            background: url(../Images/yf_info_bg.png) no-repeat -170px -208px;
            z-index: 5;
        }
        .td_center
        {
            padding: 0 10px;
            margin: 5px 0 15px 0;
            word-break: break-all;
        }
        .td_center a
        {
            color: #333333 !important;
        }
        .td_center a:link, .td_center a:visited
        {
            color: #333333;
            text-decoration: none;
        }
        .td_center a:hover
        {
            color: #3391E8 !important;
            text-decoration: none;
        }
        .td_bottom
        {
            padding-bottom: 10px;
            color: #999999;
            text-align: right;
        }
        .td_bottom .a_jl
        {
            padding-right: 25px;
        }
        
        /* 播放集数 */
        .play_page { /* width:100%; */ /* padding-bottom:0px; */ /* overflow:hidden; */ /* display: inline-block; */}
.zy_page { width:780px; padding-left:10px; padding-bottom:20px; margin:0 auto;}
.play_page li { width:58px; height:30px; overflow:hidden; margin:7px 6px 0 0; float:left;}
.zy_page li { margin:5px 10px 6px 0;}
.play_page li a {display:block; background-color:#ffffff; border: 1px solid #D3D3D3; height:28px; line-height: 28px; font-size:14px; text-align: center; text-decoration:none!important;}

.play_page li a:hover {border: 1px solid #579FF6; color: #ffffff; background-color:#69ADFD;}
.play_page li a:active {background-color:#69ADFD; border:1px solid #579FF6;color: #FFFFFF;}
.play_page li a:visited {background-color:#DFEFFF;border: 1px solid #C3E1FD;color: #666666;}

/*
.play_page li a { height:22px; line-height:22px; display:block; text-align:center; border: 1px solid #D3D3D3;}
.play_page li a:link,.play_page li a:visited {color: #666666; text-decoration:none;}
.play_page li a:hover {color: #ffffff; text-decoration: none; border: 1px solid #72B5F4; background-color:#72B5F4;}
*/
.play_page li.more { width:116px; font-family:"宋体";}
.play_page li.more_zy { width:194px; font-family:"宋体";}
.play_page li.zy_p { width:194px;}
.play_page li.ds_p2 { width:55px;}
.play_page li.zy_p1 { width:185px;}
.play_page li.more_zy1 { width:185px; font-family:"宋体";}

    </style>

    <script src="/JS/jquery-1.8.2.js" type="text/javascript"></script>
    <script src="/JS/bootstrap_gai.min.js" type="text/javascript"></script>

    <script src="/JS/artDialog4.1.7/artDialog.js" type="text/javascript"></script>
    <script src="/JS/artDialog4.1.7/plugins/iframeTools.js" type="text/javascript"></script>
    <link href="/JS/artDialog4.1.7/skins/blue.css" rel="stylesheet" type="text/css" />

</head>
<body>
    <%=head.HTML%>
    <div id="main_content" style="width: 960px;" class="container">
        <div class="area" style="position: relative; padding: 16px 0px 6px 6px; margin-bottom: 6px;">
            <div class="row-fluid">
                <div class="x-m-side span3">
                    <div class="x-m-poster">
                        <a href="<%=FirstURL %>" target="_blank" title="立即播放">
                            <img alt="<%=em.MovieName %>" width="225px" height="300px" src="http://t2.baidu.com/it/u=<%=em.ImagePath %>&fm=20">
                        </a>
                    </div>
                    <div class="x-m-area">
                        
                    </div>
                    <div class="x-m-share">
                        <!-- Baidu Button BEGIN -->
                        <div id="bdshare" class="bdshare_t bds_tools get-codes-bdshare" data="{'text': '电影《<%=em.MovieName %>》分享自@第一影院 ', 'comment':'', 'pic': '/dyImg/img/<%=Sub(em.ImagePath,2) %>/<%=em.ImagePath %>.jpg', 'desc': '<%=em.Intro %>'}">
                            <a class="bds_tsina"></a><a class="bds_tqq"></a><a class="bds_renren"></a><a class="bds_douban">
                            </a><a class="bds_qzone"></a>
                            <!--<span class="bds_more"></span>-->
                        </div>
                        <!-- Baidu Button END -->
                    </div>
                    <div class="x-m-meta">
                    </div>
                </div>
                <div class="span8" style="height: 100%;">
                    <div>
                        <h1 class="x-m-title">
                            <%=em.MovieName %>
                            <%if (em.IsFinish)
                              {%>
                            <span class="muted">（已完结）</span>
                            <%}
                              else
                              { %>
                            <span class="muted">（更新中）</span>
                            <%} %>
                        </h1>
                        <table class="table table-condensed table-striped table-bordered" style="font-size: 12px;">
                            <tr>
                                <td class="span2">
                                    <span class="x-m-label">导演</span>
                                </td>
                                <!-- <td> 陈可辛 </td>-->
                                <td>
                                    <a href="/category/key_<%=em.Director.TrimEnd('/') %>">
                                        <%=em.Director.TrimEnd('/') %></a>
                                </td>
                            </tr>
                            <tr>
                                <td class="span2">
                                    <span class="x-m-label">主演</span>
                                </td>
                                <td>
                                    <%=ALink("/category/key_", em.Actor)%>
                                </td>
                            </tr>
                            <tr>
                                <td class="span2">
                                    <span class="x-m-label">类型</span>
                                </td>
                                <!-- <td> 剧情 </td>-->
                                <td>
                                    <%=ALink("/category/genre_", em.Type)%>
                                </td>
                            </tr>
                            <tr>
                                <td class="span2">
                                    <span class="x-m-label">地区</span>
                                </td>
                                <!-- <td> 中国大陆 / 香港 </td>-->
                                <td>
                                    <%=ALink("/category/region_", em.Area)%>
                                </td>
                            </tr>
                            <%if (!string.IsNullOrEmpty(em.ReleaseTime))
                              { %>
                            <tr>
                                <td class="span2">
                                    <span class="x-m-label">年代</span>
                                </td>
                                <td>
                                    <%=em.ReleaseTime %>
                                </td>
                            </tr>
                            <%} %>
                            <!-- <td> 112分钟 </td>-->
                            <%if (!string.IsNullOrEmpty(em.Length))
                              { %>
                            <tr>
                                <td class="span2">
                                    <span class="x-m-label">片长</span>
                                </td>
                                <td>
                                    <%=em.Length %>
                                </td>
                            </tr>
                            <%} %>
                            <%if (!string.IsNullOrEmpty(em.AnotherName))
                              { %>
                            <tr>
                                <td class="span2">
                                    <span class="x-m-label">别名</span>
                                </td>
                                <td>
                                    <%=em.AnotherName %>
                                </td>
                            </tr>
                            <%} %>
                            <%if (em.DBScore > 0)
                              { %>
                            <tr class="x-m-rating">
                                <td class="span2">
                                    <span class="x-m-label">评分</span>
                                </td>
                                <td>
                                    豆瓣： <a rel="nofollow" href="javascript:void(0)" target="_blank" data-toggle="tooltip"
                                        class="x-tooltip" title="豆瓣上的信息"><span class="badge" style="color: green; font-weight: bold;">
                                            <%=em.DBScore %></span></a>
                                </td>
                            </tr>
                            <%} %>
                        </table>
                    </div>
                    <%if (!string.IsNullOrEmpty(FirstURL))
                      {%>
                    <div style="position: absolute; bottom: 6px; float: left;">
                        <div class="x-m-subtitle">
                            <div style="float: left;">
                                <a id="li_ji_bo_fang" href="<%=FirstURL %>" target="_blank" class="btn btn-large btn-primary"
                                    title="立即播放"><i class="icon-white icon-play"></i>立即播放 </a>
                            </div>
                            <div style="float: left; margin-top: 23px; margin-left: 5px;">
                                <a id="more_movie_resource" style="cursor: pointer;" title="其他资源"><span style="font-size: 13px;">
                                    更多播放地址</span></a>
                            </div>
                        </div>
                    </div>
                    <%} %>
                </div>
            </div>
        </div>
         <%if (!string.IsNullOrEmpty(tvStr)) { %>
        <div class="area" style="margin-bottom: 6px; margin-top: 6px;">
          <div class="x-m-subtitle" style="margin-bottom: 20px; margin-top: 10px">
                在线播放</div>
            <div class="play_page" data-entryid="ds.xq.liebiao" id="source_youku_13_1" style="display: inline-block;">
                <ul>
                  <%=tvStr %>
                </ul>
            </div>
        </div>

       
          
          <%} %>
        <div class="area" style="margin-bottom: 6px; margin-top: 6px;">
            <div class="row-fluid">
                <div>
                    <div class="" style="clear: both; padding-top: 10px;">
                        <p class="x-m-subtitle">
                            剧情介绍</p>
                        <div class="x-m-summary">
                            <p>
                              &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;  <%=em.Intro %>
                            </p>
                        </div>
                        <br>
                    </div>
                </div>
                <script type="text/javascript" id="wumiiRelatedItems"></script>
            </div>
        </div>

        <!-- 广告位：详细页通栏下 -->
        <div class="area" id="movie_resource" style="margin-bottom: 6px;">
            <div class="" style="clear: both; margin-top: 10px;">
                <p id="video_resource" class="x-m-subtitle">
                    影片资源
                </p>
                <div id="resource_tab" class="tabbable" style="font-size: 13px;">
                    <ul class="nav nav-tabs">
                        <%=Labels %>
                    </ul>
                    <div id="links_table" data-movie-id="50fd7abb342f10672d9f4524" class="tab-content">
                        <!-- Modal -->
                        <%=Links %>
                    </div>
                </div>
                <div style="clear: both">
                </div>
            </div>
        </div>
        
        <div class="well" style="text-align: center; margin-top: 20px; font-size: 13px;">
            <div class="">
            </div>
            <div class="muted">
                Copyright &copy; 2014 第一博客</div>
        </div>
        <div style="display: none;">
        </div>
    </div>
    <div class="btn-group btn-group-vertical z-rightedge-container" style="left: 50%;">
        <a href="javascript:void(0);" class="btn btn-small z-rightedge-buttom" style="display: none;"
            id="gotop"><i class="icon-arrow-up"></i></br>回</br>到</br>顶</br>部 </a><a href="javascript:void(0)"
                style="" class="btn btn-small z-rightedge-buttom" alt="">反</br>馈</br>意</br>见
        </a>
    </div>

    <script>
        function rwu(obj, type, oid) {
            $obj = $(obj);
            href = $(obj).attr('href');
            if (href.indexOf(oid) > 0) {
                return true;
            }
            href = '/' + type + '?id=' + oid + '&url=' + encodeURIComponent(href);
            $(obj).attr('href', href);
        }
        $(function () {
            $("#more_movie_resource").click(function () {

                window.scrollTo(0, $("#movie_resource").offset().top - $(this).offset().top - 50 + $(window).scrollTop());

            });
            $('.x-more-links').click(function (e) {
                $(this).parents('table').find('tr').show();
                $(this).parents('tr').remove();
            });
            $('#links_table').click(function (e) {
                e.stopPropagation();
                var target = $(e.target);
                var report_error = target.attr('data-error-report');
                if (report_error != '' && report_error != null) {
                    e.preventDefault();
                    $('#report_error_modal').modal({
                        keyboard: false,
                        backdrop: 'static',
                        show: true
                    });
                    $('#report_error_submit').attr('data-link-id', report_error);

                }
            });
            $('#report_error_submit').click(function (e) {
                e.preventDefault();
                var th = $(this);
                var movie_id = th.attr('data-movie-id');
                var link_id = th.attr('data-link-id');
                var tmp = '';
                var reason = $('input[name=error_reason]:checked').val()
                $('#report_error_modal').modal('hide');
                $.post(
'/report_error_links',
{ '_xsrf': cookie.getCookie("_xsrf"), 'movie_id': movie_id, 'link_id': link_id, 'reason': reason },
function (data) {
},
'json'
);
            });
            $('#resource_tab').delegate('a[data-toggle="tab"]', 'click', function (e) {
                e.preventDefault();
                e.stopPropagation();
                $(this).tab('show');
            });
        });
</script>
    

  

    <script type="text/javascript">
        function add(MovieId, site) {
            art.dialog.open('/Manage/LinkInfo.aspx?MovieId=' + MovieId+"&Site="+site, { title: '添加播放链接', width: 400, height: 500 });
        }

        function update(SourceId) {
            art.dialog.open('/Manage/LinkInfo.aspx?SourceId=' + SourceId, { title: '修改播放链接', width: 400, height:500 });
        }
        function del(mid) {
            if (confirm("确定删除")) {
            
                $.ajax({
                    type: "post",
                    url: "LinkInfo.aspx",
                    data: "method=delete&SourceId=" + mid,
                    success: function (data) {
                        if (data == "True") {
                            art.artDialog.alert("删除成功！");
                        } else {
                            art.artDialog.alert("删除失败！");
                        }
                    },
                    error: function () { alert("error!"); }
                });

            }


        }
        function init() {
            window.location.href = "/Manage/PlayLinkInfo.aspx?MovieId=" + $("#MovieId").val();
        }

        function active(a) {
            $("#tab" + a).siblings().removeClass();

        }
    </script>
</body>
</html>

