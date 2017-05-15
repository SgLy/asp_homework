using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SQLite;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class edituser : System.Web.UI.Page
{
    int userID;
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
        if (Request.QueryString["userID"] == null)
            userID = (int)Session["userID"];
        else
            userID = int.Parse(Request.QueryString["userID"]);
        if (userID != (int)Session["userID"] && Session["currentUser"].ToString() != "admin")
        {
            Response.Write(@"<script>alert('你无权修改！');</script>");
            Server.Transfer("forum.aspx");
            return;
        }
        if (!IsPostBack)
        {

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
        }
    }

    protected void Comfirm_Click(object sender, EventArgs e)
    {
        // Check valid
        if (email.Text == "" || school.Text == "" ||
            major.Text == "" || grade.Text == "" || stuID.Text == "" ||
            password.Text == "")
        {
            Response.Write(@"<script>alert('全部都不能为空哦！');</script>");
            return;
        }
        int Grade;
        if (!int.TryParse(stuID.Text, out Grade) || !int.TryParse(grade.Text, out Grade))
        {
            Response.Write(@"<script>alert('年级和学号必须为数字！');</script>");
            return;
        }

        // Store in db
        using (SQLiteConnection userDBConnection = new SQLiteConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
        {
            using (SQLiteCommand comm = userDBConnection.CreateCommand())
            {
                GC.Collect();
                userDBConnection.Open();

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

                comm.CommandText = "UPDATE users SET password=@password, email=@email, school=@school, major=@major, " +
                    "grade=@grade, stuID=@stuID WHERE id=" + userID;
                comm.Parameters.AddWithValue("@password", md5passwd);
                comm.Parameters.AddWithValue("@email", email.Text);
                comm.Parameters.AddWithValue("@school", school.Text);
                comm.Parameters.AddWithValue("@major", major.Text);
                comm.Parameters.AddWithValue("@grade", Grade);
                comm.Parameters.AddWithValue("@stuID", stuID.Text);
                comm.ExecuteNonQuery();
                Response.Write(@"<script>alert('修改成功！');</script>");
                Server.Transfer("Default.aspx");
            }
        }
    }
}