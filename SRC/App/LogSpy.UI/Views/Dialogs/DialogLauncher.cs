using System;
using StructureMap;

namespace LogSpy.UI.Views.Dialogs
{
    public class DialogLauncher: IDialogLauncher
    {
        private readonly IContainer container;

        public DialogLauncher(IContainer container)
        {
            if (container == null) throw new ArgumentNullException("container");
            this.container = container;
        }

        public void LaunchFor<TDialogHandler>(TDialogHandler handler)
        {
            var dialog = container.With(handler).GetInstance<IDialog<TDialogHandler>>();
            dialog.ShowDialog();
        }
    }
}