namespace SchoolSystem.App.Messages;

public record StudentEditMessage
{
    public required Guid StudentId { init; get; }
}
