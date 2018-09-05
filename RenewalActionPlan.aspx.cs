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
     SqlConnection con = new SqlConnection();
     string IDF;
     protected void Page_Load(object sender, EventArgs e)
     {
         if (Request.QueryString["IDF"] != null)
         {
             IDF = Request.QueryString["IDF"];
         }
         using (SqlConnection con = new SqlConnection())
         {
             con.ConnectionString = ConfigurationManager.ConnectionStrings["PATENTCN"].ConnectionString;
             con.Open();
             using (SqlCommand cmd1 = new SqlCommand())
             {
                 string sql1 = string.Empty;

                 //sql1 = "select FileNo,Description,AmtInFC,AmtInINR,Amt_InCond,Format(DueDate_Restoration,'dd-MM-yyyy') as DueDate_Restoration,Format(Intimation_Date,'dd-MM-yyyy') as Intimation_Date,Format(Confirmation_Date,'dd-MM-yyyy') as Confirmation_Date,Format(POPaymentDate,'dd-MM-yyyy') as POPaymentDate,Status from RenewalFollowup where DueDate between '" + txtFDate.Text + "'and '" + txtTDate.Text + "' and Responsibility='" + ddlResponsibility.SelectedItem.ToString() + "' and Status='" + ddlstatus.SelectedItem.ToString() + "'";

                 sql1 = "select FileNo,Title,FirstApplicant,Inventor1,Applcn_no,Filing_dt,Pat_no,Pat_dt,Status,Sub_Status,Industry1,Commercialized from PatDetails where FileNo='" + IDF.ToString() + "'";


                 cmd1.CommandText = sql1;
                 cmd1.CommandType = CommandType.Text;
                 cmd1.Connection = con;
                 SqlDataReader dr5;
                 dr5 = cmd1.ExecuteReader();
                 DataTable dt1 = new DataTable();
                 dt1.Load(dr5);
                 GvReportDate.DataSource = dt1;
                 GvReportDate.DataBind();
             }
         }
     }
    
}
