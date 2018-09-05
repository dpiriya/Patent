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

public partial class Receipt : System.Web.UI.Page
{
    SqlConnection con= new SqlConnection ();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.IsPostBack)
        {
            con.ConnectionString = ConfigurationManager.ConnectionStrings["PATENTCN"].ConnectionString;
            SqlCommand cmd = new SqlCommand();
            string sql = "select fileno from patdetails order by cast(fileno as int) desc";
            SqlDataReader dr;
            cmd.CommandType = CommandType.Text;
            cmd.Connection = con;
            cmd.CommandText = sql;
            con.Open();
            dr = cmd.ExecuteReader();
            ddlFileNo.Items.Clear();
            ddlFileNo.Items.Add("");
            while (dr.Read())
            {
                ddlFileNo.Items.Add(dr.GetString(0));
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
            
            sql = "select currencyCode,countryName from currency where status is not null order by status";
            cmd.CommandText = sql;
            dr = cmd.ExecuteReader();
            ddlCurrency.Items.Add("");
            while (dr.Read())
            {
                ddlCurrency.Items.Add(new ListItem(dr.GetString(0) + " - " + dr.GetString(1), dr.GetString(0)));
            }
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
            imgBtnSubmit.Visible = (!User.IsInRole("View"));
        }
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

    protected void imgBtnClear_Click(object sender, ImageClickEventArgs e)
    {
        ddlFileNo.Text = "";
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
        ddlFileNo.Enabled = true;
    }
    protected void imgBtnSubmit_Click(object sender, ImageClickEventArgs e)
    {
        if (ddlFileNo.Text.Trim() == "")
        {
            ClientScript.RegisterStartupScript(GetType(), "Information", "<script>alert('File Number can not be Empty')</script>");
            return;
        }
        try
        {
            SqlCommand cmd = new SqlCommand();
            System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-GB");
            con.ConnectionString = ConfigurationManager.ConnectionStrings["PATENTCN"].ConnectionString;
            string sql = "insert into patentreceipt (EntryDt,FileNo,TechTransferNo,Party,PartyRefNo,SubmissionDt,TransType,TransDescription,PaymentGroup,PaymentDescription,Currency,ForeignCost,ExRate,Cost_Rs,PaymentDate,PaymentRef,Year,SlNo) " +
            "values (convert(smalldatetime,@EntryDt,103),@FileNo,case when @TechTransferNo='' then null else @TechTransferNo end,case when @Party='' then null else @Party end," +
            "case when @PartyRefNo='' then null else @PartyRefNo end,case when @SubmissionDt='' then null else convert(smalldatetime,@SubmissionDt,103) end,case when @TransType='' then null else @TransType end," +
            "case when @TransDescription='' then null else @TransDescription end,case when @PaymentGroup='' then null else @PaymentGroup end, case when @PaymentDescription='' then null else @PaymentDescription end," +
            "case when @Currency='' then null else @Currency end,@ForeignCost,@ExRate,@Cost_Rs,case when @PaymentDate='' then null else convert(smalldatetime,@PaymentDate,103) end, " +
            "case when @PaymentRef='' then null else @PaymentRef end,case when @Year='' then null else @Year end,@SlNo)";

            cmd.CommandText = sql;
            cmd.CommandType = CommandType.Text;
            cmd.Connection = con;
            con.Open();
            SqlParameter pm1 = new SqlParameter();
            pm1.ParameterName = "@EntryDt";
            pm1.SourceColumn = "EntryDt";
            pm1.Value = DateTime.Now.ToShortDateString();
            pm1.DbType = DbType.String;
            pm1.Direction = ParameterDirection.Input;

            SqlParameter pm2 = new SqlParameter();
            pm2.ParameterName = "@fileno";
            pm2.SourceColumn = "fileno";
            pm2.Value = ddlFileNo.Text.Trim();
            pm2.DbType = DbType.String;
            pm2.Direction = ParameterDirection.Input;

            SqlParameter pm3 = new SqlParameter();
            pm3.ParameterName = "@TechTransferNo";
            pm3.SourceColumn = "TechTransferNo";
            pm3.Value = txtTechTransNo.Text.Trim();
            pm3.DbType = DbType.String;
            pm3.Direction = ParameterDirection.Input;

            SqlParameter pm4 = new SqlParameter();
            pm4.ParameterName = "@Party";
            pm4.SourceColumn = "Party";
            pm4.Value = txtPartyName.Text.Trim();
            pm4.DbType = DbType.String;
            pm4.Direction = ParameterDirection.Input;

            SqlParameter pm5 = new SqlParameter();
            pm5.ParameterName = "@PartyRefNo";
            pm5.SourceColumn = "PartyRefNo";
            pm5.Value = txtPartyRefNo.Text.Trim();
            pm5.DbType = DbType.String;
            pm5.Direction = ParameterDirection.Input;

            SqlParameter pm6 = new SqlParameter();
            pm6.ParameterName = "@SubmissionDt";
            pm6.SourceColumn = "SubmissionDt";
            pm6.Value = txtSubDate.Text.Trim();
            pm6.DbType = DbType.String;
            pm6.Direction = ParameterDirection.Input;

            SqlParameter pm7 = new SqlParameter();
            pm7.ParameterName = "@TransType";
            pm7.SourceColumn = "TransType";
            pm7.Value = txtTransType.Text.Trim();
            pm7.DbType = DbType.String;
            pm7.Direction = ParameterDirection.Input;

            SqlParameter pm8 = new SqlParameter();
            pm8.ParameterName = "@TransDescription";
            pm8.SourceColumn = "TransDescription";
            pm8.Value = txtTransDesc.Text.Trim();
            pm8.DbType = DbType.String;
            pm8.Direction = ParameterDirection.Input;

            SqlParameter pm9 = new SqlParameter();
            pm9.ParameterName = "@PaymentGroup";
            pm9.SourceColumn = "PaymentGroup";
            pm9.Value = ddlPaymentGroup.SelectedItem.Value.Trim();
            pm9.DbType = DbType.String;
            pm9.Direction = ParameterDirection.Input;

            SqlParameter pm10 = new SqlParameter();
            pm10.ParameterName = "@PaymentDescription";
            pm10.SourceColumn = "PaymentDescription";
            pm10.Value = txtPaymentDesc.Text.Trim();
            pm10.DbType = DbType.String;
            pm10.Direction = ParameterDirection.Input;

            SqlParameter pm11 = new SqlParameter();
            pm11.ParameterName = "@Currency";
            pm11.SourceColumn = "Currency";
            pm11.Value = ddlCurrency.SelectedItem.Value.Trim();
            pm11.DbType = DbType.String;
            pm11.Direction = ParameterDirection.Input;

            SqlParameter pm12 = new SqlParameter();
            pm12.ParameterName = "@ForeignCost";
            pm12.SourceColumn = "ForeignCost";
            pm12.Value = txtCost.Text.Trim() == "" ? 0 : Convert.ToDecimal(txtCost.Text.Trim());
            pm12.DbType = DbType.Decimal;
            pm12.Direction = ParameterDirection.Input;

            SqlParameter pm13 = new SqlParameter();
            pm13.ParameterName = "@ExRate";
            pm13.SourceColumn = "ExRate";
            pm13.Value = txtExRate.Text.Trim() == "" ? 0 : Convert.ToDecimal(txtExRate.Text.Trim());
            pm13.DbType = DbType.Decimal;
            pm13.Direction = ParameterDirection.Input;

            SqlParameter pm14 = new SqlParameter();
            pm14.ParameterName = "@Cost_Rs";
            pm14.SourceColumn = "Cost_Rs";
            pm14.Value = txtCostINR.Text.Trim() == "" ? 0 : Convert.ToDecimal(txtCostINR.Text.Trim());
            pm14.DbType = DbType.String;
            pm14.Direction = ParameterDirection.Input;

            SqlParameter pm15 = new SqlParameter();
            pm15.ParameterName = "@PaymentDate";
            pm15.SourceColumn = "PaymentDate";
            pm15.Value = txtPaymentDt.Text.Trim();
            pm15.DbType = DbType.String;
            pm15.Direction = ParameterDirection.Input;

            SqlParameter pm16 = new SqlParameter();
            pm16.ParameterName = "@PaymentRef";
            pm16.SourceColumn = "PaymentRef";
            pm16.Value = txtPaymentRef.Text.Trim();
            pm16.DbType = DbType.String;
            pm16.Direction = ParameterDirection.Input;

            SqlParameter pm17 = new SqlParameter();
            pm17.ParameterName = "@Year";
            pm17.SourceColumn = "Year";
            pm17.Value = ddlYear.SelectedItem.Value.Trim();
            pm17.DbType = DbType.String;
            pm17.Direction = ParameterDirection.Input;

            SqlConnection con1 = new SqlConnection();
            con1.ConnectionString = ConfigurationManager.ConnectionStrings["PATENTCN"].ConnectionString;
            string sql1 = "select case when max(slno)is Null then 1 else max(slno)+1 end from patentreceipt where fileno='" + ddlFileNo.Text.Trim() + "'";
            SqlCommand cmd1 = new SqlCommand();
            cmd1.CommandText = sql1;
            cmd1.CommandType = CommandType.Text;
            cmd1.Connection = con1;
            con1.Open();
            int slNumber = Convert.ToInt32(cmd1.ExecuteScalar());
            con1.Close();

            SqlParameter pm18 = new SqlParameter();
            pm18.ParameterName = "@SlNo";
            pm18.SourceColumn = "SlNo";
            pm18.Value = slNumber;
            pm18.DbType = DbType.Int32;
            pm18.Direction = ParameterDirection.Input;


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

            cmd.ExecuteNonQuery();

            ClientScript.RegisterStartupScript(GetType(), "Success", "<script>alert('This Record successfully Added')</script>");
            con.Close();
        }
        catch (Exception ex)
        {
            ClientScript.RegisterStartupScript(GetType(), "Error", "<script>alert('" + ex.Message.ToString() + "')</script>");
            con.Close();
            return;
        }
        imgBtnClear_Click(sender, e);
    }
    protected void ddlFileNo_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlFileNo.Enabled = false;
    }
}
