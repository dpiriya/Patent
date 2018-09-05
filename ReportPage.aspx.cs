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
using CrystalDecisions;
using CrystalDecisions.CrystalReports.Engine;
using System.Threading;

public partial class ReportPage : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Convert.ToString(Session["CrystalStatus"]) != "Load")
            {
                ThreadStart callCrystal = new ThreadStart(CallCrystalFrameWork);
                Thread CrystalFrame = new Thread(callCrystal);
                CrystalFrame.Priority = ThreadPriority.Normal;
                CrystalFrame.Start();
                Session["CrystalStatus"] = "Load";                
            }
        }        
    }
    protected void CallCrystalFrameWork()
    {
        ReportDocument rptDoc = new ReportDocument();
        rptDoc.Load(Server.MapPath("~/Report/rptBlank.rpt"));
        CrystalReportViewer1.ReportSource = rptDoc;
    }

}
