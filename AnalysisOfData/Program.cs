using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Services;
using Storage;
using Entities;
using System.Collections;

namespace AnalysisOfData
{
    class Program
    {
        static private Dictionary<int,int[]> Dictionary = new Dictionary<int, int[]>();
        static private BuildingsStorage Buildings = new BuildingsStorage();
        static private int[] reverseEnergySensorID = {3,9,17,21,13,37};

        static void Main(string[] args)
        {
            //
            var TSourceFile = @"E:\my document\C#\Project C#\AnalysisOfData\Tdata.txt";
            var startTime = DateTime.Now;
            var tpStorage = new TemperaturePointStorage();
            tpStorage.ReadInitTxtFile(TSourceFile);
            Console.WriteLine("T file reading was finished. Time: " + (DateTime.Now - startTime).ToString());
            startTime = DateTime.Now;
            //
            var QVSourceFile = @"E:\my document\C#\Project C#\AnalysisOfData\QVdata.txt";
            var qvintgrStorage = new QVIntgrPointStorage();
            qvintgrStorage.ReadInitTxtFile(QVSourceFile);
            Console.WriteLine("QV file reading was finished. Time: " + (DateTime.Now - startTime).ToString());
            startTime = DateTime.Now;
            //
            var qvStorage = new QVPointStorage();
            qvStorage.InitQVPointStorage(qvintgrStorage);
            Console.WriteLine("QV file transformation was finished. Time: " + 
                (DateTime.Now - startTime).ToString());
            startTime = DateTime.Now;
            //
            var qvStFilename = @"E:\my document\C#\Project C#\AnalysisOfData\QVPointStorage.txt";
            var tpStFilename = @"E:\my document\C#\Project C#\AnalysisOfData\TemperaturePointStorage.txt";
            qvStorage.WriteToTxtFile(qvStFilename);
            tpStorage.WriteToTxtFile(tpStFilename);
            Console.WriteLine("Writing to files was finished. Time: " + (DateTime.Now - startTime).ToString());
            startTime = DateTime.Now;
            //          
            var bldStorage = new BuildingsStorage();
            bldStorage.InitBuildingStorage(qvStorage,tpStorage);
            Console.WriteLine("BldStorage creating was finished. Time: " + (DateTime.Now - startTime).ToString());
            startTime = DateTime.Now;
            //
            bldStorage.WriteToTxtFile();
            Console.WriteLine("BldStorage writing was finished. Time: " + 
                (DateTime.Now - startTime).ToString());

            Console.Read();
        }     
    }
}
