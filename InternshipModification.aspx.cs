using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Web.Security;
using System.IO;

public partial class InternshipModification : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (User.IsInRole("Admin") || User.IsInRole("Super User") || User.IsInRole("Marketing"))
        {
            if (!this.IsPostBack)
            {
                Tab1.CssClass = "Clicked";
                MainView.ActiveViewIndex = 0;
                btnUpload.Visible = false;
            }
        }
        else
        {
            Server.Transfer("Unautherized.aspx");
        }
    }
    protected void Tab1_Click(object sender, EventArgs e)
    {
        Tab1.CssClass = "Clicked";
        Tab2.CssClass = "Initial";
        MainView.ActiveViewIndex = 0;
    }
    protected void Tab2_Click(object sender, EventArgs e)
    {
        Tab2.CssClass = "Clicked";
        Tab1.CssClass = "Initial";
        MainView.ActiveViewIndex = 1;
    }
    protected void fetchFiles(string InternshipID)
    {
        gvFileDetails.DataSource = null;
        gvFileDetails.DataBind();
        SqlCommand cmdFF = new SqlCommand();
        con.ConnectionString = ConfigurationManager.ConnectionStrings["PATENTCN"].ConnectionString;
        con.Open();
        System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-GB");
        string sql5 = "select EntryDt,InternshipID,FileDescription,ModifiedDt,Comments,FileName from Internshipfiles where InternshipID='" + InternshipID.Trim() + "' order by EntryDt desc";
        cmdFF.CommandText = sql5;
        cmdFF.CommandType = CommandType.Text;
        cmdFF.Connection = con;
        SqlDataReader drFF;
        drFF = cmdFF.ExecuteReader();
        DataTable dt = new DataTable();
        dt.Load(drFF);
        if (dt.Rows.Count > 25)
        {
            pageNumber.Visible = true;
        }
        else
        {
            pageNumber.Visible = false;
        }
        Session["InternshipView"] = dt;
        gvFileDetails.DataSource = dt;
        gvFileDetails.DataBind();
        drFF.Close();
        con.Close();
    }
    protected void gvFileDetails_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvFileDetails.PageIndex = e.NewPageIndex;
        gvFileDetails.DataSource = Session["InternshipView"];
        gvFileDetails.DataBind();
    }
    protected void gvFileDetails_RowCommand(object sender, GridViewCommandEventArgs e)
    {

    }
    protected void gvFileDetails_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        SqlTransaction trans;
        try
        {
            SqlCommand cmd = new SqlCommand();
            GridViewRow item = gvFileDetails.Rows[e.RowIndex];
            string InternshipID = item.Cells[1].Text.Trim();
            string fileDescription = item.Cells[2].Text.Trim();
            LinkButton lb = (LinkButton)item.FindControl("lbtnEdit");
            string fileName = lb.Text.Trim();
            con.ConnectionString = ConfigurationManager.ConnectionStrings["PATENTCN"].ConnectionString;
            string sql = "delete from InternshipFiles where InternshipID = '" + InternshipID.Trim() + "' and fileDescription = '" + fileDescription.Trim() + "' and fileName = '" + fileName.Trim() + "'";
            con.Open();
            trans = con.BeginTransaction();
            cmd.Transaction = trans;
            cmd.CommandText = sql;
            cmd.CommandType = CommandType.Text;
            cmd.Connection = con;
            cmd.ExecuteNonQuery();
            try
            {
                string sourceFile = @"F:\PatentDocument\Internship\" + InternshipID.Trim() + @"\" + fileName.Trim();
                string targetPath = @"F:\PatentDocument\Internship\Delete Files\" + InternshipID.Trim() + @"\";
                if (!Directory.Exists(targetPath))
                {
                    Directory.CreateDirectory(targetPath);
                }
                if (!File.Exists(targetPath + fileName.Trim()))
                {
                    string targetFile = targetPath + fileName.Trim();
                    System.IO.File.Move(sourceFile, targetFile);
                }
                else
                {
                    string tmpFileName = Path.GetFileNameWithoutExtension(sourceFile);
                    string extension = Path.GetExtension(sourceFile);
                    tmpFileName = tmpFileName + DateTime.Now.ToString("dd-MM-yyyy-hh-mm");
                    tmpFileName = tmpFileName + extension;
                    string targetFile = targetPath + tmpFileName.Trim();
                    System.IO.File.Move(sourceFile, targetFile);
                }
            }
            catch (Exception ex)
            {
                ClientScript.RegisterStartupScript(GetType(), "Error", "<script>alert('" + ex.Message.ToString() + "')</script>");
                trans.Rollback();
                con.Close();
                return;
            }
            trans.Commit();
            ClientScript.RegisterStartupScript(GetType(), "success", "<script>alert('This Record successfully Deleted')</script>");
            con.Close();
            fetchFiles(InternshipID);            
        }
        catch (Exception ex)
        {
            ClientScript.RegisterStartupScript(GetType(), "Error", "<script>alert('" + ex.Message.ToString() + "')</script>");
            con.Close();
            return;
        }
    }
    protected void gvFileDetails_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        GridViewRow item = gvFileDetails.Rows[e.NewSelectedIndex];
        string InternshipID = item.Cells[1].Text;
        LinkButton lbFileName = gvFileDetails.Rows[e.NewSelectedIndex].FindControl("lbtnEdit") as LinkButton;

        string strPath = "";
        string strFileName = "";
        strPath = @"F:\PatentDocument\Internship\" + InternshipID.Trim() + @"\";
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
    private string getSortDirection(string column)
    {
        string sortDirection = "ASC";
        string sortExpression = ViewState["sortExpression"] as string;
        if (sortExpression != null)
        {
            if (sortExpression == column)
            {
                string lastDirection = ViewState["sortDirection"] as string;
                if ((lastDirection != null) && (lastDirection == "ASC"))
                {
                    sortDirection = "Desc";
                }
            }
        }
        ViewState["sortExpression"] = column;
        ViewState["sortDirection"] = sortDirection;

        return sortDirection;
    }
    protected void gvFileDetails_Sorting(object sender, GridViewSortEventArgs e)
    {
        DataTable dt = Session["InternshipView"] as DataTable;
        if (dt != null)
        {
            dt.DefaultView.Sort = e.SortExpression + " " + getSortDirection(e.SortExpression);

            gvFileDetails.DataSource = dt;
            gvFileDetails.DataBind();
        }
    }
    protected void gvFileDetails_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        LinkButton lb = e.Row.FindControl("lbtnDelete") as LinkButton;
        if (lb != null)
        {
            if (User.IsInRole("Admin") || User.IsInRole("Super User"))
            {
                lb.Visible = true;
            }
        }

        if (!User.IsInRole("Admin") && !User.IsInRole("Super User"))
        {
            if (e.Row.RowType == DataControlRowType.DataRow || e.Row.RowType == DataControlRowType.Header)
                e.Row.Cells[6].Visible = false;

        }
    }
    protected void ClearFileUpload()
    {
        txtFileDesc.Text = "";
        FileUploadAgree.Dispose();
        txtComment.Text = "";
    }
    protected void btnUpload_Click(object sender, EventArgs e)
    {
        panNewFile.Visible = true;
        btnUpload.Visible = false;
        ClearFileUpload();
    }
    protected void btnFileSave_Click(object sender, EventArgs e)
    {
        if (txtFileDesc.Text.Trim() == "")
        {
            ClientScript.RegisterStartupScript(GetType(), "File Description", "<script>alert('Description of File can not be empty')</script>");
            return;
        }
        if (!FileUploadAgree.HasFile)
        {
            ClientScript.RegisterStartupScript(GetType(), "Upload File", "<script>alert('Select Uploading File')</script>");
            return;
        }
        string fname = FileUploadAgree.FileName;
        using (SqlConnection cn = new SqlConnection())
        {
            cn.ConnectionString = ConfigurationManager.ConnectionStrings["PATENTCN"].ConnectionString;
            try
            {
                using (SqlCommand cmd2 = new SqlCommand())
                {
                    cmd2.CommandText = "Select fileDescription,fileName from InternshipFiles where InternshipID='" + txtInternshipID.Text.Trim() + "'";
                    cmd2.CommandType = CommandType.Text;
                    cmd2.Connection = cn;
                    cn.Open();
                    SqlDataReader sdr;
                    sdr = cmd2.ExecuteReader();
                    while (sdr.Read())
                    {
                        if (sdr.GetString(0) == txtFileDesc.Text.Trim() || sdr.GetString(1) == fname)
                        {
                            ClientScript.RegisterStartupScript(GetType(), "Error", "<script>alert('File Description or File Name already Exist')</script>");
                            cn.Close();
                            return;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ClientScript.RegisterStartupScript(GetType(), "Error", "<script>alert('" + ex.Message.ToString() + "')</script>");
                cn.Close();
                return;
            }
        }
        SqlCommand cmd = new SqlCommand();
        SqlTransaction Trans;
        System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-GB");
        con.ConnectionString = ConfigurationManager.ConnectionStrings["PATENTCN"].ConnectionString;
        try
        {
            cmd.CommandText = "insert into InternshipFiles (EntryDt,InternshipID,FileDescription,FileName,Comments,UserName) values (case when @EntryDt='' then null else convert(smalldatetime,@EntryDt,103) end,@InternshipID,@FileDescription,@FileName,@Comments,@UserName)";
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
            pm2.ParameterName = "@InternshipID";
            pm2.SourceColumn = "InternshipID";
            pm2.Value = txtInternshipID.Text.Trim();
            pm2.DbType = DbType.String;
            pm2.Direction = ParameterDirection.Input;

            SqlParameter pm3 = new SqlParameter();
            pm3.ParameterName = "@FileDescription";
            pm3.SourceColumn = "FileDescription";
            pm3.Value = txtFileDesc.Text.Trim();
            pm3.DbType = DbType.String;
            pm3.Direction = ParameterDirection.Input;

            SqlParameter pm4 = new SqlParameter();
            pm4.ParameterName = "@FileName";
            pm4.SourceColumn = "FileName";
            pm4.Value = fname;
            pm4.DbType = DbType.String;
            pm4.Direction = ParameterDirection.Input;

            SqlParameter pm5 = new SqlParameter();
            pm5.ParameterName = "@Comments";
            pm5.SourceColumn = "Comments";
            if (txtComment.Text.Trim() == "") pm5.Value = DBNull.Value; else pm5.Value = txtComment.Text.Trim();
            pm5.DbType = DbType.String;
            pm5.Direction = ParameterDirection.Input;

            SqlParameter pm6 = new SqlParameter();
            pm6.ParameterName = "@UserName";
            pm6.SourceColumn = "UserName";
            pm6.Value = Membership.GetUser().UserName.ToString();
            pm6.DbType = DbType.String;
            pm6.Direction = ParameterDirection.Input;

            cmd.Parameters.Add(pm1);
            cmd.Parameters.Add(pm2);
            cmd.Parameters.Add(pm3);
            cmd.Parameters.Add(pm4);
            cmd.Parameters.Add(pm5);
            cmd.Parameters.Add(pm6);

            cmd.ExecuteNonQuery();
            try
            {
                HttpPostedFile file1 = FileUploadAgree.PostedFile;
                Int32 fileLength = file1.ContentLength;
                string fileName = file1.FileName;
                byte[] buffer = new byte[fileLength];
                file1.InputStream.Read(buffer, 0, fileLength);
                FileStream newFile;
                string strPath = @"F:\PatentDocument\Internship\" + txtInternshipID.Text.Trim() + @"\";
                if (!Directory.Exists(strPath))
                {
                    Directory.CreateDirectory(strPath);
                }
                if (!File.Exists(strPath + fileName))
                {
                    newFile = File.Open(strPath + fileName, FileMode.Create);
                    newFile.Write(buffer, 0, buffer.Length);
                    newFile.Close();
                }
                else
                {
                    ClientScript.RegisterStartupScript(GetType(), "Information", "<script>alert('This File exist in this Folder')</script>");
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
            fetchFiles(txtInternshipID.Text);
            btnFileClose_Click(sender, e);
        }
        catch (Exception ex)
        {
            ClientScript.RegisterStartupScript(GetType(), "Error", "<script>alert('" + ex.Message.ToString() + "')</script>");
            con.Close();
            return;
        }
    }
    protected void btnFileClose_Click(object sender, EventArgs e)
    {
        ClearFileUpload();
        panNewFile.Visible = false;
        btnUpload.Visible = true;
    }
    protected void btnFind_Click(object sender, EventArgs e)
    {
        txtInternshipID.Text = txtInternshipID.Text.ToUpper();
        if (FetchInternship(txtInternshipID.Text.Trim()) == false) return;
        txtInternshipID.Enabled = false;
        btnUpdate.Visible = (!User.IsInRole("View"));
        btnUpload.Visible = (!User.IsInRole("View"));
        fetchFiles(txtInternshipID.Text.Trim());
        con.Close();
        Tab1_Click(sender, e);
    }
    protected void btnClear_Click(object sender, EventArgs e)
    {
        txtInternshipID.Text = "";
        txtInternshipID.Enabled = true;
        txtName.Text = "";
        txtProgram.Text = "";
        txtRollNo.Text = "";
        txtCandidateDept.Text = "";
        txtCollege.Text = "";
        txtFaculty.Text = "";
        txtFacultyDept.Text = "";
        txtFacultyContact1.Text = "";
        txtFacultyContact2.Text = "";
        txtTitle.Text = "";
        txtCommAddress1.Text = "";
        txtCommAddress2.Text = "";
        txtCommCity.Text = "";
        txtCommPincode.Text = "";
        txtPermanentAddress1.Text = "";
        txtPermanentAddress2.Text = "";
        txtPermanentCity.Text = "";
        txtPermanentPincode.Text = "";
        txtPhoneNo.Text = "";
        txtMobileNo.Text = "";
        txtEmailID.Text = "";
        txtJoiningDt.Text = "";
        txtRelieveDt.Text = "";
        txtPresentationDt.Text = "";
        btnInternship.Visible = false;
        btnUpdate.Visible = false;
        panNewFile.Visible = false;
        btnUpload.Visible = false;
        ClearFileUpload();
        using (DataTable dt = new DataTable())
        {
            gvFileDetails.DataSource = dt;
            gvFileDetails.DataBind();
        }
        Tab1_Click(sender, e);
        txtInternshipID.Focus();
    }
    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        if (FetchInternship (txtInternshipID.Text.Trim()) == false) return;
        btnInternship.Visible = true;
        btnUpdate.Visible = false;
    }
    protected bool FetchInternship(string InternshipID)
    {
        con.ConnectionString = ConfigurationManager.ConnectionStrings["PATENTCN"].ConnectionString;
        System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-GB");
        string sql = "SELECT * FROM INTERNSHIP WHERE INTERNSHIPID LIKE '" + txtInternshipID.Text.Trim() + "'";
        con.Open();
        SqlCommand cmd = new SqlCommand();
        cmd.CommandText = sql;
        cmd.CommandType = CommandType.Text;
        cmd.Connection = con;
        SqlDataReader dr;
        dr = cmd.ExecuteReader();
        if (dr.Read())
        {
            if (!dr.IsDBNull(2)) txtName.Text = dr.GetString(2); else txtName.Text = "";
            if (!dr.IsDBNull(3)) txtRollNo.Text = dr.GetString(3); else txtRollNo.Text = "";
            if (!dr.IsDBNull(4)) txtProgram.Text = dr.GetString(4); else txtProgram.Text = "";
            if (!dr.IsDBNull(5)) txtCandidateDept.Text = dr.GetString(5); else txtCandidateDept.Text = "";
            if (!dr.IsDBNull(6)) txtCollege.Text = dr.GetString(6); else txtCollege .Text = "";
            if (!dr.IsDBNull(7)) txtFaculty.Text = dr.GetString(7); else txtFaculty.Text = "";
            if (!dr.IsDBNull(8)) txtFacultyDept.Text = dr.GetString(8); else txtFacultyDept.Text = "";
            if (!dr.IsDBNull(9)) txtFacultyContact1.Text = dr.GetString(9); else txtFacultyContact1.Text = "";
            if (!dr.IsDBNull(10)) txtFacultyContact2.Text = dr.GetString(10); else txtFacultyContact2.Text = "";
            if (!dr.IsDBNull(11)) txtTitle.Text = dr.GetString(11); else txtTitle .Text = "";
            if (!dr.IsDBNull(12)) txtCommAddress1.Text = dr.GetString(12); else txtCommAddress1.Text = "";
            if (!dr.IsDBNull(13)) txtCommAddress2.Text = dr.GetString(13); else txtCommAddress2.Text = "";
            if (!dr.IsDBNull(14)) txtCommCity.Text = dr.GetString(14); else txtCommCity.Text = "";
            if (!dr.IsDBNull(15)) txtCommPincode.Text = dr.GetString(15); else txtCommPincode.Text = "";
            if (!dr.IsDBNull(16)) txtPermanentAddress1.Text = dr.GetString(16); else txtPermanentAddress1.Text = "";
            if (!dr.IsDBNull(17)) txtPermanentAddress2.Text = dr.GetString(17); else txtPermanentAddress2.Text = "";
            if (!dr.IsDBNull(18)) txtPermanentCity.Text = dr.GetString(18); else txtPermanentCity.Text = "";
            if (!dr.IsDBNull(19)) txtPermanentPincode.Text = dr.GetString(19); else txtPermanentPincode.Text = "";
            if (!dr.IsDBNull(20)) txtPhoneNo.Text = dr.GetString(20); else txtPhoneNo.Text = "";
            if (!dr.IsDBNull(21)) txtMobileNo.Text = dr.GetString(21); else txtMobileNo.Text = "";
            if (!dr.IsDBNull(22)) txtEmailID.Text = dr.GetString(22); else txtEmailID.Text = "";
            if (!dr.IsDBNull(23)) txtJoiningDt.Text = dr.GetDateTime(23).ToShortDateString(); else txtJoiningDt.Text = "";
            if (!dr.IsDBNull(24)) txtRelieveDt.Text = dr.GetDateTime(24).ToShortDateString(); else txtRelieveDt.Text = "";
            if (!dr.IsDBNull(25)) txtPresentationDt.Text = dr.GetDateTime(25).ToShortDateString(); else txtPresentationDt.Text = "";            
        }
        else
        {
            ClientScript.RegisterStartupScript(GetType(), "Internship ID is not found", "<script>alert('Contract Number Is Not Found')</script>");
            dr.Close();
            con.Close();
            return false;
        }
        dr.Close();
        con.Close();
       return true;
    }

    protected void btnInternship_Click(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(txtInternshipID.Text.Trim()))
        {
            ClientScript.RegisterStartupScript(GetType(), "Internship ID", "<script>alert('Internship ID Can not be Empty')</script>");
            return;
        }
        System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-GB");
        con.ConnectionString = ConfigurationManager.ConnectionStrings["PATENTCN"].ConnectionString;
        con.Open();
        try
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "IntershipModify";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            SqlParameter pm1 = new SqlParameter();
            pm1.ParameterName = "@InternshipID";
            pm1.SourceColumn = "InternshipID";
            pm1.Value = txtInternshipID.Text.Trim();
            pm1.DbType = DbType.String;
            pm1.Direction = ParameterDirection.Input;

            SqlParameter pm3 = new SqlParameter();
            pm3.ParameterName = "@CandidateName";
            pm3.SourceColumn = "CandidateName";
            if (string.IsNullOrEmpty(txtName.Text.Trim())) pm3.Value = DBNull.Value; else pm3.Value = txtName.Text.Trim();
            pm3.DbType = DbType.String;
            pm3.Direction = ParameterDirection.Input;

            SqlParameter pm4 = new SqlParameter();
            pm4.ParameterName = "@RollNumber";
            pm4.SourceColumn = "RollNumber";
            if (string.IsNullOrEmpty(txtRollNo.Text.Trim())) pm4.Value = DBNull.Value; else pm4.Value = txtRollNo.Text.Trim();
            pm4.DbType = DbType.String;
            pm4.Direction = ParameterDirection.Input;

            SqlParameter pm5 = new SqlParameter();
            pm5.ParameterName = "@ProgramOrDegree";
            pm5.SourceColumn = "ProgramOrDegree";
            if (string.IsNullOrEmpty(txtProgram.Text.Trim())) pm5.Value = DBNull.Value; else pm5.Value = txtProgram.Text.Trim();
            pm5.DbType = DbType.String;
            pm5.Direction = ParameterDirection.Input;

            SqlParameter pm6 = new SqlParameter();
            pm6.ParameterName = "@Department";
            pm6.SourceColumn = "Department";
            if (string.IsNullOrEmpty(txtCandidateDept.Text.Trim())) pm6.Value = DBNull.Value; else pm6.Value = txtCandidateDept.Text.Trim();
            pm6.DbType = DbType.String;
            pm6.Direction = ParameterDirection.Input;

            SqlParameter pm7 = new SqlParameter();
            pm7.ParameterName = "@College";
            pm7.SourceColumn = "College";
            if (string.IsNullOrEmpty(txtCollege.Text.Trim())) pm7.Value = DBNull.Value; else pm7.Value = txtCollege.Text.Trim();
            pm7.DbType = DbType.String;
            pm7.Direction = ParameterDirection.Input;

            SqlParameter pm8 = new SqlParameter();
            pm8.ParameterName = "@FacultyName";
            pm8.SourceColumn = "FacultyName";
            if (string.IsNullOrEmpty(txtFaculty.Text.Trim())) pm8.Value = DBNull.Value; else pm8.Value = txtFaculty.Text.Trim();
            pm8.DbType = DbType.String;
            pm8.Direction = ParameterDirection.Input;

            SqlParameter pm9 = new SqlParameter();
            pm9.ParameterName = "@FacultyDepartment";
            pm9.SourceColumn = "FacultyDepartment";
            if (string.IsNullOrEmpty(txtFacultyDept.Text.Trim())) pm9.Value = DBNull.Value; else pm9.Value = txtFacultyDept.Text.Trim();
            pm9.DbType = DbType.String;
            pm9.Direction = ParameterDirection.Input;

            SqlParameter pm10 = new SqlParameter();
            pm10.ParameterName = "@FacultyInfo1";
            pm10.SourceColumn = "FacultyInfo1";
            if (string.IsNullOrEmpty(txtFacultyContact1.Text.Trim())) pm10.Value = DBNull.Value; else pm10.Value = txtFacultyContact1.Text.Trim();
            pm10.DbType = DbType.String;
            pm10.Direction = ParameterDirection.Input;

            SqlParameter pm11 = new SqlParameter();
            pm11.ParameterName = "@FacultyInfo2";
            pm11.SourceColumn = "FacultyInfo2";
            if (string.IsNullOrEmpty(txtFacultyContact2.Text.Trim())) pm11.Value = DBNull.Value; else pm11.Value = txtFacultyContact2.Text.Trim();
            pm11.DbType = DbType.String;
            pm11.Direction = ParameterDirection.Input;

            SqlParameter pm12 = new SqlParameter();
            pm12.ParameterName = "@Title";
            pm12.SourceColumn = "Title";
            if (string.IsNullOrEmpty(txtTitle.Text.Trim())) pm12.Value = DBNull.Value; else pm12.Value = txtTitle.Text.Trim();
            pm12.DbType = DbType.String;
            pm12.Direction = ParameterDirection.Input;

            SqlParameter pm13 = new SqlParameter();
            pm13.ParameterName = "@CommunicationAddress1";
            pm13.SourceColumn = "CommunicationAddress1";
            if (string.IsNullOrEmpty(txtCommAddress1.Text.Trim())) pm13.Value = DBNull.Value; else pm13.Value = txtCommAddress1.Text.Trim();
            pm13.DbType = DbType.String;
            pm13.Direction = ParameterDirection.Input;

            SqlParameter pm14 = new SqlParameter();
            pm14.ParameterName = "@CommunicationAddress2";
            pm14.SourceColumn = "CommunicationAddress2";
            if (string.IsNullOrEmpty(txtCommAddress2.Text.Trim())) pm14.Value = DBNull.Value; else pm14.Value = txtCommAddress2.Text.Trim();
            pm14.DbType = DbType.String;
            pm14.Direction = ParameterDirection.Input;

            SqlParameter pm15 = new SqlParameter();
            pm15.ParameterName = "@CommunicationCity";
            pm15.SourceColumn = "CommunicationCity";
            if (string.IsNullOrEmpty(txtCommCity.Text.Trim())) pm15.Value = DBNull.Value; else pm15.Value = txtCommCity.Text.Trim();
            pm15.DbType = DbType.String;
            pm15.Direction = ParameterDirection.Input;

            SqlParameter pm16 = new SqlParameter();
            pm16.ParameterName = "@CommunicationPincode";
            pm16.SourceColumn = "CommunicationPincode";
            if (string.IsNullOrEmpty(txtCommPincode.Text.Trim())) pm16.Value = DBNull.Value; else pm16.Value = txtCommPincode.Text.Trim();
            pm16.DbType = DbType.String;
            pm16.Direction = ParameterDirection.Input;

            SqlParameter pm17 = new SqlParameter();
            pm17.ParameterName = "@PermanentAddress1";
            pm17.SourceColumn = "PermanentAddress1";
            if (string.IsNullOrEmpty(txtPermanentAddress1.Text.Trim())) pm17.Value = DBNull.Value; else pm17.Value = txtPermanentAddress1.Text.Trim();
            pm17.DbType = DbType.String;
            pm17.Direction = ParameterDirection.Input;

            SqlParameter pm18 = new SqlParameter();
            pm18.ParameterName = "@PermanentAddress2";
            pm18.SourceColumn = "PermanentAddress2";
            if (string.IsNullOrEmpty(txtPermanentAddress2.Text.Trim())) pm18.Value = DBNull.Value; else pm18.Value = txtPermanentAddress2.Text.Trim();
            pm18.DbType = DbType.String;
            pm18.Direction = ParameterDirection.Input;

            SqlParameter pm19 = new SqlParameter();
            pm19.ParameterName = "@PermanentCity";
            pm19.SourceColumn = "PermanentCity";
            if (string.IsNullOrEmpty(txtPermanentCity.Text.Trim())) pm19.Value = DBNull.Value; else pm19.Value = txtPermanentCity.Text.Trim();
            pm19.DbType = DbType.String;
            pm19.Direction = ParameterDirection.Input;

            SqlParameter pm20 = new SqlParameter();
            pm20.ParameterName = "@PermanentPincode";
            pm20.SourceColumn = "PermanentPincode";
            if (string.IsNullOrEmpty(txtPermanentPincode.Text.Trim())) pm20.Value = DBNull.Value; else pm20.Value = txtPermanentPincode.Text.Trim();
            pm20.DbType = DbType.String;
            pm20.Direction = ParameterDirection.Input;

            SqlParameter pm21 = new SqlParameter();
            pm21.ParameterName = "@UserName";
            pm21.SourceColumn = "UserName";
            pm21.Value = Membership.GetUser().UserName.ToString();
            pm21.DbType = DbType.String;
            pm21.Direction = ParameterDirection.Input;

            SqlParameter pm22 = new SqlParameter();
            pm22.ParameterName = "@ContactPhoneNo";
            pm22.SourceColumn = "ContactPhoneNo";
            if (string.IsNullOrEmpty(txtPhoneNo.Text.Trim())) pm22.Value = DBNull.Value; else pm22.Value = txtPhoneNo.Text.Trim();
            pm22.DbType = DbType.String;
            pm22.Direction = ParameterDirection.Input;

            SqlParameter pm23 = new SqlParameter();
            pm23.ParameterName = "@ContactMobileNo";
            pm23.SourceColumn = "ContactMobileNo";
            if (string.IsNullOrEmpty(txtMobileNo.Text.Trim())) pm23.Value = DBNull.Value; else pm23.Value = txtMobileNo.Text.Trim();
            pm23.DbType = DbType.String;
            pm23.Direction = ParameterDirection.Input;

            SqlParameter pm24 = new SqlParameter();
            pm24.ParameterName = "@EmailID";
            pm24.SourceColumn = "EmailID";
            if (string.IsNullOrEmpty(txtEmailID.Text.Trim())) pm24.Value = DBNull.Value; else pm24.Value = txtEmailID.Text.Trim();
            pm24.DbType = DbType.String;
            pm24.Direction = ParameterDirection.Input;

            SqlParameter pm25 = new SqlParameter();
            pm25.ParameterName = "@JoiningDate";
            pm25.SourceColumn = "JoiningDate";
            if (txtJoiningDt.Text.Trim() == "")
                pm25.Value = DBNull.Value;
            else
            {
                DateTime JoiningDt;
                if (DateTime.TryParse(txtJoiningDt.Text.Trim(), out JoiningDt))
                    pm25.Value = txtJoiningDt.Text.Trim();
                else
                {
                    ClientScript.RegisterStartupScript(GetType(), "Joining Date", "<script>alert('Verify Joining Date')</script>");
                    return;
                }
            }
            pm25.DbType = DbType.String;
            pm25.Direction = ParameterDirection.Input;

            SqlParameter pm26 = new SqlParameter();
            pm26.ParameterName = "@RelievingDate";
            pm26.SourceColumn = "RelievingDate";
            if (txtRelieveDt.Text.Trim() == "")
                pm26.Value = DBNull.Value;
            else
            {
                DateTime RelieveDt;
                if (DateTime.TryParse(txtRelieveDt.Text.Trim(), out RelieveDt))
                    pm26.Value = txtRelieveDt.Text.Trim();
                else
                {
                    ClientScript.RegisterStartupScript(GetType(), "Relieving Date", "<script>alert('Verify Relieving Date')</script>");
                    return;
                }
            }
            pm26.DbType = DbType.String;
            pm26.Direction = ParameterDirection.Input;

            SqlParameter pm27 = new SqlParameter();
            pm27.ParameterName = "@PresentationDate";
            pm27.SourceColumn = "PresentationDate";
            if (txtPresentationDt.Text.Trim() == "")
                pm27.Value = DBNull.Value;
            else
            {
                DateTime PresentationDt;
                if (DateTime.TryParse(txtPresentationDt.Text.Trim(), out PresentationDt))
                    pm27.Value = txtPresentationDt.Text.Trim();
                else
                {
                    ClientScript.RegisterStartupScript(GetType(), "Presentation Date", "<script>alert('Verify Presentation Date')</script>");
                    return;
                }
            }
            pm27.DbType = DbType.String;
            pm27.Direction = ParameterDirection.Input;

            cmd.Parameters.Add(pm1);
            cmd.Parameters.Add(pm3);
            cmd.Parameters.Add(pm4);
            cmd.Parameters.Add(pm5);
            cmd.Parameters.Add(pm6);
            cmd.Parameters.Add(pm7);
            cmd.Parameters.Add(pm8);
            cmd.Parameters.Add(pm9);
            cmd.Parameters.Add(pm10);
            cmd.Parameters.Add(pm11);
            cmd.Parameters.Add(pm12);
            cmd.Parameters.Add(pm13);
            cmd.Parameters.Add(pm14);
            cmd.Parameters.Add(pm15);
            cmd.Parameters.Add(pm16);
            cmd.Parameters.Add(pm17);
            cmd.Parameters.Add(pm18);
            cmd.Parameters.Add(pm19);
            cmd.Parameters.Add(pm20);
            cmd.Parameters.Add(pm21);
            cmd.Parameters.Add(pm22);
            cmd.Parameters.Add(pm23);
            cmd.Parameters.Add(pm24);
            cmd.Parameters.Add(pm25);
            cmd.Parameters.Add(pm26);
            cmd.Parameters.Add(pm27);

            cmd.ExecuteNonQuery();

            ClientScript.RegisterStartupScript(GetType(), "success", "<script>alert('This Record successfully Modified')</script>");
            con.Close();
        }
        catch (Exception ex)
        {
            ClientScript.RegisterStartupScript(GetType(), "Error", "<script>alert('" + ex.Message.ToString() + "')</script>");
            con.Close();
            return;
        }        
    }
}
