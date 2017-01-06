using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using System.IO;

namespace gbsExtranetMVC.Helpers
{
    /// <summary>
    /// Created By: Usman Akram
    /// Date: 18-05-2015
    /// PACE SOFTWARE DEVELOPMENT LTD
    /// </summary>
    public static class ExportToWord
    {
        /// <summary>
        /// Create Word Document
        /// </summary>
        /// <param name="DocumentFileName_Path">Complete Path to Offer Letter's Document Path</param>
        /// <param name="ListOfHeadings">List of Heading to Display on Top of this Document</param>
        /// <param name="ListOfText">List of Text to Display in Numbering List</param>
        public static void CreateWordDocument(string DocumentFileName_Path, List<string> ListOfHeadings, List<string> ListOfText)
        {

            using (WordprocessingDocument myDoc =
           WordprocessingDocument.Create(DocumentFileName_Path,
                         WordprocessingDocumentType.Document))
            {
                // Add a new main document part. 
                MainDocumentPart mainPart = myDoc.AddMainDocumentPart();
                //Create Document tree for simple document. 
                mainPart.Document = new Document();

                //Create Body (this element contains
                //other elements that we want to include 
                Body body = new Body();
                //Create paragraph 

                AddHeaderStylePart(ref mainPart);

                foreach (var item in ListOfHeadings)
                {
                    InsertHeading(item, ref body);
                }
                //Create Paragraph with Numbering List
                CreateNumberListParagragh(ListOfText, ref body);


                mainPart.Document.Append(body);
                // Save changes to the main document part. 
                mainPart.Document.Save();

            }
        }

        public static void InsertHeading(string Text, ref Body body)
        {
            body.Append(CreateHeading(Text));
        }

        public static void CreateNumberListParagragh(List<string> listOfText, ref Body body)
        {
            foreach (var item in listOfText)
            {
                body.Append(GenerateListParagraph(item));
            }
        }

        public static Paragraph GenerateListParagraph(string Text)
        {
            var element =
            new Paragraph(
            new ParagraphProperties(
            new ParagraphStyleId() { Val = "ListParagraph" },
            new NumberingProperties(
            new NumberingLevelReference() { Val = 0 },
            new NumberingId() { Val = 1 })),
            new Run(
            new Text(Text))//Text you want to insert with number
            ) { RsidParagraphAddition = "005F3962", RsidParagraphProperties = "00330DA9", RsidRunAdditionDefault = "00330DA9" };
            return element;
        }

        private static Paragraph CreateHeading(string text)
        {

            Paragraph par = new Paragraph(new ParagraphProperties());
            par.ParagraphProperties.TextAlignment = new TextAlignment();
            par.ParagraphProperties.TextAlignment.Val = VerticalTextAlignmentValues.Top;
            par.ParagraphProperties.Justification = new Justification();
            par.ParagraphProperties.Justification.Val = JustificationValues.Center;
            par.ParagraphProperties.ParagraphStyleId = new ParagraphStyleId() { Val = "MyHeading1" };

            par.ParagraphProperties.Indentation = new Indentation();

            par.ParagraphProperties.Indentation.FirstLine = "12";

            Run run = new Run(new Text(text));

            //run.Append(new Break() { Type = BreakValues.Page });    // optional

            par.Append(run);

            return par;

        }

        /// <summary>
        /// Header Style name will be "MyHeading1"
        /// </summary>
        /// <param name="mainPart"></param>
        public static void AddHeaderStylePart(ref MainDocumentPart mainPart)
        {
            StyleDefinitionsPart stylePart = mainPart.AddNewPart<StyleDefinitionsPart>();

            // we have to set the properties
            RunProperties rPr = new RunProperties();
            Color color = new Color() { Val = "black" }; // the color is red

            RunFonts rFont = new RunFonts();
            rFont.Ascii = "Arial"; // the font is Arial
            rPr.Append(color);
            rPr.Append(rFont);

            rPr.Append(new Bold()); // it is Bold
            rPr.Append(new FontSize() { Val = "28" }); //font size (in 1/72 of an inch)

            //creation of a style
            Style style = new Style();
            style.StyleId = "MyHeading1"; //this is the ID of the style
            style.Append(new Name() { Val = "My Heading 1" }); //this is name
            // our style based on Normal style
            style.Append(new BasedOn() { Val = "Heading1" });
            // the next paragraph is Normal type
            style.Append(new NextParagraphStyle() { Val = "Normal" });
            style.Append(rPr);//we are adding properties previously defined

            // we have to add style that we have created to the StylePart
            stylePart.Styles = new Styles();
            stylePart.Styles.Append(style);
            stylePart.Styles.Save(); // we save the style part
        }

        public static void CreateNormalParagragh(string Text)
        {
            Paragraph paragraph = new Paragraph();
            Run run_paragraph = new Run();
            // we want to put that text into the output document 
            Text text_paragraph = new Text("Hello World!");
            //Append elements appropriately. 
            run_paragraph.Append(text_paragraph);
            paragraph.Append(run_paragraph);
        }

    }
}