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


public partial class Default2 : System.Web.UI.Page
{
    SqlConnection cnp = new SqlConnection();
    //string[] tColumn = new[] { "ContractNo", "AgreementType", "Title", "Scope", "Party", "CoordinatingPerson", "CoorCode", "Request_Dt", "Application_no", "Filing_dt", "pat_no", "pat_dt", "Initial Filing", "Attorney", "Industry1", "Industry2" };
    //string[] tColumnVal = new[] { "fileno", "title", "commercial", "FirstApplicant", "SecondApplicant", "Inventor1", "department", "request_dt", "applcn_no", "filing_dt", "pat_no", "pat_dt", "InitialFiling", "Attorney", "Industry1", "Industry2" };
    
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.IsPostBack)
        {
            cnp.ConnectionString = ConfigurationManager.ConnectionStrings["PATENTCN"].ConnectionString;
            SqlCommand cmd = new SqlCommand();
            SqlCommand cmd1 = new SqlCommand();
            string sql = "select column_name from information_schema.columns where table_name='tbl_trx_duediligence' order by ordinal_position";
            //string sql1= "SELECT column_name FROM information_schema.columns WHERE Table_Name= 'CoInventorDetails'";
            SqlDataReader dr;
            cmd.CommandType = CommandType.Text;
            cmd.Connection = cnp;
            cmd.CommandText = sql;
            cnp.Open();
            dr = cmd.ExecuteReader();
            lstColumn.DataTextField = "column_name";
            lstColumn.DataValueField = "column_name";
            lstColumn.DataSource = dr;
            lstColumn.DataBind();
            dr.Close();
            cnp.Close();

            
            SqlDataReader dr1;
            cmd1.CommandType = CommandType.Text;
            cmd1.Connection = cnp;
            cmd1.CommandText = sql;
            cnp.Open();
            dr1 = cmd1.ExecuteReader();
            lstTable.DataTextField = "column_name";
            lstTable.DataValueField = "column_name";
            lstTable.DataSource = dr1;
            lstTable.DataBind();
            dr.Close();

        }

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


    protected void btnReport_Click(object sender, EventArgs e)
    {

        System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-GB");
        cnp.ConnectionString = ConfigurationManager.ConnectionStrings["PATENTCN"].ConnectionString;
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
        if (sql == "") sql = "tbl_trx_duediligence.*";
        string dFilter = "";
        //string dFilter1 = "";
        string dFilter2 = "";
        string comm = "";
        //if (txtFilter.Text.Trim() != "" && txtFilter.Text=="ContractNO")
        //{
        //   // dFilter = "where " + txtFilter.Text.Trim();
        //    dFilter = "where Agreement." + txtFilter.Text.Trim();
        //  dFilter1= "and Royality." + txtFilter.Text.Trim();
        //  dFilter2 = dFilter + dFilter1;
        //}
        if (txtFilter.Text.Trim() != "" )
        {
            dFilter2 = "where " + txtFilter.Text.Trim();
        }
        if (txtFilter.Text.Trim() != "")
        {
        }
       
        if (dFilter != "" && comm != "")
        {
            dFilter2 = dFilter + " and " + comm;
        }
        else if (dFilter == "" && comm != "")
        {
            dFilter = "where " + comm;
        }
        sql = "select " + sql + " from tbl_trx_duediligence " + dFilter2;
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
        ge.MakeExcel(dt, "tbl_trx_duediligence");
        cnp.Close();  
    }
}
