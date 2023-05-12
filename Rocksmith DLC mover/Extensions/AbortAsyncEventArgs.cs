using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rocksmith_DLC_mover.Extensions
{
    public class AbortAsyncEventArgs : EventArgs
    {
        public AbortAsyncEventArgs(string message) 
        {
            AbortReason = message;
        }
        public string AbortReason { get; set; }
    }

    public class BackgroundWorkerUpdateProgressArgs : EventArgs
    {
        public string backgroundWorkerUpdateText { get; set; }
    }


}
