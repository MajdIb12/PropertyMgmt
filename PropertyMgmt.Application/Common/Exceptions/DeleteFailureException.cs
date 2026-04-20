namespace PropertyMgmt.Application.Common.Exceptions
{
    public class DeleteFailureException : BaseException
    {
        public DeleteFailureException(string message) : base(message) { }
    }
}