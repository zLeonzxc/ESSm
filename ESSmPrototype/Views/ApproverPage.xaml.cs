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
            ((ListView)sender).SelectedItem = null;
            await Shell.Current.GoToAsync(nameof(PendingApprovalPage));
        }
    }
}