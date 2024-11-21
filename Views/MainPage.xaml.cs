namespace ESSmPrototype.Views
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private async void OnSearchEmployeeClicked(object sender, EventArgs e)
        {
            try
            {
                await Shell.Current.Navigation.PushAsync(new EmployeeDetailsPage());
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Navigation error: {ex.Message}");
            }
        }

        private async void OnLeaveListClicked(object sender, EventArgs e)
        {
            try
            {
                await Shell.Current.Navigation.PushAsync(new EmployeeLeaveList());
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Navigation error: {ex.Message}");
            }
        }

        private async void OnApprovalClicked(object sender, EventArgs e)
        {
            try
            {
                await Shell.Current.Navigation.PushAsync(new PendingApprovalPage());
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Navigation error: {ex.Message}");
            }
        }

        private async void OnTestClicked(object sender, EventArgs e)
        {
            try
            {
                await Shell.Current.Navigation.PushAsync(new EmployeeDetailsPageTab());
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Navigation error: {ex.Message}");
            }
        }
    }
}
