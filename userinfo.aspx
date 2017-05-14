<%@ Page Language="C#" AutoEventWireup="true" CodeFile="userinfo.aspx.cs" Inherits="userinfo" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <h5>用户名</h5>
        <asp:Label id="username" runat="server"></asp:Label>
        <h5>邮箱</h5>
        <asp:Label id="email" runat="server"></asp:Label>
        <h5>学院</h5>
        <asp:Label id="school" runat="server"></asp:Label>
        <h5>专业</h5>
        <asp:Label id="major" runat="server"></asp:Label>
        <h5>年级</h5>
        <asp:Label id="grade" runat="server"></asp:Label>
        <h5>学号</h5>
        <asp:Label id="stuID" runat="server"></asp:Label>
        <asp:PlaceHolder ID="PlaceHolder" runat="server"></asp:PlaceHolder>
    </div>
    </form>
</body>
</html>
