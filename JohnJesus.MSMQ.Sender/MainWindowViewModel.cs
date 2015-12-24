using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace JohnJesus.MSMQ.Sender
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        private MessageSender msgSender;

        public MainWindowViewModel()
        {
            this.msgSender = new MessageSender(this);
        }

        private int _msgCount;
        public int MsgCount
        {
            get { return _msgCount; }
            set
            {
                if (value == _msgCount)
                    return;
                _msgCount = value;
                this.OnPropertyChanged("MsgCount");
            }
        }

        
        private string _logText;
        public string LogText {
            get { return _logText; }
            set
            {
                if (value == _logText)
                    return;
                _logText = value;
                this.OnPropertyChanged("LogText");
            }
        }

        public void LogMessage(string logMsg)
        {
            LogText += logMsg + "\n";
        }

        // Send Messages Command
        # region Send Messages Command
        private ICommand _sendMessagesCommand;
        public ICommand SendMessagesCommand
        {
            get
            {
                if (_sendMessagesCommand == null)
                {
                    _sendMessagesCommand = new RelayCommand(
                        param => SendMessages(),
                        param => CanSendMessages);
                }
                return _sendMessagesCommand;
            }
        }
        public void SendMessages()
        {
            this.msgSender.Send(this._msgCount);
        }
        bool _canSendMessages = false;
        public bool CanSendMessages
        {
            get { return _msgCount > 0; }
            set { _canSendMessages = value; }
        }
        #endregion

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            if (this.PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
