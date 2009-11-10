using System;
using LogSpy.UI.Commands;
using Microsoft.Win32;

namespace LogSpy.UI.Views.Dialogs
{
    public class OpenLogFileLogDialog : IDialog<OpenLogFileCommand>
    {
        private readonly OpenLogFileCommand command;
        private readonly OpenFileDialog openFileDialog;
        private static string logFilesPatterns;

        public OpenLogFileLogDialog(OpenLogFileCommand command)
        {
            this.command = command;
            logFilesPatterns = "Log files (*.log)|*.log|Text files (*.txt)|*.txt|Xml files (*.xml)|*.xml|All files (*.*)|*.*";
            openFileDialog = new OpenFileDialog
                                 {
                                     Title = "Open Log File",
                                     Multiselect = false,
                                     CheckFileExists = true,
                                     CheckPathExists = true,
                                     AddExtension = true,
                                     Filter = logFilesPatterns 
                                 };
            openFileDialog.FileOk += (source, args) =>
                                         {
                                             if (args.Cancel == false)
                                             {
                                                 command.OpenLogFileWith(openFileDialog.FileName);
                                             }
                                         };
        }

        public string Title
        {
            get { return openFileDialog.Title; }
            set { openFileDialog.Title = value; }
        }

        public void ShowDialog()
        {
            openFileDialog.ShowDialog();
        }


        public OpenLogFileCommand DialogHandler
        {
            get { return command; }
        }
    }
}