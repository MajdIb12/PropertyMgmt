namespace PropertyMgmt.Application.Common.Exceptions
{
    public class CancelFailedException : BaseException
    {
        public CancelFailedException(object Id) : base($"Failed to cancel booking with ID {Id}.") { }
    }
}