using DucktritionAPP.Services;
namespace DucktritionAPP.Views;

public partial class SearchPage : ContentPage
{
    private readonly GooglePlacesService _googleService;
    private bool _initialized = false;

    public SearchPage()
    {
        InitializeComponent();
        _googleService = new GooglePlacesService();
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await _googleService.InitializeAsync();
        _initialized = true;
    }

    private async void OnSearchTextChanged(object sender, TextChangedEventArgs e)
    {
        if (!_initialized) return;
        var query = e.NewTextValue;

        if (string.IsNullOrWhiteSpace(query))
        {
            ResultsCollectionView.ItemsSource = null;
            return;
        }

        var results = await _googleService.SearchPlacesAsync(query);

        LoadResults(results);

    }

    private void LoadResults(List<EstablishmentData> results)
    {
        var items = results.Select(place => new
        {
            Name = place.Name,
            Description = place.Description ?? "No description",
            Rating = place.Reviews?.FirstOrDefault()?.StarRating.ToString() ?? "—",
            Image = "placeholderimage.png"
        });

        ResultsCollectionView.ItemsSource = items;
    }
}