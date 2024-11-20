namespace Engage.Application.Exceptions
{
    public class InvalidStakeholderTypeException : Exception
    {
        public InvalidStakeholderTypeException(string stakeholderType)
             : base($"\"{stakeholderType}\" is not in the range of valid StakeholderTypes.")
        {
        }
    }
}
