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
using System.Web.Services;
using System.Web.Script.Services;
using System.Collections.Generic;




public partial class Marketing : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection();
    SqlTransaction trans;  
    protected void Page_Load(object sender, EventArgs e)
    {
        if (User.Identity.IsAuthenticated)
        {
            if (User.IsInRole("Admin") || User.IsInRole("Super User") || User.IsInRole("Marketing"))
            {
                if (!this.IsPostBack)
                {
                    System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-GB");
                    MarketingDS ds = new MarketingDS();
                    lvIdf.DataSource = ds.Tables["MarketIDF"];
                    lvIdf.DataBind();
                    ViewState["MktIDF"] = ds.Tables["MarketIDF"];
                    lvActivity.DataSource = ds.Tables["MarketActivity"];
                    lvActivity.DataBind();
                    ViewState["MktActivity"] = ds.Tables["MarketActivity"];
                    txtMktgProjNo.Text = MarketNo();

                    con.ConnectionString = ConfigurationManager.ConnectionStrings["PATENTCN"].ConnectionString;
                    con.Open();
                    string sql = "select itemList from listItemMaster where category like 'MarketingStatus' order by itemlist";
                    SqlCommand cmd = new SqlCommand();
                    cmd.CommandText = sql;
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = con;
                    SqlDataReader dr;
                    dr = cmd.ExecuteReader();
                    ddlStatus.DataTextField = "itemList";
                    ddlStatus.DataValueField = "itemList";
                    ddlStatus.DataSource = dr;
                    ddlStatus.DataBind();
                    ddlStatus.Items.Insert(0, new ListItem("", ""));
                    con.Close();
                    imgBtnSubmit.Visible = (!User.IsInRole("View"));
                }
            }
            else
            {
                Server.Transfer("Unautherized.aspx");
            }
        }
        
    /*if (Request.IsAuthenticated)
        {
            
    else
        {
            Response.Redirect("unautherized.aspx");
        }*/
    }
    protected string MarketNo()
    {
        con.ConnectionString = ConfigurationManager.ConnectionStrings["PATENTCN"].ConnectionString;
        SqlCommand cmd1 = new SqlCommand();
        string sql1 = "SELECT MAX(CONVERT(BIGINT,SUBSTRING(MktgProjectNo,3,LEN(MktgProjectNo)))) FROM MARKETINGPROJECT";
        cmd1.CommandText = sql1;
        cmd1.CommandType = CommandType.Text;
        cmd1.Connection = con;
        con.Open();
        string pro = Convert.ToString(cmd1.ExecuteScalar());
        if (pro == "")
        {
            pro = "MP1";
        }
        else
        {
            pro = "MP" + (Convert.ToInt32(pro) + 1).ToString();
        }
        con.Close();
        return pro;
    }
    /*[WebMethod]
    public static List<string> GetChannelData(string channel)
    {
     * 
     * * $("#<%=txtChannel.ClientID %>").autocomplete({
    source: function(request, response) {
    $.ajax({
    type: "POST",
    contentType: "application/json; charset=utf-8",
    url: "Marketing.aspx/GetChannelData",
    data: "{'channel':'" + $("#<%=txtChannel.ClientID %>").val() + "'}",
    dataType: "json",
    success: function(data) {
    response(data.d);
    },
    error: function(result) {
    alert("Error");
    }
    });
    },
    minLength: 2
    }); 
   
        List<string> result = new List<string>();
        SqlConnection cnp = new SqlConnection();
        cnp.ConnectionString = ConfigurationManager.ConnectionStrings["PATENTCN"].ConnectionString;
        SqlCommand cmd = new SqlCommand();
        string sql = "select channel from marketing where channel like '%'+ @SearchText +'%' group by channel order by channel";
        SqlDataReader dr;
        cmd.CommandType = CommandType.Text;
        cmd.Parameters.AddWithValue("@SearchText", channel);
        cmd.Connection = cnp;
        cmd.CommandText = sql;
        cnp.Open();
        dr = cmd.ExecuteReader();
        while (dr.Read())
        {
            result.Add(dr["channel"].ToString());
        }
        return result;
    }*/

    [WebMethod]
    public static List<string> GetCompanyData(string company)
    {
        List<string> result = new List<string>();
        SqlConnection cnp = new SqlConnection();
        cnp.ConnectionString = ConfigurationManager.ConnectionStrings["PATENTCN"].ConnectionString;
        SqlCommand cmd = new SqlCommand();
        string sql = "select companyName from companyMaster where companyName like '%'+ @SearchText +'%' group by companyName order by companyName";
        SqlDataReader dr;
        cmd.CommandType = CommandType.Text;
        cmd.Parameters.AddWithValue("@SearchText", company);
        cmd.Connection = cnp;
        cmd.CommandText = sql;
        cnp.Open();
        dr = cmd.ExecuteReader();
        while (dr.Read())
        {
            result.Add(dr["companyName"].ToString());
        }
        return result;
    }
    protected void imgBtnSubmit_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            SqlCommand cmd = new SqlCommand();
            System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-GB");
            con.ConnectionString = ConfigurationManager.ConnectionStrings["PATENTCN"].ConnectionString;
            string sql = "insert into marketingProject (EntryDt,MktgProjectNo,MktgTitle,MktgCompany,MktgGroup,ProjectValue,ValueRealization,Status,Remarks) values " +
            "(convert(smalldatetime,@EntryDt,103),@MktgProjectNo,@MktgTitle,@MktgCompany,@MktgGroup,@ProjectValue,@ValueRealization,@Status,@Remarks)";

            cmd.CommandText = sql;
            cmd.CommandType = CommandType.Text;
            cmd.Connection = con;
            con.Open();
            trans = con.BeginTransaction();
            cmd.Transaction = trans;

            SqlParameter pm1 = new SqlParameter();
            pm1.ParameterName = "@MktgProjectNo";
            pm1.SourceColumn = "MktgProjectNo";
            pm1.Value = txtMktgProjNo.Text.Trim();
            pm1.DbType = DbType.String;
            pm1.Direction = ParameterDirection.Input;

            SqlParameter pm2 = new SqlParameter();
            pm2.ParameterName = "@MktgTitle";
            pm2.SourceColumn = "MktgTitle";
            pm2.Value = txtTitle.Text.Trim();
            pm2.DbType = DbType.String;
            pm2.Direction = ParameterDirection.Input;

            SqlParameter pm3 = new SqlParameter();
            pm3.ParameterName = "@MktgCompany";
            pm3.SourceColumn = "MktgCompany";
            pm3.Value = txtCompany.Text.Trim();
            pm3.DbType = DbType.String;
            pm3.Direction = ParameterDirection.Input;

            SqlParameter pm4 = new SqlParameter();
            pm4.ParameterName = "@EntryDt";
            pm4.SourceColumn = "EntryDt";
            pm4.Value = DateTime.Now.ToShortDateString();
            pm4.DbType = DbType.String;
            pm4.Direction = ParameterDirection.Input;

            SqlParameter pm5 = new SqlParameter();
            pm5.ParameterName = "@MktgGroup";
            pm5.SourceColumn = "MktgGroup";
            if (txtGroup.Text.Trim() == "") pm5.Value = DBNull.Value; else pm5.Value = txtGroup.Text.Trim();
            pm5.DbType = DbType.String;
            pm5.Direction = ParameterDirection.Input;

            SqlParameter pm6 = new SqlParameter();
            pm6.ParameterName = "@ProjectValue";
            pm6.SourceColumn = "ProjectValue";
            decimal result;
            Boolean vNum = decimal.TryParse(txtValue.Text.Trim(),out result);
            if (vNum) pm6.Value = txtValue.Text.Trim(); else  pm6.Value = DBNull.Value;
            pm6.DbType = DbType.Decimal;
            pm6.Direction = ParameterDirection.Input;
            
            SqlParameter pm7 = new SqlParameter();
            pm7.ParameterName = "@ValueRealization";
            pm7.SourceColumn = "ValueRealization";
            decimal result1;
            Boolean iNum = decimal.TryParse(txtRealization.Text.Trim(), out result1);
            if (iNum) pm7.Value = txtRealization.Text.Trim(); else  pm7.Value =DBNull.Value;
            pm7.DbType = DbType.Decimal;
            pm7.Direction = ParameterDirection.Input;

            SqlParameter pm8 = new SqlParameter();
            pm8.ParameterName = "@Status";
            pm8.SourceColumn = "Status";
            if (ddlStatus.Text.Trim() == "") pm8.Value = DBNull.Value; else pm8.Value = ddlStatus.Text.Trim();
            pm8.DbType = DbType.String;
            pm8.Direction = ParameterDirection.Input;

            SqlParameter pm9 = new SqlParameter();
            pm9.ParameterName = "@Remarks";
            pm9.SourceColumn = "Remarks";
            if (txtRemark.Text.Trim() == "") pm9.Value = DBNull.Value; else pm9.Value = txtRemark.Text.Trim();
            pm9.DbType = DbType.String;
            pm9.Direction = ParameterDirection.Input;

            cmd.Parameters.Add(pm1);
            cmd.Parameters.Add(pm2);
            cmd.Parameters.Add(pm3);
            cmd.Parameters.Add(pm4);
            cmd.Parameters.Add(pm5);
            cmd.Parameters.Add(pm6);
            cmd.Parameters.Add(pm7);
            cmd.Parameters.Add(pm8);
            cmd.Parameters.Add(pm9);


            cmd.ExecuteNonQuery();

            DataTable dt = (DataTable)ViewState["MktIDF"];
            if (dt.Rows.Count > 0)
            {
            
                foreach (DataRow dr in dt.Rows)
                {
                    SqlCommand cmd2 = new SqlCommand();
                    cmd2.Transaction = trans;
                    string sql2 = "insert into MarketingIdf (EntryDt,MktgProjectNo,FileNo) values " +
                    "(convert(smalldatetime,@EntryDt,103),@MktgProjectNo,@FileNo)";
                    cmd2.CommandText = sql2;
                    cmd2.CommandType = CommandType.Text;
                    cmd2.Connection = con;

                    SqlParameter bpm1 = new SqlParameter();
                    bpm1.ParameterName = "@MktgProjectNo";
                    bpm1.SourceColumn = "MktgProjectNo";
                    bpm1.Value = txtMktgProjNo.Text.Trim();
                    bpm1.DbType = DbType.String;
                    bpm1.Direction = ParameterDirection.Input;

                    SqlParameter bpm2 = new SqlParameter();
                    bpm2.ParameterName = "@FileNo";
                    bpm2.SourceColumn = "FileNo";
                    bpm2.Value = dr["FileNo"].ToString();
                    bpm2.DbType = DbType.String;
                    bpm2.Direction = ParameterDirection.Input;

                    SqlParameter bpm3 = new SqlParameter();
                    bpm3.ParameterName = "@EntryDt";
                    bpm3.SourceColumn = "EntryDt";
                    bpm3.Value = DateTime.Now.ToShortDateString();
                    bpm3.DbType = DbType.String;
                    bpm3.Direction = ParameterDirection.Input;

                    cmd2.Parameters.Add(bpm1);
                    cmd2.Parameters.Add(bpm2);
                    cmd2.Parameters.Add(bpm3);

                    cmd2.ExecuteNonQuery();
                }
            }
            DataTable ActDt = (DataTable)ViewState["MktActivity"];
            if (ActDt.Rows.Count > 0)
            {
                foreach (DataRow dr in ActDt.Rows)
                {
                    SqlCommand cmd3 = new SqlCommand();
                    cmd3.Transaction = trans;
                    string sql3 = "insert into MarketingActivity(EntryDt,MktgProjectNo,SlNo,ActivityDt,Channel,ActivityType,Remarks) values " +
                    "(convert(smalldatetime,@EntryDt,103),@MktgProjectNo,@SlNo,case when @ActivityDt='' then null else convert(smalldatetime,@ActivityDt,103) end," +
                    "case when @Channel='' then null else @channel end,case when @ActivityType='' then null else @ActivityType end,case when @Remarks='' then null else @Remarks end)";
                    cmd3.CommandText = sql3;
                    cmd3.CommandType = CommandType.Text;
                    cmd3.Connection = con;

                    SqlParameter cpm1 = new SqlParameter();
                    cpm1.ParameterName = "@MktgProjectNo";
                    cpm1.SourceColumn = "MktgProjectNo";
                    cpm1.Value = txtMktgProjNo.Text.Trim();
                    cpm1.DbType = DbType.String;
                    cpm1.Direction = ParameterDirection.Input;

                    SqlParameter cpm2 = new SqlParameter();
                    cpm2.ParameterName = "@SlNo";
                    cpm2.SourceColumn = "SlNo";
                    cpm2.Value = Convert.ToInt32(dr["SlNo"]);
                    cpm2.DbType = DbType.Int32;
                    cpm2.Direction = ParameterDirection.Input;

                    SqlParameter cpm3 = new SqlParameter();
                    cpm3.ParameterName = "@ActivityDt";
                    cpm3.SourceColumn = "ActivityDt";
                    cpm3.Value =Convert.ToDateTime(dr["ActivityDt"]);
                    cpm3.DbType = DbType.DateTime;
                    cpm3.Direction = ParameterDirection.Input;

                    SqlParameter cpm4 = new SqlParameter();
                    cpm4.ParameterName = "@Channel";
                    cpm4.SourceColumn = "Channel";
                    cpm4.Value = dr["Channel"].ToString();
                    cpm4.DbType = DbType.String;
                    cpm4.Direction = ParameterDirection.Input;

                    SqlParameter cpm5 = new SqlParameter();
                    cpm5.ParameterName = "@ActivityType";
                    cpm5.SourceColumn = "ActivityType";
                    cpm5.Value = dr["ActivityType"].ToString();
                    cpm5.DbType = DbType.String;
                    cpm5.Direction = ParameterDirection.Input;

                    SqlParameter cpm6 = new SqlParameter();
                    cpm6.ParameterName = "@Remarks";
                    cpm6.SourceColumn = "Remarks";
                    cpm6.Value = dr["Remarks"].ToString();
                    cpm6.DbType = DbType.String;
                    cpm6.Direction = ParameterDirection.Input;

                    SqlParameter cpm7 = new SqlParameter();
                    cpm7.ParameterName = "@EntryDt";
                    cpm7.SourceColumn = "EntryDt";
                    cpm7.Value = DateTime.Now.ToShortDateString();
                    cpm7.DbType = DbType.String;
                    cpm7.Direction = ParameterDirection.Input;

                    cmd3.Parameters.Add(cpm1);
                    cmd3.Parameters.Add(cpm2);
                    cmd3.Parameters.Add(cpm3);
                    cmd3.Parameters.Add(cpm4);
                    cmd3.Parameters.Add(cpm5);
                    cmd3.Parameters.Add(cpm6);
                    cmd3.Parameters.Add(cpm7);
                   
                    cmd3.ExecuteNonQuery();

                }
            }

            trans.Commit();
            ClientScript.RegisterStartupScript(GetType(), "success", "<script>alert('This Record successfully Added')</script>");
            con.Close();

        }
        catch (Exception ex)
        {
            trans.Rollback();
            ClientScript.RegisterStartupScript(GetType(), "Error", "<script>alert('" + ex.Message.ToString() + "')</script>");
            con.Close();
            return;
        }
        imgBtnClear_Click(sender, e);
    }
    protected void imgBtnClear_Click(object sender, ImageClickEventArgs e)
    {
        txtMktgProjNo.Text = MarketNo();
        txtTitle.Text = "";
        txtCompany.Text = "";
        txtValue.Text = "";
        txtRealization.Text = "";
        txtGroup.Text = "";
        ddlStatus.Text = "";
        DataTable dt = (DataTable)ViewState["MktIDF"];
        dt.Clear();
        lvIdf.DataSource = dt;
        lvIdf.DataBind();
        DataTable dt1 = (DataTable)ViewState["MktActivity"];
        dt1.Clear();
        lvActivity.DataSource = dt1;
        lvActivity.DataBind();       
    }

    protected void lvIdf_ItemCreated(object sender, ListViewItemEventArgs e)
    {
        DropDownList ddl = (DropDownList)e.Item.FindControl("ddlNewIdfNo");
        if (ddl != null)
        {
            con.ConnectionString = ConfigurationManager.ConnectionStrings["PATENTCN"].ConnectionString;
            SqlCommand cmd = new SqlCommand();
            string sql = "select fileno from patdetails order by fileno";
            SqlDataReader dr;
            cmd.CommandType = CommandType.Text;
            cmd.Connection = con;
            cmd.CommandText = sql;
            con.Open();
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                ddl.Items.Add(dr["fileno"].ToString());
            }
            //ddl.DataTextField = "fileno";
            //ddl.DataValueField = "fileno";
            //ddl.DataSource = dr;
            //ddl.DataBind();
            ddl.Items.Insert(0, new ListItem("", ""));
            con.Close();
        }
    }

    protected void ddlNewIdfNo_SelectedIndexChanged(object sender, EventArgs e)
    {
        DropDownList ddlIdf = (DropDownList)sender;
        ListViewItem item1 = (ListViewItem)ddlIdf.NamingContainer;
        DropDownList ddlIdfGet = (DropDownList)item1.FindControl("ddlNewIdfNo");
        if (ddlIdfGet.SelectedValue != "")
        {
            con.ConnectionString = ConfigurationManager.ConnectionStrings["PATENTCN"].ConnectionString;
            SqlCommand cmd = new SqlCommand();
            string sql = "select title,inventor1,applcn_no,status from patdetails where fileno='" + ddlIdfGet.SelectedValue + "'";
            SqlDataReader dr;
            cmd.CommandType = CommandType.Text;
            cmd.Connection = con;
            cmd.CommandText = sql;
            con.Open();
            dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                TextBox txt = (TextBox)item1.FindControl("txtTitle");
                txt.Text = dr.GetString(0);
                txt = (TextBox)item1.FindControl("txtInventor");
                txt.Text = dr.GetString(1);
                txt = (TextBox)item1.FindControl("txtApplcnNo");
                if (!dr.IsDBNull(2))
                {
                    txt.Text = dr.GetString(2);
                }
                else
                {
                    txt.Text = "";
                }

            }
            con.Close();
        }
    }
    protected void lvIdf_ItemDataBound(object sender, ListViewItemEventArgs e)
    {
        
    }
    protected void lvIdf_ItemCanceling(object sender, ListViewCancelEventArgs e)
    {
        lvIdf.DataSource = (DataTable)ViewState["MktIDF"];
        lvIdf.DataBind();
    }
    protected void lvIdf_ItemDeleting(object sender, ListViewDeleteEventArgs e)
    {
        Label lbl = lvIdf.Items[e.ItemIndex].FindControl("lblSlNo") as Label;
        DataTable dt = (DataTable)ViewState["MktIDF"];
        int cnt = 0;
        foreach (DataRow dr in dt.Rows)
        {
            cnt += 1;
            if (dr["SlNo"].ToString() == lbl.Text.Trim())
            {
                dr.Delete();
                break;
            }
        }
        for (int i = cnt-1; i <= dt.Rows.Count - 1; i++)
        {
            dt.Rows[i][0] = i+1;
        }
        lvIdf.DataSource = (DataTable)ViewState["MktIDF"];
        lvIdf.DataBind();
    }
    
    protected void lvIdf_ItemInserting(object sender, ListViewInsertEventArgs e)
    {
        string slno = Convert.ToString(lvIdf.Items.Count + 1);
        string ldf = "";
        string title = "";
        string inventor1 = "";
        string applcn_no = "";
        string status = "";
        DropDownList ddl = e.Item.FindControl("ddlNewIdfNo") as DropDownList;
        ldf = ddl.SelectedValue;
        if (ldf != "")
        {
            TextBox txt;
            txt = e.Item.FindControl("txtTitle") as TextBox;
            title = txt.Text;
            txt = e.Item.FindControl("txtInventor") as TextBox;
            inventor1 = txt.Text;
            txt = e.Item.FindControl("txtApplcnNo") as TextBox;
            applcn_no = txt.Text;
            txt = e.Item.FindControl("txtStatus") as TextBox;
            status = txt.Text;

            DataTable dt = (DataTable)ViewState["MktIDF"];
            DataRow dr = dt.NewRow();
            dr["slno"] = slno;
            dr["fileno"] = ldf;
            dr["title"] = title;
            dr["inventor1"] = inventor1;
            dr["applcn_no"] = applcn_no;
            dr["status"] = status;
            dt.Rows.Add(dr);
            lvIdf.DataSource = (DataTable)ViewState["MktIDF"];
            lvIdf.DataBind();
        }
    }
    protected void lvActivity_DataBinding(object sender, EventArgs e)
    {

    }
    protected void lvActivity_ItemCanceling(object sender, ListViewCancelEventArgs e)
    {
        System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-GB");
        lvActivity.EditIndex = -1;
        lvActivity.DataSource = (DataTable)ViewState["MktActivity"];
        lvActivity.DataBind();
    }
    protected void lvActivity_ItemDeleting(object sender, ListViewDeleteEventArgs e)
    {
        System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-GB");
        Label lbl = lvActivity.Items[e.ItemIndex].FindControl("lblSlNo") as Label;
        DataTable dt = (DataTable)ViewState["MktActivity"];
        int cnt = 0;
        foreach (DataRow dr in dt.Rows)
        {
            cnt += 1;
            if (dr["SlNo"].ToString() == lbl.Text.Trim())
            {
                dr.Delete();
                break;
            }
        }
        for (int i = cnt-1 ; i <= dt.Rows.Count - 1; i++)
        {
            dt.Rows[i][0] = i+1;
        }
        lvActivity.DataSource = (DataTable)ViewState["MktActivity"];
        lvActivity.DataBind();
    }
    protected void lvActivity_ItemEditing(object sender, ListViewEditEventArgs e)
    {
        lvActivity.EditIndex = e.NewEditIndex;
        System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-GB");
        lvActivity.DataSource = (DataTable)ViewState["MktActivity"];
        lvActivity.DataBind();
    }
    protected void lvActivity_ItemInserting(object sender, ListViewInsertEventArgs e)
    {
        System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-GB");
        string slno = Convert.ToString(lvActivity.Items.Count + 1);
        string activityDt = "";
        string channel = "";
        string activityType= "";
        string remarks = "";
        TextBox txt;
        txt = e.Item.FindControl("txtActivityDt") as TextBox;
        activityDt = txt.Text;
        txt = e.Item.FindControl("txtChannel") as TextBox;
        channel = txt.Text;
        txt = e.Item.FindControl("txtActivityType") as TextBox;
        activityType = txt.Text;
        txt = e.Item.FindControl("txtActivityDetails") as TextBox;
        remarks = txt.Text;
        if (activityDt != "" && channel !="")
        {
            DataTable dt = (DataTable)ViewState["MktActivity"];
            DataRow dr = dt.NewRow();
            dr["slno"] = slno;
            dr["ActivityDt"] = Convert.ToDateTime(activityDt);
            dr["Channel"] = channel;
            dr["ActivityType"] = activityType;
            dr["Remarks"] = remarks;
            dt.Rows.Add(dr);
            lvActivity.DataSource = (DataTable)ViewState["MktActivity"];
            lvActivity.DataBind();
        }
    }
    protected void lvActivity_ItemUpdating(object sender, ListViewUpdateEventArgs e)
    {
        System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-GB");
        string slno = "";
        string activityDt = "";
        string channel = "";
        string activityType = "";
        string remarks = "";
        Label lbl;
        TextBox txt;
        lbl = lvActivity.Items[e.ItemIndex].FindControl("lblEdSlNo") as Label;
        slno = lbl.Text;
        txt = lvActivity.Items[e.ItemIndex].FindControl("txtEdActivityDt") as TextBox;
        activityDt = txt.Text;
        txt = lvActivity.Items[e.ItemIndex].FindControl("txtEdChannel") as TextBox;
        channel = txt.Text;
        txt = lvActivity.Items[e.ItemIndex].FindControl("txtEdActivityType") as TextBox;
        activityType = txt.Text;
        txt = lvActivity.Items[e.ItemIndex].FindControl("txtEdActivityDetails") as TextBox;
        remarks = txt.Text;
        DataTable dt = (DataTable)ViewState["MktActivity"];
        foreach (DataRow dr in dt.Rows)
        {
            if (dr["slno"].ToString() == slno)
            {
                dr["ActivityDt"] = Convert.ToDateTime(activityDt);
                dr["Channel"] = channel;
                dr["ActivityType"] = activityType;
                dr["Remarks"] = remarks;
                dr.AcceptChanges();
            }
        }
        lvActivity.EditIndex = -1;
        lvActivity.DataSource = (DataTable)ViewState["MktActivity"];
        lvActivity.DataBind();
    }
}
