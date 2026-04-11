namespace PropertyMgmt.Application.Common.Exceptions
{
    // مثال لاستثناء حقيقي
    public class BookingConflictException : BaseException
    {
        public BookingConflictException() 
            : base("This property is already booked for the selected dates.") { }
    }
}