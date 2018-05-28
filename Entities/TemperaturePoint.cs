using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    [Serializable]
    public class TemperaturePoint
    {
        private double temperature;
        private DateTime dateBegin;

        public TemperaturePoint(double temperature, DateTime timeBegin)
        {
            this.temperature = temperature;
            this.dateBegin = timeBegin;
        }

        public double Temperature
        {
            get { return temperature; }
            set { temperature = value; }
        }
        public DateTime TimeBegin
        {
            get { return dateBegin; }
            set { dateBegin = value; }
        }

        public override string ToString()
        {
            return dateBegin.ToString()+"\t"+Temperature.ToString();
        }
    }
}
