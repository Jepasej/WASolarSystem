using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace WASolarSystem.Model
{
    /// <summary>
    /// Fluent builder class for building a Planet. Most of the latter half of these should have probably been automated instead of called...
    /// </summary>
    internal class PlanetBuilder
    {
        private Planet _planet;//instance being built

        /// <summary>
        /// Constructor for the PlanetBuilder class.
        /// </summary>
        /// <param name="planet">Planet received from Builder call in Planet class</param>
        public PlanetBuilder(Planet planet)
        {
            _planet = planet;
        }

        /// <summary>
        /// Sets the diameter and radius of the planet and updates its graphical dimensions accordingly.
        /// </summary>
        /// <param name="diameter">The diameter of the planet. </param>
        public PlanetBuilder Diameter(double diameter) 
        {
            _planet.Diameter = diameter;
            _planet.Graphic.Height = diameter;
            _planet.Graphic.Width = diameter;
            _planet.Radius = diameter/2;
            return this;
        }

        /// <summary>
        /// Sets the colour of the planet from an RGB value.
        /// </summary>
        public PlanetBuilder Colour(byte red, byte green, byte blue)
        { 
            SolidColorBrush planetColour = 
                new SolidColorBrush(System.Windows.Media.Color.FromRgb(red, green, blue));
            _planet.Graphic.Fill = planetColour;
            return this;
        }

        /// <summary>
        /// Sets the perihelion (closest distance to the sun) distance of the planet relative to the specified primary focus.
        /// </summary>
        public PlanetBuilder Perihelion(Planet primaryFocus, double perihelion)
        {
            _planet.Perihelion = primaryFocus.Radius + perihelion;
            return this;
        }

        /// <summary>
        /// Sets the aphelion (furthest distance to the sun) distance of the planet relative to the specified primary focus.
        /// </summary>
        public PlanetBuilder Aphelion(Planet primaryFocus, double aphelion)
        {
            _planet.Aphelion = primaryFocus.Radius + aphelion;
            return this;
        }

        /// <summary>
        /// Calculates the semi-major axis of the planet's orbit based on its perihelion and aphelion distances.
        /// </summary>
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

        /// <summary>
        /// Calculates the distance from the center of the planet's orbit to its primary focus and updates the
        /// corresponding property in the planet object.
        /// </summary>
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

        /// <summary>
        /// Calculates the semi-minor axis of the planet's orbit based on its semi-major axis  and the distance from the
        /// center to the primary focus.
        /// </summary>
        public PlanetBuilder CalculateSemiMinorAxis()
        {
            if( _planet.CenterDistToPrimFocus!=0.0)
            {
                _planet.SemiMinorAxis = Math.Sqrt(Math.Pow(_planet.SemiMajorAxis, 2) - Math.Pow(_planet.CenterDistToPrimFocus, 2));
                return this;
            }
            else
            {
                throw new Exception("Must Calculate CenterDistToPrimaryFocus before SemiMinorAxis");                    
            }
        }

        /// <summary>
        /// Calculates the orbital speed of the planet based on its semi-major axis.
        /// </summary>
        public PlanetBuilder CalculateSpeed()
        {
            if (_planet.SemiMajorAxis != 0.0)
            { 
                _planet.Speed = 1 / _planet.SemiMajorAxis;

                //refactor this line
                _planet.Angle = Math.PI;

                return this;
            }
            else
            {
                throw new Exception("Must calculate " +
                "SemiMajorAxis before attemptint to calculate speed");
            }
        }

        /// <summary>
        /// Constructs and returns the configured <see cref="Planet"/> instance.
        /// </summary>
        public Planet Build()
        {
            return _planet;
        }
    }
}
