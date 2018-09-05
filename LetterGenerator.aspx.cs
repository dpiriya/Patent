using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using Microsoft.Office.Interop;
using System.IO;

public partial class LetterGenerator : System.Web.UI.Page
{
    SqlConnection con= new SqlConnection ();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.IsPostBack)
        {
            string sql = Convert.ToString(Session["SearchForLetter"]);
            if (!string.IsNullOrEmpty(sql))
            {
                MarketingDS ds = new MarketingDS();
                lvIdf.DataSource = ds.Tables["MarketIDF"];
                lvIdf.DataBind();
                ViewState["MktIDF"] = ds.Tables["MarketIDF"];
                using (con = new SqlConnection())
                {
                    SqlCommand cmd = new SqlCommand();
                    con.ConnectionString = ConfigurationManager.ConnectionStrings["PATENTCN"].ConnectionString;
                    con.Open();
                    cmd.CommandText = sql;
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = con;
                    SqlDataReader dr;
                    dr = cmd.ExecuteReader();
                     DataTable dt = new DataTable();
                    dt.Load(dr);
                    gvGenerator.DataSource = dt;
                    gvGenerator.DataBind();
                }
            }
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
            //ddl.DataTextField = "fileno";
            //ddl.DataValueField = "fileno";
            //ddl.DataSource = dr;
            //ddl.DataBind();
            ddl.Items.Insert(0, new ListItem("", ""));
            con.Close();
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

            }
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
        lvIdf.InsertItemPosition = InsertItemPosition.LastItem;
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
            dr["slno"] = slno;
            dr["fileno"] = ldf;
            dr["title"] = title;
            dr["inventor1"] = inventor1;
            dr["applcn_no"] = applcn_no;
            dr["status"] = status;
            dt.Rows.Add(dr);
            lvIdf.InsertItemPosition = InsertItemPosition.None;
            lvIdf.DataSource = (DataTable)ViewState["MktIDF"];
            lvIdf.DataBind();            
        }
    }

    protected void BtnSelectRecord_Click(object sender, EventArgs e)
    {
              System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-GB");
        string IDFNo="";
        foreach (ListViewDataItem row in lvIdf.Items)
        {
            if (row.ItemType == ListViewItemType.DataItem)
            {
                Label idf= row.FindControl("lblIdfNo") as Label;
                IDFNo= idf.Text.Trim();
                break;
            }
        }
        if (IDFNo == "" || IDFNo== null)
        {
            ClientScript.RegisterStartupScript(GetType(), "Information", "<script>alert('Add IDF Number')</script>");
            return;
        }
        string PType="", Title="", ApplicationNo="", FilingDt="", Specification="";
        using (con = new SqlConnection())
        {
            string sql = "select Type,Title, Applcn_no as ApplicationNo,Filing_dt as FilingDt,Specification from patdetails where fileno = '" + IDFNo + "'";
            SqlCommand cmd = new SqlCommand();
            con.ConnectionString = ConfigurationManager.ConnectionStrings["PATENTCN"].ConnectionString;
            con.Open();
            cmd.CommandText = sql;
            cmd.CommandType = CommandType.Text;
            cmd.Connection = con;
            SqlDataReader dr;
            dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                PType = dr.GetString(0);
                Title = dr.GetString(1);
                if (!dr.IsDBNull(2)) ApplicationNo = dr.GetString(2); else ApplicationNo ="";
                if (!dr.IsDBNull(3)) FilingDt = dr.GetDateTime(3).ToShortDateString(); else FilingDt = "";
                if (!dr.IsDBNull(4)) Specification = dr.GetString(4); else Specification = "";
            }
        }
        DataTable dt = new DataTable();
        dt.Columns.AddRange(new DataColumn[8] { new DataColumn("PType"), new DataColumn("Title"), new DataColumn("ApplicationNo"), new DataColumn("FilingDt"), new DataColumn("Specification"), new DataColumn("ContactName"), new DataColumn("Address1"), new DataColumn("Address2") });
        foreach (GridViewRow row in gvGenerator.Rows)
        {
            if (row.RowType == DataControlRowType.DataRow)
            {
                CheckBox chkRow = (row.Cells[0].FindControl("chkSelected") as CheckBox);
                if (chkRow.Checked)
                {
                    string ContactName = row.Cells[4].Text;
                    string Address1 = row.Cells[5].Text;
                    string Address2 = row.Cells[6].Text;
                    dt.Rows.Add(PType, Title, ApplicationNo, FilingDt, Specification, ContactName, Address1, Address2);
                }
            }
        }
        string docFileName = "TTIP" + IDFNo + ".doc";
        if (dt.Rows.Count > 0)
        {
            TechTransferIP tt = new TechTransferIP();
            tt.ProcessRequest(dt, docFileName);
        }
        else
        {
            ClientScript.RegisterStartupScript(GetType(), "Information", "<script>alert('Select Contacts')</script>");
            return;
        }

    }

    

   /* private void CreateDocument(System.Data.DataTable dt)
    {
           //Create an instance for word app
                Microsoft.Office.Interop.Word.Application winword = new Microsoft.Office.Interop.Word.Application();

                //Set animation status for word application
                winword.ShowAnimation = false;

                //Set status for word application is to be visible or not.
                winword.Visible = false;
                
                //Create a missing variable for missing value
                object missing = System.Reflection.Missing.Value;

                //Create a new document
                Microsoft.Office.Interop.Word.Document document = winword.Documents.Add(ref missing, ref missing, ref missing, ref missing);
                
                //Add header into the document
                foreach (Microsoft.Office.Interop.Word.Section section in document.Sections)
                {
                    //Get the header range and add the header details.
                    Microsoft.Office.Interop.Word.Range headerRange = section.Headers[Microsoft.Office.Interop.Word.WdHeaderFooterIndex.wdHeaderFooterPrimary].Range;
                    //headerRange.Fields.Add (headerRange, Microsoft.Office.Interop.Word.WdFieldType.wdFieldPage,ref missing,ref missing);
                    headerRange.ParagraphFormat.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphCenter;
                    headerRange.Font.ColorIndex = Microsoft.Office.Interop.Word.WdColorIndex.wdBlue;
                    headerRange.Font.Size = 10;
                    headerRange.Text = "Header text goes here";
                }

                //Add the footers into the document
                foreach (Microsoft.Office.Interop.Word.Section wordSection in document.Sections)
                {
                    //Get the footer range and add the footer details.
                    Microsoft.Office.Interop.Word.Range footerRange = wordSection.Footers[Microsoft.Office.Interop.Word.WdHeaderFooterIndex.wdHeaderFooterPrimary].Range;
                    footerRange.Font.ColorIndex = Microsoft.Office.Interop.Word.WdColorIndex.wdDarkRed;
                    footerRange.Font.Size =10;
                    footerRange.ParagraphFormat.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphCenter;
                    footerRange.Text = "Footer text goes here";
                }

                //adding text to document
                document.Content.SetRange(0, 0);
                document.Content.Text = "This is test document "+ Environment.NewLine;
                
                //Add paragraph with Heading 1 style
                Microsoft.Office.Interop.Word.Paragraph para1 = document.Content.Paragraphs.Add(ref missing);                
                object styleHeading1 = "Heading 1";
                para1.Range.set_Style(ref styleHeading1);                
                para1.Range.Text = "Para 1 text";
                para1.Range.InsertParagraphAfter();

                //Add paragraph with Heading 2 style
                Microsoft.Office.Interop.Word.Paragraph para2 = document.Content.Paragraphs.Add(ref missing);
                object styleHeading2 = "Heading 2";
                para2.Range.set_Style(ref styleHeading2);
                para2.Range.Text = "Para 2 text";
                para2.Range.InsertParagraphAfter();

                //Create a 5X5 table and insert some dummy record
                Microsoft.Office.Interop.Word.Table firstTable = document.Tables.Add(para1.Range, 5, 5, ref missing, ref missing);
                
                firstTable.Borders.Enable = 1;
                foreach (Microsoft.Office.Interop.Word.Row row in firstTable.Rows)
                {
                    foreach (Microsoft.Office.Interop.Word.Cell cell in row.Cells)
                    {
                        //Header row
                        if (cell.RowIndex == 1)
                        {
                            cell.Range.Text = "Column " + cell.ColumnIndex.ToString();
                            cell.Range.Font.Bold = 1;
                            //other format properties goes here
                            cell.Range.Font.Name = "verdana";
                            cell.Range.Font.Size = 10;
                            //cell.Range.Font.ColorIndex = WdColorIndex.wdGray25;                            
                            cell.Shading.BackgroundPatternColor = Microsoft.Office.Interop.Word.WdColor.wdColorGray25;
                            //Center alignment for the Header cells
                            cell.VerticalAlignment = Microsoft.Office.Interop.Word.WdCellVerticalAlignment.wdCellAlignVerticalCenter;
                            cell.Range.ParagraphFormat.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphCenter;
                            
                        }
                        //Data row
                        else
                        {
                            cell.Range.Text = (cell.RowIndex - 2 + cell.ColumnIndex).ToString();
                        }
                    }
                }
               
                //Save the document
                object filename = @"c:\temp1.docx";
                document.SaveAs2(ref filename,ref missing,ref missing,ref missing,ref missing,ref missing,ref missing,ref missing,ref missing,ref missing,ref missing,ref missing,ref missing,ref missing,ref missing,ref missing,ref missing);
                document.Close(ref missing, ref missing, ref missing);
                document = null;
                winword.Quit(ref missing, ref missing, ref missing);
                winword = null;


        
        
    }*/
}
