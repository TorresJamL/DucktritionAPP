using DucktritionAPP.Models;
using System.Net.Http;
using System.Text.Json;
using static DucktritionAPP.Models.GoogleServicesModel;

namespace DucktritionAPP.Services
{
    internal class GooglePlacesService
    {
        private string? _apiKey;
        private readonly HttpClient _httpClient;

        public GooglePlacesService()
        {
            _httpClient = new HttpClient();
        }

        public async Task InitializeAsync()
        {
            using var stream = await FileSystem.OpenAppPackageFileAsync("_t_.txt");
            using var reader = new StreamReader(stream);
            _apiKey = (await reader.ReadToEndAsync()).Trim();
        }

        public async Task<List<AutocompletePrediction>> GetAutocompleteAsync(string query)
        {
            string url =
                $"https://maps.googleapis.com/maps/api/place/autocomplete/json?input={Uri.EscapeDataString(query)}&key={_apiKey}&types=establishment";

            var response = await _httpClient.GetAsync(url);

            if (!response.IsSuccessStatusCode)
                return new List<AutocompletePrediction>();

            var json = await response.Content.ReadAsStringAsync();

            var data = JsonSerializer.Deserialize<AutocompleteResponse>(json);

            if (data?.predictions == null)
                return new List<AutocompletePrediction>();

            return data.predictions.Select(p => new AutocompletePrediction
            {
                place_id = p.place_id,
                primary_text = p.structured_formatting.main_text
            }).ToList();
        }

        public async Task<List<EstablishmentData>> SearchPlacesAsync(string query)
        {
            var predictions = await GetAutocompleteAsync(query);
            var results = new List<EstablishmentData>();

            foreach (var p in predictions)
            {
                var details = await GetPlaceDetailsAsync(p.place_id);
                results.Add(details);
            }

            return results;
        }


        public async Task<EstablishmentData?> GetPlaceDetailsAsync(string placeId)
        {
            var url = $"https://maps.googleapis.com/maps/api/place/details/json?place_id={placeId}&fields=name,formatted_address,rating,reviews,editorial_summary,types,photos&key={_apiKey}";
            var json = await _httpClient.GetStringAsync(url);
            var response = JsonSerializer.Deserialize<PlaceDetailsResponse>(json);

            var details = response?.result;
            if (details == null) return null;

            string? photoUrl = null;

            if (details.photos != null && details.photos.Count > 0)
            {
                var photoRef = details.photos[0].photo_reference;

                photoUrl = $"https://maps.googleapis.com/maps/api/place/photo" +
                           $"?maxwidth=400&photo_reference={photoRef}&key={_apiKey}";
            }

            return new EstablishmentData
            {
                Name = details.name,
                Description = details.editorial_summary?.overview ?? "",
                Location = details.formatted_address,
                Reviews = details.reviews?.Select(r => new Review
                {
                    StarRating = r.rating,
                    Reviewer = r.author_name,
                    ReviewMSG = r.text
                }).ToList() ?? new List<Review>(),
                PhotoURL = photoUrl,
                FilterTags = ["None"] 
            };
        }
    }
}
