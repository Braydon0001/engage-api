namespace Engage.Application.Models;

public class OperationStatus
{
    public OperationStatus()
    {

    }
    public OperationStatus(bool status)
    {
        Status = status;
    }
    public OperationStatus(int operationId)
    {
        Status = true;
        RecordsAffected = 1;
        OperationId = operationId;
    }

    public OperationStatus(int operationId, object returnObject)
    {
        Status = true;
        RecordsAffected = 1;
        OperationId = operationId;
        ReturnObject = returnObject;
    }

    public OperationStatus(int recordsAffected, int operationId)
    {
        Status = true;
        RecordsAffected = recordsAffected;
        OperationId = operationId;
    }

    public OperationStatus(bool status, int recordsAffected, int operationId)
    {
        Status = status;
        RecordsAffected = recordsAffected;
        OperationId = operationId;
    }



    public bool Status { get; set; }
    public bool Exception { get; set; }
    public int RecordsAffected { get; set; }
    public string Message { get; set; }
    public object OperationId { get; set; }
    public object ReturnObject { get; set; }
    public string ExceptionMessage { get; set; }
    public string ExceptionStackTrace { get; set; }
    public string ExceptionInnerMessage { get; set; }
    public string ExceptionInnerStackTrace { get; set; }

    public static OperationStatus CreateFromException(string message, Exception ex)
    {
        var opStatus = new OperationStatus
        {
            Status = false,
            Exception = true,
            Message = message,
            OperationId = null
        };

        if (ex == null)
        {
            return opStatus;
        }

        opStatus.ExceptionMessage = ex.Message;
        opStatus.ExceptionStackTrace = ex.StackTrace;
        opStatus.ExceptionInnerMessage = ex.InnerException?.Message;
        opStatus.ExceptionInnerStackTrace = ex.InnerException?.StackTrace;
        return opStatus;
    }

    public static OperationStatus CreateUniqueConstraintException(string message)
    {
        return new OperationStatus
        {
            Exception = true,
            ExceptionMessage = "Unique Constraint Violation",
            Message = message
        };
    }

    public static OperationStatus EnsureTrue(OperationStatus operationStatus)
    {
        if (operationStatus.Exception)
        {
            return operationStatus;
        }

        if (operationStatus.Status)
        {
            return operationStatus;
        }

        return new OperationStatus()
        {
            Status = true
        };
    }
}

public class OperationResponse
{
    public bool Status { get; set; }
    public bool Exception { get; set; }
    public int RecordsAffected { get; set; }
    public string Message { get; set; }
    public object OperationId { get; set; }
    public object ReturnObject { get; set; }

    public static OperationResponse CreateFromStatus(OperationStatus status)
    {
        var response = new OperationResponse
        {
            Status = status.Status,
            Exception = status.Exception,
            RecordsAffected = status.RecordsAffected,
            Message = status.Message,
            OperationId = status.OperationId,
            ReturnObject = status.ReturnObject,
        };

        return response;

    }
}
