using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JohnJesus.MSMQ.Common
{
    [Serializable]
    public class Payload
    {
        private Guid id;
        private string text;
        private DateTime timeStamp;
        private byte[] buffer;

        public Payload()
        {

        }


        public Payload(string text, byte bufferFillValue, int bufferSize)
        {
            id = Guid.NewGuid();
            this.text = text;
            timeStamp = DateTime.UtcNow;
            buffer = new byte[bufferSize];
            for (int i = 0; i < bufferSize; i++)
            {
                buffer[i] = bufferFillValue;
            }

        }


        public Guid Id
        {
            get
            {
                return id;
            }
            set
            {
                if (id == value)
                    return;
                id = value;
            }
        }
        public string Text
        {
            get
            {
                return text;
            }
            set
            {
                if (text == value)
                    return;
                text = value;
            }
        }
        public DateTime TimeStamp
        {
            get
            {
                return timeStamp;
            }
            set
            {
                if (timeStamp == value)
                    return;
                timeStamp = value;
            }
        }
        public byte[] Buffer
        {
            get
            {
                return buffer;
            }
            set
            {
                if (buffer == value)
                    return;
                buffer = value;
            }
        }

    }
}
