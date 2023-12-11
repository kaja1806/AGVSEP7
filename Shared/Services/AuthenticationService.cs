namespace Shared.Services;

public class AuthenticationService
{
    private readonly EventService _eventService;

    public AuthenticationService(EventService eventService)
    {
        _eventService = eventService;
    }

    public string UserName { get; private set; }
    public bool IsLoggedIn { get; private set; }

    // Add an event for notifying when the authentication state changes
    public event Action OnAuthenticationStateChanged;

    private void NotifyAuthenticationStateChanged()
    {
        OnAuthenticationStateChanged?.Invoke();
    }

    public void SetAuthenticatedUser(string userName)
    {
        UserName = userName;
        IsLoggedIn = true;

        // Notify components that the authentication state has changed
        NotifyAuthenticationStateChanged();

        // Trigger the login event using the EventService
        _eventService.TriggerLoginEvent();
    }

    public void Logout()
    {
        UserName = string.Empty;
        IsLoggedIn = false;

        // Notify components that the authentication state has changed
        NotifyAuthenticationStateChanged();

        // Trigger the logout event using the EventService
        _eventService.TriggerLogoutEvent();
    }
}