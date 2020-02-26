﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Project.VisualObjects.Interfaces;
namespace Project.VisualObjects
{
    class Asteroid : ImageObject, ICollision
    {
        public Asteroid(Point Position, Point Direction, int ImageSize)
            : base(Position, Direction, new Size(ImageSize, ImageSize), Properties.Resources.Asteroid)
        {

        }

        public bool CheckCollision(ICollision obj) => Rect.IntersectsWith(obj.Rect);
    }
}
