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


public partial class RenewalActionReport : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection();
    string IDF;
    protected void Page_Load(object sender, EventArgs e)
    {
        string sql1;
        if (Request.QueryString["IDF"] != null)
        {
            IDF = Request.QueryString["IDF"];
        } 
        con.ConnectionString = ConfigurationManager.ConnectionStrings["PATENTCN"].ConnectionString;
        con.Open();
        txtIDF.Text = IDF.ToString();
        using (SqlCommand cmd1 = new SqlCommand())
        {
            //sql1 = "select FileNo,Title,FirstApplicant,Inventor1,Applcn_no,Filing_dt,Pat_no,Pat_dt,Status,Sub_Status,Industry1,Commercialized from PatDetails where FileNo='" + IDF.ToString() + "'";

            //SqlDataAdapter sda = new SqlDataAdapter(sql1, con);
            //DataSet ds = new DataSet();
            //sda.Fill(ds);
            //gvAction.DataSource = ds.Tables[0];
            //gvAction.DataBind();
            //con.Close();
            //con.Open();

           string sql2 = "select FileNo from International where subFileNo='" + IDF.ToString() + "'";
            SqlDataReader dr7;
            cmd1.CommandType = CommandType.Text;
            cmd1.Connection = con;
            cmd1.CommandText = sql2;
            dr7 = cmd1.ExecuteReader();
            if (dr7.Read())
            {
                textFile.Text = dr7[0].ToString();
            }
            con.Close();
            //sql1 = "select FileNo,Title,FirstApplicant,Inventor1,Applcn_no,Filing_dt,Pat_no,Pat_dt,Status,Sub_Status,Industry1,Commercialized from PatDetails where FileNo='" + textFile.Text + "'";
            //con.Open();
            //SqlDataAdapter sda1 = new SqlDataAdapter(sql1, con);
            //DataSet ds1 = new DataSet();
            //sda1.Fill(ds1);
            //GridView1.DataSource = ds1.Tables[0];
            //GridView1.DataBind();
            //con.Close();
            //con.Open();
            //sql1 = "select Country,ApplicationNo,FilingDt,FileNo,PatentNo,PatentDt,Status from International where subFileNo='" + IDF.ToString() + "'";
            //SqlDataAdapter sda11 = new SqlDataAdapter(sql1, con);
            //DataSet ds11 = new DataSet();
            
            //sda11.Fill(ds11);
            //GridIntern.DataSource = ds11.Tables[0];
            //GridIntern.DataBind();
            //con.Close();

            //sql1 = "select FileNo,Title,FirstApplicant,Inventor1,Applcn_no,Filing_dt,Pat_no,Pat_dt,Status,Sub_Status,Industry1,Commercialized from PatDetails where FileNo='" + textFile.Text + "'";
            //con.Open();
            //SqlDataAdapter sda2 = new SqlDataAdapter(sql1, con);
            //DataSet ds2 = new DataSet();
            //sda1.Fill(ds2);
            //GridView1.DataSource = ds2.Tables[0];
            //GridView1.DataBind();
            //con.Close();

            con.Open();
            sql1 = "select FileNo,Description,AmtInFC,AmtInINR,Format(DueDate,'dd-MM-yyyy')as DueDate,Amt_InCond,Format(DueDate_Restoration,'dd-MM-yyyy')as DueDate_Restoration,Responsibility,Sharing_Party,Percentage,Format(Intimation_Date,'dd-MM-yyyy')as Intimation_Date ,Format(Confirmation_Date,'dd-MM-yyyy')as Confirmation_Date,Share_Received,Format(POPaymentDate,'dd-MM-yyyy') as POPaymentDate,Status from RenewalFollowup where FileNo='" + IDF.ToString() + "'";
            SqlDataAdapter sda12 = new SqlDataAdapter(sql1, con);
            DataSet ds12 = new DataSet();

            sda12.Fill(ds12);
            GridActionPlan.DataSource = ds12.Tables[0];
            GridActionPlan.DataBind();
            con.Close();


      
        con.Open();
        sql1 = "select Country,ApplicationNo,FilingDt,FileNo from International where subFileNo='" + IDF.ToString() + "'";
            SqlDataReader dr;
            cmd1.CommandType = CommandType.Text;
            cmd1.Connection = con;
            cmd1.CommandText = sql1;
  

            dr = cmd1.ExecuteReader();
            // Fileno11 = dr[3].ToString();
            if (dr.Read() == true)
            {
                txtCountry.Text = dr[0].ToString();
                txtApllicationNo.Text = dr[1].ToString();
                txtFilingDtInt.Text = dr[2].ToString();
                textFile.Text = dr[3].ToString();
            }
            else
            {
            }
        
            dr.Close();

            string sql12 = "select Title,FirstApplicant,Inventor1,Applcn_no,Format(Filing_dt,'dd-MM-yyyy')as Filing_dt,Pat_no,Pat_dt,Status,Sub_Status,Industry1,Commercialized from patdetails where FileNo='" + IDF.ToString() + "'";
            SqlDataReader dr1;
            cmd1.CommandType = CommandType.Text;
            cmd1.Connection = con;
            cmd1.CommandText = sql12;
     
            dr1 = cmd1.ExecuteReader();

            if (dr1.Read())
            {
                txttitle.Text = dr1[0].ToString();
                txtApplicant.Text = dr1[1].ToString();
                txtInventor.Text = dr1[2].ToString();
                txtApplnNo.Text = dr1[3].ToString();
                txtFilingDt.Text = dr1[4].ToString();
                txtPatentNo.Text = dr1[5].ToString();
                txtPatentDate.Text = dr1[6].ToString();
                txtStatus.Text = dr1[7].ToString();
                txtSubStatus.Text = dr1[8].ToString();
                txtIndustry.Text = dr1[9].ToString();
                txtcommer.Text = dr1[10].ToString();
            }

            

        }


        txttitle.Enabled = false;
        txtApplicant.Enabled = false;
        txtInventor.Enabled = false;
        txtApplnNo.Enabled = false;
        txtFilingDt.Enabled = false;
        txtPatentNo.Enabled = false;
        txtPatentDate.Enabled = false;
        txtCountry.Enabled = false;
        txtApllicationNo.Enabled = false;
        txtFilingDtInt.Enabled = false;
        txtApplnNo.Enabled = false;
        txtStatus.Enabled = false;
        txtSubStatus.Enabled = false;
        txtcommer.Enabled = false;
        txtIndustry.Enabled = false;
    }

    protected void linkback_Click(object sender, EventArgs e)
    {
        Response.Redirect("RenewalActionPlanReport.aspx");
    }
}

