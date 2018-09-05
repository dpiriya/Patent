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

public partial class ActionTracker : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.IsPostBack)
        {
            System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-GB");
            ddlCategory.Items.Add("");
            ddlCategory.Items.Add("IDF No");
            ddlCategory.Items.Add("Marketing No");
            ddlCategory.Items.Add("Event");
            ddlCategory.Items.Add("Commercial");
            ddlCategory.Items.Add("Others");

            txtActionDt.Text = DateTime.Now.Date.ToShortDateString();
            txtActionUser.Text = Membership.GetUser().UserName.ToString();
            
            ddlAssignUser.DataSource = Membership.GetAllUsers();
            ddlAssignUser.DataBind();
            ddlAssignUser.Items.Insert(0, "");

            ddlComplete.Items.Add("");
            ddlComplete.Items.Add("Yes");
            ddlComplete.Items.Add("No");
        }
        imgBtnSubmit.Visible = (!User.IsInRole("View"));
    }
    protected void imgBtnSubmit_Click(object sender, ImageClickEventArgs e)
    {
        if (ddlSubCategory.SelectedIndex==0)
        {
            ClientScript.RegisterStartupScript(GetType(), "Information", "<script>alert('File No Can not be Empty')</script>");
            return;
        }
        System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-GB");
        con.ConnectionString = ConfigurationManager.ConnectionStrings["PATENTCN"].ConnectionString;
        try
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "sp_ActionTracker";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            con.Open();

            SqlParameter pm1 = new SqlParameter();
            pm1.ParameterName = "@EntryDt";
            pm1.SourceColumn = "EntryDt";
            pm1.Value = DateTime.Now.ToShortDateString();
            pm1.DbType = DbType.String;
            pm1.Direction = ParameterDirection.Input;

            SqlParameter pm2 = new SqlParameter();
            pm2.ParameterName = "@subCategory";
            pm2.SourceColumn = "subCategory";
            pm2.Value = ddlSubCategory.SelectedItem.Text.Trim();
            pm2.DbType = DbType.String;
            pm2.Direction = ParameterDirection.Input;

            SqlParameter pm3 = new SqlParameter();
            pm3.ParameterName = "@Action";
            pm3.SourceColumn = "Action";
            if (txtAction.Text.Trim() == "") pm3.Value = DBNull.Value; else pm3.Value = txtAction.Text.Trim();
            pm3.DbType = DbType.String;
            pm3.Direction = ParameterDirection.Input;

            SqlParameter pm4 = new SqlParameter();
            pm4.ParameterName = "@ActionDt";
            pm4.SourceColumn = "ActionDt";
            pm4.Value = DateTime.Now.Date.ToShortDateString();
            pm4.DbType = DbType.String;
            pm4.Direction = ParameterDirection.Input;

            SqlParameter pm5 = new SqlParameter();
            pm5.ParameterName = "@ActionUser";
            pm5.SourceColumn = "ActionUser";
            pm5.Value = Membership.GetUser().UserName.ToString();
            pm5.DbType = DbType.String;
            pm5.Direction = ParameterDirection.Input;

            SqlParameter pm6 = new SqlParameter();
            pm6.ParameterName = "@NextAction";
            pm6.SourceColumn = "NextAction";
            if (txtNextAction.Text.Trim() == "") pm6.Value = DBNull.Value; else pm6.Value = txtNextAction.Text.Trim();
            pm6.DbType = DbType.String;
            pm6.Direction = ParameterDirection.Input;

            SqlParameter pm7 = new SqlParameter();
            pm7.ParameterName = "@FollowupDt";
            pm7.SourceColumn = "FollowupDt";
            if (txtFollowupDt.Text.Trim() == "")
                pm7.Value = DBNull.Value;
            else
            {
                DateTime followDt;
                if(DateTime.TryParse(txtFollowupDt.Text.Trim(),out followDt))
                    pm7.Value = txtFollowupDt.Text.Trim();
                else
                {
                    ClientScript.RegisterStartupScript(GetType(), "Information", "<script>alert('Verify Followup Date')</script>");
                    return;
                }
            }
            pm7.DbType = DbType.String;
            pm7.Direction = ParameterDirection.Input;

            SqlParameter pm8 = new SqlParameter();
            pm8.ParameterName = "@AssignUser";
            pm8.SourceColumn = "AssignUser";
            if (ddlAssignUser.SelectedItem.Text.Trim() == "") pm8.Value = DBNull.Value; else pm8.Value = ddlAssignUser.SelectedItem.Text.Trim();
            pm8.DbType = DbType.String;
            pm8.Direction = ParameterDirection.Input;

            SqlParameter pm9 = new SqlParameter();
            pm9.ParameterName = "@ActionComplete";
            pm9.SourceColumn = "ActionComplete";
            pm9.Value = ddlComplete.SelectedIndex == 2 ? 1 : 0;
            pm9.DbType = DbType.String;
            pm9.Direction = ParameterDirection.Input;

            SqlParameter pm10 = new SqlParameter();
            pm10.ParameterName = "@Comments";
            pm10.SourceColumn = "Comments";
            if (txtComments.Text.Trim() == "") pm10.Value = DBNull.Value; else pm10.Value = txtComments.Text.Trim();
            pm10.DbType = DbType.String;
            pm10.Direction = ParameterDirection.Input;

            SqlParameter pm11 = new SqlParameter();
            pm11.ParameterName = "@otherSubCategory";
            pm11.SourceColumn = "otherSubCategory";
            if (txtSubCategory.Text.Trim() == "") pm11.Value = DBNull.Value; else pm11.Value = txtSubCategory.Text.Trim();
            pm11.DbType = DbType.String;
            pm11.Direction = ParameterDirection.Input;

            SqlParameter pm12 = new SqlParameter();
            pm12.ParameterName = "@category";
            pm12.SourceColumn = "category";
            if (ddlCategory.SelectedItem.Text.Trim() == "") pm12.Value = DBNull.Value; else pm12.Value = ddlCategory.SelectedItem.Text.Trim();
            pm12.DbType = DbType.String;
            pm12.Direction = ParameterDirection.Input;

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

            cmd.ExecuteNonQuery();
            ClientScript.RegisterStartupScript(GetType(), "Information", "<script>alert('This Record successfully Added')</script>");
            con.Close();
        }
        catch (Exception ex)
        {
            ClientScript.RegisterStartupScript(GetType(), "Error", "<script>alert('" + ex.Message.ToString() + "')</script>");
            con.Close();
            return;
        }
        imgBtnClear_Click(sender, e);
    }
    protected void imgBtnClear_Click(object sender, ImageClickEventArgs e)
    {
        System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-GB");
        ddlCategory.SelectedIndex = 0;
        ddlSubCategory.SelectedIndex = 0;
        txtSubCategory.Text = ""; 
        txtAction.Text = "";
        txtActionDt.Text=DateTime.Now.Date.ToShortDateString();
        txtActionUser.Text = Membership.GetUser().UserName.ToString(); ;
        txtNextAction.Text = "";
        txtFollowupDt.Text = "";
        ddlAssignUser.SelectedIndex = 0;
        ddlComplete.SelectedIndex = 0;
        txtComments.Text = "";        
    }
    protected void ddlCategory_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlSubCategory.Items.Clear();
        con.ConnectionString = ConfigurationManager.ConnectionStrings["PATENTCN"].ConnectionString;
        SqlCommand cmd = new SqlCommand();
        if (ddlCategory.SelectedIndex == 1)
        {
            txtSubCategory.Visible = false;
            lblSub.Visible = false;
            string sql = "select fileno from patdetails order by fileno";
            SqlDataReader dr;
            cmd.CommandType = CommandType.Text;
            cmd.Connection = con;
            cmd.CommandText = sql;
            con.Open();
            dr = cmd.ExecuteReader();
            ddlSubCategory.DataTextField = "fileno";
            ddlSubCategory.DataValueField = "fileno";
            ddlSubCategory.DataSource = dr;
            ddlSubCategory.DataBind();
            ddlSubCategory.Items.Insert(0, "");
            dr.Close();
            con.Close();
        }
        else if (ddlCategory.SelectedIndex == 2)
        {
            txtSubCategory.Visible = false;
            lblSub.Visible = false;
            string sql = "select MktgProjectNo from marketingProject order by cast (substring(MktgProjectNo,3,len(MktgProjectNo)) as bigint)";
            SqlDataReader dr;
            cmd.CommandType = CommandType.Text;
            cmd.Connection = con;
            cmd.CommandText = sql;
            con.Open();
            dr = cmd.ExecuteReader();
            ddlSubCategory.DataTextField = "MktgProjectNo";
            ddlSubCategory.DataValueField = "MktgProjectNo";
            ddlSubCategory.DataSource = dr;
            ddlSubCategory.DataBind();
            ddlSubCategory.Items.Insert(0, "");
            dr.Close();
            con.Close();     
        }
        else if (ddlCategory.SelectedIndex == 3 || ddlCategory.SelectedIndex == 4 || ddlCategory.SelectedIndex == 5)
        {
            txtSubCategory.Text = "";
            txtSubCategory.Visible = true;
            lblSub.Visible = true;
            string[] OtherList = new string[] { "Agreement/Documentation", "Marketing", "Attorney/Service Provider", "IITM Faculty/Student/Staff", "Others" };
            ddlSubCategory.Items.Add("");
            for (int i = 0; i <= OtherList.GetUpperBound(0); i++)
            {
                ddlSubCategory.Items.Add(OtherList[i].Trim());
            }
        }
        else
        {
            txtSubCategory.Visible = false;
            lblSub.Visible = false;        
        }
    }
}
