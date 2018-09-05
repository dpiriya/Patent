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

public partial class MarketingSearch : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.IsPostBack)
        {
            con.ConnectionString = ConfigurationManager.ConnectionStrings["PATENTCN"].ConnectionString;
            string sql = "select column_name from information_schema.columns where table_name ='MarketingProject'";
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = sql;
            cmd.CommandType = CommandType.Text;
            cmd.Connection = con;
            con.Open();
            SqlDataReader dr;
            dr=cmd.ExecuteReader();
            ddlSearch.DataTextField = "column_name";
            ddlSearch.DataSource = dr;
            ddlSearch.DataBind();
            ddlSearch.Items.Insert(0,"All");
            dr.Close();
            sql = "select distinct(MktgGroup) from marketingproject where MktgGroup is not null order by MktgGroup";
            cmd.CommandText = sql;
            cmd.CommandType = CommandType.Text;
            cmd.Connection = con;
            dr = cmd.ExecuteReader();
            ddlSearchVal.DataTextField = "MktgGroup";
            ddlSearchVal.DataValueField = "MktgGroup";
            ddlSearchVal.DataSource = dr;
            ddlSearchVal.DataBind();
            dr.Close();
            con.Close();
        }
    }
   protected void gvMarketing_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {

        GridViewRow row = (GridViewRow)gvMarketing.Rows[e.NewSelectedIndex];
        string pNo = row.Cells[1].Text;
        Page.ClientScript.RegisterStartupScript(this.GetType(), "Marketing_window", string.Format("void(window.open('{0}','child_Marketing','menubar=0,toolbar=0,resizable=1,scrollbars=1'));", "rptView.aspx?id=MarketingProject&ProjNo=" + pNo), true);
    }
    protected void ibtnSearch_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            if (ddlSearch.SelectedIndex == -1)
            {
                gvMarketing.DataSource = null;
                gvMarketing.DataBind();
                return;
            }
            if (ddlSearch.SelectedIndex != 0 && ddlSearch.SelectedIndex != 5)
            {
                if (txtSearch.Text.Trim() == "")
                {
                    gvMarketing.DataSource = null;
                    gvMarketing.DataBind();
                    return;
                }
            }
            string filter = "";
            if (ddlSearch.SelectedIndex == 1)
            {
                filter = "convert(varchar(12),entrydt,101) = convert(smalldatetime,'" + txtSearch.Text.Trim() + "',103)";
            }
            else if (ddlSearch.SelectedIndex == 2 && txtSearch.Text.Trim() != "")
            {
                filter = "MktgProjectNo = '" + txtSearch.Text.Trim() + "'";
            }
            else if (ddlSearch.SelectedIndex == 3 && txtSearch.Text.Trim() != "")
            {
                filter = "MktgTitle like '%" + txtSearch.Text.Trim() + "%'";
            }
            else if (ddlSearch.SelectedIndex == 4 && txtSearch.Text.Trim() != "")
            {
                filter = "MktgCompany like '%" + txtSearch.Text.Trim() + "%'";
            }
            else if (ddlSearch.SelectedIndex == 5)
            {
                filter = "MktgGroup like '%" + ddlSearchVal.Text.Trim() + "%'";
            }
            else if (ddlSearch.SelectedIndex == 6 && txtSearch.Text.Trim() != "")
            {
                filter = "ProjectValue like =" + txtSearch.Text.Trim();
            }
            else if (ddlSearch.SelectedIndex == 7 && txtSearch.Text.Trim() != "")
            {
                filter = "ValueRealization =" + txtSearch.Text.Trim();
            }
            else if (ddlSearch.SelectedIndex == 8 && txtSearch.Text.Trim() != "")
            {
                filter = "Status like '%" + txtSearch.Text.Trim() + "%'";
            }
            if (filter != "")
            {
                filter = " Where " + filter;
            }
            string sql = "select EntryDt,MktgProjectNo,MktgTitle,MktgCompany,MktgGroup,ProjectValue,ValueRealization,Status from marketingproject" + filter + " order by convert(int,substring(Mktgprojectno,3,len(Mktgprojectno)))";
            con.ConnectionString = ConfigurationManager.ConnectionStrings["PATENTCN"].ConnectionString;
            SqlDataAdapter sda = new SqlDataAdapter(sql, con);
            DataSet ds = new DataSet();
            sda.Fill(ds);
            SearchResult.Visible = true;
            if (ds.Tables[0].Rows.Count > 10)
            {
                pageNumber.Visible = true;
            }
            else
            {
                pageNumber.Visible = false;
            }
            Session["MarketSearch"]=ds.Tables[0];
            gvMarketing.DataSource = ds.Tables[0];
            gvMarketing.DataBind();
        }
        catch (Exception ex)
        {
            ClientScript.RegisterStartupScript(GetType(), "Error", "<script>alert('" + ex.Message.ToString() + "')</script>");
            gvMarketing.DataSource = null;
            gvMarketing.DataBind();
            return;
        }
    }
    protected void gvMarketing_RowDataBound(object sender, GridViewRowEventArgs e)
    {
       
    }

    void lbView_Click(object sender, EventArgs e)
    {
       
    }
    
    protected void gvMarketing_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        GridViewRow row= (GridViewRow) gvMarketing.Rows[e.RowIndex];
        string pNo = row.Cells[1].Text;
        Page.ClientScript.RegisterStartupScript( this.GetType(), "Market_IDF", string.Format("child=(window.open('{0}','child_Payment','menubar=0,toolbar=0,resizable=1,width=600,height=900,scrollbars=1'));", "MarketIdfList.aspx?projNo=" + pNo), true);       
    }
    protected void ibtnPrinter_Click(object sender, ImageClickEventArgs e)
    {
        if (ddlSearch.SelectedIndex != -1)
        {

            System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-GB");
            /*string filter = "";
            if (ddlSearch.SelectedIndex == 1)
            {
                filter = "convert(varchar(12),entrydt,101) = convert(smalldatetime,'" + txtSearch.Text.Trim() + "',103)";
            }
            else if (ddlSearch.SelectedIndex == 2 && txtSearch.Text.Trim() != "")
            {
                filter = "MktgProjectNo = '" + txtSearch.Text.Trim() + "'";
            }
            else if (ddlSearch.SelectedIndex == 3 && txtSearch.Text.Trim() != "")
            {
                filter = "MktgTitle like '%" + txtSearch.Text.Trim() + "%'";
            }
            else if (ddlSearch.SelectedIndex == 4 && txtSearch.Text.Trim() != "")
            {
                filter = "MktgCompany like '%" + txtSearch.Text.Trim() + "%'";
            }
            else if (ddlSearch.SelectedIndex == 5 && txtSearch.Text.Trim() != "")
            {
                filter = "MktgGroup like '%" + txtSearch.Text.Trim() + "%'";
            }
            else if (ddlSearch.SelectedIndex == 6 && txtSearch.Text.Trim() != "")
            {
                filter = "ProjectValue like =" + txtSearch.Text.Trim();
            }
            else if (ddlSearch.SelectedIndex == 7 && txtSearch.Text.Trim() != "")
            {
                filter = "ValueRealization =" + txtSearch.Text.Trim();
            }
            else if (ddlSearch.SelectedIndex == 8 && txtSearch.Text.Trim() != "")
            {
                filter = "Status like '%" + txtSearch.Text.Trim() + "%'";
            }
            if (filter != "")
            {
                filter = " Where " + filter;
            }
            string sql = "select EntryDt,MktgProjectNo,MktgTitle,MktgCompany,MktgGroup,ProjectValue,ValueRealization,Status from marketingproject" + filter + " order by convert(int,substring(Mktgprojectno,3,len(Mktgprojectno)))";
            con.ConnectionString = ConfigurationManager.ConnectionStrings["PATENTCN"].ConnectionString;
            SqlDataAdapter sda = new SqlDataAdapter(sql, con);
            DataSet ds = new DataSet();
            sda.Fill(ds);*/

            DataTable dt = Session["MarketSearch"] as DataTable;
            if (dt != null)
            {
                string sortExpression= ViewState["sortExpression"] as string;
                string SortDirection=ViewState["sortDirection"] as string;
                if (sortExpression == null) sortExpression="MktgProjectNo";
                if (SortDirection == null) SortDirection="Asc";
                DataView dv = dt.DefaultView;
                dv.Sort = sortExpression + " " + SortDirection;
                dt = dv.ToTable();
            }
            else
            {
                return;
            }
            if (dt.Rows.Count > 0)
            {
                HttpContext.Current.Response.AppendHeader("Content-Type", "application/pdf");
                HttpContext.Current.Response.AppendHeader("Content-disposition", "attachment; filename=MarketSearch"+DateTime.Now.ToShortDateString()+".pdf");
                using (Document doc = new Document(PageSize.A4.Rotate()))
                {
                    PdfWriter.GetInstance(doc, Response.OutputStream);
                    doc.Open();
                    string tTitle = "Marketing Search List";
                    doc.Add(new Paragraph(new Phrase(tTitle, new Font(Font.FontFamily.TIMES_ROMAN, 14, 1))));
                    //GeneratePdf gp = new GeneratePdf();


                    string[] strTitle = new[] { "Entry Date", "Mktg ProjectNo", "Mktg Title", "Mktg Company","Mktg Group","Project Value","Value Realization","Status"};
                    PdfPTable table = MakePdfHorizontal(dt, 8, "Marketing Search List", strTitle);
                    doc.Add(table);
                }
                HttpContext.Current.Response.End();
            }
        }
        /*}
        catch (Exception ex)
        {
            Response.Write("<script language='javascript'>alert('"+ex.Message+"');</script>");
        } */   
    }

    public PdfPTable MakePdfHorizontal(DataTable dt, int numCol, string subTitle, string[] strTitle)
    {
        System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-GB");
        PdfPTable table = new PdfPTable(numCol);
        //actual width of table in points
        table.TotalWidth = 780f;
        //fix the absolute width of the table
        table.LockedWidth = true;
        table.HeaderRows = 1;
        //relative col widths in proportions - 1/3 and 2/3
        float[] widths = new float[] { 1f,0.8f,3f,2.2f,0.9f,0.8f,0.9f,2f};
        table.SetWidths(widths);
        table.HorizontalAlignment = 0;
        //leave a gap before and after the table
        table.SpacingBefore = 20f; 
        table.SpacingAfter = 30f;

        for (int i=0; i<=strTitle.GetUpperBound(0); i++)
        {
            PdfPCell cell1 = new PdfPCell(new Phrase(strTitle[i], new Font(Font.FontFamily.TIMES_ROMAN, 12)));
            cell1.Border = Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER | Rectangle.RIGHT_BORDER;
            cell1.BorderColor = BaseColor.BLACK;
            cell1.BackgroundColor = BaseColor.LIGHT_GRAY;
            cell1.HorizontalAlignment = Element.ALIGN_CENTER;
            cell1.VerticalAlignment = Element.ALIGN_MIDDLE;
            table.AddCell(cell1);
        }
        foreach (DataRow dr in dt.Rows)
        {
            foreach (DataColumn dc in dt.Columns)
            {
                if (dc.DataType == typeof(DateTime)) table.AddCell(Convert.ToDateTime(dr[dc]).ToShortDateString()); else table.AddCell(dr[dc].ToString());               
            }
        }
        return table;
    }
    protected void gvMarketing_Sorting(object sender, GridViewSortEventArgs e)
    {
        DataTable dt = Session["MarketSearch"] as DataTable;
        if (dt != null)
        {
            dt.DefaultView.Sort = e.SortExpression + " " + getSortDirection(e.SortExpression);

            gvMarketing.DataSource = dt;
            gvMarketing.DataBind();
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
    protected void gvMarketing_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvMarketing.PageIndex = e.NewPageIndex;
        gvMarketing.DataSource = Session["MarketSearch"];
        gvMarketing.DataBind();
    }
    protected void ddlSearch_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlSearch.SelectedIndex == 5)
        {
            txtSearch.Text = "";
            txtSearch.Visible = false;
            ddlSearchVal.Visible = true;
        }
        else
        {
            txtSearch.Visible = true;
            txtSearch.Text = "";
            ddlSearchVal.Visible = false;
        }
    }    
}
