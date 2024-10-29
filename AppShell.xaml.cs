using ESSmPrototype.ViewModels;
using System.Windows.Input;
using ESSmPrototype.Views.Employee;
using Microsoft.Maui.Controls;
using ESSmPrototype.Views;


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
            Routing.RegisterRoute(nameof(EmployeePage), typeof(EmployeePage));
            Routing.RegisterRoute(nameof(ProfilePage), typeof(ProfilePage));
            Routing.RegisterRoute(nameof(ReqLeavePage), typeof(ReqLeavePage));
            Routing.RegisterRoute(nameof(PendingLeavePage), typeof(PendingLeavePage));
            Routing.RegisterRoute(nameof(MainPage), typeof(MainPage));
            
            // Employee menu items
            Routing.RegisterRoute(nameof(EmployeeMenuItem1), typeof(EmployeeMenuItem1));
            Routing.RegisterRoute(nameof(EmployeeMenuItem2), typeof(EmployeeMenuItem2));
            Routing.RegisterRoute(nameof(EmployeeMenuItem3), typeof(EmployeeMenuItem3));
        }
    }
}
