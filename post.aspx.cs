using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
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

        using (SqlConnection dbConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
        {
            using (SqlCommand comm = dbConnection.CreateCommand())
            {
                dbConnection.Open();
                
                // Get post information
                comm.CommandText = "SELECT * FROM posts WHERE id = " + postID;
                SqlDataReader dataReader = comm.ExecuteReader();
                dataReader.Read();
                Title.Text = dataReader.GetString(2);
                // Get author
                comm.CommandText = "SELECT * FROM users WHERE id = " + dataReader.GetInt32(1);
                SqlDataReader authorNameGet = comm.ExecuteReader();
                authorNameGet.Read();
                AuthorInfo.NavigateUrl = "userinfo.aspx?id=" + dataReader.GetInt32(1);
                Author.Text = authorNameGet.GetString(1);
                Date.Text = dataReader.GetDateTime(3).ToLongTimeString();
                Content.Text = dataReader.GetString(4);


                // Get reviews
                comm.CommandText = "SELECT * FROM reviews WHERE postID = " + postID;
                dataReader = comm.ExecuteReader();

                string eachPost = @"<div><h4>auther: @auther</h4><h4>date: @date</h4><p>@content</p></div>";
                while (dataReader.Read())
                {
                    string temp = eachPost.Replace("@content", dataReader.GetString(4));
                    // Get author
                    comm.CommandText = "SELECT * FROM users where id = " + dataReader.GetInt32(1);
                    authorNameGet = comm.ExecuteReader();
                    authorNameGet.Read();
                    temp = temp.Replace("@author", authorNameGet.GetString(1));
                    temp = temp.Replace("@date", dataReader.GetDateTime(3).ToShortTimeString());

                    LiteralControl literal = new LiteralControl(temp);
                    if (Session["currentUser"].ToString() == "admin")
                    {
                        Button delete = new Button();
                        delete.Text = "删除";
                        delete.ID = dataReader.GetInt32(0).ToString();
                        delete.OnClientClick = "onDelete";
                        literal.Controls.Add(delete);
                    }
                    Reviews.Controls.Add(literal);
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

        using (SqlConnection dbConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
        {
            using (SqlCommand comm = dbConnection.CreateCommand())
            {
                dbConnection.Open();
                comm.CommandText = "INSERT INTO reviews (postID, authorID, time, content) VALUES(" + postID + ", " + Session["userID"] + 
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
                comm.CommandText = "DELETE FROM reviews WHERE id = " + delete.ID;
                comm.ExecuteNonQuery();
                dbConnection.Close();
                Response.Write(@"<script>alert('删除成功！');</script>");
            }
        }

    }