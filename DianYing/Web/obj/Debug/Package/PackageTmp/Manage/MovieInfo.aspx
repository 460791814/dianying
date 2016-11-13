<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MovieInfo.aspx.cs" Inherits="DianYing.Web.Manage.MovieInfo" %>

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
    <script src="/JS/bootstrap_gai.min.js" type="text/javascript"></script>
    <script type="text/javascript">

        function SetVal(p, d) {
            $("#" + p).val(d);
        }
        function Check() {
            var b = true;
            var movieName = $("#MovieName").val().replace(/[ ]/g, "");
            var imgpath = $("#ImagePath").val().replace(/[ ]/g, "");
            if (movieName == "") {
                art.dialog.alert("电影的名称不能为空！");
                b = false;
            }
            if (imgpath == "") {
                art.dialog.alert("电影的图片不能为空！");
                b = false;
            }
            return b;
        }
        function GetType() {

            var t = "";
            $('input[name="t"]:checked').each(function () {
                t += $(this).val() + '/';
            });
            $("#Type").val(t);
        }
        function GetArea() {

            var a = "";
            $('input[name="a"]:checked').each(function () {
                a += $(this).val() + '/';
            });
            $("#Area").val(a);
        }
    </script>
</head>
<body>
   <%=head.HTML%>
    <form id="form1" runat="server" action="MovieInfo.aspx" method="post">
    <div class="container">
    <div class="span9">
<div class="well home_content">

<fieldset>
<legend>视频基本信息</legend>
<div class="alert alert-info">
为了程序的扩展性及对多种视频风格的聚合，程序存储都是以字符串为主，请按提示进行灵活填写！
</div>


<div class="">
<form class="form-horizontal" method="post">
<input type="hidden" name="method" value="<%=method %>" />
<input type="hidden" name="MovieId"  value="<%=em.MovieId %>">
<div class="control-group">
<label class="control-label">视频类型</label>
<div class="controls">
<select name="ChannelID" class="span2">

<%= GetChannel(em.ChannelID)%>

</select>
</div>
</div>
<div class="control-group">
<label class="control-label">电影名称</label>
<div class="controls">
<input type="text" name="MovieName" id="MovieName"  value="<%= em.MovieName %>">
</div>
</div>

<div class="control-group">
<label class="control-label">电影别名</label>
<div class="controls">
<input type="text" name="AnotherName"  value="<%= em.AnotherName %>">
</div>
</div>
<div class="control-group">
<label class="control-label">导演（多个导演请用“/”进行区分）</label>
<div class="controls">
<input type="text" name="Director" value="<%=em.Director %>">
</div>
</div>

<div class="control-group">
<label class="control-label">主演（示例：徐峥/黄渤/余男/多布杰/王双宝）</label>
<div class="controls">
<input type="text" name="Actor" value="<%=em.Actor %>">
</div>
</div>

<div class="control-group">
<label class="control-label">类型（示例：喜剧/动作/爱情）</label>
<div class="controls">
<input type="hidden" name="Type" id="Type" value="<%=em.Type %>">
     <%for (int i = 0; i < Tool.Data.MovieType.Length; i++)
                          {%>
                          <%if (!string.IsNullOrEmpty(em.Type))
                            { %>
                            
                         
                          <%if (em.Type.Contains(Tool.Data.MovieType[i]))
                            {%>
                              <input type="checkbox" checked="checked" name="t" value="<%=Tool.Data.MovieType[i]%>" onclick="GetType()" /><span><%=Tool.Data.MovieType[i]%></span> 
                            <%}
                            else { %>
                              <input type="checkbox" name="t" value="<%=Tool.Data.MovieType[i]%>" onclick="GetType()" /><span><%=Tool.Data.MovieType[i]%></span> 
                            <%} %>

                  <%}
                            else { %>
                             <input type="checkbox" name="t" value="<%=Tool.Data.MovieType[i]%>" onclick="GetType()" /><span><%=Tool.Data.MovieType[i]%></span> 
                            <%} %>
                                  
                         <% } %>
</div>
</div>

<div class="control-group">
<label class="control-label">地区（示例：中国大陆/香港）</label>
<div class="controls">
<input type="hidden" name="Area" id="Area" value="<%=em.Area %>">
     <%for (int i = 0; i < Tool.Data.MovieArea.Length; i++)
                          {%>
                            <%if (!string.IsNullOrEmpty(em.Area))
                            { %>
                              <%if (em.Type.Contains(Tool.Data.MovieArea[i]))
                            {%>
                             <input type="checkbox" checked="checked" name="a" value="<%=Tool.Data.MovieArea[i]%>" onclick="GetArea()" /><%=Tool.Data.MovieArea[i]%>
                            <%}
                            else { %>
                             <input type="checkbox" name="a" value="<%=Tool.Data.MovieArea[i]%>" onclick="GetArea()"/><%=Tool.Data.MovieArea[i]%>
                            <%} %>
                              
                               <%}  else { %>
                               <input type="checkbox" name="a" value="<%=Tool.Data.MovieArea[i]%>" onclick="GetArea()"/><%=Tool.Data.MovieArea[i]%>
                            <%} %>
                         <% } %>
                      
</div>
</div>

<div class="control-group">
<label class="control-label">年代(1920-至今,只能为数字)</label>
<div class="controls">
    <select name="Year">
     <%for (int i = DateTime.Now.Year; i >= 1920; i--)
       {%>
           <%if (i == em.Year)
             {%>
            <option selected="selected" value="<%=i %>"><%=i%></option>
             <%}
             else {%>
              <option value="<%=i %>"><%=i%></option>
             <%} %>

       <%} %>
       

    </select>

</div>
</div>

<div class="control-group">
<label class="control-label">上映时间（示例：2013-05-17(中国大陆)/2013-05-30(香港)）</label>
<div class="controls">
<input type="text" name="ReleaseTime" value="<%=em.ReleaseTime %>"/>
</div>
</div>
<div class="control-group">
<label class="control-label">片长(示例：117分钟)</label>
<div class="controls">
<input type="text" name="Length" value="<%=em.Length %>">
</div>
</div>

<div class="control-group">
<label class="control-label">豆瓣评分（0到10，示例：8.8）</label>
<div class="controls">
<input type="text" name="DBScore" value="<%=em.DBScore %>">
</div>
</div>


<div class="control-group">
<label class="control-label">简介</label>
<div class="controls">
<textarea  name="Intro" style="margin: 0px 0px 10px; width: 750px; height: 148px; ">
 <%= em.Intro%>
</textarea>
</div>
</div>
<input type="hidden" name="ImagePath" id="ImagePath" value="<%=em.ImagePath %>" />
<div class="control-group">
<label class="control-label">图片</label>
<div class="controls" id="img" runat="server">
       <iframe  src="UploadImg.aspx?img=<%=em.ImagePath %>"  width="100%" height="180px"  frameborder="0" >
                            </iframe>
</div>


</div>

<div class="control-group">
<label class="control-label">是否完结</label>
<div class="controls">
<select name="IsFinish" class="span2">

<option value="true" >已完结</option>

<option value="false">未完结</option>



</select>
</div>
</div>

<div class="control-group">
<div class="controls">
<input type="submit" class="btn btn-primary" value="保存修改" onClick="return Check()" name="save_btn">
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
