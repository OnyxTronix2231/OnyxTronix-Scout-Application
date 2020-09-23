using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OnyxScoutApplication.Shared.Models.FluentValidations
{
    public class ScoutFormForamtValidator : AbstractValidator<ScoutFormFormatDto>
    {
        public ScoutFormForamtValidator()
        {
            RuleFor(x => x.Year).GreaterThanOrEqualTo(2000).LessThanOrEqualTo(2099);
            RuleForEach(x => x.AutonomousFields).SetValidator(new FieldValidator());
            RuleForEach(x => x.TeleoperatedFields).SetValidator(new FieldValidator());
            RuleForEach(x => x.EndGameFields).SetValidator(new FieldValidator());
        }
    }
}
