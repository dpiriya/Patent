using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using System.IO;


public class LetterTemplateXML : System.Web.UI.Page
{
    public LetterTemplateXML()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    public void ProcessRequest(System.Data.DataTable dt, string docFileName)
    {
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
                    //rFont.Ascii = "Times New Roman";
                    //rPr.Append(color);
                    //rPr.Append(rFont);
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
                    if (dt.Rows[0][17].ToString() == "Annual")
                    {
                        runtemp.AppendChild(new Text("Template B"));
                    }
                    else if (dt.Rows[0][17].ToString() == "Upfront Payment")
                    {
                        runtemp.AppendChild(new Text("Template A"));
                    }
                    else if (dt.Rows[0][17].ToString() == "Royalty" &&  dt.Rows[0][8].ToString()!="&nbsp;") 
                    {
                        runtemp.AppendChild(new Text("Template D2"));
                    }
                    else if (dt.Rows[0][17].ToString() == "Royalty" && dt.Rows[0][8].ToString() == "&nbsp;")
                    {
                        runtemp.AppendChild(new Text("Template D1"));
                    }
                    
                    ParagraphProperties ppDate = new ParagraphProperties();
                    Paragraph paraDate = body.AppendChild(new Paragraph());
                    Justification dateAlign = new Justification() { Val = JustificationValues.Right };
                    ppDate.Append(dateAlign);
                    paraDate.Append(ppDate);
                    Run runDate = paraDate.AppendChild(new Run());
                    runDate.AppendChild(new Text("Date : " + DateTime.Now.ToShortDateString()));

                    ParagraphProperties Normal_pPr = new ParagraphProperties(new ParagraphStyleId() { Val = "myText" });

                    Paragraph paraTo = body.AppendChild(new Paragraph());
                    Run runTo = paraTo.AppendChild(new Run());
                    runTo.AppendChild(new Text("To"));
                    runTo.AppendChild(new Break());
                    runTo.AppendChild(new Break());
                    if (dt.Rows[0][8].ToString() != "&nbsp;")
                    {
                        runTo.AppendChild(new Text("Project Accounts"));
                        runTo.AppendChild(new Break());
                        runTo.AppendChild(new Text("ICSR"));
                    }
                    else
                    {
                        //Paragraph tocustdet = body.AppendChild(new Paragraph());
                        //Run runtodetails = tocustdet.AppendChild(new Run());
                        //Justification center1 = new Justification() { Val = JustificationValues.Center };
                        //runtodetails.AppendChild(center1);
                        runTo.AppendChild(new Text(dt.Rows[0][10].ToString()));
                        runTo.AppendChild(new Break());
                        runTo.AppendChild(new Text(dt.Rows[0][11].ToString()));
                        runTo.AppendChild(new Break());
                        runTo.AppendChild(new Text(dt.Rows[0][12].ToString()));
                        runTo.AppendChild(new Break());
                        runTo.AppendChild(new Text(dt.Rows[0][13].ToString()));
                        runTo.AppendChild(new Break());
                        runTo.AppendChild(new Text(dt.Rows[0][14].ToString()));
                        runTo.AppendChild(new Break());
                        runTo.AppendChild(new Text(dt.Rows[0][15].ToString()));
                        runTo.AppendChild(new Break());
                        runTo.AppendChild(new Text(dt.Rows[0][16].ToString()));
                    }
                    runTo.AppendChild(new Break());
                                    
                    Paragraph paraTitle = body.AppendChild(new Paragraph());
                    ParagraphProperties heading_pPr = new ParagraphProperties();
                    heading_pPr.ParagraphStyleId = new ParagraphStyleId() { Val = "myHeading1" };                   
                    paraTitle.Append(heading_pPr);
                    Run runTitle = paraTitle.AppendChild(new Run());
                    RunProperties rpTitle = new RunProperties(new Underline() { Val = UnderlineValues.Single });                   
                    runTitle.Append(rpTitle);
                    if ((dt.Rows[0][17].ToString() == "Upfront Payment" || dt.Rows[0][17].ToString() == "Annual") && dt.Rows[0][8].ToString() != "&nbsp;")
                    {
                        runTitle.AppendChild(new Text("Sub : Raising of Invoice"));
                    }
                    else if (dt.Rows[0][17].ToString() == "Royalty" && dt.Rows[0][8].ToString() == "&nbsp;")
                    {
                        runTitle.AppendChild(new Text("Sub : Royalty Dues : Declaration"));
                        Paragraph subdetail = body.AppendChild(new Paragraph());
                        Run runsubdetail = subdetail.AppendChild(new Run());
                        runsubdetail.AppendChild(new Text("Dear Sir / Madam,"));
                        runsubdetail.AppendChild(new Text("Reference Agreement as per details below a royalty payment is due."));
                    }
                    else if (dt.Rows[0][17].ToString() == "Royalty" && dt.Rows[0][8].ToString() != "&nbsp;")
                    {
                        runTitle.AppendChild(new Text("Sub : Raising of Invoice [Royalty Payments]"));
                    }
                    else if(dt.Rows[0][17].ToString()=="Reimbursement" && dt.Rows[0][8].ToString()!="&nbsp;")
                    {
                        runTitle.AppendChild(new Text("Sub:Raising of Invoice [Reimbursement of costs]"));
                    }
                    else if(dt.Rows[0][17].ToString() == "Reimbursement" && dt.Rows[0][8].ToString() == "&nbsp;")
                    {
                        runTitle.AppendChild(new Text("Sub : Reimbursements of costs"));
                        Paragraph subdetail = body.AppendChild(new Paragraph());
                        Run runsubdetail = subdetail.AppendChild(new Run());
                        runsubdetail.AppendChild(new Text("Dear Sir / Madam,"));
                        runsubdetail.AppendChild(new Text("Reference Agreement as per details below,reimbursement of costs are due."));
                    }

                    // Create an empty table.
                    Table table = new Table();

                    // Create a TableProperties object and specify its border information.
                    TableProperties tblProp = new TableProperties(
                        new TableBorders(
                            new TopBorder()
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
                            }
                        )
                    );

