namespace ESSmPrototype.Views.Employees;

public partial class EmployeeLeaveDetailsPageView : ContentPage
{
    public EmployeeLeaveDetailsPageView(LeaveRequest leaveRequest)
    {
        InitializeComponent();

        var viewModel = new EmployeeLeaveDetailsViewModel
        {
            SelectedLeaveRequest = leaveRequest
        };

        BindingContext = viewModel;
    }
}
//    [QueryProperty(nameof(SelectedLeaveRequest), "SelectedLeaveRequest")]
//public partial class EmployeeLeaveDetailsPage : ContentPage
//{
//    public EmployeeLeaveDetailsPage(LeaveRequest leaveRequest)
//    {
//        InitializeComponent();
//    }

//    private LeaveRequest? _selectedLeaveRequest;
//    public LeaveRequest? SelectedLeaveRequest
//    {
//        get => _selectedLeaveRequest;
//        set
//        {
//            _selectedLeaveRequest = value;
//            OnPropertyChanged();
//            BindingContext = _selectedLeaveRequest;
//        }
//    }
//}
