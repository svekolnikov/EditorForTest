using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace EditorForTest.Utils
{
    public abstract class ViewModelBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected void IPropertyChanged(string PropertyName)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(PropertyName));
        }

        protected bool SetPropertry<T>
            (ref T Storage, T Value, [CallerMemberName] string Propertyname = null)
        {
            if (EqualityComparer<T>.Default.Equals(Storage, Value)) return false;
            Storage = Value;
            IPropertyChanged(Propertyname);
            return true;
        }
    }
}
