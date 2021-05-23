using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using DataAccesLayer.Enteties;

namespace DataAccesLayer.EntetiesConfiguration
{
    class AddConfiguration : IEntityTypeConfiguration<Ad>
    {
        public void Configure(EntityTypeBuilder<Ad> builder)
        {
            builder.HasKey(keyExpression => keyExpression.ID);
            builder.HasMany(items => items.comments).WithOne(a => a.ad).HasForeignKey(fk => fk.AdId).OnDelete(DeleteBehavior.NoAction);
            builder.HasMany(items => items.favorites).WithOne(a => a.ad).HasForeignKey(fk => fk.AdID).OnDelete(DeleteBehavior.NoAction);
            builder.HasMany(items => items.forCompares).WithOne(a => a.ad).HasForeignKey(fk => fk.AdID).OnDelete(DeleteBehavior.NoAction);
            builder.HasMany(items => items.tags).WithMany(a => a.ads);
            builder.HasMany(items => items.images).WithOne(a => a.ad).HasForeignKey(fk => fk.AdID);
        }
    }
}
