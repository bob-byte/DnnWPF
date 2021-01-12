using DnnWPF.Models.Domain;
using System.Data.Entity.ModelConfiguration;

namespace DnnWPF.Models.EntityConfigurations
{
    public class ImagesForTestsConfigurations : EntityTypeConfiguration<ImagesForTests>
    {
        public ImagesForTestsConfigurations()
        {
            HasOptional(c => c.TestedImage)
                .WithRequired(c => c.ImageForTest);
        }
    }
}
