using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DesignPattern._21Strategy
{
    public class Dog : DesignPattern._21Strategy.IMyComparable
    {
        public bool CompareTo(Dog o)
        {

            return true;
        }
    }
}
