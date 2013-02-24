#include <Windows.h>

HOOKPROC wrappedHook;

LRESULT WINAPI wrapper(int code, WPARAM wparam, LPARAM lparam) { return wrappedHook(code, wparam, lparam);}

BOOL APIENTRY DllMain(HMODULE hModule, DWORD  fdwReason, LPVOID lpReserved)
{
	return TRUE;
}

extern "C"
__declspec(dllexport)
HOOKPROC WINAPI HookWrapper(HOOKPROC process)
{
	wrappedHook = process;
	return wrapper;
}

extern "C"
__declspec(dllexport)
BOOL WINAPI Free()
{
	wrappedHook = NULL;
	return TRUE;
}