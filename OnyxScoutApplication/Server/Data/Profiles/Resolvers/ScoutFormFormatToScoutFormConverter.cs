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
    public class ScoutFormFormatToScoutFormConverter : ITypeConverter<ScoutFormFormatDto, FormDto>
    {
        private readonly IMapper mapper;

        public ScoutFormFormatToScoutFormConverter(IMapper mapper)
        {
            this.mapper = mapper;
        }

        public FormDto Convert(ScoutFormFormatDto source, FormDto destination, ResolutionContext context)
        {
            destination = new FormDto
            {
                FormDataInStages = source.FieldsInStages.Select(i => mapper.Map<FormDataInStageDto>(i)).ToList()
            };

            return destination;
        }
    }

    public class FieldToScoutFormDataConverter : ITypeConverter<FieldDto, FormDataDto>
    {
        private readonly IMapper mapper;

        public FieldToScoutFormDataConverter(IMapper mapper)
        {
            this.mapper = mapper;
        }

        public FormDataDto Convert(FieldDto source, FormDataDto destination, ResolutionContext context)
        {
            destination = new FormDataDto {Field = source, FieldId = source.Id};
            return GetScoutFormDataFromField(destination, source);
        }

        private FormDataDto GetScoutFormDataFromField(FormDataDto formData, FieldDto field)
        {
            switch (field.FieldType)
            {
                case FieldType.None:
                    break;
                case FieldType.CascadeField:
                    formData.CascadeData = mapper.Map<List<FormDataDto>>(field.CascadeFields);
                    goto case FieldType.Boolean;
                case FieldType.Boolean:
                    formData.BooleanValue = field.BoolDefaultValue;
                    break;
                case FieldType.TextField:
                    formData.StringValue = field.TextDefaultValue;
                    break;
                case FieldType.Integer:
                    formData.NumericValue = field.NumericDefaultValue;
                    break;
                case FieldType.OptionSelect:
                case FieldType.MultipleChoice:
                    formData.SelectedOptions = field.DefaultSelectedOptions;
                    break;
                case FieldType.Timer:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            foreach (var f in field.CascadeFields)
            {
                //  scoutFormData.CascadeData.Add(GetScoutFormDataFromField(f));
            }

            return formData;
        }
    }
}
