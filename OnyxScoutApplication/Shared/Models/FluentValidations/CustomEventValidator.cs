using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OnyxScoutApplication.Shared.Models.FluentValidations
{
    public class CustomEventValidator : AbstractValidator<CustomEventDto>
    {
        public CustomEventValidator()
        {
            RuleFor(x => x.Year).NotEmpty().ValidGameYear();
            RuleFor(x => x.Country).NotEmpty();
            RuleFor(x => x.Name).NotEmpty();
            
            RuleFor(x => x.StartDate).GreaterThanOrEqualTo(i => new DateTime(i.Year - 1, 1,1))
                .LessThanOrEqualTo(i => new DateTime(i.Year + 1, 1,1))
                .WithMessage("Invalid date for this match, make sure you are in the currect year!");
            
            RuleFor(x => x.Matches).NotEmpty();
            
            RuleFor(x => x.Matches).Must(HaveAllLevels).
                WithMessage("Please make sure your are not missing any match stages");
            
            RuleForEach(x => x.Matches).SetValidator(new CustomMatchValidator());
        }

        private static bool HaveAllLevels(List<CustomMatchDto> customMatches)
        {
            return customMatches.GroupBy(i => i.Level).Count() == 4;
        }
    }
}
