using System.Collections.Generic;
using EventStore.Core.DddSeedwork;
using EventStore.Core.Domains.Project.DomainEvents;

namespace EventStore.Core.Domains.Project;

public class Project : EventSourcedAggregateRoot<ProjectId>
{
    public override ProjectId Id { get; protected set; }
    public string Name { get; private set; }
    public string Code { get; private set; }
    public bool Billable { get; private set; }
    public bool IsRemoved { get; private set; }

    /// <summary>
    /// DO NOT USE THIS CTOR - It is only here for unit tests
    /// </summary>
    public Project()
    { }

    /// <summary>
    /// When an aggregate has been fetched from db, we call this CTor which will apply all events and increase the Version by 1
    /// </summary>
    /// <param name="events"></param>
    public Project(IEnumerable<IDomainEvent> events) : base(events) { }

    public static Project CreateNewProject(string name, string code, bool billable)
    {
        var project = new Project();
        project.Apply(new ProjectRegisteredDomainEvent(new ProjectId().ToString(), name, code, billable));

        return project;
    }

    public void ChangeProject(string name, string code, bool billable)
    {
        Apply(new ProjectChangedDomainEvent(name, code, billable));
    }

    public void RemoveProject()
    {
        Apply(new ProjectRemovedDomainEvent());
    }

    // Event handlers called via dynamic dispatch from EventSourcedAggregateRoot
    public void On(ProjectRegisteredDomainEvent evt)
    {
        Id = new ProjectId(evt.ProjectId);
        Name = evt.Name;
        Code = evt.Code;
        Billable = evt.Billable;
        IsRemoved = false;
    }

    public void On(ProjectChangedDomainEvent evt)
    {
        Name = evt.Name;
        Code = evt.Code;
        Billable = evt.Billable;
    }

    public void On(ProjectRemovedDomainEvent evt)
    {
        IsRemoved = true;
    }
}