using AutoMapper;
using OnyxScoutApplication.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnyxScoutApplication.Server.Data.Profiles.Resolvers
{
    public class ScoutFormValueConverter : IValueResolver<ScoutFormDto, ScoutForm, List<ScoutFormData>>
    {
        private readonly IMapper mapper;

        public ScoutFormValueConverter(IMapper mapper)
        {
            this.mapper = mapper;
        }

        public List<ScoutFormData> Resolve(ScoutFormDto source, ScoutForm destination, List<ScoutFormData> destMember,
            ResolutionContext context)
        {
            destination.Data.Clear();
            List<ScoutFormData> data = mapper.Map<List<ScoutFormData>>(source.DataByStages.SelectMany(i => i.Value));
            destination.Data.AddRange(data);
            DeleteFieldRefRecursively(destination.Data);

            return destination.Data;
        }

        private static void DeleteFieldRefRecursively(List<ScoutFormData> data)
        {
            data.ForEach(i => i.Field = null);
            data.ForEach(i => DeleteFieldRefRecursively(i.CascadeData));
        }
    }
}
