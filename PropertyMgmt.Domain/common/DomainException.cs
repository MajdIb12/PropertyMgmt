namespace PropertyMgmt.Domain.Common;
    [Serializable]
    public class DomainException : Exception
    {
        public DomainException(string? message) : base(message)
        {
        }
    }
