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
        public event Action<string, string, NotificationType> OnShow;
        public event Action OnHide;

        public void Notify(string title, string message, NotificationType type)
        {
            OnShow?.Invoke(title, message, type);
        }

        public void HideNotification()
        {
            OnHide?.Invoke();
        }
    }
}
