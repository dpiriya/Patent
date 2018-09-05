using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
/// <summary>
/// Summary description for ExaminationReportCls
/// </summary>
public class ExaminationReportCls
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["PATENTCN"].ConnectionString);
	public ExaminationReportCls()
	{
    }
    public DataTable SelectExamination (string sql)
    {
       SqlDataAdapter sda= new SqlDataAdapter (sql,con);
       DataTable dt= new DataTable();
       sda.Fill(dt);
       return dt;
    }
    public bool InsertExamination(SqlCommand cmd)
    {
        con.Open();
        cmd.CommandText = "ExamRptInsert";
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Connection = con;
        int cnt=cmd.ExecuteNonQuery();
        if (cnt == -1) return false; else return true;
    }
    public bool UpdateExamination(SqlCommand cmd)
    {
        con.Open();
        cmd.CommandText = "ExamRptUpdate";
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Connection = con;
        int cnt = cmd.ExecuteNonQuery();
        if (cnt == -1) return false; else return true;
    }
    public DataTable ExaminationResponse(string sql)
    {
        SqlDataAdapter sda = new SqlDataAdapter(sql, con);
        DataTable dt = new DataTable();
        sda.Fill(dt);
        return dt;
    }
    public bool InsertExamResponse(SqlCommand cmd)
    {
        con.Open();
        cmd.CommandText = "ExamRptResponseInsert";
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Connection = con;
        int cnt = cmd.ExecuteNonQuery();
        if (cnt == -1) return false; else return true;
    }
    public bool UpdateExamResponse(SqlCommand cmd)
    {
        con.Open();
        cmd.CommandText = "ExamRptResponseUpdate";
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Connection = con;
        int cnt = cmd.ExecuteNonQuery();
        if (cnt == -1) return false; else return true;
    }
}
