using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data.SqlClient;

public partial class Modify_Service : System.Web.UI.Page
{

    SqlConnection con = new SqlConnection();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.IsPostBack)
        {
            con.ConnectionString = ConfigurationManager.ConnectionStrings["PATENTCN"].ConnectionString;
            System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-GB");
            string sql = "select AttorneyID+' - '+AttorneyName as attorneyID from Attorney order by AttorneyID";
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
        
            SqlCommand cmd1 = new SqlCommand();
            string sql1 = "select country from ipCountry where country is not null order by country";
            cmd1.Connection = con;
            SqlDataReader dr1;
            cmd1.CommandText = sql1;
            dr1 = cmd1.ExecuteReader();
            ddlCountry.Items.Clear();
            ddlCountry.DataValueField = "country";
            ddlCountry.DataTextField = "country";
            ddlCountry.DataSource = dr1;
            //ddlCountry.Items.Add("");
            //ddlCountry.Items.Add("India");
            ddlCountry.DataBind();
            ddlCountry.Items.Insert(0,"India");
         
            dr1.Close();
            con.Close();
        }
    }
    protected void dropId_SelectedIndexChanged(object sender, EventArgs e)
    {
        string a = dropId.SelectedItem.ToString();
        string[] split = a.Split('-');
        string ext = split[0].Trim();
        System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-GB");
        con.ConnectionString = ConfigurationManager.ConnectionStrings["PATENTCN"].ConnectionString;
        con.Open();
        SqlCommand cmd = new SqlCommand();
        string sql2 = "select * from Attorney where AttorneyID='" + ext + "'";
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
           if (!dr.IsDBNull(12)) ddlCountry.SelectedItem.Text = dr.GetString(12); else ddlCountry.SelectedItem.Text = "";
            if (!dr.IsDBNull(8)) txtPhone.Text = dr.GetString(8); else txtPhone.Text = "";
            if (!dr.IsDBNull(9)) txtFax.Text = dr.GetString(9); else txtFax.Text = "";
            if (!dr.IsDBNull(10)) txtEmail.Text = dr.GetString(10); else txtEmail.Text = "";
            if (!dr.IsDBNull(13)) txtMobile.Text = dr.GetString(13); else txtMobile.Text = "";
            if (!dr.IsDBNull(14)) TxtRange.Text = dr.GetString(14); else TxtRange.Text = "";
            //  if (!dr.IsDBNull(11)) ddlCategory.SelectedItem.Text = dr.GetString(11); else ddlCategory.SelectedItem.Text = "";
        }
        con.Close();
    }
    protected void imgBtnClear_Click(object sender, ImageClickEventArgs e)
    {
        dropId.SelectedItem.Text = "";
        txtAttorneyName.Text = "";
        txtAddress1.Text = "";
        txtAddress2.Text = "";
        txtAddress3.Text = "";
        txtCity.Text = "";
        txtPincode.Text = "";
        ddlCountry.SelectedItem.Text = "";
        txtPhone.Text = "";
        txtFax.Text = "";
        txtEmail.Text = "";
        txtMobile.Text = "";
        TxtRange.Text = "";
    }
    protected void imgBtnSubmit_Click(object sender, ImageClickEventArgs e)
    {
        System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-GB");
        con.ConnectionString = ConfigurationManager.ConnectionStrings["PATENTCN"].ConnectionString;
        con.Open();

        SqlCommand cmd3 = new SqlCommand("Update Attorney set AttorneyName='" + txtAttorneyName.Text + "',Address1='" + txtAddress1.Text + "',Address2='" + txtAddress2.Text + "',Address3='" + txtAddress3.Text + "',City='" + txtCity.Text + "',PinCode='" + txtPincode.Text + "',PhoneNo='" + txtPhone.Text + "',FaxNo='" + txtFax.Text + "',EmailID='" + txtEmail.Text + "',Category='" + ddlCategory.SelectedItem.Text + "',Country='" + ddlCountry.SelectedItem.ToString() + "',Mobile_No='" + txtMobile.Text + "',RangeOfServices='" + TxtRange.Text + "' where AttorneyID='"+dropId.SelectedItem.Text+"'", con);
        cmd3.ExecuteNonQuery();
        con.Close();

    }
}
   

   
