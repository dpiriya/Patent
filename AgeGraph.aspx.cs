using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Web.UI.DataVisualization.Charting;

public partial class AgeGraph : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        AgeChart.ImageStorageMode = ImageStorageMode.UseImageLocation;
        int fromYear = Convert.ToInt32(Request.QueryString["FromYear"]);
        int toYear = Convert.ToInt32(Request.QueryString["ToYear"]);
        //AgeChart.ImageLocation = "~/App_Data/ChartPic_#SEQ(300,3)";
        //int fromYear = 2007;
        //int toYear = 2014;
        DataTable dt = GetData(fromYear, toYear);
        string[] x = new string[dt.Rows.Count];
        int[] y = new int[dt.Rows.Count];
        int[] z = new int[dt.Rows.Count];
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            x[i] = dt.Rows[i][0].ToString();
            y[i] = Convert.ToInt32(dt.Rows[i][1]);
            z[i] = Convert.ToInt32(dt.Rows[i][2]);
        }
        AgeChart.Titles[0].Text = "Patent Age Analysis \n\n Years from " + fromYear + " to "+toYear;
        AgeChart.Series[0].Points.DataBindXY(x, y);
        AgeChart.Series[0].MarkerStyle = MarkerStyle.Circle;
        AgeChart.Series[0].ChartType = SeriesChartType.Line;
        AgeChart.Series[0].IsValueShownAsLabel = true;
        AgeChart.Series[0].LabelForeColor = System.Drawing.Color.Blue;
        AgeChart.Series[0].BorderWidth = 3;
        AgeChart.Series[0].LegendText = "Patent Filed";
        AgeChart.Series[1].Points.DataBindXY(x, z);
        AgeChart.Series[1].ChartType = SeriesChartType.Line;
        AgeChart.Series[1].IsValueShownAsLabel = true;
        AgeChart.Series[1].LabelForeColor = System.Drawing.Color.Brown;
        AgeChart.Series[1].BorderWidth = 3;
        AgeChart.Series[1].LegendText = "Patent Granted";
        AgeChart.ChartAreas[0].AxisX.MajorGrid.Enabled = false;
        AgeChart.Legends[0].Docking = Docking.Bottom;
        AgeChart.ChartAreas[0].AxisX.IsLabelAutoFit = true;
        AgeChart.ChartAreas[0].AxisX.TitleFont = new System.Drawing.Font("Arial", 10f, System.Drawing.FontStyle.Bold);
        AgeChart.ChartAreas[0].AxisY.TitleFont = new System.Drawing.Font("Arial", 10f, System.Drawing.FontStyle.Bold);
    }
    protected DataTable GetData(int fromYear, int toYear)
    {
        SqlConnection con = new SqlConnection();
        con.ConnectionString = ConfigurationManager.ConnectionStrings["PATENTCN"].ConnectionString;
        System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-GB");
        string sql = "select cast(datepart(yyyy,p.filing_dt) as varchar(4)) as [Year], count(*) as [PatentFiled]," +
        "(select count(*) from patdetails where pat_no is not null and datepart(yyyy,filing_dt)=datepart(yyyy,p.filing_dt)) as [PatentGrant] " +
        "from patdetails p where p.filing_dt is not null and datepart(yyyy,p.filing_dt)>= " + fromYear + " and datepart(yyyy,p.filing_dt)<= " + toYear + " group by datepart(yyyy,p.filing_dt)";
        SqlCommand cmd = new SqlCommand();
        cmd.CommandText = sql;
        cmd.CommandType = CommandType.Text;
        cmd.Connection = con;
        con.Open();
        SqlDataAdapter da = new SqlDataAdapter();
        da.SelectCommand = cmd;
        DataTable dt = new DataTable();
        da.Fill(dt);
        con.Close();
        return dt;
    }
}
