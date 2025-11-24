using static DucktritionAPP.Services.GooglePlacesService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DucktritionAPP.Models
{
    internal class GoogleServicesModel
    {
        internal class AutocompleteResponse
        {
            public List<AutocompleteResult>? predictions { get; set; }
            public string? status { get; set; }
        }

        internal class AutocompleteResult
        {
            public string? description { get; set; }
            public string? place_id { get; set; }
            public StructuredFormatting? structured_formatting { get; set; }
        }

        internal class StructuredFormatting
        {
            public string? main_text { get; set; }
            public string? secondary_text { get; set; }
        }
        internal class PlaceDetailsResponse
        {
            public required PlaceDetails result { get; set; }
            public required string status { get; set; }
        }
        internal class PlaceDetails
        {
            public required string name { get; set; }
            public required string formatted_address { get; set; }
            public float? rating { get; set; }
            public List<GoogleReview>? reviews { get; set; }
            public string? editorial_summary { get; set; }
            public List<string>? types { get; set; }
        }
        internal class GoogleReview
        {
            public float rating { get; set; }
            public required string author_name { get; set; }
            public required string text { get; set; }
        }

    }
}
