using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;
using System.ComponentModel.Design;
using JFrogVSExtension.HttpClient;
using JFrogVSExtension.Logger;
using JFrogVSExtension.OptionsMenu;
using Microsoft.VisualStudio;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Interop;
using System.Threading;
using Task = System.Threading.Tasks.Task;
using JFrogVSExtension.Utils.ScanManager;

namespace JFrogVSExtension
{
    /// <summary>
    /// This is the class that implements the package exposed by this assembly.
    /// </summary>
    /// <remarks>
    /// <para>
    /// The minimum requirement for a class to be considered a valid package for Visual Studio
    /// is to implement the IVsPackage interface and register itself with the shell.
    /// This package uses the helper classes defined inside the Managed Package Framework (MPF)
    /// to do it: it derives from the Package class that provides the implementation of the
    /// IVsPackage interface and uses the registration attributes defined in the framework to
    /// register itself and its components with the shell. These attributes tell the pkgdef creation
    /// utility what data to put into .pkgdef file.
    /// </para>
    /// <para>
    /// To get loaded into VS, the package must be referred by &lt;Asset Type="Microsoft.VisualStudio.VsPackage" ...&gt; in .vsixmanifest file.
    /// </para>
    /// </remarks>
    [PackageRegistration(UseManagedResourcesOnly = true, AllowsBackgroundLoading = true)]
    [InstalledProductRegistration("#110", "#112", "1.0", IconResourceID = 400)] // Info on this package for Help/About
    [ProvideMenuResource("Menus.ctmenu", 1)]
    [ProvideToolWindow(typeof(MainPanel), Style = VsDockStyle.Tabbed, Window = "34E76E81-EE4A-11D0-AE2E-00A0C90FFFC3")] //Docks the window to the Output panel 
    [ProvideService(typeof(MainPanel), IsAsyncQueryable = true)]
    [Guid(MainPanelPackage.PackageGuidString)]
    [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1650:ElementDocumentationMustBeSpelledCorrectly", Justification = "pkgdef, VS and vsixmanifest are valid VS terms")]
    public sealed class MainPanelPackage : AsyncPackage
    {
        /// <summary>
        /// MainPanelPackage GUID string.
        /// </summary>
        public const string PackageGuidString = "67a004b3-f2f3-4c3e-ac15-49974b24fc49";
        private static EnvDTE.DTE dte = null;
        private IVsSolution _solution;
        private SolutionEventsHandler _solutionEventsHandler;
        private uint _solutionEventsCookie = 0;

        /// <summary>
        /// Initializes a new instance of the <see cref="MainPanel"/> class.
        /// </summary>
        public MainPanelPackage()
        {
            // Inside this method you can place any initialization code that does not require
            // any Visual Studio service because at this point the package object is created but
            // not sited yet inside Visual Studio environment. The place to do all the other
            // initialization is the Initialize method.
        }

        #region Package Members

        /// <summary>
        /// Initialization of the package; this method is called right after the package is sited, so this is the place
        /// where you can put all the initialization code that rely on services provided by VisualStudio.
        /// </summary>
        protected override async Task InitializeAsync(CancellationToken cancellationToken, IProgress<ServiceProgressData> progress)
        {
            await InitComponentsAsync();
            OleMenuCommandService commandService = await base.GetServiceAsync(typeof(IMenuCommandService)) as OleMenuCommandService;
            MainPanelCommand.Initialize(this, commandService);
            await base.InitializeAsync(cancellationToken, progress);

            // Switch to main thread for dealing with type IVsSolution.
            await ThreadHelper.JoinableTaskFactory.SwitchToMainThreadAsync();
            _solution = await base.GetServiceAsync(typeof(SVsSolution)) as IVsSolution;
            if (_solution != null)
            {
                _solutionEventsHandler = new SolutionEventsHandler();
                _solution.AdviseSolutionEvents(_solutionEventsHandler, out _solutionEventsCookie);
            }
            // To trigger upon loading a solution
            object objLoadMgr = this;   //the class that implements IVsSolutionManager
            IVsSolution pSolution = await GetServiceAsync(typeof(SVsSolution)) as IVsSolution;

            object existingLoadManager = null;
            pSolution.GetProperty((int)__VSPROPID4.VSPROPID_ActiveSolutionLoadManager, out existingLoadManager);
            pSolution.SetProperty((int)__VSPROPID4.VSPROPID_ActiveSolutionLoadManager, objLoadMgr);
        }

        protected override void Dispose(bool disposing)
        {
            ThreadHelper.ThrowIfNotOnUIThread();
            if (_solutionEventsCookie != 0)
            {
                _solution.UnadviseSolutionEvents(_solutionEventsCookie);
                _solutionEventsCookie = 0;
            }
            _solutionEventsHandler = null;

            base.Dispose(disposing);
        }

        public async Task InitComponentsAsync()
        {
            await ThreadHelper.JoinableTaskFactory.SwitchToMainThreadAsync();
            await OutputLog.InitOutputWindowPaneAsync();
            dte = (EnvDTE.DTE) await GetServiceAsync(typeof(EnvDTE.DTE));
            JFrogXrayOptions jfrogOptions = (JFrogXrayOptions)GetDialogPage(typeof(JFrogXrayOptions));
            HttpUtils.InitClient(jfrogOptions.XrayUrl,jfrogOptions.ArtifactoryUrl, jfrogOptions.User, jfrogOptions.Password,jfrogOptions.AccessToken);
            await ScanManager.Instance.InitializeAsync(jfrogOptions.XrayUrl, jfrogOptions.ArtifactoryUrl, jfrogOptions.User, jfrogOptions.Password, jfrogOptions.AccessToken,jfrogOptions.Policy, jfrogOptions.Project, jfrogOptions.Watches);
        }

        public static EnvDTE.DTE getDTE()
        {
            return dte;
        }
        #endregion
    }

    internal class SolutionEventsHandler : IVsSolutionEvents
    {
        public int OnAfterCloseSolution(object pUnkReserved)
        {
            MainPanel mainPanel = MainPanel.GetInstance();
            if (mainPanel != null)
            {
                _ = mainPanel.CloseAsync();
            }
            return VSConstants.S_OK;
        }

        public int OnAfterLoadProject(IVsHierarchy pStubHierarchy, IVsHierarchy pRealHierarchy)
        {
            return VSConstants.S_OK;
        }

        public int OnAfterOpenProject(IVsHierarchy pHierarchy, int fAdded)
        {
            return VSConstants.S_OK;
        }

        public int OnAfterOpenSolution(object pUnkReserved, int fNewSolution)
        {
            return VSConstants.S_OK;
        }

        public int OnBeforeCloseProject(IVsHierarchy pHierarchy, int fRemoved)
        {
            return VSConstants.S_OK;
        }

        public int OnBeforeCloseSolution(object pUnkReserved)
        {
            return VSConstants.S_OK;
        }

        public int OnBeforeUnloadProject(IVsHierarchy pRealHierarchy, IVsHierarchy pStubHierarchy)
        {
            return VSConstants.S_OK;
        }

        public int OnQueryCloseProject(IVsHierarchy pHierarchy, int fRemoving, ref int pfCancel)
        {
            return VSConstants.S_OK;
        }

        public int OnQueryCloseSolution(object pUnkReserved, ref int pfCancel)
        {
            return VSConstants.S_OK;
        }

        public int OnQueryUnloadProject(IVsHierarchy pRealHierarchy, ref int pfCancel)
        {
            return VSConstants.S_OK;
        }
    }


}
