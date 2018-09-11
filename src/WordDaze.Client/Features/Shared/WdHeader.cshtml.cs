using System;
using Microsoft.AspNetCore.Blazor.Components;

namespace WordDaze.Client.Shared
{
    public class WdHeaderModel : BlazorComponent
    {
        [Parameter] protected string Heading { get; set; }
        [Parameter] protected string SubHeading { get; set; }
        [Parameter] protected string Author { get; set; }
        [Parameter] protected DateTime PostedDate { get; set; }
    }
}