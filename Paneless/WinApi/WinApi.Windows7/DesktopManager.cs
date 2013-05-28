using System;
using System.Diagnostics;
using System.IO;
using System.IO.Pipes;
using Paneless.Common;
using WinApi.Interface;

namespace WinApi.Windows7
{
    public class DesktopManager : IDesktopManager
    {
        private static readonly log4net.ILog Logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private IntPtr _hook32;
        private IntPtr _hook64;
        private readonly bool _arch64;

        public DesktopManager()
        {
            _arch64 = Is64Bit();
            Logger.Debug(_arch64 ? "64Bit detected" : "32Bit detected");
        }

        public void EnumWindows(WindowsEnumProcess windowsEnumCallBack)
        {
            WinApi.EnumWindows(windowsEnumCallBack, IntPtr.Zero);
        }

        // TODO: Make this method testable
        public bool SetupWindowsHook(IntPtr shellWindowPtr)
        {
            bool result = StoreWindowPtr(shellWindowPtr);
            Process.Start(DirectoryFinder.FindDirectoryInAncestors(Identifiers.HookLauncher32));
            _hook32 = (IntPtr)Convert.ToInt32(GetIdFromPipe("PanelessHookId32-5b4f1ea2-c775-11e2-8888-47c85008ead5"));
            if (_arch64)
            {
                Process.Start(DirectoryFinder.FindDirectoryInAncestors(Identifiers.HookLauncher64));
                _hook64 = (IntPtr)Convert.ToInt32(GetIdFromPipe("PanelessHookId64-5b4f1ea2-c775-11e2-8888-47c85008ead5"));
            }
            Logger.Debug("Shell hooks registered to " + shellWindowPtr);
            return result;
        }

        private string GetIdFromPipe(string pipeName)
        {
            using (NamedPipeServerStream pipeServer = new NamedPipeServerStream(pipeName, PipeDirection.In))
            {
                pipeServer.WaitForConnection();
                using (StreamReader streamReader = new StreamReader(pipeServer))
                {
                    return streamReader.ReadToEnd();
                }
            }
        }

        public void UnregisterHooks()
        {
            UnhookWindowsHook(_hook32);
            if (_arch64)
                UnhookWindowsHook(_hook64);
            Logger.Debug("Shell hooks removed");
        }

        public bool RegisterHotKeys(IntPtr windowPtr, int keyId, uint modKeys, uint keys)
        {
            Logger.Debug("Registering hot key for " + windowPtr + " with keyId: " + keyId + " and modkeys: " + modKeys + " and keys " + keys);
            return WinApi.RegisterHotKey(windowPtr, keyId, modKeys, keys);
        }

        public bool UnregisterHotKeys(IntPtr windowPtr, int keyId)
        {
            Logger.Debug("Unregistering hot key for " + windowPtr + " with keyId" + keyId);
            return WinApi.UnregisterHotKey(windowPtr, keyId);
        }

        public int RegisterWindowMessage(string windowMessage)
        {
            Logger.Debug("Registering window message " + windowMessage);
            int result = (int)WinApi.RegisterWindowMessage(windowMessage);
            Logger.Debug("Window message registered: " + result);
            return result;
        }

        private bool UnhookWindowsHook(IntPtr hook)
        {
            Logger.Debug("Unregistering hook with HHOOK: " + hook);
            return WinApi.UnhookWindowsHookEx(hook);
        }

        private bool StoreWindowPtr(IntPtr windowPtr)
        {
            Logger.Debug("Storing pointer to " + windowPtr + " in global id " + Identifiers.PanelessWindowPropertyId);
            return WinApi.SetProp(WinApi.GetDesktopWindow(), Identifiers.PanelessWindowPropertyId, windowPtr);
        }

        private static bool Is64Bit()
        {
            if (IntPtr.Size == 8)
            {
                return true;
            }
            using (Process proc = Process.GetCurrentProcess())
            {
                bool returnValue;
                if (!WinApi.IsWow64Process(proc.Handle, out returnValue))
                {
                    return false;
                }
                return returnValue;
            }
        }
    }
}
