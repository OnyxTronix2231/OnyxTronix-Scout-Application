using AutoMapper;
using OnyxScoutApplication.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnyxScoutApplication.Server.Data.Profiles.Resolvers
{
    public class ScoutFormFormatValueConverter : IValueResolver<ScoutFormFormatDto, ScoutFormFormat, List<Field>>
    {
        private readonly IMapper mapper;

        public ScoutFormFormatValueConverter(IMapper mapper)
        {
            this.mapper = mapper;
        }

        public List<Field> Resolve(ScoutFormFormatDto source, ScoutFormFormat destination, List<Field> destMember,
            ResolutionContext context)
        {
            destination.Fields.Clear();
            List<Field> fields = mapper.Map<List<Field>>(source.FieldsByStages.SelectMany(i => i.Value));
            destination.Fields.AddRange(fields);
            return destination.Fields;
        }
    }
}
