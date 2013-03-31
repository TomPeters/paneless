#include <Windows.h>
#include <iostream>
#include <fstream>
using namespace std;

HINSTANCE g_appInstance = NULL;

HHOOK hookCallWndProc = NULL;
HHOOK hookCallGetMsg = NULL;

static LRESULT CALLBACK WndProcCallback(int code, WPARAM wparam, LPARAM lparam) {
	if(code >= 0) {
		UINT customWindowMessage = RegisterWindowMessage("PANELESS_7F75020C-34E7-45B4-A5F8-6827F9DB7DE2");
		HWND destinationWindow = (HWND)GetProp(GetDesktopWindow(), "PANELESS_WND_40FB6774-53A9-4341-9FF7-BAD24A8205C6");
		if(customWindowMessage != 0 && destinationWindow != 0) {
			CWPSTRUCT* pCwpStruct = (CWPSTRUCT*)lparam;
			SendNotifyMessage(destinationWindow, customWindowMessage, (WPARAM)pCwpStruct->hwnd, pCwpStruct->message);
		}
	}
	return CallNextHookEx(hookCallWndProc, code, wparam, lparam);
}

static LRESULT CALLBACK GetMsgCallback(int code, WPARAM wparam, LPARAM lparam) {
	if(code >= 0) {
		UINT customWindowMessage = RegisterWindowMessage("PANELESS_7F75020C-34E7-45B4-A5F8-6827F9DB7DE2");
		HWND destinationWindow = (HWND)GetProp(GetDesktopWindow(), "PANELESS_WND_40FB6774-53A9-4341-9FF7-BAD24A8205C6");
		if(customWindowMessage != 0 && destinationWindow != 0) {
			MSG* msg = (MSG*)lparam;
			SendNotifyMessage(destinationWindow, customWindowMessage, (WPARAM)msg->hwnd, msg->message);
		}
	}
	return CallNextHookEx(hookCallWndProc, code, wparam, lparam);
} 

extern "C"
__declspec(dllexport)
BOOL SetupWndProcHook() {
	hookCallWndProc = SetWindowsHookEx(WH_CALLWNDPROC, (HOOKPROC)WndProcCallback, g_appInstance, 0);
	if(hookCallWndProc == NULL) {
		return false;
	}
	return true;
}

 // Also need to hook into GetMessage to ensure we get all messages (WndProc may not be not enough)
extern "C"
__declspec(dllexport)
BOOL SetupGetMsgHook() {
	hookCallGetMsg = SetWindowsHookEx(WH_GETMESSAGE, (HOOKPROC)GetMsgCallback, g_appInstance, 0);
	if(hookCallGetMsg == NULL) {
		return false;
	}
	return true;
}

