using Microsoft.AspNetCore.Identity;

namespace EGWalks.API.Repositories {
    public interface ITokenRepository {
        string CreateJWTToken(IdentityUser identityUser, List<string> roles);
    }
}
