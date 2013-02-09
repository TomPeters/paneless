using System.Collections.Generic;
using System.Windows.Forms;
using System.Linq;

namespace Core
{
    public class Desktop
    {
        private List<Screen> _screens;

        public void PopulateScreens()
        {
            _screens = Screen.AllScreens.ToList();
        }

        public List<Screen> Screens
        {
            get { return _screens; }
        }
    }
}
