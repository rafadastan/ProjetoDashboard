using Microsoft.EntityFrameworkCore;
using Projeto.Infra.Data.Entities;
using Projeto.Infra.Data.Mappings;
using System;
using System.Collections.Generic;
using System.Text;

namespace Projeto.Infra.Data.Contexts
{
    public class DataContext : DbContext
    {
        //ctor + 2x[tab]
        public DataContext(DbContextOptions<DataContext> options)
            : base(options) //construtor da superclasse
        {

        }

        //sobrescrita do método OnModelCreating
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //adicionar cada classe de mapeamento
            modelBuilder.ApplyConfiguration(new ContaMap());
            modelBuilder.ApplyConfiguration(new CategoriaMap());
        }

        //propriedade DbSet para cada entidade
        public DbSet<Conta> Conta { get; set; }
        public DbSet<Categoria> Categoria { get; set; }
    }
}
