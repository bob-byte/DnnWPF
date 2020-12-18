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
        Boolean doTest = true;

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
                    var typesRoadSigns = readFile.ReadCSVForTypesRoadSigns(@"C:\Users\Богдан\Desktop\DnnWPF\DnnWPF\bin\Debug\signnames.csv");

                    foreach (var item in typesRoadSigns)
                    {
                        context.TypesRoadSigns.AddOrUpdate(item);
                    }
                }
                if (!context.ImagesForTests.Any())
                {
                    var imagesForTests = readFile.ReadCSVForImagesForTests(@"C:\Users\Богдан\Desktop\DnnWPF\DnnWPF\bin\Debug\GTSRB-german-traffic-sign\Test(1000img).csv");

                    foreach (var item in imagesForTests)
                    {
                        context.ImagesForTests.AddOrUpdate(item);
                        //AddValidIdsOfImageToTypeRoadSings(item.ValidId,
                        //                                  false,
                        //                                  item.PathToImage,
                        //                                  (IEnumerable<Byte>)item.ValidId);
                    }
                    context.SaveChanges();
                }
            }

            if(doTest)
            {
                try
                {
                    var emguCV = new RecognisingEmguCVNoCLAHE<Bgr, Byte>();
                    var test = new Test<Bgr, Byte, Double>();

                    var model = (Net)emguCV.LoadModel("D:\\netWithoutCLAHE.onnx");
                    test.Testing(emguCV, model,
                        @"C:\Users\Богдан\Desktop\DnnWPF\DnnWPF\bin\Debug\GTSRB-german-traffic-sign\TryTest(13img)", SearchOption.TopDirectoryOnly);
                }
                catch(Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
        }
    }
}
