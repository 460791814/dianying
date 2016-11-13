<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ChannelList.aspx.cs" Inherits="DianYing.Web.Manage.ChannelList" %>

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
        function add() {
            art.artDialog({
                title: "添加电影分类",
             content:document.getElementById("Show"),
             ok: function () {
                 $.ajax({
                     type: "post",
                     url: "/Manage/ChannelList.aspx",
                     data: "method=add&ChannelName=" + $("#ChannelName").val(),
                     success: function (data) {

                         if (data == "True") {
                             art.artDialog.alert("分类添加成功！");
                             window.location.href = "/Manage/ChannelList.aspx";
                         } else {
                             art.artDialog.alert("分类添加失败！");
                         }
                     },
                     error: function () { alert("error!"); }
                 });
                 return false;
             }
            });
        }
        function update(id, name) {
            $("#ChannelName").val(name);
            art.artDialog({
                title: "修改电影分类",
                content: document.getElementById("Show"),
                ok: function () {
                    $.ajax({
                        type: "post",
                        url: "/Manage/ChannelList.aspx",
                        data: "method=update&ChannelName=" + $("#ChannelName").val()+"&ChannelID="+id,
                        success: function (data) {

                            if (data == "True") {
                                art.artDialog.alert("分类修改成功！");
                                window.location.href = "/Manage/ChannelList.aspx";
                            } else {
                                art.artDialog.alert("分类修改失败！");
                            }
                        },
                        error: function () { alert("error!"); }
                    });
                    return false;
                }
            });
        }
        function deleteChannel(channelId)
        {
            if (confirm("删除后不可恢复，确认删除?")) {

                $.ajax({
                    type: "post",
                    url: "/Manage/ChannelList.aspx",
                    data: "method=delete&ChannelID=" + channelId,
                    success: function (data) {

                        if (data == "True") {
                            art.artDialog.alert("删除成功！");
                            window.location.href = "/Manage/ChannelList.aspx";
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
    <form id="form1" runat="server">
    <div>
      <%=head.HTML%>
        <div id="main_content" style="width: 960px;" class="container">
            <div class="row">
                
                <div class="span9">
                    <div class="well home_content">
                        <h4>视频分类</h4>
                    <div class="alert alert-info">请慎重填写，且不要轻易修改或删除！</div>
                        
                        <table class="table">
                            <tbody>
                                <tr>
                                    <th>
                                        电影分类ID
                                    </th>
                                    <th>
                                         电影分类名称
                                    </th>
                                    <th>
                                    </th>
                                </tr>
                                <%for (int i = 0; i < list.Count; i++)
                                  {%>
                                        <tr class="x-plan-item">
                                    <td>
                                        <%=list[i].ChannelID %>
                                    </td>
                                    <td>
                                       <%=list[i].ChannelName %>
                                    </td>
                                    <td>
                                        <div class="x-plan-buttons pull-right">
                                            <a  href="javascript:void(0);" onclick="update('<%=list[i].ChannelID %>','<%=list[i].ChannelName %>')" class="x-plan-seen btn btn-mini"> <i class="icon-check"></i>编辑</a> 
                                                <a  href="javascript:void(0);"  onclick="deleteChannel('<%=list[i].ChannelID %>')" class="x-plan-remove btn btn-mini"><i class="icon-remove"></i>移除</a>
                                        </div>
                                    </td>
                                </tr>
                                 <% } %>

                              <tr class="x-plan-item"><td><a class='btn btn-primary btn-small' target='_blank' rel='nofollow' onclick=add() href='javascript:void(0)'><i class='icon-white icon-play'></i>添加电影分类</a></td></tr>

                            </tbody>
                        </table>
                    </div>
                </div>
            </div>

        </div>
    </div>
    </form>
         <div class="control-group" style="display:none" id="Show">
                            <label class="control-label">
                                电影分类名称</label>
                            <div class="controls">
                                <input type="text" name="ChannelName" id="ChannelName" value="">
                            </div>
                        </div>
</body>
</html>
