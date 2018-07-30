using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using WebApplication3.Models;

namespace WebApplication3.Service
{
    public class UserService
    {
        public ClaimsIdentity CreateIdentity(MEMBER member, string authenticationType)
        {
            ClaimsIdentity _identity = new ClaimsIdentity(DefaultAuthenticationTypes.ApplicationCookie);
            _identity.AddClaim(new Claim(ClaimTypes.Name, member.MEMBERNAME));
            _identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, member.MEMBERID));
            //_identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, member.PASSWORDS));
            //_identity.AddClaim(new Claim("http://schemas.microsoft.com/accesscontrolservice/2010/07/claims/identityprovider", "ASP.NET Identity"));
            return _identity;
        }
    }
}