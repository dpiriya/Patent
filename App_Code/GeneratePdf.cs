using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Globalization;
using System.Data.SqlClient;
using System.Text;
using System.IO;
using iTextSharp.text;
using iTextSharp.text.pdf;
/// <summary>
/// Summary description for GeneratePdf
/// </summary>
public class GeneratePdf
{
	public GeneratePdf()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    public PdfPTable MakePdf(DataTable dt, int numCol, string subTitle, string[] strTitle)
    {
        System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-GB");
        PdfPTable table = new PdfPTable(numCol);
        //actual width of table in points
        table.TotalWidth = 520f;
        //fix the absolute width of the table
        table.LockedWidth = true;

        //relative col widths in proportions - 1/3 and 2/3
        float[] widths = new float[] { 1f, 2f };
        table.SetWidths(widths);
        table.HorizontalAlignment = 0;
        //leave a gap before and after the table
        table.SpacingBefore = 20f;
        table.SpacingAfter = 20f;

        PdfPCell cell1 = new PdfPCell(new Phrase(subTitle, new Font(Font.FontFamily.TIMES_ROMAN, 12,1)));
        cell1.Colspan = numCol;
        cell1.Border = Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER | Rectangle.RIGHT_BORDER;
        cell1.BorderColor = BaseColor.BLACK;
        cell1.BackgroundColor = new BaseColor(188, 190, 189);
        cell1.HorizontalAlignment = 1;
        cell1.VerticalAlignment = Element.ALIGN_MIDDLE;
        table.AddCell(cell1);

        foreach (DataRow dr in dt.Rows)
        {
            int cnt = 0;
            foreach (DataColumn dc in dt.Columns)
            {
                if (subTitle == "International Patent Details" && cnt == 0)
                {
                    PdfPCell cell2 = new PdfPCell(new Phrase(strTitle[cnt], new Font(Font.FontFamily.TIMES_ROMAN, 11, 1)));
                    cell2.BackgroundColor = new BaseColor(214, 230, 223);
                    table.AddCell(cell2);
                    PdfPCell cell3 = new PdfPCell(new Phrase(dr[dc].ToString(), new Font(Font.FontFamily.TIMES_ROMAN, 11, 1)));
                    cell3.BackgroundColor = new BaseColor(214, 230, 223);
                    table.AddCell(cell3);
                }
                else
                {
                    table.AddCell(strTitle[cnt]);
                    table.AddCell(dr[dc].ToString());
                }
                cnt += 1;
            }
        }
        return table;
    }


    public PdfPTable MakeTablePdf(DataTable dt, int numCol, string subTitle, string[] strTitle)
    {
        System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-GB");
        PdfPTable table = new PdfPTable(numCol);
        //actual width of table in points
        table.TotalWidth = 520f;
        //fix the absolute width of the table
        table.LockedWidth = true;

        //relative col widths in proportions - 1/3 and 2/3
        float[] widths = new float[] { 0.7f, 3f, 1.5f, 1f, 2f };
        table.SetWidths(widths);
        table.HorizontalAlignment = 0;
        //leave a gap before and after the table
        table.SpacingBefore = 10f;
        table.SpacingAfter = 20f;

        PdfPCell cell1 = new PdfPCell(new Phrase(subTitle, new Font(Font.FontFamily.TIMES_ROMAN, 12,1)));
        cell1.Colspan = numCol;
        cell1.Border = Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER | Rectangle.RIGHT_BORDER;
        cell1.BorderColor = BaseColor.BLACK;
        cell1.BackgroundColor = new BaseColor(188,190,189);
        cell1.HorizontalAlignment = 1;
        cell1.VerticalAlignment = Element.ALIGN_MIDDLE;
        table.AddCell(cell1);

        for (int i = 0; i <= strTitle.GetUpperBound(0);i++)
        {
            PdfPCell cell2 = new PdfPCell(new Phrase(strTitle[i], new Font(Font.FontFamily.TIMES_ROMAN, 11, 1)));
            cell2.BackgroundColor = new BaseColor(214,230,223);
            cell2.VerticalAlignment = Element.ALIGN_MIDDLE;
            table.AddCell(cell2);              
        }

        foreach (DataRow dr in dt.Rows)
        {
            int cnt = 0;
            foreach (DataColumn dc in dt.Columns)
            {
                //table.AddCell(strTitle[cnt]);
                table.AddCell(dr[dc].ToString());
                cnt += 1;
            }
        }
        return table;
    }


}
