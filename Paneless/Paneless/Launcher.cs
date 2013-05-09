﻿using System.Windows.Forms;
using Paneless.Core;
using Paneless.Layouts;
using WinApi.Interface;
using WinApi.Windows7;

[assembly: log4net.Config.XmlConfigurator(Watch = true)]

namespace Paneless.Launcher
{
    public static class Launcher
    {
        private static readonly log4net.ILog Logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        static void Main(string[] args)
        {
            Logger.Info("Paneless is starting up");
            IWindowManager windowManager = new WindowManager();
            IDesktopManager desktopManager = new DesktopManager();
            IDesktop desktop = new Desktop(desktopManager, windowManager, new LayoutFactory());
            PanelessApplicationContext applicationContext = new PanelessApplicationContext(new ContextProvider(desktop));
            Application.Run(applicationContext);
            Logger.Info("Paneless is about to close");
        }
    }
}
