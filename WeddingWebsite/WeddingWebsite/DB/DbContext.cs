namespace WeddingWebsite.DB
{
    using Microsoft.EntityFrameworkCore;
    using Models;

    public class DbContext: Microsoft.EntityFrameworkCore.DbContext
    {
        public DbContext(DbContextOptions<DbContext> options) : base(options)
        {
        }

        public DbSet<GuestViewModel> Guest { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<GuestViewModel>().HasKey("Id");
            modelBuilder.Entity<GuestViewModel>().ToTable("Guest");
            
            //modelBuilder.Entity<Course>().ToTable("Course");
            //modelBuilder.Entity<Enrollment>().ToTable("Enrollment");
            //modelBuilder.Entity<Student>().ToTable("Student");
        }
    }
}
