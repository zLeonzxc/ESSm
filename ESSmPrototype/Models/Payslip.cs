namespace ESSmPrototype.Models
{
    public class Payslip
    {
        // Company Information
        public string? CompanyLogo { get; set; }
        public string? CompanyName { get; set; }

        // Employee Information
        public string? EmployeeName { get; set; }
        public string? EmployeeCode { get; set; }
        public DateTime JoinDate { get; set; }
        public string? Department { get; set; }
        public string? Designation { get; set; }

        // Payment Information
        public DateTime DateOfPayment { get; set; }
        public string? ModeOfPayment { get; set; }
        public string? AccountNumber { get; set; }
        public DateTime Period { get; set; }

        // Earnings
        public double BasicSalary { get; set; }
        public double TransportAllowance { get; set; }
        public double GrossEarnings { get; set; }

        // Deductions
        public double SocsoEmployee { get; set; }
        public double TaxAmount { get; set; }
        public double TotalDeductions { get; set; }

        // Employer Contributions
        public double SocsoEmployer { get; set; }
        public double TotalEmployerContributions { get; set; }

        // Net Pay
        public double NetPay { get; set; }

        // Year To Date (YTD) Information
        public double YtdEpfEmployee { get; set; }
        public double YtdEpfEmployer { get; set; }
        public double YtdSocsoEmployee { get; set; }
        public double YtdSocsoEmployer { get; set; }
        public double YtdEisEmployee { get; set; }
        public double YtdEisEmployer { get; set; }
        public double YtdPcb { get; set; }
        public double YtdCp38 { get; set; }
        public double YtdGross { get; set; }
    }
}
