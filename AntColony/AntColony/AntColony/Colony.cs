using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AntColony
{
     public class Colony
    {
        public Ant[] ants;
        public int x;
        public int y;

       public Colony(int _x, int _y, int count, Map _pherHome, Map _pherFood)
        {
            ants = new Ant[count];
            x = _x;
            y = _y;
            for (int i = 0; i < count; i++)
            {
                ants[i] = new Ant(x, y, _pherHome, _pherFood);
            }
        }
    }
}
