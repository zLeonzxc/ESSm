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

        if (selectedItem == "Employees")
        {
            ((ListView)sender).SelectedItem = null;
            await Shell.Current.GoToAsync(nameof(EmployeeDetailsPage));
        }
        else if (selectedItem == "Leave")
        {
            ((ListView)sender).SelectedItem = null;
            await Shell.Current.GoToAsync(nameof(EmployeeLeaveList));
        }
    }
}