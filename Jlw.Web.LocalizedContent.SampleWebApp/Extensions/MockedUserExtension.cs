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
		            ClaimType = nameof(LocalizedContentAccess),
		            ClaimValue = nameof(LocalizedContentAccess.Authorized),
	            },

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
                    ClaimType = nameof(LocalizedContentAccess),
                    ClaimValue = nameof(LocalizedContentAccess.Authorized),
                },

                new IdentityUserClaim<string>()
                {
	                ClaimType = nameof(LocalizedContentAccess),
	                ClaimValue = nameof(LocalizedContentAccess.InsertScreenRecords),
                },


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
                // User is Authorized
                new IdentityUserClaim<string>() {ClaimValue = nameof(LocalizedContentAccess.Authorized), ClaimType = nameof(LocalizedContentAccess)},
                
                // User can read records
                new IdentityUserClaim<string>() {ClaimValue = nameof(LocalizedContentAccess.ReadFieldRecords), ClaimType = nameof(LocalizedContentAccess)},
                new IdentityUserClaim<string>() {ClaimValue = nameof(LocalizedContentAccess.ReorderFieldRecords), ClaimType = nameof(LocalizedContentAccess)},

                // User can save records
                new IdentityUserClaim<string>() {ClaimValue = nameof(LocalizedContentAccess.SaveFieldRecords), ClaimType = nameof(LocalizedContentAccess)},

                // User can rename records
				new IdentityUserClaim<string>() {ClaimValue = nameof(LocalizedContentAccess.RenameScreenRecords), ClaimType = nameof(LocalizedContentAccess)},
                new IdentityUserClaim<string>() {ClaimValue = nameof(LocalizedContentAccess.RenameFieldRecords), ClaimType = nameof(LocalizedContentAccess)},

                // User can create records
				new IdentityUserClaim<string>() {ClaimValue = nameof(LocalizedContentAccess.InsertScreenRecords), ClaimType = nameof(LocalizedContentAccess)},
                new IdentityUserClaim<string>() {ClaimValue = nameof(LocalizedContentAccess.InsertWizardRecords), ClaimType = nameof(LocalizedContentAccess)},
                new IdentityUserClaim<string>() {ClaimValue = nameof(LocalizedContentAccess.InsertFieldRecords), ClaimType = nameof(LocalizedContentAccess)},

                // User can delete records
				new IdentityUserClaim<string>() {ClaimValue = nameof(LocalizedContentAccess.DeleteScreenRecords), ClaimType = nameof(LocalizedContentAccess)},
                new IdentityUserClaim<string>() {ClaimValue = nameof(LocalizedContentAccess.DeleteWizardRecords), ClaimType = nameof(LocalizedContentAccess)},
                new IdentityUserClaim<string>() {ClaimValue = nameof(LocalizedContentAccess.DeleteFieldRecords), ClaimType = nameof(LocalizedContentAccess)},

                // User can duplicate records
                new IdentityUserClaim<string>() {ClaimValue = nameof(LocalizedContentAccess.DuplicateWizardRecords), ClaimType = nameof(LocalizedContentAccess)},
                new IdentityUserClaim<string>() {ClaimValue = nameof(LocalizedContentAccess.DuplicateScreenRecords), ClaimType = nameof(LocalizedContentAccess)},
                new IdentityUserClaim<string>() {ClaimValue = nameof(LocalizedContentAccess.DuplicateFieldRecords), ClaimType = nameof(LocalizedContentAccess)},

                // User can change label text
                new IdentityUserClaim<string>() {ClaimValue = nameof(LocalizedContentAccess.InsertLabelText), ClaimType = nameof(LocalizedContentAccess)},
                new IdentityUserClaim<string>() {ClaimValue = nameof(LocalizedContentAccess.SaveLabelText), ClaimType = nameof(LocalizedContentAccess)},
                new IdentityUserClaim<string>() {ClaimValue = nameof(LocalizedContentAccess.DeleteLabelText), ClaimType = nameof(LocalizedContentAccess)},

                // User can change error text
                new IdentityUserClaim<string>() {ClaimValue = nameof(LocalizedContentAccess.InsertErrorText), ClaimType = nameof(LocalizedContentAccess)},
                new IdentityUserClaim<string>() {ClaimValue = nameof(LocalizedContentAccess.SaveErrorText), ClaimType = nameof(LocalizedContentAccess)},
                new IdentityUserClaim<string>() {ClaimValue = nameof(LocalizedContentAccess.DeleteErrorText), ClaimType = nameof(LocalizedContentAccess)},

                // User can Preview
                new IdentityUserClaim<string>() {ClaimValue = nameof(LocalizedContentAccess.Preview), ClaimType = nameof(LocalizedContentAccess)},
                
                // User can Export
                new IdentityUserClaim<string>() {ClaimValue = nameof(LocalizedContentAccess.Export), ClaimType = nameof(LocalizedContentAccess)},
            });

        MockUserStore<TUser>.AddMockedUser(
            new TUser(new
            {
                Id = id++,
                UserName = "viewonly@draxjinn.info",
                NormalizedUserName = "viewonly@draxjinn.info",
                Email = "viewonly@draxjinn.info",
                NormalizedEmail = "viewonly@draxjinn.info",
                PasswordHash = "test",
                EmailConfirmed = true
            }),
            new[]
            {
                // User is Authorized
                new IdentityUserClaim<string>() {ClaimValue = nameof(LocalizedContentAccess.Authorized), ClaimType = nameof(LocalizedContentAccess)},
                
                // User can read records
                new IdentityUserClaim<string>() {ClaimValue = nameof(LocalizedContentAccess.ReadFieldRecords), ClaimType = nameof(LocalizedContentAccess)},

            });

        MockUserStore<TUser>.AddMockedUser(
            new TUser(new
            {
                Id = id++,
                UserName = "textonly@draxjinn.info",
                NormalizedUserName = "textonly@draxjinn.info",
                Email = "textonly@draxjinn.info",
                NormalizedEmail = "textonly@draxjinn.info",
                PasswordHash = "test",
                EmailConfirmed = true
            }),
            new[]
            {
                // User is Authorized
                new IdentityUserClaim<string>() {ClaimValue = nameof(LocalizedContentAccess.Authorized), ClaimType = nameof(LocalizedContentAccess)},
                
                // User can read records
                new IdentityUserClaim<string>() {ClaimValue = nameof(LocalizedContentAccess.ReadFieldRecords), ClaimType = nameof(LocalizedContentAccess)},

                // User can change label text
                new IdentityUserClaim<string>() {ClaimValue = nameof(LocalizedContentAccess.SaveLabelText), ClaimType = nameof(LocalizedContentAccess)},

                // User can change error text
                new IdentityUserClaim<string>() {ClaimValue = nameof(LocalizedContentAccess.SaveErrorText), ClaimType = nameof(LocalizedContentAccess)},

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