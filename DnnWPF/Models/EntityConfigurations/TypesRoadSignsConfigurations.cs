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
    class TypesRoadSignsConfigurations : EntityTypeConfiguration<TypesRoadSigns>
    {
        public TypesRoadSignsConfigurations()
        {
            HasKey(c => c.ClassId);

            //HasMany(c => c.TestedImages)
            //    .WithMany(c => c.TypesValidSigns)
            //    .Map(c =>
            //    {
            //        c.ToTable("TypeRoadSings_TestedImages");
            //        c.MapLeftKey("ClassId");
            //        c.MapRightKey("ValidSignId");
            //    });
            //HasRequired(c=> c.TestedImages)
            //    .WithMany(c => c.)
            //HasMany(c => c.TestedImagesValid)
            //    .WithRequired(c => c.TypeValidSign)
            //    .HasForeignKey(c => c.ValidSignId)
            //    .WillCascadeOnDelete(false);


            //Для визначення за Id, що ми отримуємо після методу GetMax, назви дорожнього знаку
            HasMany(c => c.TestedImages_PredictedRoadSigns)
                .WithRequired(c => c.TypePredictedSign)
                .HasForeignKey(c => c.PredictedSignId)
                .WillCascadeOnDelete(false);

            HasMany(c => c.TestedImages_ValidRoadSigns)
                .WithRequired(c => c.TypeValidSign)
                .HasForeignKey(c => c.ValidSignId);

            HasMany(c => c.ValidSignsOfImagesForTest)
                .WithRequired(c => c.TypeValidSign)
                .HasForeignKey(c => c.ValidId);
        }
    }
}
