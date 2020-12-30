﻿using OnyxScoutApplication.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnyxScoutApplication.Client.Others.Objects.Analyzers
{
    public class OptionSelectAnalayzer : IFieldAnalyzer
    {
        public FieldAverage Analyze(List<ScoutFormDto> scoutForms, FieldDto field, Func<ScoutFormDto, List<ScoutFormDataDto>> getTragetList, Func<ScoutFormDto, bool> shouldCount)
        {
            FieldAverage fieldAverage = new FieldAverage(field);
            int totalCount = 0;
            Dictionary<string, int> optionsCount = new Dictionary<string, int>();
            foreach (var scoutForm in scoutForms)
            {
                if (shouldCount(scoutForm))
                {
                    ScoutFormDataDto scoutFormData = getTragetList(scoutForm).FirstOrDefault(i => i.Field.Name == field.Name);
                    if (scoutFormData != null)
                    {
                        totalCount++;
                        if (!string.IsNullOrWhiteSpace(scoutFormData.StringValue))
                        {
                            if (optionsCount.ContainsKey(scoutFormData.StringValue))
                            {
                                optionsCount[scoutFormData.StringValue] = optionsCount[scoutFormData.StringValue]++;
                            }
                            else
                            {
                                optionsCount.Add(scoutFormData.StringValue, 1);
                            }
                        }
                    }
                }
            }
            foreach (var key in field.Options)
            {
                float count = 0;
                if (optionsCount.ContainsKey(key))
                {
                    count = optionsCount[key];
                }
                fieldAverage.optionsAvarage.Add(key, count / totalCount);
            }
            return fieldAverage;
        }
    }
}
                
        


