using A_Test.Domain;
using Microsoft.EntityFrameworkCore;
using System;

namespace A_Test.Infrastructure.EFCore
{
    public class TestContext : DbContext
    {
        public DbSet<Info> Informations  { get; set; }

        public TestContext(DbContextOptions<TestContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var assembly = typeof(InfoMapping).Assembly;
            modelBuilder.ApplyConfigurationsFromAssembly(assembly);
            base.OnModelCreating(modelBuilder);
        }
    }
}