                    // Append the TableProperties object to the empty table.
                    table.AppendChild<TableProperties>(tblProp);
                    table.Append(new TableProperties(new TableWidth() { Type = TableWidthUnitValues.Pct, Width = "100%" }));
                   
                    TableRow tr = new TableRow();
                    //tr.Append(new TableRowProperties(new TableWidth() { Type = TableWidthUnitValues.Pct, Width = "100%" }));
                    TableCell tc1 = new TableCell();
                    // Specify the width property of the table cell.
                    //tc1.Append(new TableCellProperties(
                    //    new TableCellWidth() { Type = TableWidthUnitValues.Dxa, Width = "2400" }));
                    // Specify the table cell content.
                    tc1.Append(new Paragraph(new Run(new Text(dt.Rows[0][0].ToString()))));
                    // Append the table cell to the table row.
                    tr.Append(tc1);
                    TableCell tc2 = new TableCell();
                    tc2.Append(new Paragraph(new Run(new Text("Agreement No: "+dt.Rows[0][1]))));
                    tr.Append(tc2);
                    TableCell tc3 = new TableCell();
                    tc3.Append(new Paragraph(new Run(new Text("TT A/c: " + dt.Rows[0][2]))));
                    tr.Append(tc3);

                    TableCellProperties tcp = new TableCellProperties();
                    HorizontalMerge vMerge = new HorizontalMerge()
                    {
                        Val = MergedCellValues.Restart
                    };
                    tcp.Append(vMerge);

                    TableCellProperties tcp1 = new TableCellProperties();
                    HorizontalMerge vMerge1 = new HorizontalMerge()
                    {
                        Val = MergedCellValues.Continue
                    };
                    tcp1.Append(vMerge1);

                    TableRow tr1 = new TableRow();
                    TableCell tcr1 = new TableCell();  
                    tcr1.Append(new Paragraph(new Run(new Text("Title:" + dt.Rows[0][3]))));
                    tcr1.Append(tcp);
                    tr1.Append(tcr1);

