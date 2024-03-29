﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Security.Cryptography;
using System.Text;
using System.Data.SQLite;
using System.Configuration;

public partial class _Default : System.Web.UI.Page
{
    protected void Page_PreInit(Object sender, EventArgs e) {
        if (Session["menuStyle"] == null)
            Session["menuStyle"] = "top";
        this.MasterPageFile = "~/" + (string)Session["menuStyle"] + "-menu.master";
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["currentUser"] != null)
        {
            Server.Transfer("forum.aspx");
        }
    }

    protected void loginClick(object sender, EventArgs e)
    {
        if (username.Text == "")
        {
            Response.Write(@"<script>alert('用户名不能为空！');</script>");
            return;
        } else if (password.Text == "")
        {
            Response.Write(@"<script>alert('密码不能为空！');</script>");
            return;
        }
        string Username = username.Text;
        string Password = password.Text;

        // md5 password;
        string md5passwd;
        using (MD5 md5 = MD5.Create())
        {
            byte[] hashData = md5.ComputeHash(Encoding.Default.GetBytes(Password));
            StringBuilder stringbuilder = new StringBuilder();
            for (int i = 0; i < hashData.Length; i++)
            {
                stringbuilder.Append(hashData[i].ToString());
            }
            md5passwd = stringbuilder.ToString();
        }

        using (SQLiteConnection userDBConnection = new SQLiteConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
        {
            using (SQLiteCommand comm = userDBConnection.CreateCommand())
            {
                userDBConnection.Open();
                comm.CommandText = "SELECT * FROM users WHERE username = \"" + username.Text + "\"";
                using (SQLiteDataReader dataReader = comm.ExecuteReader())
                {

                    if (!dataReader.Read())
                    {
                        Response.Write(@"<script>alert('无此用户！');</script>");
                        return;
                    }
                    if (String.Compare(md5passwd, dataReader.GetString(2)) == 0)
                    {
                        Session["currentUser"] = username.Text;
                        Session["userID"] = dataReader.GetInt32(0);
                        Server.Transfer("forum.aspx");
                    }
                    else
                    {
                        Response.Write(@"<script>alert('密码错误！');</script>");
                    }
                }
            }
        }
        
    }

}