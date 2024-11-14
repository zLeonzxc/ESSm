namespace ESSmPrototype
{
    public partial class AppShell : Shell
    {
        public static LoginViewModel? LoginViewModel { get; private set; }
        public AppShell()
        {
            InitializeComponent();

            RegisterRoutes();
        }

        

        private string _loggedInUserName = string.Empty;
        public void SetUserName(string userName)
        {
            _loggedInUserName = userName;
        }

        public void RegisterRoutes()
        {
            // Main pages
            Routing.RegisterRoute(nameof(LoginPage), typeof(LoginPage));
            Routing.RegisterRoute(nameof(MainPage), typeof(MainPage));
            Routing.RegisterRoute(nameof(EmployeePage), typeof(EmployeePage));
            Routing.RegisterRoute(nameof(HRAdminPage), typeof(HRAdminPage));
            Routing.RegisterRoute(nameof(ApproverPage), typeof(ApproverPage));

            // Menu pages
            Routing.RegisterRoute(nameof(EmployeeLeaveList), typeof(EmployeeLeaveList));
            Routing.RegisterRoute(nameof(EmployeeLeaveDetailsPageAdmin), typeof(EmployeeLeaveDetailsPageAdmin));
            Routing.RegisterRoute(nameof(EmployeeLeaveDetailsPageView), typeof(EmployeeLeaveDetailsPageView));  
            Routing.RegisterRoute(nameof(EmployeeDetailsPage), typeof(EmployeeDetailsPage));

            // Operations pages
            Routing.RegisterRoute(nameof(PendingApprovalPage), typeof(PendingApprovalPage));
        }
    }
}
