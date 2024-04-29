namespace SchoolSystem.App.Messages;

public record EvaluationEditMessage
{
    public required Guid EvaluationId { init; get; }
}
