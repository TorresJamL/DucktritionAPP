using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DucktritionAPP.Services
{
    public class DataCacheService
    {
        private readonly Dictionary<string, List<object>> data;
        public DataCacheService()
        {
            data = new Dictionary<string, List<object>>() {};
        }
        public Dictionary<string, List<object>> GetData()
        {
            return data;
        }
    }
}
