using Avalonia.Controls;
using System;

namespace PostmessageReceiver.Views;

public partial class MainWindow : Window
{
    private readonly ViewState viewState = ViewState.Instance;
    public MainWindow()
    {
        InitializeComponent();
        // AddWndProcHookCallback requires 11.1.0-beta1
        // In Nuget, select the Include prerelease checkbox.
        // https://learn.microsoft.com/en-us/nuget/create-packages/prerelease-packages
        Win32Properties.AddWndProcHookCallback(this, WndProc);

        // get window handle
        // https://docs.avaloniaui.net/docs/stay-up-to-date/upgrade-from-0.10
        viewState.Handle = TryGetPlatformHandle()?.Handle;
    }

    /// <summary>
    /// CustomWndProcHookCallback delegate.
    /// </summary>
    /// <param name="hWnd"></param>
    /// <param name="msg"></param>
    /// <param name="wParam"></param>
    /// <param name="lParam"></param>
    /// <param name="handled"></param>
    /// <returns></returns>
    private nint WndProc(nint hWnd, uint msg, nint wParam, nint lParam, ref bool handled)
    {
        // WM_APP: 0x8000..0xBFFF
        if (0x8000 <= msg && msg <= 0xBFFF)
        {
            if (viewState.VM != null)
                viewState.VM.Greeting =
                    $"message: {msg:X4} wParam: {wParam} lParam: {lParam}";
        }
        return IntPtr.Zero;
    }
}
