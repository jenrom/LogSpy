using System;

namespace LogSpy.UI
{
    public class ApplicationController
    {
        private readonly IShellView shellView;

        public ApplicationController(IShellView shellView)
        {
            if (shellView == null) throw new ArgumentNullException("shellView");
            this.shellView = shellView;
        }

        public void Initialize()
        {
            shellView.Display();
        }
    }
}