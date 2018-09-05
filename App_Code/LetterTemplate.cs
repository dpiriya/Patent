using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using Microsoft.Office.Interop.Word;
using Word = Microsoft.Office.Interop.Word;
//using DocumentFormat.OpenXml;
//using DocumentFormat.OpenXml.Packaging;
//using DocumentFormat.OpenXml.Wordprocessing;
using System.IO;


public class LetterTemplate : System.Web.UI.Page
{
    public LetterTemplate()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    public void ProcessRequest(System.Data.DataTable dt, string docFileName)
    {
        {
            object oMissing = System.Reflection.Missing.Value;
            object oEndOfDoc = "\\endofdoc"; /* \endofdoc is a predefined bookmark */

            //Start Word and create a new document.
            Word._Application oWord;
            Word._Document oDoc;
            oWord = new Word.Application();
            oWord.Visible = true;
            oDoc = oWord.Documents.Add(ref oMissing, ref oMissing,
            ref oMissing, ref oMissing);

            //foreach (DataRow row in dt.Rows)
            //{
            //Insert a paragraph at the beginning of the document.

            Word.Paragraph oPara1;
            oPara1 = oDoc.Content.Paragraphs.Add(ref oMissing);
            if (dt.Rows[0][17].ToString() == "Annual")
            {
                oPara1.Range.Text = "Template B";
            }
            else if (dt.Rows[0][17].ToString() == "Upfront Payment")
            {
                oPara1.Range.Text = "Template A";
            }
            else if(dt.Rows[0][17].ToString() == "Royalty")
            {
                oPara1.Range.Text = "Template D2";
            }
            oPara1.Range.Font.Bold = 1;
            oPara1.Format.SpaceAfter = 20;    //20 pt spacing after paragraph.
            oPara1.Range.InsertParagraphAfter();
            
            Word.Paragraph oPara2;
            oPara2 = oDoc.Content.Paragraphs.Add(ref oMissing);
            oPara2.Range.Text = "To:";
            oPara2.Range.Font.Bold = 0;
            oPara2.Format.SpaceAfter = 10;
            oPara2.Range.InsertParagraphAfter();
            oPara2.Range.Text = "Project Accounts";
            oPara2.Format.SpaceAfter = 1;
            oPara2.Range.InsertParagraphAfter();
            oPara2.Range.Text = "ICSR";
            oPara2.Format.SpaceAfter = 20;
            oPara2.Range.InsertParagraphAfter();

            
            Word.Paragraph oPara3;
            oPara3 = oDoc.Content.Paragraphs.Add(ref oMissing);
            if (dt.Rows[0][17].ToString() == "Upfront Payment" || dt.Rows[0][17].ToString() == "Annual")
            {
                oPara3.Range.Text = "Sub : Raising of Invoice";
            }
            else if(dt.Rows[0][17].ToString() == "Royalty")
            {
                oPara3.Range.Text = "Sub : Raising of Invoice [Royalty Payments]";
            }
            oPara3.Range.Font.Underline = Word.WdUnderline.wdUnderlineSingle;
            oPara3.Range.InsertParagraphAfter();

            Word.Table oTable;
            Word.Range wrdRng = oDoc.Bookmarks.get_Item(ref oEndOfDoc).Range;
            ////int i = 3;            
            //for(int i=3;i<=3;i--)
            oTable = oDoc.Tables.Add(wrdRng, 6,3, ref oMissing, ref oMissing);
            oTable.Range.ParagraphFormat.SpaceAfter = 5;
            oTable.Range.Font.Underline = Word.WdUnderline.wdUnderlineNone;
            oTable.Cell(1, 1).Range.Text = dt.Rows[0][0].ToString();
            oTable.Cell(1, 2).Range.Text = "Agreement NO: " + dt.Rows[0][1];
            oTable.Cell(1, 3).Range.Text = "TT A/c: " + dt.Rows[0][2];
            oTable.Cell(2, 1).Range.Text = "Title: " + dt.Rows[0][3];
            oTable.Rows[2].Cells[2].Merge(oTable.Rows[2].Cells[3]);
            oTable.Rows[2].Cells[1].Merge(oTable.Rows[2].Cells[2]);
            //oTable.Rows[2].Cells[1].Merge(oTable.Rows[2].Cells[3]);
            oTable.Cell(3, 1).Range.Text = "Scope: " + dt.Rows[0][4];
            oTable.Rows[3].Cells[2].Merge(oTable.Rows[3].Cells[3]);
            oTable.Rows[3].Cells[1].Merge(oTable.Rows[3].Cells[2]);
            oTable.Cell(4, 1).Range.Text = "Coordinating Person: " + dt.Rows[0][5];
            oTable.Rows[4].Cells[1].Merge(oTable.Rows[4].Cells[2]);
            oTable.Cell(4, 2).Range.Text = "Dept: " + dt.Rows[0][6];
            oTable.Cell(5, 1).Range.Text = "Type: " + dt.Rows[0][7];
            oTable.Rows[5].Cells[1].Merge(oTable.Rows[5].Cells[2]);
            oTable.Cell(5, 2).Range.Text = "Amount: " + dt.Rows[0][8];
            oTable.Cell(6, 1).Range.Text = "Due Date: " + dt.Rows[0][9];
            oTable.Rows[6].Cells[2].Merge(oTable.Rows[6].Cells[3]);
            oTable.Rows[6].Cells[1].Merge(oTable.Rows[6].Cells[2]);
            //oTable.Rows[1].Range.Font.Bold = 1;
            //oTable.Rows[1].Range.Font.Italic = 1;
            oTable.Borders.InsideLineStyle = Word.WdLineStyle.wdLineStyleSingle;
            oTable.Borders.OutsideLineStyle = Word.WdLineStyle.wdLineStyleSingle;
            
            //oTable.applyh
            //oTable.Columns[1].Width = oWord.InchesToPoints(2); //Change width of columns 1 & 2
            //oTable.Columns[2].Width = oWord.InchesToPoints(3);

            Word.Paragraph oPara4;
            oPara4 = oDoc.Content.Paragraphs.Add(ref oMissing);
            oPara4.Range.Text = "Customer";
            oPara4.Range.Font.Underline = Word.WdUnderline.wdUnderlineSingle;
            oPara4.Range.Font.Bold = 1;
            oPara4.Format.SpaceBefore = 5;
            oPara4.Format.SpaceAfter = 5;
            oPara4.Range.InsertParagraphAfter();

            Word.Paragraph oPara5;
            oPara5 = oDoc.Content.Paragraphs.Add(ref oMissing);
            oPara5.Range.Font.Bold = 0;
            oPara5.Range.Font.Underline = Word.WdUnderline.wdUnderlineNone;
            oPara5.Range.Text = dt.Rows[0][10].ToString();
            oPara5.Format.SpaceAfter = 1;
            oPara5.Range.InsertParagraphAfter();
            oPara5.Range.Text = dt.Rows[0][11].ToString();
            oPara5.Format.SpaceAfter = 1;
            oPara5.Range.InsertParagraphAfter();
            oPara5.Range.Text = dt.Rows[0][12].ToString();
            oPara5.Format.SpaceAfter = 1;
            oPara5.Range.InsertParagraphAfter();
            oPara5.Range.Text = dt.Rows[0][13].ToString();
            oPara5.Format.SpaceAfter = 1;
            oPara5.Range.InsertParagraphAfter();
            oPara5.Range.Text = dt.Rows[0][14].ToString();
            oPara5.Format.SpaceAfter = 1;
            oPara5.Range.InsertParagraphAfter();
            oPara5.Range.Text = dt.Rows[0][15].ToString();
            oPara5.Format.SpaceAfter = 1;
            oPara5.Range.InsertParagraphAfter();
            oPara5.Range.Text = dt.Rows[0][16].ToString();
            oPara5.Format.SpaceAfter = 5;
            oPara5.Range.InsertParagraphAfter();
            oPara5.Range.Font.Bold = 0;
            oPara5.Range.Font.Underline = Word.WdUnderline.wdUnderlineNone;
            if (dt.Rows[0][17].ToString() == "Upfront Payment" || dt.Rows[0][17].ToString() == "Annual")
            {
                oPara5.Range.Text = "Other available details are attached. The invoice may be raised for the above";
            }
            else if (dt.Rows[0][17].ToString() == "Royalty")
            {
                oPara5.Range.Text = "The declaration of dues and workings has been verified by us. Same is enlcosed.";
                oPara5.Format.SpaceAfter = 20;
                oPara5.Range.InsertParagraphAfter();
                oPara5.Range.Text = "The invoice may be raised for the above";
            }
            
            oPara5.Format.SpaceAfter = 10;
            oPara5.Range.InsertParagraphAfter();

            oPara5.Range.Text = "Thanking You";
            oPara5.Format.SpaceAfter = 50;
            oPara5.Range.InsertParagraphAfter();
            oPara5.Range.Text = "Supporting Documents:";
            oPara5.Format.SpaceAfter = 5;
            oPara5.Range.InsertParagraphAfter();
            if (dt.Rows[0][17].ToString() == "Upfront Payment")
            {                
                oPara5.Range.Text = "1. PDF of Agreement -" + dt.Rows[0][0];
                oPara5.Format.SpaceAfter = 1;
                oPara5.Range.InsertParagraphAfter();
                oPara5.Range.Text = "2. Customer Tax & Bank details";
               
            }
            else if(dt.Rows[0][17].ToString()=="Annual")
            {               
                oPara5.Range.Text = "1. Agreement -" + dt.Rows[0][0]+" already forwarded";
                oPara5.Format.SpaceAfter = 1;
                oPara5.Range.InsertParagraphAfter();
                oPara5.Range.Text = "2. Customer Tax & Bank details are same / change as per enclosure";
            }
            else if (dt.Rows[0][17].ToString() == "Royalty")
            {
                oPara5.Range.Text = "1. Verified declaration of dues";
                oPara5.Format.SpaceAfter = 1;
                oPara5.Range.InsertParagraphAfter();
                oPara5.Range.Text = "2. Working sheets of the customer";               
            }
            oPara5.Format.SpaceAfter = 1;
            oPara5.Range.InsertParagraphAfter();
        }
    }
}
