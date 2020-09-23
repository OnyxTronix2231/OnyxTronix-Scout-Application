using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OnyxScoutApplication.Shared.Models.FluentValidations
{
    public class ScoutFormDataValidator : AbstractValidator<ScoutFormDataDto>
    {
        public ScoutFormDataValidator()
        {
            RuleFor(x => x.NumricValue).Must((model, s) => BeNumberBetween(model, s)).
                WithMessage((model, s) => "Value must be a number between " + model.Field.MinValue + " and " + model.Field.MaxValue).When(c => c.Field.FieldType == FieldType.Numeric);
            RuleFor(x => x.NumricValue).NotEmpty().WithMessage((model, s) => "Field required").When(m => m.Field.Required && m.Field.FieldType == FieldType.Numeric);
            RuleFor(x => x.StringValue).NotEmpty().WithMessage((model, s) => "Field required").When(m => m.Field.Required && m.Field.FieldType == FieldType.TextField);
        }

        private bool BeNumberBetween(ScoutFormDataDto model, int? value)
        {
            if (value == null)
            {
                return true;
            }

            int v = (int)value;
            return v >= model.Field.MinValue && v <= model.Field.MaxValue;
        }
    }
}
