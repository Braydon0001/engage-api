namespace Engage.Application.Exceptions;

public class TrainingException : Exception
{
    public TrainingException(string message, int? trainingId = null) 
        : base(trainingId.HasValue ? $"Training id: {trainingId} ." :  message)
    {
    }
}
