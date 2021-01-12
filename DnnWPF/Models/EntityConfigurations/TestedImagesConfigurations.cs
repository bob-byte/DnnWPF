using DnnWPF.Models.Domain;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace DnnWPF.Models.EntityConfigurations
{
    public class TestedImagesConfigurations : EntityTypeConfiguration<TestedImages>
    {
        public TestedImagesConfigurations()
        {
            Property(c => c.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
        }
    }
}
