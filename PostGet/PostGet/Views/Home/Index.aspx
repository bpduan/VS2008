<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.Master" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="PostGet.Views.Home.Index" %>
<%@ Import Namespace="PostGet.Models"  %>

<asp:Content ContentPlaceHolderID="HeadContent" runat="server">

    <script src="../../Scripts/jquery-1.8.2.js" type="text/javascript"></script>

<script type="text/javascript">

    function buttonOk()
    {
        $.ajax({
            url: "/Home/DoAjax?psw=ddssll",
            data: {
                id: 100,
                Name: "段胜利",
                psw: "postPsw"
            },
            dataType: "json",
            type: "post",
            cache: false,
            success: function(r)
            {
                alert(r.msg);
                $("#fy").html(r.msg);
            }
        });
    
        
    }
</script>


</asp:Content>
<asp:Content ID="indexContent" ContentPlaceHolderID="MainContent" runat="server">
    <input type="button" onclick="buttonOk()" value="点击" />
    分页
    <div id="fy"></div>
    <a href="/Home/Index?Name=bpduan" >跳转 </a>
    <hr />
    <%=ViewData["page"] %>
    <form action="/Home/Index" method="post">
        <input name="psw" />
        
        
        <input type="submit" value="提交" />
    
    </form>
</asp:Content>
