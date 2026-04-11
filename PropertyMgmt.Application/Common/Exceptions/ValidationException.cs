namespace PropertyMgmt.Application.Common.Exceptions
{
    public class ValidationExceptions : Exception
{
    public IDictionary<string, string[]> Errors { get; }

    public ValidationExceptions()
        : base("حدث خطأ واحد أو أكثر في التحقق من البيانات.")
    {
        Errors = new Dictionary<string, string[]>();
    }

    public ValidationExceptions(IDictionary<string, string[]> failures)
        : this()
        {
            Errors = failures;
        }
}
}