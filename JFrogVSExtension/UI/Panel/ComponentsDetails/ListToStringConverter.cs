using JFrogVSExtension.Xray;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows.Data;

namespace JFrogVSExtension.ComponentsDetails
{
    // This class is required for the Grid.Resources inside the details.xaml
    // This is a BindListToTextBlock class.
    [ValueConversion(typeof(List<License>), typeof(string))]
    public class ListToStringConverter : IValueConverter
    {

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (targetType != typeof(string))
                throw new InvalidOperationException("The target must be a String");

            List<License> copyOfList = ((List<License>)value);
            if (copyOfList != null && copyOfList.Count > 0)
            {
                String licenseNames = "";
                foreach (License license in copyOfList)
                {
                    licenseNames += license.Name + " ";
                }
                return licenseNames;
            }
            return "Unknown";
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
