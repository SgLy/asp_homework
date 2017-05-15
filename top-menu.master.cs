using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class page : System.Web.UI.MasterPage
{
    protected bool is_admin = true;
    protected bool logined = false;
    protected string username = "SgLy";
    protected string userlink = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["currentUser"] == null) {
            logined = false;
        } else {
            logined = true;
            username = Session["currentUser"].ToString();
            is_admin = username == "admin";
            userlink = "userinfo.aspx?userID=" + Session["userID"];
        }
    }
}
