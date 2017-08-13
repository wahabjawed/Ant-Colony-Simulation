using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace AntColony
{
    public class Ant
    {
        Random rand = new Random();

        public double dx;
        public double dy;
        public double x;
        public double y;
        public int intX;
        public int intY;
        public int lastX;
        public int lastY;
        public int homeX;
        public int homeY;

        public bool hasFood = false;

        public float homePher = 100f;
        public float foodPher = 100f;
        public float USE_RATE = 0.995f;
        public double WANDER_CHANCE = 0.92f;

        public int bored = 0;

        public Map homeMap;
        public Map foodMap;

        public Ant(int _x, int _y, Map _homePher, Map _foodMap)
        {
            intX = homeX = _x;
            intY = homeY = _y;
            x = _x;
            y = _y;
            homeMap = _homePher;
            foodMap = _foodMap;
            dx = (Game1.rand.Next(-100, 100))/(double)(100);
            dy = (Game1.rand.Next(-100, 100)) / (double)(100);
            //Console.WriteLine(dx);
        
        }

        public void step()
        {
            
            // Wander chance .1 
            if (Game1.rand.NextDouble() > WANDER_CHANCE)
            {
                dx += (Game1.rand.Next(-100, 100) / (double)(100));
            }
            if (Game1.rand.NextDouble() > WANDER_CHANCE)
            {
                dy += (Game1.rand.Next(-100, 100) / (double)(100));
            }
            if (Game1.rand.NextDouble() > .99f)
            {
                bored += 13;
            }
            if (bored > 0)
            {
                // Ignore pheromones
                bored--;
            }
            else
            {
                // Sniff trails
                if (hasFood)
                {
                    // Look for home 
                    int[] direction = homeMap.getStrongest(intX, intY);
                    dx += direction[0] * (Game1.rand.NextDouble() * (double)1.5);
                    dy += direction[1] * (Game1.rand.NextDouble() * (double)1.5);
                }
                else
                {
                    // Look for food 
                    int[] direction = foodMap.getStrongest(intX, intY);
                    dx += direction[0] * (Game1.rand.NextDouble() * (double)1.5);
                    dy += direction[1] * (Game1.rand.NextDouble() * (double)1.5);
                }
            }
            // Bounding limits, bounce off of edge
            if (x < 2) dx = 1;
            if (x > Game1.width - 2) dx = -1;
            if (y < 2) dy = 1;
            if (y > Game1.width - 2) dy = -1;
            // Speed limit
            dx = Math.Min(dx, 1);
            dx = Math.Max(dx, -1);
            dy = Math.Min(dy, 1);
            dy = Math.Max(dy, -1);
            // Move
            x += dx;
            y += dy;
            intX = (int)Math.Floor(x);
            intY = (int)Math.Floor(y);

            // Only if ant has moved enough (to another pixel)
            if (lastX != intX || lastY != intY)
            {
                // Leave trails
                if (hasFood)
                {
                    // Leave food pheromone trail
                    foodPher = foodPher * USE_RATE;
                    foodMap.setValue(intX, intY, foodPher);
                }
                else
                {
                    // Leave home pheromone trail
                    homePher = homePher * USE_RATE;
                    homeMap.setValue(intX, intY, homePher);
                }
            }

            lastX = intX;
            lastY = intY;
        }
    }

}
