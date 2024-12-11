namespace ESSmPrototype.ViewModels
{
    public class StartedPageViewModel : INotifyPropertyChanged
    {
        private string _companyCode;

        public string CompanyCode
        {
            get => _companyCode;
            set
            {
                if (_companyCode != value)
                {
                    _companyCode = value;
                    OnPropertyChanged();
                    SaveCompanyCodeToSecureStorage(_companyCode);
                }
            }
        }

        private async void SaveCompanyCodeToSecureStorage(string companyCode)
        {
            try
            {
                await SecureStorage.SetAsync(nameof(CompanyCode), companyCode);
            }
            catch (Exception ex)
            {
                // Handle potential exceptions, such as when the device does not support secure storage
                Console.WriteLine($"Error saving to secure storage: {ex.Message}");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
