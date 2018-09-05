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
using System.Globalization;

public partial class CompanySearch2 : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.IsPostBack)
        {

            string[] searchItem = new string[] { "All", "EntryDt", "CompanyID", "CompanyName", "Address1", "Address2", "City", "IndustryType", "Business Area", "Products", "Contact Person" };

            for (int i = 0; i <= searchItem.GetUpperBound(0); i++)
            {
                ddlSearch.Items.Add(searchItem[i]);                
            }
            
        }
    }
    protected void ibtnSearch_Click(object sender, ImageClickEventArgs e)
    {
        Session["SearchQuery"] = "";
        Session["SearchForLetter"] = "";
        string sql1="";
        string sql3="";            
        try
        {
            if (ddlSearch.SelectedIndex == -1)
            {
                gvCompany.DataSource = null;
                gvCompany.DataBind();
                return;
            }
            if (ddlSearch.SelectedIndex != 0)
            {
                if (txtSearch.Text.Trim() == "" && ddlSearch.SelectedIndex != 7 && ddlSearch.SelectedIndex != 8)
                {
                    gvCompany.DataSource = null;
                    gvCompany.DataBind();
                    return;
                }
            }
            string filter = "",filter1 = "";

            if (ddlSearch.SelectedIndex == 1)
            {
                filter = "convert(varchar(12),entrydt,101) = convert(smalldatetime,'" + txtSearch.Text.Trim() + "',103)";
                filter1 = "convert(varchar(12),entrydt,101) = convert(smalldatetime,'" + txtSearch.Text.Trim() + "',103)";
            }
            else if (ddlSearch.SelectedIndex == 2 && txtSearch.Text.Trim() != "")
            {
                filter = "CompanyID = '" + txtSearch.Text.Trim() + "'" + " order by convert(int,substring(companyID,2,len(companyID)))";
                filter1 = "CompanyID = '" + txtSearch.Text.Trim() + "'";
            }
            else if (ddlSearch.SelectedIndex == 3 && txtSearch.Text.Trim() != "")
            {
                filter = "CompanyName like '%" + txtSearch.Text.Trim() + "%'";
                filter1 = "CompanyName like '%" + txtSearch.Text.Trim() + "%'";
            }
            else if (ddlSearch.SelectedIndex == 4 && txtSearch.Text.Trim() != "")
            {
                filter = "Address1 like '%" + txtSearch.Text.Trim() + "%'";
                filter1 = "Address1 like '%" + txtSearch.Text.Trim() + "%'";
            }
            else if (ddlSearch.SelectedIndex == 5 && txtSearch.Text.Trim() != "")
            {
                filter = "Address2 like '%" + txtSearch.Text.Trim() + "%'";
                filter1 = "Address2 like '%" + txtSearch.Text.Trim() + "%'";
            }
            else if (ddlSearch.SelectedIndex == 6 && txtSearch.Text.Trim() != "")
            {
                filter = "City like '%" + txtSearch.Text.Trim() + "%'";
                filter1 = "City like '%" + txtSearch.Text.Trim() + "%'"; 
            }
            else if (ddlSearch.SelectedIndex == 7)
            {
                string SearchText="";
                foreach (ListItem item in ddcboxSearchVal.Items)
                {
                    if (item.Selected) if (SearchText == "") SearchText = "'" + item.Text + "'"; else SearchText = SearchText + ",'" + item.Text + "'";
                }
                filter = "IndustryType in (" + SearchText + ")" + " order by IndustryType asc" ;
                filter1 = "IndustryType in (" + SearchText + ")";
            }
            else if (ddlSearch.SelectedIndex == 8)
            {
                string SearchText = "";
                foreach (ListItem item in ddcboxSearchVal.Items)
                {
                    if (item.Selected) if (SearchText == "") SearchText = "'" + item.Text + "'"; else SearchText = SearchText + ",'" + item.Text + "'";
                }
                filter = "companyID in (select companyID from companybusinessarea where businessArea in (" + SearchText + "))";
                filter1 = "companyID in (select companyID from companybusinessarea where businessArea in (" + SearchText + "))";
                sql1 = "select M.*,b.BusinessArea,b.Products from CompanyMaster M inner join  CompanyBusinessArea B on m.CompanyID=b.CompanyID and b.BusinessArea in (" + SearchText + ") order by convert(int,substring(M.companyID,2,len(M.companyID)))";
            }
            else if (ddlSearch.SelectedIndex == 9 && txtSearch.Text.Trim() != "")
            {
                filter = "companyID in (select companyID from companybusinessarea where products like '%" + txtSearch.Text.Trim() + "%')";
                filter1 = "companyID in (select companyID from companybusinessarea where products like '%" + txtSearch.Text.Trim() + "%')";
                sql1 = "select M.*,b.BusinessArea,b.Products from CompanyMaster M inner join  CompanyBusinessArea B on m.CompanyID=b.CompanyID and b.products like '%" + txtSearch.Text.Trim() + "%' order by convert(int,substring(M.companyID,2,len(M.companyID)))";
            }
            else if (ddlSearch.SelectedIndex == 10 && txtSearch.Text.Trim() != "")
            {
                filter = "companyID in (select companyID from companyContactDetails where contactPersonName like '%" + txtSearch.Text.Trim() + "%')";
                filter1 = "companyID in (select companyID from companyContactDetails where contactPersonName like '%" + txtSearch.Text.Trim() + "%')";
                sql1 = "select M.*, C.ContactPersonName, C.BusinessArea, C.Address1, C.Address2, C.PhoneNo1, C.PhoneNo2, C.EmailID from CompanyMaster M inner join  CompanyContactDetails C on m.CompanyID=C.CompanyID and C.contactPersonName like '%" + txtSearch.Text.Trim() + "%' order by convert(int,substring(M.companyID,2,len(M.companyID)))";
            }
            if (filter != "")
            {
                filter = " Where " + filter;
                if (ddlSearch.SelectedIndex == 8 || ddlSearch.SelectedIndex == 9 || ddlSearch.SelectedIndex == 10)
                    filter1 = " and C." + filter1;
                else
                    filter1 = " Where " + filter1;
                
            }
            string sql = "select EntryDt,CompanyID,CompanyName,Address1,Address2,City,Pincode,Phone1,Phone2,FaxNo,EmailID1,EmailID2,IndustryType from CompanyMaster" + filter ;
            if (ddlSearch.SelectedIndex != 8 && ddlSearch.SelectedIndex != 9 && ddlSearch.SelectedIndex != 10)
            {
                sql3 = "select 'false' as Selected, M.CompanyID,M.CompanyName,M.IndustryType,C.ContactPersonName,C.Address1,C.Address2 from CompanyMaster M INNER JOIN CompanyContactDetails C ON M.CompanyID=C.CompanyID and C.CompanyID in (select CompanyID from CompanyMaster" + filter1 + ") order by C.CompanyID,C.SlNo";
                Session["SearchQuery"] = sql;                
                Session["SearchForLetter"] = sql3;
            }
            else
            {
                sql3 = "select 'false' as Selected, M.CompanyID,M.CompanyName,M.IndustryType,C.ContactPersonName,C.Address1,C.Address2 from CompanyMaster M INNER JOIN CompanyContactDetails C ON M.CompanyID=C.CompanyID" + filter1 + " order by C.CompanyID,C.SlNo" ;
                Session["SearchQuery"] = sql1;
                Session["SearchForLetter"] = sql3;
            }
            con.ConnectionString = ConfigurationManager.ConnectionStrings["PATENTCN"].ConnectionString;
            SqlDataAdapter sda = new SqlDataAdapter(sql, con);
            DataSet ds = new DataSet();
            sda.Fill(ds);
            Session["companyResult"] = ds.Tables[0];
            gvCompany.DataSource = ds.Tables[0];
            gvCompany.DataBind();
        }
        catch (Exception ex)
        {
            ClientScript.RegisterStartupScript(GetType(), "Error", "<script>alert('" + ex.Message.ToString() + "')</script>");
            gvCompany.DataSource = null;
            gvCompany.DataBind();
            return;
        }
    }
    protected void ibtnPrinter_Click(object sender, ImageClickEventArgs e)
    {
        string sql = "";
        sql = Convert.ToString(Session["SearchQuery"]);
        if (string.IsNullOrEmpty(sql)) return;
        System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-GB");
        SqlConnection cnp = new SqlConnection();
        cnp.ConnectionString = ConfigurationManager.ConnectionStrings["PATENTCN"].ConnectionString;
        SqlCommand cmd = new SqlCommand();
        SqlDataReader dr;
        cmd.CommandType = CommandType.Text;
        cmd.Connection = cnp;
        cmd.CommandText = sql;
        cnp.Open();
        dr = cmd.ExecuteReader();
        DataTable dt = new DataTable();
        dt.Load(dr);
        GenerateExcel ge = new GenerateExcel();
        ge.MakeExcel(dt, "CompanySearch");
        cnp.Close();    
    }
    protected void gvCompany_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        //LinkButton lb = (LinkButton) gvCompany.NamingContainer;

        /* GridViewRow gvr = e.Row;
         LinkButton lb = e.Row.FindControl("lbtnView") as LinkButton;
         LinkButton lb1 = e.Row.FindControl("lbtnPrint") as LinkButton;
         if (lb != null)
         {
             ScriptManager.GetCurrent(Page).RegisterPostBackControl(lb);
         }
         if (lb1 != null)
         {
             ScriptManager.GetCurrent(Page).RegisterPostBackControl(lb1);
         }*/
    }
    protected void gvCompany_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        GridViewRow row = (GridViewRow)gvCompany.Rows[e.RowIndex];
        string IdNo = row.Cells[1].Text;
        Page.ClientScript.RegisterStartupScript(this.GetType(), "Business_Products", string.Format("child=(window.open('{0}','child_Payment','menubar=0,toolbar=0,resizable=1,width=600,height=900,scrollbars=1'));", "BusinessProducts.aspx?companyId=" + IdNo), true);
    }
    protected void gvCompany_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        
    }
    protected void gvCompany_Sorting(object sender, GridViewSortEventArgs e)
    {
        DataTable dt = Session["companyResult"] as DataTable;
        if (dt != null)
        {
            dt.DefaultView.Sort = e.SortExpression + " " + getSortDirection(e.SortExpression);

            gvCompany.DataSource = dt;
            gvCompany.DataBind();
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
    protected void gvCompany_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvCompany.PageIndex = e.NewPageIndex;
        gvCompany.DataSource = Session["companyResult"];
        gvCompany.DataBind();
    }
    protected void gvCompany_DataBinding(object sender, EventArgs e)
    {

    }
    protected void ddlSearch_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlSearch.SelectedIndex == 7 || ddlSearch.SelectedIndex == 8)
        {
            txtSearch.Text = "";
            txtSearch.Visible = false;
            ddlSearchVal.Visible = false;
            ddcboxSearchVal.Visible = true;
            ddcboxSearchVal.Items.Clear();

            SqlCommand cmd = new SqlCommand();
            con.ConnectionString = ConfigurationManager.ConnectionStrings["PATENTCN"].ConnectionString;
            con.Open();
            string sql = "";
            if (ddlSearch.SelectedIndex == 7)
                sql = "select distinct(IndustryType) as ListItems from CompanyMaster where IndustryType is not null order by IndustryType";
            else if (ddlSearch.SelectedIndex == 8)
                sql = "select distinct(businessArea) as ListItems from companybusinessarea";
            cmd.CommandText = sql;
            cmd.CommandType = CommandType.Text;
            cmd.Connection = con;
            SqlDataReader dr;
            dr = cmd.ExecuteReader();
            ddcboxSearchVal.DataTextField = "ListItems";
            ddcboxSearchVal.DataValueField = "ListItems";
            ddcboxSearchVal.DataSource = dr;
            ddcboxSearchVal.DataBind();
            dr.Close();
        }
        else
        {
            txtSearch.Visible = true;
            txtSearch.Text = "";
            ddlSearchVal.Items.Clear();
            ddlSearchVal.Visible = false;
            ddcboxSearchVal.Visible = false;
        }
    }
    protected void ibtnLetter_Click(object sender, ImageClickEventArgs e)
    {
        string sql3 = "";
        sql3 = Convert.ToString(Session["SearchForLetter"]);
        if (string.IsNullOrEmpty(sql3)) return;
        Page.ClientScript.RegisterStartupScript(this.GetType(), "LetterGeneratortt", String.Format("void(window.open('{0}','child_LetterGenerator','menubar=0,toolbar=0,resizable=1,scrollbars=1'));", "LetterGenerator.aspx"), true);        
    }
}
