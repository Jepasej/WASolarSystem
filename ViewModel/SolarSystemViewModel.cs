using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows.Threading;
using WASolarSystem.Model;

namespace WASolarSystem.ViewModel
{
    /// <summary>
    /// Represents the view model for a solar system simulation, managing the creation, positioning,  and movement of
    /// celestial bodies within a canvas.
    /// </summary>
    internal class SolarSystemViewModel : BaseViewModel
    {
        #region fields
        public Canvas SystemCanvas { get; set; } = new();
        private Planet sun;//NOT to scale
        private List<Planet> planets = new();
        private Timer timer;
        private double centerX;
        private double centerY;
        private double earthSize = 6;//Change to scale all planets
        private double timeScale = 0.5;//Change to scale speed of planets
        #endregion
        /// <summary>
        /// Initializes a new instance of the SolarSystemViewModel class, I would have liked to do it another 
        /// way instead of all in the constructor, any advice on this is welcome :/.
        /// To switch between DispatcherTimer and Multithreading, see method RunSimulation under region ConstructionMethods.
        /// </summary>
        /// <remarks>This constructor sets up the solar system simulation by creating planets, adding them
        /// to the canvas,  setting the sun as the central body, and starting the simulation. </remarks>
        public SolarSystemViewModel()
        {
            CreatePlanets();

            AddPlanetsToCanvas();

            SetSun();

            RunSimulation();
        }
        #region Multithreading
        private void WithMultiThreading()
        {
            timer = new Timer(MovePlanetsWTimer, planets, 0, 10);
        }

        private void MovePlanetsWTimer(Object state)
        {
            foreach(Planet p in planets)
            {
                p.Angle += p.Speed * timeScale;
                
                SystemCanvas.Dispatcher.Invoke(() =>
                {
                    p.X = CalculateNewX(p);
                    p.Y = CalculateNewY(p);
                    Canvas.SetLeft(p.Graphic, p.X);
                    Canvas.SetTop(p.Graphic, p.Y);
                }
                );
            }
        }
        #endregion
        #region DispatcherTimer
        private void WithDispatcherTimer()
        {
            DispatcherTimer dt = new DispatcherTimer();
            dt.Interval = new TimeSpan(0, 0, 0, 0, 10);
            dt.Tick += new(MovePlanets);
            dt.Start();
        }

        private void MovePlanets(object? sender, EventArgs? e)
        {
            foreach(Planet p in planets)
            {
                p.Angle += p.Speed * timeScale;
                p.X = CalculateNewX(p);
                p.Y = CalculateNewY(p);
                Canvas.SetLeft(p.Graphic, p.X);
                Canvas.SetTop(p.Graphic, p.Y);
            }
        }
        #endregion
        #region OrbitCalculation
        private int CalculateNewX(Planet p)
        {
            double x = p.SemiMajorAxis * Math.Cos(p.Angle);
            double actualX = centerX + x + p.CenterDistToPrimFocus;
            return (int)actualX;
        }

