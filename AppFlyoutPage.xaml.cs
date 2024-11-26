namespace ESSmPrototype
{
    public partial class AppFlyoutPage : FlyoutPage
    {
        public AppFlyoutPage()
        {
            InitializeComponent();
            BindingContext = new AppFlyoutPageViewModel();
        }
    }
}