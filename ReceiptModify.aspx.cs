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

public partial class ReceiptModify : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection();
    HiddenField hSlNo = new HiddenField();
    HiddenField hEntryDt = new HiddenField();
    string FileNo;
    protected void Page_Load(object sender, EventArgs e)
    {

        FileNo = Request.QueryString["fileno"];
        lblTitle.InnerText = "File Number : " + FileNo.Trim();
        if (!this.IsPostBack)
        {
            fetchReceipt();
            SqlCommand cmd = new SqlCommand();
            System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-GB");
            con.ConnectionString = ConfigurationManager.ConnectionStrings["PATENTCN"].ConnectionString;
                      
            string sql = "select currencyCode,countryName from currency where status is not null order by status";
            con.Open();
            cmd.CommandText = sql;
            cmd.CommandType = CommandType.Text;
            cmd.Connection = con;
            SqlDataReader dr;
            dr = cmd.ExecuteReader();
            ddlCurrency.Items.Add("");
            while (dr.Read())
            {
                ddlCurrency.Items.Add(new ListItem(dr.GetString(0) + " - " + dr.GetString(1), dr.GetString(0)));
            }
            dr.Close();
          
            sql = "select itemlist from listitemmaster where category like 'CostGroup' and [grouping] like 'receipt' order by itemList";
            cmd.CommandText = sql;
            dr = cmd.ExecuteReader();
            ddlPaymentGroup.DataTextField = "itemList";
            ddlPaymentGroup.DataValueField = "itemList";
            ddlPaymentGroup.DataSource = dr;
            ddlPaymentGroup.DataBind();
            ddlPaymentGroup.Items.Insert(0, new ListItem("", ""));
            dr.Close();
          
            string tYear;
            int lYear;
            if (DateTime.Now.Month >= 4) lYear = DateTime.Now.Year; else lYear = DateTime.Now.Year - 1;
            ddlYear.Items.Add("");
            for (int i = lYear; i >= 1998; i--)
            {
                tYear = i.ToString() + " - " + (i + 1).ToString();
                ddlYear.Items.Add(tYear);
            }            
        }
        con.Close();
        imgBtnSubmit.Visible = (!User.IsInRole("View"));
    }
    protected void fetchReceipt()
    {
        lvReceipt.DataSource = null;
        lvReceipt.DataBind();
        SqlCommand cmd = new SqlCommand();
        System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-GB");
        con.ConnectionString = ConfigurationManager.ConnectionStrings["PATENTCN"].ConnectionString;
        string sql = "select Fileno,SlNo,party,PaymentDate,paymentDescription,Cost_Rs from patentreceipt where fileno like '" + FileNo.Trim() + "' order by slno";
        con.Open();
        cmd.CommandText = sql;
        cmd.CommandType = CommandType.Text;
        cmd.Connection = con;
        SqlDataReader dr;
        dr = cmd.ExecuteReader();
        DataTable dt = new DataTable();
        dt.Load(dr);
        lvReceipt.DataSource = dt;
        lvReceipt.DataBind();
        con.Close();
    }

    [WebMethod]

    public static List<string> GetAutoCompleteData(string party)
    {
        List<string> result = new List<string>();
        SqlConnection cnp = new SqlConnection();
        cnp.ConnectionString = ConfigurationManager.ConnectionStrings["PATENTCN"].ConnectionString;
        SqlCommand cmd = new SqlCommand();
        string sql = "select party from patentreceipt where party like + @SearchText +'%' group by party order by party";
        SqlDataReader dr;
        cmd.CommandType = CommandType.Text;
        cmd.Parameters.AddWithValue("@SearchText", party);
        cmd.Connection = cnp;
        cmd.CommandText = sql;
        cnp.Open();
        dr = cmd.ExecuteReader();
        while (dr.Read())
        {
            result.Add(dr["party"].ToString());
        }
        return result;
    }

    [WebMethod]

    public static List<string> GetTransTypeData(string Trans)
    {
        List<string> result = new List<string>();
        SqlConnection cnp = new SqlConnection();
        cnp.ConnectionString = ConfigurationManager.ConnectionStrings["PATENTCN"].ConnectionString;
        SqlCommand cmd = new SqlCommand();
        string sql = "select TransType from patentreceipt where TransType like + @SearchText +'%' group by TransType order by TransType";
        SqlDataReader dr;
        cmd.CommandType = CommandType.Text;
        cmd.Parameters.AddWithValue("@SearchText", Trans);
        cmd.Connection = cnp;
        cmd.CommandText = sql;
        cnp.Open();
        dr = cmd.ExecuteReader();
        while (dr.Read())
        {
            result.Add(dr["TransType"].ToString());
        }
        return result;
    }
    protected void Clear()
    {
        lblFileNo.Text = "";
        lblSerial.Text = "";
        txtTechTransNo.Text = "";
        txtPartyName.Text = "";
        txtPartyRefNo.Text = "";
        txtSubDate.Text = "";
        txtTransType.Text = "";
        txtTransDesc.Text = "";
        ddlPaymentGroup.Text = "";
        txtPaymentRef.Text = "";
        txtPaymentDt.Text = "";
        txtPaymentDesc.Text = "";
        ddlCurrency.Text = "";
        txtExRate.Text = "";
        txtCost.Text = "";
        txtCostINR.Text = "";
        ddlYear.Text = "";
    }
    protected void imgBtnClear_Click(object sender, ImageClickEventArgs e)
    {
        Clear();
    }
    protected void imgBtnSubmit_Click(object sender, ImageClickEventArgs e)
    {

        if (lblFileNo.Text.Trim() == "")
        {
            ClientScript.RegisterStartupScript(GetType(), "Information", "<script>alert('File Number can not be Empty')</script>");
            return;
        }
        try
        {
            SqlCommand cmd = new SqlCommand();
            System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-GB");
            con.ConnectionString = ConfigurationManager.ConnectionStrings["PATENTCN"].ConnectionString;
            string sql = "Update patentReceipt set TechTransferNo=case when @TechTransferNo='' then null else @TechTransferNo end,Party=case when @Party='' then null else @Party end,PartyRefNo=case when @PartyRefNo='' then null else @PartyRefNo end," +
            "SubmissionDt=case when @SubmissionDt='' then null else convert(smalldatetime,@SubmissionDt,103) end,TransType=case when @TransType='' then null else @TransType end,TransDescription=case when @TransDescription='' then null else @TransDescription end," +
            "PaymentGroup=case when @PaymentGroup='' then null else @PaymentGroup end,PaymentDescription=case when @PaymentDescription='' then null else @PaymentDescription end,Currency=case when @Currency='' then null else @Currency end," +
            "ForeignCost=@ForeignCost,ExRate=@ExRate,Cost_Rs=@Cost_Rs,PaymentDate=case when @PaymentDate='' then null else convert(smalldatetime,@PaymentDate,103) end,PaymentRef=case when @PaymentRef='' then null else @PaymentRef end,Year=case when @Year='' then null else @Year end " +
            "where fileno='"+ lblFileNo.Text.Trim()+"' and slno="+lblSerial.Text.Trim();

            cmd.CommandText = sql;
            cmd.CommandType = CommandType.Text;
            cmd.Connection = con;
            con.Open();
            
            SqlParameter pm1 = new SqlParameter();
            pm1.ParameterName = "@TechTransferNo";
            pm1.SourceColumn = "TechTransferNo";
            pm1.Value = txtTechTransNo.Text.Trim();
            pm1.DbType = DbType.String;
            pm1.Direction = ParameterDirection.Input;

            SqlParameter pm2 = new SqlParameter();
            pm2.ParameterName = "@Party";
            pm2.SourceColumn = "Party";
            pm2.Value = txtPartyName.Text.Trim();
            pm2.DbType = DbType.String;
            pm2.Direction = ParameterDirection.Input;

            SqlParameter pm3 = new SqlParameter();
            pm3.ParameterName = "@PartyRefNo";
            pm3.SourceColumn = "PartyRefNo";
            pm3.Value = txtPartyRefNo.Text.Trim();
            pm3.DbType = DbType.String;
            pm3.Direction = ParameterDirection.Input;

            SqlParameter pm4 = new SqlParameter();
            pm4.ParameterName = "@SubmissionDt";
            pm4.SourceColumn = "SubmissionDt";
            pm4.Value = txtSubDate.Text.Trim();
            pm4.DbType = DbType.String;
            pm4.Direction = ParameterDirection.Input;

            SqlParameter pm5 = new SqlParameter();
            pm5.ParameterName = "@TransType";
            pm5.SourceColumn = "TransType";
            pm5.Value = txtTransType.Text.Trim();
            pm5.DbType = DbType.String;
            pm5.Direction = ParameterDirection.Input;

            SqlParameter pm6 = new SqlParameter();
            pm6.ParameterName = "@TransDescription";
            pm6.SourceColumn = "TransDescription";
            pm6.Value = txtTransDesc.Text.Trim();
            pm6.DbType = DbType.String;
            pm6.Direction = ParameterDirection.Input;

            SqlParameter pm7 = new SqlParameter();
            pm7.ParameterName = "@PaymentGroup";
            pm7.SourceColumn = "PaymentGroup";
            pm7.Value = ddlPaymentGroup.SelectedItem.Value.Trim();
            pm7.DbType = DbType.String;
            pm7.Direction = ParameterDirection.Input;

            SqlParameter pm8 = new SqlParameter();
            pm8.ParameterName = "@PaymentDescription";
            pm8.SourceColumn = "PaymentDescription";
            pm8.Value = txtPaymentDesc.Text.Trim();
            pm8.DbType = DbType.String;
            pm8.Direction = ParameterDirection.Input;

            SqlParameter pm9 = new SqlParameter();
            pm9.ParameterName = "@Currency";
            pm9.SourceColumn = "Currency";
            pm9.Value = ddlCurrency.SelectedItem.Value.Trim();
            pm9.DbType = DbType.String;
            pm9.Direction = ParameterDirection.Input;

            SqlParameter pm10 = new SqlParameter();
            pm10.ParameterName = "@ForeignCost";
            pm10.SourceColumn = "ForeignCost";
            pm10.Value = txtCost.Text.Trim() == "" ? 0 : Convert.ToDecimal(txtCost.Text.Trim());
            pm10.DbType = DbType.Decimal;
            pm10.Direction = ParameterDirection.Input;

            SqlParameter pm11 = new SqlParameter();
            pm11.ParameterName = "@ExRate";
            pm11.SourceColumn = "ExRate";
            pm11.Value = txtExRate.Text.Trim() == "" ? 0 : Convert.ToDecimal(txtExRate.Text.Trim());
            pm11.DbType = DbType.Decimal;
            pm11.Direction = ParameterDirection.Input;

            SqlParameter pm12 = new SqlParameter();
            pm12.ParameterName = "@Cost_Rs";
            pm12.SourceColumn = "Cost_Rs";
            pm12.Value = txtCostINR.Text.Trim() == "" ? 0 : Convert.ToDecimal(txtCostINR.Text.Trim());
            pm12.DbType = DbType.String;
            pm12.Direction = ParameterDirection.Input;

            SqlParameter pm13 = new SqlParameter();
            pm13.ParameterName = "@PaymentDate";
            pm13.SourceColumn = "PaymentDate";
            pm13.Value = txtPaymentDt.Text.Trim();
            pm13.DbType = DbType.String;
            pm13.Direction = ParameterDirection.Input;

            SqlParameter pm14 = new SqlParameter();
            pm14.ParameterName = "@PaymentRef";
            pm14.SourceColumn = "PaymentRef";
            pm14.Value = txtPaymentRef.Text.Trim();
            pm14.DbType = DbType.String;
            pm14.Direction = ParameterDirection.Input;

            SqlParameter pm15 = new SqlParameter();
            pm15.ParameterName = "@Year";
            pm15.SourceColumn = "Year";
            pm15.Value = ddlYear.SelectedItem.Value.Trim();
            pm15.DbType = DbType.String;
            pm15.Direction = ParameterDirection.Input;

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
           
            cmd.ExecuteNonQuery();

            ClientScript.RegisterStartupScript(GetType(), "Success", "<script>alert('This Record successfully Modified')</script>");
            con.Close();
        }
        catch (Exception ex)
        {
            ClientScript.RegisterStartupScript(GetType(), "Error", "<script>alert('" + ex.Message.ToString() + "')</script>");
            con.Close();
            return;
        }
        imgBtnClear_Click(sender, e);
        fetchReceipt();
    }
    protected void lvReceipt_SelectedIndexChanging(object sender, ListViewSelectEventArgs e)
    {
        ListViewItem item = lvReceipt.Items[e.NewSelectedIndex];
        Label slnoLbl = (Label)item.FindControl("lblSlno");
        Clear();
        SqlCommand cmd = new SqlCommand();
        System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-GB");
        con.ConnectionString = ConfigurationManager.ConnectionStrings["PATENTCN"].ConnectionString;
        string sql = "select * from patentReceipt where fileno like '"+ FileNo.Trim()+"' and slno ="+ slnoLbl.Text.Trim();
        con.Open();
        cmd.CommandText = sql;
        cmd.CommandType = CommandType.Text;
        cmd.Connection = con;
        SqlDataReader dr;
        dr = cmd.ExecuteReader();
        while (dr.Read())
        {
            lblFileNo.Text = dr.GetString(1);
            lblSerial.Text = slnoLbl.Text.Trim();
            if (!dr.IsDBNull(3))
            {
                txtTechTransNo.Text = dr.GetString(3);
            }
            else
            {
                txtTechTransNo.Text = "";
            }
            if (!dr.IsDBNull(4))
            {
                txtPartyName.Text = dr.GetString(4);
            }
            else
            {
                txtPartyName.Text = "";
            }
            if (!dr.IsDBNull(5))
            {
                txtPartyRefNo.Text = dr.GetString(5);
            }
            else
            {
                txtPartyRefNo.Text = "";
            }
            if (!dr.IsDBNull(6))
            {
                txtSubDate.Text  = dr.GetDateTime(6).ToShortDateString();
            }
            else
            {
                txtSubDate.Text="";
            }
            if (!dr.IsDBNull(7))
            {
                txtTransType.Text = dr.GetString(7);
            }
            else
            {
                txtTransType.Text = "";
            }
            if (!dr.IsDBNull(8))
            {
                txtTransDesc.Text = dr.GetString(8);
            }
            else
            {
                txtTransDesc.Text = "";
            }
            if (!dr.IsDBNull(9))
            {
                ddlPaymentGroup.Text = dr.GetString(9);
            }
            else
            {
                ddlPaymentGroup.Text = "";
            }
            if (!dr.IsDBNull(10))
            {
                txtPaymentDesc.Text = dr.GetString(10);
            }
            else
            {
                txtPaymentDesc.Text = "";
            }
            if (!dr.IsDBNull(11))
            {
                ddlCurrency.Text = dr.GetString(11);
            }
            else
            {
                ddlCurrency.Text = "";
            }
            if (!dr.IsDBNull(12))
            {
                txtCost.Text = dr.GetDecimal(12).ToString();
            }
            else
            {
                txtCost.Text = "";
            }

            if (!dr.IsDBNull(13))
            {
                txtExRate.Text = dr.GetDecimal(13).ToString();
            }
            else
            {
                txtExRate.Text = "";
            }
            if (!dr.IsDBNull(14))
            {
                txtCostINR.Text = dr.GetDecimal(14).ToString();
            }
            else
            {
                txtCostINR.Text = "";
            }
            if (!dr.IsDBNull(15))
            {
                txtPaymentDt.Text = dr.GetDateTime(15).ToShortDateString();
            }
            else
            {
                txtPaymentDt.Text = "";
            }
            if (!dr.IsDBNull(16))
            {
                txtPaymentRef.Text = dr.GetString(16);
            }
            else
            {
                txtPaymentRef.Text = "";
            }
            if (!dr.IsDBNull(17))
            {
                ddlYear.Text = dr.GetString(17);
            }
            else
            {
                ddlYear.Text = "";
            }
        }
    }
    protected void lvReceipt_ItemDeleting(object sender, ListViewDeleteEventArgs e)
    {
        try
        {
            SqlCommand cmd = new SqlCommand();
            ListViewItem item = lvReceipt.Items[e.ItemIndex];
            Label slnoLbl = (Label)item.FindControl("lblSlno");
            con.ConnectionString = ConfigurationManager.ConnectionStrings["PATENTCN"].ConnectionString;
            string sql = "delete from patentReceipt where fileno like '" + FileNo.Trim() + "' and slno =" + slnoLbl.Text.Trim();
            con.Open();
            cmd.CommandText = sql;
            cmd.CommandType = CommandType.Text;
            cmd.Connection = con;
            cmd.ExecuteNonQuery();
            ClientScript.RegisterStartupScript(GetType(), "Success", "<script>alert('This Record successfully Deleted')</script>");
            con.Close();
        }
        catch (Exception ex)
        {
            ClientScript.RegisterStartupScript(GetType(), "Error", "<script>alert('" + ex.Message.ToString() + "')</script>");
            con.Close();
            return;
        }
        Clear();
        Response.Redirect("ReceiptModify.aspx?fileno=" + FileNo);
    }
    protected void lvReceipt_ItemDataBound(object sender, ListViewItemEventArgs e)
    {
        LinkButton lb = e.Item.FindControl("lbtnDelete") as LinkButton;
        if (User.IsInRole("Admin") || User.IsInRole("Super User"))
        {
            lb.Visible = true;
        }
        else
        {
            lb.Visible = false;
        }   
    }
}
