using DnnWPF.Models.Domain;
using DnnWPF.Models.EntityConfigurations;
using System.Data.Entity;

namespace DnnWPF.Models
{
    public class RoadSignsDbContext : DbContext
    {
        public RoadSignsDbContext()
            : base("name=RoadSignsDbContext")
        { }

        public virtual DbSet<ImagesForTests> ImagesForTests { get; set; }
        public virtual DbSet<TestedImages> TestedImages { get; set; }
        public virtual DbSet<TypesRoadSigns> TypesRoadSigns { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new TestedImagesConfigurations());
            modelBuilder.Configurations.Add(new TypesRoadSignsConfigurations());
            modelBuilder.Configurations.Add(new ImagesForTestsConfigurations());
        }
    }
}