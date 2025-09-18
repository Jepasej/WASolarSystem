using System;
using System.Drawing;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using WASolarSystem.Model;
using System.Windows.Threading;
using System.Threading;
using System.Runtime.CompilerServices;

namespace WASolarSystem.ViewModel
{
    internal class SolarSystemViewModel : BaseViewModel
    {
        public Canvas SystemCanvas { get; set; } = new();
        Planet sun;
        Planet planet;
        Ellipse e;
        Ellipse eP;
        Timer timer;

        public SolarSystemViewModel() 
        {
            sun = new Planet(20, 500, 500);
            planet = new Planet(4, 100, 100);

            e = new Ellipse();
            e.Height = sun.Diameter;
            e.Width = sun.Diameter;
            e.Fill = new SolidColorBrush(System.Windows.Media.Color.FromRgb(255,255,0));

            eP = new Ellipse();
            eP.Height = planet.Diameter;
            eP.Width = planet.Diameter;
            eP.Fill = new SolidColorBrush(System.Windows.Media.Color.FromRgb(0, 200, 20));

            SystemCanvas.Children.Add(e);
            SystemCanvas.Children.Add(eP);

            //Adds an event to the Loaded state of the SystemCanvas.
            SystemCanvas.Loaded += (sender, args) =>
            {
                sun.TopLeftPoint = new Point((int)(SystemCanvas.ActualHeight - e.Height) / 2, (int)(SystemCanvas.ActualWidth - e.Width) / 2); 
                Canvas.SetTop(e, sun.TopLeftPoint.X);
                Canvas.SetLeft(e, sun.TopLeftPoint.Y);
                planet.TopLeftPoint = new Point(sun.TopLeftPoint.X-60, sun.TopLeftPoint.Y-60);
                Canvas.SetTop(eP, planet.TopLeftPoint.X);
                Canvas.SetLeft(eP, planet.TopLeftPoint.Y);
                //WithDispatcherTimer();
                WithMultiThreading();
            };

            
        }

        private void WithMultiThreading()
        {
            timer = new Timer(MovePlanetsWTimer, planet, 0, 10);
        }

        private void MovePlanetsWTimer(Object state)
        {
            int newX = CalculateNewX();
            int newY = CalculateNewY();
                SystemCanvas.Dispatcher.Invoke(() =>
                {
                    planet.TopLeftPoint = new Point(newX, newY);
                    Canvas.SetTop(eP, planet.TopLeftPoint.X);
                    Canvas.SetLeft(eP, planet.TopLeftPoint.Y);
                }
            );
        }

        private void WithDispatcherTimer()
        {
            DispatcherTimer dt = new DispatcherTimer();
            dt.Interval = new TimeSpan(0, 0, 0, 0, 10);
            dt.Tick += new(MovePlanets);
            dt.Start();
        }

        // Change the MovePlanets method signature to accept nullable object for sender and nullable EventArgs for e
        private void MovePlanets(object? sender, EventArgs? e)
        {
            planet.TopLeftPoint = new Point(CalculateNewX(), CalculateNewY());
            Canvas.SetTop(eP, planet.TopLeftPoint.X);
            Canvas.SetLeft(eP, planet.TopLeftPoint.Y);
        }
            
        private int CalculateNewX()
        {
            return planet.TopLeftPoint.X+1;
        }

        private int CalculateNewY()
        {
            return planet.TopLeftPoint.Y+1;
        }
    }
}
