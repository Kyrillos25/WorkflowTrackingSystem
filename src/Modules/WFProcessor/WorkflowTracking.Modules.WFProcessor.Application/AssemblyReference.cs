using System.Reflection;

namespace WorkflowTracking.Modules.WFProcessor.Application;
public static class AssemblyReference
{
    public static readonly Assembly Assembly = typeof(AssemblyReference).Assembly;
}
