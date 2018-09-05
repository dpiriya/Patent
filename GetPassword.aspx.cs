using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

public partial class GetPassword : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ddlUser.DataSource = Membership.GetAllUsers();
            ddlUser.DataBind();
            ddlUser.Items.Insert(0, "");
        }
    }
    protected void btnReset_Click(object sender, EventArgs e)
    {
        if (ddlUser.SelectedIndex > 0 && txtPassword.Text.Trim()!="")
        {
            try
            {
                MembershipUser user = Membership.GetUser(ddlUser.SelectedItem.Text.Trim(), false);
                user.ChangePassword(user.ResetPassword(), txtPassword.Text.Trim());
                string str = ddlUser.SelectedItem.Text.Trim() + " password has changed successfully";
                ClientScript.RegisterStartupScript(GetType(), "success", "<script>alert('" + str + "')</script>");
            }
            catch (Exception ex)
            {
                ClientScript.RegisterStartupScript(GetType(), "Error", "<script>alert('" + ex.Message.ToString() + "')</script>");                
            }
            btnClear_Click(sender, e);
        }
        else
        {
            ClientScript.RegisterStartupScript(GetType(), "Information", "<script>alert('Please Verify User Name and Password')</script>");                
        }
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("Home.aspx");
    }
    protected void btnClear_Click(object sender, EventArgs e)
    {
        ddlUser.SelectedIndex = 0;
        txtPassword.Text = "";
    }
    protected void btnUnlock_Click(object sender, EventArgs e)
    {
        if (ddlUser.SelectedIndex > 0)
        {
            try
            {
                MembershipUser user = Membership.GetUser(ddlUser.SelectedItem.Text.Trim(), false);
                user.UnlockUser();
                string str = ddlUser.SelectedItem.Text.Trim() + " Unlocked successfully";
                ClientScript.RegisterStartupScript(GetType(), "success", "<script>alert('" + str + "')</script>");
            }
            catch (Exception ex)
            {
                ClientScript.RegisterStartupScript(GetType(), "Error", "<script>alert('" + ex.Message.ToString() + "')</script>");                                
            }
            btnClear_Click(sender, e);
        }
        else
        {
            ClientScript.RegisterStartupScript(GetType(), "Information", "<script>alert('Please select User Name')</script>");                
        }
    }
}
