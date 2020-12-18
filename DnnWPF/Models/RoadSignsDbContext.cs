namespace DnnWPF.Models
{
    using DnnWPF.Models.Domain;
    using DnnWPF.Models.EntityConfigurations;
    using System;
    using System.Data.Entity;
    using System.Linq;

    public class RoadSignsDbContext : DbContext
    {
        // Контекст настроен для использования строки подключения "RoadSignsDbContext" из файла конфигурации  
        // приложения (App.config или Web.config). По умолчанию эта строка подключения указывает на базу данных 
        // "DnnWPF.RoadSignsDbContext" в экземпляре LocalDb. 
        // 
        // Если требуется выбрать другую базу данных или поставщик базы данных, измените строку подключения "RoadSignsDbContext" 
        // в файле конфигурации приложения.
        public RoadSignsDbContext()
            : base("name=RoadSignsDbContext")
        {
        }

        public virtual DbSet<ImagesForTests> ImagesForTests { get; set; }
        public virtual DbSet<TestedImages> TestedImages { get; set; }
        public virtual DbSet<TypesRoadSigns> TypesRoadSigns { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new TestedImagesConfigurations());
            modelBuilder.Configurations.Add(new TypesRoadSignsConfigurations());
            modelBuilder.Configurations.Add(new ImagesForTestsConfigurations());
        }

        // Добавьте DbSet для каждого типа сущности, который требуется включить в модель. Дополнительные сведения 
        // о настройке и использовании модели Code First см. в статье http://go.microsoft.com/fwlink/?LinkId=390109.

        // public virtual DbSet<MyEntity> MyEntities { get; set; }
    }

    //public class MyEntity
    //{
    //    public int Id { get; set; }
    //    public string Name { get; set; }
    //}
}