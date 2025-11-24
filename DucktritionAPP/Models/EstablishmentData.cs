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
        public required string ReviewMSG { get; set; }
    }
    internal class EstablishmentData
    {
        public required string Name { get; set; }
        public string? Description { get; set; }
        public List<Review>? Reviews { get; set; }
        public required string Location { get; set; }
        public List<string>? FilterTags { get; set; }
    }
}
