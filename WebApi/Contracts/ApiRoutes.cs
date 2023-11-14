namespace WebApi.Contracts;

public static class ApiRoutes
{
    public static class User
    {
        public const string Login = "/api/user/login";
        public const string ChangeDetails = "/api/user";
        public const string ChangePassword = "/api/user/password";
    }
    public static class Test
    {
        public const string Create = "/api/test/create";
        public const string Get = "/api/test/get";
    }

    public static class Admin
    {
        public const string CreateEmployee = "/api/admin/employee";
        public const string DeleteEmployee = "/api/admin/employee";
    }
}