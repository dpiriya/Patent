using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

public partial class ReportSelection4 : System.Web.UI.Page
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
                        string sql = "select column_name from information_schema.columns where table_name like 'agreement'";
                        cmd.CommandText = sql;
                        cmd.CommandType = CommandType.Text;
                        cmd.Connection = con;
                        SqlDataReader dr;
                        dr = cmd.ExecuteReader();
                        lstField.DataTextField = "column_name";
                        lstField.DataValueField = "column_name";
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
        //altered by priya

        //if (lstField.SelectedIndex == -1)
        //{

        //ClientScript.RegisterStartupScript(GetType(), "Information", "<script>alert('Please Select Search Field')</script>");
        //return;
        //}
        string mVal;
        if (id == "Contract")
        {
            if (lstField.SelectedIndex == -1)
            {
                using (SqlConnection con = new SqlConnection())
                {
                    con.ConnectionString = ConfigurationManager.ConnectionStrings["PATENTCN"].ConnectionString;
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        string sql = "select * from agreement";
                        cmd.CommandText = sql;
                        cmd.CommandType = CommandType.Text;
                        cmd.Connection = con;
                        SqlDataReader dr;
                        dr = cmd.ExecuteReader();
                        DataTable dt = new DataTable();
                        dt.Load(dr);
                        GenerateExcel ge = new GenerateExcel();
                        ge.MakeExcel(dt, "Contract");
                        gvResult.DataSource = dt;
                        gvResult.DataBind();
                        
                        dr.Close();
                    }
                
                }

            }
            if (lstField.SelectedIndex == 1 || lstField.SelectedIndex == 6 || lstField.SelectedIndex == 7 || lstField.SelectedIndex == 9)
            {
                mVal = "= '" + ddlListItem.SelectedItem.Text.Trim() + "'";
            }
            else if (lstField.SelectedIndex == 10 || lstField.SelectedIndex == 11)
            {
                mVal = "= '" + txtValue.Text.Trim() + "'";
            }
            else if (lstField.SelectedIndex == 2)
            {
                // mVal = "= '" + ddlListItem.SelectedItem.Text.Trim() + "'";
                //   mVal = "= '" + ddlListItem.SelectedItem.value.Trim() + "'";
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
            if (lstField.SelectedIndex != -1)
            {
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
    protected void imgBtnClear_Click(object sender, ImageClickEventArgs e)
    {
        ddlListItem.Items.Clear();
        ddlListItem.Visible = false;
        txtValue.Text = "";
        txtValue.Visible = false;
        lstField.SelectedIndex = -1;
        gvResult.DataSource = null;
        gvResult.DataBind();
    }
    protected void lstField_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlListItem.Items.Clear();
        txtValue.Text = "";
        if (id == "Contract")
        {
            if (lstField.SelectedIndex == 1 || lstField.SelectedIndex == 6 || lstField.SelectedIndex == 7 || lstField.SelectedIndex == 9 || lstField.SelectedIndex==16)
            {
                ddlListItem.Visible = true;
                txtValue.Visible = false;
                string sql = "select distinct(" + lstField.SelectedItem.Text.Trim() + ") from agreement order by " + lstField.SelectedItem.Text.Trim();
                using (SqlConnection con = new SqlConnection())
                {
                    con.ConnectionString = ConfigurationManager.ConnectionStrings["PATENTCN"].ConnectionString;
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.CommandText = sql;
                        cmd.CommandType = CommandType.Text;
                        cmd.Connection = con;
                        SqlDataReader dr;
                        dr = cmd.ExecuteReader();
                        ddlListItem.DataTextField = lstField.SelectedItem.Text.Trim();
                        ddlListItem.DataValueField = lstField.SelectedItem.Text.Trim();
                        ddlListItem.DataSource = dr;
                        ddlListItem.DataBind();
                        dr.Close();
                    }
                }
            }

            else if (lstField.SelectedIndex == 2)
            {
                ddlListItem.Visible = true;
                txtValue.Visible = false;
                string sql = "select ItemList,Description from ListItemMaster where Category='ContractAgreementType'";
                using (SqlConnection con = new SqlConnection())
                {
                    con.ConnectionString = ConfigurationManager.ConnectionStrings["PATENTCN"].ConnectionString;
                    con.Open();
                    using (SqlCommand cmd1 = new SqlCommand())
                    {
                        cmd1.CommandText = sql;
                        cmd1.CommandType = CommandType.Text;
                        cmd1.Connection = con;
                        SqlDataReader dr1;
                        dr1 = cmd1.ExecuteReader();
                        ddlListItem.DataTextField = "Description";
                        ddlListItem.DataValueField = "ItemList";
                        ddlListItem.DataSource = dr1;
                        ddlListItem.DataBind();
                        dr1.Close();
                    }
                }
            }

            else
            {
                ddlListItem.Visible = false;
                txtValue.Visible = true;
            }

        }
    }
    protected void gvResult_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {

        GridViewRow item = gvResult.Rows[e.NewSelectedIndex];
        LinkButton lbContractNo = gvResult.Rows[e.NewSelectedIndex].FindControl("lbtnSelect") as LinkButton;
        string ContractNo = lbContractNo.Text.Trim();
        Page.ClientScript.RegisterStartupScript(this.GetType(), "contract_window", string.Format("void(window.open('{0}','child_Contract','menubar=0,toolbar=0,resizable=1,scrollbars=1'));", "rptView.aspx?id=Contract&ContractNo=" + ContractNo), true);
    }

    protected void lbtnAction_Click(object sender, EventArgs e)
    {
        GridViewRow gvRow = (GridViewRow)((LinkButton)sender).NamingContainer;
        LinkButton lbContractNo = gvRow.FindControl("lbtnSelect") as LinkButton;
        string ContractNo = lbContractNo.Text.Trim();
        Page.ClientScript.RegisterStartupScript(this.GetType(), "ContractActionFile", string.Format("child=(window.open('{0}','child_ContractFile','menubar=0,toolbar=0,resizable=1,width=600,height=900,scrollbars=1'));", "ContractActionAndFile.aspx?ContractNo=" + ContractNo), true);
    }
}
