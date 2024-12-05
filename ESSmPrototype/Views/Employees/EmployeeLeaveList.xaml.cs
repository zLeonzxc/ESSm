namespace ESSmPrototype.Views.Employees
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
