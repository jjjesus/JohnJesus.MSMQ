using System;
using System.Collections.Generic;
using System.Linq;
using System.Messaging;
using System.Text;
using JohnJesus.MSMQ.Common;

namespace JohnJesus.MSMQ.Sender
{
    public class MessageSender
    {
        private MessageQueue messageQueue;
        private MainWindowViewModel viewModel;

        public MessageSender(MainWindowViewModel vm)
        {
            this.viewModel = vm;
            initializeQueue();
        }
        public void Send(int count)
        {
            System.Threading.ThreadPool.QueueUserWorkItem(new System.Threading.WaitCallback(AsychSendMessages), count);
        }

        private void AsychSendMessages(object countAsObject)
        {
            int count = (int)countAsObject;
            SendMessages(count);
        }

        private void SendMessages(int count)
        {
            Random random = new Random(count);
            string logmsg = string.Format("Sending {0} messages ...", count);
            this.viewModel.LogMessage(logmsg);
            DateTime start = DateTime.Now;
            for (int i = 0; i < count; i++)
            {
                byte b = Convert.ToByte(random.Next(128));
                int size = random.Next(1024);
                string text = string.Format("Message: Fill {0} {1}", b, size);
                Payload payload = new Payload(text, b, size);
                messageQueue.Send(payload);
            }
            DateTime end = DateTime.Now;
            TimeSpan ts = end - start;
            logmsg = string.Format("{0} messages sent in {1}", count, ts);
            this.viewModel.LogMessage(logmsg);
        }


        private void initializeQueue()
        {
            string queuePath = Constants.QueueName;
            if (!MessageQueue.Exists(queuePath))
            {
                this.messageQueue = MessageQueue.Create(queuePath);
            }
            else
            {
                this.messageQueue = new MessageQueue(queuePath);
            }
            this.messageQueue.Formatter = new BinaryMessageFormatter();
        }
    }
}
