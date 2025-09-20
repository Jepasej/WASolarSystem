using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace WASolarSystem.ViewModel
{
    /// <summary>
    /// Serves as the base class for view models, providing support for property change notifications.
    /// </summary>
    /// <remarks>I don't think I needed this one this time...</remarks>
    internal abstract class BaseViewModel
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
