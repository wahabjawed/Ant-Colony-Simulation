using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AntColony
{
    public class Food
    {
        public bool [] mapVals;
        public bool [,] dmapVals;
        public int length;
        public int mapW;
        public int mapH;

        // A boolean map
        public Food(int w, int h)
        {
            mapW = 800;
            mapH = 600;
            length = mapW * mapH;
            dmapVals=new bool[800,600];
            mapVals = new bool[length];
          

        }

        public void addFood(int x, int y)
        {
            if (x > 0 && y > 0)
            {

                // 10x10 bit of food
                for (int i = x; i < mapW && i < (x + 10); i++)
                {
                    for (int j = y; j < mapH && j < (y + 10); j++)
                    {
                        //Console.WriteLine("for: " + i + " " + j);

                        dmapVals[i, j] = true;
                        setValue(i, j, true);

                    }
                }

            }
        }

        public void setValue(int x, int y, bool val)
        {
           // if (mapVals.Length - 1 > (y*mapW + x))
           // {
               
                int d = (y * mapW) + x;
                mapVals[d] = val;   
           // Console.WriteLine("we have: " + d);
            //}
        }

        public void setValued(int x, int y, bool val)
        {
            // if (mapVals.Length - 1 > (y*mapW + x))
            // {

            
            dmapVals[x,y] = val;
            // Console.WriteLine("we have: " + d);
            //}
        }
        public void bite(int x, int y)
        {
            setValued(x - 1, y - 1, false);
            setValued(x - 1, y, false);
            setValued(x - 1, y + 1, false);
            setValued(x, y - 1, false);
            setValued(x, y, false);
            setValued(x, y + 1, false);
            setValued(x + 1, y - 1, false);
            setValued(x + 1, y, false);
            setValued(x + 1, y + 1, false);

            setValue(x - 1, y - 1, false);
            setValue(x - 1, y, false);
            setValue(x - 1, y + 1, false);
            setValue(x, y - 1, false);
            setValue(x, y, false);
            setValue(x, y + 1, false);
            setValue(x + 1, y - 1, false);
            setValue(x + 1, y, false);
            setValue(x + 1, y + 1, false);
        }

        public bool getValue(int index)
        {
            return mapVals[index];
        }

        public bool getValue(int x, int y)
        {
            if (mapVals.Length - 1 > (y * mapW + x))
            {
                return mapVals[y * mapW + x];
            }
            else
            {
                // Off the map
                return false;
            }
        }

    }
}
