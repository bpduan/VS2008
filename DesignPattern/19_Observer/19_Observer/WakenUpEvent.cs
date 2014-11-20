using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace _19_Observer
{
    public class WakenUpEvent
    {
        public DateTime Time{get;set;}
        public string Location{get;set;}
        public Children Source{get;set;}
    }
}
