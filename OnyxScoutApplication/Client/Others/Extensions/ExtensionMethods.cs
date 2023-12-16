using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.JSInterop;
using OnyxScoutApplication.Shared.Models.ScoutFormFormatModels;
using Syncfusion.Blazor.PivotView;

namespace OnyxScoutApplication.Client.Others.Extensions
{
    public static class ExtensionMethods
    {
        public static ValueTask<object> SaveAs(this IJSRuntime js, string filename, byte[] data)
            => js.InvokeAsync<object>(
                "blazorFuncs.saveAsFile",
                filename,
                Convert.ToBase64String(data));
    }
}
