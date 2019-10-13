using System;
using System.Collections.Generic;
using System.Linq;
using MonkeyCache;

namespace NBAStats.Core.Services.Cache.Base
{
    public class CacheBaseService
    {
        public TimeSpan ExpireIn => TimeSpan.FromDays(10);

        public void AddToCache<T>(IBarrel barrel, string key, IEnumerable<T> data)
        {
            if (data.Any())
            {
                barrel.Add(key: key, data: data, expireIn: ExpireIn);
            }
        }
    }
}
