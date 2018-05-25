using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Collections;
using System.Globalization;
using Services;


namespace Storage
{
    public class QVIntgrPointStorage
    {
        private BinaryFormatter formatter = new BinaryFormatter();
        private List<QVIntgrPoint>[] points = new List<QVIntgrPoint>[69]
            {
                new List<QVIntgrPoint>(),
                new List<QVIntgrPoint>(),
                new List<QVIntgrPoint>(),
                new List<QVIntgrPoint>(),
                new List<QVIntgrPoint>(),
                new List<QVIntgrPoint>(),
                new List<QVIntgrPoint>(),
                new List<QVIntgrPoint>(),
                new List<QVIntgrPoint>(),
                new List<QVIntgrPoint>(),
                new List<QVIntgrPoint>(),
                new List<QVIntgrPoint>(),
                new List<QVIntgrPoint>(),
                new List<QVIntgrPoint>(),
                new List<QVIntgrPoint>(),
                new List<QVIntgrPoint>(),
                new List<QVIntgrPoint>(),
                new List<QVIntgrPoint>(),
                new List<QVIntgrPoint>(),
                new List<QVIntgrPoint>(),
                new List<QVIntgrPoint>(),
                new List<QVIntgrPoint>(),
                new List<QVIntgrPoint>(),
                new List<QVIntgrPoint>(),
                new List<QVIntgrPoint>(),
                new List<QVIntgrPoint>(),
                new List<QVIntgrPoint>(),
                new List<QVIntgrPoint>(),
                new List<QVIntgrPoint>(),
                new List<QVIntgrPoint>(),
                new List<QVIntgrPoint>(),
                new List<QVIntgrPoint>(),
                new List<QVIntgrPoint>(),
                new List<QVIntgrPoint>(),
                new List<QVIntgrPoint>(),
                new List<QVIntgrPoint>(),
                new List<QVIntgrPoint>(),
                new List<QVIntgrPoint>(),
                new List<QVIntgrPoint>(),
                new List<QVIntgrPoint>(),
                new List<QVIntgrPoint>(),
                new List<QVIntgrPoint>(),
                new List<QVIntgrPoint>(),
                new List<QVIntgrPoint>(),
                new List<QVIntgrPoint>(),
                new List<QVIntgrPoint>(),
                new List<QVIntgrPoint>(),
                new List<QVIntgrPoint>(),
                new List<QVIntgrPoint>(),
                new List<QVIntgrPoint>(),
                new List<QVIntgrPoint>(),
                new List<QVIntgrPoint>(),
                new List<QVIntgrPoint>(),
                new List<QVIntgrPoint>(),
                new List<QVIntgrPoint>(),
                new List<QVIntgrPoint>(),
                new List<QVIntgrPoint>(),
                new List<QVIntgrPoint>(),
                new List<QVIntgrPoint>(),
                new List<QVIntgrPoint>(),
                new List<QVIntgrPoint>(),
                new List<QVIntgrPoint>(),
                new List<QVIntgrPoint>(),
                new List<QVIntgrPoint>(),
                new List<QVIntgrPoint>(),
                new List<QVIntgrPoint>(),
                new List<QVIntgrPoint>(),
                new List<QVIntgrPoint>(),
                new List<QVIntgrPoint>()
            }; // index = sensorid-1
        static private int[] badSensor = { 70, 67, 64, 45, 46, 47, 3, 9, 17, 21, 13};

        //
        public void ReadInitTxtFile(string filename)
        {
            CultureInfo provider = CultureInfo.InvariantCulture;
            var format = "yyyy-MM-dd HH:mm:ss";
            if (filename.Contains("QVdata"))
            {
                // DateTime.MinValue = 01.01.0001 00:00:00
                DateTime lastDate = DateTime.MinValue;
                int lastID = 0;
                foreach (var str in File.ReadAllLines(filename))
                {
                    string[] split = Parser.ParseString(str);
                    if(!badSensor.Contains(Convert.ToInt32(split[0])))
                    {
                        split[2] = split[2].Replace('.', ',');
                        if (lastID == Convert.ToInt32(split[0]))
                        {
                            var qvintgrPoint = new QVIntgrPoint(Convert.ToDouble(split[2]), lastDate,
                                DateTime.ParseExact(split[1], format, provider));
                            points[Convert.ToInt32(split[0])-1].Add(qvintgrPoint);
                        }
                        lastID = Convert.ToInt32(split[0]);
                        lastDate = DateTime.ParseExact(split[1], format, provider);
                    }                   
                }
            }
            else
                Console.WriteLine("Choose correct file.");
        }

        //change -1 later
        //static public double GetEnergy(DateTime dateTime, int sensorID)
        //{
        //    if(sensorID==0)
        //    {
        //        return -1;
        //    }
        //    else
        //    {
        //        foreach (var point in Points)
        //        {
        //            if (point.SensorId == sensorID
        //                && point.DateTime.Date == dateTime.Date && point.DateTime.Hour == dateTime.Hour)
        //            {
        //                return point.Energy;
        //            }
        //        }
        //        return -1;
        //    }        
        //}

        // Return list of point.
        public List<QVIntgrPoint> GetPointsInNormalForm(int sensorID)
        {
            if(sensorID==0)
            {
                return new List<QVIntgrPoint>();
            }
            else
            {
                var points = this.points[sensorID - 1];
                double lastValue = 0;
                foreach (var point in points)
                {
                    double temp = point.Value;
                    if (lastValue > point.Value)
                    {
                        point.Value = 0;
                        lastValue = temp;
                    }
                    else
                    {
                        point.Value -= lastValue;
                        lastValue = temp;
                    }
                }
                return points;
            }          
        }


        // Save list of points to binary file.
        //static public void Save(string dataFilename)
        //{
        //    // Gain code access to the file that we are going
        //    // to write to
        //    try
        //    {
        //        // Create a FileStream that will write data to file.
        //        FileStream writerFileStream =
        //            new FileStream(dataFilename, FileMode.Create, FileAccess.Write);
        //        BinaryWriter wrt = new BinaryWriter(writerFileStream);
        //        // Save our list of points to file
        //        wrt.Write(Points.Count);

        //        BinaryReader rdr = new BinaryReader(writerFileStream);
        //        int cnt = rdr.ReadInt32();
        //        Points.Capacity = cnt;

        //        writerFileStream.Write(BitConverter.GetBytes(Points.Count), 0, sizeof(int));
        //        formatter.Serialize(writerFileStream, Points);
        //        // Close the writerFileStream when we are done.
        //        writerFileStream.Close();
        //    }
        //    catch (Exception)
        //    {
        //        Console.WriteLine("Unable to save our points information");
        //    } // end try-catch
        //} 

        // Load list of points from binary file. 
        //static public void Load(string dataFilename)
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
        //            Points = (List<QVIntgrPoint>)
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
        //} 

        // Write list of points to txt file.
        //static public bool WriteToTxtFile(string filename)
        //{
            
        //    StreamWriter streamWriter = new StreamWriter(filename);
        //    foreach (var element in Points)
        //    {
        //        streamWriter.WriteLine(element.ToString());
        //    }
        //    streamWriter.Close();
        //    return true;
        //}

    }
}
