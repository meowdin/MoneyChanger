using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MoneyChangrer.Martin.Sun.Models
{
    public abstract class Currency
    {
       public int id { get; set; }
        public string name { get; set; }
    }
}
