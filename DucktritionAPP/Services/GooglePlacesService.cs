using DucktritionAPP.Models;
using System.Net.Http;
using System.Text.Json;
using static DucktritionAPP.Models.GoogleServicesModel;

namespace DucktritionAPP.Services
{
    internal class GooglePlacesService
    {
        private readonly string _apiKey;
        private readonly HttpClient _httpClient;

        public GooglePlacesService(string apiKey)
        {
            _apiKey = apiKey;
            _httpClient = new HttpClient();
        }

        public async Task<List<AutocompleteResult>> SearchPlacesAsync(string query)
        {
            var url = $"https://maps.googleapis.com/maps/api/place/autocomplete/json?input={query}&key={_apiKey}";
            var json = await _httpClient.GetStringAsync(url);
            var response = JsonSerializer.Deserialize<AutocompleteResponse>(json);
            return response?.predictions ?? new List<AutocompleteResult>();
        }

        public async Task<EstablishmentData?> GetPlaceDetailsAsync(string placeId)
        {
            var url = $"https://maps.googleapis.com/maps/api/place/details/json?place_id={placeId}&fields=name,formatted_address,rating,reviews,editorial_summary,types&key={_apiKey}";
            var json = await _httpClient.GetStringAsync(url);
            var response = JsonSerializer.Deserialize<PlaceDetailsResponse>(json);

            var details = response?.result;
            if (details == null) return null;

            return new EstablishmentData
            {
                Name = details.name,
                Description = details.editorial_summary ?? "",
                Location = details.formatted_address,
                Reviews = details.reviews?.Select(r => new Review
                {
                    StarRating = r.rating,
                    Reviewer = r.author_name,
                    ReviewMSG = r.text
                }).ToList() ?? new List<Review>(),
                FilterTags = ["None"] // you can modify later
            };
        }
    }
}
