using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DucktritionAPP.Models
{
    internal class Review
    {
        public float StarRating { get; set; }
        public required string Reviewer { get; set; }
        public required string ReviewMSG { get; set; } = "";
    }
    internal class EstablishmentData
    {
        public required string Name { get; set; }
        public required string Description { get; set; } = "No Description Provided";
        public required List<Review> Reviews { get; set; } = [];
        public required string Location { get; set; }
        public string? PhotoURL { get; set; }
        public List<string>? FilterTags { get; set; }

        public float GetOverallRating()
        {
            float rating = 0.0f;
            for (int i = 0; i < Reviews.Count; i++)
            {
                rating += Reviews[i].StarRating;
            }
            rating /= Reviews.Count;

            return rating;
        }
    }
}
