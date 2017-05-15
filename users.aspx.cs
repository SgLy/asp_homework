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
    protected List<Dictionary<string, string>> data;
    private Dictionary<string, string> d;
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
                data = new List<Dictionary<string, string>>();
                while (dataReader.Read())
                {
                    d = new Dictionary<string, string>(); 
                    d.Add("userID", dataReader.GetInt32(0).ToString());
                    d.Add("username", dataReader.GetString(1));
                    d.Add("email", dataReader.GetString(3));
                    d.Add("school", dataReader.GetString(4));
                    d.Add("major", dataReader.GetString(5));
                    d.Add("grade", dataReader.GetInt32(6).ToString());
                    d.Add("stuID", dataReader.GetString(7));
                    data.Add(d);
                }
                dbConnection.Close();
            }
        }
    }
}