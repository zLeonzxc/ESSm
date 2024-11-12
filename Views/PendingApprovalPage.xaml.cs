namespace ESSmPrototype.Views;

public partial class PendingApprovalPage : ContentPage
{
	public PendingApprovalPage()
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