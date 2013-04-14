using System.Collections.Generic;

namespace WinApi.Windows7.Constants
{
    internal static class WindowStatusLookup
    {
        public static readonly IDictionary<int, string> StatusMap;

        static WindowStatusLookup()
        {
            StatusMap = new Dictionary<int, string>
                {
                    {0, "SW_HIDE"},
                    {1, "SW_SHOWNORMAL"},
                    {2, "SW_SHOWMINIMIZED"},
                    {3, "SW_SHOWMAXIMIZED"},
                    {4, "SW_SHOWNOACTIVATE"},
                    {5, "SW_SHOW"},
                    {6, "SW_MINIMIZE"},
                    {7, "SW_SHOWMINNOACTIVE"},
                    {8, "SW_SHOWNA"},
                    {9, "SW_RESTORE"}
                };
        }
    }
}
