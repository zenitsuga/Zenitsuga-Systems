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
using ERP.ClassFile;
using BarcodeLib;

namespace ERP
{
    public partial class ViewSOA : Form
    {
        clsDatabaseTransactions dtrans = new clsDatabaseTransactions();
        public string BillingMonth;
        public string BillingYear;
        public string CompanyName;
        public string UnitNo;
        public string Owner;

        public ViewSOA()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DataTable dtBillingTransaction = new DataTable();
            if(string.IsNullOrEmpty(textBox1.Text))
            {
                MessageBox.Show("Error: Please enter Transaction Code to generated.", "Invalid Transaction Code", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }else{
                string QueryBilling = "SELECT MONTHNAME(co.BillStart) AS MonthName,co.YEAR,co.BillStart,co.BillEnd, ui.UnitName,CONCAT(ci.LastName,',', ci.FirstName,' ',ci.MiddleName) AS CustomerName,bd.BillingAmount,bd.BillingDescription,bd.BillingNotes FROM tbl_condo_billinginfo bi LEFT JOIN tbl_condo_billingdetails bd ON bi.sysID = bd.BillingRefID LEFT JOIN tbl_condo_cutoffinfo co ON bi.CutoffID = co.sysID LEFT JOIN tbl_condo_unitinfo ui ON bi.UnitNo = ui.SysID LEFT JOIN tbl_condo_customerinfo ci ON bi.CustomerID = ci.sysID WHERE bi.PrimaryKey = '" + textBox1.Text + "' and bi.isEnabled = 1 and bi.isVoid = 0";
                
                dtBillingTransaction = dtrans.SelectData(QueryBilling);
                
                if (dtBillingTransaction.Rows.Count == 0)
                {
                    MessageBox.Show("Error: Please enter Transaction Code to generated.", "No Transaction Code found", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                string ClearTextPath = Environment.CurrentDirectory + "\\CONDO\\REPORTING\\SOA_Clear.pdf";
                webBrowser1.Stop();

                webBrowser1.Navigate("about:blank");
                while (webBrowser1.ReadyState != WebBrowserReadyState.Complete)
                {
                    Application.DoEvents();
                    Thread.Sleep(1000);
                }

                
                string OutputPath = Environment.CurrentDirectory + "\\CONDO\\REPORTING\\SOA.pdf";
                
                GeneratePDF(OutputPath, dtBillingTransaction);
                
                if (this.webBrowser1 != null)
                {
                    this.webBrowser1.Navigate(OutputPath);
                }
                
            }
        }

        private string GenerateBarcode(string BarcodeData)
        {
            string result = string.Empty;
            try
            {
                BarcodeLib.Barcode barcode = new BarcodeLib.Barcode()
                {
                    IncludeLabel = false,
                    Alignment = AlignmentPositions.CENTER,
                    Width = 500,
                    Height = 150,
                    RotateFlipType = RotateFlipType.RotateNoneFlipNone,
                    BackColor = Color.White,
                    ForeColor = Color.Black,
                };

                System.Drawing.Image img = barcode.Encode(TYPE.CODE39, BarcodeData);
                string BarcodePath = Environment.CurrentDirectory + "\\CONDO\\REPORTING\\" + BarcodeData + ".jpg";
                if (File.Exists(BarcodePath))
                {
                    File.Delete(BarcodePath);
                }
                img.Save(BarcodePath);
                result = BarcodePath;
            }
            catch
            {
            }
            return result;
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

        private void GeneratePDF(string OutputPath,DataTable dtBillinginfo)
        {
            try
            {
                if (File.Exists(OutputPath))
                {
                    File.Delete(OutputPath);
                }

                BillingMonth = dtBillinginfo.Rows[0]["MonthName"].ToString();
                BillingYear = dtBillinginfo.Rows[0]["YEAR"].ToString();
                Owner = dtBillinginfo.Rows[0]["CUSTOMERNAME"].ToString();
                UnitNo = dtBillinginfo.Rows[0]["UnitName"].ToString();


                Document doc = new Document(PageSize.A4);
                var output = new FileStream(OutputPath, FileMode.OpenOrCreate);
                var writer = PdfWriter.GetInstance(doc, output);

                doc.Open();

                string BarcodePath = GenerateBarcode(textBox1.Text);

                var barcode = iTextSharp.text.Image.GetInstance(BarcodePath);
                barcode.SetAbsolutePosition(8, 770);
                barcode.ScaleAbsoluteHeight(30);
                barcode.ScaleAbsoluteWidth(250);
                doc.Add(barcode);
                
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

                PdfPCell cellBarcode = new PdfPCell();
                cellBarcode.Colspan = 1;
                cellBarcode.HorizontalAlignment = Element.ALIGN_RIGHT;
                cellBarcode.AddElement(new Paragraph(" "));

                AddText("Transaction No.:", 400, 795, writer);
                AddText(textBox1.Text, 490, 795, writer);

                AddText("Date:", 371, 785, writer);
                AddText(DateTime.Now.ToString("MMMM dd, yyyy"), 490, 785, writer);

                AddText("Billing Month:", 395, 775, writer);
                AddText(BillingMonth + " " + BillingYear, 484, 775, writer);

                cellBarcode.AddElement(new Paragraph(" "));

                PdfPCell cellOwner = new PdfPCell();
                cellOwner.Colspan = 1;
                cellOwner.FixedHeight = 50;
                cellOwner.HorizontalAlignment = Element.ALIGN_LEFT;
                cellOwner.AddElement(new Paragraph(" "));

                AddText(CompanyName, 100, 755, writer);
                AddText(Owner, 111, 743, writer);
                AddText("UNIT:" + UnitNo, 65, 730, writer);

                PdfPCell cell12 = new PdfPCell();


                cell12.VerticalAlignment = Element.ALIGN_CENTER;


                table1.AddCell(cellBarcode);

                table1.AddCell(cellOwner);


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

        private void ViewSOA_Load(object sender, EventArgs e)
        {
            clsIni ci = new clsIni(Environment.CurrentDirectory + "\\" + "settings.ini");
            CompanyName = ci.Read("CompanyName", "SystemSettings"); 
        }
    }
}
