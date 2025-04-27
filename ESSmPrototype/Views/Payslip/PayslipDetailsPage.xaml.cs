namespace ESSmPrototype.Views.Payslip;

public partial class PayslipDetailsPage : ContentPage
{
    public PayslipDetailsPage(ESSmPrototype.Models.Payslip selectedPayslip)
    {
        InitializeComponent();

        var viewModel = new PayslipViewModel
        {
            SelectedPayslip = selectedPayslip
        };

        BindingContext = viewModel;
    }
}