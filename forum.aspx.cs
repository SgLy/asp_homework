using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SQLite;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class forum : System.Web.UI.Page {
    protected void Page_PreInit(Object sender, EventArgs e) {
        if (Session["menuStyle"] == null)
            Session["menuStyle"] = "top";
        this.MasterPageFile = "~/" + (string)Session["menuStyle"] + "-menu.master";
    }
    protected void Page_Load(object sender, EventArgs e) {
        if (Session["currentUser"] == null) {
            Response.Redirect("Default.aspx");
            return;
        }

        // Load posts
        using (SQLiteConnection dbConnection = new SQLiteConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString)) {
            using (SQLiteCommand comm = dbConnection.CreateCommand()) {
                dbConnection.Open();
                comm.CommandText = "SELECT * FROM posts ORDER BY curtime DESC";
                using (SQLiteDataReader dataReader = comm.ExecuteReader()) {

                    string eachPost1 =
                        @"<div class=" +
                        "\"ui vertical segment\"" +
                        "><a href=" +
                        "\"post.aspx?id=@id\"" +
                        "><h2 class=\"ui title\">@title</h2></a>";
                    string eachPost2 =
                        @"<h4>由@author于@date发布</h4>" +
                        "<div class=" +
                        "\"ui raised segment\"" +
                        "><p>@content</p></div></div>";
                    while (dataReader.Read()) {
                        string temp = eachPost1.Replace("@id", dataReader.GetInt32(0).ToString());
                        temp = temp.Replace("@title", dataReader.GetString(2));
                        // Get author
                        SQLiteCommand comm2 = new SQLiteCommand();
                        comm2.Connection = dbConnection;
                        comm2.CommandText = "SELECT * FROM users where id = " + dataReader.GetInt32(1);
                        using (SQLiteDataReader authorNameGet = comm2.ExecuteReader()) {
                            authorNameGet.Read();
                            posts.Controls.Add(new LiteralControl(temp));
                            if (Session["currentUser"].ToString() == "admin") {
                                Button delete = new Button();
                                delete.Text = "删除";
                                delete.CssClass = "circular compact ui right floated basic negative button";
                                delete.ID = dataReader.GetInt32(0).ToString();
                                delete.Click += new EventHandler(this.onDelete);
                                posts.Controls.Add(delete);
                            }
                            temp = eachPost2.Replace("@content", dataReader.GetString(4));
                            temp = temp.Replace("@author", authorNameGet.GetString(1));
                            temp = temp.Replace("@date", dataReader.GetDateTime(3).ToString("yyyy-MM-dd HH:mm"));
                            posts.Controls.Add(new LiteralControl(temp));
                        }
                    }
                }
            }
        }
    }

    protected void onPost(object sender, EventArgs e) {
        if (Title.Text == "" || Content.Text == "") {
            Response.Write(@"<script>alert('所填的标题和内容不能为空！');</script>");
            return;
        }

        using (SQLiteConnection dbConnection = new SQLiteConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString)) {
            using (SQLiteCommand comm = dbConnection.CreateCommand()) {
                GC.Collect();
                dbConnection.Open();
                comm.CommandText = "INSERT INTO posts (authorID, title, curtime, content) VALUES(" + Session["userID"] + ", \"" + Title.Text +
                    "\", DateTime('now'), \"" + Content.Text + "\")";

                comm.ExecuteNonQuery();
                dbConnection.Close();
                Response.Write(@"<script>alert('发表成功！');</script>");
                Response.Redirect(Request.RawUrl);
            }
        }
    }

    protected void onDelete(object sender, EventArgs e) {
        Button delete = (Button)sender;
        using (SQLiteConnection dbConnection = new SQLiteConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString)) {
            using (SQLiteCommand comm = dbConnection.CreateCommand()) {
                GC.Collect();
                dbConnection.Open();
                comm.CommandText = "DELETE FROM posts WHERE id = " + delete.ID;
                comm.ExecuteNonQuery();
                comm.CommandText = "DELETE FROM reviews WHERE postID = " + delete.ID;
                comm.ExecuteNonQuery();
                dbConnection.Close();
                Response.Write(@"<script>alert('删除成功！');</script>");
                Response.Redirect(Request.RawUrl);
            }
        }
    }

    protected void Logout_Click(object sender, EventArgs e) {
    }
}