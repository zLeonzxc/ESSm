namespace ESSmPrototype.Views;

public partial class HRAdminPage : ContentPage
{
	public HRAdminPage()
	{
		InitializeComponent();
	}

    private async void OnItemTapped(object sender, ItemTappedEventArgs e)
    {
        if (e.Item == null)
            return;

        string? selectedItem = e.Item.ToString();

        if (selectedItem == "Employees")
        {
            await Shell.Current.GoToAsync(nameof(EmployeeDetailsPage));
        }
        else if (selectedItem == "Leave")
        {
            await Shell.Current.GoToAsync(nameof(EmployeeLeaveList));
        }
        else if (selectedItem == "Pending Approval")
        {
            await Shell.Current.GoToAsync(nameof(PendingApprovalPage));
        }
           // Deselect the item
           ((ListView)sender).SelectedItem = null;
        
    }
}