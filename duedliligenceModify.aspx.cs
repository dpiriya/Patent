using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class duedliligenceModify : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection();
    protected void Page_Load(object sender, EventArgs e)
    {
        con.ConnectionString = ConfigurationManager.ConnectionStrings["PATENTCN"].ConnectionString;
        if (!IsPostBack)
        {
            con.Open();
            SqlCommand cmdidf = new SqlCommand("select distinct FileNo from patdetails", con);
            SqlDataReader dr = cmdidf.ExecuteReader();
            while (dr.Read())
            {
                ddlidf.Items.Add(new ListItem(dr.GetString(0)));
            }
            dr.Close();
            SqlCommand cmdrpt = new SqlCommand("select itemList,ItemText from ListItemMaster where Category='DueDiligence' and Grouping='ReportType'", con);

                SqlDataReader dr1 = cmdrpt.ExecuteReader();
                ddlrpt.Items.Add("");
                while (dr1.Read())
                {
                    ddlrpt.Items.Add(new ListItem() { Text = dr1.GetString(0), Value = dr1.GetString(1) });
                }
                dr1.Close();
                //SqlCommand cmdidf1 = new SqlCommand("select distinct FileNo from patdetails", con);
                //dr1 = cmdidf1.ExecuteReader();
                //while (dr1.Read())
                //{
                //    ddlidf.Items.Add(new ListItem(dr1.GetString(0)));
                //}
                //dr1.Close();
                ddlmode.Items.Add("External");
                ddlmode.Items.Add("Inhouse");
                SqlCommand cmdattorney = new SqlCommand("select attorneyname from attorney order by attorneyname", con);
                dr1 = cmdattorney.ExecuteReader();
                ddlallocation.Items.Add("");
                while (dr1.Read())
                {
                    ddlallocation.Items.Add(new ListItem(dr1.GetString(0)));
                }
                dr1.Close();
            
            
            con.Close();
        }
    }

    protected void btnFind_Click(object sender, EventArgs e)
    {
        panelList.Visible = true;
        panUpdate.Visible = false;
        DataTable dtAction = new DataTable();
        string sql2 = "select Sno,FileNo,ReportType,Mode,Allocation,RequestDt,ReportDt,Comment,FileName from tbl_trx_duediligence where FileNo='" + ddlidf.SelectedItem.ToString() + "'";
        SqlDataAdapter sda = new SqlDataAdapter(sql2, con);
        sda.Fill(dtAction);
        lvSearch.DataSource = dtAction;
        lvSearch.DataBind();
        sda.Dispose();


    }
    protected void lvSearch_ItemCommand(object sender, ListViewCommandEventArgs e)
    {
        Label lsno = (Label)e.Item.FindControl("lblSNo");
        if (e.CommandName == "Modify")
        {
            panUpdate.Visible = true;
            con.Open();
            
            System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-GB");
            SqlCommand cmdedit = new SqlCommand("SELECT sno,EntryDt,RequestDt,ReportDt,ReportType,Mode,Allocation,Participants,IPCCode,TechnologyAction,Summary,Comment,InventorInput,Followup,srno,FileName from tbl_trx_duediligence where FileNo='" + ddlidf.SelectedValue + "' and Sno='"+lsno.Text+"'", con);
            SqlDataReader dr = cmdedit.ExecuteReader();           
            if (dr.Read())
            {
                txtSno.Text = (!dr.IsDBNull(0)) ? dr.GetInt32(0).ToString() : "";
                txtentrydt.Text = (!dr.IsDBNull(1)) ? dr.GetDateTime(1).ToString("MM/dd/yyyy") : "";
                txtrequestdt.Text = (!dr.IsDBNull(2)) ? dr.GetDateTime(2).ToString("MM/dd/yyyy") : "";
                txtrptdt.Text = (!dr.IsDBNull(3)) ? dr.GetDateTime(3).ToString("MM/dd/yyyy") : "";
                if (!dr.IsDBNull(4)) ddlrpt.SelectedItem.Text = dr.GetString(4); else ddlrpt.SelectedIndex = 0;
                if (!dr.IsDBNull(5)) ddlmode.SelectedValue = dr.GetString(5); else ddlmode.SelectedIndex = 0;
                if (ddlmode.SelectedValue == "External")
                {
                    txtallocation.Visible = false;
                    ddlallocation.Visible = true;
                    if (!dr.IsDBNull(6)) ddlallocation.SelectedItem.Text = dr.GetString(6); else ddlallocation.SelectedIndex = 0;
                }
                else if(ddlmode.SelectedValue=="Inhouse")
                {
                    ddlallocation.Visible = false;
                    txtallocation.Visible = true;
                    if (!dr.IsDBNull(6)) txtallocation.Text = dr.GetString(6); else txtallocation.Text= "";
                }
                txtparticipants.Text = (!dr.IsDBNull(7)) ? dr.GetString(7) : "";
                txtipc.Text = (!dr.IsDBNull(8)) ? dr.GetString(8) : "";
                txtaction.Text = (!dr.IsDBNull(9)) ? dr.GetString(9) : "";
                txtsummary.Text = (!dr.IsDBNull(10)) ? dr.GetString(10) : "";
                txtcomment.Text = (!dr.IsDBNull(11)) ? dr.GetString(11) : "";
                txtinput.Text = (!dr.IsDBNull(12)) ? dr.GetString(12) : "";
                txtfollowup.Text = (!dr.IsDBNull(13)) ? dr.GetString(13) : "";
                txtsrno.Text = (!dr.IsDBNull(14)) ? dr.GetString(14) : "";

            }
            else
            {
                ClientScript.RegisterStartupScript(GetType(), "File Number Is Not Found", "<script>alert('Contract Number Is Not Found')</script>");
                dr.Close();
                con.Close();
                return;
            }
            dr.Close();
            con.Close();
        }

        if (e.CommandName == "View")
        {
            LinkButton fn = (LinkButton)e.Item.FindControl("lbtnFileName");

            try
            {
                Label sno = (Label)e.Item.FindControl("lblSNo");
                con.Open();
                SqlCommand cmdpath = new SqlCommand("select FilePath from tbl_trx_duediligence where FileNo='" + ddlidf.SelectedValue + "' and Sno='" + sno.Text + "'", con);
                string path = cmdpath.ExecuteScalar().ToString();
                string file = path + fn.Text;

                if (System.IO.File.Exists(file))
                {
                    string ext = System.IO.Path.GetExtension(file);
                    FileStream fs = new FileStream(file, FileMode.Open, FileAccess.Read);
                    byte[] bytBytes = new byte[fs.Length];
                    fs.Read(bytBytes, 0, (int)fs.Length);
                    fs.Close();
                    Response.AppendHeader("Content-Disposition", "attachment;filename=" + fn.Text);
                    Response.ContentType = ext;
                    Response.BinaryWrite(bytBytes);
                    Response.End();
                }
                con.Close();
            }
            catch (Exception ex)
            {
                ClientScript.RegisterStartupScript(GetType(), "Information", "<script>alert('" + ex.ToString() + "')</script>");
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
            }

        }
        if (e.CommandName == "Upload")
        {
            Label sno = (Label)e.Item.FindControl("lblSNo");
            Label rpt = (Label)e.Item.FindControl("lbltype");
            string name="";
            try
            {
                con.Open();
                SqlCommand abb = new SqlCommand("select top 1 ItemText from ListItemMaster where ItemList='" + rpt.Text + "'", con);
                name=abb.ExecuteScalar().ToString();
                con.Close();
            }
            catch
            {
                name = rpt.Text;
                con.Close();
            }
            FileUpload fp = (FileUpload)e.Item.FindControl("fp1");
            if (fp.HasFiles)
            {
                string path = @"F:\\PatentDocument\\" + ddlidf.SelectedValue.Trim();
                string ext = System.IO.Path.GetExtension(fp.PostedFile.FileName);
                if (Directory.Exists(path))
                {
                    path += "\\DueDiligence\\";
                    string filename = ddlidf.SelectedValue.Trim() + "_" + name+sno.Text + ext;
                    string fn = path + filename;

                    if (Directory.Exists(path))
                    {
                        if (File.Exists(fn))
                        {
                            File.Delete(fn);
                            fp.SaveAs(fn);
                        }
                        else
                        {
                            fp.SaveAs(fn);
                        }
                    }
                    else
                    {
                        Directory.CreateDirectory(path);
                        fp.SaveAs(fn);
                    }
                    con.Open();
                    SqlCommand cmdfnupdate = new SqlCommand("update tbl_trx_duediligence set FilePath='" + path + "',FileName='" + filename + "' where FileNo='" + ddlidf.SelectedValue + "' and Sno='" + sno.Text + "'", con);
                    cmdfnupdate.ExecuteNonQuery();                    
                    DataTable dtAction = new DataTable();
                    string sql2 = "select Sno,FileNo,ReportType,Mode,Allocation,RequestDt,ReportDt,Comment,FileName from tbl_trx_duediligence where FileNo='" + ddlidf.SelectedItem.ToString() + "'";
                    SqlDataAdapter sda = new SqlDataAdapter(sql2, con);
                    sda.Fill(dtAction);
                    lvSearch.DataSource = dtAction;
                    lvSearch.DataBind();
                    sda.Dispose();
                    con.Close();
                }
            }
        }
        if (e.CommandName == "DelFile")
        {
            Label lbl = (Label)e.Item.FindControl("lblSNo");
            int sno = Convert.ToInt32(lbl.Text.Trim());
            LinkButton lbf = (LinkButton)e.Item.FindControl("lbtnFileName");
            if(lbf.Text!=null)
            {
                SqlCommand cmddelFile = new SqlCommand("select FilePath+FileName from tbl_trx_duediligence where FileNo='" + ddlidf.SelectedValue + "' and Sno='" + sno + "'", con);
                con.Open();
                string fn = cmddelFile.ExecuteScalar().ToString();                

                    if (System.IO.File.Exists(fn))
                    {
                        System.IO.File.Delete(fn);
                        ClientScript.RegisterStartupScript(GetType(), "Information", "<script>alert('File Deleted')</script>");
                    SqlCommand cmdup =new SqlCommand("update tbl_trx_duediligence set FileName='', FilePath='' where FileNo='" + ddlidf.SelectedValue + "' and Sno='" + sno + "'", con);
                    cmdup.ExecuteNonQuery();
                    DataTable dtAction = new DataTable();
                    string sql2 = "select Sno,FileNo,ReportType,Allocation,RequestDt,ReportDt,Comment,FileName from tbl_trx_duediligence where FileNo='" + ddlidf.SelectedItem.ToString() + "'";
                    SqlDataAdapter sda = new SqlDataAdapter(sql2, con);
                    sda.Fill(dtAction);
                    lvSearch.DataSource = dtAction;
                    lvSearch.DataBind();
                    sda.Dispose();
                    
                }
                
                con.Close();
            }
        }
    }
    protected void lvSearch_ItemDataBound(object sender, ListViewItemEventArgs e)
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
    protected void lvSearch_ItemDeleting(object sender, ListViewDeleteEventArgs e)
    {
        try
        {
            ListViewDataItem lvd = lvSearch.Items[e.ItemIndex];
            Label lbl = (Label)lvd.FindControl("lblSNo");
            int sno = Convert.ToInt32(lbl.Text.Trim());
            con.Open();
            SqlCommand cmddelFile = new SqlCommand("select FilePath+FileName from tbl_trx_duediligence where FileNo='" + ddlidf.SelectedValue + "' and Sno='" + sno + "'", con);
            string fn = cmddelFile.ExecuteScalar().ToString();
            SqlCommand cmddel = new SqlCommand("delete from tbl_trx_duediligence where FileNo='" + ddlidf.SelectedValue + "' and Sno='" + sno + "'", con);
            if (cmddel.ExecuteNonQuery() == 1)
            {

                if (System.IO.File.Exists(fn))
                {
                    System.IO.File.Delete(fn);
                    ClientScript.RegisterStartupScript(GetType(), "Information", "<script>alert('File Deleted')</script>");                   
                }
            }
            con.Close();
        }
        catch (Exception ex)
        {
            ClientScript.RegisterStartupScript(GetType(), "File Number and Sno Is Not Found", "<script>alert('Contract Number Is Not Found')</script>");
            con.Close();
        }
    }
    protected void ddlmode_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlmode.SelectedValue == "External")
        {
            txtallocation.Visible = false;
            ddlallocation.Visible = true;
            if (ddlallocation.Items.Count == 0)
            {
                SqlDataReader dr;
                con.Open();
                SqlCommand cmdattorney = new SqlCommand("select attorneyname from attorney order by attorneyname", con);
                dr = cmdattorney.ExecuteReader();
                ddlallocation.Items.Add("");
                while (dr.Read())
                {
                    ddlallocation.Items.Add(new ListItem(dr.GetString(0)));
                }
                dr.Close();
                con.Close();
            }
        }
        else
        {
            ddlallocation.Visible = false;
            txtallocation.Visible = true;
        }
    }

    protected void btnActUpdate_Click(object sender, EventArgs e)
    {
        try
        {
            con.Open();
            System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-GB");
            SqlCommand cmdupdate = new SqlCommand("update tbl_trx_duediligence set EntryDt='" + txtentrydt.Text + "',Srno='" + txtsrno.Text + "',RequestDt='" + txtrequestdt.Text + "',ReportDt='" + txtrptdt.Text + "',ReportType='" + ddlrpt.SelectedItem.Text + "',Mode='" + ddlmode.SelectedValue + "',Allocation=case when '" + ddlmode.SelectedValue+"'='External' then '" + ddlallocation.SelectedItem.Text + "' else '"+txtallocation.Text+"' end,Participants='" + txtparticipants.Text + "',IPCCode='" + txtipc.Text + "',TechnologyAction='" + txtaction.Text + "',Summary='" + txtsummary.Text + "',Comment='" + txtcomment.Text + "',InventorInput='" + txtinput.Text + "',Followup='" + txtfollowup.Text + "' where FileNo='" + ddlidf.SelectedValue.Trim() + "' and sno='" + txtSno.Text + "'", con);
            cmdupdate.ExecuteNonQuery();
            ClientScript.RegisterStartupScript(GetType(), "Information", "<script>alert('Updated Successfully')</script>");
            con.Close();
        }
        catch (Exception ex)
        {
            ClientScript.RegisterStartupScript(GetType(), "Information", "<script>alert('" + ex.ToString() + "')</script>");
            con.Close();
        }
    }

    protected void btnClose_Click(object sender, EventArgs e)
    {
        panUpdate.Visible = false;
    }
}