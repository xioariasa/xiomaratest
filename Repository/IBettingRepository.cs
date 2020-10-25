using Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repository
{
    public interface IBettingRepository
    {
        public int Create();
        public bool Open(int id);
        public void Bet(Bet bet);
        public void Close(Roulette roulette);
        public List<Roulette> GetAll();
        public Roulette GetById(int id);
    }
}