        private int CalculateNewY(Planet p)
        {
            double y = p.SemiMinorAxis * Math.Sin(p.Angle);
            double actualY = centerY + y;
            return (int)actualY;
        }
        #endregion
        #region ConstructionMethods
        private void CreatePlanets()
        {
            sun = Planet.Builder()
                .Diameter(40)
                .Colour(255, 255, 0)
                .Build();

            Planet mercury = Planet.Builder()
                .Diameter(earthSize / 3)
                .Colour(255, 255, 255)
                .Aphelion(sun, 69.8)
                .Perihelion(sun, 46)
                .CalculateSemiMajorAxis()
                .CalculateCenterDistToPrimaryFocus()
                .CalculateSemiMinorAxis()
                .CalculateSpeed()
                .Build();

            Planet venus = Planet.Builder()
                .Diameter(earthSize)
                .Colour(255, 0, 255)
                .Aphelion(sun, 108.9)
                .Perihelion(sun, 107.5)
                .CalculateSemiMajorAxis()
                .CalculateCenterDistToPrimaryFocus()
                .CalculateSemiMinorAxis()
                .CalculateSpeed()
                .Build();

            Planet earth = Planet.Builder()
                .Diameter(earthSize)
                .Colour(0, 0, 255)
                .Aphelion(sun, 152.1)
                .Perihelion(sun, 147.1)
                .CalculateSemiMajorAxis()
                .CalculateCenterDistToPrimaryFocus()
                .CalculateSemiMinorAxis()
                .CalculateSpeed()
                .Build();

            Planet mars = Planet.Builder()
                .Diameter(earthSize / 2)
                .Colour(255, 0, 0)
                .Aphelion(sun, 249.2)
                .Perihelion(sun, 206.6)
                .CalculateSemiMajorAxis()
                .CalculateCenterDistToPrimaryFocus()
                .CalculateSemiMinorAxis()
                .CalculateSpeed()
                .Build();

            Planet jupiter = Planet.Builder()
                .Diameter(earthSize * 11)
                .Colour(200, 200, 0)
                .Aphelion(sun, 816)
                .Perihelion(sun, 740.6)
                .CalculateSemiMajorAxis()
                .CalculateCenterDistToPrimaryFocus()
                .CalculateSemiMinorAxis()
                .CalculateSpeed()
                .Build();

            Planet saturn = Planet.Builder()
                .Diameter(earthSize * 9)
                .Colour(255, 100, 100)
                .Aphelion(sun, 1503.5)
                .Perihelion(sun, 1349.8)
                .CalculateSemiMajorAxis()
                .CalculateCenterDistToPrimaryFocus()
                .CalculateSemiMinorAxis()
                .CalculateSpeed()
                .Build();

            Planet uranus = Planet.Builder()
                .Diameter(earthSize * 4)
                .Colour(100, 255, 100)
                .Aphelion(sun, 3006.3)
                .Perihelion(sun, 2734.9)
                .CalculateSemiMajorAxis()
                .CalculateCenterDistToPrimaryFocus()
                .CalculateSemiMinorAxis()
                .CalculateSpeed()
                .Build();

            Planet neptune = Planet.Builder()
                .Diameter(earthSize * 4)
                .Colour(50, 50, 225)
                .Aphelion(sun, 4537)
                .Perihelion(sun, 4459.7)
                .CalculateSemiMajorAxis()
                .CalculateCenterDistToPrimaryFocus()
                .CalculateSemiMinorAxis()
                .CalculateSpeed()
                .Build();

            Planet pluto = Planet.Builder()
                .Diameter(earthSize / 5)
                .Colour(150, 150, 150)
                .Aphelion(sun, 7376.1)
                .Perihelion(sun, 4436.7)
                .CalculateSemiMajorAxis()
                .CalculateCenterDistToPrimaryFocus()
                .CalculateSemiMinorAxis()
                .CalculateSpeed()
                .Build();

            planets.AddRange(new List<Planet>()
            { mercury, venus, earth, mars, jupiter, saturn, uranus, neptune,pluto});

        }
        private void AddPlanetsToCanvas()
        {
            SystemCanvas.Children.Add(sun.Graphic);
            foreach (Planet p in planets)
            {
                SystemCanvas.Children.Add(p.Graphic);
            }
        }
        private void SetSun()
        {
            SystemCanvas.Loaded += (sender, args) =>
            {
                centerX = SystemCanvas.ActualWidth / 2;
                centerY = SystemCanvas.ActualHeight / 2;

                sun.X = (int)(centerX);
                sun.Y = (int)(centerY);
                Canvas.SetLeft(sun.Graphic, sun.X);
                Canvas.SetTop(sun.Graphic, sun.Y);
            };
        }
        private void RunSimulation()
        {
            SystemCanvas.Loaded += (sender, args) =>
            {
                //WithDispatcherTimer();
                WithMultiThreading();
            };
        }
        #endregion
    }
}
