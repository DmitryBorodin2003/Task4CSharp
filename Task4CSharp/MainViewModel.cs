using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace Task4CSharp
{
    public class MainViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private ObservableCollection<CrosswalkViewModel> crosswalks;
        private Dispatcher dispatcher;
        private int numberCrosswalks = 0;

        public ObservableCollection<CrosswalkViewModel> Crosswalks
        {
            get { return crosswalks; }
            set
            {
                crosswalks = value;
                OnPropertyChanged(nameof(Crosswalks));
            }
        }

        public MainViewModel(Dispatcher dispatcher)
        {
            this.dispatcher = dispatcher;
            Crosswalks = new ObservableCollection<CrosswalkViewModel>();
        }

        public async Task InitializeCrosswalksAsync()
        {
            await Task.Delay(100);
            Crosswalks.Add(new CrosswalkViewModel(dispatcher));
            numberCrosswalks++;
            OnPropertyChanged(nameof(numberCrosswalks));
        }

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

}
