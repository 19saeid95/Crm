namespace Crm.Application.Authorization;

public static class Permissions
{
    public static class User
    {
        public const string View = "User.View";
        public const string Create = "User.Create";
        public const string Update = "User.Update";
    }

    public static class Customer
    {
        public const string View = "Customer.View";
        public const string Create = "Customer.Create";
        public const string Update = "Customer.Update";
    }

    public static class Location
    {
        public const string View = "Location.View";
        public const string Create = "Location.Create";
        public const string Update = "Location.Update";
    }
}