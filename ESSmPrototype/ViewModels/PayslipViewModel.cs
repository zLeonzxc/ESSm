// pdf imports
using Microsoft.Maui.Controls.PlatformConfiguration;
using Syncfusion.Drawing;
using Syncfusion.Pdf;
using Syncfusion.Pdf.Graphics;


namespace ESSmPrototype.ViewModels
{
    public partial class PayslipViewModel : INotifyPropertyChanged
    {
        private Payslip? _selectedPayslip;

        public ObservableCollection<Payslip>? Payslips { get; set; }

        public ICommand PayslipTappedCommand { get; }

        public event PropertyChangedEventHandler? PropertyChanged;

        public Payslip? SelectedPayslip
        {
            get => _selectedPayslip;
            set
            {
                if (_selectedPayslip != value)
                {
                    _selectedPayslip = value;
                    OnPropertyChanged(nameof(SelectedPayslip));
                }
            }
        }

        public void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public PayslipViewModel()
        {
            // Sample data for Payslips
            Payslips = new ObservableCollection<Payslip>
            {
                new Payslip
                {
                    CompanyLogo = "oshrs.png", 
                    CompanyName = "ABC Corp",
                    EmployeeName = "John Doe",
                    EmployeeCode = "E123",
                    JoinDate = new DateTime(2019, 5, 10),
                    Department = "IT",
                    Designation = "Developer",
                    DateOfPayment = DateTime.Now,
                    ModeOfPayment = "Bank Transfer",
                    AccountNumber = "987654321",
                    Period = new DateTime(2025, 3, 1),
                    BasicSalary = 4000,
                    TransportAllowance = 300,
                    GrossEarnings = 4300,
                    SocsoEmployee = 60,
                    TaxAmount = 400,
                    TotalDeductions = 460,
                    SocsoEmployer = 80,
                    TotalEmployerContributions = 80,
                    NetPay = 3840,
                    YtdEpfEmployee = 600,
                    YtdEpfEmployer = 800,
                    YtdSocsoEmployee = 180,
                    YtdSocsoEmployer = 240,
                    YtdEisEmployee = 60,
                    YtdEisEmployer = 70,
                    YtdPcb = 1200,
                    YtdCp38 = 0,
                    YtdGross = 11500
                },
                new Payslip
                {
                    CompanyLogo = "oshrs.png", 
                    CompanyName = "ABC Corp",
                    EmployeeName = "John Doe",
                    EmployeeCode = "E123",
                    JoinDate = new DateTime(2019, 1, 15),
                    Department = "IT",
                    Designation = "Developer",
                    DateOfPayment = DateTime.Now,
                    ModeOfPayment = "Bank Transfer",
                    AccountNumber = "123456789",
                    Period = new DateTime(2025, 2, 1),
                    BasicSalary = 3000,
                    TransportAllowance = 200,
                    GrossEarnings = 3200,
                    SocsoEmployee = 50,
                    TaxAmount = 300,
                    TotalDeductions = 350,
                    SocsoEmployer = 70,
                    TotalEmployerContributions = 70,
                    NetPay = 2850,
                    YtdEpfEmployee = 500,
                    YtdEpfEmployer = 700,
                    YtdSocsoEmployee = 150,
                    YtdSocsoEmployer = 200,
                    YtdEisEmployee = 50,
                    YtdEisEmployer = 60,
                    YtdPcb = 1000,
                    YtdCp38 = 0,
                    YtdGross = 9600
                },
                new Payslip
                {
                    CompanyLogo = "oshrs.png", 
                    CompanyName = "ABC Corp",
                    EmployeeName = "John Doe",
                    EmployeeCode = "E123",
                    JoinDate = new DateTime(2019, 1, 15),
                    Department = "IT",
                    Designation = "Developer",
                    DateOfPayment = DateTime.Now,
                    ModeOfPayment = "Bank Transfer",
                    AccountNumber = "987654321",
                    Period = new DateTime(2025, 1, 1),
                    BasicSalary = 4000,
                    TransportAllowance = 300,
                    GrossEarnings = 4300,
                    SocsoEmployee = 60,
                    TaxAmount = 400,
                    TotalDeductions = 460,
                    SocsoEmployer = 80,
                    TotalEmployerContributions = 80,
                    NetPay = 3840,
                    YtdEpfEmployee = 600,
                    YtdEpfEmployer = 800,
                    YtdSocsoEmployee = 180,
                    YtdSocsoEmployer = 240,
                    YtdEisEmployee = 60,
                    YtdEisEmployer = 70,
                    YtdPcb = 1200,
                    YtdCp38 = 0,
                    YtdGross = 11500
                }
            };

            PayslipTappedCommand = new Command<Payslip>(OnPayslipTapped);

            DownloadCommand = new Command(OnDownloadCommand);
            EmailCommand = new Command(OnEmailCommand);
        }

