using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

public partial class Default3 : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.IsPostBack)
        {
            con.ConnectionString = ConfigurationManager.ConnectionStrings["PATENTCN"].ConnectionString;
            System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-GB");
            string sql = "Select AttorneyId from Attorney";
            con.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = sql;
            cmd.Connection = con;
            SqlDataReader dr;
            dr = cmd.ExecuteReader();
            drop1.DataTextField = "AttorneyId";
            drop1.DataValueField = "AttorneyId";
            drop1.DataSource = dr;
            drop1.DataBind();
            drop1.Items.Insert(0, "");
            dr.Close();
        }
    }
}
