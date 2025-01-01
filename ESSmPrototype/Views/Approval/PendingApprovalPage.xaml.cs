namespace ESSmPrototype.Views.Approval;

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
            ((ListView)sender).SelectedItem = null;
            await Navigation.PushAsync(new EmployeeLeaveDetailsPageAdmin(selectedLeaveRequest));
        }
    }
}