using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    [Serializable]
    public class QVTPoint
    {
        private DateTime dateBegin;
        private DateTime dateEnd;
        private double qDirect;
        private double qReverse;
        private double vDirect;
        private double vReverse;
        private double temperature;

        public QVTPoint(DateTime dateBegin, DateTime dateEnd, double qDirect, double qReverse, 
            double vDirect, double vReverse, double temperature)
        {
            this.DateBegin = dateBegin;
            this.DateEnd = dateEnd;
            this.QDirect = qDirect;
            this.QReverse = qReverse;
            this.VDirect = vDirect;
            this.VReverse = vReverse;
            this.Temperature = temperature;
        }
        public QVTPoint(QVPoint qVPoint, TemperaturePoint temperaturePoint)
        {
            this.DateBegin = qVPoint.DateBegin;
            this.DateEnd = qVPoint.DateEnd;
            this.QDirect = qVPoint.QDirect;
            this.QReverse = qVPoint.QReverse;
            this.VDirect = qVPoint.VDirect;
            this.VReverse = qVPoint.VReverse;
            this.Temperature = temperaturePoint.Temperature;
        }

        public DateTime DateBegin { get => dateBegin; set => dateBegin = value; }
        public DateTime DateEnd { get => dateEnd; set => dateEnd = value; }
        public double QDirect { get => qDirect; set => qDirect = value; }
        public double QReverse { get => qReverse; set => qReverse = value; }
        public double VDirect { get => vDirect; set => vDirect = value; }
        public double VReverse { get => vReverse; set => vReverse = value; }
        public double Temperature { get => temperature; set => temperature = value; }

        public override string ToString()
        {
            return dateBegin.ToString()+"\t"+dateEnd.ToString() + "\t"+qDirect.ToString() + "\t"+
                qReverse.ToString() + "\t"+vDirect.ToString() + "\t"+vReverse.ToString() + "\t"+
                temperature.ToString();
        }


    }
}
