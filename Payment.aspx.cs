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

public partial class Payment : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!User.IsInRole("Intern"))
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

                sql = "select ItemList from listitemMaster where Category like 'Consignee' order by ItemList";
                cmd.CommandText = sql;
                dr = cmd.ExecuteReader();
                ddlPMode.DataTextField = "ItemList";
                ddlPMode.DataValueField = "ItemList";
                ddlPMode.DataSource = dr;
                ddlPMode.DataBind();
                ddlPMode.Items.Insert(0, new ListItem("", ""));
                dr.Close();

                sql = "select * from (select attorneyName from Attorney union select attorneyName from ipAttorney ) as t order by t.attorneyName";
                cmd.CommandText = sql;
                dr = cmd.ExecuteReader();
                ddlParty.Items.Add("");
                while (dr.Read())
                {
                    ddlParty.Items.Add(dr.GetString(0));
                }
                dr.Close();
                string tYear;
                int lYear;
                if (DateTime.Now.Month >= 4) lYear = DateTime.Now.Year; else lYear = DateTime.Now.Year - 1;
                ddlYear.Items.Add("");
                for (int i = lYear; i >= 1980; i--)
                {
                    tYear = i.ToString() + " - " + (i + 1).ToString();
                    ddlYear.Items.Add(tYear);
                }
                sql = "select Country from ipCountry";
                cmd.CommandText = sql;
                dr = cmd.ExecuteReader();
                ddlCountry.Items.Add("");
                ddlCountry.Items.Add("India");
                while (dr.Read())
                {
                    ddlCountry.Items.Add(dr.GetString(0));
                }
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

                sql = "select ItemList from listitemMaster where Category like 'CostGroup' and Grouping='Payment' order by ItemList";
                cmd.CommandText = sql;
                dr = cmd.ExecuteReader();
                ddlCostGrp.DataTextField = "ItemList";
                ddlCostGrp.DataValueField = "ItemList";
                ddlCostGrp.DataSource = dr;
                ddlCostGrp.DataBind();
                ddlCostGrp.Items.Insert(0, new ListItem("", ""));
                dr.Close();
                con.Close();
                imgBtnSubmit.Visible = (!User.IsInRole("View"));
            }
        }
        else
        {
            Server.Transfer("Unauthorized.aspx");
        }
    }

    public void Clear()
    {
        ddlFileNo.Text = "";
        ddlPMode.Text = "";
        ddlCostGrp.Text = "";
        txtDescription.Text = "";
        ddlParty.Text = "";
        txtInvoice.Text = "";
        txtInvoiceDt.Text = "";
        txtChequeNo.Text = "";
        txtChequeDate.Text = "";
        txtChequeAmt.Text = "";
        ddlCurrency.Text = "";
        txtPayment.Text = "";
        txtExRate.Text = "";
        ddlYear.Text = "";
        ddlCountry.Text = "";
        txtRemark.Text = "";
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
            string sql = "insert into patentpayment (EntryDt,FileNO,PType,SlNo,Country,Activity,PaymentOrChequeDt,PaymentRefOrChequeNo,PaymentAmtINR,ExRate,Party,Year,costGroup,Remark,paymentAmtForeign,currency,InvoiceNo,InvoiceDt) " +
            "values (convert(smalldatetime,@EntryDt,103),@FileNo,case when @PType='' then null else @PType end,@SlNo,case when @Country='' then null else @Country end," +
            "case when @Activity='' then null else @Activity end,case when @PaymentOrChequeDt='' then null else convert(smalldatetime,@PaymentOrChequeDt,103) end,case when @PaymentRefOrChequeNo='' then null else @PaymentRefOrChequeNo end," +
            "@PaymentAmtINR,case when @ExRate='' then null else @ExRate end,case when @Party='' then null else @Party end, case when @Year='' then null else @Year end,case when @costGroup='' then null else @costGroup end,case when @Remark='' then null else @Remark end, " +
            "@paymentAmtForeign,case when @currency='' then null else @currency end, case when @InvoiceNo='' then null else @InvoiceNo end, case when @InvoiceDt='' then null else convert(smalldatetime,@InvoiceDt,103) end)";

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
            pm3.ParameterName = "@PType";
            pm3.SourceColumn = "PType";
            pm3.Value = ddlPMode.Text.Trim();
            pm3.DbType = DbType.String;
            pm3.Direction = ParameterDirection.Input;

            SqlConnection con1 = new SqlConnection();
            con1.ConnectionString = ConfigurationManager.ConnectionStrings["PATENTCN"].ConnectionString;
            string sql1 = "select case when max(slno)is Null then 1 else max(slno)+1 end from patentpayment where fileno='" + ddlFileNo.Text.Trim() + "'";
            SqlCommand cmd1 = new SqlCommand();
            cmd1.CommandText = sql1;
            cmd1.CommandType = CommandType.Text;
            cmd1.Connection = con1;
            con1.Open();
            int slNumber = Convert.ToInt32(cmd1.ExecuteScalar());
            con1.Close();

            SqlParameter pm4 = new SqlParameter();
            pm4.ParameterName = "@SlNo";
            pm4.SourceColumn = "SlNo";
            pm4.Value = slNumber;
            pm4.DbType = DbType.Int32;
            pm4.Direction = ParameterDirection.Input;

            SqlParameter pm5 = new SqlParameter();
            pm5.ParameterName = "@country";
            pm5.SourceColumn = "country";
            pm5.Value = ddlCountry.Text.Trim();
            pm5.DbType = DbType.String;
            pm5.Direction = ParameterDirection.Input;

            SqlParameter pm6 = new SqlParameter();
            pm6.ParameterName = "@Activity";
            pm6.SourceColumn = "Activity";
            pm6.Value = txtDescription.Text.Trim();
            pm6.DbType = DbType.String;
            pm6.Direction = ParameterDirection.Input;

            SqlParameter pm7 = new SqlParameter();
            pm7.ParameterName = "@PaymentOrChequeDt";
            pm7.SourceColumn = "PaymentOrChequeDt";
            pm7.Value = txtChequeDate.Text.Trim();
            pm7.DbType = DbType.String;
            pm7.Direction = ParameterDirection.Input;

            SqlParameter pm8 = new SqlParameter();
            pm8.ParameterName = "@PaymentRefOrChequeNo";
            pm8.SourceColumn = "PaymentRefOrChequeNo";
            pm8.Value = txtChequeNo.Text.Trim();
            pm8.DbType = DbType.String;
            pm8.Direction = ParameterDirection.Input;

            SqlParameter pm9 = new SqlParameter();
            pm9.ParameterName = "@PaymentAmtINR";
            pm9.SourceColumn = "PaymentAmtINR";
            pm9.Value = txtChequeAmt.Text.Trim() == "" ? 0 : Convert.ToDecimal(txtChequeAmt.Text.Trim());
            pm9.DbType = DbType.Decimal;
            pm9.Direction = ParameterDirection.Input;

            SqlParameter pm10 = new SqlParameter();
            pm10.ParameterName = "@ExRate";
            pm10.SourceColumn = "ExRate";
            pm10.Value = txtExRate.Text.Trim();
            pm10.DbType = DbType.String;
            pm10.Direction = ParameterDirection.Input;

            SqlParameter pm11 = new SqlParameter();
            pm11.ParameterName = "@Party";
            pm11.SourceColumn = "Party";
            pm11.Value = ddlParty.Text.Trim();
            pm11.DbType = DbType.String;
            pm11.Direction = ParameterDirection.Input;

            SqlParameter pm12 = new SqlParameter();
            pm12.ParameterName = "@Year";
            pm12.SourceColumn = "Year";
            pm12.Value = ddlYear.Text.Trim();
            pm12.DbType = DbType.String;
            pm12.Direction = ParameterDirection.Input;

            SqlParameter pm13 = new SqlParameter();
            pm13.ParameterName = "@costGroup";
            pm13.SourceColumn = "costGroup";
            pm13.Value = ddlCostGrp.Text.Trim();
            pm13.DbType = DbType.String;
            pm13.Direction = ParameterDirection.Input;

            SqlParameter pm14 = new SqlParameter();
            pm14.ParameterName = "@Remark";
            pm14.SourceColumn = "Remark";
            pm14.Value = txtRemark.Text.Trim();
            pm14.DbType = DbType.String;
            pm14.Direction = ParameterDirection.Input;

            SqlParameter pm15 = new SqlParameter();
            pm15.ParameterName = "@paymentAmtForeign";
            pm15.SourceColumn = "paymentAmtForeign";
            pm15.Value = txtPayment.Text.Trim() == "" ? 0 : Convert.ToDecimal(txtPayment.Text.Trim());
            pm15.DbType = DbType.Decimal;
            pm15.Direction = ParameterDirection.Input;

            SqlParameter pm16 = new SqlParameter();
            pm16.ParameterName = "@currency";
            pm16.SourceColumn = "currency";
            pm16.Value = ddlCurrency.SelectedItem.Value.Trim();
            pm16.DbType = DbType.String;
            pm16.Direction = ParameterDirection.Input;

            SqlParameter pm17 = new SqlParameter();
            pm17.ParameterName = "@InvoiceNo";
            pm17.SourceColumn = "InvoiceNo";
            pm17.Value = txtInvoice.Text.Trim();
            pm17.DbType = DbType.String;
            pm17.Direction = ParameterDirection.Input;

            SqlParameter pm18 = new SqlParameter();
            pm18.ParameterName = "@InvoiceDt";
            pm18.SourceColumn = "InvoiceDt";
            pm18.Value = txtInvoiceDt.Text.Trim();
            pm18.DbType = DbType.String;
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
    protected void imgBtnClear_Click(object sender, ImageClickEventArgs e)
    {
        Clear();
    }

    protected void ddlFileNo_SelectedIndexChanged1(object sender, EventArgs e)
    {
        ddlFileNo.Enabled = false;
    }
}
