namespace ESSmPrototype.Views;

public partial class EmployeePage : ContentPage
{
	public EmployeePage()
	{
		InitializeComponent();
	}

    private async void OnItemTapped(object sender, ItemTappedEventArgs e)
    {
        if (e.Item == null)
            return;

        string? selectedItem = e.Item.ToString();

        if (selectedItem == "Employee Info")
        {
            await Shell.Current.GoToAsync(nameof(EmployeeDetailsPage));
        }
        else if (selectedItem == "Leave List")
        {
            await Shell.Current.GoToAsync(nameof(EmployeeLeaveList));
        }
            // Deselect the item
            ((ListView)sender).SelectedItem = null;
    }
}