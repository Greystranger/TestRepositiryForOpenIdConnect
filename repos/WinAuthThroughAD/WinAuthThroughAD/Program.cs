using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.DirectoryServices;
using System.Security.Principal;


namespace WinAuthThroughAD
{
    class Program
    {
        static void Main(string[] args)
        {
            string name = @"Yatsenko";
            var claims = new List<Claim>();

            EnrichWindowsClaims(name, claims);
        }

        private static void EnrichWindowsClaims(string partName, List<Claim> claims)
        {
            // specify the search filter
            using (var search = new DirectorySearcher { Filter = string.Format("(&(objectClass=user)(sAMAccountName={0}))", partName) })
            {
                // specify which property values to return in the search
                search.PropertiesToLoad.Add("givenName"); // first name
                search.PropertiesToLoad.Add("sn"); // last name
                search.PropertiesToLoad.Add("mail"); // smtp mail address
                search.PropertiesToLoad.Add("objectSid"); // primary sid

                // perform the search
                SearchResult result = null;
                try
                {
                    result = search.FindOne();
                }
                catch (Exception ex)
                {

                    throw;
                }

                var map = new[] { ("givenName", ClaimTypes.GivenName), ("sn", ClaimTypes.Surname), ("mail", ClaimTypes.Email), ("objectSid", ClaimTypes.PrimarySid) };
                if (result != null)
                {
                    foreach (var (property, claimType) in map)
                    {
                        var claimValue = result.Properties[property];
                        if (claimValue != null && claimValue.Count > 0)
                        {
                            claims.RemoveAll(t => t.Type == claimType);
                            for (int i = 0; i < claimValue.Count; i++)
                            {
                                var value = claimValue[i];
                                if (value is string s)
                                {
                                    claims.Add(new Claim(claimType, s));
                                }
                                else if (value is byte[] b)
                                {
                                    var si = new SecurityIdentifier(b, 0);
                                    claims.Add(new Claim(claimType, si.ToString()));
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}
