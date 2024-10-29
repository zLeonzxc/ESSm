namespace ESSmPrototype.Views;

public partial class ApproverPage : ContentPage
{
	public ApproverPage()
	{
		InitializeComponent();
	}

    private async void OnItemTapped(object sender, ItemTappedEventArgs e)
    {
        if (e.Item == null)
            return;

        string? selectedItem = e.Item.ToString();

        if (selectedItem == "Pending Approval")
        {
            // Navigate to EmployeeMenuItem1
            await Shell.Current.GoToAsync(nameof(PendingApprovalPage));
        }
        //else if (selectedItem == "Leave")
        //{
        //    await Shell.Current.GoToAsync(nameof(EmployeeLeaveList));
        //}
        //else if (selectedItem == "Pending Approval")
        //{
        //    await Shell.Current.GoToAsync(nameof(PendingApprovalPage));
        //}
          // Deselect the item
          ((ListView)sender).SelectedItem = null;

    }
}