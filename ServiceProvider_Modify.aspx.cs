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

public partial class ServiceProvider_Modify : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection();
    protected void Page_Load(object sender, EventArgs e)
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
        dropId.DataTextField = "AttorneyId";
        dropId.DataValueField = "AttorneyId";
        dropId.DataSource = dr;
        dropId.DataBind();
        dropId.Items.Insert(0, "");
        dr.Close();
        con.Close();
    }

    protected void dropId_SelectedIndexChanged(object sender, EventArgs e)
    {
        
       
    }
    protected void button1_Click(object sender, EventArgs e)
    {
        if (IsPostBack == true)
        {
            string a = dropId.SelectedItem.ToString();
            System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-GB");
            con.ConnectionString = ConfigurationManager.ConnectionStrings["PATENTCN"].ConnectionString;

            SqlCommand cmd = new SqlCommand();
            string sql2 = "select * from Attorney where AttorneyID='" + dropId.SelectedItem.Text + "'";
            cmd.CommandText = sql2;
            cmd.Connection = con;
            SqlDataReader dr;
            dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                if (!dr.IsDBNull(2)) txtAttorneyName.Text = dr.GetString(2); else txtAttorneyName.Text = "";
                if (!dr.IsDBNull(3)) txtAddress1.Text = dr.GetString(3); else txtAddress1.Text = "";
                if (!dr.IsDBNull(4)) txtAddress2.Text = dr.GetString(4); else txtAddress2.Text = "";
                if (!dr.IsDBNull(5)) txtAddress3.Text = dr.GetString(5); else txtAddress3.Text = "";
                if (!dr.IsDBNull(6)) txtCity.Text = dr.GetString(6); else txtCity.Text = "";
                if (!dr.IsDBNull(7)) txtPincode.Text = dr.GetString(7); else txtPincode.Text = "";
                // if (!dr.IsDBNull(12)) ddlCountry.SelectedItem.Text = dr.GetString(12); else ddlCountry.SelectedItem.Text = "";
                if (!dr.IsDBNull(8)) txtPhone.Text = dr.GetString(8); else txtPhone.Text = "";
                if (!dr.IsDBNull(9)) txtFax.Text = dr.GetString(9); else txtFax.Text = "";
                if (!dr.IsDBNull(10)) txtEmail.Text = dr.GetString(10); else txtEmail.Text = "";
                if (!dr.IsDBNull(13)) txtMobile.Text = dr.GetString(13); else txtMobile.Text = "";
                if (!dr.IsDBNull(14)) TxtRange.Text = dr.GetString(14); else TxtRange.Text = "";
                //  if (!dr.IsDBNull(11)) ddlCategory.SelectedItem.Text = dr.GetString(11); else ddlCategory.SelectedItem.Text = "";
            }
            con.Close();
        }
    }
}

