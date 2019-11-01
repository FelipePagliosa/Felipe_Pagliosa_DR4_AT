using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using BLL;

namespace BLL
{
    public class DataContext : DbContext
    {
        public DataContext() : base("DataBaseAT")
        {

        }

        public DbSet<Author> Authors { get; set; }
        public DbSet<Book> Books { get; set; }
    }
}

