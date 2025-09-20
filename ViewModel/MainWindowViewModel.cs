using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace WASolarSystem.ViewModel
{
    /// <summary>
    /// Represents the view model for the main window of the application.
    /// </summary>
    /// <remarks>Relationship between views and viewmodels are held in datatemplates in App.xaml</remarks>
    internal class MainWindowViewModel : BaseViewModel
    {
        //where we hold our currentviewmodel
        private BaseViewModel _currentViewModel;

        public BaseViewModel CurrentViewModel
        {
            get
            {
                return _currentViewModel;
            }
            set
            {
                _currentViewModel = value;
            }
        }

        //to hold our generated viewmodels, as to not new them every time.
        //not REALLY needed in this application, as there is only one "real" viewmodel
        private List<BaseViewModel> _viewModelList;

        /// <summary>
        /// Constructor for MainWindowViewModel
        /// Sets the CurrentViewModel to the SolarSystemViewModel
        /// and adds it to our list.
        /// </summary>
        public MainWindowViewModel()
        {
            CurrentViewModel = new SolarSystemViewModel();
            AddVMToList(CurrentViewModel);
        }

        /// <summary>
        /// Adds a viewmodel to our list, if it is not null
        /// </summary>
        /// <remarks>Implements late instantiation for our viewmodellist. </remarks>
        /// <param name="vm">viewmodel to be added</param>
        private void AddVMToList(BaseViewModel vm)
        {
            if(_viewModelList == null)
                _viewModelList = new List<BaseViewModel>();

            if(vm != null) 
                _viewModelList.Add(vm);
        }
    }
}
