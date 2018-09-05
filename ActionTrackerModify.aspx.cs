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

public partial class ActionTrackerModify : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.IsPostBack)
        {
            string [] itemList = new string [] {"EntryDt","Category","SubCategory","Action","NextAction","FollowupDt","AssignUser","ActionComplete","Comments"};

            for (int i = 0; i <= itemList.GetUpperBound(0); i++)
            {
                ddlSearch.Items.Add(itemList[i]);
            }
            ddlSearch.Items.Insert(0, "All");

            ddlCategory.Items.Add("");
            ddlCategory.Items.Add("IDF No");
            ddlCategory.Items.Add("Marketing No");
            ddlCategory.Items.Add("Event");
            ddlCategory.Items.Add("Commercial");
            ddlCategory.Items.Add("Others");

            ddlAssignUser.DataSource = Membership.GetAllUsers();
            ddlAssignUser.DataBind();
            ddlAssignUser.Items.Insert(0, "");

            ddlComplete.Items.Add("");
            ddlComplete.Items.Add("Yes");
            ddlComplete.Items.Add("No");            
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
                gvTracker.DataSource = null;
                gvTracker.DataBind();                    
                return;
            }
            if (ddlSearch.SelectedIndex != 0)
            {
                if (txtSearch.Text.Trim() == "")
                {
                    gvTracker.DataSource = null;
                    gvTracker.DataBind();
                    return;
                }
            }
            string filter = "";
            if (ddlSearch.SelectedItem.Text == "EntryDt" || ddlSearch.SelectedItem.Text == "FollowupDt")
            {
                filter = "convert(varchar(12)," + ddlSearch.SelectedItem.Text + ",101) = convert(smalldatetime,'" + txtSearch.Text.Trim() + "',103)";
            }
            else if (ddlSearch.SelectedIndex != 0)
            {
                filter =  ddlSearch.SelectedItem.Text + " like '%" + txtSearch.Text.Trim() + "%'";
            }
            if (filter != "")
            {
                filter = " Where ActionUser like '" + Membership.GetUser().UserName.Trim() + "' and " + filter;
            }
            else
            {
                filter = " Where ActionUser like '" + Membership.GetUser().UserName.Trim() + "'";
            }
            string sql = "SELECT ENTRYDT,SLNO,NEXTACTION,FOLLOWUPDT,ASSIGNUSER FROM ACTIONTRACKER " + filter + " ORDER BY ENTRYDT DESC";
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
            Session["ActionTracker"] = ds.Tables[0];
            gvTracker.DataSource = ds.Tables[0];
            gvTracker.DataBind();
        }
        catch (Exception ex)
        {
            ClientScript.RegisterStartupScript(GetType(), "error", "<script>alert('" + ex.Message.ToString() + "')</script>");
            gvTracker.DataSource = null;
            gvTracker.DataBind();
            return;
        }

    }
    protected void gvTracker_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            GridViewRow row = (GridViewRow)gvTracker.Rows[e.RowIndex];
            string EntryDate = row.Cells[0].Text;
            int serialNo = Convert.ToInt32(row.Cells[1].Text);
            SqlCommand cmd = new SqlCommand();
            con.ConnectionString = ConfigurationManager.ConnectionStrings["PATENTCN"].ConnectionString;
            string sql = "delete from ActionTracker where EntryDt = convert(smalldatetime,'" + EntryDate + "',103) and slno =" + serialNo;
            con.Open();
            cmd.CommandText = sql;
            cmd.CommandType = CommandType.Text;
            cmd.Connection = con;
            cmd.ExecuteNonQuery();
            ClientScript.RegisterStartupScript(GetType(), "delete", "<script>alert('This Record successfully Deleted')</script>");
            con.Close();
        }
        catch (Exception ex)
        {
            ClientScript.RegisterStartupScript(GetType(), "error", "<script>alert('" + ex.Message.ToString() + "')</script>");
            con.Close();
            return;
        }
        imgBtnClear_Click(sender, null);
        ibtnSearch_Click(sender, null);       
    }
    protected void gvTracker_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        try
        {
            imgBtnClear_Click(sender, null);
            SqlConnection cn = new SqlConnection();
            GridViewRow row = (GridViewRow)gvTracker.Rows[e.NewSelectedIndex];
            string EntryDate = row.Cells[0].Text;
            int serialNo = Convert.ToInt32(row.Cells[1].Text);
            SqlCommand cmd = new SqlCommand();
            System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-GB");
            cn.ConnectionString = ConfigurationManager.ConnectionStrings["PATENTCN"].ConnectionString;
            string sql = "select EntryDt,SlNo,Category,SubCategory,OtherSubCategory,Action,NextAction,FollowupDt,AssignUser,ActionComplete,Comments from ActionTracker " +
            "where EntryDt = convert(smalldatetime,'" + EntryDate + "',103) and slno =" + serialNo;
            cn.Open();
            cmd.CommandText = sql;
            cmd.CommandType = CommandType.Text;
            cmd.Connection = cn;
            SqlDataReader sdr;
            sdr = cmd.ExecuteReader();
            if (sdr.Read())
            {
                lblEntryDt.Text = sdr.GetDateTime(0).ToShortDateString();
                lblSlNo.Text = sdr.GetInt32(1).ToString();
                ddlCategory.Text = sdr.GetString(2).ToString();
                ddlCategory_SelectedIndexChanged(sender, null);
                string test=ddlSubCategory.Items[2].Text;
                if (!sdr.IsDBNull(3)) ddlSubCategory.Text = sdr.GetString(3).ToString(); else ddlSubCategory.Text=""; 
                if (!sdr.IsDBNull(4)) txtSubCategory.Text =sdr.GetString(4) ; else txtSubCategory.Text = "";
                if (!sdr.IsDBNull(5)) txtAction.Text = sdr.GetString(5); else txtAction.Text = "";
                if (!sdr.IsDBNull(6)) txtNextAction.Text = sdr.GetString(6); else txtNextAction.Text = "";
                if (!sdr.IsDBNull(7)) txtFollowupDt.Text = sdr.GetDateTime(7).ToShortDateString(); else txtFollowupDt.Text = "";
                if (!sdr.IsDBNull(8)) ddlAssignUser.Text = sdr.GetString(8); else ddlAssignUser.Text = "";
                if (!sdr.IsDBNull(9)) ddlComplete.Text = sdr.GetString(9); else ddlComplete.Text = "";
                if (!sdr.IsDBNull(10)) txtComments.Text = sdr.GetString(10); else txtComments.Text = "";                
            }
            cn.Close();
        }

        catch (Exception ex)
        {
            ClientScript.RegisterStartupScript(GetType(), "error", "<script>alert('" + ex.Message.ToString() + "')</script>");
            con.Close();
            return;
        }
    }
    protected void gvTracker_Sorting(object sender, GridViewSortEventArgs e)
    {
        DataTable dt = Session["ActionTracker"] as DataTable;
        if (dt != null)
        {
            dt.DefaultView.Sort = e.SortExpression + " " + getSortDirection(e.SortExpression);

            gvTracker.DataSource = dt;
            gvTracker.DataBind();
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
    protected void gvTracker_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
            gvTracker.PageIndex = e.NewPageIndex;
            gvTracker.DataSource = Session["ActionTracker"];
            gvTracker.DataBind();
        
    }
    protected void imgBtnSubmit_Click(object sender, ImageClickEventArgs e)
    {
        if (ddlSubCategory.SelectedIndex == 0)
        {
            ClientScript.RegisterStartupScript(GetType(), "Information", "<script>alert('File No Can not be Empty')</script>");
            return;
        }
        System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-GB");
        con.ConnectionString = ConfigurationManager.ConnectionStrings["PATENTCN"].ConnectionString;
        try
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "sp_ActionTrackerModify";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            con.Open();

            SqlParameter pm1 = new SqlParameter();
            pm1.ParameterName = "@EntryDt";
            pm1.SourceColumn = "EntryDt";
            pm1.Value = lblEntryDt.Text.Trim();
            pm1.DbType = DbType.String;
            pm1.Direction = ParameterDirection.Input;

            SqlParameter pm2 = new SqlParameter();
            pm2.ParameterName = "@subCategory";
            pm2.SourceColumn = "subCategory";
            pm2.Value = ddlSubCategory.SelectedItem.Text.Trim();
            pm2.DbType = DbType.String;
            pm2.Direction = ParameterDirection.Input;

            SqlParameter pm3 = new SqlParameter();
            pm3.ParameterName = "@Action";
            pm3.SourceColumn = "Action";
            if (txtAction.Text.Trim() == "") pm3.Value = DBNull.Value; else pm3.Value = txtAction.Text.Trim();
            pm3.DbType = DbType.String;
            pm3.Direction = ParameterDirection.Input;

            SqlParameter pm4 = new SqlParameter();
            pm4.ParameterName = "@Slno";
            pm4.SourceColumn = "SlNo";
            pm4.Value = Convert.ToInt32(lblSlNo.Text.Trim());
            pm4.DbType = DbType.Int32;
            pm4.Direction = ParameterDirection.Input;

            SqlParameter pm6 = new SqlParameter();
            pm6.ParameterName = "@NextAction";
            pm6.SourceColumn = "NextAction";
            if (txtNextAction.Text.Trim() == "") pm6.Value = DBNull.Value; else pm6.Value = txtNextAction.Text.Trim();
            pm6.DbType = DbType.String;
            pm6.Direction = ParameterDirection.Input;

            SqlParameter pm7 = new SqlParameter();
            pm7.ParameterName = "@FollowupDt";
            pm7.SourceColumn = "FollowupDt";
            if (txtFollowupDt.Text.Trim() == "")
                pm7.Value = DBNull.Value;
            else
            {
                DateTime followDt;
                if (DateTime.TryParse(txtFollowupDt.Text.Trim(), out followDt))
                    pm7.Value = txtFollowupDt.Text.Trim();
                else
                {
                    ClientScript.RegisterStartupScript(GetType(), "Information", "<script>alert('Verify Followup Date')</script>");
                    return;
                }
            }
            pm7.DbType = DbType.String;
            pm7.Direction = ParameterDirection.Input;

            SqlParameter pm8 = new SqlParameter();
            pm8.ParameterName = "@AssignUser";
            pm8.SourceColumn = "AssignUser";
            if (ddlAssignUser.SelectedItem.Text.Trim() == "") pm8.Value = DBNull.Value; else pm8.Value = ddlAssignUser.SelectedItem.Text.Trim();
            pm8.DbType = DbType.String;
            pm8.Direction = ParameterDirection.Input;

            SqlParameter pm9 = new SqlParameter();
            pm9.ParameterName = "@ActionComplete";
            pm9.SourceColumn = "ActionComplete";
            pm9.Value = ddlComplete.SelectedIndex == 2 ? 1 : 0;
            pm9.DbType = DbType.String;
            pm9.Direction = ParameterDirection.Input;

            SqlParameter pm10 = new SqlParameter();
            pm10.ParameterName = "@Comments";
            pm10.SourceColumn = "Comments";
            if (txtComments.Text.Trim() == "") pm10.Value = DBNull.Value; else pm10.Value = txtComments.Text.Trim();
            pm10.DbType = DbType.String;
            pm10.Direction = ParameterDirection.Input;

            SqlParameter pm11 = new SqlParameter();
            pm11.ParameterName = "@otherSubCategory";
            pm11.SourceColumn = "otherSubCategory";
            if (txtSubCategory.Text.Trim() == "") pm11.Value = DBNull.Value; else pm11.Value = txtSubCategory.Text.Trim();
            pm11.DbType = DbType.String;
            pm11.Direction = ParameterDirection.Input;

            SqlParameter pm12 = new SqlParameter();
            pm12.ParameterName = "@category";
            pm12.SourceColumn = "category";
            if (ddlCategory.SelectedItem.Text.Trim() == "") pm12.Value = DBNull.Value; else pm12.Value = ddlCategory.SelectedItem.Text.Trim();
            pm12.DbType = DbType.String;
            pm12.Direction = ParameterDirection.Input;

            cmd.Parameters.Add(pm1);
            cmd.Parameters.Add(pm2);
            cmd.Parameters.Add(pm3);
            cmd.Parameters.Add(pm4);
            cmd.Parameters.Add(pm6);
            cmd.Parameters.Add(pm7);
            cmd.Parameters.Add(pm8);
            cmd.Parameters.Add(pm9);
            cmd.Parameters.Add(pm10);
            cmd.Parameters.Add(pm11);
            cmd.Parameters.Add(pm12);

            cmd.ExecuteNonQuery();
            ClientScript.RegisterStartupScript(GetType(), "Information", "<script>alert('This Record successfully Modified')</script>");
            con.Close();
        }
        catch (Exception ex)
        {
            ClientScript.RegisterStartupScript(GetType(), "Error", "<script>alert('" + ex.Message.ToString() + "')</script>");
            con.Close();
            return;
        }
        imgBtnClear_Click(sender, e);
        ibtnSearch_Click(sender, null);
    }
    protected void imgBtnClear_Click(object sender, ImageClickEventArgs e)
    {
        System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-GB");
        lblEntryDt.Text = "";
        lblSlNo.Text = "";
        ddlCategory.SelectedIndex = 0;
        ddlSubCategory.Items.Clear();
        txtSubCategory.Text = "";
        txtAction.Text = "";
        txtNextAction.Text = "";
        txtFollowupDt.Text = "";
        ddlAssignUser.SelectedIndex = 0;
        ddlComplete.SelectedIndex = 0;
        txtComments.Text = "";  
    }
    protected void ddlCategory_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlSubCategory.Items.Clear();
        con.ConnectionString = ConfigurationManager.ConnectionStrings["PATENTCN"].ConnectionString;
        SqlCommand cmd = new SqlCommand();
        if (ddlCategory.SelectedIndex == 1)
        {
            txtSubCategory.Visible = false;
            lblSub.Visible = false;
            string sql = "select rtrim(fileno) as fileno from patdetails order by fileno";
            SqlDataReader dr;
            cmd.CommandType = CommandType.Text;
            cmd.Connection = con;
            cmd.CommandText = sql;
            con.Open();
            dr = cmd.ExecuteReader();
            ddlSubCategory.DataTextField = "fileno";
            ddlSubCategory.DataValueField = "fileno";
            ddlSubCategory.DataSource = dr;
            ddlSubCategory.DataBind();
            ddlSubCategory.Items.Insert(0, "");
            dr.Close();
            con.Close();
        }
        else if (ddlCategory.SelectedIndex == 2)
        {
            txtSubCategory.Visible = false;
            lblSub.Visible = false;
            string sql = "select rtrim(MktgProjectNo) as MktgProjectNo from marketingProject order by cast (substring(MktgProjectNo,3,len(MktgProjectNo)) as bigint)";
            SqlDataReader dr;
            cmd.CommandType = CommandType.Text;
            cmd.Connection = con;
            cmd.CommandText = sql;
            con.Open();
            dr = cmd.ExecuteReader();
            ddlSubCategory.DataTextField = "MktgProjectNo";
            ddlSubCategory.DataValueField = "MktgProjectNo";
            ddlSubCategory.DataSource = dr;
            ddlSubCategory.DataBind();
            ddlSubCategory.Items.Insert(0, "");
            dr.Close();
            con.Close();
        }
        else if (ddlCategory.SelectedIndex == 3 || ddlCategory.SelectedIndex == 4 || ddlCategory.SelectedIndex == 5)
        {
            txtSubCategory.Text = "";
            txtSubCategory.Visible = true;
            lblSub.Visible = true;
            string[] OtherList = new string[] { "Agreement/Documentation", "Marketing", "Attorney/Service Provider", "IITM Faculty/Student/Staff", "Others" };
            ddlSubCategory.Items.Add("");
            for (int i = 0; i <= OtherList.GetUpperBound(0); i++)
            {
                ddlSubCategory.Items.Add(OtherList[i].Trim());
            }
        }
        else
        {
            txtSubCategory.Visible = false;
            lblSub.Visible = false;
        }
    }
}
