using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Projeto.Infra.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Projeto.Infra.Data.Mappings
{
    public class ContaMap : IEntityTypeConfiguration<Conta>
    {
        public void Configure(EntityTypeBuilder<Conta> builder)
        {
            builder.HasKey(c => c.IdConta);

            builder.Property(c => c.NomeConta)
                .HasMaxLength(150)
                .IsRequired();

            builder.Property(c => c.ValorConta)
                .HasColumnType("decimal(18,2)")
                .IsRequired();

            builder.Property(c => c.DataConta)
                .HasColumnType("date")
                .IsRequired();

            #region Relacionamentos

            builder.HasOne(c => c.Categoria)
                .WithMany(c => c.Conta)
                .HasForeignKey(c => c.IdCategoria);

            #endregion
        }
    }
}
