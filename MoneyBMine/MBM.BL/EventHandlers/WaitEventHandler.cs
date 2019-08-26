using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MBM.BL
{
    public class WaitEventHandler : EventArgs
    {
        public event EventHandler<EventArgs> Waiter;

        public void StartWaitEvent()
        {
            Waiter(this, EventArgs.Empty);
        }

        public void FinishWaitEvent()
        {
            Waiter(this, EventArgs.Empty);
        }

    }
}
