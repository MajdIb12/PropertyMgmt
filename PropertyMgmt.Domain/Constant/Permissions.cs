namespace PropertyMgmt.Domain.Constants;

public static class Permissions
{
    public static class Bookings
    {
        public const string View = "Permissions.Bookings.View";
        public const string Create = "Permissions.Bookings.Create";
        public const string Edit = "Permissions.Bookings.Edit";
        public const string Delete = "Permissions.Bookings.Delete";
    }

    // Listings (previously named Properties)
    public static class Listings
    {
        public const string View = "Permissions.Listings.View";
        public const string Create = "Permissions.Listings.Create";
        public const string Edit = "Permissions.Listings.Edit";
        public const string Delete = "Permissions.Listings.Delete";

        // Image-related actions for listings
        public const string UploadImages = "Permissions.Listings.UploadImages";
        public const string DeleteImage = "Permissions.Listings.DeleteImage";
        public const string SetMainImage = "Permissions.Listings.SetMainImage";
    }

    public static class ListingTypes
    {
        public const string View = "Permissions.ListingTypes.View";
        public const string Create = "Permissions.ListingTypes.Create";
        public const string Edit = "Permissions.ListingTypes.Edit";
        public const string Delete = "Permissions.ListingTypes.Delete";
    }

    public static class Conversations
    {
        public const string View = "Permissions.Conversations.View";
        public const string Send = "Permissions.Conversations.Send";
        public const string MarkAsRead = "Permissions.Conversations.MarkAsRead";
    }

    public static class Notifications
    {
        public const string View = "Permissions.Notifications.View";
        public const string MarkAsRead = "Permissions.Notifications.MarkAsRead";
    }

    public static class Admins
    {
        public const string View = "Permissions.Admins.View";
        public const string Create = "Permissions.Admins.Create";
        public const string Edit = "Permissions.Admins.Edit";
        public const string Delete = "Permissions.Admins.Delete";
    }

    public static class Users
    {
        public const string View = "Permissions.Users.View";
        public const string Create = "Permissions.Users.Create";
        public const string Edit = "Permissions.Users.Edit";
        public const string Delete = "Permissions.Users.Delete";
    }

    public static class Tenants
    {
        public const string View = "Permissions.Tenants.View";
        public const string Create = "Permissions.Tenants.Create";
        public const string Edit = "Permissions.Tenants.Edit";
        public const string Delete = "Permissions.Tenants.Delete";
    }

    public static class Subscriptions
    {
        public const string View = "Permissions.Subscriptions.View";
        public const string Create = "Permissions.Subscriptions.Create";
        public const string Edit = "Permissions.Subscriptions.Edit";
        public const string Delete = "Permissions.Subscriptions.Delete";
    }
}