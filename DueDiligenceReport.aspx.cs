using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.IO;

public partial class DueDiligenceReport : System.Web.UI.Page
{   
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            lblField.Text = "DueDiligence Field Name";
            lblValue.Text = "DueDiligence Field Value";
            using (SqlConnection con = new SqlConnection())
            {
                con.ConnectionString = ConfigurationManager.ConnectionStrings["PATENTCN"].ConnectionString;
                con.Open();
                using (SqlCommand cmd = new SqlCommand())
                {
                    string sql = "select column_name from information_schema.columns where table_name like 'tbl_trx_duediligence'";
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
    protected void imgBtnReport_Click(object sender, EventArgs e)
    {
        lvSearchb.Visible = false;
        string mVal;        
            if (lstField.SelectedIndex == -1)
            {
                using (SqlConnection con = new SqlConnection())
                {
                    con.ConnectionString = ConfigurationManager.ConnectionStrings["PATENTCN"].ConnectionString;
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        string sql = "select * from tbl_trx_duediligence";
                        cmd.CommandText = sql;
                        cmd.CommandType = CommandType.Text;
                        cmd.Connection = con;
                        SqlDataReader dr;
                        dr = cmd.ExecuteReader();
                        DataTable dt = new DataTable();
                        dt.Load(dr);
                        GenerateExcel ge = new GenerateExcel();
                        ge.MakeExcel(dt, "Duediligence");
                    lvSearch.DataSource = dt;
                    lvSearch.DataBind();

                        dr.Close();
                    }
                }
            }
            if (lstField.SelectedIndex == 7)
            {
                mVal = "= '" + ddlListItem.SelectedItem.Text.Trim() + "'";
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
                        string sql = "select * from tbl_trx_duediligence where FileNo " + mVal;
                        cmd.CommandText = sql;
                        cmd.CommandType = CommandType.Text;
                        cmd.Connection = con;
                        SqlDataReader dr;
                        dr = cmd.ExecuteReader();
                        DataTable dt = new DataTable();
                        dt.Load(dr);
                    lvSearch.DataSource = dt;
                    lvSearch.DataBind();
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
                        string sql = "select * from tbl_trx_duediligence where " + lstField.SelectedItem.Text.Trim() + mVal;
                        cmd.CommandText = sql;
                        cmd.CommandType = CommandType.Text;
                        cmd.Connection = con;
                        SqlDataReader dr;
                        dr = cmd.ExecuteReader();
                        DataTable dt = new DataTable();
                        dt.Load(dr);
                    lvSearch.DataSource = dt;
                    lvSearch.DataBind();
                        dr.Close();
                    }
                }
            }        
    }
    protected void imgBtnClear_Click(object sender, EventArgs e)
    {
        ddlListItem.Items.Clear();
        ddlListItem.Visible = false;
        txtValue.Text = "";
        txtValue.Visible = false;
        lstField.SelectedIndex = -1;
        lvSearch.DataSource = null;
        lvSearch.DataBind();
    }
    protected void lstField_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlListItem.Items.Clear();
        txtValue.Text = "";       
            if (lstField.SelectedIndex == 7)
            {
                ddlListItem.Visible = true;
                txtValue.Visible = false;
                string sql = "select distinct(" + lstField.SelectedItem.Text.Trim() + ") from tbl_trx_duediligence order by " + lstField.SelectedItem.Text.Trim();
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
                string sql = "select distinct fileno from tbl_trx_duediligence order by FileNo";
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
                        ddlListItem.DataTextField = "fileno";
                        ddlListItem.DataValueField = "fileno";
                        ddlListItem.DataSource = dr1;
                        ddlListItem.DataBind();
                        dr1.Close();
                    }
                con.Close();
                }
            }
            else
            {
                ddlListItem.Visible = false;
                txtValue.Visible = true;
            }        
    }
    //protected void gvResult_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    //{

    //    GridViewRow item = gvResult.Rows[e.NewSelectedIndex];
    //    LinkButton lbContractNo = gvResult.Rows[e.NewSelectedIndex].FindControl("lbtnSelect") as LinkButton;
    //    string ContractNo = lbContractNo.Text.Trim();
    //    Page.ClientScript.RegisterStartupScript(this.GetType(), "contract_window", string.Format("void(window.open('{0}','child_Contract','menubar=0,toolbar=0,resizable=1,scrollbars=1'));", "rptView.aspx?id=Contract&ContractNo=" + ContractNo), true);
    //}

    //protected void lbtnAction_Click(object sender, EventArgs e)
    //{
    //    GridViewRow gvRow = (GridViewRow)((LinkButton)sender).NamingContainer;
    //    //LinkButton lbContractNo = gvRow.FindControl("FileNo") as LinkButton;
    //    //string ContractNo = lbContractNo.Text.Trim();
    //    Label sno = (Label)gvRow.FindControl("lblSNo");
    //    string sno = sno.Text;
    //    Page.ClientScript.RegisterStartupScript(this.GetType(), "DueDiligenceFile", string.Format("child=(window.open('{0}','child_ContractFile','menubar=0,toolbar=0,resizable=1,width=600,height=900,scrollbars=1'));", "ContractActionAndFile.aspx?ContractNo=" + ContractNo), true);
    //}

    protected void lvSearch_ItemCommand(object sender, ListViewCommandEventArgs e)
    {
        LinkButton fn = (LinkButton)e.Item.FindControl("lbtnFileName");
        using (SqlConnection con = new SqlConnection())
        {
            try
            {
                Label sno = (Label)e.Item.FindControl("lblSNo");
                Label Fileno = (Label)e.Item.FindControl("lblFileNo");
                con.ConnectionString = ConfigurationManager.ConnectionStrings["PATENTCN"].ConnectionString;
                con.Open();
                SqlCommand cmdpath = new SqlCommand("select FilePath from tbl_trx_duediligence where FileNo='" + Fileno.Text.Trim() + "' and Sno='" + sno.Text + "'", con);
                string path = cmdpath.ExecuteScalar().ToString();
                string file = path + fn.Text;

                if (System.IO.File.Exists(file))
                {
                    string ext = System.IO.Path.GetExtension(file);
                    FileStream fs = new FileStream(file, FileMode.Open, FileAccess.Read);
                    byte[] bytBytes = new byte[fs.Length];
                    fs.Read(bytBytes, 0, (int)fs.Length);
                    fs.Close();
                    Response.AppendHeader("Content-Disposition", "attachment;filename=" + fn.Text);
                    Response.ContentType = ext;
                    Response.BinaryWrite(bytBytes);
                    Response.End();
                }
                con.Close();

            }
            catch (Exception ex)
            {
                ClientScript.RegisterStartupScript(GetType(), "Information", "<script>alert('" + ex.ToString() + "')</script>");
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
            }
        }
    }

    protected void lvSearch_ItemDataBound(object sender, ListViewItemEventArgs e)
    {

    }

    protected void imgBtnReportb_Click(object sender, EventArgs e)
    {
        lvSearchb.Visible = true;
        string mVal;
        if (lstField.SelectedIndex == -1)
        {
            using (SqlConnection con = new SqlConnection())
            {
                con.ConnectionString = ConfigurationManager.ConnectionStrings["PATENTCN"].ConnectionString;
                con.Open();
                using (SqlCommand cmd = new SqlCommand())
                {
                    string sql = "select * from tbl_trx_duediligence";
                    cmd.CommandText = sql;
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = con;
                    SqlDataReader dr;
                    dr = cmd.ExecuteReader();
                    DataTable dt = new DataTable();
                    dt.Load(dr);
                    GenerateExcel ge = new GenerateExcel();
                    ge.MakeExcel(dt, "Duediligence");
                    lvSearchb.DataSource = dt;
                    lvSearchb.DataBind();

                    dr.Close();
                }
            }
        }
        if (lstField.SelectedIndex == 7)
        {
            mVal = "= '" + ddlListItem.SelectedItem.Text.Trim() + "'";
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
                    string sql = "select * from tbl_trx_duediligence where FileNo " + mVal;
                    cmd.CommandText = sql;
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = con;
                    SqlDataReader dr;
                    dr = cmd.ExecuteReader();
                    DataTable dt = new DataTable();
                    dt.Load(dr);
                    lvSearchb.DataSource = dt;
                    lvSearchb.DataBind();
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
                    string sql = "select * from tbl_trx_duediligence where " + lstField.SelectedItem.Text.Trim() + mVal;
                    cmd.CommandText = sql;
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = con;
                    SqlDataReader dr;
                    dr = cmd.ExecuteReader();
                    DataTable dt = new DataTable();
                    dt.Load(dr);
                    lvSearch.DataSource = dt;
                    lvSearch.DataBind();
                    dr.Close();
                }
            }
        }
    }
}
