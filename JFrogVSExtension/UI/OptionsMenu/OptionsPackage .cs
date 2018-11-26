using System;
using System.Runtime.InteropServices;
using Microsoft.VisualStudio.Shell;

namespace JFrogVSExtension.OptionsMenu
{
    [PackageRegistration(UseManagedResourcesOnly = true)]
    [InstalledProductRegistration("#110", "#112", "1.0", IconResourceID = 400)]
    [ProvideOptionPage(typeof(JFrogXrayOptions), "JFrog", "JFrog Xray", 0, 0, true)]
    [Guid("8bb519a5-4864-43b0-8684-e2f2f723100c")]
    public sealed class OptionsPackage : Package
    {
        protected override void Initialize()
        {
             base.Initialize();
        }
    }

}
