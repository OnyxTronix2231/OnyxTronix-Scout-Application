using AutoMapper;
using OnyxScoutApplication.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnyxScoutApplication.Server.Data.Profiles.Resolvers
{
    public class ScoutFormDataDtoResolver<T> : IValueResolver<ScoutFormData, ScoutFormDataDto, T>
    {
        public T Resolve(ScoutFormData source, ScoutFormDataDto destination, T destMember, ResolutionContext context)
        {
            switch (source.Field.FieldType)
            {
                case FieldType.Boolean:
                    destination.BooleanValue = bool.Parse(source.Value);
                    return (T)(object) destination.BooleanValue;
                case FieldType.TextField:
                    destination.StringValue = source.Value;
                    return (T)(object) destination.StringValue;
                case FieldType.Numeric:
                    destination.NumricValue = int.Parse(source.Value);
                    return (T)(object) destination.NumricValue;
                default:
                    return default;
            }
        }
    }
}
