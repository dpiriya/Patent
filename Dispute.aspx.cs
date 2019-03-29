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
using System.IO;

public partial class Dispute : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection();
    SqlTransaction trans;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (User.Identity.IsAuthenticated)
        {
            if (User.IsInRole("Admin") || User.IsInRole("Super User") || User.IsInRole("Marketing") || !User.IsInRole("Intern"))
            {
                if (!this.IsPostBack)
                {
                    System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-GB");
                    txtRefNo.Text = DisputeNo();
                    txtcreatedDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                    SqlCommand cmd = new SqlCommand("select itemList from listItemMaster where category = 'Dispute' and Grouping='DisputeGroup' order by itemlist", con);
                    con.ConnectionString = ConfigurationManager.ConnectionStrings["PATENTCN"].ConnectionString;
                    con.Open();
                    SqlDataReader dr = cmd.ExecuteReader();
                    ddlGroup.DataTextField = "itemList";
                    ddlGroup.DataValueField = "itemList";
                    ddlGroup.DataSource = dr;
                    ddlGroup.DataBind();
                    ddlGroup.Items.Insert(0, new ListItem("", ""));
                    dr.Close();
                    SqlCommand cmd1 = new SqlCommand("select itemList from listItemMaster where category = 'Dispute' and Grouping='Status' order by itemlist", con);
                    dr = cmd1.ExecuteReader();
                    ddlStatus.DataTextField = "itemList";
                    ddlStatus.DataValueField = "itemList";
                    ddlStatus.DataSource = dr;
                    ddlStatus.DataBind();
                    dr.Close();
                    con.Close();
                    MarketingDS ds = new MarketingDS();
                    con.ConnectionString = ConfigurationManager.ConnectionStrings["PATENTCN"].ConnectionString;
                    con.Open();
                    LvMDoc.DataSource = ds.Tables["MDOC"];
                    LvMDoc.DataBind();
                    ViewState["MDOC"] = ds.Tables["MDOC"];
                    lvIdf.DataSource = ds.Tables["MarketIDF"];
                    lvIdf.DataBind();
                    ViewState["MktIDF"] = ds.Tables["MarketIDF"];
                    lvActivity.DataSource = ds.Tables["DisputeActivity"];
                    lvActivity.DataBind();
                    ViewState["DisputeActivity"] = ds.Tables["DisputeActivity"];
                    con.Close();
                    //imgBtnSubmit.Visible = (!User.IsInRole("View"));
                }
            }
            else
            {
                Server.Transfer("Unauthorized.aspx");
            }
        }
    }
    protected string DisputeNo()
    {
        con.ConnectionString = ConfigurationManager.ConnectionStrings["PATENTCN"].ConnectionString;
        con.Open();
        SqlCommand cmd1 = new SqlCommand();
        string sql1 = "SELECT MAX(CONVERT(BIGINT,SUBSTRING(DisputeNo,3,LEN(DisputeNo)))) FROM tbl_trx_dispute";
        cmd1.CommandText = sql1;
        cmd1.CommandType = CommandType.Text;
        cmd1.Connection = con;
        string pro = Convert.ToString(cmd1.ExecuteScalar());
        if (pro == "")
        {
            pro = "DIS1";
        }
        else
        {
            pro = "DIS" + (Convert.ToInt32(pro) + 1).ToString();
        }
        con.Close();
        return pro;
    }
    [WebMethod]
    protected List<string> GetCoor(string Coor)
    {
        List<string> result = new List<string>();
        SqlConnection cnp = new SqlConnection();
        cnp.ConnectionString = ConfigurationManager.ConnectionStrings["PATENTCN"].ConnectionString;
        SqlCommand cmd = new SqlCommand();
        string sql = "select companyName from companyMaster where companyName like '%'+ @SearchText +'%' group by companyName order by companyName";
        SqlDataReader dr;
        cmd.CommandType = CommandType.Text;
        cmd.Parameters.AddWithValue("@SearchText", Coor);
        cmd.Connection = cnp;
        cmd.CommandText = sql;
        cnp.Open();
        dr = cmd.ExecuteReader();
        while (dr.Read())
        {
            result.Add(dr["companyName"].ToString());
        }
        con.Close();
        return result;
    }
    protected void imgBtnSubmit_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-GB");
            con.ConnectionString = ConfigurationManager.ConnectionStrings["PATENTCN"].ConnectionString;
            con.Open();
            SqlCommand cmd = new SqlCommand("insert into tbl_trx_dispute (DisputeNo,DGroup,Title,EstimatedValue,PartyName,RealizationValue,Status,Remarks,CreatedOn,CreatedBy) values ('" + txtRefNo.Text + "','" + ddlGroup.SelectedValue + "','" + txtTitle.Text + "','" + txtValue.Text + "','" + atxtParty.Text + "','" + txtRealization.Text + "','" + atxtCoor.Text + "','" + txtRemark.Text + "','" + ddlStatus.SelectedValue + "','" + User.Identity + "','" + DateTime.Now + "'", con);
            trans = con.BeginTransaction();
            cmd.Transaction = trans;
            cmd.ExecuteNonQuery();
            trans.Commit();
            ClientScript.RegisterStartupScript(GetType(), "success", "<script>alert('This Record successfully Added')</script>");
            con.Close();

        }
        catch (Exception ex)
        {
            trans.Rollback();
            ClientScript.RegisterStartupScript(GetType(), "Error", "<script>alert('" + ex.Message.ToString() + "')</script>");
            con.Close();
            return;
        }
        imgBtnClear_Click(sender, e);
    }
    protected void imgBtnClear_Click(object sender, ImageClickEventArgs e)
    {
        txtRefNo.Text = DisputeNo();
        txtTitle.Text = "";
        atxtParty.Text = "";
        txtValue.Text = "";
        txtRealization.Text = "";
        ddlGroup.Text = "";
        atxtCoor.Text = "";
        ddlStatus.Text = "";
        DataTable dt = (DataTable)ViewState["MktIDF"];
        dt.Clear();
        //lvIdf.DataSource = dt;
        //lvIdf.DataBind();
        //DataTable dt2 = (DataTable)ViewState["MDOC"];
        //dt2.Clear();
        //LvMDoc.DataSource = dt2;
        //LvMDoc.DataBind();
        //DataTable dt1 = (DataTable)ViewState["DisputeActivity"];
        //dt1.Clear();
        //lvActivity.DataSource = dt1;
        //lvActivity.DataBind();       
    }





    protected void LvMDoc_ItemCreated(object sender, ListViewItemEventArgs e)
    {
        DropDownList ddl = (DropDownList)e.Item.FindControl("ddlNewMdocNo");
        if (ddl != null)
        {
            if (con.State == ConnectionState.Closed)
            {
                con.ConnectionString = ConfigurationManager.ConnectionStrings["PATENTCN"].ConnectionString;
                con.Open();
            }
            SqlCommand cmd = new SqlCommand();
            string sql = "select distinct ContractNo from Agreement";
            SqlDataReader dr;

            cmd.CommandType = CommandType.Text;
            cmd.Connection = con;
            cmd.CommandText = sql;
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                ddl.Items.Add(dr["ContractNo"].ToString());
            }
            ddl.Items.Insert(0, new ListItem("", ""));
            dr.Close();
            con.Close();
        }
    }

    protected void ddlNewMdocNo_SelectedIndexChanged(object sender, EventArgs e)
    {
        DropDownList ddlIdf = (DropDownList)sender;
        ListViewItem item1 = (ListViewItem)ddlIdf.NamingContainer;
        DropDownList ddlMdocGet = (DropDownList)item1.FindControl("ddlNewMdocNo");
        if (ddlMdocGet.SelectedValue != "")
        {
            con.ConnectionString = ConfigurationManager.ConnectionStrings["PATENTCN"].ConnectionString;
            SqlCommand cmd = new SqlCommand();
            string sql = "select Title,CoordinatingPerson,Status from Agreement where ContractNo='" + ddlMdocGet.SelectedValue + "'";
            SqlDataReader dr;
            cmd.CommandType = CommandType.Text;
            cmd.Connection = con;
            cmd.CommandText = sql;
            con.Open();
            dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                TextBox txt = (TextBox)item1.FindControl("txtTitle");
                txt.Text = dr.GetString(0);
                txt = (TextBox)item1.FindControl("txtOwner");
                txt.Text = dr.GetString(1);
                txt = (TextBox)item1.FindControl("txtStatus");
                txt.Text = dr.GetString(2);
            }
            con.Close();
        }
    }

    protected void LvMDoc_ItemDataBound(object sender, ListViewItemEventArgs e)
    {

    }

    protected void LvMDoc_ItemCanceling(object sender, ListViewCancelEventArgs e)
    {
        LvMDoc.DataSource = (DataTable)ViewState["MDOC"];
        LvMDoc.DataBind();
    }

    protected void LvMDoc_ItemDeleting(object sender, ListViewDeleteEventArgs e)
    {
        Label lbl = LvMDoc.Items[e.ItemIndex].FindControl("lblSlNo") as Label;
        DataTable dt = (DataTable)ViewState["MDOC"];
        int cnt = 0;
        foreach (DataRow dr in dt.Rows)
        {
            cnt += 1;
            if (dr["SNO"].ToString() == lbl.Text.Trim())
            {
                dr.Delete();
                break;
            }
        }
        for (int i = cnt - 1; i <= dt.Rows.Count - 1; i++)
        {
            dt.Rows[i][0] = i + 1;
        }
        LvMDoc.DataSource = (DataTable)ViewState["MDOC"];
        LvMDoc.DataBind();
    }

    protected void LvMDoc_ItemInserting(object sender, ListViewInsertEventArgs e)
    {
        string slno = Convert.ToString(LvMDoc.Items.Count + 1);
        string mdoc = "";
        string title = "";
        string inventor1 = "";
        string status = "";
        DropDownList ddl = e.Item.FindControl("ddlNewMdocNo") as DropDownList;
        mdoc = ddl.SelectedValue;
        if (mdoc != "")
        {
            TextBox txt;
            txt = e.Item.FindControl("txtTitle") as TextBox;
            title = txt.Text;
            txt = e.Item.FindControl("txtOwner") as TextBox;
            inventor1 = txt.Text;
            txt = e.Item.FindControl("txtStatus") as TextBox;
            status = txt.Text;


            DataTable dt = (DataTable)ViewState["MDOC"];
            DataRow dr = dt.NewRow();
            dr["SNO"] = slno;
            dr["ContractNo"] = mdoc;
            dr["Title"] = title;
            dr["CoordinatingPerson"] = inventor1;
            dr["Status"] = status;

            dt.Rows.Add(dr);
            LvMDoc.DataSource = (DataTable)ViewState["MDOC"];
            LvMDoc.DataBind();

        }
    }

    protected void lvIdf_ItemCreated(object sender, ListViewItemEventArgs e)
    {
        DropDownList ddl = (DropDownList)e.Item.FindControl("ddlNewIdfNo");
        if (ddl != null)
        {
            con.ConnectionString = ConfigurationManager.ConnectionStrings["PATENTCN"].ConnectionString;
            SqlCommand cmd = new SqlCommand();
            string sql = "select fileno from patdetails order by fileno";
            SqlDataReader dr;
            cmd.CommandType = CommandType.Text;
            cmd.Connection = con;
            cmd.CommandText = sql;
            con.Open();
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                ddl.Items.Add(dr["fileno"].ToString());
            }
            ddl.Items.Insert(0, new ListItem("", ""));
            con.Close();
        }
    }

    protected void lvIdf_ItemDataBound(object sender, ListViewItemEventArgs e)
    {

    }

    protected void lvIdf_ItemCanceling(object sender, ListViewCancelEventArgs e)
    {
        lvIdf.DataSource = (DataTable)ViewState["MktIDF"];
        lvIdf.DataBind();
    }

    protected void lvIdf_ItemDeleting(object sender, ListViewDeleteEventArgs e)
    {
        Label lbl = lvIdf.Items[e.ItemIndex].FindControl("lblSlNo") as Label;
        DataTable dt = (DataTable)ViewState["MktIDF"];
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
        lvIdf.DataSource = (DataTable)ViewState["MktIDF"];
        lvIdf.DataBind();
    }

    protected void lvIdf_ItemInserting(object sender, ListViewInsertEventArgs e)
    {
        string slno = Convert.ToString(lvIdf.Items.Count + 1);
        string ldf = "";
        string title = "";
        string inventor1 = "";
        string applcn_no = "";
        string status = "";
        DropDownList ddl = e.Item.FindControl("ddlNewIdfNo") as DropDownList;
        ldf = ddl.SelectedValue;
        if (ldf != "")
        {
            TextBox txt;
            txt = e.Item.FindControl("txtTitle") as TextBox;
            title = txt.Text;
            txt = e.Item.FindControl("txtInventor") as TextBox;
            inventor1 = txt.Text;
            txt = e.Item.FindControl("txtApplcnNo") as TextBox;
            applcn_no = txt.Text;
            txt = e.Item.FindControl("txtStatus") as TextBox;
            status = txt.Text;

            DataTable dt = (DataTable)ViewState["MktIDF"];
            DataRow dr = dt.NewRow();
            dr["SlNo"] = slno;
            dr["fileno"] = ldf;
            dr["title"] = title;
            dr["inventor1"] = inventor1;
            dr["applcn_no"] = applcn_no;
            dr["status"] = status;
            dt.Rows.Add(dr);
            lvIdf.DataSource = (DataTable)ViewState["MktIDF"];
            lvIdf.DataBind();
        }
    }

    protected void ddlNewIdfNo_SelectedIndexChanged(object sender, EventArgs e)
    {
        DropDownList ddlIdf = (DropDownList)sender;
        ListViewItem item1 = (ListViewItem)ddlIdf.NamingContainer;
        DropDownList ddlIdfGet = (DropDownList)item1.FindControl("ddlNewIdfNo");
        if (ddlIdfGet.SelectedValue != "")
        {
            con.ConnectionString = ConfigurationManager.ConnectionStrings["PATENTCN"].ConnectionString;
            SqlCommand cmd = new SqlCommand();
            string sql = "select title,inventor1,applcn_no,status from patdetails where fileno='" + ddlIdfGet.SelectedValue + "'";
            SqlDataReader dr;
            cmd.CommandType = CommandType.Text;
            cmd.Connection = con;
            cmd.CommandText = sql;
            con.Open();
            dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                TextBox txt = (TextBox)item1.FindControl("txtTitle");
                txt.Text = dr.GetString(0);
                txt = (TextBox)item1.FindControl("txtInventor");
                txt.Text = dr.GetString(1);
                txt = (TextBox)item1.FindControl("txtApplcnNo");
                if (!dr.IsDBNull(2))
                {
                    txt.Text = dr.GetString(2);
                }
                else
                {
                    txt.Text = "";
                }
                txt = (TextBox)item1.FindControl("txtStatus");
                txt.Text = dr.GetString(3);

            }
            con.Close();
        }
    }

    protected void lvActivity_ItemCanceling(object sender, ListViewCancelEventArgs e)
    {
        System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-GB");
        lvActivity.EditIndex = -1;
        lvActivity.DataSource = (DataTable)ViewState["DisputeActivity"];
        lvActivity.DataBind();
    }

    protected void lvActivity_ItemDeleting(object sender, ListViewDeleteEventArgs e)
    {
        System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-GB");
        Label lbl = lvActivity.Items[e.ItemIndex].FindControl("lblSlNo") as Label;
        DataTable dt = (DataTable)ViewState["DisputeActivity"];
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
        lvActivity.DataSource = (DataTable)ViewState["DisputeActivity"];
        lvActivity.DataBind();
    }

    //protected void lvActivity_ItemEditing(object sender, ListViewEditEventArgs e)
    //{
    //    lvActivity.EditIndex = e.NewEditIndex;
    //    System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-GB");
    //    lvActivity.DataSource = (DataTable)ViewState["DisputeActivity"];
    //    lvActivity.DataBind();
    //}

    protected void lvActivity_ItemInserting(object sender, ListViewInsertEventArgs e)
    {
        System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-GB");
        string slno = Convert.ToString(lvActivity.Items.Count + 1);
        string activityDt = "";
        string forum = "";
        string activityType = "";
        string remarks = "";
        string fn;
        TextBox txt;
        txt = e.Item.FindControl("txtActivityDt") as TextBox;
        activityDt = txt.Text;
        txt = e.Item.FindControl("txtChannel") as TextBox;
        forum = txt.Text;
        txt = e.Item.FindControl("txtActivityType") as TextBox;
        activityType = txt.Text;
        txt = e.Item.FindControl("txtActivityDetails") as TextBox;
        remarks = txt.Text;
        //LinkButton lbt = e.Item.FindControl("lbtnFileName") as LinkButton;
        FileUpload fp = e.Item.FindControl("FileUpload1") as FileUpload;
        if (fp.PostedFile.ContentLength > 0)
        {
            fp.SaveAs(Server.MapPath("~/images/" + fp.PostedFile.FileName));

            fn = fp.PostedFile.FileName;

        }
        else
        {
            fn = "File not uploaded";

        }
        if (activityDt != "" && forum != "")
        {
            DataTable dt = (DataTable)ViewState["DisputeActivity"];
            DataRow dr = dt.NewRow();
            dr["slno"] = slno;
            dr["ActivityDt"] = Convert.ToDateTime(activityDt);
            dr["Forum"] = forum;
            dr["ActivityType"] = activityType;
            dr["Remarks"] = remarks;
            dr["FileName"] = fn;
            dt.Rows.Add(dr);
            lvActivity.DataSource = (DataTable)ViewState["DisputeActivity"];
            lvActivity.DataBind();
        }
    }
   
    //protected void lvActivity_ItemUpdating(object sender, ListViewUpdateEventArgs e)
    //{
    //    System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-GB");
    //    string slno = "";
    //    string activityDt = "";
    //    string forum = "";
    //    string activityType = "";
    //    string remarks = "";
    //    Label lbl;
    //    TextBox txt;
    //    lbl = lvActivity.Items[e.ItemIndex].FindControl("lblEdSlNo") as Label;
    //    slno = lbl.Text;
    //    txt = lvActivity.Items[e.ItemIndex].FindControl("txtEdActivityDt") as TextBox;
    //    activityDt = txt.Text;
    //    txt = lvActivity.Items[e.ItemIndex].FindControl("txtEdChannel") as TextBox;
    //    forum = txt.Text;
    //    txt = lvActivity.Items[e.ItemIndex].FindControl("txtEdActivityType") as TextBox;
    //    activityType = txt.Text;
    //    txt = lvActivity.Items[e.ItemIndex].FindControl("txtEdActivityDetails") as TextBox;
    //    remarks = txt.Text;
    //    DataTable dt = (DataTable)ViewState["DisputeActivity"];
    //    foreach (DataRow dr in dt.Rows)
    //    {
    //        if (dr["slno"].ToString() == slno)
    //        {
    //            dr["ActivityDt"] = Convert.ToDateTime(activityDt);
    //            dr["Forum"] = forum;
    //            dr["ActivityType"] = activityType;
    //            dr["Remarks"] = remarks;
    //            dr.AcceptChanges();
    //        }
    //    }
    //    lvActivity.EditIndex = -1;
    //    lvActivity.DataSource = (DataTable)ViewState["DisputeActivity"];
    //    lvActivity.DataBind();
    //}

    protected void lvActivity_ItemCommand(object sender, ListViewCommandEventArgs e)
    {
        if (e.CommandName == "Upload")
        {
            System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-GB");
            string slno = Convert.ToString(lvActivity.Items.Count + 1);
            string activityDt = "";
            string forum = "";
            string activityType = "";
            string remarks = "";
            string fn;
            TextBox txt;
            txt = e.Item.FindControl("txtActivityDt") as TextBox;
            activityDt = txt.Text;
            txt = e.Item.FindControl("txtChannel") as TextBox;
            forum = txt.Text;
            txt = e.Item.FindControl("txtActivityType") as TextBox;
            activityType = txt.Text;
            txt = e.Item.FindControl("txtActivityDetails") as TextBox;
            remarks = txt.Text;
            
            FileUpload fp = (FileUpload)e.Item.FindControl("fp1");
            if (fp.HasFiles)
            {
                string path = @"F:\\PatentDocument\\Dispute\\";
                string ext = System.IO.Path.GetExtension(fp.PostedFile.FileName);
                if (Directory.Exists(path))
                {                    
                    //string filename = ;
                    //string fn = path + filename;

                    //if (Directory.Exists(path))
                    //{
                    //    if (File.Exists(fn))
                    //    {
                    //        File.Delete(fn);
                    //        fp.SaveAs(fn);
                    //    }
                    //    else
                    //    {
                    //        fp.SaveAs(fn);
                    //    }
                    //}
                    //else
                    //{
                    //    Directory.CreateDirectory(path);
                    //    fp.SaveAs(fn);
                    //}
                    //con.Open();
                    //SqlCommand cmdfnupdate = new SqlCommand("update tbl_trx_duediligence set FilePath='" + path + "',FileName='" + filename + "' where FileNo='" + ddlidf.SelectedValue + "' and Sno='" + sno.Text + "'", con);
                    //cmdfnupdate.ExecuteNonQuery();
                    //DataTable dtAction = new DataTable();
                    //string sql2 = "select Sno,FileNo,ReportType,Mode,Allocation,RequestDt,ReportDt,Comment,FileName from tbl_trx_duediligence where FileNo='" + ddlidf.SelectedItem.ToString() + "'";
                    //SqlDataAdapter sda = new SqlDataAdapter(sql2, con);
                    //sda.Fill(dtAction);
                    //lvSearch.DataSource = dtAction;
                    //lvSearch.DataBind();
                    //sda.Dispose();
                    //con.Close();
                }
            }
        }
    }

    protected void lvActivity_ItemDataBound(object sender, ListViewItemEventArgs e)
    {
        LinkButton fn = (LinkButton)e.Item.FindControl("lbtnFileName");
        if (fn.Text == "")
        {
            FileUpload fp = (FileUpload)e.Item.FindControl("fp1");
            fp.Visible = true;
            ImageButton ig = (ImageButton)e.Item.FindControl("lbtnup");
            ig.Visible = true;
        }
        else
        {
            ImageButton ig = (ImageButton)e.Item.FindControl("lbtndel");
            ig.Visible = true;
        }
    }
}

