using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MBM.BL
{
    public class AddRecordEventHandler 
    {
        /// <summary>
        /// Called on Successful adding of record
        /// </summary>
        public event EventHandler<EventArgs> AddRecordEvent;

        /// <summary>
        /// Method to fire the completed event
        /// </summary>
        public void AddCompleteEvent()
        {
            AddRecordEvent(this, EventArgs.Empty);
        }

        public event EventHandler<EventArgs> AddRecordFailedEvent;

        public void AddFailedEvent()
        {
            AddRecordFailedEvent(this, EventArgs.Empty);
        }

        public event EventHandler<EventArgs> AddRecordCounterEvent;

        public int RecordCount { get; set; }
        public void RecordCountEvent()
        {

            AddRecordCounterEvent(this, EventArgs.Empty);
        }


    }
}
