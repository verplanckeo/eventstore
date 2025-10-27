using System;
using EventStore.Core.DddSeedwork;

namespace EventStore.Core.Domains.TimeEntry;

public class TimeEntryId : EntityId
{
    private Guid _timeEntryId;

    public TimeEntryId()
    {
        _timeEntryId = Guid.NewGuid();
    }

    public TimeEntryId(string id)
    {
        if (!Guid.TryParse(id, out var timeEntryId)) 
            throw new FormatException("Id of TimeEntryId entity should be a guid (i.e.: D5E717DF-EDDE-433B-947D-0CE8EE20E4A0).");
            
        _timeEntryId = timeEntryId;
    }

    public override string ToString() => _timeEntryId.ToString();
}