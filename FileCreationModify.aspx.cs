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
using System.Web.Services;
using System.Web.Script.Services;
using System.Data.SqlClient;
using System.Collections.Generic;

public partial class FileCreationModify : System.Web.UI.Page
{
    SqlConnection cn = new SqlConnection();
    SqlConnection cnp = new SqlConnection();
    SqlConnection con = new SqlConnection();
    string deptFlag;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (User.Identity.IsAuthenticated)
        {
            if (!this.IsPostBack)
            {
                this.Title = "File Modification";

                cnp.ConnectionString = ConfigurationManager.ConnectionStrings["PATENTCN"].ConnectionString;
                SqlCommand cmd2 = new SqlCommand();
                SqlDataReader dr;
                string sql2 = "select iprname from ipr_category order by iprname";
                cmd2.CommandType = CommandType.Text;
                cmd2.Connection = cnp;
                cmd2.CommandText = sql2;
                cnp.Open();
                dr = cmd2.ExecuteReader();
                ddlIPR.Items.Clear();
                ddlIPR.Items.Add("");
                while (dr.Read())
                {
                    ddlIPR.Items.Add(new ListItem(dr.GetString(0).Trim(),dr.GetString(0).Trim()));
                }
                dr.Close();
                ddlIFiling.Items.Clear();
                ddlIFiling.Items.Add("");
                ddlIFiling.Items.Add(new ListItem("IIT Madras", "IIT Madras"));
                ddlIFiling.Items.Add(new ListItem("ICSR", "ICSR"));
                ddlIFiling.Items.Add(new ListItem("PCF", "PCF"));
                ddlIFiling.Items.Add(new ListItem("PARTNER", "PARTNER"));

                SqlCommand cmd4 = new SqlCommand();
                string sql4 = "SELECT ITEMLIST FROM LISTITEMMASTER WHERE CATEGORY LIKE 'INVENTORTYPE' ORDER BY SLNO";
                cmd4.CommandText = sql4;
                cmd4.Connection = cnp;
                cmd4.CommandType = CommandType.Text;
                dr = cmd4.ExecuteReader();
                ddlType1.DataTextField = "ItemList";
                ddlType1.DataValueField = "ItemList";
                ddlType1.DataSource = dr;
                ddlType1.DataBind();
                ddlType1.Items.Insert(0, "");
                cnp.Close();
                ddlType1.SelectedIndex = 0;
                Inventor ds = new Inventor();
                lvInventor.DataSource = ds.Tables["CoInventors"];
                lvInventor.DataBind();
                ViewState["CoInventor"] = ds.Tables["CoInventors"];
                ddlSource.Items.Add("No");
                ddlSource.Items.Add("Yes");
                ddlSource.SelectedIndex = 0;                
                dsFileCreation dsSource = new dsFileCreation();
                lvProjectList.DataSource = dsSource.Tables["dtInventionSource"];
                lvProjectList.DataBind();
                ViewState["InventionSource"] = dsSource.Tables["dtInventionSource"];
                deptFlag = "";
                imgBtnInsert.Visible = (!User.IsInRole("View"));
            }
        }
        else
        {
            Response.Redirect("Login.aspx");
        }
    }
    
    [WebMethod]

    public static List<string> GetAutoCompleteData(string fileno)
    {
        List<string> result = new List<string>();
        SqlConnection cnp = new SqlConnection();
        cnp.ConnectionString = ConfigurationManager.ConnectionStrings["PATENTCN"].ConnectionString;
        SqlCommand cmd = new SqlCommand();
        string sql = "select fileno from patdetails where fileno like + @SearchText +'%' order by fileno";
        SqlDataReader dr;
        cmd.CommandType = CommandType.Text;
        cmd.Parameters.AddWithValue("@SearchText", fileno);
        cmd.Connection = cnp;
        cmd.CommandText = sql;
        cnp.Open();
        dr = cmd.ExecuteReader();
        while (dr.Read())
        {
            result.Add(dr["fileno"].ToString());
        }
        return result;
    }
    protected void imgBtnClear_Click(object sender, ImageClickEventArgs e)
    {
        Clear();
    }
    protected void Clear()
    {
        txtTitle.Text = "";
        txtFApp.Text = "";
        txtSApp.Text = "";
        ddlIPR.Text = "";
        ddlIFiling.Text = "";
        ddlType1.Text = "";
        txtCoorID.Text = "";
        txtCoorID.Enabled = false;
        txtCoor1.Text = "";
        txtCoor1.Visible = false;
        ddlCoor1.Visible = true;
        ddlCoor1.Items.Clear();
        tdFirstInvent.InnerText = "First Inventor Dept.";
        txtDept1.Text = "";
        txtDept1.Enabled = true;
        ddlDept1.Items.Clear();
        txtRequest.Text = "";
        ddlSource.Enabled = true;
        ddlSource.SelectedIndex = 0;
        txtComments.Text = "";
        txtWriteUp.Text = "";
        DataTable ClearDt = (DataTable)ViewState["CoInventor"];
        ClearDt.Clear();
        lvInventor.DataSource = ClearDt;
        lvInventor.DataBind();
        ClearDt = (DataTable)ViewState["InventionSource"];
        ClearDt.Clear();
        lvProjectList.DataSource = ClearDt;
        lvProjectList.DataBind();
        txtFileNo.Text = "";
        txtFileNo.Enabled = true;

    }
    protected void btnFind_Click(object sender, EventArgs e)
    {
        lvInventor.EditIndex = -1;
        lvProjectList.EditIndex = -1;
        System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-GB");
        cn.ConnectionString = ConfigurationManager.ConnectionStrings["PATENTCN"].ConnectionString;
        SqlCommand cmd = new SqlCommand();
        string sql = "select fileno,title,FirstApplicant,SecondApplicant,type,InitialFiling,InventorType,Inventor1,deptcode,department," +
        "instID,request_dt,InventionSourceFromProject,Comments,Specification,Post_dated,Abstract_TT from patdetails where fileno ='" + txtFileNo.Text.Trim() + "'";
        cmd.Connection = cn;
        cmd.CommandType = CommandType.Text;
        cmd.CommandText = sql;
        SqlDataReader dr;
        cn.Open();
        dr = cmd.ExecuteReader();
        DataTable dt = new DataTable();
        dt.Load(dr);
        if (dt.Rows.Count>0)
        {
            DataRow dRow = dt.Rows[0];
            txtTitle.Text = dRow["Title"].ToString();
            txtFApp.Text = dRow["FirstApplicant"].ToString();
            txtSApp.Text = dRow["SecondApplicant"].ToString();
            ddlIPR.Text = dRow["type"].ToString();
            ddlIFiling.Text = dRow["InitialFiling"].ToString();
            ddlType1.SelectedIndex=ddlType1.Items.IndexOf(ddlType1.Items.FindByValue(dRow["InventorType"].ToString()));
            ddlType1_SelectedIndexChanged(this, EventArgs.Empty);
            hfType.Value = dRow["InventorType"].ToString();
            if (ddlType1.SelectedIndex == 1 || ddlType1.SelectedIndex == 2 || ddlType1.SelectedIndex == 3 || ddlType1.SelectedIndex == 4)
            {
                ddlDept1.SelectedIndex = ddlDept1.Items.IndexOf(ddlDept1.Items.FindByText(dRow["deptcode"].ToString()));
                ddlDept1_SelectedIndexChanged(this, EventArgs.Empty);
                txtDept1.Text = dRow["department"].ToString();
            }
            else 
            {
                ddlDept1.Text = "";
                txtDept1.Text = dRow["department"].ToString();
            }
            if (ddlType1.SelectedIndex != 1 || ddlType1.SelectedIndex == -1)
            {
                txtCoor1.Text = dRow["Inventor1"].ToString();
                txtCoorID.Text = dRow["InstID"].ToString();
            }
            else
            {
                int tmpVal;
                string Coor_Code;
                if (int.TryParse(dRow["InstID"].ToString(), out tmpVal) == true)
                {
                    
                    con.ConnectionString = ConfigurationManager.ConnectionStrings["ICSR2KSCN"].ConnectionString;                    
                    //string sql = "SELECT CR_CODE,INSTID FROM COORCOD WHERE CR_CODE LIKE '" + coorCode + "'";
                    SqlCommand cmd5 = new SqlCommand();
                    string sql5 = "SELECT CR_CODE FROM Foxoffice..COORCOD WHERE INSTID LIKE '%" + dRow["InstID"].ToString() + "%'";
                    cmd5.Connection = con;
                    cmd5.CommandType = CommandType.Text;
                    cmd5.CommandText = sql5;
                    con.Open();
                    Coor_Code= Convert.ToString(cmd5.ExecuteScalar());
                    con.Close();
                }
                else
                {
                    Coor_Code = dRow["InstID"].ToString();
                }
                ddlCoor1.SelectedIndex = ddlCoor1.Items.IndexOf(ddlCoor1.Items.FindByValue(Coor_Code));
                ddlCoor1_SelectedIndexChanged(this, EventArgs.Empty);
                txtCoorID.Text = dRow["InstID"].ToString();                
            }
            if ( dRow["request_dt"].GetType() != typeof(DBNull)) txtRequest.Text = Convert.ToDateTime(dRow["request_dt"]).ToShortDateString(); else txtRequest.Text = "";
            if (dRow["Specification"].GetType() != typeof(DBNull)) txtWriteUp.Text = Convert.ToString(dRow["Specification"]); else txtWriteUp.Text = "";
            if (dRow["Comments"].GetType() != typeof(DBNull)) txtComments.Text = Convert.ToString(dRow["Comments"]); else txtComments.Text = "";
            if (dRow["Abstract_TT"].GetType() != typeof(DBNull)) txtAbstract.Text = Convert.ToString(dRow["Abstract_TT"]); else txtAbstract.Text = "";
            if (dRow["Post_Dated"].GetType() != typeof(DBNull)) txtPost.Text = Convert.ToDateTime(dRow["Post_Dated"]).ToShortDateString(); else txtPost.Text = "";
        }
        DataTable dtCoInventor = (DataTable)ViewState["CoInventor"];
        string sql3 = "select SlNo,inventorType,DeptCode,DeptOrOrganisation as DeptOrgName,InventorID,InventorName from CoInventorDetails where fileno like '" + txtFileNo.Text.ToString() + "' order by slno";
        SqlDataAdapter sda = new SqlDataAdapter(sql3, cn);
        sda.Fill(dtCoInventor);
        dtCoInventor.AcceptChanges();
        ViewState["CoInventor"] = dtCoInventor;
        lvInventor.DataSource = dtCoInventor;
        lvInventor.DataBind();

        DataTable dtSource = (DataTable)ViewState["InventionSource"];
        dtSource.Clear();
        string sql4 = "select SlNo,ProjectType,FinancialYear,ProjectNumber,FundingAgency,ProjectTitle from InventionSource where fileno like '" + txtFileNo.Text.ToString() + "' order by slno";
        SqlDataAdapter sds = new SqlDataAdapter(sql4, cn);
        sds.Fill(dtSource);
        dtSource.AcceptChanges();
        ViewState["InventionSource"] = dtSource;
        lvProjectList.DataSource = dtSource;
        lvProjectList.DataBind();
        cn.Close();
        if (lvProjectList.Items.Count > 0)
        {
            ddlSource.SelectedIndex = 1;
            ddlSource_SelectedIndexChanged(ddlSource, EventArgs.Empty);
            ddlSource.Enabled = false;
        }
        else
        {
            ddlSource.SelectedIndex = 0;
            ddlSource_SelectedIndexChanged(ddlSource, EventArgs.Empty);
        }
        txtFileNo.Enabled = false;        
    }
    protected void imgBtnInsert_Click(object sender, ImageClickEventArgs e)
    {
        if (string.IsNullOrEmpty(txtFileNo.Text.Trim()))
        {
            ClientScript.RegisterStartupScript(GetType(), "Information", "<script>alert('File No Can not be Empty')</script>");
            return;
        }
        DataTable dtSource = (DataTable)ViewState["InventionSource"];
        if (ddlSource.SelectedIndex == 1)
        {
            if (dtSource.Rows.Count <= 0)
            {
                ClientScript.RegisterStartupScript(GetType(), "Research Projects", "<script>alert('Enter Research Project Details')</script>");
                return;
            }
        }
        cnp.ConnectionString = ConfigurationManager.ConnectionStrings["PATENTCN"].ConnectionString;
        System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-GB");
        cnp.Open();
        SqlTransaction Trans;
        Trans = cnp.BeginTransaction();
        try
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Transaction = Trans;
            cmd.CommandText = "ModifyFileCreation";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = cnp;
            SqlParameter pm1 = new SqlParameter();
            pm1.ParameterName = "@fileno";
            pm1.SourceColumn = "fileno";
            pm1.Value = txtFileNo.Text.Trim();
            pm1.DbType = DbType.String;
            pm1.Direction = ParameterDirection.Input;

            SqlParameter pm2 = new SqlParameter();
            pm2.ParameterName = "@title";
            pm2.SourceColumn = "title";
            pm2.Value = txtTitle.Text.Trim();
            pm2.DbType = DbType.String;
            pm2.Direction = ParameterDirection.Input;

            SqlParameter pm3 = new SqlParameter();
            pm3.ParameterName = "@iprCate";
            pm3.SourceColumn = "type";
            if (string.IsNullOrEmpty(ddlIPR.SelectedValue)) pm3.Value = DBNull.Value; else pm3.Value = ddlIPR.SelectedValue;
            pm3.DbType = DbType.String;
            pm3.Direction = ParameterDirection.Input;

            SqlParameter pm4 = new SqlParameter();
            pm4.ParameterName = "@iFile";
            pm4.SourceColumn = "InitialFiling";
            if (ddlIFiling.SelectedValue == "") pm4.Value = DBNull.Value; else pm4.Value = ddlIFiling.SelectedValue;
            pm4.DbType = DbType.String;
            pm4.Direction = ParameterDirection.Input;

            SqlParameter pm5 = new SqlParameter();
            pm5.ParameterName = "@FirstApplicant";
            pm5.SourceColumn = "FirstApplicant";
            if (txtFApp.Text.Trim() == "") pm5.Value = DBNull.Value; else pm5.Value = txtFApp.Text.Trim();
            pm5.DbType = DbType.String;
            pm5.Direction = ParameterDirection.Input;

            SqlParameter pm6 = new SqlParameter();
            pm6.ParameterName = "@SecondApplicant";
            pm6.SourceColumn = "SecondApplicant";
            if (txtSApp.Text.Trim() == "") pm6.Value = DBNull.Value; else pm6.Value = txtSApp.Text.Trim();
            pm6.DbType = DbType.String;
            pm6.Direction = ParameterDirection.Input;

            SqlParameter pm7 = new SqlParameter();
            pm7.ParameterName = "@InventorType";
            pm7.SourceColumn = "InventorType";
            if (ddlType1.SelectedItem.Text.Trim() == "") pm7.Value = DBNull.Value; else pm7.Value = ddlType1.SelectedItem.Text.Trim();
            pm7.DbType = DbType.String;
            pm7.Direction = ParameterDirection.Input;

            SqlParameter pm8 = new SqlParameter();
            pm8.ParameterName = "@deptCode";
            pm8.SourceColumn = "deptCode";
            if (ddlType1.SelectedItem.Text != "Non IITM" && ddlType1.SelectedItem.Text !="")
                if (ddlDept1.SelectedItem.Text.Trim() == "") pm8.Value = DBNull.Value; else pm8.Value = ddlDept1.SelectedItem.Text.Trim();
            else
                pm8.Value = DBNull.Value;
            pm8.DbType = DbType.String;
            pm8.Direction = ParameterDirection.Input;

            SqlParameter pm9 = new SqlParameter();
            pm9.ParameterName = "@department";
            pm9.SourceColumn = "department";
            if (txtDept1.Text.Trim() == "") pm9.Value = DBNull.Value; else pm9.Value = txtDept1.Text.Trim();
            pm9.DbType = DbType.String;
            pm9.Direction = ParameterDirection.Input;

            SqlParameter pm10 = new SqlParameter();
            pm10.ParameterName = "@Inventor1";
            pm10.SourceColumn = "Inventor1";
            if (ddlType1.SelectedItem.Text == "Faculty")
                if (ddlCoor1.SelectedItem.Text.Trim() == "") 
                    pm10.Value = DBNull.Value; 
                else
                {
                    if(ddlCoor1.SelectedItem.Text.IndexOf("~")!=-1)
                        pm10.Value = ddlCoor1.SelectedItem.Text.Substring(ddlCoor1.SelectedItem.Text.IndexOf("~")+1).Trim();
                    else
                        pm10.Value = ddlCoor1.SelectedItem.Text.Trim();
                }
            else
                if (txtCoor1.Text.Trim() == "") pm10.Value = DBNull.Value; else pm10.Value = txtCoor1.Text.Trim();
            pm10.DbType = DbType.String;
            pm10.Direction = ParameterDirection.Input;

            SqlParameter pm11 = new SqlParameter();
            pm11.ParameterName = "@InventorID";
            pm11.SourceColumn = "InstID";
            if (txtCoorID.Text.Trim() == "") pm11.Value = DBNull.Value; else pm11.Value = txtCoorID.Text.Trim();
            pm11.DbType = DbType.String;
            pm11.Direction = ParameterDirection.Input;

            SqlParameter pm12 = new SqlParameter();
            pm12.ParameterName = "@request_dt";
            pm12.SourceColumn = "request_dt";
            if (txtRequest.Text.Trim() == "")
                pm12.Value = DBNull.Value;
            else
            {
                DateTime Request;
                if (DateTime.TryParse(txtRequest.Text.Trim(), out Request))
                    pm12.Value = txtRequest.Text.Trim();
                else
                {
                    ClientScript.RegisterStartupScript(GetType(), "Request Date", "<script>alert('Verify Request Date')</script>");
                    return;
                }
            }
            pm12.DbType = DbType.String;
            pm12.Direction = ParameterDirection.Input;

            SqlParameter PUser = new SqlParameter();
            PUser.ParameterName = "@userName";
            PUser.SourceColumn = "userName";
            PUser.Value = Membership.GetUser().UserName.ToString();
            PUser.DbType = DbType.String;
            PUser.Direction = ParameterDirection.Input;

            SqlParameter pm13 = new SqlParameter();
            pm13.ParameterName = "@InventionSourceFromProject";
            pm13.SourceColumn = "InventionSourceFromProject";
            pm13.Value = ddlSource.SelectedIndex;
            pm13.SqlDbType = SqlDbType.Bit;
            pm13.Direction = ParameterDirection.Input;

            SqlParameter pm14 = new SqlParameter();
            pm14.ParameterName = "@Comments";
            pm14.SourceColumn = "Comments";
            if (txtComments.Text.Trim() == "") pm14.Value = DBNull.Value; else pm14.Value = txtComments.Text.Trim();
            pm14.SqlDbType = SqlDbType.VarChar;
            pm14.Direction = ParameterDirection.Input;

            SqlParameter pm15 = new SqlParameter();
            pm15.ParameterName = "@Specification";
            pm15.SourceColumn = "Specification";
            if (txtWriteUp.Text.Trim() == "") pm15.Value = DBNull.Value; else pm15.Value = txtWriteUp.Text.Trim();
            pm15.SqlDbType = SqlDbType.NVarChar;
            pm15.Direction = ParameterDirection.Input;

            SqlParameter pm16 = new SqlParameter();
            pm16.ParameterName = "@Post_Dated";
            pm16.SourceColumn = "Post_Dated";
            if (txtPost.Text.Trim() == "")
                pm16.Value = DBNull.Value;
            else
            {
                DateTime Request;
                if (DateTime.TryParse(txtPost.Text.Trim(), out Request))
                    pm16.Value = txtPost.Text.Trim();
                else
                {
                    ClientScript.RegisterStartupScript(GetType(), "Post Date", "<script>alert('Verify Post Date')</script>");
                    return;
                }
            }
            pm16.DbType = DbType.String;
            pm16.Direction = ParameterDirection.Input;

            SqlParameter pm17 = new SqlParameter();
            pm17.ParameterName = "@Abstract_TT";
            pm17.SourceColumn = "Abstract_TT";
            if (txtAbstract.Text.Trim() == "") pm17.Value = DBNull.Value; else pm17.Value = txtAbstract.Text.Trim();
            pm17.SqlDbType = SqlDbType.NVarChar;
            pm17.Direction = ParameterDirection.Input;

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
            cmd.Parameters.Add(PUser);
            cmd.Parameters.Add(pm13);
            cmd.Parameters.Add(pm14);
            cmd.Parameters.Add(pm15);
            cmd.Parameters.Add(pm16);
            cmd.Parameters.Add(pm17);
            cmd.ExecuteNonQuery();

            DataTable coInventor = (DataTable)ViewState["CoInventor"];
            DataTable RowDel = coInventor.GetChanges(DataRowState.Deleted);
            if (RowDel != null)
            {
                if (RowDel.Rows.Count > 0)
                {
                    foreach (DataRow dr in RowDel.Rows)
                    {
                        if (dr.HasVersion(DataRowVersion.Original)==true)
                        {
                            SqlCommand cmd2 = new SqlCommand();
                            cmd2.Transaction = Trans;
                            string sql2 = "delete from coInventorDetails where fileNo=@fileNo and SlNo=@SlNo";
                            cmd2.CommandText = sql2;
                            cmd2.CommandType = CommandType.Text;
                            cmd2.Connection = cnp;

                            SqlParameter bpd1 = new SqlParameter();
                            bpd1.ParameterName = "@fileNo";
                            bpd1.SourceColumn = "fileNo";
                            bpd1.Value = txtFileNo.Text.Trim();
                            bpd1.DbType = DbType.String;
                            bpd1.Direction = ParameterDirection.Input;

                            SqlParameter bpd2 = new SqlParameter();
                            bpd2.ParameterName = "@SlNo";
                            bpd2.SourceColumn = "SlNo";
                            bpd2.Value = Convert.ToInt32(dr["SlNo", DataRowVersion.Original]);
                            bpd2.DbType = DbType.Int32;
                            bpd2.Direction = ParameterDirection.Input;

                            cmd2.Parameters.Add(bpd1);
                            cmd2.Parameters.Add(bpd2);
                            cmd2.ExecuteNonQuery();
                        }
                    }
                }
            }
            DataTable RowMod = coInventor.GetChanges(DataRowState.Modified);
            if (RowMod != null)
            {
                if (RowMod.Rows.Count > 0)
                {
                    foreach (DataRow dr in RowMod.Rows)
                    {
                        SqlCommand cmd3 = new SqlCommand();
                        cmd3.Transaction = Trans;
                        cmd3.CommandText = "UpdateCoInventor";
                        cmd3.CommandType = CommandType.StoredProcedure;
                        cmd3.Connection = cnp;
                       
                        SqlParameter cpm1 = new SqlParameter();
                        cpm1.ParameterName = "@FileNo";
                        cpm1.SourceColumn = "FileNo";
                        cpm1.Value = txtFileNo.Text.Trim();
                        cpm1.DbType = DbType.String;
                        cpm1.Direction = ParameterDirection.Input;

                        SqlParameter cpm2 = new SqlParameter();
                        cpm2.ParameterName = "@SlNo";
                        cpm2.SourceColumn = "SlNo";
                        cpm2.Value = Convert.ToInt32(dr["SlNo",DataRowVersion.Current]);
                        cpm2.DbType = DbType.Int32;
                        cpm2.Direction = ParameterDirection.Input;

                        SqlParameter cpm3 = new SqlParameter();
                        cpm3.ParameterName = "@OriginalSlNo";
                        cpm3.SourceColumn = "SlNo";
                        cpm3.Value = dr["SlNo",DataRowVersion.Original].ToString();
                        cpm3.DbType = DbType.String;
                        cpm3.Direction = ParameterDirection.Input;

                        SqlParameter cpm4 = new SqlParameter();
                        cpm4.ParameterName = "@InventorType";
                        cpm4.SourceColumn = "InventorType";
                        cpm4.Value = dr["InventorType"].ToString();
                        cpm4.DbType = DbType.String;
                        cpm4.Direction = ParameterDirection.Input;

                        SqlParameter cpm5 = new SqlParameter();
                        cpm5.ParameterName = "@DeptCode";
                        cpm5.SourceColumn = "DeptCode";
                        if (dr["DeptCode"].ToString() == "") cpm5.Value = DBNull.Value; else cpm5.Value = dr["DeptCode"].ToString();
                        cpm5.DbType = DbType.String;
                        cpm5.Direction = ParameterDirection.Input;

                        SqlParameter cpm6 = new SqlParameter();
                        cpm6.ParameterName = "@DeptOrOrganisation";
                        cpm6.SourceColumn = "DeptOrOrganisation";
                        cpm6.Value = dr["DeptOrgName"].ToString();
                        cpm6.DbType = DbType.String;
                        cpm6.Direction = ParameterDirection.Input;

                        SqlParameter cpm7 = new SqlParameter();
                        cpm7.ParameterName = "@InventorID";
                        cpm7.SourceColumn = "InventorID";
                        if (dr["InventorID"].ToString() == "") cpm7.Value = DBNull.Value; else cpm7.Value = dr["InventorID"].ToString();
                        cpm7.DbType = DbType.String;
                        cpm7.Direction = ParameterDirection.Input;

                        SqlParameter cpm8 = new SqlParameter();
                        cpm8.ParameterName = "@InventorName";
                        cpm8.SourceColumn = "InventorName";
                        cpm8.Value = dr["InventorName"].ToString();
                        cpm8.DbType = DbType.String;
                        cpm8.Direction = ParameterDirection.Input;

                        cmd3.Parameters.Add(cpm1);
                        cmd3.Parameters.Add(cpm2);
                        cmd3.Parameters.Add(cpm3);
                        cmd3.Parameters.Add(cpm4);
                        cmd3.Parameters.Add(cpm5);
                        cmd3.Parameters.Add(cpm6);
                        cmd3.Parameters.Add(cpm7);
                        cmd3.Parameters.Add(cpm8);

                        cmd3.ExecuteNonQuery();
                    }
                }
            }
            DataTable RowAdd = coInventor.GetChanges(DataRowState.Added);
            if (RowAdd != null)
            {
                if (RowAdd.Rows.Count > 0)
                {
                    foreach (DataRow dr in RowAdd.Rows)
                    {
                        SqlCommand cmd4 = new SqlCommand();
                        cmd4.Transaction = Trans;
                        cmd4.CommandText = "CoInventor";
                        cmd4.CommandType = CommandType.StoredProcedure;
                        cmd4.Connection = cnp;

                        SqlParameter bpm1 = new SqlParameter();
                        bpm1.ParameterName = "@EntryDt";
                        bpm1.SourceColumn = "EntryDt";
                        bpm1.Value = DateTime.Now.ToShortDateString();
                        bpm1.DbType = DbType.String;
                        bpm1.Direction = ParameterDirection.Input;

                        SqlParameter bpm2 = new SqlParameter();
                        bpm2.ParameterName = "@SlNo";
                        bpm2.SourceColumn = "SlNo";
                        bpm2.Value = dr["SlNo"].ToString();
                        bpm2.DbType = DbType.Int32;
                        bpm2.Direction = ParameterDirection.Input;

                        SqlParameter bpm3 = new SqlParameter();
                        bpm3.ParameterName = "@FileNo";
                        bpm3.SourceColumn = "FileNo";
                        bpm3.Value = txtFileNo.Text.Trim();
                        bpm3.DbType = DbType.String;
                        bpm3.Direction = ParameterDirection.Input;

                        SqlParameter bpm4 = new SqlParameter();
                        bpm4.ParameterName = "@InventorType";
                        bpm4.SourceColumn = "InventorType";
                        bpm4.Value = dr["InventorType"].ToString();
                        bpm4.DbType = DbType.String;
                        bpm4.Direction = ParameterDirection.Input;

                        SqlParameter bpm5 = new SqlParameter();
                        bpm5.ParameterName = "@InventorName";
                        bpm5.SourceColumn = "InventorName";
                        bpm5.Value = dr["InventorName"].ToString();
                        bpm5.DbType = DbType.String;
                        bpm5.Direction = ParameterDirection.Input;

                        SqlParameter bpm6 = new SqlParameter();
                        bpm6.ParameterName = "@DeptCode";
                        bpm6.SourceColumn = "DeptCode";
                        if (dr["DeptCode"].ToString() == "") bpm6.Value = DBNull.Value; else bpm6.Value = dr["DeptCode"].ToString();
                        bpm6.DbType = DbType.String;
                        bpm6.Direction = ParameterDirection.Input;

                        SqlParameter bpm7 = new SqlParameter();
                        bpm7.ParameterName = "@DeptOrOrganisation";
                        bpm7.SourceColumn = "DeptOrOrganisation";
                        bpm7.Value = dr["DeptOrgName"].ToString();
                        bpm7.DbType = DbType.String;
                        bpm7.Direction = ParameterDirection.Input;

                        SqlParameter bpm8 = new SqlParameter();
                        bpm8.ParameterName = "@InventorID";
                        bpm8.SourceColumn = "InventorID";
                        if (dr["InventorID"].ToString() == "") bpm8.Value = DBNull.Value; else bpm8.Value = dr["InventorID"].ToString();
                        bpm8.DbType = DbType.String;
                        bpm8.Direction = ParameterDirection.Input;

                        cmd4.Parameters.Add(bpm1);
                        cmd4.Parameters.Add(bpm2);
                        cmd4.Parameters.Add(bpm3);
                        cmd4.Parameters.Add(bpm4);
                        cmd4.Parameters.Add(bpm5);
                        cmd4.Parameters.Add(bpm6);
                        cmd4.Parameters.Add(bpm7);
                        cmd4.Parameters.Add(bpm8);

                        cmd4.ExecuteNonQuery();
                    }
                }
            }
           
            DataTable InventionSource = (DataTable)ViewState["InventionSource"];
            DataTable SourceDel = InventionSource.GetChanges(DataRowState.Deleted);
            if (SourceDel != null)
            {
                if (SourceDel.Rows.Count > 0)
                {
                    foreach (DataRow dr in SourceDel.Rows)
                    {
                        if (dr.HasVersion(DataRowVersion.Original) == true)
                        {
                            SqlCommand cmd5 = new SqlCommand();
                            cmd5.Transaction = Trans;
                            string sql3 = "delete from InventionSource where fileNo=@fileNo and SlNo=@SlNo";
                            cmd5.CommandText = sql3;
                            cmd5.CommandType = CommandType.Text;
                            cmd5.Connection = cnp;

                            SqlParameter spd1 = new SqlParameter();
                            spd1.ParameterName = "@fileNo";
                            spd1.SourceColumn = "fileNo";
                            spd1.Value = txtFileNo.Text.Trim();
                            spd1.SqlDbType = SqlDbType.VarChar;
                            spd1.Direction = ParameterDirection.Input;

                            SqlParameter spd2 = new SqlParameter();
                            spd2.ParameterName = "@SlNo";
                            spd2.SourceColumn = "SlNo";
                            spd2.Value = Convert.ToInt32(dr["SlNo", DataRowVersion.Original]);
                            spd2.SqlDbType = SqlDbType.Int;
                            spd2.Direction = ParameterDirection.Input;

                            cmd5.Parameters.Add(spd1);
                            cmd5.Parameters.Add(spd2);
                            cmd5.ExecuteNonQuery();
                        }
                    }
                }
            }
            DataTable SourceMod = InventionSource.GetChanges(DataRowState.Modified);
            if (SourceMod != null)
            {
                if (SourceMod.Rows.Count > 0)
                {
                    foreach (DataRow dr in SourceMod.Rows)
                    {
                        SqlCommand cmd6 = new SqlCommand();
                        cmd6.Transaction = Trans;
                        cmd6.CommandText = "ModifyInventionSource";
                        cmd6.CommandType = CommandType.StoredProcedure;
                        cmd6.Connection = cnp;

                        SqlParameter spm1 = new SqlParameter();
                        spm1.ParameterName = "@FileNo";
                        spm1.SourceColumn = "FileNo";
                        spm1.Value = txtFileNo.Text.Trim();
                        spm1.SqlDbType = SqlDbType.VarChar;
                        spm1.Direction = ParameterDirection.Input;

                        SqlParameter spm2 = new SqlParameter();
                        spm2.ParameterName = "@SlNo";
                        spm2.SourceColumn = "SlNo";
                        spm2.Value = Convert.ToInt32(dr["SlNo", DataRowVersion.Current]);
                        spm2.SqlDbType = SqlDbType.Int;
                        spm2.Direction = ParameterDirection.Input;

                        SqlParameter spm3 = new SqlParameter();
                        spm3.ParameterName = "@OriginalSlNo";
                        spm3.SourceColumn = "SlNo";
                        spm3.Value = dr["SlNo", DataRowVersion.Original].ToString();
                        spm3.SqlDbType = SqlDbType.Int;
                        spm3.Direction = ParameterDirection.Input;

                        SqlParameter spm4 = new SqlParameter();
                        spm4.ParameterName = "@ProjectType";
                        spm4.SourceColumn = "ProjectType";
                        spm4.Value = dr["ProjectType"].ToString();
                        spm4.SqlDbType = SqlDbType.VarChar;
                        spm4.Direction = ParameterDirection.Input;

                        SqlParameter spm5 = new SqlParameter();
                        spm5.ParameterName = "@FinancialYear";
                        spm5.SourceColumn = "FinancialYear";
                        spm5.Value = dr["FinancialYear"].ToString();
                        spm5.SqlDbType = SqlDbType.VarChar;
                        spm5.Direction = ParameterDirection.Input;

                        SqlParameter spm6 = new SqlParameter();
                        spm6.ParameterName = "@ProjectNumber";
                        spm6.SourceColumn = "ProjectNumber";
                        spm6.Value = dr["ProjectNumber"].ToString();
                        spm6.SqlDbType = SqlDbType.VarChar;
                        spm6.Direction = ParameterDirection.Input;

                        SqlParameter spm7 = new SqlParameter();
                        spm7.ParameterName = "@FundingAgency";
                        spm7.SourceColumn = "FundingAgency";
                        spm7.Value = dr["FundingAgency"].ToString();
                        spm7.SqlDbType = SqlDbType.VarChar;
                        spm7.Direction = ParameterDirection.Input;

                        SqlParameter spm8 = new SqlParameter();
                        spm8.ParameterName = "@ProjectTitle";
                        spm8.SourceColumn = "ProjectTitle";
                        spm8.Value = dr["ProjectTitle"].ToString();
                        spm8.SqlDbType = SqlDbType.VarChar;
                        spm8.Direction = ParameterDirection.Input;

                        cmd6.Parameters.Add(spm1);
                        cmd6.Parameters.Add(spm2);
                        cmd6.Parameters.Add(spm3);
                        cmd6.Parameters.Add(spm4);
                        cmd6.Parameters.Add(spm5);
                        cmd6.Parameters.Add(spm6);
                        cmd6.Parameters.Add(spm7);
                        cmd6.Parameters.Add(spm8);

                        cmd6.ExecuteNonQuery();
                    }
                }
            }
            DataTable SourceAdd = InventionSource.GetChanges(DataRowState.Added);
            if (SourceAdd != null)
            {
                if (SourceAdd.Rows.Count > 0)
                {
                    foreach (DataRow dr in SourceAdd.Rows)
                    {
                        SqlCommand cmd7 = new SqlCommand();
                        cmd7.Transaction = Trans;
                        cmd7.CommandText = "NewInventionSource";
                        cmd7.CommandType = CommandType.StoredProcedure;
                        cmd7.Connection = cnp;

                        SqlParameter sip1 = new SqlParameter();
                        sip1.ParameterName = "@EntryDt";
                        sip1.SourceColumn = "EntryDt";
                        sip1.Value = DateTime.Now.ToShortDateString();
                        sip1.SqlDbType = SqlDbType.VarChar;
                        sip1.Direction = ParameterDirection.Input;

                        SqlParameter sip2 = new SqlParameter();
                        sip2.ParameterName = "@SlNo";
                        sip2.SourceColumn = "SlNo";
                        sip2.Value = dr["SlNo"].ToString();
                        sip2.SqlDbType = SqlDbType.Int;
                        sip2.Direction = ParameterDirection.Input;

                        SqlParameter sip3 = new SqlParameter();
                        sip3.ParameterName = "@FileNo";
                        sip3.SourceColumn = "FileNo";
                        sip3.Value = txtFileNo.Text.Trim();
                        sip3.SqlDbType = SqlDbType.VarChar;
                        sip3.Direction = ParameterDirection.Input;

                        SqlParameter sip4 = new SqlParameter();
                        sip4.ParameterName = "@ProjectType";
                        sip4.SourceColumn = "ProjectType";
                        sip4.Value = dr["ProjectType"].ToString();
                        sip4.SqlDbType = SqlDbType.VarChar;
                        sip4.Direction = ParameterDirection.Input;

                        SqlParameter sip5 = new SqlParameter();
                        sip5.ParameterName = "@ProjectNumber";
                        sip5.SourceColumn = "ProjectNumber";
                        sip5.Value = dr["ProjectNumber"].ToString();
                        sip5.SqlDbType = SqlDbType.VarChar;
                        sip5.Direction = ParameterDirection.Input;

                        SqlParameter sip6 = new SqlParameter();
                        sip6.ParameterName = "@FundingAgency";
                        sip6.SourceColumn = "FundingAgency";
                        sip6.Value = dr["FundingAgency"].ToString();
                        sip6.SqlDbType = SqlDbType.VarChar;
                        sip6.Direction = ParameterDirection.Input;

                        SqlParameter sip7 = new SqlParameter();
                        sip7.ParameterName = "@ProjectTitle";
                        sip7.SourceColumn = "ProjectTitle";
                        sip7.Value = dr["ProjectTitle"].ToString();
                        sip7.SqlDbType = SqlDbType.VarChar;
                        sip7.Direction = ParameterDirection.Input;

                        SqlParameter sip8 = new SqlParameter();
                        sip8.ParameterName = "@FinancialYear";
                        sip8.SourceColumn = "FinancialYear";
                        sip8.Value = dr["FinancialYear"].ToString();
                        sip8.SqlDbType = SqlDbType.VarChar;
                        sip8.Direction = ParameterDirection.Input;

                        cmd7.Parameters.Add(sip1);
                        cmd7.Parameters.Add(sip2);
                        cmd7.Parameters.Add(sip3);
                        cmd7.Parameters.Add(sip4);
                        cmd7.Parameters.Add(sip5);
                        cmd7.Parameters.Add(sip6);
                        cmd7.Parameters.Add(sip7);
                        cmd7.Parameters.Add(sip8);
                        cmd7.ExecuteNonQuery();                        
                    }
                }
            }
            Trans.Commit();
            ClientScript.RegisterStartupScript(GetType(), "success", "<script>alert('This Record successfully Modified')</script>");
            cnp.Close();
        }
        catch (Exception ex)
        {
            Trans.Rollback();
            ClientScript.RegisterStartupScript(GetType(), "Error", "<script>alert('" + ex.Message.ToString() + "')</script>");
            cnp.Close();
            return;
        }
        imgBtnClear_Click(sender, e);
    }

    protected void ddlType1_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlDept1.Items.Clear();
        txtDept1.Text = "";
        ddlCoor1.Items.Clear();
        txtCoor1.Text = "";
        txtCoorID.Text = "";
        if (ddlType1.SelectedIndex == 1)
        {
            FillDept(ddlDept1);
            ddlDept1.Visible = true;
            txtDept1.Enabled = false;
            tdFirstInvent.InnerText = "First Inventor Dept.";
            ddlCoor1.Visible = true;
            txtCoor1.Visible = false;
            txtCoorID.Enabled = false;
        }
        else if (ddlType1.SelectedIndex == 2 || ddlType1.SelectedIndex == 3 || ddlType1.SelectedIndex == 4)
        {
            FillDept(ddlDept1);
            ddlDept1.Visible = true;
            txtDept1.Enabled = false;
            tdFirstInvent.InnerText = "First Inventor Dept.";
            ddlCoor1.Visible = false;
            txtCoor1.Visible = true;
            txtCoorID.Enabled = true;
        }
        else if (ddlType1.SelectedIndex == 5)
        {
            tdFirstInvent.InnerText = "Organization Name";
            ddlCoor1.Visible = false;
            txtCoor1.Visible = true;
            txtCoorID.Enabled = true;
            ddlDept1.Visible = false;
            txtDept1.Enabled = true;
        }
        else
        {
            ddlCoor1.Visible = false;
            txtCoor1.Visible = true;
            txtCoorID.Enabled = true;
            ddlDept1.Visible = false;
            txtDept1.Enabled = true;
        }
    }
    protected void ddlDept1_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlCoor1.Items.Clear();
        txtCoor1.Text = "";
        txtCoorID.Text = "";
        if (ddlType1.SelectedIndex != 5)
        {
            txtDept1.Text = ddlDept1.SelectedValue;
        }
        if (ddlType1.SelectedIndex == 1)
        {
            deptFlag = FindDeptOrCentre(ddlDept1.SelectedItem.Text.Trim());
            if (deptFlag == "Dept")
            {
                FillCoor(ddlCoor1, ddlDept1.SelectedItem.Text.Trim());
            }
            else
            {
                FillCoor(ddlCoor1, "All");
            }
            ddlCoor1.Visible = true;
            txtCoorID.Enabled = false;
            txtCoor1.Visible = false;
        }
        else
        {

            ddlCoor1.Visible = false;
            txtCoor1.Visible = true;
            txtCoorID.Enabled = true;
        }
    }
    protected void ddlCoor1_SelectedIndexChanged(object sender, EventArgs e)
    {
        txtCoorID.Text = "";
        string tmp = FindInstID(ddlCoor1.SelectedItem.Value.Trim());
        txtCoorID.Text = tmp;
        con.Close();
    }
    protected void lvInventor_ItemCanceling(object sender, ListViewCancelEventArgs e)
    {
        System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-GB");
        lvInventor.EditIndex = -1;
        lvInventor.DataSource = (DataTable)ViewState["CoInventor"];
        lvInventor.DataBind();
    }
    protected void lvInventor_ItemCreated(object sender, ListViewItemEventArgs e)
    {
        DropDownList ddlType = (DropDownList)e.Item.FindControl("ddlNewType");
        if (ddlType != null)
        {
            con.ConnectionString = ConfigurationManager.ConnectionStrings["PATENTCN"].ConnectionString;
            SqlCommand cmd = new SqlCommand();
            string sql = "select itemList from listitemmaster where category like 'InventorType' order by slno";
            SqlDataReader dr;
            cmd.CommandType = CommandType.Text;
            cmd.Connection = con;
            cmd.CommandText = sql;
            con.Open();
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                ddlType.Items.Add(dr["itemList"].ToString());
            }
            ddlType.Items.Insert(0, new ListItem("", ""));
            con.Close();
        }
    }
    protected void lvInventor_ItemDeleting(object sender, ListViewDeleteEventArgs e)
    {
        System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-GB");
        Label lbl = lvInventor.Items[e.ItemIndex].FindControl("lblSlNo") as Label;
        DataTable dt = (DataTable)ViewState["CoInventor"];
        int cnt = 0;
        foreach (DataRow dr in dt.Rows)
        {
            if (dr.RowState != DataRowState.Deleted)
            {
                if (dr["SlNo"].ToString() == lbl.Text.Trim())
                {
                    dr.Delete();
                    break;
                }
            }
        }
        for (int i = 0; i <= dt.Rows.Count - 1; i++)
        {
            if(dt.Rows[i].RowState !=DataRowState.Deleted)
            {
                cnt += 1;
                dt.Rows[i][0] = cnt;                
            }
        }
        lvInventor.DataSource = (DataTable)ViewState["CoInventor"];
        lvInventor.DataBind();
    }
    protected void lvInventor_ItemInserting(object sender, ListViewInsertEventArgs e)
    {
        string slno = Convert.ToString(lvInventor.Items.Count + 1);
        
        DropDownList ddlType = e.Item.FindControl("ddlNewType") as DropDownList;
        if (ddlType.SelectedIndex == -1 || ddlType.SelectedIndex == 0)
        {
            ClientScript.RegisterStartupScript(this.GetType(), "Request", "<script>alert('Select Inventor Type')</script>");
            return;
        }
        DropDownList ddlDept = e.Item.FindControl("ddlNewDeptCode") as DropDownList;
        TextBox tmpDept = e.Item.FindControl("txtDept") as TextBox;
        DropDownList ddlCoor = e.Item.FindControl("ddlNewName") as DropDownList;
        TextBox tmpCoor = e.Item.FindControl("txtName") as TextBox;
        TextBox tmpID = e.Item.FindControl("txtID") as TextBox;
        if (ddlType.SelectedIndex == 1)
        {
            if (ddlDept.SelectedIndex == -1 || ddlDept.SelectedIndex == 0)
            {
                ClientScript.RegisterStartupScript(GetType(), "Dept Code", "<script>alert('Select Department')</script>");
                return;
            }
            if (ddlCoor.SelectedIndex == -1 || ddlCoor.SelectedIndex == 0)
            {
                ClientScript.RegisterStartupScript(GetType(), "Inventor Name", "<script>alert('Select Inventor Name')</script>");
                return;
            }
        }
        else if (ddlType.SelectedIndex == 2 || ddlType.SelectedIndex == 3 || ddlType.SelectedIndex == 4)
        {
            if (ddlDept.SelectedIndex == -1 || ddlDept.SelectedIndex == 0)
            {
                ClientScript.RegisterStartupScript(GetType(), "Dept Code", "<script>alert('Select Department')</script>");
                return;
            }
            if (tmpCoor.Text.Trim() == "")
            {
                ClientScript.RegisterStartupScript(GetType(), "Inventor Name", "<script>alert('Enter Inventor Name')</script>");
                return;
            }
        }
        else if (ddlType.SelectedIndex == 5)
        {
            if (tmpDept.Text.Trim() == "")
            {
                ClientScript.RegisterStartupScript(GetType(), "Dept Code", "<script>alert('Enter Organization Name')</script>");
                return;
            }
            if (tmpCoor.Text.Trim() == "")
            {
                ClientScript.RegisterStartupScript(GetType(), "Inventor Name", "<script>alert('Enter Inventor Name')</script>");
                return;
            }
        }

        DataTable dt = (DataTable)ViewState["CoInventor"];
        DataRow dr = dt.NewRow();
        dr["SlNo"] = slno;
        dr["InventorType"] = ddlType.SelectedItem.Text.Trim();
        if (ddlDept.SelectedIndex != -1 && ddlDept.SelectedIndex != 0)
            dr["DeptCode"] = ddlDept.SelectedItem.Text.Trim();
        else dr["DeptCode"] = DBNull.Value;
        UpperCaseWords toUpperWord = new UpperCaseWords();
        if (ddlType.SelectedIndex == 5) dr["DeptOrgName"] = toUpperWord.toUpperCase(tmpDept.Text.Trim());
        else dr["DeptOrgName"] = tmpDept.Text.Trim();
        if (tmpID.Text.Trim() != "") dr["InventorID"] = tmpID.Text.Trim().ToUpper();
        else dr["InventorID"] = DBNull.Value;
        if (ddlType.SelectedIndex == 1)
            if (ddlCoor.SelectedItem.Text.IndexOf("~")!=-1)
                dr["InventorName"] =  ddlCoor.SelectedItem.Text.Substring(ddlCoor.SelectedItem.Text.IndexOf("~")+1).Trim().ToUpper();
            else
                dr["InventorName"] = ddlCoor.SelectedItem.Text.Trim().ToUpper();
        else
            dr["InventorName"] = tmpCoor.Text.Trim().ToUpper();
        dt.Rows.Add(dr);
        lvInventor.DataSource = (DataTable)ViewState["CoInventor"];
        lvInventor.DataBind();
    }

    protected void ddlCoor_SelectedIndexChanged(object sender, EventArgs e)
    {
        DropDownList dList = (DropDownList)sender;
        ListViewItem item1 = (ListViewItem)dList.NamingContainer;
        DropDownList tmpName = (DropDownList)item1.FindControl("ddlNewName");
        TextBox tmpID = (TextBox)item1.FindControl("txtID");
        tmpID.Text = "";
        tmpID.Visible = true;
        tmpID.Text = FindInstID(tmpName.SelectedItem.Value.Trim());
    }

    protected string FindInstID(string coorCode)
    {
        con.ConnectionString = ConfigurationManager.ConnectionStrings["ICSR2KSCN"].ConnectionString;
        SqlCommand cmd = new SqlCommand();
        string sql = "SELECT CR_CODE,INSTID FROM Foxoffice..COORCOD WHERE CR_CODE LIKE '" + coorCode + "'";
        cmd.Connection = con;
        cmd.CommandType = CommandType.Text;
        cmd.CommandText = sql;
        con.Open();
        SqlDataReader dr;
        dr = cmd.ExecuteReader();
        dr.Read();
        string tmp;
        if (dr.IsDBNull(1))
            tmp = dr.GetString(0);
        else
            tmp = dr.GetString(1);
        con.Close();
        return tmp;
    }
    protected void ddlDept_SelectedIndexChanged(object sender, EventArgs e)
    {
        DropDownList dListGet = (DropDownList)sender;
        ListViewItem item1 = (ListViewItem)dListGet.NamingContainer;
        DropDownList tmpType = (DropDownList)item1.FindControl("ddlNewType");
        DropDownList dDept = (DropDownList)item1.FindControl("ddlNewDeptCode");
        TextBox txtDepart = (TextBox)item1.FindControl("txtDept");
        DropDownList dlCoor = (DropDownList)item1.FindControl("ddlNewName");
        TextBox tmpID = (TextBox)item1.FindControl("txtID");
        TextBox tmpName = (TextBox)item1.FindControl("txtName");
        txtDepart.Text = "";
        dlCoor.Items.Clear();
        tmpID.Text = "";
        tmpName.Text = "";
        if (tmpType.SelectedIndex != 5)
        {
            txtDepart.Text = dDept.SelectedValue;
        }
        if (tmpType.SelectedIndex == 1)
        {
            deptFlag = FindDeptOrCentre(dDept.SelectedItem.Text.Trim());
            if (deptFlag == "Dept")
            {
                FillCoor(dlCoor, dDept.SelectedItem.Text.Trim());
            }
            else
            {
                FillCoor(dlCoor, "All");
            }

            dlCoor.Visible = true;
            tmpID.Enabled = false;
            tmpName.Visible = false;
        }
        else
        {
            dlCoor.Visible = false;
            tmpID.Enabled = true;
            tmpName.Visible = true;
        }
    }
    protected string FindDeptOrCentre(string tmpDeptCode)
    {
        con.ConnectionString = ConfigurationManager.ConnectionStrings["PATENTCN"].ConnectionString;
        SqlCommand cmd = new SqlCommand();
        string sql = "SELECT DEPTTYPE FROM DEPARTMENT WHERE DEPTCODE LIKE '" + tmpDeptCode + "'";
        cmd.Connection = con;
        cmd.CommandType = CommandType.Text;
        cmd.CommandText = sql;
        con.Open();
        string tmpVal =Convert.ToString(cmd.ExecuteScalar());
        con.Close();
        return tmpVal;
    }
    protected void FillCoor(DropDownList dlCoor, string tmpDeptCode)
    {
        con.ConnectionString = ConfigurationManager.ConnectionStrings["ICSR2KSCN"].ConnectionString;
        SqlCommand cmd = new SqlCommand();
        string sql;
        if (tmpDeptCode == "All")
            sql = "SELECT  CR_CODE+ '  ~  ' + COOR_NAME AS COORNAME,cr_code FROM FoxOffice..COORCOD ORDER BY COORNAME";
        else
            sql = "SELECT COOR_NAME,CR_CODE FROM FoxOffice..COORCOD WHERE DEPARTMENT LIKE '" + tmpDeptCode + "' ORDER BY COOR_NAME";
        cmd.Connection = con;
        cmd.CommandType = CommandType.Text;
        cmd.CommandText = sql;
        SqlDataReader dr;
        con.Open();
        dr = cmd.ExecuteReader();
        dlCoor.DataTextField = "COOR_NAME";
        dlCoor.DataValueField = "CR_CODE";
        dlCoor.DataSource = dr;
        dlCoor.DataBind();
        dlCoor.Items.Insert(0, "");
        dr.Close();
        con.Close();
    }
    protected void ddlType_SelectedIndexChanged(object sender, EventArgs e)
    {
        DropDownList dListGet = (DropDownList)sender;
        ListViewItem item1 = (ListViewItem)dListGet.NamingContainer;
        DropDownList dList = (DropDownList)item1.FindControl("ddlNewType");
        DropDownList dlDept = (DropDownList)item1.FindControl("ddlNewDeptCode");
        TextBox txtDepart = (TextBox)item1.FindControl("txtDept");
        DropDownList dlCoor = (DropDownList)item1.FindControl("ddlNewName");
        TextBox tmpID = (TextBox)item1.FindControl("txtID");
        TextBox tmpName = (TextBox)item1.FindControl("txtName");
        dlDept.Items.Clear();
        txtDepart.Text = "";
        dlCoor.Items.Clear();
        tmpID.Text = "";
        tmpName.Text = "";
        if (dList.SelectedIndex == 1)
        {
            FillDept(dlDept);
            dlDept.Visible = true;
            txtDepart.Enabled = false;
            tmpID.Enabled = false;
            tmpName.Visible = false;
            dlCoor.Visible = true;
        }
        else if (dList.SelectedIndex == 2 || dList.SelectedIndex == 3 || dList.SelectedIndex == 4)
        {
            FillDept(dlDept);
            dlDept.Visible = true;
            txtDepart.Enabled = false;
            tmpID.Enabled = true;
            tmpName.Visible = true;
            dlCoor.Visible = false;
        }
        else if (dList.SelectedIndex == 5)
        {
            dlDept.Visible = false;
            txtDepart.Enabled = true;
            tmpID.Enabled = true;
            dlCoor.Visible = false;
            tmpName.Visible = true;
        }
    }
    protected void FillDept(DropDownList dlDept)
    {
        con.ConnectionString = ConfigurationManager.ConnectionStrings["PATENTCN"].ConnectionString;
        SqlCommand cmd = new SqlCommand();
        string sql = "SELECT DEPTCODE,DEPTNAME FROM DEPARTMENT ORDER BY DEPTCODE";
        cmd.Connection = con;
        cmd.CommandType = CommandType.Text;
        cmd.CommandText = sql;
        SqlDataReader dr;
        con.Open();
        dr = cmd.ExecuteReader();
        dlDept.DataTextField = "DEPTCODE";
        dlDept.DataValueField = "DEPTNAME";
        dlDept.DataSource = dr;
        dlDept.DataBind();
        dlDept.Items.Insert(0, "");
        dr.Close();
        con.Close();
    }
    protected void lvInventor_ItemDataBound(object sender, ListViewItemEventArgs e)
    {
        LinkButton lb = e.Item.FindControl("lbtnDelete") as LinkButton;
        if (lb != null)
        {
            if (User.IsInRole("Admin"))
            {
                lb.Visible = true;
            }
        }
        if (lvInventor.EditIndex == (e.Item as ListViewDataItem).DataItemIndex)
        {
            DropDownList ddlEditType = (DropDownList)e.Item.FindControl("ddlEditType");
            if (ddlEditType != null)
            {
                con.ConnectionString = ConfigurationManager.ConnectionStrings["PATENTCN"].ConnectionString;
                SqlCommand cmd = new SqlCommand();
                string sql = "select itemList from listitemmaster where category like 'InventorType' order by slno";
                SqlDataReader dr;
                cmd.CommandType = CommandType.Text;
                cmd.Connection = con;
                cmd.CommandText = sql;
                con.Open();
                dr = cmd.ExecuteReader();
                ddlEditType.DataTextField = "itemList";
                ddlEditType.DataValueField = "itemList";
                ddlEditType.DataSource = dr;
                ddlEditType.DataBind();
                ddlEditType.Items.Insert(0, new ListItem("", ""));
                con.Close();
            }
            Label lblEditType = (Label)e.Item.FindControl("lblEditType");
            ddlEditType.Items.FindByValue(lblEditType.Text.Trim()).Selected = true;
            ddlEditType_SelectedIndexChanged(ddlEditType, EventArgs.Empty);
            if (ddlEditType.SelectedIndex == 1 || ddlEditType.SelectedIndex == 2 || ddlEditType.SelectedIndex == 3 || ddlEditType.SelectedIndex == 4)
            {
                DropDownList ddlEditDept = (DropDownList)e.Item.FindControl("ddlEditDeptCode");
                Label lblEditDept = (Label)e.Item.FindControl("lblEditDeptCode");
                ddlEditDept.Items.FindByText(lblEditDept.Text.Trim()).Selected = true;
                ddlEditDept_SelectedIndexChanged(ddlEditDept, EventArgs.Empty);
            }
            if (ddlEditType.SelectedIndex == 1)
            {
                DropDownList ddlEditName = (DropDownList)e.Item.FindControl("ddlEditName");
                TextBox txtEditName = (TextBox)e.Item.FindControl("txtEditName");
                TextBox txtEditID = (TextBox)e.Item.FindControl("txtEditID");
                int tmpVal;
                string Coor_Code;
                if (int.TryParse(txtEditID.Text.Trim(), out tmpVal) == true)
                {
                    con.ConnectionString = ConfigurationManager.ConnectionStrings["PATENTCN"].ConnectionString;
                    SqlCommand cmd5 = new SqlCommand();
                    string sql5 = "SELECT COORCODE FROM FACULTYMASTER WHERE INSTID LIKE '" + txtEditID.Text.Trim() + "'";
                    cmd5.Connection = con;
                    cmd5.CommandType = CommandType.Text;
                    cmd5.CommandText = sql5;
                    con.Open();
                    Coor_Code = Convert.ToString(cmd5.ExecuteScalar());
                    con.Close();
                }
                else
                {
                    Coor_Code = txtEditID.Text.Trim();
                }
                ddlEditName.Items.FindByValue(Coor_Code).Selected = true;
                ddlEditCoor_SelectedIndexChanged(ddlEditName, EventArgs.Empty);
            }             
        }
    }
    protected void ddlEditType_SelectedIndexChanged(object sender, EventArgs e)
    {
        DropDownList dListGet = (DropDownList)sender;
        ListViewItem item1 = (ListViewItem)dListGet.NamingContainer;
        DropDownList dList = (DropDownList)item1.FindControl("ddlEditType");
        DropDownList dlDept = (DropDownList)item1.FindControl("ddlEditDeptCode");
        TextBox txtDepart = (TextBox)item1.FindControl("txtEditDept");
        DropDownList dlCoor = (DropDownList)item1.FindControl("ddlEditName");
        TextBox tmpID = (TextBox)item1.FindControl("txtEditID");
        TextBox tmpName = (TextBox)item1.FindControl("txtEditName");
        dlDept.Items.Clear();
        dlCoor.Items.Clear();
        if (dList.SelectedIndex == 1)
        {
            FillDept(dlDept);
            dlDept.Visible = true;
            txtDepart.Enabled = false;
            tmpID.Enabled = false;
            tmpName.Visible = false;
            dlCoor.Visible = true;
        }
        else if (dList.SelectedIndex == 2 || dList.SelectedIndex == 3 || dList.SelectedIndex == 4)
        {
            FillDept(dlDept);
            dlDept.Visible = true;
            txtDepart.Enabled = false;
            tmpID.Enabled = true;
            tmpName.Visible = true;
            dlCoor.Visible = false;
        }
        else //if (dList.SelectedIndex == 4)
        {
            dlDept.Visible = false;
            txtDepart.Enabled = true;
            tmpID.Enabled = true;
            dlCoor.Visible = false;
            tmpName.Visible = true;

        }

    }
    protected void ddlEditDept_SelectedIndexChanged(object sender, EventArgs e)
    {
        DropDownList dListGet = (DropDownList)sender;
        ListViewItem item1 = (ListViewItem)dListGet.NamingContainer;
        DropDownList tmpType = (DropDownList)item1.FindControl("ddlEditType");
        DropDownList dDept = (DropDownList)item1.FindControl("ddlEditDeptCode");
        TextBox txtDepart = (TextBox)item1.FindControl("txtEditDept");
        DropDownList dlCoor = (DropDownList)item1.FindControl("ddlEditName");
        TextBox tmpID = (TextBox)item1.FindControl("txtEditID");
        TextBox tmpName = (TextBox)item1.FindControl("txtEditName");
        dlCoor.Items.Clear();
        if (tmpType.SelectedIndex != 5)
        {
            txtDepart.Text = dDept.SelectedValue;
        }
        if (tmpType.SelectedIndex == 1)
        {
            deptFlag = FindDeptOrCentre(dDept.SelectedItem.Text.Trim());
            if (deptFlag == "Dept")
            {
                FillCoor(dlCoor, dDept.SelectedItem.Text.Trim());
            }
            else
            {
                FillCoor(dlCoor, "All");
            }

            dlCoor.Visible = true;
            tmpID.Enabled = false;
            tmpName.Visible = false;
        }
        else
        {
            dlCoor.Visible = false;
            tmpID.Enabled = true;
            tmpName.Visible = true;
        }
    }
    protected void ddlEditCoor_SelectedIndexChanged(object sender, EventArgs e)
    {
        DropDownList dList = (DropDownList)sender;
        ListViewItem item1 = (ListViewItem)dList.NamingContainer;
        DropDownList tmpName = (DropDownList)item1.FindControl("ddlEditName");
        TextBox tmpTxtName = (TextBox)item1.FindControl("txtEditName");
        TextBox tmpID = (TextBox)item1.FindControl("txtEditID");
        tmpTxtName.Text = "";
        tmpID.Text = "";
        tmpID.Visible = true;
        tmpID.Text = FindInstID(tmpName.SelectedItem.Value.Trim());
    }
    protected void lvInventor_ItemEditing(object sender, ListViewEditEventArgs e)
    {
        lvInventor.EditIndex = e.NewEditIndex;
        lvInventor.DataSource = (DataTable)ViewState["CoInventor"];
        lvInventor.DataBind();
    }
    protected void lvInventor_ItemUpdating(object sender, ListViewUpdateEventArgs e)
    {
        string slno;
        int InventorType;
        string txtInventorType="";
        string lblType = "";
        string DeptCode="";
        string DeptOrOrg="";
        string InventorID="";
        string InventorName="";
        string txtInventorName = "";
        DropDownList tmpDdl;
        TextBox tmpTxt;
        Label tmpLbl;
        tmpLbl=lvInventor.Items[e.ItemIndex].FindControl("lblEditSlNo") as Label;
        slno = tmpLbl.Text;
        tmpDdl = lvInventor.Items[e.ItemIndex].FindControl("ddlEditType") as  DropDownList;
        InventorType = tmpDdl.SelectedIndex;
        txtInventorType=tmpDdl.SelectedItem.Text.Trim();
        tmpLbl = lvInventor.Items[e.ItemIndex].FindControl("lblEditType") as Label;
        lblType = tmpLbl.Text;
        if (lblType != "" && (InventorType == -1 || InventorType == 0))
        {
            ClientScript.RegisterStartupScript(this.GetType(), "Request", "<script>alert('Select Inventor Type')</script>");
            return;
        }
        if (InventorType == 1 || InventorType == 2 || InventorType == 3 || InventorType == 4)
        {
            tmpDdl = lvInventor.Items[e.ItemIndex].FindControl("ddlEditDeptCode") as DropDownList;
            DeptCode = tmpDdl.SelectedItem.Text.Trim();
            if (DeptCode == "")
            {
                ClientScript.RegisterStartupScript(GetType(), "Dept Code", "<script>alert('Select Department')</script>");
                return;
            }
        }
        tmpTxt = lvInventor.Items[e.ItemIndex].FindControl("txtEditDept") as TextBox;
        DeptOrOrg = tmpTxt.Text.Trim();
        if (InventorType == 1)
        {
            tmpDdl = lvInventor.Items[e.ItemIndex].FindControl("ddlEditName") as DropDownList;
            if(tmpDdl.SelectedItem.Text.IndexOf("~")!=-1)
                InventorName = tmpDdl.SelectedItem.Text.Substring(tmpDdl.SelectedItem.Text.IndexOf("~")+1).Trim();
            else
                InventorName = tmpDdl.SelectedItem.Text.Trim();
            if (InventorName == "")
            {
                ClientScript.RegisterStartupScript(GetType(), "Inventor Name", "<script>alert('Select Inventor Name')</script>");
                return;
            }
        }
        tmpTxt = lvInventor.Items[e.ItemIndex].FindControl("txtEditName") as TextBox;
        txtInventorName = tmpTxt.Text.Trim();
        tmpTxt = lvInventor.Items[e.ItemIndex].FindControl("txtEditID") as TextBox;
        InventorID = tmpTxt.Text.Trim();

        if (InventorType == 2 || InventorType == 3 || InventorType == 4)
        {
            if (txtInventorName == "")
            {
                ClientScript.RegisterStartupScript(GetType(), "Inventor Name", "<script>alert('Enter Inventor Name')</script>");
                return;
            }
        }
        else if (InventorType == 0 || InventorType == 5 || InventorType == -1)
        {
            if (DeptOrOrg == "")
            {
                ClientScript.RegisterStartupScript(GetType(), "Dept Code", "<script>alert('Enter Organization Name')</script>");
                return;
            }
            if (txtInventorName == "")
            {
                ClientScript.RegisterStartupScript(GetType(), "Inventor Name", "<script>alert('Enter Inventor Name')</script>");
                return;
            }
        }

        DataTable dt = (DataTable)ViewState["CoInventor"];
        foreach (DataRow dr in dt.Rows)
        {
            if (dr.RowState != DataRowState.Deleted)
            {
                if (dr["slno"].ToString() == slno)
                {
                    dr["SlNo"] = slno;
                    if (txtInventorType != "") dr["InventorType"] = txtInventorType; else dr["InventorType"] = DBNull.Value;
                    if (DeptCode.Trim() != "") dr["DeptCode"] = DeptCode; else dr["DeptCode"] = DBNull.Value;
                    UpperCaseWords toUpperWord = new UpperCaseWords();
                    if (DeptOrOrg != "") dr["DeptOrgName"] = toUpperWord.toUpperCase(DeptOrOrg); else dr["DeptOrgName"] = DBNull.Value;
                    if (InventorID != "") dr["InventorID"] = InventorID.ToUpper(); else dr["InventorID"] = DBNull.Value;
                    if (InventorType == 1) dr["InventorName"] = InventorName.ToUpper(); else dr["InventorName"] = txtInventorName.ToUpper();        
                }
            }
        }
        lvInventor.EditIndex = -1;
        lvInventor.DataSource = (DataTable)ViewState["CoInventor"];
        lvInventor.DataBind();
    }

    protected void ddlSource_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlSource.SelectedIndex == 1) ViewProjectList.Visible = true; else ViewProjectList.Visible = false;
    }
    protected void lvProjectList_ItemCreated(object sender, ListViewItemEventArgs e)
    {
        DropDownList ddlType = (DropDownList)e.Item.FindControl("ddlNewProjectType");
        if (ddlType != null)
        {
            ddlType.Items.Add("");
            ddlType.Items.Add("Sponsored");
            ddlType.Items.Add("Consultancy");
        }
    }
    protected void lvProjectList_ItemDataBound(object sender, ListViewItemEventArgs e)
    {
        LinkButton lb = e.Item.FindControl("lbtnInsert") as LinkButton;
        LinkButton lb1 = e.Item.FindControl("lbtnCancel") as LinkButton;
        LinkButton lb2 = e.Item.FindControl("lbtnDelete") as LinkButton;
        if (lb != null)
        {
            ScriptManager.GetCurrent(Page).RegisterPostBackControl(lb);
        }
        if (lb1 != null)
        {
            ScriptManager.GetCurrent(Page).RegisterPostBackControl(lb1);
        }
        if (lb2 != null)
        {
            ScriptManager.GetCurrent(Page).RegisterPostBackControl(lb2);
        }        
        if (lb2 != null)
        {
            if (User.IsInRole("Admin"))
            {
                lb2.Visible = true;
            }
        }
        if (lvProjectList.EditIndex == (e.Item as ListViewDataItem).DataItemIndex)
        {
            DropDownList ddlEditProjectType = (DropDownList)e.Item.FindControl("ddlEditProjectType");
            if (ddlEditProjectType != null)
            {
                ddlEditProjectType.Items.Add("");
                ddlEditProjectType.Items.Add("Sponsored");
                ddlEditProjectType.Items.Add("Consultancy");
            }
            HiddenField hfEditType = (HiddenField)e.Item.FindControl("hfProjectType");
            ddlEditProjectType.Items.FindByText(hfEditType.Value.Trim()).Selected = true;
            ddlEditProjectType_SelectedIndexChanged(ddlEditProjectType, EventArgs.Empty);
            HiddenField hfFinYear = (HiddenField)e.Item.FindControl("hfFinYear");
            DropDownList ddlEditFinYear = (DropDownList)e.Item.FindControl("ddlEditFinYear");
            ddlEditFinYear.Items.FindByText(hfFinYear.Value.Trim()).Selected = true;
            ddlEditFinYear_SelectedIndexChanged(ddlEditFinYear, EventArgs.Empty);
            HiddenField hfProjectNo = (HiddenField)e.Item.FindControl("hfProjectNo");
            DropDownList ddlEditProjectNo = (DropDownList)e.Item.FindControl("ddlEditProjectNo");
            ddlEditProjectNo.Items.FindByText(hfProjectNo.Value.Trim()).Selected = true;            
        }
    }
    protected void ddlEditProjectType_SelectedIndexChanged(object sender, EventArgs e)
    {
        DropDownList dListGet = (DropDownList)sender;
        ListViewItem item1 = (ListViewItem)dListGet.NamingContainer;
        DropDownList ProjectType = (DropDownList)item1.FindControl("ddlEditProjectType");
        if (ProjectType.SelectedIndex != -1 && ProjectType.SelectedIndex != 0)
        {
            DropDownList FinYear = (DropDownList)item1.FindControl("ddlEditFinYear");
            DropDownList ProjectNo = (DropDownList)item1.FindControl("ddlEditProjectNo");
            FinYear.Items.Clear();
            ProjectNo.Items.Clear();
            FillFinancialYear(ProjectType.Text, FinYear);
        }
    }
    protected void ddlEditFinYear_SelectedIndexChanged(object sender, EventArgs e)
    {
        DropDownList dListGet = (DropDownList)sender;
        ListViewItem item1 = (ListViewItem)dListGet.NamingContainer;
        DropDownList FinYear = (DropDownList)item1.FindControl("ddlEditFinYear");
        if (FinYear.SelectedIndex != -1)
        {
            DropDownList ProjectType = (DropDownList)item1.FindControl("ddlEditProjectType");
            DropDownList ProjectNo = (DropDownList)item1.FindControl("ddlEditProjectNo");
            ProjectNo.Items.Clear();
            FillProjectNo(ProjectType.Text, FinYear.Text, ProjectNo);
        }
    }
    protected void ddlEditProjectNo_SelectedIndexChanged(object sender, EventArgs e)
    {
        DropDownList dListGet = (DropDownList)sender;
        ListViewItem item1 = (ListViewItem)dListGet.NamingContainer;
        DropDownList ProjectNo = (DropDownList)item1.FindControl("ddlEditProjectNo");
        if (ProjectNo.SelectedIndex != -1 && ProjectNo.SelectedIndex != 0)
        {
            DropDownList ProjectType = (DropDownList)item1.FindControl("ddlEditProjectType");
            TextBox txtAgency = (TextBox)item1.FindControl("txtEditAgency");
            TextBox txtProjectTitle = (TextBox)item1.FindControl("txtEditProjectTitle");
            txtAgency.Text = "";
            txtProjectTitle.Text = "";
            FillProjectDetails(ProjectType.Text, ProjectNo.Text.Trim(), txtAgency, txtProjectTitle);
        }
    }
    protected void lvProjectList_ItemInserting(object sender, ListViewInsertEventArgs e)
    {
        string slno = Convert.ToString(lvProjectList.Items.Count + 1);

        DropDownList ddlProjectType = e.Item.FindControl("ddlNewProjectType") as DropDownList;
        if (ddlProjectType.SelectedIndex == -1 || ddlProjectType.SelectedIndex == 0)
        {
            ClientScript.RegisterStartupScript(this.GetType(), "Project Type", "<script>alert('Select Project Type')</script>");
            return;
        }
        DropDownList ddlYear = e.Item.FindControl("ddlFinYear") as DropDownList;
        DropDownList ddlProjectNo = e.Item.FindControl("ddlNewProjectNo") as DropDownList;
        TextBox txtAgency = e.Item.FindControl("txtAgency") as TextBox;
        TextBox txtProjectTitle = e.Item.FindControl("txtProjectTitle") as TextBox;

        if (ddlYear.SelectedIndex == -1 || ddlYear.SelectedIndex == 0)
        {
            ClientScript.RegisterStartupScript(GetType(), "Financial Year", "<script>alert('Select Financial Year')</script>");
            return;
        }
        if (ddlProjectNo.SelectedIndex == -1 || ddlProjectNo.SelectedIndex == 0)
        {
            ClientScript.RegisterStartupScript(GetType(), "Project Number", "<script>alert('Select Project Number')</script>");
            return;
        }
        if (txtAgency.Text.Trim() == "")
        {
            ClientScript.RegisterStartupScript(GetType(), "Agency Name", "<script>alert('Enter Agency Name')</script>");
            return;
        }
        if (txtProjectTitle.Text.Trim() == "")
        {
            ClientScript.RegisterStartupScript(GetType(), "Project Title", "<script>alert('Enter Project Title')</script>");
            return;
        }

        DataTable dt = (DataTable)ViewState["InventionSource"];
        DataRow dr = dt.NewRow();
        dr["SlNo"] = slno;
        dr["ProjectType"] = ddlProjectType.SelectedItem.Text.Trim();
        dr["FinancialYear"] = ddlYear.SelectedItem.Text.Trim();
        dr["ProjectNumber"] = ddlProjectNo.SelectedItem.Text.Trim();
        dr["FundingAgency"] = txtAgency.Text.Trim();
        dr["ProjectTitle"] = txtProjectTitle.Text.Trim();
        dt.Rows.Add(dr);
        lvProjectList.DataSource = (DataTable)ViewState["InventionSource"];
        lvProjectList.DataBind();
    }
    protected void lvProjectList_ItemUpdating(object sender, ListViewUpdateEventArgs e)
    {
        Label editSlNo = lvProjectList.Items[e.ItemIndex].FindControl("lblEditProjectSlNo") as Label;
        string SlNo = editSlNo.Text.Trim();
        DropDownList ddlProjectType = lvProjectList.Items[e.ItemIndex].FindControl("ddlEditProjectType") as DropDownList;
        if (ddlProjectType.SelectedIndex == -1 || ddlProjectType.SelectedIndex == 0)
        {
            ClientScript.RegisterStartupScript(this.GetType(), "Project Type", "<script>alert('Select Project Type')</script>");
            return;
        }
        DropDownList ddlYear = lvProjectList.Items[e.ItemIndex].FindControl("ddlEditFinYear") as DropDownList;
        DropDownList ddlProjectNo = lvProjectList.Items[e.ItemIndex].FindControl("ddlEditProjectNo") as DropDownList;
        TextBox txtAgency = lvProjectList.Items[e.ItemIndex].FindControl("txtEditAgency") as TextBox;
        TextBox txtProjectTitle = lvProjectList.Items[e.ItemIndex].FindControl("txtEditProjectTitle") as TextBox;
        if (ddlYear.SelectedIndex == -1 || ddlYear.SelectedIndex == 0)
        {
            ClientScript.RegisterStartupScript(GetType(), "Financial Year", "<script>alert('Select Financial Year')</script>");
            return;
        }
        if (ddlProjectNo.SelectedIndex == -1 || ddlProjectNo.SelectedIndex == 0)
        {
            ClientScript.RegisterStartupScript(GetType(), "Project Number", "<script>alert('Select Project Number')</script>");
            return;
        }
        if (txtAgency.Text.Trim() == "")
        {
            ClientScript.RegisterStartupScript(GetType(), "Agency Name", "<script>alert('Enter Agency Name')</script>");
            return;
        }
        if (txtProjectTitle.Text.Trim() == "")
        {
            ClientScript.RegisterStartupScript(GetType(), "Project Title", "<script>alert('Enter Project Title')</script>");
            return;
        }
        DataTable dt = (DataTable)ViewState["InventionSource"];
        foreach (DataRow dr in dt.Rows)
        {
            if (dr.RowState != DataRowState.Deleted)
            {
                if (dr["SlNo"].ToString() == SlNo)
                {
                    dr["ProjectType"] = ddlProjectType.SelectedItem.Text.Trim();
                    dr["FinancialYear"] = ddlYear.SelectedItem.Text.Trim();
                    dr["ProjectNumber"] = ddlProjectNo.SelectedItem.Text.Trim();
                    dr["FundingAgency"] = txtAgency.Text.Trim();
                    dr["ProjectTitle"] = txtProjectTitle.Text.Trim();                    
                }
            }
        }
        lvProjectList.EditIndex = -1;
        lvProjectList.DataSource = (DataTable)ViewState["InventionSource"];
        lvProjectList.DataBind();

    }
    protected void lvProjectList_ItemCanceling(object sender, ListViewCancelEventArgs e)
    {
        System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-GB");
        lvProjectList.EditIndex = -1;
        lvProjectList.DataSource = (DataTable)ViewState["InventionSource"];
        lvProjectList.DataBind();
    }
    protected void lvProjectList_ItemDeleting(object sender, ListViewDeleteEventArgs e)
    {
        System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-GB");
        Label lbl = lvProjectList.Items[e.ItemIndex].FindControl("lblProjectSlNo") as Label;
        DataTable dt = (DataTable)ViewState["InventionSource"];
        int cnt = 0;
        foreach (DataRow dr in dt.Rows)
        {
            if (dr.RowState != DataRowState.Deleted)
            {
                if (dr["SlNo"].ToString() == lbl.Text.Trim())
                {
                    dr.Delete();
                    break;
                }
            }
        }
        for (int i = 0; i <= dt.Rows.Count - 1; i++)
        {
            if (dt.Rows[i].RowState != DataRowState.Deleted)
            {
                cnt += 1;
                dt.Rows[i][0] = cnt;
            }
        }
        lvProjectList.DataSource = (DataTable)ViewState["InventionSource"];
        lvProjectList.DataBind();

        if (lvProjectList.Items.Count <= 0)
        {
            ddlSource.Enabled = true;
            ddlSource.SelectedIndex = 0;
            ddlSource_SelectedIndexChanged(ddlSource, EventArgs.Empty);
        }
    }
    protected void ddlNewProjectType_SelectedIndexChanged(object sender, EventArgs e)
    {
        DropDownList dListGet = (DropDownList)sender;
        ListViewItem item1 = (ListViewItem)dListGet.NamingContainer;
        DropDownList ProjectType = (DropDownList)item1.FindControl("ddlNewProjectType");
        if (ProjectType.SelectedIndex != -1 && ProjectType.SelectedIndex != 0)
        {
            DropDownList FinYear = (DropDownList)item1.FindControl("ddlFinYear");
            DropDownList ProjectNo = (DropDownList)item1.FindControl("ddlNewProjectNo");
            TextBox txtAgency = (TextBox)item1.FindControl("txtAgency");
            TextBox txtProjectTitle = (TextBox)item1.FindControl("txtProjectTitle");
            FinYear.Items.Clear();
            ProjectNo.Items.Clear();
            txtAgency.Text = "";
            txtProjectTitle.Text = "";
            FillFinancialYear(ProjectType.Text, FinYear);
        }
    }
    protected void FillFinancialYear(string ProjectType, DropDownList FinYear)
    {
        con.ConnectionString = ConfigurationManager.ConnectionStrings["ICSR2KSCN"].ConnectionString;
        SqlCommand cmd = new SqlCommand();
        string sql = "";
        if (ProjectType == "Sponsored")
            sql = "select [year] as [FinancialYear] from pmaster group by [year] order by [year]";
        else if (ProjectType == "Consultancy")
            sql = "select [n_year] as [FinancialYear] from pcmaster group by [n_year] order by [n_year]";
        else return;
        cmd.CommandText = sql;
        cmd.Connection = con;
        cmd.CommandType = CommandType.Text;
        con.Open();
        SqlDataReader dr;
        dr = cmd.ExecuteReader();
        FinYear.DataTextField = "FinancialYear";
        FinYear.DataValueField = "FinancialYear";
        FinYear.DataSource = dr;
        FinYear.DataBind();
        FinYear.Items.Insert(0, "");
        con.Close();
    }
    protected void ddlFinYear_SelectedIndexChanged(object sender, EventArgs e)
    {
        DropDownList dListGet = (DropDownList)sender;
        ListViewItem item1 = (ListViewItem)dListGet.NamingContainer;
        DropDownList FinYear = (DropDownList)item1.FindControl("ddlFinYear");
        if (FinYear.SelectedIndex != -1)
        {
            DropDownList ProjectType = (DropDownList)item1.FindControl("ddlNewProjectType");
            DropDownList ProjectNo = (DropDownList)item1.FindControl("ddlNewProjectNo");
            TextBox txtAgency = (TextBox)item1.FindControl("txtAgency");
            TextBox txtProjectTitle = (TextBox)item1.FindControl("txtProjectTitle");
            ProjectNo.Items.Clear();
            txtAgency.Text = "";
            txtProjectTitle.Text = "";
            FillProjectNo(ProjectType.Text, FinYear.Text, ProjectNo);
        }
    }
    protected void FillProjectNo(string ProjectType, string FinYear, DropDownList ProjectNo)
    {
        con.ConnectionString = ConfigurationManager.ConnectionStrings["ICSR2KSCN"].ConnectionString;
        SqlCommand cmd = new SqlCommand();
        string sql = "";
        if (ProjectType == "Sponsored")
            sql = "Select Aprlno from pmaster where proj_rem like 'new' and projectStatus is null and [year]='" + FinYear.Trim() + "' order by aprlno";
        else if (ProjectType == "Consultancy")
            sql = "Select Aprlno from pcmaster where projectStatus is null and [n_year]='" + FinYear.Trim() + "' order by aprlno";
        else return;
        SqlDataReader dr;
        cmd.CommandText = sql;
        cmd.Connection = con;
        cmd.CommandType = CommandType.Text;
        con.Open();
        dr = cmd.ExecuteReader();
        ProjectNo.DataTextField = "Aprlno";
        ProjectNo.DataValueField = "Aprlno";
        ProjectNo.DataSource = dr;
        ProjectNo.DataBind();
        ProjectNo.Items.Insert(0, "");
        con.Close();
    }
    protected void ddlNewProjectNo_SelectedIndexChanged(object sender, EventArgs e)
    {
        DropDownList dListGet = (DropDownList)sender;
        ListViewItem item1 = (ListViewItem)dListGet.NamingContainer;
        DropDownList ProjectNo = (DropDownList)item1.FindControl("ddlNewProjectNo");
        if (ProjectNo.SelectedIndex != -1 && ProjectNo.SelectedIndex != 0)
        {
            DropDownList ProjectType = (DropDownList)item1.FindControl("ddlNewProjectType");
            TextBox txtAgency = (TextBox)item1.FindControl("txtAgency");
            TextBox txtProjectTitle = (TextBox)item1.FindControl("txtProjectTitle");
            txtAgency.Text = "";
            txtProjectTitle.Text = "";
            FillProjectDetails(ProjectType.Text, ProjectNo.Text.Trim(), txtAgency, txtProjectTitle);
        }
    }
    protected void FillProjectDetails(string ProjectType, string ProjectNo, TextBox txtAgency, TextBox txtProjectTitle)
    {
        con.ConnectionString = ConfigurationManager.ConnectionStrings["ICSR2KSCN"].ConnectionString;
        SqlCommand cmd = new SqlCommand();
        string sql = "";
        if (ProjectType == "Sponsored")
            sql = "Select Spon_Agen as Agency, Title from pmaster where proj_rem like 'new' and Aprlno='" + ProjectNo.Trim() + "'";
        else if (ProjectType == "Consultancy")
            sql = "Select SCOM as Agency, Title from pcmaster where Aprlno ='" + ProjectNo.Trim() + "'";
        else return;
        SqlDataReader dr;
        cmd.CommandText = sql;
        cmd.Connection = con;
        cmd.CommandType = CommandType.Text;
        con.Open();
        dr = cmd.ExecuteReader();
        if (dr.Read())
        {
            txtAgency.Text = dr.GetString(0);
            txtProjectTitle.Text = dr.GetString(1);
        }
        con.Close();
    }
    protected void lvProjectList_ItemEditing(object sender, ListViewEditEventArgs e)
    {
        lvProjectList.EditIndex = e.NewEditIndex;
        lvProjectList.DataSource = (DataTable)ViewState["InventionSource"];
        lvProjectList.DataBind();        
    }
}
