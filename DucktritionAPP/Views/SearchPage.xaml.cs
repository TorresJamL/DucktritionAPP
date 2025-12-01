using DucktritionAPP.Services;
using DucktritionAPP.Models;
using System.Text;
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
        if (query.Length < 3)
        {
            return;
        }

        var results = await _googleService.SearchPlacesAsync(query);

        LoadResults(results);

    }

    private void LoadResults(List<EstablishmentData> results)
    {
        var items = results.Select(place => new
        {
            place.Name,
            Rating = LoadStars(place.GetOverallRating()),
            place.Location,
            Description = (place.Description != "") ? place.Description : "No Description Provided",
            Image = place.PhotoURL
        });

        ResultsCollectionView.ItemsSource = items;
    }
    private string LoadStars(float rating)
    {
        StringBuilder stars = new StringBuilder("☆☆☆☆☆");
        char fullStar = '★';
        for (int i = 0; i < 5; i++)
        {
            if ((int) rating - i > 0)
            {
                stars[i] = fullStar;
            }
        }
        return stars.ToString();
    }
}