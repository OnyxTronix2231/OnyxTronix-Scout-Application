using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentValidation.Validators;
using OnyxScoutApplication.Shared.Models.ScoutFormFormatModels;
using OnyxScoutApplication.Shared.Models.ScoutFormModels;

namespace OnyxScoutApplication.Shared.Models.FluentValidations
{
    public class ScoutFormValidator : AbstractValidator<FormDto>
    {
        public ScoutFormValidator()
        {
            RuleFor(x => x.Year).NotEmpty().GreaterThanOrEqualTo(2000).LessThanOrEqualTo(2099);
            RuleFor(x => x.EventName).NotEmpty();
            RuleFor(x => x.MatchType).NotEmpty().When(f => f.Type != ScoutFormType.Pit);
            RuleFor(x => x.MatchNumber).NotEmpty().GreaterThanOrEqualTo(1).LessThanOrEqualTo(500).
                When(f => f.Type != ScoutFormType.Pit);
            RuleFor(x => x.SetNumber).NotEmpty().GreaterThanOrEqualTo(1).LessThanOrEqualTo(500).
                When(f => f.Type != ScoutFormType.Pit);
            RuleFor(x => x.TeamNumber).NotEmpty().GreaterThanOrEqualTo(1).LessThanOrEqualTo(9999);
            RuleFor(x => x.WriterUserName).NotEmpty();
            RuleForEach(x => x.FormDataInStages).SetValidator(new ScoutFormDataByStagesValidator());
        }
    }

    public class ScoutFormDataByStagesValidator : AbstractValidator<FormDataInStageDto>
    {
        public ScoutFormDataByStagesValidator()
        {
            RuleForEach(x => x.FormData).SetValidator(new ScoutFormDataValidator());
        }
    }
}
