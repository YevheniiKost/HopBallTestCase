using Core.UI;

namespace Events
{
    public class RegisterWindowEvent { public ScreenType ScreenType; public IView View; }
    public class OnBallFallEvent { }
    public class OnGetCoinEvent { };
    public class OnGameStarted { };
    public class OnGameEnded { public float Height; }
}
