using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ServiceRequest : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection();
    SqlTransaction Trans;
    protected void Page_Load(object sender, EventArgs e)
    {
        SqlConnection con1 = new SqlConnection();
        con.ConnectionString = ConfigurationManager.ConnectionStrings["PATENTCN"].ConnectionString;
        if (!this.IsPostBack)
        {
            ddlSRNo.Visible = false;
            con.ConnectionString = ConfigurationManager.ConnectionStrings["PATENTCN"].ConnectionString;
            System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-GB");
            txtSRNo.Text = GetSRNO();
            ServiceRequestDS ds = new ServiceRequestDS();
            lvIdf.DataSource = ds.Tables["tbl_trx_servicerequest"];
            lvIdf.DataBind();
            ViewState["SRIdf"] = ds.Tables["tbl_trx_servicerequest"];
            string sql = "select AttorneyID+' - '+AttorneyName as attorneyID from Attorney order by AttorneyID";
            con.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = sql;
            cmd.Connection = con;
            SqlDataReader dr;
            dr = cmd.ExecuteReader();
            dropId.DataTextField = "AttorneyId";
            dropId.DataValueField = "AttorneyId";
            dropId.DataSource = dr;
            dropId.DataBind();
            dropId.Items.Insert(0, "");
            dr.Close();
            IntDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
            con.Close();
        }

    }
    protected string GetSRNO()
    {
        con.Open();
        SqlCommand cmd = new SqlCommand("SELECT MAX(CAST( SUBSTRING(SRNO, LEN(LEFT(SRNO, CHARINDEX ('/', SRNO))) + 1, LEN(SRNO) - LEN(LEFT(SRNO, CHARINDEX ('/', SRNO)))) AS INT))+1 from tbl_trx_servicerequest ", con);
        string SRNO = Convert.ToString(cmd.ExecuteScalar());
        if (!string.IsNullOrEmpty(SRNO)) SRNO = "S " + (DateTime.Today.Month >= 4 ? (DateTime.Today.ToString("yyyy") + "-" + DateTime.Today.AddYears(1).ToString("yy")) : (DateTime.Today.AddYears(-1).ToString("yyyy") + "-" + DateTime.Today.ToString("yy"))) + "/" + SRNO;
        con.Close();
        return SRNO;
    }
    protected void lvIdf_ItemCreated(object sender, ListViewItemEventArgs e)
    {
        con.ConnectionString = ConfigurationManager.ConnectionStrings["PATENTCN"].ConnectionString;
        DropDownList ddl = (DropDownList)e.Item.FindControl("ddlNewIdfNo");
        if (ddl != null)
        {
            SqlCommand cmdA = new SqlCommand();
            string sql = "select fileno from patdetails order by fileno";
            SqlDataReader drA;
            cmdA.CommandType = CommandType.Text;
            cmdA.Connection = con;
            cmdA.CommandText = sql;
            con.Open();
            drA = cmdA.ExecuteReader();
            while (drA.Read())
            {
                ddl.Items.Add(drA["fileno"].ToString());
            }
            //SqlCommand sql1=new SqlCommand("select ")
            ddl.Items.Insert(0, new ListItem("", ""));
            con.Close();
        }

        DropDownList ddlparty = (DropDownList)e.Item.FindControl("ddlNewParty");

        if (ddlparty != null)
        {
            con.Open();
            SqlCommand cmdparty = new SqlCommand("select distinct CompanyName from CompanyMaster", con);
            SqlDataReader drparty = cmdparty.ExecuteReader();
            while (drparty.Read())
            {
                ddlparty.Items.Add(drparty["CompanyName"].ToString());
            }
            ddlparty.Items.Insert(0, new ListItem("", ""));
            con.Close();
        }
        DropDownList ddlmdoc = (DropDownList)e.Item.FindControl("ddlNewMDoc");
        if (ddlmdoc != null)
        {
            con.Open();
            SqlCommand cmdmdoc = new SqlCommand("select distinct ContractNo from Agreement", con);
            SqlDataReader drmdoc = cmdmdoc.ExecuteReader();
            while (drmdoc.Read())
            {
                ddlmdoc.Items.Add(drmdoc["ContractNo"].ToString());
            }
            ddlmdoc.Items.Insert(0, new ListItem("", ""));
            con.Close();
        }
        DropDownList ddl1 = (DropDownList)e.Item.FindControl("ddlNewAction");
        if (ddl1 != null)
        {

            con.Open();
            SqlCommand cmdddl = new SqlCommand("select ItemList from ListItemMaster where Category='action' and Grouping='Service Request'", con);
            SqlDataReader drddl = cmdddl.ExecuteReader();
            while (drddl.Read())
            {
                ddl1.Items.Add(drddl["ItemList"].ToString());
            }
            ddl1.Items.Insert(0, new ListItem("", ""));
            con.Close();
        }
        DropDownList ddlStatus = (DropDownList)e.Item.FindControl("ddlNewStatus");
        if (ddlStatus != null)
        {
            con.Open();
            SqlCommand cmdstatus = new SqlCommand("select ItemList from ListItemMaster where Category='Status' and Grouping='Service Request'", con);
            SqlDataReader drstatus = cmdstatus.ExecuteReader();
            while (drstatus.Read())
            {
                ddlStatus.Items.Add(drstatus["ItemList"].ToString());
            }
            ddlStatus.Items.Insert(0, new ListItem("", ""));
            con.Close();
        }
    }
    protected void ddlNewIdfNo_SelectedIndexChanged(object sender, EventArgs e)
    {
        //DropDownList ddlIdf = (DropDownList)sender;
        //ListViewItem item1 = (ListViewItem)ddlIdf.NamingContainer;
        //DropDownList ddlIdfGet = (DropDownList)item1.FindControl("ddlNewIdfNo");
        //if (ddlIdfGet.SelectedValue != "")
        //{
        //    SqlCommand cmd = new SqlCommand();
        //    DateTime srdt = DateTime.Today.AddDays(14);
        //    DateTime fdt = srdt.AddMonths(1);
        //    //string sql = "select 1 as slno,title,'Search Report' as action,'"+srdt.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture) + "' as TargetDate,'As per IITM Spec' as Remarks from patdetails where fileno='" + ddlIdfGet.SelectedValue + "' union select 2 as slno,title,'Provisional Filing' as action,'" + fdt.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture) + "' as TargetDate,'' as Remarks from patdetails where fileno = '" + ddlIdfGet.SelectedValue + "'";
        //    //"select title,'Search Report' as action,'"+srdt.ToString("dd/M/yyyy",CultureInfo.InvariantCulture)+"' as TargetDate,'As per IITM Spec' as Remarks from patdetails where fileno='" + ddlIdfGet.SelectedValue + "'";
        //    //SqlDataReader dr;
        //    //cmd.CommandType = CommandType.Text;
        //    //cmd.Connection = con;
        //    //cmd.CommandText = sql;
        //    con.Open();
        //    DataTable dtAction = new DataTable();
        //    string sql = "select 1 as slno,FileNo,title,'Search Report' as action,'' as party,'' as Share,'' as MDoc,'" + srdt.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture) + "' as TargetDt,'' as ActualDt,'' as Status,'As per IITM Spec' as Remarks from patdetails where fileno='" + ddlIdfGet.SelectedValue + "' union select 2 as slno,FileNo,title,'Provisional Filing' as action,'' as party,'' as Share,'' as MDoc,'" + fdt.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture) + "' as TargetDt,'' as ActualDt,'' as Status,'' as Remarks from patdetails where fileno = '" + ddlIdfGet.SelectedValue + "'";
        //    SqlDataAdapter sda = new SqlDataAdapter(sql, con);
        //    sda.Fill(dtAction);
        //    lvIdf.DataSource = dtAction;
        //    ViewState["SRIdf"] = dtAction;
        //    lvIdf.DataBind();
        //    sda.Dispose();
        //    con.Close();
        //    //dr = cmd.ExecuteReader();
        //    //if(dr.Read())
        //    //{
        //    //    Label txt = (Label)item1.FindControl("lblNewSlNo");
        //    //    if (dr[0] != DBNull.Value)
        //    //    {
        //    //        txt.Text = dr[0].ToString();
        //    //    }
        //    //    txt = (Label)item1.FindControl("lblTitle");
        //    //    txt.Text = dr.GetString(1);
        //    //    DropDownList ddl = (DropDownList)item1.FindControl("ddlAction");
        //    //    if (dr[2] != DBNull.Value)
        //    //    {
        //    //        ddl.Text = dr.GetString(2);
        //    //    }
        //    //    txt = (Label)item1.FindControl("lblTargetDt");                
        //    //        txt.Text = dr.GetString(3);                
        //    //    txt = (Label)item1.FindControl("lblRemarks");
        //    //    txt.Text = dr.GetString(4);
        //    //}
        //    //con.Close();
        //}
    }
    protected void lvIdf_ItemInserting(object sender, ListViewInsertEventArgs e)
    {
        string slno;
        string ldf = "";
        string action = "";
        string party = "";
        string targetdt = null;
        // tdt = null ;
        string share = "";
        string remarks = "", status = "", mdoc = "";
        string actualdt = null;
        string srno = txtSRNo.Text;
        DropDownList ddl = e.Item.FindControl("ddlNewIdfNo") as DropDownList;
        ldf = ddl.SelectedValue;
        if (ldf != "")
        {

            TextBox lbl = (TextBox)e.Item.FindControl("lblNewSlNo");
            slno = lbl.Text;
            DropDownList ddl1 = e.Item.FindControl("ddlNewAction") as DropDownList;
            action = ddl1.Text;
            ddl1 = e.Item.FindControl("ddlNewParty") as DropDownList;
            party = ddl1.Text;
            TextBox txt = e.Item.FindControl("txtShare") as TextBox;
            share = txt.Text;
            ddl1 = e.Item.FindControl("ddlNewMDoc") as DropDownList;
            mdoc = ddl1.Text;
            txt = e.Item.FindControl("txtTargetDt") as TextBox;
            targetdt = txt.Text;//?Convert.ToDateTime(txt.Text):(DateTime?) null;
            //tdt =(targetdt!="")? Convert.ToDateTime(targetdt):(DateTime?)null;
            txt = e.Item.FindControl("txtActualDt") as TextBox;
            actualdt = txt.Text;//!=""?Convert.ToDateTime(txt.Text):(DateTime?)null;
            ddl1 = e.Item.FindControl("ddlNewStatus") as DropDownList;
            status = ddl1.Text;
            txt = e.Item.FindControl("txtRemarks") as TextBox;
            remarks = txt.Text;
            if (action != null)
            {
                DataTable dt = (DataTable)ViewState["SRIdf"];
                //if (dt.Rows.Count > 0)
                //{
                //    DataRow drSl = dt.Rows[dt.Rows.Count - 1];
                //    if (drSl.RowState == DataRowState.Deleted)
                //    {
                //        slno = (Convert.ToInt32(drSl["Sno", DataRowVersion.Original]) + 1).ToString();

                //    }
                //    else
                //        slno = (Convert.ToInt32(drSl["slno"]) + 1).ToString();
                //}
                //else
                //{
                //    slno = "1";
                //}
                DataRow dr = dt.NewRow();
                dr["Sno"] = slno;
                dr["FileNo"] = ldf;
                dr["SharingParty"] = party;
                dr["Action"] = action;
                dr["TargetDt"] = (targetdt != "") ? (object)DateTime.ParseExact(targetdt, "dd/MM/yyyy", CultureInfo.InvariantCulture) : DBNull.Value;
                dr["ActualDt"] = actualdt != "" ? (object)DateTime.ParseExact(actualdt, "dd/MM/yyyy", CultureInfo.InvariantCulture) : DBNull.Value;
                dr["Remarks"] = remarks;
                dr["Share"] = share != "" ? (object)Convert.ToInt16(share) : DBNull.Value;
                dr["MDocNo"] = mdoc;
                dr["Status"] = status;
                dt.Rows.Add(dr);
                lvIdf.DataSource = (DataTable)ViewState["SRIdf"];
                lvIdf.DataBind();
            }
        }
    }
    protected void lvIdf_ItemCanceling(object sender, ListViewCancelEventArgs e)
    {
        lvIdf.DataSource = (DataTable)ViewState["SRIdf"];
        lvIdf.DataBind();
    }
    protected void lvIdf_ItemDeleting(object sender, ListViewDeleteEventArgs e)
    {
        int iDel = e.ItemIndex;
        DataTable dt = (DataTable)ViewState["SRIdf"];
        DataRow dr = dt.Rows[iDel];
        if (dr.RowState != DataRowState.Deleted)
        {
            dr.Delete();
        }
        lvIdf.DataSource = (DataTable)ViewState["SRIdf"];
        lvIdf.DataBind();
    }

    protected void ddlAction_SelectedIndexChanged(object sender, EventArgs e)
    {
        //DropDownList ddlAction = (DropDownList)sender;
        //ListViewItem item1 = (ListViewItem)ddlAction.NamingContainer;
        //DropDownList ddlActionGet = (DropDownList)item1.FindControl("ddlAction");

        //if (ddlActionGet.SelectedValue == "Complete Filing")
        //{
        //    DataTable dt = (DataTable)ViewState["SRIdf"];
        //    if (dt.Rows.Count == 2)
        //    {
        //        dt.Rows[1]["action"] = "Complete Filing";
        //        DataRow dr = dt.NewRow();
        //        dr[0] = 3;
        //        dr[1] = dt.Rows[0][1];
        //        dr[2] = dt.Rows[0][2];
        //        dr[3] = "RFE (along with complete specification)";
        //        dr[4] = dt.Rows[0][4];
        //        dr[7] = dt.Rows[1][7];
        //        dt.Rows.Add(dr);
        //        lvIdf.DataSource = dt;
        //        //lvIdf.DataSource = (DataTable)ViewState["SRIdf"];
        //        lvIdf.DataBind();
        //    }
        //}
    }

    protected void imgBtnSubmit_Click(object sender, ImageClickEventArgs e)
    {
        DataTable dt1 = (DataTable)ViewState["SRIdf"];
        if (dt1.Rows.Count > 0)
        {
            DataTable dtDel = dt1.GetChanges(DataRowState.Deleted);
            if (dtDel != null)
            {
                foreach (DataRow dr1 in dtDel.Rows)
                {
                    try
                    {
                        con.Open();
                        SqlCommand cmddel = new SqlCommand("delete from tbl_trx_servicerequest where SRNo='" + ddlSRNo.SelectedValue + "' and Sno='" + dr1["Sno", DataRowVersion.Original].ToString() + "'", con);
                        cmddel.ExecuteNonQuery();
                        con.Close();
                    }
                    catch (Exception ex)
                    {
                        ClientScript.RegisterStartupScript(GetType(), "Error", "<script>alert('" + ex.Message.ToString() + "')</script>");
                        con.Close();
                    }
                }
            }
        }
        foreach (ListViewItem li in lvIdf.Items)
        {
            string srno;
            if (title.Text == "New Service Request")
            {
                srno = txtSRNo.Text;
            }
            else
            {
                srno = ddlSRNo.SelectedValue;
            }
            string intDt = IntDate.Text;
            Label lsno = li.FindControl("lblSlNo") as Label;
            int sno = Convert.ToInt16(lsno.Text);
            Label lidf = li.FindControl("lblIdfNo") as Label;
            string idf = lidf.Text.Trim();
            // DropDownList dattorney = (DropDownList)li.FindControl("dropId");
            string att = dropId.SelectedValue;
            Label taction = li.FindControl("ddlAction") as Label;
            string action = taction.Text;
            Label tparty = (Label)li.FindControl("ddlParty");
            string party = tparty.Text;
            Label tshare = (Label)li.FindControl("lblShare");
            string share = tshare.Text;
            Label tmdoc = (Label)li.FindControl("lblMDoc");
            string mdoc = tmdoc.Text;
            Label ttargetdt = (Label)li.FindControl("lblTargetDt");
            string targetdt = string.IsNullOrEmpty(ttargetdt.Text) ? null : ttargetdt.Text;
            //DateTime? targetdt = (ttargetdt.Text!=null)?DateTime.ParseExact(ttargetdt.Text,"dd/MM/yyyy",CultureInfo.InvariantCulture):(DateTime?)null;
            Label tactualdt = (Label)li.FindControl("lblActualDt");
            string actualdt = string.IsNullOrEmpty(tactualdt.Text) ? null : tactualdt.Text;
            //DateTime? actualdt =(tactualdt.Text!=null)?DateTime.ParseExact(tactualdt.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture):(DateTime?)null;
            Label tstatus = (Label)li.FindControl("ddlStatus");
            string status = tstatus.Text;
            Label tremarks = (Label)li.FindControl("lblRemarks");
            string remarks = tremarks.Text;
            try
            {
                con.Open();
                Trans = con.BeginTransaction();
                if (title.Text == "Modify Service Request")
                {
                    //SqlCommand cmd = new SqlCommand("insert into tbl_trx_servicerequest values('" + srno + "','" + sno + "','" + idf + "','" + att + "','" + party + "','" + action + "','" + share + "','" + mdoc + "','" + intDt + "',case when ('" + targetdt + "'!=null) then '"+targetdt+"' else null end,case when ('" + actualdt + "'!=null) then '" + actualdt + "' else null end,'" + status + "','" + remarks + "','" + DateTime.Now + "','" + User.Identity.Name + "',null,'')", con);
                    //Trans = con.BeginTransaction();
                    //cmd.Transaction = Trans;
                    //cmd.ExecuteNonQuery();
                    try
                    {
                        DataTable dt = (DataTable)ViewState["SRIdf"];
                        if (dt.Rows.Count > 0)
                        {
                            DataTable dtDel = dt.GetChanges(DataRowState.Deleted);
                            if (dtDel != null)
                            {
                                foreach (DataRow dr1 in dtDel.Rows)
                                {
                                    SqlCommand cmddel = new SqlCommand("delete from tbl_trx_servicerequest where SRNo='" + ddlSRNo.SelectedValue + "' and Sno='" + dr1["Sno", DataRowVersion.Original].ToString() + "'", con);

                                    cmddel.Transaction = Trans;
                                    cmddel.ExecuteNonQuery();

                                }
                            }
                            DataTable dtAdd = dt.GetChanges(DataRowState.Added);
                            if (dtAdd != null)
                            {
                                foreach (DataRow dr1 in dtAdd.Rows)
                                {
                                    SqlCommand cmdadd = new SqlCommand("insert into tbl_trx_servicerequest values('" + ddlSRNo.SelectedValue + "','" + dr1["Sno"].ToString() + "','" + dr1["FileNo"].ToString() + "','" + att + "','" + dr1["SharingParty"].ToString() + "','" + dr1["Action"].ToString() + "','" + dr1["Share"].ToString() + "','" + dr1["MDocNo"].ToString() + "',case when ('" + intDt + "'!='') then '" + intDt + "' else null end,case when ('" + dr1["TargetDt"].ToString() + "'!='') then '" + dr1["TargetDt"].ToString() + "' else null end,case when ('" + dr1["Actualdt"].ToString() + "'!='') then '" + dr1["Actualdt"].ToString() + "' else null end,'" + dr1["Status"].ToString() + "','" + dr1["Remarks"].ToString() + "','" + DateTime.Now + "','" + User.Identity.Name + "',null,'')", con);
                                    cmdadd.Transaction = Trans;
                                    cmdadd.ExecuteNonQuery();
                                }
                            }
                            DataTable dtmod = dt.GetChanges(DataRowState.Modified);
                            if (dtmod != null)
                            {
                                foreach (DataRow dr1 in dtmod.Rows)
                                {
                                    SqlCommand cmdmod = new SqlCommand("update tbl_trx_servicerequest set FileNo=@fileno,Party=@party,Action=@action,SharingParty=@sharingparty,Share=@share,MDocNo=@mdocno,IntimationDt=@intdt,TargetDt=@tdt,ActualDt=@adt,Status=@status,Remarks=@remarks where SRNo='" + txtSRNo.Text + "' and Sno=@sno", con);
                                    cmdmod.Transaction = Trans;
                                    SqlParameter pm1 = new SqlParameter();
                                    pm1.ParameterName = "@fileno";
                                    pm1.SourceColumn = "FileNo";
                                    pm1.Value = dr1["FileNo", DataRowVersion.Original];
                                    pm1.DbType = DbType.String;
                                    pm1.Direction = ParameterDirection.Input;

                                    SqlParameter pm2 = new SqlParameter();
                                    pm2.ParameterName = "@party";
                                    pm2.SourceColumn = "Party";
                                    pm2.Value = dr1["Party", DataRowVersion.Original];
                                    pm2.DbType = DbType.String;
                                    pm2.Direction = ParameterDirection.Input;

                                    SqlParameter pm3 = new SqlParameter();
                                    pm3.ParameterName = "@action";
                                    pm3.SourceColumn = "Action";
                                    pm3.Value = dr1["Action", DataRowVersion.Original];
                                    pm3.DbType = DbType.String;
                                    pm3.Direction = ParameterDirection.Input;

                                    SqlParameter pm4 = new SqlParameter();
                                    pm4.ParameterName = "@sharingparty";
                                    pm4.SourceColumn = "SharingParty";
                                    pm4.Value = dr1["SharingParty", DataRowVersion.Original];
                                    pm4.DbType = DbType.String;
                                    pm4.Direction = ParameterDirection.Input;

                                    SqlParameter pm5 = new SqlParameter();
                                    pm5.ParameterName = "@share";
                                    pm5.SourceColumn = "Share";
                                    pm5.Value = dr1["Share", DataRowVersion.Original];
                                    pm5.DbType = DbType.Int16;
                                    pm5.Direction = ParameterDirection.Input;

                                    SqlParameter pm6 = new SqlParameter();
                                    pm6.ParameterName = "@mdocno";
                                    pm6.SourceColumn = "MDocNo";
                                    pm6.Value = dr1["MDocNo", DataRowVersion.Original];
                                    pm6.DbType = DbType.String;
                                    pm6.Direction = ParameterDirection.Input;

                                    SqlParameter pm7 = new SqlParameter();
                                    pm7.ParameterName = "@intdt";
                                    pm7.SourceColumn = "IntimationDt";
                                    pm7.Value = dr1["IntimationDt", DataRowVersion.Original];
                                    pm7.DbType = DbType.Date;
                                    pm7.Direction = ParameterDirection.Input;

                                    SqlParameter pm8 = new SqlParameter();
                                    pm8.ParameterName = "@tdt";
                                    pm8.SourceColumn = "TargetDt";
                                    pm8.Value = dr1["TargetDt", DataRowVersion.Original];
                                    pm8.DbType = DbType.Date;
                                    pm8.Direction = ParameterDirection.Input;

                                    SqlParameter pm9 = new SqlParameter();
                                    pm9.ParameterName = "@adt";
                                    pm9.SourceColumn = "ActualDt";
                                    pm9.Value = dr1["ActualDt", DataRowVersion.Original];
                                    pm9.DbType = DbType.Date;
                                    pm9.Direction = ParameterDirection.Input;

                                    SqlParameter pm10 = new SqlParameter();
                                    pm10.ParameterName = "@status";
                                    pm10.SourceColumn = "Status";
                                    pm10.Value = dr1["Status", DataRowVersion.Original];
                                    pm10.DbType = DbType.String;
                                    pm10.Direction = ParameterDirection.Input;

                                    SqlParameter pm11 = new SqlParameter();
                                    pm11.ParameterName = "@remarks";
                                    pm11.SourceColumn = "Remarks";
                                    pm11.Value = dr1["Remarks", DataRowVersion.Original];
                                    pm11.DbType = DbType.String;
                                    pm11.Direction = ParameterDirection.Input;

                                    cmdmod.Parameters.Add(pm1);
                                    cmdmod.Parameters.Add(pm2);
                                    cmdmod.Parameters.Add(pm3);
                                    cmdmod.Parameters.Add(pm4);
                                    cmdmod.Parameters.Add(pm5);
                                    cmdmod.Parameters.Add(pm6);
                                    cmdmod.Parameters.Add(pm7);
                                    cmdmod.Parameters.Add(pm8);
                                    cmdmod.Parameters.Add(pm9);
                                    cmdmod.Parameters.Add(pm10);
                                    cmdmod.Parameters.Add(pm11);
                                    cmdmod.ExecuteNonQuery();
                                }
                                Trans.Commit();
                                string str2 = "This Record successfully Modified";
                                ClientScript.RegisterStartupScript(GetType(), "Success", "<script>alert('" + str2 + "')</script>");
                                con.Close();
                            }
                            Trans.Commit();
                            ClientScript.RegisterStartupScript(GetType(), "Success", "<script>alert(This Record successfully Modified)</script>");
                            con.Close();
                        }
                    }
                    catch (Exception ex)
                    {
                        Trans.Rollback();
                        ClientScript.RegisterStartupScript(GetType(), "Error", "<script>alert('" + ex.Message.ToString() + "')</script>");
                        con.Close();
                    }
                }
                else
                {
                    try
                    {
                        SqlCommand cmd = new SqlCommand("insert into tbl_trx_servicerequest values('" + srno + "','" + sno + "','" + idf + "','" + att + "','" + party + "','" + action + "','" + share + "','" + mdoc + "',case when ('" + intDt + "'!=null) then '" + intDt + "' else null end,'" + targetdt + "',case when ('" + actualdt + "'!=null) then '" + actualdt + "' else null end,'" + status + "','" + remarks + "','" + DateTime.Now + "','" + User.Identity.Name + "',null,'')", con);
                        cmd.ExecuteNonQuery();
                        con.Close();
                        ClientScript.RegisterStartupScript(GetType(), "Success", "<script>alert('Updated Successfully')</script>");
                    }
                    catch (Exception ex)
                    {
                        ClientScript.RegisterStartupScript(GetType(), "Error", "<script>alert('" + ex.Message.ToString() + "')</script>");
                        con.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                ClientScript.RegisterStartupScript(GetType(), "Error", "<script>alert('" + ex.Message.ToString() + "')</script>");
                con.Close();
                return;

            }
        }
    }

    protected void imgBtnClear_Click(object sender, ImageClickEventArgs e)
    {
        txtCompany.Text = "";
        txtSRNo.Text = "";
        dropId.SelectedIndex = 0;
        IntDate.Text = "";
        DataTable dt=(DataTable)ViewState["SRIdf"];
        dt.Clear();
    }

    protected void imgBtnReport_Click(object sender, ImageClickEventArgs e)
    {
        string srno = txtSRNo.Text; int sno;
        string party = "", share = "", action = "", idf = "", mdoc = "", targetdt = "", actualdt = "", status = "", remarks = "", att = "", intdt = "";
        con.Open();
        SqlCommand cmd = new SqlCommand("select * from tbl_trx_servicerequest where SRNo='" + srno + "'", con);
        SqlDataReader dr = cmd.ExecuteReader();
        DataTable dt = new DataTable();
        dt.Columns.AddRange(new DataColumn[] {new DataColumn("SRNO"), new DataColumn("SNO"), new DataColumn("IDF"), new DataColumn("Attorney"), new DataColumn("Party"), new DataColumn("Action"), new DataColumn("Share"),new DataColumn("Mdoc"),new DataColumn("IntiDt"),
            new DataColumn("TargetDt"),new DataColumn("ActualDt"),new DataColumn("Status"), new DataColumn("Remarks")});

        while (dr.Read())
        {
            if (!dr.IsDBNull(1)) sno = Int16.Parse(dr.GetString(1)); else sno = 0;
            if (!dr.IsDBNull(2)) idf = dr.GetString(2); else idf = "";
            if (!dr.IsDBNull(3)) att = dr.GetString(3); else att = "";
            if (!dr.IsDBNull(4)) party = dr.GetString(4); else party = "";
            if (!dr.IsDBNull(5)) action = dr.GetString(5); else action = "";
            if (!dr.IsDBNull(6)) share = dr.GetString(6); else share = "";
            if (!dr.IsDBNull(7)) mdoc = dr.GetString(7); else mdoc = "";
            if (!dr.IsDBNull(8)) intdt = dr.GetString(8); else intdt = "";
            if (!dr.IsDBNull(9)) targetdt = dr.GetString(9); else targetdt = "";
            if (!dr.IsDBNull(10)) actualdt = dr.GetString(10); else actualdt = "";
            if (!dr.IsDBNull(11)) status = dr.GetString(11); else status = "";
            if (!dr.IsDBNull(12)) remarks = dr.GetString(12); else remarks = "";
            dt.Rows.Add(srno, sno, idf, att, party, action, share, mdoc, intdt, targetdt, actualdt, status, remarks);
            // if (!dr.IsDBNull(13)) Pincode = dr.GetString(13); else Pincode = "";
        }
        string docfilename = "Template for " + srno + ".docx";
        if (dt.Rows.Count > 0)
        {
            LetterTemplateXML tt = new LetterTemplateXML();
            tt.ProcessRequest(dt, docfilename);
        }
        con.Close();

    }

    protected void Modify_Click(object sender, EventArgs e)
    {
        title.Text = "Modify Service Request";
        txtSRNo.Visible = false;
        ddlSRNo.Visible = true;
        if (ddlSRNo.Items.Count == 0)
        {
            con.Open();
            SqlCommand cmdsrno = new SqlCommand("select distinct SRNo from tbl_trx_servicerequest", con);
            SqlDataReader drsrno = cmdsrno.ExecuteReader();
            while (drsrno.Read())
            {
                ddlSRNo.Items.Add(drsrno["SRNo"].ToString());
            }

            con.Close();
        }
    }

    protected void New_Click(object sender, EventArgs e)
    {
        title.Text = "New Service Request";
        txtSRNo.Visible = true;
        ddlSRNo.Visible = false;
    }

    protected void ddlSRNo_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlSRNo.Text != "")
        {
            DataTable dt1 = (DataTable)ViewState["SRIdf"];
            dt1.Clear();
            con.Open();
            try
            {
                SqlCommand cmdsrno = new SqlCommand("select IntimationDt,AttorneyID from tbl_trx_servicerequest where SRNo='" + ddlSRNo.SelectedValue + "' and Sno=1", con);
                SqlDataReader drsrno = cmdsrno.ExecuteReader();
                if (drsrno.Read())
                {
                    if (!drsrno.IsDBNull(0)) IntDate.Text = drsrno["IntimationDt"].ToString(); else IntDate.Text = "";
                    if (!drsrno.IsDBNull(1)) dropId.Text = drsrno["AttorneyID"].ToString(); else dropId.SelectedIndex = 0;
                }
                drsrno.Close();
                DataTable dt = (DataTable)ViewState["SRIdf"];
                SqlCommand cmd = new SqlCommand("select Sno,FileNo,SharingParty,Action,Share,MDocNo,TargetDt,ActualDt,Status,Remarks from tbl_trx_servicerequest where SRNo='" + ddlSRNo.SelectedValue + "'", con);
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    DataRow drow = dt.NewRow();

                    drow["Sno"] = dr.GetInt16(0);
                    drow["FileNo"] = dr.GetString(1);
                    drow["SharingParty"] = (!dr.IsDBNull(2)) ? dr.GetString(2) : "";
                    drow["Action"] = (!dr.IsDBNull(3)) ? dr.GetString(3) : "";
                    drow["Share"] = (!dr.IsDBNull(4)) ? dr.GetInt16(4) : 0;
                    drow["MDocNo"] = (!dr.IsDBNull(5)) ? dr.GetString(5) : "";
                    drow["TargetDt"] = (!dr.IsDBNull(6)) ? dr.GetDateTime(6) : (object)DBNull.Value;
                    drow["ActualDt"] = (!dr.IsDBNull(7)) ? dr.GetDateTime(7) : (object)DBNull.Value;
                    drow["Status"] = (!dr.IsDBNull(8)) ? dr.GetString(8) : "";
                    drow["Remarks"] = (!dr.IsDBNull(9)) ? dr.GetString(9) : "";
                    dt.Rows.Add(drow);
                }
                con.Close();
                dt.AcceptChanges();
                ViewState["SRIdf"] = dt;
                lvIdf.DataSource = dt;
                lvIdf.DataBind();
                dr.Close();

            }
            catch (Exception ex)
            {
                ClientScript.RegisterStartupScript(GetType(), "Error", "<script>alert('" + ex.Message.ToString() + "')</script>");
                con.Close();

            }
        }
    }
}