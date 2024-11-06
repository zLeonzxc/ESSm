using System.Collections.ObjectModel;

namespace ESSmPrototype.ViewModels;

public class EmployeeLeaveDetailsViewModel : INotifyPropertyChanged
{
    public ObservableCollection<LeaveRequest> LeaveRequests { get; set; }

    public event PropertyChangedEventHandler? PropertyChanged;
	public EmployeeLeaveDetailsViewModel()
	{
        LeaveRequests = new ObservableCollection<LeaveRequest>
        {
            new LeaveRequest ( "MY001", "John", "Software Engineer", "Pending", "Medical Leave" ),
            new LeaveRequest ( "MY002", "Jane", "Software Engineer", "Pending", "Annual Leave" ),
            new LeaveRequest ( "MY003", "Leon", "Software Engineer", "Pending", "Maternity Leave" ),
            new LeaveRequest ( "MY004", "Daus", "Software Engineer", "Pending", "Paternity Leave" ),
            new LeaveRequest ( "MY005", "Amin", "Software Engineer", "Pending", "Sick Leave" ),
        };
        AcceptCommand = new Command(OnAcceptCommand);
        RejectCommand = new Command(OnRejectCommand);
    }

    //public EmployeeLeaveDetailsViewModel() : this("", "", "", "", "") { }

    public void OnPropertyChanged(string propertyName)
	{
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    public ICommand AcceptCommand { get; set; }
    public ICommand RejectCommand { get; set; }

    protected virtual void OnAcceptCommand() { }
    protected virtual void OnRejectCommand() { }
}