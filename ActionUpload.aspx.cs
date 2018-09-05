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
using System.IO;
using System.Data.OleDb;
using System.Data.SqlClient;

public partial class ActionUpload : System.Web.UI.Page
{
    SqlTransaction trans;
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void btnUpload_Click(object sender, EventArgs e)
    {
        try
        {
            if (FileUpload1.HasFile)
            {
                string connectionString = "";
                string fileName = Path.GetFileName(FileUpload1.PostedFile.FileName);
                string fileExtension = Path.GetExtension(FileUpload1.PostedFile.FileName);
                if (fileExtension == ".xls" || fileExtension == ".xlsx")
                {
                    string fileLocation = Server.MapPath("~/App_Data/" + fileName);
                    if (System.IO.File.Exists(fileLocation))
                    {
                        System.IO.File.Delete(fileLocation);
                    }
                    FileUpload1.SaveAs(fileLocation);
                    //Check whether file extension is xls or xslx
                    if (fileExtension == ".xls")
                    {
                        connectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + fileLocation + ";Extended Properties=\"Excel 8.0;HDR=Yes;IMEX=2\"";
                    }
                    else if (fileExtension == ".xlsx")
                    {
                        connectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + fileLocation + ";Extended Properties=\"Excel 12.0;HDR=Yes;IMEX=2\"";
                    }
                    //Create OleDB Connection and OleDb Command
                    OleDbConnection con = new OleDbConnection(connectionString);
                    OleDbCommand cmd = new OleDbCommand();
                    cmd.CommandType = System.Data.CommandType.Text;
                    cmd.Connection = con;
                    OleDbDataAdapter dAdapter = new OleDbDataAdapter(cmd);
                    DataTable dtExcelRecords = new DataTable();
                    con.Open();
                    DataTable dtExcelSheetName = con.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                    string getExcelSheetName = dtExcelSheetName.Rows[0]["Table_Name"].ToString();
                    cmd.CommandText = "SELECT * FROM [" + getExcelSheetName + "]";
                    dAdapter.SelectCommand = cmd;
                    dAdapter.Fill(dtExcelRecords);
                    foreach (DataRow dr in dtExcelRecords.Rows)
                    {
                        if (dr[0].ToString().Trim() == "")
                        {
                            dr.Delete();
                        }
                    }
                    int i = 12;
                    while(dtExcelRecords.Columns.Count-1 > 11)
                    {
                        dtExcelRecords.Columns.RemoveAt(i);
                    }
                    dtExcelRecords.AcceptChanges();
                    GridView1.DataSource = dtExcelRecords;
                    GridView1.DataBind();
                    ViewState["dtExcel"] = dtExcelRecords;
                    con.Close();
                    btnInsert.Visible = true;
                }
                else
                {
                    ClientScript.RegisterStartupScript(GetType(), "Information", "<script>alert('Please upload Excel File')</script>");
                    return;
                }
            }
        }
        catch (Exception ex)
        {
            ClientScript.RegisterStartupScript(GetType(), "Error", "<script>alert('" + ex.Message.ToString() + "')</script>");
            return;
        }
        
    }
    protected void btnInsert_Click(object sender, EventArgs e)
    {
        SqlConnection con = new SqlConnection();
        System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-GB");
        DataTable dt = (DataTable)ViewState["dtExcel"];
        try
        {
            if (dt.Rows.Count > 0)
            {
                con.ConnectionString = ConfigurationManager.ConnectionStrings["PATENTCN"].ConnectionString;
                con.Open();
                trans = con.BeginTransaction();
                foreach (DataRow dr in dt.Rows)
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Transaction = trans;
                    cmd.CommandText = "sp_ActionTracker";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Connection = con;

                    SqlParameter pm1 = new SqlParameter();
                    pm1.ParameterName = "@EntryDt";
                    pm1.SourceColumn = "EntryDt";
                    pm1.Value = DateTime.Now.ToShortDateString();
                    pm1.DbType = DbType.String;
                    pm1.Direction = ParameterDirection.Input;

                    SqlParameter pm2 = new SqlParameter();
                    pm2.ParameterName = "@Category";
                    pm2.SourceColumn = "Category";
                    if (dr[1].ToString() == "") pm2.Value = DBNull.Value; else pm2.Value = dr[1].ToString();
                    pm2.DbType = DbType.String;
                    pm2.Direction = ParameterDirection.Input;

                    SqlParameter pm3 = new SqlParameter();
                    pm3.ParameterName = "@subCategory";
                    pm3.SourceColumn = "subCategory";
                    if (dr[2].ToString() == "") pm3.Value = DBNull.Value; else pm3.Value = dr[2].ToString();
                    pm3.DbType = DbType.String;
                    pm3.Direction = ParameterDirection.Input;

                    SqlParameter pm4 = new SqlParameter();
                    pm4.ParameterName = "@otherSubCategory";
                    pm4.SourceColumn = "otherSubCategory";
                    if (dr[3].ToString() == "") pm4.Value = DBNull.Value; else pm4.Value = dr[3].ToString();
                    pm4.DbType = DbType.String;
                    pm4.Direction = ParameterDirection.Input;

                    SqlParameter pm5 = new SqlParameter();
                    pm5.ParameterName = "@Action";
                    pm5.SourceColumn = "Action";
                    if (dr[4].ToString() == "") pm5.Value = DBNull.Value; else pm5.Value = dr[4].ToString();
                    pm5.DbType = DbType.String;
                    pm5.Direction = ParameterDirection.Input;

                    SqlParameter pm6 = new SqlParameter();
                    pm6.ParameterName = "@ActionDt";
                    pm6.SourceColumn = "ActionDt";
                    pm6.Value = DateTime.Now.Date.ToShortDateString();
                    pm6.DbType = DbType.String;
                    pm6.Direction = ParameterDirection.Input;

                    SqlParameter pm7 = new SqlParameter();
                    pm7.ParameterName = "@ActionUser";
                    pm7.SourceColumn = "ActionUser";
                    pm7.Value = Membership.GetUser().UserName.ToString();
                    pm7.DbType = DbType.String;
                    pm7.Direction = ParameterDirection.Input;

                    SqlParameter pm8 = new SqlParameter();
                    pm8.ParameterName = "@NextAction";
                    pm8.SourceColumn = "NextAction";
                    if (dr[7].ToString() == "") pm8.Value = DBNull.Value; else pm8.Value = dr[7].ToString();
                    pm8.DbType = DbType.String;
                    pm8.Direction = ParameterDirection.Input;

                    SqlParameter pm9 = new SqlParameter();
                    pm9.ParameterName = "@FollowupDt";
                    pm9.SourceColumn = "FollowupDt";
                    if (dr[8].ToString() == "")
                        pm9.Value = DBNull.Value;
                    else
                    {
                        DateTime followDt;
                        if (DateTime.TryParse(dr[8].ToString(), out followDt))
                            pm9.Value = dr[8].ToString();
                        else
                        {
                            pm9.Value = DBNull.Value;
                        }
                    }
                    pm9.DbType = DbType.String;
                    pm9.Direction = ParameterDirection.Input;

                    SqlParameter pm10 = new SqlParameter();
                    pm10.ParameterName = "@AssignUser";
                    pm10.SourceColumn = "AssignUser";
                    if (dr[9].ToString() == "") pm10.Value = DBNull.Value; else pm10.Value = dr[9].ToString();
                    pm10.DbType = DbType.String;
                    pm10.Direction = ParameterDirection.Input;

                    SqlParameter pm11 = new SqlParameter();
                    pm11.ParameterName = "@ActionComplete";
                    pm11.SourceColumn = "ActionComplete";
                    if (dr[10].ToString() == "") pm11.Value = DBNull.Value; else pm11.Value = dr[10].ToString();
                    pm11.DbType = DbType.String;
                    pm11.Direction = ParameterDirection.Input;

                    SqlParameter pm12 = new SqlParameter();
                    pm12.ParameterName = "@Comments";
                    pm12.SourceColumn = "Comments";
                    if (dr[11].ToString() == "") pm12.Value = DBNull.Value; else pm12.Value = dr[11].ToString();
                    pm12.DbType = DbType.String;
                    pm12.Direction = ParameterDirection.Input;

                    cmd.Parameters.Add(pm1);
                    cmd.Parameters.Add(pm2);
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

                    cmd.ExecuteNonQuery();

                }
                trans.Commit();
                ClientScript.RegisterStartupScript(GetType(), "Success", "<script>alert('This Record successfully added')</script>");
                GridView1.DataSource = null;
                GridView1.DataBind();
                btnInsert.Visible = false;
            }
        }
        catch (Exception ex)
        {
            ClientScript.RegisterStartupScript(GetType(), "Error", "<script>alert('" + ex.Message.ToString() + "')</script>");
            trans.Rollback();
            return;
        }
    }
}
