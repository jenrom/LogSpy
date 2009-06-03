using System;
using System.ComponentModel;
using System.Windows.Input;

namespace LogSpy.UI
{
    public class MenuItem: INotifyPropertyChanged
    {
        private ICommand command;

        public MenuItem(string displayText, MenuItemName name)
        {
            if (displayText == null) throw new ArgumentNullException("displayText");
            DisplayText = displayText;
            Name = name;
            Command = new DisabledCommand();
        }

        public MenuItem(string displayText, MenuItemName name, ICommand command)
        {
            if (displayText == null) throw new ArgumentNullException("displayText");
            if (command == null) throw new ArgumentNullException("command");
            DisplayText = displayText;
            Name = name;
            Command = command;
        }

        public string DisplayText { get; private set; }

        public MenuItemName Name { get; private set; }


        public ICommand Command 
        { 
            get
            {
                return command;   
            }
            set
            {
                if(command != value)
                {
                    command = value;   
                    PropertyChanged.For(this, x=>x.Command);
                }
            } 
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public bool Equals(MenuItem other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Equals(other.Name, Name);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != typeof (MenuItem)) return false;
            return Equals((MenuItem) obj);
        }


        public override int GetHashCode()
        {
            return Name.GetHashCode();
        }

        private class DisabledCommand : ICommand
        {
            public void Execute(object parameter)
            {
                throw new NotSupportedException();
            }

            public bool CanExecute(object parameter)
            {
                return false;
            }

            public event EventHandler CanExecuteChanged;
        }
    }
}