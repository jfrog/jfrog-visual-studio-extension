namespace JFrogVSExtension
{
    using System;
    using System.Runtime.InteropServices;
    using Microsoft.VisualStudio.Shell;
    using Task = System.Threading.Tasks.Task;

    /// <summary>
    /// This class implements the tool window exposed by this package and hosts a user control.
    /// </summary>
    /// <remarks>
    /// In Visual Studio tool windows are composed of a frame (implemented by the shell) and a pane,
    /// usually implemented by the package implementer.
    /// <para>
    /// This class derives from the ToolWindowPane class provided from the MPF in order to use its
    /// implementation of the IVsUIElementPane interface.
    /// </para>
    /// </remarks>
    [Guid("b8e21dce-df2e-4d2b-9fa0-b1374e5eb6f4")]
    public class MainPanel : ToolWindowPane
    {
        private static MainPanel mainPanel;
        /// <summary>
        /// Initializes a new instance of the <see cref="MainPanel"/> class.
        /// </summary>
        public MainPanel() : base(null)
        {
            this.Caption = "JFrog";
            // This is the user control hosted by the tool window; Note that, even if this class implements IDisposable,
            // we are not calling Dispose on this object. This is because ToolWindowPane calls Dispose on
            // the object returned by the Content property.
            this.Content = new MainPanelControl();
            mainPanel = this;
        }

        public static MainPanel GetInstance()
        {
            return mainPanel;
        }

        public async Task LoadAsync()
        {
            await ((MainPanelControl)this.Content).LoadAsync();
        }

        public async Task CloseAsync()
        {
            await ((MainPanelControl)this.Content).CloseAsync();
        }
    }
}
