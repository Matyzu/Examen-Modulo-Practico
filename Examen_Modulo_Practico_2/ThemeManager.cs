using System;
using System.Linq;
using System.Windows;

namespace Examen_Modulo_Practico_2
{
    public static class ThemeManager
    {
        public static void ApplyTheme(string themeName)
        {
            if (Application.Current == null) return;
            try
            {
                var dicts = Application.Current.Resources.MergedDictionaries;
                // remove any existing theme dictionaries (look for Themes/ in Uri)
                for (int i = dicts.Count - 1; i >= 0; i--)
                {
                    try
                    {
                        var src = dicts[i].Source?.OriginalString ?? string.Empty;
                        if (src.IndexOf("/Themes/", StringComparison.OrdinalIgnoreCase) >= 0 || src.IndexOf("Themes/", StringComparison.OrdinalIgnoreCase) >= 0)
                            dicts.RemoveAt(i);
                    }
                    catch { }
                }

                // prefer pack URI to ensure resource is found when running as exe
                string packUri = $"pack://application:,,,/Themes/{themeName}Theme.xaml";
                var rd = new ResourceDictionary();
                rd.Source = new Uri(packUri, UriKind.Absolute);
                dicts.Add(rd);
            }
            catch
            {
                // ignore but avoid throwing on startup
            }
        }
    }
}
