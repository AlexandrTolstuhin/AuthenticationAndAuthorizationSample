using System;
using Microsoft.AspNetCore.Identity;

namespace AuthenticationAndAuthorizationSample.Basics.Entities
{
    public class ApplicationRole : IdentityRole<Guid>
    { }
}