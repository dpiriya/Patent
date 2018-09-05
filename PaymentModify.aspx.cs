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
public partial class PaymentModify : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection();
    HiddenField hSlNo = new HiddenField();
    HiddenField hEntryDt = new HiddenField();
    string FileNo;
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            FileNo = Request.QueryString["fileno"];
            lblTitle.InnerText = "File Number : " + FileNo.Trim();
            if (!this.IsPostBack)
            {
                SqlCommand cmd = new SqlCommand();
                System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-GB");
                con.ConnectionString = ConfigurationManager.ConnectionStrings["PATENTCN"].ConnectionString;
                string sql = "select slno,activity,party,PaymentamtINR from patentpayment where fileno like '" + FileNo.Trim() + "' order by slno desc";
                con.Open();
                cmd.CommandText = sql;
                cmd.CommandType = CommandType.Text;
                cmd.Connection = con;
                SqlDataReader dr;
                dr = cmd.ExecuteReader();
                DataTable dt = new DataTable();
                dt.Load(dr);
                if (dt.Rows.Count > 15)
                {
                    pageNumber.Visible = true;
                }
                else
                {
                    pageNumber.Visible = false;
                }
                Session["PaymentModify"] = dt;
                gvPayment.DataSource = dt;
                gvPayment.DataBind();
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
                
                sql = "select * from (select attorneyName from Attorney union select attorneyName from ipAttorney) as t order by t.attorneyName";
                cmd.CommandText = sql;
                dr = cmd.ExecuteReader();
                ddlParty.Items.Add("");
                while (dr.Read())
                {
                    ddlParty.Items.Add(dr.GetString(0));
                }
                dr.Close();
                string tYear;
                ddlYear.Items.Add("");
                int lYear;
                if (DateTime.Now.Month >= 4) lYear = DateTime.Now.Year; else lYear = DateTime.Now.Year - 1;
                for (int i = lYear; i >=1980 ; i--)
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

                sql = "select ItemList from listitemMaster where Category like 'CostGroup' order by ItemList";
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
            //Clear();
        }
        catch
        {
            Response.Redirect("Payment.aspx");
        }
    }
    public void Clear()
    {
        lblFileNo.Text= "";
        lblSerial.Text = "";
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
    }
    protected void imgBtnClear_Click(object sender, ImageClickEventArgs e)
    {
        Clear();
    }
    protected void imgBtnSubmit_Click(object sender, ImageClickEventArgs e)
    {
        if (lblFileNo.Text.Trim() == "")
        {
            return;
        }

        try
        {
            SqlCommand cmd = new SqlCommand();
            System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-GB");
            con.ConnectionString = ConfigurationManager.ConnectionStrings["PATENTCN"].ConnectionString;
            string sql = "update patentpayment set PType = case when @PType='' then null else @PType end, " +
            "Country = case when @Country='' then null else @Country end, " +
            "Activity = case when @Activity='' then null else @Activity end, " +
            "PaymentOrChequeDt = case when @PaymentOrChequeDt='' then null else convert(smalldatetime,@PaymentOrChequeDt,103) end, " +
            "PaymentRefOrChequeNo = case when @PaymentRefOrChequeNo='' then null else @PaymentRefOrChequeNo end, " +
            "PaymentAmtINR = @PaymentAmtINR,Currency=case when @Currency ='' then null else @Currency end,PaymentAmtForeign=@PaymentAmtForeign,ExRate = case when @ExRate='' then null else @ExRate end, " +
            "Party =case when @Party='' then null else @Party end, " +
            "Year = case when @Year='' then null else @Year end, " +
            "costGroup =case when @costGroup='' then null else @costGroup end, " +
            "Remark =case when @Remark='' then null else @Remark end, " +
            "InvoiceNo= case when @InvoiceNo='' then null else @InvoiceNo end, " +
            "InvoiceDt= case when @InvoiceDt='' then null else convert(smalldatetime,@InvoiceDt,103) end " +
            "where fileno= '" + lblFileNo.Text.Trim() + "' and slno = " + lblSerial.Text.Trim();

            cmd.CommandText = sql;
            cmd.CommandType = CommandType.Text;
            cmd.Connection = con;
            con.Open();
            
            SqlParameter pm1 = new SqlParameter();
            pm1.ParameterName = "@PType";
            pm1.SourceColumn = "PType";
            pm1.Value = ddlPMode.Text.Trim();
            pm1.DbType = DbType.String;
            pm1.Direction = ParameterDirection.Input;

            SqlParameter pm2 = new SqlParameter();
            pm2.ParameterName = "@country";
            pm2.SourceColumn = "country";
            pm2.Value = ddlCountry.Text.Trim();
            pm2.DbType = DbType.String;
            pm2.Direction = ParameterDirection.Input;

            SqlParameter pm3 = new SqlParameter();
            pm3.ParameterName = "@Activity";
            pm3.SourceColumn = "Activity";
            pm3.Value = txtDescription.Text.Trim();
            pm3.DbType = DbType.String;
            pm3.Direction = ParameterDirection.Input;

            SqlParameter pm4 = new SqlParameter();
            pm4.ParameterName = "@PaymentOrChequeDt";
            pm4.SourceColumn = "PaymentOrChequeDt";
            pm4.Value = txtChequeDate.Text.Trim();
            pm4.DbType = DbType.String;
            pm4.Direction = ParameterDirection.Input;

            SqlParameter pm5 = new SqlParameter();
            pm5.ParameterName = "@PaymentRefOrChequeNo";
            pm5.SourceColumn = "PaymentRefOrChequeNo";
            pm5.Value = txtChequeNo.Text.Trim();
            pm5.DbType = DbType.String;
            pm5.Direction = ParameterDirection.Input;

            SqlParameter pm6 = new SqlParameter();
            pm6.ParameterName = "@PaymentAmtINR";
            pm6.SourceColumn = "PaymentAmtINR";
            pm6.Value = txtChequeAmt.Text.Trim() == "" ? 0 : Convert.ToDecimal(txtChequeAmt.Text.Trim());
            pm6.DbType = DbType.Decimal;
            pm6.Direction = ParameterDirection.Input;

            SqlParameter pm7 = new SqlParameter();
            pm7.ParameterName = "@ExRate";
            pm7.SourceColumn = "ExRate";
            pm7.Value = txtExRate.Text.Trim();
            pm7.DbType = DbType.String;
            pm7.Direction = ParameterDirection.Input;

            SqlParameter pm8 = new SqlParameter();
            pm8.ParameterName = "@Party";
            pm8.SourceColumn = "Party";
            pm8.Value = ddlParty.Text.Trim();
            pm8.DbType = DbType.String;
            pm8.Direction = ParameterDirection.Input;

            SqlParameter pm9 = new SqlParameter();
            pm9.ParameterName = "@Year";
            pm9.SourceColumn = "Year";
            pm9.Value = ddlYear.Text.Trim();
            pm9.DbType = DbType.String;
            pm9.Direction = ParameterDirection.Input;

            SqlParameter pm10 = new SqlParameter();
            pm10.ParameterName = "@costGroup";
            pm10.SourceColumn = "costGroup";
            pm10.Value = ddlCostGrp.Text.Trim();
            pm10.DbType = DbType.String;
            pm10.Direction = ParameterDirection.Input;

            SqlParameter pm11 = new SqlParameter();
            pm11.ParameterName = "@Remark";
            pm11.SourceColumn = "Remark";
            pm11.Value = txtRemark.Text.Trim();
            pm11.DbType = DbType.String;
            pm11.Direction = ParameterDirection.Input;

            SqlParameter pm12 = new SqlParameter();
            pm12.ParameterName = "@currency";
            pm12.SourceColumn = "Currency";
            pm12.Value = ddlCurrency.SelectedItem.Value;
            pm12.DbType = DbType.String;
            pm12.Direction = ParameterDirection.Input;

            SqlParameter pm13 = new SqlParameter();
            pm13.ParameterName = "@PaymentAmtForeign";
            pm13.SourceColumn = "PaymentAmtForeign";
            pm13.Value = txtPayment.Text.Trim() == "" ? 0 : Convert.ToDecimal(txtPayment.Text.Trim());
            pm13.DbType = DbType.String;
            pm13.Direction = ParameterDirection.Input;

            SqlParameter pm14 = new SqlParameter();
            pm14.ParameterName = "@InvoiceNo";
            pm14.SourceColumn = "InvoiceNo";
            pm14.Value = txtInvoice.Text.Trim();
            pm14.DbType = DbType.String;
            pm14.Direction = ParameterDirection.Input;

            SqlParameter pm15 = new SqlParameter();
            pm15.ParameterName = "@InvoiceDt";
            pm15.SourceColumn = "InvoiceDt";
            pm15.Value = txtInvoiceDt.Text.Trim();
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

            ClientScript.RegisterStartupScript(GetType(), "Success", "<script>alert('This Record successfully Updated')</script>");
            con.Close();
        }
        catch (Exception ex)
        {
            ClientScript.RegisterStartupScript(GetType(), "Error", "<script>alert('" + ex.Message.ToString() + "')</script>");
            con.Close();
            return;
        }
        Clear();
        Response.Redirect("PaymentModify.aspx?fileno="+ FileNo); 
    }
    protected void gvPayment_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        GridViewRow item = gvPayment.Rows[e.NewSelectedIndex];
        int slnoLbl = Convert.ToInt32(item.Cells[0].Text.Trim());
        Clear();
        SqlCommand cmd = new SqlCommand();
        System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-GB");
        con.ConnectionString = ConfigurationManager.ConnectionStrings["PATENTCN"].ConnectionString;
        string sql = "select FileNo,PType,SlNo,Country,CostGroup,Activity,InvoiceNo,InvoiceDt,PaymentOrChequeDt,PaymentRefOrChequeNo,PaymentAmtINR,Currency,PaymentAmtForeign,ExRate,Party,Year,Remark from patentPayment where fileno like '" + FileNo.Trim() + "' and slno =" + slnoLbl;
        con.Open();
        cmd.CommandText = sql;
        cmd.CommandType = CommandType.Text;
        cmd.Connection = con;
        SqlDataReader dr;
        dr = cmd.ExecuteReader();
        while (dr.Read())
        {
            lblFileNo.Text = dr.GetString(0);
            if (!dr.IsDBNull(1))
            {
                ddlPMode.Text = dr.GetString(1);
            }
            else
            {
                ddlPMode.Text = "";
            }
            lblSerial.Text = dr.GetInt32(2).ToString();
            if (!dr.IsDBNull(3))
            {
                ddlCountry.Text = dr.GetString(3);
            }
            else
            {
                ddlCountry.Text = "";
            }
            if (!dr.IsDBNull(4))
            {
                ddlCostGrp.Text = dr.GetString(4);
            }
            else
            {
                ddlCostGrp.Text = "";
            }
            if (!dr.IsDBNull(5))
            {
                txtDescription.Text = dr.GetString(5);
            }
            else
            {
                txtDescription.Text = "";
            }
            if (!dr.IsDBNull(6))
            {
                txtInvoice.Text = dr.GetString(6);
            }
            else
            {
                txtInvoice.Text = "";
            }
            if (!dr.IsDBNull(7))
            {
                txtInvoiceDt.Text = dr.GetDateTime(7).ToShortDateString();
            }
            else
            {
                txtInvoiceDt.Text = "";
            }
            if (!dr.IsDBNull(8))
            {
                txtChequeDate.Text = dr.GetDateTime(8).ToShortDateString();
            }
            else
            {
                txtChequeDate.Text = "";
            }
            if (!dr.IsDBNull(9))
            {
                txtChequeNo.Text = dr.GetString(9);
            }
            else
            {
                txtChequeNo.Text = "";
            }

            if (!dr.IsDBNull(10))
            {
                txtChequeAmt.Text = dr.GetDecimal(10).ToString();
            }
            else
            {
                txtChequeAmt.Text = "";
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
                txtPayment.Text = dr.GetDecimal(12).ToString();
            }
            else
            {
                txtPayment.Text = "";
            }
            if (!dr.IsDBNull(13))
            {
                txtExRate.Text = dr.GetString(13);
            }
            else
            {
                txtExRate.Text = "";
            }
            if (!dr.IsDBNull(14))
            {
                ddlParty.Text = dr.GetString(14);
            }
            else
            {
                ddlParty.Text = "";
            }


            if (!dr.IsDBNull(15))
            {
                ddlYear.Text = dr.GetString(15);
            }
            else
            {
                ddlYear.Text = "";
            }

            if (!dr.IsDBNull(16))
            {
                txtRemark.Text = dr.GetString(16);
            }
            else
            {
                txtRemark.Text = "";
            }

        }
    }
    protected void gvPayment_Sorting(object sender, GridViewSortEventArgs e)
    {
        DataTable dt = Session["PaymentModify"] as DataTable;
        if (dt != null)
        {
            dt.DefaultView.Sort = e.SortExpression + " " + getSortDirection(e.SortExpression);

            gvPayment.DataSource = dt;
            gvPayment.DataBind();
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
    protected void gvPayment_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvPayment.PageIndex = e.NewPageIndex;
        gvPayment.DataSource = Session["PaymentModify"];
        gvPayment.DataBind();
    }
    protected void gvPayment_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            SqlCommand cmd = new SqlCommand();
            GridViewRow item = gvPayment.Rows[e.RowIndex];
            int slnoLbl = Convert.ToInt32(item.Cells[0].Text.Trim());
            con.ConnectionString = ConfigurationManager.ConnectionStrings["PATENTCN"].ConnectionString;
            string sql = "delete from patentpayment where fileno like '" + FileNo.Trim() + "' and slno =" + slnoLbl;
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
        Response.Redirect("PaymentModify.aspx?fileno=" + FileNo);
    }
    protected void gvPayment_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        LinkButton lb = e.Row.FindControl("lbtnDelete") as LinkButton;
        if (lb != null)
        {
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
}
