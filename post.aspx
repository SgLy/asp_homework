<%@ Page Language="C#" AutoEventWireup="true" CodeFile="post.aspx.cs" Inherits="post" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <div class="postInfo">
            <asp:Label ID="Title" runat="server" Text="Label"></asp:Label>
            <asp:HyperLink ID="AuthorInfo" runat="server"><asp:Label ID="Author" runat="server" Text="Label"></asp:Label></asp:HyperLink>
            <asp:Label ID="Date" runat="server" Text="Label"></asp:Label>
            <asp:Label ID="Content" runat="server" Text="Label"></asp:Label>
        </div>
        <div class="reviews">
            <asp:PlaceHolder ID="Reviews" runat="server"></asp:PlaceHolder>
        </div>
        <div class="postReview">
            <asp:TextBox ID="ContentPost" runat="server"></asp:TextBox>
            <asp:Button ID="PostReview" runat="server" Text="Post Review" onClick="OnPostReview"/>
        </div>
    </div>
    </form>
</body>
</html>
