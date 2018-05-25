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
        private DateTime timeBegin;

        public TemperaturePoint(double temperature, DateTime timeBegin)
        {
            this.temperature = temperature;
            this.timeBegin = timeBegin;
        }

        public double Temperature
        {
            get { return temperature; }
            set { temperature = value; }
        }
        public DateTime TimeBegin
        {
            get { return timeBegin; }
            set { timeBegin = value; }
        }

        public override string ToString()
        {
            return timeBegin.ToString()+"\t"+Temperature.ToString();
        }
    }
}
