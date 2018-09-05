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
using CrystalDecisions;
using CrystalDecisions.CrystalReports.Engine;

public partial class InventorReview : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection();
    ReportDocument rptDoc = new ReportDocument();
    protected void Page_Load(object sender, EventArgs e)
    {
        string id = Request.QueryString["id"];
        con.ConnectionString = ConfigurationManager.ConnectionStrings["PATENTCN"].ConnectionString;
        if (id == "Inventor")
        {
            this.Title = "Inventor Wise Report";
            string SelectName = Request.QueryString["name"];
            string sql = "SELECT FILENO,TITLE,Inventor1 AS INVENTOR,APPLCN_NO,FILING_DT,PAT_NO,PAT_DT,Attorney AS ATTORNEY,STATUS,SUB_STATUS FROM PATDETAILS " +
            "WHERE Inventor1 LIKE '" + SelectName + "'";
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = sql;
            cmd.CommandType = CommandType.Text;
            cmd.Connection = con;
            con.Open();
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = cmd;
            Inventor ds = new Inventor();
            da.Fill(ds, "InventorDetails");

            rptDoc.Load(Server.MapPath("~/Report/rptInventor.rpt"));
            rptDoc.SetDataSource(ds);
            //rptDoc.ExportToHttpResponse(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, Response,false, SelectName + "IdfDetails");
            CrystalReportViewer1.ReportSource = rptDoc;
            CrystalReportViewer1.HasToggleGroupTreeButton = false;
            con.Close();
        }
        else if (id == "Inventor2")
        {
            this.Title = "Inventor Wise Report";
            string SelectName = Request.QueryString["name"];
            string sql = "SELECT FILENO,TITLE,INVENTOR1 AS INVENTOR,INDUSTRY1,INDUSTRY2,ABSTRACT,DEVELOPMENTSTATUS,COMMERCIALIZED,(SELECT SUM(PAYMENTAMTINR) FROM PATENTPAYMENT WHERE FILENO = PATDETAILS.FILENO) AS PAYMENT,(SELECT SUM(COST_RS) FROM PATENTRECEIPT WHERE FILENO = PATDETAILS.FILENO) AS RECEIPT FROM PATDETAILS " +
            "WHERE INVENTOR1 LIKE '" + SelectName + "' ORDER BY CAST(FILENO AS INT) DESC";
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = sql;
            cmd.CommandType = CommandType.Text;
            cmd.Connection = con;
            con.Open();
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = cmd;
            Inventor ds = new Inventor();
            da.Fill(ds, "InventorCommercial");

            rptDoc.Load(Server.MapPath("~/Report/rptInventor2.rpt"));
            rptDoc.SetDataSource(ds);
            CrystalReportViewer1.ReportSource = rptDoc;
            CrystalReportViewer1.HasToggleGroupTreeButton = false;
            con.Close();
        }
        else if (id == "Attorney") 
        {
            this.Title = "Attorney Wise Report";
            string SelectName = Request.QueryString["name"];
            string FiledStatus = Request.QueryString["FiledStatus"];
            SelectName = SelectName.Replace("~", "&");
            string fs="";
            if (FiledStatus == "Granted") fs = "STATUS LIKE 'Granted'";
            else if (FiledStatus == "Not Yet Granted") fs="STATUS NOT LIKE 'Granted'";
            string sql = "SELECT FILENO,TITLE,Inventor1 AS INVENTOR,APPLCN_NO,FILING_DT,PAT_NO,PAT_DT,Attorney AS ATTORNEY,STATUS,SUB_STATUS FROM PATDETAILS " +
            "WHERE Attorney like'" + SelectName.Trim()+"'";
            if (fs != "") sql= sql + " AND " + fs;
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = sql;
            cmd.CommandType = CommandType.Text;
            cmd.Connection = con;
            con.Open();
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = cmd;
            Inventor ds = new Inventor();
            da.Fill(ds, "AttorneyDetails");

            rptDoc.Load(Server.MapPath("~/Report/rptAttorney.rpt"));
            rptDoc.SetDataSource(ds);
            CrystalReportViewer1.ReportSource = rptDoc;
            CrystalReportViewer1.HasToggleGroupTreeButton = false;
            //CrystalReportViewer1.SeparatePages = false;
            con.Close();
        }
        else if (id == "FilingDt")
        {
            this.Title = "Filing Date Wise Report";
            System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-GB");
            string fromDate = Request.QueryString["FromDt"];
            string toDate = Request.QueryString["ToDt"];
            string idfStatus = Request.QueryString["IDFStatus"];
            string sql = "SELECT FILENO,TITLE,Inventor1 AS INVENTOR,DEPARTMENT,APPLCN_NO,FILING_DT,Attorney AS ATTORNEY,STATUS,SUB_STATUS FROM PATDETAILS " +
            "WHERE FILING_DT BETWEEN CONVERT(SMALLDATETIME,'" + fromDate + "',103) AND CONVERT(SMALLDATETIME,'" + toDate + "',103)";
            if (idfStatus == "All")
            {
                sql = sql + " ORDER BY FILING_DT DESC";
            }
            else if (idfStatus == "Granted")
            {
                sql = sql + " AND PAT_NO IS NOT NULL ORDER BY FILING_DT DESC";
            }
            else if (idfStatus == "Not Yet Granted")
            {
                sql = sql + " AND PAT_NO IS NULL ORDER BY FILING_DT DESC";
            }
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = sql;
            cmd.CommandType = CommandType.Text;
            cmd.Connection = con;
            con.Open();
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = cmd;
            Inventor ds = new Inventor();
            da.Fill(ds, "FilingDtList");
            rptDoc.Load(Server.MapPath("~/Report/rptFilingDate.rpt"));
            rptDoc.SetDataSource(ds);
            rptDoc.SetParameterValue("FromDate", Convert.ToDateTime(fromDate));
            rptDoc.SetParameterValue("TODate", Convert.ToDateTime(toDate));
            CrystalReportViewer1.ReportSource = rptDoc;
            CrystalReportViewer1.HasToggleGroupTreeButton = false;
            con.Close();
        }
        else if (id == "Renewal")
        {
            this.Title = "Renewal Report";
            System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-GB");
            string iMonth = Request.QueryString["FromDt"];
            string iYear = Request.QueryString["ToDt"];
            string NotRenewal=Request.QueryString["NotRenewal"];
            string fromDate = Convert.ToDateTime("01/" + iMonth + "/" + iYear).ToShortDateString();
            string tmpTo=Convert.ToDateTime(fromDate).AddMonths(1).ToShortDateString();
            string toDate = Convert.ToDateTime(tmpTo).AddDays(-1).ToShortDateString();
            string sql1 = "";
            if (NotRenewal == "No")
            {
                sql1 = " and SUB_STATUS NOT LIKE '%Not Renewed%' ";
            }
            string sql = "SELECT FILENO,TITLE,Inventor1 AS INVENTOR,APPLCN_NO AS APPLICATIONNO,FILING_DT,CASE WHEN DATEDIFF(YEAR,FILING_DT,GETDATE()) > 20 THEN '>20' ELSE CAST(DATEDIFF(YEAR,FILING_DT,GETDATE()) AS VARCHAR(5)) END AS NO_YEAR,VALIDITY_TO_DT,DATEDIFF(YEAR,FILING_DT,VALIDITY_TO_DT) AS RENEWAL_YEAR,SUB_STATUS,COMMERCIALIZED,pat_dt as Patent_Date,Pat_no FROM PATDETAILS " +
            "WHERE DATEPART(MONTH,FILING_DT)=" + Convert.ToInt32(iMonth) +" AND STATUS LIKE 'GRANTED'"+ sql1 + "ORDER BY SUB_STATUS";
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = sql;
            cmd.CommandType = CommandType.Text;
            cmd.Connection = con;
            con.Open();
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = cmd;
            Inventor ds = new Inventor();
            da.Fill(ds, "RenewalList");

            String tMonth = Convert.ToDateTime("01/" + iMonth + "/" + iYear).ToString("MMMM");
            rptDoc.Load(Server.MapPath("~/Report/rptRenewal.rpt"));
            rptDoc.SetDataSource(ds);
            rptDoc.SetParameterValue("Imonth", tMonth);
            rptDoc.SetParameterValue("Iyear", iYear);
            CrystalReportViewer1.ReportSource = rptDoc;
            CrystalReportViewer1.HasToggleGroupTreeButton = false;
            con.Close();
        }
        else if (id == "ProvisionalToComplete")
        {
            this.Title = "Provisional To Complete";
            int selMonth = Convert.ToInt32(Request.QueryString["selMonth"]);
            string tillDate = Convert.ToString(Request.QueryString["tillDate"]);
            string sql = "select fileno,title,Inventor1 as inventor,department,applcn_no,filing_dt,Attorney,status,sub_status, datediff(mm,filing_dt,convert(smalldatetime,'" + tillDate + "',103)) as filingMonths  from patdetails where filing_dt is not null and " +
            "sub_status like '%provisional%' and datediff(mm,filing_dt,convert(smalldatetime,'" + tillDate + "',103)) >= " + selMonth;
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = sql;
            cmd.CommandType = CommandType.Text;
            cmd.Connection = con;
            con.Open();
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = cmd;
            dsFindFiles ds = new dsFindFiles();
            da.Fill(ds, "provisional");
            GenerateExcel ge = new GenerateExcel();
            ge.MakeExcel(ds.Tables["provisional"], "Provisional");
            con.Close();            
        }
        else if (id == "Examination")
        {
            this.Title = "Examination and Publication";
            int selMonth = Convert.ToInt32(Request.QueryString["selMonth"]);
            string sql = "select fileno,title,Inventor1 as inventor,department,applcn_no,filing_dt,Attorney,status,sub_status, datediff(mm,filing_dt,getdate()) as filingMonths  from patdetails where filing_dt is not null and status not like 'Granted' and status not like 'Closed%' " +
            "and sub_status like '%Complete%' and sub_status not like '%Published%' and datediff(mm,filing_dt,getdate()) >= " + selMonth;
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = sql;
            cmd.CommandType = CommandType.Text;
            cmd.Connection = con;
            con.Open();
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = cmd;
            dsFindFiles ds = new dsFindFiles();
            da.Fill(ds, "Examination");
            GenerateExcel ge = new GenerateExcel();
            ge.MakeExcel(ds.Tables["Examination"],"Exam");
            con.Close();
        }
        else if (id == "PCT_Filing")
        {
            this.Title = "PCT Filing";
            int selMonth = Convert.ToInt32(Request.QueryString["selMonth"]);
            string tillDate = Convert.ToString(Request.QueryString["tillDate"]);
            string sql = "select fileno,title,Inventor1 as inventor,department,applcn_no,filing_dt,status,sub_status, datediff(mm,filing_dt,convert(smalldatetime,'" + tillDate + "',103)) as filingMonths  from patdetails where filing_dt is not null and status not like 'Granted' and status not like 'Closed%' " +
            "and (sub_status like '%Complete%' or sub_status like '%Provisional%')and fileno not in (select fileno from INTERNATIONAL where country like 'pct') and datediff(mm,filing_dt,convert(smalldatetime,'" + tillDate + "',103)) <=12 and datediff(mm,filing_dt,convert(smalldatetime,'" + tillDate + "',103)) >= " + selMonth + " order by sub_status, cast(fileno as int)";
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = sql;
            cmd.CommandType = CommandType.Text;
            cmd.Connection = con;
            con.Open();
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = cmd;
            dsFindFiles ds = new dsFindFiles();
            da.Fill(ds, "pctFiling");
            GenerateExcel ge = new GenerateExcel();
            ge.MakeExcel(ds.Tables["pctFiling"],"PCT");
            con.Close();
        }
        else if (id == "Foreign")
        {
            this.Title = "Foreign Filing Review (Excluding Direct International)";
            string Country=Request.QueryString["Country"];
            string Status=Request.QueryString["Status"];
            string Commercial =Request.QueryString["Commercial"];
            int FromMonth=Convert.ToInt32(Request.QueryString["FromMonth"]);
            int ToMonth = Convert.ToInt32(Request.QueryString["ToMonth"]);
            //string sql = "select fileno,title,Inventor1 as inventor,department,applcn_no,filing_dt,status,sub_status, datediff(mm,filing_dt,getdate()) as filingMonths  from patdetails where filing_dt is not null and status not like 'Granted' and status not like 'Closed%' " +
            //"and sub_status like '%Complete%' and fileno in (select fileno from INTERNATIONAL where country like 'pct') and datediff(mm,filing_dt,getdate()) >= " + selMonth;
            string sql = "select P.fileno,P.title,P.Inventor1 as inventor,P.department,p.Applcn_no as ApplicationNumber,p.Filing_dt as FilingDate,I.subFileNo,i.Country,I.ApplicationNo,I.FilingDt,I.Status,I.SubStatus, datediff(mm,p.Filing_dt,getdate()) as filingMonths  from patdetails P inner join International I on p.FileNo = I.FileNo";
            string query="";
            if (Country != "All") query = " and I.Country = '" + Country + "'";
            if (Status != "All") if (query != "") query = query + " and I.Status = '" + Status + "'"; else query = " and I.Status ='" + Status + "'";
            if (Commercial != "All") if (query != "") query = query + " and I.Commercial ='" + Commercial + "'"; else query = " and I.Commercial ='" + Commercial + "'";
            if (FromMonth <= ToMonth) if (query != "") query = query + " and datediff(mm,p.Filing_dt,getdate()) >=" + FromMonth + " and datediff(mm,p.Filing_dt,getdate()) <=" + ToMonth; else query = " and datediff(mm,p.Filing_dt,getdate()) >=" + FromMonth + " and datediff(mm,p.Filing_dt,getdate()) <=" + ToMonth;
            if (query != "") sql = sql + " " + query;
            sql = sql + " order by p.fileno,I.FilingDt";
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = sql;
            cmd.CommandType = CommandType.Text;
            cmd.Connection = con;
            con.Open();
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = cmd;
            dsFindFiles ds = new dsFindFiles();
            da.Fill(ds, "foreignFiling");
            GenerateExcel ge = new GenerateExcel();
            ge.MakeExcel(ds.Tables["foreignFiling"],"FDF");
            con.Close();
        }
        else if (id == "FileNoWise")
        {
            this.Title = "File Number Wise Report";
            System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-GB");
            int fromNo = Convert.ToInt32(Request.QueryString["FromNo"]);
            int toNo = Convert.ToInt32(Request.QueryString["ToNo"]);
            string commercial = Request.QueryString["commercial"];
            string tmpCommercial="";
            if (commercial == "IV" || commercial == "IITM")
            {
                tmpCommercial=" and commercial like '"+ commercial+"'";
            }
            else if (commercial == "Others")
            {
                tmpCommercial = " and commercial not like 'IV' and commercial not like 'IITM'"; ;
            }
            string sql = "SELECT FILENO,TITLE,COMMERCIAL,Inventor1 AS INVENTOR,DEPARTMENT,APPLCN_NO,FILING_DT,Attorney AS ATTORNEY,STATUS,SUB_STATUS FROM PATDETAILS " +
            "WHERE fileno >=" + fromNo.ToString() + " AND fileno <=" + toNo.ToString() +tmpCommercial + " ORDER BY CAST(FILENO AS INT) DESC";
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = sql;
            cmd.CommandType = CommandType.Text;
            cmd.Connection = con;
            con.Open();
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = cmd;
            Inventor ds = new Inventor();
            da.Fill(ds, "FileNoList");
            rptDoc.Load(Server.MapPath("~/Report/rptFileNo.rpt"));
            rptDoc.SetDataSource(ds);
            rptDoc.SetParameterValue("FromNo", fromNo);
            rptDoc.SetParameterValue("ToNo", toNo);
            CrystalReportViewer1.ReportSource = rptDoc;
            CrystalReportViewer1.HasToggleGroupTreeButton = false;
            con.Close();
        }
        else if (id == "EntryDt")
        {
            this.Title = "Entry Date Wise Report";
            System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-GB");
            string fromDate = Request.QueryString["FromDt"];
            string toDate = Request.QueryString["ToDt"];
            string idfStatus = Request.QueryString["IDFStatus"];
            string sql = "SELECT EntryDt,FILENO,TITLE,Inventor1 AS INVENTOR,DEPARTMENT,TYPE,APPLCN_NO,FILING_DT,Attorney AS ATTORNEY,STATUS,SUB_STATUS,Pat_no FROM PATDETAILS " +
            "WHERE EntryDt BETWEEN CONVERT(SMALLDATETIME,'" + fromDate + "',103) AND CONVERT(SMALLDATETIME,'" + toDate + "',103)";
            if (idfStatus == "All")
            {
                //sql = sql + " ORDER BY ENTRYDT DESC";
                sql = sql + " ORDER BY FILENO ASC";
            }
            else if (idfStatus == "Filed")
            {
                sql = sql + " AND  APPLCN_NO IS NOT NULL ORDER BY ENTRYDT DESC";
            }
            else if (idfStatus == "Not Yet Filed")
            {
                sql = sql + " AND  APPLCN_NO IS NULL AND STATUS NOT IN ('Closed - Internal','Closed - External','Direct International') ORDER BY ENTRYDT DESC";
            }
            else if (idfStatus == "Closed - Internal")
            {
                sql = sql + " AND STATUS LIKE 'Closed - Internal' ORDER BY ENTRYDT DESC";
            }
            else if (idfStatus == "Closed - External")
            {
                sql = sql + " AND STATUS LIKE 'Closed - External' ORDER BY ENTRYDT DESC";
            }
            else if (idfStatus == "Granted")
            {
                sql = sql + " AND PAT_NO IS NOT NULL ORDER BY ENTRYDT DESC";
            }
            else if (idfStatus == "Not Yet Granted")
            {
                sql = sql + " AND PAT_NO IS NULL ORDER BY ENTRYDT DESC";
            }
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = sql;
            cmd.CommandType = CommandType.Text;
            cmd.Connection = con;
            con.Open();
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = cmd;
            Inventor ds = new Inventor();
            da.Fill(ds, "EntryDtList");
            rptDoc.Load(Server.MapPath("~/Report/rptEntryDate.rpt"));
            rptDoc.SetDataSource(ds);
            rptDoc.SetParameterValue("FromDate", Convert.ToDateTime(fromDate));
            rptDoc.SetParameterValue("TODate", Convert.ToDateTime(toDate));
            CrystalReportViewer1.ReportSource = rptDoc;
            CrystalReportViewer1.HasToggleGroupTreeButton = false;
            con.Close();
        }
        else if (id == "PatDt")
        {
            this.Title = "Patent Date Wise Report";
            System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-GB");
            string fromDate = Request.QueryString["FromDt"];
            string toDate = Request.QueryString["ToDt"];
            string sql = "SELECT FILENO,TITLE,Inventor1 AS INVENTOR,DEPARTMENT,APPLCN_NO,FILING_DT,Attorney AS ATTORNEY,TYPE,PAT_NO,PAT_DT,STATUS FROM PATDETAILS " +
            "WHERE PAT_DT BETWEEN CONVERT(SMALLDATETIME,'" + fromDate + "',103) AND CONVERT(SMALLDATETIME,'" + toDate + "',103) ORDER BY  PAT_DT DESC";
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = sql;
            cmd.CommandType = CommandType.Text;
            cmd.Connection = con;
            con.Open();
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = cmd;
            Inventor ds = new Inventor();
            da.Fill(ds, "PatentDtList");
            rptDoc.Load(Server.MapPath("~/Report/rptPatentDate.rpt"));
            rptDoc.SetDataSource(ds);
            rptDoc.SetParameterValue("FromDate", Convert.ToDateTime(fromDate));
            rptDoc.SetParameterValue("TODate", Convert.ToDateTime(toDate));
            CrystalReportViewer1.ReportSource = rptDoc;
            CrystalReportViewer1.HasToggleGroupTreeButton = false;
            con.Close();
        }
        else if (id == "IntlInventor")
        {
            this.Title = "Inventor Wise Report";
            string SelectName = Request.QueryString["name"];
            string sql = "SELECT P.FILENO,P.TITLE,P.APPLCN_NO AS APPLICATION_NO,P.FILING_DT,P.PAT_NO AS PATENT_NO,P.PAT_DT AS PATENT_DT,P.TYPE,P.ATTORNEY,P.STATUS,P.SUB_STATUS,I.SUBFILENO,I.COUNTRY," +
            "I.APPLICATIONNO,I.FILINGDT,I.PATENTNO,I.PATENTDT,I.ATTORNEY AS INTLATTORNEY,I.STATUS AS INTLSTATUS,I.SUBSTATUS FROM PATDETAILS P INNER JOIN INTERNATIONAL I " +
            "ON P.FILENO=  I.FILENO AND INVENTOR1 LIKE '" + SelectName + "'";
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = sql;
            cmd.CommandType = CommandType.Text;
            cmd.Connection = con;
            con.Open();
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = cmd;
            International ds = new International();
            da.Fill(ds, "InventorList");
            rptDoc.Load(Server.MapPath("~/Report/rptIntlInventor.rpt"));
            rptDoc.SetDataSource(ds);
            rptDoc.SetParameterValue("txtProfessor", SelectName);
            CrystalReportViewer1.ReportSource = rptDoc;
            CrystalReportViewer1.HasToggleGroupTreeButton = false;
            con.Close();
        }
        else if (id == "IntlAttorney")
        {
            this.Title = "Attorney Wise Report";
            string SelectName = Request.QueryString["name"];
            string FiledStatus = Request.QueryString["FiledStatus"];
            SelectName = SelectName.Replace("~", "&");
            string fs = "";
            if (FiledStatus == "Granted") fs = "I.STATUS LIKE 'Granted'";
            else if (FiledStatus == "Not Yet Granted") fs = "I.STATUS NOT LIKE 'Granted'";
            string sql = "select I.fileno,I.Subfileno,P.Title,P.Inventor1 as Inventor,I.Country,I.ApplicationNo,I.FilingDt,I.patentNo,I.PatentDt,I.Status,I.SubStatus,I.Attorney from International I inner join Patdetails P " +
            "on I.fileno=P.fileno and I.Attorney like '" + SelectName.Trim() + "'";
            if (fs != "") sql = sql + " AND " + fs +" order by I.Country";
            else sql = sql + " order by I.Country";
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = sql;
            cmd.CommandType = CommandType.Text;
            cmd.Connection = con;
            con.Open();
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = cmd;
            International ds = new International();
            da.Fill(ds, "IntlAttorney");

            rptDoc.Load(Server.MapPath("~/Report/rptIntlAttorney.rpt"));
            rptDoc.SetDataSource(ds);
            rptDoc.SetParameterValue("lblTitle", "Name of Attorney : " + SelectName);
            CrystalReportViewer1.ReportSource = rptDoc;
            CrystalReportViewer1.HasToggleGroupTreeButton = false;
            con.Close();
        }
        else if (id == "IntlCountry")
        {
            this.Title = "Country Wise Report";
            string SelectName = Request.QueryString["name"];
            SelectName = SelectName.Replace("~", "&");
            string sql = "select I.fileno,I.Subfileno,P.Title,P.Inventor1 as Inventor,I.Country,I.ApplicationNo,I.FilingDt,I.patentNo,I.PatentDt,I.Status,I.SubStatus,I.Attorney from International I inner join Patdetails P " +
            "on I.fileno=P.fileno and I.Country like '" + SelectName.Trim() + "' order by P.Inventor1";
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = sql;
            cmd.CommandType = CommandType.Text;
            cmd.Connection = con;
            con.Open();
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = cmd;
            International ds = new International();
            da.Fill(ds, "IntlAttorney");

            rptDoc.Load(Server.MapPath("~/Report/rptIntlAttorney.rpt"));
            rptDoc.SetDataSource(ds);
            rptDoc.SetParameterValue("lblTitle", "Country Name : " + SelectName);
            CrystalReportViewer1.ReportSource = rptDoc;
            CrystalReportViewer1.HasToggleGroupTreeButton = false;
            con.Close();
        }
        else if (id == "IntlFilingDt")
        {
            this.Title = "Filing Date Wise Report";
            System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-GB");
            string fromDate = Request.QueryString["FromDt"];
            string toDate = Request.QueryString["ToDt"];
            string sql = "select I.fileno,I.Subfileno,P.Title,P.Inventor1 as Inventor,I.Country,I.ApplicationNo,I.FilingDt,I.patentNo,I.PatentDt,I.Status,I.SubStatus,I.Attorney from International I inner join Patdetails P " +
            "on I.fileno=P.fileno and I.FilingDt between CONVERT(SMALLDATETIME,'" + fromDate + "',103) AND CONVERT(SMALLDATETIME,'" + toDate + "',103) order by I.Filingdt desc";
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = sql;
            cmd.CommandType = CommandType.Text;
            cmd.Connection = con;
            con.Open();
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = cmd;
            International ds = new International();
            da.Fill(ds, "IntlAttorney");

            rptDoc.Load(Server.MapPath("~/Report/rptIntlAttorney.rpt"));
            rptDoc.SetDataSource(ds);
            rptDoc.SetParameterValue("lblTitle", "Filing date between " + fromDate + " and " + toDate);
            CrystalReportViewer1.ReportSource = rptDoc;
            CrystalReportViewer1.HasToggleGroupTreeButton = false;
            con.Close();
        }

        else if (id == "IntlPatDt")
        {
            this.Title = "Patent Date Wise Report";
            System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-GB");
            string fromDate = Request.QueryString["FromDt"];
            string toDate = Request.QueryString["ToDt"];
            string sql = "select I.fileno,I.Subfileno,P.Title,P.Inventor1 as Inventor,I.Country,I.ApplicationNo,I.FilingDt,I.patentNo,I.PatentDt,I.Status,I.SubStatus,I.Attorney from International I inner join Patdetails P " +
            "on I.fileno=P.fileno and I.PatentDt between CONVERT(SMALLDATETIME,'" + fromDate + "',103) AND CONVERT(SMALLDATETIME,'" + toDate + "',103) order by I.Patentdt desc";
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = sql;
            cmd.CommandType = CommandType.Text;
            cmd.Connection = con;
            con.Open();
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = cmd;
            International ds = new International();
            da.Fill(ds, "IntlAttorney");

            rptDoc.Load(Server.MapPath("~/Report/rptIntlAttorney.rpt"));
            rptDoc.SetDataSource(ds);
            rptDoc.SetParameterValue("lblTitle", "Patent date between " + fromDate + " and " + toDate);
            CrystalReportViewer1.ReportSource = rptDoc;
            CrystalReportViewer1.HasToggleGroupTreeButton = false;
            con.Close();
        }
        else if (id == "IntlEntryDt")
        {
            this.Title = "Patent Date Wise Report";
            System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-GB");
            string fromDate = Request.QueryString["FromDt"];
            string toDate = Request.QueryString["ToDt"];
            string sql = "select I.fileno,I.Subfileno,P.Title,P.Inventor1 as Inventor,I.Country,I.ApplicationNo,I.FilingDt,I.patentNo,I.PatentDt,I.Status,I.SubStatus,I.Attorney from International I inner join Patdetails P " +
            "on I.fileno=P.fileno and I.InputDt between CONVERT(SMALLDATETIME,'" + fromDate + "',103) AND CONVERT(SMALLDATETIME,'" + toDate + "',103) order by I.Inputdt desc";
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = sql;
            cmd.CommandType = CommandType.Text;
            cmd.Connection = con;
            con.Open();
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = cmd;
            International ds = new International();
            da.Fill(ds, "IntlAttorney");

            rptDoc.Load(Server.MapPath("~/Report/rptIntlAttorney.rpt"));
            rptDoc.SetDataSource(ds);
            rptDoc.SetParameterValue("lblTitle", "Entry date between " + fromDate + " and " + toDate);
            CrystalReportViewer1.ReportSource = rptDoc;
            CrystalReportViewer1.HasToggleGroupTreeButton = false;
            con.Close();
        }
        else if (id == "PayInventor")
        {
            this.Title = "Payment details of Inventor";
            string SelectName = Request.QueryString["name"];
            string sql = "select Fileno, (select sum(PaymentAmtINR) from patentPayment where fileno=P.fileno and paymentreforchequeno is not null and (Country='India' or Country is null)) as Domestic," +
            "(select sum(PaymentAmtINR) from patentPayment where fileno=P.fileno and paymentreforchequeno is not null and (Country <>'India' and Country is not null)) as International, " +
            "sum(PaymentAmtINR) as PaymentAmtINR from patentPayment P where P.Fileno in (select Fileno from patdetails where Inventor1 like '" + SelectName + "') " +
            "and P.paymentreforchequeno is not null group by P.Fileno order by cast(P.Fileno as integer)";
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = sql;
            cmd.CommandType = CommandType.Text;
            cmd.Connection = con;
            con.Open();
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = cmd;
            PaymentDS ds = new PaymentDS();
            da.Fill(ds, "InventorPayment");

            rptDoc.Load(Server.MapPath("~/Report/InventorCost.rpt"));
            rptDoc.SetDataSource(ds);
            rptDoc.SetParameterValue("Title", "Prof. " +  SelectName.ToUpper());
            CrystalReportViewer1.ReportSource = rptDoc;
            CrystalReportViewer1.HasToggleGroupTreeButton = false;
            con.Close();
        }
        else if (id == "PayDtWise")
        {
            this.Title = "Payment Date Wise Report";
            System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-GB");
            string fromDate = Request.QueryString["FromDt"];
            string toDate = Request.QueryString["ToDt"];
            string sql = "select Fileno, (select upper(inventor1) from patdetails where fileno=p.fileno)as Inventor,(select sum(PaymentAmtINR) from patentPayment where fileno=P.fileno and paymentOrChequeDt between CONVERT(SMALLDATETIME,'" + fromDate + "',103) AND CONVERT(SMALLDATETIME,'" + toDate + "',103) and (Country='India' or Country is null)) as Domestic, " +
            "(select sum(PaymentAmtINR) from patentPayment where fileno=P.fileno and paymentOrChequeDt between CONVERT(SMALLDATETIME,'" + fromDate + "',103) AND CONVERT(SMALLDATETIME,'" + toDate + "',103) and (Country <>'India' and Country is not null)) as International, " +
            "sum(PaymentAmtINR) as PaymentAmtINR from patentPayment P where P.Fileno in (select Fileno from patdetails where paymentOrChequeDt between CONVERT(SMALLDATETIME,'" + fromDate + "',103) AND CONVERT(SMALLDATETIME,'" + toDate + "',103)) " +
            "group by P.Fileno order by sum(PaymentAmtINR) desc";
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = sql;
            cmd.CommandType = CommandType.Text;
            cmd.Connection = con;
            con.Open();
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = cmd;
            PaymentDS ds = new PaymentDS();
            da.Fill(ds, "InventorPayment");

            rptDoc.Load(Server.MapPath("~/Report/rptCost.rpt"));
            rptDoc.SetDataSource(ds);
            rptDoc.SetParameterValue("Title", "Payment Date between " + fromDate +" and " + toDate);
            CrystalReportViewer1.ReportSource = rptDoc;
            CrystalReportViewer1.HasToggleGroupTreeButton = false;
            con.Close();
        }
        else if (id == "AgeAnalysis" || id == "AgeAnalysisGraph")
        {
            this.Title = "Age Analysis Report";
            System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-GB");
            int fromYear = Convert.ToInt32(Request.QueryString["FromYear"]);
            int toYear = Convert.ToInt32(Request.QueryString["ToYear"]);
            string sql = "select cast(datepart(yyyy,p.filing_dt) as varchar(4)) as [Year], count(*) as [PatentFiled]," +
            "(select count(*) from patdetails where pat_no is not null and datepart(yyyy,filing_dt)=datepart(yyyy,p.filing_dt)) as [PatentGrant]," +
            "(select count(*) from patdetails where pub_dt is not null and pat_no is null and datepart(yyyy,filing_dt)=datepart(yyyy,p.filing_dt)) as [Publication],"+
            "(select count(*) from patdetails where exam_dt is not null and pat_no is null and datepart(yyyy,filing_dt)=datepart(yyyy,p.filing_dt)) as [Examination]"+
            "from patdetails p where p.filing_dt is not null and datepart(yyyy,p.filing_dt)>= " + fromYear + " and datepart(yyyy,p.filing_dt)<= " + toYear + " group by datepart(yyyy,p.filing_dt)";
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = sql;
            cmd.CommandType = CommandType.Text;
            cmd.Connection = con;
            con.Open();
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = cmd;
            dsAgeAnalysis ds= new dsAgeAnalysis();
            da.Fill(ds, "dtAgeAnalysis");
            if (id == "AgeAnalysis")
                rptDoc.Load(Server.MapPath("~/Report/rptAgeAnalysis.rpt"));
            else if (id == "AgeAnalysisGraph")
                rptDoc.Load(Server.MapPath("~/Report/rptAgeAnalysisGraph.rpt"));
            rptDoc.SetDataSource(ds);
            //rptDoc.SetParameterValue("FromNo", fromNo);
            //rptDoc.SetParameterValue("ToNo", toNo);
            rptDoc.SetParameterValue("Title", "Years from " + fromYear + " to " + toYear);
            CrystalReportViewer1.ReportSource = rptDoc;
            CrystalReportViewer1.HasToggleGroupTreeButton = false;
            con.Close();
        }
        else if (id == "Commercial")
        {
            this.Title = "Commercialization Status";
            System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-GB");
            string fromDate = Request.QueryString["FromDt"];
            string toDate = Request.QueryString["ToDt"];
            string Status = Request.QueryString["Status"];
            string sql = "SELECT FILENO,TITLE,INVENTOR1,INDUSTRY1,INDUSTRY2,ABSTRACT,DEVELOPMENTSTATUS,COMMERCIALIZED,STUFF((SELECT ', ' + MKTGPROJECTNO FROM MARKETINGIDF WHERE FILENO=PATDETAILS.FILENO ORDER BY MKTGPROJECTNO FOR XML PATH ('')),1,1,'') AS MARKETING_PROJECT FROM PATDETAILS " +
            "WHERE FILING_DT BETWEEN CONVERT(SMALLDATETIME,'" + fromDate + "',103) AND CONVERT(SMALLDATETIME,'" + toDate + "',103)";
            if (Status != "All")
            {
                if (Status == "Not Yet") sql = sql + "  AND (COMMERCIALIZED IS NULL OR COMMERCIALIZED LIKE 'Not Yet')"; else sql = sql + "  AND COMMERCIALIZED LIKE '" + Status + "'";
            }
            sql = sql + " ORDER BY CAST(FILENO AS INT) DESC";
            con.Open();
            SqlDataAdapter da = new SqlDataAdapter(sql, con);
            DataSet ds= new dsCommercial();
            da.Fill(ds.Tables["CommStatus"]);
            rptDoc.Load(Server.MapPath("~/Report/rptCommercialize.rpt"));
            rptDoc.SetDataSource(ds);
            CrystalReportViewer1.ReportSource = rptDoc;
            CrystalReportViewer1.HasToggleGroupTreeButton = false;
            con.Close();
        }
        else if (id == "CommercialStatus")
        {
            this.Title = "Commercialization Status";
            System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-GB");
            string fromDate = Request.QueryString["FromDt"];
            string toDate = Request.QueryString["ToDt"];
            string Status = Request.QueryString["Status"];
            string sql="";
            if (Status=="Industry I")
            sql = "SELECT INDUSTRY1 as INDUSTRY,COMMERCIALIZED FROM PATDETAILS " +
            "WHERE FILING_DT BETWEEN CONVERT(SMALLDATETIME,'" + fromDate + "',103) AND CONVERT(SMALLDATETIME,'" + toDate + "',103)";
            else if (Status=="Industry II")
                sql = "SELECT INDUSTRY2 as INDUSTRY,COMMERCIALIZED FROM PATDETAILS " +
                "WHERE FILING_DT BETWEEN CONVERT(SMALLDATETIME,'" + fromDate + "',103) AND CONVERT(SMALLDATETIME,'" + toDate + "',103)";
            sql = sql + " ORDER BY INDUSTRY";
            con.Open();
            SqlDataAdapter da = new SqlDataAdapter(sql, con);
            DataSet ds = new dsCommercial();
            da.Fill(ds.Tables["CommercialStatus"]);
            rptDoc.Load(Server.MapPath("~/Report/rptCommercialStatus.rpt"));
            rptDoc.SetDataSource(ds);
            CrystalReportViewer1.ReportSource = rptDoc;
            CrystalReportViewer1.HasToggleGroupTreeButton = false;
            con.Close();
        }
    }
}
