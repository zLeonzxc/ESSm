namespace ESSmPrototype.ViewModels
{
    public class StartedPageViewModel : INotifyPropertyChanged
    {
        private string? _companyCode;
        private readonly LoginViewModel _loginViewModel;

        public string? CompanyCode
        {
            get => _companyCode?.ToUpper();
            set
            {
                if (_companyCode != value)
                {
                    _companyCode = value?.ToUpper();
                    OnPropertyChanged();
                }
            }
        }
        public StartedPageViewModel(LoginViewModel loginViewModel)
        {
            _loginViewModel = loginViewModel;
        }

        public bool IsOverlayVisible => _loginViewModel.IsOverlayVisible;

        public async void VerifyCompanyCode(string companyCode)
        {
            try
            {
                var handler = new HttpClientHandler
                {
                    ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true
                };

                using var httpClient = new HttpClient(handler);

                var response = await httpClient.GetAsync($"https://10.0.2.2:7087/api/Companies/{companyCode}");

                if (response.IsSuccessStatusCode)
                {
                    if (App.Current?.MainPage != null)
                    {
                        App.Current.MainPage = new LoginPage();
                    }
                }
                else
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    Debug.WriteLine("Error Code: {0}, Response Status: {1}, Message: {2}", companyCode, response, responseContent);
                    Console.WriteLine("An error occurred while registering company code [{0}]", companyCode);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Unregistered company code: {0}", ex.Message);
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
