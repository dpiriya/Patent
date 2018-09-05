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
public partial class Internship : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection();
    SqlConnection cn = new SqlConnection();
    SqlConnection cnp = new SqlConnection();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (User.Identity.IsAuthenticated)
        {
            lblInternshipID.Text = AssignID();            
        }
        else
        {
            Response.Redirect("Login.aspx");
        }
        imgBtnInsert.Visible = (!User.IsInRole("View"));
    }
    protected void imgBtnClear_Click(object sender, ImageClickEventArgs e)
    {
        lblInternshipID.Text = AssignID();
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
        txtFileDesc.Text = "";
        txtName.Focus();
    }
    protected string AssignID()
    {
        con.ConnectionString = ConfigurationManager.ConnectionStrings["PATENTCN"].ConnectionString;
        SqlCommand cmd3 = new SqlCommand();
        string sql3 = "select max(convert(int,substring(InternshipID,3,len(internshipid)-2))) from Internship";
        cmd3.CommandText = sql3;
        cmd3.Connection = con;
        cmd3.CommandType = CommandType.Text;
        con.Open();
        string InternID = Convert.ToString(cmd3.ExecuteScalar());
        con.Close();
        if (InternID == "")
            InternID = "IS" + "1"; else InternID = "IS" + Convert.ToString(Convert.ToInt32(InternID)+1);
        return InternID;
    }
    protected void imgBtnInsert_Click(object sender, ImageClickEventArgs e)
    {
        if (string.IsNullOrEmpty(lblInternshipID.Text.Trim()))
        {
            ClientScript.RegisterStartupScript(GetType(), "Internship ID", "<script>alert('Internship ID Can not be Empty')</script>");
            return;
        }
        if(txtFileDesc.Text.Trim()!="")
        {
            if (flInternship.HasFile == false)
            {
                ClientScript.RegisterStartupScript(GetType(), "Information", "<script>alert('Please select Uploading File')</script>");
                return;
            }
        }
        if (flInternship.HasFile == true)
        {
            if (txtFileDesc.Text.Trim() == "")
            {
                ClientScript.RegisterStartupScript(GetType(), "Information", "<script>alert('Uploading File description can not be empty')</script>");
                return;
            }        
        }
        System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-GB");
        cnp.ConnectionString = ConfigurationManager.ConnectionStrings["PATENTCN"].ConnectionString;
        SqlCommand cmd1 = new SqlCommand();
        string sql1 = "select InternshipID from Internship where InternshipID like '" +lblInternshipID.Text.Trim()+ "'";
        cmd1.CommandText = sql1;
        cmd1.CommandType = CommandType.Text;
        cmd1.Connection = cnp;
        cnp.Open();
        SqlDataReader dr;
        dr = cmd1.ExecuteReader();
        if (dr.Read())
        {
            ClientScript.RegisterStartupScript(GetType(), "Internship ID", "<script>alert('This Internship ID is exist in table')</script>");
            dr.Close();
            return;
        }
        dr.Close();
        SqlTransaction Trans;
        Trans = cnp.BeginTransaction();
        try
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Transaction = Trans;
            cmd.CommandText = "IntershipNew";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = cnp;
            SqlParameter pm1 = new SqlParameter();
            pm1.ParameterName = "@InternshipID";
            pm1.SourceColumn = "InternshipID";
            pm1.Value = lblInternshipID.Text.Trim();
            pm1.DbType = DbType.String;
            pm1.Direction = ParameterDirection.Input;

            SqlParameter pm2 = new SqlParameter();
            pm2.ParameterName = "@EntryDt";
            pm2.SourceColumn = "EntryDt";
            pm2.Value = DateTime.Now.Date.ToShortDateString();
            pm2.DbType = DbType.String;
            pm2.Direction = ParameterDirection.Input;

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
            cmd.Parameters.Add(pm2);
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
            if (txtFileDesc.Text.Trim() != "" && flInternship.HasFile == true)
            {
                try
                {
                    SqlCommand cmd2 = new SqlCommand();
                    string sql2 = "insert into InternshipFiles (EntryDt,InternshipID,FileDescription,FileName,UserName) values (case when @EntryDt='' then null else convert(smalldatetime,@EntryDt,103) end,@InternshipID,@FileDescription,@FileName,@UserName)";
                    cmd2.CommandText = sql2;
                    cmd2.CommandType = CommandType.Text;
                    cmd2.Connection = cnp;
                    cmd2.Transaction = Trans;
                    SqlParameter pmf1 = new SqlParameter();
                    pmf1.ParameterName = "@EntryDt";
                    pmf1.SourceColumn = "EntryDt";
                    pmf1.Value = DateTime.Now.ToShortDateString();
                    pmf1.DbType = DbType.String;
                    pmf1.Direction = ParameterDirection.Input;

                    SqlParameter pmf2 = new SqlParameter();
                    pmf2.ParameterName = "@InternshipID";
                    pmf2.SourceColumn = "InternshipID";
                    pmf2.Value = lblInternshipID.Text.Trim();
                    pmf2.DbType = DbType.String;
                    pmf2.Direction = ParameterDirection.Input;

                    SqlParameter pmf3 = new SqlParameter();
                    pmf3.ParameterName = "@FileDescription";
                    pmf3.SourceColumn = "FileDescription";
                    pmf3.Value = txtFileDesc.Text.Trim();
                    pmf3.DbType = DbType.String;
                    pmf3.Direction = ParameterDirection.Input;

                    string fname = flInternship.FileName;
                    SqlParameter pmf4 = new SqlParameter();
                    pmf4.ParameterName = "@FileName";
                    pmf4.SourceColumn = "FileName";
                    pmf4.Value = fname;
                    pmf4.DbType = DbType.String;
                    pmf4.Direction = ParameterDirection.Input;

                    SqlParameter pmf5 = new SqlParameter();
                    pmf5.ParameterName = "@UserName";
                    pmf5.SourceColumn = "UserName";
                    pmf5.Value = Membership.GetUser().UserName.ToString();
                    pmf5.DbType = DbType.String;
                    pmf5.Direction = ParameterDirection.Input;

                    cmd2.Parameters.Add(pmf1);
                    cmd2.Parameters.Add(pmf2);
                    cmd2.Parameters.Add(pmf3);
                    cmd2.Parameters.Add(pmf4);
                    cmd2.Parameters.Add(pmf5);
                    cmd2.ExecuteNonQuery();

                    try
                    {
                        HttpPostedFile file1 = flInternship.PostedFile;
                        Int32 fileLength = file1.ContentLength;
                        string fileName = file1.FileName;
                        byte[] buffer = new byte[fileLength];
                        file1.InputStream.Read(buffer, 0, fileLength);
                        FileStream newFile;
                        string strPath = @"F:\PatentDocument\Internship\" + lblInternshipID.Text.Trim() + @"\";
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
                            cnp.Close();
                            return;
                        }
                    }
                    catch (Exception ex)
                    {
                        ClientScript.RegisterStartupScript(GetType(), "Error", "<script>alert('" + ex.Message.ToString() + "')</script>");
                        Trans.Rollback();
                        cnp.Close();
                        return;
                    }
                    ClientScript.RegisterStartupScript(GetType(), "Success", "<script>alert('This Record successfully added')</script>");
                    Trans.Commit();
                    cnp.Close();
                }
                catch (Exception ex)
                {
                    ClientScript.RegisterStartupScript(GetType(), "Error", "<script>alert('" + ex.Message.ToString() + "')</script>");
                    Trans.Rollback();
                    cnp.Close();
                    return;
                }
            }
            else
            {
                Trans.Commit();
                ClientScript.RegisterStartupScript(GetType(), "success", "<script>alert('This Record successfully Added')</script>");
                cnp.Close();
            }
        }
        catch (Exception ex)
        {
            Trans.Rollback();
            ClientScript.RegisterStartupScript(GetType(), "Error", "<script>alert('" + ex.Message.ToString() + "')</script>");
            cnp.Close();
            return;
        }
        imgBtnClear_Click(sender, e);
    }
}
