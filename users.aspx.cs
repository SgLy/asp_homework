using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SQLite;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class users : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["currentUser"].ToString() != "admin")
        {
            Response.Write(@"<script>alert('你无权访问！');</script>");
            Server.Transfer("forum.aspx");
            return;
        }
        using (SQLiteConnection dbConnection = new SQLiteConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
        {
            using (SQLiteCommand comm = dbConnection.CreateCommand())
            {
                dbConnection.Open();
                comm.CommandText = "SELECT * FROM users";
                SQLiteDataReader dataReader = comm.ExecuteReader();
                string eachUser = @"<a href=" + "\"userinfo.aspx?userID=@userID\"" + @">@username</a> @email @school @major @grade @stuID";
                while (dataReader.Read())
                {
                    string temp = eachUser;
                    temp = temp.Replace("@userID", dataReader.GetInt32(0).ToString());
                    temp = temp.Replace("@username", dataReader.GetString(1));
                    temp = temp.Replace("@email", dataReader.GetString(3));
                    temp = temp.Replace("@school", dataReader.GetString(4));
                    temp = temp.Replace("@major" ,dataReader.GetString(5));
                    temp = temp.Replace("@grade", dataReader.GetInt32(6).ToString());
                    temp = temp.Replace("@stuID", dataReader.GetString(7));
                    PlaceHolder.Controls.Add(new LiteralControl(temp));
                }
                dbConnection.Close();
            }
        }
    }
}