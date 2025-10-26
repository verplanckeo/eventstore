using EventStore.Core.DddSeedwork;

namespace EventStore.Core.Domains.Project.DomainEvents;

public class ProjectRegisteredDomainEvent : DomainEvent
{
    public string ProjectId { get; set; }
    public string Name { get; set; }
    public string Code { get; set; }
    public bool Billable { get; set; }

    public ProjectRegisteredDomainEvent(string projectId, string name, string code, bool billable)
    {
        ProjectId = projectId;
        Name = name;
        Code = code;
        Billable = billable;
    }
}