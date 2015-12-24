using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JohnJesus.MSMQ.Common
{
    public class Constants
    {
        public const string DEFAULT_QUEUE_NAME = "JohnJesusQueue";
        public const string QUEUENAME_PREFIX = ".\\Private$\\";

        public static string QueueName
        {
            get
            {
                string result = string.Format("{0}{1}", QUEUENAME_PREFIX, DEFAULT_QUEUE_NAME);
                return result;
            }
        }


    }
}
