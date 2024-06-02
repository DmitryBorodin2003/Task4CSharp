using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task4CSharp
{
    public interface IEmergencyService
    {
        event EventHandler EmergencyResolved;
        CrosswalkStatus HandleEmergency();
    }

    public enum CrosswalkStatus
    {
        CarGreenLight,
        PedestrianGreenLight,
        Emergency
    }


    public class Crosswalk
    {
        private static int idCounter = 0;
        public int Id { get; }

        private int carsPassed = 0;
        private int pedestriansCrossed = 0;
        private CrosswalkStatus status = CrosswalkStatus.CarGreenLight;

        private EmergencyService emergencyService = new EmergencyService();
        private Random random = new Random();

        public event EventHandler<int> CarsPassed;
        public event EventHandler<int> PedestriansCrossed;
        public event EventHandler<CrosswalkStatus> StatusChanged;

        public Crosswalk()
        {
            Id = ++idCounter;
        }

        public void StartCrosswalk()
        {
            while (true)
            {
                if (random.Next(0, 100) < 5) // 5% - ШАНС НА АВАРИЮ
                {
                    status = CrosswalkStatus.Emergency;
                    OnStatusChanged(status);
                    Thread.Sleep(3000);
                    status = emergencyService.HandleEmergency();
                    OnStatusChanged(status);
                    continue;
                }

                if (DateTime.Now.Second % 10 == 0)
                {
                    status = CrosswalkStatus.PedestrianGreenLight;
                    OnStatusChanged(status);

                    int greenLightDuration = 5;
                    for (int i = 0; i < greenLightDuration; i++)
                    {
                        pedestriansCrossed++;
                        OnPedestriansCrossed(pedestriansCrossed);
                        Thread.Sleep(1000);
                    }

                    status = CrosswalkStatus.CarGreenLight;
                    OnStatusChanged(status);
                }
                else
                {
                    carsPassed++;
                    OnCarsPassed(carsPassed);
                }

                Thread.Sleep(1000); // Simulate each second
            }
        }

        protected virtual void OnCarsPassed(int count)
        {
            CarsPassed?.Invoke(this, count);
        }

        protected virtual void OnPedestriansCrossed(int count)
        {
            PedestriansCrossed?.Invoke(this, count);
        }

        protected virtual void OnStatusChanged(CrosswalkStatus newStatus)
        {
            StatusChanged?.Invoke(this, newStatus);
        }
    }

}
