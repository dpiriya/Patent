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

public partial class Default2 : System.Web.UI.Page
{
    string id;
    protected void Page_Load(object sender, EventArgs e)
    {
        id = Request.QueryString["id"];
        if (!IsPostBack)
        {
            using (SqlConnection con = new SqlConnection())
            {
                con.ConnectionString = ConfigurationManager.ConnectionStrings["PATENTCN"].ConnectionString;
                con.Open();
                using (SqlCommand cmd = new SqlCommand())
                {

                    string sql = "SELECT ITEMLIST,ITEMLIST + ' - ' + DESCRIPTION AS ITEM_DESC FROM LISTITEMMASTER WHERE CATEGORY='ContractAgreementType' ORDER BY ITEMLIST";

                    cmd.CommandText = sql;
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = con;
                    SqlDataReader dr1;
                    dr1 = cmd.ExecuteReader();

                    ddlAgreeType.DataTextField = "ITEM_DESC";
                    ddlAgreeType.DataValueField = "ITEMLIST";
                    ddlAgreeType.DataSource = dr1;
                    ddlAgreeType.DataBind();
                    ddlAgreeType.Items.Insert(0, new ListItem("", ""));
                }

            }


        }
    }
    protected void ImageReportButn1_Click(object sender, ImageClickEventArgs e)
    {
        using (SqlConnection con = new SqlConnection())
        {
            con.ConnectionString = ConfigurationManager.ConnectionStrings["PATENTCN"].ConnectionString;
            con.Open();
            using (SqlCommand cmd1 = new SqlCommand())
            {
                if (ddlAgreeType.SelectedItem.ToString() == "" && drop1.SelectedValue.ToString() != "All")
                {

                    string sql1 = "select ContractNo,AgreementType,AgreementNo,Title,Scope,Party,CoordinatingPerson,Remark,Format(Request,'dd-MM-yyyy')as Request,TechTransfer,Status,Format(EffectiveDt,'dd-MM-yyyy') as EffectiveDt from Agreement where Request between '" + txtFDate.Text + "' and '" + txtTDate.Text + "' and Status='" + drop1.SelectedItem.ToString() + "'"; 
                    cmd1.CommandText = sql1;
                    cmd1.CommandType = CommandType.Text;
                    cmd1.Connection = con;
                    SqlDataReader dr1;
                    dr1 = cmd1.ExecuteReader();
                    //if (dr1.Read())
                    //{
                        DataTable dt1 = new DataTable();
                        dt1.Load(dr1);
                        GvReportDate.DataSource = dt1;
                        GvReportDate.DataBind();
                        dr1.Close();
                    //}
                    //else
                    //{
                    //    lblmsg.Visible = true;
                    //}
                }
                else if (drop1.SelectedItem.ToString() == "All" && ddlAgreeType.SelectedValue.ToString() != "")
                {
                    string sql1 = "select ContractNo,AgreementType,AgreementNo,Title,Scope,Party,CoordinatingPerson,Remark,Format(Request,'dd-MM-yyyy') as Request,TechTransfer,Status,Format(EffectiveDt,'dd-MM-yyyy')as EffectiveDt from Agreement where Request between '" + txtFDate.Text + "' and '" + txtTDate.Text + "' and AgreementType='" + ddlAgreeType.SelectedValue.ToString() + "'";
                    cmd1.CommandText = sql1;
                    cmd1.CommandType = CommandType.Text;
                    cmd1.Connection = con;
                    SqlDataReader dr1;
                    dr1 = cmd1.ExecuteReader();
                    //if (dr1.Read() == true)
                    //{
                        DataTable dt1 = new DataTable();
                        dt1.Load(dr1);
                        GvReportDate.DataSource = dt1;
                        GvReportDate.DataBind();
                        dr1.Close();
                    //}
                    //else
                    //{
                    //    lblmsg.Visible = true;
                    //}

                }
                else if (drop1.SelectedItem.ToString() == "All" && ddlAgreeType.SelectedValue.ToString() == "")
                {
                    string sql1 = "select ContractNo,AgreementType,AgreementNo,Title,Scope,Party,CoordinatingPerson,Remark,Format(Request,'dd-MM-yyyy')as Request,TechTransfer,Status,Format(EffectiveDt,'dd-MM-yyyy')as EffectiveDt from Agreement where Request between '" + txtFDate.Text + "' and '" + txtTDate.Text + "'";
                    cmd1.CommandText = sql1;
                    cmd1.CommandType = CommandType.Text;
                    cmd1.Connection = con;
                    SqlDataReader dr1;
                    dr1 = cmd1.ExecuteReader();
                    //if (dr1.Read() != true)
                    //{
                    //    lblmsg.Visible = true;
                    //}
                    //else
                    //{
                        DataTable dt1 = new DataTable();
                        dt1.Load(dr1);
                        GvReportDate.DataSource = dt1;
                        GvReportDate.DataBind();
                        dr1.Close();


                    //}
                }
                else
                {
                    string sql1 = "select ContractNo,AgreementType,AgreementNo,Title,Scope,Party,CoordinatingPerson,Remark,Format(Request,'dd-MM-yyyy')as Request,TechTransfer,Status,Format(EffectiveDt,'dd-MM-yyyy') as EffectiveDt from Agreement where Request between '" + txtFDate.Text + "' and '" + txtTDate.Text + "' and Status='" + drop1.SelectedItem.ToString() + "' and AgreementType='" + ddlAgreeType.SelectedValue.ToString() + "'";
                    cmd1.CommandText = sql1;
                    cmd1.CommandType = CommandType.Text;
                    cmd1.Connection = con;
                    SqlDataReader dr1;
                     dr1 = cmd1.ExecuteReader();
                    //if (dr1.Read() == true)
                    //{
                        DataTable dt1 = new DataTable();
                        dt1.Load(dr1);
                        GvReportDate.DataSource = dt1;

                        GvReportDate.DataBind();
                        dr1.Close();

                    //}
                    //else
                    //{
                    //    lblmsg.Visible = true;

                    //}
                }
            }


        }
    }
    protected void ImageButton2_Click(object sender, ImageClickEventArgs e)
    {
        txtFDate.Text = "";
        txtTDate.Text = "";
        drop1.Text = "";
        ddlAgreeType.Text = "";
    }
}
