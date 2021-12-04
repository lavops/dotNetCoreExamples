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
    public class AnimeConfiguration : IEntityTypeConfiguration<Anime>
    {
        public void Configure(EntityTypeBuilder<Anime> builder)
        {
            builder.HasData(
                new Anime
                {
                    Id = Guid.NewGuid(),
                    Name = "Violet Evergarden",
                    Episodes = 13,
                    ReleaseDate = DateTime.Parse("Jan 11, 2018"),
                    StudioId = new Guid("edefae6b-643d-425d-82f5-5c62578dae9c")
                },
                new Anime
                {
                    Id = Guid.NewGuid(),
                    Name = "Hyouka",
                    Episodes = 22,
                    ReleaseDate = DateTime.Parse("Apr 23, 2012"),
                    StudioId = new Guid("edefae6b-643d-425d-82f5-5c62578dae9c")
                },
                new Anime
                {
                    Id = Guid.NewGuid(),
                    Name = "One Punch Man",
                    Episodes = 22,
                    ReleaseDate = DateTime.Parse("Oct 5, 2015"),
                    StudioId = new Guid("70a98b6c-7f0c-448a-bee4-35dd2bed9fae")
                },
                new Anime
                {
                    Id = Guid.NewGuid(),
                    Name = "Hunter x Hunter (2011)",
                    Episodes = 148,
                    ReleaseDate = DateTime.Parse("Oct 2, 2011"),
                    StudioId = new Guid("70a98b6c-7f0c-448a-bee4-35dd2bed9fae")
                }
            );
        }
    }
}
