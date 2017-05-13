using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class userinfo : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        int userID = int.Parse(Request.QueryString["userID"]);
        using (SqlConnection dbConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
        {
            using (SqlCommand comm = dbConnection.CreateCommand())
            {
                dbConnection.Open();
                comm.CommandText = "SELECT * FROM users WHERE id = " + userID;
                SqlDataReader dataReader = comm.ExecuteReader();
                dataReader.Read();
                username.Text = dataReader.GetString(1);
                email.Text = dataReader.GetString(3);
                school.Text = dataReader.GetString(4);
                major.Text = dataReader.GetString(5);
                grade.Text = "" + dataReader.GetInt32(6);
                stuID.Text = dataReader.GetString(7);
            }
        }
    }
}