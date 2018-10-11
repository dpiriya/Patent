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

public partial class Default2 : System.Web.UI.Page
{
    SqlConnection cnp = new SqlConnection();
    int count;
    string a;
    protected void Page_Load(object sender, EventArgs e)
    {
        divBlock.Visible = false;
        divrenewalEntry.Visible = false;
        if (!this.IsPostBack)
        {
            cnp.ConnectionString = ConfigurationManager.ConnectionStrings["PATENTCN"].ConnectionString;
            SqlCommand cmd = new SqlCommand();
            string sql = "select fileno from patdetails order by cast(fileno as int) desc";
            SqlDataReader dr;
            cmd.CommandType = CommandType.Text;
            cmd.Connection = cnp;
            cmd.CommandText = sql;
            cnp.Open();
            dr = cmd.ExecuteReader();
            ddlIDFNo.Items.Clear();
            ddlIDFNo.Items.Add("");
            dropIDF.Items.Add("");
            while (dr.Read())
            {
                ddlIDFNo.Items.Add(dr.GetString(0));
                dropIDF.Items.Add(dr.GetString(0));
            }
            dr.Close();
            string sql1 = "select SubFileNo from International";
            //SqlDataReader dr1;
            cmd.CommandType = CommandType.Text;
            cmd.Connection = cnp;
            cmd.CommandText = sql1;
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                ddlIDFNo.Items.Add(dr.GetString(0));
                dropIDF.Items.Add(dr.GetString(0));
            }
            dr.Close();
       }
    }


    protected void ddlIdfSubNo_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    //protected void ddlIDFNo_SelectedIndexChanged(object sender, EventArgs e)
    //{
       
           // string Fileno11;
           // cnp.ConnectionString = ConfigurationManager.ConnectionStrings["PATENTCN"].ConnectionString;
           //// string idf = ddlIDFNo.SelectedItem.ToString();
           // SqlCommand cmd1 = new SqlCommand();
           // string sql1 = "select Country,ApplicationNo,FilingDt,FileNo from International where subFileNo='" + ddlIDFNo.SelectedItem.ToString() +"'";
           // SqlDataReader dr;
           // cmd1.CommandType = CommandType.Text;
           // cmd1.Connection = cnp;
           // cmd1.CommandText = sql1;
           // cnp.Open();

           // dr = cmd1.ExecuteReader();
           // if (dr.Read())
           // {
           //     txtCountry.Text = dr[0].ToString();
           //     txtApllicationNo.Text = dr[1].ToString();
           //     txtFilingDtInt.Text = dr[2].ToString();
           //     textFile.Text = dr[3].ToString();
           // }
           // cnp.Close();
           // dr.Close();

           // string sql12 = "select Title,FirstApplicant,Inventor1,Applcn_no,Filing_dt,Pat_no,Pat_dt,Status,Sub_Status from patdetails where FileNo='" + ddlIDFNo.SelectedItem.Value.Trim() + "'";
           // SqlDataReader dr1;
           // cmd1.CommandType = CommandType.Text;
           // cmd1.Connection = cnp;
           // cmd1.CommandText = sql12;
           // cnp.Open();
           // dr1 = cmd1.ExecuteReader();

           // if (dr1.Read())
           // {
           //     txttitle.Text = dr1[0].ToString();
           //     txtApplicant.Text = dr1[1].ToString();
           //     txtInventor.Text = dr1[2].ToString();
           //     txtApplnNo.Text = dr1[3].ToString();
           //     txtFilingDt.Text = dr1[4].ToString();
           //     txtPatentNo.Text = dr1[5].ToString();
           //     txtPatentDate.Text = dr1[6].ToString();
           //     txtStatus.Text = dr1[7].ToString();
           //     txtSubStatus.Text = dr1[8].ToString();
           // }


           // cnp.Close();

           // string sql4 = "select Title,FirstApplicant,Inventor1,Applcn_no,Filing_dt,Pat_no,Pat_dt,Status,Sub_Status from patdetails where FileNo='" + textFile.Text + "'";
           // SqlDataReader dr11;
           // cmd1.CommandType = CommandType.Text;
           // cmd1.Connection = cnp;
           // cmd1.CommandText = sql4;
           // cnp.Open();
           // dr11 = cmd1.ExecuteReader();

           // if (dr11.Read())
           // {
           //     txttitle.Text = dr11[0].ToString();
           //     txtApplicant.Text = dr11[1].ToString();
           //     txtInventor.Text = dr11[2].ToString();
           //     txtApplnNo.Text = dr11[3].ToString();
           //     txtFilingDt.Text = dr11[4].ToString();
           //     txtPatentNo.Text = dr11[5].ToString();
           //     txtPatentDate.Text = dr11[6].ToString();
           //     txtStatus.Text = dr11[7].ToString();
           //     txtSubStatus.Text = dr11[8].ToString();
           // }


           // cnp.Close();

           // //SqlCommand cmd3 = new SqlCommand();
           // //string sql3 = "select Title,FirstApplicant,Inventor1,Applcn_no,Filing_dt,Pat_no,Pat_dt,Status,Sub_Status from patdetails where FileNo='" + ddlIDFNo.SelectedItem.Value.Trim() + "'";
           // //SqlDataReader dr3;
           // //cmd3.CommandType = CommandType.Text;
           // //cmd3.Connection = cnp;
           // //cmd3.CommandText = sql3;
           // //cnp.Open();
           // //dr3 = cmd3.ExecuteReader();
           // //if (dr3.Read())
           // //{
           // //    txttitle.Text = dr3[0].ToString();
           // //    txtApplicant.Text = dr3[1].ToString();
           // //    txtInventor.Text = dr3[2].ToString();
           // //    txtApplnNo.Text = dr3[3].ToString();
           // //    txtFilingDt.Text = dr3[4].ToString();
           // //    txtPatentNo.Text = dr3[5].ToString();
           // //    txtPatentDate.Text = dr3[6].ToString();
           // //    txtStatus.Text = dr3[7].ToString();
           // //    txtSubStatus.Text = dr3[8].ToString();
           // //}
           // //cnp.Close();

           // SqlCommand cmd11 = new SqlCommand("select count(*) from RenewalFollowup where FileNo='" + ddlIDFNo.SelectedItem.Value.Trim() + "'", cnp);
           // cnp.Open();
           // count = Convert.ToInt32(cmd11.ExecuteScalar());
           // if (count < 18)
           // {
           //     txtSlno.Text = Convert.ToInt32(count + 1).ToString();
           // }
           // else
           // {
           //     ClientScript.RegisterStartupScript(GetType(), "Success", "<script>alert('This Record already exist')</script>");
           // }

           // ddlIDFNo.Enabled = false;
           // txttitle.Enabled = false;
           // txtApplicant.Enabled = false;
           // txtInventor.Enabled = false;
           // txtApplnNo.Enabled = false;
           // txtFilingDt.Enabled = false;
           // txtPatentNo.Enabled = false;
           // txtPatentDate.Enabled = false;
           // txtCountry.Enabled = false;
           // txtApllicationNo.Enabled = false;
           // txtFilingDtInt.Enabled = false;
           // txtApplnNo.Enabled = false;
           // txtStatus.Enabled = false;
           // txtSubStatus.Enabled = false;
           // divrenewalEntry.Visible = true;
        //}
    
    protected void imgBtnInsert_Click(object sender, ImageClickEventArgs e)
    {
        System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-GB");
        cnp.ConnectionString = ConfigurationManager.ConnectionStrings["PATENTCN"].ConnectionString;

        //cnp.ConnectionString = ConfigurationManager.ConnectionStrings["PATENTCN"].ConnectionString;
        cnp.Open();


        SqlCommand cmd22 = new SqlCommand("Select count(*) from RenewalFollowup where FileNo='" + dropIDF.SelectedItem.ToString() + "'", cnp);
        SqlDataReader dr = cmd22.ExecuteReader();
        if (dr.Read() == true)
        {
            a = dr[0].ToString();
        }

        cnp.Close();
        if (Convert.ToInt32(a) < 18)
        {

            try
            {
                cnp.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "RenewalNew";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = cnp;


                SqlParameter pm1 = new SqlParameter();
                pm1.ParameterName = "@FileNo";
                pm1.SourceColumn = "FileNo";
                pm1.Value = ddlIDFNo.SelectedItem.ToString();
                pm1.DbType = DbType.String;
                pm1.Direction = ParameterDirection.Input;

                SqlParameter pm3 = new SqlParameter();
                pm3.ParameterName = "@Description";
                pm3.SourceColumn = "Description";
                pm3.Value = txtDescription.Text;
                pm3.DbType = DbType.String;
                pm3.Direction = ParameterDirection.Input;

                SqlParameter pm4 = new SqlParameter();
                pm4.ParameterName = "@Status";
                pm4.SourceColumn = "Status";
                pm4.Value = dropstatus.SelectedItem.ToString();
                pm4.DbType = DbType.String;
                pm4.Direction = ParameterDirection.Input;

                SqlParameter pm5 = new SqlParameter();
                pm5.ParameterName = "@AmtInFC";
                pm5.SourceColumn = "AmtInFC";
                pm5.Value = txtAmtInFc.Text;
                pm5.DbType = DbType.String;
                pm5.Direction = ParameterDirection.Input;

                SqlParameter pm6 = new SqlParameter();
                pm6.ParameterName = "@AmtInINR";
                pm6.SourceColumn = "AmtInINR";
                pm6.Value = txtAmtInINR.Text;
                pm6.DbType = DbType.String;
                pm6.Direction = ParameterDirection.Input;

                SqlParameter pm7 = new SqlParameter();
                pm7.ParameterName = "@DueDate";
                pm7.SourceColumn = "DueDate";
                if (txtDueDate.Text.Trim() == "")
                    pm7.Value = DBNull.Value;
                else
                {
                    DateTime Effective;
                    if (DateTime.TryParse(txtDueDate.Text.Trim(), out Effective))
                        pm7.Value = txtDueDate.Text.Trim();
                    else
                    {
                        ClientScript.RegisterStartupScript(GetType(), "Due Date", "<script>alert('Verify Due Date')</script>");
                        return;
                    }
                }
                pm7.DbType = DbType.String;
                pm7.Direction = ParameterDirection.Input;

                SqlParameter pm8 = new SqlParameter();
                pm8.ParameterName = "@Amt_InCond";
                pm8.SourceColumn = "Amt_InCond";
                pm8.Value = txtAmtOnCond.Text;
                pm8.DbType = DbType.String;
                pm8.Direction = ParameterDirection.Input;

                SqlParameter pm9 = new SqlParameter();
                pm9.ParameterName = "@DueDate_Restoration";
                pm9.SourceColumn = "DueDate_Restoration";
                if (txtDueDateResto.Text.Trim() == "")
                    pm9.Value = DBNull.Value;
                else
                {
                    DateTime Effective;
                    if (DateTime.TryParse(txtDueDateResto.Text.Trim(), out Effective))
                        pm9.Value = txtDueDateResto.Text.Trim();
                    else
                    {
                        ClientScript.RegisterStartupScript(GetType(), "Due Date", "<script>alert('Verify Due Date Restoration')</script>");
                        return;
                    }
                }
                pm9.DbType = DbType.String;
                pm9.Direction = ParameterDirection.Input;

                SqlParameter pm10 = new SqlParameter();
                pm10.ParameterName = "@Responsibility";
                pm10.SourceColumn = "Responsibility";
                pm10.Value = ddlResponsiblity.SelectedItem.ToString();
                pm10.DbType = DbType.String;
                pm10.Direction = ParameterDirection.Input;

                SqlParameter pm11 = new SqlParameter();
                pm11.ParameterName = "@Sharing_Party";
                pm11.SourceColumn = "Sharing_Party";
                pm11.Value = txtSharingParty.Text;
                pm11.DbType = DbType.String;
                pm11.Direction = ParameterDirection.Input;

                SqlParameter pm12 = new SqlParameter();
                pm12.ParameterName = "@Percentage";
                pm12.SourceColumn = "Percentage";
                pm12.Value = txtPercentage.Text;
                pm12.DbType = DbType.String;
                pm12.Direction = ParameterDirection.Input;

                SqlParameter pm13 = new SqlParameter();
                pm13.ParameterName = "@Intimation_Date";
                pm13.SourceColumn = "@Intimation_Date";
                if (txtIntimationDt.Text.Trim() == "")
                    pm13.Value = DBNull.Value;
                else
                {
                    DateTime Effective;
                    if (DateTime.TryParse(txtIntimationDt.Text.Trim(), out Effective))
                        pm13.Value = txtIntimationDt.Text.Trim();
                    else
                    {
                        ClientScript.RegisterStartupScript(GetType(), "Intimation Date", "<script>alert('Verify Intimation Date')</script>");
                        return;
                    }
                }
                pm13.DbType = DbType.String;
                pm13.Direction = ParameterDirection.Input;

                SqlParameter pm14 = new SqlParameter();
                pm14.ParameterName = "@Confirmation_Date";
                pm14.SourceColumn = "Confirmation_Date";
                if (txtConfirmDt.Text.Trim() == "")
                    pm14.Value = DBNull.Value;
                else
                {
                    DateTime Effective;
                    if (DateTime.TryParse(txtConfirmDt.Text.Trim(), out Effective))
                        pm14.Value = txtConfirmDt.Text.Trim();
                    else
                    {
                        ClientScript.RegisterStartupScript(GetType(), "Confirmation Date", "<script>alert('Verify Confirmation Date')</script>");
                        return;
                    }
                }
                pm14.DbType = DbType.String;
                pm14.Direction = ParameterDirection.Input;

                SqlParameter pm15 = new SqlParameter();
                pm15.ParameterName = "@Share_Received";
                pm15.SourceColumn = "Share_Received";
                pm15.Value = ddlShare.SelectedItem.ToString();
                pm15.DbType = DbType.String;
                pm15.Direction = ParameterDirection.Input;

                SqlParameter pm16 = new SqlParameter();
                pm16.ParameterName = "@POPaymentDate";
                pm16.SourceColumn = "POPaymentDate";
                if (txtPaymentDate.Text.Trim() == "")
                    pm16.Value = DBNull.Value;
                else
                {
                    DateTime Effective;
                    if (DateTime.TryParse(txtPaymentDate.Text.Trim(), out Effective))
                        pm16.Value = txtPaymentDate.Text.Trim();
                    else
                    {
                        ClientScript.RegisterStartupScript(GetType(), "Share Received", "<script>alert('Verify Share Date')</script>");
                        return;
                    }
                }
                pm16.DbType = DbType.String;
                pm16.Direction = ParameterDirection.Input;


                SqlParameter pm17 = new SqlParameter();
                pm17.ParameterName = "@UserName";
                pm17.SourceColumn = "UserName";
                pm17.Value = Membership.GetUser().UserName.ToString();
                pm17.DbType = DbType.String;
                pm17.Direction = ParameterDirection.Input;

                SqlParameter pm18 = new SqlParameter();
                pm18.ParameterName = "@SLNo";
                pm18.SourceColumn = "SLNo";
                pm18.Value = txtSlno.Text;
                pm18.DbType = DbType.String;
                pm18.Direction = ParameterDirection.Input;


                cmd.Parameters.Add(pm1);

                cmd.Parameters.Add(pm3);
                cmd.Parameters.Add(pm4);
                cmd.Parameters.Add(pm5);
                cmd.Parameters.Add(pm6);
                cmd.Parameters.Add(pm7);
                cmd.Parameters.Add(pm8);
                cmd.Parameters.Add(pm9);
                cmd.Parameters.Add(pm10);
                cmd.Parameters.Add(pm11);
                cmd.Parameters.Add(pm12);
                cmd.Parameters.Add(pm13);
                cmd.Parameters.Add(pm14);
                cmd.Parameters.Add(pm15);
                cmd.Parameters.Add(pm16);
                cmd.Parameters.Add(pm17);
                cmd.Parameters.Add(pm18);
                cmd.ExecuteNonQuery();
                cnp.Close();

                SqlCommand cmd12 = new SqlCommand("select count(*) from RenewalFollowup where FileNo='" + ddlIDFNo.SelectedItem.Value.Trim() + "'", cnp);
                cnp.Open();
                count = Convert.ToInt32(cmd12.ExecuteScalar());

                txtSlno.Text = Convert.ToInt32(count + 1).ToString();
                txtDescription.Text = "";
                txtAmtInFc.Text = "";
                txtAmtInINR.Text = "";
                txtAmtOnCond.Text = "";
                txtDueDate.Text = "";
                txtDueDateResto.Text = "";
                ddlResponsiblity.Text = "";
                txtSharingParty.Text = "";
                txtPercentage.Text = "";
                txtIntimationDt.Text = "";
                txtConfirmDt.Text = "";
                ddlShare.Text = "";
                txtPaymentDate.Text = "";
                dropstatus.Text = "";
                ClientScript.RegisterStartupScript(GetType(), "Success", "<script>alert('This Record Submitted Successfully')</script>");

            }



            catch (Exception ex)
            {
                ClientScript.RegisterStartupScript(GetType(), "Error", "<script>alert('" + ex.Message.ToString() + "')</script>");
                cnp.Close();
            }
        }
        else
        {
            ClientScript.RegisterStartupScript(GetType(), "Success", "<script>alert('This Record already exist')</script>");
        }
    }
    protected void imgBtnClear_Click(object sender, ImageClickEventArgs e)
    {
        txtDescription.Text = "";
        txtAmtInFc.Text = "";
        txtAmtInINR.Text = "";
        txtAmtOnCond.Text = "";
        txtDueDate.Text = "";
        txtDueDateResto.Text = "";
        ddlResponsiblity.Text = "";
        txtSharingParty.Text = "";
        txtPercentage.Text = "";
        txtIntimationDt.Text = "";
        txtConfirmDt.Text = "";
        ddlShare.Text = "";
        txtPaymentDate.Text = "";
        dropstatus.Text = "";
    }
    protected void txtFilingDt_TextChanged(object sender, EventArgs e)
    {

    }
    protected void BtnSubmit_Click(object sender, EventArgs e)
    {
        Divrenewal.Visible = false;
        if (radioSingle.Checked == true)
        {
            divrenewalEntry.Visible = true;
            divBlock.Visible = false;
        }

   
        else
        {
            divrenewalEntry.Visible = false;
            divBlock.Visible = true;
        }
    }
    protected void radioSingle_CheckedChanged(object sender, EventArgs e)
    {

    }
    protected void buttonSubmit_Click(object sender, EventArgs e)
    {
        string a11;
        cnp.ConnectionString = ConfigurationManager.ConnectionStrings["PATENTCN"].ConnectionString;
        cnp.Open();


        SqlCommand cmd22 = new SqlCommand("Select count(*) from RenewalFollowup where FileNo='" + dropIDF.SelectedItem.ToString() + "'", cnp);
        SqlDataReader dr = cmd22.ExecuteReader();
        if (dr.Read() == true)
        {
            a = dr[0].ToString();
        }

        cnp.Close();
        if (Convert.ToInt32(a) < 1)
        {
            cnp.Open();
            SqlCommand cmd23 = new SqlCommand("Select Filing_dt from PatDetails where FileNo='" + dropIDF.SelectedItem.ToString() + "'", cnp);
            SqlDataReader dr1 = cmd23.ExecuteReader();
            if (dr1.Read())
            {
                // a11 = Convert.ToDateTime(dr1.ToString());
                a11 = dr1[0].ToString();

                cnp.Close();
                cnp.Open();
                SqlCommand cmd20 = new SqlCommand("Update Renewal_Schedule set [IDF No]='" + dropIDF.SelectedItem.ToString() + "' where Indian_Foreign='Indian'", cnp);
                cmd20.ExecuteNonQuery();
                cnp.Close();

                cnp.Open();
                SqlCommand cmd21 = new SqlCommand("Update Renewal_Schedule set UserName = '" + Membership.GetUser().UserName.ToString() + "' where Indian_Foreign='Indian'", cnp);
                cmd21.ExecuteNonQuery();
                cnp.Close();

                cnp.Open();
                SqlCommand cmd18 = new SqlCommand("insert into RenewalFollowup select * from Renewal_Schedule where where Indian_Foreign='Indian'", cnp);
                cmd18.ExecuteNonQuery();
                cnp.Close();

                cnp.Open();
                SqlCommand cmd = new SqlCommand("Update RenewalFollowup set DueDate= (select DATEADD(YEAR,2,((Select Filing_dt from PatDetails where FileNo='" + dropIDF.SelectedItem.ToString() + "')-1))from RenewalFollowup where Description='For 3rd year' and  FileNo='" + dropIDF.SelectedItem.ToString() + "') where Description='For 3rd year' and FileNo='" + dropIDF.SelectedItem.ToString() + "'", cnp);
                cmd.ExecuteNonQuery();
                cnp.Close();
                string b = a11;
                cnp.Open();
                SqlCommand cmd1 = new SqlCommand("Update RenewalFollowup set DueDate= (select DATEADD(YEAR,3,(Select Filing_dt from PatDetails where FileNo='" + dropIDF.SelectedItem.ToString() + "'))from RenewalFollowup where Description='For 4th year' and FileNo='" + dropIDF.SelectedItem.ToString() + "') where Description='For 4th year' and FileNo='" + dropIDF.SelectedItem.ToString() + "'", cnp);
                cmd1.ExecuteNonQuery();
                cnp.Close();

                cnp.Open();
                SqlCommand cmd2 = new SqlCommand("Update RenewalFollowup set DueDate= (select DATEADD(YEAR,4,(Select Filing_dt from PatDetails where FileNo='" + dropIDF.SelectedItem.ToString() + "'))from RenewalFollowup where Description='For 5th year' and FileNo='" + dropIDF.SelectedItem.ToString() + "') where Description='For 5th year' and FileNo='" + dropIDF.SelectedItem.ToString() + "'", cnp);
                cmd2.ExecuteNonQuery();
                cnp.Close();

                cnp.Open();
                SqlCommand cmd3 = new SqlCommand("Update RenewalFollowup set DueDate= (select DATEADD(YEAR,5,(Select Filing_dt from PatDetails where FileNo='" + dropIDF.SelectedItem.ToString() + "'))from RenewalFollowup where Description='For 6th year' and FileNo='" + dropIDF.SelectedItem.ToString() + "') where Description='For 6th year' and FileNo='" + dropIDF.SelectedItem.ToString() + "'", cnp);
                cmd3.ExecuteNonQuery();
                cnp.Close();

                cnp.Open();
                SqlCommand cmd4 = new SqlCommand("Update RenewalFollowup set DueDate= (select DATEADD(YEAR,6,(Select Filing_dt from PatDetails where FileNo='" + dropIDF.SelectedItem.ToString() + "'))from RenewalFollowup where Description='For 7th year' and FileNo='" + dropIDF.SelectedItem.ToString() + "') where Description='For 7th year' and FileNo='" + dropIDF.SelectedItem.ToString() + "'", cnp);
                cmd4.ExecuteNonQuery();
                cnp.Close();

                cnp.Open();
                SqlCommand cmd5 = new SqlCommand("Update RenewalFollowup set DueDate= (select DATEADD(YEAR,7,(Select Filing_dt from PatDetails where FileNo='" + dropIDF.SelectedItem.ToString() + "'))from RenewalFollowup where Description='For 8th year' and FileNo='" + dropIDF.SelectedItem.ToString() + "') where Description='For 8th year' and FileNo='" + dropIDF.SelectedItem.ToString() + "'", cnp);
                cmd5.ExecuteNonQuery();
                cnp.Close();

                cnp.Open();
                SqlCommand cmd6 = new SqlCommand("Update RenewalFollowup set DueDate= (select DATEADD(YEAR,8,(Select Filing_dt from PatDetails where FileNo='" + dropIDF.SelectedItem.ToString() + "'))from RenewalFollowup where Description='For 9th year' and FileNo='" + dropIDF.SelectedItem.ToString() + "') where Description='For 9th year' and FileNo='" + dropIDF.SelectedItem.ToString() + "'", cnp);
                cmd6.ExecuteNonQuery();
                cnp.Close();

                cnp.Open();
                SqlCommand cmd7 = new SqlCommand("Update RenewalFollowup set DueDate= (select DATEADD(YEAR,9,(Select Filing_dt from PatDetails where FileNo='" + dropIDF.SelectedItem.ToString() + "'))from RenewalFollowup where Description='For 10th year' and FileNo='" + dropIDF.SelectedItem.ToString() + "') where Description='For 10th year' and FileNo='" + dropIDF.SelectedItem.ToString() + "'", cnp);
                cmd7.ExecuteNonQuery();
                cnp.Close();

                cnp.Open();
                SqlCommand cmd8 = new SqlCommand("Update RenewalFollowup set DueDate= (select DATEADD(YEAR,10,(Select Filing_dt from PatDetails where FileNo='" + dropIDF.SelectedItem.ToString() + "'))from RenewalFollowup where Description='For 11th year' and FileNo='" + dropIDF.SelectedItem.ToString() + "') where Description='For 11th year' and FileNo='" + dropIDF.SelectedItem.ToString() + "'", cnp);
                cmd8.ExecuteNonQuery();
                cnp.Close();

                cnp.Open();
                SqlCommand cmd9 = new SqlCommand("Update RenewalFollowup set DueDate= (select DATEADD(YEAR,11,(Select Filing_dt from PatDetails where FileNo='" + dropIDF.SelectedItem.ToString() + "'))from RenewalFollowup where Description='For 12th year' and FileNo='" + dropIDF.SelectedItem.ToString() + "') where Description='For 12th year' and FileNo='" + dropIDF.SelectedItem.ToString() + "'", cnp);
                cmd9.ExecuteNonQuery();
                cnp.Close();

                cnp.Open();
                SqlCommand cmd10 = new SqlCommand("Update RenewalFollowup set DueDate= (select DATEADD(YEAR,12,(Select Filing_dt from PatDetails where FileNo='" + dropIDF.SelectedItem.ToString() + "'))from RenewalFollowup where Description='For 13th year' and FileNo='" + dropIDF.SelectedItem.ToString() + "') where Description='For 13th year' and FileNo='" + dropIDF.SelectedItem.ToString() + "'", cnp);
                cmd10.ExecuteNonQuery();
                cnp.Close();

                cnp.Open();
                SqlCommand cmd11 = new SqlCommand("Update RenewalFollowup set DueDate= (select DATEADD(YEAR,13,(Select Filing_dt from PatDetails where FileNo='" + dropIDF.SelectedItem.ToString() + "'))from RenewalFollowup where Description='For 14th year' and FileNo='" + dropIDF.SelectedItem.ToString() + "') where Description='For 14th year' and FileNo='" + dropIDF.SelectedItem.ToString() + "'", cnp);
                cmd11.ExecuteNonQuery();
                cnp.Close();

                cnp.Open();
                SqlCommand cmd12 = new SqlCommand("Update RenewalFollowup set DueDate= (select DATEADD(YEAR,14,(Select Filing_dt from PatDetails where FileNo='" + dropIDF.SelectedItem.ToString() + "'))from RenewalFollowup where Description='For 15th year' and FileNo='" + dropIDF.SelectedItem.ToString() + "') where Description='For 15th year' and FileNo='" + dropIDF.SelectedItem.ToString() + "'", cnp);
                cmd12.ExecuteNonQuery();
                cnp.Close();

                cnp.Open();
                SqlCommand cmd13 = new SqlCommand("Update RenewalFollowup set DueDate= (select DATEADD(YEAR,15,(Select Filing_dt from PatDetails where FileNo='" + dropIDF.SelectedItem.ToString() + "'))from RenewalFollowup where Description='For 16th year' and FileNo='" + dropIDF.SelectedItem.ToString() + "') where Description='For 16th year' and FileNo='" + dropIDF.SelectedItem.ToString() + "'", cnp);
                cmd13.ExecuteNonQuery();
                cnp.Close();

                cnp.Open();
                SqlCommand cmd14 = new SqlCommand("Update RenewalFollowup set DueDate= (select DATEADD(YEAR,16,(Select Filing_dt from PatDetails where FileNo='" + dropIDF.SelectedItem.ToString() + "'))from RenewalFollowup where Description='For 17th year' and FileNo='" + dropIDF.SelectedItem.ToString() + "') where Description='For 17th year' and FileNo='" + dropIDF.SelectedItem.ToString() + "'", cnp);
                cmd14.ExecuteNonQuery();
                cnp.Close();

                cnp.Open();
                SqlCommand cmd15 = new SqlCommand("Update RenewalFollowup set DueDate= (select DATEADD(YEAR,17,(Select Filing_dt from PatDetails where FileNo='" + dropIDF.SelectedItem.ToString() + "'))from RenewalFollowup where Description='For 18th year' and FileNo='" + dropIDF.SelectedItem.ToString() + "') where Description='For 18th year' and FileNo='" + dropIDF.SelectedItem.ToString() + "'", cnp);
                cmd15.ExecuteNonQuery();
                cnp.Close();

                cnp.Open();
                SqlCommand cmd16 = new SqlCommand("Update RenewalFollowup set DueDate= (select DATEADD(YEAR,18,(Select Filing_dt from PatDetails where FileNo='" + dropIDF.SelectedItem.ToString() + "'))from RenewalFollowup where Description='For 19th year' and FileNo='" + dropIDF.SelectedItem.ToString() + "') where Description='For 19th year' and FileNo='" + dropIDF.SelectedItem.ToString() + "'", cnp);
                cmd16.ExecuteNonQuery();
                cnp.Close();

                cnp.Open();
                SqlCommand cmd17 = new SqlCommand("Update RenewalFollowup set DueDate= (select DATEADD(YEAR,19,(Select Filing_dt from PatDetails where FileNo='" + dropIDF.SelectedItem.ToString() + "'))from RenewalFollowup where Description='For 20th year' and FileNo='" + dropIDF.SelectedItem.ToString() + "') where Description='For 20th year' and FileNo='" + dropIDF.SelectedItem.ToString() + "'", cnp);
                cmd17.ExecuteNonQuery();
                cnp.Close();

                ClientScript.RegisterStartupScript(GetType(), "Success", "<script>alert('This Record Submitted Successfully')</script>");
            }
        }
        else
        {
            ClientScript.RegisterStartupScript(GetType(), "Success", "<script>alert('This Record already exist')</script>");
        }



      



    }
    protected void btnFind_Click(object sender, EventArgs e)
    {
         //string Fileno11;
            cnp.ConnectionString = ConfigurationManager.ConnectionStrings["PATENTCN"].ConnectionString;
           // string idf = ddlIDFNo.SelectedItem.ToString();
            SqlCommand cmd1 = new SqlCommand();
            string sql1 = "select Country,ApplicationNo,FilingDt,FileNo from International where subFileNo='" + ddlIDFNo.SelectedItem.ToString() +"'";
            SqlDataReader dr;
            cmd1.CommandType = CommandType.Text;
            cmd1.Connection = cnp;
            cmd1.CommandText = sql1;
            cnp.Open();

            dr = cmd1.ExecuteReader();
            if (dr.Read())
            {
                txtCountry.Text = dr[0].ToString();
                txtApllicationNo.Text = dr[1].ToString();
                txtFilingDtInt.Text = dr[2].ToString();
                textFile.Text = dr[3].ToString();
            }
            cnp.Close();
            dr.Close();

            string sql12 = "select Title,FirstApplicant,Inventor1,Applcn_no,Filing_dt,Pat_no,Pat_dt,Status,Sub_Status from patdetails where FileNo='" + ddlIDFNo.SelectedItem.Value.Trim() + "'";
            SqlDataReader dr1;
            cmd1.CommandType = CommandType.Text;
            cmd1.Connection = cnp;
            cmd1.CommandText = sql12;
            cnp.Open();
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
            }


            cnp.Close();

            string sql4 = "select Title,FirstApplicant,Inventor1,Applcn_no,Filing_dt,Pat_no,Pat_dt,Status,Sub_Status from patdetails where FileNo='" + textFile.Text + "'";
            SqlDataReader dr11;
            cmd1.CommandType = CommandType.Text;
            cmd1.Connection = cnp;
            cmd1.CommandText = sql4;
            cnp.Open();
            dr11 = cmd1.ExecuteReader();

            if (dr11.Read())
            {
                txttitle.Text = dr11[0].ToString();
                txtApplicant.Text = dr11[1].ToString();
                txtInventor.Text = dr11[2].ToString();
                txtApplnNo.Text = dr11[3].ToString();
                txtFilingDt.Text = dr11[4].ToString();
                txtPatentNo.Text = dr11[5].ToString();
                txtPatentDate.Text = dr11[6].ToString();
                txtStatus.Text = dr11[7].ToString();
                txtSubStatus.Text = dr11[8].ToString();
            }


            cnp.Close();

            //SqlCommand cmd3 = new SqlCommand();
            //string sql3 = "select Title,FirstApplicant,Inventor1,Applcn_no,Filing_dt,Pat_no,Pat_dt,Status,Sub_Status from patdetails where FileNo='" + ddlIDFNo.SelectedItem.Value.Trim() + "'";
            //SqlDataReader dr3;
            //cmd3.CommandType = CommandType.Text;
            //cmd3.Connection = cnp;
            //cmd3.CommandText = sql3;
            //cnp.Open();
            //dr3 = cmd3.ExecuteReader();
            //if (dr3.Read())
            //{
            //    txttitle.Text = dr3[0].ToString();
            //    txtApplicant.Text = dr3[1].ToString();
            //    txtInventor.Text = dr3[2].ToString();
            //    txtApplnNo.Text = dr3[3].ToString();
            //    txtFilingDt.Text = dr3[4].ToString();
            //    txtPatentNo.Text = dr3[5].ToString();
            //    txtPatentDate.Text = dr3[6].ToString();
            //    txtStatus.Text = dr3[7].ToString();
            //    txtSubStatus.Text = dr3[8].ToString();
            //}
            //cnp.Close();

            SqlCommand cmd11 = new SqlCommand("select count(*) from RenewalFollowup where FileNo='" + ddlIDFNo.SelectedItem.Value.Trim() + "'", cnp);
            cnp.Open();
            count = Convert.ToInt32(cmd11.ExecuteScalar());
            if (count < 18)
            {
                txtSlno.Text = Convert.ToInt32(count + 1).ToString();
            }
            else
            {
                ClientScript.RegisterStartupScript(GetType(), "Success", "<script>alert('This Record already exist')</script>");
            }

            ddlIDFNo.Enabled = false;
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
            divrenewalEntry.Visible = true;
        }

    }

