using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace _0_ThinkingOO
{
    public class Car : IVehicle 
    {
        public void Go(Address address)
        {
            Console.WriteLine("一路哼着歌去" + address.Name);
        }
    }
}
