using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Interop;

namespace Image_Tiles
{
    public enum Resolutions { WVGA, WXGA, HD720p };

    public static class ResolutionHelper
    {
        static int? ScaleFactor;

        static ResolutionHelper()
        {
            object scaleFactorValue = GetPropertyValue(App.Current.Host.Content, "ScaleFactor");
            if (scaleFactorValue != null)
            {
                ScaleFactor = Convert.ToInt32(scaleFactorValue);
            }
        }

        private static bool IsWvga
        {
            get
            {
                return ScaleFactor.HasValue && ScaleFactor.Value == 100;
            }
        }

        private static bool IsWxga
        {
            get
            {
                return ScaleFactor.HasValue && ScaleFactor.Value == 160;
            }
        }

        private static bool Is720p
        {
            get
            {
                return ScaleFactor.HasValue && ScaleFactor.Value == 150;
            }
        }

        public static Resolutions CurrentResolution
        {
            get
            {
                if (IsWxga) return Resolutions.WXGA;
                else if (Is720p) return Resolutions.HD720p;
                return Resolutions.WVGA;
            }
        }

        private static object GetPropertyValue(object instance, string name)
        {
            try
            {
                return instance.GetType().GetProperty(name).GetValue(instance, null);
            }
            catch
            {
                // Exception will occur when app is running on WP7 devices as "ScaleFactor" property doesn't exist. Return null in that case. 
                return null;
            }
        }


    }
}
