using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OnyxScoutApplication.Shared.Models.FluentValidations
{
    public class FieldValidator : AbstractValidator<FieldDto>
    {
        public FieldValidator()
        {
            RuleFor(x => x.Name).Must(s => !string.IsNullOrWhiteSpace(s)).WithMessage("Please write a valid field name");
            RuleFor(x => x.NumricDefaultValue).Must((model, s) => BeNumberBetween(model, s)).
                WithMessage((model, s) => "Default value must be a number between " + model.MinValue + " and " + model.MaxValue).When(c => c.FieldType == FieldType.Numeric);
            RuleFor(x => x.Options).Must((v) => v.Count >= 2).
               WithMessage("At least two options are required").When(c => c.FieldType == FieldType.OptionSelect);
            RuleFor(x => x.Options).Must((v) => !v.Any(i => string.IsNullOrEmpty(i))).
               WithMessage("Make sure all options are not empty").When(c => c.FieldType == FieldType.OptionSelect);
        }

        private bool BeNumberBetween(FieldDto model, int? value)
        {
            if (value == null)
            {
                return true;
            }

            int v = (int)value;
            return v >= model.MinValue && v <= model.MaxValue;
        }
    }
}
