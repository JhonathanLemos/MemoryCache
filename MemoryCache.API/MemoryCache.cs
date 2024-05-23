using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace MemoryCache.API
{
    public class MemoryCache
    {
        private readonly IMemoryCache _cache;
        private const string MEMORY_CACHE_KEY = "memory";
        public MemoryCache(IMemoryCache cache)
        {
            _cache = cache;
        }

        public async Task<IActionResult> SetInMemoryCache()
        {
            List<NewItem>? newItems = [];
            if (_cache.TryGetValue(MEMORY_CACHE_KEY, out newItems))
            {


                newItems = await _context.NewItem.ToListAsync();

                var policy = new MemoryCacheEntryOptions
                {

                    AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(3600),
                    SlidingExpiration = TimeSpan.FromSeconds(1200)
                };

                _cache.Set(MEMORY_CACHE_KEY, newItems);

            }
            return Ok(newItems);

        }

    }

}