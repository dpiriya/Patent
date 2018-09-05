using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;

public partial class Account_ChangePassword : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Membership.GetUser().UserName.ToString() == "administrator")
        {
            anchorReset.Visible = true;
        }
        else
        {
            anchorReset.Visible = false;
        }
    }
}
