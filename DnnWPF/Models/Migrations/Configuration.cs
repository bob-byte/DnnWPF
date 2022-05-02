using DnnWPF.Models;
using DnnWPF.ViewModels;
using Emgu.CV.Dnn;
using Emgu.CV.Structure;
using System;
using System.Data.Entity.Migrations;
using System.IO;
using System.Linq;

namespace DnnWPF.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<RoadSignsDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(RoadSignsDbContext context)
        {
            using (ReadFile readFile = new ReadFile())
            {
                if (!context.TypesRoadSigns.Any())
                {
                    var typesRoadSigns = readFile.ReadCSVForTypesRoadSigns("signnames.csv");

                    foreach (var typeRoadSign in typesRoadSigns)
                    {
                        context.TypesRoadSigns.AddOrUpdate(typeRoadSign);
                    }
                }
                if (!context.ImagesForTests.Any())
                {
                    var imagesForTests = readFile.ReadCSVForImagesForTests(@"GTSRB-german-traffic-sign\Test(1000img).csv");

                    foreach (var image in imagesForTests)
                    {
                        context.ImagesForTests.AddOrUpdate(image);
                    }
                }
            }
        }
    }
}
