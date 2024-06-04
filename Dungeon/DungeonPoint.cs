using System;
using System.Drawing;

namespace Dungeon
{
      
    [Serializable]
    public class DungeonPoint
    {
          
        public double X;

          
        public double Y;

          
        public Point Point
        {
            get
            {
                return new Point((int)X, (int)Y);
            }
        }

          
          
          
        public DungeonPoint(double _X = 0, double _Y = 0)
        {
            X = _X;
            Y = _Y;
        }

                 
          
        public double GetLengthTo(DungeonPoint point)
        {
            return Math.Sqrt(Math.Pow(X - point.X, 2) + Math.Pow(Y - point.Y, 2));
        }
    }
}
