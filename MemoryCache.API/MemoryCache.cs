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



            var products = await _cache.GetOrCreateAsync(MEMORY_CACHE_KEY, 
            async e =>{
                e.AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(3600);
                e.SlidingExpiration = TimeSpan.FromSeconds(1200);
                return await _context.NewItem.ToListAsync();
            })

            return Ok(newItems);


        }

    }

}