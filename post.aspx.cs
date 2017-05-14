using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SQLite;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class post : System.Web.UI.Page
{
    int postID;

    protected void Page_Load(object sender, EventArgs e)
    {
        postID = int.Parse(Request.QueryString["id"]);

        using (SQLiteConnection dbConnection = new SQLiteConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
        {
            using (SQLiteCommand comm = dbConnection.CreateCommand())
            {
                dbConnection.Open();

                // Get post information
                comm.CommandText = "SELECT * FROM posts WHERE id = " + postID;
                SQLiteDataReader dataReader = comm.ExecuteReader();
                dataReader.Read();
                Title.Text = dataReader.GetString(2);
                // Get author
                SQLiteCommand comm2 = new SQLiteCommand();
                comm2.Connection = dbConnection;
                comm2.CommandText = "SELECT * FROM users where id = " + dataReader.GetInt32(1);
                SQLiteDataReader authorNameGet = comm2.ExecuteReader();
                authorNameGet.Read();
                AuthorInfo.NavigateUrl = "userinfo.aspx?userID=" + dataReader.GetInt32(1);
                Author.Text = authorNameGet.GetString(1);
                authorNameGet.Close();
                Date.Text = dataReader.GetDateTime(3).ToString("yyyy-MM-dd HH:mm");
                Content.Text = dataReader.GetString(4);

                dataReader.Close();
                // Get reviews
                comm.CommandText = "SELECT * FROM reviews WHERE postID = " + postID;
                dataReader = comm.ExecuteReader();

                string eachPost = @"<div><h4>作者: <a href=" + "\"userinfo.aspx?userID=@userID\"" + ">@author</a></h4><h4>时间: @date</h4><p>内容：@content</p></div>";
                while (dataReader.Read())
                {
                    string temp = eachPost.Replace("@content", dataReader.GetString(4));
                    // Get author
                    comm2.Connection = dbConnection;
                    comm2.CommandText = "SELECT * FROM users where id = " + dataReader.GetInt32(2);
                    authorNameGet = comm2.ExecuteReader();
                    authorNameGet.Read();
                    temp = temp.Replace("@author", authorNameGet.GetString(1));
                    authorNameGet.Close();
                    temp = temp.Replace("@date", dataReader.GetDateTime(3).ToString("yyyy-MM-dd HH:mm"));
                    temp = temp.Replace("@userID", dataReader.GetInt32(2).ToString());

                    Reviews.Controls.Add(new LiteralControl(temp));
                    if (Session["currentUser"].ToString() == "admin")
                    {
                        Button delete = new Button();
                        delete.Text = "删除";
                        delete.ID = dataReader.GetInt32(0).ToString();
                        delete.Click += new EventHandler(this.onDelete);
                        Reviews.Controls.Add(delete);
                    }
                }

                dbConnection.Close();
            }
        }
    }

    protected void OnPostReview(object sender, EventArgs e)
    {
        if (ContentPost.Text == "")
        {
            Response.Write(@"<script>alert('所填的内容不能为空！');</script>");
            return;
        }

        using (SQLiteConnection dbConnection = new SQLiteConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
        {
            using (SQLiteCommand comm = dbConnection.CreateCommand())
            {
                GC.Collect();
                dbConnection.Open();
                comm.CommandText = "INSERT INTO reviews (postID, authorID, curtime, content) VALUES(" + postID + ", " + Session["userID"] +
                    ", DateTime('now'), \"" + Content.Text + "\")";
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
                comm.CommandText = "DELETE FROM reviews WHERE id = " + delete.ID;
                comm.ExecuteNonQuery();
                dbConnection.Close();
                Response.Write(@"<script>alert('删除成功！');</script>");
                Response.Redirect(Request.RawUrl);
            }
        }

    }
}