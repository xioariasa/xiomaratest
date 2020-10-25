using Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services
{
    public interface IRouletteService
    {
        public int Create();
        public bool Open(int id);
        public void Bet(Bet bet);
        public List<Bet> Close(int id);
        public List<Roulette> GetAll();
    }
}
