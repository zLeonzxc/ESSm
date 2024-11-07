

namespace ESSmPrototype.Views.Employee;

public partial class EmployeeLeaveList : ContentPage
{
	public EmployeeLeaveList()
	{
		InitializeComponent();
	}

    private async void OnItemTapped(object sender, ItemTappedEventArgs e)
    {
        if (e.Item is LeaveRequest selectedLeaveRequest)
        {
            await Navigation.PushAsync(new EmployeeLeaveDetailsPage(selectedLeaveRequest));
        }
    }
}