namespace ESSmPrototype.Views;

public partial class EmployeePage : ContentPage
{
	public EmployeePage()
	{
		InitializeComponent();

		List<Option> options = new List<Option>()
        {
            new Option { Title = "Menu Item 1" },
            new Option { Title = "Menu Item 2" },
            new Option { Title = "Menu Item 3" }
        };

        listOptions.ItemsSource = options;
	}

    public class Option
    {
        public string? Title { get; set; }
    }

    private async void listOptions_ItemSelected(object sender, SelectedItemChangedEventArgs e)
    {
        if (e.SelectedItem == null)
            return;

        var option = e.SelectedItem as Option;

        switch (option?.Title)
        {
            case "Menu Item 1":
                await Shell.Current.GoToAsync(nameof(EmployeeMenuItem1));
                break;
            case "Menu Item 2":
                await Shell.Current.GoToAsync(nameof(EmployeeMenuItem2));
                break;
            case "Menu Item 3":
                await Shell.Current.GoToAsync(nameof(EmployeeMenuItem3));
                break;
        }

        listOptions.SelectedItem = null;
    }
}