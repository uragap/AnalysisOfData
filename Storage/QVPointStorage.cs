using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using Entities;

namespace Storage
{
    public class QVPointStorage
    {
        private BinaryFormatter formatter = new BinaryFormatter();
        private List<QVPoint>[] points = new List<QVPoint>[19] // index = building number-1
            {
                new List<QVPoint>(),
                new List<QVPoint>(),
                new List<QVPoint>(),
                new List<QVPoint>(),
                new List<QVPoint>(),
                new List<QVPoint>(),
                new List<QVPoint>(),
                new List<QVPoint>(),
                new List<QVPoint>(),
                new List<QVPoint>(),
                new List<QVPoint>(),
                new List<QVPoint>(),
                new List<QVPoint>(),
                new List<QVPoint>(),
                new List<QVPoint>(),
                new List<QVPoint>(),
                new List<QVPoint>(),
                new List<QVPoint>(),
                new List<QVPoint>()
            }; // index = building number-1
        // [buildingsID] {qdirect,qrevers,vdirect,vrevers,t}
        private int[][] buildingSensorTable = new int[19][]
        {
            new int[] {1,3,2,4,2},
            new int[] {5,0,6,0,24},
            new int[] {7,9,8,10,47},
            new int[] {15,17,16,18,0},
            new int[] {0,0,0,0,218},
            new int[] {19,21,20,22,228},
            new int[] {0,0,0,0,238},
            new int[] {11,13,12,14,79},
            new int[] {0,0,0,0},// 9ый, которого нет
            new int[] { 35, 37, 36, 38 },
            new int[] {33,0,34,0,0},
            new int[] {0,0,0,0,0},// 12ый, который доделать, пока так
            new int[] {23,0,24,0,0},
            new int[] {41,0,42,48,289},
            new int[] {43,0,44,49,307},
            new int[] {39,0,40,0,266},
            new int[] {62,0,63,0,0},
            new int[] {65,0,66,0,0},
            new int[] {68,0,69,0,0}
        };

        public void InitQVPointStorage(QVIntgrPointStorage qVIntgrPointStorage)
        {
            for(int i=0;i<19;i++)
            {
                var qDirect = qVIntgrPointStorage.GetPointsInNormalForm(buildingSensorTable[i][0]);
                var qReverse = qVIntgrPointStorage.GetPointsInNormalForm(buildingSensorTable[i][1]);
                var vDirect = qVIntgrPointStorage.GetPointsInNormalForm(buildingSensorTable[i][2]);
                var vReverse = qVIntgrPointStorage.GetPointsInNormalForm(buildingSensorTable[i][3]);
                if(qDirect.Count == qReverse.Count && vDirect.Count == vReverse.Count && 
                    qDirect.Count == vDirect.Count)
                {
                    for(int k=0;k<qDirect.Count;k++)
                    {
                        points[i].Add(new QVPoint(qDirect[k].DateBegin, qDirect[k].DateEnd, qDirect[k].Value,
                            qReverse[k].Value, vDirect[k].Value, vReverse[k].Value));
                    }
                }
                else
                {
                    while(qDirect.Count!=qReverse.Count)
                    {
                        qReverse.Add(new QVIntgrPoint(0, DateTime.MinValue, DateTime.MinValue));
                    }
                    while (qDirect.Count != vReverse.Count)
                    {
                        vReverse.Add(new QVIntgrPoint(0, DateTime.MinValue, DateTime.MinValue));
                    }
                    for (int k = 0; k < qDirect.Count; k++)
                    {
                        points[i].Add(new QVPoint(qDirect[k].DateBegin, qDirect[k].DateEnd, qDirect[k].Value,
                            qReverse[k].Value, vDirect[k].Value, vReverse[k].Value));
                    }
                }
            }
        }
        public void WriteToTxtFile(string filename)
        {
            StreamWriter streamWriter = new StreamWriter(filename);
            foreach (var point in this.points)
            {
                foreach (var element in point)
                {
                    streamWriter.WriteLine(element.ToString());
                }
            }
            streamWriter.Close();
        }
        public List<QVPoint> GetQVPoints(int buildingNumber)
        {
            return points[buildingNumber];
        }
    }
}
