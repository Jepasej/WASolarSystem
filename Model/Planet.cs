using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Shapes;

namespace WASolarSystem.Model
{
    /// <summary>
    /// Represents a celestial body with properties defining its size and orbit 
    /// (also describes a sun)
    /// </summary>
    /// <remarks>Must be build through the Planet.Builder method.</remarks>
    internal class Planet
    {
        #region fields
        private Point _topLeftPoint;
        private Ellipse _graphic;
        private double _diameter;
        private double _radius;
        private double _perihelion;
        private double _aphelion;
        private double _semiMajorAxis;
        private double _semiMinorAxis;
        private double _speed;
        private double _centerDistToPrimFocus;
        private double _angle;
        #endregion
        #region properties
        public double Diameter { get => _diameter; set => _diameter = value; }
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
        #region deprecated
        //public Point TopLeftPoint
        //{
        //    get => _topLeftPoint;
        //    set 
        //    { 
        //        _topLeftPoint.X = value.X-(int)Radius;
        //        _topLeftPoint.Y = value.Y-(int)Radius;
        //    }
        //}
        #endregion
        public double Radius { get => _radius; set => _radius = value; }
        public double Perihelion { get => _perihelion; set => _perihelion = value; }
        public double Aphelion { get => _aphelion; set => _aphelion = value; }
        public double SemiMajorAxis { get => _semiMajorAxis; set => _semiMajorAxis = value; }
        public double CenterDistToPrimFocus { get => _centerDistToPrimFocus; set => _centerDistToPrimFocus = value; }
        public Ellipse Graphic { get => _graphic; set => _graphic = value; }
        public double Angle { get => _angle; set => _angle = value; }
        public double Speed { get => _speed; set => _speed = value; }
        public double SemiMinorAxis { get => _semiMinorAxis; set => _semiMinorAxis = value; }
        #endregion
        private Planet()
        {
            _topLeftPoint = new Point(0, 0);
            Graphic = new Ellipse();
        }
        /// <summary>
        /// Creates and returns a new instance of PlanetBuilder for constructing a Planet
        /// object.
        /// </summary>
        public static PlanetBuilder Builder() => new PlanetBuilder(new Planet());
    }
}
