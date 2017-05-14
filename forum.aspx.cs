using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SQLite;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class forum : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["currentUser"] == null)
        {
            Response.Redirect("Default.aspx");
            return;
        }
        currentUser.Text = Session["currentUser"].ToString();
        userInfoLink.NavigateUrl = "userinfo.aspx?userID=" + Session["userID"];

        // Load posts
        using (SQLiteConnection dbConnection = new SQLiteConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
        {
            using (SQLiteCommand comm = dbConnection.CreateCommand())
            {
                dbConnection.Open();
                comm.CommandText = "SELECT * FROM posts";
                using (SQLiteDataReader dataReader = comm.ExecuteReader())
                {

                    string eachPost = @"<div><a href=" + "\"post.aspx?id=@id\"" + "><h3>标题：@title</h3></a><h4>作者: @author</h4><h4>时间: @date</h4><p>@content</p></div>";
                    while (dataReader.Read())
                    {
                        string temp = eachPost.Replace("@id", dataReader.GetInt32(0).ToString());
                        temp = temp.Replace("@title", dataReader.GetString(2));
                        // Get author
                        SQLiteCommand comm2 = new SQLiteCommand();
                        comm2.Connection = dbConnection;
                        comm2.CommandText = "SELECT * FROM users where id = " + dataReader.GetInt32(1);
                        using (SQLiteDataReader authorNameGet = comm2.ExecuteReader())
                        {
                            authorNameGet.Read();
                            temp = temp.Replace("@author", authorNameGet.GetString(1));
                            temp = temp.Replace("@date", dataReader.GetDateTime(3).ToString("yyyy-MM-dd HH:mm"));
                            temp = temp.Replace("@content", dataReader.GetString(4));
                            posts.Controls.Add(new LiteralControl(temp));
                            if (Session["currentUser"].ToString() == "admin")
                            {
                                Button delete = new Button();
                                delete.Text = "删除";
                                delete.ID = dataReader.GetInt32(0).ToString();
                                delete.Click += new EventHandler(this.onDelete);
                                posts.Controls.Add(delete);
                            }
                        }
                    }
                }
            }
        }
    }

    protected void onPost(object sender, EventArgs e)
    {
        if (Title.Text == "" || Content.Text == "")
        {
            Response.Write(@"<script>alert('所填的标题和内容不能为空！');</script>");
            return;
        }

        using (SQLiteConnection dbConnection = new SQLiteConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
        {
            using (SQLiteCommand comm = dbConnection.CreateCommand())
            {
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

    protected void onDelete(object sender, EventArgs e)
    {
        Button delete = (Button)sender;
        using (SQLiteConnection dbConnection = new SQLiteConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
        {
            using (SQLiteCommand comm = dbConnection.CreateCommand())
            {
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

    protected void Logout_Click(object sender, EventArgs e)
    {
        Session.Remove("currentUser");
        Response.Redirect("Default.aspx");
    }
}