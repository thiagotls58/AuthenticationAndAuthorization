using AuthenticationAndAuthorization.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthenticationAndAuthorization.Map
{
    public class PERFIL_USUARIOMap : IEntityTypeConfiguration<PERFIL_USUARIO>
    {
        public void Configure(EntityTypeBuilder<PERFIL_USUARIO> builder)
        {
            builder.ToTable("PERFIL_USUARIO");
            builder.HasKey(pu => new { pu.PER_ID, pu.USU_ID });
            builder.Property(pu => pu.PER_ID).HasColumnName("PER_ID").IsRequired();
            builder.Property(pu => pu.USU_ID).HasColumnName("USU_ID").IsRequired();
            builder.HasOne(pu => pu.PERFIL).WithMany(p => p.PERFIL_USUARIO).HasForeignKey(pu => pu.PER_ID);
            builder.HasOne(pu => pu.USUARIO).WithMany(u => u.PERFIL_USUARIO).HasForeignKey(pu => pu.USU_ID);
        }
    }
}
