using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JohnJesus.MSMQ.QueueHandler
{
    class Program
    {
        static void Main(string[] args)
        {
            var qHandler = new QueueHandler();
            qHandler.Run();
        }
    }
}
