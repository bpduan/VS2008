using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace _0_ThinkingOO
{
    public class Plane : IVehicle
    {
        public void Go(Address address)
        {
            Console.WriteLine("һ·���ų��ȥ��" + address.Name);
        }
    }
}
