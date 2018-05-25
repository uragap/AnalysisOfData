using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using Services;
using System.Globalization;


namespace Storage
{
    public class TemperaturePointStorage
    {
        //private const string DATA_FILENAME = @"E:\my document\C#\Project C#\AnalysisOfData\TemperaturePointList.bin";
        private BinaryFormatter formatter = new BinaryFormatter();
        private List<TemperaturePoint>[] points = new List<TemperaturePoint>[19]
            {
                new List<TemperaturePoint>(),
                new List<TemperaturePoint>(),
                new List<TemperaturePoint>(),
                new List<TemperaturePoint>(),
                new List<TemperaturePoint>(),
                new List<TemperaturePoint>(),
                new List<TemperaturePoint>(),
                new List<TemperaturePoint>(),
                new List<TemperaturePoint>(),
                new List<TemperaturePoint>(),
                new List<TemperaturePoint>(),
                new List<TemperaturePoint>(),
                new List<TemperaturePoint>(),
                new List<TemperaturePoint>(),
                new List<TemperaturePoint>(),
                new List<TemperaturePoint>(),
                new List<TemperaturePoint>(),
                new List<TemperaturePoint>(),
                new List<TemperaturePoint>()
            }; // index = building number-1
        // key = sensorID, value = buildingID
        private Dictionary<int, int> sensorBuildingPairs = new Dictionary<int, int> 
        {
            {2,1}, {24,2}, {47,3}, {228,6}, {79,8}, {289,14}, {307,15}, {266,16}, {218,5}, {238,7}
        }; 

        public void ReadInitTxtFile(string filename)
        {
            CultureInfo provider = CultureInfo.InvariantCulture;
            var format = "yyyy-MM-dd HH:mm:ss";
            if (filename.Contains("Tdata"))
            {
                foreach (var str in File.ReadAllLines(filename))
                {
                    string[] split = Parser.ParseString(str);
                    if (Convert.ToInt32(split[5]) >= 8)
                    {
                        split[3] = split[3].Replace('.', ',');
                        var temperaturePoint = new TemperaturePoint(Convert.ToDouble(split[3]),
                            DateTime.ParseExact(split[1], format, provider));
                        points[sensorBuildingPairs[Convert.ToInt32(split[0])]-1].Add(temperaturePoint);
                    }
                }
            }
            else
                Console.WriteLine("Choose correct file.");
        }
        //
        public List<TemperaturePoint> GetTemperaturePoints(int buildingNumber)
        {
            return points[buildingNumber];
        }

        // Save list of points to binary file.
        //static public void Save(string DATA_FILENAME)
        //{
        //    // Gain code access to the file that we are going
        //    // to write to
        //    try
        //    {
        //        // Create a FileStream that will write data to file.
        //        FileStream writerFileStream =
        //            new FileStream(DATA_FILENAME, FileMode.Create, FileAccess.Write);
        //        // Save our list of points to file
        //        formatter.Serialize(writerFileStream, Points);
        //        // Close the writerFileStream when we are done.
        //        writerFileStream.Close();
        //    }
        //    catch (Exception)
        //    {
        //        Console.WriteLine("Unable to save our points information");
        //    } // end try-catch
        //} // end public bool Save()

        // Load list of points from binary file. 
        //static public void Load(string DATA_FILENAME)
        //{
        //    // Check if we had previously Save information of our friends
        //    // previously
        //    if (File.Exists(DATA_FILENAME))
        //    {
        //        try
        //        {
        //            // Create a FileStream will gain read access to the 
        //            // data file.
        //            FileStream readerFileStream = new FileStream(DATA_FILENAME,
        //                FileMode.Open, FileAccess.Read);
        //            // Reconstruct information of our friends from file.
        //            Points = (List<TemperaturePoint>)
        //                formatter.Deserialize(readerFileStream);
        //            // Close the readerFileStream when we are done
        //            readerFileStream.Close();

        //        }
        //        catch (Exception)
        //        {
        //            Console.WriteLine("There seems to be a file that contains " +
        //                "points information but somehow there is a problem " +
        //                "with reading it.");
        //        } // end try-catch
        //    } // end if
        //} // end public bool Load()

        // Write list of points to txt file.
        public void WriteToTxtFile(string filename)
        {
            StreamWriter streamWriter = new StreamWriter(filename);
            foreach (var point in this.points)
            {
                foreach(var element in point)
                {
                    streamWriter.WriteLine(element.ToString());
                }               
            }
            streamWriter.Close();
        }
    }
}
