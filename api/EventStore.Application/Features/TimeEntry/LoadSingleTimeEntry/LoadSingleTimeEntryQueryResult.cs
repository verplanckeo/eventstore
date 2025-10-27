namespace EventStore.Application.Features.TimeEntry.LoadSingleTimeEntry;

public class LoadSingleTimeEntryMediatorQueryResult
{
    public ReadTimeEntryModel TimeEntry { get; private set; }

    private LoadSingleTimeEntryMediatorQueryResult(ReadTimeEntryModel timeEntry)
    {
        TimeEntry = timeEntry;
    }

    public static LoadSingleTimeEntryMediatorQueryResult CreateResult(ReadTimeEntryModel timeEntry)
    {
        return new LoadSingleTimeEntryMediatorQueryResult(timeEntry);
    }
}