namespace ORM
{
    using System.Data.Entity;

    public partial class EntityModel : DbContext
    {
        public EntityModel()
            : base("name=EntityModel")
        {
        }

        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Area> Areas { get; set; }
        public virtual DbSet<Profile> Profiles { get; set; } 
        public virtual DbSet<ProfileArea> ProfileAreas { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Role>()
                .HasMany(e => e.Users)
                .WithRequired(e => e.Role)
                .WillCascadeOnDelete(false);
        }
    }
}
