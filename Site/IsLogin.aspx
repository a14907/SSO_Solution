<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="IsLogin.aspx.cs" Inherits="Site.IsLogin" %>
<%@ Import Namespace="Site" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <%--<a id="goto" href="http://localhost:14506/home/login/<%=Server.UrlEncode( DEncrypt.Encrypt( "http://"+Request.UrlReferrer.Authority+Request.UrlReferrer.LocalPath)) %>">点击跳转登录页面</a>--%>
    <a id="goto" href="http://www.sso.com/home/login/<%=Server.UrlEncode(DEncrypt.Encrypt( "http://"+Request.UrlReferrer.Authority+Request.UrlReferrer.LocalPath)) %>">点击跳转登录页面</a>
    </div>
    </form>
    <script>
        document.getElementById("goto").click();
    </script>
</body>
</html>
