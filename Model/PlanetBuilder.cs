using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace WASolarSystem.Model
{
    internal class PlanetBuilder
    {
        private Planet _planet;

        public PlanetBuilder(Planet planet)
        {
            _planet = planet;
        }

        public PlanetBuilder Diameter(double diameter) 
        {
            _planet.Diameter = diameter;
            _planet.Graphic.Height = diameter;
            _planet.Graphic.Width = diameter;
            _planet.Radius = diameter/2;
            return this;
        }

        public PlanetBuilder Colour(byte red, byte green, byte blue)
        { 
            SolidColorBrush planetColour = 
                new SolidColorBrush(System.Windows.Media.Color.FromRgb(red, green, blue));
            _planet.Graphic.Fill = planetColour;
            return this;
        }

        public PlanetBuilder Perihelion(double perihelion)
        {
            _planet.Perihelion = perihelion;
            return this;
        }

        public PlanetBuilder Aphelion(double aphelion)
        {
            _planet.Aphelion = aphelion;
            return this;
        }

        public PlanetBuilder CalculateSemiMajorAxis()
        {
            if(_planet.Perihelion!=0.0 && _planet.Aphelion!=0.0)
            {
                _planet.SemiMajorAxis = (_planet.Perihelion + _planet.Aphelion) / 2;
                return this;
            }
            else
            {
                throw new Exception("Must set Aphelion and Perihelion " +
                    "before trying to calculate a SemiMajorAxis");
            }
        }

        public PlanetBuilder CalculateCenterDistToPrimaryFocus()
        {
            if(_planet.SemiMajorAxis!=0.0)
            {
                _planet.CenterDistToPrimFocus = _planet.SemiMajorAxis - _planet.Perihelion;
                return this;
            }
            else
            {
                throw new Exception("Must set Aphelion, Perihelion and SemiMajorAxis " +
                    "before calculating CenterDisttoPrimFocus");
            }    
        }

        public Planet Build()
        {
            return _planet;
        }
    }
}
