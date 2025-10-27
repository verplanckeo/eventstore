namespace EventStore.Application.Features.TimeEntry.Register;

public class RegisterTimeEntryMediatorCommandResponse
{
    public string TimeEntryId { get; private set; }

    private RegisterTimeEntryMediatorCommandResponse(string timeEntryId)
    {
        TimeEntryId = timeEntryId;
    }

    public static RegisterTimeEntryMediatorCommandResponse CreateResponse(string timeEntryId)
    {
        return new RegisterTimeEntryMediatorCommandResponse(timeEntryId);
    }
}