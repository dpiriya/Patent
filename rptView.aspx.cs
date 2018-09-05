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

public partial class rptView : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection();
    ReportDocument rptDoc = new ReportDocument();   
    protected void Page_Load(object sender, EventArgs e)
    {
        string id= Request.QueryString["id"].ToString();
        con.ConnectionString = ConfigurationManager.ConnectionStrings["PATENTCN"].ConnectionString;
        SqlCommand cmd = new SqlCommand();
        if (id == "PaymentReport")
        {
            this.Title = "Payment Report";
            string Fileno = Request.QueryString["fileno"].ToString();
            string sql = "select fileno,title,Inventor1 as Inventor,department,applcn_no as ApplicationNo,filing_dt as FilingDt,pat_no as PatentNo,pat_dt as PatentDt from patdetails where fileno = '" + Fileno.Trim() + "'";
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = sql;
            cmd.Connection = con;
            con.Open();
            SqlDataAdapter sda = new SqlDataAdapter();
            sda.SelectCommand = cmd;
            PaymentDS ds = new PaymentDS();
            sda.Fill(ds, "PatentInfo");

            string sql1;
            sql1 = "Select SlNo,PType,Country,CostGroup,Activity,PaymentOrChequeDt,PaymentRefOrChequeNo,PaymentAmtINR,Party,Year,InvoiceNo,InvoiceDt from PatentPayment where fileno = '" + Fileno.Trim() + "' order by SlNo";
            cmd.CommandText = sql1;
            sda.SelectCommand = cmd;
            sda.Fill(ds, "PatentPayment");

            rptDoc.Load(Server.MapPath("~/Report/PaymentRpt.rpt"));
            rptDoc.SetDataSource(ds);
            CrystalReportViewer1.ReportSource = rptDoc;
            CrystalReportViewer1.HasToggleGroupTreeButton = false;
            con.Close();
        }
        else if (id == "IPReport")
        {
            this.Title = "International Patent Report";
            string Fileno = Request.QueryString["fileno"].ToString();
            string sql = "select fileno,title,Inventor1 as Inventor,department,applcn_no as ApplicationNo,filing_dt as FilingDt,pat_no as PatentNo,pat_dt as PatentDt from patdetails where fileno = '" + Fileno.Trim() + "'";
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = sql;
            cmd.Connection = con;
            con.Open();
            SqlDataAdapter sda = new SqlDataAdapter();
            sda.SelectCommand = cmd;
            International ds = new International();
            sda.Fill(ds, "IndPatent");

            string sql1;
            sql1 = "select FileNo,subFileNo,RequestDt,Country,Type,Attorney,ApplicationNo,FilingDt,PatentNo,PatentDt,Status,SubStatus from INTERNATIONAL where fileno = '" + Fileno.Trim() + "' order by subFileNo";
            cmd.CommandText = sql1;
            sda.SelectCommand = cmd;
            sda.Fill(ds, "InternationalPatent");

            rptDoc.Load(Server.MapPath("~/Report/InternationalRpt.rpt"));
            rptDoc.SetDataSource(ds);
            CrystalReportViewer1.ReportSource = rptDoc;
            CrystalReportViewer1.HasToggleGroupTreeButton = false;
            con.Close();
        }
        else if (id == "ReceiptReport")
        {
            this.Title = "Receipt Report";
            string Fileno = Request.QueryString["fileno"].ToString();
            string sql = "select fileno,title,Inventor1 as Inventor,department,applcn_no as ApplicationNo,filing_dt as FilingDt,pat_no as PatentNo,pat_dt as PatentDt from patdetails where fileno = '" + Fileno.Trim() + "'";
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = sql;
            cmd.Connection = con;
            con.Open();
            SqlDataAdapter sda = new SqlDataAdapter();
            sda.SelectCommand = cmd;
            ReceiptDS ds = new ReceiptDS();
            sda.Fill(ds, "IndianPat");

            string sql1;
            sql1 = "select EntryDt,FileNo,SlNo,TechTransferNo,Party,PartyRefNo,SubmissionDt,TransType,TransDescription,PaymentGroup,PaymentDescription,Currency,ForeignCost,ExRate,Cost_Rs,PaymentDate,PaymentRef,Year from patentreceipt where fileno = '" + Fileno.Trim() + "' order by SlNo";
            cmd.CommandText = sql1;
            sda.SelectCommand = cmd;
            sda.Fill(ds, "ReceiptDetails");

            rptDoc.Load(Server.MapPath("~/Report/RptReceipt.rpt"));
            rptDoc.SetDataSource(ds);
            CrystalReportViewer1.ReportSource = rptDoc;
            CrystalReportViewer1.HasToggleGroupTreeButton = false;
            con.Close();
        }
        else if (id == "Contract")
        {
            this.Title = "Contract Report";
            string ContractNo = Request.QueryString["ContractNo"].ToString();
            string sql = "select ContractNo,(select Description from listitemmaster where category like 'ContractAgreementType' and itemList =Agreement.AgreementType) as AgreementType,AgreementNo,Title,Scope,Party,CoordinatingPerson,(select DeptName from department where deptcode=Agreement.Dept) as Dept,EffectiveDt,ExpiryDt,Remark,TechTransfer,Status from Agreement where ContractNo = '" + ContractNo.Trim() + "'";
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = sql;
            cmd.Connection = con;
            con.Open();
            SqlDataAdapter sda = new SqlDataAdapter();
            sda.SelectCommand = cmd;
            dsContract ds = new dsContract();
            sda.Fill(ds, "dtContract");

           // string sql1;
           //// sql1 = "select ContractNo,SlNo,ActionType,Narration,ClauseRef,ResponsePerson,TargetDt,Remark,ActionStatus from AgreementAction where ContractNo = '" + ContractNo.Trim() + "' order by SlNo desc";
            //sql1 = "select SlNo,Event,Frequency,ExecutionDate,Basis,DeclDueAmt,InvoiceNo,InvoiceRaised,RecieptNo,RecieptDate,Mode,AmountInRs,Remarks,Status from Royality where ContractNo = '" + ContractNo.Trim() + "' order by SlNo desc";
            //cmd.CommandText = sql1;
            //sda.SelectCommand = cmd;
            //sda.Fill(ds, "dtContractAction");

            rptDoc.Load(Server.MapPath("~/Report/rptContract.rpt"));
            rptDoc.SetDataSource(ds);
            CrystalReportViewer1.ReportSource = rptDoc;
            CrystalReportViewer1.HasToggleGroupTreeButton = false;
            con.Close();
        }
        else if (id == "MarketingProject")
        {
            string ProjNo = Request.QueryString["ProjNo"].ToString();
            string sql1 = "select MktgProjectNo,MktgTitle,MktgCompany,MktgGroup,ProjectValue,ValueRealization,Status,Remarks from marketingProject where MktgProjectNo ='" + ProjNo + "'";
            string sql2 = "select fileno,title,inventor1,applcn_no,status from patdetails where fileno in (select fileno from marketingIDF where mktgProjectNo ='" + ProjNo + "')";
            string sql3 = "select ActivityDt,Channel,ActivityType,Remarks from marketingActivity where mktgProjectNo ='" + ProjNo + "' order by slno desc"; 
            con.Open();
            SqlDataAdapter sda = new SqlDataAdapter(sql1, con);
            SqlDataAdapter sdb = new SqlDataAdapter(sql2, con);
            SqlDataAdapter sdc = new SqlDataAdapter(sql3, con);
            MarketingDS ds = new MarketingDS();
            sda.Fill(ds, "MarketProject");
            sdb.Fill(ds, "MarketIDF");
            sdc.Fill(ds, "MarketActivity");
            rptDoc.Load(Server.MapPath("~/Report/rptMarketing.rpt"));
            rptDoc.SetDataSource(ds);
            CrystalReportViewer1.ReportSource = rptDoc;
            CrystalReportViewer1.HasToggleGroupTreeButton = false;
            con.Close();
        }
    }
}
