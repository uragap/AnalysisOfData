using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class QVPoint
    {
        private DateTime dateBegin;
        private DateTime dateEnd;
        private double qDirect;
        private double qReverse;
        private double vDirect;
        private double vReverse;

        public QVPoint(DateTime dateBegin, DateTime dateEnd, double qDirect, double qReverse, 
            double vDirect, double vReverse)
        {
            this.dateBegin = dateBegin;
            this.dateEnd = dateEnd;
            this.qDirect = qDirect;
            this.qReverse = qReverse;
            this.vDirect = vDirect;
            this.vReverse = vReverse;
        }

        public double QDirect { get => qDirect; set => qDirect = value; }
        public double QReverse { get => qReverse; set => qReverse = value; }
        public double VDirect { get => vDirect; set => vDirect = value; }
        public double VReverse { get => vReverse; set => vReverse = value; }
        public DateTime DateBegin { get => dateBegin; set => dateBegin = value; }
        public DateTime DateEnd { get => dateEnd; set => dateEnd = value; }

        public override string ToString()
        {
            return DateBegin.ToString() + "\t" + DateEnd.ToString() + "\t" + qDirect.ToString() + "\t" +
                qReverse.ToString() + "\t" + vDirect.ToString() + "\t" + vReverse.ToString();
        }
    }
}
