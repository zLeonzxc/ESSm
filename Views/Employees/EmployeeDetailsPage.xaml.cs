namespace ESSmPrototype.Views.Employees;

public partial class EmployeeDetailsPage : ContentPage
{
    public EmployeeDetailsPage()
    {
        InitializeComponent();
    }

    public void OnSearchButtonClicked(object sender, EventArgs e)
    {
        var name = NameSearchBar.Text;
        if (string.IsNullOrEmpty(name))
        {
            name = "";
        }
        var vm = BindingContext as EmployeeDetailsPageViewModel;
        vm?.SearchEmployee(name);
    }

    //public async void OnEntryButtonClicked(object sender, EventArgs e)
    //{
    //    if (Application.Current != null)
    //    {
    //        Application.Current.MainPage = new EmployeeDetailsPageTab(employee);
    //        await Task.CompletedTask;
    //    }
    //}
}
