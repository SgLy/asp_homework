using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class manage : System.Web.UI.Page {
    protected void Page_PreInit(Object sender, EventArgs e) {
        if (Session["menuStyle"] == null)
            Session["menuStyle"] = "top";
        this.MasterPageFile = "~/" + (string)Session["menuStyle"] + "-menu.master";
    }
    protected void Page_Load(object sender, EventArgs e) {

    }
}