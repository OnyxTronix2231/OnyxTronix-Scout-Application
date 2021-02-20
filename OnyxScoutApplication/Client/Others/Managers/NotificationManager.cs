using Syncfusion.Blazor.Notifications;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OnyxScoutApplication.Client.Others.Managers
{
    public enum NotificationType
    {
        Warning,
        Success,
        Danger,
        Info
    }

    public class NotificationManager
    {
        public event Action<string, string, NotificationType, int, ToastButton[]> OnShow;
        public event Action OnHide;

        public void Notify(string title, string message, NotificationType notificationType, int timeout = 3000,
            params ToastButton[] toastButtons)
        {
            OnShow?.Invoke(title, message, notificationType, timeout, toastButtons);
        }

        public void HideNotification()
        {
            OnHide?.Invoke();
        }
    }
}
