using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace WASolarSystem.ViewModel
{
    internal class MainWindowViewModel : BaseViewModel
    {
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
        private List<BaseViewModel> _viewModelList;

        //i am making some comments in this branch
        public MainWindowViewModel()
        {
            CurrentViewModel = new SolarSystemViewModel();
            AddVMToList(CurrentViewModel);
        }

        private void AddVMToList(BaseViewModel vm)
        {
            if(_viewModelList == null)
                _viewModelList = new List<BaseViewModel>();

            if(vm != null) 
                _viewModelList.Add(vm);
        }
    }
}
