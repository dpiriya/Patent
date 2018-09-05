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
using System.Web.Services;
using System.Web.Script.Services;
using System.Threading;
using CrystalDecisions;
using CrystalDecisions.CrystalReports.Engine;

public partial class _Home : System.Web.UI.Page
{

    [System.Web.Services.WebMethod]
    [System.Web.Script.Services.ScriptMethod]

    public static AjaxControlToolkit.Slide[] GetSlides()
    {
        AjaxControlToolkit.Slide[] slides = new AjaxControlToolkit.Slide[1];
        slides[0] = new AjaxControlToolkit.Slide("Slide/Images3.jpg", "Third image of my album", "");
        return (slides);
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack && Convert.ToString(Session["CrystalStatus"]) != "Load") 
        {
            ThreadStart callCrystal = new ThreadStart(CallCrystalFrameWork);
            Thread CrystalFrame = new Thread(callCrystal);
            CrystalFrame.Start();
            Session["CrystalStatus"] = "Load";
        }
    }
    protected void CallCrystalFrameWork()
    {
        ReportDocument rptDoc = new ReportDocument();
        rptDoc.Load(Server.MapPath("~/Report/rptBlank.rpt"));        
    }
    
}
