using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task4CSharp
{
    public class EmergencyService : IEmergencyService
    {
        public event EventHandler EmergencyResolved;

        public CrosswalkStatus HandleEmergency()
        {
            OnEmergencyResolved();
            return CrosswalkStatus.CarGreenLight;
        }

        protected virtual void OnEmergencyResolved()
        {
            EmergencyResolved?.Invoke(this, EventArgs.Empty);
        }
    }

}
