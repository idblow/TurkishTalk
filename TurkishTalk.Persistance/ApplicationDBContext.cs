using Microsoft.EntityFrameworkCore;



namespace TurkishTalk.Persistance
{
    public class ApplicationDBContext: DbContext
    {
        public ApplicationDBContext() {}

        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> dbContextOptions) : base(dbContextOptions){}
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=DESKTOP-5EJG1O9\\SQLEXPRESS;Database=TurkishTalk;Trusted_Connection=True;Encrypt=False;"); 
            base.OnConfiguring(optionsBuilder);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDBContext).Assembly);
            base.OnModelCreating(modelBuilder);
        }
    }
}
