namespace ESSmPrototype.Views.Employee;

public partial class EmployeeDetailsPage : ContentPage
{
	public EmployeeDetailsPage()
	{
		InitializeComponent();
	}

	private void OnSearchEntryChanged(object sender, TextChangedEventArgs e)
    {
        var vm = BindingContext as EmployeeDetailsPageViewModel;
		vm?.OnSearchTextChanged(sender, e);
    }

    
}