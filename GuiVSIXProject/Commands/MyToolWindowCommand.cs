using GuiVSIXProject;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Interop;
using System;
using System.ComponentModel.Design;

namespace MyVsExtension
{
    internal sealed class MyToolWindowCommand
    {
        public static async Task InitializeAsync(AsyncPackage package)
        {
            await ThreadHelper.JoinableTaskFactory.SwitchToMainThreadAsync();

            var commandService = await package.GetServiceAsync(
                typeof(IMenuCommandService)) as OleMenuCommandService;

            if (commandService != null)
            {
                var cmdId = new CommandID(
                    new Guid("12345678-1234-1234-1234-123456789012"),
                    0x0100);

                var cmd = new MenuCommand((s, e) => Execute(package), cmdId);
                commandService.AddCommand(cmd);
            }
        }

        private static void Execute(AsyncPackage package)
        {
            ThreadHelper.ThrowIfNotOnUIThread();

            // Получаем или создаем окно
            var window = package.FindToolWindow(typeof(MyToolWindow), 0, true);
            if (window?.Frame == null)
                throw new NotSupportedException("Cannot create tool window");

            var windowFrame = (IVsWindowFrame)window.Frame;

            // Показываем окно - оно автоматически затабится рядом с Solution Explorer
            Microsoft.VisualStudio.ErrorHandler.ThrowOnFailure(windowFrame.Show());
        }
    }
}