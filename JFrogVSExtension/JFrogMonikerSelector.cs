using JFrogVSExtension.Xray;
using Microsoft.VisualStudio.Imaging.Interop;
using System;

namespace JFrogVSExtension
{
    class JFrogMonikerSelector
    {
        private static readonly Guid JFrogMonikerGuid = new Guid("b2d9eecf-c9d2-4a0b-88fb-7a4715a2d763");
        public static ImageMoniker SeverityToMoniker(Severity severity)
        {
            int id = GetSeverityID(severity);
            return new ImageMoniker { Guid = JFrogMonikerGuid, Id = id };
        }

        public static int GetSeverityID(Severity severity)
        {
            int id = 0;
            switch (severity)
            {
                case Severity.High:
                    {
                        id = 10;
                        break;
                    }
                case Severity.Medium:
                    {
                        id = 50;
                        break;
                    }
                case Severity.Low:
                    {
                        id = 60;
                        break;
                    }
                case Severity.Normal:
                    {
                        id = 70;
                        break;
                    }
                case Severity.Unknown:
                    {
                        id = 90;
                        break;
                    }
            }
            return id;
        }
    }
}
