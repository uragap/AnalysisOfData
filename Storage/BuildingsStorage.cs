using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Globalization;

namespace Storage
{
    public class BuildingsStorage
    {
        //private BinaryFormatter formatter = new BinaryFormatter();
        private Building[] buildings = new Building[19];

        //
        public void InitBuildingStorage(QVPointStorage qVPointStorage,
            TemperaturePointStorage temperatureStorage)
        {
            for(int i=0;i<19;i++)
            {
                var list = new List<QVTPoint>();
                buildings[i] = new Building(i, CreateQVTPointList(i,qVPointStorage, temperatureStorage));
            }
        }

        private List<QVTPoint> CreateQVTPointList(int buildingNumber, QVPointStorage qVPointStorage, 
            TemperaturePointStorage temperatureStorage)
        {
            var list = new List<QVTPoint>();
            var qvpoints = qVPointStorage.GetQVPoints(buildingNumber);
            var tpoints = temperatureStorage.GetTemperaturePoints(buildingNumber);
            int i = 0;// Для qvpoints.
            int k = 0;// Для tpoints.
            while(i<qvpoints.Count && k<tpoints.Count)
            {
                if(qvpoints[i].DateBegin <= tpoints[k].TimeBegin && qvpoints[i].DateEnd>=tpoints[k].TimeBegin)
                {
                    list.Add(new QVTPoint(qvpoints[i], tpoints[k]));
                    k++;
                }
                else if(tpoints[k].TimeBegin>qvpoints[i].DateEnd)
                {
                    i++;
                }
                else if(tpoints[k].TimeBegin < qvpoints[i].DateBegin)
                {
                    k++;
                }
            }
            return list;
        }

        // Save list of buildings to binary file.
        //public void Save()
        //{
        //    // Gain code access to the file that we are going
        //    // to write to
        //    try
        //    {
        //        // Create a FileStream that will write data to file.
        //        FileStream writerFileStream =
        //            new FileStream(dataFilename, FileMode.Create, FileAccess.Write);
        //        // Save our list of points to file
        //        formatter.Serialize(writerFileStream, Buildings);
        //        // Close the writerFileStream when we are done.
        //        writerFileStream.Close();
        //    }
        //    catch (Exception)
        //    {
        //        Console.WriteLine("Unable to save our buildings information");
        //    } // end try-catch
        //}

        //// Load list of buildings from binary file. 
        //public void Load()
        //{
        //    // Check if we had previously Save information of our friends
        //    // previously
        //    if (File.Exists(dataFilename))
        //    {
        //        try
        //        {
        //            // Create a FileStream will gain read access to the 
        //            // data file.
        //            FileStream readerFileStream = new FileStream(dataFilename,
        //                FileMode.Open, FileAccess.Read);
        //            // Reconstruct information of our friends from file.
        //            Buildings = (List<Building>)
        //                formatter.Deserialize(readerFileStream);
        //            // Close the readerFileStream when we are done
        //            readerFileStream.Close();

        //        }
        //        catch (Exception)
        //        {
        //            Console.WriteLine("There seems to be a file that contains " +
        //                "buildings information but somehow there is a problem " +
        //                "with reading it.");
        //        } // end try-catch
        //    } // end if
        //}

        // Write list of buildings to txt file.
        //

        public void WriteToTxtFile()
        {
            for(int i=0;i<19;i++)
            {
                var filename = @"E:\my document\C#\Project C#\BldStorage"
                    + (i+1).ToString(CultureInfo.InvariantCulture) + ".txt";
                StreamWriter streamWriter = new StreamWriter(filename);
                buildings[i].WriteToTxtFile(streamWriter);
                streamWriter.Close();
            }
        }
    }
}
