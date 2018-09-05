using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using Microsoft.Office.Interop;
using System.IO;

public partial class Default4 : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection();
    string AName = "";
    string AAddress1 = "";
    string AAddress2 = "";
    string AAddress3 = "";
    string ACity = "";
    string APincode = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        con.ConnectionString = ConfigurationManager.ConnectionStrings["PATENTCN"].ConnectionString;
        con.Open();
        SqlCommand cmd = new SqlCommand("Select ID,Details from ServiceFormDetails", con);
        SqlDataAdapter da = new SqlDataAdapter();
        da.SelectCommand = cmd;
        DataTable dt= new DataTable();
        da.Fill(dt);
        Grid1.DataSource = dt;
        Grid1.DataBind();
        con.Close();
    }
    protected void butn1_Click(object sender, EventArgs e)
  {
        System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-GB");
    
        using (con = new SqlConnection())
        {
           // string sql = "select Type,Title, Applcn_no as ApplicationNo,Filing_dt as FilingDt,Specification from patdetails where fileno = '" + IDFNo + "'";
            string sql = "select AttorneyName,Address1,address2,Address3,city,PinCode from Attorney where AttorneyName= 'BananaIP'";
            SqlCommand cmd = new SqlCommand();
            con.ConnectionString = ConfigurationManager.ConnectionStrings["PATENTCN"].ConnectionString;
            con.Open();
            cmd.CommandText = sql;
            cmd.CommandType = CommandType.Text;
            cmd.Connection = con;
            SqlDataReader dr;
            dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                AName = dr.GetString(0);
                AAddress1 = dr.GetString(1);
                if (!dr.IsDBNull(2)) AAddress2 = dr.GetString(2); else AAddress2 = "";
                if (!dr.IsDBNull(3)) AAddress3 = dr.GetDateTime(3).ToShortDateString(); else AAddress3 = "";
                if (!dr.IsDBNull(4)) ACity = dr.GetString(4); else ACity = "";
                if (!dr.IsDBNull(4)) APincode = dr.GetString(4); else APincode = "";
            }
        }
        DataTable dt = new DataTable();
        //dt.Columns.AddRange(new DataColumn[8] { new DataColumn("PType"), new DataColumn("Title"), new DataColumn("ApplicationNo"), new DataColumn("FilingDt"), new DataColumn("Specification"), new DataColumn("ContactName"), new DataColumn("Address1"), new DataColumn("Address2") });
        //foreach (GridViewRow row in gvGenerator.Rows)
        //{
        //    if (row.RowType == DataControlRowType.DataRow)
        //    {
        //        CheckBox chkRow = (row.Cells[0].FindControl("chkSelected") as CheckBox);
        //        if (chkRow.Checked)
        //        {
        //            string ContactName = row.Cells[4].Text;
        //            string Address1 = row.Cells[5].Text;
        //            string Address2 = row.Cells[6].Text;
        //            dt.Rows.Add(PType, Title, ApplicationNo, FilingDt, Specification, ContactName, Address1, Address2);
        //        }
        //    }
        //}
        string docFileName = "TTIP" + 11 + ".doc";
        
            
            Class1 tt = new Class1();
            tt.ProcessRequest(dt, docFileName);
       
        
    }
}
