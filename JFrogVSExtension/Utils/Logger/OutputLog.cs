using Microsoft.VisualStudio.Shell.Interop;
using System;
using System.Globalization;

namespace JFrogVSExtension.Logger
{
    class OutputLog
    {
        private static IVsOutputWindow outputWindow = null;
        public static void InitOutputWindowPane(IVsOutputWindow outputWindowObj)
        {
            // Get the output window
            if (outputWindow == null)
            {
                outputWindow = outputWindowObj;
                ShowMessage("Logger init");
            }
        }

        public static void ShowMessage(String message)
        {
            if (outputWindow != null)
            {
                IVsOutputWindowPane outputWindowPane = init();
                DateTime localDate = DateTime.Now;
                var culture = new CultureInfo("en-GB");
                message = "[" + localDate.ToString(culture) + "] " + message;

                outputWindowPane.Activate();
                outputWindowPane.OutputString(message);
                outputWindowPane.OutputString("\n");
            }

        }

        private static IVsOutputWindowPane init()
        {
            int hr;
            const int VISIBLE = 1;
            const int DO_NOT_CLEAR_WITH_SOLUTION = 0;
            IVsOutputWindowPane outputWindowPane;
            // The General pane is not created by default. We must force its creation
            hr = outputWindow.CreatePane(new Guid(MainPanelPackage.PackageGuidString), "JFrog", VISIBLE, DO_NOT_CLEAR_WITH_SOLUTION);
            Microsoft.VisualStudio.ErrorHandler.ThrowOnFailure(hr);

            // Get the pane
            hr = outputWindow.GetPane(new Guid(MainPanelPackage.PackageGuidString), out outputWindowPane);
            Microsoft.VisualStudio.ErrorHandler.ThrowOnFailure(hr);
            return outputWindowPane;
        }        
    }
}
