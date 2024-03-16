using PostmessageReceiver.ViewModels;
using System;
using System.Runtime.InteropServices;

namespace PostmessageReceiver
{
    public sealed class ViewState
    {
        private static Lazy<ViewState> _instance = new();
        public static ViewState Instance => _instance.Value;

        public IntPtr? Handle;
        public MainViewModel? VM;

        [DllImport("user32.dll")]
        public static extern int PostMessage(IntPtr hWnd, uint msg, uint wParam, int lParam);

        /// <summary>
        /// Post to myself
        /// </summary>
        /// <param name="hWnd"></param>
        /// <param name="msg"></param>
        /// <param name="wParam"></param>
        /// <param name="lParam"></param>
        public void Post2Myself(IntPtr hWnd, uint msg, uint wParam, int lParam)
        {
            PostMessage(hWnd, msg, wParam, lParam);
        }
    }
}
