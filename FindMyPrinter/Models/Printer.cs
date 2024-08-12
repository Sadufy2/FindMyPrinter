using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindMyPrinter.Models
{
    public class Printer
    {
        public string IP { get; set; }
        public string Name { get; set; }

        public Printer(string ip, string name)
        {
            this.IP = ip;
            this.Name = name;
        }
    }
}
