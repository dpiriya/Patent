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

public partial class PaymentSearch : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.IsPostBack)
        {
            con.ConnectionString = ConfigurationManager.ConnectionStrings["PATENTCN"].ConnectionString;
            SqlCommand cmd = new SqlCommand();
            string sql = "select column_name from information_schema.columns where table_name='PatentPayment' order by ordinal_position";
            SqlDataReader dr;
            cmd.CommandType = CommandType.Text;
            cmd.Connection = con;
            cmd.CommandText = sql;
            con.Open();
            dr = cmd.ExecuteReader();
            lstColumn.DataTextField = "column_name";
            lstColumn.DataValueField = "column_name";
            lstColumn.DataSource = dr;
            lstColumn.DataBind();
            dr.Close();
            dr = cmd.ExecuteReader();
            lstTable.DataTextField = "column_name";
            lstTable.DataValueField = "column_name";
            lstTable.DataSource = dr;
            lstTable.DataBind();
            con.Close();
        }
    }
    protected void btnAddVal_Click(object sender, EventArgs e)
    {
        if (txtValue.Text.Trim() != "")
        {
            string tmp = "";
            string lstcol = lstColumn.SelectedItem.Value.Trim();
            if (lstcol != "EntryDt" && lstcol != "InvoiceDt" && lstcol != "PaymentOrChequeDt" && lstcol != "PaymentAmtINR")
            {
                tmp = lstcol + " like " + "'%" + txtValue.Text.Trim() + "%'";
            }
            else if (lstcol == "PaymentAmtINR")
            {
                tmp = lstcol + " = " + txtValue.Text.Trim();
            }
            else if (lstcol == "EntryDt" || lstcol == "InvoiceDt" || lstcol == "PaymentOrChequeDt")
            {
                tmp = lstcol + " = " + "convert(smalldatetime,'" + txtValue.Text.Trim() + "',103)";
            }
            if (txtFilter.Text.Trim() == "")
            {
                txtFilter.Text = tmp;
            }
            else
            {
                txtFilter.Text = txtFilter.Text.Trim() + " and " + tmp;
            }
        }
    }
    protected void btnRemoveVal_Click(object sender, EventArgs e)
    {
        string filter = txtFilter.Text.Trim();
        if (filter != "")
        {
            int pos = filter.LastIndexOf("and");
            if (pos > 0)
            {
                filter = filter.Remove(pos);
            }
            txtFilter.Text = filter;
        }
    }
    protected void btnReport_Click(object sender, EventArgs e)
    {
        System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-GB");
        con.ConnectionString = ConfigurationManager.ConnectionStrings["PATENTCN"].ConnectionString;
        string sql = "";
        for (int i = 0; i < lstSelected.Items.Count; i++)
        {
            lstSelected.SelectedIndex = i;
            lstTable.SelectedIndex = Convert.ToInt32(lstSelected.SelectedItem.Value);
            if (sql == "")
            {
                sql = sql + lstTable.SelectedItem.Value.ToString();       //lstSelected.SelectedItem.Text.ToString();
            }
            else
            {
                sql = sql + "," + lstTable.SelectedItem.Value.ToString();  //lstSelected.SelectedItem.Text.ToString();
            }
        }

        if (sql == "") sql = "*";

        string dFilter = "";
        if (txtFilter.Text.Trim() != "")
        {
            dFilter = "where " + txtFilter.Text.Trim();
            sql = "select " + sql + " from PatentPayment " + dFilter + "order by fileno";
        }
        else
        {
            sql = "select " + sql + " from PatentPayment order by fileno";
        }
        SqlCommand cmd = new SqlCommand();
        SqlDataReader dr;
        cmd.CommandType = CommandType.Text;
        cmd.Connection = con;
        cmd.CommandText = sql;
        con.Open();
        dr = cmd.ExecuteReader();
        DataTable dt = new DataTable();
        dt.Load(dr);
        GenerateExcel ge = new GenerateExcel();
        ge.MakeExcel(dt, "Payment");
        con.Close(); 
    }
    protected void btnClear_Click(object sender, EventArgs e)
    {
        txtValue.Text = "";
        txtFilter.Text = "";
        lstSelected.Items.Clear();
        lstTable.Items.Clear();
        con.ConnectionString = ConfigurationManager.ConnectionStrings["PATENTCN"].ConnectionString;
        SqlCommand cmd = new SqlCommand();
        string sql = "select column_name from information_schema.columns where table_name='PatentPayment' order by ordinal_position";
        SqlDataReader dr;
        cmd.CommandType = CommandType.Text;
        cmd.Connection = con;
        cmd.CommandText = sql;
        con.Open();
        dr = cmd.ExecuteReader();
        lstTable.DataTextField = "column_name";
        lstTable.DataValueField = "column_name";
        lstTable.DataSource = dr;
        lstTable.DataBind();
        con.Close();
    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        if (lstTable.SelectedIndex != -1)
        {
            lstSelected.Items.Insert(lstSelected.Items.Count, new ListItem(lstTable.SelectedItem.Text, lstTable.SelectedIndex.ToString()));
            lstTable.Items.FindByText(lstTable.SelectedItem.Text.ToString()).Enabled = false;
            lstTable.ClearSelection();
        }
    }
    protected void btnRemove_Click(object sender, EventArgs e)
    {
        if (lstSelected.SelectedIndex != -1)
        {
            lstTable.Items.FindByText(lstSelected.SelectedItem.Text.ToString()).Enabled = true;
            lstSelected.Items.RemoveAt(lstSelected.SelectedIndex);
        }
    }
}
