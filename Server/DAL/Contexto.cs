using Microsoft.EntityFrameworkCore;
using Shared.Models;

namespace DIVISOR_WASM.Server.DAL
{
    public class Contexto : DbContext
    {
        public Contexto(DbContextOptions<Contexto> Options) : base(Options) { }
        public DbSet<Student> Students { get; set; }
        public DbSet<Team> Teams { get; set; }
    }
}