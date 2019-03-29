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

public partial class InterAttorney : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection();
    protected void Page_Load(object sender, EventArgs e)
    {

        if (!User.IsInRole("Intern"))
        {
            if (!IsPostBack)
            {
                txtAttorneyID.Text = findMaxNo();
                imgBtnSubmit.Visible = (!User.IsInRole("View"));
            }
        }
        else
        {
            Server.Transfer("Unauthorized.aspx");
        }
    }
    protected string findMaxNo()
    {
        con.ConnectionString = ConfigurationManager.ConnectionStrings["PATENTCN"].ConnectionString;
        SqlCommand cmd = new SqlCommand();
        string sql = "select max(convert(int,substring (attorneyID,3,len(attorneyID))))+1 from IPAttorney";
        cmd.CommandType = CommandType.Text;
        cmd.Connection = con;
        cmd.CommandText = sql;
        con.Open();
        int MaxNo = Convert.ToInt32(cmd.ExecuteScalar());

        return "IA" +MaxNo.ToString();
    }
    protected void imgBtnSubmit_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            SqlCommand cmd = new SqlCommand();
            con.ConnectionString = ConfigurationManager.ConnectionStrings["PATENTCN"].ConnectionString;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "intlAttorneyNew";
            cmd.Connection = con;
            con.Open();
           
            SqlParameter pm1 = new SqlParameter();
            pm1.ParameterName = "@AttorneyID";
            pm1.SourceColumn = "AttorneyID";
            pm1.Value = txtAttorneyID.Text.Trim();
            pm1.DbType = DbType.String;
            pm1.Direction = ParameterDirection.Input;

            SqlParameter pm2 = new SqlParameter();
            pm2.ParameterName = "@AttorneyName";
            pm2.SourceColumn = "AttorneyName";
            pm2.Value = txtAttorneyName.Text.Trim();
            pm2.DbType = DbType.String;
            pm2.Direction = ParameterDirection.Input;

            SqlParameter pm3 = new SqlParameter();
            pm3.ParameterName = "@Address1";
            pm3.SourceColumn = "Address1";
            pm3.Value = txtAddress1.Text.Trim();
            pm3.DbType = DbType.String;
            pm3.Direction = ParameterDirection.Input;

            SqlParameter pm4 = new SqlParameter();
            pm4.ParameterName = "@Address2";
            pm4.SourceColumn = "Address2";
            pm4.Value = txtAddress2.Text.Trim();
            pm4.DbType = DbType.String;
            pm4.Direction = ParameterDirection.Input;

            SqlParameter pm5 = new SqlParameter();
            pm5.ParameterName = "@Address3";
            pm5.SourceColumn = "Address3";
            pm5.Value = txtAddress3.Text.Trim();
            pm5.DbType = DbType.String;
            pm5.Direction = ParameterDirection.Input;

            SqlParameter pm6 = new SqlParameter();
            pm6.ParameterName = "@City";
            pm6.SourceColumn = "City";
            pm6.Value = txtCity.Text.Trim();
            pm6.DbType = DbType.String;
            pm6.Direction = ParameterDirection.Input;

            SqlParameter pm7 = new SqlParameter();
            pm7.ParameterName = "@PinCode";
            pm7.SourceColumn = "PinCode";
            pm7.Value = txtPincode.Text.Trim();
            pm7.DbType = DbType.String;
            pm7.Direction = ParameterDirection.Input;

            SqlParameter pm8 = new SqlParameter();
            pm8.ParameterName = "@PhoneNo";
            pm8.SourceColumn = "PhoneNo";
            pm8.Value = txtPhone.Text.Trim();
            pm8.DbType = DbType.String;
            pm8.Direction = ParameterDirection.Input;

            SqlParameter pm9 = new SqlParameter();
            pm9.ParameterName = "@FaxNo";
            pm9.SourceColumn = "FaxNo";
            pm9.Value = txtFax.Text.Trim();
            pm9.DbType = DbType.String;
            pm9.Direction = ParameterDirection.Input;

            SqlParameter pm10 = new SqlParameter();
            pm10.ParameterName = "@EmailID";
            pm10.SourceColumn = "EmailID";
            pm10.Value = txtEmail.Text.Trim();
            pm10.DbType = DbType.String;
            pm10.Direction = ParameterDirection.Input;

            cmd.Parameters.Add(pm1);
            cmd.Parameters.Add(pm2);
            cmd.Parameters.Add(pm3);
            cmd.Parameters.Add(pm4);
            cmd.Parameters.Add(pm5);
            cmd.Parameters.Add(pm6);
            cmd.Parameters.Add(pm7);
            cmd.Parameters.Add(pm8);
            cmd.Parameters.Add(pm9);
            cmd.Parameters.Add(pm10);
           
            cmd.ExecuteNonQuery();
            ClientScript.RegisterStartupScript(GetType(), "Success", "<script>alert('This Record successfully added')</script>");
            con.Close();
            imgBtnClear_Click(sender, e);
        }
        catch (Exception ex)
        {
            ClientScript.RegisterStartupScript(GetType(), "Error", "<script>alert('" + ex.Message.ToString() + "')</script>");
            con.Close();
        }
    }
    protected void imgBtnClear_Click(object sender, ImageClickEventArgs e)
    {
        txtAttorneyID.Text = findMaxNo();
        txtAttorneyName.Text = "";
        txtAddress1.Text = "";
        txtAddress2.Text = "";
        txtAddress3.Text = "";
        txtCity.Text = "";
        txtPincode.Text = "";
        txtPhone.Text = "";
        txtFax.Text = "";
        txtEmail.Text = "";
    }
}
