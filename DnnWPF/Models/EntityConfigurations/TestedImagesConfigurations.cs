using DnnWPF.Models.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DnnWPF.Models.EntityConfigurations
{
    public class TestedImagesConfigurations : EntityTypeConfiguration<TestedImages>
    {
        public TestedImagesConfigurations()
        {
            //HasRequired(c => c.TypePredictedSign)
            //    .WithMany(c => c.TestedImages)
            //    .HasForeignKey(c => c.PredictedSignId)
            //    .WillCascadeOnDelete(false);
            Property(c => c.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
        }
    }
}
