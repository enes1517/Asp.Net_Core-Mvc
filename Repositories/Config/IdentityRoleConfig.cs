using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Config
{
    public class IdentityRoleConfig : IEntityTypeConfiguration<IdentityRole>
    {
        public void Configure(EntityTypeBuilder<IdentityRole> builder)
        {
            builder.HasData(
            
                new IdentityRole() { Name = "User", NormalizedName = "USER", Id = "c66ea84c-9f97-4a93-8f9c-36dbe1b23e91" },
                new IdentityRole() { Name = "Editor", NormalizedName = "EDITOR", Id = "c275ccb8-48eb-44f4-8e35-f86da478994a" },
                new IdentityRole() { Name = "Admin", NormalizedName = "ADMIN", Id = "b8b19114-f074-48ba-8f18-276762981571" }
            );
        }
    }
}
