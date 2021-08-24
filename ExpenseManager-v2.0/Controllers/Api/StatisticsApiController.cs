namespace ExpenseManager_v2._0.Controllers.Api
{
    using ExpenseManager_v2._0.Services.Statistics;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Caching.Memory;
    using System;
    using System.Collections.Generic;

    [ApiController]
    [Route("api/statistics")]
    public class StatisticsApiController : ControllerBase
    {
        private readonly IStatisticsService statistics;
        private readonly IMemoryCache cache;

        public StatisticsApiController(IStatisticsService statistics, IMemoryCache cache)
        { 
            this.statistics = statistics;
            this.cache = cache;
        }

        [HttpGet]
        public StatisticsServiceModel GetStatistics()
        {
            const string latestStatisticsCacheKey = "LatestStatisticsCacheKey";

            var latestStatistics = this.cache.Get<StatisticsServiceModel>(latestStatisticsCacheKey);

            if (latestStatistics == null)
            {
                latestStatistics = this.statistics.Total();

                var cacheOptions = new MemoryCacheEntryOptions()
                    .SetAbsoluteExpiration(TimeSpan.FromMinutes(15));

                this.cache.Set(latestStatisticsCacheKey, latestStatistics, cacheOptions);
            }

            return latestStatistics;
        }
        
    }
}
