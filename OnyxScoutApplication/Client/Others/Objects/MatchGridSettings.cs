using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using OnyxScoutApplication.Client.Shared;
using OnyxScoutApplication.Shared.Models;
using OnyxScoutApplication.Shared.Models.ScoutFormModels;

namespace OnyxScoutApplication.Client.Others.Objects
{
    public class MatchGridSettings : ComponentBase
    {
        [CascadingParameter]
        public MatchesGrid Parent { get; set; }
        
        [Parameter]
        public int TeamNumber { get; set; }

        [Parameter]
        public List<ScoutFormDto> SubmittedForms { get; set; }
        
        [Parameter]
        public string Height { get; set; } = "100%";

        [Parameter]
        public bool AllowPaging { get; set; }
        
        [Parameter]
        public string PagingScopeName { get; set; }


        public override Task SetParametersAsync(ParameterView parameters)
        {
            foreach (var parameter in parameters)
            {
                if (parameter.Cascading)
                {
                    Parent = (MatchesGrid) parameter.Value;
                    continue;
                }

                switch (parameter.Name)
                {
                    case nameof(TeamNumber):
                        TeamNumber = (int) parameter.Value;
                        break;
                    case nameof(SubmittedForms):
                        SubmittedForms = (List<ScoutFormDto>) parameter.Value;
                        break;
                    case nameof(Height):
                        Height = (string) parameter.Value;
                        break;
                    case nameof(AllowPaging):
                        AllowPaging = (bool) parameter.Value;
                        break;
                    case nameof(PagingScopeName):
                        Console.WriteLine(PagingScopeName);
                        PagingScopeName = (string) parameter.Value;
                        break;
                }
            }
            return base.SetParametersAsync(ParameterView.Empty);
        }

        protected override void OnInitialized()
        {
            Parent.Settings = this;
        }
    }
}
