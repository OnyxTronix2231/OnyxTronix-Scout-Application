using AutoMapper;
using OnyxScoutApplication.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OnyxScoutApplication.Shared.Models.ScoutFormFormatModels;
using OnyxScoutApplication.Shared.Models.ScoutFormModels;

namespace OnyxScoutApplication.Server.Data.Profiles.Resolvers
{
    public class FieldDtoToFieldValueConverter : IValueResolver<FieldDto, Field, string>
    {
        public string Resolve(FieldDto source, Field destination, string destMember,
            ResolutionContext context)
        {
            switch (source.FieldType)
            {
                case FieldType.CascadeField:
                case FieldType.Boolean:
                    destination.DefaultValue = source.BoolDefaultValue.ToString();
                    break;
                case FieldType.TextField:
                    destination.DefaultValue = source.TextDefaultValue;
                    break;
                case FieldType.Integer:
                    destination.DefaultValue = source.NumericDefaultValue?.ToString();
                    break;
                case FieldType.OptionSelect:
                case FieldType.MultipleChoice:
                    if (source.DefaultSelectedOptions != null)
                    {
                        destination.DefaultValue = string.Join(";", source.DefaultSelectedOptions.Select(i => i.Name));
                    }
                    break;
                case FieldType.Timer:
                    destination.DefaultValue = source.NumericDefaultValue?.ToString();
                    break;
                case FieldType.None:
                    throw new ArgumentOutOfRangeException();
                default:
                    throw new ArgumentOutOfRangeException();
            }

            return destination.DefaultValue;
        }
    }
}
