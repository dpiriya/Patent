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

    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void ImageReportButn1_Click1(object sender, EventArgs e)
    {
        using (SqlConnection con = new SqlConnection())
        {
            con.ConnectionString = ConfigurationManager.ConnectionStrings["PATENTCN"].ConnectionString;
            con.Open();
            using (SqlCommand cmd1 = new SqlCommand())
            {
                string sql2 = string.Empty, sql3 = string.Empty, sql4=string.Empty ;
                string sql1= "select FileNo,Description,Responsibility,Format(DueDate, 'dd-MM-yyyy') as DueDate,AmtInFC,AmtInINR,Status,Format(POPaymentDate, 'dd-MM-yyyy') as POPaymentDate from RenewalFollowup";
                if(txtFDate.Text!="" && txtTDate.Text!="")
                {
                     sql2=" where DueDate between '" + txtFDate.Text + "' and '" + txtTDate.Text + "'";
                }
                if(ddlResponsibility.SelectedItem.Text!="All")
                {
                    sql2 += (sql2 != string.Empty)?  " and " : " where ";
                    sql2+=" Responsibility='" + ddlResponsibility.SelectedItem.ToString() + "'";
                }                
                if(ddlstatus.SelectedItem.Text!="All")
                {
                    sql3 = (sql2 != string.Empty) ? " and " : " where ";
                    sql3 += "Status='" + ddlstatus.SelectedItem.ToString() + "' ";
                }
                if(ddlPhase.SelectedItem.Text!="Both")
                {
                    sql4 = (sql2 != string.Empty || sql3 != string.Empty) ? " and " : " where ";
                    sql4 += "Phase='" + ddlPhase.SelectedValue + "'";
                }



                //if (ddlResponsibility.SelectedItem.Text != "All" && ddlstatus.SelectedItem.Text != "All")
                //{
                //    //sql1 = "select FileNo,Description,AmtInFC,AmtInINR,Amt_InCond,Format(DueDate_Restoration,'dd-MM-yyyy') as DueDate_Restoration,Format(Intimation_Date,'dd-MM-yyyy') as Intimation_Date,Format(Confirmation_Date,'dd-MM-yyyy') as Confirmation_Date,Format(POPaymentDate,'dd-MM-yyyy') as POPaymentDate,Status from RenewalFollowup where DueDate between '" + txtFDate.Text + "'and '" + txtTDate.Text + "' and Responsibility='" + ddlResponsibility.SelectedItem.ToString() + "' and Status='" + ddlstatus.SelectedItem.ToString() + "'";

                //    sql1 = "select FileNo,Description,Responsibility,Format(DueDate,'dd-MM-yyyy') as DueDate,AmtInFC,AmtInINR,Status,Format(POPaymentDate,'dd-MM-yyyy') as POPaymentDate from RenewalFollowup where DueDate between '" + txtFDate.Text + "'and '" + txtTDate.Text + "' and Responsibility='" + ddlResponsibility.SelectedItem.ToString() + "' and Status='" + ddlstatus.SelectedItem.ToString() + "'";
                //}
                //else if (ddlResponsibility.SelectedItem.Text == "All" && ddlstatus.SelectedItem.Text != "All")
                //{
                //    //sql1 = "select FileNo,Description,AmtInFC,AmtInINR,Amt_InCond,Format(DueDate_Restoration,'dd-MM-yyyy') as DueDate_Restoration,Format(Intimation_Date,'dd-MM-yyyy') as Intimation_Date,Format(Confirmation_Date,'dd-MM-yyyy') as Confirmation_Date,Format(POPaymentDate,'dd-MM-yyyy') as POPaymentDate,Status from RenewalFollowup where DueDate between '" + txtFDate.Text + "'and '" + txtTDate.Text + "' and Status='" + ddlstatus.SelectedItem.ToString() + "'";
                //    sql1 = "select FileNo,Description,Responsibility,Format(DueDate,'dd-MM-yyyy') as DueDate,AmtInFC,AmtInINR,Status,Format(POPaymentDate,'dd-MM-yyyy') as POPaymentDate from RenewalFollowup where DueDate between '" + txtFDate.Text + "'and '" + txtTDate.Text + "' and Status='" + ddlstatus.SelectedItem.ToString() + "'";
                //}
                //else if (ddlstatus.SelectedItem.Text == "All" && ddlResponsibility.SelectedItem.ToString() != "All")
                //{
                //    //sql1 = "select FileNo,Description,AmtInFC,AmtInINR,Amt_InCond,Format(DueDate_Restoration,'dd-MM-yyyy') as DueDate_Restoration,Format(Intimation_Date,'dd-MM-yyyy') as Intimation_Date,Format(Confirmation_Date,'dd-MM-yyyy') as Confirmation_Date,Format(POPaymentDate,'dd-MM-yyyy') as POPaymentDate,Status from RenewalFollowup where DueDate between '" + txtFDate.Text + "'and '" + txtTDate.Text + "' and Responsibility='" + ddlResponsibility.SelectedItem.ToString() + "'";
                //    sql1 = "select FileNo,Description,Responsibility,Format(DueDate,'dd-MM-yyyy') as DueDate,AmtInFC,AmtInINR,Status,Format(POPaymentDate,'dd-MM-yyyy') as POPaymentDate from RenewalFollowup where DueDate between '" + txtFDate.Text + "'and '" + txtTDate.Text + "' and Responsibility='" + ddlResponsibility.SelectedItem.ToString() + "'";

                //}
                //else
                //{
                //    //sql1 = "select FileNo,Description,AmtInFC,AmtInINR,Amt_InCond,Format(DueDate_Restoration,'dd-MM-yyyy') as DueDate_Restoration,Format(Intimation_Date,'dd-MM-yyyy') as Intimation_Date,Format(Confirmation_Date,'dd-MM-yyyy') as Confirmation_Date,Format(POPaymentDate,'dd-MM-yyyy') as POPaymentDate,Status from RenewalFollowup where DueDate between '" + txtFDate.Text + "'and '" + txtTDate.Text + "'";
                //    sql1 = "select FileNo,Description,Responsibility,Format(DueDate,'dd-MM-yyyy') as DueDate,AmtInFC,AmtInINR,Status,Format(POPaymentDate,'dd-MM-yyyy') as POPaymentDate from RenewalFollowup where DueDate between '" + txtFDate.Text + "'and '" + txtTDate.Text + "'";
                //}
                string sql = sql1 + sql2 + sql3 + sql4;
                cmd1.CommandText = sql;
                cmd1.CommandType = CommandType.Text;
                cmd1.Connection = con;
                SqlDataReader dr5;
                dr5 = cmd1.ExecuteReader();
                DataTable dt1 = new DataTable();
                dt1.Load(dr5);
                GvReportDate.Visible = true;
                GridReportAmount.Visible = false;
                GvReportDate.DataSource = dt1;
                GvReportDate.DataBind();
            }
        }
    }
    protected void ImageReportButn_Click(object sender, EventArgs e)
    {
        using (SqlConnection con = new SqlConnection())
        {
            con.ConnectionString = ConfigurationManager.ConnectionStrings["PATENTCN"].ConnectionString;
            con.Open();
            using (SqlCommand cmd1 = new SqlCommand())
            {
                string sql2 = string.Empty, sql3 = string.Empty, sql4 = string.Empty;
                string sql1 = "select FileNo,Description,Responsibility,Format(DueDate, 'dd-MM-yyyy') as DueDate,AmtInINR,Sharing_Party,Intimation_Date,Confirmation_Date,Share_Received from RenewalFollowup";
                if (txtFDate.Text != "" && txtTDate.Text != "")
                {
                    sql2 = " where DueDate between '" + txtFDate.Text + "'and '" + txtTDate.Text + "'";
                }
                if (ddlResponsibility.SelectedItem.Text != "All")
                {
                    sql2 += (sql2 != string.Empty) ? " and " : " where ";
                    sql2 += " Responsibility='" + ddlResponsibility.SelectedItem.ToString() + "'";
                }
                if (ddlstatus.SelectedItem.Text != "All")
                {
                    sql3 = (sql2 != string.Empty) ? " and " : " where ";
                    sql3 += "Status='" + ddlstatus.SelectedItem.ToString() + "' ";
                }
                if (ddlPhase.SelectedItem.Text != "Both")
                {
                    sql4 = (sql2 != string.Empty || sql3 != string.Empty) ? " and " : " where ";
                    sql4 += "Phase='" + ddlPhase.SelectedValue + "'";
                }
                string sql = sql1 + sql2 + sql3 + sql4;
                cmd1.CommandText = sql;
                cmd1.CommandType = CommandType.Text;
                cmd1.Connection = con;
                SqlDataReader dr5;
                dr5 = cmd1.ExecuteReader();
                DataTable dt1 = new DataTable();
                dt1.Load(dr5);
                GvReportDate.Visible = false;
                GridReportAmount.Visible = true;
                GridReportAmount.DataSource = dt1;
                GridReportAmount.DataBind();
            }
        }
    }
}




