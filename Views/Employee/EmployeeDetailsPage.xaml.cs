namespace ESSmPrototype.Views.Employee;

public partial class EmployeeDetailsPage : ContentPage
{
	public EmployeeDetailsPage()
	{
		InitializeComponent();
	}

    public void OnSearchButtonClicked(object sender, EventArgs e)
    {
        var name = NameSearchBar.Text;
        if (name == "") {
            name = "";
        }
        var vm = BindingContext as EmployeeDetailsPageViewModel;
        vm?.SearchEmployee(name);
    }
    //protected void OnResetButtonClicked(object sender, EventArgs e)
    //{
    //    var vm = BindingContext as EmployeeDetailsPageViewModel;
    //    vm?.OnResetClicked(sender, e);
    //}
}