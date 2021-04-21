using System;
using System.Collections.Generic;
using System.Linq;
using FluentValidation;
using OnyxScoutApplication.Shared.Other;

namespace OnyxScoutApplication.Shared.Models.FluentValidations
{
    public class CustomMatchValidator : AbstractValidator<CustomMatchDto>
    {
        public CustomMatchValidator()
        {
            RuleFor(i => i.Date).NotEmpty().GreaterThanOrEqualTo(i => i.Event.StartDate)
                .LessThanOrEqualTo(i => new DateTime(i.Event.Year + 1, 1, 1))
                .WithMessage("Invalid date for this match, make sure you are in the currect year!");

            RuleFor(i => i.Alliances).NotEmpty();
            RuleFor(i => i.Alliances.Blue).Must(BeFull).WithMessage("Missing team/s in blue alliance");
            RuleFor(i => i.Alliances.Red).Must(BeFull).WithMessage("Missing team/s in red alliance");
        }

        private bool BeFull(CustomAllianceDto customAlliance)
        {
            if (customAlliance.Teams.Count != 3)
            {
                return false;
            }

            foreach (var team in customAlliance.Teams)
            {
                if (!team.TeamNumber.IsValidTeamNumber())
                {
                    return false;
                }

                if (string.IsNullOrWhiteSpace(team.Nickname))
                {
                    return false;
                }
            }

            return true;
        }
    }
}
