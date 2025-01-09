namespace ESSmPrototype.Views.Employees;

public partial class EmployeeDetailsPageTab : TabbedPage
{
    private readonly LoginViewModel _loginViewModel;
    public EmployeeDetailsPageTab(Employee selectedEmployee, LoginViewModel loginViewModel)
    {
        InitializeComponent();

        _loginViewModel = loginViewModel;

        BindingContext = new EmployeeDetailsPageViewModel
        {
            SelectedEmployee = selectedEmployee
        };
    }

    private async void OnBackButtonClicked(object sender, EventArgs e)
    {
        if (Application.Current != null)
        {
            
            Application.Current.MainPage = new AppShell(_loginViewModel);
            await Shell.Current.GoToAsync(nameof(EmployeeDetailsPage));
        }
    }
}
