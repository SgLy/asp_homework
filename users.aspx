<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/left-menu.master" CodeFile="users.aspx.cs" Inherits="users" %>

<asp:Content ID="Content1" ContentPlaceHolderID="title" runat="server">用户管理</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
  <asp:GridView ID="GridView1" runat="server">
  </asp:GridView>
  <asp:PlaceHolder ID="PlaceHolder" runat="server"></asp:PlaceHolder>
  <table class="ui striped table">
    <thead>
      <tr>
        <th>userID</th>
        <th>用户名</th>
        <th>邮箱</th>
        <th>学院</th>
        <th>专业</th>
        <th>年级</th>
        <th>学号</th>
        <th></th>
      </tr>
    </thead>
    <% foreach (Dictionary<string, string> d in data) { %>
    <tr>
      <td><%= d["userID"] %></td>
      <td><a href="/userinfo.aspx?userID=<%= d["userID"] %>"><%= d["username"] %></a></td>
      <td><%= d["email"] %></td>
      <td><%= d["school"] %></td>
      <td><%= d["major"] %></td>
      <td><%= d["grade"] %></td>
      <td><%= d["stuID"] %></td>
      <td><a href="edituser.aspx?userID=<%= d["userID"] %>">编辑信息</a></td>
    </tr>
    <% } %>
  </table>
</asp:Content>