        private async void OnPayslipTapped(Payslip selectedPayslip)
        {
            if (selectedPayslip != null)
            {
                SelectedPayslip = selectedPayslip;
                // Navigate to PayslipDetailsPage with the selected period
                await Shell.Current.Navigation.PushAsync(new PayslipDetailsPage(selectedPayslip));
            }

        }

        public ICommand? DownloadCommand { get; set; }
        public ICommand? EmailCommand { get; set; }

        private async void OnDownloadCommand()
        {
            if (SelectedPayslip != null)
            {
                // Get the downloads folder path
                string downloadsPath = string.Empty;

#if ANDROID
        downloadsPath = Android.OS.Environment.GetExternalStoragePublicDirectory(Android.OS.Environment.DirectoryDownloads)?.AbsolutePath ?? string.Empty;
#endif

                if (string.IsNullOrEmpty(downloadsPath))
                {
                    await Application.Current.MainPage.DisplayAlert("Error", "Unable to access the downloads folder.", "OK");
                    return;
                }

                // Generate the file path
                string outputFilePath = Path.Combine(downloadsPath, $"{SelectedPayslip.EmployeeName}_Payslip_{SelectedPayslip.Period:yyyy-MM}.pdf");

                // Generate PDF
                GeneratePayslipPdf(SelectedPayslip, outputFilePath);

                // Show success message
                await Application.Current.MainPage.DisplayAlert("Success", $"Payslip downloaded successfully to {downloadsPath}.", "OK");
            }
        }

        private async void OnEmailCommand()
        {
            if (SelectedPayslip == null)
            {
                await Application.Current.MainPage.DisplayAlert("Error", "No payslip selected.", "OK");
                return;
            }

            // Prompt the user to enter an email address
            string emailAddress = await Application.Current.MainPage.DisplayPromptAsync(
                "Send Payslip",
                "Enter the recipient's email address:",
                "Send",
                "Cancel",
                placeholder: "example@example.com",
                keyboard: Keyboard.Email);

            if (string.IsNullOrWhiteSpace(emailAddress))
            {
                await Application.Current.MainPage.DisplayAlert("Error", "Email address is required.", "OK");
                return;
            }

            // Generate the PDF file
            string downloadsPath = string.Empty;

#if ANDROID
    downloadsPath = Android.OS.Environment.GetExternalStoragePublicDirectory(Android.OS.Environment.DirectoryDownloads)?.AbsolutePath ?? string.Empty;
#endif

            if (string.IsNullOrEmpty(downloadsPath))
            {
                await Application.Current.MainPage.DisplayAlert("Error", "Unable to access the downloads folder.", "OK");
                return;
            }

            string outputFilePath = Path.Combine(downloadsPath, $"{SelectedPayslip.EmployeeName}_Payslip_{SelectedPayslip.Period:yyyy-MM}.pdf");
            GeneratePayslipPdf(SelectedPayslip, outputFilePath);

            // Send the email with the PDF attachment
            try
            {
                await SendEmailWithAttachment(emailAddress, "Payslip", "Please find the attached payslip.", outputFilePath);
                await Application.Current.MainPage.DisplayAlert("Success", "Payslip sent successfully.", "OK");
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Error", $"Failed to send email: {ex.Message}", "OK");
            }
        }

