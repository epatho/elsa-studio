using Elsa.Studio.Contracts;
using Elsa.Studio.Models;
using Elsa.Studio.UIHintHandlers.Components;
using Microsoft.AspNetCore.Components;

namespace Elsa.Studio.UIHintHandlers.Handlers;

public class FlowSwitchEditorHandler : IUIHintHandler
{
    public bool GetSupportsUIHint(string uiHint) => uiHint == "flow-switch-editor";
    public string UISyntax => WellKnownSyntaxNames.Object;

    public RenderFragment DisplayInputEditor(DisplayInputEditorContext context)
    {
        return builder =>
        {
            builder.OpenComponent(0, typeof(Cases));
            builder.AddAttribute(1, nameof(Cases.EditorContext), context);
            builder.CloseComponent();
        };
    }
}