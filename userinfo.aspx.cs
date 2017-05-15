using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SQLite;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class userinfo : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        int userID = int.Parse(Request.QueryString["userID"]);
        using (SQLiteConnection dbConnection = new SQLiteConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
        {
            using (SQLiteCommand comm = dbConnection.CreateCommand())
            {
                dbConnection.Open();
                comm.CommandText = "SELECT * FROM users WHERE id = " + userID;
                SQLiteDataReader dataReader = comm.ExecuteReader();
                dataReader.Read();
                username.Text = dataReader.GetString(1);
                email.Text = dataReader.GetString(3);
                school.Text = dataReader.GetString(4);
                major.Text = dataReader.GetString(5);
                grade.Text = "" + dataReader.GetInt32(6);
                stuID.Text = dataReader.GetString(7);
                dbConnection.Close();
            }
        }
        if ((int)Session["userID"] == userID || Session["currentUser"].ToString() == "admin")
        {
            Button edit = new Button();
            edit.Text = "编辑信息";
            edit.CssClass = "ui negative compact button";
            edit.ID = "edit" + userID.ToString();
            edit.Click += new EventHandler(this.onEdit);
            PlaceHolder.Controls.Add(edit);
        }
        if (Session["currentUser"].ToString() == "admin")
        {
            Button delete = new Button();
            delete.Text = "删除";
            delete.CssClass = "ui negative compact button";
            delete.ID = userID.ToString();
            delete.Click += new EventHandler(this.onDelete);
            PlaceHolder.Controls.Add(delete);
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
                comm.CommandText = "DELETE FROM users WHERE id = " + delete.ID;
                comm.ExecuteNonQuery();
                dbConnection.Close();
                Response.Write(@"<script>alert('删除成功！');</script>");
                Response.Redirect("forum.aspx");
            }
        }

    }

    protected void onEdit(object sender, EventArgs e)
    {
        Button delete = (Button)sender;
        Server.Transfer("edituser.aspx?userID=" + delete.ID.Substring(4));
    }
}