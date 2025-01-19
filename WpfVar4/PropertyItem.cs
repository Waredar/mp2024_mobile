using System;
using System.ComponentModel;

namespace WpfVar4
{
    public class PropertyItem : INotifyPropertyChanged
    {
        private string name = String.Empty;
        private object value = String.Empty;
        public string Name
        {
            get => name;
            set
            {
                if (name != value)
                {
                    name = value;
                    OnPropertyChanged(Name);
                }
            }
        }

        public object Value
        {
            get => value;
            set
            {
                if (this.value != value)
                {
                    this.value = value;
                    OnPropertyChanged(Name);
                }
            }
        }

        public PropertyItem(string name, string value)
        {
            this.Name = name;
            this.Value = value;
        }


        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
