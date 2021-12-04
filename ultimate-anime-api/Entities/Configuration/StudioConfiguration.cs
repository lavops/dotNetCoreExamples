using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Configuration
{
    public class StudioConfiguration : IEntityTypeConfiguration<Studio>
    {
        public void Configure(EntityTypeBuilder<Studio> builder)
        {
            builder.HasData(
                new Studio
                {
                    Id = new Guid("edefae6b-643d-425d-82f5-5c62578dae9c"),
                    Name = "Kyoto Animation",
                    Address = "Tokyo",
                    Country = "Japan"
                },
                new Studio
                {
                    Id = new Guid("70a98b6c-7f0c-448a-bee4-35dd2bed9fae"),
                    Name = "Madhouse",
                    Address = "Tokyo",
                    Country = "Japan"
                }
            );
        }
    }
}
