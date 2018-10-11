using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class duedliligenceModify : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection();
    protected void Page_Load(object sender, EventArgs e)
    {
        con.ConnectionString = ConfigurationManager.ConnectionStrings["PATENTCN"].ConnectionString;
        if (!IsPostBack)
        {
            con.Open();
            SqlCommand cmdidf = new SqlCommand("select distinct FileNo from patdetails", con);
            SqlDataReader dr = cmdidf.ExecuteReader();
            while (dr.Read())
            {
                ddlidf.Items.Add(new ListItem(dr.GetString(0)));
            }
            dr.Close();
            con.Close();
        }
    }
   
    protected void btnFind_Click(object sender, EventArgs e)
    {
        panelList.Visible = true;       
        DataTable dtAction = new DataTable();
        string sql2 = "select Sno,FileNo,ReportType,Mode,Allocation,ReportDt from tbl_trx_duediligence where FileNo='" + ddlidf.SelectedItem.ToString() + "'";
        SqlDataAdapter sda = new SqlDataAdapter(sql2, con);
        sda.Fill(dtAction);
        lvSearch.DataSource = dtAction;
        lvSearch.DataBind();
        sda.Dispose();
    }
    protected void lvSearch_ItemCommand(object sender, ListViewCommandEventArgs e)
    {
        Label lsno = (Label)e.Item.FindControl("lblSNo");
        if(e.CommandName=="Modify")
        {
            panUpdate.Visible = true;
            System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-GB");
            SqlCommand cmdedit = new SqlCommand("select * from tbl_trx_duediligence where FileNo='" + ddlidf.SelectedValue + "'", con);
            con.Open();
            SqlDataReader dr = cmdedit.ExecuteReader();
            if(dr.Read())
            {
                //if(!dr.IsDBNull(0)) txtSno.Text=
            }
        }
    }
    protected void lvSearch_ItemDataBound(object sender, ListViewItemEventArgs e)
    {

    }
    protected void lvSearch_ItemDeleting(object sender, ListViewDeleteEventArgs e)
    {

    }
}