using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

public partial class RptInternship : System.Web.UI.Page
{
    string id;
    protected void Page_Load(object sender, EventArgs e)
    {
        id = Request.QueryString["id"];
        if (!IsPostBack)
        {
            if (id == "Internship")
            {
                lblField.Text = "Internship Field Name";
                lblValue.Text = "Internship Field Value";
                using (SqlConnection con = new SqlConnection())
                {
                    con.ConnectionString = ConfigurationManager.ConnectionStrings["PATENTCN"].ConnectionString;
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        string sql = "select column_name from information_schema.columns where table_name like 'Internship'";
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
    protected void lstField_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlListItem.Items.Clear();
        txtValue.Text = "";
        if (id == "Internship")
        {
            if (lstField.SelectedIndex == 1 || lstField.SelectedIndex == 6)
            {
                ddlListItem.Visible = true;
                txtValue.Visible = false;
                string sql = "select distinct(" + lstField.SelectedItem.Text.Trim() + ") from Internship order by " + lstField.SelectedItem.Text.Trim();
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
            else
            {
                ddlListItem.Visible = false;
                txtValue.Visible = true;
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
        if (id == "Internship")
        {
            if (lstField.SelectedIndex == 1 || lstField.SelectedIndex == 6 )
            {
                mVal = "= '" + ddlListItem.SelectedItem.Text.Trim() + "'";
            }
            else if (lstField.SelectedIndex == 0 || lstField.SelectedIndex == 23 || lstField.SelectedIndex == 24 || lstField.SelectedIndex == 25) 
            {
                mVal = "= '" + txtValue.Text.Trim() + "'";
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
                    string sql = "select InternshipID,Title,CandidateName,Department,College,JoiningDate,RelievingDate from Internship where " + lstField.SelectedItem.Text.Trim() + mVal;
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
    protected void lbtnAction_Click(object sender, EventArgs e)
    {
        GridViewRow gvRow = (GridViewRow)((LinkButton)sender).NamingContainer;
        LinkButton lbInternshipNo = gvRow.FindControl("lbtnSelect") as LinkButton;
        string InternshipNo = lbInternshipNo.Text.Trim();
        Page.ClientScript.RegisterStartupScript(this.GetType(), "InternshipDetails", string.Format("child=(window.open('{0}','child_Internship','menubar=0,toolbar=0,resizable=1,width=800,height=900,scrollbars=1'));", "InternshipDetails.aspx?InternshipID=" + InternshipNo), true);
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
    protected void gvResult_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
                
    }
}
