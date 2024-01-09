using Microsoft.EntityFrameworkCore;

namespace WebAPI.Models
{
    public class PersonsContext : DbContext
    {
        public PersonsContext(DbContextOptions<PersonsContext> options) : base(options)
        {

        }

        public DbSet<Persons> Persons { get; set; }
    }
}
