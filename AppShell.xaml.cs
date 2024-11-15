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
            //Routing.RegisterRoute(nameof(MainPage), typeof(MainPage));
            Routing.RegisterRoute("//MainPage", typeof(MainPage)); // global route e.g. //MainPage
            Routing.RegisterRoute("//Employee", typeof(EmployeePage));
            Routing.RegisterRoute("//HRAdmin", typeof(HRAdminPage));
            Routing.RegisterRoute("//Approver", typeof(ApproverPage));

            // Menu pages
            Routing.RegisterRoute(nameof(EmployeeLeaveList), typeof(EmployeeLeaveList));
            Routing.RegisterRoute(nameof(EmployeeLeaveDetailsPageAdmin), typeof(EmployeeLeaveDetailsPageAdmin));
            Routing.RegisterRoute(nameof(EmployeeLeaveDetailsPageView), typeof(EmployeeLeaveDetailsPageView));
            Routing.RegisterRoute(nameof(EmployeeDetailsPage), typeof(EmployeeDetailsPage));

            // Operations pages
            Routing.RegisterRoute(nameof(PendingApprovalPage), typeof(PendingApprovalPage));
        }

        protected override async void OnNavigating(ShellNavigatingEventArgs args)
        {
            // Check if navigating to the Employee section
            if (args.Target.Location.OriginalString.Equals("Employee"))
                await Shell.Current.GoToAsync("//Employee");
            else if (args.Target.Location.OriginalString.Equals("Approver"))
                await Shell.Current.GoToAsync("//Approver");
            else if (args.Target.Location.OriginalString.Equals("HRAdmin"))
                await Shell.Current.GoToAsync("//HRAdmin");
            else
                base.OnNavigating(args);
        }
    }
}
