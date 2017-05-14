<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/top-menu.master" CodeFile="userinfo.aspx.cs" Inherits="userinfo" %>

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
              <asp:Label ID="username" runat="server"></asp:Label></td>
          </tr>
          <tr>
            <td class="collapsing">邮箱</td>
            <td>
              <asp:Label ID="email" runat="server"></asp:Label></td>
          </tr>
          <tr>
            <td class="collapsing">学院</td>
            <td>
              <asp:Label ID="school" runat="server"></asp:Label></td>
          </tr>
          <tr>
            <td class="collapsing">专业</td>
            <td>
              <asp:Label ID="major" runat="server"></asp:Label></td>
          </tr>
          <tr>
            <td class="collapsing">年级</td>
            <td>
              <asp:Label ID="grade" runat="server"></asp:Label></td>
          </tr>
          <tr>
            <td class="collapsing">学号</td>
            <td>
              <asp:Label ID="stuID" runat="server"></asp:Label></td>
          </tr>
        </tbody>
      </table>
    </div>
    <div class="sixteen wide column">
      <asp:PlaceHolder ID="PlaceHolder" runat="server"></asp:PlaceHolder>
    </div>
  </div>
</asp:Content>
