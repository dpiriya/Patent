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

public partial class FileSearch : System.Web.UI.Page
{
    SqlConnection cnp = new SqlConnection();
    string[] tColumn = new[] { "Fileno", "Title", "Commercial", "First applicant", "Second applicant", "Inventor", "Department", "Request_Dt", "Application_no", "Filing_dt", "pat_no", "pat_dt", "Initial Filing", "Attorney", "Industry1", "Industry2" };
    string[] tColumnVal = new[] { "fileno", "title", "commercial", "FirstApplicant", "SecondApplicant", "Inventor1", "department", "request_dt", "applcn_no", "filing_dt", "pat_no", "pat_dt", "InitialFiling", "Attorney", "Industry1", "Industry2" };
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.IsPostBack)
        {
            cnp.ConnectionString = ConfigurationManager.ConnectionStrings["PATENTCN"].ConnectionString;
            SqlCommand cmd = new SqlCommand();
            string sql = "select column_name from information_schema.columns where table_name='patdetails' order by ordinal_position";
            SqlDataReader dr;
            cmd.CommandType = CommandType.Text;
            cmd.Connection = cnp;
            cmd.CommandText = sql;
            cnp.Open();
            dr = cmd.ExecuteReader();
            lstTable.DataTextField = "column_name";
            lstTable.DataValueField = "column_name";
            lstTable.DataSource = dr;
            lstTable.DataBind();
            dr.Close();
            dr = cmd.ExecuteReader();
            lstColumn.DataTextField = "column_name";
            lstColumn.DataValueField = "column_name";
            lstColumn.DataSource = dr;
            lstColumn.DataBind();
            dr.Close();
            cnp.Close(); 
        }
    }

    protected void btnReport_Click(object sender, EventArgs e)
    {

        System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-GB");
        cnp.ConnectionString = ConfigurationManager.ConnectionStrings["PATENTCN"].ConnectionString;
        string sql = "";
        for (int i = 0; i < lstSelected.Items.Count; i++)
        {
            lstSelected.SelectedIndex=i;
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
        string dFilter="";
        string comm = "";
        if (RdoIITM.Checked == true)
        {
            comm = "commercial is null ";
        }
        else if ( RdoIV.Checked == true)
        {
            comm = "commercial is not null ";
        }
        if (txtFilter.Text.Trim() != "")
        {
            dFilter = "where " + txtFilter.Text.Trim();
        }
        if (dFilter != "" && comm != "")
        {
            dFilter = dFilter + " and " + comm;
        }
        else if (dFilter == "" && comm != "")
        {
            dFilter = "where "+ comm;
        }
        sql = "select " + sql + " from Patdetails " + dFilter + "Order By fileno";
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
        ge.MakeExcel(dt,"Indian");
        cnp.Close();        
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
    protected void btnAddVal_Click(object sender, EventArgs e)
    {
        if (txtValue.Text.Trim() != "")
        {
            string tmp="";
            string lstcol = lstColumn.SelectedItem.Value.Trim();
            if (lstcol != "EntryDt" && lstcol != "request_dt" && lstcol != "filing_dt" && lstcol != "Exam_dt" && lstcol != "pub_dt" && lstcol != "validity_from_dt" && lstcol != "validity_to_dt" && lstcol != "pat_dt")
            {
                tmp = lstcol + " like " + "'%" + txtValue.Text.Trim() + "%'";
            }
            else if (lstcol == "EntryDt" || lstcol == "request_dt" || lstcol == "filing_dt" || lstcol == "Exam_dt" || lstcol == "pub_dt" || lstcol == "validity_from_dt" || lstcol == "validity_to_dt" || lstcol == "pat_dt")
            {
                tmp = lstcol + " = " + "convert(smalldatetime,'" + txtValue.Text.Trim() + "',103)";
            }

            if (txtFilter.Text.Trim() == "")
            {
                txtFilter.Text = tmp;
            }
            else
            {
                txtFilter.Text = txtFilter.Text + " and " + tmp;
            }
        }
    }
    protected void btnRemoveVal_Click(object sender, EventArgs e)
    {
        string filter=txtFilter.Text.Trim();
        if (filter!="")
        {
            int pos= filter.LastIndexOf("and");
            if (pos>0)
            {
                filter=filter.Remove(pos);
            }
            txtFilter.Text = filter;
        }
    }
    protected void btnClear_Click(object sender, EventArgs e)
    {
        txtValue.Text = "";
        txtFilter.Text = "";
        lstSelected.Items.Clear();
        lstTable.Items.Clear();
        cnp.ConnectionString = ConfigurationManager.ConnectionStrings["PATENTCN"].ConnectionString;
        SqlCommand cmd = new SqlCommand();
        string sql = "select column_name from information_schema.columns where table_name='patdetails' order by ordinal_position";
        SqlDataReader dr;
        cmd.CommandType = CommandType.Text;
        cmd.Connection = cnp;
        cmd.CommandText = sql;
        cnp.Open();
        dr = cmd.ExecuteReader();
        lstTable.DataTextField = "column_name";
        lstTable.DataValueField = "column_name";
        lstTable.DataSource = dr;
        lstTable.DataBind();
        dr.Close();
        cnp.Close();        
    }
}
