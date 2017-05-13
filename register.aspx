<%@ Page Language="C#" AutoEventWireup="true" CodeFile="register.aspx.cs" Inherits="register" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <h5>用户名</h5>
        <asp:textbox id="username" runat="server"></asp:textbox>
        <h5>邮箱</h5>
        <asp:textbox id="email" runat="server"></asp:textbox>
        <h5>学院</h5>
        <asp:textbox id="school" runat="server"></asp:textbox>
        <h5>专业</h5>
        <asp:textbox id="major" runat="server"></asp:textbox>
        <h5>年级</h5>
        <asp:textbox id="grade" runat="server"></asp:textbox>
        <h5>学号</h5>
        <asp:textbox id="stuID" runat="server"></asp:textbox>
        <h5>密码</h5>
        <asp:textbox id="password" runat="server"></asp:textbox>
        <asp:Button ID="register_button" runat="server" Text="注册" onClick="registerClick"/>
    </div>
    </form>
</body>
</html>
