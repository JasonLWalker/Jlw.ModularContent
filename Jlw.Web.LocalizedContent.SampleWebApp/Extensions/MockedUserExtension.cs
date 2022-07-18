using Jlw.Extensions.Identity.Mock;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using TUser = Jlw.Extensions.Identity.Stores.ModularBaseUser;

namespace Jlw.Web.LocalizedContent.SampleWebApp;

public static class MockedUserExtension
{

    public static IServiceCollection AddMockedUsers(this IServiceCollection services)
    {
        int id = 1;
        MockUserStore<TUser>.AddMockedUser(
            new TUser(new
            {
                Id = id++,
                UserName = "testuser@draxjinn.info",
                NormalizedUserName = "testuser@draxjinn.info",
                Email = "testuser@draxjinn.info",
                NormalizedEmail = "testuser@draxjinn.info",
                PasswordHash = "test",
                EmailConfirmed = true
            }),
            new[]
            {
                    new IdentityUserClaim<string>()
                    {
                        ClaimType = "LocalizedContentAccess",
                        ClaimValue = "Read",
                    }
            });

        MockUserStore<TUser>.AddMockedUser(
            new TUser(new
            {
                Id = id++,
                UserName = "testadmin@draxjinn.info",
                NormalizedUserName = "testadmin@draxjinn.info",
                Email = "testadmin@draxjinn.info",
                NormalizedEmail = "testadmin@draxjinn.info",
                PasswordHash = "test",
                EmailConfirmed = true
            }),
            new[]
            {
                    new IdentityUserClaim<string>()
                    {
                        ClaimType = "ContentOverrideAccess",
                        ClaimValue = "Read",
                    }
            });

        MockUserStore<TUser>.AddMockedUser(
            new TUser(new
            {
                Id = id++,
                UserName = "testsuper@draxjinn.info",
                NormalizedUserName = "testsuper@draxjinn.info",
                Email = "testsuper@draxjinn.info",
                NormalizedEmail = "testsuper@draxjinn.info",
                PasswordHash = "test",
                EmailConfirmed = true
            }),
            new[]
            {
                    new IdentityUserClaim<string>()
                    {
                        ClaimType = "LocalizedContentAccess",
                        ClaimValue = "Read",
                    },
                    new IdentityUserClaim<string>()
                    {
                        ClaimType = "AdminAccess",
                        ClaimValue = "Standard20",
                    }
            });

        return services;
    }

    public static T GetNewUser<T>(object o)
    {
        var constructor = typeof(T).GetConstructor(new[] { typeof(object) });
        if (constructor is null)
            return default;

        return (T)constructor.Invoke(new[] { o });
    }
}