using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DucktritionAPP.Services
{
    public class UserFilterService
    {
        private static UserFilterService _inst;
        public static UserFilterService Inst => _inst ??= (_inst = new UserFilterService());

        public List<string> UserFilters { get; private set; } = new();
        public event Action UserFilterChanged;

        public void AddFilter(string filter)
        {
            if (!UserFilters.Contains(filter))
            {
                UserFilters.Add(filter);
                UserFilterChanged?.Invoke();
            }
        }
        public void RemoveFilter(string filter)
        {
            if (!UserFilters.Contains(filter))
            {
                UserFilters.Remove(filter);
                UserFilterChanged?.Invoke();
            }
        }
    }
}
