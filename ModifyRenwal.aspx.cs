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
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!User.IsInRole("Intern"))
        {
            if (!this.IsPostBack)
            {
                cnp.ConnectionString = ConfigurationManager.ConnectionStrings["PATENTCN"].ConnectionString;
                SqlCommand cmd = new SqlCommand();
                string sql = "select distinct fileno from RenewalFollowup order by fileno desc";
                SqlDataReader dr;
                cmd.CommandType = CommandType.Text;
                cmd.Connection = cnp;
                cmd.CommandText = sql;
                cnp.Open();
                dr = cmd.ExecuteReader();
                ddlIDFNo.Items.Clear();
                ddlIDFNo.Items.Add("");
                while (dr.Read())
                {
                    ddlIDFNo.Items.Add(dr.GetString(0));
                }
                dr.Close();
                ddlphase.Items.Clear();
                ddlphase.Items.Add("Maintenance");
                ddlphase.Items.Add("Prosecution");
            }
        }
        else
        {
            Server.Transfer("Unauthorized.aspx");
        }
    }

    protected void btnFind_Click(object sender, EventArgs e)
    {
        cnp.ConnectionString = ConfigurationManager.ConnectionStrings["PATENTCN"].ConnectionString;
        DataTable dtAction = new DataTable();
        string sql2 = "select SLNo,FileNo,AmtInFC,AmtInINR,Format(DueDate,'dd-MM-yyyy')as DueDate,Format(DueDate_Restoration,'dd-MM-yyyy') as DueDate_Restoration,Responsibility,Sharing_Party,Format(Intimation_Date,'dd-MM-yyyy') as Intimation_Date,Format(Confirmation_Date,'dd-MM-yyyy') as Confirmation_Date,Share_Received,Format(POPaymentDate,'dd-MM-yyyy') as PoPaymentDate,Status,Description from RenewalFollowup where FileNo='" + ddlIDFNo.SelectedItem.ToString() + "' and Phase='"+ddlphase.SelectedValue+"'";
        SqlDataAdapter sda = new SqlDataAdapter(sql2, cnp);
        sda.Fill(dtAction);
        lvAction.DataSource = dtAction;
        lvAction.DataBind();
        sda.Dispose();
        PanelList.Visible = true;
    }

    protected void lvAction_ItemDataBound(object sender, ListViewItemEventArgs e)
    {

    }
    protected void lvAction_ItemCommand(object sender, ListViewCommandEventArgs e)
    {
        Label tmpLabel = new Label();
        tmpLabel = (Label)e.Item.FindControl("lblSlNo");
        if (e.CommandName == "Modify")
        {
            btnNewAction.Visible = false;
            btnActSave.Visible = false;
            btnActUpdate.Visible = true;
            BindAction(ddlIDFNo.SelectedItem.ToString(), Convert.ToInt32(tmpLabel.Text.Trim()));
        }
    }
    protected void BindAction(string ContractNo, int Slno)
    {
        panUpdate.Visible = true;
        cnp.ConnectionString = ConfigurationManager.ConnectionStrings["PATENTCN"].ConnectionString;
        System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-GB");
        string sql2 = "select SLNo,AmtInFC,AmtInINR,Format(DueDate,'dd-MM-yyyy')as DueDate,Amt_InCond,Format(DueDate_Restoration,'dd-MM-yyyy') as DueDate_Restoration ,Responsibility,Format(Intimation_Date,'dd-MM-yyyy') as Intimation_Date,Format(Confirmation_Date,'dd-MM-yyyy') as Confirmation_Date,Format(POPaymentDate,'dd-MM-yyyy') as POPaymentDate,Status,Sharing_Party,Share_Received, Percentage,Description from RenewalFollowup where FileNo='" + ContractNo + "' and SLNo='" + Slno + "' and Phase='"+ddlphase.SelectedValue+"'";
        cnp.Open();
        SqlCommand cmd = new SqlCommand();
        cmd.CommandText = sql2;
        cmd.CommandType = CommandType.Text;
        cmd.Connection = cnp;
        SqlDataReader dr;
        dr = cmd.ExecuteReader();
        if (dr.Read())
        {
            if (!dr.IsDBNull(0)) txtSlno.Text = dr.GetInt32(0).ToString(); else txtSlno.Text = "";
            if (!dr.IsDBNull(1)) txtAmtInFc.Text = dr.GetString(1); else txtAmtInFc.Text = "";
            if (!dr.IsDBNull(2)) txtAmtInINR.Text = dr.GetString(2); else txtAmtInINR.Text = "";
            if (!dr.IsDBNull(3)) txtDueDate.Text = dr.GetString(3); else txtDueDate.Text = "";
            if (!dr.IsDBNull(4)) txtAmtOnCond.Text = dr.GetString(4); else txtAmtOnCond.Text = "";
            if (!dr.IsDBNull(5)) txtDueDateResto.Text = dr.GetString(5); else txtDueDateResto.Text = "";
            if (!dr.IsDBNull(6)) ddlResponsiblity.SelectedValue = dr.GetString(6); else ddlResponsiblity.SelectedIndex = 0;
            if (!dr.IsDBNull(7)) txtIntimationDt.Text = dr.GetString(7); else txtIntimationDt.Text = "";
            if (!dr.IsDBNull(8)) txtConfirmDt.Text = dr.GetString(8); else txtConfirmDt.Text = "";
            if (!dr.IsDBNull(9)) txtPaymentDate.Text = dr.GetString(9); else txtPaymentDate.Text = "";
            if (!dr.IsDBNull(10)) dropstatus.SelectedValue = dr.GetString(10); else dropstatus.SelectedIndex = 0;
            if (!dr.IsDBNull(11)) txtSharingParty.Text = dr.GetString(11); else txtSharingParty.Text = "";
            if (!dr.IsDBNull(12)) ddlShare.Text = dr.GetString(12); else ddlShare.SelectedIndex = 0;
            if (!dr.IsDBNull(13)) txtPercentage.Text = dr.GetString(13); else txtPercentage.Text = "";
            if (!dr.IsDBNull(14)) txtDescription.Text = dr.GetString(14); else txtDescription.Text = "";
            PanelList.Visible = false;

        }
        else
        {
            ClientScript.RegisterStartupScript(GetType(), "File Number Is Not Found", "<script>alert('Contract Number Is Not Found')</script>");
            dr.Close();
            cnp.Close();
            return;
        }
        dr.Close();
        cnp.Close();
    }
    protected void btnActUpdate_Click(object sender, EventArgs e)
    {

        cnp.ConnectionString = ConfigurationManager.ConnectionStrings["PATENTCN"].ConnectionString;
        cnp.Open();
        System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-GB");
        //SqlCommand cmd12 = new SqlCommand("Update RenewalFollowup set Description='" + txtDescription.Text + "',AmtInFC='" + txtAmtInFc.Text + "',AmtInINR='" + txtAmtInINR.Text + "', DueDate= case when '" + txtDueDate.Text + "'!='' then '" + txtDueDate.Text + "' else null end,Amt_InCond='" + txtAmtOnCond.Text + "',DueDate_Restoration=case when '" + txtDueDateResto.Text + "'!=null then '" + txtDueDateResto.Text + "' else null end,Responsibility='" + ddlResponsiblity.SelectedItem.ToString() + "',Sharing_Party='" + txtSharingParty.Text + "',Percentage='" + txtPercentage.Text + "',Intimation_Date= case when '" + txtIntimationDt.Text + "'!=null then '" + txtIntimationDt.Text + "' else null end,Confirmation_Date=case when '" + txtConfirmDt.Text + "'!=null then '" + txtConfirmDt.Text + "' else null end,Share_Received='"+ddlShare.SelectedItem.ToString()+ "',POPaymentDate=case when '" + txtPaymentDate.Text + "'!=null then '" + txtPaymentDate.Text+"' else null end, status='" + dropstatus.SelectedItem.ToString() + "',UserName='" + Membership.GetUser().UserName.ToString() + "' where FileNo='" + ddlIDFNo.SelectedItem.ToString() + "' and SLNo='" + txtSlno.Text + "'",cnp);
        SqlCommand cmd12 = new SqlCommand("Update RenewalFollowup set Description='" + txtDescription.Text + "',AmtInFC='" + txtAmtInFc.Text + "',AmtInINR='" + txtAmtInINR.Text + "', DueDate= case when '" + txtDueDate.Text + "'!='' then convert(smalldatetime,'" + txtDueDate.Text + "',103) else null end,Amt_InCond='" + txtAmtOnCond.Text + "',DueDate_Restoration=case when '" + txtDueDateResto.Text + "'!=null then convert(smalldatetime,'" + txtDueDateResto.Text + "',103) else null end,Responsibility='" + ddlResponsiblity.SelectedItem.ToString() + "',Sharing_Party='" + txtSharingParty.Text + "',Percentage='" + txtPercentage.Text + "',Intimation_Date= case when '" + txtIntimationDt.Text + "'!=null then convert(smalldatetime,'" + txtIntimationDt.Text + "',103) else null end,Confirmation_Date=case when '" + txtConfirmDt.Text + "'!='' then convert(smalldatetime,'" + txtConfirmDt.Text + "',103) else null end,Share_Received='" + ddlShare.SelectedItem.ToString() + "',POPaymentDate=case when '" + txtPaymentDate.Text + "'!=null then convert(smalldatetime,'" + txtPaymentDate.Text + "',103) else null end, status='" + dropstatus.SelectedItem.ToString() + "',UserName='" + Membership.GetUser().UserName.ToString() + "' where FileNo='" + ddlIDFNo.SelectedItem.ToString() + "' and SLNo='" + txtSlno.Text + "' and Phase='"+ddlphase.SelectedValue+"'", cnp);
        cmd12.ExecuteNonQuery();
        ClientScript.RegisterStartupScript(GetType(), "success", "<script>alert('This Record Successfully Updated')</script>");
        DataTable dtAction = new DataTable();
        string sql2 = "select SLNo,FileNo,Description,AmtInFC,AmtInINR,Format(DueDate,'dd-MM-yyyy')as DueDate,Amt_InCond,Format(DueDate_Restoration,'dd-MM-yyyy') as DueDate_Restoration,Responsibility,Sharing_Party,Format(Intimation_Date,'dd-MM-yyyy') as Intimation_Date,Format(Confirmation_Date,'dd-MM-yyyy') as Confirmation_Date,Share_Received,Format(POPaymentDate,'dd-MM-yyyy') as PoPaymentDate,Status from RenewalFollowup where FileNo='" + ddlIDFNo.SelectedItem.ToString() + "' and Phase='"+ddlphase.SelectedValue+"'";
        SqlDataAdapter sda = new SqlDataAdapter(sql2, cnp);
        sda.Fill(dtAction);
        lvAction.DataSource = dtAction;
        lvAction.DataBind();
        sda.Dispose();
        PanelList.Visible = true;
        cnp.Close();


    }
    protected void lvAction_ItemDeleting(object sender, ListViewDeleteEventArgs e)
    {

        try
        {
            ListViewDataItem lvd = lvAction.Items[e.ItemIndex];
            Label tmplabel = lvd.FindControl("lblSlNo") as Label;
            int SlNo = Convert.ToInt32(tmplabel.Text.Trim());
            cnp.ConnectionString = ConfigurationManager.ConnectionStrings["PATENTCN"].ConnectionString;
            string sql = "delete from RenewalFollowup where FileNo='" + ddlIDFNo.SelectedItem.ToString() + "' and Slno=" + SlNo + "";
            cnp.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = sql;
            cmd.CommandType = CommandType.Text;
            cmd.Connection = cnp;
            cmd.ExecuteNonQuery();
            ClientScript.RegisterStartupScript(GetType(), "success", "<script>alert('This Record Successfully Deleted')</script>");
            DataTable dtAction = new DataTable();
            string sql2 = "select SLNo,FileNo,AmtInFC,AmtInINR,Format(DueDate,'dd-MM-yyyy')as DueDate,Amt_InCond,Format(DueDate_Restoration,'dd-MM-yyyy') as DueDate_Restoration,Responsibility,Format(Intimation_Date,'dd-MM-yyyy') as Intimation_Date,Format(Confirmation_Date,'dd-MM-yyyy') as Confirmation_Date,Format(POPaymentDate,'dd-MM-yyyy') as PoPaymentDate,Status from RenewalFollowup where FileNo='" + ddlIDFNo.SelectedItem.ToString() + "'";
            SqlDataAdapter sda = new SqlDataAdapter(sql2, cnp);
            sda.Fill(dtAction);
            lvAction.DataSource = dtAction;
            lvAction.DataBind();
            sda.Dispose();
            PanelList.Visible = true;
            cnp.Close();

        }
        catch (Exception ex)
        {
            ClientScript.RegisterStartupScript(GetType(), "Error", "<script>alert('" + ex.Message.ToString() + "')</script>");
            cnp.Close();
            return;
        }

    }
    protected void btnClear_Click(object sender, EventArgs e)
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
    protected void btnClose_Click(object sender, EventArgs e)
    {
        panUpdate.Visible = false;
        ddlIDFNo.Text = "";
    }
}


