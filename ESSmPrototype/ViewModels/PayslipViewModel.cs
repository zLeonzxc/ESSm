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
            // Example data for Payslips
            Payslips = new ObservableCollection<Payslip>
            {
                new Payslip
                {
                    CompanyLogo = null, // Replace with actual image
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
                    CompanyLogo = null, // Replace with actual image
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
                    CompanyLogo = null, // Replace with actual image
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

    }
}
