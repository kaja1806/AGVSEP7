namespace Shared.Services;

public class EventService
{
    public event Action OnLogin;
    public event Action OnLogout;

    public void TriggerLoginEvent()
    {
        OnLogin?.Invoke();
    }

    public void TriggerLogoutEvent()
    {
        OnLogout?.Invoke();
    }
}