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

public partial class DocumentUpload : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection();
    SqlTransaction Trans;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.IsPostBack)
        {
            ddlFileType.Items.Add(new ListItem("",""));
            ddlFileType.Items.Add(new ListItem("IDF","IDF"));
            ddlFileType.Items.Add(new ListItem("Marketing", "Marketing"));
            imgBtnInsert.Visible = (!User.IsInRole("View"));
        }
    }
    protected void imgBtnInsert_Click(object sender, ImageClickEventArgs e)
    {
        SqlCommand cmd = new SqlCommand();
        System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-GB");
        con.ConnectionString = ConfigurationManager.ConnectionStrings["PATENTCN"].ConnectionString;
        if (FileUpload1.HasFile == false)
        {
            ClientScript.RegisterStartupScript(GetType(), "Information", "<script>alert('Please select Upload File')</script>");
            return;
        }
        try
        {
            string sql = "insert into PatentFileDetails (EntryDt,FileNo,FileDescription,FileName,ModifiedDt,Comments,UserName) values (case when @EntryDt='' then null else convert(smalldatetime,@EntryDt,103) end,@FileNo,@FileDescription,@FileName, case when @ModifiedDt='' then null else convert(smalldatetime,@ModifiedDt,103) end,case when @Comments='' then null else @Comments end,@UserName)";
            cmd.CommandText = sql;
            cmd.CommandType = CommandType.Text;
            cmd.Connection = con;
            con.Open();
            Trans = con.BeginTransaction();
            cmd.Transaction = Trans;
            SqlParameter pm1 = new SqlParameter();
            pm1.ParameterName = "@EntryDt";
            pm1.SourceColumn = "EntryDt";
            pm1.Value = DateTime.Now.ToShortDateString();
            pm1.DbType = DbType.String;
            pm1.Direction = ParameterDirection.Input;

            SqlParameter pm2 = new SqlParameter();
            pm2.ParameterName = "@FileNo";
            pm2.SourceColumn = "FileNo";
            pm2.Value = ddlFileNo.SelectedItem.Text.Trim();
            pm2.DbType = DbType.String;
            pm2.Direction = ParameterDirection.Input;

            string docInfo;
            if (ddlDocInfo.SelectedItem.Text.Trim() != "Others")
            {
                docInfo = ddlDocInfo.SelectedItem.Text.Trim();
            }
            else
            { 
                docInfo = txtDocInfo.Text.Trim(); 
            }
            SqlParameter pm3 = new SqlParameter();
            pm3.ParameterName = "@FileDescription";
            pm3.SourceColumn = "FileDescription";
            pm3.Value = docInfo;
            pm3.DbType = DbType.String;
            pm3.Direction = ParameterDirection.Input;

            string fname = FileUpload1.FileName;
            SqlParameter pm4 = new SqlParameter();
            pm4.ParameterName = "@FileName";
            pm4.SourceColumn = "FileName";
            pm4.Value = fname;
            pm4.DbType = DbType.String;
            pm4.Direction = ParameterDirection.Input;

            SqlParameter pm5 = new SqlParameter();
            pm5.ParameterName = "@ModifiedDt";
            pm5.SourceColumn = "ModifiedDt";
            pm5.Value = DateTime.Now.ToShortDateString();
            pm5.DbType = DbType.String;
            pm5.Direction = ParameterDirection.Input;

            SqlParameter pm6 = new SqlParameter();
            pm6.ParameterName = "@Comments";
            pm6.SourceColumn = "Comments";
            pm6.Value = txtComment.Text.Trim();
            pm6.DbType = DbType.String;
            pm6.Direction = ParameterDirection.Input;

            SqlParameter pm7 = new SqlParameter();
            pm7.ParameterName = "@UserName";
            pm7.SourceColumn = "UserName";
            pm7.Value = Membership.GetUser().UserName.ToString();
            pm7.DbType = DbType.String;
            pm7.Direction = ParameterDirection.Input;

            cmd.Parameters.Add(pm1);
            cmd.Parameters.Add(pm2);
            cmd.Parameters.Add(pm3);
            cmd.Parameters.Add(pm4);
            cmd.Parameters.Add(pm5);
            cmd.Parameters.Add(pm6);
            cmd.Parameters.Add(pm7);

            cmd.ExecuteNonQuery();
            
            try
            {
                HttpPostedFile file1 = FileUpload1.PostedFile;
                Int32 fileLength = file1.ContentLength;
                string fileName = file1.FileName;
                byte[] buffer = new byte[fileLength];
                file1.InputStream.Read(buffer, 0, fileLength);
                FileStream newFile;
                string strPath = @"F:\PatentDocument\" + ddlFileNo.SelectedItem.Text.Trim() + @"\";
                if (!Directory.Exists(strPath))
                {
                    Directory.CreateDirectory(strPath);
                }
                if (!File.Exists(strPath + fname))
                {
                    newFile = File.Open(strPath + fname, FileMode.Create);
                    newFile.Write(buffer, 0, buffer.Length);
                    newFile.Close();
                }
                else
                {
                    ClientScript.RegisterStartupScript(GetType(), "Information", "<script>alert('This File exists in this Folder')</script>");
                    Trans.Rollback();
                    con.Close();
                    return;
                }
            }
            catch (Exception ex)
            {
                ClientScript.RegisterStartupScript(GetType(), "Error", "<script>alert('" + ex.Message.ToString() + "')</script>");
                Trans.Rollback();
                con.Close();
                return;
            }
            ClientScript.RegisterStartupScript(GetType(), "Success", "<script>alert('This Record successfully added')</script>");
            Trans.Commit();
            con.Close();
        }
        catch(Exception ex)
        {
            ClientScript.RegisterStartupScript(GetType(), "Error", "<script>alert('" + ex.Message.ToString() + "')</script>");
            Trans.Rollback();
            con.Close();
            return;
        }
        imgBtnClear_Click(sender, e);
    }
    protected void imgBtnClear_Click(object sender, ImageClickEventArgs e)
    {
        ddlFileNo.Text = "";
        ddlDocInfo.Text = "";
        txtDocInfo.Text="";
        txtComment.Text = "";
    }
    protected void ddlFileType_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlFileNo.Items.Clear();
        ddlDocInfo.Items.Clear();
        con.ConnectionString = ConfigurationManager.ConnectionStrings["PATENTCN"].ConnectionString;
        SqlCommand cmd = new SqlCommand();
        if (ddlFileType.SelectedIndex == 1)
        {
            string sql = "select fileno from patdetails order by fileno";
            SqlDataReader dr;
            cmd.CommandType = CommandType.Text;
            cmd.Connection = con;
            cmd.CommandText = sql;
            con.Open();
            dr = cmd.ExecuteReader();
            ddlFileNo.Items.Add("");
            while (dr.Read())
            {
                ddlFileNo.Items.Add(dr.GetString(0));
            }
            dr.Close();
            string sql1 = "SELECT ITEMLIST FROM LISTITEMMASTER WHERE CATEGORY='File Description' and grouping is null ORDER BY ITEMLIST";
            cmd.CommandText = sql1;
            dr = cmd.ExecuteReader();
            ddlDocInfo.Items.Add("");
            while (dr.Read())
            {
                ddlDocInfo.Items.Add(new ListItem(dr.GetString(0), dr.GetString(0)));
            }
            ddlDocInfo.Items.Add(new ListItem("Others", "Others"));
            dr.Close();
            con.Close();
        }
        else if(ddlFileType.SelectedIndex == 2)
        {
            string sql = "select mktgProjectNo from marketingProject order by cast(substring(mktgProjectNo,3,len(mktgProjectNo)) as int)";
            SqlDataReader dr;
            cmd.CommandType = CommandType.Text;
            cmd.Connection = con;
            cmd.CommandText = sql;
            con.Open();
            dr = cmd.ExecuteReader();
            ddlFileNo.Items.Add("");
            while (dr.Read())
            {
                ddlFileNo.Items.Add(dr.GetString(0));
            }
            dr.Close();
            string sql1 = "SELECT ITEMLIST FROM LISTITEMMASTER WHERE CATEGORY='File Description' and grouping ='Marketing'";
            cmd.CommandText = sql1;
            dr = cmd.ExecuteReader();
            ddlDocInfo.Items.Add("");
            while (dr.Read())
            {
                ddlDocInfo.Items.Add(new ListItem(dr.GetString(0), dr.GetString(0)));
            }
            ddlDocInfo.Items.Add(new ListItem("Others", "Others"));
            dr.Close();
            con.Close();
        }
    }
}
