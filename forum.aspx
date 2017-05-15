<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/top-menu.master" CodeFile="forum.aspx.cs" Inherits="forum" %>

<asp:Content ID="Content1" ContentPlaceHolderID="title" runat="Server">帖子列表 - 逸仙论坛</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="Server">
  <style>
    .ui.raised.segment {
      margin-top: 2em;
    }
    h2.title {
      margin-top: 1em;
    }
    #newPost {
      margin-top: 4em;
    }
  </style>
  <div class="ui container">
    <asp:placeholder id="posts" runat="server"></asp:placeholder>
  </div>
  <div class="ui container" id="newPost">
    <h2 class="title">新帖子</h2>
    <h3>标题</h3>
    <div class="field">
      <asp:textbox id="Title" runat="server"></asp:textbox>
    </div>
    <h3>内容</h3>
    <div class="field">
      <asp:textbox id="Content" runat="server" textmode="MultiLine" rows="10"></asp:textbox>
    </div>
    <asp:button class="ui fluid large positive submit button" id="Post" runat="server" text="发表" onclick="onPost" />
  </div>
</asp:Content>
