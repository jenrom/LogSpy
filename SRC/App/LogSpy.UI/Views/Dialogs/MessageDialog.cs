using System;
using LogSpy.UI.Commands;
using System.Windows;
namespace LogSpy.UI.Views.Dialogs
{
    public class MessageDialog: IDialog<IDisplayMessageCommand>
    {
        private IDisplayMessageCommand _dialogHandler;

        public MessageDialog(IDisplayMessageCommand dialogHandler)
        {
            _dialogHandler = dialogHandler;
        }

        public string Title
        {
            get { throw new NotImplementedException(); }
            set { throw new NotImplementedException(); }
        }

        public void ShowDialog()
        {
            
            throw new NotImplementedException();
        }

        public IDisplayMessageCommand DialogHandler
        {
            get { return _dialogHandler; }
        }
    }
}