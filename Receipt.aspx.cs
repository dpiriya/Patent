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
using System.Web.Services;
using System.Web.Script.Services;
using System.Collections.Generic;
using System.Globalization;

public partial class Receipt : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection();

    protected void Page_Load(object sender, EventArgs e)
    {
        con.ConnectionString = ConfigurationManager.ConnectionStrings["PATENTCN"].ConnectionString;
        if (!IsPostBack)
        {
            title.Text = "New Receipt";
            ddlrcpno.Visible = false;
            con.Open();
            string RNO;
            string fy ="RC "+(DateTime.Today.Month >= 4 ? (DateTime.Today.ToString("yyyy") + "-" + DateTime.Today.AddYears(1).ToString("yy")) : (DateTime.Today.AddYears(-1).ToString("yyyy") + "-" + DateTime.Today.ToString("yy")));
            SqlCommand cmdfy = new SqlCommand("select count(*) from patentreceipt where Rno like '%" + fy + "%'", con);
            if (Convert.ToInt16(cmdfy.ExecuteScalar()) == 0)
            {
                RNO = "1";
            }
            else
            {
                SqlCommand cmdrcpt = new SqlCommand("SELECT MAX(CAST(SUBSTRING(Rno, LEN(LEFT(Rno, CHARINDEX('/', Rno))) + 1, LEN(Rno) - LEN(LEFT(Rno, CHARINDEX('/', Rno)))) AS INT)) + 1 from patentreceipt where RNo like '%" + fy + "%'", con);
                 RNO = cmdrcpt.ExecuteScalar().ToString();
                //RNO = Convert.ToString(cmdrcpt.ExecuteScalar());
            }
            if (!string.IsNullOrEmpty(RNO)) RNO = "RC " + (DateTime.Today.Month >= 4 ? (DateTime.Today.ToString("yyyy") + "-" + DateTime.Today.AddYears(1).ToString("yy")) : (DateTime.Today.AddYears(-1).ToString("yyyy") + "-" + DateTime.Today.ToString("yy"))) + "/" + RNO;
            txtrcpno.Text = RNO;
            txtrcpno.ReadOnly = true;
            SqlCommand cmdsrc = new SqlCommand("select ItemList from ListItemMaster WHERE Category='Source' and Grouping='Receipt'", con);
            SqlDataReader dr = cmdsrc.ExecuteReader();
            ddlsrc.Items.Add("");
            while (dr.Read())
            {
                ddlsrc.Items.Add(new ListItem(dr.GetString(0)));
            }
            dr.Close();
            SqlCommand cmdparty = new SqlCommand("select distinct CompanyName from CompanyMaster", con);
            dr = cmdparty.ExecuteReader();
            while (dr.Read())
            {
                ddlparty.Items.Add(dr["CompanyName"].ToString());
            }
            ddlparty.Items.Insert(0, new ListItem("", ""));
            dr.Close();
            con.Close();
            ReceiptDS ds = new ReceiptDS();
            lvIdf.DataSource = ds.Tables["tbl_sec_receiptfileno"];
            lvIdf.DataBind();
            ViewState["rcpt"] = ds.Tables["tbl_sec_receiptfileno"];
        }
    }




    protected void lvIdf_ItemCreated(object sender, ListViewItemEventArgs e)
    {
        con.ConnectionString = ConfigurationManager.ConnectionStrings["PATENTCN"].ConnectionString;
        DropDownList ddl = (DropDownList)e.Item.FindControl("ddlIdf");
        if (ddl != null)
        {

            SqlCommand idf = new SqlCommand("select RTRIM(fileno) as fileno from patdetails order by fileno", con);
            con.Open();
            SqlDataReader dr;
            dr = idf.ExecuteReader();
            while (dr.Read())
            {
                ddl.Items.Add(dr["fileno"].ToString());
            }
            //SqlCommand sql1=new SqlCommand("select ")
            ddl.Items.Insert(0, new ListItem("", ""));
            con.Close();
        }
        DropDownList ddlgrp = (DropDownList)e.Item.FindControl("ddlgrp");
        if (ddlgrp != null)
        {
            con.Open();
            SqlCommand cmdgrp = new SqlCommand("select ItemList from ListItemMaster where Category='Group' and Grouping='Receipt'", con);
            SqlDataReader dr = cmdgrp.ExecuteReader();
            while (dr.Read())
            {
                ddlgrp.Items.Add(dr["ItemList"].ToString());
            }
            ddlgrp.Items.Insert(0, new ListItem("", ""));
            dr.Close();
            con.Close();
        }
    }

    protected void lvIdf_ItemCanceling(object sender, ListViewCancelEventArgs e)
    {
        lvIdf.DataSource = (DataTable)ViewState["rcpt"];
        lvIdf.DataBind();
    }

    protected void lvIdf_ItemDeleting(object sender, ListViewDeleteEventArgs e)
    {

        int iDel = e.ItemIndex;
        DataTable dt = (DataTable)ViewState["rcpt"];
        DataRow dr = dt.Rows[iDel];
        if (dr.RowState != DataRowState.Deleted)
        {
            dr.Delete();
        }
        lvIdf.DataSource = (DataTable)ViewState["rcpt"];
        lvIdf.DataBind();
    }

    protected void lvIdf_ItemUpdating(object sender, ListViewUpdateEventArgs e)
    {
        TextBox txt = lvIdf.Items[e.ItemIndex].FindControl("txtEdSno") as TextBox;
        int sno = Convert.ToInt16(txt.Text);
        DropDownList ddl = (DropDownList)lvIdf.Items[e.ItemIndex].FindControl("ddlEdIdf");
        string idf = ddl.SelectedValue;
        ddl = (DropDownList)lvIdf.Items[e.ItemIndex].FindControl("ddlEdgrp");
        string grp = ddl.SelectedValue;
        txt = (TextBox)lvIdf.Items[e.ItemIndex].FindControl("txtEdamt");
        string amt = txt.Text;
        txt = (TextBox)lvIdf.Items[e.ItemIndex].FindControl("txtEdrem");
        string rem = txt.Text;
        DataTable dt = (DataTable)ViewState["rcpt"];
        foreach (DataRow dr in dt.Rows)
        {
            if (Convert.ToInt16(dr["Slno"]) == sno)
            {
                dr["FileNo"] = idf;
                dr["RGroup"] = grp;
                dr["SplitAmtInr"] = amt;
                dr["Remarks"] = rem;
            }

        }
        lvIdf.EditIndex = -1;
        lvIdf.DataSource = (DataTable)ViewState["rcpt"];
        lvIdf.DataBind();
    }

    protected void lvIdf_ItemInserting(object sender, ListViewInsertEventArgs e)
    {
        string rno = txtrcpno.Text;
        if (title.Text == "Modify Receipt")
        {
            rno = ddlrcpno.SelectedValue;
        }
        DropDownList ddl = e.Item.FindControl("ddlIdf") as DropDownList;
        string ldf = ddl.SelectedValue;
        if (ldf != "")
        {
            TextBox txt = (TextBox)e.Item.FindControl("txtsno");
            string slno = txt.Text;
            Label tit = (Label)e.Item.FindControl("lbltitle");
            string ttitle = tit.Text;
            DropDownList dgrp = (DropDownList)e.Item.FindControl("ddlgrp");
            string grp = dgrp.Text;
            txt = (TextBox)e.Item.FindControl("txtamt");
            string amt = txt.Text;
            txt = (TextBox)e.Item.FindControl("txtrem");
            string rem = txt.Text;
            DataTable dt = (DataTable)ViewState["rcpt"];
            DataRow dr = dt.NewRow();
            dr["ReceiptNo"] = rno;
            dr["SlNo"] = slno;
            dr["FileNo"] = ldf;
            dr["Title"] = ttitle;
            dr["RGroup"] = grp;
            dr["SplitAmtInr"] = amt;
            dr["Remarks"] = rem;
            dt.Rows.Add(dr);
            lvIdf.DataSource = (DataTable)ViewState["rcpt"];
            lvIdf.DataBind();
        }
    }

    protected void lvIdf_ItemEditing(object sender, ListViewEditEventArgs e)
    {
        lvIdf.EditIndex = e.NewEditIndex;
        lvIdf.DataSource = (DataTable)ViewState["rcpt"];
        lvIdf.DataBind();
    }

    protected void imgBtnSubmit_Click(object sender, ImageClickEventArgs e)
    {
        DataTable dt = (DataTable)ViewState["rcpt"];
        int sum = 0;
        if (dt.Rows.Count > 0)
        {
            foreach(DataRow dr in dt.Rows)
            {
                sum+=Convert.ToInt32(dr["SplitAmtINR"]);
            }
        }
        if (sum == Convert.ToInt32(txtamt.Text))
        {
            con.Open();
            SqlTransaction Trans = con.BeginTransaction();
            try
            {
                SqlCommand cmdmax = new SqlCommand("select ReceiptNo from tbl_primary_receipt where ReceiptNo='" + txtrcpno.Text + "'", con);
                cmdmax.Transaction = Trans;
                string rno = txtrcpno.Text;
                if (title.Text == "Modify Receipt")
                {
                    rno = ddlrcpno.SelectedValue;
                }
                if (cmdmax.ExecuteScalar() == null)
                {
                    decimal amt = Convert.ToInt32(txtamt.Text);
                    decimal transamt =(txttransamt.Text!="")? Convert.ToInt32(txttransamt.Text):0;
                    SqlCommand cmdprimary = new SqlCommand("insert into tbl_primary_receipt (ReceiptNo,ReceiptDt,Source,Party,PartyRefNo,ReceiptRef,ReceiptDesc,AmountINR,IntimationRef,IntimationDt,Comment,Accno,IPAccno,TransferAmt,TransferDt,CreatedOn,CreatedBy,ModifiedOn,ModifiedBy) values('" + txtrcpno.Text + "','" + txtrcpdt.Text + "','" + ddlsrc.SelectedValue + "','" + ddlparty.SelectedValue + "','" + txtpartyref.Text + "','" + txtrcpref.Text + "','" + txtrcpdesc.Text + "','"+amt + "','" + txtintref.Text + "','" + txtintdt.Text + "','" + txtcmt.Text + "','" + txtsaccno.Text + "','" + txtipaccno.Text + "','" +transamt + "','" + txttransdt.Text + "','" + DateTime.Now + "','" + User.Identity.Name + "',null,null)", con);
                    cmdprimary.Transaction = Trans;
                    cmdprimary.ExecuteNonQuery();
                    ClientScript.RegisterStartupScript(GetType(), "Success", "<script>alert('This Record successfully inserted into primary receipt table')</script>");
                }
                else
                {
                    SqlCommand cmdprimary = new SqlCommand("update tbl_primary_receipt set ReceiptDt='" + txtrcpdt.Text + "',Source='" + ddlsrc.SelectedValue + "',Party='" + ddlparty.SelectedValue + "',PartyRefNo='" + txtpartyref.Text + "',ReceiptRef='" + txtrcpref.Text + "',ReceiptDesc='" + txtrcpdesc.Text + "',AmountINR='" + txtamt.Text + "',IntimationRef='" + txtintref.Text + "',IntimationDt='" + txtintdt.Text + "',Comment='" + txtcmt.Text + "',Accno='" + txtsaccno.Text + "',IPAccno='" + txtipaccno.Text + "',TransferAmt='" + txttransamt.Text + "',TransferDt='" + txttransdt.Text + "' where ReceiptNo='" + rno + "'", con);
                    cmdprimary.Transaction = Trans;
                    cmdprimary.ExecuteNonQuery();
                    ClientScript.RegisterStartupScript(GetType(), "Success", "<script>alert('This Record successfully modified in primary receipt table')</script>");
                }
                if (dt.Rows.Count > 0)
                {

                    DataTable dtdel = dt.GetChanges(DataRowState.Deleted);
                    if (dtdel != null)
                    {
                        foreach (DataRow dr in dtdel.Rows)
                        {

                            SqlCommand cmddel = new SqlCommand("delete from tbl_sec_receiptfileno where ReceiptNo='" + rno + "' and FileNo='" + dr["FileNo"].ToString() + "' and Slno='" + dr["Slno"].ToString() + "'", con);
                            cmddel.Transaction = Trans;
                            cmddel.ExecuteNonQuery();
                            SqlCommand cmddb = new SqlCommand("delete from patentreceipt where ReceiptNo='" + rno + "' and FileNo='" + dr["FileNo"].ToString() + "' and Slno='" + dr["Slno"].ToString() + "'", con);
                            cmddb.Transaction = Trans;
                            cmddb.ExecuteNonQuery();
                        }
                    }
                }
                DataTable dtAdd = dt.GetChanges(DataRowState.Added);
                if (dtAdd != null)
                {
                    foreach (DataRow dr1 in dtAdd.Rows)
                    {

                        SqlCommand cmdadd = new SqlCommand("insert into tbl_sec_receiptfileno values('" + rno + "','" + dr1["Slno"].ToString() + "','" + dr1["FileNo"].ToString() + "','" + dr1["Title"].ToString() + "','" + dr1["RGroup"].ToString() + "','" + dr1["SplitAmtInr"].ToString() + "','" + dr1["Remarks"].ToString() + "','" + User.Identity.Name + "','" + DateTime.Now + "',null,null)", con);
                        cmdadd.Transaction = Trans;
                        cmdadd.ExecuteNonQuery();
                        decimal amt = Convert.ToDecimal(dr1["SplitAmtINR"]);
                        SqlCommand cmddb = new SqlCommand("insert into patentreceipt (EntryDt,RNo,FileNo,SlNo,TechTransferNo,Party,PartyRefNo,SubmissionDt,TransType,TransDescription,PaymentGroup,PaymentDescription,Cost_Rs,PaymentDate,PaymentRef,Year ) values ('" + DateTime.Today + "','" + rno + "','" + dr1["FileNo"].ToString() + "','" + dr1["Slno"].ToString() + "','" + txtsaccno.Text + "','" + ddlparty.SelectedValue + "','" + txtpartyref.Text + "','" + txtintdt.Text + "','" + txtintref.Text + "','" + txtcmt.Text + "','" + dr1["RGroup"].ToString() + "','" + txtrcpdesc.Text + "','" + amt + "','" + txtrcpdt.Text + "','" + txtrcpref.Text + "','" + (DateTime.Today.Month >= 4 ? (DateTime.Today.ToString("yyyy") + "-" + DateTime.Today.AddYears(1).ToString("yy")) : (DateTime.Today.AddYears(-1).ToString("yyyy") + "-" + DateTime.Today.ToString("yy"))) + "')", con);
                        cmddb.Transaction = Trans;
                        cmddb.ExecuteNonQuery();
                    }
                }
                DataTable dtmod = dt.GetChanges(DataRowState.Modified);
                if (dtmod != null)
                {
                    foreach (DataRow dr in dtmod.Rows)
                    {
                        SqlCommand cmdmod = new SqlCommand("update tbl_sec_receiptfileno set RGroup='" + dr["RGroup"].ToString() + "',SplitAmtINR='" + dr["SplitAmtInr"].ToString() + "',Remarks='" + dr["Remarks"].ToString() + "',ModifiedBy='" + User.Identity.Name + "',ModifiedOn='" + DateTime.Now + "' where SlNo='" + dr["Slno"].ToString() + "' and ReceiptNo='" + rno + "'", con);
                        cmdmod.Transaction = Trans;
                        cmdmod.ExecuteNonQuery();
                        SqlCommand cmddb = new SqlCommand("update patentreceipt set SlNo='" + dr["SlNo"].ToString() + "',TechTransferNo='" + txtsaccno.Text + "',Party='" + ddlparty.SelectedValue + "',PartyRefNo='" + txtpartyref.Text + "',SubmissionDt='" + txtintdt.Text + "',TransType='" + txtintref.Text + "',TransDescription='" + txtcmt.Text + "',PaymentGroup='" + dr["RGroup"].ToString() + "',PaymentDescription='" + txtrcpdesc.Text + "',Cost_Rs='" + txttransamt.Text + "',PaymentDate='" + txtrcpdt.Text + "',PaymentRef='" + txtrcpref.Text + ",Year='" + (DateTime.Today.Month >= 4 ? (DateTime.Today.ToString("yyyy") + "-" + DateTime.Today.AddYears(1).ToString("yy")) : (DateTime.Today.AddYears(-1).ToString("yyyy") + "-" + DateTime.Today.ToString("yy"))) + "' where Rno='" + rno + "' and FileNo='" + dr["FileNo"].ToString().Trim() + "'", con);
                        cmddb.Transaction = Trans;
                        cmddb.ExecuteNonQuery();
                    }
                }


                Trans.Commit();
                con.Close();
                ClientScript.RegisterStartupScript(GetType(), "Success", "<script>alert('This Record successfully inserted into secondary receipt table')</script>");
            }

            catch (Exception ex)
            {
                Trans.Rollback();
                ClientScript.RegisterStartupScript(GetType(), "Error", "<script>alert('" + ex.Message.ToString() + "')</script>");
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
            }
        }
        else
        {
            ClientScript.RegisterStartupScript(GetType(), "Error", "<script>alert('The total amount entered is not tally with sum of IDF total')</script>");
        }
    }

    protected void imgBtnClear_Click(object sender, ImageClickEventArgs e)
    {

    }
    protected void ddlIdf_SelectedIndexChanged(object sender, EventArgs e)
    {
        DropDownList ddlIdf = (DropDownList)sender;
        ListViewItem item1 = (ListViewItem)ddlIdf.NamingContainer;
        DropDownList ddlIdfGet = (DropDownList)item1.FindControl("ddlIdf");
        if (ddlIdfGet.SelectedValue != "")
        {
            SqlCommand cmdtitle = new SqlCommand("select Title from patdetails where fileno='" + ddlIdfGet.SelectedValue + "'", con);
            con.Open();
            string title = cmdtitle.ExecuteScalar().ToString();
            con.Close();
            Label dtitle = (Label)item1.FindControl("lbltitle");
            dtitle.Text = title;
        }

    }

    protected void lvIdf_ItemDataBound(object sender, ListViewItemEventArgs e)
    {
        if (lvIdf.EditIndex == (e.Item as ListViewItem).DataItemIndex)
        {
            DropDownList ddlIdfNo = (DropDownList)e.Item.FindControl("ddlEdIdf");
            if (ddlIdfNo != null)
            {
                SqlCommand cmd = new SqlCommand("select RTRIM(fileno) as fileno from patdetails order by fileno", con);
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                ddlIdfNo.DataTextField = "fileno";
                ddlIdfNo.DataValueField = "fileno";
                ddlIdfNo.DataSource = dr;
                ddlIdfNo.DataBind();
                ddlIdfNo.Items.Insert(0, new ListItem("", ""));
                con.Close();
            }
            Label lblEdIdf = (Label)e.Item.FindControl("lblEdIdf");
            ddlIdfNo.Items.FindByValue(lblEdIdf.Text.Trim()).Selected = true;

            DropDownList ddlgrp = (DropDownList)e.Item.FindControl("ddlEdgrp");
            if (ddlgrp != null)
            {
                con.Open();
                SqlCommand cmdgrp = new SqlCommand("select ItemList from ListItemMaster where Category='Group' and Grouping='Receipt'", con);
                SqlDataReader dr = cmdgrp.ExecuteReader();
                while (dr.Read())
                {
                    ddlgrp.Items.Add(dr["ItemList"].ToString());
                }
                ddlgrp.Items.Insert(0, new ListItem("", ""));
                dr.Close();
                con.Close();
            }
            Label lblgrp = (Label)e.Item.FindControl("lblEdgrp");
            ddlgrp.Items.FindByValue(lblgrp.Text.Trim()).Selected = true;

            //DropDownList ddlparty = (DropDownList)e.Item.FindControl("ddlEdParty");
            //if (ddlparty != null)
            //{
            //    con.Open();
            //    SqlCommand cmdparty = new SqlCommand("select distinct CompanyName from CompanyMaster", con);
            //    SqlDataReader drparty = cmdparty.ExecuteReader();
            //    while (drparty.Read())
            //    {
            //        ddlparty.Items.Add(drparty["CompanyName"].ToString());
            //    }
            //    ddlparty.Items.Insert(0, new ListItem("", ""));
            //    con.Close();
            //}
            //Label lblparty = (Label)e.Item.FindControl("lblEdParty");
            //ddlparty.Items.FindByValue(lblparty.Text.Trim()).Selected = true;
        }
    }
    protected void New_Click(object sender, EventArgs e)
    {
        title.Text = "New Receipt";
        txtrcpno.Visible = true;
        ddlrcpno.Visible = false;


        lvIdf.DataSource = null;
        lvIdf.DataBind();
    }
    protected void Modify_Click(object sender, EventArgs e)
    {
        title.Text = "Modify Receipt";
        txtrcpno.Visible = false;
        ddlrcpno.Visible = true;
        if (ddlrcpno.Items.Count == 0)
        {
            con.Open();
            SqlCommand cmdsrno = new SqlCommand("select distinct RNo from patentreceipt", con);
            SqlDataReader drsrno = cmdsrno.ExecuteReader();
            while (drsrno.Read())
            {
                ddlrcpno.Items.Add(drsrno["RNo"].ToString());
            }

            con.Close();
        }
        lvIdf.DataSource = (DataTable)ViewState["rcpt"];
        lvIdf.DataBind();
    }

    protected void ddlrcpno_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlrcpno.SelectedValue != "")
        {
            DataTable dt1 = (DataTable)ViewState["rcpt"];
            dt1.Clear();
            con.Open();
            try
            {
                SqlCommand cmd = new SqlCommand("select * from tbl_primary_receipt where ReceiptNo='" + ddlrcpno.SelectedValue + "'", con);
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    if (!dr.IsDBNull(2)) txtrcpdt.Text = Convert.ToDateTime(dr["ReceiptDt"]).ToString("MM/dd/yyyy"); else txtrcpdt.Text = "";
                    if (!dr.IsDBNull(3)) ddlsrc.SelectedValue = dr["Source"].ToString().Trim(); else ddlsrc.SelectedIndex = 0;
                    if (!dr.IsDBNull(4)) txtsaccno.Text = dr["Accno"].ToString(); else txtsaccno.Text = "";
                    if (!dr.IsDBNull(5)) ddlparty.Text = dr["Party"].ToString(); else ddlparty.SelectedIndex = 0;
                    if (!dr.IsDBNull(6)) txtpartyref.Text = dr["PartyRefNo"].ToString(); else txtpartyref.Text = "";
                    if (!dr.IsDBNull(7)) txtrcpref.Text = dr["ReceiptRef"].ToString(); else txtrcpref.Text = "";
                    if (!dr.IsDBNull(8)) txtrcpdesc.Text = dr["ReceiptDesc"].ToString(); else txtrcpdesc.Text = "";
                    if (!dr.IsDBNull(9)) txtamt.Text = dr["AmountINR"].ToString(); else txtamt.Text = "";
                    if (!dr.IsDBNull(10)) txtintref.Text = dr["IntimationRef"].ToString(); else txtintref.Text = "";
                    if (!dr.IsDBNull(11)) txtintdt.Text = Convert.ToDateTime(dr["IntimationDt"]).ToString("MM/dd/yyyy"); else txtintdt.Text = "";
                    if (!dr.IsDBNull(12)) txtcmt.Text = dr["Comment"].ToString(); else txtcmt.Text = "";
                    if (!dr.IsDBNull(13)) txtipaccno.Text = dr["IPAccno"].ToString(); else txtipaccno.Text = "";
                    if (!dr.IsDBNull(14)) txttransamt.Text = dr["TransferAmt"].ToString(); else txttransamt.Text = "";
                    if (!dr.IsDBNull(15)) txttransdt.Text = Convert.ToDateTime(dr["TransferDt"]).ToString("MM/dd/yyyy"); else txttransdt.Text = "";
                }
                dr.Close();
                DataTable dt = (DataTable)ViewState["rcpt"];
                SqlCommand cmddt = new SqlCommand("select * from tbl_sec_receiptfileno where ReceiptNo='" + ddlrcpno.SelectedValue + "'", con);
                SqlDataReader dr1 = cmddt.ExecuteReader();
                while (dr1.Read())
                {
                    DataRow drow = dt.NewRow();
                    drow["ReceiptNo"] = dr1.GetString(1);
                    drow["SlNo"] = dr1.GetInt32(2);
                    drow["FileNo"] = dr1.GetString(3);
                    drow["Title"] = dr1.GetString(4);
                    drow["Rgroup"] = dr1.GetString(5);
                    drow["SplitAmtINR"] = dr1.GetDecimal(6);
                    drow["Remarks"] = dr1.GetString(7);
                    dt.Rows.Add(drow);
                }
                con.Close();
                dt.AcceptChanges();
                ViewState["rcpt"] = dt;
                lvIdf.DataSource = dt;
                lvIdf.DataBind();
                dr1.Close();

            }
            catch (Exception ex)
            {
                ClientScript.RegisterStartupScript(GetType(), "Error", "<script>alert('" + ex.Message.ToString() + "')</script>");
                if (con.State == ConnectionState.Open)
                    con.Close();
            }
        }
    }

    protected void ddlsrc_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlsrc.SelectedValue == "TT")
        {
            autocompleteex1.Enabled = true;
        }
    }
    [System.Web.Script.Services.ScriptMethod()]
    [System.Web.Services.WebMethod]
    public static List<string> searchttaccno(string prefixText, int count)
    {
        SqlConnection con1 = new SqlConnection();
        con1.ConnectionString = ConfigurationManager.ConnectionStrings["ICSR2KSCN"].ConnectionString;
        SqlCommand cmdtt = new SqlCommand("select distinct APRLNO from FoxOffice..ttmaster where APRLNO like '" + prefixText + "'+'%'", con1);
        con1.Open();
        List<string> accno = new List<string>();
        using (SqlDataReader dr = cmdtt.ExecuteReader())
        {
            while (dr.Read())
            {
                accno.Add(dr["APRLNO"].ToString());
            }
        }
        con1.Close();
        return accno;
    }
}
