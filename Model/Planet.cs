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
        private Ellipse _graphic;
        private double _diameter;
        private double _radius;
        private double _perihelion;
        private double _aphelion;
        private double _semiMajorAxis;
        private double _centerDistToPrimFocus;

        public double Diameter { get => _diameter; set => _diameter = value; }
        #region experimental
        public int X
        {
            get => _topLeftPoint.X;
            set => _topLeftPoint.X = value-(int)Radius;
        }
        public int Y
        {
            get => _topLeftPoint.Y;
            set => _topLeftPoint.Y = value-(int)Radius;
        }
        #endregion
        public Point TopLeftPoint
        {
            get => _topLeftPoint;
            set 
            { 
                _topLeftPoint.X = value.X-(int)Radius;
                _topLeftPoint.Y = value.Y-(int)Radius;
            }
        }
        public double Radius { get => _radius; set => _radius = value; }
        public double Perihelion { get => _perihelion; set => _perihelion = value; }
        public double Aphelion { get => _aphelion; set => _aphelion = value; }
        public double SemiMajorAxis { get => _semiMajorAxis; set => _semiMajorAxis = value; }
        public double CenterDistToPrimFocus { get => _centerDistToPrimFocus; set => _centerDistToPrimFocus = value; }
        public Ellipse Graphic { get => _graphic; set => _graphic = value; }

        private Planet()
        {
            TopLeftPoint = new Point(0, 0);
            Graphic = new Ellipse();
        }

        public static PlanetBuilder Builder() => new PlanetBuilder(new Planet());
    }
}
