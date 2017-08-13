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
    public class trackpoint
    {
        Vector2 point;
        Color color;
        public float alpha=1f;

        public trackpoint(Vector2 _point, Color _color) {

            point = _point;
            color = _color;
        }

        public Color getColor() {
            return color;
        }

        public Vector2 getPoint() {
            return point;
        }

    }
}
