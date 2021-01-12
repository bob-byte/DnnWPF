using DnnWPF.Models.Domain;
using System.Data.Entity.ModelConfiguration;

namespace DnnWPF.Models.EntityConfigurations
{
    class TypesRoadSignsConfigurations : EntityTypeConfiguration<TypesRoadSigns>
    {
        public TypesRoadSignsConfigurations()
        {
            HasKey(c => c.ClassId);

            //For definition name of road sign after recognizion ClassId using method DnnWPF.ViewModels.RecognisingTypeOfRoadSign.GetMax
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
