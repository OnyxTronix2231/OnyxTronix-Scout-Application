using AutoMapper;
using OnyxScoutApplication.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnyxScoutApplication.Server.Data.Profiles.Resolvers
{
    public class ScoutFormDataValueConverter : IValueResolver<ScoutFormDataDto, ScoutFormData, string>
    {
        public string Resolve(ScoutFormDataDto source, ScoutFormData destination, string destMember, ResolutionContext context)
        {
            switch (source.Field.FieldType)
            {
                case FieldType.None:
                    break;
                case FieldType.CascadeField:
                case FieldType.Boolean:
                    destination.Value = source.BooleanValue.ToString();
                    break;
                case FieldType.OptionSelect:
                case FieldType.TextField:
                    destination.Value = source.StringValue;
                    break;
                case FieldType.Numeric:
                    destination.Value = source.NumricValue.ToString();
                    break;
                default:
                    break;
            }
            return destination.Value;
        }
    }
}
