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
        public event Action<string, string, NotificationType, int> OnShow;
        public event Action OnHide;

        public void Notify(string title, string message, NotificationType notificationType, int timeout = 1000)
        {
            OnShow?.Invoke(title, message, notificationType, timeout);
        }

        public void HideNotification()
        {
            OnHide?.Invoke();
        }
    }
}
