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


public partial class _Default : System.Web.UI.Page
{
    SqlConnection cnp = new SqlConnection();
    SqlConnection cn = new SqlConnection();
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        cnp.ConnectionString = ConfigurationManager.ConnectionStrings["PATENTCN"].ConnectionString;
        cn.ConnectionString = ConfigurationManager.ConnectionStrings["PATENTCN"].ConnectionString;
        SqlCommand cmd = new SqlCommand();
        string sql = "select fileno,count(*) from patentpayment1 group by fileno order by fileno"; 
        SqlDataReader dr;
        cmd.CommandType = CommandType.Text;
        cmd.Connection = cnp;
        cmd.CommandText = sql;
        cnp.Open();
        dr = cmd.ExecuteReader();
        SqlCommand cmd1 = new SqlCommand();
        cmd1.CommandType = CommandType.Text;
        cmd1.Connection = cn;
        while (dr.Read())
        {
            string sql1 = "select * from patentpayment1 where fileno= '" + dr.GetString(0) + "' order by slno,entrydt,chequedt";
            cmd1.CommandText = sql1;

            SqlDataAdapter sda = new SqlDataAdapter();
            sda.SelectCommand=cmd1;
            DataTable dt= new DataTable ();
            sda.Fill(dt);

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                DataRow drow = dt.Rows[i];
                drow.BeginEdit();
                //drow = dt.Rows[i];
                drow["slno"] = i + 1;
                drow.EndEdit();
                drow.AcceptChanges();
            }
            sda.Update(dt);
            sda.UpdateCommand = cmd1;
        }                         
        
    }
}
