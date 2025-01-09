namespace ESSmPrototype.Views.Employees;

public partial class EmployeeOvertimeDetailsPageAdmin : ContentPage
{
	public EmployeeOvertimeDetailsPageAdmin(OTRequest otRequest)
	{
		InitializeComponent();

		var viewModel = new PendingApprovalViewModel
        {
            SelectedOTRequest = otRequest
        };

        BindingContext = viewModel;
    }
}