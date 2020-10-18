using OnyxScoutApplication.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnyxScoutApplication.Client.Others.Objects
{
    public class TeamDataAnalyzer
    {
        public List<Tuple<FieldType, string, float>> CalculateDataFor(FieldStageType fieldStageType, List<FieldDto> format, List<ScoutFormDto> data)
        {
            List<Tuple<FieldType, string, float>> l = new List<Tuple<FieldType, string, float>>();
            for (int i = 0; i < format.Count; i++)
            {
                Console.WriteLine(i);
                Console.WriteLine(format[i].Name);
                Console.WriteLine(fieldStageType);
                l.Add(new Tuple<FieldType, string, float>(format[i].FieldType, format[i].Name, GetAvgFor(fieldStageType, format[i].FieldType, i, data)));
            }
            return l;
        }

        private float GetAvgFor(FieldStageType fieldStageType, FieldType fieldType, int i, List<ScoutFormDto> data)
        {
            if (fieldType == FieldType.Numeric)
            {
                float avarge = 0;
                int count = 0;
                foreach (var scoutForm in data)
                {
                    if (fieldStageType == FieldStageType.Autonomous)
                    {
                        avarge += (int)scoutForm.AutonomousData[i].NumricValue;
                    }
                    else if (fieldStageType == FieldStageType.Teleoperated)
                    {
                        avarge += (int)scoutForm.TeleoperatedData[i].NumricValue;
                    }
                    else if (fieldStageType == FieldStageType.EndGame)
                    {
                        avarge += (int)scoutForm.EndGameData[i].NumricValue;
                    }
                    count++;
                }
                return avarge /= count;
            }
            else if (fieldType == FieldType.Boolean)
            {
                float trueAvarage = 0;
                foreach (var scoutForm in data)
                {
                    if (fieldStageType == FieldStageType.Autonomous)
                    {
                        if (scoutForm.AutonomousData[i].BooleanValue)
                        {
                            trueAvarage++;
                        }
                    }
                    else if (fieldStageType == FieldStageType.Teleoperated)
                    {
                        if (scoutForm.TeleoperatedData[i].BooleanValue)
                        {
                            trueAvarage++;
                        }
                    }
                    else if (fieldStageType == FieldStageType.EndGame)
                    {
                        if (scoutForm.EndGameData[i].BooleanValue)
                        {
                            trueAvarage++;
                        }
                    }
                }
                return trueAvarage /= data.Count;
            }
            return 0;
        }
    }
}
