using Engage.Application.Services.Targetings.Enums;

namespace Engage.Application.Exceptions
{
    public class UnknownTargetEntityException : Exception
    {
        public UnknownTargetEntityException(TargetEntity targetEntity)
            : base($"Unknown TargetEntity \"{targetEntity}\"")
        {
        }
    }
}
