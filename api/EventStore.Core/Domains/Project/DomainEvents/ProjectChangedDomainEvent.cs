using EventStore.Core.DddSeedwork;

namespace EventStore.Core.Domains.Project.DomainEvents;

public class ProjectChangedDomainEvent : DomainEvent
{
    public string Name { get; set; }
    public string Code { get; set; }
    public bool Billable { get; set; }

    public ProjectChangedDomainEvent(string name, string code, bool billable)
    {
        Name = name;
        Code = code;
        Billable = billable;
    }
}