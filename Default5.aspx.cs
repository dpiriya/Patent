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
using System.Web.Services;
using System.Web.Script.Services;
using System.Data.SqlClient;
using System.Collections.Generic;

public partial class Default5 : System.Web.UI.Page
{
    SqlConnection cnp = new SqlConnection();
    protected void Page_Load(object sender, EventArgs e)
    {
        
        cnp.ConnectionString = ConfigurationManager.ConnectionStrings["PATENTCN"].ConnectionString;
        SqlCommand cmd = new SqlCommand();
        string sql = "select fileno from patdetails order by cast(fileno as int) desc";
        SqlDataReader dr;
        cmd.CommandType = CommandType.Text;
        cmd.Connection = cnp;
        cmd.CommandText = sql;
        cnp.Open();
        dr = cmd.ExecuteReader();
        ddlIDFNo.Items.Clear();
        ddlIDFNo.Items.Add("");
       
        while (dr.Read())
        {
            ddlIDFNo.Items.Add(dr.GetString(0));
            
        }
        dr.Close();

        SqlCommand cmd1 = new SqlCommand("select * from ServiceFormDetails", cnp);
        SqlDataAdapter da = new SqlDataAdapter(cmd1);
        DataTable dt= new DataTable();
        da.Fill(dt);
        Grid1.DataSource = dt;
        Grid1.DataBind();
   

    }
}
