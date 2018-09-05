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
using System.IO;


public partial class IdfOutlook : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            con.ConnectionString = ConfigurationManager.ConnectionStrings["PATENTCN"].ConnectionString;
            SqlCommand cmd = new SqlCommand();
            string sql = "select fileno from patdetails order by cast(fileno as int) desc";
            SqlDataReader dr;
            cmd.CommandType = CommandType.Text;
            cmd.Connection = con;
            cmd.CommandText = sql;
            con.Open();
            dr = cmd.ExecuteReader();
            ddlFileNo.Items.Clear();
            ddlFileNo.DataTextField = "fileno";
            ddlFileNo.DataValueField = "fileno";
            ddlFileNo.DataSource = dr;
            ddlFileNo.DataBind();
            ddlFileNo.Items.Insert(0, "");
            dr.Close();
            con.Close();
            ViewState["IDF"] = "0";
            ViewState["Inventor"] = "0";
            ViewState["Source"] = "0";
            ViewState["Indian"] = "0";
            ViewState["Commercial"] = "0";
            ViewState["Intl"] = "0";
            ViewState["Documents"] = "0";
        }
    }
    protected void ddlFileNo_SelectedIndexChanged(object sender, EventArgs e)
    {
        dvIDF.DataSource = null;
        dvIDF.DataBind();
        lvInventor.DataSource = "";
        lvInventor.DataBind();
        LvInventionSource.DataSource = "";
        LvInventionSource.DataBind();
        dvIndian.DataSource = null;
        dvIndian.DataBind();
        dvCommercial.DataSource = null;
        dvCommercial.DataBind();
        lvIntl.DataSource = "";
        lvIntl.DataBind();
        dvIntl.DataSource = null;
        dvIntl.DataBind();
        GVSD.DataSource = null;
        GVSD.DataBind();
        imgBtnClear.Visible = false;
        ViewState["IDF"] = "0";
        ViewState["Inventor"] = "0";
        ViewState["Source"] = "0";
        ViewState["Indian"] = "0";
        ViewState["Commercial"] = "0";
        ViewState["Intl"] = "0";
        ViewState["Documents"] = "0";
    }
    protected void lbtnIDF_Click(object sender, EventArgs e)
    {
        if (ViewState["IDF"].ToString() == "0")
        {
            con.ConnectionString = ConfigurationManager.ConnectionStrings["PATENTCN"].ConnectionString;
            System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-GB");
            string sql = "select title,type,InitialFiling,firstApplicant,secondApplicant,request_dt,Specification from patdetails where fileno like '" + ddlFileNo.Text.Trim() + "'";
            SqlDataAdapter sda = new SqlDataAdapter(sql, con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            dvIDF.DataSource = dt;
            dvIDF.DataBind();
            con.Close();
            imgBtnClear.Visible = true;
            ViewState["IDF"] = "1";
        }
        else
        {
            dvIDF.DataSource = null;
            dvIDF.DataBind();
            ViewState["IDF"] = "0";
        }
    }
    protected void imgBtnClear_Click(object sender, ImageClickEventArgs e)
    {
        ddlFileNo.Text = "";
        dvIDF.DataSource = null;
        dvIDF.DataBind();
        lvInventor.DataSource = "";
        lvInventor.DataBind();
        LvInventionSource.DataSource = "";
        LvInventionSource.DataBind();
        dvIndian.DataSource = null;
        dvIndian.DataBind();
        dvCommercial.DataSource = null;
        dvCommercial.DataBind();
        lvIntl.DataSource = "";
        lvIntl.DataBind();
        dvIntl.DataSource = null;
        dvIntl.DataBind();
        GVSD.DataSource = null;
        GVSD.DataBind();
        imgBtnClear.Visible = false;
        ViewState["IDF"] = "0";
        ViewState["Inventor"] = "0";
        ViewState["Source"] = "0";
        ViewState["Indian"] = "0";
        ViewState["Commercial"] = "0";
        ViewState["Intl"] = "0";
        ViewState["Documents"] = "0";
    }
    protected void lbtnIndian_Click(object sender, EventArgs e)
    {
        if (ViewState["Indian"].ToString() == "0")
        {
            con.ConnectionString = ConfigurationManager.ConnectionStrings["PATENTCN"].ConnectionString;
            System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-GB");
            string sql = "select Attorney,Applcn_no,Filing_dt,Examination,Exam_dt,Publication,Pub_dt,Status,Sub_status," +
            "Pat_no,Pat_dt from patdetails where fileno like '" + ddlFileNo.Text.Trim() + "'";
            SqlDataAdapter sda = new SqlDataAdapter(sql, con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            dvIndian.DataSource = dt;
            dvIndian.DataBind();
            con.Close();
            imgBtnClear.Visible = true;
            ViewState["Indian"] = "1";
        }
        else
        {
            dvIndian.DataSource = null;
            dvIndian.DataBind();
            ViewState["Indian"] = "0";
        }
    }
   
    protected void lbtnIntl_Click(object sender, EventArgs e)
    {
        if (ViewState["Intl"].ToString() == "0")
        {
            con.ConnectionString = ConfigurationManager.ConnectionStrings["PATENTCN"].ConnectionString;
            System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-GB");
            string sql = "select subFileNo,RequestDt,Country,Partner,PartnerNo,Type,Attorney,ApplicationNo,FilingDt," +
            "PublicationNo,PublicationDt,Status,SubStatus,PatentNo,PatentDt,Remark from international where fileno like '" + ddlFileNo.Text.Trim() + "' order by subfileno";
            SqlDataAdapter sda = new SqlDataAdapter(sql, con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            lvIntl.DataSource = dt;
            lvIntl.DataBind();
            con.Close();
            imgBtnClear.Visible = true;
            ViewState["Intl"] = "1";
        }
        else
        {
            lvIntl.DataSource = "";
            lvIntl.DataBind();
            dvIntl.DataSource = null;
            dvIntl.DataBind();
            ViewState["Intl"] = "0";
        }
    }
    protected void lbtnCommercial_Click1(object sender, EventArgs e)
    {
        if (ViewState["Commercial"].ToString() == "0")
        {
            con.ConnectionString = ConfigurationManager.ConnectionStrings["PATENTCN"].ConnectionString;
            System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-GB");
            string sql = "select Commercial,InventionNo,Validity_from_dt,Validity_to_dt,Industry1,Industry2,Industry3,IPC_Code," +
            "Abstract,DevelopmentStatus,Commercialized,PatentLicense,TechTransNo,Remarks from patdetails where fileno like '" + ddlFileNo.Text.Trim() + "'";
            SqlDataAdapter sda = new SqlDataAdapter(sql, con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            dvCommercial.DataSource = dt;
            dvCommercial.DataBind();
            con.Close();
            imgBtnClear.Visible = true;
            ViewState["Commercial"] = "1";
        }
        else
        {
            dvCommercial.DataSource = null;
            dvCommercial.DataBind();
            ViewState["Commercial"] = "0";
        }
    }
    protected void ibtnPrinter_Click(object sender, ImageClickEventArgs e)
    {
        if (ddlFileNo.SelectedIndex != -1)
        {
            ClientScript.RegisterStartupScript(GetType(), "New window", "<script language='javascript'>child=window.open('IdfPrint.aspx?FileNo=" + ddlFileNo.Text.Trim() + "','child','menubar=0,toolbar=0,scrollbars=0,titlebar=0,status=no,top=200,left=400,width=275,height=250')</script>");
        }
    }
   

    protected void lvIntl_SelectedIndexChanging(object sender, ListViewSelectEventArgs e)
    {
        
        ListViewItem item = lvIntl.Items[e.NewSelectedIndex];
        Label subNo = (Label)item.FindControl("lblSubFileNo");
        Label subNo1 = (Label)item.FindControl("lblSubFileNo1");
        string SubFileNo="";
        if (subNo != null)
        {
            SubFileNo = subNo.Text.Trim();
        }
        else if (subNo1 != null)
        {
            SubFileNo = subNo1.Text.Trim();
        }
        con.ConnectionString = ConfigurationManager.ConnectionStrings["PATENTCN"].ConnectionString;
        System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-GB");
        string sql = "select subFileNo,RequestDt,Country,Partner,PartnerNo,Type,Attorney,ApplicationNo,FilingDt," +
        "PublicationNo,PublicationDt,Status,SubStatus,PatentNo,PatentDt,Remark from international where fileno like '" + ddlFileNo.Text.Trim() + "' and subFileNo like '" + SubFileNo +"' order by subfileno";
        SqlDataAdapter sda = new SqlDataAdapter(sql, con);
        DataTable dt = new DataTable();
        sda.Fill(dt);
        dvIntl.DataSource = dt;
        dvIntl.DataBind();
        con.Close();
        
    }
    protected void lvIntl_ItemDataBound(object sender, ListViewItemEventArgs e)
    {
        LinkButton lb = e.Item.FindControl("lbtnEdit") as LinkButton;
        LinkButton lb1 = e.Item.FindControl("lbtnEdit1") as LinkButton;
        if (lb != null)
        {
            ScriptManager.GetCurrent(Page).RegisterPostBackControl(lb);
        }
        if (lb1 != null)
        {
            ScriptManager.GetCurrent(Page).RegisterPostBackControl(lb1);
        }
    
    }
    protected void lbtnInventor_Click(object sender, EventArgs e)
    {
        if (ViewState["Inventor"].ToString() == "0")
        {
            con.ConnectionString = ConfigurationManager.ConnectionStrings["PATENTCN"].ConnectionString;
            string sql = "select SlNo+1 as SlNo,InventorName,InventorType,InventorID,DeptOrOrganisation as Dept from coinventordetails where fileno like '" + ddlFileNo.Text.Trim() + "'" +
            "union select 1 as SlNo,Inventor1 as InventorName,InventorType,InstID as InventorID,Department as Dept from patdetails where fileno like '" + ddlFileNo.Text.Trim() + "'";
            SqlDataAdapter sda = new SqlDataAdapter(sql, con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            lvInventor.DataSource = dt;
            lvInventor.DataBind();
            con.Close();
            imgBtnClear.Visible = true;
            ViewState["Inventor"] = "1";
        }
        else
        {
            lvInventor.DataSource = "";
            lvInventor.DataBind();
            ViewState["Inventor"] = "0";
        }
    }
    protected void lbtnDocument_Click(object sender, EventArgs e)
    {
        if (ViewState["Documents"].ToString() == "0")
        {
            con.ConnectionString = ConfigurationManager.ConnectionStrings["PATENTCN"].ConnectionString;
            string sql = "select FileDescription,FileName from patentfiledetails where fileno like '" + ddlFileNo.Text.Trim() + "' order by EntryDt desc";           
            SqlDataAdapter sda = new SqlDataAdapter(sql, con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            GVSD.DataSource = dt;
            GVSD.DataBind();
            con.Close();
            imgBtnClear.Visible = true;
            ViewState["Documents"] = "1";
        }
        else
        {
            GVSD.DataSource = "";
            GVSD.DataBind();
            ViewState["Documents"] = "0";
        }
    }
    protected void GVSD_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        LinkButton lbFileName = GVSD.Rows[e.NewSelectedIndex].FindControl("lbtnEdit") as LinkButton;

        string strPath = "";
        string strFileName = "";
        strPath = @"F:\PatentDocument\" + ddlFileNo.SelectedItem.Text.Trim() + @"\";
        strFileName = lbFileName.Text.Trim();
        string extension = Path.GetExtension(strPath + strFileName);
        string mimeType = "image/jpeg";
        if (extension == ".jpg")
            mimeType = "image/jpeg";
        else if (extension == ".xls")
            mimeType = "application/vnd.xls";
        else if (extension == ".xlsx")
            mimeType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
        else if (extension == ".doc")
            mimeType = "application/msword";
        else if (extension == ".docx")
            mimeType = "application/vnd.openxmlformats-officedocument.wordprocessingml.document";
        else if (extension == ".ppt")
            mimeType = "application/vnd.ms-powerpoint";
        else if (extension == ".pptx")
            mimeType = "application/vnd.openxmlformats-officedocument.presentationml.presentation";
        else if (extension == ".pdf")
            mimeType = "application/pdf";
        else if (extension == ".txt")
            mimeType = "plain/text";
        FileStream fs = new FileStream(strPath + strFileName, FileMode.Open);
        byte[] bytBytes = new byte[fs.Length];
        fs.Read(bytBytes, 0, (int)fs.Length);
        fs.Close();
        strFileName = strFileName.Replace(" ", "-");
        Response.AppendHeader("Content-Disposition", "attachment;filename=" + strFileName);
        Response.ContentType = mimeType;
        Response.BinaryWrite(bytBytes);
        Response.End();
    }
    protected void GVSD_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        LinkButton lb = e.Row.FindControl("lbtnEdit") as LinkButton;
        if (lb != null)
        {
            ScriptManager.GetCurrent(Page).RegisterPostBackControl(lb);
        }        
    }
    protected void lbtnSource_Click(object sender, EventArgs e)
    {
        if (ViewState["Source"].ToString() == "0")
        {
            con.ConnectionString = ConfigurationManager.ConnectionStrings["PATENTCN"].ConnectionString;
            string sql = "select SlNo,ProjectNumber,ProjectType,FundingAgency,ProjectTitle from InventionSource where fileno like '" + ddlFileNo.Text.Trim() + "'";
            SqlDataAdapter sda = new SqlDataAdapter(sql, con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            LvInventionSource.DataSource = dt;
            LvInventionSource.DataBind();
            con.Close();
            imgBtnClear.Visible = true;
            ViewState["Source"] = "1";
        }
        else
        {
            LvInventionSource.DataSource = "";
            LvInventionSource.DataBind();
            ViewState["Source"] = "0";
        }
    }
}
