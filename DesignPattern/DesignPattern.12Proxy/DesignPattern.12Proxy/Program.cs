using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DesignPattern._12Proxy
{
    class Program
    {
        static void Main(string[] args)
        {
            IMoveable moveObj = new TankTimeProxy();
            moveObj.Move();
            Console.WriteLine("End");
            Console.ReadLine();
        }
    }
}
