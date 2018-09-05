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
public partial class InternshipDetails : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.IsPostBack)
        {
            string sql;
            string InternshipNo = Request.QueryString["InternshipID"].ToString();
            lblTitle.Text = "Internship Details : " + InternshipNo;
            con.ConnectionString = ConfigurationManager.ConnectionStrings["PATENTCN"].ConnectionString;
            string sql1 = "select * from Internship where InternshipID ='" + InternshipNo + "'";
            string sql2 = "select FileDescription,[FileName] from InternshipFiles where InternshipID ='" + InternshipNo + "' order by EntryDt desc"; sql = sql1 + ";" + sql2;
            con.Open();
            SqlDataAdapter sda = new SqlDataAdapter(sql, con);
            DataSet ds = new DataSet();
            sda.Fill(ds);
            dvInternship.DataSource = ds.Tables[0];
            dvInternship.DataBind();
            gvDocument.DataSource = ds.Tables[1];
            gvDocument.DataBind();
            con.Close();
        }
    }
    protected void lbtnAction_Click(object sender, EventArgs e)
    {
        string InternshipNo = Request.QueryString["InternshipID"].ToString();
        GridViewRow gvRow = (GridViewRow)((LinkButton)sender).NamingContainer;
        LinkButton lbFileName = gvRow.FindControl("lbtnView") as LinkButton;
        string strFileName = lbFileName.Text.Trim();
        string strPath = "";
        strPath = @"F:\PatentDocument\Internship\" + InternshipNo.Trim() + @"\";
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
}
