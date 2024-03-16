using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using System.Windows.Input;

namespace PostmessageReceiver.ViewModels;

public class MainViewModel : ViewModelBase
{
    [Reactive]
    public string Greeting { get; set; } = "Welcome to Avalonia!";

    public ICommand ClearGreeting { get; }
    public ICommand Post { get; }

    private readonly ViewState viewState = ViewState.Instance;
    public MainViewModel()
    {
        var i = (uint)0;
        ClearGreeting = ReactiveCommand.Create(() =>
        {
            Greeting = string.Empty;
            i = 0;
        });

        var WM_APP = (uint)0x8000;

        // In reality, whoever selects the window handles the PostMessage.
        Post = ReactiveCommand.Create(() =>
        {
            if (viewState.Handle != null)
                viewState.Post2Myself((nint)viewState.Handle,
                    msg: WM_APP + i, wParam: i, lParam: (int)(i * i));
            i++;
        });

        // Postmessage is received by Window.
        // Hold instance of this ViewModel for updating the display.
        // (This is not common.)
        viewState.VM = this;
    }
}
