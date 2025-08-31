using WorkflowTracking.Common.Domain;

namespace WorkflowTracking.Modules.WFManagment.Domain.Workflow;
public sealed class WorkflowStep : Entity
{
    private WorkflowStep()
    {
    }

    public Guid Id { get; private set; }
    public string StepName { get; set; }
    public string AssignedTo { get; set; } 
    public string ActionType { get; set; } 
    public string NextStep { get; set; }

    public Guid WorkflowId { get; set; }
    public Workflow Workflow { get; set; }

    public static WorkflowStep Create(string stepName, string assignedTo, string actionType, string nextStep)
    {
        var workflowStep = new WorkflowStep
        {
            Id = Guid.NewGuid(),
            StepName = stepName,
            AssignedTo = assignedTo,
            ActionType = actionType,
            NextStep = nextStep
        };
        return workflowStep;
    }
}
