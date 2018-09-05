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
using System.Data.SqlClient;
using System.IO;
using iTextSharp.text;
using iTextSharp.text.pdf;

public partial class IdfPrint : System.Web.UI.Page
{
    string IdfNo;
    SqlConnection con = new SqlConnection();
    protected void Page_Load(object sender, EventArgs e)
    {
        IdfNo=Request.QueryString["FileNo"];
    }
    protected void btnClose_Click(object sender, EventArgs e)
    {
        Response.Write("<script language='javascript'>window.close();</script>");
    }
    protected void btnPrint_Click(object sender, EventArgs e)
    {
        if (IdfNo != "" || IdfNo != null)
        {
           
            con.ConnectionString = ConfigurationManager.ConnectionStrings["PATENTCN"].ConnectionString;
            System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-GB");

            HttpContext.Current.Response.AppendHeader("Content-Type", "application/pdf");
            HttpContext.Current.Response.AppendHeader("Content-disposition", "attachment; filename="+IdfNo+"_overview.pdf");
            using (Document doc = new Document(PageSize.A4))
            {
                PdfWriter.GetInstance(doc, Response.OutputStream);
                doc.Open();
                string tTitle = "IDF Overview - " + IdfNo;
                doc.Add(new Paragraph(new Phrase(tTitle, new Font(Font.FontFamily.TIMES_ROMAN, 14, 1))));
                GeneratePdf gp = new GeneratePdf();

                if(chkboxIdf.Checked==true)
                {
                    string sql = "select title,type,InitialFiling,firstApplicant,secondApplicant," +
                    "convert(varchar(10),request_dt,103) as request_dt from patdetails where fileno like '" + IdfNo + "'";
                    SqlDataAdapter sda = new SqlDataAdapter(sql, con);
                    DataTable dt = new DataTable();
                    sda.Fill(dt);
                    string[] strTitle = new[] { "Title", "IDF Type", "Initial Filing", "First Applicant", "Second Applicant","Request Date" };
                    PdfPTable table =gp.MakePdf(dt, 2, "Invention Disclosure Form Details", strTitle);
                    doc.Add(table);

                    string sql1 = "select SlNo+1 as SlNo,InventorName,InventorType,InventorID,DeptOrOrganisation as Dept from coinventordetails where fileno like '" + IdfNo + "'" +
                    "union select 1 as SlNo,Inventor1 as InventorName,InventorType,InstID as InventorID,Department as Dept from patdetails where fileno like '" + IdfNo + "'";
                    SqlDataAdapter sdb = new SqlDataAdapter(sql1, con);
                    DataTable dt1 = new DataTable();
                    sdb.Fill(dt1);
                    string[] strTitle1 = new[] { "Sl.No.", "Inventor Name", "Inventor Type", "Inventor ID", "Dept/Organization",};
                    PdfPTable table1 = gp.MakeTablePdf(dt1, 5, "Inventor Details", strTitle1);
                    doc.Add(table1);
            
                }
                if (chkboxIndian.Checked==true)
                {

                    string sql2 = "select Attorney,Applcn_no,convert(varchar(10),Filing_dt,103) as Filing_dt,Examination,convert(varchar(10),Exam_dt,103) as Exam_dt,Publication,convert(varchar(10),Pub_dt,103) as Pub_dt,Status,Sub_status," +
                    "Pat_no,convert(varchar(10),Pat_dt,103) as Pat_dt from patdetails where fileno like '" + IdfNo + "'";
                    SqlDataAdapter sda1 = new SqlDataAdapter(sql2, con);
                    DataTable dt1 = new DataTable();
                    sda1.Fill(dt1);
                    if (dt1.Rows.Count > 0)
                    {
                        string[] strTitle1 = new[] { "Attorney", "Application Number", "Filing Date", "Examination", "Examination Date", "Publication", "Publication Date", "Patent Status", "Patent Sub Status", "Patent Grant Number", "Patent Issue Date" };
                        PdfPTable table1 = gp.MakePdf(dt1, 2, "Indian Patent Details", strTitle1);
                        doc.Add(table1);
                    }
                }
                if (chkboxComm.Checked==true)
                {

                    string sql3 = "select Commercial,InventionNo,convert(varchar(10),Validity_from_dt,103) as Validity_from_dt,convert(varchar(10),Validity_to_dt,103) as Validity_to_dt,Industry1,Industry2,IPC_Code," +
                    "Abstract,DevelopmentStatus,Commercialized,PatentLicense,TechTransNo,Remarks from patdetails where fileno like '" + IdfNo + "'";
                    SqlDataAdapter sda3 = new SqlDataAdapter(sql3, con);
                    DataTable dt3 = new DataTable();
                    sda3.Fill(dt3);
                    if (dt3.Rows.Count > 0)
                    {
                        string[] strTitle3 = new[] { "Commercialization Responsibility", "Partner Reference No.", "Validity From Date", "Validity Till Date", "Industry", "Usage Area", "International Patent Classification(IPC) Code", "Abstract (Value Proposition)", "Development Status", "Commercialization Status", "Partner Agreement", "Technology Transfer No./Marketing No.", "Remark" };
                        PdfPTable table3 = gp.MakePdf(dt3, 2, "Commercialization Details", strTitle3);
                        doc.Add(table3);
                    }
                }
                if (chkboxInter.Checked==true)
                {

                    string sql4 = "select subFileNo,convert(varchar(10),RequestDt,103) as RequestDt,Country,Partner,PartnerNo,Type,Attorney,ApplicationNo,convert(varchar(10),FilingDt,103)as FilingDt," +
                    "PublicationNo,convert(varchar(10),PublicationDt,103) as PublicationDt,Status,SubStatus,PatentNo,convert(varchar(10),PatentDt,103) as PatentDt,Remark from international where fileno like '" + IdfNo + "' order by subfileno";
                    SqlDataAdapter sda4 = new SqlDataAdapter(sql4, con);
                    DataTable dt4 = new DataTable();
                    sda4.Fill(dt4);
                    if (dt4.Rows.Count > 0)
                    {
                        string[] strTitle4 = new[] { "Sub File Number", "Request Date", "Country", "Partner", "Partner Number", "Invention Type", "Attorney", "Application Number", "Filing Date", "Publication Number", "Publication Date", "Patent Status", "Patent Sub Status", "Patent Grant Number", "Patent Grant Date", "Remark" };
                        PdfPTable table4 = gp.MakePdf(dt4, 2, "International Patent Details", strTitle4);

                        doc.Add(table4);
                    }
                }
                doc.Close();
                con.Close();
            }
            HttpContext.Current.Response.End();
        }
    }
    
}
