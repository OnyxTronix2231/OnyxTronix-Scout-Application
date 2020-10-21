using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OnyxScoutApplication.Shared.Models.FluentValidations
{
    public class ScoutFormValidator : AbstractValidator<ScoutFormDto>
    {
        public ScoutFormValidator()
        {
            RuleFor(x => x.Year).NotEmpty().GreaterThanOrEqualTo(2000).LessThanOrEqualTo(2099);
            RuleFor(x => x.MatchName).NotEmpty();
            RuleFor(x => x.TeamNumber).NotEmpty().GreaterThanOrEqualTo(1).LessThanOrEqualTo(9999);
            RuleFor(x => x.WriterUserName).NotEmpty();
            RuleForEach(x => x.AutonomousData).SetValidator(new ScoutFormDataValidator());
            RuleForEach(x => x.TeleoperatedData).SetValidator(new ScoutFormDataValidator());
            RuleForEach(x => x.EndGameData).SetValidator(new ScoutFormDataValidator());
        }
    }
}
