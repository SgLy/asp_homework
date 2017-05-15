<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/top-menu.master" CodeFile="post.aspx.cs" Inherits="post" %>

<asp:Content ID="Content1" ContentPlaceHolderID="title" runat="Server"><%= title %> - 逸仙论坛</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="Server">
  <style>
    #postReview #review {
      margin-top: 4em;
    }
    .ui.raised.segment {
      margin-top: 2em;
    }
  </style>
  <div class="ui container" id="postInfo">
    <div class="ui vertical segment">
      <asp:Label class="ui huge header" ID="Title" runat="server" Text="Label"></asp:Label></br></br>
    由<asp:HyperLink ID="AuthorInfo" runat="server">
      <asp:Label ID="Author" runat="server" Text="Label"></asp:Label>
    </asp:HyperLink>
      于<asp:Label ID="Date" runat="server" Text="Label"></asp:Label>发布
      <div class="ui raised segment">
        <p><%= content %></p>
      </div>
    </div>
  </div>
  <div class="ui vertical segment" id="review">
    <h3>回复：</h3>
  </div>
  <div class="ui container">
    <asp:placeholder id="Reviews" runat="server"></asp:placeholder>
  </div>
  <div class="ui vertical segment" id="postReview">
    <h3>新回复：</h3>
    <div class="field">
      <asp:TextBox TextMode="MultiLine" Rows="5" ID="ContentPost" runat="server"></asp:TextBox>
    </div>
    <asp:Button class="ui fluid large positive submit button" ID="PostReview" runat="server" Text="回复" OnClick="OnPostReview" />
  </div>
</asp:Content>
