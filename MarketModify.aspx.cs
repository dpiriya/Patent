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
using System.Collections.Generic;
using System.Web.Services;
using System.Web.Script.Services;

public partial class MarketModify : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection();
    SqlTransaction Trans;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (User.Identity.IsAuthenticated)
        {
            if (User.IsInRole("Admin") || User.IsInRole("Super User") || User.IsInRole("Marketing") ||!User.IsInRole("Intern"))
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
                Server.Transfer("Unauthorized.aspx"); 
            }
        }
    }

    [WebMethod]
    public static List<string> GetProjectNo(string mktgprojno)
    {
        List<string> result = new List<string>();
        SqlConnection cnp = new SqlConnection();
        cnp.ConnectionString = ConfigurationManager.ConnectionStrings["PATENTCN"].ConnectionString;
        SqlCommand cmd = new SqlCommand();
        string sql = "select MktgProjectNo from marketingProject where MktgProjectNo like '%'+ @SearchText +'%' group by MktgProjectNo order by MktgProjectNo";
        SqlDataReader dr;
        cmd.CommandType = CommandType.Text;
        cmd.Parameters.AddWithValue("@SearchText", mktgprojno);
        cmd.Connection = cnp;
        cmd.CommandText = sql;
        cnp.Open();
        dr = cmd.ExecuteReader();
        while (dr.Read())
        {
            result.Add(dr["MktgProjectNo"].ToString());
        }
        cnp.Close();
        return result;        
    }


    protected void imgBtnSubmit_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            SqlCommand cmd = new SqlCommand();
            System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-GB");
            con.ConnectionString = ConfigurationManager.ConnectionStrings["PATENTCN"].ConnectionString;
            string sql = "update marketingProject set MktgTitle = @MktgTitle, MktgCompany = @MktgCompany,MktgGroup=@MktgGroup," +
            "ProjectValue=@ProjectValue,ValueRealization=@ValueRealization,Status=@Status,Remarks=@Remarks where MktgProjectNo like '" + txtMktgProjNo.Text.Trim() + "'";

            cmd.CommandText = sql;
            cmd.CommandType = CommandType.Text;
            cmd.Connection = con;
            con.Open();
            Trans = con.BeginTransaction();
            cmd.Transaction = Trans;

            SqlParameter pm1 = new SqlParameter();
            pm1.ParameterName = "@MktgTitle";
            pm1.SourceColumn = "MktgTitle";
            pm1.Value = txtTitle.Text.Trim();
            pm1.DbType = DbType.String;
            pm1.Direction = ParameterDirection.Input;

            SqlParameter pm2 = new SqlParameter();
            pm2.ParameterName = "@MktgCompany";
            pm2.SourceColumn = "MktgCompany";
            pm2.Value = txtCompany.Text.Trim();
            pm2.DbType = DbType.String;
            pm2.Direction = ParameterDirection.Input;

            SqlParameter pm3 = new SqlParameter();
            pm3.ParameterName = "@MktgGroup";
            pm3.SourceColumn = "MktgGroup";
            if (txtGroup.Text.Trim() == "") pm3.Value = DBNull.Value; else pm3.Value = txtGroup.Text.Trim();
            pm3.DbType = DbType.String;
            pm3.Direction = ParameterDirection.Input;

            SqlParameter pm4 = new SqlParameter();
            pm4.ParameterName = "@ProjectValue";
            pm4.SourceColumn = "ProjectValue";
            decimal result;
            Boolean vNum = decimal.TryParse(txtValue.Text.Trim(), out result);
            if (vNum) pm4.Value = txtValue.Text.Trim(); else pm4.Value = DBNull.Value;
            pm4.DbType = DbType.Decimal;
            pm4.Direction = ParameterDirection.Input;

            SqlParameter pm5 = new SqlParameter();
            pm5.ParameterName = "@ValueRealization";
            pm5.SourceColumn = "ValueRealization";
            decimal result1;
            Boolean iNum = decimal.TryParse(txtRealization.Text.Trim(), out result1);
            if (iNum) pm5.Value = txtRealization.Text.Trim(); else pm5.Value = DBNull.Value;
            pm5.DbType = DbType.Decimal;
            pm5.Direction = ParameterDirection.Input;

            SqlParameter pm6 = new SqlParameter();
            pm6.ParameterName = "@Status";
            pm6.SourceColumn = "Status";
            if (ddlStatus.Text.Trim() == "") pm6.Value = DBNull.Value; else pm6.Value = ddlStatus.Text.Trim();
            pm6.DbType = DbType.String;
            pm6.Direction = ParameterDirection.Input;

            SqlParameter pm7 = new SqlParameter();
            pm7.ParameterName = "@Remarks";
            pm7.SourceColumn = "Remarks";
            if (txtRemark.Text.Trim() == "") pm7.Value = DBNull.Value; else pm7.Value = txtRemark.Text.Trim();
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
            DataTable dt = (DataTable)ViewState["MktIDF"];
            if (dt.Rows.Count > 0)
            {
                DataTable dtDel = dt.GetChanges(DataRowState.Deleted);
                if (dtDel != null)
                {
                    foreach (DataRow dr1 in dtDel.Rows)
                    {
                        SqlCommand cmd6 = new SqlCommand();
                        cmd6.Transaction = Trans;
                        string sql6 = "delete MarketingIdf where FileNo =@FileNo and MktgProjectNo = @MktgProjectNo";
                        cmd6.CommandText = sql6;
                        cmd6.CommandType = CommandType.Text;
                        cmd6.Connection = con;

                        SqlParameter dpm1 = new SqlParameter();
                        dpm1.ParameterName = "@MktgProjectNo";
                        dpm1.SourceColumn = "MktgProjectNo";
                        dpm1.Value = txtMktgProjNo.Text.Trim();
                        dpm1.DbType = DbType.String;
                        dpm1.Direction = ParameterDirection.Input;

                        SqlParameter dpm2 = new SqlParameter();
                        dpm2.ParameterName = "@FileNo";
                        dpm2.SourceColumn = "FileNo";
                        dpm2.Value = dr1["FileNo",DataRowVersion.Original].ToString();
                        dpm2.DbType = DbType.String;
                        dpm2.Direction = ParameterDirection.Input;

                        cmd6.Parameters.Add(dpm1);
                        cmd6.Parameters.Add(dpm2);

                        cmd6.ExecuteNonQuery();
                    }
                }

                DataTable dtAdd= dt.GetChanges(DataRowState.Added);
                if (dtAdd != null)
                {
                    foreach (DataRow dr in dtAdd.Rows)
                    {
                        SqlCommand cmd2 = new SqlCommand();
                        cmd2.Transaction = Trans;
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
            }
            DataTable ActDt = (DataTable)ViewState["MktActivity"];
            DataTable actDel = ActDt.GetChanges(DataRowState.Deleted);
            if (actDel != null)
            {
                foreach (DataRow dr in actDel.Rows)
                {
                    SqlCommand cmd7 = new SqlCommand();
                    cmd7.Transaction = Trans;
                    string sql7 = "delete from MarketingActivity where MktgProjectNo = @MktgProjectNo and SlNo= @SlNo";
                    cmd7.CommandText = sql7;
                    cmd7.CommandType = CommandType.Text;
                    cmd7.Connection = con;

                    SqlParameter cdpm1 = new SqlParameter();
                    cdpm1.ParameterName = "@MktgProjectNo";
                    cdpm1.SourceColumn = "MktgProjectNo";
                    cdpm1.Value = txtMktgProjNo.Text.Trim();
                    cdpm1.DbType = DbType.String;
                    cdpm1.Direction = ParameterDirection.Input;

                    SqlParameter cdpm2 = new SqlParameter();
                    cdpm2.ParameterName = "@SlNo";
                    cdpm2.SourceColumn = "SlNo";
                    cdpm2.Value = Convert.ToInt32(dr["SlNo", DataRowVersion.Original]);
                    cdpm2.DbType = DbType.Int32;
                    cdpm2.Direction = ParameterDirection.Input;

                    cmd7.Parameters.Add(cdpm1);
                    cmd7.Parameters.Add(cdpm2);

                    cmd7.ExecuteNonQuery();

                }
            }
            DataTable actAdd = ActDt.GetChanges(DataRowState.Added);
            if (actAdd != null)
            {
                foreach (DataRow dr in actAdd.Rows)
                {
                    SqlCommand cmd3 = new SqlCommand();
                    cmd3.Transaction = Trans;
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
                    cpm3.Value = Convert.ToDateTime(dr["ActivityDt"]);
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
            DataTable actMod = ActDt.GetChanges(DataRowState.Modified);
            if (actMod != null)
            {
                foreach (DataRow dr in actMod.Rows)
                {
                    SqlCommand cmd8 = new SqlCommand();
                    cmd8.Transaction = Trans;
                    string sql8 = "update MarketingActivity set ActivityDt= case when @ActivityDt='' then null else convert(smalldatetime,@ActivityDt,103) end," +
                    "Channel=case when @Channel='' then null else @channel end,ActivityType=case when @ActivityType='' then null else @ActivityType end," +
                    "Remarks=case when @Remarks='' then null else @Remarks end where MktgProjectNo =@MktgProjectNo and SlNo=@SlNo";
                    cmd8.CommandText = sql8;
                    cmd8.CommandType = CommandType.Text;
                    cmd8.Connection = con;

                    SqlParameter cmpm1 = new SqlParameter();
                    cmpm1.ParameterName = "@MktgProjectNo";
                    cmpm1.SourceColumn = "MktgProjectNo";
                    cmpm1.Value = txtMktgProjNo.Text.Trim();
                    cmpm1.DbType = DbType.String;
                    cmpm1.Direction = ParameterDirection.Input;

                    SqlParameter cmpm2 = new SqlParameter();
                    cmpm2.ParameterName = "@SlNo";
                    cmpm2.SourceColumn = "SlNo";
                    cmpm2.Value = Convert.ToInt32(dr["SlNo"]);
                    cmpm2.DbType = DbType.Int32;
                    cmpm2.Direction = ParameterDirection.Input;

                    SqlParameter cmpm3 = new SqlParameter();
                    cmpm3.ParameterName = "@ActivityDt";
                    cmpm3.SourceColumn = "ActivityDt";
                    cmpm3.Value = Convert.ToDateTime(dr["ActivityDt", DataRowVersion.Current]);
                    cmpm3.DbType = DbType.DateTime;
                    cmpm3.Direction = ParameterDirection.Input;

                    SqlParameter cmpm4 = new SqlParameter();
                    cmpm4.ParameterName = "@Channel";
                    cmpm4.SourceColumn = "Channel";
                    cmpm4.Value = dr["Channel"].ToString();
                    cmpm4.DbType = DbType.String;
                    cmpm4.Direction = ParameterDirection.Input;

                    SqlParameter cmpm5 = new SqlParameter();
                    cmpm5.ParameterName = "@ActivityType";
                    cmpm5.SourceColumn = "ActivityType";
                    cmpm5.Value = dr["ActivityType"].ToString();
                    cmpm5.DbType = DbType.String;
                    cmpm5.Direction = ParameterDirection.Input;

                    SqlParameter cmpm6 = new SqlParameter();
                    cmpm6.ParameterName = "@Remarks";
                    cmpm6.SourceColumn = "Remarks";
                    cmpm6.Value = dr["Remarks"].ToString();
                    cmpm6.DbType = DbType.String;
                    cmpm6.Direction = ParameterDirection.Input;

                    cmd8.Parameters.Add(cmpm1);
                    cmd8.Parameters.Add(cmpm2);
                    cmd8.Parameters.Add(cmpm3);
                    cmd8.Parameters.Add(cmpm4);
                    cmd8.Parameters.Add(cmpm5);
                    cmd8.Parameters.Add(cmpm6);

                    cmd8.ExecuteNonQuery();
                }
            }

            Trans.Commit();
            string str2 = "This Record successfully Modified";
            ClientScript.RegisterStartupScript(GetType(), "Success", "<script>alert('" + str2 + "')</script>");
            con.Close();
        }
        catch (Exception ex)
        {
            Trans.Rollback();
            ClientScript.RegisterStartupScript(GetType(), "Error", "<script>alert('" + ex.Message.ToString() + "')</script>");
            con.Close();
            return;
        }
        imgBtnClear_Click(sender, e);
    }
    protected void imgBtnClear_Click(object sender, ImageClickEventArgs e)
    {
        txtMktgProjNo.Text ="";
        txtMktgProjNo.Enabled = true;
        txtTitle.Text = "";
        txtCompany.Text = "";
        txtGroup.Text = "";
        txtValue.Text = "";
        txtRealization.Text = "";
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
    protected void lvIdf_ItemCanceling(object sender, ListViewCancelEventArgs e)
    {
        lvIdf.DataSource = (DataTable)ViewState["MktIDF"];
        lvIdf.DataBind();
    }
    protected void lvIdf_ItemCreated(object sender, ListViewItemEventArgs e)
    {
        SqlConnection con1 = new SqlConnection();
        DropDownList ddl = (DropDownList)e.Item.FindControl("ddlNewIdfNo");
        if (ddl != null)
        {
            con1.ConnectionString = ConfigurationManager.ConnectionStrings["PATENTCN"].ConnectionString;
            SqlCommand cmdA = new SqlCommand();
            string sql = "select fileno from patdetails order by fileno";
            SqlDataReader drA;
            cmdA.CommandType = CommandType.Text;
            cmdA.Connection = con1;
            cmdA.CommandText = sql;
            con1.Open();
            drA = cmdA.ExecuteReader();
            while (drA.Read())
            {
                ddl.Items.Add(drA["fileno"].ToString());
            }
            //ddl.DataTextField = "fileno";
            //ddl.DataValueField = "fileno";
            //ddl.DataSource = drA;
            //ddl.DataBind();
            ddl.Items.Insert(0, new ListItem("", ""));
            con1.Close();
        }
    }
    protected void lvIdf_ItemDeleting(object sender, ListViewDeleteEventArgs e)
    {
        int iDel = e.ItemIndex;
        DataTable dt = (DataTable)ViewState["MktIDF"];
        DataRow dr= dt.Rows[iDel];
        if (dr.RowState != DataRowState.Deleted) 
        { 
            dr.Delete(); 
        }
        lvIdf.DataSource = (DataTable)ViewState["MktIDF"];
        lvIdf.DataBind();
    }
    protected void lvIdf_ItemInserting(object sender, ListViewInsertEventArgs e)
    {
        string slno;
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
            if (dt.Rows.Count > 0)
            {
                DataRow drSl = dt.Rows[dt.Rows.Count - 1];
                if (drSl.RowState == DataRowState.Deleted)
                    slno = (Convert.ToInt32(drSl["slno", DataRowVersion.Original]) + 1).ToString();
                else
                    slno = (Convert.ToInt32(drSl["slno"]) + 1).ToString();
            }
            else
            {
                slno = "1";
            }
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
        int actDel= e.ItemIndex;
        DataTable dt = (DataTable)ViewState["MktActivity"];
        DataRow dr = dt.Rows[actDel];
        if (dr.RowState != DataRowState.Deleted) { dr.Delete(); }
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
        string slno;
        string activityDt = "";
        string channel = "";
        string activityType = "";
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
        if (activityDt != "" && channel != "")
        {
            DataTable dt = (DataTable)ViewState["MktActivity"];
            if (dt.Rows.Count > 0)
            {
                DataRow drSl = dt.Rows[dt.Rows.Count - 1];
                if (drSl.RowState == DataRowState.Deleted)
                {
                    slno = (Convert.ToInt32(drSl["slno",DataRowVersion.Original]) + 1).ToString();
                }
                else
                {
                    slno = (Convert.ToInt32(drSl["slno"]) + 1).ToString();
                }
            }
            else
            {
                slno = "1";
            }
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
            }
        }
        lvActivity.EditIndex = -1;
        lvActivity.DataSource = (DataTable)ViewState["MktActivity"];
        lvActivity.DataBind();
    }
    protected void btnFind_Click(object sender, EventArgs e)
    {
        System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-GB");
        con.ConnectionString = ConfigurationManager.ConnectionStrings["PATENTCN"].ConnectionString;
        string sql = "select MktgTitle,MktgCompany,MktgGroup,ProjectValue,ValueRealization,Status,Remarks from MarketingProject where MktgProjectNo like '" + txtMktgProjNo.Text.Trim() + "'";
        con.Open();
        SqlCommand cmd = new SqlCommand();
        cmd.CommandText = sql;
        cmd.CommandType = CommandType.Text;
        cmd.Connection = con;
        SqlDataReader dr;
        dr = cmd.ExecuteReader();
        if (dr.Read())
        {
            if (!dr.IsDBNull(0)) txtTitle.Text = dr.GetString(0).ToString(); else txtTitle.Text = "";
            if (!dr.IsDBNull(1)) txtCompany.Text = dr.GetString(1).ToString(); else txtCompany.Text = ""; 
            if (!dr.IsDBNull(2)) txtGroup.Text = dr.GetString(2).ToString(); else txtGroup.Text = "";
            if (!dr.IsDBNull(3)) txtValue.Text = dr.GetDecimal(3).ToString(); else txtValue.Text = "";
            if (!dr.IsDBNull(4)) txtRealization.Text = dr.GetDecimal(4).ToString(); else txtRealization.Text = "";
            if (!dr.IsDBNull(5)) ddlStatus.Text = dr.GetString(5).ToString(); else ddlStatus.Text = "";
            if (!dr.IsDBNull(6)) txtRemark.Text = dr.GetString(6).ToString(); else txtRemark.Text = "";
            txtMktgProjNo.Enabled = false;
        }
        dr.Close();
        cmd.Cancel();
        DataTable dtIDF = (DataTable)ViewState["MktIDF"]; 
        string sql1 = "select FileNo from MarketingIDF where MktgProjectNo like '" + txtMktgProjNo.Text.Trim() + "'";
        SqlCommand cmd1 = new SqlCommand();
        cmd1.CommandText = sql1;
        cmd1.CommandType = CommandType.Text;
        cmd1.Connection = con;
        SqlDataReader dr1;
        dr1 = cmd1.ExecuteReader();
        SqlConnection cnp = new SqlConnection();
        cnp.ConnectionString = ConfigurationManager.ConnectionStrings["PATENTCN"].ConnectionString;
        cnp.Open();
        string sql2 = "";
        SqlCommand cmd2 = new SqlCommand();
        while (dr1.Read())
        {
            sql2 = "select FileNo,Title,Inventor1,Applcn_no,Status from patdetails where fileNo like '" + dr1.GetString(0)+ "'";
            cmd2.CommandText = sql2;
            cmd2.CommandType = CommandType.Text;
            cmd2.Connection = cnp;
            SqlDataReader drIdf;
            drIdf = cmd2.ExecuteReader();
            if (drIdf.Read())
            {
                DataRow dRow = dtIDF.NewRow();
                dRow["slno"] = dtIDF.Rows.Count+1;
                dRow["fileno"] = drIdf.GetString(0);
                dRow["title"] = drIdf.GetString(1);
                dRow["inventor1"] = drIdf.GetString(2);
                if (!drIdf.IsDBNull(3)) { dRow["applcn_no"] = drIdf.GetString(3); }
                else { dRow["applcn_no"] = ""; }
                if (!drIdf.IsDBNull(4)) { dRow["status"] = drIdf.GetString(4); }
                else { dRow["status"] = ""; }
                dtIDF.Rows.Add(dRow);
            }
            drIdf.Close();
            cmd2.Cancel();
        }
        dtIDF.AcceptChanges();
        ViewState["MktIDF"] = dtIDF;
        lvIdf.DataSource = dtIDF;
        lvIdf.DataBind();
        dr1.Close();
        cmd1.Cancel();
        cnp.Close();

        DataTable dtActivity = (DataTable)ViewState["MktActivity"];
        string sql3 = "select SlNo,ActivityDt,Channel,ActivityType,Remarks from MarketingActivity where MktgProjectNo like '" + txtMktgProjNo.Text.Trim() + "' order by slno";
        SqlDataAdapter sda = new SqlDataAdapter(sql3, con);
        sda.Fill(dtActivity);
        dtActivity.AcceptChanges();
        ViewState["MktActivity"] = dtActivity;
        lvActivity.DataSource = dtActivity;
        lvActivity.DataBind();
        con.Close();
    }
}
