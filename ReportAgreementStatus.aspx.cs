using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

public partial class Default2 : System.Web.UI.Page
{
    string id;
    protected void Page_Load(object sender, EventArgs e)
    {
        id = Request.QueryString["id"];
        if (!IsPostBack)
        {
            if (id == "Contract")
            {
                lblField.Text = "Contract Field Name";
                lblValue.Text = "Contract Field Value";
                using (SqlConnection con = new SqlConnection())
                {
                    con.ConnectionString = ConfigurationManager.ConnectionStrings["PATENTCN"].ConnectionString;
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        string sql = "select EntryDt,ContractNo,AgreementType,AgreementNo,Title,Scope,Party,CoordinatingPerson,Coorcode,Dept,EffectiveDt,ExpiryDt,Remark like 'agreement'";
                        cmd.CommandText = sql;
                        cmd.CommandType = CommandType.Text;
                        cmd.Connection = con;
                        SqlDataReader dr;
                        dr = cmd.ExecuteReader();
                        lstField.DataTextField = dr.ToString();
                        lstField.DataValueField = dr.ToString();
                        lstField.DataSource = dr;
                        lstField.DataBind();
                        dr.Close();
                    }
                }
            }

        }
    }
    protected void imgBtnReport_Click(object sender, ImageClickEventArgs e)
    {
        if (lstField.SelectedIndex == -1)
        {
            ClientScript.RegisterStartupScript(GetType(), "Information", "<script>alert('Please Select Search Field')</script>");
            return;
        }
        string mVal;
        if (id == "Contract")
        {
            if (lstField.SelectedIndex == 1 || lstField.SelectedIndex == 6 || lstField.SelectedIndex == 7 || lstField.SelectedIndex == 9)
            {
                mVal = "= '" + ddlListItem.SelectedItem.Text.Trim() + "'";
            }
            else if (lstField.SelectedIndex == 10 || lstField.SelectedIndex == 11)
            {
                mVal = "= '" + txtValue.Text.Trim() + "'";
            }
            else if (lstField.SelectedIndex == 3)
            {
                mVal = "= '" + ddlListItem.SelectedItem.Value.ToString() + "'";
                using (SqlConnection con = new SqlConnection())
                {
                    con.ConnectionString = ConfigurationManager.ConnectionStrings["PATENTCN"].ConnectionString;
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        string sql = "select ContractNo,Title,Scope,Party,CoordinatingPerson from agreement where  AgreementType " + mVal;
                        cmd.CommandText = sql;
                        cmd.CommandType = CommandType.Text;
                        cmd.Connection = con;
                        SqlDataReader dr;
                        dr = cmd.ExecuteReader();
                        DataTable dt = new DataTable();
                        dt.Load(dr);
                        gvResult.DataSource = dt;
                        gvResult.DataBind();
                        dr.Close();
                    }
                }
            }
            else
            {
                mVal = " like '%" + txtValue.Text.Trim() + "%'";
            }

            using (SqlConnection con = new SqlConnection())
            {
                con.ConnectionString = ConfigurationManager.ConnectionStrings["PATENTCN"].ConnectionString;
                con.Open();
                using (SqlCommand cmd = new SqlCommand())
                {
                    string sql = "select ContractNo,Title,Scope,Party,CoordinatingPerson from agreement where " + lstField.SelectedItem.Text.Trim() + mVal;
                    cmd.CommandText = sql;
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = con;
                    SqlDataReader dr;
                    dr = cmd.ExecuteReader();
                    DataTable dt = new DataTable();
                    dt.Load(dr);
                    gvResult.DataSource = dt;
                    gvResult.DataBind();
                    dr.Close();
                }
            }
        }
    }
}
