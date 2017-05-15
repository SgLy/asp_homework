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
    protected string content;
    protected string title;
    protected void Page_PreInit(Object sender, EventArgs e) {
        if (Session["menuStyle"] == null)
            Session["menuStyle"] = "top";
        this.MasterPageFile = "~/" + (string)Session["menuStyle"] + "-menu.master";
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["currentUser"] == null) {
            Response.Redirect("Default.aspx");
            return;
        }
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

                title = dataReader.GetString(2);
                content = dataReader.GetString(4);

                dataReader.Close();
                // Get reviews
                comm.CommandText = "SELECT * FROM reviews WHERE postID = " + postID;
                dataReader = comm.ExecuteReader();

                string eachPost1 =
                    @"<div class=" +
                    "\"ui vertical segment\"" +
                    ">由<a href=" +
                    "\"userinfo.aspx?userID=@userID\"" +
                    ">@author</a>于@date发布";
                string eachPost2 =
                    @"<div class=" +
                    "\"ui raised segment\"" +
                    "><p>@content</p></div></div>";
                while (dataReader.Read())
                {
                    // Get author
                    comm2.Connection = dbConnection;
                    comm2.CommandText = "SELECT * FROM users where id = " + dataReader.GetInt32(2);
                    authorNameGet = comm2.ExecuteReader();
                    authorNameGet.Read();
                    string temp = eachPost1.Replace("@author", authorNameGet.GetString(1));
                    authorNameGet.Close();
                    temp = temp.Replace("@date", dataReader.GetDateTime(3).ToString("yyyy-MM-dd HH:mm"));
                    temp = temp.Replace("@userID", dataReader.GetInt32(2).ToString());

                    Reviews.Controls.Add(new LiteralControl(temp));
                    if (Session["currentUser"].ToString() == "admin")
                    {
                        Button delete = new Button();
                        delete.CssClass= "circular compact ui right floated basic negative button";
                        delete.Text = "删除";
                        delete.ID = dataReader.GetInt32(0).ToString();
                        delete.Click += new EventHandler(this.onDelete);
                        Reviews.Controls.Add(delete);
                    }

                    temp = eachPost2.Replace("@content", dataReader.GetString(4));
                    Reviews.Controls.Add(new LiteralControl(temp));
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
                    ", DateTime('now'), \"" + ContentPost.Text + "\")";
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