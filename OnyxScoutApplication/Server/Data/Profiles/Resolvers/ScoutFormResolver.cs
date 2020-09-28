using AutoMapper;
using OnyxScoutApplication.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnyxScoutApplication.Server.Data.Profiles.Resolvers
{
    public class ScoutFormResolver : IValueResolver<ScoutFormDto, ScoutForm, List<ScoutFormData>>
    {
        private readonly IMapper mapper;

        public ScoutFormResolver(IMapper mapper)
        {
            this.mapper = mapper;
        }

        public List<ScoutFormData> Resolve(ScoutFormDto source, ScoutForm destination, List<ScoutFormData> destMember, ResolutionContext context)
        {
            destination.Data.Clear();
            List<ScoutFormData> autonomousFields = mapper.Map<List<ScoutFormData>>(source.AutonomousData);
            autonomousFields.ForEach(i => i.Field.FieldStageType = FieldStageType.Autonomous);
            destination.Data.AddRange(autonomousFields);

            List<ScoutFormData> teleoperatedFields = mapper.Map<List<ScoutFormData>>(source.TeleoperatedData);
            teleoperatedFields.ForEach(i => i.Field.FieldStageType = FieldStageType.Teleoperated);
            destination.Data.AddRange(teleoperatedFields);

            List<ScoutFormData> endGameFields = mapper.Map<List<ScoutFormData>>(source.EndGameData);
            endGameFields.ForEach(i => i.Field.FieldStageType = FieldStageType.EndGame);
            destination.Data.AddRange(endGameFields);
            destination.Data.ForEach(x => x.Field = null);

            return destination.Data;
        }
    }
}
