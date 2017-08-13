using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System.Text;

namespace AntColony
{
    public class Map
    {
        public float[] mapVals;
        public int length = 0;
        public int mapW = 0;
        public int mapH = 0;

        public float MAX_VAL = 100.0f;
        public float EVAPORATION_RATE = 0.999f;

        // A float map
        public Map(int w, int h)
        {
            mapW = w;
            mapH = h;
            length = mapW * mapH;
            mapVals = new float[length];
            for (int i = 0; i < mapVals.Length; i++)
            {
                mapVals[i] = 0.0f;
            }
        }

        // Evaporate
        public void step()
        {
            for (int i = 0; i < mapVals.Length; i++)
            {
                mapVals[i] = mapVals[i] * EVAPORATION_RATE;
            }
        }

        public void setValue(int x, int y, float val)
        {
           
                int index = y * mapW + x;
                if (mapVals.Length - 1 > (y * mapW + x))
                {
                    float oldVal = mapVals[index];
                    //      mapVals[index] = (oldVal + val)/2;
                    if (val > oldVal)
                    {
                        mapVals[index] = val;
                    }
                }
        }

        public float getPercentage(int index)
        {
            return mapVals[index] / MAX_VAL;
        }

        public float getValue(int index)
        {
            return mapVals[index];
        }

        public float getValue(int x, int y)
        {
           // try
           // {
            if (mapVals.Length - 1 < (y * mapW + x))
            {
                return -1;
            }
            else
            {
                return mapVals[y * mapW + x];
            }
            
        }

        /**
         Returns a 2D vector of the strongest direction
         */
        public int[] getStrongest(int x, int y)
        {
            float compare = 0;
            float strongestVal = 0;
            int[] strongest = {0, 0
                              };

            compare = getValue(x - 1, y - 1); // up left
            if (compare > strongestVal)
            {
                strongest[0] = -1;
                strongest[1] = -1;
                strongestVal = compare;
            }
            compare = getValue(x, y - 1); // up
            if (compare > strongestVal)
            {
                strongest[0] = 0;
                strongest[1] = -1;
                strongestVal = compare;
            }
            compare = getValue(x + 1, y - 1); // up right
            if (compare > strongestVal)
            {
                strongest[0] = 1;
                strongest[1] = -1;
                strongestVal = compare;
            }
            compare = getValue(x - 1, y); // left
            if (compare > strongestVal)
            {
                strongest[0] = -1;
                strongest[1] = 0;
                strongestVal = compare;
            }
            compare = getValue(x + 1, y); // right
            if (compare > strongestVal)
            {
                strongest[0] = 1;
                strongest[1] = 0;
                strongestVal = compare;
            }
            compare = getValue(x - 1, y + 1); // down left
            if (compare > strongestVal)
            {
                strongest[0] = -1;
                strongest[1] = 1;
                strongestVal = compare;
            }
            compare = getValue(x, y + 1); // down
            if (compare > strongestVal)
            {
                strongest[0] = 0;
                strongest[1] = 1;
                strongestVal = compare;
            }
            compare = getValue(x + 1, y + 1); // down right
            if (compare > strongestVal)
            {
                strongest[0] = 1;
                strongest[1] = 1;
                strongestVal = compare;
            }

            return strongest;
        }
    }
}
