using DnnWPF.Models.Domain;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DnnWPF.Models.EntityConfigurations
{
    public class ImagesForTestsConfigurations : EntityTypeConfiguration<ImagesForTests>
    {
        public ImagesForTestsConfigurations()
        {
            //HasMany(c => c.TestedImagesValid)
            //    .WithRequired(c => c.TypeValidSign)
            //    .HasForeignKey(c => c.ValidSignId)
            //    .WillCascadeOnDelete(false);
            HasOptional(c => c.TestedImage)
                .WithRequired(c => c.ImageForTest);
                //.WithMany(c => c.ValidSignsInImage)
                //.Map(c =>
                //{
                //    c.ToTable("ValidRoadSignsInTestedImage");
                //    c.MapLeftKey("ValidId");
                //    c.MapRightKey("ValidSignId");
                //});
        }
    }
}
