namespace PropertyMgmt.Application.Common.Exceptions
{
    public class ConfirmFailedException : BaseException
    {
        public ConfirmFailedException() : base($"Failed to confirm booking.") { }
    }
}