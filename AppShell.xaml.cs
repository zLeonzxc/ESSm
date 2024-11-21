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
            Routing.RegisterRoute(nameof(EmployeeLeaveDetailsPageView), typeof(EmployeeLeaveDetailsPageView));


            // Operations pages
            Routing.RegisterRoute(nameof(PendingApprovalPage), typeof(PendingApprovalPage));
            Routing.RegisterRoute(nameof(EmployeeLeaveDetailsPageAdmin), typeof(EmployeeLeaveDetailsPageAdmin));
            Routing.RegisterRoute(nameof(EmployeeDetailsPage), typeof(EmployeeDetailsPage));
            Routing.RegisterRoute(nameof(EmployeeDetailsPageTab), typeof(EmployeeDetailsPageTab));

            // Employee Details page
            Routing.RegisterRoute(nameof(Personal), typeof(Personal));
            Routing.RegisterRoute(nameof(Family), typeof(Family));

        }
    }
}
