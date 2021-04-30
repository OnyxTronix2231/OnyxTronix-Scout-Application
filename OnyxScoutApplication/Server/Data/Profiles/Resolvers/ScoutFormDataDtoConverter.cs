using AutoMapper;
using OnyxScoutApplication.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OnyxScoutApplication.Shared.Models.ScoutFormFormatModels;
using OnyxScoutApplication.Shared.Models.ScoutFormModels;
using OnyxScoutApplication.Shared.Other;

namespace OnyxScoutApplication.Server.Data.Profiles.Resolvers
{
    public class ScoutFormDataDtoValueParser : IMappingAction<FormData, FormDataDto>
    {
        public void Process(FormData source, FormDataDto destination, ResolutionContext context)
        {
            switch (source.Field.FieldType)
            {
                case FieldType.CascadeField:
                    destination.CascadeData = context.Mapper.Map<List<FormDataDto>>(source.CascadeData);
                    goto case FieldType.Boolean;
                case FieldType.Boolean:
                    destination.BooleanValue = bool.Parse(source.Value);
                    break;
                case FieldType.OptionSelect:
                case FieldType.TextField:
                    destination.StringValue = source.Value;
                    break;
                case FieldType.Numeric:
                    if (!string.IsNullOrWhiteSpace(source.Value))
                    {
                        destination.NumericValue = int.Parse(source.Value);
                    }
                    break;
                case FieldType.MultipleChoice:
                    destination.SelectedOptions = source.Value?.Split(';').ToList();
                    break;
                case FieldType.None:
                    throw new ArgumentOutOfRangeException();
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}
