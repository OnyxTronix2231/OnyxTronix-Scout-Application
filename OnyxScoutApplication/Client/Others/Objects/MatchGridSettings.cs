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
        public List<FormDto> SubmittedForms { get; set; }
        
        [Parameter]
        public string Height { get; set; } = "100%";

        [Parameter]
        public bool AllowPaging { get; set; }
        
        [Parameter]
        public string PagingScopeName { get; set; }


        public override async Task SetParametersAsync(ParameterView parameters)
        {
            Console.WriteLine("MatchGridSettings:SetParametersAsync");
            foreach (var parameter in parameters)
            {
                if (parameter.Cascading)
                {
                    Parent = (MatchesGrid) parameter.Value;
                    Parent.Settings = this;
                    continue;
                }

                switch (parameter.Name)
                {
                    case nameof(TeamNumber):
                        TeamNumber = (int) parameter.Value;
                        break;
                    case nameof(SubmittedForms):
                        SubmittedForms = (List<FormDto>) parameter.Value;
                        break;
                    case nameof(Height):
                        Height = (string) parameter.Value;
                        break;
                    case nameof(AllowPaging):
                        AllowPaging = (bool) parameter.Value;
                        break;
                    case nameof(PagingScopeName):
                        PagingScopeName = (string) parameter.Value;
                        break;
                }
            }
            await base.SetParametersAsync(ParameterView.Empty).ConfigureAwait(false);
        }

        protected override void OnInitialized()
        {
            Parent.Refresh();
        }
    }
}
