namespace ESSmPrototype.ViewModels
{
    public partial class PendingApprovalViewModel : EmployeeLeaveDetailsViewModel
    {
        public ObservableCollection<OTRequest> OTRequests { get; set; }
        private OTRequest? _selectedOTRequest;
        public DateTime OTMonth { get; set; }
        public double TotalHours { get; set; }

        //public OTRequest? SelectedOTRequest
        //{
        //    get => _selectedOTRequest;
        //    set
        //    {
        //        if (_selectedOTRequest != value)
        //        {
        //            _selectedOTRequest = value;
        //            OnPropertyChanged(nameof(SelectedOTRequest));
        //            OnPropertyChanged(nameof(EmployeeID));
        //            OnPropertyChanged(nameof(EmployeeName));
        //            OnPropertyChanged(nameof(EmployeeDep));
        //            OnPropertyChanged(nameof(OTMonth));
        //            OnPropertyChanged(nameof(TotalHours));
        //            OnPropertyChanged(nameof(AppliedDate));
        //            OnPropertyChanged(nameof(LeaveStartDate));
        //            OnPropertyChanged(nameof(LeaveEndDate));
        //        }
        //    }
        //}



        public PendingApprovalViewModel()
        {
            OTRequests = new ObservableCollection<OTRequest>();
        }
    }
}
