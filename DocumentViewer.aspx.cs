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
using Ionic.Zip;

public partial class DocumentViewer : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection();
    string[] tColumn = new[] { "Fileno", "Upload Date", "Modified Date", "File Type" };
    string[] tColumnVal = new[] { "FileNo", "EntryDt", "ModifiedDt", "FileDescription" };
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!User.IsInRole("Intern"))
        {
            if (!this.IsPostBack)
            {
                /*SqlCommand cmd = new SqlCommand();
                con.ConnectionString = ConfigurationManager.ConnectionStrings["PATENTCN"].ConnectionString;
                string sql = "select FileDescription from patentfiledetails group by FileDescription order by FileDescription";
                con.Open();
                cmd.CommandText = sql;
                cmd.CommandType = CommandType.Text;
                cmd.Connection = con;
                SqlDataReader dr;
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    ddlValue.Items.Add(dr.GetString(0));
                }
                con.Close();*/
                if (User.IsInRole("Admin"))
                {
                    btnDownload.Enabled = true;
                    trFile.Visible = true;
                }
                else
                {
                    btnDownload.Enabled = false;
                    trFile.Visible = false;
                }
                lstColumn.Items.Clear();
                for (int i = 0; i <= tColumn.GetUpperBound(0); i++)
                {
                    lstColumn.Items.Add(new ListItem(tColumn[i], tColumnVal[i]));
                }
            }
        }
        else
        {
            Server.Transfer("Unauthorized.aspx");
        }
    }
    protected void btnReport_Click(object sender, EventArgs e)
    {
        if (txtFilter.Text.Trim() == "")
        {
            ClientScript.RegisterStartupScript(GetType(), "Information", "<script>alert('Please use Filter option')</script>");
            return;
        }
        try
        {
            SqlCommand cmd = new SqlCommand();
            System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-GB");
            con.ConnectionString = ConfigurationManager.ConnectionStrings["PATENTCN"].ConnectionString;
            string sql = "";
            sql = "select EntryDt,FileNo,FileDescription,ModifiedDt,Comments,FileName from patentfiledetails where " + txtFilter.Text.Trim() + " order by Fileno,EntryDt desc";
            con.Open();
            cmd.CommandText = sql;
            cmd.CommandType = CommandType.Text;
            cmd.Connection = con;
            SqlDataReader dr;
            dr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(dr);
            SearchResult.Visible = true;
            if (dt.Rows.Count > 25)
            {
                pageNumber.Visible = true;
            }
            else
            {
                pageNumber.Visible = false;
            }
            Session["DocumentView"] = dt;
            gvFileDetails.DataSource = dt;
            gvFileDetails.DataBind();

            con.Close();
        }
        catch (Exception ex)
        {
            ClientScript.RegisterStartupScript(GetType(), "Error", "<script>alert('" + ex.Message + "')</script>");
            con.Close();
        }
    }

    protected void btnClear_Click(object sender, EventArgs e)
    {
        txtValue.Text = "";
        txtFilter.Text = "";
        gvFileDetails.DataSource = null;
        gvFileDetails.DataBind();
        SearchResult.Visible = false;
        pageNumber.Visible = false;
        if (User.IsInRole("Admin"))
        {
            txtFileName.Text = "";
            chkGroup.Checked = false;
        }
    }
    protected void btnRemoveVal_Click(object sender, EventArgs e)
    {
        string filter = txtFilter.Text.Trim();
        if (filter != "")
        {
            int pos = filter.LastIndexOf("and");
            if (pos > 0)
            {
                filter = filter.Remove(pos);
            }
            txtFilter.Text = filter;

        }
    }
    protected void gvFileDetails_Sorting(object sender, GridViewSortEventArgs e)
    {
        DataTable dt = Session["DocumentView"] as DataTable;
        if (dt != null)
        {
            dt.DefaultView.Sort = e.SortExpression + " " + getSortDirection(e.SortExpression);

            gvFileDetails.DataSource = dt;
            gvFileDetails.DataBind();
        }
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
    protected void gvFileDetails_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvFileDetails.PageIndex = e.NewPageIndex;
        gvFileDetails.DataSource = Session["DocumentView"];
        gvFileDetails.DataBind();
    }
    protected void gvFileDetails_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        GridViewRow item = gvFileDetails.Rows[e.NewSelectedIndex];
        string Fileno = item.Cells[1].Text;
        LinkButton lbFileName = gvFileDetails.Rows[e.NewSelectedIndex].FindControl("lbtnEdit") as LinkButton;

        string strPath = "";
        string strFileName = "";
        strPath = @"F:\PatentDocument\" + Fileno.Trim() + @"\";
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
    protected void gvFileDetails_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        LinkButton lb = e.Row.FindControl("lbtnDelete") as LinkButton;
        if (lb != null)
        {
            if (User.IsInRole("Admin") || User.IsInRole("DocumentManage"))
            {
                lb.Visible = true;
            }
        }
        if (!User.IsInRole("Admin") && !User.IsInRole("DocumentManage"))
        {
            if (e.Row.RowType == DataControlRowType.DataRow || e.Row.RowType == DataControlRowType.Header)
                e.Row.Cells[6].Visible = false;
        }
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
            string fileNo = item.Cells[1].Text.Trim();
            string fileDescription = item.Cells[2].Text.Trim();
            LinkButton lb = (LinkButton)item.FindControl("lbtnEdit");
            string fileName = lb.Text.Trim();
            con.ConnectionString = ConfigurationManager.ConnectionStrings["PATENTCN"].ConnectionString;
            string sql = "delete from patentfiledetails where fileno = '" + fileNo.Trim() + "' and fileDescription = '" + fileDescription.Trim() + "' and fileName = '" + fileName.Trim() + "'";
            con.Open();
            trans = con.BeginTransaction();
            cmd.Transaction = trans;
            cmd.CommandText = sql;
            cmd.CommandType = CommandType.Text;
            cmd.Connection = con;
            cmd.ExecuteNonQuery();
            try
            {
                string sourceFile = @"F:\PatentDocument\" + fileNo.Trim() + @"\" + fileName.Trim();
                string targetPath = @"F:\PatentDocument\Delete Files\" + fileNo.Trim() + @"\";
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
        }
        catch (Exception ex)
        {
            ClientScript.RegisterStartupScript(GetType(), "Error", "<script>alert('" + ex.Message.ToString() + "')</script>");
            con.Close();
            return;
        }
        btnReport_Click(sender, e);
    }
    protected void btnDownload_Click(object sender, EventArgs e)
    {
        if (txtFilter.Text.Trim() == "")
        {
            ClientScript.RegisterStartupScript(GetType(), "Information", "<script>alert('Please use Filter option')</script>");
            return;
        }
        try
        {
            SqlCommand cmd = new SqlCommand();
            System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-GB");
            con.ConnectionString = ConfigurationManager.ConnectionStrings["PATENTCN"].ConnectionString;
            string sql = "";
            sql = "select FileNo,FileName from patentfiledetails where " + txtFilter.Text.Trim();
            con.Open();
            cmd.CommandText = sql;
            cmd.CommandType = CommandType.Text;
            cmd.Connection = con;
            SqlDataReader dr;
            dr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(dr);
            using (ZipFile zip = new ZipFile())
            {
                zip.AlternateEncodingUsage = ZipOption.AsNecessary;
                zip.AddDirectoryByName("files");
                foreach (DataRow drow in dt.Rows)
                {
                    string filepath = @"F:\PatentDocument\" + drow[0] + @"\" + drow[1];
                    if (chkGroup.Checked == true)
                        zip.AddFile(filepath, drow[0].ToString());
                    else
                        zip.AddFile(filepath, "files");
                }
                Response.Clear();
                Response.BufferOutput = false;
                string downFileName;
                if (txtFileName.Text.Trim() == "") downFileName = "PatentDocument"; else downFileName = txtFileName.Text.Trim();
                string zipName = string.Format("{1}_{0}.zip", DateTime.Now.ToString("yyyy-MMM-dd-hhmmss"), downFileName);
                Response.ContentType = "application/zip";
                Response.AppendHeader("Content-Disposition", "attachment;filename=" + zipName);
                zip.Save(Response.OutputStream);
                Response.End();
            }
            con.Close();

        }
        catch (Exception ex)
        {
            ClientScript.RegisterStartupScript(GetType(), "Error", "<script>alert('" + ex.Message + "')</script>");
            con.Close();
        }

    }
}
