<%@ Page Title="" Language="C#" MasterPageFile="~/top-menu.master" AutoEventWireup="true" CodeFile="manage.aspx.cs" Inherits="manage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="title" Runat="Server">管理功能</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="content" Runat="Server">
  <h2 class="ui header">功能一览</h2>
  <asp:TreeView ID="TreeView1" runat="server" DataSourceID="XmlDataSource1" CollapseImageUrl="static/img/collapse.png" ExpandImageUrl="static/img/expand.png">
    <DataBindings>
      <asp:TreeNodeBinding DataMember="逸仙论坛" NavigateUrlField="nav" TextField="#Name" />
      <asp:TreeNodeBinding DataMember="贴子一览" TextField="#Name" NavigateUrlField="nav"/>
      <asp:TreeNodeBinding DataMember="查看贴子" TextField="#Name" NavigateUrlField="nav"/>
      <asp:TreeNodeBinding DataMember="菜单样式切换" TextField="#Name" NavigateUrlField="nav"/>
      <asp:TreeNodeBinding DataMember="登录" TextField="#Name" NavigateUrlField="nav"/>
      <asp:TreeNodeBinding DataMember="登出" TextField="#Name" NavigateUrlField="nav"/>
      <asp:TreeNodeBinding DataMember="注册新账户" TextField="#Name" NavigateUrlField="nav"/>
      <asp:TreeNodeBinding DataMember="查看用户信息" TextField="#Name" NavigateUrlField="nav"/>
      <asp:TreeNodeBinding DataMember="修改自己信息" TextField="#Name" NavigateUrlField="nav"/>
      <asp:TreeNodeBinding DataMember="管理员-查看所有用户" TextField="#Name" NavigateUrlField="nav"/>
    </DataBindings>
  </asp:TreeView>
  <asp:XmlDataSource ID="XmlDataSource1" runat="server" DataFile="~/functions.xml"></asp:XmlDataSource>
</asp:Content>

