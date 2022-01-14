using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using OnyxScoutApplication.Shared.Other;

namespace OnyxScoutApplication.Client.Others.Extensions
{
    public class OnyxAuthorizeView : AuthorizeView
    {
        private Role role;

        [Parameter]
        public Role Role
        {
            get => role;
            set
            {
                Roles = value.ToString();
                role = value;
            }
        }
    }
}
