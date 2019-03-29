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
using System.Collections.Generic;
using System.Web.Services;
using System.Web.Script.Services;
using System.IO;

public partial class FileCreation : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection();
   // SqlConnection cn = new SqlConnection();
    SqlConnection cnp = new SqlConnection();
    string deptFlag;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!User.IsInRole("Intern"))
        {
            if (User.Identity.IsAuthenticated)
            {
                if (!this.IsPostBack)
                {
                    this.Title = "New File Creation";

                    cnp.ConnectionString = ConfigurationManager.ConnectionStrings["PATENTCN"].ConnectionString;
                    using (cnp)
                    {
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
                            ListItem li = new ListItem();
                            li.Text = dr.GetString(0);
                            li.Value = dr.GetString(0);
                            ddlIPR.Items.Add(li);
                        }
                        dr.Close();
                        ddlIFiling.Items.Clear();
                        ddlIFiling.Items.Add("");
                        ddlIFiling.Items.Add(new ListItem("IIT Madras", "IIT Madras"));
                        ddlIFiling.Items.Add(new ListItem("ICSR", "ICSR"));
                        ddlIFiling.Items.Add(new ListItem("PCF", "PCF"));
                        ddlIFiling.Items.Add(new ListItem("PARTNER", "PARTNER"));

                        txtFileNo.Text = AssignFileNo();

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
                        dr.Close();
                        // cnp.Close();

                        //cnp.Open();

                        ddlSource.Items.Add("No");
                        ddlSource.Items.Add("Yes");
                        ddlSource.SelectedIndex = 0;

                        //test ds1 = new test();
                        Inventor ds = new Inventor();
                        lvInventor.DataSource = ds.Tables["CoInventors"];
                        lvInventor.DataBind();
                        ViewState["CoInventor"] = ds.Tables["CoInventors"];

                        dsFileCreation dsSource = new dsFileCreation();
                        lvProjectList.DataSource = dsSource.Tables["dtInventionSource"];
                        lvProjectList.DataBind();
                        ViewState["InventionSource"] = dsSource.Tables["dtInventionSource"];
                        deptFlag = "";

                    }

                }
            }
            else
            {
                Response.Redirect("Login.aspx");
            }
            imgBtnInsert.Visible = (!User.IsInRole("View"));
        }
        else
        {
            Server.Transfer("Unauthorized.aspx");
        }
    }
    protected string AssignFileNo()
    {
        con.ConnectionString = ConfigurationManager.ConnectionStrings["PATENTCN"].ConnectionString;
        SqlCommand cmd3 = new SqlCommand();
        string sql3 = "select max(convert(int,fileno))+1 from patdetails where fileno not in (select fileno from patdetails where fileno like '%[a-zA-Z]%')";
        cmd3.CommandText = sql3;
        cmd3.Connection = con;
        cmd3.CommandType = CommandType.Text;
        con.Open();
        string fileno = cmd3.ExecuteScalar().ToString();
        con.Close();
        return fileno;
    }

    protected void lvInventor_ItemCreated(object sender, ListViewItemEventArgs e)
    {
        DropDownList ddlType = (DropDownList)e.Item.FindControl("ddlNewType");
        if (ddlType != null )
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
            
            //ddlType.DataTextField = "itemList";
            //ddlType.DataValueField = "itemList";
            //ddlType.DataSource = dr;
            //ddlType.DataBind();
           ddlType.Items.Insert(0, new ListItem("", ""));
           con.Close();
        }
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
        string sql = "SELECT CR_CODE,INSTID FROM COORCOD WHERE CR_CODE LIKE '" + coorCode + "'";
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
        string tmpVal = cmd.ExecuteScalar().ToString();
        con.Close();
        return tmpVal;
    }
    protected void FillCoor(DropDownList dlCoor, string tmpDeptCode)
    {
        try
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
        catch(Exception ex)
        {
            con.Close();
        }
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
    protected void ddlType1_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlDept1.Items.Clear();
        txtDept1.Text = "";
        ddlCoor1.Items.Clear();
        txtCoor1.Text = "";
        txtCoorID.Text = "";
        txtDept1.Width = 175;
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
            txtDept1.Width = 250;
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
        txtDept1.Width = 175;
        ddlDept1.Items.Clear();
        ddlDept1.Enabled = true;
        txtRequest.Text = "";
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
        txtFileNo.Text = AssignFileNo();
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
    protected void lvInventor_ItemDeleting(object sender, ListViewDeleteEventArgs e)
    {
        System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-GB");
        Label lbl = lvInventor.Items[e.ItemIndex].FindControl("lblSlNo") as Label;
        DataTable dt = (DataTable)ViewState["CoInventor"];
        int cnt = 0;
        foreach (DataRow dr in dt.Rows)
        {
            cnt += 1;
            if (dr["SlNo"].ToString() == lbl.Text.Trim())
            {
                dr.Delete();
                break;
            }
        }
        for (int i = cnt - 1; i <= dt.Rows.Count - 1; i++)
        {
            dt.Rows[i][0] = i + 1;
        }
        lvInventor.DataSource = (DataTable)ViewState["CoInventor"];
        lvInventor.DataBind();
    }
    protected void imgBtnInsert_Click(object sender, ImageClickEventArgs e)
    {

        string filename1 = FileUpload1.FileName;
        //string filename2 = FileUpload2.FileName;
        //string filename3 = FileUpload3.FileName;

        if (string.IsNullOrEmpty(txtFileNo.Text))
        {
            ClientScript.RegisterStartupScript(GetType(), "Inventor Name", "<script>alert('File No Can not be Empty')</script>");
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
        System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-GB");
        cnp.ConnectionString = ConfigurationManager.ConnectionStrings["PATENTCN"].ConnectionString;

        SqlCommand cmd1 = new SqlCommand();
        string sql1 = "select fileno from patdetails where fileno like '" + txtFileNo.Text.Trim() + "'";
        cmd1.CommandText = sql1;
        cmd1.CommandType = CommandType.Text;
        cmd1.Connection = cnp;
        cnp.Open();
        SqlDataReader dr;
        dr = cmd1.ExecuteReader();
        if (dr.Read())
        {
            ClientScript.RegisterStartupScript(GetType(), "Inventor Name", "<script>alert('This File No. is exist in table')</script>");
            dr.Close();
            return;
        }
        dr.Close();
        SqlTransaction Trans;
        Trans = cnp.BeginTransaction();
        try
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Transaction = Trans;
            cmd.CommandText = "fileCreation";
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
            if (ddlType1.SelectedItem.Text != "Non IITM")
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
                    if (ddlCoor1.SelectedItem.Text.IndexOf("~") != -1)
                        pm10.Value = ddlCoor1.SelectedItem.Text.Substring(ddlCoor1.SelectedItem.Text.IndexOf("~") + 1).Trim();
                    else
                        pm10.Value = ddlCoor1.SelectedItem.Text.Trim();
                }
            else
                if (txtCoor1.Text.Trim() == "") pm10.Value = DBNull.Value; else pm10.Value = txtCoor1.Text.Trim().ToUpper();
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
                    ClientScript.RegisterStartupScript(GetType(), "Inventor Name", "<script>alert('Verify Request Date')</script>");
                    return;
                }
            }
            pm12.DbType = DbType.String;
            pm12.Direction = ParameterDirection.Input;

            SqlParameter pm13 = new SqlParameter();
            pm13.ParameterName = "@EntryDt";
            pm13.SourceColumn = "EntryDt";
            pm13.Value = DateTime.Now.ToShortDateString();
            pm13.DbType = DbType.String;
            pm13.Direction = ParameterDirection.Input;

            SqlParameter pm14 = new SqlParameter();
            pm14.ParameterName = "@Status";
            pm14.SourceColumn = "Status";
            pm14.Value = "Pending - Internal";
            pm14.DbType = DbType.String;
            pm14.Direction = ParameterDirection.Input;

            SqlParameter pm15 = new SqlParameter();
            pm15.ParameterName = "@Commercial";
            pm15.SourceColumn = "Commercial";
            pm15.Value = "IITM";
            pm15.DbType = DbType.String;
            pm15.Direction = ParameterDirection.Input;

            SqlParameter pm16 = new SqlParameter();
            pm16.ParameterName = "@UserName";
            pm16.SourceColumn = "UserName";
            pm16.Value = Membership.GetUser().UserName.ToString();
            pm16.DbType = DbType.String;
            pm16.Direction = ParameterDirection.Input;

            SqlParameter pm17 = new SqlParameter();
            pm17.ParameterName = "@Abstract";
            pm17.SourceColumn = "Abstract";
            pm17.Value = "Not Yet";
            pm17.DbType = DbType.String;
            pm17.Direction = ParameterDirection.Input;

            SqlParameter pm18 = new SqlParameter();
            pm18.ParameterName = "@Commercialized";
            pm18.SourceColumn = "Commercialized";
            pm18.Value = "Not Yet";
            pm18.DbType = DbType.String;
            pm18.Direction = ParameterDirection.Input;

            SqlParameter pm19 = new SqlParameter();
            pm19.ParameterName = "@InventionSourceFromProject";
            pm19.SourceColumn = "InventionSourceFromProject";
            pm19.Value = ddlSource.SelectedIndex;
            pm19.SqlDbType = SqlDbType.Bit;
            pm19.Direction = ParameterDirection.Input;

            SqlParameter pm20 = new SqlParameter();
            pm20.ParameterName = "@Comments";
            pm20.SourceColumn = "Comments";
            if (txtComments.Text.Trim() == "") pm20.Value = DBNull.Value; else pm20.Value = txtComments.Text.Trim();
            pm20.SqlDbType = SqlDbType.VarChar;
            pm20.Direction = ParameterDirection.Input;

            SqlParameter pm21 = new SqlParameter();
            pm21.ParameterName = "@Specification";
            pm21.SourceColumn = "Specification";
            if (txtWriteUp.Text.Trim() == "") pm21.Value = DBNull.Value; else pm21.Value = txtWriteUp.Text.Trim();
            pm21.SqlDbType = SqlDbType.NVarChar;
            pm21.Direction = ParameterDirection.Input;

            SqlParameter pm22 = new SqlParameter();
            pm22.ParameterName = "@Direct_Strategy";
            pm22.SourceColumn = "Direct_Strategy";
            pm22.Value = dropstrategy.SelectedValue.ToString();
            pm22.SqlDbType = SqlDbType.NVarChar;
            pm22.Direction = ParameterDirection.Input;


            SqlParameter pm23 = new SqlParameter();
            pm23.ParameterName = "@Appln_Post";
            pm23.SourceColumn = "Appln_Post";
            pm23.Value = DropPost.SelectedValue.ToString();
            pm23.SqlDbType = SqlDbType.NVarChar;
            pm23.Direction = ParameterDirection.Input;

            SqlParameter pm24 = new SqlParameter();
            pm24.ParameterName = "@Post_Dated";
            pm24.SourceColumn = "Post_Dated";
            if (txtPost.Text.Trim() == "")
                pm24.Value = DBNull.Value;
            else
            {
                DateTime Request;
                if (DateTime.TryParse(txtPost.Text.Trim(), out Request))
                    pm24.Value = txtPost.Text.Trim();
                else
                {
                    ClientScript.RegisterStartupScript(GetType(), "Inventor Name", "<script>alert('Verify Post Dated Date')</script>");
                    return;
                }
            }
            pm24.DbType = DbType.String;
            pm24.Direction = ParameterDirection.Input;


            SqlParameter pm25 = new SqlParameter();
            pm25.ParameterName = "@Abstract_TT";
            pm25.SourceColumn = "Abstract_TT";
            pm25.Value = txtAbstract.Text;
            pm25.SqlDbType = SqlDbType.NVarChar;
            pm25.Direction = ParameterDirection.Input;

            SqlParameter pm26 = new SqlParameter();
            pm26.ParameterName = "@Design";
            pm26.SourceColumn = "Design";
            pm26.Value = filename1.ToString();
            pm26.SqlDbType = SqlDbType.NVarChar;
            pm26.Direction = ParameterDirection.Input;

            SqlParameter pm27 = new SqlParameter();
            pm27.ParameterName = "@OtherIP";
            pm27.SourceColumn = "OtherIP";
            pm27.Value = "NULL";
            pm27.SqlDbType = SqlDbType.NVarChar;
            pm27.Direction = ParameterDirection.Input;

            SqlParameter pm28 = new SqlParameter();
            pm28.ParameterName = "@Solution_Packages";
            pm28.SourceColumn = "Solution_Packages";
            pm28.Value = "NULL";
            pm28.SqlDbType = SqlDbType.NVarChar;
            pm28.Direction = ParameterDirection.Input;

            SqlParameter pm29 = new SqlParameter();
            pm29.ParameterName = "@Software";
            pm29.SourceColumn = "@Software";
            pm29.Value = "NULL";
            pm29.SqlDbType = SqlDbType.NVarChar;
            pm29.Direction = ParameterDirection.Input;

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
            cmd.Parameters.Add(pm13);
            cmd.Parameters.Add(pm14);
            cmd.Parameters.Add(pm15);
            cmd.Parameters.Add(pm16);
            cmd.Parameters.Add(pm17);
            cmd.Parameters.Add(pm18);
            cmd.Parameters.Add(pm19);
            cmd.Parameters.Add(pm20);
            cmd.Parameters.Add(pm21);
            cmd.Parameters.Add(pm22);
            cmd.Parameters.Add(pm23);
            cmd.Parameters.Add(pm24);
            cmd.Parameters.Add(pm25);
            cmd.Parameters.Add(pm26);
            cmd.Parameters.Add(pm27);
            cmd.Parameters.Add(pm28);
            cmd.Parameters.Add(pm29);
            cmd.ExecuteNonQuery();





            DataTable dt = (DataTable)ViewState["CoInventor"];
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dRow in dt.Rows)
                {
                    SqlCommand cmd2 = new SqlCommand();
                    cmd2.Transaction = Trans;
                    cmd2.CommandText = "CoInventor";
                    cmd2.CommandType = CommandType.StoredProcedure;
                    cmd2.Connection = cnp;

                    SqlParameter bpm1 = new SqlParameter();
                    bpm1.ParameterName = "@EntryDt";
                    bpm1.SourceColumn = "EntryDt";
                    bpm1.Value = DateTime.Now.ToShortDateString();
                    bpm1.DbType = DbType.String;
                    bpm1.Direction = ParameterDirection.Input;

                    SqlParameter bpm2 = new SqlParameter();
                    bpm2.ParameterName = "@SlNo";
                    bpm2.SourceColumn = "SlNo";
                    bpm2.Value = dRow["SlNo"].ToString();
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
                    bpm4.Value = dRow["InventorType"].ToString();
                    bpm4.DbType = DbType.String;
                    bpm4.Direction = ParameterDirection.Input;

                    SqlParameter bpm5 = new SqlParameter();
                    bpm5.ParameterName = "@InventorName";
                    bpm5.SourceColumn = "InventorName";
                    bpm5.Value = dRow["InventorName"].ToString();
                    bpm5.DbType = DbType.String;
                    bpm5.Direction = ParameterDirection.Input;

                    SqlParameter bpm6 = new SqlParameter();
                    bpm6.ParameterName = "@DeptCode";
                    bpm6.SourceColumn = "DeptCode";
                    if (dRow["DeptCode"].ToString() == "") bpm6.Value = DBNull.Value; else bpm6.Value = dRow["DeptCode"].ToString();
                    bpm6.DbType = DbType.String;
                    bpm6.Direction = ParameterDirection.Input;

                    SqlParameter bpm7 = new SqlParameter();
                    bpm7.ParameterName = "@DeptOrOrganisation";
                    bpm7.SourceColumn = "DeptOrOrganisation";
                    bpm7.Value = dRow["DeptOrgName"].ToString();
                    bpm7.DbType = DbType.String;
                    bpm7.Direction = ParameterDirection.Input;

                    SqlParameter bpm8 = new SqlParameter();
                    bpm8.ParameterName = "@InventorID";
                    bpm8.SourceColumn = "InventorID";
                    if (dRow["InventorID"].ToString() == "") bpm8.Value = DBNull.Value; else bpm8.Value = dRow["InventorID"].ToString();
                    bpm8.DbType = DbType.String;
                    bpm8.Direction = ParameterDirection.Input;


                    cmd2.Parameters.Add(bpm1);
                    cmd2.Parameters.Add(bpm2);
                    cmd2.Parameters.Add(bpm3);
                    cmd2.Parameters.Add(bpm4);
                    cmd2.Parameters.Add(bpm5);
                    cmd2.Parameters.Add(bpm6);
                    cmd2.Parameters.Add(bpm7);
                    cmd2.Parameters.Add(bpm8);

                    cmd2.ExecuteNonQuery();
                }
            }
            if (dtSource.Rows.Count > 0)
            {
                foreach (DataRow dRow in dtSource.Rows)
                {
                    SqlCommand cmd3 = new SqlCommand();
                    cmd3.Transaction = Trans;
                    cmd3.CommandText = "NewInventionSource";
                    cmd3.CommandType = CommandType.StoredProcedure;
                    cmd3.Connection = cnp;

                    SqlParameter spm1 = new SqlParameter();
                    spm1.ParameterName = "@EntryDt";
                    spm1.SourceColumn = "EntryDt";
                    spm1.Value = DateTime.Now.ToShortDateString();
                    spm1.SqlDbType = SqlDbType.VarChar;
                    spm1.Direction = ParameterDirection.Input;

                    SqlParameter spm2 = new SqlParameter();
                    spm2.ParameterName = "@SlNo";
                    spm2.SourceColumn = "SlNo";
                    spm2.Value = dRow["SlNo"].ToString();
                    spm2.SqlDbType = SqlDbType.Int;
                    spm2.Direction = ParameterDirection.Input;

                    SqlParameter spm3 = new SqlParameter();
                    spm3.ParameterName = "@FileNo";
                    spm3.SourceColumn = "FileNo";
                    spm3.Value = txtFileNo.Text.Trim();
                    spm3.SqlDbType = SqlDbType.VarChar;
                    spm3.Direction = ParameterDirection.Input;

                    SqlParameter spm4 = new SqlParameter();
                    spm4.ParameterName = "@ProjectType";
                    spm4.SourceColumn = "ProjectType";
                    spm4.Value = dRow["ProjectType"].ToString();
                    spm4.SqlDbType = SqlDbType.VarChar;
                    spm4.Direction = ParameterDirection.Input;

                    SqlParameter spm5 = new SqlParameter();
                    spm5.ParameterName = "@ProjectNumber";
                    spm5.SourceColumn = "ProjectNumber";
                    spm5.Value = dRow["ProjectNumber"].ToString();
                    spm5.SqlDbType = SqlDbType.VarChar;
                    spm5.Direction = ParameterDirection.Input;

                    SqlParameter spm6 = new SqlParameter();
                    spm6.ParameterName = "@FundingAgency";
                    spm6.SourceColumn = "FundingAgency";
                    spm6.Value = dRow["FundingAgency"].ToString();
                    spm6.SqlDbType = SqlDbType.VarChar;
                    spm6.Direction = ParameterDirection.Input;

                    SqlParameter spm7 = new SqlParameter();
                    spm7.ParameterName = "@ProjectTitle";
                    spm7.SourceColumn = "ProjectTitle";
                    spm7.Value = dRow["ProjectTitle"].ToString();
                    spm7.SqlDbType = SqlDbType.VarChar;
                    spm7.Direction = ParameterDirection.Input;

                    SqlParameter spm8 = new SqlParameter();
                    spm8.ParameterName = "@FinancialYear";
                    spm8.SourceColumn = "FinancialYear";
                    spm8.Value = dRow["FinancialYear"].ToString();
                    spm8.SqlDbType = SqlDbType.VarChar;
                    spm8.Direction = ParameterDirection.Input;

                    cmd3.Parameters.Add(spm1);
                    cmd3.Parameters.Add(spm2);
                    cmd3.Parameters.Add(spm3);
                    cmd3.Parameters.Add(spm4);
                    cmd3.Parameters.Add(spm5);
                    cmd3.Parameters.Add(spm6);
                    cmd3.Parameters.Add(spm7);
                    cmd3.Parameters.Add(spm8);
                    cmd3.ExecuteNonQuery();
                }
            }

            Trans.Commit();
            ClientScript.RegisterStartupScript(GetType(), "success", "<script>alert('This Record successfully Added')</script>");
            cnp.Close();
        }
        catch (Exception ex)
        {
            Trans.Rollback();
            ClientScript.RegisterStartupScript(GetType(), "Error", "<script>alert('" + ex.Message.ToString() + "')</script>");
            cnp.Close();
            return;
        }
        if (FileUpload1.FileName != null)
        {
            try
            {
                HttpPostedFile file1 = FileUpload1.PostedFile;
                Int32 fileLength = file1.ContentLength;
                string fileName = file1.FileName;
                byte[] buffer = new byte[fileLength];
                file1.InputStream.Read(buffer, 0, fileLength);
                FileStream newFile;
                string strPath = @"F:\PatentDocument\Design\" + txtFileNo.Text + @"\";
                if (!Directory.Exists(strPath))
                {
                    Directory.CreateDirectory(strPath);
                }
                if (!File.Exists(strPath + filename1))
                {
                    newFile = File.Open(strPath + filename1, FileMode.Create);
                    newFile.Write(buffer, 0, buffer.Length);
                    newFile.Close();
                    Trans.Rollback();
                }
                else
                {
                    ClientScript.RegisterStartupScript(GetType(), "Information", "<script>alert('This File exists in this Folder')</script>");
                    Trans.Rollback();
                    con.Close();
                    return;
                }
            }
            catch (Exception ex)
            {
                ClientScript.RegisterStartupScript(GetType(), "Error", "<script>alert('" + ex.Message.ToString() + "')</script>");

                con.Close();
                return;
            }
        }
        //if (FileUpload2.FileName != null)
        //{
        //    try
        //    {
        //        HttpPostedFile file2 = FileUpload2.PostedFile;
        //        Int32 fileLength = file2.ContentLength;
        //        string fileName = file2.FileName;
        //        byte[] buffer = new byte[fileLength];
        //        file2.InputStream.Read(buffer, 0, fileLength);
        //        FileStream newFile;
        //        string strPath1 = @"F:\PatentDocument\OtherIP\" + txtFileNo.Text + @"\";
        //        if (!Directory.Exists(strPath1))
        //        {
        //            Directory.CreateDirectory(strPath1);
        //        }
        //        if (!File.Exists(strPath1 + filename2))
        //        {
        //            newFile = File.Open(strPath1 + filename2, FileMode.Create);
        //            newFile.Write(buffer, 0, buffer.Length);
        //            newFile.Close();
        //            Trans.Rollback();
        //        }
        //        else
        //        {
        //            ClientScript.RegisterStartupScript(GetType(), "Information", "<script>alert('This File exists in this Folder')</script>");
        //            Trans.Rollback();
        //            con.Close();
        //            return;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        ClientScript.RegisterStartupScript(GetType(), "Error", "<script>alert('" + ex.Message.ToString() + "')</script>");

        //        con.Close();
        //        return;
        //    }
        //}
        //if (FileUpload3.FileName != null)
        //{
        //    try
        //    {
        //        HttpPostedFile file3 = FileUpload3.PostedFile;
        //        Int32 fileLength = file3.ContentLength;
        //        string fileName = file3.FileName;
        //        byte[] buffer = new byte[fileLength];
        //        file3.InputStream.Read(buffer, 0, fileLength);
        //        FileStream newFile;
        //        string strPath2 = @"F:\PatentDocument\SolutionPackages\" + txtFileNo.Text + @"\";
        //        if (!Directory.Exists(strPath2))
        //        {
        //            Directory.CreateDirectory(strPath2);
        //        }
        //        if (!File.Exists(strPath2 + filename2))
        //        {
        //            newFile = File.Open(strPath2 + filename2, FileMode.Create);
        //            newFile.Write(buffer, 0, buffer.Length);
        //            newFile.Close();
        //            Trans.Rollback();
        //        }
        //        else
        //        {
        //            ClientScript.RegisterStartupScript(GetType(), "Information", "<script>alert('This File exists in this Folder')</script>");
        //            Trans.Rollback();
        //            con.Close();
        //            return;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        ClientScript.RegisterStartupScript(GetType(), "Error", "<script>alert('" + ex.Message.ToString() + "')</script>");

        //        con.Close();
        //        return;
        //    }

        //}



 }
        
   
    protected void lvInventor_ItemDataBound(object sender, ListViewItemEventArgs e)
    {
        LinkButton lb = e.Item.FindControl("lbtnInsert") as LinkButton;
        LinkButton lb1 = e.Item.FindControl("lbtnCancel") as LinkButton;
        LinkButton lb2 = e.Item.FindControl("lbtnDelete") as LinkButton;
        if (lb != null)
        {
            ScriptManager.GetCurrent(Page).RegisterPostBackControl(lb);
        }
        else if (lb1 != null)
        {
            ScriptManager.GetCurrent(Page).RegisterPostBackControl(lb1);
        }
        else if (lb2 != null)
        {
            ScriptManager.GetCurrent(Page).RegisterPostBackControl(lb2);
        }
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
        else if (lb1 != null)
        {
            ScriptManager.GetCurrent(Page).RegisterPostBackControl(lb1);
        }
        else if (lb2 != null)
        {
            ScriptManager.GetCurrent(Page).RegisterPostBackControl(lb2);
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
        if (txtAgency.Text.Trim()== "")
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
            cnt += 1;
            if (dr["SlNo"].ToString() == lbl.Text.Trim())
            {
                dr.Delete();
                break;
            }
        }
        for (int i = cnt - 1; i <= dt.Rows.Count - 1; i++)
        {
            dt.Rows[i][0] = i + 1;
        }
        lvProjectList.DataSource = (DataTable)ViewState["InventionSource"];
        lvProjectList.DataBind();
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
            FillProjectNo(ProjectType.Text, FinYear.Text,ProjectNo);
        }
    }
    protected void FillProjectNo(string ProjectType, string FinYear, DropDownList ProjectNo)
    {
        con.ConnectionString = ConfigurationManager.ConnectionStrings["ICSR2KSCN"].ConnectionString;
        SqlCommand cmd = new SqlCommand();
        string sql="";
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
            FillProjectDetails(ProjectType.Text,ProjectNo.Text.Trim(),txtAgency,txtProjectTitle);
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
    protected void lvProjectList_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
}
