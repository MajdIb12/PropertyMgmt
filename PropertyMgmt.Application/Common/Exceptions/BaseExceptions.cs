namespace PropertyMgmt.Application.Common.Exceptions
{
    // استثناء أساسي لكل المشروع
    public abstract class BaseException : Exception
    {
        protected BaseException(string message) : base(message) { }
    }
}