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
    public class FieldToFieldDtoConvertor : IMappingAction<Field, FieldDto>
    {
        public void Process(Field source, FieldDto destination, ResolutionContext context)
        {
            switch (source.FieldType)
            {
                case FieldType.CascadeField:
                    destination.CascadeFields = context.Mapper.Map<List<FieldDto>>(source.CascadeFields);
                    goto case FieldType.Boolean;
                case FieldType.Boolean:
                    destination.BoolDefaultValue = bool.Parse(source.DefaultValue);
                    break;
                case FieldType.TextField:
                    destination.TextDefaultValue = source.DefaultValue;
                    break;
                case FieldType.Integer:
                    if (!string.IsNullOrWhiteSpace(source.DefaultValue))
                    {
                        destination.NumericDefaultValue = int.Parse(source.DefaultValue);
                    }
                    break;
                case FieldType.OptionSelect:
                case FieldType.MultipleChoice:
                   destination.DefaultSelectedOptions = source.DefaultValue?.Split(";")
                        .Select(i => context.Mapper.Map<OptionDto>(
                            source.Options.FirstOrDefault(o => o.Name == i))).Where(i => i is not null).ToList();
                    break;
                case FieldType.Timer:
                    if (!string.IsNullOrWhiteSpace(source.DefaultValue))
                    {
                        destination.NumericDefaultValue = float.Parse(source.DefaultValue);
                    }
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}
