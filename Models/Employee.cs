namespace ESSmPrototype.Models
{
    public class Employee : INotifyPropertyChanged
    {
        public string? EmployeeID { get; set; } 
        public string? EmployeeName { get; set; } 
        public string? EmployeeLegalName { get; set; } 
        public string? EmployeeLoginID { get; set; } 
        public string? Country { get; set; } 

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public Employee(string employeeID, string employeeName, string employeeLegalName, string employeeLoginID, string country)
        {
            EmployeeID = employeeID;
            EmployeeName = employeeName;
            EmployeeLegalName = employeeLegalName;
            EmployeeLoginID = employeeLoginID;
            Country = country;
        }

        public Employee(string employeeLegalName)
        {
            EmployeeLegalName = employeeLegalName;
        }



    }
}
