using FluentValidation;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using OnyxScoutApplication.Shared.Models.ScoutFormFormatModels;
using OnyxScoutApplication.Shared.Models.ScoutFormModels;

namespace OnyxScoutApplication.Shared.Models.FluentValidations
{
    public class ScoutFormDataValidator : AbstractValidator<FormDataDto>
    {
        public ScoutFormDataValidator()
        {
            RuleFor(x => x.NumericValue).Must(BeNumberBetween)
                .WithMessage(model => 
                    "Value must be a number between " + model.Field.MinValue + " and " + model.Field.MaxValue)
                .When(c => c.Field.FieldType is FieldType.Integer or FieldType.Timer);
            RuleFor(x => x.NumericValue).NotEmpty().WithMessage((model, s) => $"Field {model.Field.Name} required")
                .When(m => m.Field.Required && m.Field.FieldType is FieldType.Integer or FieldType.Timer);
            
            RuleFor(x => x.StringValue).NotEmpty().WithMessage((model, s) => $"Field {model.Field.Name} required")
                .When(m => m.Field.Required && m.Field.FieldType == FieldType.TextField);
            RuleFor(x => x.SelectedOptions).NotEmpty().WithMessage((model, s) => $"Field {model.Field.Name} required")
                .When(m => m.Field.Required &&
                           (m.Field.FieldType is FieldType.MultipleChoice or FieldType.OptionSelect));
            
            RuleForEach(x => x.CascadeData).SetValidator(this)
                .When(x => x.Field.FieldType == FieldType.CascadeField && x.BooleanValue);
        }

        private static bool BeNumberBetween(FormDataDto model, float? value)
        {
            if (value == null)
            {
                return true;
            }

            float v = (float) value;
            return v >= model.Field.MinValue && v <= model.Field.MaxValue;
        }
    }
}
