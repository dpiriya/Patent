using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.IO;

public partial class ContractActionAndFile : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.IsPostBack)
        {
            string sql;
            string ContractNo = Request.QueryString["ContractNo"].ToString();
            lblTitle.Text = "Contract Details : " + ContractNo;
            con.ConnectionString = ConfigurationManager.ConnectionStrings["PATENTCN"].ConnectionString;
            // string sql1 = "select ActionType,Narration,ClauseRef,ResponsePerson,TargetDt,Remark,ActionStatus from AgreementAction where ContractNo ='" + ContractNo + "' order by slno desc";
            string sql1 = "select SlNo,Event,Frequency,ExecutionDate,Basis,DeclDueAmt,InvoiceNo,InvoiceRaised,RecieptNo,RecieptDate,Mode,AmountInRs,Status,Remarks from Royality where ContractNo ='" + ContractNo + "' order by slno desc";
            string sql2 = "select FileDescription,[FileName] from agreementFiles where ContractNo ='" + ContractNo + "' order by EntryDt desc"; sql = sql1 + ";" + sql2;
            con.Open();
            SqlDataAdapter sda = new SqlDataAdapter(sql, con);
            DataSet ds = new DataSet();
            sda.Fill(ds);
            gvAction.DataSource = ds.Tables[0];
            gvAction.DataBind();
            gvDocument.DataSource = ds.Tables[1];
            gvDocument.DataBind();
            con.Close();
        }
    }
    protected void lbtnAction_Click(object sender, EventArgs e)
    {
        string ContractNo = Request.QueryString["ContractNo"].ToString();
        GridViewRow gvRow = (GridViewRow)((LinkButton)sender).NamingContainer;
        LinkButton lbFileName = gvRow.FindControl("lbtnView") as LinkButton;
        string strFileName = lbFileName.Text.Trim();
        string strPath = "";
        strPath = @"F:\PatentDocument\Contract Files\" + ContractNo.Trim() + @"\";
        string extension = Path.GetExtension(strPath + strFileName);
        string mimeType = "image/jpeg";
        if (extension == ".jpg")
            mimeType = "image/jpeg";
        else if (extension == ".xls")
            mimeType = "application/vnd.xls";
        else if (extension == ".xlsx")
            mimeType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
        else if (extension == ".doc")
            mimeType = "application/msword";
        else if (extension == ".docx")
            mimeType = "application/vnd.openxmlformats-officedocument.wordprocessingml.document";
        else if (extension == ".ppt")
            mimeType = "application/vnd.ms-powerpoint";
        else if (extension == ".pptx")
            mimeType = "application/vnd.openxmlformats-officedocument.presentationml.presentation";
        else if (extension == ".pdf")
            mimeType = "application/pdf";
        else if (extension == ".txt")
            mimeType = "plain/text";
        FileStream fs = new FileStream(strPath + strFileName, FileMode.Open);
        byte[] bytBytes = new byte[fs.Length];
        fs.Read(bytBytes, 0, (int)fs.Length);
        fs.Close();
        strFileName = strFileName.Replace(" ", "-");
        Response.AppendHeader("Content-Disposition", "attachment;filename=" + strFileName);
        Response.ContentType = mimeType;
        Response.BinaryWrite(bytBytes);
        Response.End();
    }
    protected void lbtnTerm_Click(object sender, EventArgs e)
    {
        using (con = new SqlConnection())
        {
            string ContractNo = Request.QueryString["ContractNo"].ToString();
            string AgreementNo = "", TechTransfer = "", Title = "", Scope = "", Coordinator = "", Party = "", Dept = "", Address1 = "", Address2 = "", City = "", State = "", Country = "", Pincode = "";
            string sql = "select ContractNo,AgreementNo,TechTransfer,Title,Scope,CoordinatingPerson,Dept,Party,Address1,Address2,City,State,Country,Pincode from Agreement a inner join CompanyMaster cm on a.Party=cm.CompanyName where contractno='" + ContractNo + "'";
            SqlCommand cmd = new SqlCommand();
            con.ConnectionString = ConfigurationManager.ConnectionStrings["PATENTCN"].ConnectionString;
            con.Open();
            cmd.CommandText = sql;
            cmd.CommandType = CommandType.Text;
            cmd.Connection = con;
            SqlDataReader dr;
            dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                if (!dr.IsDBNull(1)) AgreementNo = dr.GetString(1); else AgreementNo = "";
                if (!dr.IsDBNull(2)) TechTransfer = dr.GetString(2); else TechTransfer = "";
                if (!dr.IsDBNull(3)) Title = dr.GetString(3); else Title = "";
                if (!dr.IsDBNull(4)) Scope = dr.GetString(4); else Scope = "";
                if (!dr.IsDBNull(5)) Coordinator = dr.GetString(5); else Coordinator = "";
                if (!dr.IsDBNull(6)) Dept = dr.GetString(6); else Dept = "";
                if (!dr.IsDBNull(7)) Party = dr.GetString(7); else Party = "";
                if (!dr.IsDBNull(8)) Address1 = dr.GetString(8); else Address1 = "";
                if (!dr.IsDBNull(9)) Address2 = dr.GetString(9); else Address2 = "";
                if (!dr.IsDBNull(10)) City = dr.GetString(10); else City = "";
                if (!dr.IsDBNull(11)) State = dr.GetString(11); else State = "";
                if (!dr.IsDBNull(12)) Country = dr.GetString(12); else Country = "";
                if (!dr.IsDBNull(13)) Pincode = dr.GetString(13); else Pincode = "";
                
            }
            DataTable dt = new DataTable();
            dt.Columns.AddRange(new DataColumn[] {new DataColumn("ContractNo"), new DataColumn("AgreementNo"), new DataColumn("TechTransfer"), new DataColumn("Title"), new DataColumn("Scope"), new DataColumn("Coordinator"), new DataColumn("Dept"),new DataColumn("Type"),new DataColumn("Amount"),
            new DataColumn("Duedate"),new DataColumn("Party"),new DataColumn("Address1"), new DataColumn("Address2"),new DataColumn("City"), new DataColumn("State"),new DataColumn("Country"),new DataColumn("Pincode"),new DataColumn("Term") });

            LinkButton lbn = (LinkButton)sender;
            GridViewRow grow = (GridViewRow)lbn.NamingContainer;
            int i = Convert.ToInt32(grow.RowIndex);

            //LinkButton lbn = (LinkButton)row.FindControl("Event");
            if (lbn != null)
            {
                string Type = grow.Cells[2].Text;
                string Duedate = grow.Cells[3].Text;
                string Amount = grow.Cells[5].Text;
                string Term = lbn.Text;
                dt.Rows.Add(ContractNo, AgreementNo, TechTransfer, Title, Scope, Coordinator, Dept, Type, Amount, Duedate,Party, Address1, Address2, City, State, Country, Pincode,Term);
            }


            string docfilename = "Template for " + ContractNo + ".docx";

            if (dt.Rows.Count > 0)
            {
                LetterTemplateXML tt = new LetterTemplateXML();
                tt.ProcessRequest(dt, docfilename);
            }
        }
    }
}