namespace ESSmPrototype.Views.Employee
{
    public partial class EmployeeLeaveList : ContentPage
    {
        public EmployeeLeaveList()
        {
            InitializeComponent();
            BindingContext = new EmployeeLeaveListViewModel();
        }

        private void OnEmpIDChanged(object sender, TextChangedEventArgs e)
        {
            if (BindingContext is EmployeeLeaveListViewModel vm)
            {
                vm.EmpID = e.NewTextValue;
            }
        }

        private void OnEmpNameChanged(object sender, TextChangedEventArgs e)
        {
            if (BindingContext is EmployeeLeaveListViewModel vm)
            {
                vm.EmpName = e.NewTextValue;
            }
        }

        private void OnMonthSelectedIndexChanged(object sender, EventArgs e)
        {
            if (BindingContext is EmployeeLeaveListViewModel vm && sender is Picker picker)
            {
                vm.SelectedMonth = picker.SelectedItem as string;
            }
        }

        private void OnYearSelectedIndexChanged(object sender, EventArgs e)
        {
            if (BindingContext is EmployeeLeaveListViewModel vm && sender is Picker picker)
            {
                vm.SelectedYear = picker.SelectedItem as int?;
            }
        }

        private void OnLeaveDescriptionSelectedIndexChanged(object sender, EventArgs e)
        {
            if (BindingContext is EmployeeLeaveListViewModel vm && sender is Picker picker)
            {
                vm.SelectedLeaveDescription = picker.SelectedItem as string;
            }
        }

        private void OnLeaveStatusSelectedIndexChanged(object sender, EventArgs e)
        {
            if (BindingContext is EmployeeLeaveListViewModel vm && sender is Picker picker)
            {
                vm.SelectedLeaveStatus = picker.SelectedItem as string;
            }
        }

    }
}
