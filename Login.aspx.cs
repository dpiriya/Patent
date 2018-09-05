using System;
using System.Collections.Generic;
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
using System.Web.Services;
using System.Web.Script.Services;
public partial class Account_Login : System.Web.UI.Page
{
    [System.Web.Services.WebMethod]
    [System.Web.Script.Services.ScriptMethod]

    public static AjaxControlToolkit.Slide[] GetSlides()
    {
        AjaxControlToolkit.Slide[] slides = new AjaxControlToolkit.Slide[3];
        slides[0] = new AjaxControlToolkit.Slide("Slide/Images1.jpg", "First image of my album", "");
        slides[1] = new AjaxControlToolkit.Slide("Slide/Images2.jpg", "Second image of my album", "");
        slides[2] = new AjaxControlToolkit.Slide("Slide/Images3.jpg", "Third image of my album", "");
        return (slides);
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        //RegisterHyperLink.NavigateUrl = "Register.aspx?ReturnUrl=" + HttpUtility.UrlEncode(Request.QueryString["ReturnUrl"]);
        LoginStatus myControl = ((LoginStatus)this.Master.FindControl("logStatus"));
        myControl.Visible = false;
        Image imgSmile = (Image)this.Master.FindControl("imgSmile");
        imgSmile.Visible = false;
        Control divMenu = this.Master.FindControl("main");
        divMenu.Visible = false;
        //string userID = Membership.GetUser().UserName;
        if (HttpContext.Current.User.Identity.IsAuthenticated)
        {
            Response.Redirect("Home.aspx");                     
        }
    }

    private void InitializeComponent()
    {
        
    }
          
}


