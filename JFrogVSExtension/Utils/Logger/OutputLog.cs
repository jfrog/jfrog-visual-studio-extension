using Microsoft.VisualStudio.Shell.Interop;
using Microsoft.VisualStudio.Threading;
using System;
using System.Globalization;
using Microsoft.VisualStudio.Shell;
using System.Threading.Tasks;
using Task = System.Threading.Tasks.Task;
using Microsoft.VisualStudio;

namespace JFrogVSExtension.Logger
{
    class OutputLog
    {
        private static IVsOutputWindow outputWindow = null;
        private static Guid OutputWindowPaneUid = new Guid(MainPanelPackage.PackageGuidString);

        public static async Task InitOutputWindowPaneAsync()
        {
            await ThreadHelper.JoinableTaskFactory.SwitchToMainThreadAsync();
            if (outputWindow == null)
            {
                outputWindow = ServiceProvider.GlobalProvider.GetService(typeof(SVsOutputWindow)) as IVsOutputWindow;
            }
        }

        public static async Task ShowMessageAsync(String message)
        {
            await ThreadHelper.JoinableTaskFactory.SwitchToMainThreadAsync();
            IVsOutputWindowPane outputWindowPane = null;

            // Initialize the output window. 
            // Required if trying to output a message before the MainPanel has initialized.
            await InitOutputWindowPaneAsync();

            // Initialize the JFrog output window pane if required.
            if (outputWindow != null && ErrorHandler.Failed(outputWindow.GetPane(OutputWindowPaneUid, out outputWindowPane)))
            {
                outputWindowPane = InitJfrogWindowPane();
            }

            // If couldn't set the output window, cannot log messages.
            if (outputWindow == null)
            {
                return;
            }

            // Write the message.
            DateTime localDate = DateTime.Now;
            var culture = new CultureInfo("en-GB");
            message = "[" + localDate.ToString(culture) + "] " + message;
            outputWindowPane.Activate();
            outputWindowPane.OutputStringThreadSafe(message);
            outputWindowPane.OutputStringThreadSafe("\n");
        }

        private static IVsOutputWindowPane InitJfrogWindowPane()
        {
            ThreadHelper.ThrowIfNotOnUIThread();
            int hr;
            const int VISIBLE = 1;
            const int DO_NOT_CLEAR_WITH_SOLUTION = 0;

            hr = outputWindow.CreatePane(OutputWindowPaneUid, "JFrog", VISIBLE, DO_NOT_CLEAR_WITH_SOLUTION);
            ErrorHandler.ThrowOnFailure(hr);

            hr = outputWindow.GetPane(OutputWindowPaneUid, out IVsOutputWindowPane outputWindowPane);
            ErrorHandler.ThrowOnFailure(hr);

            return outputWindowPane;
        }
    }
}
