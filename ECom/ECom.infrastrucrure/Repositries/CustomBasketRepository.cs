using ECom.Core.Entites;
using ECom.Core.Interface;
using ECom.infrastrucrure.Data;
using Microsoft.EntityFrameworkCore;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ECom.infrastrucrure.Repositries
{
    public class CustomBasketRepository : IcustomerBasketRepository
    {
        private readonly IDatabase database;
        public CustomBasketRepository(IConnectionMultiplexer connection)
        {
            database = connection.GetDatabase();
        }
        //private readonly AppDbContext _context;

        //public CustomBasketRepository(AppDbContext context)
        //{
        //    _context = context;

        //}
        public Task<bool> deletebasketAsync(string id)
        {
            return database.KeyDeleteAsync(id);
        }

        public async Task<customerbasket> GetBasketAsync(string id)
        {
            var result =await database.StringGetAsync(id);
            if (!string.IsNullOrEmpty(result))
            {
                return JsonSerializer.Deserialize<customerbasket>(result);
            }
            return null;
        }

        public async Task<customerbasket> updatebasketAsync(customerbasket basket)
        {
            var _basket = await database.StringSetAsync(basket.id.ToString(), JsonSerializer.Serialize(basket));
            if (_basket)
            {
                return await GetBasketAsync(basket.id.ToString());
            }
            return null;
        }
    }
}
