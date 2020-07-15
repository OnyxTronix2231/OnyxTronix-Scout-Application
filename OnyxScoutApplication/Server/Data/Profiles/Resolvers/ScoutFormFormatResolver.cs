using AutoMapper;
using OnyxScoutApplication.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnyxScoutApplication.Server.Data.Profiles.Resolvers
{
    public class ScoutFormFormatResolver : IValueResolver<ScoutFormFormatDto, ScoutFormFormat, List<Field>>
    {
        private readonly IMapper mapper;

        public ScoutFormFormatResolver(IMapper mapper)
        {
            this.mapper = mapper;
        }

        public List<Field> Resolve(ScoutFormFormatDto source, ScoutFormFormat destination, List<Field> destMember, ResolutionContext context)
        {
            destination.Fields.Clear();
            List<Field> autonomousFields = mapper.Map<List<Field>>(source.AutonomousFields);
            autonomousFields.ForEach(i => i.FieldStageType = FieldStageType.Autonomous);
            destination.Fields.AddRange(autonomousFields);

            List<Field> teleoperatedFields = mapper.Map<List<Field>>(source.TeleoperatedFields);
            teleoperatedFields.ForEach(i => i.FieldStageType = FieldStageType.Teleoperated);
            destination.Fields.AddRange(teleoperatedFields);

            List<Field>endGameFields = mapper.Map<List<Field>>(source.EndGameFields);
            endGameFields.ForEach(i => i.FieldStageType = FieldStageType.EndGame);
            destination.Fields.AddRange(endGameFields);
            return destination.Fields;
        }
    }
}
