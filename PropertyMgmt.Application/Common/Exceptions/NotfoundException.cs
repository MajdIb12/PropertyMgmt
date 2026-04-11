namespace PropertyMgmt.Application.Common.Exceptions
{
    public class NotFoundException : BaseException
    {
        public NotFoundException(string Name, object Id) : base($"{Name} with ID {Id} not found.") { }
    }
}