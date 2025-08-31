using WorkflowTracking.Common.Domain;

namespace WorkflowTracking.Modules.WFManagment.Domain.Workflow;
public sealed class Workflow : Entity
{
    private readonly List<WorkflowStep> _steps = [];

    private Workflow()
    {
    }

    public Guid Id { get; private set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public IReadOnlyCollection<WorkflowStep> Steps => _steps.ToList();

    public static Workflow Create(string name, string description)
    {
        var workflow = new Workflow
        {
            Id = Guid.NewGuid(),
            Name = name,
            Description = description
        };
        return workflow;
    }

    public void AddStep(string stepName, string assignedTo, string actionType, string nextStep)
    {
        var step = WorkflowStep.Create(stepName, assignedTo, actionType, nextStep);
        step.WorkflowId = Id;
        step.Workflow = this;
        _steps.Add(step);
    }
}
