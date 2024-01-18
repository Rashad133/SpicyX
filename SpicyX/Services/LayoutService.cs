using Microsoft.EntityFrameworkCore;
using SpicyX.DAL;

namespace SpicyX.Services
{
    public class LayoutService
    {
        private readonly AppDbContext _db;
        public LayoutService(AppDbContext db)
        {
            _db = db;
        }

        public async Task<Dictionary<string,string>> GetSettingsAsync()
        {
            Dictionary<string,string> settings= await _db.Settings.ToDictionaryAsync(x=>x.Key,x=>x.Value);
            return settings;
        }
    }
}