        private void GeneratePayslipPdf(Payslip payslip, string outputFilePath)
        {
            using (PdfDocument document = new PdfDocument())
            {
                PdfPage page = document.Pages.Add();
                PdfGraphics graphics = page.Graphics;

                // Define fonts
                PdfFont headerFont = new PdfStandardFont(PdfFontFamily.Helvetica, 14, PdfFontStyle.Bold);
                PdfFont boldFont = new PdfStandardFont(PdfFontFamily.Helvetica, 10, PdfFontStyle.Bold);
                PdfFont regularFont = new PdfStandardFont(PdfFontFamily.Helvetica, 10);
                PdfFont ytdHeaderFont = new PdfStandardFont(PdfFontFamily.Helvetica, 8, PdfFontStyle.Bold);

                // Define layout settings
                float margin = 20;
                float y = margin;
                float pageWidth = page.GetClientSize().Width;

                // Replace the problematic line with the following:  
                graphics.DrawString(payslip.CompanyName ?? "Company Name", headerFont, PdfBrushes.Black, new Syncfusion.Drawing.PointF(pageWidth / 2, y), new PdfStringFormat { Alignment = PdfTextAlignment.Center });
                y += 30;

                // Draw employee information
                float columnWidth = (pageWidth - 2 * margin) / 3;
                float rowHeight = 25;

                void DrawRow(string label, string value, float x, float y)
                {
                    graphics.DrawString(label, boldFont, PdfBrushes.Black, new Syncfusion.Drawing.PointF(x, y));
                    graphics.DrawString(value, regularFont, PdfBrushes.Black, new Syncfusion.Drawing.PointF(x + 95, y));
                }

                DrawRow("Name of Employee:", payslip.EmployeeName ?? "", margin, y);
                DrawRow("Employee Code:", payslip.EmployeeCode ?? "", margin + columnWidth, y);
                DrawRow("Join Date:", payslip.JoinDate.ToString("dd/MM/yyyy"), margin + 2 * columnWidth, y);
                y += rowHeight;

                DrawRow("Department:", payslip.Department ?? "", margin, y);
                DrawRow("Designation:", payslip.Designation ?? "", margin + columnWidth, y);
                DrawRow("Date of Payment:", payslip.DateOfPayment.ToString("dd/MM/yyyy"), margin + 2 * columnWidth, y);
                y += rowHeight;

                DrawRow("Mode of Payment:", payslip.ModeOfPayment ?? "", margin, y);
                DrawRow("Account No:", payslip.AccountNumber ?? "", margin + columnWidth, y);
                DrawRow("Period:", payslip.Period.ToString("yyyy-MM"), margin + 2 * columnWidth, y);
                y += rowHeight + 10;

                // Draw salary payment statement title
                graphics.DrawString($"Salary Payment Statement ({payslip.Period:yyyy-MM})", boldFont, PdfBrushes.Black, new Syncfusion.Drawing.PointF(margin, y));
                y += rowHeight;

                // Draw earnings, deductions, and employer contributions table
                float tableWidth = pageWidth - 2 * margin;
                float cellWidth = tableWidth / 3;
                float cellHeight = 25;

                void DrawTableCell(string text, float x, float y, bool isHeader = false)
                {
                    PdfFont font = isHeader ? boldFont : regularFont;
                    graphics.DrawRectangle(PdfPens.Black, new RectangleF(x, y, cellWidth, cellHeight));
                    graphics.DrawString(text, font, PdfBrushes.Black, new RectangleF(x + 5, y + 5, cellWidth - 10, cellHeight - 10), new PdfStringFormat { Alignment = PdfTextAlignment.Left });
                }

                // Table headers
                DrawTableCell("Earnings", margin, y, true);
                DrawTableCell("Deductions", margin + cellWidth, y, true);
                DrawTableCell("Employer Contributions", margin + 2 * cellWidth, y, true);
                y += cellHeight;

                // Table rows
                DrawTableCell($"Basic Salary: {payslip.BasicSalary:C}", margin, y);
                DrawTableCell($"SOCSO Employee: {payslip.SocsoEmployee:C}", margin + cellWidth, y);
                DrawTableCell($"SOCSO Employer: {payslip.SocsoEmployer:C}", margin + 2 * cellWidth, y);
                y += cellHeight;

                DrawTableCell($"Transport Allowance: {payslip.TransportAllowance:C}", margin, y);
                DrawTableCell($"Tax Amount: {payslip.TaxAmount:C}", margin + cellWidth, y);
                DrawTableCell($"Total Employer Contributions: {payslip.TotalEmployerContributions:C}", margin + 2 * cellWidth, y);
                y += cellHeight;

                DrawTableCell($"Gross Earnings: {payslip.GrossEarnings:C}", margin, y);
                DrawTableCell($"Total Deductions: {payslip.TotalDeductions:C}", margin + cellWidth, y);
                DrawTableCell($"Net Pay: {payslip.NetPay:C}", margin + 2 * cellWidth, y);
                y += cellHeight + 10;

                // Draw Year to Date (YTD) section
                graphics.DrawString($"Year to Date ({payslip.Period:yyyy})", boldFont, PdfBrushes.Black, new Syncfusion.Drawing.PointF(margin, y));
                y += rowHeight;

                // YTD table
                float ytdCellWidth = tableWidth / 9;
                float ytdHeaderHeight = cellHeight * 2; // Double the height for the header section
                string[] ytdHeaders = { "YTD EPF", "Employee", "YTD EPF", "Employer", "YTD SOCSO", "Employee", "YTD SOCSO", "Employer", "YTD EIS", "Employee", "YTD EIS", "Employer", "YTD", "PCB", "YTD", "CP38", "YTD", "Gross" };
                double[] ytdValues = { payslip.YtdEpfEmployee, payslip.YtdEpfEmployer, payslip.YtdSocsoEmployee, payslip.YtdSocsoEmployer, payslip.YtdEisEmployee, payslip.YtdEisEmployer, payslip.YtdPcb, payslip.YtdCp38, payslip.YtdGross };

                // Draw YTD headers (split into two rows)
                for (int i = 0; i < ytdHeaders.Length; i += 2)
                {
                    graphics.DrawRectangle(PdfPens.Black, new RectangleF(margin + (i / 2) * ytdCellWidth, y, ytdCellWidth, ytdHeaderHeight));
                    graphics.DrawString(ytdHeaders[i], ytdHeaderFont, PdfBrushes.Black, new RectangleF(margin + (i / 2) * ytdCellWidth + 5, y + 5, ytdCellWidth - 10, cellHeight - 10), new PdfStringFormat { Alignment = PdfTextAlignment.Center });
                    if (i + 1 < ytdHeaders.Length)
                    {
                        graphics.DrawString(ytdHeaders[i + 1], ytdHeaderFont, PdfBrushes.Black, new RectangleF(margin + (i / 2) * ytdCellWidth + 5, y + cellHeight, ytdCellWidth - 10, cellHeight - 10), new PdfStringFormat { Alignment = PdfTextAlignment.Center });
                    }
                }
                y += ytdHeaderHeight;

                // Draw YTD values
                for (int i = 0; i < ytdValues.Length; i++)
                {
                    graphics.DrawRectangle(PdfPens.Black, new RectangleF(margin + i * ytdCellWidth, y, ytdCellWidth, cellHeight));
                    graphics.DrawString($"{ytdValues[i]:F2}", regularFont, PdfBrushes.Black, new RectangleF(margin + i * ytdCellWidth + 5, y + 5, ytdCellWidth - 10, cellHeight - 10), new PdfStringFormat { Alignment = PdfTextAlignment.Center });
                }

                // Save the PDF
                using (FileStream outputStream = new FileStream(outputFilePath, FileMode.Create, FileAccess.Write))
                {
                    document.Save(outputStream);
                }
            }
        }

        private async Task SendEmailWithAttachment(string toEmail, string subject, string body, string attachmentPath)
        {
            // Use platform-specific email functionality
#if ANDROID || IOS
    var message = new EmailMessage
    {
        Subject = subject,
        Body = body,
        To = new List<string> { toEmail }
    };

    if (File.Exists(attachmentPath))
    {
        message.Attachments.Add(new EmailAttachment(attachmentPath));
    }

    await Email.Default.ComposeAsync(message);
#else
            throw new NotSupportedException("Email functionality is not supported on this platform.");
#endif
        }
    }
}
