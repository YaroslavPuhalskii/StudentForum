#nullable disable
using System.Collections.Generic;
using System.Security.Claims;

namespace StudentForum.IntegrationTests.Helpers
{
    internal class TestClaimsProvider
    {
        public IList<Claim> Claims { get; set; }

        public TestClaimsProvider WithAdminClaims()
        {
            var provider = new TestClaimsProvider
            {
                Claims = new List<Claim>(),
            };
            provider.Claims.Add(new Claim(ClaimTypes.NameIdentifier, "6bae6274-b01d-45c0-8728-3ad95b3ca565"));
            provider.Claims.Add(new Claim(ClaimTypes.Name, "Yaraslau"));
            provider.Claims.Add(new Claim(ClaimTypes.Role, "Admin"));

            return provider;
        }

        public TestClaimsProvider WithUserClaims()
        {
            var provider = new TestClaimsProvider
            {
                Claims = new List<Claim>(),
            };
            Claims = new List<Claim>();
            provider.Claims.Add(new Claim(ClaimTypes.NameIdentifier, "da15aa3a-bc1d-43d3-8ec3-c36cd5fb039a"));
            provider.Claims.Add(new Claim(ClaimTypes.Name, "User"));

            return provider;
        }
    }
}
