using CSharp_React.EntityFramework;
using CSharp_React.EntityFramework.Tables;
using CSharp_React.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CSharp_React.Servicies
{
    public class RightService
    {
        private readonly TaxiDbContext _taxiDbContext;

        public RightService(TaxiDbContext taxiDbContext)
        {
            _taxiDbContext = taxiDbContext;
        }

        public async Task<List<string>> GetUserRights(int userId)
        {
            var rights = (await _taxiDbContext.Set<User>()
                            .Include(u => u.Role)
                                .ThenInclude(r => r.Rights)
                            .FirstOrDefaultAsync(u => u.UserId == userId))?.
                            Role?.Rights?.Select(r => r.RightLabel)?.ToList();

            rights ??= new();

            var additionalRights = (await _taxiDbContext.Set<User>()
                                    .Include(u => u.AdditionalRights)
                                        .ThenInclude(r => r.Right)
                                    .FirstOrDefaultAsync(u => u.UserId == userId))?.
                                    AdditionalRights?.Select(r => new { r.HasRight, r.Right.RightLabel })?.ToList();

            additionalRights ??= new();

            foreach (var additionalRight in additionalRights)
            {
                if (additionalRight.HasRight && !rights.Contains(additionalRight.RightLabel))
                {
                    rights.Add(additionalRight.RightLabel);
                    continue;
                }

                if(!additionalRight.HasRight && rights.Contains(additionalRight.RightLabel))
                {
                    rights.Remove(additionalRight.RightLabel);
                }
            }

            return rights;
        }

        public async Task<bool> HasComponentRightAccess(int userId, string component, string right)
        {
            var rights = await GetUserRights(userId);

            return rights.Any(r => r.Contains(component + "." + right));
        }
    }
}
