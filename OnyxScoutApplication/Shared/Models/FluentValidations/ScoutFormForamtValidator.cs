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
            RuleFor(x => x.AutonomousFields).Must(i => UniqueName(i)).WithMessage("Fields name must be unique");
            RuleFor(x => x.TeleoperatedFields).Must(i => UniqueName(i)).WithMessage("Fields name must be unique");
            RuleFor(x => x.EndGameFields).Must(i => UniqueName(i)).WithMessage("Fields name must be unique");
        }

        private bool UniqueName(List<FieldDto> fields)
        {
            return !ConcatAllFields(fields).GroupBy(f => f.Name).Where(x => x.Skip(1).Any()).Any();
        }

        private List<FieldDto> ConcatAllFields(List<FieldDto> fields)
        {
            if(fields.Count == 0)
            {
                return fields;
            }
            return fields.Concat(ConcatAllFields(fields.SelectMany(i => i.CascadeFields).ToList())).ToList();
        }
    }
}
  