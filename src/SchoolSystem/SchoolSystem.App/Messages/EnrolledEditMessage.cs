namespace SchoolSystem.App.Messages;

public record EnrolledEditMessage
{
    public required Guid EnrolledId { init; get; }
}
