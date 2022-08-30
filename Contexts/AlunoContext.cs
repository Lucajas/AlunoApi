using System;
using Microsoft.EntityFrameworkCore;
using EscolaTrab.Models;

namespace EscolaTrab.Contexts
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseInMemoryDatabase("DbAluno");
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Aluno> Alunos { get; set; }

        internal static object CreateObjectSet<Aluno>() where Aluno : class
        {
            throw new NotImplementedException();
        }

        internal object Find(string id)
        {
            throw new NotImplementedException();
        }
    }
}
