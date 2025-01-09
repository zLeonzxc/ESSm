namespace ESSmPrototype
{
    public partial class AppShell : Shell
    {
        public static LoginViewModel? LoginViewModel { get; private set; }
        public AppShell(LoginViewModel loginViewModel)
        {
            InitializeComponent();

            LoginViewModel = loginViewModel;
            BindingContext = LoginViewModel;

            RegisterRoutes();

            if (Application.Current is App app)
            {
                app.StartIdleTimer();
            }

        }

        public void RegisterRoutes()
        {
            // Main pages
            Routing.RegisterRoute(nameof(StartedPage), typeof(StartedPage));
            Routing.RegisterRoute(nameof(LoginPage), typeof(LoginPage));
            Routing.RegisterRoute(nameof(LogoutPage), typeof(LogoutPage));
            Routing.RegisterRoute(nameof(UserSettingsPage), typeof(UserSettingsPage));
            //Routing.RegisterRoute(nameof(MainPage), typeof(MainPage));
            Routing.RegisterRoute("//MainPage", typeof(MainPage)); // global route e.g. //MainPage
            Routing.RegisterRoute("//Employee", typeof(EmployeePage));
            Routing.RegisterRoute("//HRAdmin", typeof(HRAdminPage));
            Routing.RegisterRoute("//Approver", typeof(ApproverPage));

            // Menu pages
            Routing.RegisterRoute(nameof(EmployeeLeaveList), typeof(EmployeeLeaveList));
            Routing.RegisterRoute(nameof(EmployeeLeaveDetailsPageView), typeof(EmployeeLeaveDetailsPageView));


            // Operations pages
            Routing.RegisterRoute(nameof(PendingApprovalPage), typeof(PendingApprovalPage));
            Routing.RegisterRoute(nameof(EmployeeLeaveDetailsPageAdmin), typeof(EmployeeLeaveDetailsPageAdmin));
            Routing.RegisterRoute(nameof(EmployeeOvertimeDetailsPageAdmin), typeof(EmployeeOvertimeDetailsPageAdmin));
            Routing.RegisterRoute(nameof(EmployeeDetailsPage), typeof(EmployeeDetailsPage));
            Routing.RegisterRoute(nameof(EmployeeDetailsPageTab), typeof(EmployeeDetailsPageTab));
        }

    }

    
}
