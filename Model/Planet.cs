using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Shapes;

namespace WASolarSystem.Model
{
    internal class Planet
    {
        private Point _topLeftPoint;
        private double _diameter;
        private double _mass;
        private double _pull;

        public double Diameter { get => _diameter; init => _diameter = value; }
        public double Mass { get => _mass; set => _mass = value; }
        public double Pull { get => _pull; init => _pull = value; }
        public Point TopLeftPoint { get => _topLeftPoint; set => _topLeftPoint = value; }

        //lav om til builder?
        public Planet(double radius, double mass, double pull, Point topLeftPoint = new Point())
        {
            Diameter = radius;
            Mass = mass;
            Pull = pull;
            TopLeftPoint = topLeftPoint;
        }
    }
}
