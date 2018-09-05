using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using CrystalDecisions;
using CrystalDecisions.CrystalReports.Engine;

public partial class Forms_FilingComfirmation : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["PATENTCN"].ConnectionString);
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.IsPostBack)
        {
            con.Open();
            string sql = "select FileNo from Patdetails order by cast(fileno as integer) desc";
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = sql;
            cmd.CommandType = CommandType.Text;
            cmd.Connection = con;
            SqlDataReader dr;
            dr = cmd.ExecuteReader();
            ddlFileNo.DataTextField = "FileNo";
            ddlFileNo.DataValueField = "FileNo";
            ddlFileNo.DataSource = dr;
            ddlFileNo.DataBind();
            ddlFileNo.Items.Insert(0, new ListItem("",""));
        }
    }
    protected void btnReport_Click(object sender, EventArgs e)
    {

        try
        {
            con.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "FilingCompliance";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            SqlParameter sp1 = new SqlParameter();
            sp1.SourceColumn = "FileNo";
            sp1.ParameterName = "@FileNo";
            sp1.Value = ddlFileNo.Text.Trim();
            sp1.SqlDbType = SqlDbType.VarChar;
            sp1.Direction = ParameterDirection.Input;
            cmd.Parameters.Add(sp1);
            SqlDataAdapter sda = new SqlDataAdapter(cmd);           
            dsForms ds = new dsForms();
            sda.Fill(ds.Tables["dtFilingConfirmation"]);
            ReportDocument rptDoc = new ReportDocument();
            rptDoc.Load(Server.MapPath("~/Report/FilingConfirmation.rpt"));
            rptDoc.SetDataSource(ds);
            rptDoc.ExportToHttpResponse(CrystalDecisions.Shared.ExportFormatType.WordForWindows, Response,false, ddlFileNo.Text.Trim() + "FilingCompliance");
            con.Close();
        }
        catch (Exception ex)
        {
            ClientScript.RegisterStartupScript(GetType(), "Error", "<script>alert('" + ex.Message.ToString() + "')</script>");
            con.Close();
            return;
        }
    }
}
