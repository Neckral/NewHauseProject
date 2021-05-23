using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using DataAccesLayer.Enteties;

namespace DataAccesLayer.EntetiesConfiguration
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(keyExpression => keyExpression.Id);
            builder.HasMany(items => items.ads).WithOne(item => item.user).HasForeignKey(fk => fk.OwnerId);
            builder.HasMany(items => items.comments).WithOne(item => item.user).HasForeignKey(fk => fk.UserID).OnDelete(DeleteBehavior.NoAction);
            builder.HasMany(items => items.favorites).WithOne(item => item.user).HasForeignKey(fk => fk.UserID).OnDelete(DeleteBehavior.NoAction);
            builder.HasMany(item => item.forCompares).WithOne(u => u.user).HasForeignKey(fk => fk.UserID).OnDelete(DeleteBehavior.NoAction);
        }
    }
}