                    TableCell tc2r1 = new TableCell();
                    tc2r1.Append(new Paragraph());
                    tc2r1.Append(tcp1);
                    tr1.Append(tc2r1);
                    TableCell tc3r1 = new TableCell();
                    tc3r1.Append(new Paragraph());
                    TableProperties mergeend = new TableProperties();
                    HorizontalMerge hm1 = new HorizontalMerge() { Val = MergedCellValues.Continue };
                    mergeend.Append(hm1);
                    tc3r1.Append(mergeend);
                    tr1.Append(tc3r1);

                    TableCellProperties tcp2 = new TableCellProperties();
                    HorizontalMerge hMerge = new HorizontalMerge()
                    {
                        Val = MergedCellValues.Restart
                    };
                    tcp2.Append(hMerge);
                    TableCellProperties tcp3 = new TableCellProperties();
                    HorizontalMerge hMerge1 = new HorizontalMerge()
                    {
                        Val = MergedCellValues.Continue
                    };
                    tcp3.Append(hMerge1);

                    TableRow tr2 = new TableRow();                    
                    TableCell tcr2 = new TableCell();                    
                    tcr2.Append(new Paragraph(new Run(new Text("Scope: " + dt.Rows[0][4]))));
                    tcr2.Append(tcp2);
                    tr2.Append(tcr2);
                    TableCell tc2r2 = new TableCell();
                    tc2r2.Append(new Paragraph());
                    tc2r2.Append(tcp3);
                    tr2.Append(tc2r2);
                    TableCell tc3r2 = new TableCell();
                    tc3r2.Append(new Paragraph());
                    TableProperties mergeend1 = new TableProperties();
                    HorizontalMerge hm3 = new HorizontalMerge() { Val = MergedCellValues.Continue };
                    mergeend1.Append(hm3);
                    tc3r2.Append(mergeend1);
                    tr2.Append(tc3r2);

                    TableCellProperties tcp4 = new TableCellProperties();
                    HorizontalMerge hMerge2 = new HorizontalMerge()
                    {
                        Val = MergedCellValues.Restart
                    };
                    tcp4.Append(hMerge2);
                    TableCellProperties tcp5 = new TableCellProperties();
                    HorizontalMerge hMerge3 = new HorizontalMerge()
                    {
                        Val = MergedCellValues.Continue
                    };
                    tcp5.Append(hMerge3);

                    TableRow tr3 = new TableRow();
                    TableCell tcr3 = new TableCell();
                    tcr3.Append(new Paragraph(new Run(new Text("Coordinating Person: " + dt.Rows[0][5]))));
                    tcr3.Append(tcp4);
                    tr3.Append(tcr3);
                    TableCell tc2r3 = new TableCell();
                    tc2r3.Append(new Paragraph());
                    tc2r3.Append(tcp5);
                    tr3.Append(tc2r3);
                    TableCell tc3r3 = new TableCell();
                    tc3r3.Append(new Paragraph());
                    TableProperties mergeend2 = new TableProperties();
                    HorizontalMerge hm5 = new HorizontalMerge() { Val = MergedCellValues.Continue };
                    mergeend2.Append(hm5);
                    tc3r3.Append(mergeend2);
                    tr3.Append(tc3r3);

                    TableCellProperties tcp6 = new TableCellProperties();
                    HorizontalMerge hMerge4 = new HorizontalMerge()
                    {
                        Val = MergedCellValues.Restart
                    };
                    tcp6.Append(hMerge4);
                    TableCellProperties tcp7 = new TableCellProperties();
                    HorizontalMerge hMerge5 = new HorizontalMerge()
                    {
                        Val = MergedCellValues.Continue
                    };
                    tcp7.Append(hMerge5);

                    TableRow tr4 = new TableRow();
                    TableCell tc1r4 = new TableCell();
                    tc1r4.Append(new Paragraph(new Run(new Text("Type: " + dt.Rows[0][6]))));
                    tc1r4.Append(tcp6);
                    tr4.Append(tc1r4);
                    TableCell tc2r4 = new TableCell();
                    tc2r4.Append(new Paragraph());
                    tc2r4.Append(tcp7);
                    tr4.Append(tc2r4);
                    TableCell tc3r4 = new TableCell();
                    tc3r4.Append(new Paragraph(new Run(new Text("Dept: " + dt.Rows[0][6]))));
                    tr4.Append(tc3r4);

