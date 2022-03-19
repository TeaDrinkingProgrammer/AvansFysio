using Domain;
using DomainServices;
using Infrastructure;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Portal.Models {
    public class IdentitySeeder {
        private IdentityContext _context;

        public IdentitySeeder(IdentityContext context) {
            _context = context;
        }
        public async void SeedEmployees() {
            var user = new IdentityUser {
                UserName = "Henk Panken",
                NormalizedUserName = "HENK PANKEN",
                Email = "henkpanken@gmail.com",
                NormalizedEmail = "HENKPANKEN@GMAIL.COM",
                EmailConfirmed = false,
                LockoutEnabled = true,
                SecurityStamp = Guid.NewGuid().ToString()
            };

            var roleStore = new RoleStore<IdentityRole>(_context);

            if (!_context.Users.Any(u => u.UserName == user.UserName)) {
                var password = new PasswordHasher<IdentityUser>();
                var hashed = password.HashPassword(user, "AvansFysio&1");
                user.PasswordHash = hashed;
                var userStore = new UserStore<IdentityUser>(_context);
                Claim claim = new(ClaimTypes.Authentication, "Employee");
                Claim claim2 = new(ClaimTypes.Authentication, "TherapistEmployee");
                List<Claim> claims = new() { claim, claim2 };
                await userStore.AddClaimsAsync(user, claims);
                await userStore.CreateAsync(user);
            }
            //Second user (to not get problems with context)
            var user2 = new IdentityUser {
                UserName = "Lars de Jong",
                NormalizedUserName = "LARS DE JONG",
                Email = "larsdejong@gmail.com",
                NormalizedEmail = "LARSDEJONG@GMAIL.com",
                EmailConfirmed = false,
                LockoutEnabled = true,
                SecurityStamp = Guid.NewGuid().ToString()
            };

            if (!_context.Users.Any(u => u.UserName == user2.UserName)) {
                var password2 = new PasswordHasher<IdentityUser>();
                var hashed2 = password2.HashPassword(user, "AvansFysio&1");
                user2.PasswordHash = hashed2;
                var userStore2 = new UserStore<IdentityUser>(_context);
                Claim claim3 = new(ClaimTypes.Authentication, "Employee");
                Claim claim4 = new(ClaimTypes.Authentication, "StudentEmployee");
                List<Claim> claims2 = new() { claim3, claim4 };
                await userStore2.AddClaimsAsync(user2, claims2);
                await userStore2.CreateAsync(user2);
            }
        }
    }
}
