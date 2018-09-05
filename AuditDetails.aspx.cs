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

public partial class AuditDetails : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.IsPostBack)
        {
            con.ConnectionString = ConfigurationManager.ConnectionStrings["PATENTCN"].ConnectionString;
            string sql = "select column_name from information_schema.columns where table_name ='UserAuditDetails'";
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = sql;
            cmd.CommandType = CommandType.Text;
            cmd.Connection = con;
            con.Open();
            SqlDataReader dr;
            dr = cmd.ExecuteReader();
            ddlSearch.DataTextField = "column_name";
            ddlSearch.DataSource = dr;
            ddlSearch.DataBind();
            ddlSearch.Items.Insert(0, "All");
            con.Close();
        }
    }
    protected void ibtnPrinter_Click(object sender, ImageClickEventArgs e)
    {

    }
    protected void ibtnSearch_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            if (ddlSearch.SelectedIndex == -1)
            {
                gvAudit.DataSource = null;
                gvAudit.DataBind();
                return;
            }
            if (ddlSearch.SelectedIndex != 0)
            {
                if (txtSearch.Text.Trim() == "")
                {
                    gvAudit.DataSource = null;
                    gvAudit.DataBind();
                    return;
                }
            }
            string filter = "";
            if (ddlSearch.SelectedIndex == 1)
            {
                filter = "convert(varchar(12),ModifyDt,101) = convert(smalldatetime,'" + txtSearch.Text.Trim() + "',103)";
            }
            else if (ddlSearch.SelectedIndex == 2 && txtSearch.Text.Trim() != "")
            {
                filter = "TableName  like '%" + txtSearch.Text.Trim() + "%'";
            }
            else if (ddlSearch.SelectedIndex == 3 && txtSearch.Text.Trim() != "")
            {
                filter = "ColumnName like '%" + txtSearch.Text.Trim() + "%'";
            }
            else if (ddlSearch.SelectedIndex == 4 && txtSearch.Text.Trim() != "")
            {
                filter = "FileNo like '%" + txtSearch.Text.Trim() + "%'";
            }
            else if (ddlSearch.SelectedIndex == 5 && txtSearch.Text.Trim() != "")
            {
                filter = "RowID like '%" + txtSearch.Text.Trim() + "%'";
            }
            else if (ddlSearch.SelectedIndex == 6 && txtSearch.Text.Trim() != "")
            {
                filter = "PreviousValue like '%" + txtSearch.Text.Trim() +"%'";
            }
            else if (ddlSearch.SelectedIndex == 7 && txtSearch.Text.Trim() != "")
            {
                filter = "CurrentValue like '%" + txtSearch.Text.Trim()+"%'";
            }
            else if (ddlSearch.SelectedIndex == 8 && txtSearch.Text.Trim() != "")
            {
                filter = "UserName like '%" + txtSearch.Text.Trim() + "%'";
            }
            if (filter != "")
            {
                filter = " Where " + filter;
            }
            string sql = "select ModifyDt,TableName,ColumnName,FileNo,RowID,PreviousValue,CurrentValue,UserName from UserAuditDetails" + filter + " order by ModifyDt desc";
            con.ConnectionString = ConfigurationManager.ConnectionStrings["PATENTCN"].ConnectionString;
            SqlDataAdapter sda = new SqlDataAdapter(sql, con);
            DataSet ds = new DataSet();
            sda.Fill(ds);
            SearchResult.Visible = true;
            if (ds.Tables[0].Rows.Count > 15)
            {
                pageNumber.Visible = true;
            }
            else
            {
                pageNumber.Visible = false;
            }
            Session["UserAudit"] = ds.Tables[0];
            gvAudit.DataSource = ds.Tables[0];
            gvAudit.DataBind();
        }
        catch (Exception ex)
        {
            ClientScript.RegisterStartupScript(GetType(), "Error", "<script>alert('" + ex.Message.ToString() + "')</script>");
            gvAudit.DataSource = null;
            gvAudit.DataBind();
            return;
        }
    }
    protected void gvAudit_Sorting(object sender, GridViewSortEventArgs e)
    {
        DataTable dt = Session["UserAudit"] as DataTable;
        if (dt != null)
        {
            dt.DefaultView.Sort = e.SortExpression + " " + getSortDirection(e.SortExpression);

            gvAudit.DataSource = dt;
            gvAudit.DataBind();
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
    protected void gvAudit_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvAudit.PageIndex = e.NewPageIndex;
        gvAudit.DataSource = Session["UserAudit"];
        gvAudit.DataBind();
    }
}
