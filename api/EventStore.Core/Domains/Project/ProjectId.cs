using System;
using EventStore.Core.DddSeedwork;

namespace EventStore.Core.Domains.Project;

public class ProjectId : EntityId
{
    private Guid _projectId;

    public ProjectId()
    {
        _projectId = Guid.NewGuid();
    }

    public ProjectId(string id)
    {
        if (!Guid.TryParse(id, out var projectId)) 
            throw new FormatException("Id of ProjectId entity should be a guid (i.e.: D5E717DF-EDDE-433B-947D-0CE8EE20E4A0).");
            
        _projectId = projectId;
    }

    public override string ToString() => _projectId.ToString();
}