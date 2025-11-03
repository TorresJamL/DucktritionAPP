using DucktritionAPP.Services;
namespace DucktritionAPP.Views;

public partial class SearchPage : ContentPage
{
    private readonly DataService _dataService;

    public SearchPage()
    {
        InitializeComponent();
        _dataService = new DataService();
        LoadResults(_dataService.GetData());
    }

    private void OnSearchTextChanged(object sender, TextChangedEventArgs e)
    {
        var query = e.NewTextValue;
        var results = _dataService.Search(query);
        LoadResults(results);
    }

    private void LoadResults(Dictionary<string, List<object>> results)
    {
        var items = results.Select(kvp => new
        {
            Name = kvp.Key,
            Description = kvp.Value[0].ToString(),
            Rating = kvp.Value[1].ToString(),
            Image = kvp.Value[2].ToString()
        });

        ResultsCollectionView.ItemsSource = items;
    }
}