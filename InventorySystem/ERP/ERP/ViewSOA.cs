using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.Threading;

namespace ERP
{
    public partial class ViewSOA : Form
    {
        public string BillingMonth;

        public ViewSOA()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(string.IsNullOrEmpty(textBox1.Text))
            {
                MessageBox.Show("Error: Please enter Transaction Code to generated.", "Invalid Transaction Code", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }else{
                string ClearTextPath = Environment.CurrentDirectory + "\\CONDO\\REPORTING\\SOA_Clear.pdf";
                webBrowser1.Stop();

                webBrowser1.Navigate("about:blank");
                while (webBrowser1.ReadyState != WebBrowserReadyState.Complete)
                {
                    sApplication.DoEvents();
                    Thread.Sleep(1000);
                }

                string OutputPath = Environment.CurrentDirectory + "\\CONDO\\REPORTING\\SOA.pdf";
                
                GeneratePDF(OutputPath);
                
                if (this.webBrowser1 != null)
                {
                    this.webBrowser1.Navigate(OutputPath);
                }
            }
        }

        private PdfContentByte AddText(string Value,int x,int y,PdfWriter writer)
        {
            PdfContentByte pcResult = writer.DirectContent;
            try
            {
                pcResult.BeginText();
                // put the alignment and coordinates here
                pcResult.ShowTextAligned(1, Value, x, y, 0);
                pcResult.EndText();
            }catch
            {
            }
            return pcResult;
        }

        private void GeneratePDF(string OutputPath)
        {
            try
            {
                if (File.Exists(OutputPath))
                {
                    File.Delete(OutputPath);
                }

                Document doc = new Document(PageSize.A4);
                var output = new FileStream(OutputPath, FileMode.OpenOrCreate);
                var writer = PdfWriter.GetInstance(doc, output);

                doc.Open();

                //var barcode = iTextSharp.text.Image.GetInstance(Server.MapPath("~/ABsIS_Logo.jpg"));
                //barcode.SetAbsolutePosition(430, 770);
                //barcode.ScaleAbsoluteHeight(30);
                //barcode.ScaleAbsoluteWidth(70);
                //doc.Add(barcode);
                PdfContentByte cb = writer.DirectContent;

                // select the font properties
                BaseFont bf = BaseFont.CreateFont(BaseFont.TIMES_BOLD, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
                cb.SetFontAndSize(bf, 12);

                // write the text in the pdf content
                cb.BeginText();
                string title = "STATEMENT OF ACCOUNT";
                // put the alignment and coordinates here
                cb.ShowTextAligned(1, title, 300, 810, 0);
                cb.EndText();


                PdfPTable table1 = new PdfPTable(1);
                table1.DefaultCell.Border = 0;
                table1.WidthPercentage = 100;


                PdfPCell cell11 = new PdfPCell();
                cell11.Colspan = 1;
                cell11.HorizontalAlignment = Element.ALIGN_RIGHT;
                cell11.AddElement(new Paragraph(" "));

                AddText("Transaction No.:", 400, 795, writer);
                AddText(textBox1.Text, 451, 795, writer);

                AddText("Date:", 371, 785, writer);
                AddText(DateTime.Now.ToString("MMMM dd, yyyy"), 484, 785, writer);

                AddText("Month:", 371, 775, writer);
                AddText(BillingMonth, 484, 775, writer);

                cell11.AddElement(new Paragraph(" "));

                PdfPCell cell12 = new PdfPCell();


                cell12.VerticalAlignment = Element.ALIGN_CENTER;


                table1.AddCell(cell11);

                table1.AddCell(cell12);


                PdfPTable table2 = new PdfPTable(3);

                //One row added

                PdfPCell cell21 = new PdfPCell();

                cell21.AddElement(new Paragraph("Photo Type"));

                PdfPCell cell22 = new PdfPCell();

                cell22.AddElement(new Paragraph("No. of Copies"));

                PdfPCell cell23 = new PdfPCell();

                cell23.AddElement(new Paragraph("Amount"));


                table2.AddCell(cell21);

                table2.AddCell(cell22);

                table2.AddCell(cell23);


                //New Row Added

                PdfPCell cell31 = new PdfPCell();

                cell31.AddElement(new Paragraph("Safe"));

                cell31.FixedHeight = 300.0f;

                PdfPCell cell32 = new PdfPCell();

                cell32.AddElement(new Paragraph("2"));

                cell32.FixedHeight = 300.0f;

                PdfPCell cell33 = new PdfPCell();

                cell33.AddElement(new Paragraph("20.00 * " + "2" + " = " + (20 * Convert.ToInt32("2")) + ".00"));

                cell33.FixedHeight = 300.0f;



                table2.AddCell(cell31);

                table2.AddCell(cell32);

                table2.AddCell(cell33);


                PdfPCell cell2A = new PdfPCell(table2);

                cell2A.Colspan = 2;

                table1.AddCell(cell2A);

                PdfPCell cell41 = new PdfPCell();

                cell41.AddElement(new Paragraph("Name : " + "ABC"));

                cell41.AddElement(new Paragraph("Advance : " + "advance"));

                cell41.VerticalAlignment = Element.ALIGN_LEFT;

                PdfPCell cell42 = new PdfPCell();

                cell42.AddElement(new Paragraph("Customer ID : " + "011"));

                cell42.AddElement(new Paragraph("Balance : " + "3993"));

                cell42.VerticalAlignment = Element.ALIGN_RIGHT;


                table1.AddCell(cell41);

                table1.AddCell(cell42);


                doc.Add(table1);

                doc.Close();
                doc.Dispose();
            }
            catch(Exception ex)
            {
                MessageBox.Show("Error:" + ex.Message.ToString(), "Exceptional Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
