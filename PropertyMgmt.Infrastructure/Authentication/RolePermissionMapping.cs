using PropertyMgmt.Domain.Constants;

namespace PropertyMgmt.Infrastructure.Authentication;

public static class RolePermissionMapping
{
    public static IEnumerable<string> GetPermissionsForRole(string roleName)
    {
        return roleName switch
        {
            "MasterAdmin" => GetAllPermissions(),

            // الأدمن من النوع الأول يملك صلاحيات كاملة داخل شركته عدا الحذف الحرج لنظام الماستر
            "AdminType1" => new[]
            {
                // Bookings
                Permissions.Bookings.View, Permissions.Bookings.Create, Permissions.Bookings.Edit, Permissions.Bookings.Delete,
                // Listings
                Permissions.Listings.View, Permissions.Listings.Create, Permissions.Listings.Edit, Permissions.Listings.Delete,
                Permissions.Listings.UploadImages, Permissions.Listings.DeleteImage, Permissions.Listings.SetMainImage,
                // Listing types
                Permissions.ListingTypes.View, Permissions.ListingTypes.Create, Permissions.ListingTypes.Edit, Permissions.ListingTypes.Delete,
                // Conversations
                Permissions.Conversations.View, Permissions.Conversations.Send, Permissions.Conversations.MarkAsRead,
                // Notifications
                Permissions.Notifications.View, Permissions.Notifications.MarkAsRead,
                // Admins & Users & Tenants & Subscriptions
                Permissions.Admins.View, Permissions.Admins.Create, Permissions.Admins.Edit, Permissions.Admins.Delete,
                Permissions.Users.View, Permissions.Users.Create, Permissions.Users.Edit, Permissions.Users.Delete,
                Permissions.Tenants.View, Permissions.Tenants.Create, Permissions.Tenants.Edit, Permissions.Tenants.Delete,
                Permissions.Subscriptions.View, Permissions.Subscriptions.Create, Permissions.Subscriptions.Edit, Permissions.Subscriptions.Delete
            },

            // الأدمن من النوع الثاني (مشرف مثلاً) يملك صلاحيات العرض والإنشاء والتعديل دون الحذف
            "AdminType2" => new[]
            {
                Permissions.Bookings.View, Permissions.Bookings.Create, Permissions.Bookings.Edit,
                Permissions.Listings.View, Permissions.Listings.Create, Permissions.Listings.Edit,
                Permissions.ListingTypes.View, Permissions.ListingTypes.Create, Permissions.ListingTypes.Edit,
                Permissions.Conversations.View, Permissions.Conversations.Send,
                Permissions.Notifications.View,
                Permissions.Users.View, Permissions.Users.Create,
            },

            // المستخدم العادي (مستأجر أو موظف محدود)
            "User" => new[]
            {
                Permissions.Bookings.View, Permissions.Bookings.Create,
                Permissions.Listings.View,
                Permissions.Conversations.View, Permissions.Conversations.Send,
                Permissions.Notifications.View
            },

            _ => Array.Empty<string>()
        };
    }

    private static IEnumerable<string> GetAllPermissions()
    {
        return new[]
        {
            // Bookings
            Permissions.Bookings.View, Permissions.Bookings.Create, Permissions.Bookings.Edit, Permissions.Bookings.Delete,
            // Listings
            Permissions.Listings.View, Permissions.Listings.Create, Permissions.Listings.Edit, Permissions.Listings.Delete,
            Permissions.Listings.UploadImages, Permissions.Listings.DeleteImage, Permissions.Listings.SetMainImage,
            // Listing types
            Permissions.ListingTypes.View, Permissions.ListingTypes.Create, Permissions.ListingTypes.Edit, Permissions.ListingTypes.Delete,
            // Conversations
            Permissions.Conversations.View, Permissions.Conversations.Send, Permissions.Conversations.MarkAsRead,
            // Notifications
            Permissions.Notifications.View, Permissions.Notifications.MarkAsRead,
            // Admins, Users, Tenants, Subscriptions
            Permissions.Admins.View, Permissions.Admins.Create, Permissions.Admins.Edit, Permissions.Admins.Delete,
            Permissions.Users.View, Permissions.Users.Create, Permissions.Users.Edit, Permissions.Users.Delete,
            Permissions.Tenants.View, Permissions.Tenants.Create, Permissions.Tenants.Edit, Permissions.Tenants.Delete,
            Permissions.Subscriptions.View, Permissions.Subscriptions.Create, Permissions.Subscriptions.Edit, Permissions.Subscriptions.Delete
        };
    }
}