namespace ESSmPrototype.Views.Employee;

public partial class EmployeeLeaveDetailsPage : ContentPage
{
	public EmployeeLeaveDetailsPage()
	{
		InitializeComponent();

        var viewModel = new EmployeeLeaveDetailsViewModel();

        viewModel.AcceptCommand = new Command(OnAcceptCommand);

        viewModel.RejectCommand = new Command(OnRejectCommand);
														
		BindingContext = new EmployeeLeaveDetailsViewModel("MY001", // Employee ID
														   "John",  // Employee Name
														   "Software Engineer", // Employee Position
														   "Pending", // Leave Approval Status
														   "Medical Leave"); // Leave Reason
	}

    private async void OnAcceptCommand()
    {
        var viewModel = (EmployeeLeaveDetailsViewModel)BindingContext;
        viewModel.LeaveApprovalStatus = "Approved";
        viewModel.OnPropertyChanged(nameof(viewModel.LeaveApprovalStatus));
        await Task.Delay(1000);
    }

    private async void OnRejectCommand()
    {
        var viewModel = (EmployeeLeaveDetailsViewModel)BindingContext;
        viewModel.LeaveApprovalStatus = "Rejected";
        viewModel.OnPropertyChanged(nameof(viewModel.LeaveApprovalStatus));
        await Task.Delay(1000);
    }

}