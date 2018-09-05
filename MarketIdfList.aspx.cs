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

public partial class MarketIdfList : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.IsPostBack)
        {
            string sql;
            string ProjNo = Request.QueryString["projNo"].ToString();
            lblTitle.Text = "Marketing Project Number : " + ProjNo;
            con.ConnectionString = ConfigurationManager.ConnectionStrings["PATENTCN"].ConnectionString;
            string sql1 = "select MktgProjectNo,MktgTitle,MktgCompany,MktgGroup,ProjectValue,ValueRealization,Status,Remarks from marketingProject where MktgProjectNo ='" + ProjNo +"'";
            string sql2="select fileno,title,inventor1,applcn_no,status from patdetails where fileno in (select fileno from marketingIDF where mktgProjectNo ='" + ProjNo +"')";
            string sql3 = "select ActivityDt,Channel,ActivityType,Remarks from marketingActivity where mktgProjectNo ='" + ProjNo + "' order by slno desc"; sql = sql1 + ";" + sql2 + ";" + sql3;
            con.Open();
            SqlDataAdapter sda = new SqlDataAdapter(sql, con);
            DataSet ds = new DataSet();
            sda.Fill(ds);
            dvMarket.DataSource = ds.Tables[0];
            dvMarket.DataBind();
            gvIDF.DataSource = ds.Tables[1];
            gvIDF.DataBind();
            gvActivity.DataSource = ds.Tables[2];
            gvActivity.DataBind();
            con.Close();
            
        }
    }
}
