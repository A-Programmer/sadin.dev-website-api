namespace Website.Common.WebFrameworks.Routing;

public static class Routes
{
    public const string BaseRootAddress = "api/[controller]";
    public static class ContactUs
    {
        public static class Get
        {
            public const string GetAll = "";
            public const string GetById = "{id}";
        }
        public static class Post
        {
            public const string Add = "";
        }
        public static class Delete
        {
            public const string Remove = "{id}";
        }
        public static class Edit
        {
            public const string Update = "";
            public const string Activate = "{id}/active";
            public const string Deactive = "{id}/deactive";
            public const string ChangeStatus = "{id}/change_status";
        }
    }
}