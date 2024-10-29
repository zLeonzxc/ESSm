namespace ESSmPrototype.Views;

public partial class PendingLeavePage : ContentPage
{
	public PendingLeavePage()
	{
		InitializeComponent();
	}

    private async void OnViewCellTapped(object sender, EventArgs e)
    {
        var viewCell = sender as TapGestureRecognizer;
        if (viewCell == null)
            return;

        var commandParameter = viewCell.CommandParameter as string;
        if (commandParameter == null)
            return;

        switch (commandParameter)
        {
            case "1":
                await Shell.Current.GoToAsync(nameof(EmployeeLeaveDetailsPage));
                break;
                // Add more cases for other pages
        }
    }
}