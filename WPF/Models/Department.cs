using System.ComponentModel;

namespace WPF.Models
{
    class Department : INotifyPropertyChanged
    {
        private string _Name;
        public int Id { get; set; }

        public string Name
        {
            get => _Name;
            set
            {
                _Name = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Name)));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public override string ToString()
        {
            return Name;
        }
    }
}
