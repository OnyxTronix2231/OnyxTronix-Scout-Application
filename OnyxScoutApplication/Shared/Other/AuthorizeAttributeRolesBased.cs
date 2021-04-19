using Microsoft.AspNetCore.Authorization;

namespace OnyxScoutApplication.Shared.Other
{
    public class OnyxAuthorizeAttribute : AuthorizeAttribute
    {
        private Role role;

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
