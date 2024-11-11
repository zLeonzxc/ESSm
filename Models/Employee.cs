namespace ESSmPrototype.Models
{
    public class Employee(string employeeID, string employeeName, string employeeLegalName, string employeeLoginID, string country) : INotifyPropertyChanged
    {
        public string? EmployeeID { get; set; } = employeeID;
        public string? EmployeeName { get; set; } = employeeName;
        public string? EmployeeLegalName { get; set; } = employeeLegalName;
        public string? EmployeeLoginID { get; set; } = employeeLoginID;
        public string? Country { get; set; } = country;

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }



    }
}
