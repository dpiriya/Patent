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
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using System.IO;
/// <summary>
/// Summary description for TechTransferIP
/// </summary>
public class TechTransferIP : System.Web.UI.Page
{
	public TechTransferIP()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    public void ProcessRequest(System.Data.DataTable dt, string docFileName)
    { 

        using (MemoryStream mem = new MemoryStream())
        {
            // Create Document
            using (WordprocessingDocument wordDocument =
            WordprocessingDocument.Create(mem, WordprocessingDocumentType.Document, true))
            {
                // Add a main document part. 
                MainDocumentPart mainPart = wordDocument.AddMainDocumentPart();
                StyleDefinitionsPart stylePart= mainPart.AddNewPart<StyleDefinitionsPart>();

                RunProperties rPrNormal = new RunProperties();
                RunFonts rFont1 = new RunFonts();
                //rFont1.Ascii = "Times New Roman";
                //rPrNormal.Append(rFont1);
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
                //rFont.Ascii = "Times New Roman";
                //rPr.Append(color);
                //rPr.Append(rFont);
                rPr.Append(new Bold());
                rPr.Append(new DocumentFormat.OpenXml.Wordprocessing.FontSize() { Val = "24" });
                DocumentFormat.OpenXml.Wordprocessing.Style style= new DocumentFormat.OpenXml.Wordprocessing.Style();
                style.StyleId = "myHeading1";
                style.Append(new Name() {Val="My Heading 1"});
                style.Append(new NextParagraphStyle() { Val = "Normal" });
                style.Append(rPr);
                stylePart.Styles = new Styles();
                stylePart.Styles.Append(style);
                stylePart.Styles.Save();

                // Create the document structure and add some text.
                mainPart.Document = new DocumentFormat.OpenXml.Wordprocessing.Document();
                
                Body body = mainPart.Document.AppendChild(new Body());
                ParagraphProperties Normal_pPr = new ParagraphProperties(new ParagraphStyleId() { Val = "myText" });

                foreach (DataRow row in dt.Rows)
                {
                    Paragraph paraTo = body.AppendChild(new Paragraph());
                    Run runHead = paraTo.AppendChild(new Run());
                    RunProperties rpHead = new RunProperties();
                    rpHead.Append(new Bold());
                    runHead.Append(rpHead);
                    runHead.AppendChild(new Text("Mr.Ateet P"));
                    runHead.AppendChild(new Break());
                    runHead.AppendChild(new Text("Marketing Manager"));
                    runHead.AppendChild(new Break());
                    runHead.AppendChild(new Text("IPM Cell"));
                    runHead.AppendChild(new Break());
                    Run runTo = paraTo.AppendChild(new Run());                    
                    runTo.AppendChild(new Text("To"));
                    runTo.AppendChild(new Break());
                    runTo.AppendChild(new Text("" + row["ContactName"]));
                    runTo.AppendChild(new Break());
                    runTo.AppendChild(new Text(""+row["Address1"]));
                    runTo.AppendChild(new Break());
                    runTo.AppendChild(new Text("" + row["Address2"]));                                                            
                    Paragraph paraDate = body.AppendChild(new Paragraph());
                    ParagraphProperties ppDate = new ParagraphProperties();
                    Justification dateAlign = new Justification() { Val = JustificationValues.Right };
                    ppDate.Append(dateAlign);
                    paraDate.Append(ppDate);
                    Run runDate = paraDate.AppendChild(new Run());
                    runDate.AppendChild(new Text("Date : " + DateTime.Now.ToShortDateString()));                    
                    Paragraph paraTitle = body.AppendChild(new Paragraph());
                    ParagraphProperties heading_pPr = new ParagraphProperties();
                    heading_pPr.ParagraphStyleId = new ParagraphStyleId() { Val = "myHeading1" };
                    Justification centerHeading = new Justification() { Val = JustificationValues.Center };
                    heading_pPr.Append(centerHeading);
                    paraTitle.Append(heading_pPr);
                    Run runTitle = paraTitle.AppendChild(new Run());
                    runTitle.AppendChild(new Text("SUB : TECHNOLOGY TRANSFER - IP Licensing, Collaboration"));
                    Paragraph para = body.AppendChild(new Paragraph());
                    Run run = para.AppendChild(new Run());
                    run.AppendChild(new Text("Dear Sir / Madam,"));
                    Paragraph para1 = body.AppendChild(new Paragraph(new ParagraphProperties(new SpacingBetweenLines() { After = "120" })));
                    Run run1 = para1.AppendChild(new Run());
                    run1.AppendChild(new Text("Greetings from IIT Madras!"));
                    Paragraph para2 = body.AppendChild(new Paragraph(new ParagraphProperties(new SpacingBetweenLines() { After = "120" })));
                    Run run2 = para2.AppendChild(new Run());
                    Text para21Text = new Text() { Text = "Indian Institute of Technology Madras, is a leading Institute for Technology Research and Education. Further details are available at ",Space=SpaceProcessingModeValues.Preserve};
                    run2.AppendChild(para21Text);
                    RunProperties rpBold = new RunProperties();
                    Bold bold = new Bold();
                    rpBold.Append(bold);
                    Text para22Text = new Text() { Text = "https://www.iitm.ac.in" };
                    run2.AppendChild(rpBold);
                    run2.AppendChild(para22Text);
                    Paragraph para3 = body.AppendChild(new Paragraph(new ParagraphProperties(new SpacingBetweenLines() { After = "120" })));                    
                    Run run3 = para3.AppendChild(new Run());
                    run3.AppendChild(new Text("Researchers at the institute have developed following technology with potential application in your industry. Details are as follows:"));
                    Paragraph para4 = body.AppendChild(new Paragraph(new ParagraphProperties(new Indentation() { Left = "500" }, new SpacingBetweenLines() { After = "120" })));
                    Run run4 = para4.AppendChild(new Run());
                    Text para41Text = new Text() { Text = "IP Type :     ", Space = SpaceProcessingModeValues.Preserve };
                    RunProperties rpBold4 = new RunProperties();
                    rpBold4.Append(new Bold());
                    run4.AppendChild(rpBold4);
                    rpBold4.Append(para41Text);
                    Run run41 = para4.AppendChild(new Run());
                    Text para42Text = new Text() { Text = "" + row["PType"]};
                    run41.Append(para42Text);
                    Paragraph para5 = body.AppendChild(new Paragraph(new ParagraphProperties(new Indentation() { Left = "500" }, new Justification() { Val = JustificationValues.Both }, new SpacingBetweenLines() { After = "120" })));
                    Run run5 = para5.AppendChild(new Run());
                    RunProperties rprun5 = new RunProperties();
                    rprun5.Append(new Bold());
                    run5.Append(rprun5);
                    Text para51Text = new Text() { Text = "Title :     ", Space = SpaceProcessingModeValues.Preserve };
                    run5.Append(para51Text);
                    Run run51 = para5.AppendChild(new Run());
                    run51.AppendChild(new Text("" + row["Title"]));
                    Paragraph para6 = body.AppendChild(new Paragraph(new ParagraphProperties(new Indentation() { Left = "500" }, new SpacingBetweenLines() { After = "120" })));
                    Run run6 = para6.AppendChild(new Run());
                    RunProperties rp6 = new RunProperties();
                    rp6.Append(new Bold());
                    run6.Append(rp6);
                    Text para61Text = new Text() {Text="Reference :     ", Space=SpaceProcessingModeValues.Preserve};
                    run6.Append(para61Text);
                    Run run61 = para6.AppendChild(new Run());
                    run61.Append( new Text ("Application Number : " + row["ApplicationNo"] + "            Filing Date : " + row["FilingDt"]));
                    Paragraph para7 = body.AppendChild(new Paragraph(new ParagraphProperties(new Indentation() { Left = "500" }, new Justification() { Val = JustificationValues.Both }, new SpacingBetweenLines() { After = "120" })));
                    Run run7 = para7.AppendChild(new Run());
                    RunProperties rp7 = new RunProperties();
                    rp7.Append(new Bold());
                    run7.Append(rp7);
                    Text para71Text = new Text() { Text = "Abstract :     ", Space = SpaceProcessingModeValues.Preserve };
                    run7.Append(para71Text);
                    Run run71 = para7.AppendChild(new Run());
                    run71.AppendChild(new Text("" + row["Specification"]));
                    Paragraph para8 = body.AppendChild(new Paragraph(new ParagraphProperties(new SpacingBetweenLines() { After = "120" })));
                    Run run8 = para8.AppendChild(new Run());
                    Text para81Text= new Text () {Text="We are writing to check your interest in the technology for Licensing, or further development. Other related technologies may be accessed at ", Space= SpaceProcessingModeValues.Preserve};
                    run8.Append(para81Text);
                    Text para82Text = new Text() { Text = "https://icsris.iitm.ac.in/ipr" };
                    RunProperties rp8 = new RunProperties();
                    rp8.Append(new Bold());
                    run8.Append(rp8);
                    run8.Append(para82Text);
                    Paragraph para10 = body.AppendChild(new Paragraph(new ParagraphProperties(new SpacingBetweenLines() { After = "120" })));
                    Run run10 = para10.AppendChild(new Run());
                    run10.AppendChild(new Text("We are looking forward to your interest and feedback."));
                    Paragraph para11 = body.AppendChild(new Paragraph());
                    Run run11 = para11.AppendChild(new Run());
                    run11.AppendChild(new Text("Yours sincerely,"));
                    Paragraph para12 = body.AppendChild(new Paragraph());
                    ParagraphProperties Design_pPr = new ParagraphProperties();
                    Design_pPr.ParagraphStyleId = new ParagraphStyleId() { Val = "myHeading1" };
                    para12.Append(Design_pPr);                    
                    Run run12 = para12.AppendChild(new Run());
                    run12.AppendChild(new Break());
                    run12.AppendChild(new Text("Marketing Manager"));
                    Paragraph tt1 = new Paragraph(new Run(new Break() { Type = BreakValues.Page }));
                    body.Append(tt1);
                }
                //Paragraph p232= new Paragraph();
               // ParagraphProperties pp220 = new ParagraphProperties();
                //SectionProperties sp1 = new SectionProperties();
                
                
                //.PrependChild<RunProperties>(rPrNormal);
                mainPart.Document.Save();
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
