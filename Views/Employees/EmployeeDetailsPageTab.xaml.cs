namespace ESSmPrototype.Views.Employees;

public partial class EmployeeDetailsPageTab : TabbedPage
{
    public EmployeeDetailsPageTab(Employee selectedEmployee)
    {
        InitializeComponent();

        BindingContext = new EmployeeDetailsPageViewModel
        {
            SelectedEmployee = selectedEmployee
        };
    }

    private async void OnBackButtonClicked(object sender, EventArgs e)
    {
        if (Application.Current != null)
        {
            //await Shell.Current.Navigation.PushAsync(new EmployeeDetailsPageTab());
            Application.Current.MainPage = new AppShell();
            await Shell.Current.GoToAsync(nameof(EmployeeDetailsPage));
        }
    }
}
