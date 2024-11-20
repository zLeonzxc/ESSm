namespace ESSmPrototype.Views.Employee
{
    public partial class EmployeeLeaveList : ContentPage
    {
        public EmployeeLeaveList()
        {
            InitializeComponent();
            BindingContext = new EmployeeLeaveListViewModel();
        }
    }
}
