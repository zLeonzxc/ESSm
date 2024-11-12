namespace ESSmPrototype.Views.Employee;

public partial class EmployeeLeaveDetailsPage : ContentPage
{
    public EmployeeLeaveDetailsPage(LeaveRequest leaveRequest)
    {
        InitializeComponent();

        var viewModel = new EmployeeLeaveDetailsViewModel
        {
            AcceptCommand = new Command(OnAcceptCommand),
            RejectCommand = new Command(OnRejectCommand),
            SelectedLeaveRequest = leaveRequest
        };

        BindingContext = viewModel;
    }

    private async void OnAcceptCommand()
    {
        var viewModel = (EmployeeLeaveDetailsViewModel)BindingContext;
        if (viewModel.SelectedLeaveRequest != null)
        {
            viewModel.SelectedLeaveRequest.LeaveApprovalStatus = "Approved";
            viewModel.OnPropertyChanged(nameof(viewModel.SelectedLeaveRequest.LeaveApprovalStatus));
        }
        await Task.Delay(1000);
    }

    private async void OnRejectCommand()
    {
        var viewModel = (EmployeeLeaveDetailsViewModel)BindingContext;
        if (viewModel.SelectedLeaveRequest != null)
        {
            viewModel.SelectedLeaveRequest.LeaveApprovalStatus = "Rejected";
            viewModel.OnPropertyChanged(nameof(viewModel.SelectedLeaveRequest.LeaveApprovalStatus));
        }
        await Task.Delay(1000);
    }
}
