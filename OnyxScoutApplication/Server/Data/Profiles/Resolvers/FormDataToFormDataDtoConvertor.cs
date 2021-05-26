using AutoMapper;
using OnyxScoutApplication.Shared.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using OnyxScoutApplication.Shared.Models.ScoutFormFormatModels;
using OnyxScoutApplication.Shared.Models.ScoutFormModels;
using OnyxScoutApplication.Shared.Other;

namespace OnyxScoutApplication.Server.Data.Profiles.Resolvers
{
    public class FormDataToFormDataDtoConvertor : IMappingAction<FormData, FormDataDto>
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
                case FieldType.TextField:
                    destination.StringValue = source.Value;
                    break;
                case FieldType.Integer:
                    if (!string.IsNullOrWhiteSpace(source.Value))
                    {
                        destination.NumericValue = int.Parse(source.Value);
                    }
                    break;
                case FieldType.OptionSelect:
                case FieldType.MultipleChoice:
                   destination.SelectedOptions = source.Value?.Split(";")
                        .Select(i => context.Mapper.Map<OptionDto>(
                            source.Field.Options.FirstOrDefault(o => o.Name == i))).Where(i => i is not null).ToList();
                    break;
                case FieldType.Timer:
                    if (!string.IsNullOrWhiteSpace(source.Value))
                    {
                        destination.NumericValue = float.Parse(source.Value);
                    }
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}
