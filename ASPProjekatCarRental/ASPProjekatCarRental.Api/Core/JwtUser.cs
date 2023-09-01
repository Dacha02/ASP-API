using ASPProjekatCarRental.Domain;

namespace ASPProjekatCarRental.Api.Core
{
    public class JwtUser : IApplicationUser
    {
        public string Identity { get; set; }

        public int Id {get; set; }

        public IEnumerable<int> UseCaseIds { get; set; } = new List<int>();

        public string Email { get; set; }
    }

    public class AnnonymousUser : IApplicationUser
    {
        public string Identity => "Annonymous";

        public int Id => 1;

        public IEnumerable<int> UseCaseIds => new List<int> { 1,2,3,11 };

        public string Email => "annymous@asp-api.com";
    }
}
