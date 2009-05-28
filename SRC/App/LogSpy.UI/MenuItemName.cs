using System;

namespace LogSpy.UI
{
    /// <summary>
    /// Represents a menu item name that is used for identifing menu items. The class contains also the default menu items
    /// </summary>
    public class MenuItemName
    {
        public static readonly MenuItemName OpenLogFile = new MenuItemName("OpenLogFileWith");
        public static readonly MenuItemName StopListening = new MenuItemName("StopListening");

        private readonly string name;

        public MenuItemName(string name)
        {
            if (name == null) throw new ArgumentNullException("name");
            this.name = name;
        }

        public bool Equals(MenuItemName other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Equals(other.name, name);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != typeof (MenuItemName)) return false;
            return Equals((MenuItemName) obj);
        }

        public override int GetHashCode()
        {
            return (name != null ? name.GetHashCode() : 0);
        }

        public override string ToString()
        {
            return name;
        }
    }
}