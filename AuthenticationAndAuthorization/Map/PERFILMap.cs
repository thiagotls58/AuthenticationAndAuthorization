using AuthenticationAndAuthorization.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthenticationAndAuthorization.Map
{
    public class PERFILMap : IEntityTypeConfiguration<PERFIL>
    {
        public void Configure(EntityTypeBuilder<PERFIL> builder)
        {
            builder.ToTable("PERFIL");
            builder.HasKey(p => p.PER_ID);
            builder.Property(p => p.PER_ID).HasColumnName("PER_ID").IsRequired();
            builder.Property(p => p.PER_NOME).HasColumnName("PER_NOME").HasMaxLength(100).IsRequired();
        }
    }
}
