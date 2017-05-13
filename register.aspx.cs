using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class register : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void registerClick(object sender, EventArgs e)
    {
        // Check valid
        if (username.Text == "" || email.Text == "" || school.Text == "" ||
            major.Text == "" || grade.Text == "" || stuID.Text == "" ||
            password.Text == "")
        {
            Response.Write(@"<script>alert('全部都不能为空哦！');</script>");
            return;
        }
        int Grade;
        if (!int.TryParse(stuID.Text, out Grade) || ! int.TryParse(grade.Text, out Grade))
        {
            Response.Write(@"<script>alert('年级和学号必须为数字！');</script>");
        }

        // Store in db
        using (SqlConnection userDBConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
        {
            using (SqlCommand comm = userDBConnection.CreateCommand())
            {
                userDBConnection.Open();
                comm.CommandText = "SELECT * FROM users WHERE username = " + username.Text;
                SqlDataReader dataReader = comm.ExecuteReader();
                if (dataReader.Read())
                {
                    Response.Write(@"<script>alert('用户已存在！');</script>");
                    return;
                }

                // md5 password
                string md5passwd;
                using (MD5 md5 = MD5.Create())
                {
                    byte[] hashData = md5.ComputeHash(Encoding.Default.GetBytes(password.Text));
                    StringBuilder stringbuilder = new StringBuilder();
                    for (int i = 0; i < hashData.Length; i++)
                    {
                        stringbuilder.Append(hashData[i].ToString());
                    }
                    md5passwd = stringbuilder.ToString();
                }

                comm.CommandText = "INSERT INTO users (username, password, email, school, major, grade, stuID) VALUES(@usernam, @password, " +
                    "@email, @school, @major, @grade, @stuID)";
                comm.Parameters.AddWithValue("@username", username.Text);
                comm.Parameters.AddWithValue("@password", md5passwd);
                comm.Parameters.AddWithValue("@email", email.Text);
                comm.Parameters.AddWithValue("@school", school.Text);
                comm.Parameters.AddWithValue("@major", major.Text);
                comm.Parameters.AddWithValue("@grade", Grade);
                comm.Parameters.AddWithValue("@stuID", stuID.Text);
                comm.ExecuteNonQuery();
                Response.Write(@"<script>alert('注册成功！');</script>");
                Server.Transfer("Default.aspx");
            }
        }
    }
}