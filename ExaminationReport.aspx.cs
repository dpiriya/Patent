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
public partial class ExaminationReport : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["PATENTCN"].ConnectionString);
    protected void Page_Load(object sender, EventArgs e)
    {
        if (User.Identity.IsAuthenticated)
        {
            if (!this.IsPostBack)
            {
                try
                {
                    ContentPlaceHolder contentHolder = PreviousPage.Master.FindControl("cphBody") as ContentPlaceHolder;
                    DropDownList ddlFileNo = (DropDownList)contentHolder.FindControl("ddlFileNo");
                    if (!string.IsNullOrEmpty(ddlFileNo.Text.Trim()))
                        lblFileNo.Text = ddlFileNo.Text.Trim();
                    else
                    {
                        Response.Redirect("IndianPatent.aspx");
                        return;
                    }
                }
                catch (Exception ex)
                {
                    ClientScript.RegisterStartupScript(GetType(), "Error", "<script>alert('" + ex.Message.ToString() + "')</script>");
                    Response.Redirect("IndianPatent.aspx");
                    return;
                }
                ddlFER.Items.Add("FER 1");
                ddlFER.Items.Add("FER 2");
                ShowResult();
            }
        }
        else
        {
            Response.Redirect("Login.aspx");
        }
    }
    protected void ShowResult()
    {
        string sql = "select * from examinationReport where FileNo ='" + lblFileNo.Text.Trim() + "' and ExaminationReport='" + ddlFER.SelectedItem.Text.Trim() + "'";
        DataTable dt = SelectExamination(sql);
        string sql1 = "select * from examinationResponse where FileNo ='" + lblFileNo.Text.Trim() + "' and ExaminationReport='" + ddlFER.SelectedItem.Text.Trim() + "' order by ResponseSlNo";
        DataTable dt1 = SelectExamination(sql1);
        if (dt.Rows.Count < 1) lvExamRpt.InsertItemPosition = InsertItemPosition.LastItem;
        else lvExamRpt.InsertItemPosition = InsertItemPosition.None;
        if (dt1.Rows.Count < 1) lvExamResponse.InsertItemPosition = InsertItemPosition.LastItem;
        else lvExamResponse.InsertItemPosition = InsertItemPosition.None;
        lvExamRpt.DataSource = dt;
        lvExamRpt.DataBind();
        lvExamResponse.DataSource = dt1;
        lvExamResponse.DataBind();
        ViewState["ExaminationReport"] = dt;
        ViewState["ExamRptResponse"] = dt1;
    }
    public DataTable SelectExamination(string sql)
    {
        SqlDataAdapter sda = new SqlDataAdapter(sql, con);
        dsExaminationReport ds = new dsExaminationReport();
        sda.Fill(ds.Tables["dtExaminationRpt"]);
        return ds.Tables["dtExaminationRpt"];
    }
    public DataTable ExaminationResponse(string sql)
    {
        SqlDataAdapter sda = new SqlDataAdapter(sql, con);
        dsExaminationReport ds = new dsExaminationReport();
        sda.Fill(ds.Tables["dtExaminationResponse"]);
        return ds.Tables["dtExaminationResponse"];
    }

    public SqlCommand ExaminationparamCollection(int SlNo,string ExamDt,string ExamFile,string Remarks)
    {
        SqlCommand cmd = new SqlCommand();
        SqlParameter pm1 = new SqlParameter();
        pm1.SourceColumn = "FileNo";
        pm1.ParameterName = "@FileNo";
        pm1.Value = lblFileNo.Text.Trim();
        pm1.SqlDbType = SqlDbType.Char;
        pm1.Direction = ParameterDirection.Input;
        SqlParameter pm2 = new SqlParameter();
        pm2.SourceColumn = "ExaminationReport";
        pm2.ParameterName = "@ExaminationReport";
        pm2.Value = ddlFER.SelectedItem.Text.Trim();
        pm2.SqlDbType = SqlDbType.Char;
        pm2.Direction = ParameterDirection.Input;
        SqlParameter pm3 = new SqlParameter();
        pm3.SourceColumn = "SlNo";
        pm3.ParameterName = "@SlNo";
        pm3.Value = SlNo;
        pm3.SqlDbType = SqlDbType.Int;
        pm3.Direction = ParameterDirection.Input;
        SqlParameter pm4 = new SqlParameter();
        pm4.SourceColumn = "ExaminationDate";
        pm4.ParameterName = "@ExaminationDate";
        pm4.Value = ExamDt;
        pm4.SqlDbType = SqlDbType.Char;
        pm4.Direction = ParameterDirection.Input;
        SqlParameter pm5 = new SqlParameter();
        pm5.SourceColumn = "ExaminationDocument";
        pm5.ParameterName = "@ExaminationDocument";
        pm5.Value = ExamFile;
        pm5.SqlDbType = SqlDbType.Char;
        pm5.Direction = ParameterDirection.Input;
        SqlParameter pm6 = new SqlParameter();
        pm6.SourceColumn = "Remarks";
        pm6.ParameterName = "@Remarks";
        pm6.Value = Remarks;
        pm6.SqlDbType = SqlDbType.Char;
        pm6.Direction = ParameterDirection.Input;
        cmd.Parameters.Add(pm1);
        cmd.Parameters.Add(pm2);
        cmd.Parameters.Add(pm3);
        cmd.Parameters.Add(pm4);
        cmd.Parameters.Add(pm5);
        cmd.Parameters.Add(pm6);
        return cmd;
    }
    protected void lvExamRpt_ItemCanceling(object sender, ListViewCancelEventArgs e)
    {
        lvExamRpt.EditIndex = -1;
        lvExamRpt.DataSource = (DataTable)ViewState["ExaminationReport"];
        lvExamRpt.DataBind();
    }
    protected void lvExamRpt_ItemDeleting(object sender, ListViewDeleteEventArgs e)
    {
        Label lbl = lvExamRpt.Items[e.ItemIndex].FindControl("lblExamSlNo") as Label;
        LinkButton lbExamFile = lvExamRpt.Items[e.ItemIndex].FindControl("lbtnFile") as LinkButton;
        SqlTransaction Trans;
        try
        {
            con.Open();
            Trans = con.BeginTransaction();                    
            SqlCommand cmd = new SqlCommand();
            cmd.Transaction = Trans;
            cmd.CommandText = "ExaminationDelete";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            SqlParameter pm1 = new SqlParameter();
            pm1.SourceColumn = "FileNo";
            pm1.ParameterName = "@FileNo";
            pm1.Value = lblFileNo.Text.Trim();
            pm1.SqlDbType = SqlDbType.Char;
            pm1.Direction = ParameterDirection.Input;
            SqlParameter pm2 = new SqlParameter();
            pm2.SourceColumn = "ExaminationReport";
            pm2.ParameterName = "@ExaminationReport";
            pm2.Value = ddlFER.SelectedItem.Text.Trim();
            pm2.SqlDbType = SqlDbType.Char;
            pm2.Direction = ParameterDirection.Input;
            SqlParameter pm3 = new SqlParameter();
            pm3.SourceColumn = "SlNo";
            pm3.ParameterName = "@SlNo";
            pm3.Value = lbl.Text.Trim();
            pm3.SqlDbType = SqlDbType.Int;
            pm3.Direction = ParameterDirection.Input;
            cmd.Parameters.Add(pm1); cmd.Parameters.Add(pm2); cmd.Parameters.Add(pm3);
            cmd.ExecuteNonQuery();
            try
            {
                string sourceFile = @"F:\PatentDocument\" + lblFileNo.Text.Trim() + @"\Examination" + @"\" + lbExamFile.Text.Trim();
                System.IO.File.Delete(sourceFile);                
            }
            catch (Exception ex)
            {
                ClientScript.RegisterStartupScript(GetType(), "Error", "<script>alert('" + ex.Message.ToString() + "')</script>");
                Trans.Rollback();
                con.Close();
                return;
            }
            Trans.Commit();
            ClientScript.RegisterStartupScript(GetType(), "success", "<script>alert('This Record successfully Deleted')</script>");
            con.Close();
        }
        catch (Exception ex)
        {
            ClientScript.RegisterStartupScript(GetType(), "Error", "<script>alert('" + ex.Message.ToString() + "')</script>");
            con.Close();
            return;
        }
        string sql = "select * from examinationReport where FileNo ='" + lblFileNo.Text.Trim() + "' and ExaminationReport='" + ddlFER.SelectedItem.Text.Trim() + "'";
        DataTable dt = SelectExamination(sql);
        ViewState["ExaminationReport"] = dt;
        if (dt.Rows.Count == 0) lvExamRpt.InsertItemPosition = InsertItemPosition.LastItem; 
        lvExamRpt.DataSource = dt;
        lvExamRpt.DataBind();       
    }
    protected void lvExamRpt_ItemInserting(object sender, ListViewInsertEventArgs e)
    {
        System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-GB");
        TextBox examDt=e.Item.FindControl("txtExamDt") as TextBox;        
        FileUpload uploadFile = e.Item.FindControl("FileUploadER") as FileUpload;
        TextBox Remarks=e.Item.FindControl("txtExamRemarks") as TextBox;        
        DateTime examDate;
        if (!DateTime.TryParse(examDt.Text.Trim(),out examDate))           
        {
            ClientScript.RegisterStartupScript(GetType(), "Examination Date", "<script>alert('Verify Examination Date')</script>");
            return;
        }
        if (uploadFile.HasFile == false)
        {
            ClientScript.RegisterStartupScript(GetType(), "Information", "<script>alert('Please select Upload File')</script>");
            return;
        }
        DataTable dt= (DataTable) ViewState["ExaminationReport"] ;
        int Maxvalue= 0;
        foreach(DataRow dr in dt.Rows)
        {
            int getSlNo= dr.Field<int>("SlNo");            
            Maxvalue=Math.Max(Maxvalue,getSlNo);
        }
        dt.Clear();
        string ExamRptFile="";
        string FileExtension = uploadFile.PostedFile.FileName.Substring(uploadFile.PostedFile.FileName.LastIndexOf(".") + 1).ToLower();
        if (Maxvalue > 0)
        {
            Maxvalue = Maxvalue + 1;
            ExamRptFile = "ExaminationReport-" + lblFileNo.Text.Trim() + "-" + ddlFER.SelectedItem.Text.Trim() + "-" + Maxvalue.ToString() + "." + FileExtension;
        }
        else
        {
            Maxvalue = 1;
            ExamRptFile = "ExaminationReport-" + lblFileNo.Text.Trim() + "-" + ddlFER.SelectedItem.Text.Trim() + "-" + "1" + "." + FileExtension;
        }
        SqlTransaction Trans;
        con.Open();            
        Trans = con.BeginTransaction();                   
        try
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "ExaminationInsert";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            cmd.Transaction = Trans;
            SqlParameter pm1 = new SqlParameter();
            pm1.SourceColumn = "FileNo";
            pm1.ParameterName = "@FileNo";
            pm1.Value = lblFileNo.Text.Trim();
            pm1.SqlDbType = SqlDbType.Char;
            pm1.Direction = ParameterDirection.Input;
            SqlParameter pm2 = new SqlParameter();
            pm2.SourceColumn = "ExaminationReport";
            pm2.ParameterName = "@ExaminationReport";
            pm2.Value = ddlFER.SelectedItem.Text.Trim();
            pm2.SqlDbType = SqlDbType.Char;
            pm2.Direction = ParameterDirection.Input;
            SqlParameter pm3 = new SqlParameter();
            pm3.SourceColumn = "SlNo";
            pm3.ParameterName = "@SlNo";
            pm3.Value = Maxvalue;
            pm3.SqlDbType = SqlDbType.Int;
            pm3.Direction = ParameterDirection.Input;
            SqlParameter pm4 = new SqlParameter();
            pm4.SourceColumn = "ExaminationDate";
            pm4.ParameterName = "@ExaminationDate";
            pm4.Value = examDate.ToShortDateString();
            pm4.SqlDbType = SqlDbType.Char;
            pm4.Direction = ParameterDirection.Input;
            SqlParameter pm5 = new SqlParameter();
            pm5.SourceColumn = "ExaminationDocument";
            pm5.ParameterName = "@ExaminationDocument";
            pm5.Value = ExamRptFile;
            pm5.SqlDbType = SqlDbType.Char;
            pm5.Direction = ParameterDirection.Input;
            SqlParameter pm6 = new SqlParameter();
            pm6.SourceColumn = "Remarks";
            pm6.ParameterName = "@Remarks";
            pm6.Value = Remarks.Text.Trim();
            pm6.SqlDbType = SqlDbType.Char;
            pm6.Direction = ParameterDirection.Input;
            SqlParameter pm7 = new SqlParameter();
            pm7.SourceColumn = "EntryDt";
            pm7.ParameterName = "@EntryDt";
            pm7.Value = DateTime.Now.ToShortDateString();
            pm7.SqlDbType = SqlDbType.Char;
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
                HttpPostedFile file1 = uploadFile.PostedFile;
                Int32 fileLength = file1.ContentLength;
                string fileName = file1.FileName;
                byte[] buffer = new byte[fileLength];
                file1.InputStream.Read(buffer, 0, fileLength);
                FileStream newFile;
                string strPath = @"F:\PatentDocument\" + lblFileNo.Text.Trim() + @"\Examination"  + @"\";
                if (!Directory.Exists(strPath))
                {
                    Directory.CreateDirectory(strPath);
                }
                if (!File.Exists(strPath + ExamRptFile))
                {
                    newFile = File.Open(strPath + ExamRptFile, FileMode.Create);
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
            string sql = "select * from examinationReport where FileNo ='" + lblFileNo.Text.Trim() + "' and ExaminationReport='" + ddlFER.SelectedItem.Text.Trim() + "'";
            dt = SelectExamination(sql);
            ViewState["ExaminationReport"] = dt;
            lvExamRpt.InsertItemPosition = InsertItemPosition.None;
            lvExamRpt.DataSource = dt;
            lvExamRpt.DataBind();
        }
        catch (Exception ex)
        {
            ClientScript.RegisterStartupScript(GetType(), "Error", "<script>alert('" + ex.Message.ToString() + "')</script>");
            Trans.Rollback();
            con.Close();
            return;
        }
    }    
    protected void lvExamResponse_ItemCanceling(object sender, ListViewCancelEventArgs e)
    {
        lvExamResponse.EditIndex = -1;
        lvExamResponse.DataSource = (DataTable)ViewState["ExamRptResponse"];
        lvExamResponse.DataBind();
    }
    protected void lvExamResponse_ItemDeleting(object sender, ListViewDeleteEventArgs e)
    {
        Label lblResponseSlno = lvExamResponse.Items[e.ItemIndex].FindControl("lblSlNo") as Label;
        LinkButton lbExamResponseFile = lvExamResponse.Items[e.ItemIndex].FindControl("lbtnResponseFile") as LinkButton;
        SqlTransaction Trans;
        try
        {
            con.Open();
            Trans = con.BeginTransaction();
            SqlCommand cmd = new SqlCommand();
            cmd.Transaction = Trans;
            cmd.CommandText = "ExamResponseDelete";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            SqlParameter pm1 = new SqlParameter();
            pm1.SourceColumn = "FileNo";
            pm1.ParameterName = "@FileNo";
            pm1.Value = lblFileNo.Text.Trim();
            pm1.SqlDbType = SqlDbType.Char;
            pm1.Direction = ParameterDirection.Input;
            SqlParameter pm2 = new SqlParameter();
            pm2.SourceColumn = "ExaminationReport";
            pm2.ParameterName = "@ExaminationReport";
            pm2.Value = ddlFER.SelectedItem.Text.Trim();
            pm2.SqlDbType = SqlDbType.Char;
            pm2.Direction = ParameterDirection.Input;
            SqlParameter pm3 = new SqlParameter();
            pm3.SourceColumn = "ResponseSlNo";
            pm3.ParameterName = "@ResponseSlNo";
            pm3.Value = lblResponseSlno.Text.Trim();
            pm3.SqlDbType = SqlDbType.Int;
            pm3.Direction = ParameterDirection.Input;
            cmd.Parameters.Add(pm1); cmd.Parameters.Add(pm2); cmd.Parameters.Add(pm3);
            cmd.ExecuteNonQuery();
            try
            {
                string sourceFile = @"F:\PatentDocument\" + lblFileNo.Text.Trim() + @"\Examination" + @"\" + lbExamResponseFile.Text.Trim();
                System.IO.File.Delete(sourceFile);
            }
            catch (Exception ex)
            {
                ClientScript.RegisterStartupScript(GetType(), "Error", "<script>alert('" + ex.Message.ToString() + "')</script>");
                Trans.Rollback();
                con.Close();
                return;
            }
            Trans.Commit();
            ClientScript.RegisterStartupScript(GetType(), "success", "<script>alert('This Record successfully Deleted')</script>");
            con.Close();
        }
        catch (Exception ex)
        {
            ClientScript.RegisterStartupScript(GetType(), "Error", "<script>alert('" + ex.Message.ToString() + "')</script>");
            con.Close();
            return;
        }
        string sql = "select * from examinationResponse where FileNo ='" + lblFileNo.Text.Trim() + "' and ExaminationReport='" + ddlFER.SelectedItem.Text.Trim() + "' order by ResponseSlNo";
        DataTable dt = SelectExamination(sql);
        ViewState["ExamRptResponse"] = dt;
        if (dt.Rows.Count == 0) lvExamResponse.InsertItemPosition = InsertItemPosition.LastItem;
        lvExamResponse.DataSource = dt;
        lvExamResponse.DataBind();   
    }
    protected void btnExamResponseAdd_Click(object sender, EventArgs e)
    {
        if (lvExamResponse.InsertItemPosition == InsertItemPosition.None)
            lvExamResponse.InsertItemPosition = InsertItemPosition.LastItem;
        else
            lvExamResponse.InsertItemPosition = InsertItemPosition.None;
        lvExamResponse.DataSource = (DataTable)ViewState["ExamRptResponse"];
        lvExamResponse.DataBind();
    }
    protected void lvExamResponse_ItemInserting(object sender, ListViewInsertEventArgs e)
    {
        System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-GB");
        TextBox examDt = e.Item.FindControl("txtResponseDt") as TextBox;
        FileUpload uploadFile = e.Item.FindControl("FileUploadResponse") as FileUpload;
        TextBox Remarks = e.Item.FindControl("txtResponseRemarks") as TextBox;
        DateTime examDate;
        if (!DateTime.TryParse(examDt.Text.Trim(), out examDate))
        {
            ClientScript.RegisterStartupScript(GetType(), "Response Date", "<script>alert('Verify Response Date')</script>");
            return;
        }
        if (uploadFile.HasFile == false)
        {
            ClientScript.RegisterStartupScript(GetType(), "Information", "<script>alert('Please select Upload File')</script>");
            return;
        }
        DataTable dt = (DataTable)ViewState["ExamRptResponse"];
        int Maxvalue = 0;
        foreach (DataRow dr in dt.Rows)
        {
            int getSlNo = dr.Field<int>("ResponseSlNo");
            Maxvalue = Math.Max(Maxvalue, getSlNo);
        }
        dt.Clear();
        string ExamRptFile = "";
        string FileExtension = uploadFile.PostedFile.FileName.Substring(uploadFile.PostedFile.FileName.LastIndexOf(".") + 1).ToLower();
        if (Maxvalue > 0)
        {
            Maxvalue = Maxvalue + 1;
            ExamRptFile = "ResponseExamRpt-" + lblFileNo.Text.Trim() + "-" + ddlFER.SelectedItem.Text.Trim() + "-" + Maxvalue.ToString() + "." + FileExtension;
        }
        else
        {
            Maxvalue = 1;
            ExamRptFile = "ResponseExamRpt-" + lblFileNo.Text.Trim() + "-" + ddlFER.SelectedItem.Text.Trim() + "-" + "1" + "." + FileExtension;
        }
        SqlTransaction Trans;
        con.Open();
        Trans = con.BeginTransaction();
        try
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "ExaminationResponseInsert";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            cmd.Transaction = Trans;
            SqlParameter pm1 = new SqlParameter();
            pm1.SourceColumn = "FileNo";
            pm1.ParameterName = "@FileNo";
            pm1.Value = lblFileNo.Text.Trim();
            pm1.SqlDbType = SqlDbType.Char;
            pm1.Direction = ParameterDirection.Input;
            SqlParameter pm2 = new SqlParameter();
            pm2.SourceColumn = "ExaminationReport";
            pm2.ParameterName = "@ExaminationReport";
            pm2.Value = ddlFER.SelectedItem.Text.Trim();
            pm2.SqlDbType = SqlDbType.Char;
            pm2.Direction = ParameterDirection.Input;
            SqlParameter pm3 = new SqlParameter();
            pm3.SourceColumn = "ResponseSlNo";
            pm3.ParameterName = "@ResponseSlNo";
            pm3.Value = Maxvalue;
            pm3.SqlDbType = SqlDbType.Int;
            pm3.Direction = ParameterDirection.Input;
            SqlParameter pm4 = new SqlParameter();
            pm4.SourceColumn = "ResponseDate";
            pm4.ParameterName = "@ResponseDate";
            pm4.Value = examDate.ToShortDateString();
            pm4.SqlDbType = SqlDbType.Char;
            pm4.Direction = ParameterDirection.Input;
            SqlParameter pm5 = new SqlParameter();
            pm5.SourceColumn = "ResponseDocument";
            pm5.ParameterName = "@ResponseDocument";
            pm5.Value = ExamRptFile;
            pm5.SqlDbType = SqlDbType.Char;
            pm5.Direction = ParameterDirection.Input;
            SqlParameter pm6 = new SqlParameter();
            pm6.SourceColumn = "Remarks";
            pm6.ParameterName = "@Remarks";
            pm6.Value = Remarks.Text.Trim();
            pm6.SqlDbType = SqlDbType.Char;
            pm6.Direction = ParameterDirection.Input;
            SqlParameter pm7 = new SqlParameter();
            pm7.SourceColumn = "EntryDt";
            pm7.ParameterName = "@EntryDt";
            pm7.Value = DateTime.Now.ToShortDateString();
            pm7.SqlDbType = SqlDbType.Char;
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
                HttpPostedFile file1 = uploadFile.PostedFile;
                Int32 fileLength = file1.ContentLength;
                string fileName = file1.FileName;
                byte[] buffer = new byte[fileLength];
                file1.InputStream.Read(buffer, 0, fileLength);
                FileStream newFile;
                string strPath = @"F:\PatentDocument\" + lblFileNo.Text.Trim() + @"\Examination" + @"\";
                if (!Directory.Exists(strPath))
                {
                    Directory.CreateDirectory(strPath);
                }
                if (!File.Exists(strPath + ExamRptFile))
                {
                    newFile = File.Open(strPath + ExamRptFile, FileMode.Create);
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
            string sql = "select * from examinationResponse where FileNo ='" + lblFileNo.Text.Trim() + "' and ExaminationReport='" + ddlFER.SelectedItem.Text.Trim() + "'";
            dt = SelectExamination(sql);
            ViewState["ExamRptResponse"] = dt;
            lvExamResponse.InsertItemPosition = InsertItemPosition.None;
            lvExamResponse.DataSource = dt;
            lvExamResponse.DataBind();
        }
        catch (Exception ex)
        {
            ClientScript.RegisterStartupScript(GetType(), "Error", "<script>alert('" + ex.Message.ToString() + "')</script>");
            Trans.Rollback();
            con.Close();
            return;
        }
    }       
    protected void btnExamAdd_Click(object sender, EventArgs e)
    {
        if (lvExamRpt.InsertItemPosition == InsertItemPosition.None) 
            lvExamRpt.InsertItemPosition = InsertItemPosition.LastItem;
        else
            lvExamRpt.InsertItemPosition = InsertItemPosition.None;
        lvExamRpt.DataSource = (DataTable)ViewState["ExaminationReport"];
        lvExamRpt.DataBind();
    }
    protected void ddlFER_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlFER.SelectedIndex == 1)
        {
            string sql1 = "select count(*) from examinationReport where FileNo ='" + lblFileNo.Text.Trim() + "' and ExaminationReport='FER 1'";
            con.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = sql1;
            cmd.CommandType = CommandType.Text;
            cmd.Connection = con;
            int CountValue = Convert.ToInt32(cmd.ExecuteScalar());
            if (CountValue == 0)
            {
                ClientScript.RegisterStartupScript(GetType(), "Error", "<script>alert('First Examination Report can not be empty')</script>");
                ddlFER.SelectedIndex = 0;
            }
            con.Close();
        }
        ShowResult();
    }
    protected void lvExamRpt_SelectedIndexChanging(object sender, ListViewSelectEventArgs e)
    {
        LinkButton lbFileName = lvExamRpt.Items[e.NewSelectedIndex].FindControl("lbtnFile") as LinkButton;
        string strPath = "";
        string strFileName = "";
        strPath = @"F:\PatentDocument\" + lblFileNo.Text.Trim() + @"\Examination" + @"\";
        strFileName = lbFileName.Text.Trim();
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
    protected void lvExamResponse_SelectedIndexChanging(object sender, ListViewSelectEventArgs e)
    {
        LinkButton lbFileName = lvExamResponse.Items[e.NewSelectedIndex].FindControl("lbtnResponseFile") as LinkButton;
        string strPath = "";
        string strFileName = "";
        strPath = @"F:\PatentDocument\" + lblFileNo.Text.Trim() + @"\Examination" + @"\";
        strFileName = lbFileName.Text.Trim();
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
    protected void lvExamRpt_ItemDataBound(object sender, ListViewItemEventArgs e)
    {
        if (e.Item.ItemType == ListViewItemType.DataItem)
        {
            LinkButton lb = e.Item.FindControl("lbtnDelete") as LinkButton;
            if (lb != null)
            {
                if (User.IsInRole("Admin") || User.IsInRole("DocumentManage"))
                {
                    lb.Visible = true;
                }
            }
        }
    }
    protected void lvExamResponse_ItemDataBound(object sender, ListViewItemEventArgs e)
    {
        if (e.Item.ItemType == ListViewItemType.DataItem)
        {
            LinkButton lb = e.Item.FindControl("lbtnDelete") as LinkButton;
            if (lb != null)
            {
                if (User.IsInRole("Admin") || User.IsInRole("DocumentManage"))
                {
                    lb.Visible = true;
                }
            }
        }
    }
}
