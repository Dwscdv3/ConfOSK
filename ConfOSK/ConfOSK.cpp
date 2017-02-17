#include "stdafx.h"
#include <Windows.h>

int main(int argc, char*  argv[])
{
	HWND osk = FindWindow(NULL, L"ÆÁÄ»¼üÅÌ");
	LONG ws = GetWindowLong(osk, GWL_STYLE);

	if (strcmp(argv[1], "lock") == 0)
	{
		if (strcmp(argv[2], "on") == 0)
		{
			LONG ret = SetWindowLong(osk, GWL_STYLE, ws & ~WS_THICKFRAME);
			return ret;
		}
		else if (strcmp(argv[2], "off") == 0)
		{
			LONG ret = SetWindowLong(osk, GWL_STYLE, ws | WS_THICKFRAME);
			return ret;
		}
	}
	else if (strcmp(argv[1], "opacity") == 0)
	{
		SetLayeredWindowAttributes(osk, NULL, atoi(argv[2]), LWA_ALPHA);
	}
	return -42;
}