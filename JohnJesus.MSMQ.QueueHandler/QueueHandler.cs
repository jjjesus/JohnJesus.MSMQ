using System;
using System.Collections.Generic;
using System.Linq;
using System.Messaging;
using System.Text;
using JohnJesus.MSMQ.Common;

namespace JohnJesus.MSMQ.QueueHandler
{
    public class QueueHandler
    {
        private int _receivedCounter;
        private bool _isRunning;
        private MessageQueue _messageQueue;

        public QueueHandler()
        {
            initializeQueue();
        }

        private void initializeQueue()
        {
            this._receivedCounter = 0;
            string queuePath = Constants.QueueName;
            if (!MessageQueue.Exists(queuePath))
            {
                _messageQueue = MessageQueue.Create(queuePath);
            }
            else
            {
                _messageQueue = new MessageQueue(queuePath);
            }
            this._isRunning = true;
            _messageQueue.Formatter = new BinaryMessageFormatter();
        }

        public void Run()
        {
            while (_isRunning)
            {
                try
                {
                    System.Messaging.Message message = _messageQueue.Receive();
                    handleMessage(message);
                } catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
        }

        private void handleMessage(System.Messaging.Message message)
        {
            Payload payload = message.Body as Payload;
            if (payload != null)
            {
                _receivedCounter++;
                if ((_receivedCounter % 10000) == 0)
                {
                    string messageText = string.Format("Received {0} messages", _receivedCounter);
                    Console.WriteLine(messageText);
                }
            }
        }
    }
}
