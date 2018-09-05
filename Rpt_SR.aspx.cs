using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class SR_Report : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection();
    protected void Page_Load(object sender, EventArgs e)
    {
        con.ConnectionString = ConfigurationManager.ConnectionStrings["PATENTCN"].ConnectionString;
        if (!this.IsPostBack)
        {
            
            if (ddlSRNo.Items.Count == 0)
            {
                SqlCommand cmd = new SqlCommand("select distinct SRNo from tbl_trx_servicerequest", con);
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    ddlSRNo.Items.Add(dr.GetString(0));
                }
                dr.Close();
                con.Close();
            }
        }
        }

    protected void imgBtnSubmit_Click(object sender, ImageClickEventArgs e)
    {
        string srno = ddlSRNo.SelectedValue;       
        SqlCommand cmd = new SqlCommand("select sr.*,Title from tbl_trx_servicerequest sr inner join PatDetails p on sr.FileNo=p.FileNo where SRNo='" + srno + "'", con);
        SqlCommand cmd1 = new SqlCommand("select FileNo,InventorType,InstID,Inventor1,FirstApplicant from PatDetails where FileNo in(select FileNo from tbl_trx_servicerequest where SRNo = '" + srno + "')  union select FileNo,InventorType,InventorID,InventorName,'IITM' from CoInventorDetails where FileNo in(select FileNo from tbl_trx_servicerequest where SRNo = '" + srno + "')", con);
        con.Open();
       
        DataTable dt=new DataTable();
        dt.Load(cmd.ExecuteReader());
        DataTable dt1 = new DataTable();
        dt1.Load(cmd1.ExecuteReader());
        ServiceRequestLt tt = new ServiceRequestLt();
        tt.ProcessRequest(dt,dt1,srno + ".docx");
        con.Close();
    }

    protected void imgBtnClear_Click(object sender, ImageClickEventArgs e)
    {

    }
}