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

        if (selectedItem == "Pending Leave")
        {
            // Navigate to EmployeeMenuItem1
            await Shell.Current.GoToAsync(nameof(PendingLeavePage));
        }
        else if (selectedItem == "Leave List")
        {
            await Shell.Current.GoToAsync(nameof(EmployeeLeaveList));
        }
        else if (selectedItem == "Employee Page 3")
        {
            await Shell.Current.GoToAsync(nameof(EmployeeMenuItem3));
        }
            // Deselect the item
            ((ListView)sender).SelectedItem = null;
    }
}