                    TableCellProperties tcp8 = new TableCellProperties();
                    HorizontalMerge hMerge6 = new HorizontalMerge()
                    {
                        Val = MergedCellValues.Restart
                    };
                    tcp8.Append(hMerge6);
                    TableCellProperties tcp9 = new TableCellProperties();
                    HorizontalMerge hMerge7 = new HorizontalMerge()
                    {
                        Val = MergedCellValues.Continue
                    };
                    tcp9.Append(hMerge7);

                    TableRow tr5 = new TableRow();
                    TableCell tc1r5 = new TableCell();
                    tc1r5.Append(new Paragraph(new Run(new Text("Due Date: " + dt.Rows[0][9]))));
                    tc1r5.Append(tcp8);
                    tr5.Append(tc1r5);
                    TableCell tc2r5 = new TableCell();
                    tc2r5.Append(new Paragraph());
                    tc2r5.Append(tcp9);
                    tr5.Append(tc2r5);
                    TableCell tc3r5 = new TableCell();
                    tc3r5.Append(new Paragraph(new Run(new Text("Amount: " + dt.Rows[0][8]))));
                    tr5.Append(tc3r5);

                    // Append the table row to the table.
                    table.Append(tr);
                    table.Append(tr1);
                    table.Append(tr2);
                    table.Append(tr3);
                    table.Append(tr4);
                    table.Append(tr5);
                    // Append the table to the document.
                    doc.MainDocumentPart.Document.Body.Append(table);

                    if (dt.Rows[0][8].ToString() != "&nbsp;")
                    {
                        Paragraph customer = body.AppendChild(new Paragraph());
                        Run runcustomer = customer.AppendChild(new Run());
                        RunProperties cusprp = new RunProperties(new Bold(), new Underline() { Val = UnderlineValues.Single });
                        //cusprp.Bold = new Bold();
                        //cusprp.Underline = new Underline();
                        runcustomer.Append(new Break());
                        runcustomer.Append(cusprp);
                        runcustomer.AppendChild(new Text("Customer"));

                        Paragraph custdet = body.AppendChild(new Paragraph());
                        Run rundetails = custdet.AppendChild(new Run());
                        Justification center = new Justification() { Val = JustificationValues.Center };
                        rundetails.AppendChild(center);
                        rundetails.AppendChild(new Text(dt.Rows[0][10].ToString()));
                        rundetails.AppendChild(new Break());
                        rundetails.AppendChild(new Text(dt.Rows[0][11].ToString()));
                        rundetails.AppendChild(new Break());
                        rundetails.AppendChild(new Text(dt.Rows[0][12].ToString()));
                        rundetails.AppendChild(new Break());
                        rundetails.AppendChild(new Text(dt.Rows[0][13].ToString()));
                        rundetails.AppendChild(new Break());
                        rundetails.AppendChild(new Text(dt.Rows[0][14].ToString()));
                        rundetails.AppendChild(new Break());
                        rundetails.AppendChild(new Text(dt.Rows[0][15].ToString()));
                        rundetails.AppendChild(new Break());
                        rundetails.AppendChild(new Text(dt.Rows[0][16].ToString()));
                    }
                    Paragraph extra = body.AppendChild(new Paragraph());
                    Run runextra = extra.AppendChild(new Run());
                    runextra.AppendChild(new Break());
                    if ((dt.Rows[0][17].ToString() == "Upfront Payment" || dt.Rows[0][17].ToString() == "Annual") && dt.Rows[0][8] != null)
                    {
                        runextra.AppendChild(new Text("Other available details are attached. The invoice may be raised for the above."));
                    }
                    else if (dt.Rows[0][17].ToString() == "Royalty" && dt.Rows[0][8].ToString() != "&nbsp;")
                    {
                        runextra.AppendChild(new Text("The declaration of dues and workings has been verified by us. Same is enclosed."));
                        runextra.AppendChild(new Break());
                        runextra.AppendChild(new Text("The invoice may be raised for the above"));
                    }
                    else if (dt.Rows[0][17].ToString() == "Royalty" && dt.Rows[0][8].ToString() == "&nbsp;")
                    {
                        runextra.AppendChild(new Text("Request you to send a declaration along with workings. After verification and confirmation we will raise the invoice"));
                        //runextra.AppendChild(new Text("Working sheets of the dues is enclosed. Request you to verify and confirm"));
                        //runextra.AppendChild(new Break());
                        //runextra.AppendChild(new Text("Invoice will be sent on receipt of confirmation"));
                    }
                    else if (dt.Rows[0][17].ToString() == "Reimbursement" && dt.Rows[0][8].ToString() == "&nbsp;")
                    {
                        runextra.AppendChild(new Text("Working sheet of the dues is enclosed. Request you to verify and confirm."));
                        runextra.AppendChild(new Break());
                        runextra.AppendChild(new Text("Invoice will be sent on receipt of confimation"));
                    }
                    else if (dt.Rows[0][17].ToString() == "Reimbursement" && dt.Rows[0][8].ToString() != "&nbsp;")
                    {
                        runextra.AppendChild(new Text("Working sheet along with customer's confirmation is enclosed"));
                        runextra.AppendChild(new Break());
                        runextra.AppendChild(new Text("The invoice may be raised for the above"));
                    }
                    runextra.AppendChild(new Break());
                    runextra.AppendChild(new Break());
                    runextra.AppendChild(new Text("Thanking You"));
                    runextra.AppendChild(new Break());
                    
