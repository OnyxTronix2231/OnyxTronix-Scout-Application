using Microsoft.AspNetCore.Components;
using OnyxScoutApplication.Client.Others.Managers;
using Syncfusion.Blazor.Notifications;
using System.Collections.Generic;
using System.Linq;

namespace OnyxScoutApplication.Client.Others.Objects
{

    public class NotificationBase : ComponentBase
    {
        [Inject]
        NotificationManager NotificationManager { get; set; }

        protected string Title { get; set; }
        protected string Message { get; set; }
        protected int Timeout { get; set; }
        protected ToastButton[] ToastButtons { get; set; }
        protected NotificationType NotificationType { get; set; }
        protected bool IsVisible { get; set; }

        protected override void OnInitialized()
        {
            NotificationManager.OnShow += OnShow;
            NotificationManager.OnHide += OnHide;
        }

        public virtual void OnShow(string title, string message, NotificationType notificationType, int timeout, params ToastButton[] toastButtons)
        {
            Title = title;
            Message = message;
            NotificationType = notificationType;
            Timeout = timeout;
            ToastButtons = toastButtons;
            IsVisible = true;
            StateHasChanged();
        }

        public virtual void OnHide()
        {
            IsVisible = false;
            StateHasChanged();
        }
    }
}
