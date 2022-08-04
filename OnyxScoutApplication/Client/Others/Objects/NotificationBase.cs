using System;
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
        // ReSharper disable once UnusedAutoPropertyAccessor.Local
        private NotificationManager NotificationManager { get; set; }

        protected string Title { get; private set; }
        protected string Message { get; private set; }
        protected int Timeout { get; private set; }
        protected ToastButton[] ToastButtons { get; private set; }
        protected NotificationType NotificationType { get; private set; }
        //protected bool IsVisible { get; set; }

        protected override void OnInitialized()
        {
            NotificationManager.OnShow =
                new EventCallback<NotificationManager.NotificationEventArgs>(this, (Action<NotificationManager.NotificationEventArgs>) OnShow);
        }

        protected virtual void OnShow(NotificationManager.NotificationEventArgs obj)
        {
            Title = obj.Title;
            Message = obj.Message;
            NotificationType = obj.NotificationType;
            Timeout = obj.Timeout;
            ToastButtons = obj.ToastButtons;
            StateHasChanged();
        }
    }
}
