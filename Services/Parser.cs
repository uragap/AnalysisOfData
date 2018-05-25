using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    static public class Parser
    {
        static public string[] ParseString(string str)
        {
            return str.Split('\t');
        }
    }
}
