using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace Task4CSharp
{
    public class CrosswalkViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private Crosswalk crosswalk;
        private Dispatcher dispatcher;

        public int Id => crosswalk.Id;
        public int CarsPassed { get; private set; }
        public int PedestriansCrossed { get; private set; }
        public string Status { get; private set; }

        public CrosswalkViewModel(Dispatcher dispatcher)
        {
            this.dispatcher = dispatcher;
            crosswalk = new Crosswalk();

            crosswalk.CarsPassed += (sender, count) =>
            {
                dispatcher.Invoke(() => CarsPassed = count);
                OnPropertyChanged(nameof(CarsPassed));
            };

            crosswalk.PedestriansCrossed += (sender, count) =>
            {
                dispatcher.Invoke(() => PedestriansCrossed = count);
                OnPropertyChanged(nameof(PedestriansCrossed));
            };

            crosswalk.StatusChanged += (sender, newStatus) =>
            {
                dispatcher.Invoke(() => Status = newStatus.ToString());
                OnPropertyChanged(nameof(Status));
            };

            Task.Run(() => crosswalk.StartCrosswalk());

            Status = CrosswalkStatus.CarGreenLight.ToString();
        }

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
