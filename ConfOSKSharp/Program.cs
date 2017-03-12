using System;
using System . Collections . Generic;
using System . Diagnostics;
using System . Globalization;
using System . Runtime . InteropServices;
using System . Text;

namespace ConfOSKSharp
{
    class Program
    {
        [DllImport ( "user32.dll" , SetLastError = true )]
        static extern int GetWindowLong ( IntPtr hWnd , int nIndex );
        [DllImport ( "user32.dll" )]
        static extern int SetWindowLong ( IntPtr hWnd , int nIndex , int dwNewLong );
        [DllImport ( "user32.dll" )]
        static extern bool SetLayeredWindowAttributes ( IntPtr hwnd , uint crKey , byte bAlpha , uint dwFlags );

        const int GWL_STYLE = -16;
        const int WS_THICKFRAME = 0x00040000;

        public enum StringDict
        {
            ProcessNotExist,
            NoArguments,
        }
        public static readonly Dictionary<StringDict , Dictionary<string , string>> LOCALIZATION = new Dictionary<StringDict , Dictionary<string , string>> ()
        {
            { StringDict.ProcessNotExist , new Dictionary<string , string> () {
                { "en" , "On-Screen Keyboard isn't running." },
                { "zh" , "屏幕键盘尚未运行。" },
            } },
            { StringDict.NoArguments , new Dictionary<string , string> () { // 看心情替换成有意义的帮助信息
                { "en" , "No arguments." },
                { "zh" , "没有启动参数。" },
            } },
        };
        public const string DEFAULT_LANG = "en";
        public static string CURRENT_LANG = "en";

        static int Main ( string [] args )
        {
            if ( args . Length == 0 )
            {
                OutputLocalizationText ( StringDict . NoArguments );
                return 0;
            }
            CultureInfo ci = CultureInfo . InstalledUICulture;
            CURRENT_LANG = ci . TwoLetterISOLanguageName;

            var procs = Process . GetProcessesByName ( "osk" );
            if ( procs . Length > 0 )
            {
                var proc = procs [ 0 ];
                var window = proc . MainWindowHandle;

                if ( args [ 0 ] . ToLower () == "lock" )
                {
                    var ws = GetWindowLong ( window , GWL_STYLE );
                    if ( args [ 1 ] . ToLower () == "on" )
                    {
                        SetWindowLong ( window , GWL_STYLE , ws & ~WS_THICKFRAME );
                        return 0;
                    }
                    else if ( args [ 1 ] . ToLower () == "off" )
                    {
                        SetWindowLong ( window , GWL_STYLE , ws | WS_THICKFRAME );
                        return 0;
                    }
                }
                else if ( args [ 0 ] . ToLower () == "opacity" )
                {
                    SetLayeredWindowAttributes ( window , 0 , byte . Parse ( args [ 1 ] ) , 2 );
                    return 0;
                }
            }
            else
            {
                OutputLocalizationText ( StringDict . ProcessNotExist );
            }

            return -1;
        }

        public static void OutputLocalizationText ( StringDict text , bool newLine = true )
        {
            if ( LOCALIZATION [ text ] . ContainsKey ( CURRENT_LANG ) )
            {
                Console . Write ( LOCALIZATION [ text ] [ CURRENT_LANG ] );
            }
            else
            {
                Console . Write ( LOCALIZATION [ text ] [ DEFAULT_LANG ] );
            }
            if ( newLine )
            {
                Console . WriteLine ();
            }
        }
    }
}
