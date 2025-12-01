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
        public class AutocompletePrediction
        {
            public required string place_id { get; set; }
            public required string primary_text { get; set; }
        }

        internal class AutocompleteResponse
        {
            public required List<Prediction> predictions { get; set; }

            internal class Prediction
            {
                public required string place_id { get; set; }
                public required StructuredFormatting structured_formatting { get; set; }

                internal class StructuredFormatting
                {
                    public required string main_text { get; set; }
                }
            }
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
            public EditorialSummary? editorial_summary { get; set; }
            public List<PhotoInfo>? photos { get; set; } = [];
            public List<string>? types { get; set; }
        }

        internal class PhotoInfo
        {
            public string photo_reference { get; set; } = "";
            public int height { get; set; }
            public int width { get; set; }
        }

        internal class EditorialSummary
        {
            public string overview { get; set; } = "";
            public string language { get; set; } = "";
        }

        internal class GoogleReview
        {
            public float rating { get; set; }
            public required string author_name { get; set; }
            public required string text { get; set; }
        }

    }
}
