using Healthcare.Domain.Shared;

namespace Healthcare.Domain.Errors;

public static class DomainErrors
{
    public static class Test
    {
        public static Error SomethingWentWrong => new Error($"Test.{nameof(SomethingWentWrong)}", "Something went wrong");
    }

    public static class General
    {
        public static Error InternalServerError =
            new Error($"General.{nameof(InternalServerError)}", "Please contact administrator");
    }
}