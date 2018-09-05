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

public partial class BusinessProducts : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.IsPostBack)
        {
            string sql;
            string companyIdNo = Request.QueryString["companyId"].ToString();
            lblTitle.Text = "Marketing Company Details : " + companyIdNo;
            con.ConnectionString = ConfigurationManager.ConnectionStrings["PATENTCN"].ConnectionString;
            string sql1 = "select CompanyID,CompanyName,Address1,Address2,City,Pincode,Phone1,Phone2,FaxNo,EmailID1,EmailID2,IndustryType from CompanyMaster where CompanyID ='" + companyIdNo + "'";
            string sql2 = "select SlNo,BusinessArea,Products from companyBusinessArea where CompanyID ='" + companyIdNo + "'order by SlNo";
            string sql3 = "select SlNo,ContactPersonName,BusinessArea,Address1,Address2,PhoneNo1,PhoneNo2,EmailID from companyContactDetails where CompanyID ='" + companyIdNo + "' order by SlNo"; sql = sql1 + ";" + sql2 + ";" + sql3;
            con.Open();
            SqlDataAdapter sda = new SqlDataAdapter(sql, con);
            DataSet ds = new DataSet();
            sda.Fill(ds);
            dvCompany.DataSource = ds.Tables[0];
            dvCompany.DataBind();
            gvBusiness.DataSource = ds.Tables[1];
            gvBusiness.DataBind();
            gvContact.DataSource = ds.Tables[2];
            gvContact.DataBind();
            con.Close();

        }
    }
}
