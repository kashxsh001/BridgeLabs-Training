using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace ClassLibrary1
{
    
    
        public class LongRunningService
        {
            public string LongRunningTask()
            {
                Thread.Sleep(3000);

                return "Task Completed";
            }
        }
}
