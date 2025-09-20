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
    internal class SolarSystemViewModel : BaseViewModel
    {
        public Canvas SystemCanvas { get; set; } = new();
        Planet sun;
        List<Planet> planets = new();
        
        Timer timer;
        double centerX;
        double centerY;
        double angle = 0.0;
        double earthSize = 6;

        int i = 40;

        public SolarSystemViewModel() 
        {
            sun = Planet.Builder()
                .Diameter(10)
                .Colour(255,255,0)
                .Build();
                
            Planet mercury = Planet.Builder()
                .Diameter(earthSize/3)
                .Colour(255,255,255)
                .Aphelion(69.8)
                .Perihelion(46)
                .CalculateSemiMajorAxis()
                .CalculateCenterDistToPrimaryFocus()
                .Build();

            Planet venus = Planet.Builder()
                .Diameter(earthSize)
                .Colour(255, 0, 255)
                .Aphelion(108.9)
                .Perihelion(107.5)
                .CalculateSemiMajorAxis()
                .CalculateCenterDistToPrimaryFocus()
                .Build();

            Planet earth = Planet.Builder()
                .Diameter(earthSize)
                .Colour(0, 0, 255)
                .Aphelion(152.1)
                .Perihelion(147.1)
                .CalculateSemiMajorAxis()
                .CalculateCenterDistToPrimaryFocus()
                .Build();

            Planet mars = Planet.Builder()
                .Diameter(earthSize/2)
                .Colour(255, 0, 0)
                .Aphelion(249.2)
                .Perihelion(206.6)
                .CalculateSemiMajorAxis()
                .CalculateCenterDistToPrimaryFocus()
                .Build();

            Planet jupiter = Planet.Builder()
                .Diameter(earthSize*11)
                .Colour(200, 200, 0)
                .Aphelion(816)
                .Perihelion(740.6)
                .CalculateSemiMajorAxis()
                .CalculateCenterDistToPrimaryFocus()
                .Build();

            Planet saturn = Planet.Builder()
                .Diameter(earthSize*9)
                .Colour(255, 100, 100)
                .Aphelion(1503.5)
                .Perihelion(1349.8)
                .CalculateSemiMajorAxis()
                .CalculateCenterDistToPrimaryFocus()
                .Build();

            Planet uranus = Planet.Builder()
                .Diameter(earthSize*4)
                .Colour(100, 255, 100)
                .Aphelion(3006.3)
                .Perihelion(2734.9)
                .CalculateSemiMajorAxis()
                .CalculateCenterDistToPrimaryFocus()
                .Build();

            Planet neptune = Planet.Builder()
                .Diameter(earthSize*4)
                .Colour(50, 50, 225)
                .Aphelion(4537)
                .Perihelion(4459.7)
                .CalculateSemiMajorAxis()
                .CalculateCenterDistToPrimaryFocus()
                .Build();

            Planet pluto = Planet.Builder()
                .Diameter(earthSize/5)
                .Colour(150, 150, 150)
                .Aphelion(7376.1)
                .Perihelion(4436.7)
                .CalculateSemiMajorAxis()
                .CalculateCenterDistToPrimaryFocus()
                .Build();

            planets.AddRange(new List<Planet>()
            { mercury, venus, earth, mars, jupiter, saturn, uranus, neptune,pluto});
            
            SystemCanvas.Children.Add(sun.Graphic);
            foreach(Planet p in planets)
            {
                SystemCanvas.Children.Add(p.Graphic);
            }

            //Adds an event to the Loaded state of the SystemCanvas.
            SystemCanvas.Loaded += (sender, args) =>
            {
                centerX = SystemCanvas.ActualHeight/2;
                centerY = SystemCanvas.ActualWidth/2;
                
                sun.TopLeftPoint = new Point((int)(centerX - sun.Radius), (int)(centerY - sun.Radius)); 
                Canvas.SetTop(sun.Graphic, sun.TopLeftPoint.X);
                Canvas.SetLeft(sun.Graphic, sun.TopLeftPoint.Y);
                
                foreach(Planet p in planets)
                {
                    p.X = sun.TopLeftPoint.X - i;
                    p.Y = sun.TopLeftPoint.Y - i;
                    Canvas.SetTop(p.Graphic, p.X);
                    Canvas.SetLeft(p.Graphic, p.Y);
                }    
                //WithDispatcherTimer();
                WithMultiThreading();
            };

            
        }

        private void WithMultiThreading()
        {
            timer = new Timer(MovePlanetsWTimer, planets, 0, 10);
        }

        private void MovePlanetsWTimer(Object state)
        {
            foreach(Planet p in planets)
            {
                int newX = CalculateNewX(p);
                int newY = CalculateNewY(p);

                SystemCanvas.Dispatcher.Invoke(() =>
                {
                    p.TopLeftPoint = new Point(newX, newY);
                    Canvas.SetTop(p.Graphic, p.X);
                    Canvas.SetLeft(p.Graphic, p.Y);
                }
                );
            }
            angle += 0.01;
            angle = angle % 360;

        }

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
                p.TopLeftPoint = new Point(CalculateNewX(p), CalculateNewY(p));
                Canvas.SetTop(p.Graphic, p.X);
                Canvas.SetLeft(p.Graphic, p.Y);
            }
        }
            
        private int CalculateNewX(Planet p)
        {
            double x = p.Perihelion * Math.Cos(angle);
            double actualX = centerX + x;
            return (int)actualX;
        }

        private int CalculateNewY(Planet p)
        {
            double y = p.Aphelion * Math.Sin(angle);
            double actualY = centerY + y;
            return (int)actualY;
        }
    }
}
