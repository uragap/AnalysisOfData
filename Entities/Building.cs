using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;


namespace Entities
{
    public class Building
    {
        //private int number;
        private List<QVTPoint> points = new List<QVTPoint>();
        

        public Building(int number, List<QVTPoint> points)
        {
            this.Number = number;
            this.Points = points;
        }

        public int Number { get; private set; }
        public List<QVTPoint> Points { get => points; set => points = value; }

        public void WriteToTxtFile(StreamWriter streamWriter)
        {            
            foreach(var point in points)
            {
                streamWriter.WriteLine(point.ToString());
            }
        }

    }
}
