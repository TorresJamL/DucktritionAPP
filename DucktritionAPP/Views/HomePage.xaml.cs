 using DucktritionAPP.Services;

namespace DucktritionAPP.Views;

public partial class HomePage : ContentPage
{
	readonly List<string> appFilters = [
        "Vegetarian", "Nut Allegy", "Shellfish Allergy", "Lactose Intolerant",
        "Pescetarian"
        ];
    readonly UserFilterService filterService = UserFilterService.Inst;

	public HomePage()
	{
		InitializeComponent();
        BuildAvailableFilters();
	}
    void BuildAvailableFilters()
    {
        AvailableFiltersArea.Children.Clear();

        foreach (var filter in appFilters.Except(filterService.UserFilters))
        {
            var layout = new Grid
            {
                ColumnDefinitions =
                {
                    new ColumnDefinition { Width = GridLength.Star },
                    new ColumnDefinition { Width = GridLength.Auto }
                }, Padding = new Thickness(0, 5)
            };
            var label = new Label
            {
                Text = filter,
                VerticalOptions = LayoutOptions.Center
            };
            var addButton = new Button
            {
                Text = "+",
                HorizontalOptions = LayoutOptions.End,
                WidthRequest = 15,
                HeightRequest = 15,
                CornerRadius = 10,
                BackgroundColor = Colors.Green,
                VerticalOptions = LayoutOptions.Center
            };
            addButton.Clicked += (s, e) =>
            {
                filterService.UserFilters.Add(filter);
                BuildActiveFilters();
                BuildAvailableFilters();
            };
            layout.Add(label, 0, 0);
            layout.Add(addButton, 1, 0);
            AvailableFiltersArea.Children.Add(layout);
        }
    }

    void BuildActiveFilters()
    {
        ActiveFiltersArea.Children.Clear();
        var header = new Label
        {
            Text = "Active Filters",
            FontAttributes = FontAttributes.Bold
        };
        ActiveFiltersArea.Children.Add(header);

        foreach (var filter in filterService.UserFilters)
        {
            var layout = new Grid
            {
                ColumnDefinitions =
                {
                    new ColumnDefinition { Width = GridLength.Star },
                    new ColumnDefinition { Width = GridLength.Auto }
                }, Padding = new Thickness(0, 5)
            };
            var label = new Label
            {
                Text = filter,
                VerticalOptions = LayoutOptions.Center
            };
            var removeButton = new Button
            {
                Text = "X",
                HorizontalOptions = LayoutOptions.End,
                WidthRequest = 15,
                HeightRequest = 15,
                CornerRadius = 10,
                BackgroundColor = Colors.Red
            };
            removeButton.Clicked += (s, e) =>
            {
                filterService.UserFilters.Remove(filter);
                BuildActiveFilters();
                BuildAvailableFilters();
            };
            layout.Add(label, 0, 0);
            layout.Add(removeButton, 1, 0);
            ActiveFiltersArea.Children.Add(layout);
        }
    }
}