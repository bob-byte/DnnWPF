using DnnWPF.Models.Domain;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace DnnWPF.Models
{
    public class ReadFile : IDisposable
    {
        private StreamReader reader;

        private Boolean disposedValue = false; // Для определения избыточных вызовов

        internal List<TypesRoadSigns> ReadCSVForTypesRoadSigns(String nameFile)
        {
            List<TypesRoadSigns> rowsForDB = new List<TypesRoadSigns>();

            reader = new StreamReader(nameFile);
            while (reader.Peek() >= 0)
            {
                String textRow = reader.ReadLine();

                //If row contains headers of columns
                if (textRow.Contains("Class"))
                {
                    continue;
                }

                String[] arrayData = textRow.Split(new Char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);

                if (arrayData.Length >= 4)
                {
                    throw new FileLoadException("Bad format of file with description road signs");
                }

                Byte id;
                if (!Byte.TryParse(arrayData[0], out id))
                {
                    throw new FormatException("Can\'t convert column id to Byte type");
                }

                rowsForDB.Add(new TypesRoadSigns
                {
                    ClassId = id,
                    Name = arrayData[1],
                    PathToTemplateImage = arrayData[2],
                    PrecisionRecognising = 0,
                    CountTest = 0
                });
            }

            return rowsForDB;
        }

        public void AddTestImageToTypeRoadSings(Byte classId, Boolean whetherSaveChanges, IEnumerable<String> pathsToImages)
        {
            using (var context = new RoadSignsDbContext())
            {
                var images = context.ImagesForTests.Where(t => pathsToImages.Contains(t.PathToImage)).ToList();
                if (images == null)
                {
                    throw new ArgumentNullException("Can\'t get any image with these paths");
                }

                var roadSign = context.TypesRoadSigns.Single(c => c.ClassId == classId);

                images.ForEach(c => roadSign.AddTestImage(c));
                if (whetherSaveChanges)
                {
                    context.SaveChanges();
                }
            }
        }

        internal List<ImagesForTests> ReadCSVForImagesForTests(String nameFile)
        {
            List<ImagesForTests> rowsForDB = new List<ImagesForTests>();

            reader = new StreamReader(nameFile);
            while (reader.Peek() >= 0)
            {
                String textRow = reader.ReadLine();
                if (textRow.Contains("Class"))
                {
                    continue;
                }
                String[] arrayData = textRow.Split(new Char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

                Byte id;
                if (!Byte.TryParse(arrayData[6], out id))
                {
                    throw new FormatException("Can\'t convert column id to Byte type");
                }

                rowsForDB.Add(new ImagesForTests
                {
                    ValidId = id,
                    PathToImage = arrayData[7]
                });
            }

            return rowsForDB;
        }

        #region IDisposable Support

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                reader?.Dispose();
                disposedValue = true;
            }
        }

        ~ReadFile()
        {
            Dispose(false);
        }

        public void Dispose()
        {
            Dispose(true);

            GC.SuppressFinalize(this);
        }
        #endregion

    }
}