                    Paragraph support = body.AppendChild(new Paragraph());
                    Run runsup = support.AppendChild(new Run());

                    if (dt.Rows[0][17].ToString() == "Upfront Payment" && dt.Rows[0][8].ToString() != "&nbsp;")
                    {
                        runsup.AppendChild(new Text("Supporting Documents:"));
                        runsup.AppendChild(new Break());
                        runsup.AppendChild(new Text("1. PDF of Agreement -" + dt.Rows[0][0]));
                        runsup.AppendChild(new Break());
                        runsup.AppendChild(new Text("2. Customer Tax & Bank details"));
                    }
                    else if (dt.Rows[0][17].ToString() == "Annual" && dt.Rows[0][8].ToString() != "&nbsp;")
                    {
                        runsup.AppendChild(new Text("Supporting Documents:"));
                        runsup.AppendChild(new Break());
                        runsup.AppendChild(new Text(" 1. Agreement " + dt.Rows[0][0] + " already forwarded"));
                        runsup.AppendChild(new Break());
                        runsup.AppendChild(new Text(" 2. Customer Tax & Bank details are same /change as per enclosure"));
                    }
                    else if (dt.Rows[0][17].ToString() == "Royalty" && dt.Rows[0][8].ToString() != "&nbsp;")
                    {
                        runsup.AppendChild(new Text("Supporting Documents:"));
                        runsup.AppendChild(new Break());
                        runsup.AppendChild(new Text("1. Verified declaration of dues"));
                        runsup.AppendChild(new Break());
                        runsup.AppendChild(new Text("2. Working sheets of the customer"));
                    }
                    else if (dt.Rows[0][17].ToString() == "Reimbursement" && dt.Rows[0][8].ToString() != "&nbsp;")
                    {
                        runsup.AppendChild(new Text("Supporting Documents:"));
                        runsup.AppendChild(new Break());
                        runsup.AppendChild(new Text("1. Confirmation from the customer"));
                        runsup.AppendChild(new Break());
                        runsup.AppendChild(new Text("2. Detailed workings"));
                    }
                    else if (dt.Rows[0][17].ToString() == "Reimbursement" && dt.Rows[0][8].ToString() == "&nbsp;")
                    {
                        runsup.AppendChild(new Text("Enclosure:"));
                        runsup.AppendChild(new Break());
                        runsup.AppendChild(new Text("1. Working document on dues"));
                        runsup.AppendChild(new Break());
                        runsup.AppendChild(new Text("2. Copies of payment invoices"));
                    }
                    runsup.AppendChild(new Break());
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
}
