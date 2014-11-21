using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DesignPattern._21Strategy
{
    public class Cat
    {
        public int Weight { get; set; }
        public int Height { get; set; }

        public override string ToString()
        {
            return "Cat:Height="+ Height + "Weight="+Weight+"   ";
            //return base.ToString("Cat:Height=" + Height + "Weight=" + Weight + "   ");
        }
    }
}
