using System.Collections.Generic;

namespace EventStore.Application.Features.TimeEntry.LoadAllEntries;

public class LoadAllTimeEntriesMediatorQueryResult
{
    public IEnumerable<ReadTimeEntryModel> TimeEntries { get; private set; }

    private LoadAllTimeEntriesMediatorQueryResult(List<ReadTimeEntryModel> timeEntries)
    {
        TimeEntries = timeEntries;
    }

    public static LoadAllTimeEntriesMediatorQueryResult CreateResult(List<ReadTimeEntryModel> timeEntries)
    {
        return new LoadAllTimeEntriesMediatorQueryResult(timeEntries);
    }
}