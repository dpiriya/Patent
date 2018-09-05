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


public partial class PaymentReport : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection();
    string id;
    protected void Page_Load(object sender, EventArgs e)
    {
        id = Request.QueryString["id"];
        if (id == "PaymentReport")
        {
            this.Title = "PAYMENT REPORT";
            lblTitle.InnerText = "PAYMENT REPORT";
        }
        else if (id == "PaymentModify")
        {
            this.Title = "PAYMENT MODIFICATION";
            lblTitle.InnerText = "PAYMENT MODIFICATION";
        }
        else if (id == "IPReport")
        {
            this.Title = "International Patent Report";
            lblTitle.InnerText = "International Patent Report";
        }
        else if (id == "ReceiptReport")
        {
            this.Title = "RECEIPT REPORT";
            lblTitle.InnerText = "RECEIPT REPORT";
        }
        else if (id == "ReceiptModify")
        {
            this.Title = "RECEIPT MODIFICATION";
            lblTitle.InnerText = "RECEIPT MODIFICATION";
        }
        else if (id == "Contract")
        {
            this.Title = "Contract Report";
            lblTitle.InnerText = "CONTRACT REPORT";
            lblFile.InnerText = "Contract Number";
        }
        if (!this.IsPostBack)
        {
            con.ConnectionString = ConfigurationManager.ConnectionStrings["PATENTCN"].ConnectionString;
            SqlCommand cmd = new SqlCommand();
            string sql = "";
            if (id == "ReceiptModify" || id == "ReceiptReport")
            {
                sql = "select distinct(cast(fileno as int)) from patentReceipt order by cast(fileno as int) desc";
            }
            else if (id == "PaymentModify" || id == "PaymentReport")
            {
                sql = "select distinct(cast(fileno as int)) from patentPayment order by cast(fileno as int) desc";
            }
            else if (id == "IPReport")
            {
                sql = "select distinct(cast(fileno as int)) from INTERNATIONAL order by cast(fileno as int) desc";
            }
            SqlDataReader dr;
            cmd.CommandType = CommandType.Text;
            cmd.Connection = con;
            cmd.CommandText = sql;
            con.Open();
            dr = cmd.ExecuteReader();
            ddlFileNo.Items.Clear();
            ddlFileNo.Items.Add("");
            if (id == "Contract")
            {
                while (dr.Read())
                {
                    ddlFileNo.Items.Add(dr.GetString(0));
                }
            }
            else
            {
                while (dr.Read())
                {
                    ddlFileNo.Items.Add(dr.GetInt32(0).ToString());
                }
            }
            dr.Close();
            con.Close();
        }

    }
    protected void imgBtnSubmit_Click(object sender, ImageClickEventArgs e)
    {
        
        if (id == "PaymentReport")
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "Payment_window", string.Format("void(window.open('{0}','child_Payment','menubar=0,toolbar=0,resizable=1,scrollbars=1'));", "rptView.aspx?id=PaymentReport&fileno=" + ddlFileNo.Text.Trim()), true);
        }
        else if (id == "PaymentModify")
        {
            Response.Redirect("PaymentModify.aspx?fileno=" + ddlFileNo.Text.Trim());
        }
        else if (id == "IPReport")
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "International_window", string.Format("void(window.open('{0}','child_ipReport','menubar=0,toolbar=0,resizable=1,scrollbars=1'));", "rptView.aspx?id=IPReport&fileno=" + ddlFileNo.Text.Trim()), true);
        }
        else if (id == "ReceiptReport")
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "Receipt_window", string.Format("void(window.open('{0}','child_Receipt','menubar=0,toolbar=0,resizable=1,scrollbars=1'));", "rptView.aspx?id=ReceiptReport&fileno=" + ddlFileNo.Text.Trim()), true);
        }
        else if (id == "ReceiptModify")
        {
            Response.Redirect("ReceiptModify.aspx?fileno=" + ddlFileNo.Text.Trim());
        }        
    }
    protected void imgBtnClear_Click(object sender, ImageClickEventArgs e)
    {
        ddlFileNo.Text = "";
    }
}
