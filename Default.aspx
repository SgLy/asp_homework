<%@ Page Title="" Language="C#" MasterPageFile="~/page.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="ui main container">
        <h1 class="ui header">test</h1>
        <div class="login">
            <h5>用户名</h5>
            <asp:textbox id="username" runat="server"></asp:textbox>
            <h5>密码</h5>
            <asp:textbox id="password" runat="server"></asp:textbox>
            <asp:button runat="server" text="登陆" ID="login_button" OnClick="loginClick" />
            <asp:button runat="server" text="注册" ID="register_button" PostBackUrl="register.aspx" />
        </div>
    </div>
</asp:Content>

