using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class forum : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        currentUser.Text = Session["currentUser"].ToString();
        userInfoLink.NavigateUrl = "userinfo.aspx?userID=" + Session["userID"];

        // Load posts
        using (SqlConnection dbConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
        {
            using (SqlCommand comm = dbConnection.CreateCommand())
            {
                dbConnection.Open();
                comm.CommandText = "SELECT * FROM posts";
                SqlDataReader dataReader = comm.ExecuteReader();

                string eachPost = @"<div><a href=" + "\"post.aspx?id=@id\"" +  "><h3>@title</h3></a><h4>auther: @auther</h4><h4>date: @date</h4><p>@content</p></div>";
                while (dataReader.Read())
                {
                    string temp = eachPost.Replace("@id", dataReader.GetInt32(0).ToString());
                    temp = temp.Replace("@title", dataReader.GetString(2));
                    // Get author
                    comm.CommandText = "SELECT * FROM users WHERE id = " + dataReader.GetInt32(1);
                    SqlDataReader authorNameGet = comm.ExecuteReader();
                    authorNameGet.Read();
                    temp = temp.Replace("@author", authorNameGet.GetString(1));
                    temp = temp.Replace("@date", dataReader.GetDateTime(3).ToShortTimeString());
                    temp = temp.Replace("@content", dataReader.GetString(4));
                    LiteralControl literal = new LiteralControl(temp);
                    if (Session["currentUser"].ToString() == "admin")
                    {
                        Button delete = new Button();
                        delete.Text = "删除";
                        delete.ID = dataReader.GetInt32(0).ToString();
                        delete.OnClientClick = "onDelete";
                        literal.Controls.Add(delete);
                    }
                    posts.Controls.Add(literal);
                }
                dbConnection.Close();
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

        using (SqlConnection dbConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
        {
            using (SqlCommand comm = dbConnection.CreateCommand())
            {
                dbConnection.Open();
                comm.CommandText = "INSERT INTO posts (authorID, title, time, content) VALUES(" + Session["userID"] + ", " + Title.Text + 
                    ", GETDATE(), " + Content.Text + ")";
                comm.ExecuteNonQuery();
                dbConnection.Close();
                Response.Write(@"<script>alert('发表成功！');</script>");
            }
        }
    }

    protected void onDelete(object sender, EventArgs e)
    {
        Button delete = (Button)sender;
        using (SqlConnection dbConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
        {
            using (SqlCommand comm = dbConnection.CreateCommand())
            {
                dbConnection.Open();
                comm.CommandText = "DELETE FROM posts WHERE id = " + delete.ID;
                comm.ExecuteNonQuery();
                dbConnection.Close();
                Response.Write(@"<script>alert('删除成功！');</script>");
            }
        }
    }
}