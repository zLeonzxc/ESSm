

namespace ESSmPrototype.Views.Employee;

public partial class EmployeeLeaveList : ContentPage
{
	public EmployeeLeaveList()
	{
		InitializeComponent();
	}

    private async void OnItemTapped(object sender, ItemTappedEventArgs e)
    {
        if (e.Item == null)
            return;

        string? selectedItem = e.Item.ToString();

        if (selectedItem != null)
        {
            await Shell.Current.GoToAsync(nameof(EmployeeLeaveDetailsPage));
        }
            // Deselect the item
            ((ListView)sender).SelectedItem = null;
    }
}