<%@ Page Language="C#" AutoEventWireup="true" CodeFile="forum.aspx.cs" Inherits="forum" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <div>
            <h3>welcome</h3>
            <asp:HyperLink ID="userInfoLink" runat="server">
                <asp:Label ID="currentUser" runat="server" Text="Label"></asp:Label>
            </asp:HyperLink>
            <asp:Button ID="Logout" runat="server" Text="登出" OnClick="Logout_Click" />
        </div>
        <asp:PlaceHolder ID="posts" runat="server"></asp:PlaceHolder>
        <div class = "newPost">
            <h3>新帖子</h3>
            <h4>标题</h4>
            <asp:TextBox ID="Title" runat="server"></asp:TextBox>
            <h4>内容</h4>
            <asp:TextBox ID="Content" runat="server"></asp:TextBox>
            <asp:Button ID="Post" runat="server" Text="发表" onClick="onPost"/>
        </div>
    </div>
    </form>
</body>
</html>
