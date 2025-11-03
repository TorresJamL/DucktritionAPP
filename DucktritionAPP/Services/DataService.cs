using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DucktritionAPP.Services
{
    public class DataService
    {
        private readonly Dictionary<string, List<object>> data;
        public DataService()
        {
            data = new Dictionary<string, List<object>>()
            {
                { "Taco Haven", new List<object> { "Authentic Mexican street tacos", 4.6, "placeholderimage.png" } },
                { "Brew & Bean", new List<object> { "Cozy cafe with great espresso", 4.2, "placeholderimage.png" } },
                { "Sushi Zen", new List<object> { "Fresh sushi and sashimi", 4.8, "placeholderimage.png" } },
                { "Burger Point", new List<object> { "Classic American burgers", 4.1, "placeholderimage.png" } },
                { "Foody's!", new List<object> { "We're, still here!", 3.0, "placeholderimage.png" } },
                { "Jamil's Joint", new List<object> { "Edible Food, I swear", -0.5, "placeholderimage.png" } },
                { "Mama Mary's", new List<object> { "Best Pizza Place of The Nation", -0.5, "placeholderimage.png" } },
            };
        }
        public Dictionary<string, List<object>> GetData()
        {
            return data;
        }
        public Dictionary<string, List<object>> Search(string query)
        {
            query = query?.ToLower() ?? "";
            var results = new Dictionary<string, List<object>>();

            foreach (var kvp in data)
            {
                if (kvp.Key.ToLower().Contains(query))
                    results.Add(kvp.Key, kvp.Value);
            }

            return results;
        }
    }
}
