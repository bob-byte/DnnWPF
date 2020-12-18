using DnnWPF.Models.Domain;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DnnWPF.Models
{
    public class ReadFile : IDisposable
    {
        private StreamReader reader;

        internal List<TypesRoadSigns> ReadCSVForTypesRoadSigns(String nameFile)
        {
            List<TypesRoadSigns> rowsForDB = new List<TypesRoadSigns>();

            reader = new StreamReader(nameFile);
            while (reader.Peek() >= 0)
            {
                String textRow = reader.ReadLine();
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
                //String[] imageNamesArray = imageNames.ToArray();

                //for (int i = 0; i < imageNamesArray.Length; i++)
                //{
                //    if (!images.Any(t => t.PathToImage.Equals(imageNamesArray[i], StringComparison.CurrentCultureIgnoreCase)))
                //    {
                //        images.Add(new ImagesForTests { PathToImage = imageNamesArray[i], ValidId = validIdsArray[i] });
                //    }
                //}

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
        private bool disposedValue = false; // Для определения избыточных вызовов

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    GC.Collect();
                }

                reader.Dispose();
                disposedValue = true;
            }
        }

        // TODO: переопределить метод завершения, только если Dispose(bool disposing) выше включает код для освобождения неуправляемых ресурсов.
        ~ReadFile()
        {
            Dispose(false);
        }

        // Этот код добавлен для правильной реализации шаблона высвобождаемого класса.
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion

    }
}
