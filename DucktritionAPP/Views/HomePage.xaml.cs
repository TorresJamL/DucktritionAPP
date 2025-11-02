using DucktritionAPP.Services;

namespace DucktritionAPP.Views;

public partial class HomePage : ContentPage
{
	List<string> appFilters = new() { "Vegetarian", "Almond Allegy", "Shellfish Allergy", "Lactose Intolerant" };
    UserFilterService filterService = UserFilterService.Inst;

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
            var layout = new HorizontalStackLayout
            {
                HorizontalOptions = LayoutOptions.Fill,
                Spacing = 10
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
                WidthRequest = 30,
                HeightRequest = 30,
                CornerRadius = 15,
                BackgroundColor = Colors.LightGray
            };

            addButton.Clicked += (s, e) =>
            {
                filterService.UserFilters.Add(filter);
                BuildActiveFilters();
                BuildAvailableFilters();
            };

            layout.Children.Add(label);
            layout.Children.Add(addButton);
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
            var layout = new HorizontalStackLayout
            {
                HorizontalOptions = LayoutOptions.Fill,
                Spacing = 10
            };

            var label = new Label
            {
                Text = filter,
                VerticalOptions = LayoutOptions.Center
            };

            var removeButton = new Button
            {
                Text = "X",
                WidthRequest = 30,
                HeightRequest = 30,
                CornerRadius = 15,
                BackgroundColor = Colors.LightGray
            };

            removeButton.Clicked += (s, e) =>
            {
                filterService.UserFilters.Remove(filter);
                BuildActiveFilters();
                BuildAvailableFilters();
            };

            layout.Children.Add(label);
            layout.Children.Add(removeButton);
            ActiveFiltersArea.Children.Add(layout);
        }
    }
}