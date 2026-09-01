namespace CarRent.Web.Services;

public class NotificationService
{
    public event Action<string, string>? OnShowMessage;

    public void ShowError(string message)
    {
        OnShowMessage?.Invoke(message, "danger");
    }

    public void ShowSuccess(string message)
    {
        OnShowMessage?.Invoke(message, "success");
    }

    public void ShowWarning(string message)
    {
        OnShowMessage?.Invoke(message, "warning");
    }
}