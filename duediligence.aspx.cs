using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class duediligence : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection();
    protected void Page_Load(object sender, EventArgs e)
    {
        con.ConnectionString = ConfigurationManager.ConnectionStrings["PATENTCN"].ConnectionString;
        if (!IsPostBack)
        {
            title.Text = "New Entry";
            SqlCommand cmdrpt = new SqlCommand("select itemList,ItemText from ListItemMaster where Category='DueDiligence' and Grouping='ReportType'", con);
            con.Open();
            SqlDataReader dr = cmdrpt.ExecuteReader();
            ddlrpt.Items.Add("");
            while (dr.Read())
            {
                ddlrpt.Items.Add(new ListItem(){ Text = dr.GetString(0), Value = dr.GetString(1)});
            }
            dr.Close();
            SqlCommand cmdidf = new SqlCommand("select distinct FileNo from patdetails", con);
            dr = cmdidf.ExecuteReader();
            while (dr.Read())
            {
                ddlidfno.Items.Add(new ListItem(dr.GetString(0)));
            }
            dr.Close();
            ddlmode.Items.Add("Inhouse");
            ddlmode.Items.Add("External");
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
    
    
    protected void imgBtnSubmit_Click(object sender, ImageClickEventArgs e)
    {
        string path = @"F:\\PatentDocument\\" + ddlidfno.SelectedValue.Trim();
        try
        {
            if (this.fp1.HasFile)
            {
                string ext = System.IO.Path.GetExtension(this.fp1.PostedFile.FileName);
                if (Directory.Exists(path))
                {
                    path += "\\DueDiligence\\";
                    string fn = path + ddlidfno.SelectedValue.Trim() + "_" + ddlrpt.SelectedValue.Trim() + ext;
                    if (Directory.Exists(path))
                    {
                        this.fp1.SaveAs(fn);
                    }
                    else
                    {
                        Directory.CreateDirectory(path);
                        this.fp1.SaveAs(fn);
                    }
                    try
                    {
                        con.Open();
                        SqlCommand cmdsno = new SqlCommand("select * from tbl_trx_duediligence where FileNo='" + ddlidfno.SelectedValue.Trim() + "'", con);
                        int sno = 1;
                        if( cmdsno.ExecuteScalar()!=null)
                        {
                            sno =Convert.ToInt32(cmdsno.ExecuteScalar());
                        }                            
                        SqlCommand cmdsave = new SqlCommand("insert into tbl_trx_duediligence (Sno,FileNo,EntryDt,RequestDt,ReportDt,ReportType,mode,Allocation,Participants,IPCCode,TechnologyAction,Summary,Comment,InventorInput,Followup,CreatedOn,CreatedBy) values('"+sno+"','" + ddlidfno.SelectedValue + "','" + txtentrydt.Text + "','" + txtrequestdt.Text + "','" + txtrptdt.Text + "','" + ddlrpt.SelectedItem.Text + "','" + ddlmode.SelectedValue + "','" + txtallocation.Text + "','" + txtparticipants.Text + "','" + txtipc.Text + "','" + txtaction.Text + "','" + txtsummary.Text + "','" + txtcomment.Text + "','" + txtinput.Text + "','" + txtfollowup.Text + "','" + DateTime.Now + "','" + User.Identity.Name + "')", con);
                        cmdsave.ExecuteNonQuery();
                        ClientScript.RegisterStartupScript(GetType(), "Information", "<script>alert('Inserted Successfully')</script>");
                        con.Close();
                    }
                    catch (Exception ex)
                    {
                        if (System.IO.File.Exists(fn))
                        {
                            System.IO.File.Delete(fn);
                            ClientScript.RegisterStartupScript(GetType(), "Information", "<script>alert('Error Occured'+" + ex + ")</script>");
                            if(con.State==System.Data.ConnectionState.Open)
                            {
                                con.Close();
                            }
                        }
                    }
                }
                else
                {
                    ClientScript.RegisterStartupScript(GetType(), "Information", "<script>alert('Error in choosing the fileno.. Kinldy redo the process')</script>");
                    return;
                }
            }
            else
            {
                ClientScript.RegisterStartupScript(GetType(), "Information", "<script>alert('Select a file to upload')</script>");
                return;
            }
        }
        catch(Exception ex)
        {
            ClientScript.RegisterStartupScript(GetType(), "Information", "<script>alert("+ex.ToString()+")</script>");
            return;
        }
    }

    protected void ddlmode_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlmode.SelectedValue == "External")
        {
            txtallocation.Visible = false;
            ddlallocation.Visible = true;
        }
        else
        {
            ddlallocation.Visible = false;
            txtallocation.Visible = true;
        }
    }

    protected void imgBtnClear_Click(object sender, ImageClickEventArgs e)
    {
        txtaction.Text = "";
        txtallocation.Text = "";
        txtcomment.Text = "";
        txtfollowup.Text = "";
        txtinput.Text = "";
        txtipc.Text = "";
        txtparticipants.Text = "";
        txtsrno.Text = "";
        txtsummary.Text = "";
        ddlallocation.SelectedIndex = 0;
        ddlrpt.SelectedIndex = 0;
    }
}