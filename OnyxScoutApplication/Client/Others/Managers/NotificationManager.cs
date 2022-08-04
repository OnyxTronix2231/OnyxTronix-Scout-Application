using Syncfusion.Blazor.Notifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;

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
        public class NotificationEventArgs
        {
            public string Title { get; init; }
            public string Message { get; set;}
            public NotificationType NotificationType { get; init;}
            public int Timeout { get; init;}
            public ToastButton[] ToastButtons { get; init;}
        }
        
        public EventCallback<NotificationEventArgs> OnShow;

        public async Task NotifyAsync(string title, string message, NotificationType notificationType, int timeout = 6000,
            params ToastButton[] toastButtons)
        {
            await OnShow.InvokeAsync(new NotificationEventArgs
            {
                Title = title, Message = message, NotificationType = notificationType, Timeout = timeout,
                ToastButtons = toastButtons
            });
        }
    }
}
