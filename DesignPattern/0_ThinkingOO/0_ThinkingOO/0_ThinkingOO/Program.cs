using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace _0_ThinkingOO
{
    class Program
    {
        static void Main(string[] args)
        {
            Driver d = new Driver() { Name="老张"};
            d.Drive(new Car());
            d.Drive(new Plane());
            Console.ReadLine();
        }
    }
}
