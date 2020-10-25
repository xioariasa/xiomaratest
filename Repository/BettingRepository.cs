using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EasyCaching.Core;
using Models;

namespace Repository
{
    public class BettingRepository : IBettingRepository
    {
        private const string Key = "Roulette";
        private IEasyCachingProviderFactory EasyCachingProviderFactory;

        private IEasyCachingProvider EasyCachingProvider;

        public BettingRepository(IEasyCachingProviderFactory cachingProviderFactory)
        {
            this.EasyCachingProviderFactory = cachingProviderFactory;
            this.EasyCachingProvider = this.EasyCachingProviderFactory.GetCachingProvider("roulette");
        }
        public void Bet(Bet bet)
        {
            var allRoulettes = this.GetAll();
            var openRoulette = allRoulettes.FirstOrDefault(x => x.IsOpen);
            openRoulette.Bets.Add(bet);
            EasyCachingProvider.Set(Key + openRoulette.Id, openRoulette, TimeSpan.FromDays(365));
        }

        public void Close(Roulette roulette)
        {
            roulette.IsOpen = false;
            EasyCachingProvider.Set(Key + roulette.Id, roulette, TimeSpan.FromDays(365));
        }

        public int Create()
        {
            var roulette = new Roulette();
            var id = 0;
            var allRoulettes = this.GetAll();
            id = allRoulettes.Count + 1;
            roulette.Id = id;
            roulette.IsOpen = false;
            EasyCachingProvider.Set(Key + roulette.Id, roulette, TimeSpan.FromDays(365));
            return id;
        }

        public List<Roulette> GetAll()
        {
            var allRoulettes = this.EasyCachingProvider.GetByPrefix<Roulette>(Key);
            return allRoulettes.Select(x => x.Value.Value).ToList();
        }

        public Roulette GetById(int id)
        {
            var allRoulettes = this.GetAll();
            var item = allRoulettes.FirstOrDefault(x => x.Id == id);
            return item;
        }

        public bool Open(int id)
        {
            var item = this.EasyCachingProvider.Get<Roulette>(Key + id);
            if (!item.HasValue)
            {
                return false;
            }
            item.Value.IsOpen = true;
            EasyCachingProvider.Set(Key + item.Value.Id, item.Value, TimeSpan.FromDays(365));
            return true;
        }
    }
}
