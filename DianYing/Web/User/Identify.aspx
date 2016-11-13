<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Identify.aspx.cs" Inherits="DianYing.Web.User.Identify" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
   <link href="/Style/diyibk.min.css" rel="stylesheet" type="text/css" />
   <link href="/Style/Default.css" rel="stylesheet" type="text/css" />
    <link href="/Style/Page.css" rel="stylesheet" type="text/css" />
    <script src="/JS/jquery-1.8.2.js" type="text/javascript"></script>

    <script type="text/javascript">
        function del(d, mid) {
          
            $.getJSON("/Handler/IdentityHandler.ashx?method=" + d + "&status=false&movieid=" + mid, function (json) {

            });
            $("#" + mid).remove();
        }
    </script>
</head>
<body>
<div class="well home_content">

<h4>喜欢的电影（共 <span id="like_count"><%=TotalCount%></span> 部）</h4>




<table class="table">
<tbody><tr><th>电影名称</th><th>上映日期</th><th></th></tr>
    <asp:Repeater ID="RepList" runat="server">
   <ItemTemplate>
<tr class="x-seen-item" id="<%#Eval("MovieId") %>"><td>
<a class="x-seen-a" href="/movie/<%#Eval("MovieId") %>" rel="tooltip" target="_blank" data-original-title="导演：<%#Eval("Director") %>&lt;br&gt;主演：<%#Eval("Actor") %>"> <%#Eval("MovieName") %> </a>
</td>
<td><%#Eval("ReleaseTime") %></td>
<td>
<div class="x-seen-buttons pull-right">
<a  id="del" href="javascript:void(0);" onclick="del('<%=this.Request["action"] %>',<%#Eval("MovieId") %>)" class="x-seen-remove btn btn-mini"> <i class="icon-remove"></i> 移除</a>
</div>
</td>
</tr>
</ItemTemplate>
 </asp:Repeater>
</tbody></table>




</div>
</body>
</html>
