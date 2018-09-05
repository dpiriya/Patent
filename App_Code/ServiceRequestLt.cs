using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using DocumentFormat.OpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for ServiceRequestLt
/// </summary>
public class ServiceRequestLt
{
    public ServiceRequestLt()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    public void ProcessRequest(System.Data.DataTable dt,System.Data.DataTable dt1, string docFileName)
    {
        using (MemoryStream mem = new MemoryStream())
        {
            using (WordprocessingDocument doc = WordprocessingDocument.Create(mem, WordprocessingDocumentType.Document, true))
            {
                MainDocumentPart mainPart = doc.AddMainDocumentPart();
                StyleDefinitionsPart stylePart = mainPart.AddNewPart<StyleDefinitionsPart>();

                RunProperties rPrNormal = new RunProperties();
                RunFonts rFont1 = new RunFonts();
                rPrNormal.Append(new DocumentFormat.OpenXml.Wordprocessing.FontSize() { Val = "24" });
                DocumentFormat.OpenXml.Wordprocessing.Style style1 = new DocumentFormat.OpenXml.Wordprocessing.Style();
                style1.StyleId = "myText";
                style1.Append(new Name() { Val = "text" });
                style1.Append(rPrNormal);
                stylePart.Styles = new Styles();
                stylePart.Styles.Append(style1);

                RunProperties rPr = new RunProperties();
                Color color = new Color() { Val = "FF0000" };
                RunFonts rFont = new RunFonts();
                rPr.Append(new Bold());
                rPr.Append(new DocumentFormat.OpenXml.Wordprocessing.FontSize() { Val = "24" });
                DocumentFormat.OpenXml.Wordprocessing.Style style = new DocumentFormat.OpenXml.Wordprocessing.Style();
                style.StyleId = "myHeading1";
                style.Append(new Name() { Val = "My Heading 1" });
                style.Append(new NextParagraphStyle() { Val = "Normal" });
                style.Append(rPr);
                stylePart.Styles = new Styles();
                stylePart.Styles.Append(style);
                stylePart.Styles.Save();

                mainPart.Document = new DocumentFormat.OpenXml.Wordprocessing.Document();

                Body body = mainPart.Document.AppendChild(new Body());


                ParagraphProperties pptemp = new ParagraphProperties();
                Paragraph template = body.AppendChild(new Paragraph());
                Justification templatealign = new Justification() { Val = JustificationValues.Center };
                pptemp.Append(templatealign);
                template.Append(pptemp);
                Run runtemp = template.AppendChild(new Run());
                //RunProperties rptemp = new RunProperties(new Bold());
                RunProperties runProperties = runtemp.AppendChild(new RunProperties(new Bold()));
                runtemp.AppendChild(new Text("Service Request (Strictly Confidential)"));
                ParagraphProperties ppDate = new ParagraphProperties();
                Paragraph paraDate = body.AppendChild(new Paragraph());
                Justification dateAlign = new Justification() { Val = JustificationValues.Right };
                ppDate.Append(dateAlign);
                paraDate.Append(ppDate);
                Run runDate = paraDate.AppendChild(new Run());
                runDate.AppendChild(new Text("Date : " + DateTime.Now.ToShortDateString()));

                Paragraph paraTo = body.AppendChild(new Paragraph());
                Run runTo = paraTo.AppendChild(new Run());
                runTo.AppendChild(new Text("SR No:" + dt.Rows[0][0]));
                runTo.AppendChild(new Break());
                runTo.AppendChild(new Text(dt.Rows[0][3].ToString()));
                runTo.AppendChild(new Break());

                Paragraph title = body.AppendChild(new Paragraph());                
                Run rtitle = title.AppendChild(new Run());
                RunProperties rprptitle = rtitle.AppendChild(new RunProperties(new Bold()));
                rtitle.AppendChild(new Text("Services to be provided"));
                rtitle.AppendChild(new Break());

                //create table
                Table table = new Table();

                //create tbl prop and border
                TableProperties tblprop = new TableProperties(new TableBorders(new TopBorder()
                {
                    Val = new EnumValue<BorderValues>(BorderValues.Single),
                    Size = 10
                },
                new BottomBorder()
                {
                    Val = new EnumValue<BorderValues>(BorderValues.Single),
                    Size = 10
                },
                new LeftBorder()
                {
                    Val = new EnumValue<BorderValues>(BorderValues.Single),
                    Size = 10
                },
                new RightBorder()
                {
                    Val = new EnumValue<BorderValues>(BorderValues.Single),
                    Size = 10
                },
                new InsideHorizontalBorder()
                {
                    Val = new EnumValue<BorderValues>(BorderValues.Single),
                    Size = 10
                },
                new InsideVerticalBorder()
                {
                    Val = new EnumValue<BorderValues>(BorderValues.Single),
                    Size = 10
                }));

                //Append table prop to empty table
                table.AppendChild<TableProperties>(tblprop);
                table.Append(new TableProperties(new TableWidth() { Type = TableWidthUnitValues.Pct, Width = "100%" }));

                TableRow tr = new TableRow();
                TableCell tc1 = new TableCell();
                tc1.Append(new Paragraph(new Run(new Text("SLNO"))));
                tr.Append(tc1);

                TableCell tc2 = new TableCell();
                tc2.Append(new Paragraph(new Run(new Text("IDF"))));
                tr.Append(tc2);

                TableCell tc3 = new TableCell();
                tc3.Append(new Paragraph(new Run(new Text("Title"))));
                tr.Append(tc3);

                TableCell tc4 = new TableCell();
                tc4.Append(new Paragraph(new Run(new Text("Action"))));
                tr.Append(tc4);

                TableCell tc5 = new TableCell();
                tc5.Append(new Paragraph(new Run(new Text("Target Date"))));
                tr.Append(tc5);

                TableCell tc6 = new TableCell();
                tc6.Append(new Paragraph(new Run(new Text("Remarks"))));
                tr.Append(tc6);

                table.Append(tr);

                for (int i = 0; dt.Rows.Count > i; i++)
                {
                    TableRow t1 = new TableRow();
                    TableCell trc1 = new TableCell();
                    trc1.Append(new Paragraph(new Run(new Text(dt.Rows[i][1].ToString()))));
                    t1.Append(trc1);

                    TableCell trc2 = new TableCell();
                    trc2.Append(new Paragraph(new Run(new Text(dt.Rows[i][2].ToString()))));
                    t1.Append(trc2);

                    TableCell trc3 = new TableCell();
                    trc3.Append(new Paragraph(new Run(new Text(dt.Rows[i][17].ToString()))));
                    t1.Append(trc3);

                    TableCell trc4 = new TableCell();
                    trc4.Append(new Paragraph(new Run(new Text(dt.Rows[i][5].ToString()))));
                    t1.Append(trc4);

                    TableCell trc5 = new TableCell();
                    trc5.Append(new Paragraph(new Run(new Text(dt.Rows[i][9].ToString()))));
                    t1.Append(trc5);

                    TableCell trc6 = new TableCell();
                    trc6.Append(new Paragraph(new Run(new Text(dt.Rows[i][12].ToString()))));
                    t1.Append(trc6);
                    table.Append(t1);
                }

                body.Append(table);

                Paragraph title2 = body.AppendChild(new Paragraph());
                Run rtitle2 = title2.AppendChild(new Run());
                RunProperties runProperties2 = rtitle2.AppendChild(new RunProperties(new Bold()));
                rtitle2.AppendChild(new Break());
                rtitle2.AppendChild(new Text("Inventor Details: IDF " + dt.Rows[0][0].ToString()));
                rtitle2.AppendChild(new Break());

                Table table1 = new Table();
                TableProperties tblprop1 = new TableProperties(new TableBorders(new TopBorder()
                {
                    Val = new EnumValue<BorderValues>(BorderValues.Single),
                    Size = 10
                },
                new BottomBorder()
                {
                    Val = new EnumValue<BorderValues>(BorderValues.Single),
                    Size = 10
                },
                new LeftBorder()
                {
                    Val = new EnumValue<BorderValues>(BorderValues.Single),
                    Size = 10
                },
                new RightBorder()
                {
                    Val = new EnumValue<BorderValues>(BorderValues.Single),
                    Size = 10
                },
                new InsideHorizontalBorder()
                {
                    Val = new EnumValue<BorderValues>(BorderValues.Single),
                    Size = 10
                },
                new InsideVerticalBorder()
                {
                    Val = new EnumValue<BorderValues>(BorderValues.Single),
                    Size = 10
                }));
                table1.AppendChild<TableProperties>(tblprop1);
                table1.Append(new TableProperties(new TableWidth() { Type = TableWidthUnitValues.Pct, Width = "100%" }));

                TableRow t2 = new TableRow();
                TableCell t1c1 = new TableCell();
                t1c1.Append(new Paragraph(new Run(new Text("SLNO"))));
                t2.Append(t1c1);
                TableCell t1c2 = new TableCell();
                t1c2.Append(new Paragraph(new Run(new Text("Inventor Name"))));
                t2.Append(t1c2);
                TableCell t1c3 = new TableCell();
                t1c3.Append(new Paragraph(new Run(new Text("Applicants"))));
                t2.Append(t1c3);
                TableCell t1c4 = new TableCell();
                t1c4.Append(new Paragraph(new Run(new Text("Category"))));
                t2.Append(t1c4);
                TableCell t1c5 = new TableCell();
                t1c5.Append(new Paragraph(new Run(new Text("IDNO"))));
                t2.Append(t1c5);
                TableCell t1c6 = new TableCell();
                t1c6.Append(new Paragraph(new Run(new Text("Email ID"))));
                t2.Append(t1c6);
                TableCell t1c7 = new TableCell();
                t1c7.Append(new Paragraph(new Run(new Text("Phone no"))));
                t2.Append(t1c7);
                table1.Append(t2);
                for (int i = 0; dt1.Rows.Count > i; i++)
                {
                    TableRow tr2 = new TableRow();
                    TableCell trc1 = new TableCell();
                    trc1.Append(new Paragraph(new Run(new Text(i+1.ToString()))));
                    tr2.Append(trc1);

                    TableCell trc2 = new TableCell();
                    trc2.Append(new Paragraph(new Run(new Text(dt1.Rows[i][3].ToString()))));
                    tr2.Append(trc2);

                    TableCell trc3 = new TableCell();
                    trc3.Append(new Paragraph(new Run(new Text(dt1.Rows[i][4].ToString()))));
                    tr2.Append(trc3);

                    TableCell trc4 = new TableCell();
                    trc4.Append(new Paragraph(new Run(new Text(dt1.Rows[i][1].ToString()))));
                    tr2.Append(trc4);

                    TableCell trc5 = new TableCell();
                    trc5.Append(new Paragraph(new Run(new Text(dt1.Rows[i][2].ToString()))));
                    tr2.Append(trc5);

                    TableCell trc6 = new TableCell();
                    trc6.Append(new Paragraph(new Run(new Text())));
                    tr2.Append(trc6);

                    TableCell trc7 = new TableCell();
                    trc7.Append(new Paragraph(new Run(new Text())));
                    tr2.Append(trc7);
                    table1.Append(tr2);

                }
                body.Append(table1);

                Paragraph bill = body.AppendChild(new Paragraph());
                Run runbill = bill.AppendChild(new Run());
                RunProperties prprun = new RunProperties(new Bold());
                runbill.Append(prprun);
                prprun.Append(new Break());
                prprun.AppendChild(new Text("Billing and other instructions"));
                prprun.Append(new Break());

                ParagraphProperties billpp = new ParagraphProperties();                
                Paragraph billdt = body.AppendChild(new Paragraph());
                Run billdtrun = billdt.AppendChild(new Run());
                //Numbering element = new Numbering(new AbstractNum(new Level(new NumberingFormat() { Val = NumberFormatValues.Bullet },new LevelText() { Val = "·" }){ LevelIndex = 0 }){ AbstractNumberId = 1 },new NumberingInstance(new AbstractNumId() { Val = 1 }){ NumberID = 1 });
                //billpp.Append(element);
                //billdt.Append(billpp);
                billdtrun.AppendChild(new Text("* Please provide leads / support for commercialisation"));
                billdtrun.AppendChild(new Break());
                billdtrun.AppendChild(new Text("* All bills must quote the Service request no & the IDF No"));
                billdtrun.AppendChild(new Break());
                billdtrun.AppendChild(new Text("* In case of any delays contact IP office"));
                billdtrun.AppendChild(new Break());

                Paragraph from = body.AppendChild(new Paragraph());
                Run fromrun = from.AppendChild(new Run());
                fromrun.Append(new Break());
                fromrun.Append(new Text("Thanking You - Pankaj Kumar Bhagat"));
                fromrun.Append(new Break());
                fromrun.Append(new Text("Cheif Manager, IPM Cell"));
                fromrun.Append(new Break());
                fromrun.Append(new Text("Intellectual Property Management Cell"));
                fromrun.Append(new Break());
                fromrun.Append(new Text("Centre for Industrial Consultancy & Sponsored Research"));
                fromrun.Append(new Break());
                fromrun.Append(new Text("IInd floor, IC & SR Bldg., IIT Madras, Chennai 600036. INDIA "));
                fromrun.Append(new Break());
                fromrun.Append(new Text("Phone: 044-2257 9756/9751; ipoffice@iitm.ac.in / cmipm-icsr@imail.iitm.ac.in"));
                fromrun.Append(new Break());
                fromrun.Append(new Text("IITM IPR Patent Filings: https://icandsr.iitm.ac.in/ipr/Tech.Transfer"));

                doc.MainDocumentPart.Document.Save();
            }
            System.Web.HttpContext.Current.Response.ContentType = "application/vnd.openxmlformats-officedocument.wordprocessingml.document";
            System.Web.HttpContext.Current.Response.AppendHeader("Content-Disposition", "attachment;filename=" + docFileName);
            mem.Seek(0, SeekOrigin.Begin);
            mem.WriteTo(System.Web.HttpContext.Current.Response.OutputStream);
            System.Web.HttpContext.Current.Response.Flush();
            System.Web.HttpContext.Current.Response.End();
        }
    }
}