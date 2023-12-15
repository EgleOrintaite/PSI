using Microsoft.EntityFrameworkCore;
using WebService;

namespace NoteTakingApp
{
    public class NoteContext : DbContext
    {
        public DbSet<Note> Notes { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=EGLE\\MSSQLSERVER01;Database=Notes;Integrated Security=True;MultipleActiveResultSets=True;TrustServerCertificate=True;");
        }
    }
}
