namespace ESSmPrototype.Views.Employees;

public partial class EmployeeLeaveDetailsPageAdmin : ContentPage
{
    public EmployeeLeaveDetailsPageAdmin(LeaveRequest leaveRequest)
    {
        InitializeComponent();

        var viewModel = new EmployeeLeaveDetailsViewModel
        {
            SelectedLeaveRequest = leaveRequest
        };

        BindingContext = viewModel;
    }
}

