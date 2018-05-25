using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    [Serializable]
    public class QVIntgrPoint
    {
        private double value;
        private DateTime dateBegin;
        private DateTime dateEnd;

        public QVIntgrPoint(double value, DateTime dateBegin, DateTime dateEnd)
        {
            this.value = value;
            this.dateBegin = dateBegin;
            this.dateEnd = dateEnd;
        }

        public double Value
        {
            get { return value; }
            set { this.value = value; }
        }
        public DateTime DateBegin
        {
            get { return dateBegin; }
            set { dateBegin = value; }
        }
        public DateTime DateEnd
        {
            get { return dateEnd; }
            set { dateEnd = value; }
        }

        public override string ToString()
        {
            return dateBegin.ToString()+"\t"+dateEnd.ToString()+"\t"+value.ToString();
        }
    }
}
