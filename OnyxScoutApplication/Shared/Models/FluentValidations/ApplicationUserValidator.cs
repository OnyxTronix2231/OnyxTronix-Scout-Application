using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OnyxScoutApplication.Shared.Models.FluentValidations
{
    public class ApplicationUserValidator : AbstractValidator<ApplicationUserDto>
    {
        public ApplicationUserValidator()
        {
            RuleFor(x => x.UserRoles).Must(r => r.Any()).WithMessage("User must have at least one role");
            RuleFor(x => x.UserRoles).Must(NotContainsDuplicates).WithMessage("Duplicates roles, please make sure this user doesn't have the same role more then once");
        }

        private static bool NotContainsDuplicates(List<ApplicationUserRoleDto> roles)
        {
            return roles.All(role => roles.Count(i => i.Role.Id == role.Role.Id) <= 1);
        }
    }
}
