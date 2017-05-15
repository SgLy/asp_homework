<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/top-menu.master" CodeFile="edituser.aspx.cs" Inherits="edituser" %>

<asp:Content ID="Content1" ContentPlaceHolderID="title" runat="server">编辑<%= username.Text %>@逸仙论坛</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="Server">
  <style>
    #grid {
      max-width: 700px;
      margin: 0 auto;
    }

    #description {
      padding: 0;
    }
  </style>
  <div class="ui middle aligned center aligned grid" id="grid">
    <div class="sixteen wide column">
      <table class="ui definition table" id="description">
        <tbody>
          <tr>
            <td class="collapsing">用户名</td>
            <td>
                <asp:Label ID="username" runat="server" Text="Label"></asp:Label></td>
          </tr>
          <tr>
            <td class="collapsing">密码</td>
            <td>
              <asp:TextBox ID="password" type="password" runat="server"></asp:TextBox></td>
          </tr>
          <tr>
            <td class="collapsing">邮箱</td>
            <td>
              <asp:TextBox ID="email" runat="server"></asp:TextBox></td>
          </tr>
          <tr>
            <td class="collapsing">学院</td>
            <td>
              <asp:TextBox ID="school" runat="server"></asp:TextBox></td>
          </tr>
          <tr>
            <td class="collapsing">专业</td>
            <td>
              <asp:TextBox ID="major" runat="server"></asp:TextBox></td>
          </tr>
          <tr>
            <td class="collapsing">年级</td>
            <td>
              <asp:TextBox ID="grade" runat="server"></asp:TextBox></td>
          </tr>
          <tr>
            <td class="collapsing">学号</td>
            <td>
              <asp:TextBox ID="stuID" runat="server"></asp:TextBox></td>
          </tr>
        </tbody>
      </table>
    </div>
    <div class="sixteen wide column">
      <asp:Button class="ui fluid large positive submit button" runat="server" Text="确认" ID="Comfirm" OnClick="Comfirm_Click" />
    </div>
  </div>
</asp:Content>
