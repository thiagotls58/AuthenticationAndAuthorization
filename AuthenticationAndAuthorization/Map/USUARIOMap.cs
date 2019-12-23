using AuthenticationAndAuthorization.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthenticationAndAuthorization.Map
{
    public class USUARIOMap : IEntityTypeConfiguration<USUARIO>
    {
        public void Configure(EntityTypeBuilder<USUARIO> builder)
        {
            builder.ToTable("USUARIO");
            builder.HasKey(u => u.USU_ID);
            builder.Property(u => u.USU_ID).HasColumnName("USU_ID").IsRequired();
            builder.Property(u => u.USU_NOME).HasColumnName("USU_NOME").HasMaxLength(100).IsRequired();
            builder.Property(u => u.USU_EMAIL).HasColumnName("USU_EMAIL").HasMaxLength(100).IsRequired();
            builder.Property(u => u.USU_SENHA).HasColumnName("USU_SENHA").HasMaxLength(100).IsRequired();
        }
    }
}
