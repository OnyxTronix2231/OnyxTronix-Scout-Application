using Microsoft.AspNetCore.Components;
using OnyxScoutApplication.Client.Others.Managers;
using System.Collections.Generic;
using System.Linq;

namespace OnyxScoutApplication.Client.Others.Objects
{

    public class NotificationBase : ComponentBase
    {
        [Inject]
        NotificationManager NotificationService { get; set; }

        protected string Title { get; set; }
        protected string Message { get; set; }
        protected NotificationType NotificationType { get; set; }
        protected bool IsVisible { get; set; }

        protected override void OnInitialized()
        {
            NotificationService.OnShow += OnShow;
            NotificationService.OnHide += OnHide;
        }

        public virtual void OnShow(string title, string message, NotificationType notificationType)
        {
            Title = title;
            Message = message;
            NotificationType = notificationType;
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
