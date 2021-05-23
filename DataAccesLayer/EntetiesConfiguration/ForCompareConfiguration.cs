using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using DataAccesLayer.Enteties;

namespace DataAccesLayer.EntetiesConfiguration
{
    class ForCompareConfiguration : IEntityTypeConfiguration<ForCompare>
    {
        public void Configure(EntityTypeBuilder<ForCompare> builder)
        {
            builder.HasKey(key => new { key.AdID, key.UserID });
        }
    }
}
