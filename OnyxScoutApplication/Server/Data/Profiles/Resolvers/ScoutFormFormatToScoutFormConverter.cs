using AutoMapper;
using OnyxScoutApplication.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Query.Internal;
using OnyxScoutApplication.Shared.Models.ScoutFormFormatModels;
using OnyxScoutApplication.Shared.Models.ScoutFormModels;
using Syncfusion.ExcelExport;
using OnyxScoutApplication.Shared.Other;

namespace OnyxScoutApplication.Server.Data.Profiles.Resolvers
{
    public class ScoutFormFormatToScoutFormConverter : ITypeConverter<ScoutFormFormatDto, ScoutFormDto>
    {
        private readonly IMapper mapper;

        public ScoutFormFormatToScoutFormConverter(IMapper mapper)
        {
            this.mapper = mapper;
        }

        public ScoutFormDto Convert(ScoutFormFormatDto source, ScoutFormDto destination, ResolutionContext context)
        {
            destination = new ScoutFormDto
            {
                FormDataInStages = source.FieldsInStages.Select(i => mapper.Map<FormDataInStageDto>(i)).ToList()
            };

            return destination;
        }

        public class FieldToScoutFormDataConverter : ITypeConverter<FieldDto, ScoutFormDataDto>
        {
            private readonly IMapper mapper;

            public FieldToScoutFormDataConverter(IMapper mapper)
            {
                this.mapper = mapper;
            }
            
            public ScoutFormDataDto Convert(FieldDto source, ScoutFormDataDto destination, ResolutionContext context)
            {
                destination = new ScoutFormDataDto {Field = source, FieldId = source.Id};
                return GetScoutFormDataFromField(destination, source);
            }

            private ScoutFormDataDto GetScoutFormDataFromField(ScoutFormDataDto scoutFormData, FieldDto field)
            {
                switch (field.FieldType)
                {
                    case FieldType.None:
                        break;
                    case FieldType.CascadeField:
                        scoutFormData.CascadeData = mapper.Map<List<ScoutFormDataDto>>(field.CascadeFields);
                        goto case FieldType.Boolean;
                    case FieldType.Boolean:
                        scoutFormData.BooleanValue = field.BoolDefaultValue;
                        break;
                    case FieldType.OptionSelect:
                    case FieldType.TextField:
                        scoutFormData.StringValue = field.TextDefaultValue;
                        break;
                    case FieldType.Numeric:
                        scoutFormData.NumericValue = field.NumericDefaultValue;
                        break;
                    case FieldType.MultipleChoice:
                        scoutFormData.SelectedOptions = field.DefaultSelectedOptions;
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }

                foreach (var f in field.CascadeFields)
                {
                  //  scoutFormData.CascadeData.Add(GetScoutFormDataFromField(f));
                }

                return scoutFormData;
            }

           
        }
    }
}
