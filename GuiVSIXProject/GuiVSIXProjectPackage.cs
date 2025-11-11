global using Community.VisualStudio.Toolkit;
global using Microsoft.VisualStudio.Shell;
global using System;
global using Task = System.Threading.Tasks.Task;
using MyVsExtension;
using System.Runtime.InteropServices;
using System.Threading;

namespace GuiVSIXProject
{
    [PackageRegistration(UseManagedResourcesOnly = true, AllowsBackgroundLoading = true)]
    [InstalledProductRegistration("My Extension", "Описание расширения", "1.0")]
    [ProvideMenuResource("Menus.ctmenu", 1)]
    [ProvideToolWindow(typeof(MyToolWindow),
    Style = VsDockStyle.Tabbed,
    Window = "3ae79031-e1bc-11d0-8f78-00a0c9110057")] // GUID окна Solution Explorer
    [Guid("12345678-1234-1234-1234-123456789012")]
    public sealed class MyPackage : AsyncPackage
    {
        protected override async Task InitializeAsync(
            CancellationToken cancellationToken,
            IProgress<ServiceProgressData> progress)
        {
            await this.JoinableTaskFactory.SwitchToMainThreadAsync(cancellationToken);
            await MyToolWindowCommand.InitializeAsync(this);
        }
    }
}