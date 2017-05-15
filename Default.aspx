<%@ Page Title="" Language="C#"  AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="title" runat="Server">登录逸仙论坛</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="Server">
  <style>
    .column {
      max-width: 450px;
    }
  </style>
    <div class="ui middle aligned center aligned grid">
      <div class="column">
        <h2 class="ui header">登录</h2>
        <div class="ui stacked segment">
          <div class="field">
            <div class="ui left icon input">
              <i class="user icon"></i>
              <asp:TextBox clientidmode="Static" ID="username" type="input" placeholder="账号" runat="server"></asp:TextBox>
            </div>
          </div>
          <div class="field">
            <div class="ui left icon input">
              <i class="lock icon"></i>
              <asp:TextBox ClientIDMode="Static" ID="password" type="password" runat="server" TextMode="Password" placeholder="密码"></asp:TextBox>
            </div>
          </div>
          <asp:Button class="ui fluid large positive submit button" runat="server" Text="登录" ID="login_button" OnClick="loginClick" />
        </div>
      </div>
    </div>
</asp:content>
