using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Web.Security;
using System.Web.Services;


public partial class Default2 : System.Web.UI.Page
{
    SqlConnection cnp = new SqlConnection();
   
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.IsPostBack)
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

            SqlCommand cmd1 = new SqlCommand();
            string sql1 = "select SubFileNo from International";
            SqlDataReader dr1;
            cmd1.CommandType = CommandType.Text;
            cmd1.Connection = cnp;
            cmd1.CommandText = sql1;
            dr1 = cmd1.ExecuteReader();
            while (dr1.Read())
            {
                ddlIDFNo.Items.Add(dr1.GetString(0));
            }
            dr1.Close();
        }

    }
    protected void ImageReportButn1_Click(object sender, ImageClickEventArgs e)
    {
       // Page.ClientScript.RegisterStartupScript(this.GetType(), "ContractActionFile", string.Format("child=(window.open('{0}','child_ContractFile','menubar=0,toolbar=0,resizable=1,width=600,height=900,scrollbars=1'));", "RenewalActionReport.aspx?IDF=" + ddlIDFNo.SelectedValue.ToString()), true);
        Response.Redirect("RenewalActionReport.aspx?IDF=" + ddlIDFNo.SelectedValue.ToString());

    }
    protected void ddlIDFNo_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
}
